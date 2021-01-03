namespace MCDzienny
{
    // Token: 0x0200004F RID: 79
    public class HumanoidMob : Mob
    {
        // Token: 0x060001F2 RID: 498 RVA: 0x0000B854 File Offset: 0x00009A54
        public HumanoidMob(Level level, float x, float y, float z) : base(level)
        {
            modelName = "humanoid";
            setPos(x, y, z);
        }
    }
}