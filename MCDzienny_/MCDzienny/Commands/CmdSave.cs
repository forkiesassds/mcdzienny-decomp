namespace MCDzienny
{
    // Token: 0x02000294 RID: 660
    public class CmdSave : Command
    {
        // Token: 0x17000711 RID: 1809
        // (get) Token: 0x060012E7 RID: 4839 RVA: 0x00068608 File Offset: 0x00066808
        public override string name
        {
            get { return "save"; }
        }

        // Token: 0x17000712 RID: 1810
        // (get) Token: 0x060012E8 RID: 4840 RVA: 0x00068610 File Offset: 0x00066810
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000713 RID: 1811
        // (get) Token: 0x060012E9 RID: 4841 RVA: 0x00068618 File Offset: 0x00066818
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x17000714 RID: 1812
        // (get) Token: 0x060012EA RID: 4842 RVA: 0x00068620 File Offset: 0x00066820
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x17000715 RID: 1813
        // (get) Token: 0x060012EB RID: 4843 RVA: 0x00068624 File Offset: 0x00066824
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x060012ED RID: 4845 RVA: 0x00068630 File Offset: 0x00066830
        public override void Use(Player p, string message)
        {
            if (message.ToLower() == "lava")
            {
                Player.SendMessage(p, "There's a new command for saving lava maps - /savelavamap");
                return;
            }

            if (message.ToLower() == "all")
            {
                foreach (var level in Server.levels)
                    try
                    {
                        level.Save();
                    }
                    catch
                    {
                    }

                Player.GlobalMessage("All levels have been saved.");
                return;
            }

            if (message.IndexOf(' ') == -1 && message != "")
            {
                var level2 = Level.Find(message);
                if (level2 == null)
                {
                    Player.SendMessage(p, "Could not find level specified");
                    return;
                }

                level2.Save(true);
                Player.SendMessage(p, string.Format("Level \"{0}\" saved.", level2.name));
                var num = p.level.Backup(true);
                if (p != null && num != -1)
                {
                    p.level.ChatLevel(string.Format("Backup {0} saved.", num));
                }
            }
            else if (message.Split(' ').Length == 2)
            {
                var level3 = Level.Find(message.Split(' ')[0]);
                var text = message.Split(' ')[1].ToLower();
                if (level3 != null)
                {
                    level3.Save(true);
                    level3.Backup(true, text);
                    Player.GlobalMessage(string.Format("{0} had a backup created named &b{1}", level3.name, text));
                    return;
                }

                Player.SendMessage(p, "Could not find level specified");
            }
            else
            {
                if (p == null)
                {
                    Use(p, "all");
                    return;
                }

                p.level.Save(true);
                Player.SendMessage(p, string.Format("Level \"{0}\" saved.", p.level.name));
                var num2 = p.level.Backup(true);
                if (num2 != -1) p.level.ChatLevel(string.Format("Backup {0} saved.", num2));
            }
        }

        // Token: 0x060012EE RID: 4846 RVA: 0x00068850 File Offset: 0x00066A50
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/save - Saves the level you are currently in");
            Player.SendMessage(p, "/save all - Saves all loaded levels.");
            Player.SendMessage(p, "/save <map> - Saves the specified map.");
            Player.SendMessage(p, "/save <map> <name> - Backups the map with a given restore name");
        }
    }
}