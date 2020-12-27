using System;
using System.Collections.Generic;

namespace MCDzienny
{
    // Token: 0x02000072 RID: 114
    public class CmdSimpleCuboidTest : Command
    {
        // Token: 0x170000CE RID: 206
        // (get) Token: 0x060002F4 RID: 756 RVA: 0x0001062C File Offset: 0x0000E82C
        public override string name
        {
            get { return "cube"; }
        }

        // Token: 0x170000CF RID: 207
        // (get) Token: 0x060002F5 RID: 757 RVA: 0x00010634 File Offset: 0x0000E834
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170000D0 RID: 208
        // (get) Token: 0x060002F6 RID: 758 RVA: 0x0001063C File Offset: 0x0000E83C
        public override string type
        {
            get { return "build"; }
        }

        // Token: 0x170000D1 RID: 209
        // (get) Token: 0x060002F7 RID: 759 RVA: 0x00010644 File Offset: 0x0000E844
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x170000D2 RID: 210
        // (get) Token: 0x060002F8 RID: 760 RVA: 0x00010648 File Offset: 0x0000E848
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Builder; }
        }

        // Token: 0x170000D3 RID: 211
        // (get) Token: 0x060002F9 RID: 761 RVA: 0x0001064C File Offset: 0x0000E84C
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x060002FA RID: 762 RVA: 0x00010650 File Offset: 0x0000E850
        public override void Use(Player p, string message)
        {
            var b = byte.MaxValue;
            if (message != "")
            {
                b = Block.Parse(message);
                if (b == 255)
                {
                    Player.SendMessage(p, "Unknown block type.");
                    return;
                }

                if (!Block.canPlace(p, b))
                {
                    Player.SendMessage(p, "You can't place this block type.");
                    return;
                }
            }

            Player.SendMessage(p, "Place two blocks to determine the edges.");
            BlockCatch.CaptureMultipleBlocks(p, 2, DrawCuboid, new BasicDrawArgs(b));
        }

        // Token: 0x060002FB RID: 763 RVA: 0x000106C4 File Offset: 0x0000E8C4
        private void DrawCuboid(Player p, List<ChangeInfo> changes, BasicDrawArgs da)
        {
            var type = changes[1].Type;
            if (da.Type1 != 255) type = da.Type1;
            int num = Math.Min(changes[0].X, changes[1].X);
            int num2 = Math.Max(changes[0].X, changes[1].X);
            int num3 = Math.Min(changes[0].Y, changes[1].Y);
            int num4 = Math.Max(changes[0].Y, changes[1].Y);
            int num5 = Math.Min(changes[0].Z, changes[1].Z);
            int num6 = Math.Max(changes[0].Z, changes[1].Z);
            for (var i = num; i <= num2; i++)
            for (var j = num3; j <= num4; j++)
            for (var k = num5; k <= num6; k++)
                p.BlockChanges.Add(i, j, k, type);
            if (p.group.maxBlocks < p.BlockChanges.Count)
            {
                Player.SendMessage(p,
                    string.Format("You can't place {0} blocks. Your limit is {1}.", p.BlockChanges.Count,
                        p.group.maxBlocks));
                p.BlockChanges.Abort();
                return;
            }

            Player.SendMessage(p,
                string.Format("You've built a simple cuboid that consists of {0} blocks.", p.BlockChanges.Count));
            p.BlockChanges.Commit();
            if (p.staticCommands) BlockCatch.CaptureMultipleBlocks(p, 2, DrawCuboid, da);
        }

        // Token: 0x060002FC RID: 764 RVA: 0x0001088C File Offset: 0x0000EA8C
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/cube <block> - draws a cube.");
            Player.SendMessage(p, "block - a block type e.g. water, it's optional.");
        }
    }
}