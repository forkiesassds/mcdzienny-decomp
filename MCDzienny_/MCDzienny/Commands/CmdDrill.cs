using System.Collections.Generic;

namespace MCDzienny
{
    // Token: 0x02000116 RID: 278
    public class CmdDrill : Command
    {
        // Token: 0x170003D5 RID: 981
        // (get) Token: 0x06000864 RID: 2148 RVA: 0x0002A8FC File Offset: 0x00028AFC
        public override string name
        {
            get { return "drill"; }
        }

        // Token: 0x170003D6 RID: 982
        // (get) Token: 0x06000865 RID: 2149 RVA: 0x0002A904 File Offset: 0x00028B04
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170003D7 RID: 983
        // (get) Token: 0x06000866 RID: 2150 RVA: 0x0002A90C File Offset: 0x00028B0C
        public override string type
        {
            get { return "build"; }
        }

        // Token: 0x170003D8 RID: 984
        // (get) Token: 0x06000867 RID: 2151 RVA: 0x0002A914 File Offset: 0x00028B14
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x170003D9 RID: 985
        // (get) Token: 0x06000868 RID: 2152 RVA: 0x0002A918 File Offset: 0x00028B18
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x170003DA RID: 986
        // (get) Token: 0x06000869 RID: 2153 RVA: 0x0002A91C File Offset: 0x00028B1C
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x0600086B RID: 2155 RVA: 0x0002A928 File Offset: 0x00028B28
        public override void Use(Player p, string message)
        {
            CatchPos catchPos;
            catchPos.distance = 20;
            if (message != "")
                try
                {
                    catchPos.distance = int.Parse(message);
                }
                catch
                {
                    Help(p);
                    return;
                }

            catchPos.x = 0;
            catchPos.y = 0;
            catchPos.z = 0;
            p.blockchangeObject = catchPos;
            Player.SendMessage(p, "Destroy the block you wish to drill.");
            p.ClearBlockchange();
            p.Blockchange += Blockchange1;
        }

        // Token: 0x0600086C RID: 2156 RVA: 0x0002A9BC File Offset: 0x00028BBC
        public override void Help(Player p)
        {
            Player.SendMessage(p,
                "/drill [distance] - Drills a hole, destroying all similar blocks in a 3x3 rectangle ahead of you.");
        }

        // Token: 0x0600086D RID: 2157 RVA: 0x0002A9CC File Offset: 0x00028BCC
        public void Blockchange1(Player p, ushort x, ushort y, ushort z, byte type)
        {
            if (!p.staticCommands) p.ClearBlockchange();
            var catchPos = (CatchPos) p.blockchangeObject;
            var tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            var num = 0;
            var num2 = 0;
            if (p.rot[0] <= 32 || p.rot[0] >= 224)
                num2 = -1;
            else if (p.rot[0] <= 96)
                num = 1;
            else if (p.rot[0] <= 160)
                num2 = 1;
            else
                num = -1;
            var list = new List<Pos>();
            var num3 = 0;
            var item = default(Pos);
            if (num != 0)
            {
                var num4 = x;
                while (num3 < catchPos.distance)
                {
                    for (var num5 = (ushort) (y - 1); num5 <= (ushort) (y + 1); num5 = (ushort) (num5 + 1))
                    for (var num6 = (ushort) (z - 1); num6 <= (ushort) (z + 1); num6 = (ushort) (num6 + 1))
                    {
                        item.x = num4;
                        item.y = num5;
                        item.z = num6;
                        list.Add(item);
                    }

                    num3++;
                    num4 = (ushort) (num4 + (ushort) num);
                }
            }
            else
            {
                var num7 = z;
                while (num3 < catchPos.distance)
                {
                    for (var num8 = (ushort) (y - 1); num8 <= (ushort) (y + 1); num8 = (ushort) (num8 + 1))
                    for (var num9 = (ushort) (x - 1); num9 <= (ushort) (x + 1); num9 = (ushort) (num9 + 1))
                    {
                        item.x = num9;
                        item.y = num8;
                        item.z = num7;
                        list.Add(item);
                    }

                    num3++;
                    num7 = (ushort) (num7 + (ushort) num2);
                }
            }

            if (list.Count > p.group.maxBlocks)
            {
                Player.SendMessage(p, string.Format("You tried to drill {0} blocks.", list.Count));
                Player.SendMessage(p, string.Format("You cannot drill more than {0}.", p.group.maxBlocks));
                return;
            }

            foreach (var item2 in list)
                if (p.level.GetTile(item2.x, item2.y, item2.z) == tile)
                    p.level.Blockchange(p, item2.x, item2.y, item2.z, 0);
            Player.SendMessage(p, string.Format("{0} blocks.", list.Count));
        }

        // Token: 0x02000117 RID: 279
        private struct CatchPos
        {
            // Token: 0x040003D2 RID: 978
            public ushort x;

            // Token: 0x040003D3 RID: 979
            public ushort y;

            // Token: 0x040003D4 RID: 980
            public ushort z;

            // Token: 0x040003D5 RID: 981
            public int distance;
        }

        // Token: 0x02000118 RID: 280
        private struct Pos
        {
            // Token: 0x040003D6 RID: 982
            public ushort x;

            // Token: 0x040003D7 RID: 983
            public ushort y;

            // Token: 0x040003D8 RID: 984
            public ushort z;
        }
    }
}