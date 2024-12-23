namespace MCDzienny.StoreSystem
{
    public class ZombieStore
    {
        public static ItemsCollection storeItems = new ItemsCollection();

        public static void SetDefaultItems()
        {
            storeItems.Clear();
            storeItems.Add(new Disinfect());
            storeItems.Add(new Color());
            storeItems.Add(new Title());
            storeItems.Add(new Welcome());
            storeItems.Add(new Farewell());
        }

        public void BuyItem(Player p, string itemName)
        {
            Item item = null;
            int result;
            if (int.TryParse(itemName, out result))
            {
                item = storeItems.FindByNumber(p, result);
                if (item == null)
                {
                    Player.SendMessage(p, "Couldn't find the item. Are you sure the item nuber is: " + result + "?");
                    return;
                }
            }
            else
            {
                item = storeItems.Find(itemName);
                if (item == null)
                {
                    Player.SendMessage(p, "Store doesn't offer the item named: " + itemName);
                    return;
                }
            }
            if (item.OnBuying(p))
            {
                if (item.GetPrice(p) > p.money)
                {
                    Player.SendMessage(p, "You don't have enough money to buy this item!");
                    return;
                }
                p.money -= item.GetPrice(p);
                item.OnBought(p);
            }
        }

        public void DisplayItems(Player p, int page)
        {
            Player.SendMessage(p, "You can buy:");
            int num = 1;
            for (int i = 0; i < storeItems.SortedList.Count; i++)
            {
                if (storeItems.SortedList[i].GetIsListed(p))
                {
                    if (storeItems.SortedList[i].GetAmount(p) == 1)
                    {
                        Player.SendMessage(
                            p,
                            "%c" + num + ": %d" + storeItems.SortedList[i].Name + " - " + (p.money - storeItems.SortedList[i].GetPrice(p) < 0 ? "%c" : "%a") +
                            storeItems.SortedList[i].GetPrice(p) + " " + Server.moneys + Server.DefaultColor + storeItems.SortedList[i].GetDescription(p));
                    }
                    else
                    {
                        Player.SendMessage(
                            p,
                            "%c" + num + ": %d" + storeItems.SortedList[i].Name + "(x" + storeItems.SortedList[i].GetAmount(p) + ") - " +
                            (p.money - storeItems.SortedList[i].GetPrice(p) < 0 ? "%c" : "%a") + storeItems.SortedList[i].GetPrice(p) + " " + Server.moneys +
                            Server.DefaultColor + storeItems.SortedList[i].GetDescription(p));
                    }
                    num++;
                }
            }
        }
    }
}