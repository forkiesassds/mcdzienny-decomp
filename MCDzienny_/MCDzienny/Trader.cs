using System.Collections;

namespace MCDzienny
{
    public class Trader
    {
        public static Queue messages = Queue.Synchronized(new Queue());
    }
}