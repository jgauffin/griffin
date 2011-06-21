using System;
using System.Collections.Concurrent;

namespace Griffin.Core.Net.Buffers
{
    public class ObjectPool<T> : IObjectPool<T> where T : class
    {
        private readonly Func<T> _factoryMethod;
        private readonly ConcurrentQueue<T> _queue = new ConcurrentQueue<T>();

        public ObjectPool(Func<T> factoryMethod)
        {
            _factoryMethod = factoryMethod;
        }

        public void Allocate(int count)
        {
            for (int i = 0; i < count; i++)
                _queue.Enqueue(_factoryMethod());
        }

        #region IObjectPool<T> Members

        public void Push(object reusableObject)
        {
            _queue.Enqueue((T) reusableObject);
        }

        public T Pull()
        {
            T buffer;
            return !_queue.TryDequeue(out buffer) ? _factoryMethod() : buffer;
        }

        #endregion
    }
}