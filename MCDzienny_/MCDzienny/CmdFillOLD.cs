using System;
using System.Collections.Generic;

namespace MCDzienny
{
    public class CmdFillOLD : Command
    {

        readonly List<Pos> fromWhere = new List<Pos>();

        int deep;

        public override string name { get { return "fillold"; } }

        public override string shortcut { get { return "f_"; } }

        public override string type { get { return "build"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }

        public override void Use(Player p, string message)
        {
            int num = message.Split(' ').Length;
            if (num > 2)
            {
                Help(p);
                return;
            }
            CatchPos catchPos = default(CatchPos);
            if (num == 2)
            {
                int num2 = message.IndexOf(' ');
                string text = message.Substring(0, num2).ToLower();
                string text2 = message.Substring(num2 + 1).ToLower();
                catchPos.type = Block.Byte(text);
                if (catchPos.type == byte.MaxValue)
                {
                    Player.SendMessage(p, "There is no block \"" + text + "\".");
                    return;
                }
                if (!Block.canPlace(p, catchPos.type))
                {
                    Player.SendMessage(p, "Cannot place that.");
                    return;
                }
                switch (text2)
                {
                    case "up":
                        catchPos.FillType = 1;
                        break;
                    case "down":
                        catchPos.FillType = 2;
                        break;
                    case "layer":
                        catchPos.FillType = 3;
                        break;
                    case "vertical_x":
                        catchPos.FillType = 4;
                        break;
                    case "vertical_z":
                        catchPos.FillType = 5;
                        break;
                    default:
                        Player.SendMessage(p, "Invalid fill type");
                        return;
                }
            }
            else if (message != "")
            {
                message = message.ToLower();
                switch (message)
                {
                    case "up":
                        catchPos.FillType = 1;
                        catchPos.type = byte.MaxValue;
                        break;
                    case "down":
                        catchPos.FillType = 2;
                        catchPos.type = byte.MaxValue;
                        break;
                    case "layer":
                        catchPos.FillType = 3;
                        catchPos.type = byte.MaxValue;
                        break;
                    case "vertical_x":
                        catchPos.FillType = 4;
                        catchPos.type = byte.MaxValue;
                        break;
                    case "vertical_z":
                        catchPos.FillType = 5;
                        catchPos.type = byte.MaxValue;
                        break;
                    default:
                        catchPos.type = Block.Byte(message);
                        if (catchPos.type == byte.MaxValue)
                        {
                            Player.SendMessage(p, "Invalid block or fill type");
                            return;
                        }
                        if (!Block.canPlace(p, catchPos.type))
                        {
                            Player.SendMessage(p, "Cannot place that.");
                            return;
                        }
                        catchPos.FillType = 0;
                        break;
                }
            }
            else
            {
                catchPos.type = byte.MaxValue;
                catchPos.FillType = 0;
            }
            catchPos.x = 0;
            catchPos.y = 0;
            catchPos.z = 0;
            p.blockchangeObject = catchPos;
            Player.SendMessage(p, "Destroy the block you wish to fill.");
            p.ClearBlockchange();
            p.Blockchange += Blockchange1;
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/fill [block] [type] - Fills the area specified with [block].");
            Player.SendMessage(p, "[types] - up, down, layer, vertical_x, vertical_z");
        }

        public void Blockchange1(Player p, ushort x, ushort y, ushort z, byte type)
        {
            try
            {
                p.ClearBlockchange();
                CatchPos catchPos = (CatchPos)p.blockchangeObject;
                if (catchPos.type == byte.MaxValue)
                {
                    catchPos.type = p.bindings[type];
                }
                byte tile = p.level.GetTile(x, y, z);
                p.SendBlockchange(x, y, z, tile);
                if (catchPos.type == tile)
                {
                    Player.SendMessage(p, "Cannot fill the same time");
                    return;
                }
                if (!Block.canPlace(p, tile) && !Block.BuildIn(tile))
                {
                    Player.SendMessage(p, "Cannot fill that.");
                    return;
                }
                byte[] blocks = new byte[p.level.blocks.Length];
                var buffer = new List<Pos>();
                p.level.blocks.CopyTo(blocks, 0);
                fromWhere.Clear();
                deep = 0;
                FloodFill(p, x, y, z, catchPos.type, tile, catchPos.FillType, ref blocks, ref buffer);
                int count = fromWhere.Count;
                for (int i = 0; i < count; i++)
                {
                    count = fromWhere.Count;
                    Pos pos = fromWhere[i];
                    deep = 0;
                    FloodFill(p, pos.x, pos.y, pos.z, catchPos.type, tile, catchPos.FillType, ref blocks, ref buffer);
                    count = fromWhere.Count;
                }
                fromWhere.Clear();
                if (buffer.Count > p.group.maxBlocks)
                {
                    Player.SendMessage(p, "You tried to fill " + buffer.Count + " blocks.");
                    Player.SendMessage(p, "You cannot fill more than " + p.group.maxBlocks + ".");
                    return;
                }
                foreach (Pos item in buffer)
                {
                    p.level.Blockchange(p, item.x, item.y, item.z, catchPos.type);
                }
                Player.SendMessage(p, "Filled " + buffer.Count + " blocks.");
                buffer.Clear();
                if (p.staticCommands)
                {
                    p.Blockchange += Blockchange1;
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        public void FloodFill(Player p, ushort x, ushort y, ushort z, byte b, byte oldType, int fillType, ref byte[] blocks, ref List<Pos> buffer)
        {
            try
            {
                Pos item = default(Pos);
                item.x = x;
                item.y = y;
                item.z = z;
                if (deep > 4000)
                {
                    fromWhere.Add(item);
                    return;
                }
                blocks[x + p.level.width * z + p.level.width * p.level.depth * y] = b;
                buffer.Add(item);
                if (fillType != 4)
                {
                    if (GetTile((ushort)(x + 1), y, z, p.level, blocks) == oldType)
                    {
                        deep++;
                        FloodFill(p, (ushort)(x + 1), y, z, b, oldType, fillType, ref blocks, ref buffer);
                        deep--;
                    }
                    if (x - 1 > 0 && GetTile((ushort)(x - 1), y, z, p.level, blocks) == oldType)
                    {
                        deep++;
                        FloodFill(p, (ushort)(x - 1), y, z, b, oldType, fillType, ref blocks, ref buffer);
                        deep--;
                    }
                }
                if (fillType != 5)
                {
                    if (GetTile(x, y, (ushort)(z + 1), p.level, blocks) == oldType)
                    {
                        deep++;
                        FloodFill(p, x, y, (ushort)(z + 1), b, oldType, fillType, ref blocks, ref buffer);
                        deep--;
                    }
                    if (z - 1 > 0 && GetTile(x, y, (ushort)(z - 1), p.level, blocks) == oldType)
                    {
                        deep++;
                        FloodFill(p, x, y, (ushort)(z - 1), b, oldType, fillType, ref blocks, ref buffer);
                        deep--;
                    }
                }
                if ((fillType == 0 || fillType == 1 || fillType > 3) && GetTile(x, (ushort)(y + 1), z, p.level, blocks) == oldType)
                {
                    deep++;
                    FloodFill(p, x, (ushort)(y + 1), z, b, oldType, fillType, ref blocks, ref buffer);
                    deep--;
                }
                if ((fillType == 0 || fillType == 2 || fillType > 3) && y - 1 > 0 && GetTile(x, (ushort)(y - 1), z, p.level, blocks) == oldType)
                {
                    deep++;
                    FloodFill(p, x, (ushort)(y - 1), z, b, oldType, fillType, ref blocks, ref buffer);
                    deep--;
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        public byte GetTile(ushort x, ushort y, ushort z, Level l, byte[] blocks)
        {
            if (x < 0)
            {
                return byte.MaxValue;
            }
            if (x >= l.width)
            {
                return byte.MaxValue;
            }
            if (y < 0)
            {
                return byte.MaxValue;
            }
            if (y >= l.height)
            {
                return byte.MaxValue;
            }
            if (z < 0)
            {
                return byte.MaxValue;
            }
            if (z >= l.depth)
            {
                return byte.MaxValue;
            }
            try
            {
                return blocks[l.PosToInt(x, y, z)];
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
                return byte.MaxValue;
            }
        }

        struct CatchPos
        {
            public ushort x;

            public ushort y;

            public ushort z;

            public byte type;

            public int FillType;
        }

        public struct Pos
        {
            public ushort x;

            public ushort y;

            public ushort z;
        }
    }
}