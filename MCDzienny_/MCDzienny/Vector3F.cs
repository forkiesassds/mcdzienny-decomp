using System;

namespace MCDzienny
{
    // Token: 0x0200039E RID: 926
    public class Vector3F
    {
        // Token: 0x04000E87 RID: 3719
        public float X;

        // Token: 0x04000E88 RID: 3720
        public float Y;

        // Token: 0x04000E89 RID: 3721
        public float Z;

        // Token: 0x06001A56 RID: 6742 RVA: 0x000B9AC4 File Offset: 0x000B7CC4
        public Vector3F(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        // Token: 0x06001A57 RID: 6743 RVA: 0x000B9AE4 File Offset: 0x000B7CE4
        public Vector3F add(float x, float y, float z)
        {
            return new Vector3F(X + x, Y + y, Z + z);
        }

        // Token: 0x06001A58 RID: 6744 RVA: 0x000B9B04 File Offset: 0x000B7D04
        public float distance(Vector3F other)
        {
            var num = other.X - X;
            var num2 = other.Y - Y;
            var num3 = other.Z - Z;
            return (float) Math.Sqrt(num * num + num2 * num2 + num3 * num3);
        }

        // Token: 0x06001A59 RID: 6745 RVA: 0x000B9B50 File Offset: 0x000B7D50
        public float distanceSquared(Vector3F other)
        {
            var num = other.X - X;
            var num2 = other.Y - Y;
            var num3 = other.Z - Z;
            return num * num + num2 * num2 + num3 * num3;
        }

        // Token: 0x06001A5A RID: 6746 RVA: 0x000B9B94 File Offset: 0x000B7D94
        public Vector3F getXIntersection(Vector3F other, float x)
        {
            var num = other.X - X;
            var num2 = other.Y - Y;
            var num3 = other.Z - Z;
            if (num * num < 1E-07f) return null;
            if ((x = (x - X) / num) < 0f || x > 1f) return null;
            return new Vector3F(X + num * x, Y + num2 * x, Z + num3 * x);
        }

        // Token: 0x06001A5B RID: 6747 RVA: 0x000B9C18 File Offset: 0x000B7E18
        public Vector3F getYIntersection(Vector3F other, float y)
        {
            var num = other.X - X;
            var num2 = other.Y - Y;
            var num3 = other.Z - Z;
            if (num2 * num2 < 1E-07f) return null;
            if ((y = (y - Y) / num2) < 0f || y > 1f) return null;
            return new Vector3F(X + num * y, Y + num2 * y, Z + num3 * y);
        }

        // Token: 0x06001A5C RID: 6748 RVA: 0x000B9C9C File Offset: 0x000B7E9C
        public Vector3F getZIntersection(Vector3F other, float z)
        {
            var num = other.X - X;
            var num2 = other.Y - Y;
            var num3 = other.Z - Z;
            if (num3 * num3 < 1E-07f) return null;
            if ((z = (z - Z) / num3) < 0f || z > 1f) return null;
            return new Vector3F(X + num * z, Y + num2 * z, Z + num3 * z);
        }

        // Token: 0x06001A5D RID: 6749 RVA: 0x000B9D20 File Offset: 0x000B7F20
        public Vector3F normalize()
        {
            var num = (float) Math.Sqrt(X * X + Y * Y + Z * Z);
            return new Vector3F(X / num, Y / num, Z / num);
        }

        // Token: 0x06001A5E RID: 6750 RVA: 0x000B9D7C File Offset: 0x000B7F7C
        public Vector3F subtract(Vector3F other)
        {
            return new Vector3F(X - other.X, Y - other.Y, Z - other.Z);
        }

        // Token: 0x06001A5F RID: 6751 RVA: 0x000B9DAC File Offset: 0x000B7FAC
        public string toString()
        {
            return string.Concat("(", X, ", ", Y, ", ", Z, ")");
        }
    }
}