using System.Collections.Generic;

namespace MonoTorrent.Common
{
    class Cache<T> : ICache<T> where T : class, ICacheable, new()
    {
        readonly bool autoCreate;

        readonly Queue<T> cache;

        public Cache()
            : this(autoCreate: false) {}

        public Cache(bool autoCreate)
        {
            this.autoCreate = autoCreate;
            cache = new Queue<T>();
        }

        public int Count { get { return cache.Count; } }

        public T Dequeue()
        {
            if (cache.Count > 0)
            {
                return cache.Dequeue();
            }
            if (!autoCreate)
            {
                return null;
            }
            return new T();
        }

        public void Enqueue(T instance)
        {
            instance.Initialise();
            cache.Enqueue(instance);
        }

        public ICache<T> Synchronize()
        {
            return new SynchronizedCache<T>(this);
        }
    }
}