using System;
using Griffin.Core.Net.Buffers;
using Griffin.Core.Net.Handlers;
using Griffin.Core.Net.Messages;
using Griffin.Core.Net.Protocols.Http.Implementation;

namespace Griffin.Core.Net.Protocols.Http
{
    /// <summary>
    /// A HTTP parser using delegates to switch parsing methods.
    /// </summary>
    public class Decoder : IUpstreamHandler
    {
        [ThreadStatic]
        private static Context _context;

        private class Context
        {
            public readonly BufferSliceReader _reader = new BufferSliceReader();
            public int _bodyBytesLeft;
            public BufferSlice _buffer;
            public string _headerName;
            public string _headerValue;
            public Func<bool> _parserMethod;
            public HttpMessage _message;
            public bool _isComplete;

            public HttpRequest Request = new HttpRequest();
            public HttpResponse Response = new HttpResponse();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Decoder"/> class.
        /// </summary>
        public Decoder()
        {
            _context._parserMethod = ParseFirstLine;
        }

        /// <summary>
        /// Parser method to copy all body bytes.
        /// </summary>
        /// <returns></returns>
        /// <remarks>Needed since a TCP packet can contain multiple messages
        /// after each other, or partial messages.</remarks>
        private bool ParseBody()
        {
            if (_context._reader.RemainingLength == 0)
                return false;

            int bytesLeft = (int)Math.Min(_context._message.ContentLength - _context._message.Body.Length, _context._buffer.RemainingCount);
            _context._message.Body.Write(_context._buffer.Buffer, _context._buffer.CurrentOffset, bytesLeft);
            _context._buffer.CurrentOffset += bytesLeft;

            _context._isComplete = _context._message.Body.Length == _context._message.ContentLength;

            // we have either:
            // A) read part of the buffer (since body is completed) 
            // B) read the entire buffer (and we can therefore not read more)
            return false;
        }

        /// <summary>
        /// Try to find a header name.
        /// </summary>
        /// <returns></returns>
        private bool GetHeaderName()
        {
            // empty line. body is begining.
            if (_context._reader.Current == '\r' && _context._reader.Peek == '\n')
            {
                // Eat the line break
                _context._reader.Consume('\r', '\n');

                // Don't have a body?
                if (_context._bodyBytesLeft == 0)
                {
                    _context._isComplete = true;
                    _context._parserMethod = ParseFirstLine;
                }
                else
                    _context._parserMethod = ParseBody;

                return true;
            }

            _context._headerName = _context._reader.ReadUntil(':');
            if (_context._headerName == null)
                return false;

            _context._reader.Consume(); // eat colon
            _context._parserMethod = GetHeaderValue;
            return true;
        }

        /// <summary>
        /// Get header values.
        /// </summary>
        /// <returns></returns>
        /// <remarks>Will also look for multi header values and automatically merge them to one line.</remarks>
        /// <exception cref="BadRequestException">Content length is not a number.</exception>
        private bool GetHeaderValue()
        {
            // remove white spaces.
            _context._reader.Consume(' ', '\t');

            // multi line or empty value?
            if (_context._reader.Current == '\r' && _context._reader.Peek == '\n')
            {
                _context._reader.Consume('\r', '\n');

                // empty value.
                if (_context._reader.Current != '\t' && _context._reader.Current != ' ')
                {
                    OnHeader(_context._headerName, string.Empty);
                    _context._headerName = null;
                    _context._headerValue = string.Empty;
                    _context._parserMethod = GetHeaderName;
                    return true;
                }

                if (_context._reader.RemainingLength < 1)
                    return false;

                // consume one whitespace
                _context._reader.Consume();

                // and fetch the rest.
                return GetHeaderValue();
            }

            string value = _context._reader.ReadLine();
            if (value == null)
                return false;

            _context._headerValue += value;
            if (string.Compare(_context._headerName, "Content-Length", true) == 0)
            {
                if (!int.TryParse(value, out _context._bodyBytesLeft))
                    throw new BadRequestException("Content length is not a number.");
            }

            OnHeader(_context._headerName, value);
            
            _context._headerName = null;
            _context._headerValue = string.Empty;
            _context._parserMethod = GetHeaderName;
            return true;
        }

        /// <summary>
        /// First message line.
        /// </summary>
        /// <param name="words">Will always contain three elements.</param>
        /// <exception cref="BadRequestException"><c>BadRequestException</c>.</exception>
        protected virtual void OnFirstLine(string[] words)
        {
            string firstWord = words[0].ToUpper();

            _context._message = firstWord.StartsWith("HTTP") ? (HttpMessage)_context.Response : _context.Request;
            _context._message.SetFirstLine(words[0], words[1], words[2]);
        }

        private void OnHeader(string name, string value)
        {
            _context._message.Add(name, value);
        }

        /// <summary>
        /// Parses the first line in a request/response.
        /// </summary>
        /// <returns><c>true</c> if first line is well formatted; otherwise <c>false</c>.</returns>
        /// <exception cref="BadRequestException">Invalid request/response line.</exception>
        private bool ParseFirstLine()
        {
            _context._reader.Consume('\r', '\n');

            // Do not contain a complete first line.
            if (!_context._reader.Contains('\n'))
                return false;

            var words = new string[3];
            words[0] = _context._reader.ReadUntil(' ');
            _context._reader.Consume(); // eat delimiter
            words[1] = _context._reader.ReadUntil(' ');
            _context._reader.Consume(); // eat delimiter
            words[2] = _context._reader.ReadLine();
            if (string.IsNullOrEmpty(words[0])
                || string.IsNullOrEmpty(words[1])
                || string.IsNullOrEmpty(words[2]))
                throw new BadRequestException("Invalid request/response line.");

            OnFirstLine(words);
            _context._parserMethod = GetHeaderName;
            return true;
        }

        /// <summary>
        /// Reset parser to initial state.
        /// </summary>
        public void Reset()
        {
            _context._headerValue = null;
            _context._headerName = string.Empty;
            _context._bodyBytesLeft = 0;
            _context._parserMethod = ParseFirstLine;
        }


        /// <summary>
        /// Gets if this pipeline can be shared between multiple channels
        /// </summary>
        /// <remarks>
        /// Return <c>false</c> if you have member variables.
        /// </remarks>
        public bool IsSharable
        {
            get { return false; }
        }

        /// <summary>
        /// Process data that was received from the channel.
        /// </summary>
        /// <param name="ctx">Context which is used for the currently processed channel</param>
        /// <param name="e">Event being processed.</param>
        public void HandleUpstream(IChannelHandlerContext ctx, IChannelEvent e)
        {
            if (e is ConnectedEvent)
            {
                ctx.State = new Context();
            }
            if (e is ClosedEvent)
            {
                Reset();
            }
            else if (e is MessageEvent)
            {
                _context = ctx.State.As<Context>();
                _context._buffer = e.As<MessageEvent>().Message.As<BufferSlice>();
                _context._reader.Assign(_context._buffer);
                while (_context._parserMethod()) ;
            
                if (_context._isComplete)
                {
                    e.As<MessageEvent>().Message = _context._message;
                    OnComplete(ctx, e);
                }
                return;
            }

            ctx.SendUpstream(e);
        }

        /// <summary>
        /// Called when a message has been parsed successfully.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="channelEvent"></param>
        protected virtual void OnComplete(IChannelHandlerContext ctx, IChannelEvent channelEvent)
        {
            ctx.SendUpstream(channelEvent);
        }
    }
}