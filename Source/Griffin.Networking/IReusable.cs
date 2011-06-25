using System;

namespace Griffin.Networking
{
    /// <summary>
    /// Object can be reused
    /// </summary>
    /// <remarks>
    /// </remarks>
    public abstract class Reusable<T> : IDisposable where T : class
    {
        private readonly IObjectPool<T> _objectPool;

        protected Reusable(IObjectPool<T> objectPool)
        {
            _objectPool = objectPool;
        }

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
            _objectPool.Push(this);
        }

        #endregion

        /// <summary>
        /// Reset object so that it can be reused.
        /// </summary>
        public abstract void Reset();
    }
}