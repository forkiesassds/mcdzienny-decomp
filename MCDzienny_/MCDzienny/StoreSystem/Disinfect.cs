namespace MCDzienny.StoreSystem
{
    public class Disinfect : Item
    {
        public override string Name { get { return "Disinfect"; } }

        public override int ListPosition { get { return 1; } }

        public override int GetAmount(Player p)
        {
            return 1;
        }

        public override int GetPrice(Player p)
        {
            return 100;
        }

        public override bool GetIsListed(Player p)
        {
            return true;
        }

        public override string GetDescription(Player p)
        {
            return " - turns you back into a human,";
        }

        public override string GetHelp(Player p)
        {
            return "If you buy this item you will change back to a human.";
        }

        public override bool OnBuying(Player p)
        {
            if (!p.isZombie)
            {
                Player.SendMessage(p, "You are still a human. You don't need the disinfection... yet.");
                return false;
            }
            return true;
        }

        public override void OnBought(Player p)
        {
            InfectionSystem.InfectionSystem.RemoveZombieDataAndSkin(p);
            Player.SendMessage(p, "You are a human again.");
            Player.GlobalMessageLevel(InfectionSystem.InfectionSystem.currentInfectionLevel, p.PublicName + " was disinfected.");
            InfectionSystem.InfectionSystem.DisplayHumansLeft();
        }
    }
}