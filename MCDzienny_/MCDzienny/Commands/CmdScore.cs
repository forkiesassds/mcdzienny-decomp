namespace MCDzienny
{
    // Token: 0x020000E0 RID: 224
    public class CmdScore : Command
    {
        // Token: 0x1700033B RID: 827
        // (get) Token: 0x0600073A RID: 1850 RVA: 0x00024CE8 File Offset: 0x00022EE8
        public override string name
        {
            get { return "experience"; }
        }

        // Token: 0x1700033C RID: 828
        // (get) Token: 0x0600073B RID: 1851 RVA: 0x00024CF0 File Offset: 0x00022EF0
        public override string shortcut
        {
            get { return "score"; }
        }

        // Token: 0x1700033D RID: 829
        // (get) Token: 0x0600073C RID: 1852 RVA: 0x00024CF8 File Offset: 0x00022EF8
        public override string type
        {
            get { return ""; }
        }

        // Token: 0x1700033E RID: 830
        // (get) Token: 0x0600073D RID: 1853 RVA: 0x00024D00 File Offset: 0x00022F00
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x1700033F RID: 831
        // (get) Token: 0x0600073E RID: 1854 RVA: 0x00024D04 File Offset: 0x00022F04
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Banned; }
        }

        // Token: 0x17000340 RID: 832
        // (get) Token: 0x0600073F RID: 1855 RVA: 0x00024D08 File Offset: 0x00022F08
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x17000341 RID: 833
        // (get) Token: 0x06000740 RID: 1856 RVA: 0x00024D0C File Offset: 0x00022F0C
        public override CommandScope Scope
        {
            get { return CommandScope.Lava; }
        }

        // Token: 0x06000742 RID: 1858 RVA: 0x00024D18 File Offset: 0x00022F18
        public override void Use(Player p, string message)
        {
            Player.SendMessage(p, string.Format("Your best score: {0}", p.bestScore));
            Player.SendMessage(p, string.Format("Your total experience: {0}", p.totalScore));
        }

        // Token: 0x06000743 RID: 1859 RVA: 0x00024D50 File Offset: 0x00022F50
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/score - Shows your best score and total experience.");
        }
    }
}