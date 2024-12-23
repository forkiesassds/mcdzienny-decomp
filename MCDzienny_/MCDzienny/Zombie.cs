namespace MCDzienny
{
    public class Zombie : HumanoidMob
    {
        public Zombie(Level level, float x, float y, float z)
            : base(level, x, y, z)
        {
            modelName = "zombie";
            textureName = "/mob/zombie.png";
            heightOffset = 1.62f;
            BasicAttackAI basicAttackAI = new BasicAttackAI();
            deathScore = 80;
            basicAttackAI.defaultLookAngle = 30;
            ai = basicAttackAI;
        }
    }
}