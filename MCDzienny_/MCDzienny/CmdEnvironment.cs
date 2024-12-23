using MCDzienny.Levels.Effects;
using MCDzienny.Levels.Info;

namespace MCDzienny
{
    class CmdEnvironment : Command
    {
        public override string name { get { return "environment"; } }

        public override string shortcut { get { return "env"; } }

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
            EnvironmentHandler eh = new EnvironmentHandler();
            Environment e = eh.Parse(message);
            if (e == null)
            {
                Player.SendMessage(p, "Unknown environment: " + message);
                return;
            }
            Level l = p.level;
            p.level.Info.Environment = message;
            Player.players.ForEachSync(delegate(Player pl)
            {
                if (pl.level == l)
                {
                    eh.SendToPlayer(pl, e);
                }
            });
            LevelInfoManager levelInfoManager = new LevelInfoManager();
            LevelInfoConverter levelInfoConverter = new LevelInfoConverter();
            LevelInfoRaw info = levelInfoConverter.ToRaw(p.level.Info);
            levelInfoManager.Save(p.level, info);
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/env [type] - sets the environment to a given type.");
            Player.SendMessage(p, "Available types:");
            Player.SendMessage(p, "day (default), pinky, vanilla, cloudless, stormy, night, darkness.");
        }
    }
}