namespace MCDzienny
{
    public class CmdMute : Command
    {
        public override string name { get { return "mute"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override void Use(Player p, string message)
        {
            if (message == "" || message.Split(' ').Length > 2)
            {
                Help(p);
                return;
            }
            Player player = Player.Find(message);
            if (player == null)
            {
                Player.SendMessage(p, "The player entered is not online.");
            }
            else if (player.muted)
            {
                player.muted = false;
                Player.GlobalChat(null, string.Format("{0} has been &bun-muted", player.color + player.PublicName + Server.DefaultColor), showname: false);
            }
            else if (p != null && player != p && player.group.Permission > p.group.Permission)
            {
                Player.SendMessage(p, "Cannot mute someone of a higher rank.");
            }
            else
            {
                player.muted = true;
                Player.GlobalChat(null, string.Format("{0} has been &8muted", player.color + player.PublicName + Server.DefaultColor), showname: false);
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/mute <player> - Mutes or unmutes the player.");
        }
    }
}