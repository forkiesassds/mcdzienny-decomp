namespace MCDzienny
{
    public class CmdWall : Command
    {
        public override string name { get { return "wall"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Builder; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            all.Find("line").Use(p, "wall");
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/wall - place two blocks to create a wall.");
        }
    }
}