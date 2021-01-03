namespace MCDzienny
{
    // Token: 0x020000E5 RID: 229
    internal class CmdCmdUnload : Command
    {
        // Token: 0x17000351 RID: 849
        // (get) Token: 0x06000762 RID: 1890 RVA: 0x000252AC File Offset: 0x000234AC
        public override string name
        {
            get { return "cmdunload"; }
        }

        // Token: 0x17000352 RID: 850
        // (get) Token: 0x06000763 RID: 1891 RVA: 0x000252B4 File Offset: 0x000234B4
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000353 RID: 851
        // (get) Token: 0x06000764 RID: 1892 RVA: 0x000252BC File Offset: 0x000234BC
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x17000354 RID: 852
        // (get) Token: 0x06000765 RID: 1893 RVA: 0x000252C4 File Offset: 0x000234C4
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000355 RID: 853
        // (get) Token: 0x06000766 RID: 1894 RVA: 0x000252C8 File Offset: 0x000234C8
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Nobody; }
        }

        // Token: 0x06000768 RID: 1896 RVA: 0x000252D4 File Offset: 0x000234D4
        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }

            if (core.Contains(message.Split(' ')[0]))
            {
                Player.SendMessage(p,
                    string.Format("/{0} is a core command, you cannot unload it!", message.Split(' ')[0]));
                return;
            }

            var command = all.Find(message.Split(' ')[0]);
            if (command == null)
            {
                Player.SendMessage(p, string.Format("{0} is not a valid or loaded command.", message.Split(' ')[0]));
                return;
            }

            all.Remove(command);
            GrpCommands.fillRanks();
            Player.SendMessage(p, "Command was successfully unloaded.");
        }

        // Token: 0x06000769 RID: 1897 RVA: 0x000253A8 File Offset: 0x000235A8
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/cmdunload <command> - Unloads a command from the server.");
        }
    }
}