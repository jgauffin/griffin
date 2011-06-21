using System;

namespace Griffin.Core.Logging.Targets
{
    public class ConsoleConfiguration
    {
        public ConsoleConfiguration()
        {
            DebugColor = ConsoleColor.Gray;
            ErrorColor = ConsoleColor.Red;
            WarningColor = ConsoleColor.Magenta;
            InfoColor = ConsoleColor.White;
        }

        /// <summary>
        /// Gets or sets color of debug messages
        /// </summary>
        public ConsoleColor DebugColor { get; set; }

        /// <summary>
        /// Gets or sets color of error messages
        /// </summary>
        public ConsoleColor ErrorColor { get; set; }

        /// <summary>
        /// Gets or sets color of information messages
        /// </summary>
        public ConsoleColor InfoColor { get; set; }

        /// <summary>
        /// Gets or sets color of warning messages
        /// </summary>
        public ConsoleColor WarningColor { get; set; }
    }
}