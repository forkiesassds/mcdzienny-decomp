using System.IO;

namespace MCDzienny
{
    class CmdRestore : Command
    {
        public override string name { get { return "restore"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            if (message != "")
            {
                Server.s.Log(Server.backupLocation + "/" + p.level.name + "/" + message + "/" + p.level.name + ".lvl");
                if (File.Exists(Server.backupLocation + "/" + p.level.name + "/" + message + "/" + p.level.name + ".lvl"))
                {
                    try
                    {
                        File.Copy(Server.backupLocation + "/" + p.level.name + "/" + message + "/" + p.level.name + ".lvl", "levels/" + p.level.name + ".lvl",
                                  overwrite: true);
                        Level level = Level.Load(p.level.name);
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
                            File.Copy("levels/" + p.level.name + ".lvl.backup", "levels/" + p.level.name + ".lvl", overwrite: true);
                        }
                        return;
                    }
                    catch
                    {
                        Server.s.Log("Restore fail");
                        return;
                    }
                }
                Player.SendMessage(p, string.Format("Backup {0} does not exist.", message));
            }
            else if (Directory.Exists(Server.backupLocation + "/" + p.level.name))
            {
                string[] directories = Directory.GetDirectories(Server.backupLocation + "/" + p.level.name);
                int num = directories.Length;
                Player.SendMessage(p, string.Format("{0} has {1} backups .", p.level.name, num));
                bool flag = false;
                string text = "";
                string[] array = directories;
                foreach (string text2 in array)
                {
                    string text3 = text2.Substring(text2.LastIndexOf('\\') + 1);
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

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/restore <number> - restores a previous backup of the current map");
        }
    }
}