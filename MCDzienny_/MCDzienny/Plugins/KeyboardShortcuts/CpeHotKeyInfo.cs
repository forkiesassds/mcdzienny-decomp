namespace MCDzienny.Plugins.KeyboardShortcuts
{
    // Token: 0x02000040 RID: 64
    public class CpeHotKeyInfo
    {
        // Token: 0x040000DA RID: 218
        public static readonly byte None = 0;

        // Token: 0x040000DB RID: 219
        public static readonly byte Ctrl = 1;

        // Token: 0x040000DC RID: 220
        public static readonly byte Shift = 2;

        // Token: 0x040000DD RID: 221
        public static readonly byte Alt = 4;

        // Token: 0x1700005B RID: 91
        // (get) Token: 0x06000161 RID: 353 RVA: 0x000089FC File Offset: 0x00006BFC
        // (set) Token: 0x06000162 RID: 354 RVA: 0x00008A04 File Offset: 0x00006C04
        public string Label { get; set; }

        // Token: 0x1700005C RID: 92
        // (get) Token: 0x06000163 RID: 355 RVA: 0x00008A10 File Offset: 0x00006C10
        // (set) Token: 0x06000164 RID: 356 RVA: 0x00008A18 File Offset: 0x00006C18
        public string Message { get; set; }

        // Token: 0x1700005D RID: 93
        // (get) Token: 0x06000165 RID: 357 RVA: 0x00008A24 File Offset: 0x00006C24
        // (set) Token: 0x06000166 RID: 358 RVA: 0x00008A2C File Offset: 0x00006C2C
        public int KeyCode { get; set; }

        // Token: 0x1700005E RID: 94
        // (get) Token: 0x06000167 RID: 359 RVA: 0x00008A38 File Offset: 0x00006C38
        // (set) Token: 0x06000168 RID: 360 RVA: 0x00008A40 File Offset: 0x00006C40
        public byte KeyMod { get; set; }
    }
}