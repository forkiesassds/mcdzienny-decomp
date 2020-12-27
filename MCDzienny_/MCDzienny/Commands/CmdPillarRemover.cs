namespace MCDzienny
{
    // Token: 0x020000D3 RID: 211
    internal class CmdPillarRemover : Command
    {
        // Token: 0x17000304 RID: 772
        // (get) Token: 0x060006E0 RID: 1760 RVA: 0x00023484 File Offset: 0x00021684
        public override string name
        {
            get { return "pillareraser"; }
        }

        // Token: 0x17000305 RID: 773
        // (get) Token: 0x060006E1 RID: 1761 RVA: 0x0002348C File Offset: 0x0002168C
        public override string shortcut
        {
            get { return "pe"; }
        }

        // Token: 0x17000306 RID: 774
        // (get) Token: 0x060006E2 RID: 1762 RVA: 0x00023494 File Offset: 0x00021694
        public override string type
        {
            get { return "build"; }
        }

        // Token: 0x17000307 RID: 775
        // (get) Token: 0x060006E3 RID: 1763 RVA: 0x0002349C File Offset: 0x0002169C
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x17000308 RID: 776
        // (get) Token: 0x060006E4 RID: 1764 RVA: 0x000234A0 File Offset: 0x000216A0
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Builder; }
        }

        // Token: 0x17000309 RID: 777
        // (get) Token: 0x060006E5 RID: 1765 RVA: 0x000234A4 File Offset: 0x000216A4
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x060006E7 RID: 1767 RVA: 0x000234B0 File Offset: 0x000216B0
        public override void Use(Player p, string message)
        {
            CatchPos catchPos;
            catchPos.type = byte.MaxValue;
            if (!Block.canPlace(p, catchPos.type) && catchPos.type != 255)
            {
                Player.SendMessage(p, "Cannot place this block type!");
                return;
            }

            catchPos.x = 0;
            catchPos.y = 0;
            catchPos.z = 0;
            p.blockchangeObject = catchPos;
            Player.SendMessage(p, "Destroy a block.");
            p.ClearBlockchange();
            p.Blockchange += Blockchange1;
        }

        // Token: 0x060006E8 RID: 1768 RVA: 0x0002353C File Offset: 0x0002173C
        public void Blockchange1(Player p, ushort x, ushort y, ushort z, byte type)
        {
            if (!p.staticCommands) p.ClearBlockchange();
            var tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            var o = p.level.PosToInt(x, y, z);
            if (p.core == null) p.core = new Core();
            p.core.PillarEraser(p, tile, o);
        }

        // Token: 0x060006E9 RID: 1769 RVA: 0x000235A4 File Offset: 0x000217A4
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/pillareraser (/pe) - hit the pillar you want to remove. ");
        }

        // Token: 0x020000D4 RID: 212
        private struct CatchPos
        {
            // Token: 0x04000385 RID: 901
            public ushort x;

            // Token: 0x04000386 RID: 902
            public ushort y;

            // Token: 0x04000387 RID: 903
            public ushort z;

            // Token: 0x04000388 RID: 904
            public byte type;
        }
    }
}