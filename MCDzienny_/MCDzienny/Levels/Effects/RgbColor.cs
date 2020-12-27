namespace MCDzienny.Levels.Effects
{
    // Token: 0x02000034 RID: 52
    public class RgbColor
    {
        // Token: 0x0600012D RID: 301 RVA: 0x00008244 File Offset: 0x00006444
        public RgbColor(byte red, byte green, byte blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        // Token: 0x17000049 RID: 73
        // (get) Token: 0x06000127 RID: 295 RVA: 0x00008208 File Offset: 0x00006408
        // (set) Token: 0x06000128 RID: 296 RVA: 0x00008210 File Offset: 0x00006410
        public byte Red { get; set; }

        // Token: 0x1700004A RID: 74
        // (get) Token: 0x06000129 RID: 297 RVA: 0x0000821C File Offset: 0x0000641C
        // (set) Token: 0x0600012A RID: 298 RVA: 0x00008224 File Offset: 0x00006424
        public byte Green { get; set; }

        // Token: 0x1700004B RID: 75
        // (get) Token: 0x0600012B RID: 299 RVA: 0x00008230 File Offset: 0x00006430
        // (set) Token: 0x0600012C RID: 300 RVA: 0x00008238 File Offset: 0x00006438
        public byte Blue { get; set; }
    }
}