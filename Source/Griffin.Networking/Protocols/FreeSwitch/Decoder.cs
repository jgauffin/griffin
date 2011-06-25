using System;
using System.Text;
using Griffin.Core;
using Griffin.Networking.Buffers;
using Griffin.Networking.Handlers;
using Griffin.Networking.Messages;

namespace Griffin.Networking.Protocols.FreeSwitch
{
    internal class Decoder : IUpstreamHandler
    {
        [ThreadStatic] private static DecoderContext _context;

        public bool IsSharable
        {
            get { return false; }
        }

        #region IUpstreamHandler Members

        public void HandleUpstream(IChannelHandlerContext ctx, IChannelEvent e)
        {
            if (e is ConnectedEvent)
            {
                ctx.State = new DecoderContext
                                {
                                    ParserMethod = ParseBeforeHeader
                                };
            }
            else if (e is ClosedEvent)
            {
                ctx.State = null;
            }
            if (!(e is MessageEvent))
            {
                ctx.SendUpstream(e);
                return;
            }


            var evt = e.As<MessageEvent>();
            _context = ctx.State.As<DecoderContext>();

            var buffer = evt.Message.As<BufferSlice>();
            _context.Reader.Assign(buffer);


            try
            {
                while (_context.ParserMethod()) ;
            }
            catch (Exception err)
            {
                ctx.SendUpstream(new ExceptionEvent(err));
                return;
            }

            if (!_context.IsComplete)
                return; // no more processing

            evt.Message = _context.Message;
            _context.ParserMethod = ParseBeforeHeader;
            ctx.SendUpstream(evt);
            _context.Message.Reset();
        }

        #endregion

        private bool ParseBeforeHeader()
        {
            while (_context.Reader.Peek == '\n')
                _context.Reader.Consume();

            _context.ParserMethod = ParseHeaderName;
            return true;
        }

        private bool ParseBody()
        {
            if (_context.Message.ContentLength <= 0)
            {
                _context.IsComplete = true;
                return false;
            }

            int bytesLeft = _context.Message.ContentLength - (int) _context.Message.Body.Length;
            int count = Math.Min(bytesLeft, _context.Reader.RemainingLength);
            _context.Message.Append(_context.Reader.Buffer, _context.Reader.Index, count);
            _context.IsComplete = _context.Message.Body.Length == _context.Message.ContentLength;
            _context.Reader.Index += count;
            if (_context.IsComplete)
                _context.Message.Body.Position = 0;
            return false;
        }

        private bool ParseHeaderName()
        {
            // empty line == go for body.
            if (_context.Reader.Current == '\n')
            {
                _context.Reader.Consume();
                _context.ParserMethod = ParseBody;
                return true;
            }

            _context.Reader.ConsumeWhiteSpaces();
            _context.CurrentHeaderName = _context.Reader.ReadUntil(": ");
            if (_context.CurrentHeaderName == null)
                return false;

            if (_context.Reader.Current != ':')
            {
                _context.Reader.ConsumeWhiteSpaces();
                _context.Reader.ReadUntil(':');
            }
            _context.Reader.Consume(':');

            _context.ParserMethod = ParseHeaderValue;
            return true;
        }

        private bool ParseHeaderValue()
        {
            _context.Reader.ConsumeWhiteSpaces();
            string value = _context.Reader.ReadLine();
            if (value == null)
                return false;

            _context.Message.Headers.Add(_context.CurrentHeaderName, value);
            _context.CurrentHeaderName = null;

            _context.ParserMethod = ParseHeaderName;
            return true;
        }

        public void Reset()
        {
            _context.Message.Reset();
            _context.ParserMethod = ParseBeforeHeader;
            _context.IsComplete = false;
            _context.CurrentHeaderName = null;
        }

        #region Nested type: DecoderContext

        private class DecoderContext
        {
            public DecoderContext()
            {
                Message = new Message();
                CurrentHeaderName = "";
                IsComplete = false;
                Reader = new BufferSliceReader();
            }

            public Encoding Encoding { get; set; }

            public Message Message { get; set; }
            public BufferSliceReader Reader { get; set; }
            public string CurrentHeaderName { get; set; }
            public bool IsComplete { get; set; }
            public ParserMethod ParserMethod { get; set; }
        }

        #endregion

        #region Nested type: ParserMethod

        private delegate bool ParserMethod();

        #endregion
    }
}