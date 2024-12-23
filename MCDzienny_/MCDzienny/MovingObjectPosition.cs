namespace MCDzienny
{
    public class MovingObjectPosition
    {

        public Entity entity;
        public int entityPos;

        public int face;

        public Vector3F vec;

        public int x;

        public int y;

        public int z;

        public MovingObjectPosition(Entity entity)
        {
            entityPos = 1;
            this.entity = entity;
        }

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