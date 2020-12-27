using System;
using System.Collections.Generic;
using System.Threading;

namespace MCDzienny
{
    // Token: 0x020001DC RID: 476
    public class PlayerCollection : List<Player>
    {
        // Token: 0x040006F9 RID: 1785
        public readonly ReaderWriterLockSlim Lock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

        // Token: 0x14000014 RID: 20
        // (add) Token: 0x06000D45 RID: 3397 RVA: 0x0004C24C File Offset: 0x0004A44C
        // (remove) Token: 0x06000D46 RID: 3398 RVA: 0x0004C284 File Offset: 0x0004A484
        public event EventHandler<PlayerEventArgs> PlayerAdded;

        // Token: 0x14000015 RID: 21
        // (add) Token: 0x06000D47 RID: 3399 RVA: 0x0004C2BC File Offset: 0x0004A4BC
        // (remove) Token: 0x06000D48 RID: 3400 RVA: 0x0004C2F4 File Offset: 0x0004A4F4
        public event EventHandler<PlayerEventArgs> PlayerRemoved;

        // Token: 0x06000D49 RID: 3401 RVA: 0x0004C32C File Offset: 0x0004A52C
        protected void OnPlayerAdded(PlayerEventArgs e)
        {
            if (PlayerAdded != null) PlayerAdded(this, e);
        }

        // Token: 0x06000D4A RID: 3402 RVA: 0x0004C344 File Offset: 0x0004A544
        protected void OnPlayerRemoved(PlayerEventArgs e)
        {
            if (PlayerRemoved != null) PlayerRemoved(this, e);
            new EventArgs();
        }

        // Token: 0x06000D4B RID: 3403 RVA: 0x0004C364 File Offset: 0x0004A564
        public new void Add(Player item)
        {
            Lock.EnterWriteLock();
            try
            {
                base.Add(item);
            }
            finally
            {
                Lock.ExitWriteLock();
            }

            OnPlayerAdded(new PlayerEventArgs(item));
        }

        // Token: 0x06000D4C RID: 3404 RVA: 0x0004C3B0 File Offset: 0x0004A5B0
        public new void Remove(Player item)
        {
            var flag = false;
            Lock.EnterWriteLock();
            try
            {
                flag = base.Remove(item);
            }
            finally
            {
                Lock.ExitWriteLock();
            }

            if (flag) OnPlayerRemoved(new PlayerEventArgs(item));
        }

        // Token: 0x06000D4D RID: 3405 RVA: 0x0004C400 File Offset: 0x0004A600
        public void ForEachSync(Action<Player> action)
        {
            Lock.EnterReadLock();
            try
            {
                foreach (var obj in this) action(obj);
            }
            finally
            {
                Lock.ExitReadLock();
            }
        }

        // Token: 0x06000D4E RID: 3406 RVA: 0x0004C474 File Offset: 0x0004A674
        private new void RemoveAt(int index)
        {
        }

        // Token: 0x06000D4F RID: 3407 RVA: 0x0004C478 File Offset: 0x0004A678
        private new void RemoveRange(int index, int count)
        {
        }

        // Token: 0x06000D50 RID: 3408 RVA: 0x0004C47C File Offset: 0x0004A67C
        private new void RemoveAll(Predicate<Player> match)
        {
        }

        // Token: 0x06000D51 RID: 3409 RVA: 0x0004C480 File Offset: 0x0004A680
        private new void AddRange(IEnumerable<Player> collection)
        {
        }

        // Token: 0x06000D52 RID: 3410 RVA: 0x0004C484 File Offset: 0x0004A684
        public List<Player> GetCopy()
        {
            Lock.EnterReadLock();
            List<Player> result;
            try
            {
                result = new List<Player>(this);
            }
            finally
            {
                Lock.ExitReadLock();
            }

            return result;
        }

        // Token: 0x06000D53 RID: 3411 RVA: 0x0004C4C4 File Offset: 0x0004A6C4
        private new void Clear()
        {
        }
    }
}