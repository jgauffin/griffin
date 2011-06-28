namespace Griffin.Logging
{
    /// <summary>
    /// Extension method attaching the logging configuration api to the Griffin configurer.
    /// </summary>
    public static class ConfigureExtension
    {
        /// <summary>
        /// Access the logging configuration.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns>Fluent logging api</returns>
        /// <example>
        /// <code>
        /// Configure.Griffin.Logging().XXXX;
        /// </code>
        /// </example>
        public static FluentConfiguration Logging(this Configure instance)
        {
            return new FluentConfiguration();
        }
    }
}