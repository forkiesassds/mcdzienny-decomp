using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Timers;
using MCDzienny.CpeApi;
using MCDzienny.MultiMessages;
using MCDzienny.Settings;

namespace MCDzienny.InfectionSystem
{
    public class InfectionUtils
    {
        static readonly List<string> votersList = new List<string>();

        static readonly int[] votes = new int[3];

        static DateTime endTime;

        public static int display = 15;

        public static TimeSpan TimeToEnd
        {
            get
            {
                TimeSpan result = endTime.Subtract(DateTime.Now);
                if (result.TotalSeconds < 0.0)
                {
                    result = new TimeSpan(0L);
                }
                return result;
            }
        }

        public static DateTime StartTime { get; set; }

        public static DateTime EndTime { get { return endTime; } set { endTime = value; } }

        public static bool CountVotes(string vote, Player p = null)
        {
            if (votersList.Contains(p.name.ToLower()))
            {
                if (p.group.Permission >= LevelPermission.Operator)
                {
                    return false;
                }
                Player.SendMessage(p, "You have already voted. Wait a moment till the end of voting.");
            }
            else
            {
                switch (vote)
                {
                    case "1":
                        votes[0]++;
                        votersList.Add(p.name.ToLower());
                        Player.SendMessage(p, "You have voted for the 1st map.");
                        break;
                    case "2":
                        votes[1]++;
                        votersList.Add(p.name.ToLower());
                        Player.SendMessage(p, "You have voted for the 2nd map.");
                        break;
                    case "3":
                        votes[2]++;
                        votersList.Add(p.name.ToLower());
                        Player.SendMessage(p, "You have voted for the 3rd map.");
                        break;
                    default:
                        Player.SendMessage(p, "In order to vote write 1 or 2 or 3.");
                        break;
                }
            }
            return true;
        }

