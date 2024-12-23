using System;
using System.Collections.Generic;
using System.Timers;

namespace MCDzienny
{
    public class CmdMegaboid : Command
    {

        public override string name { get { return "megaboid"; } }

        public override string shortcut { get { return "zm"; } }

        public override string type { get { return "build"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            if (p.megaBoid)
            {
                Player.SendMessage(p, "You may only have on Megaboid going at once. Use /abort to cancel it.");
                return;
            }
            int num = message.Split(' ').Length;
            if (num > 2)
            {
                Help(p);
                return;
            }
            if (num == 2)
            {
                int num2 = message.IndexOf(' ');
                string arg = message.Substring(0, num2).ToLower();
                string text = message.Substring(num2 + 1).ToLower();
                byte b = Block.Byte(arg);
                if (b == byte.MaxValue)
                {
                    Player.SendMessage(p, string.Format("There is no block \"{0}\".", arg));
                    return;
                }
                if (!Block.canPlace(p, b))
                {
                    Player.SendMessage(p, "Cannot place that.");
                    return;
                }
                CuboidType solid;
                switch (text)
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
                BlockChangeInfo blockChangeInfo = default(BlockChangeInfo);
                blockChangeInfo.solid = solid;
                blockChangeInfo.type = b;
                blockChangeInfo.x = 0;
                blockChangeInfo.y = 0;
                blockChangeInfo.z = 0;
                p.blockchangeObject = blockChangeInfo;
            }
            else if (message != "")
            {
                CuboidType solid2 = CuboidType.Solid;
                message = message.ToLower();
                byte b2 = byte.MaxValue;
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
                        byte b3 = Block.Byte(message);
                        if (b3 == byte.MaxValue)
                        {
                            Player.SendMessage(p, string.Format("There is no block \"{0}\".", message));
                            return;
                        }
                        if (!Block.canPlace(p, b3))
                        {
                            Player.SendMessage(p, "Cannot place that.");
                            return;
                        }
                        b2 = b3;
                        break;
                    }
                }
                BlockChangeInfo blockChangeInfo2 = default(BlockChangeInfo);
                blockChangeInfo2.solid = solid2;
                blockChangeInfo2.type = b2;
                blockChangeInfo2.x = 0;
                blockChangeInfo2.y = 0;
                blockChangeInfo2.z = 0;
                p.blockchangeObject = blockChangeInfo2;
            }
            else
            {
                BlockChangeInfo blockChangeInfo3 = default(BlockChangeInfo);
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

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/megaboid [block] [type] - create a cuboid of blocks.");
            Player.SendMessage(p, "Available types:");
            Player.SendMessage(p, "solid, hollow, walls, holes, wire, random");
            Player.SendMessage(p, "Shortcut: /zm");
        }

        public void Blockchange1(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            byte tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            BlockChangeInfo blockChangeInfo = (BlockChangeInfo)p.blockchangeObject;
            blockChangeInfo.x = x;
            blockChangeInfo.y = y;
            blockChangeInfo.z = z;
            p.blockchangeObject = blockChangeInfo;
            p.Blockchange += Blockchange2;
        }

