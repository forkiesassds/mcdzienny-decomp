using System;
using System.Collections.Generic;

namespace MCDzienny
{
    // Token: 0x02000256 RID: 598
    public class CmdCopy : Command
    {
        // Token: 0x040008D6 RID: 2262
        public int allowoffset;

        // Token: 0x1700064A RID: 1610
        // (get) Token: 0x06001161 RID: 4449 RVA: 0x0005EB7C File Offset: 0x0005CD7C
        public override string name
        {
            get { return "copy"; }
        }

        // Token: 0x1700064B RID: 1611
        // (get) Token: 0x06001162 RID: 4450 RVA: 0x0005EB84 File Offset: 0x0005CD84
        public override string shortcut
        {
            get { return "c"; }
        }

        // Token: 0x1700064C RID: 1612
        // (get) Token: 0x06001163 RID: 4451 RVA: 0x0005EB8C File Offset: 0x0005CD8C
        public override string type
        {
            get { return "build"; }
        }

        // Token: 0x1700064D RID: 1613
        // (get) Token: 0x06001164 RID: 4452 RVA: 0x0005EB94 File Offset: 0x0005CD94
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x1700064E RID: 1614
        // (get) Token: 0x06001165 RID: 4453 RVA: 0x0005EB98 File Offset: 0x0005CD98
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.AdvBuilder; }
        }

        // Token: 0x1700064F RID: 1615
        // (get) Token: 0x06001166 RID: 4454 RVA: 0x0005EB9C File Offset: 0x0005CD9C
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x06001168 RID: 4456 RVA: 0x0005EBA8 File Offset: 0x0005CDA8
        public override void Use(Player p, string message)
        {
            CatchPos catchPos;
            catchPos.ignoreTypes = new List<byte>();
            catchPos.type = 0;
            p.copyoffset[0] = 0;
            p.copyoffset[1] = 0;
            p.copyoffset[2] = 0;
            allowoffset = message.IndexOf('@');
            if (allowoffset != -1) message = message.Replace("@ ", "");
            if (message.ToLower() == "cut")
            {
                catchPos.type = 1;
                message = "";
            }
            else if (message.ToLower() == "air")
            {
                catchPos.type = 2;
                message = "";
            }
            else if (message == "@")
            {
                message = "";
            }
            else if (message.IndexOf(' ') != -1)
            {
                if (!(message.Split(' ')[0] == "ignore"))
                {
                    Help(p);
                    return;
                }

                foreach (var text in message.Substring(message.IndexOf(' ') + 1).Split(' '))
                    if (Block.Byte(text) != 255)
                    {
                        catchPos.ignoreTypes.Add(Block.Byte(text));
                        Player.SendMessage(p, string.Format("Ignoring &b{0}", text));
                    }

                message = "";
            }

            catchPos.x = 0;
            catchPos.y = 0;
            catchPos.z = 0;
            p.blockchangeObject = catchPos;
            if (message != "")
            {
                Help(p);
                return;
            }

            Player.SendMessage(p, "Place two blocks to determine the edges.");
            p.ClearBlockchange();
            p.Blockchange += Blockchange1;
        }

        // Token: 0x06001169 RID: 4457 RVA: 0x0005ED7C File Offset: 0x0005CF7C
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/copy - Copies the blocks in an area.");
            Player.SendMessage(p, "/copy cut - Copies the blocks in an area, then removes them.");
            Player.SendMessage(p, "/copy air - Copies the blocks in an area, including air.");
            Player.SendMessage(p, "/copy ignore <block1> <block2>.. - Ignores <blocks> when copying");
            Player.SendMessage(p,
                "/copy @ - @ toggle for all the above, gives you a third click after copying that determines where to paste from");
        }

        // Token: 0x0600116A RID: 4458 RVA: 0x0005EDB8 File Offset: 0x0005CFB8
        public void Blockchange1(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            var tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            var catchPos = (CatchPos) p.blockchangeObject;
            p.copystart[0] = x;
            p.copystart[1] = y;
            p.copystart[2] = z;
            catchPos.x = x;
            catchPos.y = y;
            catchPos.z = z;
            p.blockchangeObject = catchPos;
            p.Blockchange += Blockchange2;
        }

        // Token: 0x0600116B RID: 4459 RVA: 0x0005EE48 File Offset: 0x0005D048
        public void Blockchange2(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            var tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            var catchPos = (CatchPos) p.blockchangeObject;
            p.CopyBuffer.Clear();
            var num = 0;
            if (catchPos.type == 2)
                p.copyAir = true;
            else
                p.copyAir = false;
            for (var num2 = Math.Min(catchPos.x, x); num2 <= Math.Max(catchPos.x, x); num2 = (ushort) (num2 + 1))
            for (var num3 = Math.Min(catchPos.y, y); num3 <= Math.Max(catchPos.y, y); num3 = (ushort) (num3 + 1))
            for (var num4 = Math.Min(catchPos.z, z); num4 <= Math.Max(catchPos.z, z); num4 = (ushort) (num4 + 1))
            {
                tile = p.level.GetTile(num2, num3, num4);
                if (Block.canPlace(p, tile))
                {
                    if (tile == 0 && catchPos.type != 2 || catchPos.ignoreTypes.Contains(tile)) num++;
                    if (catchPos.ignoreTypes.Contains(tile))
                        BufferAdd(p, (ushort) (num2 - catchPos.x), (ushort) (num3 - catchPos.y),
                            (ushort) (num4 - catchPos.z), 0);
                    else
                        BufferAdd(p, (ushort) (num2 - catchPos.x), (ushort) (num3 - catchPos.y),
                            (ushort) (num4 - catchPos.z), tile);
                }
                else
                {
                    BufferAdd(p, (ushort) (num2 - catchPos.x), (ushort) (num3 - catchPos.y),
                        (ushort) (num4 - catchPos.z), 0);
                }
            }

            if (p.CopyBuffer.Count - num > p.group.maxBlocks)
            {
                Player.SendMessage(p, string.Format("You tried to copy {0} blocks.", p.CopyBuffer.Count));
                Player.SendMessage(p, string.Format("You cannot copy more than {0}.", p.group.maxBlocks));
                p.CopyBuffer.Clear();
                return;
            }

            if (catchPos.type == 1)
                for (var num5 = Math.Min(catchPos.x, x); num5 <= Math.Max(catchPos.x, x); num5 += 1)
                for (var num6 = Math.Min(catchPos.y, y); num6 <= Math.Max(catchPos.y, y); num6 += 1)
                for (var num7 = Math.Min(catchPos.z, z); num7 <= Math.Max(catchPos.z, z); num7 += 1)
                {
                    tile = p.level.GetTile(num5, num6, num7);
                    if (tile != 0 && Block.canPlace(p, tile)) p.level.Blockchange(p, num5, num6, num7, 0);
                }

            Player.SendMessage(p, string.Format("{0} blocks copied.", p.CopyBuffer.Count - num));
            if (allowoffset != -1)
            {
                Player.SendMessage(p, "Place a block to determine where to paste from");
                p.Blockchange += Blockchange3;
            }
        }

        // Token: 0x0600116C RID: 4460 RVA: 0x0005F16C File Offset: 0x0005D36C
        public void Blockchange3(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            var tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            var catchPos = (CatchPos) p.blockchangeObject;
            p.copyoffset[0] = p.copystart[0] - x;
            p.copyoffset[1] = p.copystart[1] - y;
            p.copyoffset[2] = p.copystart[2] - z;
        }

        // Token: 0x0600116D RID: 4461 RVA: 0x0005F1E0 File Offset: 0x0005D3E0
        private void BufferAdd(Player p, ushort x, ushort y, ushort z, byte type)
        {
            Player.CopyPos item;
            item.x = x;
            item.y = y;
            item.z = z;
            item.type = type;
            p.CopyBuffer.Add(item);
        }

        // Token: 0x02000257 RID: 599
        private struct CatchPos
        {
            // Token: 0x040008D7 RID: 2263
            public ushort x;

            // Token: 0x040008D8 RID: 2264
            public ushort y;

            // Token: 0x040008D9 RID: 2265
            public ushort z;

            // Token: 0x040008DA RID: 2266
            public int type;

            // Token: 0x040008DB RID: 2267
            public List<byte> ignoreTypes;
        }
    }
}