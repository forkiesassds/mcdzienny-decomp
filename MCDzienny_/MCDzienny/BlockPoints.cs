namespace MCDzienny
{
    // Token: 0x0200039D RID: 925
    public class BlockPoints
    {
        // Token: 0x04000E86 RID: 3718
        public byte blockType;

        // Token: 0x04000E85 RID: 3717
        public Vector3 position;

        // Token: 0x06001A55 RID: 6741 RVA: 0x000B9AAC File Offset: 0x000B7CAC
        public BlockPoints(Vector3 position, byte blockType)
        {
            this.position = position;
            this.blockType = blockType;
        }
    }
}