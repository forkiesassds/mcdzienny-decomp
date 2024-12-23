using System;
using System.Collections.Generic;
using System.IO;

namespace MCDzienny
{
    public class CmdUnloaded : Command
    {
        public override string name { get { return "unloaded"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "information"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }

        public override void Use(Player p, string message)
        {
            try
            {
                var list = new List<string>(Server.levels.Count);
                string text = "";
                int num = 0;
                int num2 = 0;
                if (message != "")
                {
                    try
                    {
                        num2 = int.Parse(message) * 50;
                        num = num2 - 50;
                    }
                    catch
                    {
                        Help(p);
                        return;
                    }
                }
                DirectoryInfo directoryInfo = new DirectoryInfo("levels/");
                FileInfo[] files = directoryInfo.GetFiles("*.lvl");
                foreach (Level level in Server.levels)
                {
                    list.Add(level.name.ToLower());
                }
                if (num2 == 0)
                {
                    FileInfo[] array = files;
                    foreach (FileInfo fileInfo in array)
                    {
                        if (!list.Contains(fileInfo.Name.Replace(".lvl", "").ToLower()))
                        {
                            text = text + ", " + fileInfo.Name.Replace(".lvl", "");
                        }
                    }
                    if (text != "")
                    {
                        Player.SendMessage(p, "Unloaded levels: ");
                        Player.SendMessage(p, "&4" + text.Remove(0, 2));
                        if (files.Length > 50)
                        {
                            Player.SendMessage(p, "For a more structured list, use /unloaded <1/2/3/..>");
                        }
                    }
                    else
                    {
                        Player.SendMessage(p, "No maps are unloaded");
                    }
                    return;
                }
                if (num2 > files.Length)
                {
                    num2 = files.Length;
                }
                if (num > files.Length)
                {
                    Player.SendMessage(p, string.Format("No maps beyond number {0}", files.Length));
                    return;
                }
                Player.SendMessage(p, string.Format("Unloaded levels ({0} to {1}):", num, num2));
                for (int j = num; j < num2; j++)
                {
                    if (!list.Contains(files[j].Name.Replace(".lvl", "").ToLower()))
                    {
                        text = text + ", " + files[j].Name.Replace(".lvl", "");
                    }
                }
                if (text != "")
                {
                    Player.SendMessage(p, "&4" + text.Remove(0, 2));
                }
                else
                {
                    Player.SendMessage(p, "No maps are unloaded");
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
                Player.SendMessage(p, "An error occured");
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/unloaded - Lists all unloaded levels.");
            Player.SendMessage(p, "/unloaded <1/2/3/..> - Shows a compact list.");
        }
    }
}