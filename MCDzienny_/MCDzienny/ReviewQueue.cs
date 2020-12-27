using System;
using System.Collections.Generic;
using System.Linq;

namespace MCDzienny
{
    // Token: 0x020001CD RID: 461
    public class ReviewQueue
    {
        // Token: 0x040006AD RID: 1709
        private List<Player> queue = new List<Player>();

        // Token: 0x040006AE RID: 1710
        private readonly object thisLock = new object();

        // Token: 0x170004ED RID: 1261
        // (get) Token: 0x06000CDB RID: 3291 RVA: 0x0004A164 File Offset: 0x00048364
        public int QueueLength
        {
            get
            {
                int count;
                lock (thisLock)
                {
                    count = queue.Count;
                }

                return count;
            }
        }

        // Token: 0x06000CDC RID: 3292 RVA: 0x0004A1A4 File Offset: 0x000483A4
        public void Enqueue(Player p)
        {
            lock (thisLock)
            {
                queue.Add(p);
            }
        }

        // Token: 0x06000CDD RID: 3293 RVA: 0x0004A1E4 File Offset: 0x000483E4
        public Player Dequeue()
        {
            Player result;
            lock (thisLock)
            {
                if (queue.Count == 0) throw new InvalidOperationException("ReviewQueue is empty.");
                var player = queue[0];
                queue.RemoveAt(0);
                result = player;
            }

            return result;
        }

        // Token: 0x06000CDE RID: 3294 RVA: 0x0004A24C File Offset: 0x0004844C
        public Player Peek()
        {
            Player result;
            lock (thisLock)
            {
                if (queue.Count == 0) throw new InvalidOperationException("ReviewQueue is empty.");
                result = queue[0];
            }

            return result;
        }

        // Token: 0x06000CDF RID: 3295 RVA: 0x0004A2A8 File Offset: 0x000484A8
        public void Remove(Player p)
        {
            lock (thisLock)
            {
                queue.Remove(p);
            }
        }

        // Token: 0x06000CE0 RID: 3296 RVA: 0x0004A2E8 File Offset: 0x000484E8
        public int QuequePosition(Player p)
        {
            int result;
            lock (thisLock)
            {
                result = queue.FindIndex(obj => obj == p);
            }

            return result;
        }

        // Token: 0x06000CE1 RID: 3297 RVA: 0x0004A348 File Offset: 0x00048548
        public bool Contains(Player p)
        {
            bool result;
            lock (thisLock)
            {
                result = queue.Contains(p);
            }

            return result;
        }

        // Token: 0x06000CE2 RID: 3298 RVA: 0x0004A38C File Offset: 0x0004858C
        public void Clear()
        {
            lock (thisLock)
            {
                queue.Clear();
            }
        }

        // Token: 0x06000CE3 RID: 3299 RVA: 0x0004A3CC File Offset: 0x000485CC
        public bool RemoveDisconnectedPlayers()
        {
            var result = false;
            lock (thisLock)
            {
                result = queue.Any(p => p.disconnected);
                queue = (from p in queue
                    where !p.disconnected
                    select p).ToList();
            }

            return result;
        }

        // Token: 0x06000CE4 RID: 3300 RVA: 0x0004A460 File Offset: 0x00048660
        public List<string> PlayersOnQueueByName()
        {
            RemoveDisconnectedPlayers();
            var list = new List<string>();
            lock (thisLock)
            {
                for (var i = 0; i < queue.Count; i++) list.Add(queue[i].name);
            }

            return list;
        }

        // Token: 0x06000CE5 RID: 3301 RVA: 0x0004A4D0 File Offset: 0x000486D0
        public List<Player> PlayersOnQueue()
        {
            var list = new List<Player>();
            lock (thisLock)
            {
                for (var i = 0; i < queue.Count; i++) list.Add(queue[i]);
            }

            return list;
        }
    }
}