namespace MCDzienny
{
    // Token: 0x020000DA RID: 218
    public class CmdPing : Command
    {
        // Token: 0x1700031F RID: 799
        // (get) Token: 0x0600070D RID: 1805 RVA: 0x00024474 File Offset: 0x00022674
        public override string name
        {
            get { return "ping"; }
        }

        // Token: 0x17000320 RID: 800
        // (get) Token: 0x0600070E RID: 1806 RVA: 0x0002447C File Offset: 0x0002267C
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000321 RID: 801
        // (get) Token: 0x0600070F RID: 1807 RVA: 0x00024484 File Offset: 0x00022684
        public override string type
        {
            get { return "info"; }
        }

        // Token: 0x17000322 RID: 802
        // (get) Token: 0x06000710 RID: 1808 RVA: 0x0002448C File Offset: 0x0002268C
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000323 RID: 803
        // (get) Token: 0x06000711 RID: 1809 RVA: 0x00024490 File Offset: 0x00022690
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Admin; }
        }

        // Token: 0x06000712 RID: 1810 RVA: 0x00024494 File Offset: 0x00022694
        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Player.players.ForEach(delegate(Player who)
                {
                    if (who.connectionSpeed == -2)
                    {
                        Player.SendMessage(p, who.name + " ping: greenlist");
                        return;
                    }

                    if (who.connectionSpeed == -1)
                    {
                        Player.SendMessage(p, who.name + " ping: is being tested");
                        return;
                    }

                    Player.SendMessage(p, who.name + " ping: " + who.connectionSpeed);
                });
                return;
            }

            var player = Player.Find(message);
            if (player == null)
            {
                Help(p);
                return;
            }

            player.TestConnection();
        }

        // Token: 0x06000713 RID: 1811 RVA: 0x000244F8 File Offset: 0x000226F8
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/ping - Connection speed.");
        }
    }
}