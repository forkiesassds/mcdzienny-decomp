using System;

namespace MCDzienny
{
    // Token: 0x0200004D RID: 77
    public abstract class Entity
    {
        // Token: 0x04000115 RID: 277
        public AABB bb;

        // Token: 0x0400011D RID: 285
        public float bbHeight = 1.8f;

        // Token: 0x0400011C RID: 284
        public float bbWidth = 0.6f;

        // Token: 0x04000123 RID: 291
        public BlockMap blockMap;

        // Token: 0x04000118 RID: 280
        public bool collision;

        // Token: 0x04000130 RID: 304
        public float distanceWalkedModified;

        // Token: 0x04000131 RID: 305
        public float distanceWalkedOnStepModified;

        // Token: 0x04000121 RID: 289
        public float fallDistance;

        // Token: 0x0400012D RID: 301
        public bool flyingMode;

        // Token: 0x04000129 RID: 297
        public float footSize;

        // Token: 0x0400011B RID: 283
        public float heightOffset;

        // Token: 0x04000117 RID: 279
        public bool horizontalCollision;

        // Token: 0x0400012C RID: 300
        public bool hovered;

        // Token: 0x04000107 RID: 263
        public Level level;

        // Token: 0x04000120 RID: 288
        public bool makeStepSound = true;

        // Token: 0x04000122 RID: 290
        private int nextStep = 1;

        // Token: 0x0400012A RID: 298
        public bool noPhysics;

        // Token: 0x04000116 RID: 278
        public bool onGround;

        // Token: 0x0400012F RID: 303
        public float prevDistanceWalkedModified;

        // Token: 0x0400012B RID: 299
        public float pushthrough;

        // Token: 0x0400011A RID: 282
        public bool removed;

        // Token: 0x04000119 RID: 281
        public bool slide = true;

        // Token: 0x04000127 RID: 295
        public int textureId;

        // Token: 0x0400011F RID: 287
        public float walkDist;

        // Token: 0x0400011E RID: 286
        public float walkDistO;

        // Token: 0x0400010B RID: 267
        public float x;

        // Token: 0x0400010E RID: 270
        public float xd;

        // Token: 0x04000108 RID: 264
        public float xo;

        // Token: 0x04000124 RID: 292
        public float xOld;

        // Token: 0x04000112 RID: 274
        public float xRot;

        // Token: 0x04000114 RID: 276
        public float xRotO;

        // Token: 0x0400010C RID: 268
        public float y;

        // Token: 0x0400010F RID: 271
        public float yd;

        // Token: 0x04000109 RID: 265
        public float yo;

        // Token: 0x04000125 RID: 293
        public float yOld;

        // Token: 0x04000111 RID: 273
        public float yRot;

        // Token: 0x04000113 RID: 275
        public float yRotO;

        // Token: 0x04000128 RID: 296
        public float ySlideOffset;

        // Token: 0x0400010D RID: 269
        public float z;

        // Token: 0x04000110 RID: 272
        public float zd;

        // Token: 0x0400010A RID: 266
        public float zo;

        // Token: 0x04000126 RID: 294
        public float zOld;

        // Token: 0x060001BA RID: 442 RVA: 0x00009F94 File Offset: 0x00008194
        public Entity(Level level)
        {
            this.level = level;
            setPos(0f, 0f, 0f);
        }

        public Entity()
        {
        }

        // Token: 0x060001BB RID: 443 RVA: 0x00009FF0 File Offset: 0x000081F0
        public void awardKillScore(Entity killer, int score)
        {
        }

        // Token: 0x060001BC RID: 444 RVA: 0x00009FF4 File Offset: 0x000081F4
        protected virtual void causeFallDamage(float height)
        {
        }

        // Token: 0x060001BD RID: 445 RVA: 0x00009FF8 File Offset: 0x000081F8
        public float distanceTo(Entity other)
        {
            var num = x - other.x;
            var num2 = y - other.y;
            var num3 = z - other.z;
            return (float) Math.Sqrt(num * num + num2 * num2 + num3 * num3);
        }

        // Token: 0x060001BE RID: 446 RVA: 0x0000A044 File Offset: 0x00008244
        public float distanceTo(float x, float y, float z)
        {
            var num = this.x - x;
            var num2 = this.y - y;
            var num3 = this.z - z;
            return (float) Math.Sqrt(num * num + num2 * num2 + num3 * num3);
        }

        // Token: 0x060001BF RID: 447 RVA: 0x0000A080 File Offset: 0x00008280
        public float distanceToSq(Entity other)
        {
            var num = x - other.x;
            var num2 = y - other.y;
            var num3 = z - other.z;
            return num * num + num2 * num2 + num3 * num3;
        }

