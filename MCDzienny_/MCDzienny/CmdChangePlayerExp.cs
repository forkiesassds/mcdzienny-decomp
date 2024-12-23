namespace MCDzienny
{
    public class CmdChangePlayerExp : Command
    {
        public override string name { get { return "cpe"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }

        public override string CustomName { get { return Lang.Command.ChangePlayerExpName; } }

        public override void Use(Player p, string message)
        {
            if (message.Split(' ').Length != 2)
            {
                Player.SendMessage(p, Lang.Command.ChangePlayerExpMessage);
                return;
            }
            string text = message.Split(' ')[0];
            int num = 0;
            try
            {
                num = int.Parse(message.Split(' ')[1]);
            }
            catch
            {
                Player.SendMessage(p, Lang.Command.ChangePlayerExpMessage1);
                return;
            }
            Player player = Player.Find(text);
            if (player == null)
            {
                Player.SendMessage(p, Lang.Command.ChangePlayerExpMessage2);
                return;
            }
            player.totalScore += num;
            Player.SendMessage(p, string.Format(Lang.Command.ChangePlayerExpMessage3, num));
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, Lang.Command.ChangePlayerExpHelp);
        }
    }
}