namespace MCDzienny
{
    class CmdSaveLavaMap : Command
    {
        public override string name { get { return "savelavamap"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                if (p == null)
                {
                    Server.s.Log("You cannot use this command from Console");
                    return;
                }
                p.level.mapType = MapType.Lava;
                p.level.Save(Override: true, lavalvl: true);
                Player.SendMessage(p, string.Format("Level \"{0}\" was saved as lava map.", p.level.name));
            }
            else if (message.Split(' ').Length == 1)
            {
                Level level = Level.Find(message);
                if (level != null)
                {
                    level.mapType = MapType.Lava;
                    level.Save(Override: true, lavalvl: true);
                    Player.SendMessage(p, string.Format("Level \"{0}\" saved as lava map.", level.name));
                    int num = p.level.LavaMapBackup();
                    if (num != -1)
                    {
                        p.level.ChatLevel(string.Format("Backup {0} saved.", num));
                    }
                }
                else
                {
                    Player.SendMessage(p, "Could not find level specified.");
                }
            }
            else if (message.Split(' ').Length == 2)
            {
                Level level2 = Level.Find(message.Split(' ')[0]);
                string text = message.Split(' ')[1].ToLower();
                if (level2 != null)
                {
                    p.level.LavaMapBackup(text);
                    Player.GlobalMessage(string.Format("{0} had a backup created named &b{1}", level2.name, text));
                }
                else
                {
                    Player.SendMessage(p, "Could not find level specified");
                }
            }
            else
            {
                Help(p);
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/savelavamap - Saves the level you are currently in.");
            Player.SendMessage(p, "/savelavamap <map> - Saves the specified map.");
            Player.SendMessage(p, "/savelavamap <map> <name> - Saves lava map under given name.");
        }
    }
}