namespace MCDzienny
{
    public class CmdCodes : Command
    {
        public override string name { get { return "codes"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }

        public override void Use(Player p, string message)
        {
            Player.SendMessage(p, "Color Codes - put % in front of the number or letter.");
            Player.SendMessage(
                p,
                "%0Black = 0, %1Dark Blue = 1, %2Dark Green = 2,            %3Dark Teal = 3, %4Dark Red = 4, %5Purple = 5,       %6Gold = 6, %7Gray = 7, %8Dark Gray = 8, %9Blue = 9, %aBright Green = a, %bTeal = b, %cRed = c, %dPink = d, %eYellow = e, %fWhite = f");
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/codes - color codes, put % in front of the number or letter.  Example command. Made By: PlatinumKiller");
        }
    }
}