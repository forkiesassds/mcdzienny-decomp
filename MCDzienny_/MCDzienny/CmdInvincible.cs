namespace MCDzienny
{
    public class CmdInvincible : Command
    {
        public override string name { get { return "invincible"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override void Use(Player p, string message)
        {
            Player player = !(message != "") ? p : Player.Find(message);
            if (player == null)
            {
                Player.SendMessage(p, "Cannot find player.");
                return;
            }
            if (player.group.Permission > p.group.Permission)
            {
                Player.SendMessage(p, "Cannot toggle invincibility for someone of higher rank");
                return;
            }
            if (player.invincible)
            {
                player.invincible = false;
                if (Server.cheapMessage)
                {
                    Player.GlobalChat(p, string.Format("{0} has stopped being immortal", player.color + player.PublicName + Server.DefaultColor), showname: false);
                }
                return;
            }
            player.invincible = true;
            if (Server.cheapMessage)
            {
                Player.GlobalChat(p, player.color + player.PublicName + Server.DefaultColor + " " + Server.cheapMessageGiven, showname: false);
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/invincible [name] - Turns invincible mode on/off.");
            Player.SendMessage(p, "If [name] is given, that player's invincibility is toggled");
        }
    }
}