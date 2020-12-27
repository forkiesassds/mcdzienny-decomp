namespace MCDzienny
{
    // Token: 0x020002DE RID: 734
    internal class CmdPause : Command
    {
        // Token: 0x17000812 RID: 2066
        // (get) Token: 0x060014C9 RID: 5321 RVA: 0x00072BB0 File Offset: 0x00070DB0
        public override string name
        {
            get { return "pause"; }
        }

        // Token: 0x17000813 RID: 2067
        // (get) Token: 0x060014CA RID: 5322 RVA: 0x00072BB8 File Offset: 0x00070DB8
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000814 RID: 2068
        // (get) Token: 0x060014CB RID: 5323 RVA: 0x00072BC0 File Offset: 0x00070DC0
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x17000815 RID: 2069
        // (get) Token: 0x060014CC RID: 5324 RVA: 0x00072BC8 File Offset: 0x00070DC8
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000816 RID: 2070
        // (get) Token: 0x060014CD RID: 5325 RVA: 0x00072BCC File Offset: 0x00070DCC
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x060014CF RID: 5327 RVA: 0x00072BD8 File Offset: 0x00070DD8
        public override void Use(Player p, string message)
        {
            Server.pause = !Server.pause;
            if (Server.pause)
            {
                Player.SendMessage(p, "Physics was stopped.");
                return;
            }

            Player.SendMessage(p, "Physics is on.");
        }

        // Token: 0x060014D0 RID: 5328 RVA: 0x00072C08 File Offset: 0x00070E08
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/pause - pauses physics");
        }
    }
}