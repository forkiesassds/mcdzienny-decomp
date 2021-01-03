using System;
using System.Collections.Generic;
using System.Timers;

namespace MCDzienny
{
    // Token: 0x02000006 RID: 6
    internal class AutoSaver
    {
        // Token: 0x04000005 RID: 5
        private static int count = 1;

        // Token: 0x04000006 RID: 6
        private readonly Timer autoSaver = new Timer();

        // Token: 0x0600000D RID: 13 RVA: 0x0000207C File Offset: 0x0000027C
        public AutoSaver(int interval)
        {
            autoSaver.Interval = interval * 1000;
            autoSaver.Elapsed += autoSaver_Elapsed;
            autoSaver.Start();
        }

        // Token: 0x0600000E RID: 14 RVA: 0x000020D0 File Offset: 0x000002D0
        private void autoSaver_Elapsed(object sender, ElapsedEventArgs e)
        {
            Server.mainLoop.Queue(delegate { Run(); });
            if (Player.players.Count > 0)
            {
                var allCount = "";
                var players = new List<string>();
                Player.players.ForEachSync(delegate(Player pl) { players.Add(pl.name); });
                try
                {
                    Server.s.Log("!PLAYERS ONLINE: " + string.Join(", ", players.ToArray()), true);
                }
                catch
                {
                }

                allCount = "";
                Server.levels.ForEach(delegate(Level l) { allCount = allCount + ", " + l.name; });
                try
                {
                    Server.s.Log("!LEVELS ONLINE: " + allCount.Remove(0, 2), true);
                }
                catch
                {
                }
            }
        }

        // Token: 0x0600000F RID: 15 RVA: 0x000021DC File Offset: 0x000003DC
        public static void Run()
        {
            try
            {
                count--;
                Server.levels.ForEach(delegate(Level l)
                {
                    if (l.mapType != MapType.Lava && l.mapType != MapType.Zombie)
                        try
                        {
                            if (l.changed)
                            {
                                l.Save();
                                if (count == 0)
                                {
                                    var num = l.Backup();
                                    if (num != -1)
                                    {
                                        l.ChatLevel("Backup " + num + " saved.");
                                        Server.s.Log(string.Concat("Backup ", num, " saved for ", l.name));
                                    }
                                }
                            }
                        }
                        catch
                        {
                            Server.s.Log("Backup for " + l.name + " has caused an error.");
                        }
                });
                if (count <= 0) count = 15;
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }

            try
            {
                Player.players.GetCopy().ForEach(delegate(Player p) { p.Save(); });
            }
            catch (Exception ex2)
            {
                Server.ErrorLog(ex2);
            }
        }
    }
}