        public void Blockchange2(Player p, ushort x, ushort y, ushort z, byte type)
        {
            Timer megaTimer = new Timer(1.0);
            p.ClearBlockchange();
            byte tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            BlockChangeInfo cpos = (BlockChangeInfo)p.blockchangeObject;
            if (cpos.type != byte.MaxValue)
            {
                type = cpos.type;
            }
            else
            {
                type = p.bindings[type];
            }
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
                case CuboidType.Holes:
                {
                    bool flag = true;
                    for (ushort num7 = Math.Min(cpos.x, x); num7 <= Math.Max(cpos.x, x); num7++)
                    {
                        bool flag2 = flag;
                        for (ushort num8 = Math.Min(cpos.y, y); num8 <= Math.Max(cpos.y, y); num8++)
                        {
                            bool flag3 = flag;
                            for (ushort num9 = Math.Min(cpos.z, z); num9 <= Math.Max(cpos.z, z); num9++)
                            {
                                flag = !flag;
                                if (flag && p.level.GetTile(num7, num8, num9) != type)
                                {
                                    buffer.Add(new Pos(num7, num8, num9));
                                }
                            }
                            flag = !flag3;
                        }
                        flag = !flag2;
                    }
                    break;
                }
                case CuboidType.Wire:
                {
                    for (ushort num4 = Math.Min(cpos.x, x); num4 <= Math.Max(cpos.x, x); num4++)
                    {
                        buffer.Add(new Pos(num4, y, z));
                        buffer.Add(new Pos(num4, y, cpos.z));
                        buffer.Add(new Pos(num4, cpos.y, z));
                        buffer.Add(new Pos(num4, cpos.y, cpos.z));
                    }
                    for (ushort num5 = Math.Min(cpos.y, y); num5 <= Math.Max(cpos.y, y); num5++)
                    {
                        buffer.Add(new Pos(x, num5, z));
                        buffer.Add(new Pos(x, num5, cpos.z));
                        buffer.Add(new Pos(cpos.x, num5, z));
                        buffer.Add(new Pos(cpos.x, num5, cpos.z));
                    }
                    for (ushort num6 = Math.Min(cpos.z, z); num6 <= Math.Max(cpos.z, z); num6++)
                    {
                        buffer.Add(new Pos(x, y, num6));
                        buffer.Add(new Pos(x, cpos.y, num6));
                        buffer.Add(new Pos(cpos.x, y, num6));
                        buffer.Add(new Pos(cpos.x, cpos.y, num6));
                    }
                    break;
                }
                case CuboidType.Random:
                {
                    Random random = new Random();
                    for (ushort num = Math.Min(cpos.x, x); num <= Math.Max(cpos.x, x); num++)
                    {
                        for (ushort num2 = Math.Min(cpos.y, y); num2 <= Math.Max(cpos.y, y); num2++)
                        {
                            for (ushort num3 = Math.Min(cpos.z, z); num3 <= Math.Max(cpos.z, z); num3++)
                            {
                                if (random.Next(1, 11) <= 5 && p.level.GetTile(num, num2, num3) != type)
                                {
                                    buffer.Add(new Pos(num, num2, num3));
                                }
                            }
                        }
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
            int CurrentLoop = 0;
            Level currentLevel = p.level;
            int unallowedBlocks = 0;
            Pos pos;
            megaTimer.Elapsed += delegate
            {
                if (!p.megaBoid || p.disconnected)
                {
                    megaTimer.Close();
                }
                else
                {
                    pos = buffer[CurrentLoop];
                    try
                    {
                        if (!currentLevel.BlockchangeChecks(p, pos.x, pos.y, pos.z, type, currentLevel.GetTile(pos.x, pos.y, pos.z)))
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
                    catch {}
                    CurrentLoop++;
                    if (CurrentLoop % 1000 == 0)
                    {
                        Player.SendMessage(p, string.Format("{0} blocks down, {1} to go.", CurrentLoop, buffer.Count - CurrentLoop));
                    }
                    if (CurrentLoop >= buffer.Count)
                    {
                        Player.SendMessage(p, "Megaboid completed.");
                        p.megaBoid = false;
                        megaTimer.Close();
                    }
                    megaTimer.Start();
                }
            };
            megaTimer.AutoReset = false;
            megaTimer.Start();
            if (p.staticCommands)
            {
                p.Blockchange += Blockchange1;
            }
        }

        static void DrawWalls(Player p, ushort x, ushort y, ushort z, byte type, BlockChangeInfo cpos, List<Pos> buffer)
        {
            for (ushort num = Math.Min(cpos.y, y); num <= Math.Max(cpos.y, y); num++)
            {
                for (ushort num2 = Math.Min(cpos.z, z); num2 <= Math.Max(cpos.z, z); num2++)
                {
                    if (p.level.GetTile(cpos.x, num, num2) != type)
                    {
                        buffer.Add(new Pos(cpos.x, num, num2));
                    }
                    if (cpos.x != x && p.level.GetTile(x, num, num2) != type)
                    {
                        buffer.Add(new Pos(x, num, num2));
                    }
                }
            }
            if (Math.Abs(cpos.x - x) < 2 || Math.Abs(cpos.z - z) < 2)
            {
                return;
            }
            for (ushort num3 = (ushort)(Math.Min(cpos.x, x) + 1); num3 <= Math.Max(cpos.x, x) - 1; num3++)
            {
                for (ushort num4 = Math.Min(cpos.y, y); num4 <= Math.Max(cpos.y, y); num4++)
                {
                    if (p.level.GetTile(num3, num4, cpos.z) != type)
                    {
                        buffer.Add(new Pos(num3, num4, cpos.z));
                    }
                    if (cpos.z != z && p.level.GetTile(num3, num4, z) != type)
                    {
                        buffer.Add(new Pos(num3, num4, z));
                    }
                }
            }
        }

        static void DrawHollow(Player p, ushort x, ushort y, ushort z, byte type, BlockChangeInfo cpos, List<Pos> buffer)
        {
            for (ushort num = Math.Min(cpos.y, y); num <= Math.Max(cpos.y, y); num++)
            {
                for (ushort num2 = Math.Min(cpos.z, z); num2 <= Math.Max(cpos.z, z); num2++)
                {
                    if (p.level.GetTile(cpos.x, num, num2) != type)
                    {
                        buffer.Add(new Pos(cpos.x, num, num2));
                    }
                    if (cpos.x != x && p.level.GetTile(x, num, num2) != type)
                    {
                        buffer.Add(new Pos(x, num, num2));
                    }
                }
            }
            if (Math.Abs(cpos.x - x) < 2)
            {
                return;
            }
            for (ushort num3 = (ushort)(Math.Min(cpos.x, x) + 1); num3 <= Math.Max(cpos.x, x) - 1; num3++)
            {
                for (ushort num4 = Math.Min(cpos.z, z); num4 <= Math.Max(cpos.z, z); num4++)
                {
                    if (p.level.GetTile(num3, cpos.y, num4) != type)
                    {
                        buffer.Add(new Pos(num3, cpos.y, num4));
                    }
                    if (cpos.y != y && p.level.GetTile(num3, y, num4) != type)
                    {
                        buffer.Add(new Pos(num3, y, num4));
                    }
                }
            }
            if (Math.Abs(cpos.y - y) < 2)
            {
                return;
            }
            for (ushort num5 = (ushort)(Math.Min(cpos.x, x) + 1); num5 <= Math.Max(cpos.x, x) - 1; num5++)
            {
                for (ushort num6 = (ushort)(Math.Min(cpos.y, y) + 1); num6 <= Math.Max(cpos.y, y) - 1; num6++)
                {
                    if (p.level.GetTile(num5, num6, cpos.z) != type)
                    {
                        buffer.Add(new Pos(num5, num6, cpos.z));
                    }
                    if (cpos.z != z && p.level.GetTile(num5, num6, z) != type)
                    {
                        buffer.Add(new Pos(num5, num6, z));
                    }
                }
            }
        }

        static void DrawSolid(Player p, ushort x, ushort y, ushort z, byte type, BlockChangeInfo cpos, List<Pos> buffer)
        {
            for (ushort num = Math.Min(cpos.x, x); num <= Math.Max(cpos.x, x); num++)
            {
                for (ushort num2 = Math.Min(cpos.y, y); num2 <= Math.Max(cpos.y, y); num2++)
                {
                    for (ushort num3 = Math.Min(cpos.z, z); num3 <= Math.Max(cpos.z, z); num3++)
                    {
                        if (p.level.GetTile(num, num2, num3) != type)
                        {
                            buffer.Add(new Pos(num, num2, num3));
                        }
                    }
                }
            }
        }

        struct Pos
        {
            public readonly ushort x;

            public readonly ushort y;

            public readonly ushort z;

            public Pos(ushort x, ushort y, ushort z)
            {
                this.x = x;
                this.y = y;
                this.z = z;
            }
        }

        struct BlockChangeInfo
        {
            public CuboidType solid;

            public byte type;

            public ushort x;

            public ushort y;

            public ushort z;
        }

        enum CuboidType
        {
            Solid,
            Hollow,
            Walls,
            Wire,
            Random,
            Holes
        }
    }
}