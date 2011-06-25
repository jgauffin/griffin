namespace Griffin.Core.Threading
{
    /// <summary>
    /// context used when invoking a job.
    /// </summary>
    public class TimedJobObserverContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimedJobObserverContext"/> class.
        /// </summary>
        /// <param name="job">The job to execute.</param>
        public TimedJobObserverContext(TimedJob job)
        {
            Job = job;
        }

        /// <summary>
        /// Gets job to execute
        /// </summary>
        public TimedJob Job { get; private set; }

        /// <summary>
        /// Gets or sets state object used by the observer
        /// </summary>
        public object State { get; set; }
    }
}