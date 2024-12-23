namespace MCDzienny
{
    public class CmdXban : Command
    {
        public override string name { get { return "xban"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }
            string text = message.Split(' ')[0];
            Player player = Player.Find(message.Split(' ')[0]);
            if (player != null)
            {
                player.disconnectionReason = DisconnectionReason.IPBan;
                all.Find("ban").Use(p, text);
                all.Find("undo").Use(p, text + " all");
                all.Find("banip").Use(p, "@" + text);
                all.Find("kick").Use(p, message);
                all.Find("undo").Use(p, text + " all");
            }
            else
            {
                all.Find("ban").Use(p, text);
                all.Find("banip").Use(p, "@" + text);
                all.Find("undo").Use(p, text + " all");
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/xban [name] - undo [name] all + banip + ban + kick ^^ ");
            Player.SendMessage(p, "/xban [name] [reason] ");
        }
    }
}