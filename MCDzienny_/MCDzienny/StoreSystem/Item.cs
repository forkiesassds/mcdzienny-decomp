namespace MCDzienny.StoreSystem
{
    // Token: 0x02000232 RID: 562
    public abstract class Item
    {
        // Token: 0x170005E2 RID: 1506
        // (get) Token: 0x0600105E RID: 4190
        public abstract string Name { get; }

        // Token: 0x170005E3 RID: 1507
        // (get) Token: 0x0600105F RID: 4191
        public abstract int ListPosition { get; }

        // Token: 0x06001060 RID: 4192
        public abstract int GetAmount(Player p);

        // Token: 0x06001061 RID: 4193
        public abstract bool GetIsListed(Player p);

        // Token: 0x06001062 RID: 4194
        public abstract string GetDescription(Player p);

        // Token: 0x06001063 RID: 4195
        public abstract string GetHelp(Player p);

        // Token: 0x06001064 RID: 4196
        public abstract int GetPrice(Player p);

        // Token: 0x06001065 RID: 4197
        public abstract bool OnBuying(Player p);

        // Token: 0x06001066 RID: 4198
        public abstract void OnBought(Player p);
    }
}