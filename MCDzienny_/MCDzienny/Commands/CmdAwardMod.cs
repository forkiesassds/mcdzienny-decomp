namespace MCDzienny
{
    // Token: 0x0200011E RID: 286
    public class CmdAwardMod : Command
    {
        // Token: 0x170003E6 RID: 998
        // (get) Token: 0x0600088E RID: 2190 RVA: 0x0002BA6C File Offset: 0x00029C6C
        public override string name
        {
            get { return "awardmod"; }
        }

        // Token: 0x170003E7 RID: 999
        // (get) Token: 0x0600088F RID: 2191 RVA: 0x0002BA74 File Offset: 0x00029C74
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170003E8 RID: 1000
        // (get) Token: 0x06000890 RID: 2192 RVA: 0x0002BA7C File Offset: 0x00029C7C
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x170003E9 RID: 1001
        // (get) Token: 0x06000891 RID: 2193 RVA: 0x0002BA84 File Offset: 0x00029C84
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170003EA RID: 1002
        // (get) Token: 0x06000892 RID: 2194 RVA: 0x0002BA88 File Offset: 0x00029C88
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Admin; }
        }

        // Token: 0x170003EB RID: 1003
        // (get) Token: 0x06000893 RID: 2195 RVA: 0x0002BA8C File Offset: 0x00029C8C
        public override string CustomName
        {
            get { return Lang.Command.AwardModName; }
        }

        // Token: 0x06000895 RID: 2197 RVA: 0x0002BA9C File Offset: 0x00029C9C
        public override void Use(Player p, string message)
        {
            if (message == "" || message.IndexOf(' ') == -1)
            {
                Help(p);
                return;
            }

            var flag = true;
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

                var text = message.Split(':')[0].Trim();
                var text2 = message.Split(':')[1].Trim();
                if (!Awards.addAward(text, text2))
                    Player.SendMessage(p, Lang.Command.AwardModMessage1);
                else
                    Player.GlobalChat(p, string.Format(Lang.Command.AwardModMessage2, text, text2), false);
            }
            else if (!Awards.removeAward(message))
            {
                Player.SendMessage(p, Lang.Command.AwardModMessage3);
            }
            else
            {
                Player.GlobalChat(p, string.Format(Lang.Command.AwardModMessage4, message), false);
            }

            Awards.Save();
        }

        // Token: 0x06000896 RID: 2198 RVA: 0x0002BBFC File Offset: 0x00029DFC
        public override void Help(Player p)
        {
            Player.SendMessage(p, Lang.Command.AwardModHelp);
            Player.SendMessage(p, Lang.Command.AwardModHelp1);
            Player.SendMessage(p, string.Format(Lang.Command.AwardModHelp2, Server.DefaultColor));
        }
    }
}