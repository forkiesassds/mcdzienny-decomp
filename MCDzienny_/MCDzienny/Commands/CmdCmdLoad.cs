namespace MCDzienny
{
    // Token: 0x020000BA RID: 186
    internal class CmdCmdLoad : Command
    {
        // Token: 0x170002BD RID: 701
        // (get) Token: 0x06000651 RID: 1617 RVA: 0x0002145C File Offset: 0x0001F65C
        public override string name
        {
            get { return "cmdload"; }
        }

        // Token: 0x170002BE RID: 702
        // (get) Token: 0x06000652 RID: 1618 RVA: 0x00021464 File Offset: 0x0001F664
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x170002BF RID: 703
        // (get) Token: 0x06000653 RID: 1619 RVA: 0x0002146C File Offset: 0x0001F66C
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x170002C0 RID: 704
        // (get) Token: 0x06000654 RID: 1620 RVA: 0x00021474 File Offset: 0x0001F674
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170002C1 RID: 705
        // (get) Token: 0x06000655 RID: 1621 RVA: 0x00021478 File Offset: 0x0001F678
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Nobody; }
        }

        // Token: 0x06000657 RID: 1623 RVA: 0x00021484 File Offset: 0x0001F684
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
            var text = Scripting.Load(message);
            if (text != null)
            {
                Player.SendMessage(p, text);
                return;
            }

            GrpCommands.fillRanks();
            Player.SendMessage(p, "Command was successfully loaded.");
        }

        // Token: 0x06000658 RID: 1624 RVA: 0x00021518 File Offset: 0x0001F718
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/cmdload <command name> - Loads a command into the server for use.");
        }
    }
}