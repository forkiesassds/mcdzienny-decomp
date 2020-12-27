namespace MCDzienny
{
    // Token: 0x020002A2 RID: 674
    public class CmdTnt : Command
    {
        // Token: 0x1700074F RID: 1871
        // (get) Token: 0x06001350 RID: 4944 RVA: 0x0006A388 File Offset: 0x00068588
        public override string name
        {
            get { return "tnt"; }
        }

        // Token: 0x17000750 RID: 1872
        // (get) Token: 0x06001351 RID: 4945 RVA: 0x0006A390 File Offset: 0x00068590
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000751 RID: 1873
        // (get) Token: 0x06001352 RID: 4946 RVA: 0x0006A398 File Offset: 0x00068598
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x17000752 RID: 1874
        // (get) Token: 0x06001353 RID: 4947 RVA: 0x0006A3A0 File Offset: 0x000685A0
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x17000753 RID: 1875
        // (get) Token: 0x06001354 RID: 4948 RVA: 0x0006A3A4 File Offset: 0x000685A4
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Builder; }
        }

        // Token: 0x17000754 RID: 1876
        // (get) Token: 0x06001355 RID: 4949 RVA: 0x0006A3A8 File Offset: 0x000685A8
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x06001357 RID: 4951 RVA: 0x0006A3B4 File Offset: 0x000685B4
        public override void Use(Player p, string message)
        {
            if (message.Split(' ').Length > 1)
            {
                Help(p);
                return;
            }

            if (p.BlockAction == 13 || p.BlockAction == 14)
            {
                p.BlockAction = 0;
                Player.SendMessage(p, string.Format("TNT mode is now &cOFF{0}.", Server.DefaultColor));
            }
            else if (message.ToLower() == "small" || message == "")
            {
                p.BlockAction = 13;
                Player.SendMessage(p, string.Format("TNT mode is now &aON{0}.", Server.DefaultColor));
            }
            else if (message.ToLower() == "big")
            {
                if (p.group.Permission > LevelPermission.AdvBuilder)
                {
                    p.BlockAction = 14;
                    Player.SendMessage(p, string.Format("TNT mode is now &aON{0}.", Server.DefaultColor));
                }
                else
                {
                    Player.SendMessage(p, "This mode is reserved for OPs");
                }
            }
            else
            {
                Help(p);
            }

            p.painting = false;
        }

        // Token: 0x06001358 RID: 4952 RVA: 0x0006A4B0 File Offset: 0x000686B0
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/tnt [small/big] - Creates exploding TNT (with Physics 3).");
            Player.SendMessage(p, "Big TNT is reserved for OP+.");
        }
    }
}