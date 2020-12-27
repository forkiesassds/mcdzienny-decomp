namespace MCDzienny
{
    // Token: 0x020000D8 RID: 216
    public class CmdLives : Command
    {
        // Token: 0x17000314 RID: 788
        // (get) Token: 0x060006FC RID: 1788 RVA: 0x000240D8 File Offset: 0x000222D8
        public override string name
        {
            get { return "lives"; }
        }

        // Token: 0x17000315 RID: 789
        // (get) Token: 0x060006FD RID: 1789 RVA: 0x000240E0 File Offset: 0x000222E0
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000316 RID: 790
        // (get) Token: 0x060006FE RID: 1790 RVA: 0x000240E8 File Offset: 0x000222E8
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x17000317 RID: 791
        // (get) Token: 0x060006FF RID: 1791 RVA: 0x000240F0 File Offset: 0x000222F0
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000318 RID: 792
        // (get) Token: 0x06000700 RID: 1792 RVA: 0x000240F4 File Offset: 0x000222F4
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Banned; }
        }

        // Token: 0x17000319 RID: 793
        // (get) Token: 0x06000701 RID: 1793 RVA: 0x000240F8 File Offset: 0x000222F8
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x06000703 RID: 1795 RVA: 0x00024104 File Offset: 0x00022304
        public override void Use(Player p, string message)
        {
            Player.SendMessage(p, string.Format("You have: {0} lives.", p.lives));
        }

        // Token: 0x06000704 RID: 1796 RVA: 0x00024124 File Offset: 0x00022324
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/lives - Displays how many lives you have.");
        }
    }
}