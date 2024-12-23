namespace MCDzienny
{
    public class CmdBuy : Command
    {
        public override string name { get { return "buy"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }

        public override bool ConsoleAccess { get { return false; } }

        public override string CustomName { get { return Lang.Command.BuyName; } }

        public override CommandScope Scope { get { return CommandScope.All; } }

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
            }
            else if (Store.life.amount > 0 && (message == Store.life.realPosition || message == Store.life.name.ToLower()))
            {
                Store.BuyLife(p);
            }
            else if (Store.armor.amount > 0 && (message == Store.armor.realPosition || message == Store.armor.name.ToLower()))
            {
                Store.BuyArmor(p);
            }
            else if (Store.water.amount > 0 && (message == Store.water.realPosition || message == Store.water.name.ToLower()))
            {
                Store.BuyWater(p);
            }
            else if (Store.sponge.amount > 0 && (message == Store.sponge.realPosition || message == Store.sponge.name.ToLower()))
            {
                Store.BuySponge(p);
            }
            else if (Store.hammer.amount > 0 && (message == Store.hammer.realPosition || message == Store.hammer.name.ToLower()))
            {
                Store.BuyHammer(p);
            }
            else if (Store.door.amount > 0 && (message == Store.door.realPosition || message == Store.door.name.ToLower()))
            {
                Store.BuyDoor(p);
            }
            else if (Store.teleport.amount > 0 && (message == Store.teleport.realPosition || message == Store.teleport.name.ToLower()))
            {
                Store.BuyTeleport(p);
            }
            else if (Store.color.amount > 0 && (message == Store.color.realPosition || message == Store.color.name.ToLower()))
            {
                Store.BuyColor(p);
            }
            else if (Store.title.amount > 0 && (message == Store.title.realPosition || message == Store.title.name.ToLower()))
            {
                Store.BuyTitle(p);
            }
            else if (Store.titleColor.amount > 0 && (message == Store.titleColor.realPosition || message == Store.titleColor.name.ToLower()))
            {
                Store.BuyTitleColor(p);
            }
            else if (Store.promotion.amount > 0 && (message == Store.promotion.realPosition || message == Store.promotion.name.ToLower()))
            {
                Store.BuyPromotion(p);
            }
            else if (Store.welcomeMessage.amount > 0 && (message == Store.welcomeMessage.realPosition || message == Store.welcomeMessage.name.ToLower()))
            {
                Store.BuyWelcome(p);
            }
            else if (Store.farewellMessage.amount > 0 && (message == Store.farewellMessage.realPosition || message == Store.farewellMessage.name.ToLower()))
            {
                Store.BuyFarewell(p);
            }
            else
            {
                all.Find("store").Use(p, "");
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, Lang.Command.BuyHelp);
        }
    }
}