namespace MCDzienny
{
    // Token: 0x020002E9 RID: 745
    public class CmdRepeat : Command
    {
        // Token: 0x17000841 RID: 2113
        // (get) Token: 0x06001516 RID: 5398 RVA: 0x00074CA0 File Offset: 0x00072EA0
        public override string name
        {
            get { return "repeat"; }
        }

        // Token: 0x17000842 RID: 2114
        // (get) Token: 0x06001517 RID: 5399 RVA: 0x00074CA8 File Offset: 0x00072EA8
        public override string shortcut
        {
            get { return "m"; }
        }

        // Token: 0x17000843 RID: 2115
        // (get) Token: 0x06001518 RID: 5400 RVA: 0x00074CB0 File Offset: 0x00072EB0
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x17000844 RID: 2116
        // (get) Token: 0x06001519 RID: 5401 RVA: 0x00074CB8 File Offset: 0x00072EB8
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x17000845 RID: 2117
        // (get) Token: 0x0600151A RID: 5402 RVA: 0x00074CBC File Offset: 0x00072EBC
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Guest; }
        }

        // Token: 0x17000846 RID: 2118
        // (get) Token: 0x0600151B RID: 5403 RVA: 0x00074CC0 File Offset: 0x00072EC0
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x0600151D RID: 5405 RVA: 0x00074CCC File Offset: 0x00072ECC
        public override void Use(Player p, string message)
        {
            try
            {
                if (p.lastCMD == "")
                {
                    Player.SendMessage(p, "No commands used yet.");
                }
                else if (p.lastCMD.Length > 5 && p.lastCMD.Substring(0, 6) == "static")
                {
                    Player.SendMessage(p, "Can't repeat static");
                }
                else
                {
                    Player.SendMessage(p, "Using &b/" + p.lastCMD);
                    if (p.lastCMD.IndexOf(' ') == -1)
                        all.Find(p.lastCMD).Use(p, "");
                    else
                        all.Find(p.lastCMD.Substring(0, p.lastCMD.IndexOf(' ')))
                            .Use(p, p.lastCMD.Substring(p.lastCMD.IndexOf(' ') + 1));
                }
            }
            catch
            {
                Player.SendMessage(p, "An error occured!");
            }
        }

        // Token: 0x0600151E RID: 5406 RVA: 0x00074DD8 File Offset: 0x00072FD8
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/repeat - Repeats the last used command");
        }
    }
}