using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using MCDzienny.CpeApi;
using MCDzienny.MultiMessages;
using MCDzienny.Settings;

namespace MCDzienny.InfectionSystem
{
    // Token: 0x020000A3 RID: 163
    public class InfectionUtils
    {
        // Token: 0x04000247 RID: 583
        private static readonly List<string> votersList = new List<string>();

        // Token: 0x04000248 RID: 584
        private static readonly int[] votes = new int[3];

        // Token: 0x04000249 RID: 585
        private static DateTime endTime;

        // Token: 0x0400024A RID: 586

        // Token: 0x0400024B RID: 587
        public static int display = 15;

        // Token: 0x17000138 RID: 312
        // (get) Token: 0x0600045A RID: 1114 RVA: 0x000191F0 File Offset: 0x000173F0
        public static TimeSpan TimeToEnd
        {
            get
            {
                var result = endTime.Subtract(DateTime.Now);
                if (result.TotalSeconds < 0.0) result = new TimeSpan(0L);
                return result;
            }
        }

        // Token: 0x17000139 RID: 313
        // (get) Token: 0x0600045B RID: 1115 RVA: 0x0001922C File Offset: 0x0001742C
        // (set) Token: 0x0600045C RID: 1116 RVA: 0x00019234 File Offset: 0x00017434
        public static DateTime StartTime { get; set; }

        // Token: 0x1700013A RID: 314
        // (get) Token: 0x0600045D RID: 1117 RVA: 0x0001923C File Offset: 0x0001743C
        // (set) Token: 0x0600045E RID: 1118 RVA: 0x00019244 File Offset: 0x00017444
        public static DateTime EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

        // Token: 0x0600045F RID: 1119 RVA: 0x0001924C File Offset: 0x0001744C
        public static bool CountVotes(string vote, Player p = null)
        {
            if (votersList.Contains(p.name.ToLower()))
            {
                if (p.group.Permission >= LevelPermission.Operator) return false;
                Player.SendMessage(p, "You have already voted. Wait a moment till the end of voting.");
            }
            else
            {
                if (vote != null)
                {
                    if (vote == "1")
                    {
                        votes[0]++;
                        votersList.Add(p.name.ToLower());
                        Player.SendMessage(p, "You have voted for the 1st map.");
                        return true;
                    }

                    if (vote == "2")
                    {
                        votes[1]++;
                        votersList.Add(p.name.ToLower());
                        Player.SendMessage(p, "You have voted for the 2nd map.");
                        return true;
                    }

                    if (vote == "3")
                    {
                        votes[2]++;
                        votersList.Add(p.name.ToLower());
                        Player.SendMessage(p, "You have voted for the 3rd map.");
                        return true;
                    }
                }

                Player.SendMessage(p, "In order to vote write 1 or 2 or 3.");
            }

            return true;
        }

