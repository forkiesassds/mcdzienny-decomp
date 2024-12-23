namespace MCDzienny
{
    public class Position
    {

        public bool HasPosition;

        public bool HasRotation;

        public float Pitch;
        public float X;

        public float Y;

        public float Yaw;

        public float Z;

        public Position(float yaw, float pitch)
        {
            Yaw = yaw;
            Pitch = pitch;
            HasRotation = true;
            HasPosition = false;
        }

        public Position(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
            HasPosition = true;
            HasRotation = false;
        }

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