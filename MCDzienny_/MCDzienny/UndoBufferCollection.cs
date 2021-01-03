using System.Collections.Generic;

namespace MCDzienny
{
    // Token: 0x020001DF RID: 479
    public class UndoBufferCollection : List<Player.UndoPos>
    {
        // Token: 0x04000702 RID: 1794
        private const int ItemsCountLimit = 219525;

        // Token: 0x04000703 RID: 1795
        private readonly Player player;

        // Token: 0x06000D5B RID: 3419 RVA: 0x0004C558 File Offset: 0x0004A758
        public UndoBufferCollection(Player player)
        {
            this.player = player;
        }

        // Token: 0x06000D5C RID: 3420 RVA: 0x0004C568 File Offset: 0x0004A768
        public new void Add(Player.UndoPos item)
        {
            base.Add(item);
            if (Count > 219525) player.SaveUndoToNewFile();
        }
    }
}