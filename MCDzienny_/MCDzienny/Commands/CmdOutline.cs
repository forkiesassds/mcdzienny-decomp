using System;
using System.Collections.Generic;

namespace MCDzienny
{
    // Token: 0x020002CB RID: 715
    public class CmdOutline : Command
    {
        // Token: 0x170007D3 RID: 2003
        // (get) Token: 0x06001450 RID: 5200 RVA: 0x00070724 File Offset: 0x0006E924
        public override string name
        {
            get { return "outline"; }
        }

        // Token: 0x170007D4 RID: 2004
        // (get) Token: 0x06001451 RID: 5201 RVA: 0x0007072C File Offset: 0x0006E92C
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170007D5 RID: 2005
        // (get) Token: 0x06001452 RID: 5202 RVA: 0x00070734 File Offset: 0x0006E934
        public override string type
        {
            get { return "build"; }
        }

        // Token: 0x170007D6 RID: 2006
        // (get) Token: 0x06001453 RID: 5203 RVA: 0x0007073C File Offset: 0x0006E93C
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x170007D7 RID: 2007
        // (get) Token: 0x06001454 RID: 5204 RVA: 0x00070740 File Offset: 0x0006E940
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.AdvBuilder; }
        }

        // Token: 0x170007D8 RID: 2008
        // (get) Token: 0x06001455 RID: 5205 RVA: 0x00070744 File Offset: 0x0006E944
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x06001457 RID: 5207 RVA: 0x00070750 File Offset: 0x0006E950
        public override void Use(Player p, string message)
        {
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

            if (!Block.canPlace(p, b2))
            {
                Player.SendMessage(p, "Cannot place that block type.");
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

        // Token: 0x06001458 RID: 5208 RVA: 0x00070860 File Offset: 0x0006EA60
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/outline [type] [type2] - Outlines [type] with [type2]");
        }

        // Token: 0x06001459 RID: 5209 RVA: 0x00070870 File Offset: 0x0006EA70
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

        // Token: 0x0600145A RID: 5210 RVA: 0x000708E4 File Offset: 0x0006EAE4
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
            {
                var flag = false;
                if (p.level.GetTile(num - 1, num2, num3) == cpos.type)
                    flag = true;
                else if (p.level.GetTile(num + 1, num2, num3) == cpos.type)
                    flag = true;
                else if (p.level.GetTile(num, num2 - 1, num3) == cpos.type)
                    flag = true;
                else if (p.level.GetTile(num, num2 + 1, num3) == cpos.type)
                    flag = true;
                else if (p.level.GetTile(num, num2, num3 - 1) == cpos.type)
                    flag = true;
                else if (p.level.GetTile(num, num2, num3 + 1) == cpos.type) flag = true;
                if (flag && p.level.GetTile(num, num2, num3) != cpos.type)
                {
                    Pos item;
                    item.x = num;
                    item.y = num2;
                    item.z = num3;
                    list.Add(item);
                }
            }

            if (list.Count > p.group.maxBlocks)
            {
                Player.SendMessage(p, string.Format("You tried to outline more than {0} blocks.", list.Count));
                Player.SendMessage(p, string.Format("You cannot outline more than {0}", p.group.maxBlocks + ".)"));
                return;
            }

            list.ForEach(delegate(Pos pos1) { p.level.Blockchange(p, pos1.x, pos1.y, pos1.z, cpos.type2); });
            Player.SendMessage(p, string.Format("You outlined {0} blocks.", list.Count));
            if (p.staticCommands) p.Blockchange += Blockchange1;
        }

        // Token: 0x0600145B RID: 5211 RVA: 0x00070C48 File Offset: 0x0006EE48
        private void BufferAdd(List<Pos> list, ushort x, ushort y, ushort z)
        {
            Pos item;
            item.x = x;
            item.y = y;
            item.z = z;
            list.Add(item);
        }

        // Token: 0x020002CC RID: 716
        private struct Pos
        {
            // Token: 0x0400099F RID: 2463
            public ushort x;

            // Token: 0x040009A0 RID: 2464
            public ushort y;

            // Token: 0x040009A1 RID: 2465
            public ushort z;
        }

        // Token: 0x020002CD RID: 717
        private struct CatchPos
        {
            // Token: 0x040009A2 RID: 2466
            public byte type;

            // Token: 0x040009A3 RID: 2467
            public byte type2;

            // Token: 0x040009A4 RID: 2468
            public ushort x;

            // Token: 0x040009A5 RID: 2469
            public ushort y;

            // Token: 0x040009A6 RID: 2470
            public ushort z;
        }
    }
}