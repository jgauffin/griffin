namespace Griffin.Networking.Messages
{
    public class EventFactory
    {
        public T Create<T>() where T : class, IChannelEvent
        {
            return null;
        }

        public void Release<T>(T value) where T : IChannelEvent
        {
        }
    }
}