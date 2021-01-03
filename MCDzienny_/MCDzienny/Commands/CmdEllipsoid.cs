namespace MCDzienny
{
    // Token: 0x020000BF RID: 191
    internal class CmdEllipsoid : Command
    {
        // Token: 0x170002D6 RID: 726
        // (get) Token: 0x06000679 RID: 1657 RVA: 0x00021990 File Offset: 0x0001FB90
        public override string name
        {
            get { return "ellipsoid"; }
        }

        // Token: 0x170002D7 RID: 727
        // (get) Token: 0x0600067A RID: 1658 RVA: 0x00021998 File Offset: 0x0001FB98
        public override string shortcut
        {
            get { return "ell"; }
        }

        // Token: 0x170002D8 RID: 728
        // (get) Token: 0x0600067B RID: 1659 RVA: 0x000219A0 File Offset: 0x0001FBA0
        public override string type
        {
            get { return "build"; }
        }

        // Token: 0x170002D9 RID: 729
        // (get) Token: 0x0600067C RID: 1660 RVA: 0x000219A8 File Offset: 0x0001FBA8
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x170002DA RID: 730
        // (get) Token: 0x0600067D RID: 1661 RVA: 0x000219AC File Offset: 0x0001FBAC
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Builder; }
        }

        // Token: 0x170002DB RID: 731
        // (get) Token: 0x0600067E RID: 1662 RVA: 0x000219B0 File Offset: 0x0001FBB0
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x06000680 RID: 1664 RVA: 0x000219BC File Offset: 0x0001FBBC
        public override void Use(Player p, string message)
        {
            CatchPos catchPos;
            catchPos.type = byte.MaxValue;
            if (message != "")
            {
                catchPos.type = Block.Byte(message.Split(' ')[0].ToLower());
                if (catchPos.type == 255)
                {
                    Help(p);
                    return;
                }
            }

            if (!Block.canPlace(p, catchPos.type) && catchPos.type != 255)
            {
                Player.SendMessage(p, "Cannot place this block type!");
                return;
            }

            catchPos.x = 0;
            catchPos.y = 0;
            catchPos.z = 0;
            p.blockchangeObject = catchPos;
            Player.SendMessage(p, "Place two blocks to determine the edges.");
            p.ClearBlockchange();
            p.Blockchange += Blockchange1;
        }

        // Token: 0x06000681 RID: 1665 RVA: 0x00021A90 File Offset: 0x0001FC90
        public void Blockchange1(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            var tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            var catchPos = (CatchPos) p.blockchangeObject;
            catchPos.x = x;
            catchPos.y = y;
            catchPos.z = z;
            p.blockchangeObject = catchPos;
            p.Blockchange += Blockchange2;
        }

        // Token: 0x06000682 RID: 1666 RVA: 0x00021B04 File Offset: 0x0001FD04
        public void Blockchange2(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            var tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            var catchPos = (CatchPos) p.blockchangeObject;
            if (catchPos.type != 255) type = catchPos.type;
            if (!Block.canPlace(p, type))
            {
                Player.SendMessage(p, "Cannot place that.");
                return;
            }

            if (p.core == null) p.core = new Core();
            p.core.Ellipsoid(p, new[]
            {
                catchPos.x,
                catchPos.y,
                catchPos.z,
                x,
                y,
                (double) z
            }, type);
        }

        // Token: 0x06000683 RID: 1667 RVA: 0x00021BC8 File Offset: 0x0001FDC8
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/ellipsoid - draws hollow sphere. ");
        }

        // Token: 0x020000C0 RID: 192
        private struct CatchPos
        {
            // Token: 0x04000355 RID: 853
            public ushort x;

            // Token: 0x04000356 RID: 854
            public ushort y;

            // Token: 0x04000357 RID: 855
            public ushort z;

            // Token: 0x04000358 RID: 856
            public byte type;
        }
    }
}