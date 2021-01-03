namespace MCDzienny
{
    // Token: 0x02000109 RID: 265
    public class CmdFlipHeads : Command
    {
        // Token: 0x170003AF RID: 943
        // (get) Token: 0x06000820 RID: 2080 RVA: 0x00028DF0 File Offset: 0x00026FF0
        public override string name
        {
            get { return "flipheads"; }
        }

        // Token: 0x170003B0 RID: 944
        // (get) Token: 0x06000821 RID: 2081 RVA: 0x00028DF8 File Offset: 0x00026FF8
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170003B1 RID: 945
        // (get) Token: 0x06000822 RID: 2082 RVA: 0x00028E00 File Offset: 0x00027000
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x170003B2 RID: 946
        // (get) Token: 0x06000823 RID: 2083 RVA: 0x00028E08 File Offset: 0x00027008
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170003B3 RID: 947
        // (get) Token: 0x06000824 RID: 2084 RVA: 0x00028E0C File Offset: 0x0002700C
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x06000826 RID: 2086 RVA: 0x00028E18 File Offset: 0x00027018
        public override void Use(Player p, string message)
        {
            Server.flipHead = !Server.flipHead;
            if (Server.flipHead)
            {
                foreach (var player in Player.players) player.flipHead = true;
                Player.GlobalChat(p, "All necks were broken", false);
                return;
            }

            foreach (var player2 in Player.players) player2.flipHead = false;
            Player.GlobalChat(p, "All necks were mended", false);
        }

        // Token: 0x06000827 RID: 2087 RVA: 0x00028ED8 File Offset: 0x000270D8
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/flipheads - Does as it says on the tin");
        }
    }
}