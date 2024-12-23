using System.Collections.Generic;

namespace MCDzienny.StoreSystem
{
    public class ItemsCollection
    {

        readonly List<Item> itemsCollection = new List<Item>();
        bool dirty;

        public int Count { get { return itemsCollection.Count; } }

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

        public Item this[int i] { get { return itemsCollection[i]; } }

        public void Add(Item item)
        {
            itemsCollection.Add(item);
            dirty = true;
        }

        public bool Remove(Item item)
        {
            dirty = true;
            return itemsCollection.Remove(item);
        }

        public Item Find(string name)
        {
            name = name.ToLower();
            foreach (Item item in itemsCollection)
            {
                if (item.Name.ToLower() == name.ToLower())
                {
                    return item;
                }
            }
            return null;
        }

        internal Item FindByNumber(Player p, int itemNumber)
        {
            if (itemNumber > SortedList.Count || itemNumber < 1)
            {
                return null;
            }
            itemNumber--;
            int num = 0;
            for (int i = 0; i < SortedList.Count; i++)
            {
                if (SortedList[i].GetIsListed(p))
                {
                    if (itemNumber == num)
                    {
                        return SortedList[i];
                    }
                    num++;
                }
            }
            return null;
        }

        public void Clear()
        {
            itemsCollection.Clear();
        }
    }
}