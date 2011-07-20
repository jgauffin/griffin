namespace Griffin
{
    /// <summary>
    /// Base class for fluent configuration of Griffin framework (or others)
    /// </summary>
    /// <remarks>
    /// <para>
    /// It should be easy to find the proper fluent interface for configurations. Therefore
    /// you can use this class to help finding all configuration options that are available for your
    /// framework. Simply create an extension method which uses <c>Configure</c> as instance class. Make
    /// sure that your extension method is placed in the "Griffin" namespace and not somewhere else. But keep
    /// all configuration classes in the proper namespace.
    /// </para>
    /// <para>
    /// When doing fluent interfaces, please DO use multiple classes. It makes it much easier to
    /// understand how a component should be configured. Think of it as a TreeView where you can
    /// go deeper to configure certain details. Use a property called <c>Done</c> to mark that a subdetail
    /// configuration has been completed (if there are no natural way to complete a sub configuration).
    /// </para>
    /// <para>
    /// Also add a method called <c>Build()</c> or similar to indicate that the whole configuration as been 
    /// completed.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// Configure.Griffin.Logging() // &gt;-- this is an extension method
    ///    .LogNamespace("Griffin.Logging.Tests").AndSubNamespaces.ToTargetNamed("Console")
    ///    .AddTarget("Console").As.ConsoleLogger().Done 
    ///    .Build();
    /// </code>
    /// </example>
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