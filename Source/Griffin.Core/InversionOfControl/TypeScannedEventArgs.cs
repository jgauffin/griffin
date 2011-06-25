using System;
using Griffin.InversionOfControl;

namespace Griffin.Core.InversionOfControl
{
    /// <summary>
    /// A type that is decorated with the <see cref="ComponentAttribute"/>.
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