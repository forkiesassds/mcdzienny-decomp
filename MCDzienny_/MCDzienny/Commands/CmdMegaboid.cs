using System;
using System.Collections.Generic;
using System.Timers;

namespace MCDzienny
{
    // Token: 0x020002B4 RID: 692
    public class CmdMegaboid : Command
    {
        // Token: 0x1700078F RID: 1935
        // (get) Token: 0x060013D1 RID: 5073 RVA: 0x0006D150 File Offset: 0x0006B350
        public override string name
        {
            get { return "megaboid"; }
        }

        // Token: 0x17000790 RID: 1936
        // (get) Token: 0x060013D2 RID: 5074 RVA: 0x0006D158 File Offset: 0x0006B358
        public override string shortcut
        {
            get { return "zm"; }
        }

        // Token: 0x17000791 RID: 1937
        // (get) Token: 0x060013D3 RID: 5075 RVA: 0x0006D160 File Offset: 0x0006B360
        public override string type
        {
            get { return "build"; }
        }

        // Token: 0x17000792 RID: 1938
        // (get) Token: 0x060013D4 RID: 5076 RVA: 0x0006D168 File Offset: 0x0006B368
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x17000793 RID: 1939
        // (get) Token: 0x060013D5 RID: 5077 RVA: 0x0006D16C File Offset: 0x0006B36C
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x17000794 RID: 1940
        // (get) Token: 0x060013D6 RID: 5078 RVA: 0x0006D170 File Offset: 0x0006B370
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x060013D7 RID: 5079 RVA: 0x0006D174 File Offset: 0x0006B374
        public override void Use(Player p, string message)
        {
            if (p.megaBoid)
            {
                Player.SendMessage(p, "You may only have on Megaboid going at once. Use /abort to cancel it.");
                return;
            }

            var num = message.Split(' ').Length;
            if (num > 2)
            {
                Help(p);
                return;
            }

            if (num == 2)
            {
                var num2 = message.IndexOf(' ');
                var text = message.Substring(0, num2).ToLower();
                var text2 = message.Substring(num2 + 1).ToLower();
                var b = Block.Byte(text);
                if (b == byte.MaxValue)
                {
                    Player.SendMessage(p, string.Format("There is no block \"{0}\".", text));
                    return;
                }

                if (!Block.canPlace(p, b))
                {
                    Player.SendMessage(p, "Cannot place that.");
                    return;
                }

                CuboidType solid;
                switch (text2)
                {
                    case "solid":
                        solid = CuboidType.Solid;
                        break;
                    case "hollow":
                        solid = CuboidType.Hollow;
                        break;
                    case "walls":
                        solid = CuboidType.Walls;
                        break;
                    case "random":
                        solid = CuboidType.Random;
                        break;
                    case "wire":
                        solid = CuboidType.Wire;
                        break;
                    case "holes":
                        solid = CuboidType.Holes;
                        break;
                    default:
                        Help(p);
                        return;
                }

                var blockChangeInfo = default(BlockChangeInfo);
                blockChangeInfo.solid = solid;
                blockChangeInfo.type = b;
                blockChangeInfo.x = 0;
                blockChangeInfo.y = 0;
                blockChangeInfo.z = 0;
                p.blockchangeObject = blockChangeInfo;
            }
            else if (message != "")
            {
                var solid2 = CuboidType.Solid;
                message = message.ToLower();
                var type = byte.MaxValue;
                switch (message)
                {
                    case "solid":
                        solid2 = CuboidType.Solid;
                        break;
                    case "hollow":
                        solid2 = CuboidType.Hollow;
                        break;
                    case "walls":
                        solid2 = CuboidType.Walls;
                        break;
                    case "wire":
                        solid2 = CuboidType.Wire;
                        break;
                    case "holes":
                        solid2 = CuboidType.Holes;
                        break;
                    case "random":
                        solid2 = CuboidType.Random;
                        break;
                    default:
                    {
                        var b2 = Block.Byte(message);
                        if (b2 == byte.MaxValue)
                        {
                            Player.SendMessage(p, string.Format("There is no block \"{0}\".", message));
                            return;
                        }

                        if (!Block.canPlace(p, b2))
                        {
                            Player.SendMessage(p, "Cannot place that.");
                            return;
                        }

                        type = b2;
                        break;
                    }
                }

                var blockChangeInfo2 = default(BlockChangeInfo);
                blockChangeInfo2.solid = solid2;
                blockChangeInfo2.type = type;
                blockChangeInfo2.x = 0;
                blockChangeInfo2.y = 0;
                blockChangeInfo2.z = 0;
                p.blockchangeObject = blockChangeInfo2;
            }
            else
            {
                var blockChangeInfo3 = default(BlockChangeInfo);
                blockChangeInfo3.solid = CuboidType.Solid;
                blockChangeInfo3.type = byte.MaxValue;
                blockChangeInfo3.x = 0;
                blockChangeInfo3.y = 0;
                blockChangeInfo3.z = 0;
                p.blockchangeObject = blockChangeInfo3;
            }

            Player.SendMessage(p, "Place two blocks to determine the edges.");
            p.ClearBlockchange();
            p.Blockchange += Blockchange1;
        }

