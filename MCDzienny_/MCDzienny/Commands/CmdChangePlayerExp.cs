namespace MCDzienny
{
    // Token: 0x020000B8 RID: 184
    public class CmdChangePlayerExp : Command
    {
        // Token: 0x170002B2 RID: 690
        // (get) Token: 0x06000640 RID: 1600 RVA: 0x00021290 File Offset: 0x0001F490
        public override string name
        {
            get { return "cpe"; }
        }

        // Token: 0x170002B3 RID: 691
        // (get) Token: 0x06000641 RID: 1601 RVA: 0x00021298 File Offset: 0x0001F498
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170002B4 RID: 692
        // (get) Token: 0x06000642 RID: 1602 RVA: 0x000212A0 File Offset: 0x0001F4A0
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x170002B5 RID: 693
        // (get) Token: 0x06000643 RID: 1603 RVA: 0x000212A8 File Offset: 0x0001F4A8
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170002B6 RID: 694
        // (get) Token: 0x06000644 RID: 1604 RVA: 0x000212AC File Offset: 0x0001F4AC
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Admin; }
        }

        // Token: 0x170002B7 RID: 695
        // (get) Token: 0x06000645 RID: 1605 RVA: 0x000212B0 File Offset: 0x0001F4B0
        public override string CustomName
        {
            get { return Lang.Command.ChangePlayerExpName; }
        }

        // Token: 0x06000647 RID: 1607 RVA: 0x000212C0 File Offset: 0x0001F4C0
        public override void Use(Player p, string message)
        {
            if (message.Split(' ').Length != 2)
            {
                Player.SendMessage(p, Lang.Command.ChangePlayerExpMessage);
                return;
            }

            var name = message.Split(' ')[0];
            var num = 0;
            try
            {
                num = int.Parse(message.Split(' ')[1]);
            }
            catch
            {
                Player.SendMessage(p, Lang.Command.ChangePlayerExpMessage1);
                return;
            }

            var player = Player.Find(name);
            if (player != null)
            {
                player.totalScore += num;
                Player.SendMessage(p, string.Format(Lang.Command.ChangePlayerExpMessage3, num));
                return;
            }

            Player.SendMessage(p, Lang.Command.ChangePlayerExpMessage2);
        }

        // Token: 0x06000648 RID: 1608 RVA: 0x00021384 File Offset: 0x0001F584
        public override void Help(Player p)
        {
            Player.SendMessage(p, Lang.Command.ChangePlayerExpHelp);
        }
    }
}