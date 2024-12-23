namespace MCDzienny
{
    public class CmdGive : Command
    {
        public override string name { get { return "give"; } }

        public override string shortcut { get { return "gib"; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }

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
            if (player == null)
            {
                Player.SendMessage(p, "Could not find player entered");
                return;
            }
            if (player == p)
            {
                Player.SendMessage(p, string.Format("Sorry. Can't allow you to give {0} to yourself", Server.moneys));
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
            if (num < 0)
            {
                Player.SendMessage(p, string.Format("Cannot give someone negative {0}", Server.moneys));
                return;
            }
            player.money += num;
            Player.GlobalMessage(string.Format("{0} was given {1} {2}", player.color + player.prefix + player.PublicName + Server.DefaultColor, num, Server.moneys));
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/give [player] <amount> - Gives [player] <amount> " + Server.moneys);
        }
    }
}