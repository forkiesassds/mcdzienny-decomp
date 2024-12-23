using System;
using System.Diagnostics;

namespace MCDzienny
{
    public class CmdServerReport : Command
    {
        public override string name { get { return "serverreport"; } }

        public override string shortcut { get { return "sr"; } }

        public override string type { get { return "information"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }

        public override void Use(Player p, string message)
        {
            //IL_0021: Unknown result type (might be due to invalid IL or missing references)
            //IL_002b: Expected O, but got Unknown
            //IL_0066: Unknown result type (might be due to invalid IL or missing references)
            //IL_0070: Expected O, but got Unknown
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
                Server.ProcessCounter = new PerformanceCounter("Process", "% Processor Time", Process.GetCurrentProcess().ProcessName);
                Server.ProcessCounter.BeginInit();
                Server.ProcessCounter.NextValue();
            }
            // _ = Process.GetCurrentProcess().TotalProcessorTime;
            TimeSpan timeSpan = DateTime.Now - Process.GetCurrentProcess().StartTime;
            string message2 = string.Format("CPU Usage (Processes : All Processes):{0} : {1}", Server.ProcessCounter.NextValue(), Server.PCCounter.NextValue());
            string message3 = string.Format("Memory Usage: {0} Megabytes", Math.Round(Process.GetCurrentProcess().PrivateMemorySize64 / 1048576.0).ToString());
            string message4 = string.Format("Uptime: {0} Days {1} Hours {2} Minutes {3} Seconds", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
            string message5 = string.Format("Threads: {0}", Process.GetCurrentProcess().Threads.Count);
            Player.SendMessage(p, message4);
            Player.SendMessage(p, message3);
            Player.SendMessage(p, message2);
            Player.SendMessage(p, message5);
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/serverreport - Get server CPU%, RAM usage, and uptime.");
        }
    }
}