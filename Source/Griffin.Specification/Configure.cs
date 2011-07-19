namespace Griffin
{
    /// <summary>
    /// Base class for fluent configuration of Griffin framework (or others)
    /// </summary>
    /// <remarks>
    /// It should be easy to find the proper fluent interface for configurations. Therefore
    /// you can use this class to help finding all configuration options that are available for your
    /// framework. Simply create an extension method which uses <c>Configure</c> as instance class. Make
    /// sure that your extension method is placed in the "Griffin" namespace and not somewhere else. But keep
    /// all configuration classes in the proper namespace.
    /// </remarks>
    public class Configure
    {
        private static readonly Configure Instance = new Configure();

        private Configure()
        {
        }

        /// <summary>
        /// Get the starting point for fluent configuration.
        /// </summary>
        public static Configure Griffin
        {
            get { return Instance; }
        }
    }
}