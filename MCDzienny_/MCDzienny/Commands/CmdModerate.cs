namespace MCDzienny
{
    // Token: 0x02000278 RID: 632
    public class CmdModerate : Command
    {
        // Token: 0x170006AC RID: 1708
        // (get) Token: 0x06001223 RID: 4643 RVA: 0x00064818 File Offset: 0x00062A18
        public override string name
        {
            get { return "moderate"; }
        }

        // Token: 0x170006AD RID: 1709
        // (get) Token: 0x06001224 RID: 4644 RVA: 0x00064820 File Offset: 0x00062A20
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170006AE RID: 1710
        // (get) Token: 0x06001225 RID: 4645 RVA: 0x00064828 File Offset: 0x00062A28
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x170006AF RID: 1711
        // (get) Token: 0x06001226 RID: 4646 RVA: 0x00064830 File Offset: 0x00062A30
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170006B0 RID: 1712
        // (get) Token: 0x06001227 RID: 4647 RVA: 0x00064834 File Offset: 0x00062A34
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x06001229 RID: 4649 RVA: 0x00064840 File Offset: 0x00062A40
        public override void Use(Player p, string message)
        {
            if (message != "")
            {
                Help(p);
                return;
            }

            if (Server.chatmod)
            {
                Server.chatmod = false;
                Player.GlobalChat(null,
                    string.Format("{0}Chat moderation has been disabled.  Everyone can now speak.",
                        Server.DefaultColor), false);
                return;
            }

            Server.chatmod = true;
            Player.GlobalChat(null,
                string.Format("{0}Chat moderation engaged!  Silence the plebians!", Server.DefaultColor), false);
        }

        // Token: 0x0600122A RID: 4650 RVA: 0x000648A4 File Offset: 0x00062AA4
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/moderate - Toggles chat moderation status.  When enabled, only voiced");
            Player.SendMessage(p, "players may speak.");
        }
    }
}