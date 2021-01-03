namespace MCDzienny
{
    // Token: 0x020001DE RID: 478
    public class PlayerPosition
    {
        // Token: 0x04000700 RID: 1792
        public int RotX;

        // Token: 0x04000701 RID: 1793
        public int RotY;

        // Token: 0x040006FD RID: 1789
        public int X;

        // Token: 0x040006FE RID: 1790
        public int Y;

        // Token: 0x040006FF RID: 1791
        public int Z;

        // Token: 0x06000D58 RID: 3416 RVA: 0x0004C500 File Offset: 0x0004A700
        public PlayerPosition()
        {
        }

        // Token: 0x06000D59 RID: 3417 RVA: 0x0004C508 File Offset: 0x0004A708
        public PlayerPosition(int X, int Y, int Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }

        // Token: 0x06000D5A RID: 3418 RVA: 0x0004C528 File Offset: 0x0004A728
        public PlayerPosition(int X, int Y, int Z, int RotX, int RotY)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            this.RotX = RotX;
            this.RotY = RotY;
        }
    }
}