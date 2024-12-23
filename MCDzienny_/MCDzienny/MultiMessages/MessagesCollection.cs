using System;

namespace MCDzienny.MultiMessages
{
    public class MessagesCollection : KeyStringArrayCollection
    {
        readonly Random rand = new Random();

        public new string this[string key]
        {
            get
            {
                string[] array = base[key];
                int num = rand.Next(0, array.Length - 1);
                return array[num];
            }
        }
    }
}