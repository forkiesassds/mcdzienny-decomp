using System;
using System.Collections.Generic;
using System.IO;

namespace MCDzienny
{
    public class CmdRetrieve : Command
    {

        public List<CopyOwner> list = new List<CopyOwner>();

        public override string name { get { return "retrieve"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "build"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }

        public override void Use(Player p, string message)
        {
            try
            {
                if (!File.Exists("extra/copy/index.copydb"))
                {
                    Player.SendMessage(p, "No copy index found! Save something before trying to retrieve it!");
                    return;
                }
                if (message == "")
                {
                    Help(p);
                    return;
                }
                if (message.Split(' ')[0] == "info")
                {
                    if (message.IndexOf(' ') == -1)
                    {
                        Help(p);
                        return;
                    }
                    message = message.Split(' ')[1];
                    if (File.Exists("extra/copy/" + message + ".copy"))
                    {
                        using (StreamReader streamReader = new StreamReader(File.OpenRead("extra/copy/" + message + ".copy")))
                        {
                            string message2 = streamReader.ReadLine();
                            Player.SendMessage(p, message2);
                            return;
                        }
                    }
                }
                if (message.Split(' ')[0] == "find")
                {
                    message = message.Replace("find", "");
                    string text = "";
                    int num = 0;
                    int result = 0;
                    int num2 = 0;
                    bool flag = int.TryParse(message, out result);
                    if (!(message == ""))
                    {
                        if (!flag)
                        {
                            message = message.Trim();
                            list.Clear();
                            string[] array = File.ReadAllLines("extra/copy/index.copydb");
                            foreach (string text2 in array)
                            {
                                CopyOwner copyOwner = new CopyOwner();
                                copyOwner.file = text2.Split(' ')[0];
                                copyOwner.name = text2.Split(' ')[1];
                                list.Add(copyOwner);
                            }
                            new List<CopyOwner>();
                            for (int j = 0; j < list.Count; j++)
                            {
                                if (list[j].name.ToLower() == message.ToLower())
                                {
                                    text = text + ", " + list[j].file;
                                }
                            }
                            if (text == "")
                            {
                                Player.SendMessage(p, string.Format("No saves found for player: {0}", message));
                                return;
                            }
                            Player.SendMessage(p, "Saved copy files: ");
                            Player.SendMessage(p, string.Format("&f {0}", text.Remove(0, 2)));
                            return;
                        }
                        if (flag)
                        {
                            num = result * 50;
                            num2 = num - 50;
                        }
                    }
                    DirectoryInfo directoryInfo = new DirectoryInfo("extra/copy/");
                    FileInfo[] files = directoryInfo.GetFiles("*.copy");
                    if (num == 0)
                    {
                        FileInfo[] array2 = files;
                        foreach (FileInfo fileInfo in array2)
                        {
                            text = text + ", " + fileInfo.Name.Replace(".copy", "");
                        }
                        if (text != "")
                        {
                            Player.SendMessage(p, "Saved copy files: ");
                            Player.SendMessage(p, string.Format("&f {0}", text.Remove(0, 2)));
                            if (files.Length > 50)
                            {
                                Player.SendMessage(p, "For a more structured list, use /retrieve find <1/2/3/...>");
                            }
                        }
                        else
                        {
                            Player.SendMessage(p, "There are no saved copies.");
                        }
                        return;
                    }
                    if (num > files.Length)
                    {
                        num = files.Length;
                    }
                    if (num2 > files.Length)
                    {
                        Player.SendMessage(p, string.Format("No saved copies beyond number {0}", files.Length));
                        return;
                    }
                    Player.SendMessage(p, string.Format("Saved copies ({0} to {1}):", num2, num));
                    for (int l = num2; l < num; l++)
                    {
                        text = text + ", " + files[l].Name.Replace(".copy", "");
                    }
                    if (text != "")
                    {
                        Player.SendMessage(p, string.Format("&f{0}", text.Remove(0, 2)));
                    }
                    else
                    {
                        Player.SendMessage(p, "There are no saved copies.");
                    }
                }
                else if (message.IndexOf(' ') == -1)
                {
                    message = message.Split(' ')[0];
                    if (File.Exists("extra/copy/" + message + ".copy"))
                    {
                        p.CopyBuffer.Clear();
                        bool flag2 = false;
                        string[] array3 = File.ReadAllLines("extra/copy/" + message + ".copy");
                        Player.CopyPos item = default(Player.CopyPos);
                        foreach (string text3 in array3)
                        {
                            if (flag2)
                            {
                                item.x = Convert.ToUInt16(text3.Split(' ')[0]);
                                item.y = Convert.ToUInt16(text3.Split(' ')[1]);
                                item.z = Convert.ToUInt16(text3.Split(' ')[2]);
                                item.type = Convert.ToByte(text3.Split(' ')[3]);
                                p.CopyBuffer.Add(item);
                            }
                            else
                            {
                                flag2 = true;
                            }
                        }
                        Player.SendMessage(p, string.Format("&f{0} has been placed copybuffer.  Paste away!", message + Server.DefaultColor));
                    }
                    else
                    {
                        Player.SendMessage(p, "Could not find copy specified");
                    }
                }
                else
                {
                    Help(p);
                }
            }
            catch (Exception ex)
            {
                Player.SendMessage(p, "An error occured");
                Server.ErrorLog(ex);
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/retrieve <filename> - Retrieves saved copy file to your copy buffer. /paste to place it!");
            Player.SendMessage(p, "/retrieve info <filename> - Gets information about the saved file.");
            Player.SendMessage(p, "/retrieve find - Prints a list of all saved copies.");
            Player.SendMessage(p, "/retrieve find <1/2/3/..> - Shows a compact list.");
            Player.SendMessage(p, "/retrieve find <name> - Prints a list of all saved copies made by player <name>.");
        }

        public class CopyOwner
        {

            public string file;
            public string name;
        }
    }
}