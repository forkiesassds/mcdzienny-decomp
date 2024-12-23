namespace MCDzienny
{
    public class CmdMake : Command
    {
        public override string name { get { return "make"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }

        public override void Use(Player p, string message)
        {
            if (message.Split(' ').Length < 2)
            {
                Help(p);
                return;
            }
            Player player = Player.Find(message.Split(' ')[0]);
            if (player == null)
            {
                Player.SendMessage(p, "Player is not online.");
                return;
            }
            if (p != null && player.group.Permission >= p.group.Permission)
            {
                Player.SendMessage(p, "Cannot use this on someone of equal or greater rank.");
                return;
            }
            string text;
            string message2;
            if (message.Split(' ').Length == 2)
            {
                text = message.Split(' ')[1];
                message2 = "";
            }
            else
            {
                text = message.Split(' ')[1];
                message2 = message.Substring(message.IndexOf(' ', message.IndexOf(' ') + 1) + 1);
            }
            Command command = all.Find(text);
            if (command == null)
            {
                Player.SendMessage(p, "Unknown command: " + text);
            }
            else if (p == null || p.group.CanExecute(command))
            {
                command.Use(player, message2);
            }
            else
            {
                Player.SendMessage(p, "This command requires higher permission than you have.");
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/make - Make another user use a command, (/make player command parameter)");
            Player.SendMessage(p, "ex: /make dzienny tp notch");
        }
    }
}