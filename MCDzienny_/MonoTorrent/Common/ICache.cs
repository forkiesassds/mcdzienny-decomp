namespace MonoTorrent.Common
{
    // Token: 0x02000378 RID: 888
    internal interface ICache<T>
    {
        // Token: 0x170008FC RID: 2300
        // (get) Token: 0x0600199A RID: 6554
        int Count { get; }

        // Token: 0x0600199B RID: 6555
        T Dequeue();

        // Token: 0x0600199C RID: 6556
        void Enqueue(T instance);
    }
}