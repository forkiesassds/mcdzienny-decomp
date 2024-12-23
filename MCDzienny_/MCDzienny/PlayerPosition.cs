namespace MCDzienny
{
    public class PlayerPosition
    {

        public int RotX;

        public int RotY;
        public int X;

        public int Y;

        public int Z;

        public PlayerPosition() {}

        public PlayerPosition(int X, int Y, int Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }

        public PlayerPosition(int X, int Y, int Z, int RotX, int RotY)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            this.RotX = RotX;
            this.RotY = RotY;
        }
    }
}