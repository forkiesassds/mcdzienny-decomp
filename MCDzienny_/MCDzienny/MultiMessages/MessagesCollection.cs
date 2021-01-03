using System;

namespace MCDzienny.MultiMessages
{
    // Token: 0x020001D1 RID: 465
    public class MessagesCollection : KeyStringArrayCollection
    {
        // Token: 0x040006B9 RID: 1721
        private readonly Random rand = new Random();

        // Token: 0x170004F2 RID: 1266
        public string this[string key]
        {
            get
            {
                var array = base[key];
                var num = rand.Next(0, array.Length - 1);
                return array[num];
            }
        }
    }
}