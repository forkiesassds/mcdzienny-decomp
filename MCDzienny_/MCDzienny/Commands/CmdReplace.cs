using System;
using System.Collections.Generic;

namespace MCDzienny
{
    // Token: 0x0200028A RID: 650
    public class CmdReplace : Command
    {
        // Token: 0x170006F1 RID: 1777
        // (get) Token: 0x060012AB RID: 4779 RVA: 0x00067304 File Offset: 0x00065504
        public override string name
        {
            get { return "replace"; }
        }

        // Token: 0x170006F2 RID: 1778
        // (get) Token: 0x060012AC RID: 4780 RVA: 0x0006730C File Offset: 0x0006550C
        public override string shortcut
        {
            get { return "r"; }
        }

        // Token: 0x170006F3 RID: 1779
        // (get) Token: 0x060012AD RID: 4781 RVA: 0x00067314 File Offset: 0x00065514
        public override string type
        {
            get { return "build"; }
        }

        // Token: 0x170006F4 RID: 1780
        // (get) Token: 0x060012AE RID: 4782 RVA: 0x0006731C File Offset: 0x0006551C
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x170006F5 RID: 1781
        // (get) Token: 0x060012AF RID: 4783 RVA: 0x00067320 File Offset: 0x00065520
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.AdvBuilder; }
        }

        // Token: 0x170006F6 RID: 1782
        // (get) Token: 0x060012B0 RID: 4784 RVA: 0x00067324 File Offset: 0x00065524
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x060012B1 RID: 4785 RVA: 0x00067328 File Offset: 0x00065528
        public override void Use(Player p, string message)
        {
            if (p.level.mapType == MapType.Lava && p.group.Permission < LevelPermission.Admin)
                p.SendMessage("Only admin is allowed to use this command on lava map");
            var num = message.Split(' ').Length;
            if (num != 2)
            {
                Help(p);
                return;
            }

            var num2 = message.IndexOf(' ');
            var text = message.Substring(0, num2).ToLower();
            var text2 = message.Substring(num2 + 1).ToLower();
            var b = Block.Byte(text);
            if (b == 255)
            {
                Player.SendMessage(p, string.Format("There is no block \"{0}\".", text));
                return;
            }

            var b2 = Block.Byte(text2);
            if (b2 == 255)
            {
                Player.SendMessage(p, string.Format("There is no block \"{0}\".", text2));
                return;
            }

            if (!Block.canPlace(p, b) && !Block.BuildIn(b))
            {
                Player.SendMessage(p, "Cannot replace that.");
                return;
            }

            if (!Block.canPlace(p, b2))
            {
                Player.SendMessage(p, "Cannot place that.");
                return;
            }

            CatchPos catchPos;
            catchPos.type2 = b2;
            catchPos.type = b;
            catchPos.x = 0;
            catchPos.y = 0;
            catchPos.z = 0;
            p.blockchangeObject = catchPos;
            Player.SendMessage(p, "Place two blocks to determine the edges.");
            p.ClearBlockchange();
            p.Blockchange += Blockchange1;
        }

        // Token: 0x060012B2 RID: 4786 RVA: 0x00067480 File Offset: 0x00065680
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/replace [type] [type2] - replace type with type2 inside a selected cuboid");
        }

        // Token: 0x060012B3 RID: 4787 RVA: 0x00067490 File Offset: 0x00065690
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

        // Token: 0x060012B4 RID: 4788 RVA: 0x00067504 File Offset: 0x00065704
        public void Blockchange2(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            var tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            var cpos = (CatchPos) p.blockchangeObject;
            if (cpos.type != 255) type = cpos.type;
            var list = new List<Pos>();
            for (var num = Math.Min(cpos.x, x); num <= Math.Max(cpos.x, x); num += 1)
            for (var num2 = Math.Min(cpos.y, y); num2 <= Math.Max(cpos.y, y); num2 += 1)
            for (var num3 = Math.Min(cpos.z, z); num3 <= Math.Max(cpos.z, z); num3 += 1)
                if (p.level.GetTile(num, num2, num3) == type)
                    BufferAdd(list, num, num2, num3);
            if (list.Count > p.group.maxBlocks)
            {
                Player.SendMessage(p, string.Format("You tried to replace {0} blocks.", list.Count));
                Player.SendMessage(p, string.Format("You cannot replace more than {0}.", p.group.maxBlocks));
                return;
            }

            Player.SendMessage(p, string.Format("{0} blocks.", list.Count.ToString()));
            list.ForEach(delegate(Pos pos) { p.level.Blockchange(p, pos.x, pos.y, pos.z, cpos.type2); });
            if (p.staticCommands) p.Blockchange += Blockchange1;
        }

        // Token: 0x060012B5 RID: 4789 RVA: 0x00067714 File Offset: 0x00065914
        private void BufferAdd(List<Pos> list, ushort x, ushort y, ushort z)
        {
            Pos item;
            item.x = x;
            item.y = y;
            item.z = z;
            list.Add(item);
        }

        // Token: 0x0200028B RID: 651
        private struct Pos
        {
            // Token: 0x04000934 RID: 2356
            public ushort x;

            // Token: 0x04000935 RID: 2357
            public ushort y;

            // Token: 0x04000936 RID: 2358
            public ushort z;
        }

        // Token: 0x0200028C RID: 652
        private struct CatchPos
        {
            // Token: 0x04000937 RID: 2359
            public byte type;

            // Token: 0x04000938 RID: 2360
            public byte type2;

            // Token: 0x04000939 RID: 2361
            public ushort x;

            // Token: 0x0400093A RID: 2362
            public ushort y;

            // Token: 0x0400093B RID: 2363
            public ushort z;
        }
    }
}