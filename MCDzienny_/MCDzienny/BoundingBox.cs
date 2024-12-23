using System;
using System.Collections.Generic;
using System.Linq;

namespace MCDzienny
{
    public struct BoundingBox
    {
        struct Edge
        {
            public readonly Vector3 point1;

            public readonly Vector3 point2;

            public Edge(Vector3 point1, Vector3 point2)
            {
                this.point1 = point1;
                this.point2 = point2;
            }
        }

        public Vector3 Min;

        public Vector3 Max;

        public BoundingBox(Vector3 min, Vector3 max)
        {
            Min = new Vector3(min);
            Max = new Vector3(max);
        }

        public BoundingBox(params Vector3[] vertices)
        {
            float x;
            float num = x = vertices[0].X;
            float y;
            float num2 = y = vertices[0].Y;
            float z;
            float num3 = z = vertices[0].Z;
            for (int i = 0; i < vertices.Length; i++)
            {
                Vector3 vector = vertices[i];
                if (!(vector == vertices.First()))
                {
                    if (vector.X > num)
                    {
                        num = vector.X;
                    }
                    else if (vector.X < x)
                    {
                        x = vector.X;
                    }
                    if (vector.Y > num2)
                    {
                        num2 = vector.Y;
                    }
                    else if (vector.Y < y)
                    {
                        y = vector.Y;
                    }
                    if (vector.Z > num3)
                    {
                        num3 = vector.Z;
                    }
                    else if (vector.Z < z)
                    {
                        z = vector.Z;
                    }
                }
            }
            Min = new Vector3(x, y, z);
            Max = new Vector3(num, num2, num3);
        }

        public List<Vector3> BoxOutline()
        {
            var hashSet = new HashSet<Vector3>();
            var array = new Edge[12]
            {
                new Edge(Min, new Vector3(Min.X, Min.Y, Max.Z)), new Edge(Min, new Vector3(Min.X, Max.Y, Min.Z)), new Edge(Min, new Vector3(Max.X, Min.Y, Min.Z)),
                new Edge(Max, new Vector3(Min.X, Max.Y, Max.Z)), new Edge(Max, new Vector3(Max.X, Max.Y, Min.Z)), new Edge(Max, new Vector3(Max.X, Min.Y, Max.Z)),
                new Edge(new Vector3(Min.X, Max.Y, Max.Z), new Vector3(Min.X, Max.Y, Min.Z)), new Edge(new Vector3(Min.X, Max.Y, Max.Z), new Vector3(Min.X, Min.Y, Max.Z)),
                new Edge(new Vector3(Max.X, Min.Y, Max.Z), new Vector3(Min.X, Min.Y, Max.Z)), new Edge(new Vector3(Max.X, Min.Y, Max.Z), new Vector3(Max.X, Min.Y, Min.Z)),
                new Edge(new Vector3(Max.X, Max.Y, Min.Z), new Vector3(Max.X, Min.Y, Min.Z)), new Edge(new Vector3(Max.X, Max.Y, Min.Z), new Vector3(Min.X, Max.Y, Min.Z))
            };
            foreach (Edge edge in array)
            {
                hashSet.UnionWith(DottedLine(edge));
            }
            return hashSet.ToList();
        }

        HashSet<Vector3> DottedLine(Edge edge)
        {
            return DottedLine(edge.point1, edge.point2);
        }

        bool IsEven(int value)
        {
            if (value % 2 == 0)
            {
                return true;
            }
            return false;
        }

        bool IsOdd(int value)
        {
            if (value % 2 != 0)
            {
                return true;
            }
            return false;
        }

        HashSet<Vector3> DottedLine(Vector3 vec1, Vector3 vec2)
        {
            var hashSet = new HashSet<Vector3>();
            if (vec1.X - vec2.X != 0f)
            {
                float num = Math.Max(vec1.X, vec2.X);
                float num2 = Math.Min(vec1.X, vec2.X);
                float num3 = num2;
                if (IsEven((int)vec1.Y) && IsEven((int)vec1.Z) || IsOdd((int)vec1.Y) && IsOdd((int)vec1.Z))
                {
                    for (; num3 < num; num3 += 1f)
                    {
                        if (IsOdd((int)num3))
                        {
                            hashSet.Add(new Vector3(num3, vec1.Y, vec1.Z));
                        }
                    }
                }
                else
                {
                    for (; num3 < num; num3 += 1f)
                    {
                        if (IsEven((int)num3))
                        {
                            hashSet.Add(new Vector3(num3, vec1.Y, vec1.Z));
                        }
                    }
                }
            }
            else if (vec1.Y - vec2.Y != 0f)
            {
                float num4 = Math.Max(vec1.Y, vec2.Y);
                float num5 = Math.Min(vec1.Y, vec2.Y);
                float num6 = num5;
                if (IsEven((int)vec1.X) && IsEven((int)vec1.Z) || IsOdd((int)vec1.X) && IsOdd((int)vec1.Z))
                {
                    for (; num6 < num4; num6 += 1f)
                    {
                        if (IsOdd((int)num6))
                        {
                            hashSet.Add(new Vector3(vec1.X, num6, vec1.Z));
                        }
                    }
                }
                else
                {
                    for (; num6 < num4; num6 += 1f)
                    {
                        if (IsEven((int)num6))
                        {
                            hashSet.Add(new Vector3(vec1.X, num6, vec1.Z));
                        }
                    }
                }
            }
            else if (vec1.Z - vec2.Z != 0f)
            {
                float num7 = Math.Max(vec1.Z, vec2.Z);
                float num8 = Math.Min(vec1.Z, vec2.Z);
                float num9 = num8;
                if (IsEven((int)vec1.X) && IsEven((int)vec1.Y) || IsOdd((int)vec1.X) && IsOdd((int)vec1.Y))
                {
                    for (; num9 < num7; num9 += 1f)
                    {
                        if (IsOdd((int)num9))
                        {
                            hashSet.Add(new Vector3(vec1.X, vec1.Y, num9));
                        }
                    }
                }
                else
                {
                    for (; num9 < num7; num9 += 1f)
                    {
                        if (IsEven((int)num9))
                        {
                            hashSet.Add(new Vector3(vec1.X, vec1.Y, num9));
                        }
                    }
                }
            }
            return hashSet;
        }
    }
}