namespace MCDzienny
{
    // Token: 0x020000DC RID: 220
    public class CmdPoints : Command
    {
        // Token: 0x17000324 RID: 804
        // (get) Token: 0x06000717 RID: 1815 RVA: 0x00024598 File Offset: 0x00022798
        public override string name
        {
            get { return "money"; }
        }

        // Token: 0x17000325 RID: 805
        // (get) Token: 0x06000718 RID: 1816 RVA: 0x000245A0 File Offset: 0x000227A0
        public override string shortcut
        {
            get { return "points"; }
        }

        // Token: 0x17000326 RID: 806
        // (get) Token: 0x06000719 RID: 1817 RVA: 0x000245A8 File Offset: 0x000227A8
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x17000327 RID: 807
        // (get) Token: 0x0600071A RID: 1818 RVA: 0x000245B0 File Offset: 0x000227B0
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000328 RID: 808
        // (get) Token: 0x0600071B RID: 1819 RVA: 0x000245B4 File Offset: 0x000227B4
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Banned; }
        }

        // Token: 0x17000329 RID: 809
        // (get) Token: 0x0600071C RID: 1820 RVA: 0x000245B8 File Offset: 0x000227B8
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x0600071E RID: 1822 RVA: 0x000245C4 File Offset: 0x000227C4
        public override void Use(Player p, string message)
        {
            Player.SendMessage(p, string.Format("You have: {0} {1}.", p.money, Server.moneys));
        }

        // Token: 0x0600071F RID: 1823 RVA: 0x000245E8 File Offset: 0x000227E8
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/money - Displays the amount of " + Server.moneys + " you have.");
        }
    }
}