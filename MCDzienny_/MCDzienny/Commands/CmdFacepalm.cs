namespace MCDzienny.Commands
{
    // Token: 0x0200005D RID: 93
    public class CmdFacepalm : Command
    {
        // Token: 0x17000089 RID: 137
        // (get) Token: 0x06000235 RID: 565 RVA: 0x0000CB38 File Offset: 0x0000AD38
        public override string name
        {
            get { return "facepalm"; }
        }

        // Token: 0x1700008A RID: 138
        // (get) Token: 0x06000236 RID: 566 RVA: 0x0000CB40 File Offset: 0x0000AD40
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x1700008B RID: 139
        // (get) Token: 0x06000237 RID: 567 RVA: 0x0000CB48 File Offset: 0x0000AD48
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x1700008C RID: 140
        // (get) Token: 0x06000238 RID: 568 RVA: 0x0000CB50 File Offset: 0x0000AD50
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x1700008D RID: 141
        // (get) Token: 0x06000239 RID: 569 RVA: 0x0000CB54 File Offset: 0x0000AD54
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Banned; }
        }

        // Token: 0x1700008E RID: 142
        // (get) Token: 0x0600023A RID: 570 RVA: 0x0000CB58 File Offset: 0x0000AD58
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x0600023B RID: 571 RVA: 0x0000CB5C File Offset: 0x0000AD5C
        public override void Use(Player p, string message)
        {
            message = message.Trim();
            if (!(message != ""))
            {
                Player.GlobalMessage("{0}%s facepalmed.", p.color + p.PublicName);
                return;
            }

            var player = Player.Find(message);
            if (player == null)
            {
                Player.SendMessage(p, "Couldn't find the player.");
                return;
            }

            Player.GlobalMessage("{0}%s looked at {1}%s and facepalmed.", p.color + p.PublicName,
                player.color + player.PublicName);
        }

        // Token: 0x0600023C RID: 572 RVA: 0x0000CBF8 File Offset: 0x0000ADF8
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/facepalm - just facepalms ;)");
            Player.SendMessage(p, "/facepalm [player]");
        }
    }
}