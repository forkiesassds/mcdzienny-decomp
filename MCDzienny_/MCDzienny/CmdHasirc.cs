namespace MCDzienny
{
    public class CmdHasirc : Command
    {
        public override string name { get { return "hasirc"; } }

        public override string shortcut { get { return "irc"; } }

        public override string type { get { return "information"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }

        public override void Use(Player p, string message)
        {
            if (message != "")
            {
                Help(p);
                return;
            }
            string text = "";
            string arg;
            if (Server.irc)
            {
                arg = string.Format("&aEnabled{0}.", Server.DefaultColor);
                text = Server.ircServer + " > " + Server.ircChannel;
            }
            else
            {
                arg = string.Format("&cDisabled{0}.", Server.DefaultColor);
            }
            Player.SendMessage(p, string.Format("IRC is {0}", arg));
            if (text != "")
            {
                Player.SendMessage(p, string.Format("Location: {0}", text));
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/hasirc - Denotes whether or not the server has IRC active.");
            Player.SendMessage(p, "If IRC is active, server and channel are also displayed.");
        }
    }
}