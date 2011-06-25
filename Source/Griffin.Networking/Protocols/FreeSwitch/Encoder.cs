using System;
using System.IO;
using System.Text;
using Griffin.Core;
using Griffin.Networking.Buffers;
using Griffin.Networking.Handlers;
using Griffin.Networking.Messages;

namespace Griffin.Networking.Protocols.FreeSwitch
{
    public class Encoder : IDownstreamHandler
    {
        private static readonly byte[] LineFeed = Encoding.ASCII.GetBytes("\n");
        private MemoryStream _stream = new MemoryStream();

        public bool IsSharable
        {
            get { return true; }
        }

        #region IDownstreamHandler Members

        /// <summary>
        /// Takes a <see cref="Message"/> and converts it into a byte buffer.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="e"></param>
        public void HandleDownstream(IChannelHandlerContext ctx, IChannelEvent e)
        {
            if (e is MessageEvent)
            {
                var evt = e.As<MessageEvent>();
                if (evt.Message is Message)
                    evt.Message = EncodeMessage(evt.Message.As<Message>());
                else if (evt.Message is Command)
                    evt.Message = EncodeCommand(evt.Message.As<Command>());
            }

            ctx.SendDownstream(e);
        }

        #endregion

        private BufferSlice EncodeCommand(Command command)
        {
            byte[] cmd = Encoding.ASCII.GetBytes(command.BuildCommandString() + "\n\n");
            Console.WriteLine("Sending " + command.BuildCommandString() + "\n\n");
            return new BufferSlice(cmd, 0, cmd.Length);
        }

        private BufferSlice EncodeMessage(Message msg)
        {
            if (msg.Body.Length != 0)
                msg.Headers["Content-Length"] = msg.Body.Length.ToString();

            var buffer = new byte[65535];
            long length = 0;
            using (var stream = new MemoryStream(buffer))
            {
                stream.SetLength(0);
                using (var writer = new StreamWriter(stream))
                {
                    foreach (string key in msg.Headers)
                    {
                        writer.Write("{0}: {1}\n".FormatWith(key, msg.Headers[key]));
                    }
                    writer.Write("\n");
                    msg.Body.CopyTo(stream);

                    writer.Flush();
                    length = stream.Length;
                }
            }

            string tmp = Encoding.ASCII.GetString(buffer, 0, (int) length);
            return new BufferSlice(buffer, 0, (int) length);
        }
    }
}