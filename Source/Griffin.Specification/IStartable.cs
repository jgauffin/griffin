namespace Griffin
{
    /// <summary>
    /// Implemented by services that can be started.
    /// </summary>
    public interface IStartable
    {
        /// <summary>
        /// Start the service
        /// </summary>
        /// <param name="context">The context is implemented by each application that are using the framework</param>
        /// <seealso cref="EmptyStartableContext"/>
        void Start(IStartableContext context);

        /// <summary>
        /// Stop the service
        /// </summary>
        void Stop();
    }
}