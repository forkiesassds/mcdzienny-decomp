using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MCDzienny
{
    public class CmdFixMyMaps : Command
    {
        readonly string MyMapDirectory = "maps" + Path.DirectorySeparatorChar + "mymaps";

        public override string name { get { return "fixmymap"; } }

        public override string shortcut { get { return "fixmymaps"; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Nobody; } }

        public override bool ConsoleAccess { get { return true; } }

        public override void Use(Player p, string message)
        {
            int num = 0;
            foreach (string folder in GetFolders(MyMapDirectory))
            {
                foreach (string folder2 in GetFolders(folder))
                {
                    foreach (string folder3 in GetFolders(folder2))
                    {
                        if (folder3.Length == folder3.LastIndexOf(Path.DirectorySeparatorChar) + 4)
                        {
                            num++;
                            char c2 = folder3[folder3.Length - 1];
                            Directory.Move(folder3, folder3.Substring(0, folder3.Length - 3) + c2);
                        }
                    }
                    if (folder2.Length == folder2.LastIndexOf(Path.DirectorySeparatorChar) + 3)
                    {
                        num++;
                        char c3 = folder2[folder2.Length - 1];
                        Directory.Move(folder2, folder2.Substring(0, folder2.Length - 2) + c3);
                    }
                }
            }
            Player.SendMessage(p, string.Format("All {0} mymaps folders were fixed.", num));
        }

        List<string> GetFolders(string path)
        {
            return Directory.GetDirectories(path).ToList();
        }

        public override void Help(Player p) {}
    }
}