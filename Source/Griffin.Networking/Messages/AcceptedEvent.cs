using Griffin.Networking.Channels;

namespace Griffin.Networking.Messages
{
    internal class AcceptedEvent : IServerEvent
    {
        public IChannel ClientChannel { get; set; }
    }
}