        // Token: 0x060013D8 RID: 5080 RVA: 0x0006D484 File Offset: 0x0006B684
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/megaboid [block] [type] - create a cuboid of blocks.");
            Player.SendMessage(p, "Available types:");
            Player.SendMessage(p, "solid, hollow, walls, holes, wire, random");
            Player.SendMessage(p, "Shortcut: /zm");
        }

        // Token: 0x060013D9 RID: 5081 RVA: 0x0006D4B4 File Offset: 0x0006B6B4
        public void Blockchange1(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            var tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            var blockChangeInfo = (BlockChangeInfo) p.blockchangeObject;
            blockChangeInfo.x = x;
            blockChangeInfo.y = y;
            blockChangeInfo.z = z;
            p.blockchangeObject = blockChangeInfo;
            p.Blockchange += Blockchange2;
        }

        // Token: 0x060013DA RID: 5082 RVA: 0x0006D528 File Offset: 0x0006B728
        public void Blockchange2(Player p, ushort x, ushort y, ushort z, byte type)
        {
            var megaTimer = new Timer(1.0);
            p.ClearBlockchange();
            var tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            var cpos = (BlockChangeInfo) p.blockchangeObject;
            if (cpos.type != 255)
                type = cpos.type;
            else
                type = p.bindings[type];
            var buffer = new List<Pos>();
            switch (cpos.solid)
            {
                case CuboidType.Solid:
                    DrawSolid(p, x, y, z, type, cpos, buffer);
                    break;
                case CuboidType.Hollow:
                    DrawHollow(p, x, y, z, type, cpos, buffer);
                    break;
                case CuboidType.Walls:
                    DrawWalls(p, x, y, z, type, cpos, buffer);
                    break;
                case CuboidType.Wire:
                    for (var num = Math.Min(cpos.x, x); num <= Math.Max(cpos.x, x); num += 1)
                    {
                        buffer.Add(new Pos(num, y, z));
                        buffer.Add(new Pos(num, y, cpos.z));
                        buffer.Add(new Pos(num, cpos.y, z));
                        buffer.Add(new Pos(num, cpos.y, cpos.z));
                    }

                    for (var num2 = Math.Min(cpos.y, y); num2 <= Math.Max(cpos.y, y); num2 += 1)
                    {
                        buffer.Add(new Pos(x, num2, z));
                        buffer.Add(new Pos(x, num2, cpos.z));
                        buffer.Add(new Pos(cpos.x, num2, z));
                        buffer.Add(new Pos(cpos.x, num2, cpos.z));
                    }

                    for (var num3 = Math.Min(cpos.z, z); num3 <= Math.Max(cpos.z, z); num3 += 1)
                    {
                        buffer.Add(new Pos(x, y, num3));
                        buffer.Add(new Pos(x, cpos.y, num3));
                        buffer.Add(new Pos(cpos.x, y, num3));
                        buffer.Add(new Pos(cpos.x, cpos.y, num3));
                    }

                    break;
                case CuboidType.Random:
                {
                    var random = new Random();
                    for (var num4 = Math.Min(cpos.x, x); num4 <= Math.Max(cpos.x, x); num4 += 1)
                    for (var num5 = Math.Min(cpos.y, y); num5 <= Math.Max(cpos.y, y); num5 += 1)
                    for (var num6 = Math.Min(cpos.z, z); num6 <= Math.Max(cpos.z, z); num6 += 1)
                        if (random.Next(1, 11) <= 5 && p.level.GetTile(num4, num5, num6) != type)
                            buffer.Add(new Pos(num4, num5, num6));
                    break;
                }
                case CuboidType.Holes:
                {
                    var flag = true;
                    for (var num7 = Math.Min(cpos.x, x); num7 <= Math.Max(cpos.x, x); num7 += 1)
                    {
                        var flag2 = flag;
                        for (var num8 = Math.Min(cpos.y, y); num8 <= Math.Max(cpos.y, y); num8 += 1)
                        {
                            var flag3 = flag;
                            for (var num9 = Math.Min(cpos.z, z); num9 <= Math.Max(cpos.z, z); num9 += 1)
                            {
                                flag = !flag;
                                if (flag && p.level.GetTile(num7, num8, num9) != type)
                                    buffer.Add(new Pos(num7, num8, num9));
                            }

                            flag = !flag3;
                        }

                        flag = !flag2;
                    }

                    break;
                }
            }

            if (buffer.Count > 450000)
            {
                Player.SendMessage(p, "You cannot megaboid more than 450000 blocks.");
                Player.SendMessage(p, string.Format("You tried to megaboid {0} blocks.", buffer.Count));
                return;
            }

            Player.SendMessage(p, buffer.Count + " blocks.");
            Player.SendMessage(p, "Use /abort to cancel the megaboid at any time.");
            p.megaBoid = true;
            var CurrentLoop = 0;
            var currentLevel = p.level;
            var unallowedBlocks = 0;
            Pos pos;
            megaTimer.Elapsed += delegate
            {
                if (!p.megaBoid || p.disconnected)
                {
                    megaTimer.Close();
                    return;
                }

                pos = buffer[CurrentLoop];
                try
                {
                    if (!currentLevel.BlockchangeChecks(p, pos.x, pos.y, pos.z, type,
                        currentLevel.GetTile(pos.x, pos.y, pos.z)))
                    {
                        unallowedBlocks++;
                        if (unallowedBlocks > 5)
                        {
                            p.megaBoid = false;
                            megaTimer.Close();
                            Player.SendMessage(p, "Megaboid aborted.");
                            return;
                        }
                    }

                    currentLevel.Blockchange(p, pos.x, pos.y, pos.z, type);
                }
                catch
                {
                }

                CurrentLoop++;
                if (CurrentLoop % 1000 == 0)
                    Player.SendMessage(p,
                        string.Format("{0} blocks down, {1} to go.", CurrentLoop, buffer.Count - CurrentLoop));
                if (CurrentLoop >= buffer.Count)
                {
                    Player.SendMessage(p, "Megaboid completed.");
                    p.megaBoid = false;
                    megaTimer.Close();
                }

                megaTimer.Start();
            };
            megaTimer.AutoReset = false;
            megaTimer.Start();
            if (p.staticCommands) p.Blockchange += Blockchange1;
        }

