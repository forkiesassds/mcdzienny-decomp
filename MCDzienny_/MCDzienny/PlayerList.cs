using System.Collections.Generic;
using System.IO;

namespace MCDzienny
{
    // Token: 0x0200035C RID: 860
    public sealed class PlayerList
    {
        // Token: 0x04000CE7 RID: 3303
        public Group group;

        // Token: 0x04000CE8 RID: 3304
        private readonly List<string> players = new List<string>();

        // Token: 0x060018AA RID: 6314 RVA: 0x000A7720 File Offset: 0x000A5920
        public void Add(string p)
        {
            players.Add(p.ToLower());
        }

        // Token: 0x060018AB RID: 6315 RVA: 0x000A7734 File Offset: 0x000A5934
        public bool Remove(string p)
        {
            return players.Remove(p.ToLower());
        }

        // Token: 0x060018AC RID: 6316 RVA: 0x000A7748 File Offset: 0x000A5948
        public bool Contains(string p)
        {
            return players.Contains(p.ToLower());
        }

        // Token: 0x060018AD RID: 6317 RVA: 0x000A775C File Offset: 0x000A595C
        public List<string> All()
        {
            return new List<string>(players);
        }

        // Token: 0x060018AE RID: 6318 RVA: 0x000A776C File Offset: 0x000A596C
        public void Save(string path)
        {
            Save(path, true);
        }

        // Token: 0x060018AF RID: 6319 RVA: 0x000A7778 File Offset: 0x000A5978
        public void Save()
        {
            Save(group.fileName);
        }

        // Token: 0x060018B0 RID: 6320 RVA: 0x000A778C File Offset: 0x000A598C
        public void Save(string path, bool console)
        {
            using (var file = File.CreateText("ranks/" + path))
            {
                players.ForEach(delegate(string p) { file.WriteLine(p); });
            }

            if (console) Server.s.Log("SAVED: " + path);
        }

        // Token: 0x060018B1 RID: 6321 RVA: 0x000A7814 File Offset: 0x000A5A14
        public static PlayerList Load(string path, Group groupName)
        {
            if (!Directory.Exists("ranks")) Directory.CreateDirectory("ranks");
            path = "ranks/" + path;
            var playerList = new PlayerList();
            playerList.group = groupName;
            if (File.Exists(path))
            {
                foreach (var p in File.ReadAllLines(path)) playerList.Add(p);
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