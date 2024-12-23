using System.Text;

namespace MCDzienny
{
    public class CmdAlive : Command
    {
        public override string name { get { return "alive"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }

        public override string CustomName { get { return Lang.Command.AliveName; } }

        public override CommandScope Scope { get { return CommandScope.Lava | CommandScope.Zombie; } }

        public override void Use(Player p, string message)
        {
            if (p != null && p.level.mapType == MapType.Zombie)
            {
                all.Find("humans").Use(p, "");
                return;
            }
            StringBuilder alivePlayers = new StringBuilder();
            Player.SendMessage(p, Lang.Command.AliveMessage);
            Player.players.ForEachSync(delegate(Player who)
            {
                if (who.lives > 0 && !who.hidden && !who.invincible)
                {
                    alivePlayers.Append("%c" + who.PublicName + ", ");
                }
            });
            Player.SendMessage(p, alivePlayers.ToString().TrimEnd().TrimEnd(','));
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, Lang.Command.AliveHelp);
        }
    }
}