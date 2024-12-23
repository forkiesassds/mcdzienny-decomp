namespace MCDzienny
{
    public class CmdKick : Command
    {
        public override string name { get { return "kick"; } }

        public override string shortcut { get { return "k"; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }
            Player player = Player.Find(message.Split(' ')[0]);
            if (player == null)
            {
                Player.SendMessage(p, "Could not find player specified.");
                return;
            }
            message = message.Split(' ').Length > 1 ? message.Substring(message.IndexOf(' ') + 1) :
                p != null ? string.Format("You were kicked by {0}!", p.PublicName) : "You were kicked by an IRC controller!";
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
                                      string.Format("{0} tried to kick {1} but failed.", p.color + p.PublicName + Server.DefaultColor, player.color + player.PublicName), showname: false);
                    return;
                }
            }
            if (player.disconnectionReason != 0 && player.disconnectionReason != DisconnectionReason.NameBan)
            {
                player.disconnectionReason = DisconnectionReason.Kicked;
            }
            player.Kick(message);
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/kick <player> [message] - Kicks a player.");
        }
    }
}