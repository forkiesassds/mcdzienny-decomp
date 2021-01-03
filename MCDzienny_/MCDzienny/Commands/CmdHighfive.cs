namespace MCDzienny.Commands
{
    // Token: 0x02000060 RID: 96
    public class CmdHighfive : Command
    {
        // Token: 0x1700009B RID: 155
        // (get) Token: 0x06000256 RID: 598 RVA: 0x0000D0A0 File Offset: 0x0000B2A0
        public override string name
        {
            get { return "highfive"; }
        }

        // Token: 0x1700009C RID: 156
        // (get) Token: 0x06000257 RID: 599 RVA: 0x0000D0A8 File Offset: 0x0000B2A8
        public override string shortcut
        {
            get { return "high5"; }
        }

        // Token: 0x1700009D RID: 157
        // (get) Token: 0x06000258 RID: 600 RVA: 0x0000D0B0 File Offset: 0x0000B2B0
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x1700009E RID: 158
        // (get) Token: 0x06000259 RID: 601 RVA: 0x0000D0B8 File Offset: 0x0000B2B8
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x1700009F RID: 159
        // (get) Token: 0x0600025A RID: 602 RVA: 0x0000D0BC File Offset: 0x0000B2BC
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Banned; }
        }

        // Token: 0x170000A0 RID: 160
        // (get) Token: 0x0600025B RID: 603 RVA: 0x0000D0C0 File Offset: 0x0000B2C0
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x0600025C RID: 604 RVA: 0x0000D0C4 File Offset: 0x0000B2C4
        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }

            var player = Player.Find(message);
            if (player == null || player.hidden)
            {
                Player.SendMessage(p, "Couldn't find the player.");
                return;
            }

            Player.GlobalMessage("{0} high fived {1}!", p.color + p.PublicName + Server.DefaultColor,
                player.color + player.PublicName + Server.DefaultColor);
            Player.SendMessage(player, "You were high fived by {0}", p.color + p.PublicName);
        }

        // Token: 0x0600025D RID: 605 RVA: 0x0000D170 File Offset: 0x0000B370
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/highfive [player] - high fives the [player].");
            Player.SendMessage(p, "Shortcut: /high5");
        }
    }
}