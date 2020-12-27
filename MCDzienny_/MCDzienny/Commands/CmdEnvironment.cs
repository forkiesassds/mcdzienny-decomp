using MCDzienny.Levels.Effects;
using MCDzienny.Levels.Info;

namespace MCDzienny
{
    // Token: 0x02000011 RID: 17
    internal class CmdEnvironment : Command
    {
        // Token: 0x17000012 RID: 18
        // (get) Token: 0x06000060 RID: 96 RVA: 0x00004B88 File Offset: 0x00002D88
        public override string name
        {
            get { return "environment"; }
        }

        // Token: 0x17000013 RID: 19
        // (get) Token: 0x06000061 RID: 97 RVA: 0x00004B90 File Offset: 0x00002D90
        public override string shortcut
        {
            get { return "env"; }
        }

        // Token: 0x17000014 RID: 20
        // (get) Token: 0x06000062 RID: 98 RVA: 0x00004B98 File Offset: 0x00002D98
        public override string type
        {
            get { return "build"; }
        }

        // Token: 0x17000015 RID: 21
        // (get) Token: 0x06000063 RID: 99 RVA: 0x00004BA0 File Offset: 0x00002DA0
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000016 RID: 22
        // (get) Token: 0x06000064 RID: 100 RVA: 0x00004BA4 File Offset: 0x00002DA4
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x17000017 RID: 23
        // (get) Token: 0x06000065 RID: 101 RVA: 0x00004BA8 File Offset: 0x00002DA8
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x06000066 RID: 102 RVA: 0x00004BAC File Offset: 0x00002DAC
        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Help(p);
                return;
            }

            var eh = new EnvironmentHandler();
            var e = eh.Parse(message);
            if (e == null)
            {
                Player.SendMessage(p, "Unknown environment: " + message);
                return;
            }

            var l = p.level;
            p.level.Info.Environment = message;
            Player.players.ForEachSync(delegate(Player pl)
            {
                if (pl.level == l) eh.SendToPlayer(pl, e);
            });
            var levelInfoManager = new LevelInfoManager();
            var levelInfoConverter = new LevelInfoConverter();
            var info = levelInfoConverter.ToRaw(p.level.Info);
            levelInfoManager.Save(p.level, info);
        }

        // Token: 0x06000067 RID: 103 RVA: 0x00004C6C File Offset: 0x00002E6C
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/env [type] - sets the environment to a given type.");
            Player.SendMessage(p, "Available types:");
            Player.SendMessage(p, "day (default), pinky, vanilla, cloudless, stormy, night, darkness.");
        }
    }
}