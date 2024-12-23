namespace MCDzienny
{
    public class CmdLavaPortal : Command
    {
        public override string name { get { return "lavaportal"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "build"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
            }
            else if (message.ToLower() == "show")
            {
                all.Find("portal").Use(p, message);
            }
            else
            {
                all.Find("portal").Use(p, message + " lavamap");
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/lavaportal [orange/blue/air/water/lava] [multi] - Activates Portal mode.");
            Player.SendMessage(p, "/lavaportal [type] multi - Place Entry blocks until exit is wanted.");
            Player.SendMessage(p, "/lavaportal show - Shows portals, green = in, red = out.");
        }
    }
}