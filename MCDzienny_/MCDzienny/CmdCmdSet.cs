namespace MCDzienny
{
    public class CmdCmdSet : Command
    {
        public override string name { get { return "cmdset"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "mod"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }

        public override void Use(Player p, string message)
        {
            if (message == "" || message.IndexOf(' ') == -1)
            {
                Help(p);
                return;
            }
            string text = all.FindShort(message.Split(' ')[0]);
            Command foundCmd;
            if (text == "")
            {
                foundCmd = all.Find(message.Split(' ')[0]);
            }
            else
            {
                foundCmd = all.Find(text);
            }
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
            LevelPermission levelPermission = Level.PermissionFromName(message.Split(' ')[1]);
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
            GrpCommands.rankAllowance rankAllowance = GrpCommands.allowedCommands.Find(rA => rA.commandName == foundCmd.name);
            rankAllowance.lowestRank = levelPermission;
            GrpCommands.allowedCommands[GrpCommands.allowedCommands.FindIndex(rA => rA.commandName == foundCmd.name)] = rankAllowance;
            GrpCommands.Save(GrpCommands.allowedCommands);
            GrpCommands.fillRanks();
            Player.GlobalMessage(string.Format("&d{0}'s permission was changed to {1}", foundCmd.name + Server.DefaultColor, Level.PermissionToName(levelPermission)));
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/cmdset [cmd] [rank] - Changes [cmd] rank to [rank]");
            Player.SendMessage(p, "Only commands you can use can be modified");
            Player.SendMessage(p, "Available ranks: " + Group.concatList());
        }
    }
}