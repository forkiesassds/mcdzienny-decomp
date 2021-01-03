namespace MCDzienny.Notification
{
    // Token: 0x020001D3 RID: 467
    public sealed class Channel
    {
        // Token: 0x040006C1 RID: 1729
        public static readonly Channel General = new Channel("General", "0");

        // Token: 0x040006C2 RID: 1730
        public static readonly Channel Lava = new Channel("Lava", "1");

        // Token: 0x040006C3 RID: 1731
        public static readonly Channel Zombie = new Channel("Zombie", "2");

        // Token: 0x040006C4 RID: 1732
        public static readonly Channel Freebuild = new Channel("Freebuild", "3");

        // Token: 0x040006C0 RID: 1728

        // Token: 0x040006BF RID: 1727

        // Token: 0x06000D14 RID: 3348 RVA: 0x0004B020 File Offset: 0x00049220
        private Channel(string name, string id)
        {
            Name = name;
            ID = id;
        }

        // Token: 0x170004F3 RID: 1267
        // (get) Token: 0x06000D12 RID: 3346 RVA: 0x0004B010 File Offset: 0x00049210
        public string Name { get; private set; }

        // Token: 0x170004F4 RID: 1268
        // (get) Token: 0x06000D13 RID: 3347 RVA: 0x0004B018 File Offset: 0x00049218
        public string ID { get; private set; }
    }
}