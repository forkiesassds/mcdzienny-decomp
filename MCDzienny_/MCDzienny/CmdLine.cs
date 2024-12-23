using System;
using System.Collections.Generic;

namespace MCDzienny
{
    public class CmdLine : Command
    {

        public override string name { get { return "line"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "build"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Builder; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            message = message.ToLower();
            CatchPos catchPos = default(CatchPos);
            if (message == "")
            {
                catchPos.maxNum = 0;
                catchPos.extraType = 0;
                catchPos.type = byte.MaxValue;
            }
            else if (message.IndexOf(' ') == -1)
            {
                try
                {
                    catchPos.maxNum = int.Parse(message);
                    catchPos.extraType = 0;
                    catchPos.type = byte.MaxValue;
                }
                catch
                {
                    catchPos.maxNum = 0;
                    if (message == "wall")
                    {
                        catchPos.extraType = 1;
                        catchPos.type = byte.MaxValue;
                    }
                    else if (message == "straight")
                    {
                        catchPos.extraType = 2;
                        catchPos.type = byte.MaxValue;
                    }
                    else
                    {
                        catchPos.extraType = 0;
                        catchPos.type = Block.Byte(message);
                        if (catchPos.type == byte.MaxValue)
                        {
                            Help(p);
                            return;
                        }
                    }
                }
            }
            else if (message.Split(' ').Length == 2)
            {
                try
                {
                    catchPos.maxNum = int.Parse(message.Split(' ')[0]);
                    catchPos.type = Block.Byte(message.Split(' ')[1]);
                    if (catchPos.type == byte.MaxValue)
                    {
                        if (message.Split(' ')[1] == "wall")
                        {
                            catchPos.extraType = 1;
                        }
                        else if (message.Split(' ')[1] == "straight")
                        {
                            catchPos.extraType = 2;
                        }
                        else
                        {
                            catchPos.extraType = 0;
                        }
                    }
                    else
                    {
                        catchPos.extraType = 0;
                    }
                }
                catch
                {
                    catchPos.maxNum = 0;
                    catchPos.type = Block.Byte(message.Split(' ')[0]);
                    if (catchPos.type == byte.MaxValue)
                    {
                        Help(p);
                        return;
                    }
                    if (message.Split(' ')[1] == "wall")
                    {
                        catchPos.extraType = 1;
                    }
                    else if (message.Split(' ')[1] == "straight")
                    {
                        catchPos.extraType = 2;
                    }
                    else
                    {
                        catchPos.extraType = 0;
                    }
                }
            }
            else
            {
                try
                {
                    catchPos.maxNum = int.Parse(message.Split(' ')[0]);
                }
                catch
                {
                    Help(p);
                    return;
                }
                catchPos.type = Block.Byte(message.Split(' ')[1]);
                if (catchPos.type == byte.MaxValue)
                {
                    Help(p);
                    return;
                }
                if (message.Split(' ')[2] == "wall")
                {
                    catchPos.extraType = 1;
                }
                else if (message.Split(' ')[2] == "straight")
                {
                    catchPos.extraType = 2;
                }
                else
                {
                    catchPos.extraType = 0;
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

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/line [num] <block> [extra] - Creates a line between two blocks [num] long.");
            Player.SendMessage(p, "Possible [extras] - wall");
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
            type = catchPos.type != byte.MaxValue ? catchPos.type : p.bindings[type];
            if (!Block.canPlace(p, type))
            {
                Player.SendMessage(p, "Cannot place that.");
                return;
            }
            var list = new List<CatchPos>();
            CatchPos item = default(CatchPos);
            if (catchPos.extraType == 2)
            {
                int num = Math.Abs(catchPos.x - x);
                int num2 = Math.Abs(catchPos.y - y);
                int num3 = Math.Abs(catchPos.z - z);
                if (num > num2 && num > num3)
                {
                    y = catchPos.y;
                    z = catchPos.z;
                }
                else if (num2 > num && num2 > num3)
                {
                    x = catchPos.x;
                    z = catchPos.z;
                }
                else if (num3 > num2 && num3 > num)
                {
                    y = catchPos.y;
                    x = catchPos.x;
                }
            }
            if (catchPos.maxNum == 0)
            {
                catchPos.maxNum = 100000;
            }
            int[] array = new int[3]
            {
                catchPos.x, catchPos.y, catchPos.z
            };
            int num4 = x - catchPos.x;
            int num5 = y - catchPos.y;
            int num6 = z - catchPos.z;
            int num7 = num4 >= 0 ? 1 : -1;
            int num8 = Math.Abs(num4);
            int num9 = num5 >= 0 ? 1 : -1;
            int num10 = Math.Abs(num5);
            int num11 = num6 >= 0 ? 1 : -1;
            int num12 = Math.Abs(num6);
            int num13 = num8 << 1;
            int num14 = num10 << 1;
            int num15 = num12 << 1;
            if (num8 >= num10 && num8 >= num12)
            {
                int num16 = num14 - num8;
                int num17 = num15 - num8;
                for (int i = 0; i < num8; i++)
                {
                    item.x = (ushort)array[0];
                    item.y = (ushort)array[1];
                    item.z = (ushort)array[2];
                    list.Add(item);
                    if (num16 > 0)
                    {
                        array[1] += num9;
                        num16 -= num13;
                    }
                    if (num17 > 0)
                    {
                        array[2] += num11;
                        num17 -= num13;
                    }
                    num16 += num14;
                    num17 += num15;
                    array[0] += num7;
                }
            }
            else if (num10 >= num8 && num10 >= num12)
            {
                int num16 = num13 - num10;
                int num17 = num15 - num10;
                for (int i = 0; i < num10; i++)
                {
                    item.x = (ushort)array[0];
                    item.y = (ushort)array[1];
                    item.z = (ushort)array[2];
                    list.Add(item);
                    if (num16 > 0)
                    {
                        array[0] += num7;
                        num16 -= num14;
                    }
                    if (num17 > 0)
                    {
                        array[2] += num11;
                        num17 -= num14;
                    }
                    num16 += num13;
                    num17 += num15;
                    array[1] += num9;
                }
            }
            else
            {
                int num16 = num14 - num12;
                int num17 = num13 - num12;
                for (int i = 0; i < num12; i++)
                {
                    item.x = (ushort)array[0];
                    item.y = (ushort)array[1];
                    item.z = (ushort)array[2];
                    list.Add(item);
                    if (num16 > 0)
                    {
                        array[1] += num9;
                        num16 -= num15;
                    }
                    if (num17 > 0)
                    {
                        array[0] += num7;
                        num17 -= num15;
                    }
                    num16 += num14;
                    num17 += num13;
                    array[2] += num11;
                }
            }
            item.x = (ushort)array[0];
            item.y = (ushort)array[1];
            item.z = (ushort)array[2];
            list.Add(item);
            int num18 = Math.Min(list.Count, catchPos.maxNum);
            if (catchPos.extraType == 1)
            {
                num18 *= Math.Abs(catchPos.y - y);
            }
            if (num18 > p.group.maxBlocks)
            {
                Player.SendMessage(p, string.Format("You tried to fill {0} blocks at once.", num18));
                Player.SendMessage(p, string.Format("You are limited to {0}", p.group.maxBlocks));
                return;
            }
            for (num18 = 0; num18 < catchPos.maxNum && num18 < list.Count; num18++)
            {
                if (catchPos.extraType != 1)
                {
                    p.level.Blockchange(p, list[num18].x, list[num18].y, list[num18].z, type);
                    continue;
                }
                for (ushort num19 = Math.Min(catchPos.y, y); num19 <= Math.Max(catchPos.y, y); num19++)
                {
                    p.level.Blockchange(p, list[num18].x, num19, list[num18].z, type);
                }
            }
            Player.SendMessage(p, string.Format("Line was {0} blocks long.", num18.ToString()));
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

            public int maxNum;

            public int extraType;

            public byte type;
        }
    }
}