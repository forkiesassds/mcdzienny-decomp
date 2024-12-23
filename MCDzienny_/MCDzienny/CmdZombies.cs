using System.Text;

namespace MCDzienny
{
    public class CmdZombies : Command
    {
        public override string name { get { return "zombies"; } }

        public override string shortcut { get { return "zombie"; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }

        public override CommandScope Scope { get { return CommandScope.Zombie; } }

        public override void Use(Player p, string message)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("%c");
            InfectionSystem.InfectionSystem.infected.ForEach(delegate(Player pl) { sb.Append(pl.PublicName).Append(", "); });
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 2, 2);
            }
            Player.SendMessage(p, "%cZombies:");
            Player.SendMessage(p, sb.ToString());
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/zombies - displays list of zombies.");
        }
    }
}