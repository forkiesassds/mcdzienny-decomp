namespace MCDzienny
{
    // Token: 0x02000295 RID: 661
    internal class CmdSay : Command
    {
        // Token: 0x17000716 RID: 1814
        // (get) Token: 0x060012EF RID: 4847 RVA: 0x00068880 File Offset: 0x00066A80
        public override string name
        {
            get { return "say"; }
        }

        // Token: 0x17000717 RID: 1815
        // (get) Token: 0x060012F0 RID: 4848 RVA: 0x00068888 File Offset: 0x00066A88
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000718 RID: 1816
        // (get) Token: 0x060012F1 RID: 4849 RVA: 0x00068890 File Offset: 0x00066A90
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x17000719 RID: 1817
        // (get) Token: 0x060012F2 RID: 4850 RVA: 0x00068898 File Offset: 0x00066A98
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x1700071A RID: 1818
        // (get) Token: 0x060012F3 RID: 4851 RVA: 0x0006889C File Offset: 0x00066A9C
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x060012F5 RID: 4853 RVA: 0x000688A8 File Offset: 0x00066AA8
        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }

            if (p != null && (p.muted || p.IsTempMuted))
            {
                Player.SendMessage(p, "Sorry, you are muted.");
                return;
            }

            message = message.Replace("%", "&");
            Player.GlobalChat(p, message, false);
            message = message.Replace("&", "\u0003");
            Player.IRCSay(message);
        }

        // Token: 0x060012F6 RID: 4854 RVA: 0x0006891C File Offset: 0x00066B1C
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/say - broadcasts a global message to everyone in the server.");
        }
    }
}