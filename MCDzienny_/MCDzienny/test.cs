using System;
using System.Collections.Generic;

namespace MCDzienny
{
    // Token: 0x0200013C RID: 316
    public static class test
    {
        // Token: 0x0600096F RID: 2415 RVA: 0x0002E6F0 File Offset: 0x0002C8F0
        public static IEnumerable<T> TakeHighest<T>(this IEnumerable<T> source, int n) where T : IComparable<T>
        {
            var list = new List<T>(n);
            using (var enumerator = source.GetEnumerator())
            {
                for (var i = 0; i < n; i++)
                {
                    if (!enumerator.MoveNext()) return list;
                    list.Add(enumerator.Current);
                }

                list.Sort();
                while (enumerator.MoveNext())
                {
                    var t = enumerator.Current;
                    if (t.CompareTo(list[0]) > 0)
                    {
                        list.RemoveAt(0);
                        list.Add(enumerator.Current);
                    }
                }
            }

            return list;
        }

        // Token: 0x06000970 RID: 2416 RVA: 0x0002E794 File Offset: 0x0002C994
        public static IEnumerable<double> TopNSorted(this IEnumerable<double> source, int n)
        {
            var list = new List<double>(n + 1);
            using (var enumerator = source.GetEnumerator())
            {
                for (var i = 0; i < n; i++)
                {
                    if (!enumerator.MoveNext()) throw new InvalidOperationException("Not enough elements");
                    list.Add(enumerator.Current);
                }

                list.Sort();
                while (enumerator.MoveNext())
                {
                    var item = enumerator.Current;
                    var num = list.BinarySearch(item);
                    if (num < 0) num = ~num;
                    if (num < n)
                    {
                        list.Insert(num, item);
                        list.RemoveAt(n);
                    }
                }
            }

            return list;
        }
    }
}