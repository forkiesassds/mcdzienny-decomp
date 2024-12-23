namespace MCDzienny
{
    public class CmdItems : Command
    {
        public override string name { get { return "items"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }

        public override bool ConsoleAccess { get { return false; } }

        public override CommandScope Scope { get { return CommandScope.Lava; } }

        public override void Use(Player p, string message)
        {
            Player.SendMessage(p, "You have:");
            Player.SendMessage(p, string.Format("water blocks: {0},", p.waterBlocks));
            Player.SendMessage(p, string.Format("sponges: {0},", p.spongeBlocks));
            Player.SendMessage(p, string.Format("doors: {0},", p.doorBlocks));
            Player.SendMessage(p, string.Format("hammer: {0},", p.hammer));
            Player.SendMessage(p, string.Format("teleport: {0}.", p.hasTeleport ? 1 : 0));
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/items - shows your possesions.");
        }
    }
}