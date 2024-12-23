using System;
using System.Collections.Generic;

namespace MCDzienny
{
    public static class test
    {
        public static IEnumerable<T> TakeHighest<T>(this IEnumerable<T> source, int n) where T : IComparable<T>
        {
            var list = new List<T>(n);
            using (IEnumerator<T> enumerator = source.GetEnumerator())
            {
                for (int i = 0; i < n; i++)
                {
                    if (enumerator.MoveNext())
                    {
                        list.Add(enumerator.Current);
                        continue;
                    }
                    return list;
                }
                list.Sort();
                while (enumerator.MoveNext())
                {
                    if (enumerator.Current.CompareTo(list[0]) > 0)
                    {
                        list.RemoveAt(0);
                        list.Add(enumerator.Current);
                    }
                }
                return list;
            }
        }

        public static IEnumerable<double> TopNSorted(this IEnumerable<double> source, int n)
        {
            var list = new List<double>(n + 1);
            using (IEnumerator<double> enumerator = source.GetEnumerator())
            {
                for (int i = 0; i < n; i++)
                {
                    if (enumerator.MoveNext())
                    {
                        list.Add(enumerator.Current);
                        continue;
                    }
                    throw new InvalidOperationException("Not enough elements");
                }
                list.Sort();
                while (enumerator.MoveNext())
                {
                    double current = enumerator.Current;
                    int num = list.BinarySearch(current);
                    if (num < 0)
                    {
                        num = ~num;
                    }
                    if (num < n)
                    {
                        list.Insert(num, current);
                        list.RemoveAt(n);
                    }
                }
                return list;
            }
        }
    }
}