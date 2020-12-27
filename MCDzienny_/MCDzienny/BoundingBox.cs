using System;
using System.Collections.Generic;
using System.Linq;

namespace MCDzienny
{
    // Token: 0x020001B0 RID: 432
    public struct BoundingBox
    {
        // Token: 0x06000C54 RID: 3156 RVA: 0x00047FB8 File Offset: 0x000461B8
        public BoundingBox(Vector3 min, Vector3 max)
        {
            Min = new Vector3(min);
            Max = new Vector3(max);
        }

        // Token: 0x06000C55 RID: 3157 RVA: 0x00047FD4 File Offset: 0x000461D4
        public BoundingBox(params Vector3[] vertices)
        {
            float x;
            var num = x = vertices[0].X;
            float y;
            var num2 = y = vertices[0].Y;
            float z;
            var num3 = z = vertices[0].Z;
            foreach (var v in vertices)
                if (!(v == vertices.First()))
                {
                    if (v.X > x)
                        x = v.X;
                    else if (v.X < num) num = v.X;
                    if (v.Y > y)
                        y = v.Y;
                    else if (v.Y < num2) num2 = v.Y;
                    if (v.Z > z)
                        z = v.Z;
                    else if (v.Z < num3) num3 = v.Z;
                }

            Min = new Vector3(num, num2, num3);
            Max = new Vector3(x, y, z);
        }

        // Token: 0x06000C56 RID: 3158 RVA: 0x000480E0 File Offset: 0x000462E0
        public List<Vector3> BoxOutline()
        {
            var hashSet = new HashSet<Vector3>();
            foreach (var edge in new[]
            {
                new Edge(Min, new Vector3(Min.X, Min.Y, Max.Z)),
                new Edge(Min, new Vector3(Min.X, Max.Y, Min.Z)),
                new Edge(Min, new Vector3(Max.X, Min.Y, Min.Z)),
                new Edge(Max, new Vector3(Min.X, Max.Y, Max.Z)),
                new Edge(Max, new Vector3(Max.X, Max.Y, Min.Z)),
                new Edge(Max, new Vector3(Max.X, Min.Y, Max.Z)),
                new Edge(new Vector3(Min.X, Max.Y, Max.Z), new Vector3(Min.X, Max.Y, Min.Z)),
                new Edge(new Vector3(Min.X, Max.Y, Max.Z), new Vector3(Min.X, Min.Y, Max.Z)),
                new Edge(new Vector3(Max.X, Min.Y, Max.Z), new Vector3(Min.X, Min.Y, Max.Z)),
                new Edge(new Vector3(Max.X, Min.Y, Max.Z), new Vector3(Max.X, Min.Y, Min.Z)),
                new Edge(new Vector3(Max.X, Max.Y, Min.Z), new Vector3(Max.X, Min.Y, Min.Z)),
                new Edge(new Vector3(Max.X, Max.Y, Min.Z), new Vector3(Min.X, Max.Y, Min.Z))
            })
                hashSet.UnionWith(DottedLine(edge));
            return hashSet.ToList();
        }

        // Token: 0x06000C57 RID: 3159 RVA: 0x000484D0 File Offset: 0x000466D0
        private HashSet<Vector3> DottedLine(Edge edge)
        {
            return DottedLine(edge.point1, edge.point2);
        }

        // Token: 0x06000C58 RID: 3160 RVA: 0x000484E8 File Offset: 0x000466E8
        private bool IsEven(int value)
        {
            return value % 2 == 0;
        }

        // Token: 0x06000C59 RID: 3161 RVA: 0x000484F4 File Offset: 0x000466F4
        private bool IsOdd(int value)
        {
            return value % 2 != 0;
        }

        // Token: 0x06000C5A RID: 3162 RVA: 0x00048500 File Offset: 0x00046700
        private HashSet<Vector3> DottedLine(Vector3 vec1, Vector3 vec2)
        {
            var hashSet = new HashSet<Vector3>();
            if (vec1.X - vec2.X != 0f)
            {
                var num = Math.Max(vec1.X, vec2.X);
                var num2 = Math.Min(vec1.X, vec2.X);
                var num3 = num2;
                if (!IsEven((int) vec1.Y) || !IsEven((int) vec1.Z))
                    if (!IsOdd((int) vec1.Y) || !IsOdd((int) vec1.Z))
                    {
                        while (num3 < num)
                        {
                            if (IsEven((int) num3)) hashSet.Add(new Vector3(num3, vec1.Y, vec1.Z));
                            num3 += 1f;
                        }

                        return hashSet;
                    }

                while (num3 < num)
                {
                    if (IsOdd((int) num3)) hashSet.Add(new Vector3(num3, vec1.Y, vec1.Z));
                    num3 += 1f;
                }
            }
            else if (vec1.Y - vec2.Y != 0f)
            {
                var num4 = Math.Max(vec1.Y, vec2.Y);
                var num5 = Math.Min(vec1.Y, vec2.Y);
                var num6 = num5;
                if (!IsEven((int) vec1.X) || !IsEven((int) vec1.Z))
                    if (!IsOdd((int) vec1.X) || !IsOdd((int) vec1.Z))
                    {
                        while (num6 < num4)
                        {
                            if (IsEven((int) num6)) hashSet.Add(new Vector3(vec1.X, num6, vec1.Z));
                            num6 += 1f;
                        }

                        return hashSet;
                    }

                while (num6 < num4)
                {
                    if (IsOdd((int) num6)) hashSet.Add(new Vector3(vec1.X, num6, vec1.Z));
                    num6 += 1f;
                }
            }
            else if (vec1.Z - vec2.Z != 0f)
            {
                var num7 = Math.Max(vec1.Z, vec2.Z);
                var num8 = Math.Min(vec1.Z, vec2.Z);
                var num9 = num8;
                if (!IsEven((int) vec1.X) || !IsEven((int) vec1.Y))
                    if (!IsOdd((int) vec1.X) || !IsOdd((int) vec1.Y))
                    {
                        while (num9 < num7)
                        {
                            if (IsEven((int) num9)) hashSet.Add(new Vector3(vec1.X, vec1.Y, num9));
                            num9 += 1f;
                        }

                        return hashSet;
                    }

                while (num9 < num7)
                {
                    if (IsOdd((int) num9)) hashSet.Add(new Vector3(vec1.X, vec1.Y, num9));
                    num9 += 1f;
                }
            }

            return hashSet;
        }

        // Token: 0x04000672 RID: 1650
        public Vector3 Min;

        // Token: 0x04000673 RID: 1651
        public Vector3 Max;

        // Token: 0x020001B1 RID: 433
        private struct Edge
        {
            // Token: 0x06000C5B RID: 3163 RVA: 0x00048800 File Offset: 0x00046A00
            public Edge(Vector3 point1, Vector3 point2)
            {
                this.point1 = point1;
                this.point2 = point2;
            }

            // Token: 0x04000674 RID: 1652
            public readonly Vector3 point1;

            // Token: 0x04000675 RID: 1653
            public readonly Vector3 point2;
        }
    }
}