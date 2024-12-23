namespace MCDzienny.StoreSystem
{
    public abstract class Item
    {
        public abstract string Name { get; }

        public abstract int ListPosition { get; }

        public abstract int GetAmount(Player p);

        public abstract bool GetIsListed(Player p);

        public abstract string GetDescription(Player p);

        public abstract string GetHelp(Player p);

        public abstract int GetPrice(Player p);

        public abstract bool OnBuying(Player p);

        public abstract void OnBought(Player p);
    }
}