using System.Collections.Generic;

namespace MCDzienny.StoreSystem
{
    // Token: 0x02000237 RID: 567
    public class ItemsCollection
    {
        // Token: 0x04000892 RID: 2194
        private bool dirty;

        // Token: 0x04000893 RID: 2195
        private readonly List<Item> itemsCollection = new List<Item>();

        // Token: 0x170005ED RID: 1517
        // (get) Token: 0x0600108B RID: 4235 RVA: 0x00055E98 File Offset: 0x00054098
        public int Count
        {
            get { return itemsCollection.Count; }
        }

        // Token: 0x170005EE RID: 1518
        // (get) Token: 0x0600108C RID: 4236 RVA: 0x00055EA8 File Offset: 0x000540A8
        public List<Item> SortedList
        {
            get
            {
                if (dirty)
                {
                    itemsCollection.Sort((x, y) => x.ListPosition.CompareTo(y.ListPosition));
                    dirty = false;
                }

                return itemsCollection;
            }
        }

        // Token: 0x170005EF RID: 1519
        public Item this[int i]
        {
            get { return itemsCollection[i]; }
        }

        // Token: 0x0600108E RID: 4238 RVA: 0x00055EF8 File Offset: 0x000540F8
        public void Add(Item item)
        {
            itemsCollection.Add(item);
            dirty = true;
        }

        // Token: 0x0600108F RID: 4239 RVA: 0x00055F10 File Offset: 0x00054110
        public bool Remove(Item item)
        {
            dirty = true;
            return itemsCollection.Remove(item);
        }

        // Token: 0x06001090 RID: 4240 RVA: 0x00055F28 File Offset: 0x00054128
        public Item Find(string name)
        {
            name = name.ToLower();
            foreach (var item in itemsCollection)
                if (item.Name.ToLower() == name.ToLower())
                    return item;
            return null;
        }

        // Token: 0x06001091 RID: 4241 RVA: 0x00055F9C File Offset: 0x0005419C
        internal Item FindByNumber(Player p, int itemNumber)
        {
            if (itemNumber > SortedList.Count || itemNumber < 1) return null;
            itemNumber--;
            var num = 0;
            for (var i = 0; i < SortedList.Count; i++)
                if (SortedList[i].GetIsListed(p))
                {
                    if (itemNumber == num) return SortedList[i];
                    num++;
                }

            return null;
        }

        // Token: 0x06001092 RID: 4242 RVA: 0x00056004 File Offset: 0x00054204
        public void Clear()
        {
            itemsCollection.Clear();
        }
    }
}