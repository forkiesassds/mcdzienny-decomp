namespace MCDzienny
{
    // Token: 0x020001A2 RID: 418
    public class ChangeInfo
    {
        // Token: 0x0400064B RID: 1611

        // Token: 0x0400064A RID: 1610

        // Token: 0x04000647 RID: 1607

        // Token: 0x04000648 RID: 1608

        // Token: 0x04000649 RID: 1609

        // Token: 0x06000C26 RID: 3110 RVA: 0x00047244 File Offset: 0x00045444
        public ChangeInfo(int x, int y, int z, byte type, byte action)
        {
            X = (ushort) x;
            Y = (ushort) y;
            Z = (ushort) z;
            Type = type;
            Action = action;
        }

        // Token: 0x170004C6 RID: 1222
        // (get) Token: 0x06000C21 RID: 3105 RVA: 0x0004721C File Offset: 0x0004541C
        public ushort X { get; private set; }

        // Token: 0x170004C7 RID: 1223
        // (get) Token: 0x06000C22 RID: 3106 RVA: 0x00047224 File Offset: 0x00045424
        public ushort Y { get; private set; }

        // Token: 0x170004C8 RID: 1224
        // (get) Token: 0x06000C23 RID: 3107 RVA: 0x0004722C File Offset: 0x0004542C
        public ushort Z { get; private set; }

        // Token: 0x170004C9 RID: 1225
        // (get) Token: 0x06000C24 RID: 3108 RVA: 0x00047234 File Offset: 0x00045434
        public byte Type { get; private set; }

        // Token: 0x170004CA RID: 1226
        // (get) Token: 0x06000C25 RID: 3109 RVA: 0x0004723C File Offset: 0x0004543C
        public byte Action { get; private set; }

        // Token: 0x06000C27 RID: 3111 RVA: 0x00047274 File Offset: 0x00045474
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            var changeInfo = obj as ChangeInfo;
            return changeInfo != null && X == changeInfo.X && Y == changeInfo.Y && Z == changeInfo.Z &&
                   Type == changeInfo.Type && Action == changeInfo.Action;
        }

        // Token: 0x06000C28 RID: 3112 RVA: 0x000472DC File Offset: 0x000454DC
        public override int GetHashCode()
        {
            return X ^ Y ^ Z ^ Type ^ Action;
        }
    }
}