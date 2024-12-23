using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using MCDzienny.Communication;
using MCDzienny.CpeApi;
using MCDzienny.Gui;
using MCDzienny.Misc;
using MCDzienny.MultiMessages;
using MCDzienny.Settings;
using Timer = System.Timers.Timer;

namespace MCDzienny.InfectionSystem
{
    public static class InfectionSystem
    {
        static volatile bool infectionInProgress;

        static readonly Stopwatch roundStopwatch;

        static int playersOnStartCount;

        public static Timer MainLoop;

        public static Timer TimeDisplay;

        static readonly Random random;

        public static List<Player> infected;

        public static List<Player> notInfected;

        static readonly Timer printMapRating;

        static readonly Timer printMapAuthor;

        public static volatile bool phase1holder;

        public static volatile bool phase2holder;

        public static volatile bool nextMap;

        public static int time;

        public static int time2;

        public static int stime;

        public static int stime2;

        public static Level currentInfectionLevel;

        static string currentMap;

        static string lastmap;

        public static int mapNumber;

        public static int selectedMapIndex;

        static int pcount;

        static List<Player> playersToBeMoved;

        public static bool skipVoting;

        public static InfectionMaps.InfectionMap currentInfectionMap;

        static readonly object roundEndedLock;

        static readonly V1.MessageOptions cpeBeenInfectedOptions;

        static readonly V1.MessageOptions options;

        static InfectionSystem()
        {
            roundStopwatch = new Stopwatch();
            playersOnStartCount = 0;
            MainLoop = new Timer(500.0);
            TimeDisplay = new Timer(1000.0);
            random = new Random();
            infected = new List<Player>();
            notInfected = new List<Player>();
            printMapRating = new Timer(25000.0);
            printMapAuthor = new Timer(3000.0);
            phase1holder = true;
            phase2holder = false;
            nextMap = false;
            time = 1;
            time2 = 8;
            stime = 1;
            stime2 = 8;
            selectedMapIndex = 1;
            pcount = 0;
            playersToBeMoved = new List<Player>();
            skipVoting = false;
            RoundTime = 8.0;
            roundEndedLock = new object();
            cpeBeenInfectedOptions = new V1.MessageOptions
            {
                MinDisplayTime = TimeSpan.FromSeconds(1.0),
                DisplayTime = TimeSpan.FromSeconds(4.0),
                MaxDelay = TimeSpan.FromSeconds(3.0)
            };
            options = new V1.MessageOptions
            {
                MinDisplayTime = TimeSpan.FromSeconds(1.0)
            };
            printMapRating.AutoReset = false;
            printMapRating.Elapsed += OnMapRatingTimerElapsed;
            printMapAuthor.AutoReset = false;
            printMapAuthor.Elapsed += OnMapAuthorTimerElapsed;
            TimeDisplay.Elapsed += InfectionUtils.RoundTimeManager;
            MainLoop.Elapsed += InfectionCore;
            AnnounceWinners += AnnounceWinnersDefault;
            PayReward += PayRewardDefault;
        }

        public static string NextZombie { get; set; }

        public static double RoundTime { get; set; }

        static event EventHandler<PayRewardEventArgs> payReward;

        public static event EventHandler<PayRewardEventArgs> PayReward
        {
            add
            {
                lock (roundEndedLock)
                {
                    payReward += value;
                }
            }
            remove
            {
                lock (roundEndedLock)
                {
                    payReward -= value;
                }
            }
        }

        public static event EventHandler<AnnounceWinnersEventArgs> AnnounceWinners;

        public static event EventHandler<RoundStartEventArgs> RoundStart;

        public static void OnRoundStart(object sender, RoundStartEventArgs e)
        {
            if (RoundStart != null)
            {
                RoundStart(sender, e);
            }
        }

        public static void OnPayReward(object sender, PayRewardEventArgs e)
        {
            if (payReward != null)
            {
                payReward(sender, e);
            }
        }

        public static void OnAnnounceWinners(object sender, AnnounceWinnersEventArgs e)
        {
            if (AnnounceWinners != null)
            {
                AnnounceWinners(sender, e);
            }
        }

