namespace MCDzienny
{
    // Token: 0x020000EF RID: 239
    public class CmdDrop : Command
    {
        // Token: 0x1700035B RID: 859
        // (get) Token: 0x06000787 RID: 1927 RVA: 0x0002616C File Offset: 0x0002436C
        public override string name
        {
            get { return "drop"; }
        }

        // Token: 0x1700035C RID: 860
        // (get) Token: 0x06000788 RID: 1928 RVA: 0x00026174 File Offset: 0x00024374
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x1700035D RID: 861
        // (get) Token: 0x06000789 RID: 1929 RVA: 0x0002617C File Offset: 0x0002437C
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x1700035E RID: 862
        // (get) Token: 0x0600078B RID: 1931 RVA: 0x0002618C File Offset: 0x0002438C
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x1700035F RID: 863
        // (get) Token: 0x0600078C RID: 1932 RVA: 0x00026190 File Offset: 0x00024390
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Builder; }
        }

        // Token: 0x17000360 RID: 864
        // (get) Token: 0x0600078D RID: 1933 RVA: 0x00026194 File Offset: 0x00024394
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x0600078E RID: 1934 RVA: 0x00026198 File Offset: 0x00024398
        public override void Use(Player p, string message)
        {
            if (message != "")
            {
                Help(p);
                return;
            }

            if (p.hasflag != null)
            {
                p.level.ctfgame.DropFlag(p, p.hasflag);
                return;
            }

            Player.SendMessage(p, "You are not carrying a flag.");
        }

        // Token: 0x0600078F RID: 1935 RVA: 0x000261E8 File Offset: 0x000243E8
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/drop - Drop the flag if you are carrying it.");
        }
    }
}