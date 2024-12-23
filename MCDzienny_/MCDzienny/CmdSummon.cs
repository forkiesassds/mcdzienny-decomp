namespace MCDzienny
{
    public class CmdSummon : Command
    {
        public override string name { get { return "summon"; } }

        public override string shortcut { get { return "s"; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }
            if (message.ToLower() == "all")
            {
                foreach (Player player2 in Player.players)
                {
                    if (player2.level == p.level && player2 != p && player2.group.Permission <= p.group.Permission)
                    {
                        player2.SendPos(byte.MaxValue, p.pos[0], p.pos[1], p.pos[2], p.rot[0], 0);
                        player2.SendMessage(string.Format("You were summoned by {0}.", p.color + p.PublicName + Server.DefaultColor));
                    }
                }
                return;
            }
            Player player = Player.Find(message);
            if (player == null || player.hidden)
            {
                Player.SendMessage(p, string.Format("There is no player \"{0}\"!", message));
                return;
            }
            if (player.group.Permission > p.group.Permission && p != null)
            {
                Player.SendMessage(p, "You can't summon a player with a higher rank than yours.");
                return;
            }
            if (p.level != player.level)
            {
                Player.SendMessage(p, string.Format("{0} is in a different level.", player.name));
                return;
            }
            player.SendPos(byte.MaxValue, p.pos[0], p.pos[1], p.pos[2], p.rot[0], 0);
            player.SendMessage(string.Format("You were summoned by {0}.", p.color + p.PublicName + Server.DefaultColor));
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/summon <player> - Summons a player to your position.");
            Player.SendMessage(p, "/summon all - Summons all players in the map");
        }
    }
}