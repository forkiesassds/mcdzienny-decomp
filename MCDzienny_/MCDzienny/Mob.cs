using System;

namespace MCDzienny
{
    public class Mob : Entity
    {
        public static readonly int ATTACK_DURATION = 5;

        public static readonly int TOTAL_AIR_SUPPLY = 300;

        public AI ai;

        public int airSupply = 300;

        public bool allowAlpha = true;

        protected float animStep;

        protected float animStepO;

        public int attackTime;

        protected float bobStrength = 1f;

        protected bool dead;

        protected int deathScore;

        public int deathTime;

        public bool hasHair = true;

        public int health = 20;

        public float hurtDir;

        public int hurtDuration;

        public int hurtTime;

        public int invulnerableDuration = 20;

        public int invulnerableTime;

        public int lastHealth;

        public string modelName;

        protected float oRun;

        public float oTilt;

        public Random random = new Random();

        public float renderOffset;

        public float rot;

        public float rotA;

        public float rotOffs;

        protected float run;

        public float speed;

        protected string textureName = "/char.png";

        protected int tickCount;

        public float tilt;

        public float timeOffs;

        protected float yBodyRot;

        protected float yBodyRotO;

        public Mob(Level level)
            : base(level)
        {
            rotA = (float)(random.NextDouble() + 1.0) * 0.01f;
            setPos(x, y, z);
            timeOffs = (float)random.NextDouble() * 12398f;
            rot = (float)(random.NextDouble() * Math.PI * 2.0);
            speed = 1f;
            ai = new BasicAI();
            footSize = 0.5f;
        }

        public override bool isPickable()
        {
            return !removed;
        }

        public override bool isPushable()
        {
            return !removed;
        }

        public override void tick()
        {
            base.tick();
            oTilt = tilt;
            if (attackTime > 0)
            {
                attackTime--;
            }
            if (hurtTime > 0)
            {
                hurtTime--;
            }
            if (invulnerableTime > 0)
            {
                invulnerableTime--;
            }
            if (health <= 0)
            {
                deathTime++;
                if (deathTime > 20)
                {
                    if (ai != null)
                    {
                        ai.beforeRemove();
                    }
                    remove();
                }
            }
            if (isUnderWater())
            {
                if (airSupply > 0)
                {
                    airSupply--;
                }
                else
                {
                    hurt(null, 2);
                }
            }
            else
            {
                airSupply = 300;
            }
            if (isInWater())
            {
                fallDistance = 0f;
            }
            if (isInLava())
            {
                hurt(null, 10);
            }
            animStepO = animStep;
            yBodyRotO = yBodyRot;
            yRotO = yRot;
            xRotO = xRot;
            tickCount++;
            aiStep();
            float num = x - xo;
            float num2 = z - zo;
            float num3 = (float)Math.Sqrt(num * num + num2 * num2);
            float num4 = yBodyRot;
            float num5 = 0f;
            oRun = run;
            float num6 = 0f;
            if (num3 > 0.05f)
            {
                num6 = 1f;
                num5 = num3 * 3f;
                num4 = (float)Math.Atan2(num2, num) * 180f / (float)Math.PI - 90f;
            }
            if (!onGround)
            {
                num6 = 0f;
            }
            run += (num6 - run) * 0.3f;
            for (num = num4 - yBodyRot; num < -180f; num += 360f) {}
            while (num >= 180f)
            {
                num -= 360f;
            }
            yBodyRot += num * 0.1f;
            for (num = yRot - yBodyRot; num < -180f; num += 360f) {}
            while (num >= 180f)
            {
                num -= 360f;
            }
            bool flag = num < -90f || num >= 90f;
            if (num < -75f)
            {
                num = -75f;
            }
            if (num >= 75f)
            {
                num = 75f;
            }
            yBodyRot = yRot - num;
            yBodyRot += num * 0.1f;
            if (flag)
            {
                num5 = 0f - num5;
            }
            while (yRot - yRotO < -180f)
            {
                yRotO -= 360f;
            }
            while (yRot - yRotO >= 180f)
            {
                yRotO += 360f;
            }
            while (yBodyRot - yBodyRotO < -180f)
            {
                yBodyRotO -= 360f;
            }
            while (yBodyRot - yBodyRotO >= 180f)
            {
                yBodyRotO += 360f;
            }
            while (xRot - xRotO < -180f)
            {
                xRotO -= 360f;
            }
            while (xRot - xRotO >= 180f)
            {
                xRotO += 360f;
            }
            animStep += num5;
        }

