using System;

namespace MCDzienny
{
    public class Vector3F
    {
        public float X;

        public float Y;

        public float Z;

        public Vector3F(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3F add(float x, float y, float z)
        {
            return new Vector3F(X + x, Y + y, Z + z);
        }

        public float distance(Vector3F other)
        {
            float num = other.X - X;
            float num2 = other.Y - Y;
            float num3 = other.Z - Z;
            return (float)Math.Sqrt(num * num + num2 * num2 + num3 * num3);
        }

        public float distanceSquared(Vector3F other)
        {
            float num = other.X - X;
            float num2 = other.Y - Y;
            float num3 = other.Z - Z;
            return num * num + num2 * num2 + num3 * num3;
        }

        public Vector3F getXIntersection(Vector3F other, float x)
        {
            float num = other.X - X;
            float num2 = other.Y - Y;
            float num3 = other.Z - Z;
            if (!(num * num < 1E-07f))
            {
                if (!((x = (x - X) / num) >= 0f) || !(x <= 1f))
                {
                    return null;
                }
                return new Vector3F(X + num * x, Y + num2 * x, Z + num3 * x);
            }
            return null;
        }

        public Vector3F getYIntersection(Vector3F other, float y)
        {
            float num = other.X - X;
            float num2 = other.Y - Y;
            float num3 = other.Z - Z;
            if (!(num2 * num2 < 1E-07f))
            {
                if (!((y = (y - Y) / num2) >= 0f) || !(y <= 1f))
                {
                    return null;
                }
                return new Vector3F(X + num * y, Y + num2 * y, Z + num3 * y);
            }
            return null;
        }

        public Vector3F getZIntersection(Vector3F other, float z)
        {
            float num = other.X - X;
            float num2 = other.Y - Y;
            float num3 = other.Z - Z;
            if (!(num3 * num3 < 1E-07f))
            {
                if (!((z = (z - Z) / num3) >= 0f) || !(z <= 1f))
                {
                    return null;
                }
                return new Vector3F(X + num * z, Y + num2 * z, Z + num3 * z);
            }
            return null;
        }

        public Vector3F normalize()
        {
            float num = (float)Math.Sqrt(X * X + Y * Y + Z * Z);
            return new Vector3F(X / num, Y / num, Z / num);
        }

        public Vector3F subtract(Vector3F other)
        {
            return new Vector3F(X - other.X, Y - other.Y, Z - other.Z);
        }

        public string toString()
        {
            return "(" + X + ", " + Y + ", " + Z + ")";
        }
    }
}