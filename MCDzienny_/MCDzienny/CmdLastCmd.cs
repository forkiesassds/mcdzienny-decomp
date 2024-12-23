namespace MCDzienny
{
    public class CmdLastCmd : Command
    {
        public override string name { get { return "lastcmd"; } }

        public override string shortcut { get { return "last"; } }

        public override string type { get { return "information"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                foreach (Player player2 in Player.players)
                {
                    Player.SendMessage(p, string.Format("{0} last used \"{1}\"", player2.color + player2.name + Server.DefaultColor, player2.lastCMD));
                }
                return;
            }
            Player player = Player.Find(message);
            if (player == null)
            {
                Player.SendMessage(p, "Could not find player entered");
            }
            else
            {
                Player.SendMessage(p, string.Format("{0} last used \"{1}\"", player.color + player.name + Server.DefaultColor, player.lastCMD));
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/last [user] - Shows last command used by [user]");
            Player.SendMessage(p, "/last by itself will show all last commands (SPAMMY)");
        }
    }
}