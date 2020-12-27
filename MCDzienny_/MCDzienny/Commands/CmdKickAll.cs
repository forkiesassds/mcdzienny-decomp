namespace MCDzienny
{
    // Token: 0x02000127 RID: 295
    public class CmdKickAll : Command
    {
        // Token: 0x1700040B RID: 1035
        // (get) Token: 0x060008D1 RID: 2257 RVA: 0x0002C620 File Offset: 0x0002A820
        public override string name
        {
            get { return "kickall"; }
        }

        // Token: 0x1700040C RID: 1036
        // (get) Token: 0x060008D2 RID: 2258 RVA: 0x0002C628 File Offset: 0x0002A828
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x1700040D RID: 1037
        // (get) Token: 0x060008D3 RID: 2259 RVA: 0x0002C630 File Offset: 0x0002A830
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x1700040E RID: 1038
        // (get) Token: 0x060008D4 RID: 2260 RVA: 0x0002C638 File Offset: 0x0002A838
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x1700040F RID: 1039
        // (get) Token: 0x060008D5 RID: 2261 RVA: 0x0002C63C File Offset: 0x0002A83C
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x060008D6 RID: 2262 RVA: 0x0002C640 File Offset: 0x0002A840
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/kickall - kicks all players from the server.");
        }

        // Token: 0x060008D7 RID: 2263 RVA: 0x0002C650 File Offset: 0x0002A850
        public override void Use(Player p, string message)
        {
            var count = Player.players.Count;
            if (p != null)
            {
                for (var i = count - 1; i >= 0; i--)
                    try
                    {
                        Player.players[i].disconnectionReason = DisconnectionReason.Kicked;
                        Player.players[i].Kick(string.Format("You were kicked by {0}", p.PublicName));
                    }
                    catch
                    {
                    }

                return;
            }

            for (var j = count - 1; j >= 0; j--)
                try
                {
                    Player.players[j].disconnectionReason = DisconnectionReason.Kicked;
                    Player.players[j].Kick("You were kicked.");
                }
                catch
                {
                }
        }
    }
}