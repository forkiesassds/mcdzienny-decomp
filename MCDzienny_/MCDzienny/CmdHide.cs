namespace MCDzienny
{
    public class CmdHide : Command
    {
        public override string name { get { return "hide"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            if (message != "" && message != "s")
            {
                Help(p);
                return;
            }
            bool flag = true;
            if (message == "s")
            {
                flag = false;
            }
            if (p.possess != "")
            {
                Player.SendMessage(p, "Stop your current possession first.");
                return;
            }
            p.hidden = !p.hidden;
            if (p.hidden)
            {
                Player.GlobalDie(p, self: true);
                if (flag)
                {
                    Player.GlobalMessageOps(string.Format("To Ops -{0}-{1} is now &finvisible{2}.", p.color + p.name, Server.DefaultColor, Server.DefaultColor));
                    Player.GlobalChat(p, string.Format("&c- {0} disconnected.", p.color + p.prefix + p.PublicName + Server.DefaultColor), showname: false);
                }
                else
                {
                    Player.SendMessage(p, "You're now &finvisible&e.");
                }
            }
            else
            {
                Player.GlobalSpawn(p, p.pos[0], p.pos[1], p.pos[2], p.rot[0], p.rot[1], self: false);
                if (flag)
                {
                    Player.GlobalMessageOps(string.Format("To Ops -{0}-{1} is now &8visible{2}.", p.color + p.name, Server.DefaultColor, Server.DefaultColor));
                    Player.GlobalChat(p, string.Format("&a+ {0} joined the game.", p.color + p.prefix + p.PublicName + Server.DefaultColor), showname: false);
                }
                else
                {
                    Player.SendMessage(p, "You're now &8visible&e.");
                }
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/hide - make yourself (in)visible to other players.");
            Player.SendMessage(p, "/hide s - doesn't display disconnected/joined message.");
        }
    }
}