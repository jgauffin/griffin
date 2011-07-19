namespace Griffin
{
    /// <summary>
    /// Action to take when an unhandled exception is caught.
    /// </summary>
    public enum ExceptionPolicy
    {
        /// <summary>
        /// Throw it and kill the application
        /// </summary>
        Throw,

        /// <summary>
        /// Continue as it was never thrown.
        /// </summary>
        Ignore
    }
}