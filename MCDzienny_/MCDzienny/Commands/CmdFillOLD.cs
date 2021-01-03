using System;
using System.Collections.Generic;

namespace MCDzienny
{
    // Token: 0x0200025E RID: 606
    public class CmdFillOLD : Command
    {
        // Token: 0x040008F1 RID: 2289
        private int deep;

        // Token: 0x040008F2 RID: 2290
        private readonly List<Pos> fromWhere = new List<Pos>();

        // Token: 0x17000656 RID: 1622
        // (get) Token: 0x0600117F RID: 4479 RVA: 0x000601E8 File Offset: 0x0005E3E8
        public override string name
        {
            get { return "fillold"; }
        }

        // Token: 0x17000657 RID: 1623
        // (get) Token: 0x06001180 RID: 4480 RVA: 0x000601F0 File Offset: 0x0005E3F0
        public override string shortcut
        {
            get { return "f_"; }
        }

        // Token: 0x17000658 RID: 1624
        // (get) Token: 0x06001181 RID: 4481 RVA: 0x000601F8 File Offset: 0x0005E3F8
        public override string type
        {
            get { return "build"; }
        }

        // Token: 0x17000659 RID: 1625
        // (get) Token: 0x06001182 RID: 4482 RVA: 0x00060200 File Offset: 0x0005E400
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x1700065A RID: 1626
        // (get) Token: 0x06001183 RID: 4483 RVA: 0x00060204 File Offset: 0x0005E404
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.AdvBuilder; }
        }

        // Token: 0x06001185 RID: 4485 RVA: 0x0006021C File Offset: 0x0005E41C
        public override void Use(Player p, string message)
        {
            var num = message.Split(' ').Length;
            if (num > 2)
            {
                Help(p);
                return;
            }

            CatchPos catchPos;
            if (num == 2)
            {
                var num2 = message.IndexOf(' ');
                var text = message.Substring(0, num2).ToLower();
                var a = message.Substring(num2 + 1).ToLower();
                catchPos.type = Block.Byte(text);
                if (catchPos.type == 255)
                {
                    Player.SendMessage(p, "There is no block \"" + text + "\".");
                    return;
                }

                if (!Block.canPlace(p, catchPos.type))
                {
                    Player.SendMessage(p, "Cannot place that.");
                    return;
                }

                if (a == "up")
                {
                    catchPos.FillType = 1;
                }
                else if (a == "down")
                {
                    catchPos.FillType = 2;
                }
                else if (a == "layer")
                {
                    catchPos.FillType = 3;
                }
                else if (a == "vertical_x")
                {
                    catchPos.FillType = 4;
                }
                else
                {
                    if (!(a == "vertical_z"))
                    {
                        Player.SendMessage(p, "Invalid fill type");
                        return;
                    }

                    catchPos.FillType = 5;
                }
            }
            else if (message != "")
            {
                message = message.ToLower();
                if (message == "up")
                {
                    catchPos.FillType = 1;
                    catchPos.type = byte.MaxValue;
                }
                else if (message == "down")
                {
                    catchPos.FillType = 2;
                    catchPos.type = byte.MaxValue;
                }
                else if (message == "layer")
                {
                    catchPos.FillType = 3;
                    catchPos.type = byte.MaxValue;
                }
                else if (message == "vertical_x")
                {
                    catchPos.FillType = 4;
                    catchPos.type = byte.MaxValue;
                }
                else if (message == "vertical_z")
                {
                    catchPos.FillType = 5;
                    catchPos.type = byte.MaxValue;
                }
                else
                {
                    catchPos.type = Block.Byte(message);
                    if (catchPos.type == 255)
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

        // Token: 0x06001186 RID: 4486 RVA: 0x000604D8 File Offset: 0x0005E6D8
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/fill [block] [type] - Fills the area specified with [block].");
            Player.SendMessage(p, "[types] - up, down, layer, vertical_x, vertical_z");
        }

        // Token: 0x06001187 RID: 4487 RVA: 0x000604F0 File Offset: 0x0005E6F0
        public void Blockchange1(Player p, ushort x, ushort y, ushort z, byte type)
        {
            try
            {
                p.ClearBlockchange();
                var catchPos = (CatchPos) p.blockchangeObject;
                if (catchPos.type == 255) catchPos.type = p.bindings[type];
                var tile = p.level.GetTile(x, y, z);
                p.SendBlockchange(x, y, z, tile);
                if (catchPos.type == tile)
                {
                    Player.SendMessage(p, "Cannot fill the same time");
                }
                else if (!Block.canPlace(p, tile) && !Block.BuildIn(tile))
                {
                    Player.SendMessage(p, "Cannot fill that.");
                }
                else
                {
                    var array = new byte[p.level.blocks.Length];
                    var list = new List<Pos>();
                    p.level.blocks.CopyTo(array, 0);
                    fromWhere.Clear();
                    deep = 0;
                    FloodFill(p, x, y, z, catchPos.type, tile, catchPos.FillType, ref array, ref list);
                    var count = fromWhere.Count;
                    for (var i = 0; i < count; i++)
                    {
                        count = fromWhere.Count;
                        var pos = fromWhere[i];
                        deep = 0;
                        FloodFill(p, pos.x, pos.y, pos.z, catchPos.type, tile, catchPos.FillType, ref array, ref list);
                        count = fromWhere.Count;
                    }

                    fromWhere.Clear();
                    if (list.Count > p.group.maxBlocks)
                    {
                        Player.SendMessage(p, "You tried to fill " + list.Count + " blocks.");
                        Player.SendMessage(p, "You cannot fill more than " + p.group.maxBlocks + ".");
                    }
                    else
                    {
                        foreach (var pos2 in list) p.level.Blockchange(p, pos2.x, pos2.y, pos2.z, catchPos.type);
                        Player.SendMessage(p, "Filled " + list.Count + " blocks.");
                        list.Clear();
                        if (p.staticCommands) p.Blockchange += Blockchange1;
                    }
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        // Token: 0x06001188 RID: 4488 RVA: 0x00060798 File Offset: 0x0005E998
        public void FloodFill(Player p, ushort x, ushort y, ushort z, byte b, byte oldType, int fillType,
            ref byte[] blocks, ref List<Pos> buffer)
        {
            try
            {
                var item = default(Pos);
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
                    if (GetTile((ushort) (x + 1), y, z, p.level, blocks) == oldType)
                    {
                        deep++;
                        FloodFill(p, (ushort) (x + 1), y, z, b, oldType, fillType, ref blocks, ref buffer);
                        deep--;
                    }

                    if (x - 1 > 0 && GetTile((ushort) (x - 1), y, z, p.level, blocks) == oldType)
                    {
                        deep++;
                        FloodFill(p, (ushort) (x - 1), y, z, b, oldType, fillType, ref blocks, ref buffer);
                        deep--;
                    }
                }

                if (fillType != 5)
                {
                    if (GetTile(x, y, (ushort) (z + 1), p.level, blocks) == oldType)
                    {
                        deep++;
                        FloodFill(p, x, y, (ushort) (z + 1), b, oldType, fillType, ref blocks, ref buffer);
                        deep--;
                    }

                    if (z - 1 > 0 && GetTile(x, y, (ushort) (z - 1), p.level, blocks) == oldType)
                    {
                        deep++;
                        FloodFill(p, x, y, (ushort) (z - 1), b, oldType, fillType, ref blocks, ref buffer);
                        deep--;
                    }
                }

                if ((fillType == 0 || fillType == 1 || fillType > 3) &&
                    GetTile(x, (ushort) (y + 1), z, p.level, blocks) == oldType)
                {
                    deep++;
                    FloodFill(p, x, (ushort) (y + 1), z, b, oldType, fillType, ref blocks, ref buffer);
                    deep--;
                }

                if ((fillType == 0 || fillType == 2 || fillType > 3) && y - 1 > 0 &&
                    GetTile(x, (ushort) (y - 1), z, p.level, blocks) == oldType)
                {
                    deep++;
                    FloodFill(p, x, (ushort) (y - 1), z, b, oldType, fillType, ref blocks, ref buffer);
                    deep--;
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        // Token: 0x06001189 RID: 4489 RVA: 0x00060A50 File Offset: 0x0005EC50
        public byte GetTile(ushort x, ushort y, ushort z, Level l, byte[] blocks)
        {
            if (x < 0) return byte.MaxValue;
            if (x >= l.width) return byte.MaxValue;
            if (y < 0) return byte.MaxValue;
            if (y >= l.height) return byte.MaxValue;
            if (z < 0) return byte.MaxValue;
            if (z >= l.depth) return byte.MaxValue;
            byte result;
            try
            {
                result = blocks[l.PosToInt(x, y, z)];
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
                result = byte.MaxValue;
            }

            return result;
        }

        // Token: 0x0200025F RID: 607
        private struct CatchPos
        {
            // Token: 0x040008F3 RID: 2291
            public ushort x;

            // Token: 0x040008F4 RID: 2292
            public ushort y;

            // Token: 0x040008F5 RID: 2293
            public ushort z;

            // Token: 0x040008F6 RID: 2294
            public byte type;

            // Token: 0x040008F7 RID: 2295
            public int FillType;
        }

        // Token: 0x02000260 RID: 608
        public struct Pos
        {
            // Token: 0x040008F8 RID: 2296
            public ushort x;

            // Token: 0x040008F9 RID: 2297
            public ushort y;

            // Token: 0x040008FA RID: 2298
            public ushort z;
        }
    }
}