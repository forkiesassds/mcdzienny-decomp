using System;
using System.Collections.Generic;

namespace MCDzienny
{
    // Token: 0x0200029A RID: 666
    public class CmdSpheroid : Command
    {
        // Token: 0x17000730 RID: 1840
        // (get) Token: 0x06001318 RID: 4888 RVA: 0x00069034 File Offset: 0x00067234
        public override string name
        {
            get { return "spheroid"; }
        }

        // Token: 0x17000731 RID: 1841
        // (get) Token: 0x06001319 RID: 4889 RVA: 0x0006903C File Offset: 0x0006723C
        public override string shortcut
        {
            get { return "e"; }
        }

        // Token: 0x17000732 RID: 1842
        // (get) Token: 0x0600131A RID: 4890 RVA: 0x00069044 File Offset: 0x00067244
        public override string type
        {
            get { return "build"; }
        }

        // Token: 0x17000733 RID: 1843
        // (get) Token: 0x0600131B RID: 4891 RVA: 0x0006904C File Offset: 0x0006724C
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x17000734 RID: 1844
        // (get) Token: 0x0600131C RID: 4892 RVA: 0x00069050 File Offset: 0x00067250
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Builder; }
        }

        // Token: 0x17000735 RID: 1845
        // (get) Token: 0x0600131D RID: 4893 RVA: 0x00069054 File Offset: 0x00067254
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x0600131F RID: 4895 RVA: 0x00069060 File Offset: 0x00067260
        public override void Use(Player p, string message)
        {
            if (p.level.mapType == MapType.Lava && p.group.Permission < LevelPermission.Admin)
                p.SendMessage("Only admin is allowed to use this command on lava map");
            CatchPos catchPos;
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

                if (catchPos.type == 255)
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

                if (catchPos.type == 255)
                {
                    Help(p);
                    return;
                }

                if (message.Split(' ')[1].ToLower() == "vertical")
                {
                    catchPos.vertical = true;
                }
                else
                {
                    if (message.Split(' ')[1].ToLower() == "hollow")
                    {
                        all.Find("ellipsoid").Use(p, message.Split(' ')[0]);
                        return;
                    }

                    Help(p);
                    return;
                }
            }

            if (!Block.canPlace(p, catchPos.type) && catchPos.type != 255)
            {
                Player.SendMessage(p, "Cannot place this block type!");
                return;
            }

            p.blockchangeObject = catchPos;
            Player.SendMessage(p, "Place two blocks to determine the edges.");
            p.ClearBlockchange();
            p.Blockchange += Blockchange1;
        }

        // Token: 0x06001320 RID: 4896 RVA: 0x000692B0 File Offset: 0x000674B0
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/spheroid [type] <vertical> - Create a spheroid of blocks.");
            Player.SendMessage(p, "If <vertical> is added, it will be a vertical tube");
        }

        // Token: 0x06001321 RID: 4897 RVA: 0x000692C8 File Offset: 0x000674C8
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

        // Token: 0x06001322 RID: 4898 RVA: 0x0006933C File Offset: 0x0006753C
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

            var list = new List<Pos>();
            if (!catchPos.vertical)
            {
                int num = Math.Min(catchPos.x, x);
                int num2 = Math.Max(catchPos.x, x);
                int num3 = Math.Min(catchPos.y, y);
                int num4 = Math.Max(catchPos.y, y);
                int num5 = Math.Min(catchPos.z, z);
                int num6 = Math.Max(catchPos.z, z);
                var num7 = (num2 - num + 1) / 2 + 0.25;
                var num8 = (num4 - num3 + 1) / 2 + 0.25;
                var num9 = (num6 - num5 + 1) / 2 + 0.25;
                var num10 = 1.0 / (num7 * num7);
                var num11 = 1.0 / (num8 * num8);
                var num12 = 1.0 / (num9 * num9);
                double num13 = (num2 + num) / 2;
                double num14 = (num4 + num3) / 2;
                double num15 = (num6 + num5) / 2;
                var num16 = (int) (2.356194490192345 * num7 * num8 * num9);
                if (num16 > p.group.maxBlocks)
                {
                    Player.SendMessage(p, string.Format("You tried to spheroid {0} blocks.", num16));
                    Player.SendMessage(p, string.Format("You cannot spheroid more than {0}.", p.group.maxBlocks));
                    return;
                }

                Player.SendMessage(p, num16 + " blocks.");
                for (var i = num; i <= num2; i += 8)
                for (var j = num3; j <= num4; j += 8)
                for (var k = num5; k <= num6; k += 8)
                {
                    var num17 = 0;
                    while (num17 < 8 && k + num17 <= num6)
                    {
                        var num18 = 0;
                        while (num18 < 8 && j + num18 <= num4)
                        {
                            var num19 = 0;
                            while (num19 < 8 && i + num19 <= num2)
                            {
                                var num20 = i + num19 - num13;
                                var num21 = j + num18 - num14;
                                var num22 = k + num17 - num15;
                                if (num20 * num20 * num10 + num21 * num21 * num11 + num22 * num22 * num12 <= 1.0)
                                    p.level.Blockchange(p, (ushort) (num19 + i), (ushort) (j + num18),
                                        (ushort) (k + num17), type);
                                num19++;
                            }

                            num18++;
                        }

                        num17++;
                    }
                }
            }
            else
            {
                var num23 = Math.Abs(catchPos.x - x) / 2;
                var num24 = 1 - num23;
                var num25 = 1;
                var num26 = -2 * num23;
                var l = 0;
                var num27 = num23;
                var num28 = Math.Min(catchPos.x, x) + num23;
                var num29 = Math.Min(catchPos.z, z) + num23;
                var item = new Pos
                {
                    x = (ushort) num28,
                    z = (ushort) (num29 + num23)
                };
                list.Add(item);
                item.z = (ushort) (num29 - num23);
                list.Add(item);
                item.x = (ushort) (num28 + num23);
                item.z = (ushort) num29;
                list.Add(item);
                item.x = (ushort) (num28 - num23);
                list.Add(item);
                while (l < num27)
                {
                    if (num24 >= 0)
                    {
                        num27--;
                        num26 += 2;
                        num24 += num26;
                    }

                    l++;
                    num25 += 2;
                    num24 += num25;
                    item.z = (ushort) (num29 + num27);
                    item.x = (ushort) (num28 + l);
                    list.Add(item);
                    item.x = (ushort) (num28 - l);
                    list.Add(item);
                    item.z = (ushort) (num29 - num27);
                    item.x = (ushort) (num28 + l);
                    list.Add(item);
                    item.x = (ushort) (num28 - l);
                    list.Add(item);
                    item.z = (ushort) (num29 + l);
                    item.x = (ushort) (num28 + num27);
                    list.Add(item);
                    item.x = (ushort) (num28 - num27);
                    list.Add(item);
                    item.z = (ushort) (num29 - l);
                    item.x = (ushort) (num28 + num27);
                    list.Add(item);
                    item.x = (ushort) (num28 - num27);
                    list.Add(item);
                }

                var num30 = Math.Abs(y - catchPos.y) + 1;
                if (list.Count * num30 > p.group.maxBlocks)
                {
                    Player.SendMessage(p, string.Format("You tried to spheroid {0} blocks.", list.Count * num30));
                    Player.SendMessage(p, string.Format("You cannot spheroid more than {0}.", p.group.maxBlocks));
                    return;
                }

                Player.SendMessage(p, string.Format("{0} blocks.", list.Count * num30));
                foreach (var pos in list)
                    for (var num31 = Math.Min(catchPos.y, y); num31 <= Math.Max(catchPos.y, y); num31 += 1)
                        p.level.Blockchange(p, pos.x, num31, pos.z, type);
            }

            if (p.staticCommands) p.Blockchange += Blockchange1;
        }

        // Token: 0x06001323 RID: 4899 RVA: 0x00069914 File Offset: 0x00067B14
        private void BufferAdd(List<Pos> list, ushort x, ushort y, ushort z)
        {
            Pos item;
            item.x = x;
            item.y = y;
            item.z = z;
            list.Add(item);
        }

        // Token: 0x0200029B RID: 667
        private struct Pos
        {
            // Token: 0x04000944 RID: 2372
            public ushort x;

            // Token: 0x04000945 RID: 2373
            public ushort y;

            // Token: 0x04000946 RID: 2374
            public ushort z;
        }

        // Token: 0x0200029C RID: 668
        private struct CatchPos
        {
            // Token: 0x04000947 RID: 2375
            public byte type;

            // Token: 0x04000948 RID: 2376
            public ushort x;

            // Token: 0x04000949 RID: 2377
            public ushort y;

            // Token: 0x0400094A RID: 2378
            public ushort z;

            // Token: 0x0400094B RID: 2379
            public bool vertical;
        }
    }
}