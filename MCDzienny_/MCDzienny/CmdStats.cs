using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using MCDzienny.Misc;

namespace MCDzienny
{
    public class CmdStats : Command
    {

        const string statisticsDirectory = "statistics";

        public override string name { get { return "stats"; } }

        public override string shortcut { get { return "stat"; } }

        public override string type { get { return "other"; } }

        public override bool museumUsable { get { return true; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Nobody; } }

        public override void Use(Player p, string message)
        {
            if (!Directory.Exists("statistics"))
            {
                Directory.CreateDirectory("statistics");
            }
            Message message2 = new Message(message);
            switch (message2.ReadStringLower())
            {
                case "maprating":
                case null:
                    DisplayMapRating(p, message2);
                    break;
                case "winratio":
                    DisplayWinRatio(p, message2);
                    break;
                default:
                    Help(p);
                    break;
                case "humanstars":
                case "zombiestars":
                case "totalstars":
                    break;
            }
        }

        void DisplayWinRatio(Player p, Message m)
        {
            string outputFilePath = null;
            if (m.ReadStringLower() != "d")
            {
                outputFilePath = "statistics" + Path.DirectorySeparatorChar + "winratio" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".html";
            }
            new Thread((ThreadStart)delegate
            {
                try
                {
                    Player.SendMessage(p, "Generating the results, please wait.");
                    var list = new List<WinRatio>();
                    int result = 0;
                    int result2 = 0;
                    using (DataTable dataTable =
                           DBInterface.fillData(
                               "SELECT MapName, SUM(CASE WHEN WhoWon = 0 THEN 1 ELSE 0 END) AS HumanWinCount, SUM(CASE WHEN WhoWon = 1 THEN 1 ELSE 0 END) AS ZombieWinCount FROM ZombieRounds GROUP BY MapName"))
                    {
                        foreach (DataRow row in dataTable.Rows)
                        {
                            int.TryParse(row["ZombieWinCount"].ToString(), out result);
                            int.TryParse(row["HumanWinCount"].ToString(), out result2);
                            list.Add(new WinRatio(row["MapName"].ToString(), result2, result));
                        }
                    }
                    list = (from r in list
                        where r.RoundsCount > 0
                        orderby r.HumanWinRatio descending, r.HumanWinCount descending, r.ZombieWinCount
                        select r).ToList();
                    if (outputFilePath == null)
                    {
                        Player.SendMessage(p, "Maps and ratings, sorted from the highest to the lowest rating:");
                        Player.SendMessage(p, "No. - Name - Human Win Ratio[%] - HumanWins - ZombieWins");
                        int num = 1;
                        {
                            foreach (WinRatio item in list)
                            {
                                Player.SendMessage(
                                    p,
                                    (num + ".").PadRight(4, ' ') + item.MapName.PadRight(20, '.') + " . " + item.HumanWinRatio.ToString("P2").PadLeft(7) + " ." +
                                    item.HumanWinCount.ToString().PadLeft(4) + " ." + item.ZombieWinCount.ToString().PadLeft(4));
                                num++;
                            }
                            return;
                        }
                    }
                    using (StreamWriter streamWriter = new StreamWriter(outputFilePath))
                    {
                        streamWriter.WriteLine("<html>");
                        streamWriter.WriteLine("<body>");
                        streamWriter.WriteLine("<table>");
                        streamWriter.WriteLine("<caption>Maps win ratio sorted by human win ratio</caption>");
                        streamWriter.WriteLine("<thead>");
                        streamWriter.WriteLine("<tr>");
                        streamWriter.WriteLine("<th>No.</th>");
                        streamWriter.WriteLine("<th>Map Name</th>");
                        streamWriter.WriteLine("<th>Human Win Ratio[%]</th>");
                        streamWriter.WriteLine("<th>Human Wins</th>");
                        streamWriter.WriteLine("<th>Zombie Wins</th>");
                        streamWriter.WriteLine("<th>Rounds Count</th>");
                        streamWriter.WriteLine("</tr>");
                        streamWriter.WriteLine("</thead>");
                        streamWriter.WriteLine("<tbody>");
                        int num2 = 1;
                        foreach (WinRatio item2 in list)
                        {
                            streamWriter.WriteLine("<tr>");
                            streamWriter.WriteLine(WrapWithTd(num2.ToString(), item2.MapName, item2.HumanWinRatio.ToString("P2"), item2.HumanWinCount.ToString(),
                                                              item2.ZombieWinCount.ToString(), item2.RoundsCount.ToString()));
                            streamWriter.WriteLine("</tr>");
                            num2++;
                        }
                        streamWriter.WriteLine("</tbody>");
                        streamWriter.WriteLine("</table>");
                        streamWriter.WriteLine("</body>");
                        streamWriter.WriteLine("</html>");
                    }
                    Player.SendMessage(p, "Results were saved to a file: " + outputFilePath);
                }
                catch (Exception ex)
                {
                    Server.ErrorLog(ex);
                }
            }).Start();
        }

