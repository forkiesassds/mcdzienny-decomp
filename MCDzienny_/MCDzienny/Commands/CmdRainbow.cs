using System;
using System.Collections.Generic;

namespace MCDzienny
{
    // Token: 0x020002F5 RID: 757
    public class CmdRainbow : Command
    {
        // Token: 0x17000879 RID: 2169
        // (get) Token: 0x06001572 RID: 5490 RVA: 0x0007614C File Offset: 0x0007434C
        public override string name
        {
            get { return "rainbow"; }
        }

        // Token: 0x1700087A RID: 2170
        // (get) Token: 0x06001573 RID: 5491 RVA: 0x00076154 File Offset: 0x00074354
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x1700087B RID: 2171
        // (get) Token: 0x06001574 RID: 5492 RVA: 0x0007615C File Offset: 0x0007435C
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x1700087C RID: 2172
        // (get) Token: 0x06001575 RID: 5493 RVA: 0x00076164 File Offset: 0x00074364
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x1700087D RID: 2173
        // (get) Token: 0x06001576 RID: 5494 RVA: 0x00076168 File Offset: 0x00074368
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.AdvBuilder; }
        }

        // Token: 0x1700087E RID: 2174
        // (get) Token: 0x06001577 RID: 5495 RVA: 0x0007616C File Offset: 0x0007436C
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x06001579 RID: 5497 RVA: 0x00076178 File Offset: 0x00074378
        public override void Use(Player p, string message)
        {
            CatchPos catchPos;
            catchPos.x = 0;
            catchPos.y = 0;
            catchPos.z = 0;
            p.blockchangeObject = catchPos;
            Player.SendMessage(p, "Place two blocks to determine the edges.");
            p.ClearBlockchange();
            p.Blockchange += Blockchange1;
        }

        // Token: 0x0600157A RID: 5498 RVA: 0x000761CC File Offset: 0x000743CC
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/rainbow - Taste the rainbow");
        }

        // Token: 0x0600157B RID: 5499 RVA: 0x000761DC File Offset: 0x000743DC
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

        // Token: 0x0600157C RID: 5500 RVA: 0x00076250 File Offset: 0x00074450
        public void Blockchange2(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            var tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            var catchPos = (CatchPos) p.blockchangeObject;
            var list = new List<Pos>();
            byte b = 33;
            var num = Math.Abs(catchPos.x - x);
            var num2 = Math.Abs(catchPos.y - y);
            var num3 = Math.Abs(catchPos.z - z);
            if (num >= num2 && num >= num3)
                for (var num4 = Math.Min(catchPos.x, x); num4 <= Math.Max(catchPos.x, x); num4 += 1)
                {
                    b += 1;
                    if (b > 33) b = 21;
                    for (var num5 = Math.Min(catchPos.y, y); num5 <= Math.Max(catchPos.y, y); num5 += 1)
                    for (var num6 = Math.Min(catchPos.z, z); num6 <= Math.Max(catchPos.z, z); num6 += 1)
                        if (p.level.GetTile(num4, num5, num6) != 0)
                            BufferAdd(list, num4, num5, num6, b);
                }
            else if (num2 > num && num2 > num3)
                for (var num7 = Math.Min(catchPos.y, y); num7 <= Math.Max(catchPos.y, y); num7 += 1)
                {
                    b += 1;
                    if (b > 33) b = 21;
                    for (var num8 = Math.Min(catchPos.x, x); num8 <= Math.Max(catchPos.x, x); num8 += 1)
                    for (var num9 = Math.Min(catchPos.z, z); num9 <= Math.Max(catchPos.z, z); num9 += 1)
                        if (p.level.GetTile(num8, num7, num9) != 0)
                            BufferAdd(list, num8, num7, num9, b);
                }
            else if (num3 > num2 && num3 > num)
                for (var num10 = Math.Min(catchPos.z, z); num10 <= Math.Max(catchPos.z, z); num10 += 1)
                {
                    b += 1;
                    if (b > 33) b = 21;
                    for (var num11 = Math.Min(catchPos.y, y); num11 <= Math.Max(catchPos.y, y); num11 += 1)
                    for (var num12 = Math.Min(catchPos.x, x); num12 <= Math.Max(catchPos.x, x); num12 += 1)
                        if (p.level.GetTile(num12, num11, num10) != 0)
                            BufferAdd(list, num12, num11, num10, b);
                }

            if (list.Count > p.group.maxBlocks)
            {
                Player.SendMessage(p, string.Format("You tried to replace {0} blocks.", list.Count));
                Player.SendMessage(p, string.Format("You cannot replace more than {0}.", p.group.maxBlocks));
                return;
            }

            Player.SendMessage(p, string.Format("{0} blocks.", list.Count.ToString()));
            list.ForEach(delegate(Pos pos) { p.level.Blockchange(p, pos.x, pos.y, pos.z, pos.newType); });
            if (p.staticCommands) p.Blockchange += Blockchange1;
        }

        // Token: 0x0600157D RID: 5501 RVA: 0x00076618 File Offset: 0x00074818
        private void BufferAdd(List<Pos> list, ushort x, ushort y, ushort z, byte newType)
        {
            Pos item;
            item.x = x;
            item.y = y;
            item.z = z;
            item.newType = newType;
            list.Add(item);
        }

        // Token: 0x020002F6 RID: 758
        private struct Pos
        {
            // Token: 0x040009BD RID: 2493
            public ushort x;

            // Token: 0x040009BE RID: 2494
            public ushort y;

            // Token: 0x040009BF RID: 2495
            public ushort z;

            // Token: 0x040009C0 RID: 2496
            public byte newType;
        }

        // Token: 0x020002F7 RID: 759
        private struct CatchPos
        {
            // Token: 0x040009C1 RID: 2497
            public ushort x;

            // Token: 0x040009C2 RID: 2498
            public ushort y;

            // Token: 0x040009C3 RID: 2499
            public ushort z;
        }
    }
}