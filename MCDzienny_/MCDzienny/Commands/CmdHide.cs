namespace MCDzienny
{
    // Token: 0x02000268 RID: 616
    public class CmdHide : Command
    {
        // Token: 0x17000671 RID: 1649
        // (get) Token: 0x060011B5 RID: 4533 RVA: 0x00061BF0 File Offset: 0x0005FDF0
        public override string name
        {
            get { return "hide"; }
        }

        // Token: 0x17000672 RID: 1650
        // (get) Token: 0x060011B6 RID: 4534 RVA: 0x00061BF8 File Offset: 0x0005FDF8
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000673 RID: 1651
        // (get) Token: 0x060011B7 RID: 4535 RVA: 0x00061C00 File Offset: 0x0005FE00
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x17000674 RID: 1652
        // (get) Token: 0x060011B8 RID: 4536 RVA: 0x00061C08 File Offset: 0x0005FE08
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000675 RID: 1653
        // (get) Token: 0x060011B9 RID: 4537 RVA: 0x00061C0C File Offset: 0x0005FE0C
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x17000676 RID: 1654
        // (get) Token: 0x060011BA RID: 4538 RVA: 0x00061C10 File Offset: 0x0005FE10
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x060011BB RID: 4539 RVA: 0x00061C14 File Offset: 0x0005FE14
        public override void Use(Player p, string message)
        {
            if (message != "" && message != "s")
            {
                Help(p);
                return;
            }

            var flag = true;
            if (message == "s") flag = false;
            if (p.possess != "")
            {
                Player.SendMessage(p, "Stop your current possession first.");
                return;
            }

            p.hidden = !p.hidden;
            if (p.hidden)
            {
                Player.GlobalDie(p, true);
                if (flag)
                {
                    Player.GlobalMessageOps(string.Format("To Ops -{0}-{1} is now &finvisible{2}.", p.color + p.name,
                        Server.DefaultColor, Server.DefaultColor));
                    Player.GlobalChat(p,
                        string.Format("&c- {0} disconnected.", p.color + p.prefix + p.PublicName + Server.DefaultColor),
                        false);
                    return;
                }

                Player.SendMessage(p, "You're now &finvisible&e.");
            }
            else
            {
                Player.GlobalSpawn(p, p.pos[0], p.pos[1], p.pos[2], p.rot[0], p.rot[1], false);
                if (flag)
                {
                    Player.GlobalMessageOps(string.Format("To Ops -{0}-{1} is now &8visible{2}.", p.color + p.name,
                        Server.DefaultColor, Server.DefaultColor));
                    Player.GlobalChat(p,
                        string.Format("&a+ {0} joined the game.",
                            p.color + p.prefix + p.PublicName + Server.DefaultColor), false);
                    return;
                }

                Player.SendMessage(p, "You're now &8visible&e.");
            }
        }

        // Token: 0x060011BC RID: 4540 RVA: 0x00061D94 File Offset: 0x0005FF94
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/hide - make yourself (in)visible to other players.");
            Player.SendMessage(p, "/hide s - doesn't display disconnected/joined message.");
        }
    }
}