using System;
using System.Diagnostics;

namespace MCDzienny
{
    // Token: 0x02000296 RID: 662
    public class CmdServerReport : Command
    {
        // Token: 0x1700071B RID: 1819
        // (get) Token: 0x060012F7 RID: 4855 RVA: 0x0006892C File Offset: 0x00066B2C
        public override string name
        {
            get { return "serverreport"; }
        }

        // Token: 0x1700071C RID: 1820
        // (get) Token: 0x060012F8 RID: 4856 RVA: 0x00068934 File Offset: 0x00066B34
        public override string shortcut
        {
            get { return "sr"; }
        }

        // Token: 0x1700071D RID: 1821
        // (get) Token: 0x060012F9 RID: 4857 RVA: 0x0006893C File Offset: 0x00066B3C
        public override string type
        {
            get { return "information"; }
        }

        // Token: 0x1700071E RID: 1822
        // (get) Token: 0x060012FA RID: 4858 RVA: 0x00068944 File Offset: 0x00066B44
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x1700071F RID: 1823
        // (get) Token: 0x060012FB RID: 4859 RVA: 0x00068948 File Offset: 0x00066B48
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Admin; }
        }

        // Token: 0x060012FC RID: 4860 RVA: 0x0006894C File Offset: 0x00066B4C
        public override void Use(Player p, string message)
        {
            if (Server.PCCounter == null)
            {
                Player.SendMessage(p, "Starting PCCounter...one second");
                Server.PCCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                Server.PCCounter.BeginInit();
                Server.PCCounter.NextValue();
            }

            if (Server.ProcessCounter == null)
            {
                Player.SendMessage(p, "Starting ProcessCounter...one second");
                Server.ProcessCounter = new PerformanceCounter("Process", "% Processor Time",
                    Process.GetCurrentProcess().ProcessName);
                Server.ProcessCounter.BeginInit();
                Server.ProcessCounter.NextValue();
            }

            var totalProcessorTime = Process.GetCurrentProcess().TotalProcessorTime;
            var timeSpan = DateTime.Now - Process.GetCurrentProcess().StartTime;
            var message2 = string.Format("CPU Usage (Processes : All Processes):{0} : {1}",
                Server.ProcessCounter.NextValue(), Server.PCCounter.NextValue());
            var message3 = string.Format("Memory Usage: {0} Megabytes",
                Math.Round(Process.GetCurrentProcess().PrivateMemorySize64 / 1048576.0).ToString());
            var message4 = string.Format("Uptime: {0} Days {1} Hours {2} Minutes {3} Seconds", timeSpan.Days,
                timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
            var message5 = string.Format("Threads: {0}", Process.GetCurrentProcess().Threads.Count);
            Player.SendMessage(p, message4);
            Player.SendMessage(p, message3);
            Player.SendMessage(p, message2);
            Player.SendMessage(p, message5);
        }

        // Token: 0x060012FD RID: 4861 RVA: 0x00068AE8 File Offset: 0x00066CE8
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/serverreport - Get server CPU%, RAM usage, and uptime.");
        }
    }
}