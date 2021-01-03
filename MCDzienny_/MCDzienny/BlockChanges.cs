using System.Collections.Generic;

namespace MCDzienny
{
    // Token: 0x02000165 RID: 357
    public class BlockChanges
    {
        // Token: 0x040004A2 RID: 1186
        private readonly HashSet<BlockInfo> blockChanges;

        // Token: 0x040004A3 RID: 1187
        private readonly Player player;

        // Token: 0x06000A11 RID: 2577 RVA: 0x000350B4 File Offset: 0x000332B4
        public BlockChanges(Player p)
        {
            blockChanges = new HashSet<BlockInfo>(new PositionComparer());
            player = p;
        }

        // Token: 0x1700047E RID: 1150
        // (get) Token: 0x06000A10 RID: 2576 RVA: 0x000350A4 File Offset: 0x000332A4
        public int Count
        {
            get { return blockChanges.Count; }
        }

        // Token: 0x06000A12 RID: 2578 RVA: 0x000350D4 File Offset: 0x000332D4
        public bool Add(BlockInfo block)
        {
            var tile = player.level.GetTile(block.X, block.Y, block.Z);
            return Block.Convert(block.Type) != Block.Convert(tile) &&
                   player.level.BlockchangeChecks(player, block.X, block.Y, block.Z, block.Type, tile) &&
                   blockChanges.Add(block);
        }

        // Token: 0x06000A13 RID: 2579 RVA: 0x00035158 File Offset: 0x00033358
        public bool Add(int x, int y, int z, byte type)
        {
            var tile = player.level.GetTile(x, y, z);
            return Block.Convert(type) != Block.Convert(tile) &&
                   player.level.BlockchangeChecks(player, (ushort) x, (ushort) y, (ushort) z, type, tile) &&
                   blockChanges.Add(new BlockInfo(x, y, z, type));
        }

        // Token: 0x06000A14 RID: 2580 RVA: 0x000351C4 File Offset: 0x000333C4
        public bool Remove(BlockInfo block)
        {
            return blockChanges.Remove(block);
        }

        // Token: 0x06000A15 RID: 2581 RVA: 0x000351D4 File Offset: 0x000333D4
        public void Commit()
        {
            if (blockChanges.Count == 0) return;
            foreach (var blockInfo in blockChanges)
                player.level.BlockchangeAftercheck(player, blockInfo.X, blockInfo.Y, blockInfo.Z, blockInfo.Type,
                    player.level.GetTile(blockInfo.X, blockInfo.Y, blockInfo.Z));
            player.level.Blockchange(blockChanges);
            blockChanges.Clear();
        }

        // Token: 0x06000A16 RID: 2582 RVA: 0x000352A0 File Offset: 0x000334A0
        public void Abort()
        {
            blockChanges.Clear();
        }

        // Token: 0x02000166 RID: 358
        internal class PositionComparer : IEqualityComparer<BlockInfo>
        {
            // Token: 0x06000A17 RID: 2583 RVA: 0x000352B0 File Offset: 0x000334B0
            public bool Equals(BlockInfo x, BlockInfo y)
            {
                return x != null && y != null && x.X == y.X && x.Y == y.Y && x.Z == y.Z;
            }

            // Token: 0x06000A18 RID: 2584 RVA: 0x000352E8 File Offset: 0x000334E8
            public int GetHashCode(BlockInfo obj)
            {
                return obj.X ^ obj.Y ^ obj.Z;
            }
        }
    }
}