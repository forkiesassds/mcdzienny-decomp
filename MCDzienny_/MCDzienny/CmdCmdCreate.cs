using System;
using System.IO;

namespace MCDzienny
{
    public class CmdCmdCreate : Command
    {
        public override string name { get { return "cmdcreate"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }

        public override void Use(Player p, string message)
        {
            if (message == "" || message.IndexOf(' ') != -1)
            {
                Help(p);
                return;
            }
            if (File.Exists("extra/commands/source/Cmd" + message + ".cs"))
            {
                Player.SendMessage(p, string.Format("File Cmd{0}.cs already exists.  Choose another name.", message));
                return;
            }
            try
            {
                Scripting.CreateNew(message);
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
                Player.SendMessage(p, "An error occurred creating the class file.");
                return;
            }
            Player.SendMessage(p, "Successfully created a new command class.");
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/cmdcreate <message> - Creates a dummy command class named Cmd<Message> from which you can make a new command.");
        }
    }
}