using System;
using System.Reflection;

namespace MCDzienny
{
    // Token: 0x0200026C RID: 620
    public class CmdInfo : Command
    {
        // Token: 0x1700067D RID: 1661
        // (get) Token: 0x060011CC RID: 4556 RVA: 0x0006298C File Offset: 0x00060B8C
        public override string name
        {
            get { return "info"; }
        }

        // Token: 0x1700067E RID: 1662
        // (get) Token: 0x060011CD RID: 4557 RVA: 0x00062994 File Offset: 0x00060B94
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x1700067F RID: 1663
        // (get) Token: 0x060011CE RID: 4558 RVA: 0x0006299C File Offset: 0x00060B9C
        public override string type
        {
            get { return "information"; }
        }

        // Token: 0x17000680 RID: 1664
        // (get) Token: 0x060011CF RID: 4559 RVA: 0x000629A4 File Offset: 0x00060BA4
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000681 RID: 1665
        // (get) Token: 0x060011D0 RID: 4560 RVA: 0x000629A8 File Offset: 0x00060BA8
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Banned; }
        }

        // Token: 0x060011D2 RID: 4562 RVA: 0x000629B4 File Offset: 0x00060BB4
        public override void Use(Player p, string message)
        {
            if (message != "")
            {
                Help(p);
                return;
            }

            Player.SendMessage(p,
                string.Format("This server runs on &bMCDzienny{0}, which is based on MCLawl.", Server.DefaultColor));
            Player.SendMessage(p,
                string.Format("This server's version: &a{0}", Assembly.GetExecutingAssembly().GetName().Version));
            var timeSpan = DateTime.Now.Subtract(Server.TimeOnline);
            var text = "Time online: &b";
            if (timeSpan.Days == 1)
                text = text + timeSpan.Days + " day, ";
            else if (timeSpan.Days > 0) text = text + timeSpan.Days + " days, ";
            if (timeSpan.Hours == 1)
                text = text + timeSpan.Hours + " hour, ";
            else if (timeSpan.Days > 0 || timeSpan.Hours > 0) text = text + timeSpan.Hours + " hours, ";
            if (timeSpan.Minutes == 1)
                text = text + timeSpan.Minutes + " minute and ";
            else if (timeSpan.Hours > 0 || timeSpan.Days > 0 || timeSpan.Minutes > 0)
                text = text + timeSpan.Minutes + " minutes and ";
            if (timeSpan.Seconds == 1)
                text = text + timeSpan.Seconds + " second";
            else
                text = text + timeSpan.Seconds + " seconds";
            Player.SendMessage(p, text);
            if (Server.updateTimer.Interval > 1000.0)
                Player.SendMessage(p, string.Format("Server is currently in &5Low Lag{0} mode.", Server.DefaultColor));
        }

        // Token: 0x060011D3 RID: 4563 RVA: 0x00062B88 File Offset: 0x00060D88
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/info - Displays the server information.");
        }
    }
}