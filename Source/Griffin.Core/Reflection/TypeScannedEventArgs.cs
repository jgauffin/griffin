using System;

namespace Griffin.Core.Reflection
{
    /// <summary>
    /// A type that was found in an assembly.
    /// </summary>
    public class TypeScannedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeScannedEventArgs"/> class.
        /// </summary>
        /// <param name="foundType">Found type.</param>
        public TypeScannedEventArgs(Type foundType)
        {
            FoundType = foundType;
        }

        /// <summary>
        /// Gets the type that was found during the scanning process.
        /// </summary>
        public Type FoundType { get; private set; }
    }
}