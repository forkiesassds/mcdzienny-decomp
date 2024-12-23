using System;
using System.Collections.Generic;

namespace MCDzienny
{
    public abstract class Entity
    {

        public AABB bb;

        public float bbHeight = 1.8f;

        public float bbWidth = 0.6f;

        public BlockMap blockMap;

        public bool collision;

        public float distanceWalkedModified;

        public float distanceWalkedOnStepModified;

        public float fallDistance;

        public bool flyingMode;

        public float footSize;

        public float heightOffset;

        public bool horizontalCollision;

        public bool hovered;
        public Level level;

        public bool makeStepSound = true;

        int nextStep = 1;

        int nextStepDistance;

        public bool noPhysics;

        public bool onGround;

        public float prevDistanceWalkedModified;

        public float pushthrough;

        public bool removed;

        public bool slide = true;

        public int textureId;

        public float walkDist;

        public float walkDistO;

        public float x;

        public float xd;

        public float xo;

        public float xOld;

        public float xRot;

        public float xRotO;

        public float y;

        public float yd;

        public float yo;

        public float yOld;

        public float yRot;

        public float yRotO;

        public float ySlideOffset;

        public float z;

        public float zd;

        public float zo;

        public float zOld;

        public Entity(Level level)
        {
            this.level = level;
            setPos(0f, 0f, 0f);
        }

        public void awardKillScore(Entity killer, int score) {}

        protected virtual void causeFallDamage(float height) {}

        public float distanceTo(Entity other)
        {
            float num = x - other.x;
            float num2 = y - other.y;
            float num3 = z - other.z;
            return (float)Math.Sqrt(num * num + num2 * num2 + num3 * num3);
        }

        public float distanceTo(float x, float y, float z)
        {
            float num = this.x - x;
            float num2 = this.y - y;
            float num3 = this.z - z;
            return (float)Math.Sqrt(num * num + num2 * num2 + num3 * num3);
        }

        public float distanceToSq(Entity other)
        {
            float num = x - other.x;
            float num2 = y - other.y;
            float num3 = z - other.z;
            return num * num + num2 * num2 + num3 * num3;
        }

        public float distanceToSq(float x, float y, float z)
        {
            float num = this.x - x;
            float num2 = this.y - y;
            float num3 = this.z - z;
            return num * num + num2 * num2 + num3 * num3;
        }

        public float getBrightness(float var1)
        {
            int num = (int)x;
            int num2 = (int)(y + heightOffset / 2f - 0.5f);
            int num3 = (int)z;
            return level.getBrightness(num, num2, num3);
        }

        public int getTexture()
        {
            return textureId;
        }

        public virtual void hurt(Entity attacker, int damage) {}

        public void interpolateTurn(float yRot, float xRot)
        {
            this.yRot = (float)(this.yRot + yRot * 0.15);
            this.xRot = (float)(this.xRot - xRot * 0.15);
            if (this.xRot < -90f)
            {
                this.xRot = -90f;
            }
            if (this.xRot > 90f)
            {
                this.xRot = 90f;
            }
        }

        public bool intersects(float x0, float y0, float z0, float x1, float y1, float z1)
        {
            return bb.intersects(x0, y0, z0, x1, y1, z1);
        }

        public bool isCreativeModeAllowed()
        {
            return false;
        }

        public bool isFree(float x, float y, float z)
        {
            AABB aabb = bb.cloneMove(x, y, z);
            if (level.GetCubes(aabb).Count <= 0)
            {
                return !level.ContainsAnyLiquid(aabb);
            }
            return false;
        }

        public bool isFree(float x, float y, float z, float size)
        {
            AABB aabb = bb.grow(size, size, size).cloneMove(x, y, z);
            if (level.GetCubes(aabb).Count <= 0)
            {
                return !level.ContainsAnyLiquid(aabb);
            }
            return false;
        }

        public bool isInLava()
        {
            return level.containsLiquid(bb.grow(0f, -0.4f, 0f), 10);
        }

        public virtual bool isInOrOnRope()
        {
            return level.containsBlock(bb.grow(-0.5f, 0f, -0.5f), 51);
        }

        public virtual bool isInWater()
        {
            return level.containsLiquid(bb.grow(0f, -0.4f, 0f), 8);
        }

        public virtual bool isLit()
        {
            int num = (int)x;
            int num2 = (int)y;
            int num3 = (int)z;
            return level.IsLit(num, num2, num3);
        }

        public virtual bool isPickable()
        {
            return false;
        }

