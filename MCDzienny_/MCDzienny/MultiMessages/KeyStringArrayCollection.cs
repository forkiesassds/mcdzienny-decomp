using System.Collections.Generic;

namespace MCDzienny.MultiMessages
{
    public class KeyStringArrayCollection : Dictionary<string, string[]>
    {
        public new string[] this[string key]
        {
            get
            {
                string[] value;
                TryGetValue(key, out value);
                return value;
            }
        }
    }
}