namespace MCDzienny
{
    // Token: 0x020002D8 RID: 728
    public class CmdDelete : Command
    {
        // Token: 0x170007FA RID: 2042
        // (get) Token: 0x0600149D RID: 5277 RVA: 0x00071E54 File Offset: 0x00070054
        public override string name
        {
            get { return "delete"; }
        }

        // Token: 0x170007FB RID: 2043
        // (get) Token: 0x0600149E RID: 5278 RVA: 0x00071E5C File Offset: 0x0007005C
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170007FC RID: 2044
        // (get) Token: 0x0600149F RID: 5279 RVA: 0x00071E64 File Offset: 0x00070064
        public override string type
        {
            get { return "build"; }
        }

        // Token: 0x170007FD RID: 2045
        // (get) Token: 0x060014A0 RID: 5280 RVA: 0x00071E6C File Offset: 0x0007006C
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x170007FE RID: 2046
        // (get) Token: 0x060014A1 RID: 5281 RVA: 0x00071E70 File Offset: 0x00070070
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.AdvBuilder; }
        }

        // Token: 0x170007FF RID: 2047
        // (get) Token: 0x060014A2 RID: 5282 RVA: 0x00071E74 File Offset: 0x00070074
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x17000800 RID: 2048
        // (get) Token: 0x060014A3 RID: 5283 RVA: 0x00071E78 File Offset: 0x00070078
        public override CommandScope Scope
        {
            get { return CommandScope.Freebuild | CommandScope.Lava | CommandScope.Home; }
        }

        // Token: 0x060014A4 RID: 5284 RVA: 0x00071E7C File Offset: 0x0007007C
        public override void Use(Player p, string message)
        {
            if (message != "")
            {
                Help(p);
                return;
            }

            p.deleteMode = !p.deleteMode;
            Player.SendMessage(p, string.Format("Delete mode: &a{0}", p.deleteMode));
        }

        // Token: 0x060014A5 RID: 5285 RVA: 0x00071EC8 File Offset: 0x000700C8
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/delete - Deletes any block you click");
            Player.SendMessage(p, "\"any block\" meaning door_air, portals, mb's, etc");
        }
    }
}