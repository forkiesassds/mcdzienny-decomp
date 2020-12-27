using System;

namespace MCDzienny
{
    // Token: 0x0200004E RID: 78
    public class Mob : Entity
    {
        // Token: 0x04000132 RID: 306
        public static readonly int ATTACK_DURATION = 5;

        // Token: 0x04000133 RID: 307
        public static readonly int TOTAL_AIR_SUPPLY = 300;

        // Token: 0x04000155 RID: 341
        public AI ai;

        // Token: 0x0400014C RID: 332
        public int airSupply = 300;

        // Token: 0x04000143 RID: 323
        public bool allowAlpha = true;

        // Token: 0x0400013E RID: 318
        protected float animStep;

        // Token: 0x0400013F RID: 319
        protected float animStepO;

        // Token: 0x04000151 RID: 337
        public int attackTime;

        // Token: 0x04000146 RID: 326
        protected float bobStrength = 1f;

        // Token: 0x04000154 RID: 340
        protected bool dead;

        // Token: 0x04000147 RID: 327
        protected int deathScore;

        // Token: 0x04000150 RID: 336
        public int deathTime;

        // Token: 0x04000141 RID: 321
        public bool hasHair = true;

        // Token: 0x04000149 RID: 329
        public int health = 20;

        // Token: 0x0400014F RID: 335
        public float hurtDir;

        // Token: 0x0400014E RID: 334
        public int hurtDuration;

        // Token: 0x0400014D RID: 333
        public int hurtTime;

        // Token: 0x04000135 RID: 309
        public int invulnerableDuration = 20;

        // Token: 0x0400014B RID: 331
        public int invulnerableTime;

        // Token: 0x0400014A RID: 330
        public int lastHealth;

        // Token: 0x04000145 RID: 325
        public string modelName;

        // Token: 0x0400013C RID: 316
        protected float oRun;

        // Token: 0x04000152 RID: 338
        public float oTilt;

        // Token: 0x04000134 RID: 308
        public Random random = new Random();

        // Token: 0x04000148 RID: 328
        public float renderOffset;

        // Token: 0x04000136 RID: 310
        public float rot;

        // Token: 0x04000139 RID: 313
        public float rotA;

        // Token: 0x04000144 RID: 324
        public float rotOffs;

        // Token: 0x0400013D RID: 317
        protected float run;

        // Token: 0x04000138 RID: 312
        public float speed;

        // Token: 0x04000142 RID: 322
        protected string textureName = "/char.png";

        // Token: 0x04000140 RID: 320
        protected int tickCount;

        // Token: 0x04000153 RID: 339
        public float tilt;

        // Token: 0x04000137 RID: 311
        public float timeOffs;

        // Token: 0x0400013A RID: 314
        protected float yBodyRot;

        // Token: 0x0400013B RID: 315
        protected float yBodyRotO;

        // Token: 0x060001E5 RID: 485 RVA: 0x0000AE48 File Offset: 0x00009048
        public Mob(Level level) : base(level)
        {
            rotA = (float) (random.NextDouble() + 1.0) * 0.01f;
            setPos(x, y, z);
            timeOffs = (float) random.NextDouble() * 12398f;
            rot = (float) (random.NextDouble() * 3.141592653589793 * 2.0);
            speed = 1f;
            ai = new BasicAI();
            footSize = 0.5f;
        }

        // Token: 0x060001E6 RID: 486 RVA: 0x0000AF40 File Offset: 0x00009140
        public override bool isPickable()
        {
            return !removed;
        }

        // Token: 0x060001E7 RID: 487 RVA: 0x0000AF4C File Offset: 0x0000914C
        public override bool isPushable()
        {
            return !removed;
        }

        // Token: 0x060001E8 RID: 488 RVA: 0x0000AF58 File Offset: 0x00009158
        public override void tick()
        {
            base.tick();
            oTilt = tilt;
            if (attackTime > 0) attackTime--;
            if (hurtTime > 0) hurtTime--;
            if (invulnerableTime > 0) invulnerableTime--;
            if (health <= 0)
            {
                deathTime++;
                if (deathTime > 20)
                {
                    if (ai != null) ai.beforeRemove();
                    remove();
                }
            }

            if (isUnderWater())
            {
                if (airSupply > 0)
                    airSupply--;
                else
                    hurt(null, 2);
            }
            else
            {
                airSupply = 300;
            }

            if (isInWater()) fallDistance = 0f;
            if (isInLava()) hurt(null, 10);
            animStepO = animStep;
            yBodyRotO = yBodyRot;
            yRotO = yRot;
            xRotO = xRot;
            tickCount++;
            aiStep();
            var num = x - xo;
            var num2 = z - zo;
            var num3 = (float) Math.Sqrt(num * num + num2 * num2);
            var num4 = yBodyRot;
            var num5 = 0f;
            oRun = run;
            var num6 = 0f;
            if (num3 > 0.05f)
            {
                num6 = 1f;
                num5 = num3 * 3f;
                num4 = (float) Math.Atan2(num2, num) * 180f / 3.1415927f - 90f;
            }

            if (!onGround) num6 = 0f;
            run += (num6 - run) * 0.3f;
            for (num = num4 - yBodyRot; num < -180f; num += 360f)
            {
            }

            while (num >= 180f) num -= 360f;
            yBodyRot += num * 0.1f;
            for (num = yRot - yBodyRot; num < -180f; num += 360f)
            {
            }

            while (num >= 180f) num -= 360f;
            var flag = num < -90f || num >= 90f;
            if (num < -75f) num = -75f;
            if (num >= 75f) num = 75f;
            yBodyRot = yRot - num;
            yBodyRot += num * 0.1f;
            if (flag) num5 = -num5;
            while (yRot - yRotO < -180f) yRotO -= 360f;
            while (yRot - yRotO >= 180f) yRotO += 360f;
            while (yBodyRot - yBodyRotO < -180f) yBodyRotO -= 360f;
            while (yBodyRot - yBodyRotO >= 180f) yBodyRotO += 360f;
            while (xRot - xRotO < -180f) xRotO -= 360f;
            while (xRot - xRotO >= 180f) xRotO += 360f;
            animStep += num5;
        }

