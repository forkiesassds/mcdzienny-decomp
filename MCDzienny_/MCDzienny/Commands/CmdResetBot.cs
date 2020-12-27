namespace MCDzienny
{
    // Token: 0x0200028E RID: 654
    internal class CmdResetBot : Command
    {
        // Token: 0x170006F7 RID: 1783
        // (get) Token: 0x060012B9 RID: 4793 RVA: 0x0006778C File Offset: 0x0006598C
        public override string name
        {
            get { return "resetbot"; }
        }

        // Token: 0x170006F8 RID: 1784
        // (get) Token: 0x060012BA RID: 4794 RVA: 0x00067794 File Offset: 0x00065994
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170006F9 RID: 1785
        // (get) Token: 0x060012BB RID: 4795 RVA: 0x0006779C File Offset: 0x0006599C
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x170006FA RID: 1786
        // (get) Token: 0x060012BC RID: 4796 RVA: 0x000677A4 File Offset: 0x000659A4
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170006FB RID: 1787
        // (get) Token: 0x060012BD RID: 4797 RVA: 0x000677A8 File Offset: 0x000659A8
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x060012BF RID: 4799 RVA: 0x000677B4 File Offset: 0x000659B4
        public override void Use(Player p, string message)
        {
            IRCBot.Reset();
        }

        // Token: 0x060012C0 RID: 4800 RVA: 0x000677BC File Offset: 0x000659BC
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/resetbot - reloads the IRCBot. FOR EMERGENCIES ONLY!");
        }
    }
}