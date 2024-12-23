namespace MCDzienny
{
    public class CmdTake : Command
    {
        public override string name { get { return "take"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }

        public override void Use(Player p, string message)
        {
            if (message.IndexOf(' ') == -1 || message.Split(' ').Length != 2)
            {
                Help(p);
                return;
            }
            Player player = Player.Find(message.Split(' ')[0]);
            if (player == null)
            {
                Player.SendMessage(p, "Could not find player entered");
                return;
            }
            if (player == p)
            {
                Player.SendMessage(p, "Sorry. Can't allow you to take money from yourself");
                return;
            }
            int num;
            try
            {
                num = int.Parse(message.Split(' ')[1]);
            }
            catch
            {
                Player.SendMessage(p, "Invalid amount");
                return;
            }
            if (player.money - num < 0)
            {
                Player.SendMessage(p, string.Format("Players cannot have under 0 {0}", Server.moneys));
                return;
            }
            if (num < 0)
            {
                Player.SendMessage(p, string.Format("Cannot take negative {0}", Server.moneys));
                return;
            }
            player.money -= num;
            Player.GlobalMessage(string.Format("{0} was rattled down for {1} {2}", player.color + player.prefix + player.PublicName + Server.DefaultColor, num,
                                               Server.moneys));
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, string.Format("/take [player] <amount> - Takes <amount> of {0} from [player]", Server.moneys));
        }
    }
}