        public virtual bool isPushable()
        {
            return false;
        }

        public virtual bool isShootable()
        {
            return false;
        }

        public virtual bool isUnderWater()
        {
            byte tile = level.GetTile((int)x, (int)(y + 0.12f), (int)z);
            return Block.IsWater(tile);
        }

        public void move(float x, float y, float z)
        {
            if (noPhysics)
            {
                bb.move(x, y, z);
                this.x = (bb.x0 + bb.x1) / 2f;
                this.y = bb.y0 + heightOffset - ySlideOffset;
                this.z = (bb.z0 + bb.z1) / 2f;
            }
            else
            {
                float num = this.x;
                float num2 = this.z;
                float num3 = x;
                float num4 = y;
                float num5 = z;
                AABB aABB = bb.copy();
                List<AABB> cubes = level.GetCubes(bb.expand(x, y, z));
                for (int i = 0; i < cubes.Count; i++)
                {
                    y = cubes[i].clipYCollide(bb, y);
                }
                bb.move(0f, y, 0f);
                if (!slide && num4 != y)
                {
                    z = 0f;
                    y = 0f;
                    x = 0f;
                }
                bool flag = onGround || num4 != y && num4 < 0f;
                for (int j = 0; j < cubes.Count; j++)
                {
                    x = cubes[j].clipXCollide(bb, x);
                }
                bb.move(x, 0f, 0f);
                if (!slide && num3 != x)
                {
                    z = 0f;
                    y = 0f;
                    x = 0f;
                }
                for (int j = 0; j < cubes.Count; j++)
                {
                    z = cubes[j].clipZCollide(bb, z);
                }
                bb.move(0f, 0f, z);
                if (!slide && num5 != z)
                {
                    z = 0f;
                    y = 0f;
                    x = 0f;
                }
                float num6;
                float num7;
                if (footSize > 0f && flag && ySlideOffset < 0.05f && (num3 != x || num5 != z))
                {
                    num6 = x;
                    num7 = y;
                    float num8 = z;
                    x = num3;
                    y = footSize;
                    z = num5;
                    AABB aABB2 = bb.copy();
                    bb = aABB.copy();
                    cubes = level.GetCubes(bb.expand(num3, y, num5));
                    for (int k = 0; k < cubes.Count; k++)
                    {
                        y = cubes[k].clipYCollide(bb, y);
                    }
                    bb.move(0f, y, 0f);
                    if (!slide && num4 != y)
                    {
                        z = 0f;
                        y = 0f;
                        x = 0f;
                    }
                    for (int k = 0; k < cubes.Count; k++)
                    {
                        x = cubes[k].clipXCollide(bb, x);
                    }
                    bb.move(x, 0f, 0f);
                    if (!slide && num3 != x)
                    {
                        z = 0f;
                        y = 0f;
                        x = 0f;
                    }
                    for (int k = 0; k < cubes.Count; k++)
                    {
                        z = cubes[k].clipZCollide(bb, z);
                    }
                    bb.move(0f, 0f, z);
                    if (!slide && num5 != z)
                    {
                        z = 0f;
                        y = 0f;
                        x = 0f;
                    }
                    if (num6 * num6 + num8 * num8 >= x * x + z * z)
                    {
                        x = num6;
                        y = num7;
                        z = num8;
                        bb = aABB2.copy();
                    }
                    else
                    {
                        ySlideOffset = (float)(ySlideOffset + 0.5);
                    }
                }
                horizontalCollision = num3 != x || num5 != z;
                onGround = num4 != y && num4 < 0f;
                collision = horizontalCollision || num4 != y;
                if (onGround)
                {
                    if (fallDistance > 0f)
                    {
                        causeFallDamage(fallDistance / 2f);
                        fallDistance = 0f;
                    }
                }
                else if (y < 0f)
                {
                    fallDistance -= y;
                }
                if (num3 != x)
                {
                    xd = 0f;
                }
                if (num4 != y)
                {
                    yd = 0f;
                }
                if (num5 != z)
                {
                    zd = 0f;
                }
                this.x = (bb.x0 + bb.x1) / 2f;
                this.y = bb.y0 + heightOffset - ySlideOffset;
                this.z = (bb.z0 + bb.z1) / 2f;
                num6 = this.x - num;
                num7 = this.z - num2;
                walkDist = (float)(walkDist + Math.Sqrt(num6 * num6 + num7 * num7) * 0.6);
            }
            int num9 = (int)Math.Floor(this.x);
            int num10 = (int)Math.Floor(this.y - 0.20000000298023224 - heightOffset);
            int num11 = (int)Math.Floor(this.z);
            int tile = level.GetTile(num9, num10, num11);
            if (walkDist > nextStep && tile > 0)
            {
                nextStep++;
            }
            ySlideOffset *= 0.4f;
        }

