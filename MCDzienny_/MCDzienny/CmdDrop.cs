namespace MCDzienny
{
    public class CmdDrop : Command
    {
        public override string name { get { return "drop"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Builder; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            if (message != "")
            {
                Help(p);
            }
            else if (p.hasflag != null)
            {
                p.level.ctfgame.DropFlag(p, p.hasflag);
            }
            else
            {
                Player.SendMessage(p, "You are not carrying a flag.");
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/drop - Drop the flag if you are carrying it.");
        }
    }
}