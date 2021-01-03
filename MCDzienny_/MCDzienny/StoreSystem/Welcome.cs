namespace MCDzienny.StoreSystem
{
    // Token: 0x02000239 RID: 569
    public class Welcome : Item
    {
        // Token: 0x170005F2 RID: 1522
        // (get) Token: 0x0600109F RID: 4255 RVA: 0x000560AC File Offset: 0x000542AC
        public override string Name
        {
            get { return "Welcome"; }
        }

        // Token: 0x170005F3 RID: 1523
        // (get) Token: 0x060010A0 RID: 4256 RVA: 0x000560B4 File Offset: 0x000542B4
        public override int ListPosition
        {
            get { return 12; }
        }

        // Token: 0x060010A1 RID: 4257 RVA: 0x000560B8 File Offset: 0x000542B8
        public override int GetAmount(Player p)
        {
            return 1;
        }

        // Token: 0x060010A2 RID: 4258 RVA: 0x000560BC File Offset: 0x000542BC
        public override int GetPrice(Player p)
        {
            return 200;
        }

        // Token: 0x060010A3 RID: 4259 RVA: 0x000560C4 File Offset: 0x000542C4
        public override bool GetIsListed(Player p)
        {
            return true;
        }

        // Token: 0x060010A4 RID: 4260 RVA: 0x000560C8 File Offset: 0x000542C8
        public override string GetDescription(Player p)
        {
            return " - use /welcome [message] to set your welcome message,";
        }

        // Token: 0x060010A5 RID: 4261 RVA: 0x000560D0 File Offset: 0x000542D0
        public override string GetHelp(Player p)
        {
            return "Welcome item lets you set the message that is diplayed when you connect to the server.";
        }

        // Token: 0x060010A6 RID: 4262 RVA: 0x000560D8 File Offset: 0x000542D8
        public override bool OnBuying(Player p)
        {
            if (p.boughtWelcome)
            {
                Player.SendMessage(p,
                    "You have already bought this item. In order to use it write: /welcome [message]");
                return false;
            }

            return true;
        }

        // Token: 0x060010A7 RID: 4263 RVA: 0x000560F0 File Offset: 0x000542F0
        public override void OnBought(Player p)
        {
            p.boughtWelcome = true;
            Player.SendMessage(p, "In order to set your welcome message write: /welcome [message]");
        }
    }
}