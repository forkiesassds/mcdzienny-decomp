namespace MCDzienny
{
    // Token: 0x020000DD RID: 221
    public class CmdPosition : Command
    {
        // Token: 0x1700032A RID: 810
        // (get) Token: 0x06000720 RID: 1824 RVA: 0x00024604 File Offset: 0x00022804
        public override string name
        {
            get { return "position"; }
        }

        // Token: 0x1700032B RID: 811
        // (get) Token: 0x06000721 RID: 1825 RVA: 0x0002460C File Offset: 0x0002280C
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x1700032C RID: 812
        // (get) Token: 0x06000722 RID: 1826 RVA: 0x00024614 File Offset: 0x00022814
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x1700032D RID: 813
        // (get) Token: 0x06000723 RID: 1827 RVA: 0x0002461C File Offset: 0x0002281C
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x1700032E RID: 814
        // (get) Token: 0x06000724 RID: 1828 RVA: 0x00024620 File Offset: 0x00022820
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Banned; }
        }

        // Token: 0x1700032F RID: 815
        // (get) Token: 0x06000725 RID: 1829 RVA: 0x00024624 File Offset: 0x00022824
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x06000727 RID: 1831 RVA: 0x00024630 File Offset: 0x00022830
        public override void Use(Player p, string message)
        {
            if (message != "")
            {
                Help(p);
                return;
            }

            if (LavaSystem.CheckSpawn(p))
                Player.SendMessage(p, "You are in safe distance from the map spawn");
            else
                Player.SendMessage(p, "%cYou are too close to the map spawn! Go go go! Or you don't score.");
            if (p.IsAboveSeaLevel)
            {
                Player.SendMessage(p, "You are above the sea level. It gives you higher score.");
                return;
            }

            Player.SendMessage(p, "You are below the sea level.");
        }

        // Token: 0x06000728 RID: 1832 RVA: 0x00024694 File Offset: 0x00022894
        public override void Help(Player p)
        {
            Player.SendMessage(p,
                "/position - Displays information about your position that are important for scoring points in lava survival.");
        }
    }
}