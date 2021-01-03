using System.Collections.Generic;

namespace MCDzienny.MultiMessages
{
    // Token: 0x020001D0 RID: 464
    public class KeyStringArrayCollection : Dictionary<string, string[]>
    {
        // Token: 0x170004F1 RID: 1265
        public new string[] this[string key]
        {
            get
            {
                string[] result;
                TryGetValue(key, out result);
                return result;
            }
        }
    }
}