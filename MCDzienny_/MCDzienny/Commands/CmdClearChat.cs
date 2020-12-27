namespace MCDzienny
{
    // Token: 0x02000122 RID: 290
    public class CmdClearChat : Command
    {
        // Token: 0x040003DF RID: 991
        private static readonly byte[] SpacedBuffer = new byte[65];

        // Token: 0x060008B4 RID: 2228 RVA: 0x0002BF60 File Offset: 0x0002A160
        public CmdClearChat()
        {
            for (var i = 1; i < 65; i++) SpacedBuffer[i] = 32;
        }

        // Token: 0x170003FB RID: 1019
        // (get) Token: 0x060008AF RID: 2223 RVA: 0x0002BF40 File Offset: 0x0002A140
        public override string name
        {
            get { return "clearchat"; }
        }

        // Token: 0x170003FC RID: 1020
        // (get) Token: 0x060008B0 RID: 2224 RVA: 0x0002BF48 File Offset: 0x0002A148
        public override string shortcut
        {
            get { return "cc"; }
        }

        // Token: 0x170003FD RID: 1021
        // (get) Token: 0x060008B1 RID: 2225 RVA: 0x0002BF50 File Offset: 0x0002A150
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x170003FE RID: 1022
        // (get) Token: 0x060008B2 RID: 2226 RVA: 0x0002BF58 File Offset: 0x0002A158
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170003FF RID: 1023
        // (get) Token: 0x060008B3 RID: 2227 RVA: 0x0002BF5C File Offset: 0x0002A15C
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Banned; }
        }

        // Token: 0x060008B5 RID: 2229 RVA: 0x0002BF8C File Offset: 0x0002A18C
        public static void SendEmptyChatMessages(Player p)
        {
            SpacedBuffer[0] = p.id;
            p.SendRaw(13, SpacedBuffer);
        }

        // Token: 0x060008B6 RID: 2230 RVA: 0x0002BFA8 File Offset: 0x0002A1A8
        public static void Send20EmptyLines(Player p)
        {
            for (var i = 0; i < 20; i++) SendEmptyChatMessages(p);
        }

        // Token: 0x060008B7 RID: 2231 RVA: 0x0002BFC8 File Offset: 0x0002A1C8
        public override void Use(Player p, string message)
        {
            message = message.Trim();
            if (message != "")
            {
                if (p != null && p.group.Permission < LevelPermission.Operator)
                {
                    Player.SendMessage(p, "You have to be OP+ to use /clearchat on others.");
                    return;
                }

                if (message.ToLower() == "all")
                {
                    Player.players.ForEachSync(delegate(Player pl) { Send20EmptyLines(pl); });
                    Player.SendMessage(p, "Chat was cleared for everyone.");
                    return;
                }

                var player = Player.Find(message);
                if (player == null)
                {
                    Player.SendMessage(p, "Couldn't find the player.");
                    return;
                }

                Send20EmptyLines(player);
                Player.SendMessage(p, "Chat was cleared for " + player.name);
            }
            else
            {
                if (p == null)
                {
                    Help(p);
                    return;
                }

                Send20EmptyLines(p);
            }
        }

        // Token: 0x060008B8 RID: 2232 RVA: 0x0002C094 File Offset: 0x0002A294
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/clearchat - cleares your chat.");
            Player.SendMessage(p, "/clearchat all - cleares chat for everyone. OP+");
            Player.SendMessage(p, "/clearchat [player] - clears chat for the [player]. OP+");
            Player.SendMessage(p, "Shortcut: /cc");
        }
    }
}