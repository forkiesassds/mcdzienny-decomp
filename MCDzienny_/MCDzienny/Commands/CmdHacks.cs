namespace MCDzienny
{
    // Token: 0x02000264 RID: 612
    public class CmdHacks : Command
    {
        // Token: 0x17000661 RID: 1633
        // (get) Token: 0x06001198 RID: 4504 RVA: 0x000610C0 File Offset: 0x0005F2C0
        public override string name
        {
            get { return "hacks"; }
        }

        // Token: 0x17000662 RID: 1634
        // (get) Token: 0x06001199 RID: 4505 RVA: 0x000610C8 File Offset: 0x0005F2C8
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000663 RID: 1635
        // (get) Token: 0x0600119A RID: 4506 RVA: 0x000610D0 File Offset: 0x0005F2D0
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x17000664 RID: 1636
        // (get) Token: 0x0600119B RID: 4507 RVA: 0x000610D8 File Offset: 0x0005F2D8
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000665 RID: 1637
        // (get) Token: 0x0600119C RID: 4508 RVA: 0x000610DC File Offset: 0x0005F2DC
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Banned; }
        }

        // Token: 0x17000666 RID: 1638
        // (get) Token: 0x0600119D RID: 4509 RVA: 0x000610E0 File Offset: 0x0005F2E0
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x0600119F RID: 4511 RVA: 0x000610EC File Offset: 0x0005F2EC
        public override void Use(Player p, string message)
        {
            if (message != "")
            {
                Help(p);
                return;
            }

            p.Kick("Your IP has been backtraced + reported to FBI Cyber Crimes Unit.");
        }

        // Token: 0x060011A0 RID: 4512 RVA: 0x00061110 File Offset: 0x0005F310
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/hacks - HACK THE PLANET");
        }
    }
}