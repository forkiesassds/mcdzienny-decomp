using System;

namespace MCDzienny
{
    // Token: 0x020002FF RID: 767
    public class CmdMeasure : Command
    {
        // Token: 0x1700089B RID: 2203
        // (get) Token: 0x060015B1 RID: 5553 RVA: 0x00077770 File Offset: 0x00075970
        public override string name
        {
            get { return "measure"; }
        }

        // Token: 0x1700089C RID: 2204
        // (get) Token: 0x060015B2 RID: 5554 RVA: 0x00077778 File Offset: 0x00075978
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x1700089D RID: 2205
        // (get) Token: 0x060015B3 RID: 5555 RVA: 0x00077780 File Offset: 0x00075980
        public override string type
        {
            get { return "information"; }
        }

        // Token: 0x1700089E RID: 2206
        // (get) Token: 0x060015B4 RID: 5556 RVA: 0x00077788 File Offset: 0x00075988
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x1700089F RID: 2207
        // (get) Token: 0x060015B5 RID: 5557 RVA: 0x0007778C File Offset: 0x0007598C
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Guest; }
        }

        // Token: 0x170008A0 RID: 2208
        // (get) Token: 0x060015B6 RID: 5558 RVA: 0x00077790 File Offset: 0x00075990
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x060015B8 RID: 5560 RVA: 0x0007779C File Offset: 0x0007599C
        public override void Use(Player p, string message)
        {
            if (message.IndexOf(' ') != -1)
            {
                Help(p);
                return;
            }

            CatchPos catchPos;
            catchPos.toIgnore = Block.Byte(message);
            if (catchPos.toIgnore == 255 && message != "")
            {
                Player.SendMessage(p, "Could not find block specified");
                return;
            }

            catchPos.x = 0;
            catchPos.y = 0;
            catchPos.z = 0;
            p.blockchangeObject = catchPos;
            Player.SendMessage(p, "Place two blocks to determine the edges.");
            p.ClearBlockchange();
            p.Blockchange += Blockchange1;
        }

        // Token: 0x060015B9 RID: 5561 RVA: 0x00077838 File Offset: 0x00075A38
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/measure [ignore] - Measures all the blocks between two points");
            Player.SendMessage(p, "/measure [ignore] - Enter a block to ignore them");
        }

        // Token: 0x060015BA RID: 5562 RVA: 0x00077850 File Offset: 0x00075A50
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

        // Token: 0x060015BB RID: 5563 RVA: 0x000778C4 File Offset: 0x00075AC4
        public void Blockchange2(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            var tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            var catchPos = (CatchPos) p.blockchangeObject;
            var num = 0;
            for (var num2 = Math.Min(catchPos.x, x); num2 <= Math.Max(catchPos.x, x); num2 += 1)
            for (var num3 = Math.Min(catchPos.y, y); num3 <= Math.Max(catchPos.y, y); num3 += 1)
            for (var num4 = Math.Min(catchPos.z, z); num4 <= Math.Max(catchPos.z, z); num4 += 1)
                if (p.level.GetTile(num2, num3, num4) != catchPos.toIgnore)
                    num++;
            Player.SendMessage(p,
                string.Format("{0} blocks are between ({1}, {2}, {3}) and ({4}, {5}, {6})", num, catchPos.x, catchPos.y,
                    catchPos.z, x, y, z));
            if (p.staticCommands) p.Blockchange += Blockchange1;
        }

        // Token: 0x02000300 RID: 768
        private struct CatchPos
        {
            // Token: 0x040009C9 RID: 2505
            public ushort x;

            // Token: 0x040009CA RID: 2506
            public ushort y;

            // Token: 0x040009CB RID: 2507
            public ushort z;

            // Token: 0x040009CC RID: 2508
            public byte toIgnore;
        }
    }
}