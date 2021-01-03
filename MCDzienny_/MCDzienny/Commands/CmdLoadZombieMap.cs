using System;
using System.IO;

namespace MCDzienny
{
    // Token: 0x02000137 RID: 311
    internal class CmdLoadZombieMap : Command
    {
        // Token: 0x1700045B RID: 1115
        // (get) Token: 0x06000951 RID: 2385 RVA: 0x0002DF78 File Offset: 0x0002C178
        public override string name
        {
            get { return "loadzombiemap"; }
        }

        // Token: 0x1700045C RID: 1116
        // (get) Token: 0x06000952 RID: 2386 RVA: 0x0002DF80 File Offset: 0x0002C180
        public override string shortcut
        {
            get { return "loadinfectionmap"; }
        }

        // Token: 0x1700045D RID: 1117
        // (get) Token: 0x06000953 RID: 2387 RVA: 0x0002DF88 File Offset: 0x0002C188
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x1700045E RID: 1118
        // (get) Token: 0x06000954 RID: 2388 RVA: 0x0002DF90 File Offset: 0x0002C190
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x1700045F RID: 1119
        // (get) Token: 0x06000955 RID: 2389 RVA: 0x0002DF94 File Offset: 0x0002C194
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x06000957 RID: 2391 RVA: 0x0002DFA0 File Offset: 0x0002C1A0
        public override void Use(Player p, string message)
        {
            try
            {
                if (message == "")
                {
                    Help(p);
                }
                else if (message.Split(' ').Length > 2)
                {
                    Help(p);
                }
                else
                {
                    var num = message.IndexOf(' ');
                    var s = "0";
                    if (num != -1)
                    {
                        s = message.Substring(num + 1).ToLower();
                        message = message.Substring(0, num).ToLower();
                    }
                    else
                    {
                        message = message.ToLower();
                    }

                    foreach (var level in Server.levels)
                        if (level.name == message)
                        {
                            Player.SendMessage(p, string.Format("{0} is already loaded!", message));
                            return;
                        }

                    if (!File.Exists("infection/maps/" + message + ".lvl"))
                    {
                        Player.SendMessage(p, "Level \"" + message + "\" doesn't exist!");
                    }
                    else
                    {
                        Level level2 = null;
                        level2 = Level.Load(message, 3, MapType.Zombie);
                        if (level2 == null)
                        {
                            if (!File.Exists("infection/maps/" + message + ".lvl.backup"))
                            {
                                Player.SendMessage(p, string.Format("Backup of {0} does not exist.", message));
                                return;
                            }

                            Server.s.Log("Attempting to load backup.");
                            File.Copy("infection/maps/" + message + ".lvl.backup", "infection/maps/" + message + ".lvl",
                                true);
                            level2 = Level.Load(message, 3, true);
                            if (level2 == null)
                            {
                                Player.SendMessage(p, string.Format("Backup of {0} failed.", message));
                                return;
                            }
                        }

                        if (p != null && level2.permissionvisit > p.group.Permission)
                        {
                            Player.SendMessage(p,
                                string.Format("This map is for {0} only!",
                                    Level.PermissionToName(level2.permissionvisit)));
                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                        }
                        else
                        {
                            foreach (var level3 in Server.levels)
                                if (level3.name == message)
                                {
                                    Player.SendMessage(p, string.Format("{0} is already loaded!", message));
                                    GC.Collect();
                                    GC.WaitForPendingFinalizers();
                                    return;
                                }

                            lock (Server.levels)
                            {
                                Server.AddLevel(level2);
                            }

                            Player.SendMessage(p, string.Format("Level \"{0}\" loaded.", level2.name));
                            try
                            {
                                var num2 = int.Parse(s);
                                if (num2 >= 1 && num2 <= 4) level2.setPhysics(num2);
                            }
                            catch
                            {
                                Player.SendMessage(p, "Second argument invalid");
                            }
                        }
                    }
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

        // Token: 0x06000958 RID: 2392 RVA: 0x0002E2F8 File Offset: 0x0002C4F8
        public override void Help(Player p)
        {
            Player.SendMessage(p,
                "/loadzombiemap <map_name> - Loads a zombie map that is located in 'infection/maps/' directory.");
        }
    }
}