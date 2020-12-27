namespace MCDzienny
{
    // Token: 0x020000FA RID: 250
    public class CmdClick : Command
    {
        // Token: 0x1700036C RID: 876
        // (get) Token: 0x060007AF RID: 1967 RVA: 0x00027240 File Offset: 0x00025440
        public override string name
        {
            get { return "click"; }
        }

        // Token: 0x1700036D RID: 877
        // (get) Token: 0x060007B0 RID: 1968 RVA: 0x00027248 File Offset: 0x00025448
        public override string shortcut
        {
            get { return "x"; }
        }

        // Token: 0x1700036E RID: 878
        // (get) Token: 0x060007B1 RID: 1969 RVA: 0x00027250 File Offset: 0x00025450
        public override string type
        {
            get { return "build"; }
        }

        // Token: 0x1700036F RID: 879
        // (get) Token: 0x060007B2 RID: 1970 RVA: 0x00027258 File Offset: 0x00025458
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x17000370 RID: 880
        // (get) Token: 0x060007B3 RID: 1971 RVA: 0x0002725C File Offset: 0x0002545C
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Guest; }
        }

        // Token: 0x17000371 RID: 881
        // (get) Token: 0x060007B4 RID: 1972 RVA: 0x00027260 File Offset: 0x00025460
        public override CommandScope Scope
        {
            get { return CommandScope.Lava; }
        }

        // Token: 0x060007B6 RID: 1974 RVA: 0x0002726C File Offset: 0x0002546C
        public override void Use(Player p, string message)
        {
            var array = message.Split(' ');
            var lastClick = p.lastClick;
            if (message.IndexOf(' ') != -1)
            {
                if (array.Length != 3)
                {
                    Help(p);
                    return;
                }

                for (var i = 0; i < 3; i++)
                    if (array[i].ToLower() == "x" || array[i].ToLower() == "y" || array[i].ToLower() == "z")
                    {
                        lastClick[i] = p.lastClick[i];
                    }
                    else
                    {
                        if (!isValid(array[i], i, p))
                        {
                            Player.SendMessage(p, string.Format("\"{0}\" was not valid", array[i]));
                            return;
                        }

                        lastClick[i] = ushort.Parse(array[i]);
                    }
            }

            p.manualChange(lastClick[0], lastClick[1], lastClick[2], 0, 1);
            Player.SendMessage(p, string.Format("Clicked &b({0}, {1}, {2})", lastClick[0], lastClick[1], lastClick[2]));
        }

        // Token: 0x060007B7 RID: 1975 RVA: 0x0002736C File Offset: 0x0002556C
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/click [x y z] - Fakes a click");
            Player.SendMessage(p, "If no xyz is given, it uses the last place clicked");
            Player.SendMessage(p, "/click 200 y 200 will cause it to click at 200x, last y and 200z");
        }

        // Token: 0x060007B8 RID: 1976 RVA: 0x00027390 File Offset: 0x00025590
        private bool isValid(string message, int dimension, Player p)
        {
            ushort num;
            try
            {
                num = ushort.Parse(message);
            }
            catch
            {
                return false;
            }

            return num >= 0 && (num < p.level.width || dimension != 0) && (num < p.level.height || dimension != 1) &&
                   (num < p.level.depth || dimension != 2);
        }
    }
}