        // Token: 0x060001C0 RID: 448 RVA: 0x0000A0C4 File Offset: 0x000082C4
        public float distanceToSq(float x, float y, float z)
        {
            var num = this.x - x;
            var num2 = this.y - y;
            var num3 = this.z - z;
            return num * num + num2 * num2 + num3 * num3;
        }

        // Token: 0x060001C1 RID: 449 RVA: 0x0000A0F8 File Offset: 0x000082F8
        public float getBrightness(float var1)
        {
            var num = (int) x;
            var num2 = (int) (y + heightOffset / 2f - 0.5f);
            var num3 = (int) z;
            return level.getBrightness(num, num2, num3);
        }

        // Token: 0x060001C2 RID: 450 RVA: 0x0000A140 File Offset: 0x00008340
        public int getTexture()
        {
            return textureId;
        }

        // Token: 0x060001C3 RID: 451 RVA: 0x0000A148 File Offset: 0x00008348
        public virtual void hurt(Entity attacker, int damage)
        {
        }

        // Token: 0x060001C4 RID: 452 RVA: 0x0000A14C File Offset: 0x0000834C
        public void interpolateTurn(float yRot, float xRot)
        {
            this.yRot = (float) (this.yRot + yRot * 0.15);
            this.xRot = (float) (this.xRot - xRot * 0.15);
            if (this.xRot < -90f) this.xRot = -90f;
            if (this.xRot > 90f) this.xRot = 90f;
        }

        // Token: 0x060001C5 RID: 453 RVA: 0x0000A1C0 File Offset: 0x000083C0
        public bool intersects(float x0, float y0, float z0, float x1, float y1, float z1)
        {
            return bb.intersects(x0, y0, z0, x1, y1, z1);
        }

        // Token: 0x060001C6 RID: 454 RVA: 0x0000A1D8 File Offset: 0x000083D8
        public bool isCreativeModeAllowed()
        {
            return false;
        }

        // Token: 0x060001C7 RID: 455 RVA: 0x0000A1DC File Offset: 0x000083DC
        public bool isFree(float x, float y, float z)
        {
            var aabb = bb.cloneMove(x, y, z);
            return level.GetCubes(aabb).Count <= 0 && !level.ContainsAnyLiquid(aabb);
        }

        // Token: 0x060001C8 RID: 456 RVA: 0x0000A220 File Offset: 0x00008420
        public bool isFree(float x, float y, float z, float size)
        {
            var aabb = bb.grow(size, size, size).cloneMove(x, y, z);
            return level.GetCubes(aabb).Count <= 0 && !level.ContainsAnyLiquid(aabb);
        }

        // Token: 0x060001C9 RID: 457 RVA: 0x0000A26C File Offset: 0x0000846C
        public bool isInLava()
        {
            return level.containsLiquid(bb.grow(0f, -0.4f, 0f), 10);
        }

        // Token: 0x060001CA RID: 458 RVA: 0x0000A298 File Offset: 0x00008498
        public virtual bool isInOrOnRope()
        {
            return level.containsBlock(bb.grow(-0.5f, 0f, -0.5f), 51);
        }

        // Token: 0x060001CB RID: 459 RVA: 0x0000A2C4 File Offset: 0x000084C4
        public virtual bool isInWater()
        {
            return level.containsLiquid(bb.grow(0f, -0.4f, 0f), 8);
        }

        // Token: 0x060001CC RID: 460 RVA: 0x0000A2EC File Offset: 0x000084EC
        public virtual bool isLit()
        {
            var num = (int) x;
            var num2 = (int) y;
            var num3 = (int) z;
            return level.IsLit(num, num2, num3);
        }

        // Token: 0x060001CD RID: 461 RVA: 0x0000A320 File Offset: 0x00008520
        public virtual bool isPickable()
        {
            return false;
        }

        // Token: 0x060001CE RID: 462 RVA: 0x0000A324 File Offset: 0x00008524
        public virtual bool isPushable()
        {
            return false;
        }

        // Token: 0x060001CF RID: 463 RVA: 0x0000A328 File Offset: 0x00008528
        public virtual bool isShootable()
        {
            return false;
        }

        // Token: 0x060001D0 RID: 464 RVA: 0x0000A32C File Offset: 0x0000852C
        public virtual bool isUnderWater()
        {
            var tile = level.GetTile((int) x, (int) (y + 0.12f), (int) z);
            return Block.IsWater(tile);
        }

