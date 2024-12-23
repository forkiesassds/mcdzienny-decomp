using System.Collections.Generic;

namespace MCDzienny
{
    public class BlockMap
    {

        readonly int depth;

        readonly int height;

        readonly Slot slot;

        readonly Slot slot2;

        readonly int width;

        public List<Entity> all = new List<Entity>();

        public List<Entity>[] entityGrid;

        public BlockMap(int width, int height, int depth)
        {
            slot = new Slot(this);
            slot2 = new Slot(this);
            this.width = width / 16;
            this.height = height / 16;
            this.depth = depth / 16;
            if (this.width == 0)
            {
                this.width = 1;
            }
            if (this.height == 0)
            {
                this.height = 1;
            }
            if (this.depth == 0)
            {
                this.depth = 1;
            }
            entityGrid = new List<Entity>[this.width * this.height * this.depth];
            for (width = 0; width < this.width; width++)
            {
                for (height = 0; height < this.height; height++)
                {
                    for (depth = 0; depth < this.depth; depth++)
                    {
                        entityGrid[(depth * this.height + height) * this.width + width] = new List<Entity>();
                    }
                }
            }
        }

        public void insert(Entity entity)
        {
            all.Add(entity);
            slot.init(entity.x, entity.y, entity.z).add(entity);
            entity.xOld = entity.x;
            entity.yOld = entity.y;
            entity.zOld = entity.z;
            entity.blockMap = this;
        }

        public void remove(Entity entity)
        {
            slot.init(entity.xOld, entity.yOld, entity.zOld).remove(entity);
            all.Remove(entity);
        }

        public void moved(Entity entity)
        {
            Slot slot = this.slot.init(entity.xOld, entity.yOld, entity.zOld);
            Slot slot2 = this.slot2.init(entity.x, entity.y, entity.z);
            if (!slot.Equals(slot2))
            {
                slot.remove(entity);
                slot2.add(entity);
                entity.xOld = entity.x;
                entity.yOld = entity.y;
                entity.zOld = entity.z;
            }
        }

        public List<Entity> getEntities(Entity except, float x0, float y0, float z0, float x1, float y1, float z1)
        {
            return getEntities(except, x0, y0, z0, x1, y1, z1, new List<Entity>());
        }

        public List<Entity> getEntities(Entity except, float x0, float y0, float z0, float x1, float y1, float z1, List<Entity> foundEntities)
        {
            Slot slot = this.slot.init(x0, y0, z0);
            Slot slot2 = this.slot2.init(x1, y1, z1);
            for (int i = slot.getXSlot() - 1; i <= slot2.getXSlot() + 1; i++)
            {
                for (int j = slot.getYSlot() - 1; j <= slot2.getYSlot() + 1; j++)
                {
                    for (int k = slot.getZSlot() - 1; k <= slot2.getZSlot() + 1; k++)
                    {
                        if (i < 0 || j < 0 || k < 0 || i >= width || j >= height || k >= depth)
                        {
                            continue;
                        }
                        List<Entity> list = entityGrid[(k * height + j) * width + i];
                        foreach (Entity item in list)
                        {
                            if (item != except && item.intersects(x0, y0, z0, x1, y1, z1))
                            {
                                foundEntities.Add(item);
                            }
                        }
                    }
                }
            }
            return foundEntities;
        }

        public void removeAllNonCreativeModeEntities()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    for (int k = 0; k < depth; k++)
                    {
                        List<Entity> list = entityGrid[(k * height + j) * width + i];
                        for (int l = 0; l < list.Count; l++)
                        {
                            if (!list[l].isCreativeModeAllowed())
                            {
                                list.RemoveAt(l--);
                            }
                        }
                    }
                }
            }
        }

        public void clear()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    for (int k = 0; k < depth; k++)
                    {
                        entityGrid[(k * height + j) * width + i].Clear();
                    }
                }
            }
        }

        public List<Entity> getEntities(Entity except, AABB aabb)
        {
            return getEntities(except, aabb.x0, aabb.y0, aabb.z0, aabb.x1, aabb.y1, aabb.z1, new List<Entity>());
        }

        public List<Entity> getEntities(Entity except, AABB aabb, List<Entity> entities)
        {
            return getEntities(except, aabb.x0, aabb.y0, aabb.z0, aabb.x1, aabb.y1, aabb.z1, entities);
        }

        public void tickAll()
        {
            for (int i = 0; i < all.Count; i++)
            {
                Entity entity = all[i];
                entity.tick();
                if (entity.removed)
                {
                    all.RemoveAt(i--);
                    slot.init(entity.xOld, entity.yOld, entity.zOld).remove(entity);
                    continue;
                }
                int num = (int)(entity.xOld / 16f);
                int num2 = (int)(entity.yOld / 16f);
                int num3 = (int)(entity.zOld / 16f);
                int num4 = (int)(entity.x / 16f);
                int num5 = (int)(entity.y / 16f);
                int num6 = (int)(entity.z / 16f);
                if (num != num4 || num2 != num5 || num3 != num6)
                {
                    moved(entity);
                }
            }
        }

        static int getWidth(BlockMap map)
        {
            return map.width;
        }

        static int getDepth(BlockMap map)
        {
            return map.height;
        }

        static int getHeight(BlockMap map)
        {
            return map.depth;
        }

        class Slot
        {

            readonly BlockMap blockMap;
            int xSlot;

            int ySlot;

            int zSlot;

            public Slot(BlockMap map)
            {
                blockMap = map;
            }

            public Slot init(float x, float y, float z)
            {
                xSlot = (int)(x / 16f);
                ySlot = (int)(y / 16f);
                zSlot = (int)(z / 16f);
                if (xSlot < 0)
                {
                    xSlot = 0;
                }
                if (ySlot < 0)
                {
                    ySlot = 0;
                }
                if (zSlot < 0)
                {
                    zSlot = 0;
                }
                if (xSlot >= getWidth(blockMap))
                {
                    xSlot = getWidth(blockMap) - 1;
                }
                if (ySlot >= getDepth(blockMap))
                {
                    ySlot = getDepth(blockMap) - 1;
                }
                if (zSlot >= getHeight(blockMap))
                {
                    zSlot = getHeight(blockMap) - 1;
                }
                return this;
            }

            public void add(Entity entity)
            {
                if (xSlot >= 0 && ySlot >= 0 && zSlot >= 0)
                {
                    blockMap.entityGrid[(zSlot * getDepth(blockMap) + ySlot) * getWidth(blockMap) + xSlot].Add(entity);
                }
            }

            public void remove(Entity entity)
            {
                if (xSlot >= 0 && ySlot >= 0 && zSlot >= 0)
                {
                    blockMap.entityGrid[(zSlot * getDepth(blockMap) + ySlot) * getWidth(blockMap) + xSlot].Remove(entity);
                }
            }

            public override bool Equals(object other)
            {
                Slot slot1 = other as Slot;
                if (slot1 == null)
                {
                    return false;
                }
                if (xSlot == slot1.xSlot && ySlot == slot1.ySlot)
                {
                    return zSlot == slot1.zSlot;
                }
                return false;
            }

            public int getXSlot()
            {
                return xSlot;
            }

            public int getYSlot()
            {
                return ySlot;
            }

            public int getZSlot()
            {
                return zSlot;
            }
        }
    }
}