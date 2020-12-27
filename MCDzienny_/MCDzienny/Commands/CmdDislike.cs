using System;

namespace MCDzienny
{
    // Token: 0x02000131 RID: 305
    public class CmdDislike : Command
    {
        // Token: 0x17000439 RID: 1081
        // (get) Token: 0x0600091C RID: 2332 RVA: 0x0002D554 File Offset: 0x0002B754
        public override string name
        {
            get { return "dislike"; }
        }

        // Token: 0x1700043A RID: 1082
        // (get) Token: 0x0600091D RID: 2333 RVA: 0x0002D55C File Offset: 0x0002B75C
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x1700043B RID: 1083
        // (get) Token: 0x0600091E RID: 2334 RVA: 0x0002D564 File Offset: 0x0002B764
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x1700043C RID: 1084
        // (get) Token: 0x0600091F RID: 2335 RVA: 0x0002D56C File Offset: 0x0002B76C
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x1700043D RID: 1085
        // (get) Token: 0x06000920 RID: 2336 RVA: 0x0002D570 File Offset: 0x0002B770
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Guest; }
        }

        // Token: 0x1700043E RID: 1086
        // (get) Token: 0x06000921 RID: 2337 RVA: 0x0002D574 File Offset: 0x0002B774
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x06000922 RID: 2338 RVA: 0x0002D578 File Offset: 0x0002B778
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
                    if (dataTable.Rows[0]["Vote"].ToString() == "2")
                    {
                        Player.SendMessage(p, "You already gave this map a dislike.");
                    }
                    else if (dataTable.Rows[0]["Vote"].ToString() == "1")
                    {
                        if (p.level.mapType == MapType.MyMap)
                            DBInterface.ExecuteQuery(string.Concat("UPDATE `Ratings` SET Vote = 2 WHERE Username='",
                                p.name, "' AND Map=", p.level.MapDbId));
                        else
                            DBInterface.ExecuteQuery(string.Concat("UPDATE `Rating", p.level.name,
                                "` SET Vote = 2 WHERE Username='", p.name, "'"));
                        Player.SendMessage(p, "You changed your vote from a like to a dislike for this map.");
                        Player.GlobalMessageLevel(p.level,
                            string.Format("{0} gave this map %7a dislike{1}.",
                                p.color + p.PublicName + Server.DefaultColor, Server.DefaultColor));
                        p.MapRatingCooldown = DateTime.Now.AddMinutes(1.0);
                    }
                }
                else
                {
                    if (p.level.mapType == MapType.MyMap)
                        DBInterface.ExecuteQuery(string.Concat("INSERT INTO `Ratings` (Map, Username, Vote) VALUES (",
                            p.level.MapDbId, ",'", p.name, "', 2 )"));
                    else
                        DBInterface.ExecuteQuery(string.Concat("INSERT INTO `Rating", p.level.name,
                            "` (Username, Vote) VALUES ('", p.name, "', 2 )"));
                    Player.SendMessage(p, "You gave this map a dislike.");
                    Player.GlobalMessageLevel(p.level,
                        string.Format("{0} gave this map %7a dislike{1}.", p.color + p.PublicName + Server.DefaultColor,
                            Server.DefaultColor));
                    p.MapRatingCooldown = DateTime.Now.AddMinutes(1.0);
                }
            }
        }

        // Token: 0x06000923 RID: 2339 RVA: 0x0002D8EC File Offset: 0x0002BAEC
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/dislike - you give a thumb down for a map you are currently in.");
        }
    }
}