        public void moveRelative(float x, float y, float z)
        {
            float num;
            if ((num = (float)Math.Sqrt(x * x + y * y)) >= 0.01f)
            {
                if (num < 1f)
                {
                    num = 1f;
                }
                num = z / num;
                x *= num;
                y *= num;
                z = (float)Math.Sin(yRot * (float)Math.PI / 180f);
                num = (float)Math.Cos(yRot * (float)Math.PI / 180f);
                xd += x * num - y * z;
                zd += y * num + x * z;
            }
        }

        public void moveTo(float x, float y, float z, float yRot, float xRot)
        {
            xo = this.x = x;
            yo = this.y = y;
            zo = this.z = z;
            this.yRot = yRot;
            this.xRot = xRot;
            setPos(x, y, z);
        }

        public void playerTouch(Entity other) {}

        public void playSound(string var1, float var2, float var3) {}

        public void push(Entity other)
        {
            float num = other.x - x;
            float num2 = other.z - z;
            float num3;
            if ((num3 = num * num + num2 * num2) >= 0.01f)
            {
                num3 = (float)Math.Sqrt(num3);
                num /= num3;
                num2 /= num3;
                num /= num3;
                num2 /= num3;
                num *= 0.05f;
                num2 *= 0.05f;
                num *= 1f - pushthrough;
                num2 *= 1f - pushthrough;
                push(0f - num, 0f, 0f - num2);
                other.push(num, 0f, num2);
            }
        }

        protected void push(float xd, float yd, float zd)
        {
            this.xd += xd;
            this.yd += yd;
            this.zd += zd;
        }

        public void remove()
        {
            removed = true;
        }

        public virtual void render(TextureManager manager, float var2) {}

        public virtual void renderHover(TextureManager manager, float var2) {}

        public void resetPos()
        {
            if (level != null)
            {
                float num = level.spawnx + 0.5f;
                float num2 = level.spawny;
                float num3 = level.spawnz;
                setPos(num, num2, num3);
                xd = 0f;
                yd = 0f;
                zd = 0f;
                yRot = level.SpawnRotY;
                xRot = 0f;
            }
        }

        public void setLevel(Level level)
        {
            this.level = level;
        }

        public void setPos(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            float num = bbWidth / 2f;
            float num2 = bbHeight / 2f;
            bb = new AABB(x - num, y - num2, z - num, x + num, y + num2, z + num);
        }

        public void setPos(Position pos)
        {
            if (pos.HasPosition)
            {
                setPos(pos.X, pos.Y, pos.Z);
            }
            else
            {
                setPos(x, y, z);
            }
            if (pos.HasRotation)
            {
                setRot(pos.Yaw, pos.Pitch);
            }
            else
            {
                setRot(yRot, xRot);
            }
        }

        protected void setRot(float yRot, float xRot)
        {
            this.yRot = yRot;
            this.xRot = xRot;
        }

        public void setSize(float bbWidth, float bbHeight)
        {
            this.bbWidth = bbWidth;
            this.bbHeight = bbHeight;
        }

        public bool shouldRender(Vector3F var1)
        {
            float num = x - var1.X;
            float num2 = y - var1.Y;
            float num3 = z - var1.Z;
            num3 = num * num + num2 * num2 + num3 * num3;
            return shouldRenderAtSqrDistance(num3);
        }

        public bool shouldRenderAtSqrDistance(float var1)
        {
            float num = bb.getSize() * 64f;
            return var1 < num * num;
        }

        public virtual void tick()
        {
            walkDistO = walkDist;
            xo = x;
            yo = y;
            zo = z;
            xRotO = xRot;
            yRotO = yRot;
        }

        public virtual void turn(float yRot, float xRot)
        {
            float num = this.xRot;
            float num2 = this.yRot;
            this.yRot = (float)(this.yRot + yRot * 0.15);
            this.xRot = (float)(this.xRot - xRot * 0.15);
            if (this.xRot < -90f)
            {
                this.xRot = -90f;
            }
            if (this.xRot > 90f)
            {
                this.xRot = 90f;
            }
            xRotO += this.xRot - num;
            yRotO += this.yRot - num2;
        }
    }
}