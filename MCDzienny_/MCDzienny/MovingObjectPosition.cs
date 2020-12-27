namespace MCDzienny
{
    // Token: 0x02000164 RID: 356
    public class MovingObjectPosition
    {
        // Token: 0x040004A1 RID: 1185
        public Entity entity;

        // Token: 0x0400049B RID: 1179
        public int entityPos;

        // Token: 0x0400049F RID: 1183
        public int face;

        // Token: 0x040004A0 RID: 1184
        public Vector3F vec;

        // Token: 0x0400049C RID: 1180
        public int x;

        // Token: 0x0400049D RID: 1181
        public int y;

        // Token: 0x0400049E RID: 1182
        public int z;

        // Token: 0x06000A0E RID: 2574 RVA: 0x00035034 File Offset: 0x00033234
        public MovingObjectPosition(Entity entity)
        {
            entityPos = 1;
            this.entity = entity;
        }

        // Token: 0x06000A0F RID: 2575 RVA: 0x0003504C File Offset: 0x0003324C
        public MovingObjectPosition(int x, int y, int z, int side, Vector3F blockPos)
        {
            entityPos = 0;
            this.x = x;
            this.y = y;
            this.z = z;
            face = side;
            vec = new Vector3F(blockPos.X, blockPos.Y, blockPos.Z);
        }
    }
}