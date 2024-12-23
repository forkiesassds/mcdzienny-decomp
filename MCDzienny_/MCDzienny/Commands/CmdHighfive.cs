namespace MCDzienny.Commands
{
    public class CmdHighfive : Command
    {
        public override string name { get { return "highfive"; } }

        public override string shortcut { get { return "high5"; } }

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
            Player.GlobalMessage("{0} high fived {1}!", p.color + p.PublicName + Server.DefaultColor, player.color + player.PublicName + Server.DefaultColor);
            Player.SendMessage(player, "You were high fived by {0}", p.color + p.PublicName);
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/highfive [player] - high fives the [player].");
            Player.SendMessage(p, "Shortcut: /high5");
        }
    }
}