        public static void AnnounceWinnersDefault(object sender, AnnounceWinnersEventArgs e)
        {
            try
            {
                var source = new List<Player>(e.NotInfected);
                int num = source.Count(p => p != null && p.level == e.CurrentInfectionLevel);
                if (num > 0)
                {
                    IEnumerable<Player> enumerable = source.OrderByDescending(p => (int)(p.ExtraData["winning_streak"] ?? 0));
                    if (GeneralSettings.All.ExperimentalMessages)
                    {
                        V1.MessageOptions messageOptions = new V1.MessageOptions();
                        messageOptions.MinDisplayTime = TimeSpan.FromSeconds(4.0);
                        messageOptions.MaxDelay = TimeSpan.FromSeconds(2.0);
                        V1.MessageOptions messageOptions2 = messageOptions;
                        e.CurrentInfectionLevel.ChatLevelCpe(V1.MessageType.Announcement, messageOptions2, "%2The winners are Humans");
                    }
                    Player.GlobalMessageLevel(e.CurrentInfectionLevel, "%2____ The winners are Humans ____");
                    StringBuilder stringBuilder = new StringBuilder();
                    foreach (Player item in enumerable)
                    {
                        if (item != null && item.level == e.CurrentInfectionLevel)
                        {
                            stringBuilder.Append(item.StarsTag + "%a" + item.PublicName);
                            stringBuilder.Append(", ");
                        }
                    }
                    if (stringBuilder.Length >= 2)
                    {
                        stringBuilder.Remove(stringBuilder.Length - 2, 2);
                    }
                    Player.GlobalMessageLevel(e.CurrentInfectionLevel, stringBuilder.ToString());
                    return;
                }
                var source2 = new List<Player>(e.Infected);
                IEnumerable<IGrouping<int, Player>> enumerable2 = (from z in source2
                    where z.ExtraData.ContainsKey("kills") && (int)z.ExtraData["kills"] > 0
                    where z.level == e.CurrentInfectionLevel
                    group z by (int)z.ExtraData["kills"]
                    into g
                    orderby g.Key descending
                    select g).Take(3);
                int num2 = 0;
                StringBuilder stringBuilder2 = new StringBuilder();
                foreach (IGrouping<int, Player> item2 in enumerable2)
                {
                    if (num2 < 3)
                    {
                        string[] array = item2.Select(z => z.PublicName).ToArray();
                        stringBuilder2.Append(string.Join(", ", array));
                        stringBuilder2.Append(" %7(" + item2.Key + (item2.Key == 1 ? " kill)%c " : " kills)%c "));
                        num2 += array.Length;
                        continue;
                    }
                    break;
                }
                if (GeneralSettings.All.ExperimentalMessages)
                {
                    V1.MessageOptions messageOptions3 = new V1.MessageOptions();
                    messageOptions3.MinDisplayTime = TimeSpan.FromSeconds(4.0);
                    messageOptions3.MaxDelay = TimeSpan.FromSeconds(2.0);
                    V1.MessageOptions messageOptions4 = messageOptions3;
                    e.CurrentInfectionLevel.ChatLevelCpe(V1.MessageType.Announcement, messageOptions4, "%4The winners are Zombies");
                }
                Player.GlobalMessageLevel(e.CurrentInfectionLevel, string.Concat(MCColor.DarkRed, "____ The winners are Zombies ____"));
                if (num2 > 0)
                {
                    Player.GlobalMessageLevel(e.CurrentInfectionLevel, string.Concat(MCColor.DarkRed, "Best zombies:"));
                    Player.GlobalMessageLevel(e.CurrentInfectionLevel, string.Concat(MCColor.Red, stringBuilder2.ToString()));
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        public static void PayRewardDefault(object sender, PayRewardEventArgs e)
        {
            int notInfectedCount = 0;
            notInfected.ForEach(delegate(Player p)
            {
                if (p != null && p.level == currentInfectionLevel)
                {
                    notInfectedCount++;
                }
            });
            if (notInfectedCount > 0)
            {
                notInfected.ForEach(delegate(Player p)
                {
                    if (p != null && p.level == currentInfectionLevel)
                    {
                        int num = DateTime.Now.Subtract(InfectionUtils.StartTime).Minutes;
                        if (num <= 0)
                        {
                            num = 1;
                        }
                        int num2 = (num - 1) * 20 + 30;
                        int num3 = 5;
                        int num4 = 50;
                        int num5 = num3 * num2 + num4;
                        int num6 = num5 / 10 > 50 ? 50 : num5 / 10;
                        Player.SendMessage(p, Server.DefaultColor + "You won as Human! Congratulations!");
                        Player.SendMessage(p, "Minutes alive: %a{0}", num, num2);
                        Player.SendMessage(p, "+%a{0}%s EXP! +%a{1}%s {2}!", num5, num6, Server.moneys);
                        p.PlayerExperienceOnZombie += num5;
                        p.WonAsHumanTimes++;
                        p.RoundsOnZombie++;
                        p.money += num6;
                    }
                });
                infected.ForEach(delegate(Player p)
                {
                    if (p != null && p.level == currentInfectionLevel)
                    {
                        int num7 = 0;
                        if (p.extraData.ContainsKey("infection_time"))
                        {
                            num7 = ((DateTime)p.extraData["infection_time"]).Subtract(InfectionUtils.StartTime).Minutes;
                        }
                        if (num7 < 0)
                        {
                            num7 = 0;
                        }
                        int num8 = num7 * 20;
                        int num9 = 3;
                        int num10 = 0;
                        if (p.extraData.ContainsKey("kills"))
                        {
                            num10 = (int)p.extraData["kills"];
                        }
                        int num11 = 0;
                        int num12 = 20;
                        int num13 = 40;
                        num11 = num10 <= 3 ? num10 * num12 : 3 * num12 + (num10 - 3) * num13;
                        int num14 = num11 + num8;
                        int num15 = num14 / 10 > 40 ? 40 : num14 / 10;
                        if (num7 > 4 || num10 > 0)
                        {
                            Player.SendMessage(p, Server.DefaultColor + "You lost, but you played well!");
                            Player.SendMessage(p, string.Format("Players infected: %4{0}%s Minutes alive: %4{1}", num10, num7));
                            Player.SendMessage(p, string.Format("+%4{0}%s EXP! +%4{1}%s {2}!", num14, num15, Server.moneys));
                            p.PlayerExperienceOnZombie += num14;
                            p.money += num15;
                            p.ZombifiedCount += num10;
                        }
                        p.RoundsOnZombie++;
                    }
                });
                return;
            }
            infected.ForEach(delegate(Player p)
            {
                if (p != null && p.level == currentInfectionLevel)
                {
                    int num16 = 0;
                    if (p.extraData.ContainsKey("infection_time"))
                    {
                        num16 = ((DateTime)p.extraData["infection_time"]).Subtract(InfectionUtils.StartTime).Minutes;
                    }
                    if (num16 < 3)
                    {
                        num16 = 0;
                    }
                    int num17 = num16 * 20;
                    int num18 = 3;
                    int num19 = 0;
                    if (p.extraData.ContainsKey("kills"))
                    {
                        num19 = (int)p.extraData["kills"];
                    }
                    int num20 = 0;
                    int num21 = 20;
                    int num22 = 40;
                    num20 = num19 <= 3 ? num19 * num21 : 3 * num21 + (num19 - 3) * num22;
                    int num23 = num20 * num18;
                    int num24 = 50;
                    int num25 = num23 + num17 + num24;
                    int num26 = num25 / 10 > 40 ? 40 : num25 / 10;
                    Player.SendMessage(p, Server.DefaultColor + "You won as Zombie! Congratulations!");
                    Player.SendMessage(p, string.Format("Players infected: %4{0}%s Minutes alive: %4{1}", num19, num16));
                    Player.SendMessage(p, string.Format("+%4{0}%s EXP! +%4{1}%s {2}!", num25, num26, Server.moneys));
                    p.PlayerExperienceOnZombie += num25;
                    p.WonAsZombieTimes++;
                    p.ZombifiedCount += num19;
                    p.RoundsOnZombie++;
                    p.money += num26;
                }
            });
        }

        public static void Start()
        {
            DirectoryUtil.CreateIfNotExists("infection/maps");
            Thread thread = new Thread(InfectionThread);
            thread.IsBackground = true;
            thread.Start();
        }

        public static void Stop()
        {
            nextMap = false;
            phase1holder = false;
            phase2holder = false;
            infectionInProgress = false;
            MainLoop.Stop();
            TimeDisplay.Stop();
        }

        static bool StartInfection()
        {
            Server.s.Log("Infection system starts...");
            ResetZombieStatusForAll();
            infected.Clear();
            notInfected.Clear();
            RoundStart.Trigger(null, new RoundStartEventArgs
            {
                CurrentInfectionLevel = currentInfectionLevel
            });
            SetAllAsNotInfected();
            Player.GlobalMessageLevel(currentInfectionLevel, MessagesManager.GetString("InfectionVirusWillBeReleased"));
            if (GeneralSettings.All.ExperimentalMessages && notInfected.Count >= InfectionSettings.All.MinimumPlayers)
            {
                V1.MessageOptions messageOptions = new V1.MessageOptions();
                messageOptions.MinDisplayTime = TimeSpan.FromSeconds(1.0);
                V1.MessageOptions messageOptions2 = messageOptions;
                Server.InfectionLevel.ChatLevelCpe(V1.MessageType.Announcement, messageOptions2,
                                                   string.Format(MessagesManager.GetString("InfectionRoundStartsIn"), currentInfectionMap.CountdownSeconds));
            }
            else
            {
                Player.GlobalMessageLevel(currentInfectionLevel, string.Format(MessagesManager.GetString("InfectionRoundStartsIn"), currentInfectionMap.CountdownSeconds));
            }
            Thread.Sleep(1000);
            CountDownToStart(currentInfectionMap.CountdownSeconds - 1);
            SetAllAsNotInfected();
            if (notInfected.Count < InfectionSettings.All.MinimumPlayers)
            {
                Server.s.Log("Not enough players to start the infection!");
                if (GeneralSettings.All.ExperimentalMessages)
                {
                    V1.MessageOptions messageOptions3 = new V1.MessageOptions();
                    messageOptions3.MinDisplayTime = TimeSpan.FromSeconds(3.0);
                    messageOptions3.DisplayTime = TimeSpan.FromSeconds(5.0);
                    messageOptions3.MaxDelay = TimeSpan.FromSeconds(5.0);
                    V1.MessageOptions messageOptions4 = messageOptions3;
                    currentInfectionLevel.ChatLevelCpe(V1.MessageType.Announcement, messageOptions4, "%dWaiting for one more player.");
                }
                else
                {
                    Player.GlobalMessageLevel(currentInfectionLevel, "%dWaiting for one more player.");
                }
                return false;
            }
            roundStopwatch.Reset();
            roundStopwatch.Start();
            playersOnStartCount = notInfected.Count;
            InfectionUtils.StartTime = DateTime.Now;
            Player player = null;
            if (NextZombie != null)
            {
                player = Player.Find(NextZombie);
            }
            NextZombie = null;
            if (player == null)
            {
                player = notInfected[random.Next(notInfected.Count)];
            }
            InfectPlayer(player);
            Player.GlobalMessageLevel(currentInfectionLevel, MessagesManager.GetString("InfectionRunHumans"));
            SetAndStartRoundTimer();
            MainLoop.Start();
            return true;
        }

        static void SetAndStartRoundTimer()
        {
            RoundTime = currentInfectionMap.RoundTimeMinutes;
            InfectionUtils.EndTime = DateTime.Now.AddMinutes(RoundTime);
            TimeDisplay.Start();
        }

        public static void InfectionCore(object sender, ElapsedEventArgs e)
        {
            UpdateNotInfectedList();
            infected.ForEach(delegate(Player player1)
            {
                Player.players.ForEach(delegate(Player player2)
                {
                    if (player2.level == currentInfectionLevel && player1 != player2 && !player2.isZombie && player1.IsTouching(player2) && !player2.IsRefree)
                    {
                        IncreaseKillCount(player1);
                        InfectPlayer(player2, player1);
                        DisplayHumansLeft();
                    }
                });
            });
            if (notInfected.Count <= 0)
            {
                EndInfectionRound();
            }
        }

        public static void DisplayHumansLeft()
        {
            int count = notInfected.Count;
            if (count != 0)
            {
                if (count <= 3)
                {
                    Player.GlobalMessageLevel(currentInfectionLevel,
                                              string.Format(MessagesManager.GetString("InfectionHumansLeft"), count) + " " +
                                              GetNamesFromPlayerList(notInfected, MCColor.DarkGreen));
                }
                else if (count % 5 == 0 || count <= 5)
                {
                    Player.GlobalMessageLevel(currentInfectionLevel, string.Format(MessagesManager.GetString("InfectionHumansLeft"), count));
                }
            }
        }

        static string GetNamesFromPlayerList(List<Player> list, MCColor color)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(color);
            list.ForEach(delegate(Player p) { sb.Append(p.PublicName).Append(", "); });
            if (sb.Length >= 2)
            {
                sb.Remove(sb.Length - 2, 2);
            }
            return sb.ToString();
        }

        public static void EndInfectionRound()
        {
            Player.GlobalMessage(MessagesManager.GetString("InfectionEnded"));
            TimeDisplay.Stop();
            MainLoop.Stop();
            roundStopwatch.Stop();
            SaveZombieRoundInfo(InfectionUtils.StartTime, currentInfectionMap, (int)roundStopwatch.Elapsed.TotalSeconds, playersOnStartCount,
                                infected.Count + notInfected.Count, notInfected.Count > 0 ? 1 : 0, notInfected.Count > 0 ? notInfected.Count : infected.Count);
            AnnounceAndAwardWinners();
            Thread.Sleep(2000);
            nextMap = true;
            phase2holder = false;
            phase1holder = false;
        }

        static void SaveZombieRoundInfo(DateTime startDate, InfectionMaps.InfectionMap map, int durationInSeconds, int playersCountOnStart, int playersCountOnEnd,
                                        int whoWon, int winnersCount)
        {
            try
            {
                DBInterface.ExecuteQuery(
                    "INSERT INTO ZombieRounds (DateTime, MapName, RoundTime, BuildingAllowed, PillaringAllowed, Duration, PlayersCountOnStart, PlayersCountOnEnd, WhoWon, WinnersCount)" +
                    string.Format(" VALUES ( '{0}', '{1}', {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9} )", startDate.ToString("yyyy-MM-dd HH:mm:ss"), map.Name,
                                  map.RoundTimeMinutes, map.IsBuildingAllowed ? 1 : 0, map.IsPillaringAllowed ? 1 : 0, durationInSeconds, playersCountOnStart,
                                  playersCountOnEnd, whoWon, winnersCount));
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        static void ResetZombieStatusForAll()
        {
            infected.ForEach(delegate(Player p) { RemoveZombieDataAndSkin(p); });
            infected.Clear();
            notInfected.ForEach(delegate(Player p) { RemoveZombieDataAndSkin(p); });
            notInfected.Clear();
        }

        static void AnnounceAndAwardWinners()
        {
            try
            {
                GiveStars();
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
            AnnounceWinnersEventArgs announceWinnersEventArgs = new AnnounceWinnersEventArgs();
            announceWinnersEventArgs.CurrentInfectionLevel = currentInfectionLevel;
            announceWinnersEventArgs.Infected = infected;
            announceWinnersEventArgs.NotInfected = notInfected;
            AnnounceWinners.Trigger(null, announceWinnersEventArgs);
            if (InfectionSettings.All.UsePlayerLevels)
            {
                PayRewardEventArgs payRewardEventArgs = new PayRewardEventArgs();
                payRewardEventArgs.CurrentInfectionLevel = currentInfectionLevel;
                payRewardEventArgs.Infected = infected;
                payRewardEventArgs.NotInfected = notInfected;
                payReward.Trigger(null, payRewardEventArgs);
            }
        }

        static void ResetHumanStars(Player p)
        {
            if (p.ExtraData.ContainsKey("winning_streak"))
            {
                p.ExtraData.Remove("winning_streak");
                p.StarsTag = "";
            }
        }

        static void GiveStars()
        {
            var list = new List<Player>(notInfected);
            var list2 = new List<Player>(infected);
            foreach (Player item in list)
            {
                if (item.ExtraData.ContainsKey("best_zombie_streak"))
                {
                    item.ExtraData.Remove("best_zombie_streak");
                }
                if (item.ExtraData.ContainsKey("winning_streak"))
                {
                    item.ExtraData["winning_streak"] = (int)item.ExtraData["winning_streak"] + 1;
                }
                else
                {
                    item.ExtraData["winning_streak"] = 1;
                }
                switch ((int)item.ExtraData["winning_streak"])
                {
                    case 1:
                        item.StarsTag = "%4*";
                        item.ExtraData["bronze_stars_count"] = (int)item.ExtraData["bronze_stars_count"] + 1;
                        break;
                    case 2:
                        item.StarsTag = "%7*";
                        item.ExtraData["silver_stars_count"] = (int)item.ExtraData["silver_stars_count"] + 1;
                        break;
                    default:
                        item.StarsTag = "%6*";
                        item.ExtraData["gold_stars_count"] = (int)item.ExtraData["gold_stars_count"] + 1;
                        break;
                }
            }
            foreach (Player item2 in list2)
            {
                if (item2.ExtraData.ContainsKey("winning_streak"))
                {
                    item2.ExtraData.Remove("winning_streak");
                    item2.StarsTag = "";
                }
            }
            IGrouping<int, Player> grouping = (from z in list2
                where z.ExtraData.ContainsKey("kills") && (int)z.ExtraData["kills"] > 0
                group z by (int)z.ExtraData["kills"]
                into z
                orderby z.Key descending
                select z).FirstOrDefault();
            if (grouping != null)
            {
                foreach (Player item3 in grouping)
                {
                    if (item3.ExtraData.ContainsKey("best_zombie_streak"))
                    {
                        item3.ExtraData["best_zombie_streak"] = (int)item3.ExtraData["best_zombie_streak"] + 1;
                    }
                    else
                    {
                        item3.ExtraData["best_zombie_streak"] = 1;
                    }
                    item3.StarsTag = "%2*";
                    item3.ExtraData["rotten_stars_count"] = (int)item3.ExtraData["rotten_stars_count"] + 1;
                }
            }
            IEnumerable<Player> enumerable = grouping == null ? list2 : list2.Except(grouping);
            foreach (Player item4 in enumerable)
            {
                if (item4.ExtraData.ContainsKey("best_zombie_streak"))
                {
                    item4.ExtraData.Remove("best_zombie_streak");
                    item4.StarsTag = "";
                }
            }
            foreach (Player item5 in list)
            {
                item5.SaveStarsCount();
            }
            foreach (Player item6 in list2)
            {
                item6.SaveStarsCount();
            }
        }

        static void InfectPlayer(Player infectee, Player zombie = null, AnnoncementType annoncementType = AnnoncementType.Public)
        {
            if (!infectee.extraData.ContainsKey("true_color"))
            {
                infectee.extraData.Add("true_color", infectee.color);
                infectee.extraData.Add("kills", 0);
                if (zombie != null)
                {
                    infectee.extraData.Add("infection_time", DateTime.Now);
                }
            }
            Player.GlobalDie(infectee, self: false);
            infectee.isZombie = true;
            if (InfectionSettings.All.BrokenNeckZombies)
            {
                infectee.flipHead = true;
            }
            Player.players.ForEachSync(delegate(Player p)
            {
                if (!p.mapLoading && p.level == infectee.level && !infectee.hidden && p != infectee)
                {
                    p.SendSpawn(infectee.id, p.ShowAlias ? "&c" + InfectionSettings.All.ZombieAlias : "&c" + infectee.PublicName, infectee.ModelName, infectee.pos[0],
                                infectee.pos[1], infectee.pos[2], infectee.rot[0], infectee.rot[1]);
                    if (p.IsCpeSupported)
                    {
                        p.SendRaw(new Packets().MakeChangeModel(infectee.id, Model.Zombie));
                    }
                }
            });
            infected.Add(infectee);
            notInfected.Remove(infectee);
            infectee.ExtraData["infection_time"] = DateTime.Now;
            if (zombie != null)
            {
                Player.GlobalMessageLevel(currentInfectionLevel, string.Format(MessagesManager.GetString("InfectionWasBitten"), infectee.PublicName, zombie.PublicName));
                infectee.ExtraData["infector"] = zombie.PublicName;
                DisplayBeenInfectedMsg(infectee);
            }
            else
            {
                switch (annoncementType)
                {
                    case AnnoncementType.Public:
                        Player.GlobalMessageLevel(currentInfectionLevel, string.Format(MessagesManager.GetString("InfectionWasInfected"), infectee.PublicName));
                        DisplayBeenInfectedMsg(infectee);
                        break;
                    case AnnoncementType.Personal:
                        Player.SendMessage(infectee, string.Format(MessagesManager.GetString("InfectionWasInfected"), infectee.PublicName));
                        DisplayBeenInfectedMsg(infectee);
                        break;
                }
            }
            ResetHumanStars(infectee);
        }

        static void DisplayBeenInfectedMsg(Player p)
        {
            if (GeneralSettings.All.ExperimentalMessages && p.Cpe.MessageTypes == 1)
            {
                V1.SendMessage(p, V1.MessageType.Announcement, cpeBeenInfectedOptions, "%c~ You have been infected! ~");
            }
            Player.SendMessage(p, "%c----------------------------");
            Player.SendMessage(p, "%c|     You have been infected!      |");
            Player.SendMessage(p, "%c----------------------------");
        }

        static void CountDownToStart(int seconds)
        {
            while (seconds > 0)
            {
                if (seconds == 1)
                {
                    if (GeneralSettings.All.ExperimentalMessages && notInfected.Count >= InfectionSettings.All.MinimumPlayers)
                    {
                        Server.InfectionLevel.ChatLevelCpe(V1.MessageType.Announcement, options,
                                                           string.Format(MessagesManager.GetString("InfectionRoundStartsIn"), seconds));
                    }
                    else
                    {
                        Server.InfectionLevel.ChatLevel(string.Format(MessagesManager.GetString("InfectionRoundStartsIn"), seconds));
                    }
                }
                else if (seconds <= 5)
                {
                    if (GeneralSettings.All.ExperimentalMessages && notInfected.Count >= InfectionSettings.All.MinimumPlayers)
                    {
                        Server.InfectionLevel.ChatLevelCpe(V1.MessageType.Announcement, options,
                                                           string.Format(MessagesManager.GetString("InfectionRoundStartsIn"), seconds));
                    }
                    else
                    {
                        Server.InfectionLevel.ChatLevel(string.Format(MessagesManager.GetString("InfectionRoundStartsIn"), seconds));
                    }
                }
                else if (seconds % 10 == 0 || seconds <= 5)
                {
                    if (GeneralSettings.All.ExperimentalMessages && notInfected.Count >= InfectionSettings.All.MinimumPlayers)
                    {
                        Server.InfectionLevel.ChatLevelCpe(V1.MessageType.Announcement, options,
                                                           string.Format(MessagesManager.GetString("InfectionRoundStartsIn"), seconds));
                    }
                    else
                    {
                        Player.GlobalMessageLevel(currentInfectionLevel, string.Format(MessagesManager.GetString("InfectionRoundStartsIn"), seconds));
                    }
                }
                seconds--;
                Thread.Sleep(1000);
            }
        }

        static void SetAllAsNotInfected()
        {
            Player.players.ForEachSync(delegate(Player pl)
            {
                if (pl.level == currentInfectionLevel && !pl.IsRefree && !notInfected.Contains(pl))
                {
                    pl.isZombie = false;
                    pl.flipHead = false;
                    notInfected.Add(pl);
                }
            });
        }

        static void UpdateNotInfectedList()
        {
            Player.players.ForEach(delegate(Player p)
            {
                if (p.level == currentInfectionLevel && (!p.isZombie || !infected.Contains(p)) && !notInfected.Contains(p) && !p.Loading && p.fullylogged && !p.IsRefree)
                {
                    InfectPlayer(p, null, AnnoncementType.Personal);
                    ShowInfectedToPlayer(p);
                }
            });
            notInfected.ForEach(delegate(Player pl)
            {
                if (!Player.players.Contains(pl) || pl.level != currentInfectionLevel)
                {
                    notInfected.Remove(pl);
                }
            });
            infected.ForEach(delegate(Player pl)
            {
                if (!Player.players.Contains(pl) || pl.level != currentInfectionLevel)
                {
                    if (infected.Count == 1)
                    {
                        infected.Remove(pl);
                        pl.isZombie = false;
                        InfectPlayer(notInfected[random.Next(notInfected.Count)]);
                    }
                    else
                    {
                        infected.Remove(pl);
                        pl.isZombie = false;
                    }
                }
            });
        }

        static void ShowInfectedToPlayer(Player p)
        {
            var infectedCopy = new List<Player>(infected);
            Player.players.ForEachSync(delegate(Player pl)
            {
                if (p != pl && infectedCopy.Contains(pl) && !pl.hidden)
                {
                    p.SendDie(pl.id);
                    p.SendSpawn(pl.id, "&c" + InfectionSettings.All.ZombieAlias, pl.ModelName, pl.pos[0], pl.pos[1], pl.pos[2], pl.rot[0], pl.rot[1]);
                    if (p.IsCpeSupported)
                    {
                        p.SendRaw(new Packets().MakeChangeModel(pl.id, Model.Zombie));
                    }
                }
            });
        }

        static void IncreaseKillCount(Player p)
        {
            if (p.extraData.ContainsKey("kills"))
            {
                p.extraData["kills"] = (int)p.extraData["kills"] + 1;
                if ((int)p.extraData["kills"] % 3 == 0)
                {
                    Player.GlobalMessageLevel(currentInfectionLevel,
                                              string.Format(MessagesManager.GetString("InfectionKillingSpree"), p.PublicName, p.extraData["kills"]));
                }
            }
            else
            {
                p.extraData.Add("kills", 1);
            }
        }

        public static void RemoveZombieDataAndSkin(Player player)
        {
            if (player.extraData.ContainsKey("true_color"))
            {
                player.color = (string)player.extraData["true_color"];
                player.extraData.Remove("true_color");
            }
            if (player.extraData.ContainsKey("kills"))
            {
                player.extraData.Remove("kills");
            }
            if (player.extraData.ContainsKey("infection_time"))
            {
                player.extraData.Remove("infection_time");
            }
            if (player.extraData.ContainsKey("infector"))
            {
                player.extraData.Remove("infector");
            }
            player.isZombie = false;
            player.flipHead = false;
            Player.GlobalDie(player, self: false);
            Player.GlobalSpawn(player);
        }

        public static void InfectionMapInitialization()
        {
            currentMap = InfectionMaps.infectionMaps[0].Name;
            currentInfectionMap = InfectionMaps.infectionMaps[0];
            Server.InfectionLevel = Level.Load(currentMap, 4, MapType.Zombie);
            Server.InfectionLevel.unload = false;
            Server.AddLevel(Server.InfectionLevel);
            currentInfectionLevel = Server.InfectionLevel;
        }

        static void InfectionThread()
        {
            infectionInProgress = true;
            while (infectionInProgress && InfectionMaps.infectionMaps.Count > 0)
            {
                while (phase1holder)
                {
                    if (time == 0)
                    {
                        if (currentInfectionMap.InfectionCommands.Count > 0)
                        {
                            new Thread(InfectionCommands.StartInfectionCommands).Start(currentInfectionMap);
                        }
                        phase1holder = false;
                        phase2holder = true;
                        Thread.Sleep(8000);
                        break;
                    }
                    time--;
                }
                while (phase2holder)
                {
                    if (!StartInfection())
                    {
                        phase1holder = true;
                        phase2holder = false;
                        time = stime;
                    }
                    phase2holder = false;
                }
                if (nextMap)
                {
                    nextMap = false;
                    phase1holder = true;
                    phase2holder = true;
                    time = stime;
                    time2 = stime2;
                    Thread.Sleep(10000);
                    if (!skipVoting && InfectionSettings.All.VotingSystem)
                    {
                        selectedMapIndex = InfectionUtils.Voting();
                        if (selectedMapIndex == -1)
                        {
                            Server.s.Log("Not enough infection maps for voting system to work. Voting turned off.");
                            InfectionSettings.All.VotingSystem = false;
                            if (!Server.CLI)
                            {
                                Window.thisWindow.UpdateProperties();
                            }
                            selectedMapIndex = 0;
                        }
                    }
                    skipVoting = false;
                    ResetZombieStatusForAll();
                    while (true)
                    {
                        if (selectedMapIndex < InfectionMaps.infectionMaps.Count)
                        {
                            if (currentMap == InfectionMaps.infectionMaps[selectedMapIndex].Name)
                            {
                                currentInfectionLevel.Unload(reload: true);
                                Command.all.Find("loadzombiemap").Use(null, currentMap);
                                Level infectionLevel = Server.InfectionLevel;
                                currentInfectionLevel = Level.Find(currentMap);
                                Server.InfectionLevel = currentInfectionLevel;
                                currentInfectionMap.IsPillaringAllowed = currentInfectionMap.IsBuildingAllowed && currentInfectionMap.IsPillaringAllowed;
                                currentInfectionLevel.IsPillaringAllowed = currentInfectionMap.IsPillaringAllowed;
                                MoveAllToNextMap(infectionLevel);
                            }
                            else
                            {
                                lastmap = currentMap;
                                currentMap = InfectionMaps.infectionMaps[selectedMapIndex].Name;
                                if (!Level.IsLevelLoaded(currentMap))
                                {
                                    Command.all.Find("loadzombiemap").Use(null, currentMap);
                                }
                                currentInfectionLevel = Level.Find(currentMap);
                                currentInfectionMap = InfectionMaps.infectionMaps[selectedMapIndex];
                                currentInfectionMap.IsPillaringAllowed = currentInfectionMap.IsBuildingAllowed && currentInfectionMap.IsPillaringAllowed;
                                currentInfectionLevel.IsPillaringAllowed = currentInfectionMap.IsPillaringAllowed;
                                Level infectionLevel2 = Server.InfectionLevel;
                                Server.InfectionLevel = currentInfectionLevel;
                                MoveAllToNextMap(infectionLevel2);
                                Command.all.Find("unload").Use(null, lastmap);
                            }
                            selectedMapIndex++;
                            break;
                        }
                        if (currentMap == InfectionMaps.infectionMaps[0].Name)
                        {
                            currentInfectionLevel.Unload(reload: true);
                            Command.all.Find("loadzombiemap").Use(null, currentMap);
                            currentInfectionLevel = Level.Find(currentMap);
                            Level infectionLevel3 = Server.InfectionLevel;
                            Server.InfectionLevel = currentInfectionLevel;
                            MoveAllToNextMap(infectionLevel3);
                            currentInfectionLevel = Level.Find(currentMap);
                            break;
                        }
                        selectedMapIndex = 0;
                    }
                    Server.s.Log("Current infection map: " + currentMap);
                    if (InfectionSettings.All.ShowMapRating)
                    {
                        printMapRating.Start();
                    }
                    if (InfectionSettings.All.ShowMapAuthor && !string.IsNullOrEmpty(currentInfectionMap.Author))
                    {
                        printMapAuthor.Start();
                    }
                }
                Server.pause = false;
                Thread.Sleep(1000);
            }
        }

        static void MoveAllToNextMapSeq(Level from)
        {
            Player.players.GetCopy().ForEach(delegate(Player p)
            {
                try
                {
                    if (p.level == from)
                    {
                        SendPlayerToMap(p, currentMap);
                    }
                }
                catch (Exception ex)
                {
                    Server.ErrorLog(ex);
                }
            });
        }

        static void MoveAllToNextMap(Level from)
        {
            MoveAllToNextMapSeq(from);
        }

        public static void SendPlayerToMap(Player p, string map)
        {
            p.SendToMap(Level.FindExact(map));
        }

        public static void SetMapIndex(int index)
        {
            selectedMapIndex = index;
        }

        static void OnMapRatingTimerElapsed(object source, ElapsedEventArgs e)
        {
            InfectionUtils.PrintMapRating();
        }

        static void OnMapAuthorTimerElapsed(object source, ElapsedEventArgs e)
        {
            InfectionUtils.PrintMapAuthor();
        }
    }
}