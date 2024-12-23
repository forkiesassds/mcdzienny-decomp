using System;
using System.Reflection;

namespace MCDzienny
{
    public class CmdInfo : Command
    {
        public override string name { get { return "info"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "information"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }

        public override void Use(Player p, string message)
        {
            if (message != "")
            {
                Help(p);
                return;
            }
            Player.SendMessage(p, string.Format("This server runs on &bMCDzienny{0}, which is based on MCLawl.", Server.DefaultColor));
            Player.SendMessage(p, string.Format("This server's version: &a{0}", Assembly.GetExecutingAssembly().GetName().Version));
            TimeSpan timeSpan = DateTime.Now.Subtract(Server.TimeOnline);
            string text = "Time online: &b";
            if (timeSpan.Days == 1)
            {
                text = text + timeSpan.Days + " day, ";
            }
            else if (timeSpan.Days > 0)
            {
                text = text + timeSpan.Days + " days, ";
            }
            if (timeSpan.Hours == 1)
            {
                text = text + timeSpan.Hours + " hour, ";
            }
            else if (timeSpan.Days > 0 || timeSpan.Hours > 0)
            {
                text = text + timeSpan.Hours + " hours, ";
            }
            if (timeSpan.Minutes == 1)
            {
                text = text + timeSpan.Minutes + " minute and ";
            }
            else if (timeSpan.Hours > 0 || timeSpan.Days > 0 || timeSpan.Minutes > 0)
            {
                text = text + timeSpan.Minutes + " minutes and ";
            }
            text = timeSpan.Seconds != 1 ? text + timeSpan.Seconds + " seconds" : text + timeSpan.Seconds + " second";
            Player.SendMessage(p, text);
            if (Server.updateTimer.Interval > 1000.0)
            {
                Player.SendMessage(p, string.Format("Server is currently in &5Low Lag{0} mode.", Server.DefaultColor));
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/info - Displays the server information.");
        }
    }
}