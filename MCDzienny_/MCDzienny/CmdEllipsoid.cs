namespace MCDzienny
{
    class CmdEllipsoid : Command
    {

        public override string name { get { return "ellipsoid"; } }

        public override string shortcut { get { return "ell"; } }

        public override string type { get { return "build"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Builder; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            CatchPos catchPos = default(CatchPos);
            catchPos.type = byte.MaxValue;
            if (message != "")
            {
                catchPos.type = Block.Byte(message.Split(' ')[0].ToLower());
                if (catchPos.type == byte.MaxValue)
                {
                    Help(p);
                    return;
                }
            }
            if (!Block.canPlace(p, catchPos.type) && catchPos.type != byte.MaxValue)
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

        public void Blockchange1(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            byte tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            CatchPos catchPos = (CatchPos)p.blockchangeObject;
            catchPos.x = x;
            catchPos.y = y;
            catchPos.z = z;
            p.blockchangeObject = catchPos;
            p.Blockchange += Blockchange2;
        }

        public void Blockchange2(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            byte tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            CatchPos catchPos = (CatchPos)p.blockchangeObject;
            if (catchPos.type != byte.MaxValue)
            {
                type = catchPos.type;
            }
            if (!Block.canPlace(p, type))
            {
                Player.SendMessage(p, "Cannot place that.");
                return;
            }
            if (p.core == null)
            {
                p.core = new Core();
            }
            p.core.Ellipsoid(p, new double[6]
            {
                catchPos.x, catchPos.y, catchPos.z, x, y, z
            }, type);
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/ellipsoid - draws hollow sphere. ");
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