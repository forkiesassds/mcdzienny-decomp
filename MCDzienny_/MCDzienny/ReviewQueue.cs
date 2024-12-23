using System;
using System.Collections.Generic;
using System.Linq;

namespace MCDzienny
{
    public class ReviewQueue
    {

        readonly object thisLock = new object();
        List<Player> queue = new List<Player>();

        public int QueueLength
        {
            get
            {
                lock (thisLock)
                {
                    return queue.Count;
                }
            }
        }

        public void Enqueue(Player p)
        {
            lock (thisLock)
            {
                queue.Add(p);
            }
        }

        public Player Dequeue()
        {
            lock (thisLock)
            {
                if (queue.Count == 0)
                {
                    throw new InvalidOperationException("ReviewQueue is empty.");
                }
                Player result = queue[0];
                queue.RemoveAt(0);
                return result;
            }
        }

        public Player Peek()
        {
            lock (thisLock)
            {
                if (queue.Count == 0)
                {
                    throw new InvalidOperationException("ReviewQueue is empty.");
                }
                return queue[0];
            }
        }

        public void Remove(Player p)
        {
            lock (thisLock)
            {
                queue.Remove(p);
            }
        }

        public int QuequePosition(Player p)
        {
            lock (thisLock)
            {
                return queue.FindIndex(obj => obj == p);
            }
        }

        public bool Contains(Player p)
        {
            lock (thisLock)
            {
                return queue.Contains(p);
            }
        }

        public void Clear()
        {
            lock (thisLock)
            {
                queue.Clear();
            }
        }

        public bool RemoveDisconnectedPlayers()
        {
            bool flag = false;
            lock (thisLock)
            {
                flag = queue.Any(p => p.disconnected);
                queue = queue.Where(p => !p.disconnected).ToList();
                return flag;
            }
        }

        public List<string> PlayersOnQueueByName()
        {
            RemoveDisconnectedPlayers();
            var list = new List<string>();
            lock (thisLock)
            {
                for (int i = 0; i < queue.Count; i++)
                {
                    list.Add(queue[i].name);
                }
                return list;
            }
        }

        public List<Player> PlayersOnQueue()
        {
            var list = new List<Player>();
            lock (thisLock)
            {
                for (int i = 0; i < queue.Count; i++)
                {
                    list.Add(queue[i]);
                }
                return list;
            }
        }
    }
}