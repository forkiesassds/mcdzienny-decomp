namespace MCDzienny
{
    // Token: 0x02000160 RID: 352
    public class BlockInfo
    {
        // Token: 0x04000486 RID: 1158

        // Token: 0x04000483 RID: 1155

        // Token: 0x04000484 RID: 1156

        // Token: 0x04000485 RID: 1157

        // Token: 0x06000A06 RID: 2566 RVA: 0x00034F50 File Offset: 0x00033150
        public BlockInfo(int x, int y, int z, byte type)
        {
            X = (ushort) x;
            Y = (ushort) y;
            Z = (ushort) z;
            Type = type;
        }

        // Token: 0x17000479 RID: 1145
        // (get) Token: 0x06000A02 RID: 2562 RVA: 0x00034F30 File Offset: 0x00033130
        public ushort X { get; private set; }

        // Token: 0x1700047A RID: 1146
        // (get) Token: 0x06000A03 RID: 2563 RVA: 0x00034F38 File Offset: 0x00033138
        public ushort Y { get; private set; }

        // Token: 0x1700047B RID: 1147
        // (get) Token: 0x06000A04 RID: 2564 RVA: 0x00034F40 File Offset: 0x00033140
        public ushort Z { get; private set; }

        // Token: 0x1700047C RID: 1148
        // (get) Token: 0x06000A05 RID: 2565 RVA: 0x00034F48 File Offset: 0x00033148
        public byte Type { get; private set; }

        // Token: 0x06000A07 RID: 2567 RVA: 0x00034F78 File Offset: 0x00033178
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            var changeInfo = obj as ChangeInfo;
            return changeInfo != null && X == changeInfo.X && Y == changeInfo.Y && Z == changeInfo.Z &&
                   Type == changeInfo.Type;
        }

        // Token: 0x06000A08 RID: 2568 RVA: 0x00034FD0 File Offset: 0x000331D0
        public override int GetHashCode()
        {
            return X ^ Y ^ Z ^ Type;
        }
    }
}