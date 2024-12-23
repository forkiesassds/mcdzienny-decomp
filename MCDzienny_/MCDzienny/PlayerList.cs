using System;
using System.Collections.Generic;
using System.IO;

namespace MCDzienny
{
    public sealed class PlayerList
    {

        readonly List<string> players = new List<string>();
        public Group group;

        public void Add(string p)
        {
            players.Add(p.ToLower());
        }

        public bool Remove(string p)
        {
            return players.Remove(p.ToLower());
        }

        public bool Contains(string p)
        {
            return players.Contains(p.ToLower());
        }

        public List<string> All()
        {
            return new List<string>(players);
        }

        public void Save(string path)
        {
            Save(path, console: true);
        }

        public void Save()
        {
            Save(group.fileName);
        }

        public void Save(string path, bool console)
        {
            StreamWriter file = File.CreateText("ranks/" + path);
            try
            {
                players.ForEach(delegate(string p) { file.WriteLine(p); });
            }
            finally
            {
                if (file != null)
                {
                    ((IDisposable)file).Dispose();
                }
            }
            if (console)
            {
                Server.s.Log("SAVED: " + path);
            }
        }

        public static PlayerList Load(string path, Group groupName)
        {
            if (!Directory.Exists("ranks"))
            {
                Directory.CreateDirectory("ranks");
            }
            path = "ranks/" + path;
            PlayerList playerList = new PlayerList();
            playerList.group = groupName;
            if (File.Exists(path))
            {
                string[] array = File.ReadAllLines(path);
                foreach (string p in array)
                {
                    playerList.Add(p);
                }
            }
            else
            {
                File.Create(path).Close();
                Server.s.Log("CREATED NEW: " + path);
            }
            return playerList;
        }
    }
}