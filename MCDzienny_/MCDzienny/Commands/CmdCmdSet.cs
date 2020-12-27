namespace MCDzienny
{
    // Token: 0x020002F1 RID: 753
    public class CmdCmdSet : Command
    {
        // Token: 0x17000868 RID: 2152
        // (get) Token: 0x06001555 RID: 5461 RVA: 0x000759CC File Offset: 0x00073BCC
        public override string name
        {
            get { return "cmdset"; }
        }

        // Token: 0x17000869 RID: 2153
        // (get) Token: 0x06001556 RID: 5462 RVA: 0x000759D4 File Offset: 0x00073BD4
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x1700086A RID: 2154
        // (get) Token: 0x06001557 RID: 5463 RVA: 0x000759DC File Offset: 0x00073BDC
        public override string type
        {
            get { return "mod"; }
        }

        // Token: 0x1700086B RID: 2155
        // (get) Token: 0x06001558 RID: 5464 RVA: 0x000759E4 File Offset: 0x00073BE4
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x1700086C RID: 2156
        // (get) Token: 0x06001559 RID: 5465 RVA: 0x000759E8 File Offset: 0x00073BE8
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Admin; }
        }

        // Token: 0x0600155B RID: 5467 RVA: 0x000759F4 File Offset: 0x00073BF4
        public override void Use(Player p, string message)
        {
            if (message == "" || message.IndexOf(' ') == -1)
            {
                Help(p);
                return;
            }

            var text = all.FindShort(message.Split(' ')[0]);
            Command foundCmd;
            if (text == "")
                foundCmd = all.Find(message.Split(' ')[0]);
            else
                foundCmd = all.Find(text);
            if (foundCmd == null)
            {
                Player.SendMessage(p, "Could not find command entered");
                return;
            }

            if (p != null && !p.group.CanExecute(foundCmd))
            {
                Player.SendMessage(p, "This command is higher than your rank.");
                return;
            }

            var levelPermission = Level.PermissionFromName(message.Split(' ')[1]);
            if (levelPermission == LevelPermission.Null)
            {
                Player.SendMessage(p, "Could not find rank specified");
                return;
            }

            if (p != null && levelPermission > p.group.Permission)
            {
                Player.SendMessage(p, "Cannot set to a rank higher than yourself.");
                return;
            }

            var rankAllowance = GrpCommands.allowedCommands.Find(rA => rA.commandName == foundCmd.name);
            rankAllowance.lowestRank = levelPermission;
            GrpCommands.allowedCommands[GrpCommands.allowedCommands.FindIndex(rA => rA.commandName == foundCmd.name)] =
                rankAllowance;
            GrpCommands.Save(GrpCommands.allowedCommands);
            GrpCommands.fillRanks();
            Player.GlobalMessage(string.Format("&d{0}'s permission was changed to {1}",
                foundCmd.name + Server.DefaultColor, Level.PermissionToName(levelPermission)));
        }

        // Token: 0x0600155C RID: 5468 RVA: 0x00075B90 File Offset: 0x00073D90
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/cmdset [cmd] [rank] - Changes [cmd] rank to [rank]");
            Player.SendMessage(p, "Only commands you can use can be modified");
            Player.SendMessage(p, "Available ranks: " + Group.concatList());
        }
    }
}