namespace MCDzienny
{
    public class CmdAward : Command
    {
        public override string name { get { return "award"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override string CustomName { get { return Lang.Command.AwardName; } }

        public override void Use(Player p, string message)
        {
            if (message == "" || message.IndexOf(' ') == -1)
            {
                Help(p);
                return;
            }
            bool flag = true;
            if (message.Split(' ')[0].ToLower() == Lang.Command.AwardParameter)
            {
                flag = true;
                message = message.Substring(message.IndexOf(' ') + 1);
            }
            else if (message.Split(' ')[0].ToLower() == Lang.Command.AwardParameter1)
            {
                flag = false;
                message = message.Substring(message.IndexOf(' ') + 1);
            }
            string text = message.Split(' ')[0];
            Player player = Player.Find(message);
            if (player != null)
            {
                text = player.name;
            }
            string text2 = message.Substring(message.IndexOf(' ') + 1);
            if (!Awards.awardExists(text2))
            {
                Player.SendMessage(p, Lang.Command.AwardMessage);
                Player.SendMessage(p, Lang.Command.AwardMessage1);
                return;
            }
            if (flag)
            {
                if (Awards.giveAward(text, text2))
                {
                    Player.GlobalChat(p, string.Format(Lang.Command.AwardMessage2, Server.FindColor(text) + text + Server.DefaultColor, Awards.camelCase(text2)),
                                      showname: false);
                }
                else
                {
                    Player.SendMessage(p, Lang.Command.AwardMessage3);
                }
            }
            else if (Awards.takeAward(text, text2))
            {
                Player.GlobalChat(
                    p, string.Format(Lang.Command.AwardMessage4, Server.FindColor(text) + text + Server.DefaultColor, Awards.camelCase(text2) + Server.DefaultColor),
                    showname: false);
            }
            else
            {
                Player.SendMessage(p, Lang.Command.AwardMessage5);
            }
            Awards.Save();
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, Lang.Command.AwardHelp);
            Player.SendMessage(p, Lang.Command.AwardHelp1);
            Player.SendMessage(p, Lang.Command.AwardHelp2);
        }
    }
}