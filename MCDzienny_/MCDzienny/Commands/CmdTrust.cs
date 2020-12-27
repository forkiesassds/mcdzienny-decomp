namespace MCDzienny
{
    // Token: 0x02000301 RID: 769
    public class CmdTrust : Command
    {
        // Token: 0x170008A1 RID: 2209
        // (get) Token: 0x060015BC RID: 5564 RVA: 0x00077A28 File Offset: 0x00075C28
        public override string name
        {
            get { return "trust"; }
        }

        // Token: 0x170008A2 RID: 2210
        // (get) Token: 0x060015BD RID: 5565 RVA: 0x00077A30 File Offset: 0x00075C30
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170008A3 RID: 2211
        // (get) Token: 0x060015BE RID: 5566 RVA: 0x00077A38 File Offset: 0x00075C38
        public override string type
        {
            get { return "moderation"; }
        }

        // Token: 0x170008A4 RID: 2212
        // (get) Token: 0x060015BF RID: 5567 RVA: 0x00077A40 File Offset: 0x00075C40
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170008A5 RID: 2213
        // (get) Token: 0x060015C0 RID: 5568 RVA: 0x00077A44 File Offset: 0x00075C44
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x060015C2 RID: 5570 RVA: 0x00077A50 File Offset: 0x00075C50
        public override void Use(Player p, string message)
        {
            if (message == "" || message.IndexOf(' ') != -1)
            {
                Help(p);
                return;
            }

            var player = Player.Find(message);
            if (player == null)
            {
                Player.SendMessage(p, "Could not find player specified");
                return;
            }

            player.ignoreGrief = !player.ignoreGrief;
            Player.SendMessage(p,
                string.Format("{0}'s trust status: {1}", player.color + player.PublicName + Server.DefaultColor,
                    player.ignoreGrief));
            player.SendMessage(string.Format("Your trust status was changed to: {0}", player.ignoreGrief));
        }

        // Token: 0x060015C3 RID: 5571 RVA: 0x00077AF0 File Offset: 0x00075CF0
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/trust <name> - Turns off the anti-grief for <name>");
        }
    }
}