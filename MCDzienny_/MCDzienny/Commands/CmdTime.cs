using MCDzienny.InfectionSystem;
using MCDzienny.Settings;

namespace MCDzienny
{
    // Token: 0x020002A1 RID: 673
    public class CmdTime : Command
    {
        // Token: 0x17000749 RID: 1865
        // (get) Token: 0x06001347 RID: 4935 RVA: 0x0006A22C File Offset: 0x0006842C
        public override string name
        {
            get { return "time"; }
        }

        // Token: 0x1700074A RID: 1866
        // (get) Token: 0x06001348 RID: 4936 RVA: 0x0006A234 File Offset: 0x00068434
        public override string shortcut
        {
            get { return "t"; }
        }

        // Token: 0x1700074B RID: 1867
        // (get) Token: 0x06001349 RID: 4937 RVA: 0x0006A23C File Offset: 0x0006843C
        public override string type
        {
            get { return "information"; }
        }

        // Token: 0x1700074C RID: 1868
        // (get) Token: 0x0600134A RID: 4938 RVA: 0x0006A244 File Offset: 0x00068444
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x1700074D RID: 1869
        // (get) Token: 0x0600134B RID: 4939 RVA: 0x0006A248 File Offset: 0x00068448
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Banned; }
        }

        // Token: 0x1700074E RID: 1870
        // (get) Token: 0x0600134C RID: 4940 RVA: 0x0006A24C File Offset: 0x0006844C
        public override CommandScope Scope
        {
            get { return CommandScope.Lava | CommandScope.Zombie; }
        }

        // Token: 0x0600134D RID: 4941 RVA: 0x0006A250 File Offset: 0x00068450
        public override void Use(Player p, string message)
        {
            if (p.level.mapType == MapType.Zombie)
            {
                var timeToEnd = InfectionUtils.TimeToEnd;
                Player.SendMessage(p,
                    string.Format("%7Time left: %e{0}:{1}", timeToEnd.Minutes, timeToEnd.Seconds.ToString("00")));
                return;
            }

            Player.SendMessage(p, string.Format("Lava is currently: %c{0}", LavaSettings.All.LavaState));
            if (LavaSystem.time != -1)
                Player.SendMessage(p,
                    string.Format("Time to lava flood: (less than) %c{0} min.",
                        (LavaSystem.time + 1) + Server.DefaultColor));
            else
                Player.SendMessage(p, "Time to lava flood: %cflood has already started.");
            Player.SendMessage(p,
                string.Format("Time to the end of the round: (less than) %c{0} min.",
                    (LavaSystem.time + LavaSystem.time2 + 1) + Server.DefaultColor));
        }

        // Token: 0x0600134E RID: 4942 RVA: 0x0006A330 File Offset: 0x00068530
        public override void Help(Player p)
        {
            switch (p.level.mapType)
            {
                case MapType.Lava:
                    Player.SendMessage(p, "/time - shows times and lava mood.");
                    return;
                case MapType.Zombie:
                    Player.SendMessage(p, "/time - shows time left.");
                    return;
                default:
                    Player.SendMessage(p, "/time - shows time left.");
                    return;
            }
        }
    }
}