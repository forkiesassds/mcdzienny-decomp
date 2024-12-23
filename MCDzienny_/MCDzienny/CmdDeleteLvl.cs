using System;
using System.IO;

namespace MCDzienny
{
    public class CmdDeleteLvl : Command
    {
        public override string name { get { return "deletelvl"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }
            message.Replace("'", "");
            Level level = Level.Find(message);
            if (level != null)
            {
                level.Unload();
            }
            if (level == Server.mainLevel)
            {
                Player.SendMessage(p, "Cannot delete the main level.");
                return;
            }
            if (level != null && level.mapType == MapType.Lava)
            {
                Player.SendMessage(p, "Cannot delete lava map using this command");
                return;
            }
            try
            {
                if (!Directory.Exists("levels/deleted"))
                {
                    Directory.CreateDirectory("levels/deleted");
                }
                if (File.Exists("levels/" + message + ".lvl"))
                {
                    if (File.Exists("levels/deleted/" + message + ".lvl"))
                    {
                        int num = 0;
                        while (File.Exists("levels/deleted/" + message + num + ".lvl"))
                        {
                            num++;
                        }
                        File.Move("levels/" + message + ".lvl", "levels/deleted/" + message + num + ".lvl");
                    }
                    else
                    {
                        File.Move("levels/" + message + ".lvl", "levels/deleted/" + message + ".lvl");
                    }
                    Player.SendMessage(p, "Created backup.");
                    try
                    {
                        File.Delete("levels/level properties/" + message + ".properties");
                    }
                    catch {}
                    try
                    {
                        File.Delete("levels/level properties/" + message);
                    }
                    catch {}
                    DBInterface.ExecuteQuery("DROP TABLE IF EXISTS Block" + message);
                    DBInterface.ExecuteQuery("DROP TABLE IF EXISTS Portals" + message);
                    DBInterface.ExecuteQuery("DROP TABLE IF EXISTS Messages" + message);
                    DBInterface.ExecuteQuery("DROP TABLE IF EXISTS Zone" + message);
                    Player.GlobalMessage(string.Format("Level {0} was deleted.", message));
                    Server.s.Log("Level " + message + " was deleted.");
                }
                else
                {
                    Player.SendMessage(p, "Could not find specified level.");
                }
            }
            catch (Exception ex)
            {
                Player.SendMessage(p, "Error when deleting.");
                Server.ErrorLog(ex);
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/deletelvl [map] - Completely deletes [map] (portals, MBs, everything");
            Player.SendMessage(p, "A backup of the map will be placed in the levels/deleted folder");
        }
    }
}