namespace MCDzienny.Commands
{
    public class CmdPoke : Command
    {
        public override string name { get { return "poke"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }
            Player player = Player.Find(message);
            if (player == null || player.hidden)
            {
                Player.SendMessage(p, "Couldn't find the player.");
                return;
            }
            Player.SendMessage(player, "You were poked by {0}!", p.color + p.PublicName + Server.DefaultColor);
            Player.SendMessage(p, "You just poked {0}.", player.color + player.PublicName + Server.DefaultColor);
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/poke [player] - pokes the [player].");
        }
    }
}