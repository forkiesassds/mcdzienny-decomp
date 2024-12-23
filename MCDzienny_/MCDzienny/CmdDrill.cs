using System.Collections.Generic;

namespace MCDzienny
{
    public class CmdDrill : Command
    {

        public override string name { get { return "drill"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "build"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            CatchPos catchPos = default(CatchPos);
            catchPos.distance = 20;
            if (message != "")
            {
                try
                {
                    catchPos.distance = int.Parse(message);
                }
                catch
                {
                    Help(p);
                    return;
                }
            }
            catchPos.x = 0;
            catchPos.y = 0;
            catchPos.z = 0;
            p.blockchangeObject = catchPos;
            Player.SendMessage(p, "Destroy the block you wish to drill.");
            p.ClearBlockchange();
            p.Blockchange += Blockchange1;
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/drill [distance] - Drills a hole, destroying all similar blocks in a 3x3 rectangle ahead of you.");
        }

        public void Blockchange1(Player p, ushort x, ushort y, ushort z, byte type)
        {
            if (!p.staticCommands)
            {
                p.ClearBlockchange();
            }
            CatchPos catchPos = (CatchPos)p.blockchangeObject;
            byte tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            int num = 0;
            int num2 = 0;
            if (p.rot[0] <= 32 || p.rot[0] >= 224)
            {
                num2 = -1;
            }
            else if (p.rot[0] <= 96)
            {
                num = 1;
            }
            else if (p.rot[0] <= 160)
            {
                num2 = 1;
            }
            else
            {
                num = -1;
            }
            var list = new List<Pos>();
            int num3 = 0;
            Pos item = default(Pos);
            if (num != 0)
            {
                ushort num4 = x;
                while (num3 < catchPos.distance)
                {
                    for (ushort num5 = (ushort)(y - 1); num5 <= (ushort)(y + 1); num5++)
                    {
                        for (ushort num6 = (ushort)(z - 1); num6 <= (ushort)(z + 1); num6++)
                        {
                            item.x = num4;
                            item.y = num5;
                            item.z = num6;
                            list.Add(item);
                        }
                    }
                    num3++;
                    num4 += (ushort)num;
                }
            }
            else
            {
                ushort num7 = z;
                while (num3 < catchPos.distance)
                {
                    for (ushort num8 = (ushort)(y - 1); num8 <= (ushort)(y + 1); num8++)
                    {
                        for (ushort num9 = (ushort)(x - 1); num9 <= (ushort)(x + 1); num9++)
                        {
                            item.x = num9;
                            item.y = num8;
                            item.z = num7;
                            list.Add(item);
                        }
                    }
                    num3++;
                    num7 += (ushort)num2;
                }
            }
            if (list.Count > p.group.maxBlocks)
            {
                Player.SendMessage(p, string.Format("You tried to drill {0} blocks.", list.Count));
                Player.SendMessage(p, string.Format("You cannot drill more than {0}.", p.group.maxBlocks));
                return;
            }
            foreach (Pos item2 in list)
            {
                if (p.level.GetTile(item2.x, item2.y, item2.z) == tile)
                {
                    p.level.Blockchange(p, item2.x, item2.y, item2.z, 0);
                }
            }
            Player.SendMessage(p, string.Format("{0} blocks.", list.Count));
        }

        struct CatchPos
        {
            public ushort x;

            public ushort y;

            public ushort z;

            public int distance;
        }

        struct Pos
        {
            public ushort x;

            public ushort y;

            public ushort z;
        }
    }
}