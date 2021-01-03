using System;

namespace MCDzienny
{
    // Token: 0x02000130 RID: 304
    public class CmdLike : Command
    {
        // Token: 0x17000433 RID: 1075
        // (get) Token: 0x06000913 RID: 2323 RVA: 0x0002D1A4 File Offset: 0x0002B3A4
        public override string name
        {
            get { return "like"; }
        }

        // Token: 0x17000434 RID: 1076
        // (get) Token: 0x06000914 RID: 2324 RVA: 0x0002D1AC File Offset: 0x0002B3AC
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000435 RID: 1077
        // (get) Token: 0x06000915 RID: 2325 RVA: 0x0002D1B4 File Offset: 0x0002B3B4
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x17000436 RID: 1078
        // (get) Token: 0x06000916 RID: 2326 RVA: 0x0002D1BC File Offset: 0x0002B3BC
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x17000437 RID: 1079
        // (get) Token: 0x06000917 RID: 2327 RVA: 0x0002D1C0 File Offset: 0x0002B3C0
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Guest; }
        }

        // Token: 0x17000438 RID: 1080
        // (get) Token: 0x06000918 RID: 2328 RVA: 0x0002D1C4 File Offset: 0x0002B3C4
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x06000919 RID: 2329 RVA: 0x0002D1C8 File Offset: 0x0002B3C8
        public override void Use(Player p, string message)
        {
            if (p == null)
            {
                Player.SendMessage(null, "You cannot use this command from console.");
                return;
            }

            if (p.MapRatingCooldown > DateTime.Now)
            {
                Player.SendMessage(p,
                    string.Format("Your cooldown on voting: {0}",
                        Utils.TimeSpanToString(p.MapRatingCooldown - DateTime.Now)));
                return;
            }

            string queryString;
            if (p.level.mapType == MapType.MyMap)
                queryString = string.Concat("SELECT * FROM `Ratings` WHERE Username='", p.name, "' AND Map=",
                    p.level.MapDbId);
            else
                queryString = string.Concat("SELECT * FROM `Rating", p.level.name, "` WHERE Username='", p.name, "'");
            using (var dataTable = DBInterface.fillData(queryString))
            {
                if (dataTable.Rows.Count > 0)
                {
                    if (dataTable.Rows[0]["Vote"].ToString() == "1")
                    {
                        Player.SendMessage(p, "You already gave this map a like.");
                    }
                    else if (dataTable.Rows[0]["Vote"].ToString() == "2")
                    {
                        if (p.level.mapType == MapType.MyMap)
                            DBInterface.ExecuteQuery(string.Concat("UPDATE `Ratings` SET Vote = 1 WHERE Username='",
                                p.name, "' AND Map=", p.level.MapDbId));
                        else
                            DBInterface.ExecuteQuery(string.Concat("UPDATE `Rating", p.level.name,
                                "` SET Vote = 1 WHERE Username='", p.name, "'"));
                        Player.SendMessage(p, "You changed your vote from a dislike to a like for this map.");
                        Player.GlobalMessageLevel(p.level,
                            string.Format("{0} gave this map %aa like{1}.",
                                p.color + p.PublicName + Server.DefaultColor, Server.DefaultColor));
                        p.MapRatingCooldown = DateTime.Now.AddMinutes(1.0);
                    }
                }
                else
                {
                    if (p.level.mapType == MapType.MyMap)
                        DBInterface.ExecuteQuery(string.Concat("INSERT INTO `Ratings` (Map, Username, Vote) VALUES (",
                            p.level.MapDbId, ",'", p.name, "', 1 )"));
                    else
                        DBInterface.ExecuteQuery(string.Concat("INSERT INTO `Rating", p.level.name,
                            "` (Username, Vote) VALUES ('", p.name, "', 1 )"));
                    Player.SendMessage(p, "You gave this map a like.");
                    Player.GlobalMessageLevel(p.level,
                        string.Format("{0} gave this map %aa like{1}.", p.color + p.PublicName + Server.DefaultColor,
                            Server.DefaultColor));
                    p.MapRatingCooldown = DateTime.Now.AddMinutes(1.0);
                }
            }
        }

        // Token: 0x0600091A RID: 2330 RVA: 0x0002D53C File Offset: 0x0002B73C
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/like - you give the thumbs up to the map you are currently in.");
        }
    }
}