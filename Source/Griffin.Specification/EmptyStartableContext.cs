namespace Griffin
{
    /// <summary>
    /// Use this class if you do not have your own context
    /// </summary>
    /// <seealso cref="IStartableContext"/>
    /// <seealso cref="IStartable"/>
    public sealed class EmptyStartableContext : IStartableContext
    {
        private static readonly EmptyStartableContext _instance = new EmptyStartableContext();

        private EmptyStartableContext()
        {
        }

        /// <summary>
        /// Get current instance
        /// </summary>
        public static EmptyStartableContext Instance
        {
            get { return _instance; }
        }
    }
}