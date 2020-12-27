namespace MCDzienny
{
    // Token: 0x020000DF RID: 223
    internal class CmdSaveLavaMap : Command
    {
        // Token: 0x17000336 RID: 822
        // (get) Token: 0x06000732 RID: 1842 RVA: 0x00024B1C File Offset: 0x00022D1C
        public override string name
        {
            get { return "savelavamap"; }
        }

        // Token: 0x17000337 RID: 823
        // (get) Token: 0x06000733 RID: 1843 RVA: 0x00024B24 File Offset: 0x00022D24
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000338 RID: 824
        // (get) Token: 0x06000734 RID: 1844 RVA: 0x00024B2C File Offset: 0x00022D2C
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x17000339 RID: 825
        // (get) Token: 0x06000735 RID: 1845 RVA: 0x00024B34 File Offset: 0x00022D34
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x1700033A RID: 826
        // (get) Token: 0x06000736 RID: 1846 RVA: 0x00024B38 File Offset: 0x00022D38
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x06000738 RID: 1848 RVA: 0x00024B44 File Offset: 0x00022D44
        public override void Use(Player p, string message)
        {
            if (!(message == ""))
            {
                if (message.Split(' ').Length == 1)
                {
                    var level = Level.Find(message);
                    if (level == null)
                    {
                        Player.SendMessage(p, "Could not find level specified.");
                        return;
                    }

                    level.mapType = MapType.Lava;
                    level.Save(true, true);
                    Player.SendMessage(p, string.Format("Level \"{0}\" saved as lava map.", level.name));
                    var num = p.level.LavaMapBackup();
                    if (num != -1)
                    {
                        p.level.ChatLevel(string.Format("Backup {0} saved.", num));
                        return;
                    }
                }
                else if (message.Split(' ').Length == 2)
                {
                    var level2 = Level.Find(message.Split(' ')[0]);
                    var text = message.Split(' ')[1].ToLower();
                    if (level2 != null)
                    {
                        p.level.LavaMapBackup(text);
                        Player.GlobalMessage(string.Format("{0} had a backup created named &b{1}", level2.name, text));
                        return;
                    }

                    Player.SendMessage(p, "Could not find level specified");
                    return;
                }
                else
                {
                    Help(p);
                }

                return;
            }

            if (p == null)
            {
                Server.s.Log("You cannot use this command from Console");
                return;
            }

            p.level.mapType = MapType.Lava;
            p.level.Save(true, true);
            Player.SendMessage(p, string.Format("Level \"{0}\" was saved as lava map.", p.level.name));
        }

        // Token: 0x06000739 RID: 1849 RVA: 0x00024CC4 File Offset: 0x00022EC4
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/savelavamap - Saves the level you are currently in.");
            Player.SendMessage(p, "/savelavamap <map> - Saves the specified map.");
            Player.SendMessage(p, "/savelavamap <map> <name> - Saves lava map under given name.");
        }
    }
}