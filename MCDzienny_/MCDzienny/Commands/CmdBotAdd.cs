namespace MCDzienny
{
    // Token: 0x02000247 RID: 583
    public class CmdBotAdd : Command
    {
        // Token: 0x170005F4 RID: 1524
        // (get) Token: 0x060010DC RID: 4316 RVA: 0x0005C11C File Offset: 0x0005A31C
        public override string name
        {
            get { return "botadd"; }
        }

        // Token: 0x170005F5 RID: 1525
        // (get) Token: 0x060010DD RID: 4317 RVA: 0x0005C124 File Offset: 0x0005A324
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170005F6 RID: 1526
        // (get) Token: 0x060010DE RID: 4318 RVA: 0x0005C12C File Offset: 0x0005A32C
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x170005F7 RID: 1527
        // (get) Token: 0x060010DF RID: 4319 RVA: 0x0005C134 File Offset: 0x0005A334
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x170005F8 RID: 1528
        // (get) Token: 0x060010E0 RID: 4320 RVA: 0x0005C138 File Offset: 0x0005A338
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Admin; }
        }

        // Token: 0x170005F9 RID: 1529
        // (get) Token: 0x060010E1 RID: 4321 RVA: 0x0005C13C File Offset: 0x0005A33C
        public override string CustomName
        {
            get { return Lang.Command.BotAddName; }
        }

        // Token: 0x060010E3 RID: 4323 RVA: 0x0005C14C File Offset: 0x0005A34C
        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }

            if (!PlayerBot.ValidName(message))
            {
                Player.SendMessage(p, string.Format(Lang.Command.BotAddMessage, message));
                return;
            }

            PlayerBot.playerbots.Add(new PlayerBot(message, p.level, p.pos[0], p.pos[1], p.pos[2], p.rot[0], 0));
        }

        // Token: 0x060010E4 RID: 4324 RVA: 0x0005C1C8 File Offset: 0x0005A3C8
        public override void Help(Player p)
        {
            Player.SendMessage(p, Lang.Command.BotAddHelp);
        }
    }
}