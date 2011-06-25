using System;
using System.Collections.Concurrent;
using System.Collections.Specialized;
using System.IO;
using Griffin.Core;
using Griffin.Networking.Handlers;
using Griffin.Networking.Messages;
using Griffin.Networking.Services;

namespace Griffin.Networking.Protocols.FreeSwitch
{
    public class FreeSwitchClientService : ClientService
    {
        private readonly ConcurrentStack<Command> _commands = new ConcurrentStack<Command>();
        private readonly string _password;

        private readonly ConcurrentDictionary<string, BackgroundCommand> _waitingCommands =
            new ConcurrentDictionary<string, BackgroundCommand>();

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
            var msg = (Message) e.Message;
            string contentType = msg.Headers["Content-Type"];


            var reader = new StreamReader(msg.Body);
            string body = reader.ReadToEnd();
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
                            throw new InvalidOperationException(
                                "Failed to find a command for the recieved command/reply");
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
            var lines = new NameValueCollection();
            var reader = new StreamReader(body);
            while ((line = reader.ReadLine()) != null)
            {
                if (line == string.Empty)
                    break;

                string[] parts = line.Split(':');
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
            string uid = reply.Headers["Job-UUID"];
            cmd.As<BackgroundCommand>().JobId = uid;
            _waitingCommands[uid] = cmd.As<BackgroundCommand>();
        }
    }
}