namespace MCDzienny
{
    class CmdPillarRemover : Command
    {

        public override string name { get { return "pillareraser"; } }

        public override string shortcut { get { return "pe"; } }

        public override string type { get { return "build"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Builder; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            CatchPos catchPos = default(CatchPos);
            catchPos.type = byte.MaxValue;
            if (!Block.canPlace(p, catchPos.type) && catchPos.type != byte.MaxValue)
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

        public void Blockchange1(Player p, ushort x, ushort y, ushort z, byte type)
        {
            if (!p.staticCommands)
            {
                p.ClearBlockchange();
            }
            byte tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            int o = p.level.PosToInt(x, y, z);
            if (p.core == null)
            {
                p.core = new Core();
            }
            p.core.PillarEraser(p, tile, o);
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/pillareraser (/pe) - hit the pillar you want to remove. ");
        }

        struct CatchPos
        {
            public ushort x;

            public ushort y;

            public ushort z;

            public byte type;
        }
    }
}