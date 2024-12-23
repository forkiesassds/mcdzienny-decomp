using System;
using MCDzienny.InfectionSystem;
using MCDzienny.Settings;

namespace MCDzienny
{
    public class CmdTime : Command
    {
        public override string name { get { return "time"; } }

        public override string shortcut { get { return "t"; } }

        public override string type { get { return "information"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }

        public override CommandScope Scope { get { return CommandScope.Lava | CommandScope.Zombie; } }

        public override void Use(Player p, string message)
        {
            if (p.level.mapType == MapType.Zombie)
            {
                TimeSpan timeToEnd = InfectionUtils.TimeToEnd;
                Player.SendMessage(p, string.Format("%7Time left: %e{0}:{1}", timeToEnd.Minutes, timeToEnd.Seconds.ToString("00")));
                return;
            }
            Player.SendMessage(p, string.Format("Lava is currently: %c{0}", LavaSettings.All.LavaState));
            if (LavaSystem.time != -1)
            {
                Player.SendMessage(p, string.Format("Time to lava flood: (less than) %c{0} min.", LavaSystem.time + 1 + Server.DefaultColor));
            }
            else
            {
                Player.SendMessage(p, "Time to lava flood: %cflood has already started.");
            }
            Player.SendMessage(p, string.Format("Time to the end of the round: (less than) %c{0} min.", LavaSystem.time + LavaSystem.time2 + 1 + Server.DefaultColor));
        }

        public override void Help(Player p)
        {
            switch (p.level.mapType)
            {
                case MapType.Zombie:
                    Player.SendMessage(p, "/time - shows time left.");
                    break;
                case MapType.Lava:
                    Player.SendMessage(p, "/time - shows times and lava mood.");
                    break;
                default:
                    Player.SendMessage(p, "/time - shows time left.");
                    break;
            }
        }
    }
}