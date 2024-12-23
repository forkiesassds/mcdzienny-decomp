using System;
using System.Collections.Generic;
using System.Threading;

namespace MCDzienny
{
    public class PlayerCollection : List<Player>
    {
        public readonly ReaderWriterLockSlim Lock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

        public event EventHandler<PlayerEventArgs> PlayerAdded;

        public event EventHandler<PlayerEventArgs> PlayerRemoved;

        protected void OnPlayerAdded(PlayerEventArgs e)
        {
            if (PlayerAdded != null)
            {
                PlayerAdded(this, e);
            }
        }

        protected void OnPlayerRemoved(PlayerEventArgs e)
        {
            if (PlayerRemoved != null)
            {
                PlayerRemoved(this, e);
            }
            new EventArgs();
        }

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

        public new void Remove(Player item)
        {
            bool flag = false;
            Lock.EnterWriteLock();
            try
            {
                flag = base.Remove(item);
            }
            finally
            {
                Lock.ExitWriteLock();
            }
            if (flag)
            {
                OnPlayerRemoved(new PlayerEventArgs(item));
            }
        }

        public void ForEachSync(Action<Player> action)
        {
            Lock.EnterReadLock();
            try
            {
                using (Enumerator enumerator = GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        Player current = enumerator.Current;
                        action(current);
                    }
                }
            }
            finally
            {
                Lock.ExitReadLock();
            }
        }

        new void RemoveAt(int index) {}

        new void RemoveRange(int index, int count) {}

        new void RemoveAll(Predicate<Player> match) {}

        new void AddRange(IEnumerable<Player> collection) {}

        public List<Player> GetCopy()
        {
            Lock.EnterReadLock();
            try
            {
                return new List<Player>(this);
            }
            finally
            {
                Lock.ExitReadLock();
            }
        }

        new void Clear() {}
    }
}