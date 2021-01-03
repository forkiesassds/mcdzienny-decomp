namespace MCDzienny.StoreSystem
{
    // Token: 0x02000236 RID: 566
    public class Disinfect : Item
    {
        // Token: 0x170005EB RID: 1515
        // (get) Token: 0x06001081 RID: 4225 RVA: 0x00055E1C File Offset: 0x0005401C
        public override string Name
        {
            get { return "Disinfect"; }
        }

        // Token: 0x170005EC RID: 1516
        // (get) Token: 0x06001082 RID: 4226 RVA: 0x00055E24 File Offset: 0x00054024
        public override int ListPosition
        {
            get { return 1; }
        }

        // Token: 0x06001083 RID: 4227 RVA: 0x00055E28 File Offset: 0x00054028
        public override int GetAmount(Player p)
        {
            return 1;
        }

        // Token: 0x06001084 RID: 4228 RVA: 0x00055E2C File Offset: 0x0005402C
        public override int GetPrice(Player p)
        {
            return 100;
        }

        // Token: 0x06001085 RID: 4229 RVA: 0x00055E30 File Offset: 0x00054030
        public override bool GetIsListed(Player p)
        {
            return true;
        }

        // Token: 0x06001086 RID: 4230 RVA: 0x00055E34 File Offset: 0x00054034
        public override string GetDescription(Player p)
        {
            return " - turns you back into a human,";
        }

        // Token: 0x06001087 RID: 4231 RVA: 0x00055E3C File Offset: 0x0005403C
        public override string GetHelp(Player p)
        {
            return "If you buy this item you will change back to a human.";
        }

        // Token: 0x06001088 RID: 4232 RVA: 0x00055E44 File Offset: 0x00054044
        public override bool OnBuying(Player p)
        {
            if (!p.isZombie)
            {
                Player.SendMessage(p, "You are still a human. You don't need the disinfection... yet.");
                return false;
            }

            return true;
        }

        // Token: 0x06001089 RID: 4233 RVA: 0x00055E5C File Offset: 0x0005405C
        public override void OnBought(Player p)
        {
            InfectionSystem.InfectionSystem.RemoveZombieDataAndSkin(p);
            Player.SendMessage(p, "You are a human again.");
            Player.GlobalMessageLevel(InfectionSystem.InfectionSystem.currentInfectionLevel,
                p.PublicName + " was disinfected.");
            InfectionSystem.InfectionSystem.DisplayHumansLeft();
        }
    }
}