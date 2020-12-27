using MCDzienny.Levels.Effects;
using MCDzienny.Levels.Info;

namespace MCDzienny
{
    // Token: 0x02000017 RID: 23
    internal class CmdWeather : Command
    {
        // Token: 0x17000024 RID: 36
        // (get) Token: 0x06000084 RID: 132 RVA: 0x00004FDC File Offset: 0x000031DC
        public override string name
        {
            get { return "weather"; }
        }

        // Token: 0x17000025 RID: 37
        // (get) Token: 0x06000085 RID: 133 RVA: 0x00004FE4 File Offset: 0x000031E4
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000026 RID: 38
        // (get) Token: 0x06000086 RID: 134 RVA: 0x00004FEC File Offset: 0x000031EC
        public override string type
        {
            get { return "build"; }
        }

        // Token: 0x17000027 RID: 39
        // (get) Token: 0x06000087 RID: 135 RVA: 0x00004FF4 File Offset: 0x000031F4
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000028 RID: 40
        // (get) Token: 0x06000088 RID: 136 RVA: 0x00004FF8 File Offset: 0x000031F8
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x17000029 RID: 41
        // (get) Token: 0x06000089 RID: 137 RVA: 0x00004FFC File Offset: 0x000031FC
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x0600008A RID: 138 RVA: 0x00005000 File Offset: 0x00003200
        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }

            var wh = new WeatherHandler();
            var w = wh.Parse(message);
            if (w == WeatherHandler.Unknown)
            {
                Player.SendMessage(p, "Unknown weather: " + message);
                return;
            }

            var l = p.level;
            p.level.Info.Weather = message;
            Player.players.ForEachSync(delegate(Player pl)
            {
                if (pl.level == l) wh.SendToPlayer(pl, w);
            });
            var levelInfoManager = new LevelInfoManager();
            var levelInfoConverter = new LevelInfoConverter();
            var info = levelInfoConverter.ToRaw(p.level.Info);
            levelInfoManager.Save(p.level, info);
        }

        // Token: 0x0600008B RID: 139 RVA: 0x000050C4 File Offset: 0x000032C4
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/weather [type] - sets the weather to a given type.");
            Player.SendMessage(p, "Available types:");
            Player.SendMessage(p, "normal, raining, snowing.");
        }
    }
}