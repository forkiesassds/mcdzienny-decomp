using System;
using System.Diagnostics;
using MCDzienny_.Gui;

namespace MCDzienny
{
    public class CmdUpdate : Command
    {
        public override string name { get { return "update"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "information"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override void Use(Player p, string message)
        {
            if (!Updater.CheckForUpdates())
            {
                Player.SendMessage(p, "The software is up to date");
                return;
            }
            Player.SendMessage(p, "New version was found. The update process was started.");
            if (PlatformID.Unix == Environment.OSVersion.Platform)
            {
                Process.Start("mono", "Updater.exe quick " + Process.GetCurrentProcess().Id);
            }
            else
            {
                Process.Start("Updater.exe", "quick " + Process.GetCurrentProcess().Id);
            }
            Program.ExitProgram(AutoRestart: false);
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/update - Updates the server if it's out of date");
        }
    }
}