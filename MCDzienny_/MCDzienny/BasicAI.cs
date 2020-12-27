using System;

namespace MCDzienny
{
    // Token: 0x02000008 RID: 8
    public class BasicAI : AI
    {
        // Token: 0x04000013 RID: 19
        protected int attackDelay;

        // Token: 0x04000016 RID: 22
        public Entity attackTarget;

        // Token: 0x04000012 RID: 18
        public bool jumping;

        // Token: 0x04000010 RID: 16
        public Level level;

        // Token: 0x04000011 RID: 17
        public Mob mob;

        // Token: 0x04000015 RID: 21
        protected int noActionTime;

        // Token: 0x0400000C RID: 12
        public Random random = new Random();

        // Token: 0x04000017 RID: 23
        public bool running;

        // Token: 0x04000014 RID: 20
        public float runSpeed = 0.7f;

        // Token: 0x0400000D RID: 13
        public float xxa;

        // Token: 0x0400000F RID: 15
        protected float yRotA;

        // Token: 0x0400000E RID: 14
        public float yya;

        // Token: 0x06000017 RID: 23 RVA: 0x000023BC File Offset: 0x000005BC
        public override void tick(Level level, Mob mob)
        {
            noActionTime++;
            Entity closestPlayer;
            if (noActionTime > 600 && random.Next(800) == 0 &&
                (closestPlayer = level.GetClosestPlayer(mob.x, mob.y, mob.z)) != null)
            {
                var num = closestPlayer.x - mob.x;
                var num2 = closestPlayer.y - mob.y;
                var num3 = closestPlayer.z - mob.z;
                if (num * num + num2 * num2 + num3 * num3 < 1024f) noActionTime = 0;
            }

            this.level = level;
            this.mob = mob;
            if (attackDelay > 0) attackDelay--;
            if (mob.health <= 0)
            {
                jumping = false;
                xxa = 0f;
                yya = 0f;
                yRotA = 0f;
            }
            else
            {
                update();
            }

            var flag = mob.isInWater();
            var flag2 = mob.isInLava();
            if (jumping)
            {
                if (flag)
                    mob.yd += 0.04f;
                else if (flag2)
                    mob.yd += 0.04f;
                else if (mob.onGround) jumpFromGround();
            }

            xxa *= 0.98f;
            yya *= 0.98f;
            yRotA *= 0.9f;
            mob.travel(xxa, yya);
            var list = level.findEntities(mob, mob.bb.grow(0.2f, 0f, 0.2f));
            if (list != null && list.Count > 0)
                for (var i = 0; i < list.Count; i++)
                {
                    var entity = list[i];
                    if (entity.isPushable()) entity.push(mob);
                }
        }

        // Token: 0x06000018 RID: 24 RVA: 0x000025B4 File Offset: 0x000007B4
        protected void jumpFromGround()
        {
            mob.yd = 0.42f;
        }

        // Token: 0x06000019 RID: 25 RVA: 0x000025C8 File Offset: 0x000007C8
        protected virtual void update()
        {
            if (random.NextDouble() < 0.07)
            {
                xxa = (float) ((random.NextDouble() - 0.5) * runSpeed);
                yya = (float) (random.NextDouble() * runSpeed);
            }

            jumping = random.NextDouble() < 0.01;
            if (random.NextDouble() < 0.04) yRotA = (float) (random.NextDouble() - 0.5) * 60f;
            mob.yRot += yRotA;
            mob.xRot = defaultLookAngle;
            if (attackTarget != null)
            {
                yya = runSpeed;
                jumping = random.NextDouble() < 0.04;
            }

            var flag = mob.isInWater();
            var flag2 = mob.isInLava();
            if (flag || flag2) jumping = random.NextDouble() < 0.8;
        }

        // Token: 0x0600001A RID: 26 RVA: 0x00002714 File Offset: 0x00000914
        public override void beforeRemove()
        {
        }

        // Token: 0x0600001B RID: 27 RVA: 0x00002718 File Offset: 0x00000918
        public override void hurt(Entity var1, int var2)
        {
            base.hurt(var1, var2);
            noActionTime = 0;
        }
    }
}