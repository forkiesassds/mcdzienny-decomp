namespace MCDzienny
{
    public class CmdLives : Command
    {
        public override string name { get { return "lives"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            Player.SendMessage(p, string.Format("You have: {0} lives.", p.lives));
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/lives - Displays how many lives you have.");
        }
    }
}