namespace MCDzienny.Commands
{
    public class CmdTextures : Command
    {
        public override string name { get { return "textures"; } }

        public override string shortcut { get { return "texture"; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            Player player = null;
            if (message != "")
            {
                if (p != null && p.group.Permission < LevelPermission.Operator)
                {
                    Player.SendMessage(p, "@ Your level is not high enough for using this command on other players.");
                    return;
                }
                player = Player.Find(message.Trim());
            }
            else
            {
                if (p == null)
                {
                    Help(p);
                    return;
                }
                player = p;
            }
            if (player != null && player.IsUsingWom)
            {
                if (player.IsUsingTextures)
                {
                    player.RemoveTextures();
                    Player.SendMessage(p, string.Format("@ {0} stops using custom texture pack.", player.PublicName));
                }
                else
                {
                    player.SendTextures();
                    Player.SendMessage(p, string.Format("@ {0} starts using custom texture pack.", player.PublicName));
                }
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/textures - toggles the use of custom texture pack.");
        }
    }
}