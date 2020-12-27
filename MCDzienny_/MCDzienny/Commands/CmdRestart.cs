using System;
using System.Diagnostics;
using System.Timers;
using MCDzienny_.Gui;

namespace MCDzienny
{
    // Token: 0x0200028F RID: 655
    public class CmdRestart : Command
    {
        // Token: 0x0400093E RID: 2366
        private static readonly Timer restartTimer = new Timer();

        // Token: 0x0400093F RID: 2367
        private static int lastSeconds = 3;

        // Token: 0x04000940 RID: 2368
        private static bool restartInProgress;

        // Token: 0x170006FC RID: 1788
        // (get) Token: 0x060012C1 RID: 4801 RVA: 0x000677CC File Offset: 0x000659CC
        public override string name
        {
            get { return "restart"; }
        }

        // Token: 0x170006FD RID: 1789
        // (get) Token: 0x060012C2 RID: 4802 RVA: 0x000677D4 File Offset: 0x000659D4
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170006FE RID: 1790
        // (get) Token: 0x060012C3 RID: 4803 RVA: 0x000677DC File Offset: 0x000659DC
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x170006FF RID: 1791
        // (get) Token: 0x060012C4 RID: 4804 RVA: 0x000677E4 File Offset: 0x000659E4
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000700 RID: 1792
        // (get) Token: 0x060012C5 RID: 4805 RVA: 0x000677E8 File Offset: 0x000659E8
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Admin; }
        }

        // Token: 0x060012C6 RID: 4806 RVA: 0x000677EC File Offset: 0x000659EC
        public override void Use(Player p, string message)
        {
            if (message != "")
            {
                Help(p);
                return;
            }

            if (restartInProgress)
            {
                Player.SendMessage(p, "@The restart timer was stopped.");
                Player.GlobalChatWorld(null, "%cRestart aborted!", false);
                Server.s.Log("Restart aborted.");
                restartInProgress = false;
                Server.shuttingDown = false;
                restartTimer.Elapsed -= RestartTimerCallback1;
                restartTimer.Elapsed -= RestartTimerCallback2;
                return;
            }

            lastSeconds = 3;
            Server.s.Log("The server will be restarted in 20 seconds.");
            Player.GlobalChatWorld(null, "%cThe server will be restarted in 20 seconds.", false);
            restartTimer.Interval = 10000.0;
            restartTimer.Elapsed += RestartTimerCallback1;
            restartTimer.Start();
            restartInProgress = true;
            Server.shuttingDown = true;
        }

        // Token: 0x060012C7 RID: 4807 RVA: 0x000678DC File Offset: 0x00065ADC
        public void RestartTimerCallback1(object sender, ElapsedEventArgs e)
        {
            if (!restartInProgress) return;
            Player.GlobalChatWorld(null, "%c10 seconds to restart.", false);
            Server.s.Log("10 seconds to restart.");
            restartTimer.Interval = 7000.0;
            restartTimer.Elapsed -= RestartTimerCallback1;
            restartTimer.Elapsed += RestartTimerCallback2;
        }

        // Token: 0x060012C8 RID: 4808 RVA: 0x0006794C File Offset: 0x00065B4C
        public void RestartTimerCallback2(object sender, ElapsedEventArgs e)
        {
            if (!restartInProgress) return;
            restartTimer.Interval = 1000.0;
            Player.GlobalChatWorld(null, lastSeconds == 0 ? "%cRestart!" : "%c" + lastSeconds, false);
            lastSeconds--;
            if (lastSeconds == 0)
            {
                restartTimer.Elapsed -= RestartTimerCallback2;
                Restart();
            }
        }

        // Token: 0x060012C9 RID: 4809 RVA: 0x000679CC File Offset: 0x00065BCC
        public void Restart()
        {
            if (PlatformID.Unix == Environment.OSVersion.Platform)
                Process.Start("mono", "Updater.exe restart " + Process.GetCurrentProcess().Id);
            else
                Process.Start("Updater.exe", "restart " + Process.GetCurrentProcess().Id);
            Program.ExitProgram(false);
        }

        // Token: 0x060012CA RID: 4810 RVA: 0x00067A38 File Offset: 0x00065C38
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/restart - Restarts the server within 20 sec.");
            Player.SendMessage(p, "Using /restart during the countdown aborts the procedure.");
        }
    }
}