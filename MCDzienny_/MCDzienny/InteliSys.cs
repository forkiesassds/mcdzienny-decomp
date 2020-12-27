using System;
using System.Threading;
using MCDzienny.Gui;
using MCDzienny.Settings;
using Timer = System.Timers.Timer;

namespace MCDzienny
{
    // Token: 0x0200019C RID: 412
    public static class InteliSys
    {
        // Token: 0x0400062F RID: 1583
        public static volatile int pendingPacketsAvg;

        // Token: 0x04000630 RID: 1584
        public static volatile int pendingPacketsSum;

        // Token: 0x04000631 RID: 1585
        private static int playerCount;

        // Token: 0x04000632 RID: 1586
        private static int throttle;

        // Token: 0x06000C15 RID: 3093 RVA: 0x00046E80 File Offset: 0x00045080
        public static void PacketsMonitoring()
        {
            var timer = new Timer(300.0);
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
                        return;
                    }

                    if (Thread.VolatileRead(ref p.pendingPackets) >= pendingPacketsAvg)
                    {
                        if (p.readyForAve)
                            p.countToAve = true;
                        else
                            p.readyForAve = true;
                        pendingPacketsSum += Thread.VolatileRead(ref p.pendingPackets);
                        playerCount++;
                        return;
                    }

                    p.readyForAve = false;
                });
                if (playerCount > 0)
                    pendingPacketsAvg = pendingPacketsSum / playerCount;
                else
                    pendingPacketsAvg = 0;
                throttle++;
                if (!Server.CLI && throttle % 5 == 0)
                {
                    if (Server.shuttingDown) return;
                    var window = Window.thisWindow;
                    if (window != null)
                        window.toolStripStatusLabelLagometer.GetCurrentParent().BeginInvoke(new Action(delegate
                        {
                            window.toolStripStatusLabelLagometer.Text = "Lag(avg.) : " + pendingPacketsAvg;
                        }));
                }
            };
            timer.Start();
            var timer2 = new Timer(20000.0);
            timer2.Elapsed += delegate
            {
                var kickTreshold = pendingPacketsAvg + GeneralSettings.All.Threshold1;
                var kickTreshold2 = pendingPacketsAvg * GeneralSettings.All.Threshold2;
                var result = 0;
                if (!Server.CLI) Server.s.Log("#", true);
                Player.players.ForEach(delegate(Player p)
                {
                    result = Thread.VolatileRead(ref p.pendingPackets);
                    if (!Server.CLI) Server.s.Log(p.name + " " + result, true);
                    if (result > kickTreshold && result > kickTreshold2 && GeneralSettings.All.KickSlug)
                        p.Kick("Slow connection detected.");
                });
            };
            timer2.Start();
        }
    }
}