        // Token: 0x06000460 RID: 1120 RVA: 0x00019380 File Offset: 0x00017580
        public static int Voting()
        {
            if (InfectionMaps.infectionMaps.Count < 4) return -1;
            var array = new int[InfectionMaps.infectionMaps.Count];
            for (var i = 0; i < array.Length; i++) array[i] = i;
            var random = new Random();
            var array2 = new int[3];
            var num = random.Next(0, InfectionMaps.infectionMaps.Count);
            array2[0] = array[num];
            array[num] = array[array.Length - 1];
            num = random.Next(0, InfectionMaps.infectionMaps.Count - 1);
            array2[1] = array[num];
            array[num] = array[array.Length - 2];
            num = random.Next(0, InfectionMaps.infectionMaps.Count - 2);
            array2[2] = array[num];
            if (GeneralSettings.All.ExperimentalMessages)
            {
                var options = new V1.MessageOptions
                {
                    DisplayTime = TimeSpan.FromSeconds(4.0),
                    MaxDelay = TimeSpan.FromSeconds(3.0)
                };
                Server.InfectionLevel.ChatLevelCpe(V1.MessageType.Announcement, options,
                    "%aM%ba%cp %av%bo%ct%ai%bn%cg");
                Server.InfectionLevel.ChatLevel(Cpe.V1.Status1, "_________ Vote for the next map _________");
                var text = string.Concat("Write %c1%s for ", InfectionMaps.infectionMaps[array2[0]].Name,
                    ", %c2%s for ", InfectionMaps.infectionMaps[array2[1]].Name, ", %c3%s for ",
                    InfectionMaps.infectionMaps[array2[2]].Name);
                if (text.Length <= 61)
                {
                    Server.InfectionLevel.ChatLevel(Cpe.V1.Status2, text);
                }
                else
                {
                    string message;
                    string message2;
                    BreakMessage(text, out message, out message2);
                    Server.InfectionLevel.ChatLevel(Cpe.V1.Status2, message);
                    Server.InfectionLevel.ChatLevel(Cpe.V1.Status3, message2);
                }
            }
            else
            {
                Server.InfectionLevel.ChatLevel("_________ Vote for the next map _________");
                Server.InfectionLevel.ChatLevel(string.Concat("Write %c1%s for ",
                    InfectionMaps.infectionMaps[array2[0]].Name, ", %c2%s for ",
                    InfectionMaps.infectionMaps[array2[1]].Name, ", %c3%s for ",
                    InfectionMaps.infectionMaps[array2[2]].Name));
            }

            Server.s.Log("Vote for the next map");
            Server.s.Log(string.Concat("Write 1 for ", InfectionMaps.infectionMaps[array2[0]].Name, ", 2 for ",
                InfectionMaps.infectionMaps[array2[1]].Name, ", 3 for ", InfectionMaps.infectionMaps[array2[2]].Name));
            Server.voteMode = true;
            Thread.Sleep(1000 * InfectionSettings.All.MapVoteDurationSeconds);
            Server.voteMode = false;
            if (GeneralSettings.All.ExperimentalMessages)
            {
                Server.InfectionLevel.ChatLevel(Cpe.V1.Status1, TimeSpan.FromSeconds(6.0),
                    "_________ Results of the voting _________");
                var text2 = string.Concat("Map: ", InfectionMaps.infectionMaps[array2[0]].Name, " - ", votes[0], ", ",
                    InfectionMaps.infectionMaps[array2[1]].Name, " - ", votes[1], ", ",
                    InfectionMaps.infectionMaps[array2[2]].Name, " - ", votes[2], " votes.");
                if (text2.Length <= 61)
                {
                    Server.InfectionLevel.ChatLevel(Cpe.V1.Status2, TimeSpan.FromSeconds(6.0), text2);
                    Server.InfectionLevel.ChatLevel(Cpe.V1.Status3, "");
                }
                else
                {
                    string message3;
                    string message4;
                    BreakMessage(text2, out message3, out message4);
                    Server.InfectionLevel.ChatLevel(Cpe.V1.Status2, TimeSpan.FromSeconds(6.0), message3);
                    Server.InfectionLevel.ChatLevel(Cpe.V1.Status3, TimeSpan.FromSeconds(6.0), message4);
                }
            }
            else
            {
                Server.InfectionLevel.ChatLevel("_________ Results of the voting _________");
                Server.InfectionLevel.ChatLevel(string.Concat("Map: ", InfectionMaps.infectionMaps[array2[0]].Name,
                    " - ", votes[0], ", ", InfectionMaps.infectionMaps[array2[1]].Name, " - ", votes[1], ", ",
                    InfectionMaps.infectionMaps[array2[2]].Name, " - ", votes[2], " votes."));
            }

            Server.s.Log("Results of voting:");
            Server.s.Log(string.Concat("Map: ", InfectionMaps.infectionMaps[array2[0]].Name, " - ", votes[0], ", ",
                InfectionMaps.infectionMaps[array2[1]].Name, " - ", votes[1], ", ",
                InfectionMaps.infectionMaps[array2[2]].Name, " - ", votes[2], " votes."));
            int num2;
            if (votes[0] >= votes[1])
            {
                if (votes[0] >= votes[2])
                    num2 = 0;
                else
                    num2 = 2;
            }
            else if (votes[1] >= votes[2])
            {
                num2 = 1;
            }
            else
            {
                num2 = 2;
            }

            Thread.Sleep(4000);
            Server.InfectionLevel.ChatLevel("The next map is: %a" + InfectionMaps.infectionMaps[array2[num2]].Name);
            Server.s.Log("The next map is: " + InfectionMaps.infectionMaps[array2[num2]].Name);
            votes[0] = 0;
            votes[1] = 0;
            votes[2] = 0;
            votersList.Clear();
            return array2[num2];
        }

