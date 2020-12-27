namespace MCDzienny
{
    // Token: 0x0200027C RID: 636
    public class CmdPaint : Command
    {
        // Token: 0x170006C0 RID: 1728
        // (get) Token: 0x06001245 RID: 4677 RVA: 0x00065068 File Offset: 0x00063268
        public override string name
        {
            get { return "paint"; }
        }

        // Token: 0x170006C1 RID: 1729
        // (get) Token: 0x06001246 RID: 4678 RVA: 0x00065070 File Offset: 0x00063270
        public override string shortcut
        {
            get { return "p"; }
        }

        // Token: 0x170006C2 RID: 1730
        // (get) Token: 0x06001247 RID: 4679 RVA: 0x00065078 File Offset: 0x00063278
        public override string type
        {
            get { return "build"; }
        }

        // Token: 0x170006C3 RID: 1731
        // (get) Token: 0x06001248 RID: 4680 RVA: 0x00065080 File Offset: 0x00063280
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x170006C4 RID: 1732
        // (get) Token: 0x06001249 RID: 4681 RVA: 0x00065084 File Offset: 0x00063284
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Builder; }
        }

        // Token: 0x170006C5 RID: 1733
        // (get) Token: 0x0600124A RID: 4682 RVA: 0x00065088 File Offset: 0x00063288
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x0600124B RID: 4683 RVA: 0x0006508C File Offset: 0x0006328C
        public override void Use(Player p, string message)
        {
            if (message != "")
            {
                Help(p);
                return;
            }

            p.painting = !p.painting;
            if (p.painting)
                Player.SendMessage(p, string.Format("Painting mode: &aON{0}.", Server.DefaultColor));
            else
                Player.SendMessage(p, string.Format("Painting mode: &cOFF{0}.", Server.DefaultColor));
            p.BlockAction = 0;
        }

        // Token: 0x0600124C RID: 4684 RVA: 0x000650F8 File Offset: 0x000632F8
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/paint - Turns painting mode on/off.");
        }
    }
}