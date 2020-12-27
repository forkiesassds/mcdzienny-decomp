namespace MCDzienny
{
    // Token: 0x02000132 RID: 306
    public class CmdResults : Command
    {
        // Token: 0x1700043F RID: 1087
        // (get) Token: 0x06000925 RID: 2341 RVA: 0x0002D904 File Offset: 0x0002BB04
        public override string name
        {
            get { return "maprating"; }
        }

        // Token: 0x17000440 RID: 1088
        // (get) Token: 0x06000926 RID: 2342 RVA: 0x0002D90C File Offset: 0x0002BB0C
        public override string shortcut
        {
            get { return "mr"; }
        }

        // Token: 0x17000441 RID: 1089
        // (get) Token: 0x06000927 RID: 2343 RVA: 0x0002D914 File Offset: 0x0002BB14
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x17000442 RID: 1090
        // (get) Token: 0x06000928 RID: 2344 RVA: 0x0002D91C File Offset: 0x0002BB1C
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000443 RID: 1091
        // (get) Token: 0x06000929 RID: 2345 RVA: 0x0002D920 File Offset: 0x0002BB20
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Guest; }
        }

        // Token: 0x17000444 RID: 1092
        // (get) Token: 0x0600092A RID: 2346 RVA: 0x0002D924 File Offset: 0x0002BB24
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x0600092B RID: 2347 RVA: 0x0002D928 File Offset: 0x0002BB28
        public override void Use(Player p, string message)
        {
            if (p == null)
            {
                Player.SendMessage(null, "You cannot use this command from console.");
                return;
            }

            string queryString;
            string queryString2;
            if (p.level.mapType == MapType.MyMap)
            {
                queryString = string.Concat("SELECT COUNT(*) FROM `Rating", p.level.name,
                    "` WHERE Vote = 1 AND WHERE Map=", p.level.MapDbId);
                queryString2 = string.Concat("SELECT COUNT(*) FROM `Rating", p.level.name,
                    "` WHERE Vote = 2 AND WHERE Map=", p.level.MapDbId);
            }
            else
            {
                queryString = "SELECT COUNT(*) FROM `Rating" + p.level.name + "` WHERE Vote = 1";
                queryString2 = "SELECT COUNT(*) FROM `Rating" + p.level.name + "` WHERE Vote = 2";
            }

            using (var dataTable = DBInterface.fillData(queryString))
            {
                using (var dataTable2 = DBInterface.fillData(queryString2))
                {
                    Player.SendMessage(p,
                        string.Format("The rating for this map is: %a{0}%s likes, %7{1}%s dislikes.",
                            dataTable.Rows[0]["COUNT(*)"], dataTable2.Rows[0]["COUNT(*)"]));
                }
            }
        }

        // Token: 0x0600092C RID: 2348 RVA: 0x0002DA9C File Offset: 0x0002BC9C
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/maprating (/mr) - show the rating of the map.");
        }
    }
}