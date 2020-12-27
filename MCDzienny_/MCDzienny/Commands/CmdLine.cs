using System;
using System.Collections.Generic;

namespace MCDzienny
{
    // Token: 0x020002BF RID: 703
    public class CmdLine : Command
    {
        // Token: 0x170007A5 RID: 1957
        // (get) Token: 0x06001400 RID: 5120 RVA: 0x0006E82C File Offset: 0x0006CA2C
        public override string name
        {
            get { return "line"; }
        }

        // Token: 0x170007A6 RID: 1958
        // (get) Token: 0x06001401 RID: 5121 RVA: 0x0006E834 File Offset: 0x0006CA34
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170007A7 RID: 1959
        // (get) Token: 0x06001402 RID: 5122 RVA: 0x0006E83C File Offset: 0x0006CA3C
        public override string type
        {
            get { return "build"; }
        }

        // Token: 0x170007A8 RID: 1960
        // (get) Token: 0x06001403 RID: 5123 RVA: 0x0006E844 File Offset: 0x0006CA44
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x170007A9 RID: 1961
        // (get) Token: 0x06001404 RID: 5124 RVA: 0x0006E848 File Offset: 0x0006CA48
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Builder; }
        }

        // Token: 0x170007AA RID: 1962
        // (get) Token: 0x06001405 RID: 5125 RVA: 0x0006E84C File Offset: 0x0006CA4C
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x06001406 RID: 5126 RVA: 0x0006E850 File Offset: 0x0006CA50
        public override void Use(Player p, string message)
        {
            message = message.ToLower();
            CatchPos catchPos;
            if (!(message == ""))
            {
                if (message.IndexOf(' ') == -1)
                    try
                    {
                        catchPos.maxNum = int.Parse(message);
                        catchPos.extraType = 0;
                        catchPos.type = byte.MaxValue;
                        goto IL_349;
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
                            if (catchPos.type == 255)
                            {
                                Help(p);
                                return;
                            }
                        }

                        goto IL_349;
                    }

                if (message.Split(' ').Length == 2)
                    try
                    {
                        catchPos.maxNum = int.Parse(message.Split(' ')[0]);
                        catchPos.type = Block.Byte(message.Split(' ')[1]);
                        if (catchPos.type == 255)
                        {
                            if (message.Split(' ')[1] == "wall")
                                catchPos.extraType = 1;
                            else if (message.Split(' ')[1] == "straight")
                                catchPos.extraType = 2;
                            else
                                catchPos.extraType = 0;
                        }
                        else
                        {
                            catchPos.extraType = 0;
                        }

                        goto IL_349;
                    }
                    catch
                    {
                        catchPos.maxNum = 0;
                        catchPos.type = Block.Byte(message.Split(' ')[0]);
                        if (catchPos.type == 255)
                        {
                            Help(p);
                            return;
                        }

                        if (message.Split(' ')[1] == "wall")
                            catchPos.extraType = 1;
                        else if (message.Split(' ')[1] == "straight")
                            catchPos.extraType = 2;
                        else
                            catchPos.extraType = 0;
                        goto IL_349;
                    }

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
                if (catchPos.type == 255)
                {
                    Help(p);
                }
                else
                {
                    if (message.Split(' ')[2] == "wall")
                    {
                        catchPos.extraType = 1;
                        goto IL_349;
                    }

                    if (message.Split(' ')[2] == "straight")
                    {
                        catchPos.extraType = 2;
                        goto IL_349;
                    }

                    catchPos.extraType = 0;
                    goto IL_349;
                }

                return;
            }

            catchPos.maxNum = 0;
            catchPos.extraType = 0;
            catchPos.type = byte.MaxValue;
            IL_349:
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

        // Token: 0x06001407 RID: 5127 RVA: 0x0006EC40 File Offset: 0x0006CE40
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/line [num] <block> [extra] - Creates a line between two blocks [num] long.");
            Player.SendMessage(p, "Possible [extras] - wall");
        }

        // Token: 0x06001408 RID: 5128 RVA: 0x0006EC58 File Offset: 0x0006CE58
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

        // Token: 0x06001409 RID: 5129 RVA: 0x0006ECCC File Offset: 0x0006CECC
        public void Blockchange2(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            var tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            var catchPos = (CatchPos) p.blockchangeObject;
            if (catchPos.type == 255)
                type = p.bindings[type];
            else
                type = catchPos.type;
            if (!Block.canPlace(p, type))
            {
                Player.SendMessage(p, "Cannot place that.");
                return;
            }

            var list = new List<CatchPos>();
            var item = default(CatchPos);
            if (catchPos.extraType == 2)
            {
                var num = Math.Abs(catchPos.x - x);
                var num2 = Math.Abs(catchPos.y - y);
                var num3 = Math.Abs(catchPos.z - z);
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

            if (catchPos.maxNum == 0) catchPos.maxNum = 100000;
            int[] array =
            {
                catchPos.x,
                catchPos.y,
                catchPos.z
            };
            var num4 = x - catchPos.x;
            var num5 = y - catchPos.y;
            var num6 = z - catchPos.z;
            var num7 = num4 < 0 ? -1 : 1;
            var num8 = Math.Abs(num4);
            var num9 = num5 < 0 ? -1 : 1;
            var num10 = Math.Abs(num5);
            var num11 = num6 < 0 ? -1 : 1;
            var num12 = Math.Abs(num6);
            var num13 = num8 << 1;
            var num14 = num10 << 1;
            var num15 = num12 << 1;
            if (num8 >= num10 && num8 >= num12)
            {
                var num16 = num14 - num8;
                var num17 = num15 - num8;
                for (var i = 0; i < num8; i++)
                {
                    item.x = (ushort) array[0];
                    item.y = (ushort) array[1];
                    item.z = (ushort) array[2];
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
                var num16 = num13 - num10;
                var num17 = num15 - num10;
                for (var i = 0; i < num10; i++)
                {
                    item.x = (ushort) array[0];
                    item.y = (ushort) array[1];
                    item.z = (ushort) array[2];
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
                var num16 = num14 - num12;
                var num17 = num13 - num12;
                for (var i = 0; i < num12; i++)
                {
                    item.x = (ushort) array[0];
                    item.y = (ushort) array[1];
                    item.z = (ushort) array[2];
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

            item.x = (ushort) array[0];
            item.y = (ushort) array[1];
            item.z = (ushort) array[2];
            list.Add(item);
            var num18 = Math.Min(list.Count, catchPos.maxNum);
            if (catchPos.extraType == 1) num18 *= Math.Abs(catchPos.y - y);
            if (num18 > p.group.maxBlocks)
            {
                Player.SendMessage(p, string.Format("You tried to fill {0} blocks at once.", num18));
                Player.SendMessage(p, string.Format("You are limited to {0}", p.group.maxBlocks));
                return;
            }

            num18 = 0;
            while (num18 < catchPos.maxNum && num18 < list.Count)
            {
                if (catchPos.extraType != 1)
                    p.level.Blockchange(p, list[num18].x, list[num18].y, list[num18].z, type);
                else
                    for (var num19 = Math.Min(catchPos.y, y); num19 <= Math.Max(catchPos.y, y); num19 += 1)
                        p.level.Blockchange(p, list[num18].x, num19, list[num18].z, type);
                num18++;
            }

            Player.SendMessage(p, string.Format("Line was {0} blocks long.", num18.ToString()));
            if (p.staticCommands) p.Blockchange += Blockchange1;
        }

        // Token: 0x020002C0 RID: 704
        private struct CatchPos
        {
            // Token: 0x0400098D RID: 2445
            public ushort x;

            // Token: 0x0400098E RID: 2446
            public ushort y;

            // Token: 0x0400098F RID: 2447
            public ushort z;

            // Token: 0x04000990 RID: 2448
            public int maxNum;

            // Token: 0x04000991 RID: 2449
            public int extraType;

            // Token: 0x04000992 RID: 2450
            public byte type;
        }
    }
}