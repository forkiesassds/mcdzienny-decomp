namespace MCDzienny
{
    // Token: 0x0200011C RID: 284
    public class CmdAward : Command
    {
        // Token: 0x170003DB RID: 987
        // (get) Token: 0x0600087D RID: 2173 RVA: 0x0002B53C File Offset: 0x0002973C
        public override string name
        {
            get { return "award"; }
        }

        // Token: 0x170003DC RID: 988
        // (get) Token: 0x0600087E RID: 2174 RVA: 0x0002B544 File Offset: 0x00029744
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170003DD RID: 989
        // (get) Token: 0x0600087F RID: 2175 RVA: 0x0002B54C File Offset: 0x0002974C
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x170003DE RID: 990
        // (get) Token: 0x06000880 RID: 2176 RVA: 0x0002B554 File Offset: 0x00029754
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170003DF RID: 991
        // (get) Token: 0x06000881 RID: 2177 RVA: 0x0002B558 File Offset: 0x00029758
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x170003E0 RID: 992
        // (get) Token: 0x06000882 RID: 2178 RVA: 0x0002B55C File Offset: 0x0002975C
        public override string CustomName
        {
            get { return Lang.Command.AwardName; }
        }

        // Token: 0x06000884 RID: 2180 RVA: 0x0002B56C File Offset: 0x0002976C
        public override void Use(Player p, string message)
        {
            if (message == "" || message.IndexOf(' ') == -1)
            {
                Help(p);
                return;
            }

            var flag = true;
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

            var text = message.Split(' ')[0];
            var player = Player.Find(message);
            if (player != null) text = player.name;
            var text2 = message.Substring(message.IndexOf(' ') + 1);
            if (!Awards.awardExists(text2))
            {
                Player.SendMessage(p, Lang.Command.AwardMessage);
                Player.SendMessage(p, Lang.Command.AwardMessage1);
                return;
            }

            if (flag)
            {
                if (Awards.giveAward(text, text2))
                    Player.GlobalChat(p,
                        string.Format(Lang.Command.AwardMessage2, Server.FindColor(text) + text + Server.DefaultColor,
                            Awards.camelCase(text2)), false);
                else
                    Player.SendMessage(p, Lang.Command.AwardMessage3);
            }
            else if (Awards.takeAward(text, text2))
            {
                Player.GlobalChat(p,
                    string.Format(Lang.Command.AwardMessage4, Server.FindColor(text) + text + Server.DefaultColor,
                        Awards.camelCase(text2) + Server.DefaultColor), false);
            }
            else
            {
                Player.SendMessage(p, Lang.Command.AwardMessage5);
            }

            Awards.Save();
        }

        // Token: 0x06000885 RID: 2181 RVA: 0x0002B704 File Offset: 0x00029904
        public override void Help(Player p)
        {
            Player.SendMessage(p, Lang.Command.AwardHelp);
            Player.SendMessage(p, Lang.Command.AwardHelp1);
            Player.SendMessage(p, Lang.Command.AwardHelp2);
        }
    }
}