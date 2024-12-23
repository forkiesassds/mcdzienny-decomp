namespace MCDzienny.StoreSystem
{
    public class Title : Item
    {
        public override string Name { get { return "Title"; } }

        public override int ListPosition { get { return 9; } }

        public override int GetAmount(Player p)
        {
            return 1;
        }

        public override int GetPrice(Player p)
        {
            return 280;
        }

        public override bool GetIsListed(Player p)
        {
            return true;
        }

        public override string GetDescription(Player p)
        {
            return " - use /title [your title] to set a new title,";
        }

        public override string GetHelp(Player p)
        {
            return "This item lets you change your title.";
        }

        public override bool OnBuying(Player p)
        {
            if (p.boughtTitle)
            {
                Player.SendMessage(p, "You have already bought this item. In order to use it write: /title [your title]");
                return false;
            }
            return true;
        }

        public override void OnBought(Player p)
        {
            p.boughtTitle = true;
            Player.SendMessage(p, "In order to change your title use the command: /title");
        }
    }
}