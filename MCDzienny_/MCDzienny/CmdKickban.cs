namespace MCDzienny
{
    public class CmdKickban : Command
    {
        public override string name { get { return "kickban"; } }

        public override string shortcut { get { return "kb"; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }
            string message2 = message.Split(' ')[0];
            Player player = Player.Find(message2);
            all.Find("ban").Use(p, message2);
            if (player != null)
            {
                player.disconnectionReason = DisconnectionReason.NameBan;
                all.Find("kick").Use(p, message);
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/kickban <player> [message] - Kicks and bans a player with an optional message.");
        }
    }
}