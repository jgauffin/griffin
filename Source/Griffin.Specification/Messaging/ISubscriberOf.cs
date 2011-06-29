namespace Griffin.Messaging
{
    /// <summary>
    /// Implemented by classes that want to receive a certain message
    /// </summary>
    /// <typeparam name="T">Type of message to receive</typeparam>
    public interface ISubscriberOf<T> where T : IMessage
    {
        /// <summary>
        /// Process the message that arrived.
        /// </summary>
        /// <param name="message">Message to process</param>
        /// <remarks>
        /// Exceptions may be thrown, but will not abort processing by other subscribers.
        /// </remarks>
        void ProcessMessage(T message);
    }
}
