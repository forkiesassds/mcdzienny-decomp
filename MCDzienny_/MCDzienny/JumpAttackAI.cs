namespace MCDzienny
{
    public class JumpAttackAI : BasicAttackAI
    {
        public JumpAttackAI()
        {
            runSpeed *= 0.8f;
        }

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