using System.IO;

namespace MCDzienny
{
    // Token: 0x02000290 RID: 656
    internal class CmdRestore : Command
    {
        // Token: 0x17000701 RID: 1793
        // (get) Token: 0x060012CD RID: 4813 RVA: 0x00067A70 File Offset: 0x00065C70
        public override string name
        {
            get { return "restore"; }
        }

        // Token: 0x17000702 RID: 1794
        // (get) Token: 0x060012CE RID: 4814 RVA: 0x00067A78 File Offset: 0x00065C78
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000703 RID: 1795
        // (get) Token: 0x060012CF RID: 4815 RVA: 0x00067A80 File Offset: 0x00065C80
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x17000704 RID: 1796
        // (get) Token: 0x060012D0 RID: 4816 RVA: 0x00067A88 File Offset: 0x00065C88
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x17000705 RID: 1797
        // (get) Token: 0x060012D1 RID: 4817 RVA: 0x00067A8C File Offset: 0x00065C8C
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x17000706 RID: 1798
        // (get) Token: 0x060012D2 RID: 4818 RVA: 0x00067A90 File Offset: 0x00065C90
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x060012D4 RID: 4820 RVA: 0x00067A9C File Offset: 0x00065C9C
        public override void Use(Player p, string message)
        {
            if (message != "")
            {
                Server.s.Log(string.Concat(Server.backupLocation, "/", p.level.name, "/", message, "/", p.level.name,
                    ".lvl"));
                if (File.Exists(string.Concat(Server.backupLocation, "/", p.level.name, "/", message, "/", p.level.name,
                    ".lvl")))
                    try
                    {
                        File.Copy(
                            string.Concat(Server.backupLocation, "/", p.level.name, "/", message, "/", p.level.name,
                                ".lvl"), "levels/" + p.level.name + ".lvl", true);
                        var level = Level.Load(p.level.name);
                        if (level != null)
                        {
                            p.level.spawnx = level.spawnx;
                            p.level.spawny = level.spawny;
                            p.level.spawnz = level.spawnz;
                            p.level.depth = level.depth;
                            p.level.width = level.width;
                            p.level.height = level.height;
                            p.level.blocks = level.blocks;
                            p.level.setPhysics(0);
                            p.level.ClearPhysics();
                            all.Find("reveal").Use(p, "all");
                        }
                        else
                        {
                            Server.s.Log("Restore nulled");
                            File.Copy("levels/" + p.level.name + ".lvl.backup", "levels/" + p.level.name + ".lvl",
                                true);
                        }

                        return;
                    }
                    catch
                    {
                        Server.s.Log("Restore fail");
                        return;
                    }

                Player.SendMessage(p, string.Format("Backup {0} does not exist.", message));
                return;
            }

            if (Directory.Exists(Server.backupLocation + "/" + p.level.name))
            {
                var directories = Directory.GetDirectories(Server.backupLocation + "/" + p.level.name);
                var num = directories.Length;
                Player.SendMessage(p, string.Format("{0} has {1} backups .", p.level.name, num));
                var flag = false;
                var text = "";
                foreach (var text2 in directories)
                {
                    var text3 = text2.Substring(text2.LastIndexOf('\\') + 1);
                    try
                    {
                        int.Parse(text3);
                    }
                    catch
                    {
                        flag = true;
                        text = text + ", " + text3;
                    }
                }

                if (flag)
                {
                    Player.SendMessage(p, "Custom-named restores:");
                    Player.SendMessage(p, "> " + text.Remove(0, 2));
                }
            }
            else
            {
                Player.SendMessage(p, string.Format("{0} has no backups yet.", p.level.name));
            }
        }

        // Token: 0x060012D5 RID: 4821 RVA: 0x00067E7C File Offset: 0x0006607C
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/restore <number> - restores a previous backup of the current map");
        }
    }
}