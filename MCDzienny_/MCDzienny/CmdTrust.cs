namespace MCDzienny
{
    public class CmdTrust : Command
    {
        public override string name { get { return "trust"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "moderation"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override void Use(Player p, string message)
        {
            if (message == "" || message.IndexOf(' ') != -1)
            {
                Help(p);
                return;
            }
            Player player = Player.Find(message);
            if (player == null)
            {
                Player.SendMessage(p, "Could not find player specified");
                return;
            }
            player.ignoreGrief = !player.ignoreGrief;
            Player.SendMessage(p, string.Format("{0}'s trust status: {1}", player.color + player.PublicName + Server.DefaultColor, player.ignoreGrief));
            player.SendMessage(string.Format("Your trust status was changed to: {0}", player.ignoreGrief));
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/trust <name> - Turns off the anti-grief for <name>");
        }
    }
}