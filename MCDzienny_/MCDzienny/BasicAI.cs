using System;
using System.Collections.Generic;

namespace MCDzienny
{
    public class BasicAI : AI
    {

        protected int attackDelay;

        public Entity attackTarget;

        public bool jumping;

        public Level level;

        public Mob mob;

        protected int noActionTime;
        public Random random = new Random();

        public bool running;

        public float runSpeed = 0.7f;

        public float xxa;

        protected float yRotA;

        public float yya;

        public override void tick(Level level, Mob mob)
        {
            noActionTime++;
            Entity closestPlayer;
            if (noActionTime > 600 && random.Next(800) == 0 && (closestPlayer = level.GetClosestPlayer(mob.x, mob.y, mob.z)) != null)
            {
                float num = closestPlayer.x - mob.x;
                float num2 = closestPlayer.y - mob.y;
                float num3 = closestPlayer.z - mob.z;
                if (num * num + num2 * num2 + num3 * num3 < 1024f)
                {
                    noActionTime = 0;
                }
            }
            this.level = level;
            this.mob = mob;
            if (attackDelay > 0)
            {
                attackDelay--;
            }
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
            bool flag = mob.isInWater();
            bool flag2 = mob.isInLava();
            if (jumping)
            {
                if (flag)
                {
                    mob.yd += 0.04f;
                }
                else if (flag2)
                {
                    mob.yd += 0.04f;
                }
                else if (mob.onGround)
                {
                    jumpFromGround();
                }
            }
            xxa *= 0.98f;
            yya *= 0.98f;
            yRotA *= 0.9f;
            mob.travel(xxa, yya);
            List<Entity> list = level.findEntities(mob, mob.bb.grow(0.2f, 0f, 0.2f));
            if (list == null || list.Count <= 0)
            {
                return;
            }
            for (int i = 0; i < list.Count; i++)
            {
                Entity entity = list[i];
                if (entity.isPushable())
                {
                    entity.push(mob);
                }
            }
        }

        protected void jumpFromGround()
        {
            mob.yd = 0.42f;
        }

        protected virtual void update()
        {
            if (random.NextDouble() < 0.07)
            {
                xxa = (float)((random.NextDouble() - 0.5) * runSpeed);
                yya = (float)(random.NextDouble() * runSpeed);
            }
            jumping = random.NextDouble() < 0.01;
            if (random.NextDouble() < 0.04)
            {
                yRotA = (float)(random.NextDouble() - 0.5) * 60f;
            }
            mob.yRot += yRotA;
            mob.xRot = defaultLookAngle;
            if (attackTarget != null)
            {
                yya = runSpeed;
                jumping = random.NextDouble() < 0.04;
            }
            bool flag = mob.isInWater();
            bool flag2 = mob.isInLava();
            if (flag || flag2)
            {
                jumping = random.NextDouble() < 0.8;
            }
        }

        public override void beforeRemove() {}

        public override void hurt(Entity var1, int var2)
        {
            base.hurt(var1, var2);
            noActionTime = 0;
        }
    }
}