        // Token: 0x06000461 RID: 1121 RVA: 0x00019A98 File Offset: 0x00017C98
        private static void BreakMessage(string message, out string firstLine, out string secondLine)
        {
            if (message.Length <= 61)
            {
                firstLine = message;
                secondLine = "";
                return;
            }

            var num = message.Substring(0, 61).LastIndexOf(' ');
            num = num >= 0 ? num : 61;
            firstLine = message.Substring(0, num);
            secondLine = message.Substring(firstLine.Length);
        }

        // Token: 0x06000462 RID: 1122 RVA: 0x00019AF0 File Offset: 0x00017CF0
        public static void PrintMapRating()
        {
            using (var dataTable =
                DBInterface.fillData(
                    "SELECT COUNT(*) FROM `Rating" + InfectionSystem.currentInfectionLevel.name + "` WHERE Vote = 1",
                    true))
            {
                using (var dataTable2 = DBInterface.fillData(
                    "SELECT COUNT(*) FROM `Rating" + InfectionSystem.currentInfectionLevel.name + "` WHERE Vote = 2",
                    true))
                {
                    var result = 0;
                    int.TryParse(dataTable.Rows[0]["COUNT(*)"].ToString(), out result);
                    var result2 = 0;
                    int.TryParse(dataTable2.Rows[0]["COUNT(*)"].ToString(), out result2);
                    var likePercentage = 0.0;
                    if (result + result2 > 0) likePercentage = result / (double) (result + result2) * 100.0;
                    Player.GlobalMessageLevelSendEmptyLine(InfectionSystem.currentInfectionLevel);
                    Player.GlobalMessageLevel(InfectionSystem.currentInfectionLevel,
                        "%2[%a" + LikesBar(likePercentage) + "%2]");
                    Player.GlobalMessageLevel(InfectionSystem.currentInfectionLevel,
                        "%2Map rating: %a" + result + " %2likes, %7" + result2 + " %2dislikes.");
                    Player.GlobalMessageLevel(InfectionSystem.currentInfectionLevel, Lang.LavaSystem.MapRatingTip);
                }
            }
        }

        // Token: 0x06000463 RID: 1123 RVA: 0x00019C78 File Offset: 0x00017E78
        private static string LikesBar(double likePercentage)
        {
            var num = (int) (likePercentage / 3.3333 + 0.5);
            if (num == 30) return "______________________________";
            var text = "______________________________";
            return text.Insert(num, "%7");
        }

        // Token: 0x06000464 RID: 1124 RVA: 0x00019CBC File Offset: 0x00017EBC
        public static void PrintMapAuthor()
        {
            Player.GlobalMessageLevel(InfectionSystem.currentInfectionLevel,
                "%d* %fMap name:%b " + InfectionSystem.currentInfectionLevel.name);
            Player.GlobalMessageLevel(InfectionSystem.currentInfectionLevel,
                "%d* %fAuthor:%b " + InfectionSystem.currentInfectionMap.Author);
            Player.GlobalMessageLevel(InfectionSystem.currentInfectionLevel,
                "%d* %fPillaring:%b " +
                (InfectionSystem.currentInfectionMap.IsPillaringAllowed ? "allowed" : "disallowed"));
        }

        // Token: 0x06000465 RID: 1125 RVA: 0x00019D34 File Offset: 0x00017F34
        public static void RoundTimeManager(object sender, ElapsedEventArgs e)
        {
            try
            {
                var timeSpan = endTime.Subtract(DateTime.Now);
                if (TimeToEnd.TotalSeconds < 1.0)
                {
                    InfectionSystem.EndInfectionRound();
                }
                else
                {
                    if (timeSpan.TotalSeconds <= 10.0 && timeSpan.TotalSeconds >= 1.0)
                    {
                        Player.GlobalMessageLevel(InfectionSystem.currentInfectionLevel,
                            string.Format(MessagesManager.GetString("InfectionRoundEndsIn"),
                                timeSpan.Seconds.ToString()));
                    }
                    else if (display <= 0)
                    {
                        Player.GlobalMessageLevel(InfectionSystem.currentInfectionLevel,
                            string.Format(MessagesManager.GetString("InfectionTimeLeft"), timeSpan.Minutes,
                                timeSpan.Seconds.ToString("00")));
                        var ieh = MessagesManager.GetString("InfectionEncourageHumans");
                        var iez = MessagesManager.GetString("InfectionEncourageZombies");
                        InfectionSystem.notInfected.ForEach(delegate(Player p) { p.SendMessage(ieh); });
                        InfectionSystem.infected.ForEach(delegate(Player p) { p.SendMessage(iez); });
                        display = 55;
                    }

                    display--;
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }
    }
}