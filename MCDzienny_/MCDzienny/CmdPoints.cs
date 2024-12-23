namespace MCDzienny
{
    public class CmdPoints : Command
    {
        public override string name { get { return "money"; } }

        public override string shortcut { get { return "points"; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            Player.SendMessage(p, string.Format("You have: {0} {1}.", p.money, Server.moneys));
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/money - Displays the amount of " + Server.moneys + " you have.");
        }
    }
}