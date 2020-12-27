using System;

namespace MCDzienny
{
    // Token: 0x020002C8 RID: 712
    public class CmdStairs : Command
    {
        // Token: 0x170007C7 RID: 1991
        // (get) Token: 0x0600143A RID: 5178 RVA: 0x000701D8 File Offset: 0x0006E3D8
        public override string name
        {
            get { return "stairs"; }
        }

        // Token: 0x170007C8 RID: 1992
        // (get) Token: 0x0600143B RID: 5179 RVA: 0x000701E0 File Offset: 0x0006E3E0
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170007C9 RID: 1993
        // (get) Token: 0x0600143C RID: 5180 RVA: 0x000701E8 File Offset: 0x0006E3E8
        public override string type
        {
            get { return "build"; }
        }

        // Token: 0x170007CA RID: 1994
        // (get) Token: 0x0600143D RID: 5181 RVA: 0x000701F0 File Offset: 0x0006E3F0
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x170007CB RID: 1995
        // (get) Token: 0x0600143E RID: 5182 RVA: 0x000701F4 File Offset: 0x0006E3F4
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.AdvBuilder; }
        }

        // Token: 0x170007CC RID: 1996
        // (get) Token: 0x0600143F RID: 5183 RVA: 0x000701F8 File Offset: 0x0006E3F8
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x170007CD RID: 1997
        // (get) Token: 0x06001446 RID: 5190 RVA: 0x000704F4 File Offset: 0x0006E6F4
        // (set) Token: 0x06001447 RID: 5191 RVA: 0x000704FC File Offset: 0x0006E6FC
        public ushort z { get; set; }

        // Token: 0x06001441 RID: 5185 RVA: 0x00070204 File Offset: 0x0006E404
        public override void Use(Player p, string message)
        {
            if (p.level.mapType == MapType.Lava && p.group.Permission < LevelPermission.Admin)
                p.SendMessage("Only admin is allowed to use this command on lava map");
            CatchPos catchPos;
            catchPos.x = 0;
            catchPos.y = 0;
            catchPos.z = 0;
            p.blockchangeObject = catchPos;
            Player.SendMessage(p, "Place two blocks to determine the height.");
            p.ClearBlockchange();
            p.Blockchange += Blockchange1;
        }

        // Token: 0x06001442 RID: 5186 RVA: 0x00070280 File Offset: 0x0006E480
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/stairs - Creates a spiral staircase the height you want.");
        }

        // Token: 0x06001443 RID: 5187 RVA: 0x00070290 File Offset: 0x0006E490
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

        // Token: 0x06001444 RID: 5188 RVA: 0x00070304 File Offset: 0x0006E504
        public void Swap(ref int a, ref int b)
        {
            var num = a;
            a = b;
            b = num;
        }

        // Token: 0x06001445 RID: 5189 RVA: 0x0007031C File Offset: 0x0006E51C
        public void Blockchange2(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            var tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            var catchPos = (CatchPos) p.blockchangeObject;
            if (catchPos.y == y)
            {
                Player.SendMessage(p, "Cannot create a stairway 0 blocks high.");
                return;
            }

            var num = catchPos.x;
            var num2 = catchPos.z;
            int num3;
            if (catchPos.x > x && catchPos.z > z)
                num3 = 0;
            else if (catchPos.x > x && catchPos.z < z)
                num3 = 1;
            else if (catchPos.x < x && catchPos.z > z)
                num3 = 2;
            else
                num3 = 3;
            for (var num4 = Math.Min(catchPos.y, y); num4 <= Math.Max(catchPos.y, y); num4 += 1)
                if (num3 == 0)
                {
                    num += 1;
                    p.level.Blockchange(p, num, num4, num2, 44);
                    num += 1;
                    p.level.Blockchange(p, num, num4, num2, 43);
                    num3 = 1;
                }
                else if (num3 == 1)
                {
                    num2 += 1;
                    p.level.Blockchange(p, num, num4, num2, 44);
                    num2 += 1;
                    p.level.Blockchange(p, num, num4, num2, 43);
                    num3 = 2;
                }
                else if (num3 == 2)
                {
                    num -= 1;
                    p.level.Blockchange(p, num, num4, num2, 44);
                    num -= 1;
                    p.level.Blockchange(p, num, num4, num2, 43);
                    num3 = 3;
                }
                else
                {
                    num2 -= 1;
                    p.level.Blockchange(p, num, num4, num2, 44);
                    num2 -= 1;
                    p.level.Blockchange(p, num, num4, num2, 43);
                    num3 = 0;
                }

            if (p.staticCommands) p.Blockchange += Blockchange1;
        }

        // Token: 0x020002C9 RID: 713
        private struct CatchPos
        {
            // Token: 0x0400099C RID: 2460
            public ushort x;

            // Token: 0x0400099D RID: 2461
            public ushort y;

            // Token: 0x0400099E RID: 2462
            public ushort z;
        }
    }
}