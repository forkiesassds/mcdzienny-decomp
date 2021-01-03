using System;

namespace MCDzienny
{
    // Token: 0x020002FD RID: 765
    public class CmdWrite : Command
    {
        // Token: 0x17000895 RID: 2197
        // (get) Token: 0x060015A6 RID: 5542 RVA: 0x0007744C File Offset: 0x0007564C
        public override string name
        {
            get { return "write"; }
        }

        // Token: 0x17000896 RID: 2198
        // (get) Token: 0x060015A7 RID: 5543 RVA: 0x00077454 File Offset: 0x00075654
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000897 RID: 2199
        // (get) Token: 0x060015A8 RID: 5544 RVA: 0x0007745C File Offset: 0x0007565C
        public override string type
        {
            get { return "build"; }
        }

        // Token: 0x17000898 RID: 2200
        // (get) Token: 0x060015A9 RID: 5545 RVA: 0x00077464 File Offset: 0x00075664
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x17000899 RID: 2201
        // (get) Token: 0x060015AA RID: 5546 RVA: 0x00077468 File Offset: 0x00075668
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x1700089A RID: 2202
        // (get) Token: 0x060015AB RID: 5547 RVA: 0x0007746C File Offset: 0x0007566C
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x060015AC RID: 5548 RVA: 0x00077470 File Offset: 0x00075670
        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }

            CatchPos catchPos;
            catchPos.givenMessage = message.ToUpper();
            catchPos.x = 0;
            catchPos.y = 0;
            catchPos.z = 0;
            p.blockchangeObject = catchPos;
            Player.SendMessage(p, "Place two blocks to determine direction.");
            p.ClearBlockchange();
            p.Blockchange += Blockchange1;
        }

        // Token: 0x060015AD RID: 5549 RVA: 0x000774E8 File Offset: 0x000756E8
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/write [message] - Writes [message] in blocks");
        }

        // Token: 0x060015AE RID: 5550 RVA: 0x000774F8 File Offset: 0x000756F8
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

        // Token: 0x060015AF RID: 5551 RVA: 0x0007756C File Offset: 0x0007576C
        public void Blockchange2(Player p, ushort x, ushort y, ushort z, byte type)
        {
            type = p.bindings[type];
            p.ClearBlockchange();
            if (!Block.canPlace(p, type))
            {
                Player.SendMessage(p, "You are not allowed to place this block type.");
                return;
            }

            var tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            var catchPos = (CatchPos) p.blockchangeObject;
            if (x == catchPos.x && z == catchPos.z)
            {
                Player.SendMessage(p, "No direction was selected");
                return;
            }

            if (Math.Abs(catchPos.x - x) > Math.Abs(catchPos.z - z))
            {
                var num = catchPos.x;
                if (x > catchPos.x)
                    foreach (var c in catchPos.givenMessage)
                        num = FindReference.writeLetter(p, c, num, catchPos.y, catchPos.z, type, 0);
                else
                    foreach (var c2 in catchPos.givenMessage)
                        num = FindReference.writeLetter(p, c2, num, catchPos.y, catchPos.z, type, 1);
            }
            else
            {
                var num = catchPos.z;
                if (z > catchPos.z)
                    foreach (var c3 in catchPos.givenMessage)
                        num = FindReference.writeLetter(p, c3, catchPos.x, catchPos.y, num, type, 2);
                else
                    foreach (var c4 in catchPos.givenMessage)
                        num = FindReference.writeLetter(p, c4, catchPos.x, catchPos.y, num, type, 3);
            }

            if (p.staticCommands) p.Blockchange += Blockchange1;
        }

        // Token: 0x020002FE RID: 766
        private struct CatchPos
        {
            // Token: 0x040009C5 RID: 2501
            public ushort x;

            // Token: 0x040009C6 RID: 2502
            public ushort y;

            // Token: 0x040009C7 RID: 2503
            public ushort z;

            // Token: 0x040009C8 RID: 2504
            public string givenMessage;
        }
    }
}