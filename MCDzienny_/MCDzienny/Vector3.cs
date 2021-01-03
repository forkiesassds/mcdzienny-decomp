using System;

namespace MCDzienny
{
    // Token: 0x020001CF RID: 463
    public struct Vector3 : IEquatable<Vector3>
    {
        // Token: 0x06000CEB RID: 3307 RVA: 0x0004A584 File Offset: 0x00048784
        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        // Token: 0x06000CEC RID: 3308 RVA: 0x0004A59C File Offset: 0x0004879C
        public Vector3(Vector3 copy)
        {
            X = copy.X;
            Y = copy.Y;
            Z = copy.Z;
        }

        // Token: 0x170004EE RID: 1262
        // (get) Token: 0x06000CED RID: 3309 RVA: 0x0004A5C8 File Offset: 0x000487C8
        public float Length
        {
            get { return (float) Math.Sqrt(X * X + Y * Y + Z * Z); }
        }

        // Token: 0x170004EF RID: 1263
        // (get) Token: 0x06000CEE RID: 3310 RVA: 0x0004A5FC File Offset: 0x000487FC
        public float LengthSquared
        {
            get { return X * X + Y * Y + Z * Z; }
        }

        // Token: 0x170004F0 RID: 1264
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
                        return;
                    case 1:
                        Y = value;
                        return;
                    default:
                        Z = value;
                        return;
                }
            }
        }

        // Token: 0x06000CF1 RID: 3313 RVA: 0x0004A694 File Offset: 0x00048894
        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        // Token: 0x06000CF2 RID: 3314 RVA: 0x0004A6C8 File Offset: 0x000488C8
        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        // Token: 0x06000CF3 RID: 3315 RVA: 0x0004A6FC File Offset: 0x000488FC
        public static Vector3 operator *(Vector3 v1, float scalar)
        {
            return new Vector3(v1.X * scalar, v1.Y * scalar, v1.Z * scalar);
        }

        // Token: 0x06000CF4 RID: 3316 RVA: 0x0004A720 File Offset: 0x00048920
        public static Vector3 operator *(float scalar, Vector3 v1)
        {
            return new Vector3(v1.X * scalar, v1.Y * scalar, v1.Z * scalar);
        }

        // Token: 0x06000CF5 RID: 3317 RVA: 0x0004A744 File Offset: 0x00048944
        public static Vector3 operator /(Vector3 v1, float scalar)
        {
            return new Vector3(v1.X / scalar, v1.Y / scalar, v1.Z / scalar);
        }

        // Token: 0x06000CF6 RID: 3318 RVA: 0x0004A768 File Offset: 0x00048968
        public override bool Equals(object obj)
        {
            return obj is Vector3 && Equals((Vector3) obj);
        }

        // Token: 0x06000CF7 RID: 3319 RVA: 0x0004A780 File Offset: 0x00048980
        public bool Equals(Vector3 obj)
        {
            return X == obj.X && Y == obj.Y && Z == obj.Z;
        }

        // Token: 0x06000CF8 RID: 3320 RVA: 0x0004A7B4 File Offset: 0x000489B4
        public static bool operator ==(Vector3 v1, Vector3 v2)
        {
            return v1.Equals(v2);
        }

        // Token: 0x06000CF9 RID: 3321 RVA: 0x0004A7C0 File Offset: 0x000489C0
        public static bool operator !=(Vector3 v1, Vector3 v2)
        {
            return !v1.Equals(v2);
        }

        // Token: 0x06000CFA RID: 3322 RVA: 0x0004A7D0 File Offset: 0x000489D0
        public override int GetHashCode()
        {
            var num = 17;
            var num2 = 37345323;
            return (int) (((num * num2 + X) * num2 + Y) * num2 + Z);
        }

        // Token: 0x06000CFB RID: 3323 RVA: 0x0004A808 File Offset: 0x00048A08
        public float Dot(Vector3 vec)
        {
            return X * vec.X + Y * vec.Y + Z * vec.Z;
        }

        // Token: 0x06000CFC RID: 3324 RVA: 0x0004A838 File Offset: 0x00048A38
        public Vector3 Cross(Vector3 vec)
        {
            return new Vector3(Y * vec.Z - Z * vec.Y, Z * vec.X - X * vec.Z, X * vec.Y - Y * vec.X);
        }

        // Token: 0x06000CFD RID: 3325 RVA: 0x0004A8A4 File Offset: 0x00048AA4
        public Vector3 Negate()
        {
            return new Vector3(X * -1f, Y * -1f, Z * -1f);
        }

        // Token: 0x06000CFE RID: 3326 RVA: 0x0004A8D0 File Offset: 0x00048AD0
        public Vector3 Abs()
        {
            return new Vector3(Math.Abs(X), Math.Abs(Y), Math.Abs(Z));
        }

        // Token: 0x06000CFF RID: 3327 RVA: 0x0004A8F8 File Offset: 0x00048AF8
        public Vector3 Normalize()
        {
            if (X == 0f && Y == 0f && Z == 0f) return ZERO;
            var num = Math.Sqrt(X * (double) X + Y * (double) Y + Z * (double) Z);
            return new Vector3((float) (X / num), (float) (Y / num), (float) (Z / num));
        }

        // Token: 0x06000D00 RID: 3328 RVA: 0x0004A98C File Offset: 0x00048B8C
        public float AngleBetween(Vector3 otherVector)
        {
            return Dot(otherVector) / (Length * otherVector.Length);
        }

        // Token: 0x06000D01 RID: 3329 RVA: 0x0004A9A4 File Offset: 0x00048BA4
        public override string ToString()
        {
            return string.Format("({0}, {1}, {2})", X, Y, Z);
        }

        // Token: 0x040006B2 RID: 1714
        public static readonly Vector3 ZERO = new Vector3(0f, 0f, 0f);

        // Token: 0x040006B3 RID: 1715
        public static readonly Vector3 UNIT_X = new Vector3(1f, 0f, 0f);

        // Token: 0x040006B4 RID: 1716
        public static readonly Vector3 UNIT_Y = new Vector3(0f, 1f, 0f);

        // Token: 0x040006B5 RID: 1717
        public static readonly Vector3 UNIT_Z = new Vector3(0f, 0f, 1f);

        // Token: 0x040006B6 RID: 1718
        public float X;

        // Token: 0x040006B7 RID: 1719
        public float Y;

        // Token: 0x040006B8 RID: 1720
        public float Z;
    }
}