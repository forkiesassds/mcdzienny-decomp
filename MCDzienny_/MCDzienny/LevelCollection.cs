using System;
using System.Collections.Generic;

namespace MCDzienny
{
    // Token: 0x020001A8 RID: 424
    public class LevelCollection : List<Level>
    {
        // Token: 0x06000C38 RID: 3128 RVA: 0x00047518 File Offset: 0x00045718
        public LevelCollection()
        {
        }

        // Token: 0x06000C39 RID: 3129 RVA: 0x00047520 File Offset: 0x00045720
        public LevelCollection(int capacity) : base(capacity)
        {
        }

        // Token: 0x06000C3A RID: 3130 RVA: 0x0004752C File Offset: 0x0004572C
        public LevelCollection(IEnumerable<Level> collection) : base(collection)
        {
        }

        // Token: 0x14000012 RID: 18
        // (add) Token: 0x06000C34 RID: 3124 RVA: 0x00047438 File Offset: 0x00045638
        // (remove) Token: 0x06000C35 RID: 3125 RVA: 0x00047470 File Offset: 0x00045670
        public event EventHandler<LevelEventArgs> LevelAdded;

        // Token: 0x14000013 RID: 19
        // (add) Token: 0x06000C36 RID: 3126 RVA: 0x000474A8 File Offset: 0x000456A8
        // (remove) Token: 0x06000C37 RID: 3127 RVA: 0x000474E0 File Offset: 0x000456E0
        public event EventHandler<LevelEventArgs> LevelRemoved;

        // Token: 0x06000C3B RID: 3131 RVA: 0x00047538 File Offset: 0x00045738
        protected void OnLevelAdded(LevelEventArgs e)
        {
            if (LevelAdded != null) LevelAdded(this, e);
        }

        // Token: 0x06000C3C RID: 3132 RVA: 0x00047550 File Offset: 0x00045750
        protected void OnLevelRemoved(LevelEventArgs e)
        {
            if (LevelRemoved != null) LevelRemoved(this, e);
            new EventArgs();
        }

        // Token: 0x06000C3D RID: 3133 RVA: 0x00047570 File Offset: 0x00045770
        public new void Add(Level item)
        {
            base.Add(item);
            OnLevelAdded(new LevelEventArgs(item));
        }

        // Token: 0x06000C3E RID: 3134 RVA: 0x00047588 File Offset: 0x00045788
        public new void Remove(Level item)
        {
            var flag = base.Remove(item);
            if (flag) OnLevelRemoved(new LevelEventArgs(item));
        }

        // Token: 0x06000C3F RID: 3135 RVA: 0x000475AC File Offset: 0x000457AC
        private new void RemoveAt(int index)
        {
        }

        // Token: 0x06000C40 RID: 3136 RVA: 0x000475B0 File Offset: 0x000457B0
        private new void RemoveRange(int index, int count)
        {
        }

        // Token: 0x06000C41 RID: 3137 RVA: 0x000475B4 File Offset: 0x000457B4
        private new void RemoveAll(Predicate<Level> match)
        {
        }

        // Token: 0x06000C42 RID: 3138 RVA: 0x000475B8 File Offset: 0x000457B8
        private new void AddRange(IEnumerable<Level> collection)
        {
        }

        // Token: 0x06000C43 RID: 3139 RVA: 0x000475BC File Offset: 0x000457BC
        private new void Clear()
        {
        }
    }
}