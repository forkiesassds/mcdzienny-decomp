namespace MCDzienny
{
    public abstract class AI
    {
        public int defaultLookAngle;

        public virtual void tick(Level level, Mob mob) {}

        public virtual void beforeRemove() {}

        public virtual void hurt(Entity from, int damage) {}
    }
}