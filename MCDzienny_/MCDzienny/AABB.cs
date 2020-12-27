namespace MCDzienny
{
    // Token: 0x02000167 RID: 359
    public class AABB
    {
        // Token: 0x040004A4 RID: 1188
        private readonly float epsilon;

        // Token: 0x040004A5 RID: 1189
        public float x0;

        // Token: 0x040004A8 RID: 1192
        public float x1;

        // Token: 0x040004A6 RID: 1190
        public float y0;

        // Token: 0x040004A9 RID: 1193
        public float y1;

        // Token: 0x040004A7 RID: 1191
        public float z0;

        // Token: 0x040004AA RID: 1194
        public float z1;

        // Token: 0x06000A1A RID: 2586 RVA: 0x00035308 File Offset: 0x00033508
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

        // Token: 0x06000A1B RID: 2587 RVA: 0x00035348 File Offset: 0x00033548
        public AABB expand(float f, float f1, float f2)
        {
            var num = x0;
            var num2 = y0;
            var num3 = z0;
            var num4 = x1;
            var num5 = y1;
            var num6 = z1;
            if (f < 0f) num += f;
            if (f > 0f) num4 += f;
            if (f1 < 0f) num2 += f1;
            if (f1 > 0f) num5 += f1;
            if (f2 < 0f) num3 += f2;
            if (f2 > 0f) num6 += f2;
            return new AABB(num, num2, num3, num4, num5, num6);
        }

        // Token: 0x06000A1C RID: 2588 RVA: 0x000353DC File Offset: 0x000335DC
        public AABB grow(float f, float f1, float f2)
        {
            var num = x0 - f;
            var num2 = y0 - f1;
            var num3 = z0 - f2;
            f = x1 + f;
            f1 = y1 + f1;
            f2 = z1 + f2;
            return new AABB(num, num2, num3, f, f1, f2);
        }

        // Token: 0x06000A1D RID: 2589 RVA: 0x00035430 File Offset: 0x00033630
        public AABB cloneMove(float f, float f1, float f2)
        {
            return new AABB(x0 + f2, y0 + f1, z0 + f2, x1 + f, y1 + f1, z1 + f2);
        }

        // Token: 0x06000A1E RID: 2590 RVA: 0x00035468 File Offset: 0x00033668
        public float clipXCollide(AABB aabb, float f)
        {
            if (aabb.y1 <= y0 || aabb.y0 >= y1) return f;
            if (aabb.z1 <= z0 || aabb.z0 >= z1) return f;
            float num;
            if (f > 0f && aabb.x1 <= x0 && (num = x0 - aabb.x1 - epsilon) < f) f = num;
            if (f < 0f && aabb.x0 >= x1 && (num = x1 - aabb.x0 + epsilon) > f) f = num;
            return f;
        }

        // Token: 0x06000A1F RID: 2591 RVA: 0x00035518 File Offset: 0x00033718
        public float clipYCollide(AABB aabb, float f)
        {
            if (aabb.x1 <= x0 || aabb.x0 >= x1) return f;
            if (aabb.z1 <= z0 || aabb.z0 >= z1) return f;
            float num;
            if (f > 0f && aabb.y1 <= y0 && (num = y0 - aabb.y1 - epsilon) < f) f = num;
            if (f < 0f && aabb.y0 >= y1 && (num = y1 - aabb.y0 + epsilon) > f) f = num;
            return f;
        }

        // Token: 0x06000A20 RID: 2592 RVA: 0x000355C8 File Offset: 0x000337C8
        public float clipZCollide(AABB aabb, float f)
        {
            if (aabb.x1 <= x0 || aabb.x0 >= x1) return f;
            if (aabb.y1 <= y0 || aabb.y0 >= y1) return f;
            float num;
            if (f > 0f && aabb.z1 <= z0 && (num = z0 - aabb.z1 - epsilon) < f) f = num;
            if (f < 0f && aabb.z0 >= z1 && (num = z1 - aabb.z0 + epsilon) > f) f = num;
            return f;
        }

        // Token: 0x06000A21 RID: 2593 RVA: 0x00035678 File Offset: 0x00033878
        public bool intersects(AABB aabb)
        {
            return aabb.x1 > x0 && aabb.x0 < x1 && aabb.y1 > y0 && aabb.y0 < y1 && aabb.z1 > z0 && aabb.z0 < z1;
        }

        // Token: 0x06000A22 RID: 2594 RVA: 0x000356E0 File Offset: 0x000338E0
        public bool intersectsInner(AABB aabb)
        {
            return aabb.x1 >= x0 && aabb.x0 <= x1 && aabb.y1 >= y0 && aabb.y0 <= y1 && aabb.z1 >= z0 && aabb.z0 <= z1;
        }

        // Token: 0x06000A23 RID: 2595 RVA: 0x0003574C File Offset: 0x0003394C
        public void move(float xd, float yd, float zd)
        {
            x0 += xd;
            y0 += yd;
            z0 += zd;
            x1 += xd;
            y1 += yd;
            z1 += zd;
        }

        // Token: 0x06000A24 RID: 2596 RVA: 0x000357B0 File Offset: 0x000339B0
        public bool intersects(float f, float f1, float f2, float f3, float f4, float f5)
        {
            return f3 > x0 && f < x1 && f4 > y0 && f1 < y1 && f5 > z0 && f2 < z1;
        }

        // Token: 0x06000A25 RID: 2597 RVA: 0x000357FC File Offset: 0x000339FC
        public float getSize()
        {
            var num = x1 - x0;
            var num2 = y1 - y0;
            var num3 = z1 - z0;
            return (num + num2 + num3) / 3f;
        }

        // Token: 0x06000A26 RID: 2598 RVA: 0x00035840 File Offset: 0x00033A40
        public AABB shrink(float f, float f1, float f2)
        {
            var num = x0;
            var num2 = y0;
            var num3 = z0;
            var num4 = x1;
            var num5 = y1;
            var num6 = z1;
            if (f < 0f) num -= f;
            if (f > 0f) num4 -= f;
            if (f1 < 0f) num2 -= f1;
            if (f1 > 0f) num5 -= f1;
            if (f2 < 0f) num3 -= f2;
            if (f2 > 0f) num6 -= f2;
            return new AABB(num, num2, num3, num4, num5, num6);
        }

        // Token: 0x06000A27 RID: 2599 RVA: 0x000358D4 File Offset: 0x00033AD4
        public AABB copy()
        {
            return new AABB(x0, y0, z0, x1, y1, z1);
        }
    }
}