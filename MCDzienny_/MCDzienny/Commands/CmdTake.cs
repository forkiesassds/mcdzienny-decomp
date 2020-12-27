namespace MCDzienny
{
    // Token: 0x020002FA RID: 762
    public class CmdTake : Command
    {
        // Token: 0x17000884 RID: 2180
        // (get) Token: 0x06001588 RID: 5512 RVA: 0x00076814 File Offset: 0x00074A14
        public override string name
        {
            get { return "take"; }
        }

        // Token: 0x17000885 RID: 2181
        // (get) Token: 0x06001589 RID: 5513 RVA: 0x0007681C File Offset: 0x00074A1C
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000886 RID: 2182
        // (get) Token: 0x0600158A RID: 5514 RVA: 0x00076824 File Offset: 0x00074A24
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x17000887 RID: 2183
        // (get) Token: 0x0600158B RID: 5515 RVA: 0x0007682C File Offset: 0x00074A2C
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000888 RID: 2184
        // (get) Token: 0x0600158C RID: 5516 RVA: 0x00076830 File Offset: 0x00074A30
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Admin; }
        }

        // Token: 0x0600158E RID: 5518 RVA: 0x0007683C File Offset: 0x00074A3C
        public override void Use(Player p, string message)
        {
            if (message.IndexOf(' ') == -1 || message.Split(' ').Length != 2)
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
            }
            else
            {
                if (num < 0)
                {
                    Player.SendMessage(p, string.Format("Cannot take negative {0}", Server.moneys));
                    return;
                }

                player.money -= num;
                Player.GlobalMessage(string.Format("{0} was rattled down for {1} {2}",
                    player.color + player.prefix + player.PublicName + Server.DefaultColor, num, Server.moneys));
            }
        }

        // Token: 0x0600158F RID: 5519 RVA: 0x0007696C File Offset: 0x00074B6C
        public override void Help(Player p)
        {
            Player.SendMessage(p,
                string.Format("/take [player] <amount> - Takes <amount> of {0} from [player]", Server.moneys));
        }
    }
}