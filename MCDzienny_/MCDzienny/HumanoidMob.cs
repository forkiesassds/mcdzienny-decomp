namespace MCDzienny
{
    public class HumanoidMob : Mob
    {
        public HumanoidMob(Level level, float x, float y, float z)
            : base(level)
        {
            modelName = "humanoid";
            setPos(x, y, z);
        }
    }
}