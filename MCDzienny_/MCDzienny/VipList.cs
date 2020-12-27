using System.Collections.Generic;
using System.IO;

namespace MCDzienny
{
    // Token: 0x0200039F RID: 927
    internal class VipList
    {
        // Token: 0x04000E8A RID: 3722
        private static readonly List<string> vipList = new List<string>();

        // Token: 0x04000E8B RID: 3723
        private static List<string> vipListLowerCase = new List<string>();

        // Token: 0x04000E8C RID: 3724
        private static readonly object locker = new object();

        // Token: 0x06001A60 RID: 6752 RVA: 0x000B9E10 File Offset: 0x000B8010
        public static void Init()
        {
            if (!File.Exists("ranks/viplist.txt")) File.Create("ranks/viplist.txt").Close();
            var array = File.ReadAllLines("ranks/viplist.txt");
            foreach (var text in array) vipList.Add(text.Trim());
        }

        // Token: 0x06001A61 RID: 6753 RVA: 0x000B9E68 File Offset: 0x000B8068
        public static void Save()
        {
            lock (locker)
            {
                File.WriteAllLines("ranks/viplist.txt", vipList.ToArray());
            }
        }

        // Token: 0x06001A62 RID: 6754 RVA: 0x000B9EB0 File Offset: 0x000B80B0
        public static void AddVIP(string s)
        {
            if (!vipList.Contains(s))
            {
                vipList.Add(s);
                vipList.Sort();
                Save();
            }
        }

        // Token: 0x06001A63 RID: 6755 RVA: 0x000B9EDC File Offset: 0x000B80DC
        public static void RemoveVIP(string s)
        {
            if (vipList.Contains(s))
            {
                vipList.Remove(s);
                Save();
            }
        }

        // Token: 0x06001A64 RID: 6756 RVA: 0x000B9EFC File Offset: 0x000B80FC
        public static string[] GetArray()
        {
            return vipList.ToArray();
        }

        // Token: 0x06001A65 RID: 6757 RVA: 0x000B9F08 File Offset: 0x000B8108
        public static bool IsOnList(string s)
        {
            s = s.ToLower();
            foreach (var text in vipList)
                if (text.ToLower() == s)
                    return true;
            return false;
        }
    }
}