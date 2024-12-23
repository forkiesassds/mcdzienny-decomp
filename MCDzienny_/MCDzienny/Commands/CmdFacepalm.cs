namespace MCDzienny.Commands
{
    public class CmdFacepalm : Command
    {
        public override string name { get { return "facepalm"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            message = message.Trim();
            if (message != "")
            {
                Player player = Player.Find(message);
                if (player == null)
                {
                    Player.SendMessage(p, "Couldn't find the player.");
                    return;
                }
                Player.GlobalMessage("{0}%s looked at {1}%s and facepalmed.", p.color + p.PublicName, player.color + player.PublicName);
            }
            else
            {
                Player.GlobalMessage("{0}%s facepalmed.", p.color + p.PublicName);
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/facepalm - just facepalms ;)");
            Player.SendMessage(p, "/facepalm [player]");
        }
    }
}