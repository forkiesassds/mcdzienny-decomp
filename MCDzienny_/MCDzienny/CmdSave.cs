namespace MCDzienny
{
    public class CmdSave : Command
    {
        public override string name { get { return "save"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override void Use(Player p, string message)
        {
            if (message.ToLower() == "lava")
            {
                Player.SendMessage(p, "There's a new command for saving lava maps - /savelavamap");
            }
            else if (message.ToLower() == "all")
            {
                foreach (Level level3 in Server.levels)
                {
                    try
                    {
                        level3.Save();
                    }
                    catch {}
                }
                Player.GlobalMessage("All levels have been saved.");
            }
            else if (message.IndexOf(' ') == -1 && message != "")
            {
                Level level = Level.Find(message);
                if (level != null)
                {
                    level.Save(Override: true);
                    Player.SendMessage(p, string.Format("Level \"{0}\" saved.", level.name));
                    int num = p.level.Backup(Forced: true);
                    if (p != null && num != -1)
                    {
                        p.level.ChatLevel(string.Format("Backup {0} saved.", num));
                    }
                }
                else
                {
                    Player.SendMessage(p, "Could not find level specified");
                }
            }
            else if (message.Split(' ').Length == 2)
            {
                Level level2 = Level.Find(message.Split(' ')[0]);
                string text = message.Split(' ')[1].ToLower();
                if (level2 != null)
                {
                    level2.Save(Override: true);
                    level2.Backup(true, text);
                    Player.GlobalMessage(string.Format("{0} had a backup created named &b{1}", level2.name, text));
                }
                else
                {
                    Player.SendMessage(p, "Could not find level specified");
                }
            }
            else if (p == null)
            {
                Use(p, "all");
            }
            else
            {
                p.level.Save(Override: true);
                Player.SendMessage(p, string.Format("Level \"{0}\" saved.", p.level.name));
                int num2 = p.level.Backup(Forced: true);
                if (num2 != -1)
                {
                    p.level.ChatLevel(string.Format("Backup {0} saved.", num2));
                }
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/save - Saves the level you are currently in");
            Player.SendMessage(p, "/save all - Saves all loaded levels.");
            Player.SendMessage(p, "/save <map> - Saves the specified map.");
            Player.SendMessage(p, "/save <map> <name> - Backups the map with a given restore name");
        }
    }
}