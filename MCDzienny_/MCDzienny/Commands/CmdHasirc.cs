namespace MCDzienny
{
    // Token: 0x02000265 RID: 613
    public class CmdHasirc : Command
    {
        // Token: 0x17000667 RID: 1639
        // (get) Token: 0x060011A1 RID: 4513 RVA: 0x00061120 File Offset: 0x0005F320
        public override string name
        {
            get { return "hasirc"; }
        }

        // Token: 0x17000668 RID: 1640
        // (get) Token: 0x060011A2 RID: 4514 RVA: 0x00061128 File Offset: 0x0005F328
        public override string shortcut
        {
            get { return "irc"; }
        }

        // Token: 0x17000669 RID: 1641
        // (get) Token: 0x060011A3 RID: 4515 RVA: 0x00061130 File Offset: 0x0005F330
        public override string type
        {
            get { return "information"; }
        }

        // Token: 0x1700066A RID: 1642
        // (get) Token: 0x060011A4 RID: 4516 RVA: 0x00061138 File Offset: 0x0005F338
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x1700066B RID: 1643
        // (get) Token: 0x060011A5 RID: 4517 RVA: 0x0006113C File Offset: 0x0005F33C
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Banned; }
        }

        // Token: 0x060011A7 RID: 4519 RVA: 0x00061148 File Offset: 0x0005F348
        public override void Use(Player p, string message)
        {
            if (message != "")
            {
                Help(p);
                return;
            }

            var text = "";
            string arg;
            if (Server.irc)
            {
                arg = string.Format("&aEnabled{0}.", Server.DefaultColor);
                text = Server.ircServer + " > " + Server.ircChannel;
            }
            else
            {
                arg = string.Format("&cDisabled{0}.", Server.DefaultColor);
            }

            Player.SendMessage(p, string.Format("IRC is {0}", arg));
            if (text != "") Player.SendMessage(p, string.Format("Location: {0}", text));
        }

        // Token: 0x060011A8 RID: 4520 RVA: 0x000611E0 File Offset: 0x0005F3E0
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/hasirc - Denotes whether or not the server has IRC active.");
            Player.SendMessage(p, "If IRC is active, server and channel are also displayed.");
        }
    }
}