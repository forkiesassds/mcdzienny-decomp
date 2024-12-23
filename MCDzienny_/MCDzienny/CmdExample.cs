using MCDzienny.Cpe;

namespace MCDzienny
{
    public class CmdExample : Command
    {
        public override string name { get { return "example"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            if (p.Cpe.EnvWeatherType == 1)
            {
                V1.EnvSetWeatherType(p, 1);
            }
            else
            {
                Player.SendMessage(p, "Sorry, this command isn't compatible with your game client.");
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "example - changes the weather.");
        }
    }
}