        public static int Voting()
        {
            if (InfectionMaps.infectionMaps.Count < 4)
            {
                return -1;
            }
            int[] array = new int[InfectionMaps.infectionMaps.Count];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = i;
            }
            Random random = new Random();
            int[] array2 = new int[3];
            int num = random.Next(0, InfectionMaps.infectionMaps.Count);
            array2[0] = array[num];
            array[num] = array[array.Length - 1];
            num = random.Next(0, InfectionMaps.infectionMaps.Count - 1);
            array2[1] = array[num];
            array[num] = array[array.Length - 2];
            num = random.Next(0, InfectionMaps.infectionMaps.Count - 2);
            array2[2] = array[num];
            if (GeneralSettings.All.ExperimentalMessages)
            {
                V1.MessageOptions messageOptions = new V1.MessageOptions();
                messageOptions.DisplayTime = TimeSpan.FromSeconds(4.0);
                messageOptions.MaxDelay = TimeSpan.FromSeconds(3.0);
                V1.MessageOptions options = messageOptions;
                Server.InfectionLevel.ChatLevelCpe(V1.MessageType.Announcement, options, "%aM%ba%cp %av%bo%ct%ai%bn%cg");
                Server.InfectionLevel.ChatLevel(Cpe.V1.Status1, "_________ Vote for the next map _________");
                string text = "Write %c1%s for " + InfectionMaps.infectionMaps[array2[0]].Name + ", %c2%s for " + InfectionMaps.infectionMaps[array2[1]].Name +
                    ", %c3%s for " + InfectionMaps.infectionMaps[array2[2]].Name;
                if (text.Length <= 61)
                {
                    Server.InfectionLevel.ChatLevel(Cpe.V1.Status2, text);
                }
                else
                {
                    string firstLine;
                    string secondLine;
                    BreakMessage(text, out firstLine, out secondLine);
                    Server.InfectionLevel.ChatLevel(Cpe.V1.Status2, firstLine);
                    Server.InfectionLevel.ChatLevel(Cpe.V1.Status3, secondLine);
                }
            }
            else
            {
                Server.InfectionLevel.ChatLevel("_________ Vote for the next map _________");
                Server.InfectionLevel.ChatLevel("Write %c1%s for " + InfectionMaps.infectionMaps[array2[0]].Name + ", %c2%s for " +
                                                InfectionMaps.infectionMaps[array2[1]].Name + ", %c3%s for " + InfectionMaps.infectionMaps[array2[2]].Name);
            }
            Server.s.Log("Vote for the next map");
            Server.s.Log("Write 1 for " + InfectionMaps.infectionMaps[array2[0]].Name + ", 2 for " + InfectionMaps.infectionMaps[array2[1]].Name + ", 3 for " +
                         InfectionMaps.infectionMaps[array2[2]].Name);
            Server.voteMode = true;
            Thread.Sleep(1000 * InfectionSettings.All.MapVoteDurationSeconds);
            Server.voteMode = false;
            if (GeneralSettings.All.ExperimentalMessages)
            {
                Server.InfectionLevel.ChatLevel(Cpe.V1.Status1, TimeSpan.FromSeconds(6.0), "_________ Results of the voting _________");
                string text2 = "Map: " + InfectionMaps.infectionMaps[array2[0]].Name + " - " + votes[0] + ", " + InfectionMaps.infectionMaps[array2[1]].Name + " - " +
                    votes[1] + ", " + InfectionMaps.infectionMaps[array2[2]].Name + " - " + votes[2] + " votes.";
                if (text2.Length <= 61)
                {
                    Server.InfectionLevel.ChatLevel(Cpe.V1.Status2, TimeSpan.FromSeconds(6.0), text2);
                    Server.InfectionLevel.ChatLevel(Cpe.V1.Status3, "");
                }
                else
                {
                    string firstLine2;
                    string secondLine2;
                    BreakMessage(text2, out firstLine2, out secondLine2);
                    Server.InfectionLevel.ChatLevel(Cpe.V1.Status2, TimeSpan.FromSeconds(6.0), firstLine2);
                    Server.InfectionLevel.ChatLevel(Cpe.V1.Status3, TimeSpan.FromSeconds(6.0), secondLine2);
                }
            }
            else
            {
                Server.InfectionLevel.ChatLevel("_________ Results of the voting _________");
                Server.InfectionLevel.ChatLevel("Map: " + InfectionMaps.infectionMaps[array2[0]].Name + " - " + votes[0] + ", " +
                                                InfectionMaps.infectionMaps[array2[1]].Name + " - " + votes[1] + ", " + InfectionMaps.infectionMaps[array2[2]].Name +
                                                " - " + votes[2] + " votes.");
            }
            Server.s.Log("Results of voting:");
            Server.s.Log("Map: " + InfectionMaps.infectionMaps[array2[0]].Name + " - " + votes[0] + ", " + InfectionMaps.infectionMaps[array2[1]].Name + " - " + votes[1] +
                         ", " + InfectionMaps.infectionMaps[array2[2]].Name + " - " + votes[2] + " votes.");
            int num2 = votes[0] >= votes[1] ? votes[0] < votes[2] ? 2 : 0 : votes[1] >= votes[2] ? 1 : 2;
            Thread.Sleep(4000);
            Server.InfectionLevel.ChatLevel("The next map is: %a" + InfectionMaps.infectionMaps[array2[num2]].Name);
            Server.s.Log("The next map is: " + InfectionMaps.infectionMaps[array2[num2]].Name);
            votes[0] = 0;
            votes[1] = 0;
            votes[2] = 0;
            votersList.Clear();
            return array2[num2];
        }

        static void BreakMessage(string message, out string firstLine, out string secondLine)
        {
            if (message.Length <= 61)
            {
                firstLine = message;
                secondLine = "";
            }
            else
            {
                int num = message.Substring(0, 61).LastIndexOf(' ');
                firstLine = message.Substring(0, num >= 0 ? num : 61);
                secondLine = message.Substring(firstLine.Length);
            }
        }

