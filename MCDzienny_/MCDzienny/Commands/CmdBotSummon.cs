namespace MCDzienny
{
    // Token: 0x02000249 RID: 585
    public class CmdBotSummon : Command
    {
        // Token: 0x17000600 RID: 1536
        // (get) Token: 0x060010EE RID: 4334 RVA: 0x0005C300 File Offset: 0x0005A500
        public override string name
        {
            get { return "botsummon"; }
        }

        // Token: 0x17000601 RID: 1537
        // (get) Token: 0x060010EF RID: 4335 RVA: 0x0005C308 File Offset: 0x0005A508
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000602 RID: 1538
        // (get) Token: 0x060010F0 RID: 4336 RVA: 0x0005C310 File Offset: 0x0005A510
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x17000603 RID: 1539
        // (get) Token: 0x060010F1 RID: 4337 RVA: 0x0005C318 File Offset: 0x0005A518
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x17000604 RID: 1540
        // (get) Token: 0x060010F2 RID: 4338 RVA: 0x0005C31C File Offset: 0x0005A51C
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Admin; }
        }

        // Token: 0x17000605 RID: 1541
        // (get) Token: 0x060010F3 RID: 4339 RVA: 0x0005C320 File Offset: 0x0005A520
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x17000606 RID: 1542
        // (get) Token: 0x060010F4 RID: 4340 RVA: 0x0005C324 File Offset: 0x0005A524
        public override string CustomName
        {
            get { return Lang.Command.BotSummonName; }
        }

        // Token: 0x060010F6 RID: 4342 RVA: 0x0005C334 File Offset: 0x0005A534
        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }

            var playerBot = PlayerBot.Find(message);
            if (playerBot == null)
            {
                Player.SendMessage(p, string.Format(Lang.Command.BotSummonMessage, message));
                return;
            }

            if (p.level != playerBot.level)
            {
                Player.SendMessage(p, string.Format(Lang.Command.BotSummonMessage1, playerBot.name));
                return;
            }

            playerBot.SetPos(p.pos[0], p.pos[1], p.pos[2], p.rot[0], 0);
        }

        // Token: 0x060010F7 RID: 4343 RVA: 0x0005C3C4 File Offset: 0x0005A5C4
        public override void Help(Player p)
        {
            Player.SendMessage(p, Lang.Command.BotSummonHelp);
        }
    }
}