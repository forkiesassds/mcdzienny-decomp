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
    // Token: 0x02000073 RID: 115
    public class CmdStats : Command
    {
        // Token: 0x040001A7 RID: 423
        private const string statisticsDirectory = "statistics";

        // Token: 0x170000D4 RID: 212
        // (get) Token: 0x060002FE RID: 766 RVA: 0x000108AC File Offset: 0x0000EAAC
        public override string name
        {
            get { return "stats"; }
        }

        // Token: 0x170000D5 RID: 213
        // (get) Token: 0x060002FF RID: 767 RVA: 0x000108B4 File Offset: 0x0000EAB4
        public override string shortcut
        {
            get { return "stat"; }
        }

        // Token: 0x170000D6 RID: 214
        // (get) Token: 0x06000300 RID: 768 RVA: 0x000108BC File Offset: 0x0000EABC
        public override string type
        {
            get { return "other"; }
        }

        // Token: 0x170000D7 RID: 215
        // (get) Token: 0x06000301 RID: 769 RVA: 0x000108C4 File Offset: 0x0000EAC4
        public override bool museumUsable
        {
            get { return true; }
        }

        // Token: 0x170000D8 RID: 216
        // (get) Token: 0x06000302 RID: 770 RVA: 0x000108C8 File Offset: 0x0000EAC8
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Nobody; }
        }

        // Token: 0x06000303 RID: 771 RVA: 0x000108CC File Offset: 0x0000EACC
        public override void Use(Player p, string message)
        {
            if (!Directory.Exists("statistics")) Directory.CreateDirectory("statistics");
            var message2 = new Message(message);
            string key;
            switch (key = message2.ReadStringLower())
            {
                case "maprating":
                    goto IL_A7;
                case "winratio":
                    DisplayWinRatio(p, message2);
                    return;
                case "humanstars":
                case "zombiestars":
                case "totalstars":
                    return;
                case null:
                    break;
                default:
                    Help(p);
                    return;
            }

            IL_A7:
            DisplayMapRating(p, message2);
        }

        // Token: 0x06000304 RID: 772 RVA: 0x00010998 File Offset: 0x0000EB98
        private void DisplayWinRatio(Player p, Message m)
        {
            string outputFilePath = null;
            if (m.ReadStringLower() != "d")
                outputFilePath = string.Concat("statistics", Path.DirectorySeparatorChar, "winratio",
                    DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"), ".html");
            new Thread(delegate()
            {
                try
                {
                    Player.SendMessage(p, "Generating the results, please wait.");
                    var list = new List<WinRatio>();
                    var zombieWinCount = 0;
                    var humanWinCount = 0;
                    using (var dataTable = DBInterface.fillData(
                        "SELECT MapName, SUM(CASE WHEN WhoWon = 0 THEN 1 ELSE 0 END) AS HumanWinCount, SUM(CASE WHEN WhoWon = 1 THEN 1 ELSE 0 END) AS ZombieWinCount FROM ZombieRounds GROUP BY MapName")
                    )
                    {
                        foreach (var obj in dataTable.Rows)
                        {
                            var dataRow = (DataRow) obj;
                            int.TryParse(dataRow["ZombieWinCount"].ToString(), out zombieWinCount);
                            int.TryParse(dataRow["HumanWinCount"].ToString(), out humanWinCount);
                            list.Add(new WinRatio(dataRow["MapName"].ToString(), humanWinCount, zombieWinCount));
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
                        var num = 1;
                        using (var enumerator2 = list.GetEnumerator())
                        {
                            while (enumerator2.MoveNext())
                            {
                                var winRatio = enumerator2.Current;
                                Player.SendMessage(p,
                                    string.Concat((num + ".").PadRight(4, ' '), winRatio.MapName.PadRight(20, '.'),
                                        " . ", winRatio.HumanWinRatio.ToString("P2").PadLeft(7), " .",
                                        winRatio.HumanWinCount.ToString().PadLeft(4), " .",
                                        winRatio.ZombieWinCount.ToString().PadLeft(4)));
                                num++;
                            }

                            goto IL_44C;
                        }
                    }

                    using (var streamWriter = new StreamWriter(outputFilePath))
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
                        var num2 = 1;
                        foreach (var winRatio2 in list)
                        {
                            streamWriter.WriteLine("<tr>");
                            streamWriter.WriteLine(WrapWithTd(num2.ToString(), winRatio2.MapName,
                                winRatio2.HumanWinRatio.ToString("P2"), winRatio2.HumanWinCount.ToString(),
                                winRatio2.ZombieWinCount.ToString(), winRatio2.RoundsCount.ToString()));
                            streamWriter.WriteLine("</tr>");
                            num2++;
                        }

                        streamWriter.WriteLine("</tbody>");
                        streamWriter.WriteLine("</table>");
                        streamWriter.WriteLine("</body>");
                        streamWriter.WriteLine("</html>");
                    }

                    Player.SendMessage(p, "Results were saved to a file: " + outputFilePath);
                    IL_44C: ;
                }
                catch (Exception ex)
                {
                    Server.ErrorLog(ex);
                }
            }).Start();
        }

        // Token: 0x06000305 RID: 773 RVA: 0x00010A30 File Offset: 0x0000EC30
        private static void DisplayMapRating(Player p, Message m)
        {
            string outputFilePath = null;
            if (m.ReadStringLower() != "d")
                outputFilePath = string.Concat("statistics", Path.DirectorySeparatorChar, "maprating",
                    DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"), ".html");
            new Thread(delegate()
            {
                try
                {
                    Player.SendMessage(p, "Generating the results, please wait.");
                    var list = new List<Result>();
                    if (Server.useMySQL)
                        using (var dataTable = DBInterface.fillData("SHOW TABLES LIKE 'Rating%'"))
                        {
                            foreach (var obj in dataTable.Rows)
                            {
                                var dataRow = (DataRow) obj;
                                using (var dataTable2 =
                                    DBInterface.fillData("SELECT COUNT(*) FROM `" + dataRow[0] + "` WHERE Vote = 1"))
                                {
                                    using (var dataTable3 =
                                        DBInterface.fillData("SELECT COUNT(*) FROM `" + dataRow[0] +
                                                             "` WHERE Vote = 2"))
                                    {
                                        list.Add(new Result(dataRow[0].ToString().Remove(0, 6),
                                            int.Parse(dataTable2.Rows[0]["COUNT(*)"].ToString()),
                                            int.Parse(dataTable3.Rows[0]["COUNT(*)"].ToString())));
                                    }
                                }
                            }

                            goto IL_2A1;
                        }

                    using (var dataTable4 = DBInterface.fillData("SELECT * FROM sqlite_master WHERE type='table'"))
                    {
                        var list2 = new List<string>();
                        foreach (var obj2 in dataTable4.Rows)
                        {
                            var dataRow2 = (DataRow) obj2;
                            if (dataRow2["name"].ToString().StartsWith("Rating"))
                                list2.Add(dataRow2["name"].ToString());
                        }

                        foreach (var text in list2)
                            using (var dataTable5 =
                                DBInterface.fillData("SELECT COUNT(*) FROM `" + text + "` WHERE Vote = 1"))
                            {
                                using (var dataTable6 =
                                    DBInterface.fillData("SELECT COUNT(*) FROM `" + text + "` WHERE Vote = 2"))
                                {
                                    list.Add(new Result(text.Remove(0, 6),
                                        int.Parse(dataTable5.Rows[0]["COUNT(*)"].ToString()),
                                        int.Parse(dataTable6.Rows[0]["COUNT(*)"].ToString())));
                                }
                            }
                    }

                    IL_2A1:
                    list = (from r in list
                        where r.Likes + r.Dislikes > 0
                        orderby r.LikesPercentage descending, r.Likes descending, r.Dislikes
                        select r).ToList();
                    if (outputFilePath == null)
                    {
                        Player.SendMessage(p, "Maps and ratings, sorted from the highest to the lowest rating:");
                        Player.SendMessage(p, "No. - Map Name - Likes% - Likes - Dislikes");
                        var num = 1;
                        using (var enumerator4 = list.GetEnumerator())
                        {
                            while (enumerator4.MoveNext())
                            {
                                var result = enumerator4.Current;
                                Player.SendMessage(p,
                                    string.Concat((num + ".").PadRight(4, ' '), result.MapName.PadRight(20, '.'), " . ",
                                        result.LikesPercentage.ToString("P2").PadLeft(7), " .",
                                        result.Likes.ToString().PadLeft(4), " .",
                                        result.Dislikes.ToString().PadLeft(4)));
                                num++;
                            }

                            goto IL_608;
                        }
                    }

                    using (var streamWriter = new StreamWriter(outputFilePath))
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
                        var num2 = 1;
                        foreach (var result2 in list)
                        {
                            streamWriter.WriteLine("<tr>");
                            streamWriter.WriteLine(WrapWithTd(num2.ToString(), result2.MapName,
                                result2.LikesPercentage.ToString("P2"), result2.Likes.ToString(),
                                result2.Dislikes.ToString()));
                            streamWriter.WriteLine("</tr>");
                            num2++;
                        }

                        streamWriter.WriteLine("</tbody>");
                        streamWriter.WriteLine("</table>");
                        streamWriter.WriteLine("</body>");
                        streamWriter.WriteLine("</html>");
                    }

                    Player.SendMessage(p, "Results were saved to a file: " + outputFilePath);
                    IL_608: ;
                }
                catch (Exception ex)
                {
                    Server.ErrorLog(ex);
                }
            }).Start();
        }

        // Token: 0x06000306 RID: 774 RVA: 0x00010AC8 File Offset: 0x0000ECC8
        public static string WrapWithTd(params string[] fields)
        {
            var stringBuilder = new StringBuilder();
            foreach (var str in fields) stringBuilder.Append("<td>" + str + "</td>");
            return stringBuilder.ToString();
        }

        // Token: 0x06000307 RID: 775 RVA: 0x00010B0C File Offset: 0x0000ED0C
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/stats maprating - saves map rating to a file in statistics directory.");
            Player.SendMessage(p, "/stats maprating d - displays map rating.");
            Player.SendMessage(p, "/stats winratio <d> - saves <displays> human/zombie win ratio.");
        }

        // Token: 0x02000074 RID: 116
        private class Result
        {
            // Token: 0x040001A9 RID: 425
            public readonly int Dislikes;

            // Token: 0x040001A8 RID: 424
            public readonly int Likes;

            // Token: 0x040001AA RID: 426
            public readonly double LikesPercentage;

            // Token: 0x040001AB RID: 427
            public readonly string MapName;

            // Token: 0x06000309 RID: 777 RVA: 0x00010B38 File Offset: 0x0000ED38
            public Result(string mapName, int likes, int dislikes)
            {
                Likes = likes;
                Dislikes = dislikes;
                MapName = mapName;
                LikesPercentage = likes + dislikes > 0 ? likes / (double) (likes + dislikes) : 0.0;
            }
        }

        // Token: 0x02000075 RID: 117
        private class WinRatio
        {
            // Token: 0x040001AE RID: 430
            public readonly int HumanWinCount;

            // Token: 0x040001B0 RID: 432
            public readonly double HumanWinRatio;

            // Token: 0x040001AC RID: 428
            public readonly string MapName;

            // Token: 0x040001AD RID: 429
            public readonly int RoundsCount;

            // Token: 0x040001AF RID: 431
            public readonly int ZombieWinCount;

            // Token: 0x0600030A RID: 778 RVA: 0x00010B74 File Offset: 0x0000ED74
            public WinRatio(string mapName, int humanWinCount, int zombieWinCount)
            {
                MapName = mapName;
                RoundsCount = humanWinCount + zombieWinCount;
                HumanWinCount = humanWinCount;
                ZombieWinCount = zombieWinCount;
                HumanWinRatio = RoundsCount > 0 ? HumanWinCount / (double) RoundsCount : 0.0;
            }
        }
    }
}