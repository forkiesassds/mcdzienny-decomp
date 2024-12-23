using System;
using System.Diagnostics;
using System.Timers;
using MCDzienny_.Gui;

namespace MCDzienny
{
    public class CmdRestart : Command
    {
        static readonly Timer restartTimer = new Timer();

        static int lastSeconds = 3;

        static bool restartInProgress;

        public override string name { get { return "restart"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }

        public override void Use(Player p, string message)
        {
            if (message != "")
            {
                Help(p);
            }
            else if (restartInProgress)
            {
                Player.SendMessage(p, "@The restart timer was stopped.");
                Player.GlobalChatWorld(null, "%cRestart aborted!", showname: false);
                Server.s.Log("Restart aborted.");
                restartInProgress = false;
                Server.shuttingDown = false;
                restartTimer.Elapsed -= RestartTimerCallback1;
                restartTimer.Elapsed -= RestartTimerCallback2;
            }
            else
            {
                lastSeconds = 3;
                Server.s.Log("The server will be restarted in 20 seconds.");
                Player.GlobalChatWorld(null, "%cThe server will be restarted in 20 seconds.", showname: false);
                restartTimer.Interval = 10000.0;
                restartTimer.Elapsed += RestartTimerCallback1;
                restartTimer.Start();
                restartInProgress = true;
                Server.shuttingDown = true;
            }
        }

        public void RestartTimerCallback1(object sender, ElapsedEventArgs e)
        {
            if (restartInProgress)
            {
                Player.GlobalChatWorld(null, "%c10 seconds to restart.", showname: false);
                Server.s.Log("10 seconds to restart.");
                restartTimer.Interval = 7000.0;
                restartTimer.Elapsed -= RestartTimerCallback1;
                restartTimer.Elapsed += RestartTimerCallback2;
            }
        }

        public void RestartTimerCallback2(object sender, ElapsedEventArgs e)
        {
            if (restartInProgress)
            {
                restartTimer.Interval = 1000.0;
                Player.GlobalChatWorld(null, lastSeconds == 0 ? "%cRestart!" : "%c" + lastSeconds, showname: false);
                lastSeconds--;
                if (lastSeconds == 0)
                {
                    restartTimer.Elapsed -= RestartTimerCallback2;
                    Restart();
                }
            }
        }

        public void Restart()
        {
            if (PlatformID.Unix == Environment.OSVersion.Platform)
            {
                Process.Start("mono", "Updater.exe restart " + Process.GetCurrentProcess().Id);
            }
            else
            {
                Process.Start("Updater.exe", "restart " + Process.GetCurrentProcess().Id);
            }
            Program.ExitProgram(AutoRestart: false);
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/restart - Restarts the server within 20 sec.");
            Player.SendMessage(p, "Using /restart during the countdown aborts the procedure.");
        }
    }
}