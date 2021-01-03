using System;
using System.IO;

namespace MCDzienny.Commands
{
    // Token: 0x020000D5 RID: 213
    public class CmdRenameLvl : Command
    {
        // Token: 0x1700030A RID: 778
        // (get) Token: 0x060006EA RID: 1770 RVA: 0x000235B4 File Offset: 0x000217B4
        public override string name
        {
            get { return "renamelvl"; }
        }

        // Token: 0x1700030B RID: 779
        // (get) Token: 0x060006EB RID: 1771 RVA: 0x000235BC File Offset: 0x000217BC
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x1700030C RID: 780
        // (get) Token: 0x060006EC RID: 1772 RVA: 0x000235C4 File Offset: 0x000217C4
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x1700030D RID: 781
        // (get) Token: 0x060006ED RID: 1773 RVA: 0x000235CC File Offset: 0x000217CC
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x1700030E RID: 782
        // (get) Token: 0x060006EE RID: 1774 RVA: 0x000235D0 File Offset: 0x000217D0
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Admin; }
        }

        // Token: 0x060006F0 RID: 1776 RVA: 0x000235DC File Offset: 0x000217DC
        public override void Use(Player p, string message)
        {
            if (message == "" || message.IndexOf(' ') == -1)
            {
                Help(p);
                return;
            }

            message = message.Replace("'", "");
            var level = Level.Find(message.Split(' ')[0]);
            if (level == null)
            {
                Player.SendMessage(p, "The map couldn't be found.");
                return;
            }

            var text = message.Split(' ')[1];
            if (level.mapType == MapType.Lava)
            {
                Player.SendMessage(p, "You can't rename lava map this way.");
                return;
            }

            if (File.Exists("levels/" + text))
            {
                Player.SendMessage(p, "Level already exists.");
                return;
            }

            if (level == Server.mainLevel)
            {
                Player.SendMessage(p, "Cannot rename the main level.");
                return;
            }

            if (level != null) level.Unload();
            try
            {
                File.Move("levels/" + level.name + ".lvl", "levels/" + text + ".lvl");
                try
                {
                    File.Move("levels/level properties/" + level.name + ".properties",
                        "levels/level properties/" + text + ".properties");
                }
                catch
                {
                }

                try
                {
                    File.Move("levels/level properties/" + level.name,
                        "levels/level properties/" + text + ".properties");
                }
                catch
                {
                }

                DBInterface.ExecuteQuery(string.Concat("RENAME TABLE `Block", level.name.ToLower(), "` TO `Block",
                    text.ToLower(), "`, `Portals", level.name.ToLower(), "` TO `Portals", text.ToLower(),
                    "`, `Messages", level.name.ToLower(), "` TO Messages", text.ToLower(), ", `Zone",
                    level.name.ToLower(), "` TO `Zone", text.ToLower(), "`"));
                Player.GlobalMessage(string.Format("Renamed {0} to {1}", level.name, text));
            }
            catch (Exception ex)
            {
                Player.SendMessage(p, "Error when renaming.");
                Server.ErrorLog(ex);
            }
        }

        // Token: 0x060006F1 RID: 1777 RVA: 0x00023880 File Offset: 0x00021A80
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/renamelvl <level> <new name> - Renames <level> to <new name>");
            Player.SendMessage(p, "Portals going to <level> will be lost");
        }
    }
}