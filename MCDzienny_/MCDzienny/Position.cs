namespace MCDzienny
{
    // Token: 0x020001F6 RID: 502
    public class Position
    {
        // Token: 0x04000751 RID: 1873
        public bool HasPosition;

        // Token: 0x04000750 RID: 1872
        public bool HasRotation;

        // Token: 0x0400074F RID: 1871
        public float Pitch;

        // Token: 0x0400074B RID: 1867
        public float X;

        // Token: 0x0400074C RID: 1868
        public float Y;

        // Token: 0x0400074E RID: 1870
        public float Yaw;

        // Token: 0x0400074D RID: 1869
        public float Z;

        // Token: 0x06000DDA RID: 3546 RVA: 0x0004E03C File Offset: 0x0004C23C
        public Position(float yaw, float pitch)
        {
            Yaw = yaw;
            Pitch = pitch;
            HasRotation = true;
            HasPosition = false;
        }

        // Token: 0x06000DDB RID: 3547 RVA: 0x0004E060 File Offset: 0x0004C260
        public Position(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
            HasPosition = true;
            HasRotation = false;
        }

        // Token: 0x06000DDC RID: 3548 RVA: 0x0004E08C File Offset: 0x0004C28C
        public Position(float x, float y, float z, float yaw, float pitch)
        {
            X = x;
            Y = y;
            Z = z;
            Yaw = yaw;
            Pitch = pitch;
            HasRotation = true;
            HasPosition = true;
        }
    }
}