namespace MCDzienny
{
    public class CmdModerate : Command
    {
        public override string name { get { return "moderate"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override void Use(Player p, string message)
        {
            if (message != "")
            {
                Help(p);
            }
            else if (Server.chatmod)
            {
                Server.chatmod = false;
                Player.GlobalChat(null, string.Format("{0}Chat moderation has been disabled.  Everyone can now speak.", Server.DefaultColor), showname: false);
            }
            else
            {
                Server.chatmod = true;
                Player.GlobalChat(null, string.Format("{0}Chat moderation engaged!  Silence the plebians!", Server.DefaultColor), showname: false);
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/moderate - Toggles chat moderation status.  When enabled, only voiced");
            Player.SendMessage(p, "players may speak.");
        }
    }
}