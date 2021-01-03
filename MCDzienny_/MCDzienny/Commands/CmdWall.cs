namespace MCDzienny
{
    // Token: 0x0200007F RID: 127
    public class CmdWall : Command
    {
        // Token: 0x170000F6 RID: 246
        // (get) Token: 0x06000355 RID: 853 RVA: 0x000125E8 File Offset: 0x000107E8
        public override string name
        {
            get { return "wall"; }
        }

        // Token: 0x170000F7 RID: 247
        // (get) Token: 0x06000356 RID: 854 RVA: 0x000125F0 File Offset: 0x000107F0
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170000F8 RID: 248
        // (get) Token: 0x06000357 RID: 855 RVA: 0x000125F8 File Offset: 0x000107F8
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x170000F9 RID: 249
        // (get) Token: 0x06000358 RID: 856 RVA: 0x00012600 File Offset: 0x00010800
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170000FA RID: 250
        // (get) Token: 0x06000359 RID: 857 RVA: 0x00012604 File Offset: 0x00010804
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Builder; }
        }

        // Token: 0x170000FB RID: 251
        // (get) Token: 0x0600035A RID: 858 RVA: 0x00012608 File Offset: 0x00010808
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x0600035B RID: 859 RVA: 0x0001260C File Offset: 0x0001080C
        public override void Use(Player p, string message)
        {
            all.Find("line").Use(p, "wall");
        }

        // Token: 0x0600035C RID: 860 RVA: 0x00012628 File Offset: 0x00010828
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/wall - place two blocks to create a wall.");
        }
    }
}