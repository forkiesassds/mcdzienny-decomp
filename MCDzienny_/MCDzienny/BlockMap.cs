using System.Collections.Generic;

namespace MCDzienny
{
    // Token: 0x0200000A RID: 10
    public class BlockMap
    {
        // Token: 0x0400001F RID: 31
        public List<Entity> all = new List<Entity>();

        // Token: 0x0400001B RID: 27
        private readonly int depth;

        // Token: 0x0400001E RID: 30
        public List<Entity>[] entityGrid;

        // Token: 0x0400001A RID: 26
        private readonly int height;

        // Token: 0x0400001C RID: 28
        private readonly Slot slot;

        // Token: 0x0400001D RID: 29
        private readonly Slot slot2;

        // Token: 0x04000019 RID: 25
        private readonly int width;

        // Token: 0x06000022 RID: 34 RVA: 0x00002A58 File Offset: 0x00000C58
        public BlockMap(int width, int height, int depth)
        {
            slot = new Slot(this);
            slot2 = new Slot(this);
            this.width = width / 16;
            this.height = height / 16;
            this.depth = depth / 16;
            if (this.width == 0) this.width = 1;
            if (this.height == 0) this.height = 1;
            if (this.depth == 0) this.depth = 1;
            entityGrid = new List<Entity>[this.width * this.height * this.depth];
            for (width = 0; width < this.width; width++)
            for (height = 0; height < this.height; height++)
            for (depth = 0; depth < this.depth; depth++)
                entityGrid[(depth * this.height + height) * this.width + width] = new List<Entity>();
        }

        // Token: 0x06000023 RID: 35 RVA: 0x00002B50 File Offset: 0x00000D50
        public void insert(Entity entity)
        {
            all.Add(entity);
            slot.init(entity.x, entity.y, entity.z).add(entity);
            entity.xOld = entity.x;
            entity.yOld = entity.y;
            entity.zOld = entity.z;
            entity.blockMap = this;
        }

        // Token: 0x06000024 RID: 36 RVA: 0x00002BB8 File Offset: 0x00000DB8
        public void remove(Entity entity)
        {
            slot.init(entity.xOld, entity.yOld, entity.zOld).remove(entity);
            all.Remove(entity);
        }

        // Token: 0x06000025 RID: 37 RVA: 0x00002BEC File Offset: 0x00000DEC
        public void moved(Entity entity)
        {
            var slot = this.slot.init(entity.xOld, entity.yOld, entity.zOld);
            var slot2 = this.slot2.init(entity.x, entity.y, entity.z);
            if (!slot.Equals(slot2))
            {
                slot.remove(entity);
                slot2.add(entity);
                entity.xOld = entity.x;
                entity.yOld = entity.y;
                entity.zOld = entity.z;
            }
        }

        // Token: 0x06000026 RID: 38 RVA: 0x00002C70 File Offset: 0x00000E70
        public List<Entity> getEntities(Entity except, float x0, float y0, float z0, float x1, float y1, float z1)
        {
            return getEntities(except, x0, y0, z0, x1, y1, z1, new List<Entity>());
        }

        // Token: 0x06000027 RID: 39 RVA: 0x00002C94 File Offset: 0x00000E94
        public List<Entity> getEntities(Entity except, float x0, float y0, float z0, float x1, float y1, float z1,
            List<Entity> foundEntities)
        {
            var slot = this.slot.init(x0, y0, z0);
            var slot2 = this.slot2.init(x1, y1, z1);
            for (var i = slot.getXSlot() - 1; i <= slot2.getXSlot() + 1; i++)
            for (var j = slot.getYSlot() - 1; j <= slot2.getYSlot() + 1; j++)
            for (var k = slot.getZSlot() - 1; k <= slot2.getZSlot() + 1; k++)
                if (i >= 0 && j >= 0 && k >= 0 && i < width && j < height && k < depth)
                {
                    var list = entityGrid[(k * height + j) * width + i];
                    foreach (var entity in list)
                        if (entity != except && entity.intersects(x0, y0, z0, x1, y1, z1))
                            foundEntities.Add(entity);
                }

            return foundEntities;
        }

        // Token: 0x06000028 RID: 40 RVA: 0x00002DD8 File Offset: 0x00000FD8
        public void removeAllNonCreativeModeEntities()
        {
            for (var i = 0; i < width; i++)
            for (var j = 0; j < height; j++)
            for (var k = 0; k < depth; k++)
            {
                var list = entityGrid[(k * height + j) * width + i];
                for (var l = 0; l < list.Count; l++)
                    if (!list[l].isCreativeModeAllowed())
                        list.RemoveAt(l--);
            }
        }

        // Token: 0x06000029 RID: 41 RVA: 0x00002E64 File Offset: 0x00001064
        public void clear()
        {
            for (var i = 0; i < width; i++)
            for (var j = 0; j < height; j++)
            for (var k = 0; k < depth; k++)
                entityGrid[(k * height + j) * width + i].Clear();
        }

