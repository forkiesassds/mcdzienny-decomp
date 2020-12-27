namespace MCDzienny
{
    // Token: 0x0200027A RID: 634
    public class CmdMute : Command
    {
        // Token: 0x170006B6 RID: 1718
        // (get) Token: 0x06001233 RID: 4659 RVA: 0x00064BC0 File Offset: 0x00062DC0
        public override string name
        {
            get { return "mute"; }
        }

        // Token: 0x170006B7 RID: 1719
        // (get) Token: 0x06001234 RID: 4660 RVA: 0x00064BC8 File Offset: 0x00062DC8
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170006B8 RID: 1720
        // (get) Token: 0x06001235 RID: 4661 RVA: 0x00064BD0 File Offset: 0x00062DD0
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x170006B9 RID: 1721
        // (get) Token: 0x06001236 RID: 4662 RVA: 0x00064BD8 File Offset: 0x00062DD8
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170006BA RID: 1722
        // (get) Token: 0x06001237 RID: 4663 RVA: 0x00064BDC File Offset: 0x00062DDC
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x06001238 RID: 4664 RVA: 0x00064BE0 File Offset: 0x00062DE0
        public override void Use(Player p, string message)
        {
            if (message == "" || message.Split(' ').Length > 2)
            {
                Help(p);
                return;
            }

            var player = Player.Find(message);
            if (player == null)
            {
                Player.SendMessage(p, "The player entered is not online.");
                return;
            }

            if (player.muted)
            {
                player.muted = false;
                Player.GlobalChat(null,
                    string.Format("{0} has been &bun-muted", player.color + player.PublicName + Server.DefaultColor),
                    false);
                return;
            }

            if (p != null && player != p && player.group.Permission > p.group.Permission)
            {
                Player.SendMessage(p, "Cannot mute someone of a higher rank.");
                return;
            }

            player.muted = true;
            Player.GlobalChat(null,
                string.Format("{0} has been &8muted", player.color + player.PublicName + Server.DefaultColor), false);
        }

        // Token: 0x06001239 RID: 4665 RVA: 0x00064CC0 File Offset: 0x00062EC0
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/mute <player> - Mutes or unmutes the player.");
        }
    }
}