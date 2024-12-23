using System;

namespace MCDzienny
{
    public struct Vector3 : IEquatable<Vector3>
    {
        public static readonly Vector3 ZERO = new Vector3(0f, 0f, 0f);

        public static readonly Vector3 UNIT_X = new Vector3(1f, 0f, 0f);

        public static readonly Vector3 UNIT_Y = new Vector3(0f, 1f, 0f);

        public static readonly Vector3 UNIT_Z = new Vector3(0f, 0f, 1f);

        public float X;

        public float Y;

        public float Z;

        public float Length { get { return (float)Math.Sqrt(X * X + Y * Y + Z * Z); } }

        public float LengthSquared { get { return X * X + Y * Y + Z * Z; } }

        public float this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0:
                        return X;
                    case 1:
                        return Y;
                    default:
                        return Z;
                }
            }
            set
            {
                switch (i)
                {
                    case 0:
                        X = value;
                        break;
                    case 1:
                        Y = value;
                        break;
                    default:
                        Z = value;
                        break;
                }
            }
        }

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3(Vector3 copy)
        {
            X = copy.X;
            Y = copy.Y;
            Z = copy.Z;
        }

        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        public static Vector3 operator *(Vector3 v1, float scalar)
        {
            return new Vector3(v1.X * scalar, v1.Y * scalar, v1.Z * scalar);
        }

        public static Vector3 operator *(float scalar, Vector3 v1)
        {
            return new Vector3(v1.X * scalar, v1.Y * scalar, v1.Z * scalar);
        }

        public static Vector3 operator /(Vector3 v1, float scalar)
        {
            return new Vector3(v1.X / scalar, v1.Y / scalar, v1.Z / scalar);
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector3)
            {
                return Equals((Vector3)obj);
            }
            return false;
        }

        public bool Equals(Vector3 obj)
        {
            if (X == obj.X && Y == obj.Y)
            {
                return Z == obj.Z;
            }
            return false;
        }

        public static bool operator ==(Vector3 v1, Vector3 v2)
        {
            return v1.Equals(v2);
        }

        public static bool operator !=(Vector3 v1, Vector3 v2)
        {
            return !v1.Equals(v2);
        }

        public override int GetHashCode()
        {
            int num = 17;
            int num2 = 37345323;
            return (int)(((num * num2 + X) * num2 + Y) * num2 + Z);
        }

        public float Dot(Vector3 vec)
        {
            return X * vec.X + Y * vec.Y + Z * vec.Z;
        }

        public Vector3 Cross(Vector3 vec)
        {
            return new Vector3(Y * vec.Z - Z * vec.Y, Z * vec.X - X * vec.Z, X * vec.Y - Y * vec.X);
        }

        public Vector3 Negate()
        {
            return new Vector3(X * -1f, Y * -1f, Z * -1f);
        }

        public Vector3 Abs()
        {
            return new Vector3(Math.Abs(X), Math.Abs(Y), Math.Abs(Z));
        }

        public Vector3 Normalize()
        {
            if (X == 0f && Y == 0f && Z == 0f)
            {
                return ZERO;
            }
            double num = Math.Sqrt(X * (double)X + Y * (double)Y + Z * (double)Z);
            return new Vector3((float)(X / num), (float)(Y / num), (float)(Z / num));
        }

        public float AngleBetween(Vector3 otherVector)
        {
            return Dot(otherVector) / (Length * otherVector.Length);
        }

        public override string ToString()
        {
            return string.Format("({0}, {1}, {2})", X, Y, Z);
        }
    }
}