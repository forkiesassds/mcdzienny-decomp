using System;
using System.IO;

namespace MCDzienny
{
    public class CmdText : Command
    {
        public override string name { get { return "text"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }

        public override void Use(Player p, string message)
        {
            if (!Directory.Exists("extra/text/"))
            {
                Directory.CreateDirectory("extra/text");
            }
            if (message.IndexOf(' ') == -1)
            {
                Help(p);
                return;
            }
            try
            {
                if (message.Split(' ')[0].ToLower() == "delete")
                {
                    if (File.Exists("extra/text/" + message.Split(' ')[1] + ".txt"))
                    {
                        File.Delete("extra/text/" + message.Split(' ')[1] + ".txt");
                        Player.SendMessage(p, "Deleted file");
                    }
                    else
                    {
                        Player.SendMessage(p, "Could not find file specified");
                    }
                    return;
                }
                bool flag = false;
                string path = "extra/text/" + message.Split(' ')[0] + ".txt";
                string text = Group.findPerm(LevelPermission.Guest).name;
                if (Group.Find(message.Split(' ')[1]) != null)
                {
                    text = Group.Find(message.Split(' ')[1]).name;
                    flag = true;
                }
                message = message.Substring(message.IndexOf(' ') + 1);
                if (flag)
                {
                    message = message.Substring(message.IndexOf(' ') + 1);
                }
                string text2 = message;
                if (text2 == "")
                {
                    Help(p);
                    return;
                }
                text2 = File.Exists(path) ? " " + text2 : "#" + text + Environment.NewLine + text2;
                File.AppendAllText(path, text2);
                Player.SendMessage(p, "Added text");
            }
            catch
            {
                Help(p);
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/text [file] [rank] [message] - Makes a /view-able text");
            Player.SendMessage(p, "The [rank] entered is the minimum rank to view the file");
            Player.SendMessage(p, "The [message] is entered into the text file");
            Player.SendMessage(p, "If the file already exists, text will be added to the end");
        }
    }
}