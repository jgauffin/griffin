namespace Griffin.Core
{
    /// <summary>
    /// Extensions added to string classes
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Format a string
        /// </summary>
        /// <param name="instance">String that got formatters specified.</param>
        /// <param name="arguments">Arguments to format with</param>
        /// <returns>Formatted string</returns>
        /// <remarks>
        /// Uses <see cref="string.Format()"/> internally.
        /// </remarks>
        public static string FormatWith(this string instance, params object[] arguments)
        {
            return string.Format(instance, arguments);
        }

        /// <summary>
        /// Determines whether a string is null or empty.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>
        ///   <c>true</c> if string is null or empty; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrEmpty(this string instance)
        {
            return string.IsNullOrEmpty(instance);
        }
    }
}