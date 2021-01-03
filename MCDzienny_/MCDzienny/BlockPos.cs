namespace MCDzienny
{
    // Token: 0x0200019F RID: 415
    public struct BlockPos
    {
        // Token: 0x06000C1F RID: 3103 RVA: 0x000471E8 File Offset: 0x000453E8
        public BlockPos(ushort x, ushort y, ushort z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        // Token: 0x06000C20 RID: 3104 RVA: 0x00047200 File Offset: 0x00045400
        public BlockPos(int x, int y, int z)
        {
            this.x = (ushort) x;
            this.y = (ushort) y;
            this.z = (ushort) z;
        }

        // Token: 0x0400063B RID: 1595
        public ushort x;

        // Token: 0x0400063C RID: 1596
        public ushort y;

        // Token: 0x0400063D RID: 1597
        public ushort z;
    }
}