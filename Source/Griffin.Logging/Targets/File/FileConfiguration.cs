namespace Griffin.Logging.Targets.File
{
    public class FileConfiguration
    {
        public FileConfiguration()
        {
            DateFormat = "yyyy-MM-dd";
            DateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff";
        }

        /// <summary>
        /// Gets or sets location to save logs.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets number of days to keep logs.
        /// </summary>
        public int DaysToKeep { get; set; }

        /// <summary>
        /// Gets or sets create a sub folder and place log in it
        /// </summary>
        /// <seealso cref="DateFormat"/>
        public bool CreateDateFolder { get; set; }

        /// <summary>
        /// Gets or sets format to use for dates.
        /// </summary>
        /// <remarks>
        /// Used when creating folder (when <see cref="CreateDateFolder"/> is set to <c>true</c>).
        /// </remarks>
        public string DateFormat { get; set; }

        /// <summary>
        /// Gets or sets date/time format.
        /// </summary>
        /// <remarks>
        /// Used for each log entry
        /// </remarks>
        public string DateTimeFormat { get; set; }
    }
}