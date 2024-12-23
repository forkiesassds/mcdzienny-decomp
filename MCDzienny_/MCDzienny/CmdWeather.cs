using MCDzienny.Levels.Effects;
using MCDzienny.Levels.Info;

namespace MCDzienny
{
    class CmdWeather : Command
    {
        public override string name { get { return "weather"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "build"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }
            WeatherHandler wh = new WeatherHandler();
            int w = wh.Parse(message);
            if (w == WeatherHandler.Unknown)
            {
                Player.SendMessage(p, "Unknown weather: " + message);
                return;
            }
            Level l = p.level;
            p.level.Info.Weather = message;
            Player.players.ForEachSync(delegate(Player pl)
            {
                if (pl.level == l)
                {
                    wh.SendToPlayer(pl, w);
                }
            });
            LevelInfoManager levelInfoManager = new LevelInfoManager();
            LevelInfoConverter levelInfoConverter = new LevelInfoConverter();
            LevelInfoRaw info = levelInfoConverter.ToRaw(p.level.Info);
            levelInfoManager.Save(p.level, info);
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/weather [type] - sets the weather to a given type.");
            Player.SendMessage(p, "Available types:");
            Player.SendMessage(p, "normal, raining, snowing.");
        }
    }
}