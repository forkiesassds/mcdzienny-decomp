namespace MCDzienny
{
    public struct BlockPos
    {
        public ushort x;

        public ushort y;

        public ushort z;

        public BlockPos(ushort x, ushort y, ushort z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public BlockPos(int x, int y, int z)
        {
            this.x = (ushort)x;
            this.y = (ushort)y;
            this.z = (ushort)z;
        }
    }
}