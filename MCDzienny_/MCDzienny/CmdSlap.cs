namespace MCDzienny
{
    public class CmdSlap : Command
    {
        public override string name { get { return "slap"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override bool ConsoleAccess { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }
            Player player = Player.Find(message);
            if (player == null)
            {
                Player.SendMessage(p, "Could not find player specified");
                return;
            }
            ushort x = (ushort)(player.pos[0] / 32);
            ushort num = (ushort)(player.pos[1] / 32);
            ushort z = (ushort)(player.pos[2] / 32);
            ushort num2 = 0;
            for (ushort num3 = num; num3 <= 1000; num3++)
            {
                if (!Block.Walkthrough(p.level.GetTile(x, num3, z)) && p.level.GetTile(x, num3, z) != byte.MaxValue)
                {
                    num2 = (ushort)(num3 - 1);
                    player.level.ChatLevel(string.Format("{0} was slapped into the roof by {1}", player.color + player.PublicName + Server.DefaultColor,
                                                         p.color + p.PublicName));
                    break;
                }
            }
            if (num2 == 0)
            {
                player.level.ChatLevel(string.Format("{0} was slapped sky high by {1}", player.color + player.PublicName + Server.DefaultColor, p.color + p.PublicName));
                num2 = 1000;
            }
            player.SendPos(byte.MaxValue, player.pos[0], (ushort)(num2 * 32), player.pos[2], player.rot[0], player.rot[1]);
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/slap <name> - Slaps <name>, knocking them into the air");
        }
    }
}