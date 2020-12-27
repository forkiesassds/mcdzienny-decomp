namespace MCDzienny
{
    // Token: 0x02000051 RID: 81
    public class JumpAttackAI : BasicAttackAI
    {
        // Token: 0x060001F4 RID: 500 RVA: 0x0000B8CC File Offset: 0x00009ACC
        public JumpAttackAI()
        {
            runSpeed *= 0.8f;
        }

        // Token: 0x060001F5 RID: 501 RVA: 0x0000B8E8 File Offset: 0x00009AE8
        protected new void jumpFromGround()
        {
            if (attackTarget == null)
            {
                base.jumpFromGround();
                return;
            }

            mob.xd = 0f;
            mob.zd = 0f;
            mob.moveRelative(0f, 1f, 0.6f);
            mob.yd = 0.5f;
        }
    }
}