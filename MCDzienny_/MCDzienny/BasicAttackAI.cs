using System;

namespace MCDzienny
{
    // Token: 0x02000009 RID: 9
    public class BasicAttackAI : BasicAI
    {
        // Token: 0x04000018 RID: 24
        public int damage = 6;

        // Token: 0x0600001D RID: 29 RVA: 0x0000274C File Offset: 0x0000094C
        protected override void update()
        {
            base.update();
            if (mob.health > 0) doAttack();
        }

        // Token: 0x0600001E RID: 30 RVA: 0x00002768 File Offset: 0x00000968
        protected void doAttack()
        {
            Entity closestPlayer = level.GetClosestPlayer(mob.x, mob.y, mob.z);
            var num = 16f;
            if (attackTarget != null && attackTarget.removed) attackTarget = null;
            if (closestPlayer != null && attackTarget == null)
            {
                var num2 = closestPlayer.x - mob.x;
                var num3 = closestPlayer.y - mob.y;
                var num4 = closestPlayer.z - mob.z;
                if (num2 * num2 + num3 * num3 + num4 * num4 < num * num) attackTarget = closestPlayer;
            }

            if (attackTarget != null)
            {
                var num2 = attackTarget.x - mob.x;
                var num3 = attackTarget.y - mob.y;
                var num4 = attackTarget.z - mob.z;
                float num5;
                if ((num5 = num2 * num2 + num3 * num3 + num4 * num4) > num * num * 2f * 2f && random.Next(100) == 0)
                    attackTarget = null;
                if (attackTarget != null)
                {
                    num5 = (float) Math.Sqrt(num5);
                    mob.yRot = (float) (Math.Atan2(num4, num2) * 180.0 / 3.1415927410125732) - 90f;
                    mob.xRot = -(float) (Math.Atan2(num3, num5) * 180.0 / 3.1415927410125732);
                    if (Math.Sqrt(num2 * num2 + num3 * num3 + num4 * num4) < 2.0 && attackDelay == 0)
                        attack(attackTarget);
                }
            }
        }

        // Token: 0x0600001F RID: 31 RVA: 0x00002954 File Offset: 0x00000B54
        public bool attack(Entity target)
        {
            if (level.clip(new Vector3F(mob.x, mob.y, mob.z), new Vector3F(target.x, target.y, target.z)) !=
                null) return false;
            mob.attackTime = 5;
            attackDelay = random.Next(20) + 10;
            var num = (int) ((random.NextDouble() + random.NextDouble()) / 2.0 * damage + 1.0);
            target.hurt(mob, num);
            noActionTime = 0;
            return true;
        }

        // Token: 0x06000020 RID: 32 RVA: 0x00002A1C File Offset: 0x00000C1C
        public override void hurt(Entity attacker, int damage)
        {
            base.hurt(attacker, damage);
            if (attacker != null && !attacker.GetType().Equals(mob.GetType())) attackTarget = attacker;
        }
    }
}