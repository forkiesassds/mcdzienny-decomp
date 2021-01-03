using System.Collections;

namespace MCDzienny
{
    // Token: 0x02000396 RID: 918
    public class Trader
    {
        // Token: 0x04000E6C RID: 3692
        public static Queue messages = Queue.Synchronized(new Queue());
    }
}