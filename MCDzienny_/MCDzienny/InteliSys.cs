using System;
using System.Threading;
using MCDzienny.Gui;
using MCDzienny.RemoteAccess;
using MCDzienny.Settings;
using Timer = System.Timers.Timer;

namespace MCDzienny
{
    public static class InteliSys
    {
        public static volatile int pendingPacketsAvg;

        public static volatile int pendingPacketsSum;

        static int playerCount;

        static int throttle;

        public static void PacketsMonitoring()
        {
            Timer timer = new Timer(300.0);
            pendingPacketsSum = 0;
            timer.Elapsed += delegate
            {
                pendingPacketsSum = 0;
                playerCount = 0;
                Player.players.ForEach(delegate(Player p)
                {
                    if (p.countToAve)
                    {
                        pendingPacketsSum += Thread.VolatileRead(ref p.pendingPackets);
                        playerCount++;
                    }
                    else if (Thread.VolatileRead(ref p.pendingPackets) >= pendingPacketsAvg)
                    {
                        if (p.readyForAve)
                        {
                            p.countToAve = true;
                        }
                        else
                        {
                            p.readyForAve = true;
                        }
                        pendingPacketsSum += Thread.VolatileRead(ref p.pendingPackets);
                        playerCount++;
                    }
                    else
                    {
                        p.readyForAve = false;
                    }
                });
                if (playerCount > 0)
                {
                    pendingPacketsAvg = pendingPacketsSum / playerCount;
                }
                else
                {
                    pendingPacketsAvg = 0;
                }
                throttle++;
                if (!Server.CLI && throttle % 5 == 0)
                {
                    if (Server.shuttingDown)
                    {
                        return;
                    }
                    Window window = Window.thisWindow;
                    if (window != null)
                    {
                        window.toolStripStatusLabelLagometer.GetCurrentParent().BeginInvoke((Action)delegate
                        {
                            window.toolStripStatusLabelLagometer.Text =
                                "Lag(avg.) : " + pendingPacketsAvg;
                        });
                    }
                }
                if (throttle % 10 == 0)
                {
                    RemoteClient.remoteClients.ForEach(delegate(RemoteClient rc) { rc.SendLag(pendingPacketsAvg); });
                }
            };
            timer.Start();
            Timer timer2 = new Timer(20000.0);
            timer2.Elapsed += delegate
            {
                int kickTreshold = pendingPacketsAvg + GeneralSettings.All.Threshold1;
                int kickTreshold2 = pendingPacketsAvg * GeneralSettings.All.Threshold2;
                int result = 0;
                if (!Server.CLI)
                {
                    Server.s.Log("#", systemMsg: true);
                }
                Player.players.ForEach(delegate(Player p)
                {
                    result = Thread.VolatileRead(ref p.pendingPackets);
                    if (!Server.CLI)
                    {
                        Server.s.Log(p.name + " " + result, systemMsg: true);
                    }
                    if (result > kickTreshold && result > kickTreshold2 && GeneralSettings.All.KickSlug)
                    {
                        p.Kick("Slow connection detected.");
                    }
                });
            };
            timer2.Start();
        }
    }
}