namespace MCDzienny.StoreSystem
{
    public class Farewell : Item
    {
        public override string Name { get { return "Farewell"; } }

        public override int ListPosition { get { return 13; } }

        public override int GetAmount(Player p)
        {
            return 1;
        }

        public override int GetPrice(Player p)
        {
            return 180;
        }

        public override bool GetIsListed(Player p)
        {
            return true;
        }

        public override string GetDescription(Player p)
        {
            return " - use /farewell [message] to set your farewell message.";
        }

        public override string GetHelp(Player p)
        {
            return "Farewell item lets you set the message that is diplayed when you disconnect from the server.";
        }

        public override bool OnBuying(Player p)
        {
            if (p.boughtFarewell)
            {
                Player.SendMessage(p, "You have already bought this item. In order to use it write: /farewell [message]");
                return false;
            }
            return true;
        }

        public override void OnBought(Player p)
        {
            p.boughtFarewell = true;
            Player.SendMessage(p, "In order to set your farewell message write: /farewell [message]");
        }
    }
}