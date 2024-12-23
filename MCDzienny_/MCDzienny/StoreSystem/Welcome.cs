namespace MCDzienny.StoreSystem
{
    public class Welcome : Item
    {
        public override string Name { get { return "Welcome"; } }

        public override int ListPosition { get { return 12; } }

        public override int GetAmount(Player p)
        {
            return 1;
        }

        public override int GetPrice(Player p)
        {
            return 200;
        }

        public override bool GetIsListed(Player p)
        {
            return true;
        }

        public override string GetDescription(Player p)
        {
            return " - use /welcome [message] to set your welcome message,";
        }

        public override string GetHelp(Player p)
        {
            return "Welcome item lets you set the message that is diplayed when you connect to the server.";
        }

        public override bool OnBuying(Player p)
        {
            if (p.boughtWelcome)
            {
                Player.SendMessage(p, "You have already bought this item. In order to use it write: /welcome [message]");
                return false;
            }
            return true;
        }

        public override void OnBought(Player p)
        {
            p.boughtWelcome = true;
            Player.SendMessage(p, "In order to set your welcome message write: /welcome [message]");
        }
    }
}