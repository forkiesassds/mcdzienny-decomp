using System.Collections.Generic;
using System.Linq;

namespace MCDzienny.Commands
{
    // Token: 0x0200007B RID: 123
    public class CmdQuad : Command
    {
        // Token: 0x170000E4 RID: 228
        // (get) Token: 0x0600032A RID: 810 RVA: 0x00011C60 File Offset: 0x0000FE60
        public override string name
        {
            get { return "quad"; }
        }

        // Token: 0x170000E5 RID: 229
        // (get) Token: 0x0600032B RID: 811 RVA: 0x00011C68 File Offset: 0x0000FE68
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170000E6 RID: 230
        // (get) Token: 0x0600032C RID: 812 RVA: 0x00011C70 File Offset: 0x0000FE70
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x170000E7 RID: 231
        // (get) Token: 0x0600032D RID: 813 RVA: 0x00011C78 File Offset: 0x0000FE78
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170000E8 RID: 232
        // (get) Token: 0x0600032E RID: 814 RVA: 0x00011C7C File Offset: 0x0000FE7C
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Banned; }
        }

        // Token: 0x170000E9 RID: 233
        // (get) Token: 0x0600032F RID: 815 RVA: 0x00011C80 File Offset: 0x0000FE80
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x06000330 RID: 816 RVA: 0x00011C84 File Offset: 0x0000FE84
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

            Player.SendMessage(p, "Place 4 blocks to determine vertices of the quad.");
            BlockCatch.CaptureMultipleBlocks(p, 4, DrawQuad, new BasicDrawArgs(b));
        }

        // Token: 0x06000331 RID: 817 RVA: 0x00011D08 File Offset: 0x0000FF08
        private void DrawQuad(Player p, List<ChangeInfo> changes, BasicDrawArgs da)
        {
            var list = (from c in changes
                select new BlockPoints(new Vector3(c.X, c.Z, c.Y), c.Type)).ToList();
            if (da.Type1 != 255) list[3].blockType = da.Type1;
            if (!Block.canPlace(p, list[3].blockType))
            {
                Player.SendMessage(p, "Cannot place this type of block.");
                return;
            }

            new Core().DrawQuad(p, list);
            if (p.staticCommands)
            {
                Player.SendMessage(p, "Place 4 blocks to determine vertices of the quad.");
                BlockCatch.CaptureMultipleBlocks(p, 4, DrawQuad, da);
            }
        }

        // Token: 0x06000332 RID: 818 RVA: 0x00011DB0 File Offset: 0x0000FFB0
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/quad - draws a quad.");
        }
    }
}