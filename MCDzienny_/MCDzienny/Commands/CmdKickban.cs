namespace MCDzienny
{
    // Token: 0x0200026F RID: 623
    public class CmdKickban : Command
    {
        // Token: 0x1700068C RID: 1676
        // (get) Token: 0x060011E4 RID: 4580 RVA: 0x00062EC0 File Offset: 0x000610C0
        public override string name
        {
            get { return "kickban"; }
        }

        // Token: 0x1700068D RID: 1677
        // (get) Token: 0x060011E5 RID: 4581 RVA: 0x00062EC8 File Offset: 0x000610C8
        public override string shortcut
        {
            get { return "kb"; }
        }

        // Token: 0x1700068E RID: 1678
        // (get) Token: 0x060011E6 RID: 4582 RVA: 0x00062ED0 File Offset: 0x000610D0
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x1700068F RID: 1679
        // (get) Token: 0x060011E7 RID: 4583 RVA: 0x00062ED8 File Offset: 0x000610D8
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000690 RID: 1680
        // (get) Token: 0x060011E8 RID: 4584 RVA: 0x00062EDC File Offset: 0x000610DC
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x060011EA RID: 4586 RVA: 0x00062EE8 File Offset: 0x000610E8
        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }

            var text = message.Split(' ')[0];
            var player = Player.Find(text);
            all.Find("ban").Use(p, text);
            if (player != null)
            {
                player.disconnectionReason = DisconnectionReason.NameBan;
                all.Find("kick").Use(p, message);
            }
        }

        // Token: 0x060011EB RID: 4587 RVA: 0x00062F60 File Offset: 0x00061160
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/kickban <player> [message] - Kicks and bans a player with an optional message.");
        }
    }
}