namespace Griffin.InversionOfControl
{
    /// <summary>
    /// Gets a parameter used when configuring the inversion of control container.
    /// </summary>
    public class Parameter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Parameter"/> class.
        /// </summary>
        /// <param name="name">Constructor argument name.</param>
        /// <param name="value">The value.</param>
        public Parameter(string name, object value)
        {
            Name = name;
            Value = value;
        }

        /// <summary>
        /// Get parameter name
        /// </summary>
        /// <remarks>
        /// It should correspond to the name of the constructor argument.
        /// </remarks>
        public string Name { get; set; }

        /// <summary>
        /// Gets value to use
        /// </summary>
        public object Value { get; set; }
    }
}