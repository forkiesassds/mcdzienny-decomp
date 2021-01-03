using System;
using System.IO;

namespace MCDzienny
{
    // Token: 0x020002E4 RID: 740
    public class CmdDeleteLvl : Command
    {
        // Token: 0x1700082B RID: 2091
        // (get) Token: 0x060014F2 RID: 5362 RVA: 0x000742EC File Offset: 0x000724EC
        public override string name
        {
            get { return "deletelvl"; }
        }

        // Token: 0x1700082C RID: 2092
        // (get) Token: 0x060014F3 RID: 5363 RVA: 0x000742F4 File Offset: 0x000724F4
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x1700082D RID: 2093
        // (get) Token: 0x060014F4 RID: 5364 RVA: 0x000742FC File Offset: 0x000724FC
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x1700082E RID: 2094
        // (get) Token: 0x060014F5 RID: 5365 RVA: 0x00074304 File Offset: 0x00072504
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x1700082F RID: 2095
        // (get) Token: 0x060014F6 RID: 5366 RVA: 0x00074308 File Offset: 0x00072508
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Admin; }
        }

        // Token: 0x060014F7 RID: 5367 RVA: 0x0007430C File Offset: 0x0007250C
        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }

            message.Replace("'", "");
            var level = Level.Find(message);
            if (level != null) level.Unload();
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
                if (!Directory.Exists("levels/deleted")) Directory.CreateDirectory("levels/deleted");
                if (File.Exists("levels/" + message + ".lvl"))
                {
                    if (File.Exists("levels/deleted/" + message + ".lvl"))
                    {
                        var num = 0;
                        while (File.Exists(string.Concat("levels/deleted/", message, num, ".lvl"))) num++;
                        File.Move("levels/" + message + ".lvl", string.Concat("levels/deleted/", message, num, ".lvl"));
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
                    catch
                    {
                    }

                    try
                    {
                        File.Delete("levels/level properties/" + message);
                    }
                    catch
                    {
                    }

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

        // Token: 0x060014F8 RID: 5368 RVA: 0x00074584 File Offset: 0x00072784
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/deletelvl [map] - Completely deletes [map] (portals, MBs, everything");
            Player.SendMessage(p, "A backup of the map will be placed in the levels/deleted folder");
        }
    }
}