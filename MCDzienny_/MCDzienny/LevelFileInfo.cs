namespace MCDzienny
{
    // Token: 0x02000161 RID: 353
    public class LevelFileInfo
    {
        // Token: 0x04000489 RID: 1161
        public int Depth;

        // Token: 0x04000488 RID: 1160
        public int Height;

        // Token: 0x0400048A RID: 1162
        public PlayerPosition Spawn;

        // Token: 0x04000487 RID: 1159
        public int Width;

        // Token: 0x1700047D RID: 1149
        // (get) Token: 0x06000A09 RID: 2569 RVA: 0x00034FF0 File Offset: 0x000331F0
        public int BlockCount
        {
            get { return Width * Height * Depth; }
        }
    }
}