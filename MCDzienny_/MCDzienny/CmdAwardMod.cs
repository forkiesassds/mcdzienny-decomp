namespace MCDzienny
{
    public class CmdAwardMod : Command
    {
        public override string name { get { return "awardmod"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }

        public override string CustomName { get { return Lang.Command.AwardModName; } }

        public override void Use(Player p, string message)
        {
            if (message == "" || message.IndexOf(' ') == -1)
            {
                Help(p);
                return;
            }
            bool flag = true;
            if (message.Split(' ')[0].ToLower() == Lang.Command.AwardModParameter)
            {
                message = message.Substring(message.IndexOf(' ') + 1);
            }
            else if (message.Split(' ')[0].ToLower() == Lang.Command.AwardModParameter1)
            {
                flag = false;
                message = message.Substring(message.IndexOf(' ') + 1);
            }
            if (flag)
            {
                if (message.IndexOf(":") == -1)
                {
                    Player.SendMessage(p, Lang.Command.AwardModMessage);
                    Help(p);
                    return;
                }
                string text = message.Split(':')[0].Trim();
                string text2 = message.Split(':')[1].Trim();
                if (!Awards.addAward(text, text2))
                {
                    Player.SendMessage(p, Lang.Command.AwardModMessage1);
                }
                else
                {
                    Player.GlobalChat(p, string.Format(Lang.Command.AwardModMessage2, text, text2), showname: false);
                }
            }
            else if (!Awards.removeAward(message))
            {
                Player.SendMessage(p, Lang.Command.AwardModMessage3);
            }
            else
            {
                Player.GlobalChat(p, string.Format(Lang.Command.AwardModMessage4, message), showname: false);
            }
            Awards.Save();
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, Lang.Command.AwardModHelp);
            Player.SendMessage(p, Lang.Command.AwardModHelp1);
            Player.SendMessage(p, string.Format(Lang.Command.AwardModHelp2, Server.DefaultColor));
        }
    }
}