using System;
using System.Data;

namespace MCDzienny
{
    public class CmdDislike : Command
    {
        public override string name { get { return "dislike"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            if (p == null)
            {
                Player.SendMessage(null, "You cannot use this command from console.");
                return;
            }
            if (p.MapRatingCooldown > DateTime.Now)
            {
                Player.SendMessage(p, string.Format("Your cooldown on voting: {0}", Utils.TimeSpanToString(p.MapRatingCooldown - DateTime.Now)));
                return;
            }
            string text = null;
            text = p.level.mapType != MapType.MyMap ? "SELECT * FROM `Rating" + p.level.name + "` WHERE Username='" + p.name + "'"
                : "SELECT * FROM `Ratings` WHERE Username='" + p.name + "' AND Map=" + p.level.MapDbId;
            using (DataTable dataTable = DBInterface.fillData(text))
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
                        {
                            DBInterface.ExecuteQuery("UPDATE `Ratings` SET Vote = 2 WHERE Username='" + p.name + "' AND Map=" + p.level.MapDbId);
                        }
                        else
                        {
                            DBInterface.ExecuteQuery("UPDATE `Rating" + p.level.name + "` SET Vote = 2 WHERE Username='" + p.name + "'");
                        }
                        Player.SendMessage(p, "You changed your vote from a like to a dislike for this map.");
                        Player.GlobalMessageLevel(p.level,
                                                  string.Format("{0} gave this map %7a dislike{1}.", p.color + p.PublicName + Server.DefaultColor, Server.DefaultColor));
                        p.MapRatingCooldown = DateTime.Now.AddMinutes(1.0);
                    }
                }
                else
                {
                    if (p.level.mapType == MapType.MyMap)
                    {
                        DBInterface.ExecuteQuery("INSERT INTO `Ratings` (Map, Username, Vote) VALUES (" + p.level.MapDbId + ",'" + p.name + "', 2 )");
                    }
                    else
                    {
                        DBInterface.ExecuteQuery("INSERT INTO `Rating" + p.level.name + "` (Username, Vote) VALUES ('" + p.name + "', 2 )");
                    }
                    Player.SendMessage(p, "You gave this map a dislike.");
                    Player.GlobalMessageLevel(p.level,
                                              string.Format("{0} gave this map %7a dislike{1}.", p.color + p.PublicName + Server.DefaultColor, Server.DefaultColor));
                    p.MapRatingCooldown = DateTime.Now.AddMinutes(1.0);
                }
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/dislike - you give a thumb down for a map you are currently in.");
        }
    }
}