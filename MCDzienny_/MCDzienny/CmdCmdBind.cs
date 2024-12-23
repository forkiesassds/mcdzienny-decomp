using System;

namespace MCDzienny
{
    public class CmdCmdBind : Command
    {
        public override string name { get { return "cmdbind"; } }

        public override string shortcut { get { return "cb"; } }

        public override string type { get { return "build"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Builder; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            string text = "";
            int num = 0;
            if (message.IndexOf(' ') == -1)
            {
                bool flag = false;
                for (int i = 0; i < 10; i++)
                {
                    if (p.cmdBind[i] != null)
                    {
                        Player.SendMessage(p, string.Format("&c/{0} bound to &b{1} {2}", i, p.cmdBind[i], p.messageBind[i]));
                        flag = true;
                    }
                }
                if (!flag)
                {
                    Player.SendMessage(p, "You have no commands binded");
                }
                return;
            }
            if (message.Split(' ').Length == 1)
            {
                try
                {
                    num = Convert.ToInt16(message);
                    if (p.cmdBind[num] == null)
                    {
                        Player.SendMessage(p, "No command stored here yet.");
                    }
                    else
                    {
                        string arg = "/" + p.cmdBind[num] + " " + p.messageBind[num];
                        Player.SendMessage(p, string.Format("Stored command: &b{0}", arg));
                    }
                    return;
                }
                catch
                {
                    Help(p);
                    return;
                }
            }
            if (message.Split(' ').Length <= 1)
            {
                return;
            }
            try
            {
                num = Convert.ToInt16(message.Split(' ')[message.Split(' ').Length - 1]);
                string arg = message.Split(' ')[0];
                if (message.Split(' ').Length > 2)
                {
                    text = message.Substring(message.IndexOf(' ') + 1);
                    text = text.Remove(text.LastIndexOf(' '));
                }
                p.cmdBind[num] = arg;
                p.messageBind[num] = text;
                Player.SendMessage(p, string.Format("Binded &b/{0} {1} to &c/{2}", arg, text, num));
            }
            catch
            {
                Help(p);
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/cmdbind [command] [num] - Binds [command] to [num]");
            Player.SendMessage(p, "[num] must be between 0 and 9");
            Player.SendMessage(p, "Use with \"/[num]\" &b(example: /2)");
            Player.SendMessage(p, "Use /cmdbind [num] to see stored commands.");
        }
    }
}