namespace MCDzienny.StoreSystem
{
    // Token: 0x0200023E RID: 574
    public class ZombieStore
    {
        // Token: 0x040008A1 RID: 2209
        public static ItemsCollection storeItems = new ItemsCollection();

        // Token: 0x060010B0 RID: 4272 RVA: 0x000566DC File Offset: 0x000548DC
        public static void SetDefaultItems()
        {
            storeItems.Clear();
            storeItems.Add(new Disinfect());
            storeItems.Add(new Color());
            storeItems.Add(new Title());
            storeItems.Add(new Welcome());
            storeItems.Add(new Farewell());
        }

        // Token: 0x060010B1 RID: 4273 RVA: 0x00056740 File Offset: 0x00054940
        public void BuyItem(Player p, string itemName)
        {
            int num;
            Item item;
            if (int.TryParse(itemName, out num))
            {
                item = storeItems.FindByNumber(p, num);
                if (item == null)
                {
                    Player.SendMessage(p, "Couldn't find the item. Are you sure the item nuber is: " + num + "?");
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

            if (!item.OnBuying(p)) return;
            if (item.GetPrice(p) > p.money)
            {
                Player.SendMessage(p, "You don't have enough money to buy this item!");
                return;
            }

            p.money -= item.GetPrice(p);
            item.OnBought(p);
        }

        // Token: 0x060010B2 RID: 4274 RVA: 0x000567E8 File Offset: 0x000549E8
        public void DisplayItems(Player p, int page)
        {
            Player.SendMessage(p, "You can buy:");
            var num = 1;
            for (var i = 0; i < storeItems.SortedList.Count; i++)
                if (storeItems.SortedList[i].GetIsListed(p))
                {
                    if (storeItems.SortedList[i].GetAmount(p) == 1)
                        Player.SendMessage(p,
                            string.Concat("%c", num, ": %d", storeItems.SortedList[i].Name, " - ",
                                p.money - storeItems.SortedList[i].GetPrice(p) < 0 ? "%c" : "%a",
                                storeItems.SortedList[i].GetPrice(p), " ", Server.moneys, Server.DefaultColor,
                                storeItems.SortedList[i].GetDescription(p)));
                    else
                        Player.SendMessage(p,
                            string.Concat("%c", num, ": %d", storeItems.SortedList[i].Name, "(x",
                                storeItems.SortedList[i].GetAmount(p), ") - ",
                                p.money - storeItems.SortedList[i].GetPrice(p) < 0 ? "%c" : "%a",
                                storeItems.SortedList[i].GetPrice(p), " ", Server.moneys, Server.DefaultColor,
                                storeItems.SortedList[i].GetDescription(p)));
                    num++;
                }
        }
    }
}