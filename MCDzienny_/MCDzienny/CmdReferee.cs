namespace MCDzienny
{
    public class CmdReferee : Command
    {
        public override string name { get { return "referee"; } }

        public override string shortcut { get { return "ref"; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override bool ConsoleAccess { get { return false; } }

        public override CommandScope Scope { get { return CommandScope.Zombie; } }

        public override void Use(Player p, string message)
        {
            Player player = null;
            if (message != "")
            {
                player = Player.Find(message);
            }
            if (player == null)
            {
                player = p;
            }
            if (!player.IsRefree)
            {
                player.IsRefree = true;
                if (InfectionSystem.InfectionSystem.infected.Contains(player))
                {
                    InfectionSystem.InfectionSystem.infected.Remove(player);
                }
                if (InfectionSystem.InfectionSystem.notInfected.Contains(player))
                {
                    InfectionSystem.InfectionSystem.notInfected.Remove(player);
                }
                InfectionSystem.InfectionSystem.RemoveZombieDataAndSkin(player);
                if (!player.hidden)
                {
                    all.Find("hide").Use(p, "s");
                }
                Player.SendMessage(player, "You are a referee now.");
                Player.GlobalMessageLevel(player.level, player.color + player.PublicName + Server.DefaultColor + " referees this round.");
            }
            else
            {
                Player.GlobalDie(player, self: false);
                player.IsRefree = false;
                if (InfectionSystem.InfectionSystem.RoundTime > 0.0)
                {
                    Player.GlobalSpawn(player);
                }
                if (player.hidden)
                {
                    all.Find("hide").Use(p, "s");
                }
                Player.SendMessage(player, "You aren't a referee anymore.");
                Player.GlobalMessageLevel(player.level, player.color + player.PublicName + Server.DefaultColor + " stopped being a referee.");
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/referee - toggles a refree status.");
            Player.SendMessage(p, "/referee [player] - toggles a refree status for a [player].");
        }
    }
}