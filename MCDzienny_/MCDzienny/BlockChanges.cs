using System.Collections.Generic;

namespace MCDzienny
{
    public class BlockChanges
    {

        readonly HashSet<BlockInfo> blockChanges;

        readonly Player player;

        public BlockChanges(Player p)
        {
            blockChanges = new HashSet<BlockInfo>(new PositionComparer());
            player = p;
        }

        public int Count { get { return blockChanges.Count; } }

        public bool Add(BlockInfo block)
        {
            byte tile = player.level.GetTile(block.X, block.Y, block.Z);
            if (Block.Convert(block.Type) == Block.Convert(tile))
            {
                return false;
            }
            if (!player.level.BlockchangeChecks(player, block.X, block.Y, block.Z, block.Type, tile))
            {
                return false;
            }
            return blockChanges.Add(block);
        }

        public bool Add(int x, int y, int z, byte type)
        {
            byte tile = player.level.GetTile(x, y, z);
            if (Block.Convert(type) == Block.Convert(tile))
            {
                return false;
            }
            if (!player.level.BlockchangeChecks(player, (ushort)x, (ushort)y, (ushort)z, type, tile))
            {
                return false;
            }
            return blockChanges.Add(new BlockInfo(x, y, z, type));
        }

        public bool Remove(BlockInfo block)
        {
            return blockChanges.Remove(block);
        }

        public void Commit()
        {
            if (blockChanges.Count == 0)
            {
                return;
            }
            foreach (BlockInfo blockChange in blockChanges)
            {
                player.level.BlockchangeAftercheck(player, blockChange.X, blockChange.Y, blockChange.Z, blockChange.Type,
                                                   player.level.GetTile(blockChange.X, blockChange.Y, blockChange.Z));
            }
            player.level.Blockchange(blockChanges);
            blockChanges.Clear();
        }

        public void Abort()
        {
            blockChanges.Clear();
        }

        internal class PositionComparer : IEqualityComparer<BlockInfo>
        {
            public bool Equals(BlockInfo x, BlockInfo y)
            {
                if (x == null || y == null)
                {
                    return false;
                }
                if (x.X == y.X && x.Y == y.Y)
                {
                    return x.Z == y.Z;
                }
                return false;
            }

            public int GetHashCode(BlockInfo obj)
            {
                return obj.X ^ obj.Y ^ obj.Z;
            }
        }
    }
}