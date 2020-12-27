using System;
using System.Collections.Generic;
using MCDzienny.Settings;

namespace MCDzienny
{
    // Token: 0x02000258 RID: 600
    public class CmdCuboid : Command
    {
        // Token: 0x040008DC RID: 2268
        public static readonly int DefaultRandomFactor = 50;

        // Token: 0x17000650 RID: 1616
        // (get) Token: 0x0600116E RID: 4462 RVA: 0x0005F21C File Offset: 0x0005D41C
        public override string name
        {
            get { return "cuboid"; }
        }

        // Token: 0x17000651 RID: 1617
        // (get) Token: 0x0600116F RID: 4463 RVA: 0x0005F224 File Offset: 0x0005D424
        public override string shortcut
        {
            get { return "z"; }
        }

        // Token: 0x17000652 RID: 1618
        // (get) Token: 0x06001170 RID: 4464 RVA: 0x0005F22C File Offset: 0x0005D42C
        public override string type
        {
            get { return "build"; }
        }

        // Token: 0x17000653 RID: 1619
        // (get) Token: 0x06001171 RID: 4465 RVA: 0x0005F234 File Offset: 0x0005D434
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x17000654 RID: 1620
        // (get) Token: 0x06001172 RID: 4466 RVA: 0x0005F238 File Offset: 0x0005D438
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Builder; }
        }

        // Token: 0x17000655 RID: 1621
        // (get) Token: 0x06001173 RID: 4467 RVA: 0x0005F23C File Offset: 0x0005D43C
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x06001174 RID: 4468 RVA: 0x0005F240 File Offset: 0x0005D440
        public override void Use(Player p, string message)
        {
            if (!LavaSettings.All.AllowCuboidOnLavaMaps && p.level.mapType == MapType.Lava &&
                p.group.Permission < LevelPermission.Admin)
            {
                Player.SendMessage(p, "Only admin is allowed to use this command on lava map");
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
                var defaultRandomFactor = DefaultRandomFactor;
                if (text.ToLower() == "random")
                {
                    var solid = SolidType.random;
                    if (!int.TryParse(text2, out defaultRandomFactor))
                    {
                        Player.SendMessage(p, "Incorrect random factor. Should be within 0..100 range");
                        return;
                    }

                    CatchPos catchPos;
                    catchPos.solid = solid;
                    catchPos.type = byte.MaxValue;
                    catchPos.x = 0;
                    catchPos.y = 0;
                    catchPos.z = 0;
                    catchPos.randomFactor = defaultRandomFactor;
                    p.blockchangeObject = catchPos;
                    Player.SendMessage(p, "Place two blocks to determine the edges.");
                    p.ClearBlockchange();
                    p.Blockchange += Blockchange1;
                    return;
                }
                else
                {
                    var b = Block.Byte(text);
                    if (b == 255)
                    {
                        Player.SendMessage(p, string.Format("There is no block \"{0}\".", text));
                        return;
                    }

                    if (!Block.canPlace(p, b))
                    {
                        Player.SendMessage(p, "Cannot place that.");
                        return;
                    }

                    SolidType solid;
                    if (text2 == "solid")
                    {
                        solid = SolidType.solid;
                    }
                    else if (text2 == "hollow")
                    {
                        solid = SolidType.hollow;
                    }
                    else if (text2 == "walls")
                    {
                        solid = SolidType.walls;
                    }
                    else if (text2 == "holes")
                    {
                        solid = SolidType.holes;
                    }
                    else if (text2 == "wire")
                    {
                        solid = SolidType.wire;
                    }
                    else
                    {
                        if (!(text2 == "random"))
                        {
                            Help(p);
                            return;
                        }

                        solid = SolidType.random;
                    }

                    CatchPos catchPos2;
                    catchPos2.solid = solid;
                    catchPos2.type = b;
                    catchPos2.x = 0;
                    catchPos2.y = 0;
                    catchPos2.z = 0;
                    catchPos2.randomFactor = defaultRandomFactor;
                    p.blockchangeObject = catchPos2;
                }
            }
            else if (message != "")
            {
                var solid2 = SolidType.solid;
                message = message.ToLower();
                var type = byte.MaxValue;
                if (message == "solid")
                {
                    solid2 = SolidType.solid;
                }
                else if (message == "hollow")
                {
                    solid2 = SolidType.hollow;
                }
                else if (message == "walls")
                {
                    solid2 = SolidType.walls;
                }
                else if (message == "holes")
                {
                    solid2 = SolidType.holes;
                }
                else if (message == "wire")
                {
                    solid2 = SolidType.wire;
                }
                else if (message == "random")
                {
                    solid2 = SolidType.random;
                }
                else
                {
                    var b2 = Block.Byte(message);
                    if (b2 == 255)
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
                }

                CatchPos catchPos3;
                catchPos3.solid = solid2;
                catchPos3.type = type;
                catchPos3.x = 0;
                catchPos3.y = 0;
                catchPos3.z = 0;
                catchPos3.randomFactor = DefaultRandomFactor;
                p.blockchangeObject = catchPos3;
            }
            else
            {
                CatchPos catchPos4;
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

        // Token: 0x06001175 RID: 4469 RVA: 0x0005F5E0 File Offset: 0x0005D7E0
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/cuboid [type] <solid/hollow/walls/holes/wire/random> - create a cuboid of blocks.");
        }

        // Token: 0x06001176 RID: 4470 RVA: 0x0005F5F0 File Offset: 0x0005D7F0
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

        // Token: 0x06001177 RID: 4471 RVA: 0x0005F664 File Offset: 0x0005D864
        public void Blockchange2(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            var tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            var catchPos = (CatchPos) p.blockchangeObject;
            if (catchPos.type != byte.MaxValue)
                type = catchPos.type;
            else
                type = p.bindings[type];
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
                    for (var num = Math.Min(catchPos.x, x); num <= Math.Max(catchPos.x, x); num = (ushort) (num + 1))
                    for (var num2 = Math.Min(catchPos.y, y);
                        num2 <= Math.Max(catchPos.y, y);
                        num2 = (ushort) (num2 + 1))
                    for (var num3 = Math.Min(catchPos.z, z);
                        num3 <= Math.Max(catchPos.z, z);
                        num3 = (ushort) (num3 + 1))
                        if (p.level.GetTile(num, num2, num3) != type)
                            BufferAdd(list, num, num2, num3);
                    break;
                }
                case SolidType.hollow:
                {
                    for (var num2 = Math.Min(catchPos.y, y);
                        num2 <= Math.Max(catchPos.y, y);
                        num2 = (ushort) (num2 + 1))
                    for (var num3 = Math.Min(catchPos.z, z);
                        num3 <= Math.Max(catchPos.z, z);
                        num3 = (ushort) (num3 + 1))
                    {
                        if (p.level.GetTile(catchPos.x, num2, num3) != type) BufferAdd(list, catchPos.x, num2, num3);
                        if (catchPos.x != x && p.level.GetTile(x, num2, num3) != type) BufferAdd(list, x, num2, num3);
                    }

                    if (Math.Abs(catchPos.x - x) < 2) break;
                    for (var num = (ushort) (Math.Min(catchPos.x, x) + 1);
                        num <= Math.Max(catchPos.x, x) - 1;
                        num = (ushort) (num + 1))
                    for (var num3 = Math.Min(catchPos.z, z);
                        num3 <= Math.Max(catchPos.z, z);
                        num3 = (ushort) (num3 + 1))
                    {
                        if (p.level.GetTile(num, catchPos.y, num3) != type) BufferAdd(list, num, catchPos.y, num3);
                        if (catchPos.y != y && p.level.GetTile(num, y, num3) != type) BufferAdd(list, num, y, num3);
                    }

                    if (Math.Abs(catchPos.y - y) < 2) break;
                    for (var num = (ushort) (Math.Min(catchPos.x, x) + 1);
                        num <= Math.Max(catchPos.x, x) - 1;
                        num = (ushort) (num + 1))
                    for (var num2 = (ushort) (Math.Min(catchPos.y, y) + 1);
                        num2 <= Math.Max(catchPos.y, y) - 1;
                        num2 = (ushort) (num2 + 1))
                    {
                        if (p.level.GetTile(num, num2, catchPos.z) != type) BufferAdd(list, num, num2, catchPos.z);
                        if (catchPos.z != z && p.level.GetTile(num, num2, z) != type) BufferAdd(list, num, num2, z);
                    }

                    break;
                }
                case SolidType.walls:
                {
                    for (var num2 = Math.Min(catchPos.y, y);
                        num2 <= Math.Max(catchPos.y, y);
                        num2 = (ushort) (num2 + 1))
                    for (var num3 = Math.Min(catchPos.z, z);
                        num3 <= Math.Max(catchPos.z, z);
                        num3 = (ushort) (num3 + 1))
                    {
                        if (p.level.GetTile(catchPos.x, num2, num3) != type) BufferAdd(list, catchPos.x, num2, num3);
                        if (catchPos.x != x && p.level.GetTile(x, num2, num3) != type) BufferAdd(list, x, num2, num3);
                    }

                    if (Math.Abs(catchPos.x - x) < 2 || Math.Abs(catchPos.z - z) < 2) break;
                    for (var num = (ushort) (Math.Min(catchPos.x, x) + 1);
                        num <= Math.Max(catchPos.x, x) - 1;
                        num = (ushort) (num + 1))
                    for (var num2 = Math.Min(catchPos.y, y);
                        num2 <= Math.Max(catchPos.y, y);
                        num2 = (ushort) (num2 + 1))
                    {
                        if (p.level.GetTile(num, num2, catchPos.z) != type) BufferAdd(list, num, num2, catchPos.z);
                        if (catchPos.z != z && p.level.GetTile(num, num2, z) != type) BufferAdd(list, num, num2, z);
                    }

                    break;
                }
                case SolidType.holes:
                {
                    var flag = true;
                    for (var num = Math.Min(catchPos.x, x); num <= Math.Max(catchPos.x, x); num = (ushort) (num + 1))
                    {
                        var flag2 = flag;
                        for (var num2 = Math.Min(catchPos.y, y);
                            num2 <= Math.Max(catchPos.y, y);
                            num2 = (ushort) (num2 + 1))
                        {
                            var flag3 = flag;
                            for (var num3 = Math.Min(catchPos.z, z);
                                num3 <= Math.Max(catchPos.z, z);
                                num3 = (ushort) (num3 + 1))
                            {
                                flag = !flag;
                                if (flag && p.level.GetTile(num, num2, num3) != type) BufferAdd(list, num, num2, num3);
                            }

                            flag = !flag3;
                        }

                        flag = !flag2;
                    }

                    break;
                }
                case SolidType.wire:
                {
                    for (var num = Math.Min(catchPos.x, x); num <= Math.Max(catchPos.x, x); num = (ushort) (num + 1))
                    {
                        BufferAdd(list, num, y, z);
                        BufferAdd(list, num, y, catchPos.z);
                        BufferAdd(list, num, catchPos.y, z);
                        BufferAdd(list, num, catchPos.y, catchPos.z);
                    }

                    for (var num2 = Math.Min(catchPos.y, y);
                        num2 <= Math.Max(catchPos.y, y);
                        num2 = (ushort) (num2 + 1))
                    {
                        BufferAdd(list, x, num2, z);
                        BufferAdd(list, x, num2, catchPos.z);
                        BufferAdd(list, catchPos.x, num2, z);
                        BufferAdd(list, catchPos.x, num2, catchPos.z);
                    }

                    for (var num3 = Math.Min(catchPos.z, z);
                        num3 <= Math.Max(catchPos.z, z);
                        num3 = (ushort) (num3 + 1))
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
                    var random = new Random();
                    for (var num = Math.Min(catchPos.x, x); num <= Math.Max(catchPos.x, x); num = (ushort) (num + 1))
                    for (var num2 = Math.Min(catchPos.y, y);
                        num2 <= Math.Max(catchPos.y, y);
                        num2 = (ushort) (num2 + 1))
                    for (var num3 = Math.Min(catchPos.z, z);
                        num3 <= Math.Max(catchPos.z, z);
                        num3 = (ushort) (num3 + 1))
                        if (random.Next(0, 100) <= catchPos.randomFactor && p.level.GetTile(num, num2, num3) != type)
                            BufferAdd(list, num, num2, num3);
                    break;
                }
            }

            if (Server.forceCuboid)
            {
                var counter = 1;
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
                    Player.SendMessage(p,
                        string.Format("Tried to cuboid {0} blocks, but your limit is {1}.", list.Count,
                            p.group.maxBlocks));
                    Player.SendMessage(p, "Executed cuboid up to limit.");
                }
                else
                {
                    Player.SendMessage(p, string.Format("{0} blocks.", list.Count.ToString()));
                }

                if (p.staticCommands) p.Blockchange += Blockchange1;
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
                if (p.staticCommands) p.Blockchange += Blockchange1;
            }
        }

        // Token: 0x06001178 RID: 4472 RVA: 0x000600E8 File Offset: 0x0005E2E8
        private void BufferAdd(List<Pos> list, ushort x, ushort y, ushort z)
        {
            Pos item;
            item.x = x;
            item.y = y;
            item.z = z;
            list.Add(item);
        }

        // Token: 0x02000259 RID: 601
        private struct Pos
        {
            // Token: 0x040008DD RID: 2269
            public ushort x;

            // Token: 0x040008DE RID: 2270
            public ushort y;

            // Token: 0x040008DF RID: 2271
            public ushort z;
        }

        // Token: 0x0200025A RID: 602
        private struct CatchPos
        {
            // Token: 0x040008E0 RID: 2272
            public SolidType solid;

            // Token: 0x040008E1 RID: 2273
            public byte type;

            // Token: 0x040008E2 RID: 2274
            public ushort x;

            // Token: 0x040008E3 RID: 2275
            public ushort y;

            // Token: 0x040008E4 RID: 2276
            public ushort z;

            // Token: 0x040008E5 RID: 2277
            public int randomFactor;
        }

        // Token: 0x0200025B RID: 603
        private enum SolidType
        {
            // Token: 0x040008E7 RID: 2279
            solid,

            // Token: 0x040008E8 RID: 2280
            hollow,

            // Token: 0x040008E9 RID: 2281
            walls,

            // Token: 0x040008EA RID: 2282
            holes,

            // Token: 0x040008EB RID: 2283
            wire,

            // Token: 0x040008EC RID: 2284
            random
        }
    }
}