        // Token: 0x060001D1 RID: 465 RVA: 0x0000A368 File Offset: 0x00008568
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
                var num = this.x;
                var num2 = this.z;
                var num3 = x;
                var num4 = y;
                var num5 = z;
                var aabb = bb.copy();
                var cubes = level.GetCubes(bb.expand(x, y, z));
                for (var i = 0; i < cubes.Count; i++) y = cubes[i].clipYCollide(bb, y);
                bb.move(0f, y, 0f);
                if (!slide && num4 != y)
                {
                    z = 0f;
                    y = 0f;
                    x = 0f;
                }

                var flag = onGround || num4 != y && num4 < 0f;
                for (var j = 0; j < cubes.Count; j++) x = cubes[j].clipXCollide(bb, x);
                bb.move(x, 0f, 0f);
                if (!slide && num3 != x)
                {
                    z = 0f;
                    y = 0f;
                    x = 0f;
                }

                for (var j = 0; j < cubes.Count; j++) z = cubes[j].clipZCollide(bb, z);
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
                    var num8 = z;
                    x = num3;
                    y = footSize;
                    z = num5;
                    var aabb2 = bb.copy();
                    bb = aabb.copy();
                    cubes = level.GetCubes(bb.expand(num3, y, num5));
                    for (var k = 0; k < cubes.Count; k++) y = cubes[k].clipYCollide(bb, y);
                    bb.move(0f, y, 0f);
                    if (!slide && num4 != y)
                    {
                        z = 0f;
                        y = 0f;
                        x = 0f;
                    }

                    for (var k = 0; k < cubes.Count; k++) x = cubes[k].clipXCollide(bb, x);
                    bb.move(x, 0f, 0f);
                    if (!slide && num3 != x)
                    {
                        z = 0f;
                        y = 0f;
                        x = 0f;
                    }

                    for (var k = 0; k < cubes.Count; k++) z = cubes[k].clipZCollide(bb, z);
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
                        bb = aabb2.copy();
                    }
                    else
                    {
                        ySlideOffset = (float) (ySlideOffset + 0.5);
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

                if (num3 != x) xd = 0f;
                if (num4 != y) yd = 0f;
                if (num5 != z) zd = 0f;
                this.x = (bb.x0 + bb.x1) / 2f;
                this.y = bb.y0 + heightOffset - ySlideOffset;
                this.z = (bb.z0 + bb.z1) / 2f;
                num6 = this.x - num;
                num7 = this.z - num2;
                walkDist = (float) (walkDist + Math.Sqrt(num6 * num6 + num7 * num7) * 0.6);
            }

            var num9 = (int) Math.Floor(this.x);
            var num10 = (int) Math.Floor(this.y - 0.20000000298023224 - heightOffset);
            var num11 = (int) Math.Floor(this.z);
            int tile = level.GetTile(num9, num10, num11);
            if (makeStepSound && onGround)
            {
                var flag2 = noPhysics;
            }

