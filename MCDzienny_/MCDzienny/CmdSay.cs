namespace MCDzienny
{
    class CmdSay : Command
    {
        public override string name { get { return "say"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }
            if (p != null && (p.muted || p.IsTempMuted))
            {
                Player.SendMessage(p, "Sorry, you are muted.");
                return;
            }
            message = message.Replace("%", "&");
            Player.GlobalChat(p, message, showname: false);
            message = message.Replace("&", "\u0003");
            Player.IRCSay(message);
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/say - broadcasts a global message to everyone in the server.");
        }
    }
}