using System;
using System.Collections.Generic;

namespace MCDzienny
{
    public class LevelCollection : List<Level>
    {

        public LevelCollection() {}

        public LevelCollection(int capacity)
            : base(capacity) {}

        public LevelCollection(IEnumerable<Level> collection)
            : base(collection) {}
        public event EventHandler<LevelEventArgs> LevelAdded;

        public event EventHandler<LevelEventArgs> LevelRemoved;

        protected void OnLevelAdded(LevelEventArgs e)
        {
            if (LevelAdded != null)
            {
                LevelAdded(this, e);
            }
        }

        protected void OnLevelRemoved(LevelEventArgs e)
        {
            if (LevelRemoved != null)
            {
                LevelRemoved(this, e);
            }
            new EventArgs();
        }

        public new void Add(Level item)
        {
            base.Add(item);
            OnLevelAdded(new LevelEventArgs(item));
        }

        public new void Remove(Level item)
        {
            if (base.Remove(item))
            {
                OnLevelRemoved(new LevelEventArgs(item));
            }
        }

        new void RemoveAt(int index) {}

        new void RemoveRange(int index, int count) {}

        new void RemoveAll(Predicate<Level> match) {}

        new void AddRange(IEnumerable<Level> collection) {}

        new void Clear() {}
    }
}