namespace MCDzienny
{
    public class CmdClearChat : Command
    {
        static readonly byte[] SpacedBuffer = new byte[65];

        public CmdClearChat()
        {
            for (int i = 1; i < 65; i++)
            {
                SpacedBuffer[i] = 32;
            }
        }

        public override string name { get { return "clearchat"; } }

        public override string shortcut { get { return "cc"; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }

        public static void SendEmptyChatMessages(Player p)
        {
            SpacedBuffer[0] = p.id;
            p.SendRaw(13, SpacedBuffer);
        }

        public static void Send20EmptyLines(Player p)
        {
            for (int i = 0; i < 20; i++)
            {
                SendEmptyChatMessages(p);
            }
        }

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
                Player player = Player.Find(message);
                if (player == null)
                {
                    Player.SendMessage(p, "Couldn't find the player.");
                    return;
                }
                Send20EmptyLines(player);
                Player.SendMessage(p, "Chat was cleared for " + player.name);
            }
            else if (p == null)
            {
                Help(p);
            }
            else
            {
                Send20EmptyLines(p);
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/clearchat - cleares your chat.");
            Player.SendMessage(p, "/clearchat all - cleares chat for everyone. OP+");
            Player.SendMessage(p, "/clearchat [player] - clears chat for the [player]. OP+");
            Player.SendMessage(p, "Shortcut: /cc");
        }
    }
}