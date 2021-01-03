namespace MCDzienny
{
    // Token: 0x020000B7 RID: 183
    public class CmdBuy : Command
    {
        // Token: 0x170002AA RID: 682
        // (get) Token: 0x06000635 RID: 1589 RVA: 0x00020EBC File Offset: 0x0001F0BC
        public override string name
        {
            get { return "buy"; }
        }

        // Token: 0x170002AB RID: 683
        // (get) Token: 0x06000636 RID: 1590 RVA: 0x00020EC4 File Offset: 0x0001F0C4
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170002AC RID: 684
        // (get) Token: 0x06000637 RID: 1591 RVA: 0x00020ECC File Offset: 0x0001F0CC
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x170002AD RID: 685
        // (get) Token: 0x06000638 RID: 1592 RVA: 0x00020ED4 File Offset: 0x0001F0D4
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170002AE RID: 686
        // (get) Token: 0x06000639 RID: 1593 RVA: 0x00020ED8 File Offset: 0x0001F0D8
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Banned; }
        }

        // Token: 0x170002AF RID: 687
        // (get) Token: 0x0600063A RID: 1594 RVA: 0x00020EDC File Offset: 0x0001F0DC
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x170002B0 RID: 688
        // (get) Token: 0x0600063B RID: 1595 RVA: 0x00020EE0 File Offset: 0x0001F0E0
        public override string CustomName
        {
            get { return Lang.Command.BuyName; }
        }

        // Token: 0x170002B1 RID: 689
        // (get) Token: 0x0600063C RID: 1596 RVA: 0x00020EE8 File Offset: 0x0001F0E8
        public override CommandScope Scope
        {
            get { return CommandScope.All; }
        }

        // Token: 0x0600063E RID: 1598 RVA: 0x00020EF4 File Offset: 0x0001F0F4
        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }

            if (p.level == InfectionSystem.InfectionSystem.currentInfectionLevel)
            {
                StoreSystem.Store.ZombieStore.BuyItem(p, message);
                return;
            }

            message = message.ToLower();
            if (message == Lang.Command.BuyParameter)
            {
                Store.PrintListMore(p);
                return;
            }

            if (Store.life.amount > 0 && (message == Store.life.realPosition || message == Store.life.name.ToLower()))
            {
                Store.BuyLife(p);
                return;
            }

            if (Store.armor.amount > 0 &&
                (message == Store.armor.realPosition || message == Store.armor.name.ToLower()))
            {
                Store.BuyArmor(p);
                return;
            }

            if (Store.water.amount > 0 &&
                (message == Store.water.realPosition || message == Store.water.name.ToLower()))
            {
                Store.BuyWater(p);
                return;
            }

            if (Store.sponge.amount > 0 &&
                (message == Store.sponge.realPosition || message == Store.sponge.name.ToLower()))
            {
                Store.BuySponge(p);
                return;
            }

            if (Store.hammer.amount > 0 &&
                (message == Store.hammer.realPosition || message == Store.hammer.name.ToLower()))
            {
                Store.BuyHammer(p);
                return;
            }

            if (Store.door.amount > 0 && (message == Store.door.realPosition || message == Store.door.name.ToLower()))
            {
                Store.BuyDoor(p);
                return;
            }

            if (Store.teleport.amount > 0 &&
                (message == Store.teleport.realPosition || message == Store.teleport.name.ToLower()))
            {
                Store.BuyTeleport(p);
                return;
            }

            if (Store.color.amount > 0 &&
                (message == Store.color.realPosition || message == Store.color.name.ToLower()))
            {
                Store.BuyColor(p);
                return;
            }

            if (Store.title.amount > 0 &&
                (message == Store.title.realPosition || message == Store.title.name.ToLower()))
            {
                Store.BuyTitle(p);
                return;
            }

            if (Store.titleColor.amount > 0 &&
                (message == Store.titleColor.realPosition || message == Store.titleColor.name.ToLower()))
            {
                Store.BuyTitleColor(p);
                return;
            }

            if (Store.promotion.amount > 0 &&
                (message == Store.promotion.realPosition || message == Store.promotion.name.ToLower()))
            {
                Store.BuyPromotion(p);
                return;
            }

            if (Store.welcomeMessage.amount > 0 && (message == Store.welcomeMessage.realPosition ||
                                                    message == Store.welcomeMessage.name.ToLower()))
            {
                Store.BuyWelcome(p);
                return;
            }

            if (Store.farewellMessage.amount > 0 && (message == Store.farewellMessage.realPosition ||
                                                     message == Store.farewellMessage.name.ToLower()))
            {
                Store.BuyFarewell(p);
                return;
            }

            all.Find("store").Use(p, "");
        }

        // Token: 0x0600063F RID: 1599 RVA: 0x00021280 File Offset: 0x0001F480
        public override void Help(Player p)
        {
            Player.SendMessage(p, Lang.Command.BuyHelp);
        }
    }
}