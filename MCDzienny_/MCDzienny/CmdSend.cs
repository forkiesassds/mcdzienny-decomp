namespace MCDzienny
{
    public class CmdSend : Command
    {
        public override string name { get { return "send"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Builder; } }

        public override void Use(Player p, string message)
        {
            if (message == "" || message.IndexOf(' ') == -1)
            {
                Help(p);
                return;
            }
            Player player = Player.Find(message.Split(' ')[0]);
            string text = player == null ? message.Split(' ')[0] : player.name;
            string message2 = message.Substring(message.IndexOf(' ') + 1);
            Player.SendToInbox(p.name, text, message2);
            Player.SendMessage(p, string.Format("Message sent to &5{0}.", text));
            if (player != null)
            {
                player.SendMessage(string.Format("Message recieved from &5{0}.", p.name + Server.DefaultColor));
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/send [name] <message> - Sends <message> to [name].");
        }
    }
}