namespace MCDzienny
{
    class CmdCmdUnload : Command
    {
        public override string name { get { return "cmdunload"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Nobody; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }
            if (core.Contains(message.Split(' ')[0]))
            {
                Player.SendMessage(p, string.Format("/{0} is a core command, you cannot unload it!", message.Split(' ')[0]));
                return;
            }
            Command command = all.Find(message.Split(' ')[0]);
            if (command == null)
            {
                Player.SendMessage(p, string.Format("{0} is not a valid or loaded command.", message.Split(' ')[0]));
            }
            else
            {
                all.Remove(command);
                GrpCommands.fillRanks();
                Player.SendMessage(p, "Command was successfully unloaded.");
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/cmdunload <command> - Unloads a command from the server.");
        }
    }
}