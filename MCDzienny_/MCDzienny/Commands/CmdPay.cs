namespace MCDzienny
{
    // Token: 0x020002FB RID: 763
    public class CmdPay : Command
    {
        // Token: 0x17000889 RID: 2185
        // (get) Token: 0x06001590 RID: 5520 RVA: 0x00076984 File Offset: 0x00074B84
        public override string name
        {
            get { return "pay"; }
        }

        // Token: 0x1700088A RID: 2186
        // (get) Token: 0x06001591 RID: 5521 RVA: 0x0007698C File Offset: 0x00074B8C
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x1700088B RID: 2187
        // (get) Token: 0x06001592 RID: 5522 RVA: 0x00076994 File Offset: 0x00074B94
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x1700088C RID: 2188
        // (get) Token: 0x06001593 RID: 5523 RVA: 0x0007699C File Offset: 0x00074B9C
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x1700088D RID: 2189
        // (get) Token: 0x06001594 RID: 5524 RVA: 0x000769A0 File Offset: 0x00074BA0
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Banned; }
        }

        // Token: 0x1700088E RID: 2190
        // (get) Token: 0x06001595 RID: 5525 RVA: 0x000769A4 File Offset: 0x00074BA4
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x06001596 RID: 5526 RVA: 0x000769A8 File Offset: 0x00074BA8
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
            }
            else
            {
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
        }

        // Token: 0x06001597 RID: 5527 RVA: 0x00076B64 File Offset: 0x00074D64
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/pay [player] <amount> - Pays <amount> of " + Server.moneys + " to [player]");
        }
    }
}