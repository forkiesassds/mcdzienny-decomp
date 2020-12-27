namespace MCDzienny
{
    // Token: 0x020002DD RID: 733
    public class CmdMode : Command
    {
        // Token: 0x1700080C RID: 2060
        // (get) Token: 0x060014C0 RID: 5312 RVA: 0x000729E0 File Offset: 0x00070BE0
        public override string name
        {
            get { return "mode"; }
        }

        // Token: 0x1700080D RID: 2061
        // (get) Token: 0x060014C1 RID: 5313 RVA: 0x000729E8 File Offset: 0x00070BE8
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x1700080E RID: 2062
        // (get) Token: 0x060014C2 RID: 5314 RVA: 0x000729F0 File Offset: 0x00070BF0
        public override string type
        {
            get { return "build"; }
        }

        // Token: 0x1700080F RID: 2063
        // (get) Token: 0x060014C3 RID: 5315 RVA: 0x000729F8 File Offset: 0x00070BF8
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x17000810 RID: 2064
        // (get) Token: 0x060014C4 RID: 5316 RVA: 0x000729FC File Offset: 0x00070BFC
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Guest; }
        }

        // Token: 0x17000811 RID: 2065
        // (get) Token: 0x060014C5 RID: 5317 RVA: 0x00072A00 File Offset: 0x00070C00
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x060014C7 RID: 5319 RVA: 0x00072A0C File Offset: 0x00070C0C
        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                if (p.modeType != 0)
                {
                    Player.SendMessage(p,
                        string.Format("&b{0} mode: &cOFF",
                            Block.Name(p.modeType)[0].ToString().ToUpper() +
                            Block.Name(p.modeType).Remove(0, 1).ToLower() + Server.DefaultColor));
                    p.modeType = 0;
                    p.BlockAction = 0;
                    return;
                }

                Help(p);
            }
            else
            {
                var b = Block.Byte(message);
                if (b == 255)
                {
                    Player.SendMessage(p, "Could not find block given.");
                    return;
                }

                if (b == 0)
                {
                    Player.SendMessage(p, "Cannot use Air Mode.");
                    return;
                }

                if (!Block.canPlace(p, b))
                {
                    Player.SendMessage(p, "Cannot place this block at your rank.");
                    return;
                }

                if (p.modeType == b)
                {
                    Player.SendMessage(p,
                        string.Format("&b{0} mode: &cOFF",
                            Block.Name(p.modeType)[0].ToString().ToUpper() +
                            Block.Name(p.modeType).Remove(0, 1).ToLower() + Server.DefaultColor));
                    p.modeType = 0;
                    p.BlockAction = 0;
                    return;
                }

                p.BlockAction = 6;
                p.modeType = b;
                Player.SendMessage(p,
                    string.Format("&b{0} mode: &aON",
                        Block.Name(p.modeType)[0].ToString().ToUpper() + Block.Name(p.modeType).Remove(0, 1).ToLower() +
                        Server.DefaultColor));
            }
        }

        // Token: 0x060014C8 RID: 5320 RVA: 0x00072B98 File Offset: 0x00070D98
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/mode [block] - Makes every block placed into [block].");
            Player.SendMessage(p, "/[block] also works");
        }
    }
}