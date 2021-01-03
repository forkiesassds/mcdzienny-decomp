using System.Collections;
using System.Collections.Generic;

namespace MCDzienny.Notification
{
    // Token: 0x020001D8 RID: 472
    public class ItemCollection : IEnumerable
    {
        // Token: 0x040006D9 RID: 1753
        private readonly List<Item> itemCollection;

        // Token: 0x06000D36 RID: 3382 RVA: 0x0004BA48 File Offset: 0x00049C48
        public ItemCollection()
        {
            itemCollection = new List<Item>();
        }

        // Token: 0x170004FB RID: 1275
        // (get) Token: 0x06000D33 RID: 3379 RVA: 0x0004BA18 File Offset: 0x00049C18
        public int Count
        {
            get { return itemCollection.Count; }
        }

        // Token: 0x170004FC RID: 1276
        public Item this[int index]
        {
            get { return itemCollection[index]; }
            set { itemCollection[index] = value; }
        }

        // Token: 0x06000D3A RID: 3386 RVA: 0x0004BAA8 File Offset: 0x00049CA8
        public IEnumerator GetEnumerator()
        {
            return itemCollection.GetEnumerator();
        }

        // Token: 0x06000D37 RID: 3383 RVA: 0x0004BA5C File Offset: 0x00049C5C
        public void Add(Item item)
        {
            itemCollection.Add(item);
        }

        // Token: 0x06000D38 RID: 3384 RVA: 0x0004BA6C File Offset: 0x00049C6C
        public bool Remove(Item item)
        {
            return itemCollection.Remove(item);
        }

        // Token: 0x06000D39 RID: 3385 RVA: 0x0004BA7C File Offset: 0x00049C7C
        public void SortByPubDateIncreasing()
        {
            itemCollection.Sort((a, b) => a.PubDate.CompareTo(b.PubDate));
        }
    }
}