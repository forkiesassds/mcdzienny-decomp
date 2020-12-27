using System.Collections.Generic;
using System.Linq;

namespace MCDzienny.Commands
{
    // Token: 0x0200007E RID: 126
    public class CmdTriangle : Command
    {
        // Token: 0x170000F0 RID: 240
        // (get) Token: 0x0600034A RID: 842 RVA: 0x00012454 File Offset: 0x00010654
        public override string name
        {
            get { return "triangle"; }
        }

        // Token: 0x170000F1 RID: 241
        // (get) Token: 0x0600034B RID: 843 RVA: 0x0001245C File Offset: 0x0001065C
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170000F2 RID: 242
        // (get) Token: 0x0600034C RID: 844 RVA: 0x00012464 File Offset: 0x00010664
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x170000F3 RID: 243
        // (get) Token: 0x0600034D RID: 845 RVA: 0x0001246C File Offset: 0x0001066C
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170000F4 RID: 244
        // (get) Token: 0x0600034E RID: 846 RVA: 0x00012470 File Offset: 0x00010670
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Banned; }
        }

        // Token: 0x170000F5 RID: 245
        // (get) Token: 0x0600034F RID: 847 RVA: 0x00012474 File Offset: 0x00010674
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x06000350 RID: 848 RVA: 0x00012478 File Offset: 0x00010678
        public override void Use(Player p, string message)
        {
            var b = byte.MaxValue;
            if (message != "")
            {
                b = Block.Byte(message.ToLower().Trim());
                if (b == 255)
                {
                    Player.SendMessage(p, "Unknown type of block.");
                    return;
                }

                if (!Block.canPlace(p, b))
                {
                    Player.SendMessage(p, "Cannot place that.");
                    return;
                }
            }

            Player.SendMessage(p, "Place 3 blocks to determine vertices of a triangle.");
            BlockCatch.CaptureMultipleBlocks(p, 3, DrawTriangle, new BasicDrawArgs(b));
        }

        // Token: 0x06000351 RID: 849 RVA: 0x000124F8 File Offset: 0x000106F8
        private void DrawTriangle(Player p, List<ChangeInfo> changes, BasicDrawArgs da)
        {
            var list = (from c in changes
                select new BlockPoints(new Vector3(c.X, c.Z, c.Y), c.Type)).ToList();
            if (da.Type1 != 255) list[2].blockType = da.Type1;
            if (!Block.canPlace(p, list[2].blockType))
            {
                Player.SendMessage(p, "You are not allowed to place this block type.");
                return;
            }

            new Core().DrawTriangle(p, list);
            if (p.staticCommands)
            {
                Player.SendMessage(p, "Place 3 blocks to determine vertices of the triangle.");
                BlockCatch.CaptureMultipleBlocks(p, 3, DrawTriangle, da);
            }
        }

        // Token: 0x06000352 RID: 850 RVA: 0x000125A0 File Offset: 0x000107A0
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/triangle - place three blocks to determine vertices of triangle.");
            Player.SendMessage(p, "/triangle [block] - draws triangle that consists of [block].");
        }
    }
}