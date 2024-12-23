namespace MCDzienny
{
    public class CmdLowlag : Command
    {
        public override string name { get { return "lowlag"; } }

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
            else if (Server.updateTimer.Interval > 1000.0)
            {
                Server.updateTimer.Interval = 100.0;
                Player.GlobalChat(null, string.Format("&dLow lag {0}mode was turned &cOFF{1}.", Server.DefaultColor, Server.DefaultColor), showname: false);
            }
            else
            {
                Server.updateTimer.Interval = 10000.0;
                Player.GlobalChat(null, string.Format("&dLow lag {0}mode was turned &aON{1}.", Server.DefaultColor, Server.DefaultColor), showname: false);
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/lowlag - Turns lowlag mode on or off");
        }
    }
}