        // Token: 0x0600002A RID: 42 RVA: 0x00002EC4 File Offset: 0x000010C4
        public List<Entity> getEntities(Entity except, AABB aabb)
        {
            return getEntities(except, aabb.x0, aabb.y0, aabb.z0, aabb.x1, aabb.y1, aabb.z1, new List<Entity>());
        }

        // Token: 0x0600002B RID: 43 RVA: 0x00002F04 File Offset: 0x00001104
        public List<Entity> getEntities(Entity except, AABB aabb, List<Entity> entities)
        {
            return getEntities(except, aabb.x0, aabb.y0, aabb.z0, aabb.x1, aabb.y1, aabb.z1, entities);
        }

        // Token: 0x0600002C RID: 44 RVA: 0x00002F40 File Offset: 0x00001140
        public void tickAll()
        {
            for (var i = 0; i < all.Count; i++)
            {
                var entity = all[i];
                entity.tick();
                if (entity.removed)
                {
                    all.RemoveAt(i--);
                    slot.init(entity.xOld, entity.yOld, entity.zOld).remove(entity);
                }
                else
                {
                    var num = (int) (entity.xOld / 16f);
                    var num2 = (int) (entity.yOld / 16f);
                    var num3 = (int) (entity.zOld / 16f);
                    var num4 = (int) (entity.x / 16f);
                    var num5 = (int) (entity.y / 16f);
                    var num6 = (int) (entity.z / 16f);
                    if (num != num4 || num2 != num5 || num3 != num6) moved(entity);
                }
            }
        }

        // Token: 0x0600002D RID: 45 RVA: 0x00003028 File Offset: 0x00001228
        private static int getWidth(BlockMap map)
        {
            return map.width;
        }

        // Token: 0x0600002E RID: 46 RVA: 0x00003030 File Offset: 0x00001230
        private static int getDepth(BlockMap map)
        {
            return map.height;
        }

        // Token: 0x0600002F RID: 47 RVA: 0x00003038 File Offset: 0x00001238
        private static int getHeight(BlockMap map)
        {
            return map.depth;
        }

        // Token: 0x0200000B RID: 11
        private class Slot
        {
            // Token: 0x04000023 RID: 35
            private readonly BlockMap blockMap;

            // Token: 0x04000020 RID: 32
            private int xSlot;

            // Token: 0x04000021 RID: 33
            private int ySlot;

            // Token: 0x04000022 RID: 34
            private int zSlot;

            // Token: 0x06000030 RID: 48 RVA: 0x00003040 File Offset: 0x00001240
            public Slot(BlockMap map)
            {
                blockMap = map;
            }

            // Token: 0x06000031 RID: 49 RVA: 0x00003050 File Offset: 0x00001250
            public Slot init(float x, float y, float z)
            {
                xSlot = (int) (x / 16f);
                ySlot = (int) (y / 16f);
                zSlot = (int) (z / 16f);
                if (xSlot < 0) xSlot = 0;
                if (ySlot < 0) ySlot = 0;
                if (zSlot < 0) zSlot = 0;
                if (xSlot >= getWidth(blockMap)) xSlot = getWidth(blockMap) - 1;
                if (ySlot >= getDepth(blockMap)) ySlot = getDepth(blockMap) - 1;
                if (zSlot >= getHeight(blockMap)) zSlot = getHeight(blockMap) - 1;
                return this;
            }

            // Token: 0x06000032 RID: 50 RVA: 0x0000312C File Offset: 0x0000132C
            public void add(Entity entity)
            {
                if (xSlot >= 0 && ySlot >= 0 && zSlot >= 0)
                    blockMap.entityGrid[(zSlot * getDepth(blockMap) + ySlot) * getWidth(blockMap) + xSlot].Add(entity);
            }

            // Token: 0x06000033 RID: 51 RVA: 0x00003194 File Offset: 0x00001394
            public void remove(Entity entity)
            {
                if (xSlot >= 0 && ySlot >= 0 && zSlot >= 0)
                    blockMap.entityGrid[(zSlot * getDepth(blockMap) + ySlot) * getWidth(blockMap) + xSlot]
                        .Remove(entity);
            }

            // Token: 0x06000034 RID: 52 RVA: 0x000031FC File Offset: 0x000013FC
            public override bool Equals(object other)
            {
                var slot = other as Slot;
                return slot != null && xSlot == slot.xSlot && ySlot == slot.ySlot && zSlot == slot.zSlot;
            }

            // Token: 0x06000035 RID: 53 RVA: 0x00003244 File Offset: 0x00001444
            public int getXSlot()
            {
                return xSlot;
            }

            // Token: 0x06000036 RID: 54 RVA: 0x0000324C File Offset: 0x0000144C
            public int getYSlot()
            {
                return ySlot;
            }

            // Token: 0x06000037 RID: 55 RVA: 0x00003254 File Offset: 0x00001454
            public int getZSlot()
            {
                return zSlot;
            }
        }
    }
}