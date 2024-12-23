using System.Collections;
using System.Collections.Generic;

namespace MCDzienny.Notification
{
    public class ItemCollection : IEnumerable
    {
        readonly List<Item> itemCollection;

        public ItemCollection()
        {
            itemCollection = new List<Item>();
        }

        public int Count { get { return itemCollection.Count; } }

        public Item this[int index] { get { return itemCollection[index]; } set { itemCollection[index] = value; } }

        public IEnumerator GetEnumerator()
        {
            return itemCollection.GetEnumerator();
        }

        public void Add(Item item)
        {
            itemCollection.Add(item);
        }

        public bool Remove(Item item)
        {
            return itemCollection.Remove(item);
        }

        public void SortByPubDateIncreasing()
        {
            itemCollection.Sort((a, b) => a.PubDate.CompareTo(b.PubDate));
        }
    }
}