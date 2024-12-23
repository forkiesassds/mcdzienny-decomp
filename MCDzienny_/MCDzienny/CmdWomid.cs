using MCDzienny.Settings;

namespace MCDzienny
{
    class CmdWomid : Command
    {
        public override string name { get { return "womid"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return ""; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            if (message.Length >= 5)
            {
                if (GeneralSettings.All.KickWomUsers && !message.ToLower().Contains("xwom") && message.ToLower().Contains("wom"))
                {
                    p.Kick("Upgrade your WOM client to XWOM!");
                }
                if (message.Substring(message.Length - 5) == "2.0.5" || message.Substring(message.Length - 5) == "2.0.6")
                {
                    p.Kick("Upgrade your WOM client to the newest version!");
                }
                p.IsUsingWom = true;
            }
        }

        public override void Help(Player p) {}
    }
}