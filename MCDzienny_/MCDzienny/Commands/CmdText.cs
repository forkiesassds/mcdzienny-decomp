using System;
using System.IO;

namespace MCDzienny
{
    // Token: 0x02000302 RID: 770
    public class CmdText : Command
    {
        // Token: 0x170008A6 RID: 2214
        // (get) Token: 0x060015C4 RID: 5572 RVA: 0x00077B00 File Offset: 0x00075D00
        public override string name
        {
            get { return "text"; }
        }

        // Token: 0x170008A7 RID: 2215
        // (get) Token: 0x060015C5 RID: 5573 RVA: 0x00077B08 File Offset: 0x00075D08
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170008A8 RID: 2216
        // (get) Token: 0x060015C6 RID: 5574 RVA: 0x00077B10 File Offset: 0x00075D10
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x170008A9 RID: 2217
        // (get) Token: 0x060015C7 RID: 5575 RVA: 0x00077B18 File Offset: 0x00075D18
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170008AA RID: 2218
        // (get) Token: 0x060015C8 RID: 5576 RVA: 0x00077B1C File Offset: 0x00075D1C
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.AdvBuilder; }
        }

        // Token: 0x060015CA RID: 5578 RVA: 0x00077B28 File Offset: 0x00075D28
        public override void Use(Player p, string message)
        {
            if (!Directory.Exists("extra/text/")) Directory.CreateDirectory("extra/text");
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
                }
                else
                {
                    var flag = false;
                    var path = "extra/text/" + message.Split(' ')[0] + ".txt";
                    var name = Group.findPerm(LevelPermission.Guest).name;
                    if (Group.Find(message.Split(' ')[1]) != null)
                    {
                        name = Group.Find(message.Split(' ')[1]).name;
                        flag = true;
                    }

                    message = message.Substring(message.IndexOf(' ') + 1);
                    if (flag) message = message.Substring(message.IndexOf(' ') + 1);
                    var text = message;
                    if (text == "")
                    {
                        Help(p);
                    }
                    else
                    {
                        if (!File.Exists(path))
                            text = "#" + name + Environment.NewLine + text;
                        else
                            text = " " + text;
                        File.AppendAllText(path, text);
                        Player.SendMessage(p, "Added text");
                    }
                }
            }
            catch
            {
                Help(p);
            }
        }

        // Token: 0x060015CB RID: 5579 RVA: 0x00077D20 File Offset: 0x00075F20
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/text [file] [rank] [message] - Makes a /view-able text");
            Player.SendMessage(p, "The [rank] entered is the minimum rank to view the file");
            Player.SendMessage(p, "The [message] is entered into the text file");
            Player.SendMessage(p, "If the file already exists, text will be added to the end");
        }
    }
}