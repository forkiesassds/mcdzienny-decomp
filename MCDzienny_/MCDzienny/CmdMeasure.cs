using System;

namespace MCDzienny
{
    public class CmdMeasure : Command
    {

        public override string name { get { return "measure"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "information"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            if (message.IndexOf(' ') != -1)
            {
                Help(p);
                return;
            }
            CatchPos catchPos = default(CatchPos);
            catchPos.toIgnore = Block.Byte(message);
            if (catchPos.toIgnore == byte.MaxValue && message != "")
            {
                Player.SendMessage(p, "Could not find block specified");
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

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/measure [ignore] - Measures all the blocks between two points");
            Player.SendMessage(p, "/measure [ignore] - Enter a block to ignore them");
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
            int num = 0;
            for (ushort num2 = Math.Min(catchPos.x, x); num2 <= Math.Max(catchPos.x, x); num2++)
            {
                for (ushort num3 = Math.Min(catchPos.y, y); num3 <= Math.Max(catchPos.y, y); num3++)
                {
                    for (ushort num4 = Math.Min(catchPos.z, z); num4 <= Math.Max(catchPos.z, z); num4++)
                    {
                        if (p.level.GetTile(num2, num3, num4) != catchPos.toIgnore)
                        {
                            num++;
                        }
                    }
                }
            }
            Player.SendMessage(p, string.Format("{0} blocks are between ({1}, {2}, {3}) and ({4}, {5}, {6})", num, catchPos.x, catchPos.y, catchPos.z, x, y, z));
            if (p.staticCommands)
            {
                p.Blockchange += Blockchange1;
            }
        }

        struct CatchPos
        {
            public ushort x;

            public ushort y;

            public ushort z;

            public byte toIgnore;
        }
    }
}