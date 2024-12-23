namespace MCDzienny
{
    public class CmdAbort : Command
    {
        public override string name { get { return "abort"; } }

        public override string shortcut { get { return "a"; } }

        public override string type { get { return "build"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }

        public override bool ConsoleAccess { get { return false; } }

        public override string CustomName { get { return Lang.Command.CmdAbortName; } }

        public override void Use(Player p, string message)
        {
            p.ClearBlockchange();
            p.painting = false;
            p.BlockAction = 0;
            p.megaBoid = false;
            p.cmdTimer = false;
            p.staticCommands = false;
            p.deleteMode = false;
            p.ZoneCheck = false;
            p.modeType = 0;
            p.aiming = false;
            p.onTrain = false;
            Player.SendMessage(p, Lang.Command.CmdAbortMessage);
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, Lang.Command.CmdAbortHelp);
        }
    }
}