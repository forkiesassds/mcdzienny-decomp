namespace MCDzienny
{
    class CmdCmdLoad : Command
    {
        public override string name { get { return "cmdload"; } }

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
            if (all.Contains(message.Split(' ')[0]))
            {
                Player.SendMessage(p, "That command is already loaded!");
                return;
            }
            message = "Cmd" + message.Split(' ')[0];
            string text = Scripting.Load(message);
            if (text != null)
            {
                Player.SendMessage(p, text);
                return;
            }
            GrpCommands.fillRanks();
            Player.SendMessage(p, "Command was successfully loaded.");
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/cmdload <command name> - Loads a command into the server for use.");
        }
    }
}