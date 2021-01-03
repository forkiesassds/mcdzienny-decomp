namespace MCDzienny
{
    // Token: 0x02000004 RID: 4
    public abstract class AI
    {
        // Token: 0x04000001 RID: 1
        public int defaultLookAngle;

        // Token: 0x06000009 RID: 9 RVA: 0x00002068 File Offset: 0x00000268
        public virtual void tick(Level level, Mob mob)
        {
        }

        // Token: 0x0600000A RID: 10 RVA: 0x0000206C File Offset: 0x0000026C
        public virtual void beforeRemove()
        {
        }

        // Token: 0x0600000B RID: 11 RVA: 0x00002070 File Offset: 0x00000270
        public virtual void hurt(Entity from, int damage)
        {
        }
    }
}