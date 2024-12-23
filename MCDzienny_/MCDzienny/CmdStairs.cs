using System;

namespace MCDzienny
{
    public class CmdStairs : Command
    {

        public override string name { get { return "stairs"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "build"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }

        public override bool ConsoleAccess { get { return false; } }

        public ushort z { get; set; }

        public override void Use(Player p, string message)
        {
            if (p.level.mapType == MapType.Lava && p.group.Permission < LevelPermission.Admin)
            {
                p.SendMessage("Only admin is allowed to use this command on lava map");
            }
            CatchPos catchPos = default(CatchPos);
            catchPos.x = 0;
            catchPos.y = 0;
            catchPos.z = 0;
            p.blockchangeObject = catchPos;
            Player.SendMessage(p, "Place two blocks to determine the height.");
            p.ClearBlockchange();
            p.Blockchange += Blockchange1;
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/stairs - Creates a spiral staircase the height you want.");
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

        public void Swap(ref int a, ref int b)
        {
            int num = a;
            a = b;
            b = num;
        }

        public void Blockchange2(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            byte tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            CatchPos catchPos = (CatchPos)p.blockchangeObject;
            if (catchPos.y == y)
            {
                Player.SendMessage(p, "Cannot create a stairway 0 blocks high.");
                return;
            }
            int num = 0;
            ushort num2 = catchPos.x;
            ushort num3 = catchPos.z;
            num = catchPos.x <= x || catchPos.z <= z ? catchPos.x > x && catchPos.z < z ? 1 : catchPos.x >= x || catchPos.z <= z ? 3 : 2 : 0;
            for (ushort num4 = Math.Min(catchPos.y, y); num4 <= Math.Max(catchPos.y, y); num4++)
            {
                switch (num)
                {
                    case 0:
                        num2++;
                        p.level.Blockchange(p, num2, num4, num3, 44);
                        num2++;
                        p.level.Blockchange(p, num2, num4, num3, 43);
                        num = 1;
                        break;
                    case 1:
                        num3++;
                        p.level.Blockchange(p, num2, num4, num3, 44);
                        num3++;
                        p.level.Blockchange(p, num2, num4, num3, 43);
                        num = 2;
                        break;
                    case 2:
                        num2--;
                        p.level.Blockchange(p, num2, num4, num3, 44);
                        num2--;
                        p.level.Blockchange(p, num2, num4, num3, 43);
                        num = 3;
                        break;
                    default:
                        num3--;
                        p.level.Blockchange(p, num2, num4, num3, 44);
                        num3--;
                        p.level.Blockchange(p, num2, num4, num3, 43);
                        num = 0;
                        break;
                }
            }
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
        }
    }
}