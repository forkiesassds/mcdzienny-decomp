using System;
using System.Collections.Generic;
using System.IO;

namespace MCDzienny
{
    // Token: 0x02000291 RID: 657
    public class CmdRetrieve : Command
    {
        // Token: 0x04000941 RID: 2369
        public List<CopyOwner> list = new List<CopyOwner>();

        // Token: 0x17000707 RID: 1799
        // (get) Token: 0x060012D6 RID: 4822 RVA: 0x00067E8C File Offset: 0x0006608C
        public override string name
        {
            get { return "retrieve"; }
        }

        // Token: 0x17000708 RID: 1800
        // (get) Token: 0x060012D7 RID: 4823 RVA: 0x00067E94 File Offset: 0x00066094
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000709 RID: 1801
        // (get) Token: 0x060012D8 RID: 4824 RVA: 0x00067E9C File Offset: 0x0006609C
        public override string type
        {
            get { return "build"; }
        }

        // Token: 0x1700070A RID: 1802
        // (get) Token: 0x060012D9 RID: 4825 RVA: 0x00067EA4 File Offset: 0x000660A4
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x1700070B RID: 1803
        // (get) Token: 0x060012DA RID: 4826 RVA: 0x00067EA8 File Offset: 0x000660A8
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.AdvBuilder; }
        }

        // Token: 0x060012DC RID: 4828 RVA: 0x00067EC0 File Offset: 0x000660C0
        public override void Use(Player p, string message)
        {
            try
            {
                if (!File.Exists("extra/copy/index.copydb"))
                {
                    Player.SendMessage(p, "No copy index found! Save something before trying to retrieve it!");
                }
                else if (message == "")
                {
                    Help(p);
                }
                else
                {
                    if (message.Split(' ')[0] == "info")
                    {
                        if (message.IndexOf(' ') != -1)
                        {
                            message = message.Split(' ')[1];
                            if (!File.Exists("extra/copy/" + message + ".copy")) goto IL_DC;
                            using (var streamReader =
                                new StreamReader(File.OpenRead("extra/copy/" + message + ".copy")))
                            {
                                var message2 = streamReader.ReadLine();
                                Player.SendMessage(p, message2);
                                return;
                            }
                        }

                        Help(p);
                        return;
                    }

                    IL_DC:
                    if (message.Split(' ')[0] == "find")
                    {
                        message = message.Replace("find", "");
                        var text = "";
                        var num = 0;
                        var num2 = 0;
                        var num3 = 0;
                        var flag = int.TryParse(message, out num2);
                        if (!(message == ""))
                        {
                            if (!flag)
                            {
                                message = message.Trim();
                                list.Clear();
                                foreach (var text2 in File.ReadAllLines("extra/copy/index.copydb"))
                                {
                                    var copyOwner = new CopyOwner();
                                    copyOwner.file = text2.Split(' ')[0];
                                    copyOwner.name = text2.Split(' ')[1];
                                    list.Add(copyOwner);
                                }

                                new List<CopyOwner>();
                                for (var j = 0; j < list.Count; j++)
                                    if (list[j].name.ToLower() == message.ToLower())
                                        text = text + ", " + list[j].file;
                                if (text == "")
                                {
                                    Player.SendMessage(p, string.Format("No saves found for player: {0}", message));
                                }
                                else
                                {
                                    Player.SendMessage(p, "Saved copy files: ");
                                    Player.SendMessage(p, string.Format("&f {0}", text.Remove(0, 2)));
                                }

                                return;
                            }

                            if (flag)
                            {
                                num = num2 * 50;
                                num3 = num - 50;
                            }
                        }

                        var directoryInfo = new DirectoryInfo("extra/copy/");
                        var files = directoryInfo.GetFiles("*.copy");
                        if (num == 0)
                        {
                            foreach (var fileInfo in files) text = text + ", " + fileInfo.Name.Replace(".copy", "");
                            if (text != "")
                            {
                                Player.SendMessage(p, "Saved copy files: ");
                                Player.SendMessage(p, string.Format("&f {0}", text.Remove(0, 2)));
                                if (files.Length > 50)
                                    Player.SendMessage(p, "For a more structured list, use /retrieve find <1/2/3/...>");
                            }
                            else
                            {
                                Player.SendMessage(p, "There are no saved copies.");
                            }
                        }
                        else
                        {
                            if (num > files.Length) num = files.Length;
                            if (num3 > files.Length)
                            {
                                Player.SendMessage(p, string.Format("No saved copies beyond number {0}", files.Length));
                            }
                            else
                            {
                                Player.SendMessage(p, string.Format("Saved copies ({0} to {1}):", num3, num));
                                for (var l = num3; l < num; l++)
                                    text = text + ", " + files[l].Name.Replace(".copy", "");
                                if (text != "")
                                    Player.SendMessage(p, string.Format("&f{0}", text.Remove(0, 2)));
                                else
                                    Player.SendMessage(p, "There are no saved copies.");
                            }
                        }
                    }
                    else if (message.IndexOf(' ') == -1)
                    {
                        message = message.Split(' ')[0];
                        if (File.Exists("extra/copy/" + message + ".copy"))
                        {
                            p.CopyBuffer.Clear();
                            var flag2 = false;
                            foreach (var text3 in File.ReadAllLines("extra/copy/" + message + ".copy"))
                                if (flag2)
                                {
                                    Player.CopyPos item;
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

                            Player.SendMessage(p,
                                string.Format("&f{0} has been placed copybuffer.  Paste away!",
                                    message + Server.DefaultColor));
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
            }
            catch (Exception ex)
            {
                Player.SendMessage(p, "An error occured");
                Server.ErrorLog(ex);
            }
        }

        // Token: 0x060012DD RID: 4829 RVA: 0x00068488 File Offset: 0x00066688
        public override void Help(Player p)
        {
            Player.SendMessage(p,
                "/retrieve <filename> - Retrieves saved copy file to your copy buffer. /paste to place it!");
            Player.SendMessage(p, "/retrieve info <filename> - Gets information about the saved file.");
            Player.SendMessage(p, "/retrieve find - Prints a list of all saved copies.");
            Player.SendMessage(p, "/retrieve find <1/2/3/..> - Shows a compact list.");
            Player.SendMessage(p, "/retrieve find <name> - Prints a list of all saved copies made by player <name>.");
        }

        // Token: 0x02000292 RID: 658
        public class CopyOwner
        {
            // Token: 0x04000943 RID: 2371
            public string file;

            // Token: 0x04000942 RID: 2370
            public string name;
        }
    }
}