        public void aiStep()
        {
            if (ai != null)
            {
                ai.tick(level, this);
            }
        }

        public void heal(int var1)
        {
            if (health > 0)
            {
                health += var1;
                if (health > 20)
                {
                    health = 20;
                }
                invulnerableTime = invulnerableDuration / 2;
            }
        }

        public override void hurt(Entity attacker, int damage)
        {
            if (level.creativeMode || health <= 0)
            {
                return;
            }
            ai.hurt(attacker, damage);
            if (invulnerableTime > invulnerableDuration / 2f)
            {
                if (lastHealth - damage >= health)
                {
                    return;
                }
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
                float num = attacker.x - x;
                float num2 = attacker.z - z;
                hurtDir = (float)(Math.Atan2(num2, num) * 180.0 / 3.1415927410125732) - yRot;
                knockback(attacker, damage, num, num2);
            }
            else
            {
                hurtDir = (int)(random.NextDouble() * 2.0) * 180;
            }
            if (health <= 0)
            {
                die(attacker);
            }
        }

        public void knockback(Entity var1, int var2, float var3, float var4)
        {
            float num = (float)Math.Sqrt(var3 * var3 + var4 * var4);
            float num2 = 0.4f;
            xd /= 2f;
            yd /= 2f;
            zd /= 2f;
            xd -= var3 / num * num2;
            yd += 0.4f;
            zd -= var4 / num * num2;
            if (yd > 0.4f)
            {
                yd = 0.4f;
            }
        }

        public void die(Entity killer)
        {
            if (!level.creativeMode)
            {
                if (deathScore > 0)
                {
                    if (killer != null)
                    {
                        killer.awardKillScore(this, deathScore);
                    }
                }
                dead = true;
            }
        }

        protected override void causeFallDamage(float height)
        {
            if (!level.creativeMode)
            {
                int num = (int)Math.Ceiling(height - 3f);
                if (num > 0)
                {
                    hurt(null, num);
                }
            }
        }

        public void travel(float yya, float xxa)
        {
            float num = 1f;
            if (ai is BasicAI)
            {
                BasicAI basicAI = (BasicAI)ai;
                num = !basicAI.running ? 1f : 1.4f;
            }
            if (isInWater())
            {
                float num2 = y;
                moveRelative(yya, xxa, 0.02f * num);
                move(xd, yd, zd);
                xd *= 0.8f;
                yd *= 0.8f;
                zd *= 0.8f;
                yd = (float)(yd - 0.02);
                if (horizontalCollision && isFree(xd, yd + 0.6f - y + num2, zd))
                {
                    yd = 0.3f;
                }
                return;
            }
            if (isInLava())
            {
                float num2 = y;
                moveRelative(yya, xxa, 0.02f * num);
                move(xd, yd, zd);
                xd *= 0.5f;
                yd *= 0.5f;
                zd *= 0.5f;
                yd = (float)(yd - 0.02);
                if (horizontalCollision && isFree(xd, yd + 0.6f - y + num2, zd))
                {
                    yd = 0.3f;
                }
                return;
            }
            moveRelative(yya, xxa, (onGround ? 0.1f : 0.02f) * num);
            move(xd, yd, zd);
            xd *= 0.91f;
            yd *= 0.98f;
            zd *= 0.91f;
            yd = (float)(yd - 0.08);
            if (onGround)
            {
                float num2 = 0.6f;
                xd *= num2;
                zd *= num2;
            }
        }

        public override bool isShootable()
        {
            return true;
        }
    }
}