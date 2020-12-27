using System;
using System.Collections.Generic;

namespace MonoTorrent.Common
{
    // Token: 0x02000379 RID: 889
    internal class Cache<T> : ICache<T> where T : class, ICacheable, new()
    {
        // Token: 0x04000DEE RID: 3566
        private readonly bool autoCreate;

        // Token: 0x04000DEF RID: 3567
        private readonly Queue<T> cache;

        // Token: 0x0600199E RID: 6558 RVA: 0x000B5DF8 File Offset: 0x000B3FF8
        public Cache() : this(false)
        {
        }

        // Token: 0x0600199F RID: 6559 RVA: 0x000B5E04 File Offset: 0x000B4004
        public Cache(bool autoCreate)
        {
            this.autoCreate = autoCreate;
            cache = new Queue<T>();
        }

        // Token: 0x170008FD RID: 2301
        // (get) Token: 0x0600199D RID: 6557 RVA: 0x000B5DE8 File Offset: 0x000B3FE8
        public int Count
        {
            get { return cache.Count; }
        }

        // Token: 0x060019A0 RID: 6560 RVA: 0x000B5E20 File Offset: 0x000B4020
        public T Dequeue()
        {
            if (cache.Count > 0) return cache.Dequeue();
            if (!autoCreate) return default(T);
            return Activator.CreateInstance<T>();
        }

        // Token: 0x060019A1 RID: 6561 RVA: 0x000B5E60 File Offset: 0x000B4060
        public void Enqueue(T instance)
        {
            instance.Initialise();
            cache.Enqueue(instance);
        }

        // Token: 0x060019A2 RID: 6562 RVA: 0x000B5E7C File Offset: 0x000B407C
        public ICache<T> Synchronize()
        {
            return new SynchronizedCache<T>(this);
        }
    }
}