namespace MCDzienny
{
    // Token: 0x02000050 RID: 80
    public class Zombie : HumanoidMob
    {
        // Token: 0x060001F3 RID: 499 RVA: 0x0000B874 File Offset: 0x00009A74
        public Zombie(Level level, float x, float y, float z) : base(level, x, y, z)
        {
            modelName = "zombie";
            textureName = "/mob/zombie.png";
            heightOffset = 1.62f;
            var basicAttackAI = new BasicAttackAI();
            deathScore = 80;
            basicAttackAI.defaultLookAngle = 30;
            ai = basicAttackAI;
        }
    }
}