        // Token: 0x060013DB RID: 5083 RVA: 0x0006DB18 File Offset: 0x0006BD18
        private static void DrawWalls(Player p, ushort x, ushort y, ushort z, byte type, BlockChangeInfo cpos,
            List<Pos> buffer)
        {
            for (var num = Math.Min(cpos.y, y); num <= Math.Max(cpos.y, y); num = (ushort) (num + 1))
            for (var num2 = Math.Min(cpos.z, z); num2 <= Math.Max(cpos.z, z); num2 = (ushort) (num2 + 1))
            {
                if (p.level.GetTile(cpos.x, num, num2) != type) buffer.Add(new Pos(cpos.x, num, num2));
                if (cpos.x != x && p.level.GetTile(x, num, num2) != type) buffer.Add(new Pos(x, num, num2));
            }

            if (Math.Abs(cpos.x - x) < 2 || Math.Abs(cpos.z - z) < 2) return;
            for (var num3 = (ushort) (Math.Min(cpos.x, x) + 1);
                num3 <= Math.Max(cpos.x, x) - 1;
                num3 = (ushort) (num3 + 1))
            for (var num4 = Math.Min(cpos.y, y); num4 <= Math.Max(cpos.y, y); num4 = (ushort) (num4 + 1))
            {
                if (p.level.GetTile(num3, num4, cpos.z) != type) buffer.Add(new Pos(num3, num4, cpos.z));
                if (cpos.z != z && p.level.GetTile(num3, num4, z) != type) buffer.Add(new Pos(num3, num4, z));
            }
        }

