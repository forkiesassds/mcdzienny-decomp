using System;
using System.Collections.Generic;

namespace MCDzienny
{
    // Token: 0x020000C2 RID: 194
    public class CmdHammer : Command
    {
        // Token: 0x170002E1 RID: 737
        // (get) Token: 0x0600068C RID: 1676 RVA: 0x00021CA4 File Offset: 0x0001FEA4
        public override string name
        {
            get { return "hammer"; }
        }

        // Token: 0x170002E2 RID: 738
        // (get) Token: 0x0600068D RID: 1677 RVA: 0x00021CAC File Offset: 0x0001FEAC
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170002E3 RID: 739
        // (get) Token: 0x0600068E RID: 1678 RVA: 0x00021CB4 File Offset: 0x0001FEB4
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x170002E4 RID: 740
        // (get) Token: 0x0600068F RID: 1679 RVA: 0x00021CBC File Offset: 0x0001FEBC
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170002E5 RID: 741
        // (get) Token: 0x06000690 RID: 1680 RVA: 0x00021CC0 File Offset: 0x0001FEC0
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Banned; }
        }

        // Token: 0x170002E6 RID: 742
        // (get) Token: 0x06000691 RID: 1681 RVA: 0x00021CC4 File Offset: 0x0001FEC4
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x170002E7 RID: 743
        // (get) Token: 0x06000692 RID: 1682 RVA: 0x00021CC8 File Offset: 0x0001FEC8
        public override CommandScope Scope
        {
            get { return CommandScope.Lava; }
        }

        // Token: 0x06000694 RID: 1684 RVA: 0x00021CD4 File Offset: 0x0001FED4
        public override void Use(Player p, string message)
        {
            if (p.hammer <= 0)
            {
                Player.SendMessage(p, "You run out of hammer. Check the /store.");
                return;
            }

            CatchPos catchPos;
            catchPos.type = byte.MaxValue;
            catchPos.x = 0;
            catchPos.y = 0;
            catchPos.z = 0;
            p.blockchangeObject = catchPos;
            Player.SendMessage(p, "Place two blocks to determine the edges.");
            p.ClearBlockchange();
            p.Blockchange += Blockchange1;
        }

        // Token: 0x06000695 RID: 1685 RVA: 0x00021D4C File Offset: 0x0001FF4C
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

        // Token: 0x06000696 RID: 1686 RVA: 0x00021DC0 File Offset: 0x0001FFC0
        public void Blockchange2(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            var tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            var catchPos = (CatchPos) p.blockchangeObject;
            if (catchPos.type != 255)
                type = catchPos.type;
            else
                type = p.bindings[type];
            if (!Block.canPlace(p, type))
            {
                Player.SendMessage(p, "Cannot place that.");
                return;
            }

            var list = new List<Pos>();
            list.Capacity = Math.Abs(catchPos.x - x) * Math.Abs(catchPos.y - y) * Math.Abs(catchPos.z - z);
            for (var num = Math.Min(catchPos.x, x); num <= Math.Max(catchPos.x, x); num += 1)
            for (var num2 = Math.Min(catchPos.y, y); num2 <= Math.Max(catchPos.y, y); num2 += 1)
            for (var num3 = Math.Min(catchPos.z, z); num3 <= Math.Max(catchPos.z, z); num3 += 1)
                if (p.level.GetTile(num, num2, num3) != type)
                    BufferAdd(list, num, num2, num3);
            if (list.Count > p.hammer)
            {
                Player.SendMessage(p, string.Format("You tried to hammer {0} blocks.", list.Count));
                Player.SendMessage(p, string.Format("But your hammer may endure no more than {0}.", p.hammer));
                return;
            }

            p.hammer -= list.Count;
            Player.SendMessage(p, list.Count + " blocks.");
            list.ForEach(delegate(Pos pos) { p.level.Blockchange(p, pos.x, pos.y, pos.z, type); });
        }

        // Token: 0x06000697 RID: 1687 RVA: 0x0002201C File Offset: 0x0002021C
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/hammer - Allows you to build faster.");
        }

        // Token: 0x06000698 RID: 1688 RVA: 0x0002202C File Offset: 0x0002022C
        private void BufferAdd(List<Pos> list, ushort x, ushort y, ushort z)
        {
            Pos item;
            item.x = x;
            item.y = y;
            item.z = z;
            list.Add(item);
        }

        // Token: 0x020000C3 RID: 195
        private struct Pos
        {
            // Token: 0x04000359 RID: 857
            public ushort x;

            // Token: 0x0400035A RID: 858
            public ushort y;

            // Token: 0x0400035B RID: 859
            public ushort z;
        }

        // Token: 0x020000C4 RID: 196
        private struct CatchPos
        {
            // Token: 0x0400035C RID: 860
            public byte type;

            // Token: 0x0400035D RID: 861
            public ushort x;

            // Token: 0x0400035E RID: 862
            public ushort y;

            // Token: 0x0400035F RID: 863
            public ushort z;
        }
    }
}