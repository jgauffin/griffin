namespace Griffin.Core.Threading
{
    public class TimedJobObserver
    {
        public virtual void OnStartingJob(TimedJobObserverContext context)
        {
        }

        /// <summary>
        /// A job has ended
        /// </summary>
        /// <param name="context">Same context as when the job was being started.</param>
        /// <remarks>
        /// <para>
        /// The context is unique for this observer which means that you can use the <see cref="TimedJoObserverContext.State"/> property
        /// to save any context information. A typical example is a database connection that should be closed.
        /// </para>
        /// <para>
        /// Note that this method will always be invoked (even if an exception is thrown).
        /// </para>
        /// </remarks>
        public virtual void OnJobEnded(TimedJobObserverContext context)
        {
        }
    }
}