using System;
using System.IO;

namespace MCDzienny
{
    // Token: 0x02000272 RID: 626
    public class CmdLoad : Command
    {
        // Token: 0x17000696 RID: 1686
        // (get) Token: 0x060011F7 RID: 4599 RVA: 0x000631D0 File Offset: 0x000613D0
        public override string name
        {
            get { return "load"; }
        }

        // Token: 0x17000697 RID: 1687
        // (get) Token: 0x060011F8 RID: 4600 RVA: 0x000631D8 File Offset: 0x000613D8
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000698 RID: 1688
        // (get) Token: 0x060011F9 RID: 4601 RVA: 0x000631E0 File Offset: 0x000613E0
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x17000699 RID: 1689
        // (get) Token: 0x060011FA RID: 4602 RVA: 0x000631E8 File Offset: 0x000613E8
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x1700069A RID: 1690
        // (get) Token: 0x060011FB RID: 4603 RVA: 0x000631EC File Offset: 0x000613EC
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x060011FD RID: 4605 RVA: 0x000631F8 File Offset: 0x000613F8
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
                        if (level.name == message && level.Owner == "")
                        {
                            Player.SendMessage(p, string.Format("{0} is already loaded!", message));
                            return;
                        }

                    if (Server.FreebuildCount == Server.levels.Capacity)
                    {
                        if (Server.levels.Capacity == 1)
                        {
                            Player.SendMessage(p, "You can't load any levels!");
                        }
                        else
                        {
                            all.Find("unload").Use(p, "empty");
                            if (Server.FreebuildCount == Server.levels.Capacity)
                            {
                                Player.SendMessage(p, "No maps are empty to unload. Cannot load map.");
                                return;
                            }
                        }
                    }

                    if (!File.Exists("levels/" + message + ".lvl"))
                    {
                        Player.SendMessage(p, string.Format("Level \"{0}\" doesn't exist!", message));
                    }
                    else
                    {
                        Level level2 = null;
                        level2 = Level.Load(message);
                        if (level2 == null)
                        {
                            if (!File.Exists("levels/" + message + ".lvl.backup"))
                            {
                                Player.SendMessage(p, string.Format("Backup of {0} does not exist.", message));
                                return;
                            }

                            Server.s.Log("Attempting to load backup.");
                            File.Copy("levels/" + message + ".lvl.backup", "levels/" + message + ".lvl", true);
                            level2 = Level.Load(message);
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
                                if (level3.name == message && level3.Owner == "")
                                {
                                    Player.SendMessage(p, string.Format("{0} is already loaded!", message));
                                    GC.Collect();
                                    GC.WaitForPendingFinalizers();
                                    return;
                                }

                            Server.AddLevel(level2);
                            Player.GlobalMessage(string.Format("Level \"{0}\" loaded.", level2.name));
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

        // Token: 0x060011FE RID: 4606 RVA: 0x000635A4 File Offset: 0x000617A4
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/load <level> - Loads a level.");
        }
    }
}