namespace MCDzienny
{
    // Token: 0x02000081 RID: 129
    public class CmdXundo : Command
    {
        // Token: 0x17000102 RID: 258
        // (get) Token: 0x06000367 RID: 871 RVA: 0x0001270C File Offset: 0x0001090C
        public override string name
        {
            get { return "xundo"; }
        }

        // Token: 0x17000103 RID: 259
        // (get) Token: 0x06000368 RID: 872 RVA: 0x00012714 File Offset: 0x00010914
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000104 RID: 260
        // (get) Token: 0x06000369 RID: 873 RVA: 0x0001271C File Offset: 0x0001091C
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x17000105 RID: 261
        // (get) Token: 0x0600036A RID: 874 RVA: 0x00012724 File Offset: 0x00010924
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000106 RID: 262
        // (get) Token: 0x0600036B RID: 875 RVA: 0x00012728 File Offset: 0x00010928
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x0600036C RID: 876 RVA: 0x0001272C File Offset: 0x0001092C
        public override void Use(Player p, string message)
        {
            var player = Player.Find(message);
            if (player == null)
            {
                Player.SendMessage(p, "Could not find the player specified.");
                return;
            }

            if (p != null && player.group.Permission >= p.group.Permission)
            {
                Player.SendMessage(p, "You don't have the permission to undo this player actions.");
                return;
            }

            all.Find("undo").Use(null, message + " all");
        }

        // Token: 0x0600036D RID: 877 RVA: 0x00012798 File Offset: 0x00010998
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/xundo [player] - undoes all player actions.");
        }
    }
}