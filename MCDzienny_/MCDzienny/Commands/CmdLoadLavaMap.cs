using System;
using System.IO;

namespace MCDzienny
{
    // Token: 0x020000D9 RID: 217
    internal class CmdLoadLavaMap : Command
    {
        // Token: 0x1700031A RID: 794
        // (get) Token: 0x06000705 RID: 1797 RVA: 0x00024134 File Offset: 0x00022334
        public override string name
        {
            get { return "loadlavamap"; }
        }

        // Token: 0x1700031B RID: 795
        // (get) Token: 0x06000706 RID: 1798 RVA: 0x0002413C File Offset: 0x0002233C
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x1700031C RID: 796
        // (get) Token: 0x06000707 RID: 1799 RVA: 0x00024144 File Offset: 0x00022344
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x1700031D RID: 797
        // (get) Token: 0x06000708 RID: 1800 RVA: 0x0002414C File Offset: 0x0002234C
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x1700031E RID: 798
        // (get) Token: 0x06000709 RID: 1801 RVA: 0x00024150 File Offset: 0x00022350
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x0600070B RID: 1803 RVA: 0x0002415C File Offset: 0x0002235C
        public override void Use(Player p, string message)
        {
            try
            {
                if (message == "" || message.Split(' ').Length > 2)
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

                    if (!File.Exists("lava/maps/" + message + ".lvl"))
                    {
                        Player.SendMessage(p, string.Format("Level \"{0}\" doesn't exist!", message));
                    }
                    else
                    {
                        Level level2 = null;
                        level2 = Level.Load(message, 3, true);
                        if (level2 == null)
                        {
                            if (!File.Exists("lava/maps/" + message + ".lvl.backup"))
                            {
                                Player.SendMessage(p, string.Format("Backup of {0} does not exist.", message));
                                return;
                            }

                            Server.s.Log("Attempting to load backup.");
                            File.Copy("lava/maps/" + message + ".lvl.backup", "lava/maps/" + message + ".lvl", true);
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

                            Server.AddLevel(level2);
                            Player.GlobalMessage(string.Format("Level \"{0}\" loaded.", level2.name));
                            try
                            {
                                int.Parse(s);
                                level2.setPhysics(3);
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

        // Token: 0x0600070C RID: 1804 RVA: 0x00024464 File Offset: 0x00022664
        public override void Help(Player p)
        {
            Player.SendMessage(p,
                "/loadlavamap <map_name> - Loads a lava map that is located in 'lava/maps/' directory.");
        }
    }
}