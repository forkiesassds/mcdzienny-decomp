using System;
using System.Collections.Generic;
using System.IO;

namespace MCDzienny
{
    // Token: 0x020002AB RID: 683
    public class CmdUnloaded : Command
    {
        // Token: 0x17000775 RID: 1909
        // (get) Token: 0x06001393 RID: 5011 RVA: 0x0006BC28 File Offset: 0x00069E28
        public override string name
        {
            get { return "unloaded"; }
        }

        // Token: 0x17000776 RID: 1910
        // (get) Token: 0x06001394 RID: 5012 RVA: 0x0006BC30 File Offset: 0x00069E30
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000777 RID: 1911
        // (get) Token: 0x06001395 RID: 5013 RVA: 0x0006BC38 File Offset: 0x00069E38
        public override string type
        {
            get { return "information"; }
        }

        // Token: 0x17000778 RID: 1912
        // (get) Token: 0x06001396 RID: 5014 RVA: 0x0006BC40 File Offset: 0x00069E40
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000779 RID: 1913
        // (get) Token: 0x06001397 RID: 5015 RVA: 0x0006BC44 File Offset: 0x00069E44
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Guest; }
        }

        // Token: 0x06001399 RID: 5017 RVA: 0x0006BC50 File Offset: 0x00069E50
        public override void Use(Player p, string message)
        {
            try
            {
                var list = new List<string>(Server.levels.Count);
                var text = "";
                var num = 0;
                var num2 = 0;
                if (message != "")
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

                var directoryInfo = new DirectoryInfo("levels/");
                var files = directoryInfo.GetFiles("*.lvl");
                foreach (var level in Server.levels) list.Add(level.name.ToLower());
                if (num2 == 0)
                {
                    foreach (var fileInfo in files)
                        if (!list.Contains(fileInfo.Name.Replace(".lvl", "").ToLower()))
                            text = text + ", " + fileInfo.Name.Replace(".lvl", "");
                    if (text != "")
                    {
                        Player.SendMessage(p, "Unloaded levels: ");
                        Player.SendMessage(p, "&4" + text.Remove(0, 2));
                        if (files.Length > 50)
                            Player.SendMessage(p, "For a more structured list, use /unloaded <1/2/3/..>");
                    }
                    else
                    {
                        Player.SendMessage(p, "No maps are unloaded");
                    }
                }
                else
                {
                    if (num2 > files.Length) num2 = files.Length;
                    if (num > files.Length)
                    {
                        Player.SendMessage(p, string.Format("No maps beyond number {0}", files.Length));
                    }
                    else
                    {
                        Player.SendMessage(p, string.Format("Unloaded levels ({0} to {1}):", num, num2));
                        for (var j = num; j < num2; j++)
                            if (!list.Contains(files[j].Name.Replace(".lvl", "").ToLower()))
                                text = text + ", " + files[j].Name.Replace(".lvl", "");
                        if (text != "")
                            Player.SendMessage(p, "&4" + text.Remove(0, 2));
                        else
                            Player.SendMessage(p, "No maps are unloaded");
                    }
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
                Player.SendMessage(p, "An error occured");
            }
        }

        // Token: 0x0600139A RID: 5018 RVA: 0x0006BF00 File Offset: 0x0006A100
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/unloaded - Lists all unloaded levels.");
            Player.SendMessage(p, "/unloaded <1/2/3/..> - Shows a compact list.");
        }
    }
}