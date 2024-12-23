namespace MCDzienny
{
    public class ChangeInfo
    {

        readonly byte action;

        readonly byte type;
        readonly ushort x;

        readonly ushort y;

        readonly ushort z;

        public ChangeInfo(int x, int y, int z, byte type, byte action)
        {
            this.x = (ushort)x;
            this.y = (ushort)y;
            this.z = (ushort)z;
            this.type = type;
            this.action = action;
        }

        public ushort X { get { return x; } }

        public ushort Y { get { return y; } }

        public ushort Z { get { return z; } }

        public byte Type { get { return type; } }

        public byte Action { get { return action; } }

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
            if (x == changeInfo.X && y == changeInfo.Y && z == changeInfo.Z && type == changeInfo.Type)
            {
                return action == changeInfo.Action;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return x ^ y ^ z ^ type ^ action;
        }
    }
}