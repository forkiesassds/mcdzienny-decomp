namespace MCDzienny
{
    // Token: 0x020002BE RID: 702
    public class CmdLowlag : Command
    {
        // Token: 0x170007A0 RID: 1952
        // (get) Token: 0x060013F8 RID: 5112 RVA: 0x0006E760 File Offset: 0x0006C960
        public override string name
        {
            get { return "lowlag"; }
        }

        // Token: 0x170007A1 RID: 1953
        // (get) Token: 0x060013F9 RID: 5113 RVA: 0x0006E768 File Offset: 0x0006C968
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170007A2 RID: 1954
        // (get) Token: 0x060013FA RID: 5114 RVA: 0x0006E770 File Offset: 0x0006C970
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x170007A3 RID: 1955
        // (get) Token: 0x060013FB RID: 5115 RVA: 0x0006E778 File Offset: 0x0006C978
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170007A4 RID: 1956
        // (get) Token: 0x060013FC RID: 5116 RVA: 0x0006E77C File Offset: 0x0006C97C
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x060013FE RID: 5118 RVA: 0x0006E788 File Offset: 0x0006C988
        public override void Use(Player p, string message)
        {
            if (message != "")
            {
                Help(p);
                return;
            }

            if (Server.updateTimer.Interval > 1000.0)
            {
                Server.updateTimer.Interval = 100.0;
                Player.GlobalChat(null,
                    string.Format("&dLow lag {0}mode was turned &cOFF{1}.", Server.DefaultColor, Server.DefaultColor),
                    false);
                return;
            }

            Server.updateTimer.Interval = 10000.0;
            Player.GlobalChat(null,
                string.Format("&dLow lag {0}mode was turned &aON{1}.", Server.DefaultColor, Server.DefaultColor),
                false);
        }

        // Token: 0x060013FF RID: 5119 RVA: 0x0006E81C File Offset: 0x0006CA1C
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/lowlag - Turns lowlag mode on or off");
        }
    }
}