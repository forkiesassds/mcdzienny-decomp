namespace MCDzienny.StoreSystem
{
    public class Color : Item
    {
        public override string Name { get { return "Color"; } }

        public override int ListPosition { get { return 8; } }

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
            return " - use /color [color] to set your name color,";
        }

        public override string GetHelp(Player p)
        {
            return "In order to use this item use /color command.";
        }

        public override bool OnBuying(Player p)
        {
            if (p.boughtColor)
            {
                Player.SendMessage(p, "You have already bought this item. In order to use it write: /color");
                return false;
            }
            return true;
        }

        public override void OnBought(Player p)
        {
            p.boughtColor = true;
            Player.SendMessage(p, "In order to change the color of your name use the command: /color");
        }
    }
}