        public static void PrintMapRating()
        {
            using (DataTable dataTable =
                   DBInterface.fillData("SELECT COUNT(*) FROM `Rating" + InfectionSystem.currentInfectionLevel.name + "` WHERE Vote = 1", skipError: true))
            {
                using (DataTable dataTable2 = DBInterface.fillData("SELECT COUNT(*) FROM `Rating" + InfectionSystem.currentInfectionLevel.name + "` WHERE Vote = 2",
                                                                   skipError: true))
                {
                    int result = 0;
                    int.TryParse(dataTable.Rows[0]["COUNT(*)"].ToString(), out result);
                    int result2 = 0;
                    int.TryParse(dataTable2.Rows[0]["COUNT(*)"].ToString(), out result2);
                    double likePercentage = 0.0;
                    if (result + result2 > 0)
                    {
                        likePercentage = result / (double)(result + result2) * 100.0;
                    }
                    Player.GlobalMessageLevelSendEmptyLine(InfectionSystem.currentInfectionLevel);
                    Player.GlobalMessageLevel(InfectionSystem.currentInfectionLevel, "%2[%a" + LikesBar(likePercentage) + "%2]");
                    Player.GlobalMessageLevel(InfectionSystem.currentInfectionLevel, "%2Map rating: %a" + result + " %2likes, %7" + result2 + " %2dislikes.");
                    Player.GlobalMessageLevel(InfectionSystem.currentInfectionLevel, Lang.LavaSystem.MapRatingTip);
                }
            }
        }

        static string LikesBar(double likePercentage)
        {
            int num = (int)(likePercentage / 3.3333 + 0.5);
            if (num == 30)
            {
                return "______________________________";
            }
            string text = "______________________________";
            return text.Insert(num, "%7");
        }

        public static void PrintMapAuthor()
        {
            Player.GlobalMessageLevel(InfectionSystem.currentInfectionLevel, "%d* %fMap name:%b " + InfectionSystem.currentInfectionLevel.name);
            Player.GlobalMessageLevel(InfectionSystem.currentInfectionLevel, "%d* %fAuthor:%b " + InfectionSystem.currentInfectionMap.Author);
            Player.GlobalMessageLevel(InfectionSystem.currentInfectionLevel,
                                      "%d* %fPillaring:%b " + (InfectionSystem.currentInfectionMap.IsPillaringAllowed ? "allowed" : "disallowed"));
        }

        public static void RoundTimeManager(object sender, ElapsedEventArgs e)
        {
            try
            {
                TimeSpan timeSpan = endTime.Subtract(DateTime.Now);
                if (TimeToEnd.TotalSeconds < 1.0)
                {
                    InfectionSystem.EndInfectionRound();
                    return;
                }
                if (timeSpan.TotalSeconds <= 10.0 && timeSpan.TotalSeconds >= 1.0)
                {
                    Player.GlobalMessageLevel(InfectionSystem.currentInfectionLevel,
                                              string.Format(MessagesManager.GetString("InfectionRoundEndsIn"), timeSpan.Seconds.ToString()));
                }
                else if (display <= 0)
                {
                    Player.GlobalMessageLevel(InfectionSystem.currentInfectionLevel,
                                              string.Format(MessagesManager.GetString("InfectionTimeLeft"), timeSpan.Minutes, timeSpan.Seconds.ToString("00")));
                    string ieh = MessagesManager.GetString("InfectionEncourageHumans");
                    string iez = MessagesManager.GetString("InfectionEncourageZombies");
                    InfectionSystem.notInfected.ForEach(delegate(Player p) { p.SendMessage(ieh); });
                    InfectionSystem.infected.ForEach(delegate(Player p) { p.SendMessage(iez); });
                    display = 55;
                }
                display--;
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }
    }
}