        // Token: 0x060013DC RID: 5084 RVA: 0x0006DCA4 File Offset: 0x0006BEA4
        private static void DrawHollow(Player p, ushort x, ushort y, ushort z, byte type, BlockChangeInfo cpos,
            List<Pos> buffer)
        {
            for (var num = Math.Min(cpos.y, y); num <= Math.Max(cpos.y, y); num = (ushort) (num + 1))
            for (var num2 = Math.Min(cpos.z, z); num2 <= Math.Max(cpos.z, z); num2 = (ushort) (num2 + 1))
            {
                if (p.level.GetTile(cpos.x, num, num2) != type) buffer.Add(new Pos(cpos.x, num, num2));
                if (cpos.x != x && p.level.GetTile(x, num, num2) != type) buffer.Add(new Pos(x, num, num2));
            }

            if (Math.Abs(cpos.x - x) < 2) return;
            for (var num3 = (ushort) (Math.Min(cpos.x, x) + 1);
                num3 <= Math.Max(cpos.x, x) - 1;
                num3 = (ushort) (num3 + 1))
            for (var num4 = Math.Min(cpos.z, z); num4 <= Math.Max(cpos.z, z); num4 = (ushort) (num4 + 1))
            {
                if (p.level.GetTile(num3, cpos.y, num4) != type) buffer.Add(new Pos(num3, cpos.y, num4));
                if (cpos.y != y && p.level.GetTile(num3, y, num4) != type) buffer.Add(new Pos(num3, y, num4));
            }

            if (Math.Abs(cpos.y - y) < 2) return;
            for (var num5 = (ushort) (Math.Min(cpos.x, x) + 1);
                num5 <= Math.Max(cpos.x, x) - 1;
                num5 = (ushort) (num5 + 1))
            for (var num6 = (ushort) (Math.Min(cpos.y, y) + 1);
                num6 <= Math.Max(cpos.y, y) - 1;
                num6 = (ushort) (num6 + 1))
            {
                if (p.level.GetTile(num5, num6, cpos.z) != type) buffer.Add(new Pos(num5, num6, cpos.z));
                if (cpos.z != z && p.level.GetTile(num5, num6, z) != type) buffer.Add(new Pos(num5, num6, z));
            }
        }

        // Token: 0x060013DD RID: 5085 RVA: 0x0006DEF0 File Offset: 0x0006C0F0
        private static void DrawSolid(Player p, ushort x, ushort y, ushort z, byte type, BlockChangeInfo cpos,
            List<Pos> buffer)
        {
            for (var num = Math.Min(cpos.x, x); num <= Math.Max(cpos.x, x); num += 1)
            for (var num2 = Math.Min(cpos.y, y); num2 <= Math.Max(cpos.y, y); num2 += 1)
            for (var num3 = Math.Min(cpos.z, z); num3 <= Math.Max(cpos.z, z); num3 += 1)
                if (p.level.GetTile(num, num2, num3) != type)
                    buffer.Add(new Pos(num, num2, num3));
        }

        // Token: 0x020002B5 RID: 693
        private struct Pos
        {
            // Token: 0x060013DF RID: 5087 RVA: 0x0006DF98 File Offset: 0x0006C198
            public Pos(ushort x, ushort y, ushort z)
            {
                this.x = x;
                this.y = y;
                this.z = z;
            }

            // Token: 0x0400096C RID: 2412
            public readonly ushort x;

            // Token: 0x0400096D RID: 2413
            public readonly ushort y;

            // Token: 0x0400096E RID: 2414
            public readonly ushort z;
        }

        // Token: 0x020002B6 RID: 694
        private struct BlockChangeInfo
        {
            // Token: 0x0400096F RID: 2415
            public CuboidType solid;

            // Token: 0x04000970 RID: 2416
            public byte type;

            // Token: 0x04000971 RID: 2417
            public ushort x;

            // Token: 0x04000972 RID: 2418
            public ushort y;

            // Token: 0x04000973 RID: 2419
            public ushort z;
        }

        // Token: 0x020002B7 RID: 695
        private enum CuboidType
        {
            // Token: 0x04000975 RID: 2421
            Solid,

            // Token: 0x04000976 RID: 2422
            Hollow,

            // Token: 0x04000977 RID: 2423
            Walls,

            // Token: 0x04000978 RID: 2424
            Wire,

            // Token: 0x04000979 RID: 2425
            Random,

            // Token: 0x0400097A RID: 2426
            Holes
        }
    }
}