            if (walkDist > nextStep && tile > 0) nextStep++;
            ySlideOffset *= 0.4f;
        }

        // Token: 0x060001D2 RID: 466 RVA: 0x0000A96C File Offset: 0x00008B6C
        public void moveRelative(float x, float y, float z)
        {
            float num;
            if ((num = (float) Math.Sqrt(x * x + y * y)) >= 0.01f)
            {
                if (num < 1f) num = 1f;
                num = z / num;
                x *= num;
                y *= num;
                z = (float) Math.Sin(yRot * 3.1415927f / 180f);
                num = (float) Math.Cos(yRot * 3.1415927f / 180f);
                xd += x * num - y * z;
                zd += y * num + x * z;
            }
        }

        // Token: 0x060001D3 RID: 467 RVA: 0x0000AA0C File Offset: 0x00008C0C
        public void moveTo(float x, float y, float z, float yRot, float xRot)
        {
            this.x = x;
            xo = x;
            this.y = y;
            yo = y;
            this.z = z;
            zo = z;
            this.yRot = yRot;
            this.xRot = xRot;
            setPos(x, y, z);
        }

        // Token: 0x060001D4 RID: 468 RVA: 0x0000AA64 File Offset: 0x00008C64
        public void playerTouch(Entity other)
        {
        }

        // Token: 0x060001D5 RID: 469 RVA: 0x0000AA68 File Offset: 0x00008C68
        public void playSound(string var1, float var2, float var3)
        {
        }

        // Token: 0x060001D6 RID: 470 RVA: 0x0000AA6C File Offset: 0x00008C6C
        public void push(Entity other)
        {
            var num = other.x - x;
            var num2 = other.z - z;
            float num3;
            if ((num3 = num * num + num2 * num2) >= 0.01f)
            {
                num3 = (float) Math.Sqrt(num3);
                num /= num3;
                num2 /= num3;
                num /= num3;
                num2 /= num3;
                num *= 0.05f;
                num2 *= 0.05f;
                num *= 1f - pushthrough;
                num2 *= 1f - pushthrough;
                push(-num, 0f, -num2);
                other.push(num, 0f, num2);
            }
        }

        // Token: 0x060001D7 RID: 471 RVA: 0x0000AB08 File Offset: 0x00008D08
        protected void push(float xd, float yd, float zd)
        {
            this.xd += xd;
            this.yd += yd;
            this.zd += zd;
        }

        // Token: 0x060001D8 RID: 472 RVA: 0x0000AB34 File Offset: 0x00008D34
        public void remove()
        {
            removed = true;
        }

        // Token: 0x060001D9 RID: 473 RVA: 0x0000AB40 File Offset: 0x00008D40
        public virtual void render(TextureManager manager, float var2)
        {
        }

        // Token: 0x060001DA RID: 474 RVA: 0x0000AB44 File Offset: 0x00008D44
        public virtual void renderHover(TextureManager manager, float var2)
        {
        }

        // Token: 0x060001DB RID: 475 RVA: 0x0000AB48 File Offset: 0x00008D48
        public void resetPos()
        {
            if (level != null)
            {
                var num = level.spawnx + 0.5f;
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

        // Token: 0x060001DC RID: 476 RVA: 0x0000ABD4 File Offset: 0x00008DD4
        public void setLevel(Level level)
        {
            this.level = level;
        }

        // Token: 0x060001DD RID: 477 RVA: 0x0000ABE0 File Offset: 0x00008DE0
        public void setPos(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            var num = bbWidth / 2f;
            var num2 = bbHeight / 2f;
            bb = new AABB(x - num, y - num2, z - num, x + num, y + num2, z + num);
        }

        // Token: 0x060001DE RID: 478 RVA: 0x0000AC3C File Offset: 0x00008E3C
        public void setPos(Position pos)
        {
            if (pos.HasPosition)
                setPos(pos.X, pos.Y, pos.Z);
            else
                setPos(x, y, z);
            if (pos.HasRotation)
            {
                setRot(pos.Yaw, pos.Pitch);
                return;
            }

            setRot(yRot, xRot);
        }

        // Token: 0x060001DF RID: 479 RVA: 0x0000ACB0 File Offset: 0x00008EB0
        protected void setRot(float yRot, float xRot)
        {
            this.yRot = yRot;
            this.xRot = xRot;
        }

        // Token: 0x060001E0 RID: 480 RVA: 0x0000ACC0 File Offset: 0x00008EC0
        public void setSize(float bbWidth, float bbHeight)
        {
            this.bbWidth = bbWidth;
            this.bbHeight = bbHeight;
        }

        // Token: 0x060001E1 RID: 481 RVA: 0x0000ACD0 File Offset: 0x00008ED0
        public bool shouldRender(Vector3F var1)
        {
            var num = x - var1.X;
            var num2 = y - var1.Y;
            var num3 = z - var1.Z;
            num3 = num * num + num2 * num2 + num3 * num3;
            return shouldRenderAtSqrDistance(num3);
        }

        // Token: 0x060001E2 RID: 482 RVA: 0x0000AD1C File Offset: 0x00008F1C
        public bool shouldRenderAtSqrDistance(float var1)
        {
            var num = bb.getSize() * 64f;
            return var1 < num * num;
        }

        // Token: 0x060001E3 RID: 483 RVA: 0x0000AD44 File Offset: 0x00008F44
        public virtual void tick()
        {
            walkDistO = walkDist;
            xo = x;
            yo = y;
            zo = z;
            xRotO = xRot;
            yRotO = yRot;
        }

        // Token: 0x060001E4 RID: 484 RVA: 0x0000AD9C File Offset: 0x00008F9C
        public virtual void turn(float yRot, float xRot)
        {
            var num = this.xRot;
            var num2 = this.yRot;
            this.yRot = (float) (this.yRot + yRot * 0.15);
            this.xRot = (float) (this.xRot - xRot * 0.15);
            if (this.xRot < -90f) this.xRot = -90f;
            if (this.xRot > 90f) this.xRot = 90f;
            xRotO += this.xRot - num;
            yRotO += this.yRot - num2;
        }
    }
}