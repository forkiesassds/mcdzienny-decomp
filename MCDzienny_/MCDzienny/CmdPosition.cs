namespace MCDzienny
{
    public class CmdPosition : Command
    {
        public override string name { get { return "position"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            if (message != "")
            {
                Help(p);
                return;
            }
            if (LavaSystem.CheckSpawn(p))
            {
                Player.SendMessage(p, "You are in safe distance from the map spawn");
            }
            else
            {
                Player.SendMessage(p, "%cYou are too close to the map spawn! Go go go! Or you don't score.");
            }
            if (p.IsAboveSeaLevel)
            {
                Player.SendMessage(p, "You are above the sea level. It gives you higher score.");
            }
            else
            {
                Player.SendMessage(p, "You are below the sea level.");
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/position - Displays information about your position that are important for scoring points in lava survival.");
        }
    }
}