namespace MCDzienny
{
    // Token: 0x0200026E RID: 622
    public class CmdKick : Command
    {
        // Token: 0x17000687 RID: 1671
        // (get) Token: 0x060011DC RID: 4572 RVA: 0x00062D6C File Offset: 0x00060F6C
        public override string name
        {
            get { return "kick"; }
        }

        // Token: 0x17000688 RID: 1672
        // (get) Token: 0x060011DD RID: 4573 RVA: 0x00062D74 File Offset: 0x00060F74
        public override string shortcut
        {
            get { return "k"; }
        }

        // Token: 0x17000689 RID: 1673
        // (get) Token: 0x060011DE RID: 4574 RVA: 0x00062D7C File Offset: 0x00060F7C
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x1700068A RID: 1674
        // (get) Token: 0x060011DF RID: 4575 RVA: 0x00062D84 File Offset: 0x00060F84
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x1700068B RID: 1675
        // (get) Token: 0x060011E0 RID: 4576 RVA: 0x00062D88 File Offset: 0x00060F88
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.AdvBuilder; }
        }

        // Token: 0x060011E2 RID: 4578 RVA: 0x00062D94 File Offset: 0x00060F94
        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }

            var player = Player.Find(message.Split(' ')[0]);
            if (player == null)
            {
                Player.SendMessage(p, "Could not find player specified.");
                return;
            }

            if (message.Split(' ').Length > 1)
                message = message.Substring(message.IndexOf(' ') + 1);
            else if (p == null)
                message = "You were kicked by an IRC controller!";
            else
                message = string.Format("You were kicked by {0}!", p.PublicName);
            if (p != null)
            {
                if (player == p)
                {
                    Player.SendMessage(p, "You cannot kick yourself!");
                    return;
                }

                if (player.group.Permission >= p.group.Permission && p != null)
                {
                    Player.GlobalChat(p,
                        string.Format("{0} tried to kick {1} but failed.", p.color + p.PublicName + Server.DefaultColor,
                            player.color + player.PublicName), false);
                    return;
                }
            }

            if (player.disconnectionReason != DisconnectionReason.IPBan &&
                player.disconnectionReason != DisconnectionReason.NameBan)
                player.disconnectionReason = DisconnectionReason.Kicked;
            player.Kick(message);
        }

        // Token: 0x060011E3 RID: 4579 RVA: 0x00062EB0 File Offset: 0x000610B0
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/kick <player> [message] - Kicks a player.");
        }
    }
}