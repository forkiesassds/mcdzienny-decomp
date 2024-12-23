using System.Collections.Generic;
using System.IO;

namespace MCDzienny
{
    class VipList
    {
        static readonly List<string> vipList = new List<string>();

        static List<string> vipListLowerCase = new List<string>();

        static readonly object locker = new object();

        public static void Init()
        {
            if (!File.Exists("ranks/viplist.txt"))
            {
                File.Create("ranks/viplist.txt").Close();
            }
            string[] array = File.ReadAllLines("ranks/viplist.txt");
            string[] array2 = array;
            foreach (string text in array2)
            {
                vipList.Add(text.Trim());
            }
        }

        public static void Save()
        {
            lock (locker)
            {
                File.WriteAllLines("ranks/viplist.txt", vipList.ToArray());
            }
        }

        public static void AddVIP(string s)
        {
            if (!vipList.Contains(s))
            {
                vipList.Add(s);
                vipList.Sort();
                Save();
            }
        }

        public static void RemoveVIP(string s)
        {
            if (vipList.Contains(s))
            {
                vipList.Remove(s);
                Save();
            }
        }

        public static string[] GetArray()
        {
            return vipList.ToArray();
        }

        public static bool IsOnList(string s)
        {
            s = s.ToLower();
            foreach (string vip in vipList)
            {
                if (vip.ToLower() == s)
                {
                    return true;
                }
            }
            return false;
        }
    }
}