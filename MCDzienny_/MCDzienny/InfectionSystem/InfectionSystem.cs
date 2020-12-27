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
    // Token: 0x02000193 RID: 403
    public static class InfectionSystem
    {
        // Token: 0x040005EF RID: 1519
        private static volatile bool infectionInProgress;

        // Token: 0x040005F0 RID: 1520
        private static readonly Stopwatch roundStopwatch = new Stopwatch();

        // Token: 0x040005F1 RID: 1521
        private static int playersOnStartCount;

        // Token: 0x040005F2 RID: 1522
        public static Timer MainLoop = new Timer(500.0);

        // Token: 0x040005F3 RID: 1523
        public static Timer TimeDisplay = new Timer(1000.0);

        // Token: 0x040005F4 RID: 1524
        private static readonly Random random = new Random();

        // Token: 0x040005F5 RID: 1525
        public static List<Player> infected = new List<Player>();

        // Token: 0x040005F6 RID: 1526
        public static List<Player> notInfected = new List<Player>();

        // Token: 0x040005F7 RID: 1527
        private static readonly Timer printMapRating = new Timer(25000.0);

        // Token: 0x040005F8 RID: 1528
        private static readonly Timer printMapAuthor = new Timer(3000.0);

        // Token: 0x040005F9 RID: 1529
        public static volatile bool phase1holder = true;

        // Token: 0x040005FA RID: 1530
        public static volatile bool phase2holder;

        // Token: 0x040005FB RID: 1531
        public static volatile bool nextMap;

        // Token: 0x040005FC RID: 1532
        public static int time = 1;

        // Token: 0x040005FD RID: 1533
        public static int time2 = 8;

        // Token: 0x040005FE RID: 1534
        public static int stime = 1;

        // Token: 0x040005FF RID: 1535
        public static int stime2 = 8;

        // Token: 0x04000600 RID: 1536
        public static Level currentInfectionLevel;

        // Token: 0x04000601 RID: 1537
        private static string currentMap;

        // Token: 0x04000602 RID: 1538
        private static string lastmap;

        // Token: 0x04000603 RID: 1539
        public static int mapNumber;

        // Token: 0x04000604 RID: 1540
        public static int selectedMapIndex = 1;

        // Token: 0x04000605 RID: 1541
        private static int pcount = 0;

        // Token: 0x04000606 RID: 1542
        private static List<Player> playersToBeMoved = new List<Player>();

        // Token: 0x04000607 RID: 1543
        public static bool skipVoting;

        // Token: 0x04000608 RID: 1544
        public static InfectionMaps.InfectionMap currentInfectionMap;

        // Token: 0x04000609 RID: 1545
        private static double roundTime = 8.0;

        // Token: 0x0400060A RID: 1546
        private static readonly object roundEndedLock = new object();

        // Token: 0x0400060E RID: 1550
        private static readonly V1.MessageOptions cpeBeenInfectedOptions = new V1.MessageOptions
        {
            MinDisplayTime = TimeSpan.FromSeconds(1.0),
            DisplayTime = TimeSpan.FromSeconds(4.0),
            MaxDelay = TimeSpan.FromSeconds(3.0)
        };

        // Token: 0x0400060F RID: 1551
        private static readonly V1.MessageOptions options = new V1.MessageOptions
        {
            MinDisplayTime = TimeSpan.FromSeconds(1.0)
        };

        // Token: 0x06000BCF RID: 3023 RVA: 0x000446F0 File Offset: 0x000428F0
        static InfectionSystem()
        {
            printMapRating.AutoReset = false;
            printMapRating.Elapsed += OnMapRatingTimerElapsed;
            printMapAuthor.AutoReset = false;
            printMapAuthor.Elapsed += OnMapAuthorTimerElapsed;
            TimeDisplay.Elapsed += InfectionUtils.RoundTimeManager;
            MainLoop.Elapsed += InfectionCore;
            AnnounceWinners += AnnounceWinnersDefault;
            PayReward += PayRewardDefault;
        }

        // Token: 0x170004C4 RID: 1220
        // (get) Token: 0x06000BC5 RID: 3013 RVA: 0x00044538 File Offset: 0x00042738
        // (set) Token: 0x06000BC6 RID: 3014 RVA: 0x00044540 File Offset: 0x00042740
        public static string NextZombie { get; set; }

        // Token: 0x170004C5 RID: 1221
        // (get) Token: 0x06000BF2 RID: 3058 RVA: 0x000464D8 File Offset: 0x000446D8
        // (set) Token: 0x06000BF3 RID: 3059 RVA: 0x000464E0 File Offset: 0x000446E0
        public static double RoundTime
        {
            get { return roundTime; }
            set { roundTime = value; }
        }

        // Token: 0x1400000E RID: 14
        // (add) Token: 0x06000BC7 RID: 3015 RVA: 0x00044548 File Offset: 0x00042748
        // (remove) Token: 0x06000BC8 RID: 3016 RVA: 0x0004457C File Offset: 0x0004277C
        private static event EventHandler<PayRewardEventArgs> payReward;

        // Token: 0x1400000F RID: 15
        // (add) Token: 0x06000BC9 RID: 3017 RVA: 0x000445B0 File Offset: 0x000427B0
        // (remove) Token: 0x06000BCA RID: 3018 RVA: 0x000445E8 File Offset: 0x000427E8
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

        // Token: 0x14000010 RID: 16
        // (add) Token: 0x06000BCB RID: 3019 RVA: 0x00044620 File Offset: 0x00042820
        // (remove) Token: 0x06000BCC RID: 3020 RVA: 0x00044654 File Offset: 0x00042854
        public static event EventHandler<AnnounceWinnersEventArgs> AnnounceWinners;

        // Token: 0x14000011 RID: 17
        // (add) Token: 0x06000BCD RID: 3021 RVA: 0x00044688 File Offset: 0x00042888
        // (remove) Token: 0x06000BCE RID: 3022 RVA: 0x000446BC File Offset: 0x000428BC
        public static event EventHandler<RoundStartEventArgs> RoundStart;

        // Token: 0x06000BD0 RID: 3024 RVA: 0x000448D4 File Offset: 0x00042AD4
        public static void OnRoundStart(object sender, RoundStartEventArgs e)
        {
            var roundStart = RoundStart;
            if (roundStart != null) roundStart(sender, e);
        }

        // Token: 0x06000BD1 RID: 3025 RVA: 0x000448F4 File Offset: 0x00042AF4
        public static void OnPayReward(object sender, PayRewardEventArgs e)
        {
            var eventHandler = payReward;
            if (eventHandler != null) eventHandler(sender, e);
        }

        // Token: 0x06000BD2 RID: 3026 RVA: 0x00044914 File Offset: 0x00042B14
        public static void OnAnnounceWinners(object sender, AnnounceWinnersEventArgs e)
        {
            var announceWinners = AnnounceWinners;
            if (announceWinners != null) announceWinners(sender, e);
        }

        // Token: 0x06000BD3 RID: 3027 RVA: 0x00044934 File Offset: 0x00042B34
        public static void AnnounceWinnersDefault(object sender, AnnounceWinnersEventArgs e)
        {
            try
            {
                var source = new List<Player>(e.NotInfected);
                var num = source.Count(p => p != null && p.level == e.CurrentInfectionLevel);
                if (num > 0)
                {
                    IEnumerable<Player> enumerable = from p in source
                        orderby (int) (p.ExtraData["winning_streak"] ?? 0) descending
                        select p;
                    if (GeneralSettings.All.ExperimentalMessages)
                    {
                        var messageOptions = new V1.MessageOptions
                        {
                            MinDisplayTime = TimeSpan.FromSeconds(4.0),
                            MaxDelay = TimeSpan.FromSeconds(2.0)
                        };
                        e.CurrentInfectionLevel.ChatLevelCpe(V1.MessageType.Announcement, messageOptions,
                            "%2The winners are Humans");
                    }

                    Player.GlobalMessageLevel(e.CurrentInfectionLevel, "%2____ The winners are Humans ____");
                    var stringBuilder = new StringBuilder();
                    foreach (var player in enumerable)
                        if (player != null && player.level == e.CurrentInfectionLevel)
                        {
                            stringBuilder.Append(player.StarsTag + "%a" + player.PublicName);
                            stringBuilder.Append(", ");
                        }

                    if (stringBuilder.Length >= 2) stringBuilder.Remove(stringBuilder.Length - 2, 2);
                    Player.GlobalMessageLevel(e.CurrentInfectionLevel, stringBuilder.ToString());
                }
                else
                {
                    var source2 = new List<Player>(e.Infected);
                    var enumerable2 = (from z in source2
                        where z.ExtraData.ContainsKey("kills") && (int) z.ExtraData["kills"] > 0
                        where z.level == e.CurrentInfectionLevel
                        group z by (int) z.ExtraData["kills"]
                        into g
                        orderby g.Key descending
                        select g).Take(3);
                    var num2 = 0;
                    var stringBuilder2 = new StringBuilder();
                    foreach (var grouping in enumerable2)
                    {
                        if (num2 >= 3) break;
                        var array = (from z in grouping
                            select z.PublicName).ToArray();
                        stringBuilder2.Append(string.Join(", ", array));
                        stringBuilder2.Append(" %7(" + grouping.Key + (grouping.Key == 1 ? " kill)%c " : " kills)%c "));
                        num2 += array.Length;
                    }

                    if (GeneralSettings.All.ExperimentalMessages)
                    {
                        var messageOptions2 = new V1.MessageOptions
                        {
                            MinDisplayTime = TimeSpan.FromSeconds(4.0),
                            MaxDelay = TimeSpan.FromSeconds(2.0)
                        };
                        e.CurrentInfectionLevel.ChatLevelCpe(V1.MessageType.Announcement, messageOptions2,
                            "%4The winners are Zombies");
                    }

                    Player.GlobalMessageLevel(e.CurrentInfectionLevel,
                        MCColor.DarkRed + "____ The winners are Zombies ____");
                    if (num2 > 0)
                    {
                        Player.GlobalMessageLevel(e.CurrentInfectionLevel, MCColor.DarkRed + "Best zombies:");
                        Player.GlobalMessageLevel(e.CurrentInfectionLevel, MCColor.Red + stringBuilder2);
                    }
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        // Token: 0x06000BD4 RID: 3028 RVA: 0x00044D54 File Offset: 0x00042F54
        public static void PayRewardDefault(object sender, PayRewardEventArgs e)
        {
            var notInfectedCount = 0;
            notInfected.ForEach(delegate(Player p)
            {
                if (p != null && p.level == currentInfectionLevel) notInfectedCount++;
            });
            if (notInfectedCount > 0)
            {
                notInfected.ForEach(delegate(Player p)
                {
                    if (p != null && p.level == currentInfectionLevel)
                    {
                        var num = DateTime.Now.Subtract(InfectionUtils.StartTime).Minutes;
                        if (num <= 0) num = 1;
                        var num2 = (num - 1) * 20 + 30;
                        var num3 = 5;
                        var num4 = 50;
                        var num5 = num3 * num2 + num4;
                        var num6 = num5 / 10 > 50 ? 50 : num5 / 10;
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
                        var num = 0;
                        if (p.extraData.ContainsKey("infection_time"))
                            num = ((DateTime) p.extraData["infection_time"]).Subtract(InfectionUtils.StartTime).Minutes;
                        if (num < 0) num = 0;
                        var num2 = num * 20;
                        var num3 = 0;
                        if (p.extraData.ContainsKey("kills")) num3 = (int) p.extraData["kills"];
                        var num4 = 20;
                        var num5 = 40;
                        int num6;
                        if (num3 > 3)
                            num6 = 3 * num4 + (num3 - 3) * num5;
                        else
                            num6 = num3 * num4;
                        var num7 = num6 + num2;
                        var num8 = num7 / 10 > 40 ? 40 : num7 / 10;
                        if (num > 4 || num3 > 0)
                        {
                            Player.SendMessage(p, Server.DefaultColor + "You lost, but you played well!");
                            Player.SendMessage(p,
                                string.Format("Players infected: %4{0}%s Minutes alive: %4{1}", num3, num));
                            Player.SendMessage(p,
                                string.Format("+%4{0}%s EXP! +%4{1}%s {2}!", num7, num8, Server.moneys));
                            p.PlayerExperienceOnZombie += num7;
                            p.money += num8;
                            p.ZombifiedCount += num3;
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
                    var num = 0;
                    if (p.extraData.ContainsKey("infection_time"))
                        num = ((DateTime) p.extraData["infection_time"]).Subtract(InfectionUtils.StartTime).Minutes;
                    if (num < 3) num = 0;
                    var num2 = num * 20;
                    var num3 = 3;
                    var num4 = 0;
                    if (p.extraData.ContainsKey("kills")) num4 = (int) p.extraData["kills"];
                    var num5 = 20;
                    var num6 = 40;
                    int num7;
                    if (num4 > 3)
                        num7 = 3 * num5 + (num4 - 3) * num6;
                    else
                        num7 = num4 * num5;
                    var num8 = num7 * num3;
                    var num9 = 50;
                    var num10 = num8 + num2 + num9;
                    var num11 = num10 / 10 > 40 ? 40 : num10 / 10;
                    Player.SendMessage(p, Server.DefaultColor + "You won as Zombie! Congratulations!");
                    Player.SendMessage(p, string.Format("Players infected: %4{0}%s Minutes alive: %4{1}", num4, num));
                    Player.SendMessage(p, string.Format("+%4{0}%s EXP! +%4{1}%s {2}!", num10, num11, Server.moneys));
                    p.PlayerExperienceOnZombie += num10;
                    p.WonAsZombieTimes++;
                    p.ZombifiedCount += num4;
                    p.RoundsOnZombie++;
                    p.money += num11;
                }
            });
        }

        // Token: 0x06000BD5 RID: 3029 RVA: 0x00044E04 File Offset: 0x00043004
        public static void Start()
        {
            DirectoryUtil.CreateIfNotExists("infection/maps");
            new Thread(InfectionThread)
            {
                IsBackground = true
            }.Start();
        }

        // Token: 0x06000BD6 RID: 3030 RVA: 0x00044E3C File Offset: 0x0004303C
        public static void Stop()
        {
            nextMap = false;
            phase1holder = false;
            phase2holder = false;
            infectionInProgress = false;
            MainLoop.Stop();
            TimeDisplay.Stop();
        }

        // Token: 0x06000BD7 RID: 3031 RVA: 0x00044E74 File Offset: 0x00043074
        private static bool StartInfection()
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
                var messageOptions = new V1.MessageOptions
                {
                    MinDisplayTime = TimeSpan.FromSeconds(1.0)
                };
                Server.InfectionLevel.ChatLevelCpe(V1.MessageType.Announcement, messageOptions,
                    string.Format(MessagesManager.GetString("InfectionRoundStartsIn"),
                        currentInfectionMap.CountdownSeconds));
            }
            else
            {
                Player.GlobalMessageLevel(currentInfectionLevel,
                    string.Format(MessagesManager.GetString("InfectionRoundStartsIn"),
                        currentInfectionMap.CountdownSeconds));
            }

            Thread.Sleep(1000);
            CountDownToStart(currentInfectionMap.CountdownSeconds - 1);
            SetAllAsNotInfected();
            if (notInfected.Count < InfectionSettings.All.MinimumPlayers)
            {
                Server.s.Log("Not enough players to start the infection!");
                if (GeneralSettings.All.ExperimentalMessages)
                {
                    var messageOptions2 = new V1.MessageOptions
                    {
                        MinDisplayTime = TimeSpan.FromSeconds(3.0),
                        DisplayTime = TimeSpan.FromSeconds(5.0),
                        MaxDelay = TimeSpan.FromSeconds(5.0)
                    };
                    currentInfectionLevel.ChatLevelCpe(V1.MessageType.Announcement, messageOptions2,
                        "%dWaiting for one more player.");
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
            if (NextZombie != null) player = Player.Find(NextZombie);
            NextZombie = null;
            if (player == null) player = notInfected[random.Next(notInfected.Count)];
            InfectPlayer(player);
            Player.GlobalMessageLevel(currentInfectionLevel, MessagesManager.GetString("InfectionRunHumans"));
            SetAndStartRoundTimer();
            MainLoop.Start();
            return true;
        }

        // Token: 0x06000BD8 RID: 3032 RVA: 0x000450D0 File Offset: 0x000432D0
        private static void SetAndStartRoundTimer()
        {
            RoundTime = currentInfectionMap.RoundTimeMinutes;
            InfectionUtils.EndTime = DateTime.Now.AddMinutes(RoundTime);
            TimeDisplay.Start();
        }

        // Token: 0x06000BD9 RID: 3033 RVA: 0x00045110 File Offset: 0x00043310
        public static void InfectionCore(object sender, ElapsedEventArgs e)
        {
            UpdateNotInfectedList();
            infected.ForEach(delegate(Player player1)
            {
                Player.players.ForEach(delegate(Player player2)
                {
                    if (player2.level == currentInfectionLevel && player1 != player2 && !player2.isZombie &&
                        player1.IsTouching(player2) && !player2.IsRefree)
                    {
                        IncreaseKillCount(player1);
                        InfectPlayer(player2, player1);
                        DisplayHumansLeft();
                    }
                });
            });
            if (notInfected.Count <= 0) EndInfectionRound();
        }

        // Token: 0x06000BDA RID: 3034 RVA: 0x00045150 File Offset: 0x00043350
        public static void DisplayHumansLeft()
        {
            var count = notInfected.Count;
            if (count == 0) return;
            if (count <= 3)
            {
                Player.GlobalMessageLevel(currentInfectionLevel,
                    string.Format(MessagesManager.GetString("InfectionHumansLeft"), count) + " " +
                    GetNamesFromPlayerList(notInfected, MCColor.DarkGreen));
                return;
            }

            if (count % 5 == 0 || count <= 5)
                Player.GlobalMessageLevel(currentInfectionLevel,
                    string.Format(MessagesManager.GetString("InfectionHumansLeft"), count));
        }

        // Token: 0x06000BDB RID: 3035 RVA: 0x000451D4 File Offset: 0x000433D4
        private static string GetNamesFromPlayerList(List<Player> list, MCColor color)
        {
            var sb = new StringBuilder();
            sb.Append(color);
            list.ForEach(delegate(Player p) { sb.Append(p.PublicName).Append(", "); });
            if (sb.Length >= 2) sb.Remove(sb.Length - 2, 2);
            return sb.ToString();
        }

        // Token: 0x06000BDC RID: 3036 RVA: 0x0004524C File Offset: 0x0004344C
        public static void EndInfectionRound()
        {
            Player.GlobalMessage(MessagesManager.GetString("InfectionEnded"));
            TimeDisplay.Stop();
            MainLoop.Stop();
            roundStopwatch.Stop();
            SaveZombieRoundInfo(InfectionUtils.StartTime, currentInfectionMap,
                (int) roundStopwatch.Elapsed.TotalSeconds, playersOnStartCount, infected.Count + notInfected.Count,
                notInfected.Count > 0 ? 1 : 0, notInfected.Count > 0 ? notInfected.Count : infected.Count);
            AnnounceAndAwardWinners();
            Thread.Sleep(2000);
            nextMap = true;
            phase2holder = false;
            phase1holder = false;
        }

        // Token: 0x06000BDD RID: 3037 RVA: 0x00045320 File Offset: 0x00043520
        private static void SaveZombieRoundInfo(DateTime startDate, InfectionMaps.InfectionMap map,
            int durationInSeconds, int playersCountOnStart, int playersCountOnEnd, int whoWon, int winnersCount)
        {
            try
            {
                DBInterface.ExecuteQuery(
                    "INSERT INTO ZombieRounds (DateTime, MapName, RoundTime, BuildingAllowed, PillaringAllowed, Duration, PlayersCountOnStart, PlayersCountOnEnd, WhoWon, WinnersCount)" +
                    string.Format(" VALUES ( '{0}', '{1}', {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9} )",
                        startDate.ToString("yyyy-MM-dd HH:mm:ss"), map.Name, map.RoundTimeMinutes,
                        map.IsBuildingAllowed ? 1 : 0, map.IsPillaringAllowed ? 1 : 0, durationInSeconds,
                        playersCountOnStart, playersCountOnEnd, whoWon, winnersCount));
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        // Token: 0x06000BDE RID: 3038 RVA: 0x000453EC File Offset: 0x000435EC
        private static void ResetZombieStatusForAll()
        {
            infected.ForEach(delegate(Player p) { RemoveZombieDataAndSkin(p); });
            infected.Clear();
            notInfected.ForEach(delegate(Player p) { RemoveZombieDataAndSkin(p); });
            notInfected.Clear();
        }

        // Token: 0x06000BDF RID: 3039 RVA: 0x0004545C File Offset: 0x0004365C
        private static void AnnounceAndAwardWinners()
        {
            try
            {
                GiveStars();
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }

            var announceWinnersEventArgs = new AnnounceWinnersEventArgs();
            announceWinnersEventArgs.CurrentInfectionLevel = currentInfectionLevel;
            announceWinnersEventArgs.Infected = infected;
            announceWinnersEventArgs.NotInfected = notInfected;
            AnnounceWinners.Trigger(null, announceWinnersEventArgs);
            if (!InfectionSettings.All.UsePlayerLevels) return;
            var payRewardEventArgs = new PayRewardEventArgs();
            payRewardEventArgs.CurrentInfectionLevel = currentInfectionLevel;
            payRewardEventArgs.Infected = infected;
            payRewardEventArgs.NotInfected = notInfected;
            payReward.Trigger(null, payRewardEventArgs);
        }

        // Token: 0x06000BE0 RID: 3040 RVA: 0x000454FC File Offset: 0x000436FC
        private static void ResetHumanStars(Player p)
        {
            if (p.ExtraData.ContainsKey("winning_streak"))
            {
                p.ExtraData.Remove("winning_streak");
                p.StarsTag = "";
            }
        }

        // Token: 0x06000BE1 RID: 3041 RVA: 0x0004552C File Offset: 0x0004372C
        private static void GiveStars()
        {
            var list = new List<Player>(notInfected);
            var list2 = new List<Player>(infected);
            foreach (var player in list)
            {
                if (player.ExtraData.ContainsKey("best_zombie_streak")) player.ExtraData.Remove("best_zombie_streak");
                if (player.ExtraData.ContainsKey("winning_streak"))
                    player.ExtraData["winning_streak"] = (int) player.ExtraData["winning_streak"] + 1;
                else
                    player.ExtraData["winning_streak"] = 1;
                switch ((int) player.ExtraData["winning_streak"])
                {
                    case 1:
                        player.StarsTag = "%4*";
                        player.ExtraData["bronze_stars_count"] = (int) player.ExtraData["bronze_stars_count"] + 1;
                        break;
                    case 2:
                        player.StarsTag = "%7*";
                        player.ExtraData["silver_stars_count"] = (int) player.ExtraData["silver_stars_count"] + 1;
                        break;
                    default:
                        player.StarsTag = "%6*";
                        player.ExtraData["gold_stars_count"] = (int) player.ExtraData["gold_stars_count"] + 1;
                        break;
                }
            }

            foreach (var player2 in list2)
                if (player2.ExtraData.ContainsKey("winning_streak"))
                {
                    player2.ExtraData.Remove("winning_streak");
                    player2.StarsTag = "";
                }

            var grouping = (from z in list2
                where z.ExtraData.ContainsKey("kills") && (int) z.ExtraData["kills"] > 0
                group z by (int) z.ExtraData["kills"]
                into z
                orderby z.Key descending
                select z).FirstOrDefault();
            if (grouping != null)
                foreach (var player3 in grouping)
                {
                    if (player3.ExtraData.ContainsKey("best_zombie_streak"))
                        player3.ExtraData["best_zombie_streak"] = (int) player3.ExtraData["best_zombie_streak"] + 1;
                    else
                        player3.ExtraData["best_zombie_streak"] = 1;
                    player3.StarsTag = "%2*";
                    player3.ExtraData["rotten_stars_count"] = (int) player3.ExtraData["rotten_stars_count"] + 1;
                }

            IEnumerable<Player> enumerable;
            if (grouping != null)
                enumerable = list2.Except(grouping);
            else
                enumerable = list2;
            foreach (var player4 in enumerable)
                if (player4.ExtraData.ContainsKey("best_zombie_streak"))
                {
                    player4.ExtraData.Remove("best_zombie_streak");
                    player4.StarsTag = "";
                }

            foreach (var player5 in list) player5.SaveStarsCount();
            foreach (var player6 in list2) player6.SaveStarsCount();
        }

        // Token: 0x06000BE2 RID: 3042 RVA: 0x000459C8 File Offset: 0x00043BC8
        private static void InfectPlayer(Player infectee, Player zombie = null,
            AnnoncementType annoncementType = AnnoncementType.Public)
        {
            if (!infectee.extraData.ContainsKey("true_color"))
            {
                infectee.extraData.Add("true_color", infectee.color);
                infectee.extraData.Add("kills", 0);
                if (zombie != null) infectee.extraData.Add("infection_time", DateTime.Now);
            }

            Player.GlobalDie(infectee, false);
            infectee.isZombie = true;
            if (InfectionSettings.All.BrokenNeckZombies) infectee.flipHead = true;
            Player.players.ForEachSync(delegate(Player p)
            {
                if (p.mapLoading) return;
                if (p.level != infectee.level || infectee.hidden) return;
                if (p != infectee)
                {
                    p.SendSpawn(infectee.id,
                        p.ShowAlias ? "&c" + InfectionSettings.All.ZombieAlias : "&c" + infectee.PublicName,
                        infectee.ModelName, infectee.pos[0], infectee.pos[1], infectee.pos[2], infectee.rot[0],
                        infectee.rot[1]);
                    if (p.IsCpeSupported) p.SendRaw(new Packets().MakeChangeModel(infectee.id, Model.Zombie));
                }
            });
            infected.Add(infectee);
            notInfected.Remove(infectee);
            infectee.ExtraData["infection_time"] = DateTime.Now;
            if (zombie != null)
            {
                Player.GlobalMessageLevel(currentInfectionLevel,
                    string.Format(MessagesManager.GetString("InfectionWasBitten"), infectee.PublicName,
                        zombie.PublicName));
                infectee.ExtraData["infector"] = zombie.PublicName;
                DisplayBeenInfectedMsg(infectee);
            }
            else
            {
                switch (annoncementType)
                {
                    case AnnoncementType.Personal:
                        Player.SendMessage(infectee,
                            string.Format(MessagesManager.GetString("InfectionWasInfected"), infectee.PublicName));
                        DisplayBeenInfectedMsg(infectee);
                        break;
                    case AnnoncementType.Public:
                        Player.GlobalMessageLevel(currentInfectionLevel,
                            string.Format(MessagesManager.GetString("InfectionWasInfected"), infectee.PublicName));
                        DisplayBeenInfectedMsg(infectee);
                        break;
                }
            }

            ResetHumanStars(infectee);
        }

        // Token: 0x06000BE3 RID: 3043 RVA: 0x00045BB4 File Offset: 0x00043DB4
        private static void DisplayBeenInfectedMsg(Player p)
        {
            if (GeneralSettings.All.ExperimentalMessages && p.Cpe.MessageTypes == 1)
                V1.SendMessage(p, V1.MessageType.Announcement, cpeBeenInfectedOptions, "%c~ You have been infected! ~");
            Player.SendMessage(p, "%c----------------------------");
            Player.SendMessage(p, "%c|     You have been infected!      |");
            Player.SendMessage(p, "%c----------------------------");
        }

        // Token: 0x06000BE4 RID: 3044 RVA: 0x00045C10 File Offset: 0x00043E10
        private static void CountDownToStart(int seconds)
        {
            while (seconds > 0)
            {
                if (seconds == 1)
                {
                    if (GeneralSettings.All.ExperimentalMessages &&
                        notInfected.Count >= InfectionSettings.All.MinimumPlayers)
                        Server.InfectionLevel.ChatLevelCpe(V1.MessageType.Announcement, options,
                            string.Format(MessagesManager.GetString("InfectionRoundStartsIn"), seconds));
                    else
                        Server.InfectionLevel.ChatLevel(
                            string.Format(MessagesManager.GetString("InfectionRoundStartsIn"), seconds));
                }
                else if (seconds <= 5)
                {
                    if (GeneralSettings.All.ExperimentalMessages &&
                        notInfected.Count >= InfectionSettings.All.MinimumPlayers)
                        Server.InfectionLevel.ChatLevelCpe(V1.MessageType.Announcement, options,
                            string.Format(MessagesManager.GetString("InfectionRoundStartsIn"), seconds));
                    else
                        Server.InfectionLevel.ChatLevel(
                            string.Format(MessagesManager.GetString("InfectionRoundStartsIn"), seconds));
                }
                else if (seconds % 10 == 0 || seconds <= 5)
                {
                    if (GeneralSettings.All.ExperimentalMessages &&
                        notInfected.Count >= InfectionSettings.All.MinimumPlayers)
                        Server.InfectionLevel.ChatLevelCpe(V1.MessageType.Announcement, options,
                            string.Format(MessagesManager.GetString("InfectionRoundStartsIn"), seconds));
                    else
                        Player.GlobalMessageLevel(currentInfectionLevel,
                            string.Format(MessagesManager.GetString("InfectionRoundStartsIn"), seconds));
                }

                seconds--;
                Thread.Sleep(1000);
            }
        }

        // Token: 0x06000BE5 RID: 3045 RVA: 0x00045D90 File Offset: 0x00043F90
        private static void SetAllAsNotInfected()
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

        // Token: 0x06000BE6 RID: 3046 RVA: 0x00045DBC File Offset: 0x00043FBC
        private static void UpdateNotInfectedList()
        {
            Player.players.ForEach(delegate(Player p)
            {
                if (p.level == currentInfectionLevel && (!p.isZombie || !infected.Contains(p)) &&
                    !notInfected.Contains(p) && !p.Loading && p.fullylogged && !p.IsRefree)
                {
                    InfectPlayer(p, null, AnnoncementType.Personal);
                    ShowInfectedToPlayer(p);
                }
            });
            notInfected.ForEach(delegate(Player pl)
            {
                if (!Player.players.Contains(pl) || pl.level != currentInfectionLevel) notInfected.Remove(pl);
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
                        return;
                    }

                    infected.Remove(pl);
                    pl.isZombie = false;
                }
            });
        }

        // Token: 0x06000BE7 RID: 3047 RVA: 0x00045E40 File Offset: 0x00044040
        private static void ShowInfectedToPlayer(Player p)
        {
            var infectedCopy = new List<Player>(infected);
            Player.players.ForEachSync(delegate(Player pl)
            {
                if (p != pl && infectedCopy.Contains(pl) && !pl.hidden)
                {
                    p.SendDie(pl.id);
                    p.SendSpawn(pl.id, "&c" + InfectionSettings.All.ZombieAlias, pl.ModelName, pl.pos[0], pl.pos[1],
                        pl.pos[2], pl.rot[0], pl.rot[1]);
                    if (p.IsCpeSupported) p.SendRaw(new Packets().MakeChangeModel(pl.id, Model.Zombie));
                }
            });
        }

        // Token: 0x06000BE8 RID: 3048 RVA: 0x00045E80 File Offset: 0x00044080
        private static void IncreaseKillCount(Player p)
        {
            if (p.extraData.ContainsKey("kills"))
            {
                p.extraData["kills"] = (int) p.extraData["kills"] + 1;
                if ((int) p.extraData["kills"] % 3 == 0)
                    Player.GlobalMessageLevel(currentInfectionLevel,
                        string.Format(MessagesManager.GetString("InfectionKillingSpree"), p.PublicName,
                            p.extraData["kills"]));
            }
            else
            {
                p.extraData.Add("kills", 1);
            }
        }

        // Token: 0x06000BE9 RID: 3049 RVA: 0x00045F2C File Offset: 0x0004412C
        public static void RemoveZombieDataAndSkin(Player player)
        {
            if (player.extraData.ContainsKey("true_color"))
            {
                player.color = (string) player.extraData["true_color"];
                player.extraData.Remove("true_color");
            }

            if (player.extraData.ContainsKey("kills")) player.extraData.Remove("kills");
            if (player.extraData.ContainsKey("infection_time")) player.extraData.Remove("infection_time");
            if (player.extraData.ContainsKey("infector")) player.extraData.Remove("infector");
            player.isZombie = false;
            player.flipHead = false;
            Player.GlobalDie(player, false);
            Player.GlobalSpawn(player);
        }

        // Token: 0x06000BEA RID: 3050 RVA: 0x00045FFC File Offset: 0x000441FC
        public static void InfectionMapInitialization()
        {
            currentMap = InfectionMaps.infectionMaps[0].Name;
            currentInfectionMap = InfectionMaps.infectionMaps[0];
            Server.InfectionLevel = Level.Load(currentMap, 4, MapType.Zombie);
            Server.InfectionLevel.unload = false;
            Server.AddLevel(Server.InfectionLevel);
            currentInfectionLevel = Server.InfectionLevel;
        }

        // Token: 0x06000BEB RID: 3051 RVA: 0x00046060 File Offset: 0x00044260
        private static void InfectionThread()
        {
            infectionInProgress = true;
            while (infectionInProgress && InfectionMaps.infectionMaps.Count > 0)
            {
                while (phase1holder)
                {
                    if (time == 0)
                    {
                        if (currentInfectionMap.InfectionCommands.Count > 0)
                            new Thread(InfectionCommands.StartInfectionCommands).Start(currentInfectionMap);
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
                            if (!Server.CLI) Window.thisWindow.UpdateProperties();
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
                                currentInfectionLevel.Unload(true);
                                Command.all.Find("loadzombiemap").Use(null, currentMap);
                                var infectionLevel = Server.InfectionLevel;
                                currentInfectionLevel = Level.Find(currentMap);
                                Server.InfectionLevel = currentInfectionLevel;
                                currentInfectionMap.IsPillaringAllowed = currentInfectionMap.IsBuildingAllowed &&
                                                                         currentInfectionMap.IsPillaringAllowed;
                                currentInfectionLevel.IsPillaringAllowed = currentInfectionMap.IsPillaringAllowed;
                                MoveAllToNextMap(infectionLevel);
                            }
                            else
                            {
                                lastmap = currentMap;
                                currentMap = InfectionMaps.infectionMaps[selectedMapIndex].Name;
                                if (!Level.IsLevelLoaded(currentMap))
                                    Command.all.Find("loadzombiemap").Use(null, currentMap);
                                currentInfectionLevel = Level.Find(currentMap);
                                currentInfectionMap = InfectionMaps.infectionMaps[selectedMapIndex];
                                currentInfectionMap.IsPillaringAllowed = currentInfectionMap.IsBuildingAllowed &&
                                                                         currentInfectionMap.IsPillaringAllowed;
                                currentInfectionLevel.IsPillaringAllowed = currentInfectionMap.IsPillaringAllowed;
                                var infectionLevel2 = Server.InfectionLevel;
                                Server.InfectionLevel = currentInfectionLevel;
                                MoveAllToNextMap(infectionLevel2);
                                Command.all.Find("unload").Use(null, lastmap);
                            }

                            selectedMapIndex++;
                            break;
                        }

                        if (currentMap == InfectionMaps.infectionMaps[0].Name)
                        {
                            currentInfectionLevel.Unload(true);
                            Command.all.Find("loadzombiemap").Use(null, currentMap);
                            currentInfectionLevel = Level.Find(currentMap);
                            var infectionLevel3 = Server.InfectionLevel;
                            Server.InfectionLevel = currentInfectionLevel;
                            MoveAllToNextMap(infectionLevel3);
                            currentInfectionLevel = Level.Find(currentMap);
                            break;
                        }

                        selectedMapIndex = 0;
                    }

                    Server.s.Log("Current infection map: " + currentMap);
                    if (InfectionSettings.All.ShowMapRating) printMapRating.Start();
                    if (InfectionSettings.All.ShowMapAuthor && !string.IsNullOrEmpty(currentInfectionMap.Author))
                        printMapAuthor.Start();
                }

                Server.pause = false;
                Thread.Sleep(1000);
            }
        }

        // Token: 0x06000BEC RID: 3052 RVA: 0x00046464 File Offset: 0x00044664
        private static void MoveAllToNextMapSeq(Level from)
        {
            Player.players.GetCopy().ForEach(delegate(Player p)
            {
                try
                {
                    if (p.level == from) SendPlayerToMap(p, currentMap);
                }
                catch (Exception ex)
                {
                    Server.ErrorLog(ex);
                }
            });
        }

        // Token: 0x06000BED RID: 3053 RVA: 0x0004649C File Offset: 0x0004469C
        private static void MoveAllToNextMap(Level from)
        {
            MoveAllToNextMapSeq(from);
        }

        // Token: 0x06000BEE RID: 3054 RVA: 0x000464B0 File Offset: 0x000446B0
        public static void SendPlayerToMap(Player p, string map)
        {
            p.SendToMap(Level.FindExact(map));
        }

        // Token: 0x06000BEF RID: 3055 RVA: 0x000464C0 File Offset: 0x000446C0
        public static void SetMapIndex(int index)
        {
            selectedMapIndex = index;
        }

        // Token: 0x06000BF0 RID: 3056 RVA: 0x000464C8 File Offset: 0x000446C8
        private static void OnMapRatingTimerElapsed(object source, ElapsedEventArgs e)
        {
            InfectionUtils.PrintMapRating();
        }

        // Token: 0x06000BF1 RID: 3057 RVA: 0x000464D0 File Offset: 0x000446D0
        private static void OnMapAuthorTimerElapsed(object source, ElapsedEventArgs e)
        {
            InfectionUtils.PrintMapAuthor();
        }
    }
}