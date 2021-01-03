using System;
using System.Diagnostics;
using MCDzienny_.Gui;

namespace MCDzienny
{
    // Token: 0x020002E7 RID: 743
    public class CmdUpdate : Command
    {
        // Token: 0x17000836 RID: 2102
        // (get) Token: 0x06001505 RID: 5381 RVA: 0x00074990 File Offset: 0x00072B90
        public override string name
        {
            get { return "update"; }
        }

        // Token: 0x17000837 RID: 2103
        // (get) Token: 0x06001506 RID: 5382 RVA: 0x00074998 File Offset: 0x00072B98
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000838 RID: 2104
        // (get) Token: 0x06001507 RID: 5383 RVA: 0x000749A0 File Offset: 0x00072BA0
        public override string type
        {
            get { return "information"; }
        }

        // Token: 0x17000839 RID: 2105
        // (get) Token: 0x06001508 RID: 5384 RVA: 0x000749A8 File Offset: 0x00072BA8
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x1700083A RID: 2106
        // (get) Token: 0x06001509 RID: 5385 RVA: 0x000749AC File Offset: 0x00072BAC
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x0600150B RID: 5387 RVA: 0x000749B8 File Offset: 0x00072BB8
        public override void Use(Player p, string message)
        {
            if (!Updater.CheckForUpdates())
            {
                Player.SendMessage(p, "The software is up to date");
                return;
            }

            Player.SendMessage(p, "New version was found. The update process was started.");
            if (PlatformID.Unix == Environment.OSVersion.Platform)
                Process.Start("mono", "Updater.exe quick " + Process.GetCurrentProcess().Id);
            else
                Process.Start("Updater.exe", "quick " + Process.GetCurrentProcess().Id);
            Program.ExitProgram(false);
        }

        // Token: 0x0600150C RID: 5388 RVA: 0x00074A40 File Offset: 0x00072C40
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/update - Updates the server if it's out of date");
        }
    }
}