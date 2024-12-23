using System;
using System.Collections.Generic;
using MCDzienny.Settings;

namespace MCDzienny
{
    public class CmdCuboid : Command
    {

        public static readonly int DefaultRandomFactor = 50;

        public override string name { get { return "cuboid"; } }

        public override string shortcut { get { return "z"; } }

        public override string type { get { return "build"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Builder; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            if (!LavaSettings.All.AllowCuboidOnLavaMaps && p.level.mapType == MapType.Lava && p.group.Permission < LevelPermission.Admin)
            {
                Player.SendMessage(p, "Only admin is allowed to use this command on lava map");
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
                string text = message.Substring(0, num2).ToLower();
                string text2 = message.Substring(num2 + 1).ToLower();
                int result = DefaultRandomFactor;
                SolidType solid;
                if (text.ToLower() == "random")
                {
                    solid = SolidType.random;
                    if (!int.TryParse(text2, out result))
                    {
                        Player.SendMessage(p, "Incorrect random factor. Should be within 0..100 range");
                        return;
                    }
                    CatchPos catchPos = default(CatchPos);
                    catchPos.solid = solid;
                    catchPos.type = byte.MaxValue;
                    catchPos.x = 0;
                    catchPos.y = 0;
                    catchPos.z = 0;
                    catchPos.randomFactor = result;
                    p.blockchangeObject = catchPos;
                    Player.SendMessage(p, "Place two blocks to determine the edges.");
                    p.ClearBlockchange();
                    p.Blockchange += Blockchange1;
                    return;
                }
                byte b = Block.Byte(text);
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
                switch (text2)
                {
                    case "solid":
                        solid = SolidType.solid;
                        break;
                    case "hollow":
                        solid = SolidType.hollow;
                        break;
                    case "walls":
                        solid = SolidType.walls;
                        break;
                    case "holes":
                        solid = SolidType.holes;
                        break;
                    case "wire":
                        solid = SolidType.wire;
                        break;
                    case "random":
                        solid = SolidType.random;
                        break;
                    default:
                        Help(p);
                        return;
                }
                CatchPos catchPos2 = default(CatchPos);
                catchPos2.solid = solid;
                catchPos2.type = b;
                catchPos2.x = 0;
                catchPos2.y = 0;
                catchPos2.z = 0;
                catchPos2.randomFactor = result;
                p.blockchangeObject = catchPos2;
            }
            else if (message != "")
            {
                SolidType solid2 = SolidType.solid;
                message = message.ToLower();
                byte b2 = byte.MaxValue;
                switch (message)
                {
                    case "solid":
                        solid2 = SolidType.solid;
                        break;
                    case "hollow":
                        solid2 = SolidType.hollow;
                        break;
                    case "walls":
                        solid2 = SolidType.walls;
                        break;
                    case "holes":
                        solid2 = SolidType.holes;
                        break;
                    case "wire":
                        solid2 = SolidType.wire;
                        break;
                    case "random":
                        solid2 = SolidType.random;
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
                CatchPos catchPos3 = default(CatchPos);
                catchPos3.solid = solid2;
                catchPos3.type = b2;
                catchPos3.x = 0;
                catchPos3.y = 0;
                catchPos3.z = 0;
                catchPos3.randomFactor = DefaultRandomFactor;
                p.blockchangeObject = catchPos3;
            }
            else
            {
                CatchPos catchPos4 = default(CatchPos);
                catchPos4.solid = SolidType.solid;
                catchPos4.type = byte.MaxValue;
                catchPos4.x = 0;
                catchPos4.y = 0;
                catchPos4.z = 0;
                catchPos4.randomFactor = DefaultRandomFactor;
                p.blockchangeObject = catchPos4;
            }
            Player.SendMessage(p, "Place two blocks to determine the edges.");
            p.ClearBlockchange();
            p.Blockchange += Blockchange1;
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/cuboid [type] <solid/hollow/walls/holes/wire/random> - create a cuboid of blocks.");
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
            else
            {
                type = p.bindings[type];
            }
            if (!Block.canPlace(p, type))
            {
                Player.SendMessage(p, "Cannot place that.");
                return;
            }
            var list = new List<Pos>();
            switch (catchPos.solid)
            {
                case SolidType.solid:
                {
                    for (ushort num = Math.Min(catchPos.x, x); num <= Math.Max(catchPos.x, x); num++)
                    {
                        for (ushort num2 = Math.Min(catchPos.y, y); num2 <= Math.Max(catchPos.y, y); num2++)
                        {
                            for (ushort num3 = Math.Min(catchPos.z, z); num3 <= Math.Max(catchPos.z, z); num3++)
                            {
                                if (p.level.GetTile(num, num2, num3) != type)
                                {
                                    BufferAdd(list, num, num2, num3);
                                }
                            }
                        }
                    }
                    break;
                }
                case SolidType.hollow:
                {
                    for (ushort num2 = Math.Min(catchPos.y, y); num2 <= Math.Max(catchPos.y, y); num2++)
                    {
                        for (ushort num3 = Math.Min(catchPos.z, z); num3 <= Math.Max(catchPos.z, z); num3++)
                        {
                            if (p.level.GetTile(catchPos.x, num2, num3) != type)
                            {
                                BufferAdd(list, catchPos.x, num2, num3);
                            }
                            if (catchPos.x != x && p.level.GetTile(x, num2, num3) != type)
                            {
                                BufferAdd(list, x, num2, num3);
                            }
                        }
                    }
                    if (Math.Abs(catchPos.x - x) < 2)
                    {
                        break;
                    }
                    for (ushort num = (ushort)(Math.Min(catchPos.x, x) + 1); num <= Math.Max(catchPos.x, x) - 1; num++)
                    {
                        for (ushort num3 = Math.Min(catchPos.z, z); num3 <= Math.Max(catchPos.z, z); num3++)
                        {
                            if (p.level.GetTile(num, catchPos.y, num3) != type)
                            {
                                BufferAdd(list, num, catchPos.y, num3);
                            }
                            if (catchPos.y != y && p.level.GetTile(num, y, num3) != type)
                            {
                                BufferAdd(list, num, y, num3);
                            }
                        }
                    }
                    if (Math.Abs(catchPos.y - y) < 2)
                    {
                        break;
                    }
                    for (ushort num = (ushort)(Math.Min(catchPos.x, x) + 1); num <= Math.Max(catchPos.x, x) - 1; num++)
                    {
                        for (ushort num2 = (ushort)(Math.Min(catchPos.y, y) + 1); num2 <= Math.Max(catchPos.y, y) - 1; num2++)
                        {
                            if (p.level.GetTile(num, num2, catchPos.z) != type)
                            {
                                BufferAdd(list, num, num2, catchPos.z);
                            }
                            if (catchPos.z != z && p.level.GetTile(num, num2, z) != type)
                            {
                                BufferAdd(list, num, num2, z);
                            }
                        }
                    }
                    break;
                }
                case SolidType.walls:
                {
                    for (ushort num2 = Math.Min(catchPos.y, y); num2 <= Math.Max(catchPos.y, y); num2++)
                    {
                        for (ushort num3 = Math.Min(catchPos.z, z); num3 <= Math.Max(catchPos.z, z); num3++)
                        {
                            if (p.level.GetTile(catchPos.x, num2, num3) != type)
                            {
                                BufferAdd(list, catchPos.x, num2, num3);
                            }
                            if (catchPos.x != x && p.level.GetTile(x, num2, num3) != type)
                            {
                                BufferAdd(list, x, num2, num3);
                            }
                        }
                    }
                    if (Math.Abs(catchPos.x - x) < 2 || Math.Abs(catchPos.z - z) < 2)
                    {
                        break;
                    }
                    for (ushort num = (ushort)(Math.Min(catchPos.x, x) + 1); num <= Math.Max(catchPos.x, x) - 1; num++)
                    {
                        for (ushort num2 = Math.Min(catchPos.y, y); num2 <= Math.Max(catchPos.y, y); num2++)
                        {
                            if (p.level.GetTile(num, num2, catchPos.z) != type)
                            {
                                BufferAdd(list, num, num2, catchPos.z);
                            }
                            if (catchPos.z != z && p.level.GetTile(num, num2, z) != type)
                            {
                                BufferAdd(list, num, num2, z);
                            }
                        }
                    }
                    break;
                }
                case SolidType.holes:
                {
                    bool flag = true;
                    for (ushort num = Math.Min(catchPos.x, x); num <= Math.Max(catchPos.x, x); num++)
                    {
                        bool flag2 = flag;
                        for (ushort num2 = Math.Min(catchPos.y, y); num2 <= Math.Max(catchPos.y, y); num2++)
                        {
                            bool flag3 = flag;
                            for (ushort num3 = Math.Min(catchPos.z, z); num3 <= Math.Max(catchPos.z, z); num3++)
                            {
                                flag = !flag;
                                if (flag && p.level.GetTile(num, num2, num3) != type)
                                {
                                    BufferAdd(list, num, num2, num3);
                                }
                            }
                            flag = !flag3;
                        }
                        flag = !flag2;
                    }
                    break;
                }
                case SolidType.wire:
                {
                    for (ushort num = Math.Min(catchPos.x, x); num <= Math.Max(catchPos.x, x); num++)
                    {
                        BufferAdd(list, num, y, z);
                        BufferAdd(list, num, y, catchPos.z);
                        BufferAdd(list, num, catchPos.y, z);
                        BufferAdd(list, num, catchPos.y, catchPos.z);
                    }
                    for (ushort num2 = Math.Min(catchPos.y, y); num2 <= Math.Max(catchPos.y, y); num2++)
                    {
                        BufferAdd(list, x, num2, z);
                        BufferAdd(list, x, num2, catchPos.z);
                        BufferAdd(list, catchPos.x, num2, z);
                        BufferAdd(list, catchPos.x, num2, catchPos.z);
                    }
                    for (ushort num3 = Math.Min(catchPos.z, z); num3 <= Math.Max(catchPos.z, z); num3++)
                    {
                        BufferAdd(list, x, y, num3);
                        BufferAdd(list, x, catchPos.y, num3);
                        BufferAdd(list, catchPos.x, y, num3);
                        BufferAdd(list, catchPos.x, catchPos.y, num3);
                    }
                    break;
                }
                case SolidType.random:
                {
                    Random random = new Random();
                    for (ushort num = Math.Min(catchPos.x, x); num <= Math.Max(catchPos.x, x); num++)
                    {
                        for (ushort num2 = Math.Min(catchPos.y, y); num2 <= Math.Max(catchPos.y, y); num2++)
                        {
                            for (ushort num3 = Math.Min(catchPos.z, z); num3 <= Math.Max(catchPos.z, z); num3++)
                            {
                                if (random.Next(0, 100) <= catchPos.randomFactor && p.level.GetTile(num, num2, num3) != type)
                                {
                                    BufferAdd(list, num, num2, num3);
                                }
                            }
                        }
                    }
                    break;
                }
            }
            if (Server.forceCuboid)
            {
                int counter = 1;
                list.ForEach(delegate(Pos pos)
                {
                    if (counter <= p.group.maxBlocks)
                    {
                        counter++;
                        p.level.Blockchange(p, pos.x, pos.y, pos.z, type);
                    }
                });
                if (counter >= p.group.maxBlocks)
                {
                    Player.SendMessage(p, string.Format("Tried to cuboid {0} blocks, but your limit is {1}.", list.Count, p.group.maxBlocks));
                    Player.SendMessage(p, "Executed cuboid up to limit.");
                }
                else
                {
                    Player.SendMessage(p, string.Format("{0} blocks.", list.Count.ToString()));
                }
                if (p.staticCommands)
                {
                    p.Blockchange += Blockchange1;
                }
            }
            else if (list.Count > p.group.maxBlocks)
            {
                Player.SendMessage(p, string.Format("You tried to cuboid {0} blocks.", list.Count));
                Player.SendMessage(p, string.Format("You cannot cuboid more than {0}.", p.group.maxBlocks));
            }
            else
            {
                Player.SendMessage(p, string.Format("{0} blocks.", list.Count.ToString()));
                list.ForEach(delegate(Pos pos) { p.level.Blockchange(p, pos.x, pos.y, pos.z, type); });
                if (p.staticCommands)
                {
                    p.Blockchange += Blockchange1;
                }
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
            public SolidType solid;

            public byte type;

            public ushort x;

            public ushort y;

            public ushort z;

            public int randomFactor;
        }

        enum SolidType
        {
            solid,
            hollow,
            walls,
            holes,
            wire,
            random
        }
    }
}