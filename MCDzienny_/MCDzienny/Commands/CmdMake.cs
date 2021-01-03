namespace MCDzienny
{
    // Token: 0x020000CC RID: 204
    public class CmdMake : Command
    {
        // Token: 0x170002F4 RID: 756
        // (get) Token: 0x060006BF RID: 1727 RVA: 0x00022DC8 File Offset: 0x00020FC8
        public override string name
        {
            get { return "make"; }
        }

        // Token: 0x170002F5 RID: 757
        // (get) Token: 0x060006C0 RID: 1728 RVA: 0x00022DD0 File Offset: 0x00020FD0
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170002F6 RID: 758
        // (get) Token: 0x060006C1 RID: 1729 RVA: 0x00022DD8 File Offset: 0x00020FD8
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x170002F7 RID: 759
        // (get) Token: 0x060006C2 RID: 1730 RVA: 0x00022DE0 File Offset: 0x00020FE0
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x170002F8 RID: 760
        // (get) Token: 0x060006C3 RID: 1731 RVA: 0x00022DE4 File Offset: 0x00020FE4
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Admin; }
        }

        // Token: 0x060006C4 RID: 1732 RVA: 0x00022DE8 File Offset: 0x00020FE8
        public override void Use(Player p, string message)
        {
            if (message.Split(' ').Length < 2)
            {
                Help(p);
                return;
            }

            var player = Player.Find(message.Split(' ')[0]);
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

            var command = all.Find(text);
            if (command == null)
            {
                Player.SendMessage(p, "Unknown command: " + text);
                return;
            }

            if (p == null || p.group.CanExecute(command)) command.Use(player, message2);
            Player.SendMessage(p, "This command requires higher permission than you have.");
        }

        // Token: 0x060006C5 RID: 1733 RVA: 0x00022F24 File Offset: 0x00021124
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/make - Make another user use a command, (/make player command parameter)");
            Player.SendMessage(p, "ex: /make dzienny tp notch");
        }
    }
}