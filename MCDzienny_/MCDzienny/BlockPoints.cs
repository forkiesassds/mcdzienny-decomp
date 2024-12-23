namespace MCDzienny
{
    public class BlockPoints
    {

        public byte blockType;
        public Vector3 position;

        public BlockPoints(Vector3 position, byte blockType)
        {
            this.position = position;
            this.blockType = blockType;
        }
    }
}