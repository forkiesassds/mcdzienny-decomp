using System;
using System.IO;

namespace MCDzienny
{
    public class CmdView : Command
    {
        public override string name { get { return "view"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }

        public override void Use(Player p, string message)
        {
            if (!Directory.Exists("extra/text/"))
            {
                Directory.CreateDirectory("extra/text");
            }
            if (message == "")
            {
                DirectoryInfo directoryInfo = new DirectoryInfo("extra/text/");
                string text = "";
                FileInfo[] files = directoryInfo.GetFiles("*.txt");
                foreach (FileInfo fileInfo in files)
                {
                    try
                    {
                        string text2 = File.ReadAllLines("extra/text/" + fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length) + ".txt")[0];
                        if (p != null && text2[0] == '#')
                        {
                            if (Group.Find(text2.Substring(1)).Permission <= p.group.Permission)
                            {
                                text = text + ", " + fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length);
                            }
                        }
                        else
                        {
                            text = text + ", " + fileInfo.Name.Remove(fileInfo.Name.Length - 4);
                        }
                    }
                    catch (Exception ex)
                    {
                        Server.ErrorLog(ex);
                        Player.SendMessage(p, "Error");
                    }
                }
                if (text == "")
                {
                    Player.SendMessage(p, "No files are viewable by you");
                    return;
                }
                Player.SendMessage(p, "Available files:");
                Player.SendMessage(p, text.Remove(0, 2));
                return;
            }
            Player player = null;
            if (message.IndexOf(' ') != -1)
            {
                player = Player.Find(message.Split(' ')[message.Split(' ').Length - 1]);
                if (player != null)
                {
                    message = message.Substring(0, message.LastIndexOf(' '));
                }
            }
            if (player == null)
            {
                player = p;
            }
            if (File.Exists("extra/text/" + message + ".txt"))
            {
                try
                {
                    string[] array = File.ReadAllLines("extra/text/" + message + ".txt");
                    if (array[0][0] == '#')
                    {
                        if (Group.Find(array[0].Substring(1)).Permission <= p.group.Permission)
                        {
                            for (int j = 1; j < array.Length; j++)
                            {
                                Player.SendMessage(player, array[j]);
                            }
                        }
                        else
                        {
                            Player.SendMessage(p, "You cannot view this file");
                        }
                    }
                    else
                    {
                        for (int k = 1; k < array.Length; k++)
                        {
                            Player.SendMessage(player, array[k]);
                        }
                    }
                    return;
                }
                catch
                {
                    Player.SendMessage(p, "An error occurred when retrieving the file");
                    return;
                }
            }
            Player.SendMessage(p, "File specified doesn't exist");
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/view [file] [player] - Views [file]'s contents");
            Player.SendMessage(p, "/view by itself will list all files you can view");
            Player.SendMessage(p, "If [player] is give, that player is shown the file");
        }
    }
}