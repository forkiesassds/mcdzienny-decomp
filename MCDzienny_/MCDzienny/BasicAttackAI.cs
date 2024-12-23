using System;

namespace MCDzienny
{
    public class BasicAttackAI : BasicAI
    {
        public int damage = 6;

        protected override void update()
        {
            base.update();
            if (mob.health > 0)
            {
                doAttack();
            }
        }

        protected void doAttack()
        {
            Entity closestPlayer = level.GetClosestPlayer(mob.x, mob.y, mob.z);
            float num = 16f;
            if (attackTarget != null && attackTarget.removed)
            {
                attackTarget = null;
            }
            float num2;
            float num3;
            float num4;
            if (closestPlayer != null && attackTarget == null)
            {
                num2 = closestPlayer.x - mob.x;
                num3 = closestPlayer.y - mob.y;
                num4 = closestPlayer.z - mob.z;
                if (num2 * num2 + num3 * num3 + num4 * num4 < num * num)
                {
                    attackTarget = closestPlayer;
                }
            }
            if (attackTarget == null)
            {
                return;
            }
            num2 = attackTarget.x - mob.x;
            num3 = attackTarget.y - mob.y;
            num4 = attackTarget.z - mob.z;
            float num5;
            if ((num5 = num2 * num2 + num3 * num3 + num4 * num4) > num * num * 2f * 2f && random.Next(100) == 0)
            {
                attackTarget = null;
            }
            if (attackTarget != null)
            {
                num5 = (float)Math.Sqrt(num5);
                mob.yRot = (float)(Math.Atan2(num4, num2) * 180.0 / 3.1415927410125732) - 90f;
                mob.xRot = 0f - (float)(Math.Atan2(num3, num5) * 180.0 / 3.1415927410125732);
                if (Math.Sqrt(num2 * num2 + num3 * num3 + num4 * num4) < 2.0 && attackDelay == 0)
                {
                    attack(attackTarget);
                }
            }
        }

        public bool attack(Entity target)
        {
            if (level.clip(new Vector3F(mob.x, mob.y, mob.z), new Vector3F(target.x, target.y, target.z)) != null)
            {
                return false;
            }
            mob.attackTime = 5;
            attackDelay = random.Next(20) + 10;
            int num = (int)((random.NextDouble() + random.NextDouble()) / 2.0 * damage + 1.0);
            target.hurt(mob, num);
            noActionTime = 0;
            return true;
        }

        public override void hurt(Entity attacker, int damage)
        {
            base.hurt(attacker, damage);
            if (attacker != null && !attacker.GetType().Equals(mob.GetType()))
            {
                attackTarget = attacker;
            }
        }
    }
}