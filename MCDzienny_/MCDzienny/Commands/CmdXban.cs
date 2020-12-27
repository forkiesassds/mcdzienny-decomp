namespace MCDzienny
{
    // Token: 0x02000120 RID: 288
    public class CmdXban : Command
    {
        // Token: 0x170003F1 RID: 1009
        // (get) Token: 0x0600089F RID: 2207 RVA: 0x0002BC98 File Offset: 0x00029E98
        public override string name
        {
            get { return "xban"; }
        }

        // Token: 0x170003F2 RID: 1010
        // (get) Token: 0x060008A0 RID: 2208 RVA: 0x0002BCA0 File Offset: 0x00029EA0
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170003F3 RID: 1011
        // (get) Token: 0x060008A1 RID: 2209 RVA: 0x0002BCA8 File Offset: 0x00029EA8
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x170003F4 RID: 1012
        // (get) Token: 0x060008A2 RID: 2210 RVA: 0x0002BCB0 File Offset: 0x00029EB0
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x170003F5 RID: 1013
        // (get) Token: 0x060008A3 RID: 2211 RVA: 0x0002BCB4 File Offset: 0x00029EB4
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Admin; }
        }

        // Token: 0x060008A5 RID: 2213 RVA: 0x0002BCC0 File Offset: 0x00029EC0
        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }

            var text = message.Split(' ')[0];
            var player = Player.Find(message.Split(' ')[0]);
            if (player != null)
            {
                player.disconnectionReason = DisconnectionReason.IPBan;
                all.Find("ban").Use(p, text);
                all.Find("undo").Use(p, text + " all");
                all.Find("banip").Use(p, "@" + text);
                all.Find("kick").Use(p, message);
                all.Find("undo").Use(p, text + " all");
                return;
            }

            all.Find("ban").Use(p, text);
            all.Find("banip").Use(p, "@" + text);
            all.Find("undo").Use(p, text + " all");
        }

        // Token: 0x060008A6 RID: 2214 RVA: 0x0002BE08 File Offset: 0x0002A008
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/xban [name] - undo [name] all + banip + ban + kick ^^ ");
            Player.SendMessage(p, "/xban [name] [reason] ");
        }
    }
}