        // Token: 0x060001E9 RID: 489 RVA: 0x0000B314 File Offset: 0x00009514
        public void aiStep()
        {
            if (ai != null) ai.tick(level, this);
        }

        // Token: 0x060001EA RID: 490 RVA: 0x0000B330 File Offset: 0x00009530
        public void heal(int var1)
        {
            if (health > 0)
            {
                health += var1;
                if (health > 20) health = 20;
                invulnerableTime = invulnerableDuration / 2;
            }
        }

        // Token: 0x060001EB RID: 491 RVA: 0x0000B36C File Offset: 0x0000956C
        public override void hurt(Entity attacker, int damage)
        {
            if (!level.creativeMode && health > 0)
            {
                ai.hurt(attacker, damage);
                if (invulnerableTime > invulnerableDuration / 2f)
                {
                    if (lastHealth - damage >= health) return;
                    health = lastHealth - damage;
                }
                else
                {
                    lastHealth = health;
                    invulnerableTime = invulnerableDuration;
                    health -= damage;
                    hurtTime = hurtDuration = 10;
                }

                hurtDir = 0f;
                if (attacker != null)
                {
                    var num = attacker.x - x;
                    var num2 = attacker.z - z;
                    hurtDir = (float) (Math.Atan2(num2, num) * 180.0 / 3.1415927410125732) - yRot;
                    knockback(attacker, damage, num, num2);
                }
                else
                {
                    hurtDir = (int) (random.NextDouble() * 2.0) * 180;
                }

                if (health <= 0) die(attacker);
            }
        }

        // Token: 0x060001EC RID: 492 RVA: 0x0000B4A4 File Offset: 0x000096A4
        public void knockback(Entity var1, int var2, float var3, float var4)
        {
            var num = (float) Math.Sqrt(var3 * var3 + var4 * var4);
            var num2 = 0.4f;
            xd /= 2f;
            yd /= 2f;
            zd /= 2f;
            xd -= var3 / num * num2;
            yd += 0.4f;
            zd -= var4 / num * num2;
            if (yd > 0.4f) yd = 0.4f;
        }

        // Token: 0x060001ED RID: 493 RVA: 0x0000B550 File Offset: 0x00009750
        public void die(Entity killer)
        {
            if (!level.creativeMode)
            {
                if (deathScore > 0 && killer != null) killer.awardKillScore(this, deathScore);
                dead = true;
            }
        }

        // Token: 0x060001EE RID: 494 RVA: 0x0000B580 File Offset: 0x00009780
        protected override void causeFallDamage(float height)
        {
            if (!level.creativeMode)
            {
                var num = (int) Math.Ceiling(height - 3f);
                if (num > 0) hurt(null, num);
            }
        }

        // Token: 0x060001EF RID: 495 RVA: 0x0000B5B8 File Offset: 0x000097B8
        public void travel(float yya, float xxa)
        {
            var num = 1f;
            if (ai is BasicAI)
            {
                var basicAI = (BasicAI) ai;
                if (basicAI.running)
                    num = 1.4f;
                else
                    num = 1f;
            }

            if (isInWater())
            {
                var num2 = y;
                moveRelative(yya, xxa, 0.02f * num);
                move(xd, yd, zd);
                xd *= 0.8f;
                yd *= 0.8f;
                zd *= 0.8f;
                yd = (float) (yd - 0.02);
                if (horizontalCollision && isFree(xd, yd + 0.6f - y + num2, zd))
                {
                    yd = 0.3f;
                }
            }
            else if (isInLava())
            {
                var num2 = y;
                moveRelative(yya, xxa, 0.02f * num);
                move(xd, yd, zd);
                xd *= 0.5f;
                yd *= 0.5f;
                zd *= 0.5f;
                yd = (float) (yd - 0.02);
                if (horizontalCollision && isFree(xd, yd + 0.6f - y + num2, zd)) yd = 0.3f;
            }
            else
            {
                moveRelative(yya, xxa, (onGround ? 0.1f : 0.02f) * num);
                move(xd, yd, zd);
                xd *= 0.91f;
                yd *= 0.98f;
                zd *= 0.91f;
                yd = (float) (yd - 0.08);
                if (onGround)
                {
                    var num2 = 0.6f;
                    xd *= num2;
                    zd *= num2;
                }
            }
        }

        // Token: 0x060001F0 RID: 496 RVA: 0x0000B83C File Offset: 0x00009A3C
        public override bool isShootable()
        {
            return true;
        }
    }
}