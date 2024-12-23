using System;
using System.Collections.Generic;

namespace MCDzienny
{
    public class CmdSpheroid : Command
    {

        public override string name { get { return "spheroid"; } }

        public override string shortcut { get { return "e"; } }

        public override string type { get { return "build"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Builder; } }

        public override bool ConsoleAccess { get { return false; } }

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
            catchPos.vertical = false;
            if (message == "")
            {
                catchPos.type = byte.MaxValue;
            }
            else if (message.IndexOf(' ') == -1)
            {
                catchPos.type = Block.Byte(message);
                catchPos.vertical = false;
                if (!Block.canPlace(p, catchPos.type))
                {
                    Player.SendMessage(p, "Cannot place that.");
                    return;
                }
                if (catchPos.type == byte.MaxValue)
                {
                    if (message.ToLower() == "hollow")
                    {
                        all.Find("ellipsoid").Use(p, "");
                    }
                    else
                    {
                        if (!(message.ToLower() == "vertical"))
                        {
                            Help(p);
                            return;
                        }
                        catchPos.vertical = true;
                    }
                }
            }
            else
            {
                catchPos.type = Block.Byte(message.Split(' ')[0]);
                if (!Block.canPlace(p, catchPos.type))
                {
                    Player.SendMessage(p, "Cannot place that.");
                    return;
                }
                if (catchPos.type == byte.MaxValue)
                {
                    Help(p);
                    return;
                }
                if (!(message.Split(' ')[1].ToLower() == "vertical"))
                {
                    if (message.Split(' ')[1].ToLower() == "hollow")
                    {
                        all.Find("ellipsoid").Use(p, message.Split(' ')[0]);
                    }
                    else
                    {
                        Help(p);
                    }
                    return;
                }
                catchPos.vertical = true;
            }
            if (!Block.canPlace(p, catchPos.type) && catchPos.type != byte.MaxValue)
            {
                Player.SendMessage(p, "Cannot place this block type!");
                return;
            }
            p.blockchangeObject = catchPos;
            Player.SendMessage(p, "Place two blocks to determine the edges.");
            p.ClearBlockchange();
            p.Blockchange += Blockchange1;
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/spheroid [type] <vertical> - Create a spheroid of blocks.");
            Player.SendMessage(p, "If <vertical> is added, it will be a vertical tube");
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
            var list = new List<Pos>();
            if (!catchPos.vertical)
            {
                int num = Math.Min(catchPos.x, x);
                int num2 = Math.Max(catchPos.x, x);
                int num3 = Math.Min(catchPos.y, y);
                int num4 = Math.Max(catchPos.y, y);
                int num5 = Math.Min(catchPos.z, z);
                int num6 = Math.Max(catchPos.z, z);
                double num7 = (num2 - num + 1) / 2 + 0.25;
                double num8 = (num4 - num3 + 1) / 2 + 0.25;
                double num9 = (num6 - num5 + 1) / 2 + 0.25;
                double num10 = 1.0 / (num7 * num7);
                double num11 = 1.0 / (num8 * num8);
                double num12 = 1.0 / (num9 * num9);
                double num13 = (num2 + num) / 2;
                double num14 = (num4 + num3) / 2;
                double num15 = (num6 + num5) / 2;
                int num16 = (int)(Math.PI * 3.0 / 4.0 * num7 * num8 * num9);
                if (num16 > p.group.maxBlocks)
                {
                    Player.SendMessage(p, string.Format("You tried to spheroid {0} blocks.", num16));
                    Player.SendMessage(p, string.Format("You cannot spheroid more than {0}.", p.group.maxBlocks));
                    return;
                }
                Player.SendMessage(p, num16 + " blocks.");
                for (int i = num; i <= num2; i += 8)
                {
                    for (int j = num3; j <= num4; j += 8)
                    {
                        for (int k = num5; k <= num6; k += 8)
                        {
                            for (int l = 0; l < 8 && k + l <= num6; l++)
                            {
                                for (int m = 0; m < 8 && j + m <= num4; m++)
                                {
                                    for (int n = 0; n < 8 && i + n <= num2; n++)
                                    {
                                        double num17 = i + n - num13;
                                        double num18 = j + m - num14;
                                        double num19 = k + l - num15;
                                        if (num17 * num17 * num10 + num18 * num18 * num11 + num19 * num19 * num12 <= 1.0)
                                        {
                                            p.level.Blockchange(p, (ushort)(n + i), (ushort)(j + m), (ushort)(k + l), type);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                int num20 = Math.Abs(catchPos.x - x) / 2;
                int num21 = 1 - num20;
                int num22 = 1;
                int num23 = -2 * num20;
                int num24 = 0;
                int num25 = num20;
                int num26 = Math.Min(catchPos.x, x) + num20;
                int num27 = Math.Min(catchPos.z, z) + num20;
                Pos item = default(Pos);
                item.x = (ushort)num26;
                item.z = (ushort)(num27 + num20);
                list.Add(item);
                item.z = (ushort)(num27 - num20);
                list.Add(item);
                item.x = (ushort)(num26 + num20);
                item.z = (ushort)num27;
                list.Add(item);
                item.x = (ushort)(num26 - num20);
                list.Add(item);
                while (num24 < num25)
                {
                    if (num21 >= 0)
                    {
                        num25--;
                        num23 += 2;
                        num21 += num23;
                    }
                    num24++;
                    num22 += 2;
                    num21 += num22;
                    item.z = (ushort)(num27 + num25);
                    item.x = (ushort)(num26 + num24);
                    list.Add(item);
                    item.x = (ushort)(num26 - num24);
                    list.Add(item);
                    item.z = (ushort)(num27 - num25);
                    item.x = (ushort)(num26 + num24);
                    list.Add(item);
                    item.x = (ushort)(num26 - num24);
                    list.Add(item);
                    item.z = (ushort)(num27 + num24);
                    item.x = (ushort)(num26 + num25);
                    list.Add(item);
                    item.x = (ushort)(num26 - num25);
                    list.Add(item);
                    item.z = (ushort)(num27 - num24);
                    item.x = (ushort)(num26 + num25);
                    list.Add(item);
                    item.x = (ushort)(num26 - num25);
                    list.Add(item);
                }
                int num28 = Math.Abs(y - catchPos.y) + 1;
                if (list.Count * num28 > p.group.maxBlocks)
                {
                    Player.SendMessage(p, string.Format("You tried to spheroid {0} blocks.", list.Count * num28));
                    Player.SendMessage(p, string.Format("You cannot spheroid more than {0}.", p.group.maxBlocks));
                    return;
                }
                Player.SendMessage(p, string.Format("{0} blocks.", list.Count * num28));
                foreach (Pos item2 in list)
                {
                    for (ushort num29 = Math.Min(catchPos.y, y); num29 <= Math.Max(catchPos.y, y); num29++)
                    {
                        p.level.Blockchange(p, item2.x, num29, item2.z, type);
                    }
                }
            }
            if (p.staticCommands)
            {
                p.Blockchange += Blockchange1;
            }
        }

        void BufferAdd(List<Pos> list, ushort x, ushort y, ushort z)
        {
            Pos item = default(Pos);
            item.x = x;
            item.y = y;
            item.z = z;
            list.Add(item);
        }

        struct Pos
        {
            public ushort x;

            public ushort y;

            public ushort z;
        }

        struct CatchPos
        {
            public byte type;

            public ushort x;

            public ushort y;

            public ushort z;

            public bool vertical;
        }
    }
}