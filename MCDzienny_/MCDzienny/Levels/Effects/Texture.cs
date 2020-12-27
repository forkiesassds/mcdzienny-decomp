namespace MCDzienny.Levels.Effects
{
    // Token: 0x02000035 RID: 53
    public class Texture
    {
        // Token: 0x06000136 RID: 310 RVA: 0x000082B4 File Offset: 0x000064B4
        public Texture(string url, sbyte sideBlock, sbyte edgeBlock, short sideLevel)
        {
            Url = url;
            SideBlock = sideBlock;
            EdgeBlock = edgeBlock;
            SideLevel = sideLevel;
        }

        // Token: 0x1700004C RID: 76
        // (get) Token: 0x0600012E RID: 302 RVA: 0x00008264 File Offset: 0x00006464
        // (set) Token: 0x0600012F RID: 303 RVA: 0x0000826C File Offset: 0x0000646C
        public string Url { get; set; }

        // Token: 0x1700004D RID: 77
        // (get) Token: 0x06000130 RID: 304 RVA: 0x00008278 File Offset: 0x00006478
        // (set) Token: 0x06000131 RID: 305 RVA: 0x00008280 File Offset: 0x00006480
        public sbyte SideBlock { get; set; }

        // Token: 0x1700004E RID: 78
        // (get) Token: 0x06000132 RID: 306 RVA: 0x0000828C File Offset: 0x0000648C
        // (set) Token: 0x06000133 RID: 307 RVA: 0x00008294 File Offset: 0x00006494
        public sbyte EdgeBlock { get; set; }

        // Token: 0x1700004F RID: 79
        // (get) Token: 0x06000134 RID: 308 RVA: 0x000082A0 File Offset: 0x000064A0
        // (set) Token: 0x06000135 RID: 309 RVA: 0x000082A8 File Offset: 0x000064A8
        public short SideLevel { get; set; }
    }
}