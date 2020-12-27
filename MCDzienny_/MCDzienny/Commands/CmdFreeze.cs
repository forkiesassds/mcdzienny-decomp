namespace MCDzienny
{
    // Token: 0x020002D6 RID: 726
    public class CmdFreeze : Command
    {
        // Token: 0x170007EF RID: 2031
        // (get) Token: 0x0600148C RID: 5260 RVA: 0x00071964 File Offset: 0x0006FB64
        public override string name
        {
            get { return "freeze"; }
        }

        // Token: 0x170007F0 RID: 2032
        // (get) Token: 0x0600148D RID: 5261 RVA: 0x0007196C File Offset: 0x0006FB6C
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170007F1 RID: 2033
        // (get) Token: 0x0600148E RID: 5262 RVA: 0x00071974 File Offset: 0x0006FB74
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x170007F2 RID: 2034
        // (get) Token: 0x0600148F RID: 5263 RVA: 0x0007197C File Offset: 0x0006FB7C
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170007F3 RID: 2035
        // (get) Token: 0x06001490 RID: 5264 RVA: 0x00071980 File Offset: 0x0006FB80
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x06001492 RID: 5266 RVA: 0x0007198C File Offset: 0x0006FB8C
        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }

            var player = Player.Find(message);
            if (player == null)
            {
                Player.SendMessage(p, "Could not find player.");
                return;
            }

            if (player == p)
            {
                Player.SendMessage(p, "Cannot freeze yourself.");
                return;
            }

            if (p != null && player.group.Permission >= p.group.Permission)
            {
                Player.SendMessage(p, "Cannot freeze someone of equal or greater rank.");
                return;
            }

            if (!player.frozen)
            {
                player.frozen = true;
                Player.GlobalChat(null,
                    string.Format("{0} has been &bfrozen.", player.color + player.PublicName + Server.DefaultColor),
                    false);
                return;
            }

            player.frozen = false;
            Player.GlobalChat(null,
                string.Format("{0} has been &adefrosted.", player.color + player.PublicName + Server.DefaultColor),
                false);
        }

        // Token: 0x06001493 RID: 5267 RVA: 0x00071A60 File Offset: 0x0006FC60
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/freeze <name> - Stops <name> from moving until unfrozen.");
        }
    }
}