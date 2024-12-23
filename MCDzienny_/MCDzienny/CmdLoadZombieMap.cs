using System;
using System.IO;

namespace MCDzienny
{
    class CmdLoadZombieMap : Command
    {
        public override string name { get { return "loadzombiemap"; } }

        public override string shortcut { get { return "loadinfectionmap"; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override void Use(Player p, string message)
        {
            try
            {
                if (message == "")
                {
                    Help(p);
                    return;
                }
                if (message.Split(' ').Length > 2)
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
                if (!File.Exists("infection/maps/" + message + ".lvl"))
                {
                    Player.SendMessage(p, "Level \"" + message + "\" doesn't exist!");
                    return;
                }
                Level level = null;
                level = Level.Load(message, 3, MapType.Zombie);
                if (level == null)
                {
                    if (!File.Exists("infection/maps/" + message + ".lvl.backup"))
                    {
                        Player.SendMessage(p, string.Format("Backup of {0} does not exist.", message));
                        return;
                    }
                    Server.s.Log("Attempting to load backup.");
                    File.Copy("infection/maps/" + message + ".lvl.backup", "infection/maps/" + message + ".lvl", overwrite: true);
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
                lock (Server.levels)
                {
                    Server.AddLevel(level);
                }
                Player.SendMessage(p, string.Format("Level \"{0}\" loaded.", level.name));
                try
                {
                    int num2 = int.Parse(s);
                    if (num2 >= 1 && num2 <= 4)
                    {
                        level.setPhysics(num2);
                    }
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
            Player.SendMessage(p, "/loadzombiemap <map_name> - Loads a zombie map that is located in 'infection/maps/' directory.");
        }
    }
}