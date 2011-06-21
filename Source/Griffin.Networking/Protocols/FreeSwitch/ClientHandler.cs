using System;
using System.Collections.Concurrent;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using Griffin.Core.Net.Buffers;
using Griffin.Core.Net.Channels;
using Griffin.Core.Net.Handlers;
using Griffin.Core.Net.Messages;
using Griffin.Core.Net.Services;

namespace Griffin.Core.Net.Protocols.FreeSwitch
{
    public class FreeSwitchClientService : ClientService
    {
        private readonly string _password;
        ConcurrentStack<Command> _commands = new ConcurrentStack<Command>();
        ConcurrentDictionary<string, BackgroundCommand> _waitingCommands = new ConcurrentDictionary<string, BackgroundCommand>();

        public FreeSwitchClientService(string password)
        {
            _password = password;
        }


        protected override void ExceptionCaught(IChannelHandlerContext ctx, ExceptionEvent e)
        {
            throw e.Exception;
        }

        protected override void HandleMessage(IChannelHandlerContext ctx, MessageEvent e)
        {
            var msg = (Message)e.Message;
            var contentType = msg.Headers["Content-Type"];


            var reader = new StreamReader(msg.Body);
            var body = reader.ReadToEnd();
            msg.Body.Position = 0;

            switch (contentType)
            {
                case "auth/request":
                    SendCommand("auth", _password);
                    break;
                case "command/reply":
                    {
                        Command cmd;
                        if (!_commands.TryPop(out cmd))
                            throw new InvalidOperationException("Failed to find a command for the recieved command/reply");
                        OnCommand(cmd, msg);
                    }
                    
                    break;
                case "text/event-plain":
                    ParseEvent(msg.Body);
                    break;
                default:
                    break;
            }
        }

        private void ParseEvent(Stream body)
        {
            string line = "";
            NameValueCollection lines = new NameValueCollection();
            StreamReader reader = new StreamReader(body);
            while ((line = reader.ReadLine()) != null)
            {
                if (line == string.Empty)
                    break;

                var parts = line.Split(':');
                lines.Add(parts[0], parts[1].Trim());
            }

            OnEvent(lines);
        }

        protected void OnEvent(NameValueCollection lines)
        {
            Console.WriteLine("Received: " + lines["Event-Name"]);
        }

        public void SendBackgroundCommand(string command, params string[] arguments)
        {
            var cmd = new BackgroundCommand(new AnyCommand(command, arguments));
            _commands.Push(cmd);
            SendDownstream(new MessageEvent(cmd));
        }

        public void SendCommand(string command, params string[] arguments)
        {
            var cmd = new AnyCommand(command, arguments);
            _commands.Push(cmd);
            SendDownstream(new MessageEvent(cmd));
        }

        private void OnCommand(Command cmd, Message reply)
        {
            switch (cmd.CommandName)
            {
                case "auth":
                    SendCommand("events", "plain", "all");
                    break;
                case "events":
                    //SendBackgroundCommand("status");
                    break;
                case "bgapi":
                    HandleBgApiResponse(cmd, reply);
                    break;
                default:
                    Console.WriteLine("Unknown command: " + cmd.CommandName);
                    break;
            }
        }

        private void HandleBgApiResponse(Command cmd, Message reply)
        {
            var uid = reply.Headers["Job-UUID"];
            cmd.As<BackgroundCommand>().JobId = uid;
            _waitingCommands[uid] = cmd.As<BackgroundCommand>();
        }
    }
}