        static void DisplayMapRating(Player p, Message m)
        {
            string outputFilePath = null;
            if (m.ReadStringLower() != "d")
            {
                outputFilePath = "statistics" + Path.DirectorySeparatorChar + "maprating" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".html";
            }
            new Thread((ThreadStart)delegate
            {
                try
                {
                    Player.SendMessage(p, "Generating the results, please wait.");
                    var list = new List<Result>();
                    if (Server.useMySQL)
                    {
                        using (DataTable dataTable = DBInterface.fillData("SHOW TABLES LIKE 'Rating%'"))
                        {
                            foreach (DataRow row in dataTable.Rows)
                            {
                                using (DataTable dataTable2 = DBInterface.fillData("SELECT COUNT(*) FROM `" + row[0] + "` WHERE Vote = 1"))
                                {
                                    using (DataTable dataTable3 = DBInterface.fillData("SELECT COUNT(*) FROM `" + row[0] + "` WHERE Vote = 2"))
                                    {
                                        list.Add(new Result(row[0].ToString().Remove(0, 6), int.Parse(dataTable2.Rows[0]["COUNT(*)"].ToString()),
                                                            int.Parse(dataTable3.Rows[0]["COUNT(*)"].ToString())));
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        using (DataTable dataTable4 = DBInterface.fillData("SELECT * FROM sqlite_master WHERE type='table'"))
                        {
                            var list2 = new List<string>();
                            foreach (DataRow row2 in dataTable4.Rows)
                            {
                                if (row2["name"].ToString().StartsWith("Rating"))
                                {
                                    list2.Add(row2["name"].ToString());
                                }
                            }
                            foreach (string item in list2)
                            {
                                using (DataTable dataTable5 = DBInterface.fillData("SELECT COUNT(*) FROM `" + item + "` WHERE Vote = 1"))
                                {
                                    using (DataTable dataTable6 = DBInterface.fillData("SELECT COUNT(*) FROM `" + item + "` WHERE Vote = 2"))
                                    {
                                        list.Add(new Result(item.Remove(0, 6), int.Parse(dataTable5.Rows[0]["COUNT(*)"].ToString()),
                                                            int.Parse(dataTable6.Rows[0]["COUNT(*)"].ToString())));
                                    }
                                }
                            }
                        }
                    }
                    list = (from r in list
                        where r.Likes + r.Dislikes > 0
                        orderby r.LikesPercentage descending, r.Likes descending, r.Dislikes
                        select r).ToList();
                    if (outputFilePath == null)
                    {
                        Player.SendMessage(p, "Maps and ratings, sorted from the highest to the lowest rating:");
                        Player.SendMessage(p, "No. - Map Name - Likes% - Likes - Dislikes");
                        int num = 1;
                        {
                            foreach (Result item2 in list)
                            {
                                Player.SendMessage(
                                    p,
                                    (num + ".").PadRight(4, ' ') + item2.MapName.PadRight(20, '.') + " . " + item2.LikesPercentage.ToString("P2").PadLeft(7) + " ." +
                                    item2.Likes.ToString().PadLeft(4) + " ." + item2.Dislikes.ToString().PadLeft(4));
                                num++;
                            }
                            return;
                        }
                    }
                    using (StreamWriter streamWriter = new StreamWriter(outputFilePath))
                    {
                        streamWriter.WriteLine("<html>");
                        streamWriter.WriteLine("<body>");
                        streamWriter.WriteLine("<table>");
                        streamWriter.WriteLine("<caption>Maps rating sorted by likes share</caption>");
                        streamWriter.WriteLine("<thead>");
                        streamWriter.WriteLine("<tr>");
                        streamWriter.WriteLine("<th>No.</th>");
                        streamWriter.WriteLine("<th>Map Name</th>");
                        streamWriter.WriteLine("<th>Likes share[%]</th>");
                        streamWriter.WriteLine("<th>Likes</th>");
                        streamWriter.WriteLine("<th>Dislikes</th>");
                        streamWriter.WriteLine("</tr>");
                        streamWriter.WriteLine("</thead>");
                        streamWriter.WriteLine("<tbody>");
                        int num2 = 1;
                        foreach (Result item3 in list)
                        {
                            streamWriter.WriteLine("<tr>");
                            streamWriter.WriteLine(WrapWithTd(num2.ToString(), item3.MapName, item3.LikesPercentage.ToString("P2"), item3.Likes.ToString(),
                                                              item3.Dislikes.ToString()));
                            streamWriter.WriteLine("</tr>");
                            num2++;
                        }
                        streamWriter.WriteLine("</tbody>");
                        streamWriter.WriteLine("</table>");
                        streamWriter.WriteLine("</body>");
                        streamWriter.WriteLine("</html>");
                    }
                    Player.SendMessage(p, "Results were saved to a file: " + outputFilePath);
                }
                catch (Exception ex)
                {
                    Server.ErrorLog(ex);
                }
            }).Start();
        }

        public static string WrapWithTd(params string[] fields)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (string text in fields)
            {
                stringBuilder.Append("<td>" + text + "</td>");
            }
            return stringBuilder.ToString();
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/stats maprating - saves map rating to a file in statistics directory.");
            Player.SendMessage(p, "/stats maprating d - displays map rating.");
            Player.SendMessage(p, "/stats winratio <d> - saves <displays> human/zombie win ratio.");
        }

        class Result
        {

            public readonly int Dislikes;
            public readonly int Likes;

            public readonly double LikesPercentage;

            public readonly string MapName;

            public Result(string mapName, int likes, int dislikes)
            {
                Likes = likes;
                Dislikes = dislikes;
                MapName = mapName;
                LikesPercentage = likes + dislikes > 0 ? likes / (double)(likes + dislikes) : 0.0;
            }
        }

        class WinRatio
        {

            public readonly int HumanWinCount;

            public readonly double HumanWinRatio;
            public readonly string MapName;

            public readonly int RoundsCount;

            public readonly int ZombieWinCount;

            public WinRatio(string mapName, int humanWinCount, int zombieWinCount)
            {
                MapName = mapName;
                RoundsCount = humanWinCount + zombieWinCount;
                HumanWinCount = humanWinCount;
                ZombieWinCount = zombieWinCount;
                HumanWinRatio = RoundsCount > 0 ? HumanWinCount / (double)RoundsCount : 0.0;
            }
        }
    }
}