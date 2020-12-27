namespace MCDzienny
{
    // Token: 0x02000113 RID: 275
    public class CmdCrashServer : Command
    {
        // Token: 0x170003C5 RID: 965
        // (get) Token: 0x0600084B RID: 2123 RVA: 0x0002A65C File Offset: 0x0002885C
        public override string name
        {
            get { return "crashserver"; }
        }

        // Token: 0x170003C6 RID: 966
        // (get) Token: 0x0600084C RID: 2124 RVA: 0x0002A664 File Offset: 0x00028864
        public override string shortcut
        {
            get { return "crash"; }
        }

        // Token: 0x170003C7 RID: 967
        // (get) Token: 0x0600084D RID: 2125 RVA: 0x0002A66C File Offset: 0x0002886C
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x170003C8 RID: 968
        // (get) Token: 0x0600084E RID: 2126 RVA: 0x0002A674 File Offset: 0x00028874
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170003C9 RID: 969
        // (get) Token: 0x0600084F RID: 2127 RVA: 0x0002A678 File Offset: 0x00028878
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Banned; }
        }

        // Token: 0x170003CA RID: 970
        // (get) Token: 0x06000850 RID: 2128 RVA: 0x0002A67C File Offset: 0x0002887C
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x06000852 RID: 2130 RVA: 0x0002A688 File Offset: 0x00028888
        public override void Use(Player p, string message)
        {
            if (message != "")
            {
                Help(p);
                return;
            }

            Player.GlobalMessageOps(string.Format("{0} used &b/crashserver", p.color + p.name + Server.DefaultColor));
            p.Kick("Server crash! Error code 0x0005A4");
        }

        // Token: 0x06000853 RID: 2131 RVA: 0x0002A6DC File Offset: 0x000288DC
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/crashserver - Crash the server with a generic error");
        }
    }
}