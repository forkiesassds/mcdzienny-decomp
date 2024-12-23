using System;
using System.Collections.Generic;
using System.Timers;

namespace MCDzienny
{
    class AutoSaver
    {
        static int count = 1;

        readonly Timer autoSaver = new Timer();

        public AutoSaver(int interval)
        {
            autoSaver.Interval = interval * 1000;
            autoSaver.Elapsed += autoSaver_Elapsed;
            autoSaver.Start();
        }

        void autoSaver_Elapsed(object sender, ElapsedEventArgs e)
        {
            Server.mainLoop.Queue(delegate { Run(); });
            if (Player.players.Count > 0)
            {
                string allCount = "";
                var players = new List<string>();
                Player.players.ForEachSync(delegate(Player pl) { players.Add(pl.name); });
                try
                {
                    Server.s.Log("!PLAYERS ONLINE: " + string.Join(", ", players.ToArray()), systemMsg: true);
                }
                catch {}
                allCount = "";
                Server.levels.ForEach(delegate(Level l) { allCount = allCount + ", " + l.name; });
                try
                {
                    Server.s.Log("!LEVELS ONLINE: " + allCount.Remove(0, 2), systemMsg: true);
                }
                catch {}
            }
        }

        public static void Run()
        {
            try
            {
                count--;
                Server.levels.ForEach(delegate(Level l)
                {
                    if (l.mapType != MapType.Lava && l.mapType != MapType.Zombie)
                    {
                        try
                        {
                            if (l.changed)
                            {
                                l.Save();
                                if (count == 0)
                                {
                                    int num = l.Backup();
                                    if (num != -1)
                                    {
                                        l.ChatLevel("Backup " + num + " saved.");
                                        Server.s.Log("Backup " + num + " saved for " + l.name);
                                    }
                                }
                            }
                        }
                        catch
                        {
                            Server.s.Log("Backup for " + l.name + " has caused an error.");
                        }
                    }
                });
                if (count <= 0)
                {
                    count = 15;
                }
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