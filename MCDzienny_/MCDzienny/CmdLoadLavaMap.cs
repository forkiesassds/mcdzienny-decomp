using System;
using System.IO;

namespace MCDzienny
{
    class CmdLoadLavaMap : Command
    {
        public override string name { get { return "loadlavamap"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override void Use(Player p, string message)
        {
            try
            {
                if (message == "" || message.Split(' ').Length > 2)
                {
                    Help(p);
                    return;
                }
                int num = message.IndexOf(' ');
                string s = "0";
                if (num != -1)
                {
                    s = message.Substring(num + 1).ToLower();
                    message = message.Substring(0, num).ToLower();
                }
                else
                {
                    message = message.ToLower();
                }
                foreach (Level level2 in Server.levels)
                {
                    if (level2.name == message)
                    {
                        Player.SendMessage(p, string.Format("{0} is already loaded!", message));
                        return;
                    }
                }
                if (!File.Exists("lava/maps/" + message + ".lvl"))
                {
                    Player.SendMessage(p, string.Format("Level \"{0}\" doesn't exist!", message));
                    return;
                }
                Level level = null;
                level = Level.Load(message, 3, lavaSurv: true);
                if (level == null)
                {
                    if (!File.Exists("lava/maps/" + message + ".lvl.backup"))
                    {
                        Player.SendMessage(p, string.Format("Backup of {0} does not exist.", message));
                        return;
                    }
                    Server.s.Log("Attempting to load backup.");
                    File.Copy("lava/maps/" + message + ".lvl.backup", "lava/maps/" + message + ".lvl", overwrite: true);
                    level = Level.Load(message, 3, lavaSurv: true);
                    if (level == null)
                    {
                        Player.SendMessage(p, string.Format("Backup of {0} failed.", message));
                        return;
                    }
                }
                if (p != null && level.permissionvisit > p.group.Permission)
                {
                    Player.SendMessage(p, string.Format("This map is for {0} only!", Level.PermissionToName(level.permissionvisit)));
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    return;
                }
                foreach (Level level3 in Server.levels)
                {
                    if (level3.name == message)
                    {
                        Player.SendMessage(p, string.Format("{0} is already loaded!", message));
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        return;
                    }
                }
                Server.AddLevel(level);
                Player.GlobalMessage(string.Format("Level \"{0}\" loaded.", level.name));
                try
                {
                    int.Parse(s);
                    level.setPhysics(3);
                }
                catch
                {
                    Player.SendMessage(p, "Second argument invalid");
                }
            }
            catch (Exception ex)
            {
                Player.GlobalMessage("An error occured with /load");
                Server.ErrorLog(ex);
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/loadlavamap <map_name> - Loads a lava map that is located in 'lava/maps/' directory.");
        }
    }
}