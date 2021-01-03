using System;

namespace MCDzienny
{
    // Token: 0x02000356 RID: 854
    public class PositionChangedEventArgs : EventArgs
    {
        // Token: 0x06001889 RID: 6281 RVA: 0x000A56D4 File Offset: 0x000A38D4
        public PositionChangedEventArgs(int x, int y, int z, byte pitch, byte jaw)
        {
            X = x;
            Y = y;
            Z = z;
            Pitch = pitch;
            Jaw = jaw;
        }

        // Token: 0x170008F5 RID: 2293
        // (get) Token: 0x0600187F RID: 6271 RVA: 0x000A5670 File Offset: 0x000A3870
        // (set) Token: 0x06001880 RID: 6272 RVA: 0x000A5678 File Offset: 0x000A3878
        public int X { get; set; }

        // Token: 0x170008F6 RID: 2294
        // (get) Token: 0x06001881 RID: 6273 RVA: 0x000A5684 File Offset: 0x000A3884
        // (set) Token: 0x06001882 RID: 6274 RVA: 0x000A568C File Offset: 0x000A388C
        public int Y { get; set; }

        // Token: 0x170008F7 RID: 2295
        // (get) Token: 0x06001883 RID: 6275 RVA: 0x000A5698 File Offset: 0x000A3898
        // (set) Token: 0x06001884 RID: 6276 RVA: 0x000A56A0 File Offset: 0x000A38A0
        public int Z { get; set; }

        // Token: 0x170008F8 RID: 2296
        // (get) Token: 0x06001885 RID: 6277 RVA: 0x000A56AC File Offset: 0x000A38AC
        // (set) Token: 0x06001886 RID: 6278 RVA: 0x000A56B4 File Offset: 0x000A38B4
        public byte Pitch { get; set; }

        // Token: 0x170008F9 RID: 2297
        // (get) Token: 0x06001887 RID: 6279 RVA: 0x000A56C0 File Offset: 0x000A38C0
        // (set) Token: 0x06001888 RID: 6280 RVA: 0x000A56C8 File Offset: 0x000A38C8
        public byte Jaw { get; set; }
    }
}