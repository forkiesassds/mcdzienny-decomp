using System;
using System.IO;

namespace MCDzienny
{
    // Token: 0x02000303 RID: 771
    public class CmdView : Command
    {
        // Token: 0x170008AB RID: 2219
        // (get) Token: 0x060015CC RID: 5580 RVA: 0x00077D50 File Offset: 0x00075F50
        public override string name
        {
            get { return "view"; }
        }

        // Token: 0x170008AC RID: 2220
        // (get) Token: 0x060015CD RID: 5581 RVA: 0x00077D58 File Offset: 0x00075F58
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170008AD RID: 2221
        // (get) Token: 0x060015CE RID: 5582 RVA: 0x00077D60 File Offset: 0x00075F60
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x170008AE RID: 2222
        // (get) Token: 0x060015CF RID: 5583 RVA: 0x00077D68 File Offset: 0x00075F68
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170008AF RID: 2223
        // (get) Token: 0x060015D0 RID: 5584 RVA: 0x00077D6C File Offset: 0x00075F6C
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Banned; }
        }

        // Token: 0x060015D2 RID: 5586 RVA: 0x00077D78 File Offset: 0x00075F78
        public override void Use(Player p, string message)
        {
            if (!Directory.Exists("extra/text/")) Directory.CreateDirectory("extra/text");
            if (!(message == ""))
            {
                Player player = null;
                if (message.IndexOf(' ') != -1)
                {
                    player = Player.Find(message.Split(' ')[message.Split(' ').Length - 1]);
                    if (player != null) message = message.Substring(0, message.LastIndexOf(' '));
                }

                if (player == null) player = p;
                if (File.Exists("extra/text/" + message + ".txt"))
                    try
                    {
                        var array = File.ReadAllLines("extra/text/" + message + ".txt");
                        if (array[0][0] == '#')
                        {
                            if (Group.Find(array[0].Substring(1)).Permission <= p.group.Permission)
                                for (var i = 1; i < array.Length; i++)
                                    Player.SendMessage(player, array[i]);
                            else
                                Player.SendMessage(p, "You cannot view this file");
                        }
                        else
                        {
                            for (var j = 1; j < array.Length; j++) Player.SendMessage(player, array[j]);
                        }

                        return;
                    }
                    catch
                    {
                        Player.SendMessage(p, "An error occurred when retrieving the file");
                        return;
                    }

                Player.SendMessage(p, "File specified doesn't exist");
                return;
            }

            var directoryInfo = new DirectoryInfo("extra/text/");
            var text = "";
            foreach (var fileInfo in directoryInfo.GetFiles("*.txt"))
                try
                {
                    var text2 = File.ReadAllLines("extra/text/" +
                                                  fileInfo.Name.Substring(0,
                                                      fileInfo.Name.Length - fileInfo.Extension.Length) + ".txt")[0];
                    if (p != null && text2[0] == '#')
                    {
                        if (Group.Find(text2.Substring(1)).Permission <= p.group.Permission)
                            text = text + ", " +
                                   fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length);
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

            if (text == "")
            {
                Player.SendMessage(p, "No files are viewable by you");
                return;
            }

            Player.SendMessage(p, "Available files:");
            Player.SendMessage(p, text.Remove(0, 2));
        }

        // Token: 0x060015D3 RID: 5587 RVA: 0x00078038 File Offset: 0x00076238
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/view [file] [player] - Views [file]'s contents");
            Player.SendMessage(p, "/view by itself will list all files you can view");
            Player.SendMessage(p, "If [player] is give, that player is shown the file");
        }
    }
}