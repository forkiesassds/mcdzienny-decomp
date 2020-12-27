namespace MCDzienny
{
    // Token: 0x020002F9 RID: 761
    public class CmdGive : Command
    {
        // Token: 0x1700087F RID: 2175
        // (get) Token: 0x06001580 RID: 5504 RVA: 0x0007668C File Offset: 0x0007488C
        public override string name
        {
            get { return "give"; }
        }

        // Token: 0x17000880 RID: 2176
        // (get) Token: 0x06001581 RID: 5505 RVA: 0x00076694 File Offset: 0x00074894
        public override string shortcut
        {
            get { return "gib"; }
        }

        // Token: 0x17000881 RID: 2177
        // (get) Token: 0x06001582 RID: 5506 RVA: 0x0007669C File Offset: 0x0007489C
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x17000882 RID: 2178
        // (get) Token: 0x06001583 RID: 5507 RVA: 0x000766A4 File Offset: 0x000748A4
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000883 RID: 2179
        // (get) Token: 0x06001584 RID: 5508 RVA: 0x000766A8 File Offset: 0x000748A8
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Admin; }
        }

        // Token: 0x06001586 RID: 5510 RVA: 0x000766B4 File Offset: 0x000748B4
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

            var player = Player.Find(message.Split(' ')[0]);
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
            }
            else
            {
                if (num < 0)
                {
                    Player.SendMessage(p, string.Format("Cannot give someone negative {0}", Server.moneys));
                    return;
                }

                player.money += num;
                Player.GlobalMessage(string.Format("{0} was given {1} {2}",
                    player.color + player.prefix + player.PublicName + Server.DefaultColor, num, Server.moneys));
            }
        }

        // Token: 0x06001587 RID: 5511 RVA: 0x000767FC File Offset: 0x000749FC
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/give [player] <amount> - Gives [player] <amount> " + Server.moneys);
        }
    }
}