namespace MCDzienny
{
    public class CmdPay : Command
    {
        public override string name { get { return "pay"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            if (message.IndexOf(' ') == -1)
            {
                Help(p);
                return;
            }
            if (message.Split(' ').Length != 2)
            {
                Help(p);
                return;
            }
            Player player = Player.Find(message.Split(' ')[0]);
            if (player == null || player.hidden)
            {
                Player.SendMessage(p, "Could not find player entered");
                return;
            }
            if (player == p)
            {
                Player.SendMessage(p, "Sorry. Can't allow you to pay yourself");
                return;
            }
            if (player.ip == p.ip)
            {
                Player.SendMessage(p, "Sorry, you can't pay player with the same IP as you.");
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
            if (player.money + num > 16777215)
            {
                Player.SendMessage(p, string.Format("Players cannot have over 16777215 {0}", Server.moneys));
                return;
            }
            if (p.money - num < 0)
            {
                Player.SendMessage(p, string.Format("You don't have that much {0}", Server.moneys));
                return;
            }
            if (num < 0)
            {
                Player.SendMessage(p, string.Format("Cannot pay negative {0}", Server.moneys));
                return;
            }
            player.money += num;
            p.money -= num;
            Player.GlobalMessage(string.Format("{0} paid {1} {2} {3}", p.color + p.PublicName + Server.DefaultColor,
                                               player.color + player.PublicName + Server.DefaultColor, num, Server.moneys));
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/pay [player] <amount> - Pays <amount> of " + Server.moneys + " to [player]");
        }
    }
}