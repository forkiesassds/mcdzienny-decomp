namespace MCDzienny
{
    public class CmdFreeze : Command
    {
        public override string name { get { return "freeze"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }
            Player player = Player.Find(message);
            if (player == null)
            {
                Player.SendMessage(p, "Could not find player.");
            }
            else if (player == p)
            {
                Player.SendMessage(p, "Cannot freeze yourself.");
            }
            else if (p != null && player.group.Permission >= p.group.Permission)
            {
                Player.SendMessage(p, "Cannot freeze someone of equal or greater rank.");
            }
            else if (!player.frozen)
            {
                player.frozen = true;
                Player.GlobalChat(null, string.Format("{0} has been &bfrozen.", player.color + player.PublicName + Server.DefaultColor), showname: false);
            }
            else
            {
                player.frozen = false;
                Player.GlobalChat(null, string.Format("{0} has been &adefrosted.", player.color + player.PublicName + Server.DefaultColor), showname: false);
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/freeze <name> - Stops <name> from moving until unfrozen.");
        }
    }
}