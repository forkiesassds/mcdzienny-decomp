using System;

namespace MCDzienny
{
    public class CmdCompile : Command
    {
        public override string name { get { return "compile"; } }

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
            bool flag = false;
            try
            {
                flag = Scripting.Compile(message);
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
                Player.SendMessage(p, "An exception was thrown during compilation.");
                return;
            }
            if (flag)
            {
                Player.SendMessage(p, "Compiled successfully.");
            }
            else
            {
                Player.SendMessage(p, "Compilation error.  Please check logs/compiler/errors.txt for more information.");
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/compile <class name> - Compiles a command class file into a DLL.");
        }
    }
}