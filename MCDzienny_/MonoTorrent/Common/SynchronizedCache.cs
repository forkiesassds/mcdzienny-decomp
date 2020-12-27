namespace MonoTorrent.Common
{
    // Token: 0x0200037A RID: 890
    internal class SynchronizedCache<T> : ICache<T>
    {
        // Token: 0x04000DF0 RID: 3568
        private readonly ICache<T> cache;

        // Token: 0x060019A4 RID: 6564 RVA: 0x000B5E94 File Offset: 0x000B4094
        public SynchronizedCache(ICache<T> cache)
        {
            Check.Cache(cache);
            this.cache = cache;
        }

        // Token: 0x170008FE RID: 2302
        // (get) Token: 0x060019A3 RID: 6563 RVA: 0x000B5E84 File Offset: 0x000B4084
        public int Count
        {
            get { return cache.Count; }
        }

        // Token: 0x060019A5 RID: 6565 RVA: 0x000B5EAC File Offset: 0x000B40AC
        public T Dequeue()
        {
            T result;
            lock (cache)
            {
                result = cache.Dequeue();
            }

            return result;
        }

        // Token: 0x060019A6 RID: 6566 RVA: 0x000B5EEC File Offset: 0x000B40EC
        public void Enqueue(T instance)
        {
            lock (cache)
            {
                cache.Enqueue(instance);
            }
        }
    }
}