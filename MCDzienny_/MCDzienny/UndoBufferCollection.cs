using System.Collections.Generic;

namespace MCDzienny
{
    public class UndoBufferCollection : List<Player.UndoPos>
    {
        const int ItemsCountLimit = 219525;

        readonly Player player;

        public UndoBufferCollection(Player player)
        {
            this.player = player;
        }

        public new void Add(Player.UndoPos item)
        {
            base.Add(item);
            if (Count > 219525)
            {
                player.SaveUndoToNewFile();
            }
        }
    }
}