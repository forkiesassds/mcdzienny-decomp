namespace MCDzienny
{
    public class AABB
    {
        readonly float epsilon;

        public float x0;

        public float x1;

        public float y0;

        public float y1;

        public float z0;

        public float z1;

        public AABB(float x0, float y0, float z0, float x1, float y1, float z1)
        {
            epsilon = 0f;
            this.x0 = x0;
            this.y0 = y0;
            this.z0 = z0;
            this.x1 = x1;
            this.y1 = y1;
            this.z1 = z1;
        }

        public AABB expand(float f, float f1, float f2)
        {
            float num = x0;
            float num2 = y0;
            float num3 = z0;
            float num4 = x1;
            float num5 = y1;
            float num6 = z1;
            if (f < 0f)
            {
                num += f;
            }
            if (f > 0f)
            {
                num4 += f;
            }
            if (f1 < 0f)
            {
                num2 += f1;
            }
            if (f1 > 0f)
            {
                num5 += f1;
            }
            if (f2 < 0f)
            {
                num3 += f2;
            }
            if (f2 > 0f)
            {
                num6 += f2;
            }
            return new AABB(num, num2, num3, num4, num5, num6);
        }

        public AABB grow(float f, float f1, float f2)
        {
            float num = x0 - f;
            float num2 = y0 - f1;
            float num3 = z0 - f2;
            f = x1 + f;
            f1 = y1 + f1;
            f2 = z1 + f2;
            return new AABB(num, num2, num3, f, f1, f2);
        }

        public AABB cloneMove(float f, float f1, float f2)
        {
            return new AABB(x0 + f2, y0 + f1, z0 + f2, x1 + f, y1 + f1, z1 + f2);
        }

        public float clipXCollide(AABB aabb, float f)
        {
            if (aabb.y1 <= y0 || aabb.y0 >= y1)
            {
                return f;
            }
            if (aabb.z1 <= z0 || aabb.z0 >= z1)
            {
                return f;
            }
            float num;
            if (f > 0f && aabb.x1 <= x0 && (num = x0 - aabb.x1 - epsilon) < f)
            {
                f = num;
            }
            if (f < 0f && aabb.x0 >= x1 && (num = x1 - aabb.x0 + epsilon) > f)
            {
                f = num;
            }
            return f;
        }

        public float clipYCollide(AABB aabb, float f)
        {
            if (aabb.x1 <= x0 || aabb.x0 >= x1)
            {
                return f;
            }
            if (aabb.z1 <= z0 || aabb.z0 >= z1)
            {
                return f;
            }
            float num;
            if (f > 0f && aabb.y1 <= y0 && (num = y0 - aabb.y1 - epsilon) < f)
            {
                f = num;
            }
            if (f < 0f && aabb.y0 >= y1 && (num = y1 - aabb.y0 + epsilon) > f)
            {
                f = num;
            }
            return f;
        }

        public float clipZCollide(AABB aabb, float f)
        {
            if (aabb.x1 <= x0 || aabb.x0 >= x1)
            {
                return f;
            }
            if (aabb.y1 <= y0 || aabb.y0 >= y1)
            {
                return f;
            }
            float num;
            if (f > 0f && aabb.z1 <= z0 && (num = z0 - aabb.z1 - epsilon) < f)
            {
                f = num;
            }
            if (f < 0f && aabb.z0 >= z1 && (num = z1 - aabb.z0 + epsilon) > f)
            {
                f = num;
            }
            return f;
        }

        public bool intersects(AABB aabb)
        {
            if (aabb.x1 <= x0 || aabb.x0 >= x1)
            {
                return false;
            }
            if (aabb.y1 <= y0 || aabb.y0 >= y1)
            {
                return false;
            }
            if (aabb.z1 > z0)
            {
                return aabb.z0 < z1;
            }
            return false;
        }

        public bool intersectsInner(AABB aabb)
        {
            if (aabb.x1 < x0 || aabb.x0 > x1)
            {
                return false;
            }
            if (aabb.y1 < y0 || aabb.y0 > y1)
            {
                return false;
            }
            if (aabb.z1 >= z0)
            {
                return aabb.z0 <= z1;
            }
            return false;
        }

        public void move(float xd, float yd, float zd)
        {
            x0 += xd;
            y0 += yd;
            z0 += zd;
            x1 += xd;
            y1 += yd;
            z1 += zd;
        }

        public bool intersects(float f, float f1, float f2, float f3, float f4, float f5)
        {
            if (f3 <= x0 || f >= x1)
            {
                return false;
            }
            if (f4 <= y0 || f1 >= y1)
            {
                return false;
            }
            if (f5 > z0)
            {
                return f2 < z1;
            }
            return false;
        }

        public float getSize()
        {
            float num = x1 - x0;
            float num2 = y1 - y0;
            float num3 = z1 - z0;
            return (num + num2 + num3) / 3f;
        }

        public AABB shrink(float f, float f1, float f2)
        {
            float num = x0;
            float num2 = y0;
            float num3 = z0;
            float num4 = x1;
            float num5 = y1;
            float num6 = z1;
            if (f < 0f)
            {
                num -= f;
            }
            if (f > 0f)
            {
                num4 -= f;
            }
            if (f1 < 0f)
            {
                num2 -= f1;
            }
            if (f1 > 0f)
            {
                num5 -= f1;
            }
            if (f2 < 0f)
            {
                num3 -= f2;
            }
            if (f2 > 0f)
            {
                num6 -= f2;
            }
            return new AABB(num, num2, num3, num4, num5, num6);
        }

        public AABB copy()
        {
            return new AABB(x0, y0, z0, x1, y1, z1);
        }
    }
}