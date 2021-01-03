using System.Diagnostics;
using System.Timers;
using MCDzienny.Gui;
using MCDzienny.Settings;
using MCDzienny_.Gui;

namespace MCDzienny
{
    // Token: 0x020000E1 RID: 225
    internal class CmdShutdown : Command
    {
        // Token: 0x0400038B RID: 907
        private static readonly Timer shutdownTimer = new Timer();

        // Token: 0x0400038C RID: 908
        private static int lastSeconds = 3;

        // Token: 0x0400038D RID: 909
        private static bool shutdownInProgress;

        // Token: 0x17000342 RID: 834
        // (get) Token: 0x06000744 RID: 1860 RVA: 0x00024D60 File Offset: 0x00022F60
        public override string name
        {
            get { return "shutdown"; }
        }

        // Token: 0x17000343 RID: 835
        // (get) Token: 0x06000745 RID: 1861 RVA: 0x00024D68 File Offset: 0x00022F68
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000344 RID: 836
        // (get) Token: 0x06000746 RID: 1862 RVA: 0x00024D70 File Offset: 0x00022F70
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x17000345 RID: 837
        // (get) Token: 0x06000747 RID: 1863 RVA: 0x00024D78 File Offset: 0x00022F78
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000346 RID: 838
        // (get) Token: 0x06000748 RID: 1864 RVA: 0x00024D7C File Offset: 0x00022F7C
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Admin; }
        }

        // Token: 0x06000749 RID: 1865 RVA: 0x00024D80 File Offset: 0x00022F80
        public override void Use(Player p, string message)
        {
            if (message != "")
            {
                Help(p);
                return;
            }

            if (shutdownInProgress)
            {
                Player.SendMessage(p, "@The shutdown timer was stopped.");
                Player.GlobalChatWorld(null, "%cShutdown aborted!", false);
                Server.s.Log("Shutdown aborted.");
                shutdownInProgress = false;
                Server.shuttingDown = false;
                shutdownTimer.Elapsed -= RestartTimerCallback1;
                shutdownTimer.Elapsed -= RestartTimerCallback2;
                return;
            }

            lastSeconds = 3;
            Server.s.Log("The server will be shut down in 20 seconds.");
            Player.GlobalChatWorld(null, "%cThe server will be shut down in 20 seconds.", false);
            shutdownTimer.Interval = 10000.0;
            shutdownTimer.Elapsed += RestartTimerCallback1;
            shutdownTimer.Start();
            shutdownInProgress = true;
            Server.shuttingDown = true;
        }

        // Token: 0x0600074A RID: 1866 RVA: 0x00024E70 File Offset: 0x00023070
        public void RestartTimerCallback1(object sender, ElapsedEventArgs e)
        {
            if (!shutdownInProgress) return;
            Player.GlobalChatWorld(null, "%c10 seconds to shutdown.", false);
            Server.s.Log("10 seconds to shutdown.");
            shutdownTimer.Interval = 7000.0;
            shutdownTimer.Elapsed -= RestartTimerCallback1;
            shutdownTimer.Elapsed += RestartTimerCallback2;
        }

        // Token: 0x0600074B RID: 1867 RVA: 0x00024EE0 File Offset: 0x000230E0
        public void RestartTimerCallback2(object sender, ElapsedEventArgs e)
        {
            if (!shutdownInProgress) return;
            shutdownTimer.Interval = 1000.0;
            Player.GlobalChatWorld(null, lastSeconds == 0 ? "%cShutdown!" : "%c" + lastSeconds, false);
            lastSeconds--;
            if (lastSeconds == 0)
            {
                shutdownTimer.Elapsed -= RestartTimerCallback2;
                Shutdown();
            }
        }

        // Token: 0x0600074C RID: 1868 RVA: 0x00024F60 File Offset: 0x00023160
        public void Shutdown()
        {
            GuiSettings.All.Save();
            if (!Program.SaveAll(false)) return;
            try
            {
                if (!Server.CLI && Window.thisWindow.notifyIcon1 != null)
                {
                    Window.thisWindow.notifyIcon1.Icon = null;
                    Window.thisWindow.notifyIcon1.Visible = false;
                }
            }
            catch
            {
            }

            Process.GetCurrentProcess().Kill();
        }

        // Token: 0x0600074D RID: 1869 RVA: 0x00024FDC File Offset: 0x000231DC
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/shutdown - shuts down the server within 20 seconds,");
            Player.SendMessage(p, "/shutdown - when used again it aborts the shutdown.");
        }
    }
}