namespace MCDzienny
{
    public class CmdPaint : Command
    {
        public override string name { get { return "paint"; } }

        public override string shortcut { get { return "p"; } }

        public override string type { get { return "build"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Builder; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            if (message != "")
            {
                Help(p);
                return;
            }
            p.painting = !p.painting;
            if (p.painting)
            {
                Player.SendMessage(p, string.Format("Painting mode: &aON{0}.", Server.DefaultColor));
            }
            else
            {
                Player.SendMessage(p, string.Format("Painting mode: &cOFF{0}.", Server.DefaultColor));
            }
            p.BlockAction = 0;
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/paint - Turns painting mode on/off.");
        }
    }
}