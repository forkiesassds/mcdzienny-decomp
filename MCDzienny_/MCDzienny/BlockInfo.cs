namespace MCDzienny
{
    public class BlockInfo
    {

        readonly byte type;
        readonly ushort x;

        readonly ushort y;

        readonly ushort z;

        public BlockInfo(int x, int y, int z, byte type)
        {
            this.x = (ushort)x;
            this.y = (ushort)y;
            this.z = (ushort)z;
            this.type = type;
        }

        public ushort X { get { return x; } }

        public ushort Y { get { return y; } }

        public ushort Z { get { return z; } }

        public byte Type { get { return type; } }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            ChangeInfo changeInfo = obj as ChangeInfo;
            if (changeInfo == null)
            {
                return false;
            }
            if (x == changeInfo.X && y == changeInfo.Y && z == changeInfo.Z)
            {
                return type == changeInfo.Type;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return x ^ y ^ z ^ type;
        }
    }
}