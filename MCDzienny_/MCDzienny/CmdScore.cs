namespace MCDzienny
{
    public class CmdScore : Command
    {
        public override string name { get { return "experience"; } }

        public override string shortcut { get { return "score"; } }

        public override string type { get { return ""; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }

        public override bool ConsoleAccess { get { return false; } }

        public override CommandScope Scope { get { return CommandScope.Lava; } }

        public override void Use(Player p, string message)
        {
            Player.SendMessage(p, string.Format("Your best score: {0}", p.bestScore));
            Player.SendMessage(p, string.Format("Your total experience: {0}", p.totalScore));
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/score - Shows your best score and total experience.");
        }
    }
}