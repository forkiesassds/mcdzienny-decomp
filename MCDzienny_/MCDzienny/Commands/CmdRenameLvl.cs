using System;
using System.IO;

namespace MCDzienny.Commands
{
    public class CmdRenameLvl : Command
    {
        public override string name { get { return "renamelvl"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }

        public override void Use(Player p, string message)
        {
            if (message == "" || message.IndexOf(' ') == -1)
            {
                Help(p);
                return;
            }
            message = message.Replace("'", "");
            Level level = Level.Find(message.Split(' ')[0]);
            if (level == null)
            {
                Player.SendMessage(p, "The map couldn't be found.");
                return;
            }
            string text = message.Split(' ')[1];
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
            level.Unload();
            try
            {
                File.Move("levels/" + level.name + ".lvl", "levels/" + text + ".lvl");
                try
                {
                    File.Move("levels/level properties/" + level.name + ".properties", "levels/level properties/" + text + ".properties");
                }
                catch {}
                try
                {
                    File.Move("levels/level properties/" + level.name, "levels/level properties/" + text + ".properties");
                }
                catch {}
                DBInterface.ExecuteQuery("RENAME TABLE `Block" + level.name.ToLower() + "` TO `Block" + text.ToLower() + "`, `Portals" + level.name.ToLower() +
                                         "` TO `Portals" + text.ToLower() + "`, `Messages" + level.name.ToLower() + "` TO Messages" + text.ToLower() + ", `Zone" +
                                         level.name.ToLower() + "` TO `Zone" + text.ToLower() + "`");
                Player.GlobalMessage(string.Format("Renamed {0} to {1}", level.name, text));
            }
            catch (Exception ex)
            {
                Player.SendMessage(p, "Error when renaming.");
                Server.ErrorLog(ex);
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/renamelvl <level> <new name> - Renames <level> to <new name>");
            Player.SendMessage(p, "Portals going to <level> will be lost");
        }
    }
}