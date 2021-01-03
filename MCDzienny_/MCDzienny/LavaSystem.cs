using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using System.Xml;
using MCDzienny.Gui;
using MCDzienny.Misc;
using MCDzienny.MultiMessages;
using MCDzienny.Settings;
using Timer = System.Timers.Timer;

namespace MCDzienny
{
    // Token: 0x0200030E RID: 782
    public class LavaSystem
    {
        // Token: 0x02000312 RID: 786
        // (Invoke) Token: 0x060016CE RID: 5838
        public delegate void CountScoreDelegate(Player p, int airBlocksCount);

        // Token: 0x02000313 RID: 787
        // (Invoke) Token: 0x060016D2 RID: 5842
        public delegate void PayRewardDelegate(Player p, int rewardDifference);

        // Token: 0x04000B00 RID: 2816
        public static bool waterFlood;

        // Token: 0x04000B01 RID: 2817
        public static bool skipVoting;

        // Token: 0x04000B02 RID: 2818
        private static int pcount;

        // Token: 0x04000B03 RID: 2819
        public static byte startingBlock = 194;

        // Token: 0x04000B04 RID: 2820
        private static readonly Timer printMapRating = new Timer(25000.0);

        // Token: 0x04000B05 RID: 2821
        private static readonly Timer printLavaMood = new Timer(10000.0);

        // Token: 0x04000B06 RID: 2822
        private static readonly Timer printMapAuthor = new Timer(3000.0);

        // Token: 0x04000B07 RID: 2823
        public static int chanceCalm = 35;

        // Token: 0x04000B08 RID: 2824
        public static int chanceDisturbed = 45;

        // Token: 0x04000B09 RID: 2825
        public static int chanceFurious = 15;

        // Token: 0x04000B0A RID: 2826
        public static int chanceWild = 5;

        // Token: 0x04000B0B RID: 2827
        private static byte b;

        // Token: 0x04000B0C RID: 2828
        private static bool stopTest;

        // Token: 0x04000B0D RID: 2829
        public static int immortalTime = 20;

        // Token: 0x04000B0E RID: 2830
        private static int iOffset;

        // Token: 0x04000B0F RID: 2831
        public static int amountOffset = 2;

        // Token: 0x04000B10 RID: 2832
        private static bool earthQuake = false;

        // Token: 0x04000B11 RID: 2833
        public static int lavaUpDelay = 30000;

        // Token: 0x04000B12 RID: 2834
        private static int ii = 1;

        // Token: 0x04000B13 RID: 2835
        private static string currentMap;

        // Token: 0x04000B14 RID: 2836
        private static string lastmap;

        // Token: 0x04000B15 RID: 2837
        public static int stime = 14;

        // Token: 0x04000B16 RID: 2838
        public static int stime2 = 8;

        // Token: 0x04000B17 RID: 2839
        public static Level currentlvl;

        // Token: 0x04000B18 RID: 2840
        public static volatile bool phase1holder = true;

        // Token: 0x04000B19 RID: 2841
        public static volatile bool phase2holder;

        // Token: 0x04000B1A RID: 2842
        public static volatile bool nextMap;

        // Token: 0x04000B1B RID: 2843
        public static int time = 14;

        // Token: 0x04000B1C RID: 2844
        public static int time2 = 8;

        // Token: 0x04000B1D RID: 2845
        private static bool runonce = true;

        // Token: 0x04000B1E RID: 2846
        private static int counter;

        // Token: 0x04000B1F RID: 2847
        public static Random rand = new Random();

        // Token: 0x04000B20 RID: 2848
        public static Random moodRandom = new Random();

        // Token: 0x04000B21 RID: 2849
        public static bool protectionHolder;

        // Token: 0x04000B22 RID: 2850
        public static LavaMap currentLavaMap;

        // Token: 0x04000B23 RID: 2851
        private static int halfmap;

        // Token: 0x04000B24 RID: 2852
        private static readonly List<int> blocks = new List<int>();

        // Token: 0x04000B25 RID: 2853
        private static byte[] blocksArray = new byte[0];

        // Token: 0x04000B26 RID: 2854
        private static long count;

        // Token: 0x04000B27 RID: 2855
        private static long countBelow;

        // Token: 0x04000B28 RID: 2856
        public static int mapNumber;

        // Token: 0x04000B29 RID: 2857
        private static readonly int[] votes = new int[3];

        // Token: 0x04000B2A RID: 2858
        private static readonly Timer serverMessageTimer = new Timer(600000.0);

        // Token: 0x04000B2B RID: 2859
        private static readonly List<string> votersList = new List<string>();

        // Token: 0x04000B2C RID: 2860
        public static List<Winner> winnersList = new List<Winner>();

        // Token: 0x04000B2D RID: 2861
        public static List<string> deadplayers = new List<string>();

        // Token: 0x04000B2E RID: 2862
        public static List<lmaps> oldLavaMaps = new List<lmaps>();

        // Token: 0x04000B2F RID: 2863
        private static readonly List<bCheck> blockCheck = new List<bCheck>();

        // Token: 0x04000B32 RID: 2866
        private static Thread lavaloop;

        // Token: 0x04000B33 RID: 2867
        public static ResourceManager rm =
            new ResourceManager("MCDzienny.Ling.LavaSystem", Assembly.GetExecutingAssembly());

        // Token: 0x04000B34 RID: 2868
        public static List<LavaMap> lavaMaps = new List<LavaMap>();

        // Token: 0x04000B35 RID: 2869
        private static int posToInt;

        // Token: 0x0600168A RID: 5770 RVA: 0x000882AC File Offset: 0x000864AC
        static LavaSystem()
        {
            CountScore += CountScoreDefault;
            PayReward += PayRewardDefault;
        }

        // Token: 0x14000024 RID: 36
        // (add) Token: 0x06001686 RID: 5766 RVA: 0x000881DC File Offset: 0x000863DC
        // (remove) Token: 0x06001687 RID: 5767 RVA: 0x00088210 File Offset: 0x00086410
        public static event CountScoreDelegate CountScore;

        // Token: 0x14000025 RID: 37
        // (add) Token: 0x06001688 RID: 5768 RVA: 0x00088244 File Offset: 0x00086444
        // (remove) Token: 0x06001689 RID: 5769 RVA: 0x00088278 File Offset: 0x00086478
        public static event PayRewardDelegate PayReward;

        // Token: 0x0600168B RID: 5771 RVA: 0x00088458 File Offset: 0x00086658
        public static void Start()
        {
            if (!Directory.Exists("lava")) Directory.CreateDirectory("lava");
            if (!Directory.Exists("lava/maps")) Directory.CreateDirectory("lava/maps");
            if (lavaloop == null || !lavaloop.IsAlive)
            {
                lavaloop = new Thread(LavaThread);
                lavaloop.IsBackground = true;
                lavaloop.Start();
            }
        }

        // Token: 0x0600168C RID: 5772 RVA: 0x000884D4 File Offset: 0x000866D4
        public static void LavaMapInitialization()
        {
            currentMap = lavaMaps[0].Name;
            currentLavaMap = lavaMaps[0];
            time = lavaMaps[0].Phase1 == 0 ? time : lavaMaps[0].Phase1;
            Server.LavaLevel = Level.Load(currentMap, 3, true);
            Server.LavaLevel.unload = false;
            Server.LavaLevel.UnloadLock = true;
            Server.AddLevel(Server.LavaLevel);
            currentlvl = Server.LavaLevel;
        }

        // Token: 0x0600168D RID: 5773 RVA: 0x000885A0 File Offset: 0x000867A0
        internal static void UpdateTimeStatus()
        {
            if (Server.CLI) return;
            string timeToFlood;
            if (time >= 0)
                timeToFlood = time + 1 + "min";
            else
                timeToFlood = "started!";
            string timeToEnd;
            if (time2 >= 0 || time >= 0)
                timeToEnd = time + time2 + 2 + "min";
            else
                timeToEnd = "finished!";
            Window.thisWindow.toolStripStatusLabelLagometer.GetCurrentParent().Invoke(new Action(delegate
            {
                Window.thisWindow.toolStripStatusLabelRoundTime.Text =
                    string.Format("Flood starts in : {0}   Round ends in : {1}", timeToFlood, timeToEnd);
            }));
        }

        // Token: 0x0600168E RID: 5774 RVA: 0x00088674 File Offset: 0x00086874
        private static void LavaThread()
        {
            protectionHolder = false;
            while ((Server.mode == Mode.Lava || Server.mode == Mode.LavaFreebuild) && lavaMaps.Count > 0)
            {
                if (runonce)
                {
                    printMapRating.AutoReset = false;
                    printMapRating.Elapsed += OnMapRatingTimerElapsed;
                    printLavaMood.AutoReset = false;
                    printLavaMood.Elapsed += OnChangeMoodTimerElapsed;
                    printMapAuthor.AutoReset = false;
                    printMapAuthor.Elapsed += OnMapAuthorTimerElapsed;
                    runonce = false;
                }

                while (phase1holder)
                {
                    if (time == 0)
                    {
                        if (!waterFlood)
                            Player.GlobalMessageLevel(currentlvl, MessagesManager.GetString("LavaIsComing"));
                        else
                            Player.GlobalMessageLevel(currentlvl, MessagesManager.GetString("WaterIsComing"));
                        Server.s.Log("Look out! Lava is coming!!!");
                        new Thread(StartLavaFlood).Start(currentLavaMap);
                        if (currentLavaMap.LavaCommands.Count > 0)
                            new Thread(StartLavaCommandsPhase2).Start(currentLavaMap);
                        if (currentlvl.physics < 3) currentlvl.physics = 3;
                        var height = currentlvl.height;
                        phase1holder = false;
                        phase2holder = true;
                        time = -1;
                        UpdateTimeStatus();
                        if (LavaSettings.All.OverloadProtection && Player.players.Count > 5)
                            new Thread((ThreadStart) delegate
                            {
                                var interval = Server.updateTimer.Interval;
                                Server.updateTimer.Interval = 10000.0;
                                var num2 = 1;
                                protectionHolder = true;
                                while (protectionHolder)
                                {
                                    Thread.Sleep(30000);
                                    if (num2 > 9) protectionHolder = false;
                                    num2++;
                                }

                                Server.updateTimer.Interval = interval;
                            }).Start();
                        Thread.Sleep(8000);
                        break;
                    }

                    if (time == 1)
                    {
                        if (!waterFlood)
                        {
                            Player.GlobalMessageLevel(currentlvl,
                                string.Format(MessagesManager.GetString("MinuteToLavaFlood"), time));
                            Server.s.Log(time + " minute left to lava flood");
                        }
                        else
                        {
                            Player.GlobalMessageLevel(currentlvl,
                                string.Format(MessagesManager.GetString("MinuteToWaterFlood"), time));
                            Server.s.Log(time + " minute left to water flood");
                        }

                        if (LavaSettings.All.RandomLavaState) printLavaMood.Start();
                    }
                    else if (!waterFlood)
                    {
                        Player.GlobalMessageLevel(currentlvl,
                            string.Format(MessagesManager.GetString("MinutesToLavaFlood"), time));
                        Server.s.Log(time + " minutes left to lava flood");
                    }
                    else
                    {
                        Player.GlobalMessageLevel(currentlvl,
                            string.Format(MessagesManager.GetString("MinutesToWaterFlood"), time));
                        Server.s.Log(time + " minutes left to water flood");
                    }

                    time--;
                    UpdateTimeStatus();
                    for (var i = 0; i < 60; i++)
                    {
                        Thread.Sleep(1000);
                        if (!phase1holder) break;
                    }
                }

                while (phase2holder)
                {
                    if (time2 == 0)
                    {
                        Server.pause = true;
                        Player.GlobalMessageLevel(currentlvl, MessagesManager.GetString("SurvivorsCongratulations"));
                        Server.s.Log("Survivors, Congratulations!!!");
                        time2 = -1;
                        UpdateTimeStatus();
                        try
                        {
                            CheckWinners();
                            AnnounceWinners();
                        }
                        catch (Exception ex)
                        {
                            Server.ErrorLog(ex);
                        }

                        phase2holder = false;
                        nextMap = true;
                        if (skipVoting) break;
                        if (LavaSettings.All.VotingSystem)
                        {
                            Thread.Sleep(10000);
                            mapNumber = Voting();
                            if (mapNumber == -1)
                            {
                                Server.s.Log("Not enough maps for voting system to work. Voting turned off.");
                                LavaSettings.All.VotingSystem = false;
                                if (!Server.CLI) Window.thisWindow.UpdateProperties();
                                mapNumber = 0;
                            }
                        }
                        else
                        {
                            Thread.Sleep(18000);
                        }

                        break;
                    }

                    if (time2 == 1)
                    {
                        if (!waterFlood)
                        {
                            Player.GlobalMessageLevel(currentlvl,
                                string.Format(MessagesManager.GetString("MinuteToLavaFloodEnd"), time2));
                            Server.s.Log(time2 + " minute left to end of lava flood");
                        }
                        else
                        {
                            Player.GlobalMessageLevel(currentlvl,
                                string.Format(MessagesManager.GetString("MinuteToWaterFloodEnd"), time2));
                            Server.s.Log(time2 + " minute left to end of water flood");
                        }
                    }
                    else if (!waterFlood)
                    {
                        Player.GlobalMessageLevel(currentlvl,
                            string.Format(MessagesManager.GetString("MinutesToLavaFloodEnd"), time2));
                        Server.s.Log(time2 + " minutes left to end of lava flood");
                    }
                    else
                    {
                        Player.GlobalMessageLevel(currentlvl,
                            string.Format(MessagesManager.GetString("MinutesToWaterFloodEnd"), time2));
                        Server.s.Log(time2 + " minutes left to end of water flood");
                    }

                    time2--;
                    UpdateTimeStatus();
                    for (var j = 0; j < 60; j++)
                    {
                        Thread.Sleep(1000);
                        if (!phase2holder) break;
                    }
                }

                if (nextMap)
                {
                    nextMap = false;
                    phase1holder = true;
                    phase2holder = true;
                    time = stime;
                    time2 = stime2;
                    if (!skipVoting && LavaSettings.All.VotingSystem) ii = mapNumber;
                    skipVoting = false;
                    while (true)
                    {
                        if (ii < lavaMaps.Count)
                        {
                            if (currentMap == lavaMaps[ii].Name)
                            {
                                var playersToBeMoved2 = new List<Player>();
                                Player.players.ForEachSync(delegate(Player p)
                                {
                                    if (p.level == Server.LavaLevel) playersToBeMoved2.Add(p);
                                });
                                currentlvl.UnloadLock = false;
                                currentlvl.Unload(true);
                                Command.all.Find("loadlavamap").Use(null, currentMap);
                                currentlvl = Level.Find(currentMap);
                                Server.LavaLevel = currentlvl;
                                currentlvl.UnloadLock = true;
                                foreach (var item in playersToBeMoved2)
                                    try
                                    {
                                        Command.all.Find("goto").Use(item, currentMap);
                                    }
                                    catch
                                    {
                                    }
                            }
                            else
                            {
                                currentlvl.UnloadLock = false;
                                lastmap = currentMap;
                                currentMap = lavaMaps[ii].Name;
                                if (!IsMapLoaded(currentMap)) Command.all.Find("loadlavamap").Use(null, currentMap);
                                currentlvl = Level.Find(currentMap);
                                currentLavaMap = lavaMaps[ii];
                                time = lavaMaps[ii].Phase1 <= 0 ? time : lavaMaps[ii].Phase1;
                                time2 = lavaMaps[ii].Phase2 <= 0 ? time2 : lavaMaps[ii].Phase2;
                                var playersToBeMoved = new List<Player>();
                                Player.players.ForEachSync(delegate(Player p)
                                {
                                    if (p.level == Server.LavaLevel) playersToBeMoved.Add(p);
                                });
                                Server.LavaLevel = currentlvl;
                                currentlvl.UnloadLock = true;
                                foreach (var item2 in playersToBeMoved)
                                    try
                                    {
                                        Command.all.Find("goto").Use(item2, currentMap);
                                    }
                                    catch
                                    {
                                    }

                                Command.all.Find("unload").Use(null, lastmap);
                            }

                            ii++;
                            break;
                        }

                        if (currentMap == lavaMaps[0].Name)
                        {
                            currentlvl.UnloadLock = false;
                            currentlvl.Unload(true);
                            Command.all.Find("loadlavamap").Use(null, currentMap);
                            pcount = Player.players.Count;
                            for (var num = pcount - 1; num >= 0; num--)
                                try
                                {
                                    if (Player.players[num].level.name == lastmap)
                                        Command.all.Find("goto").Use(Player.players[num], currentMap);
                                }
                                catch (Exception ex2)
                                {
                                    Server.ErrorLog(ex2);
                                }

                            currentlvl = Level.Find(currentMap);
                            break;
                        }

                        ii = 0;
                    }

                    ResetLives();
                    if (Server.useHeaven) BringPlayersFromHeaven();
                    Server.s.Log("Current map: " + currentMap);
                    PrepareCreepingLava(currentLavaMap);
                    if (LavaSettings.All.ShowMapRating) printMapRating.Start();
                    if (LavaSettings.All.ShowMapAuthor && !string.IsNullOrEmpty(currentLavaMap.Author))
                        printMapAuthor.Start();
                    if (LavaSettings.All.RandomLavaState)
                    {
                        RandomizeMood();
                        printLavaMood.Start();
                    }

                    if (currentLavaMap.LavaCommands.Count > 0)
                        new Thread(StartLavaCommandsPhase1).Start(currentLavaMap);
                    currentlvl.unload = false;
                }

                Server.pause = false;
            }
        }

        // Token: 0x0600168F RID: 5775 RVA: 0x00089120 File Offset: 0x00087320
        private static void OnMapRatingTimerElapsed(object source, ElapsedEventArgs e)
        {
            PrintMapRating();
        }

        // Token: 0x06001690 RID: 5776 RVA: 0x00089128 File Offset: 0x00087328
        private static void OnMapAuthorTimerElapsed(object source, ElapsedEventArgs e)
        {
            PrintMapAuthor();
        }

        // Token: 0x06001691 RID: 5777 RVA: 0x00089130 File Offset: 0x00087330
        private static void OnChangeMoodTimerElapsed(object source, ElapsedEventArgs e)
        {
            PrintLavaMood();
        }

        // Token: 0x06001692 RID: 5778 RVA: 0x00089138 File Offset: 0x00087338
        public static void PlaceLavaSource(LavaSource lSource)
        {
            if (lSource.Block.ToLower() == "creeping" || lSource.Block.ToLower() == "crawling")
            {
                CreepingLava(lSource);
                return;
            }

            var b = Block.Byte(lSource.Block);
            if (b == 255) b = 194;
            if (waterFlood)
            {
                var b2 = b;
                switch (b2)
                {
                    case 80:
                    case 81:
                    case 82:
                    case 83:
                        break;
                    default:
                        switch (b2)
                        {
                            case 194:
                            case 195:
                                break;
                            default:
                                goto IL_8D;
                        }

                        break;
                }

                b = 193;
                IL_8D:
                waterFlood = false;
            }

            currentlvl.Blockchange((ushort) lSource.X, (ushort) lSource.Y, (ushort) lSource.Z, b);
        }

        // Token: 0x06001693 RID: 5779 RVA: 0x00089200 File Offset: 0x00087400
        public static void DoLavaCommand(LavaCommand lCommand)
        {
            var text = lCommand.Command.TrimStart('/').Trim();
            var name = text.Split(' ')[0].ToLower();
            var message = text.Substring(text.IndexOf(' ') + 1);
            Command.all.Find(name).Use(null, message);
        }

        // Token: 0x06001694 RID: 5780 RVA: 0x0008926C File Offset: 0x0008746C
        private static int SortLavaSourceByDelay(LavaSource x, LavaSource y)
        {
            if (x.Delay > y.Delay) return 1;
            if (x.Delay < y.Delay) return -1;
            return 0;
        }

        // Token: 0x06001695 RID: 5781 RVA: 0x00089290 File Offset: 0x00087490
        private static int SortLavaCommandByDelay(LavaCommand x, LavaCommand y)
        {
            if (x.Delay > y.Delay) return 1;
            if (x.Delay < y.Delay) return -1;
            return 0;
        }

        // Token: 0x06001696 RID: 5782 RVA: 0x000892B4 File Offset: 0x000874B4
        public static void StartLavaFlood(object lavaMap)
        {
            var lavaMap2 = (LavaMap) lavaMap;
            lavaMap2.LavaSources.Sort(SortLavaSourceByDelay);
            var num = 0;
            foreach (var lavaSource in lavaMap2.LavaSources)
            {
                Thread.Sleep((lavaSource.Delay - num) * 1000);
                if (!phase2holder) break;
                PlaceLavaSource(lavaSource);
                num = lavaSource.Delay;
            }
        }

        // Token: 0x06001697 RID: 5783 RVA: 0x0008934C File Offset: 0x0008754C
        public static void StartLavaCommandsPhase1(object lavaMap)
        {
            var lavaMap2 = (LavaMap) lavaMap;
            lavaMap2.LavaCommands.Sort(SortLavaCommandByDelay);
            var num = 0;
            foreach (var lavaCommand in lavaMap2.LavaCommands)
                if (lavaCommand.Phase == 1)
                {
                    Thread.Sleep((lavaCommand.Delay - num) * 1000);
                    if (!phase1holder) break;
                    DoLavaCommand(lavaCommand);
                    num = lavaCommand.Delay;
                }
        }

        // Token: 0x06001698 RID: 5784 RVA: 0x000893EC File Offset: 0x000875EC
        public static void StartLavaCommandsPhase2(object lavaMap)
        {
            var lavaMap2 = (LavaMap) lavaMap;
            lavaMap2.LavaCommands.Sort(SortLavaCommandByDelay);
            var num = 0;
            foreach (var lavaCommand in lavaMap2.LavaCommands)
                if (lavaCommand.Phase != 1)
                {
                    Thread.Sleep((lavaCommand.Delay - num) * 1000);
                    if (!phase2holder) break;
                    DoLavaCommand(lavaCommand);
                    num = lavaCommand.Delay;
                }
        }

        // Token: 0x06001699 RID: 5785 RVA: 0x0008948C File Offset: 0x0008768C
        public static void SaveLavaMapsXML()
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.AppendChild(xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", "yes"));
            xmlDocument.AppendChild(xmlDocument.CreateWhitespace("\r\n"));
            xmlDocument.AppendChild(xmlDocument.CreateComment(
                "\r\n* As Source you can use any Blocks, for example:\r\nahl, lta, ltb, ltc, ltd, lava_up, ahw\r\n* Attributes x,y,z determines the place where lava block will be placed.\r\nTo find appropriate x,y,z values, use /about command in game and\r\nclick in the place where you want to lava to start from. Then read coordinates.\r\n* Delay attribute determines how much time in seconds has to pass before\r\nthe block/command will be placed/triggered.\r\n* Phase = \"1\" is time for building a shelter, phase =\"2\" is time when lava flood started\r\n"));
            xmlDocument.AppendChild(xmlDocument.CreateWhitespace("\r\n"));
            xmlDocument.AppendChild(
                xmlDocument.CreateComment("For more help visit http://mcdzienny.cba.pl and go to Help section."));
            xmlDocument.AppendChild(xmlDocument.CreateWhitespace("\r\n"));
            xmlDocument.AppendChild(xmlDocument.CreateComment("Lava maps list"));
            xmlDocument.AppendChild(xmlDocument.CreateWhitespace("\r\n"));
            var xmlElement = xmlDocument.CreateElement("Maps");
            xmlDocument.AppendChild(xmlElement);
            foreach (var lavaMap in lavaMaps)
            {
                var xmlElement2 = xmlDocument.CreateElement("Map");
                var xmlAttribute = xmlDocument.CreateAttribute("name");
                var xmlAttribute2 = xmlDocument.CreateAttribute("author");
                var xmlAttribute3 = xmlDocument.CreateAttribute("phase1");
                var xmlAttribute4 = xmlDocument.CreateAttribute("phase2");
                xmlAttribute.Value = lavaMap.Name;
                xmlAttribute2.Value = lavaMap.Author;
                xmlAttribute3.Value = lavaMap.Phase1.ToString();
                xmlAttribute4.Value = lavaMap.Phase2.ToString();
                xmlElement2.SetAttributeNode(xmlAttribute);
                xmlElement2.SetAttributeNode(xmlAttribute3);
                xmlElement2.SetAttributeNode(xmlAttribute4);
                xmlElement2.SetAttributeNode(xmlAttribute2);
                foreach (var lavaSource in lavaMap.LavaSources)
                {
                    var xmlElement3 = xmlDocument.CreateElement("Source");
                    var xmlAttribute5 = xmlDocument.CreateAttribute("x");
                    var xmlAttribute6 = xmlDocument.CreateAttribute("y");
                    var xmlAttribute7 = xmlDocument.CreateAttribute("z");
                    var xmlAttribute8 = xmlDocument.CreateAttribute("type");
                    var xmlAttribute9 = xmlDocument.CreateAttribute("block");
                    var xmlAttribute10 = xmlDocument.CreateAttribute("delay");
                    xmlAttribute5.Value = lavaSource.X.ToString();
                    xmlAttribute6.Value = lavaSource.Y.ToString();
                    xmlAttribute7.Value = lavaSource.Z.ToString();
                    xmlAttribute8.Value = lavaSource.Type;
                    xmlAttribute9.Value = lavaSource.Block;
                    xmlAttribute10.Value = lavaSource.Delay.ToString();
                    xmlElement3.SetAttributeNode(xmlAttribute9);
                    xmlElement3.SetAttributeNode(xmlAttribute5);
                    xmlElement3.SetAttributeNode(xmlAttribute6);
                    xmlElement3.SetAttributeNode(xmlAttribute7);
                    xmlElement3.SetAttributeNode(xmlAttribute8);
                    xmlElement3.SetAttributeNode(xmlAttribute10);
                    xmlElement2.AppendChild(xmlElement3);
                }

                foreach (var lavaCommand in lavaMap.LavaCommands)
                {
                    var xmlElement4 = xmlDocument.CreateElement("Command");
                    var xmlAttribute11 = xmlDocument.CreateAttribute("command");
                    var xmlAttribute12 = xmlDocument.CreateAttribute("delay");
                    var xmlAttribute13 = xmlDocument.CreateAttribute("phase");
                    xmlAttribute11.Value = lavaCommand.Command;
                    xmlAttribute12.Value = lavaCommand.Delay.ToString();
                    xmlAttribute13.Value = lavaCommand.Phase != 1 ? "2" : "1";
                    xmlElement4.SetAttributeNode(xmlAttribute11);
                    xmlElement4.SetAttributeNode(xmlAttribute12);
                    xmlElement4.SetAttributeNode(xmlAttribute13);
                    xmlElement2.AppendChild(xmlElement4);
                }

                xmlElement.AppendChild(xmlElement2);
            }

            xmlDocument.AppendChild(xmlElement);
            xmlDocument.Save("lava/maps.txt");
        }

        // Token: 0x0600169A RID: 5786 RVA: 0x000898B8 File Offset: 0x00087AB8
        public static void LoadLavaMapsXML()
        {
            try
            {
                if (!File.Exists("lava/maps.txt")) File.Create("lava/maps.txt").Close();
                lavaMaps.Clear();
                var streamReader = new StreamReader("lava/maps.txt");
                var xmlDocument = new XmlDocument();
                xmlDocument.Load(streamReader);
                var elementsByTagName = xmlDocument.GetElementsByTagName("Map");
                for (var i = 0; i < elementsByTagName.Count; i++)
                {
                    var list = new List<LavaSource>();
                    var list2 = new List<LavaCommand>();
                    var lavaMap = new LavaMap();
                    var attributes = elementsByTagName[i].Attributes;
                    foreach (XmlAttribute item in attributes)
                        if (item.Name.ToLower() == "name")
                            lavaMap.Name = item.Value.ToLower();
                        else if (item.Name.ToLower() == "phase1")
                            try
                            {
                                lavaMap.Phase1 = int.Parse(item.Value);
                            }
                            catch
                            {
                            }
                        else if (item.Name.ToLower() == "phase2")
                            try
                            {
                                lavaMap.Phase2 = int.Parse(item.Value);
                            }
                            catch
                            {
                            }
                        else if (item.Name.ToLower() == "author") lavaMap.Author = item.Value;

                    var childNodes = elementsByTagName[i].ChildNodes;
                    foreach (XmlNode item2 in childNodes)
                    {
                        if (item2.Name.ToLower() == "source")
                        {
                            var attributes2 = item2.Attributes;
                            var lavaSource = new LavaSource();
                            foreach (XmlAttribute item3 in attributes2)
                                switch (item3.Name.ToLower())
                                {
                                    case "x":
                                        try
                                        {
                                            lavaSource.X = int.Parse(item3.Value);
                                        }
                                        catch
                                        {
                                        }

                                        break;
                                    case "y":
                                        try
                                        {
                                            lavaSource.Y = int.Parse(item3.Value);
                                        }
                                        catch
                                        {
                                        }

                                        break;
                                    case "z":
                                        try
                                        {
                                            lavaSource.Z = int.Parse(item3.Value);
                                        }
                                        catch
                                        {
                                        }

                                        break;
                                    case "type":
                                        lavaSource.Type = item3.Value;
                                        break;
                                    case "block":
                                        lavaSource.Block = item3.Value;
                                        break;
                                    case "delay":
                                        try
                                        {
                                            lavaSource.Delay = int.Parse(item3.Value);
                                        }
                                        catch
                                        {
                                        }

                                        break;
                                }

                            list.Add(lavaSource);
                        }

                        if (!(item2.Name.ToLower() == "command")) continue;
                        var attributes3 = item2.Attributes;
                        var lavaCommand = new LavaCommand();
                        foreach (XmlAttribute item4 in attributes3)
                            switch (item4.Name.ToLower())
                            {
                                case "command":
                                    lavaCommand.Command = item4.Value;
                                    break;
                                case "delay":
                                    try
                                    {
                                        lavaCommand.Delay = int.Parse(item4.Value);
                                    }
                                    catch
                                    {
                                    }

                                    break;
                                case "phase":
                                    try
                                    {
                                        lavaCommand.Phase = byte.Parse(item4.Value);
                                    }
                                    catch
                                    {
                                    }

                                    break;
                            }

                        list2.Add(lavaCommand);
                    }

                    if (list.Count > 0)
                    {
                        lavaMap.LavaSources = list;
                        lavaMap.LavaCommands = list2;
                        lavaMaps.Add(lavaMap);
                    }
                }

                VerifyMapNames();
                if (!Server.CLI) Window.thisWindow.UpdateLavaMaps();
                streamReader.Close();
            }
            catch (Exception)
            {
                if (!Server.CLI)
                {
                    MessageBox.Show("File: 'lava/maps.txt' is corrupted!", "Error");
                }
                else
                {
                    Server.s.Log("* File: 'lava/maps.txt' is corrupted!");
                    Thread.Sleep(4000);
                }

                throw;
            }
        }

        // Token: 0x0600169B RID: 5787 RVA: 0x00089E94 File Offset: 0x00088094
        public static void VerifyMapNames()
        {
            var directoryInfo = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location
                .Remove(Assembly.GetExecutingAssembly().Location.Length - 15) + "/lava/maps/"));
            var files = directoryInfo.GetFiles();
            var list = new List<string>();
            foreach (var fileInfo in files)
            {
                var a = fileInfo.Name.Substring(fileInfo.Name.LastIndexOf('.'));
                if (a == ".lvl") list.Add(fileInfo.Name.ToLower().Remove(fileInfo.Name.LastIndexOf('.')));
            }

            var list2 = new List<LavaMap>();
            foreach (var lavaMap in lavaMaps)
                if (!list.Contains(lavaMap.Name))
                    list2.Add(lavaMap);
            foreach (var item in list2) lavaMaps.Remove(item);
            list2.Clear();
            list.Clear();
        }

        // Token: 0x0600169C RID: 5788 RVA: 0x0008A000 File Offset: 0x00088200
        public static void ConvertLavaMaps()
        {
            if (!File.Exists("lava/lavamaps.txt") && LoadLavaMaps())
            {
                foreach (var lmaps in oldLavaMaps)
                    lavaMaps.Add(new LavaMap(lmaps.mapName, lmaps.timeBefore, lmaps.timeAfter, new List<LavaSource>
                    {
                        new LavaSource("", lmaps.lavaUp == 1 ? "creeping" : "ahl", lmaps.x, lmaps.y, lmaps.z, 0)
                    }));
                SaveLavaMapsXML();
            }
        }

        // Token: 0x0600169D RID: 5789 RVA: 0x0008A0D0 File Offset: 0x000882D0
        public static bool LoadLavaMaps()
        {
            if (File.Exists("lava/lavamaps.txt"))
            {
                var streamReader = new StreamReader("lava/lavamaps.txt");
                oldLavaMaps.Clear();
                string text;
                while ((text = streamReader.ReadLine()) != null)
                    if (text.Split(':').Length > 3 && text.Split(':').Length < 8)
                    {
                        text = text.Replace(" ", "");
                        var array = text.Split(':');
                        var item = default(lmaps);
                        try
                        {
                            item.mapName = array[0];
                            item.x = ushort.Parse(array[1]);
                            item.y = ushort.Parse(array[2]);
                            item.z = ushort.Parse(array[3]);
                            item.timeBefore = (ushort) (array.Length > 4 ? ushort.Parse(array[4]) : 0);
                            item.timeAfter = (ushort) (array.Length > 5 ? ushort.Parse(array[5]) : 0);
                            item.lavaUp = (byte) (array.Length <= 6 ? 3 :
                                array[6].ToLower() == "true" || array[6].ToLower() == "1" ? 1 :
                                !(array[6].ToLower() == "false") && !(array[6].ToLower() == "0") ? 3 : 0);
                            item.rage = (byte) (array.Length <= 7 ? 3 :
                                array[7].ToLower() == "true" || array[6].ToLower() == "1" ? 1 :
                                !(array[6].ToLower() == "false") && !(array[6].ToLower() == "0") ? 3 : 0);
                            item.lavaAmount = array.Length > 8 ? int.Parse(array[8]) : 0;
                            item.lavaDelay = array.Length > 9 ? int.Parse(array[9]) : 0;
                            oldLavaMaps.Add(item);
                        }
                        catch
                        {
                        }
                    }

                streamReader.Dispose();
                streamReader.Close();
                return true;
            }

            return false;
        }

        // Token: 0x0600169E RID: 5790 RVA: 0x0008A324 File Offset: 0x00088524
        public static void RandomizeMood()
        {
            if (chanceCalm + chanceDisturbed + chanceFurious + chanceWild != 100)
            {
                Server.s.Log("Error: chances in Randomize Mood don't sum up to 100.");
                chanceCalm = 35;
                chanceDisturbed = 45;
                chanceFurious = 15;
                chanceWild = 5;
                ServerProperties.Save();
            }

            var num = moodRandom.Next(0, 100);
            if (num < chanceCalm)
            {
                LavaSettings.All.LavaState = LavaState.Calm;
                return;
            }

            if (num < chanceCalm + chanceDisturbed)
            {
                LavaSettings.All.LavaState = LavaState.Disturbed;
                return;
            }

            if (num < chanceCalm + chanceDisturbed + chanceWild)
            {
                LavaSettings.All.LavaState = LavaState.Furious;
                return;
            }

            LavaSettings.All.LavaState = LavaState.Wild;
        }

        // Token: 0x0600169F RID: 5791 RVA: 0x0008A3E4 File Offset: 0x000885E4
        public static void PrintLavaMood()
        {
            switch (LavaSettings.All.LavaState)
            {
                case LavaState.Calm:
                    Player.GlobalMessageLevel(Server.LavaLevel, MessagesManager.GetString("LavaStateCalm"));
                    return;
                case LavaState.Disturbed:
                    Player.GlobalMessageLevel(Server.LavaLevel, MessagesManager.GetString("LavaStateDisturbed"));
                    return;
                case LavaState.Furious:
                    Player.GlobalMessageLevel(Server.LavaLevel, MessagesManager.GetString("LavaStateFurious"));
                    return;
                case LavaState.Wild:
                    Player.GlobalMessageLevel(Server.LavaLevel, MessagesManager.GetString("LavaStateWild"));
                    return;
                default:
                    return;
            }
        }

        // Token: 0x060016A0 RID: 5792 RVA: 0x0008A468 File Offset: 0x00088668
        public static void PrintMapRating()
        {
            using (var dataTable =
                DBInterface.fillData("SELECT * FROM `Rating" + Server.LavaLevel.name + "` WHERE Vote = 1", true))
            {
                using (var dataTable2 =
                    DBInterface.fillData("SELECT * FROM `Rating" + Server.LavaLevel.name + "` WHERE Vote = 2", true))
                {
                    Player.GlobalMessageLevel(Server.LavaLevel,
                        string.Format(Lang.LavaSystem.MapRatingResults, dataTable.Rows.Count, dataTable2.Rows.Count));
                    Player.GlobalMessageLevel(Server.LavaLevel, Lang.LavaSystem.MapRatingTip);
                }
            }
        }

        // Token: 0x060016A1 RID: 5793 RVA: 0x0008A52C File Offset: 0x0008872C
        public static void PrintMapAuthor()
        {
            Player.GlobalMessageLevel(Server.LavaLevel, string.Format(Lang.LavaSystem.MapName, currentlvl.name));
            Player.GlobalMessageLevel(Server.LavaLevel,
                string.Format(Lang.LavaSystem.MapAuthor, currentLavaMap.Author));
        }

        // Token: 0x060016A2 RID: 5794 RVA: 0x0008A56C File Offset: 0x0008876C
        public static void CountScoreDefault(Player p, int blocksAround)
        {
            p.score = 150 + blocksAround + p.lives * 20;
        }

        // Token: 0x060016A3 RID: 5795 RVA: 0x0008A588 File Offset: 0x00088788
        public static void PayRewardDefault(Player p, int rewardDifference)
        {
            if (p.IsAboveSeaLevel)
            {
                if (p.IronChallenge != 0)
                {
                    p.money += LavaSettings.All.RewardAboveSeaLevel * 2;
                    Player.SendMessage(p,
                        string.Format(Lang.LavaSystem.RewardMessageAboveSea, LavaSettings.All.RewardAboveSeaLevel * 2,
                            Server.moneys));
                }
                else
                {
                    p.money += LavaSettings.All.RewardAboveSeaLevel;
                    Player.SendMessage(p,
                        string.Format(Lang.LavaSystem.RewardMessageAboveSea, LavaSettings.All.RewardAboveSeaLevel,
                            Server.moneys));
                }
            }
            else if (p.IronChallenge != 0)
            {
                p.money += LavaSettings.All.RewardBelowSeaLevel * 2;
                Player.SendMessage(p,
                    string.Format(Lang.LavaSystem.RewardMessageBelowSea, LavaSettings.All.RewardBelowSeaLevel * 2,
                        Server.moneys,
                        rewardDifference < 1
                            ? ""
                            : string.Format(Lang.LavaSystem.RewardMessageBelowSea2, rewardDifference)));
            }
            else
            {
                p.money += LavaSettings.All.RewardBelowSeaLevel;
                Player.SendMessage(p,
                    string.Format(Lang.LavaSystem.RewardMessageBelowSea, LavaSettings.All.RewardBelowSeaLevel,
                        Server.moneys,
                        rewardDifference < 1
                            ? ""
                            : string.Format(Lang.LavaSystem.RewardMessageBelowSea2, rewardDifference)));
            }
        }

        // Token: 0x060016A4 RID: 5796 RVA: 0x0008A6E0 File Offset: 0x000888E0
        public static void OnCountScore(Player p, int blocksAround)
        {
            if (CountScore != null) CountScore(p, blocksAround);
        }

        // Token: 0x060016A5 RID: 5797 RVA: 0x0008A6F8 File Offset: 0x000888F8
        public static void OnPayReward(Player p, int rewardDifference)
        {
            if (PayReward != null) PayReward(p, rewardDifference);
        }

        // Token: 0x060016A6 RID: 5798 RVA: 0x0008A710 File Offset: 0x00088910
        public static void CheckWinners()
        {
            winnersList.Clear();
            lock (Player.players)
            {
                Player.players.ForEach(delegate(Player who)
                {
                    if (Server.LavaLevel == who.level && who.lives > 0 && !who.hidden && !who.invincible &&
                        CheckSpawn(who))
                    {
                        if (LavaSettings.All.ScoreMode == ScoreSystem.BasedOnAir)
                        {
                            OnCountScore(who, (int) CheckBlocksAround(who));
                            if (LavaSettings.All.StarSystem)
                            {
                                if (who.winningStreak == 1)
                                    who.score = (int) (who.score * 1.1f);
                                else if (who.winningStreak == 2)
                                    who.score = (int) (who.score * 1.2f);
                                else if (who.winningStreak >= 3) who.score = (int) (who.score * 1.3f);
                            }

                            who.totalScore += who.score;
                            if (who.score > who.bestScore) who.bestScore = who.score;
                        }
                        else if (LavaSettings.All.ScoreMode == ScoreSystem.Fixed)
                        {
                            who.score = LavaSettings.All.ScoreRewardFixed;
                        }
                        else if (LavaSettings.All.ScoreMode == ScoreSystem.NoScore)
                        {
                            who.score = 0;
                        }

                        who.timesWon++;
                        who.Save();
                        winnersList.Add(new Winner
                        {
                            name = who.PublicName,
                            score = who.score
                        });
                        winnersList.Sort((a, b) => a.score.CompareTo(b.score));
                    }
                });
            }
        }

        // Token: 0x060016A7 RID: 5799 RVA: 0x0008A774 File Offset: 0x00088974
        public static void AnnounceWinners()
        {
            if (winnersList.Count <= 0) return;
            var index = 0;
            if (LavaSettings.All.ScoreMode == ScoreSystem.BasedOnAir)
            {
                Player.GlobalMessageLevel(currentlvl, MessagesManager.GetString("TopWinners"));
                var num = 0;
                if (winnersList.Count > 6) num = winnersList.Count - 6;
                for (var i = num; i < winnersList.Count - 1; i++)
                {
                    Player.GlobalMessageLevel(currentlvl,
                        string.Format(MessagesManager.GetString("ScoreResult"), winnersList[i].name,
                            winnersList[i].score));
                    Server.s.Log(winnersList[i].name + " (Score: " + winnersList[i].score + ")");
                }

                Player.GlobalMessageLevel(currentlvl,
                    string.Format(MessagesManager.GetString("ScoreResultBest"), winnersList[winnersList.Count - 1].name,
                        winnersList[winnersList.Count - 1].score));
                Server.s.Log(winnersList[index].name + " This round best! (Score: " +
                             winnersList[winnersList.Count - 1].score + ")");
            }
            else
            {
                Player.GlobalMessageLevel(currentlvl, MessagesManager.GetString("WinnersList"));
                Server.s.Log("Winners list:");
                foreach (var winners in winnersList)
                {
                    Player.GlobalMessageLevel(currentlvl, "%c" + winners.name);
                    Server.s.Log(winners.name);
                }
            }

            var rewardDifference = 0;
            rewardDifference = LavaSettings.All.RewardAboveSeaLevel - LavaSettings.All.RewardBelowSeaLevel;
            Player.players.ForEach(delegate(Player who)
            {
                if (Server.LavaLevel == who.level)
                {
                    if (who.lives > 0 && !who.hidden && !who.invincible)
                    {
                        if (CheckSpawn(who))
                        {
                            OnPayReward(who, rewardDifference);
                            if (LavaSettings.All.ScoreMode == ScoreSystem.Fixed)
                                Player.SendMessage(who,
                                    string.Format(MessagesManager.GetString("ExperienceGain"),
                                        LavaSettings.All.ScoreRewardFixed + Server.DefaultColor));
                            else
                                Player.SendMessage(who,
                                    string.Format(MessagesManager.GetString("ExperienceGain"),
                                        who.score + Server.DefaultColor));
                            who.winningStreak++;
                        }
                        else
                        {
                            Player.SendMessage(who,
                                string.Format(Lang.LavaSystem.WarningTooCloseToSpawn,
                                    LavaSettings.All.RequiredDistanceFromSpawn));
                            who.winningStreak = 0;
                        }

                        who.invincible = true;
                    }
                    else
                    {
                        who.winningStreak = 0;
                    }
                }
            });
        }

        // Token: 0x060016A8 RID: 5800 RVA: 0x0008AA18 File Offset: 0x00088C18
        public static void BringPlayersFromHeaven()
        {
            lock (Player.players)
            {
                Player.players.ForEach(delegate(Player who)
                {
                    if (who.inHeaven)
                    {
                        who.inHeaven = false;
                        Command.all.Find("goto").Use(who, Server.LavaLevel.name);
                    }
                });
            }
        }

        // Token: 0x060016A9 RID: 5801 RVA: 0x0008AA74 File Offset: 0x00088C74
        public static void ResetLives()
        {
            while (true)
            {
                try
                {
                    Player.players.ForEach(delegate(Player who)
                    {
                        who.hackWarnings = 0;
                        who.lives = LavaSettings.All.LivesAtStart;
                        who.score = 0;
                        who.flipHead = false;
                        who.WarnedForHacksTimes = 0;
                        who.IronChallenge = IronChallengeType.None;
                        TierSystem.TierSet(who);
                        TierSystem.ColorSet(who);
                        TierSystem.GiveItems(who);
                        who.invincible = false;
                    });
                    Server.s.Log("Lives set back to " + LavaSettings.All.LivesAtStart);
                }
                catch
                {
                    if (counter < 2)
                    {
                        counter++;
                        continue;
                    }
                }

                break;
            }

            counter = 0;
        }

        // Token: 0x060016AA RID: 5802 RVA: 0x0008AB00 File Offset: 0x00088D00
        public static bool CheckSpawn(Player who)
        {
            var pos = who.pos;
            var num = Math.Abs(pos[0] / 32 - who.level.spawnx);
            var num2 = Math.Abs(pos[1] / 32 - who.level.spawny);
            var num3 = Math.Abs(pos[2] / 32 - who.level.spawnz);
            return num >= LavaSettings.All.RequiredDistanceFromSpawn ||
                   num2 >= LavaSettings.All.RequiredDistanceFromSpawn ||
                   num3 >= LavaSettings.All.RequiredDistanceFromSpawn;
        }

        // Token: 0x060016AB RID: 5803 RVA: 0x0008AB88 File Offset: 0x00088D88
        private static long CheckBlocksAround(Player who)
        {
            var pos = who.pos;
            var x = (ushort) (pos[0] / 32);
            var y = (ushort) (pos[1] / 32 - 1);
            var z = (ushort) (pos[2] / 32);
            halfmap = who.level.PosToInt(0, who.level.height / 2 - 1, 0);
            if (who.level.GetTile(x, y, z) == 0)
            {
                count++;
                blocks.Add(who.level.PosToInt(x, y, z));
                blockCheck.Clear();
                if (LavaSettings.All.PreventScoreAbuse)
                {
                    blocksArray = new byte[who.level.blocks.Length];
                    new List<bCheck>();
                    Test2(x, y, z, who);
                    var num = 0;
                    var num2 = 0;
                    while (blockCheck.Count > 0)
                    {
                        num2 = blockCheck.Count;
                        num = 0;
                        var num3 = 0;
                        while (num3 < num2)
                        {
                            if (count <= 1500)
                            {
                                Test2(blockCheck[num3].xChk, blockCheck[num3].yChk, blockCheck[num3].zChk, who);
                                num++;
                                num3++;
                                continue;
                            }

                            goto IL_0204;
                        }

                        blockCheck.RemoveRange(0, num);
                    }
                }
                else
                {
                    blocksArray = new byte[who.level.blocks.Length];
                    new List<bCheck>();
                    Test2(x, y, z, who);
                    var num4 = 0;
                    var num5 = 0;
                    while (blockCheck.Count > 0)
                    {
                        num5 = blockCheck.Count;
                        num4 = 0;
                        var num6 = 0;
                        while (num6 < num5)
                        {
                            if (!stopTest)
                            {
                                Test3(blockCheck[num6].xChk, blockCheck[num6].yChk, blockCheck[num6].zChk, who);
                                num4++;
                                num6++;
                                continue;
                            }

                            goto IL_0204;
                        }

                        blockCheck.RemoveRange(0, num4);
                    }
                }

                goto IL_0204;
            }

            return 0L;
            IL_0204:
            if (stopTest)
            {
                count = 2L;
                countBelow = 2L;
            }
            else if (count > 3000)
            {
                count = 2080L;
            }
            else if (count > 1400)
            {
                count = 1000 + (int) ((count - 1000) * 0.3);
            }

            if (countBelow > 7000) countBelow = 7000L;
            var result = count + countBelow / 7;
            blockCheck.Clear();
            blocks.Clear();
            count = 0L;
            countBelow = 0L;
            stopTest = false;
            return result;
        }

        // Token: 0x060016AC RID: 5804 RVA: 0x0008AE50 File Offset: 0x00089050
        public static string GetCheckBlocksCount()
        {
            return blockCheck.Count.ToString();
        }

        // Token: 0x060016AD RID: 5805 RVA: 0x0008AE70 File Offset: 0x00089070
        public static bool IsMapLoaded(string mapName)
        {
            foreach (var level in Server.levels)
                if (level.name == currentMap)
                {
                    Server.s.Log(currentMap + " is already loaded!");
                    return true;
                }

            return false;
        }

        // Token: 0x060016AE RID: 5806 RVA: 0x0008AEF0 File Offset: 0x000890F0
        public static void Test2(int x, int y, int z, Player who)
        {
            posToInt = who.level.PosToInt(x + 1, y, z);
            b = who.level.GetTile(posToInt);
            if (b == 0)
            {
                if (blocksArray[posToInt] == 0)
                {
                    if (posToInt > halfmap)
                        count += 1L;
                    else
                        countBelow += 1L;
                    blocksArray[posToInt] = 1;
                    blockCheck.Add(new bCheck
                    {
                        xChk = x + 1,
                        yChk = y,
                        zChk = z
                    });
                }
            }
            else if (b == 194)
            {
                stopTest = true;
                return;
            }

            posToInt = who.level.PosToInt(x - 1, y, z);
            b = who.level.GetTile(posToInt);
            if (b == 0)
            {
                if (blocksArray[posToInt] == 0)
                {
                    if (posToInt > halfmap)
                        count += 1L;
                    else
                        countBelow += 1L;
                    blocksArray[posToInt] = 1;
                    blockCheck.Add(new bCheck
                    {
                        xChk = x - 1,
                        yChk = y,
                        zChk = z
                    });
                }
            }
            else if (b == 194)
            {
                stopTest = true;
                return;
            }

            posToInt = who.level.PosToInt(x, y + 1, z);
            b = who.level.GetTile(posToInt);
            if (b == 0)
            {
                if (blocksArray[posToInt] == 0)
                {
                    if (posToInt > halfmap)
                        count += 1L;
                    else
                        countBelow += 1L;
                    blocksArray[posToInt] = 1;
                    blockCheck.Add(new bCheck
                    {
                        xChk = x,
                        yChk = y + 1,
                        zChk = z
                    });
                }
            }
            else if (b == 194)
            {
                stopTest = true;
                return;
            }

            posToInt = who.level.PosToInt(x, y - 1, z);
            b = who.level.GetTile(posToInt);
            if (b == 0 && blocksArray[posToInt] == 0)
            {
                if (posToInt > halfmap)
                    count += 1L;
                else
                    countBelow += 1L;
                blocksArray[posToInt] = 1;
                blockCheck.Add(new bCheck
                {
                    xChk = x,
                    yChk = y - 1,
                    zChk = z
                });
            }

            posToInt = who.level.PosToInt(x, y, z + 1);
            b = who.level.GetTile(posToInt);
            if (b == 0)
            {
                if (blocksArray[posToInt] == 0)
                {
                    if (posToInt > halfmap)
                        count += 1L;
                    else
                        countBelow += 1L;
                    blocksArray[posToInt] = 1;
                    blockCheck.Add(new bCheck
                    {
                        xChk = x,
                        yChk = y,
                        zChk = z + 1
                    });
                }
            }
            else if (b == 194)
            {
                stopTest = true;
                return;
            }

            posToInt = who.level.PosToInt(x, y, z - 1);
            b = who.level.GetTile(posToInt);
            if (b == 0)
            {
                if (blocksArray[posToInt] == 0)
                {
                    if (posToInt > halfmap)
                        count += 1L;
                    else
                        countBelow += 1L;
                    blocksArray[posToInt] = 1;
                    blockCheck.Add(new bCheck
                    {
                        xChk = x,
                        yChk = y,
                        zChk = z - 1
                    });
                }
            }
            else if (b == 194)
            {
                stopTest = true;
            }
        }

        // Token: 0x060016AF RID: 5807 RVA: 0x0008B30C File Offset: 0x0008950C
        public static void Test3(int x, int y, int z, Player who)
        {
            if (count > 1500L) return;
            posToInt = who.level.PosToInt(x + 1, y, z);
            b = who.level.GetTile(posToInt);
            if (b == 0 && blocksArray[posToInt] == 0)
            {
                if (posToInt > halfmap)
                    count += 1L;
                else
                    countBelow += 1L;
                blocksArray[posToInt] = 1;
                blockCheck.Add(new bCheck
                {
                    xChk = x + 1,
                    yChk = y,
                    zChk = z
                });
            }

            posToInt = who.level.PosToInt(x - 1, y, z);
            b = who.level.GetTile(posToInt);
            if (b == 0 && blocksArray[posToInt] == 0)
            {
                if (posToInt > halfmap)
                    count += 1L;
                else
                    countBelow += 1L;
                blocksArray[posToInt] = 1;
                blockCheck.Add(new bCheck
                {
                    xChk = x - 1,
                    yChk = y,
                    zChk = z
                });
            }

            posToInt = who.level.PosToInt(x, y + 1, z);
            b = who.level.GetTile(posToInt);
            if (b == 0 && blocksArray[posToInt] == 0)
            {
                if (posToInt > halfmap)
                    count += 1L;
                else
                    countBelow += 1L;
                blocksArray[posToInt] = 1;
                blockCheck.Add(new bCheck
                {
                    xChk = x,
                    yChk = y + 1,
                    zChk = z
                });
            }

            posToInt = who.level.PosToInt(x, y - 1, z);
            b = who.level.GetTile(posToInt);
            if (b == 0 && blocksArray[posToInt] == 0)
            {
                if (posToInt > halfmap)
                    count += 1L;
                else
                    countBelow += 1L;
                blocksArray[posToInt] = 1;
                blockCheck.Add(new bCheck
                {
                    xChk = x,
                    yChk = y - 1,
                    zChk = z
                });
            }

            posToInt = who.level.PosToInt(x, y, z + 1);
            b = who.level.GetTile(posToInt);
            if (b == 0 && blocksArray[posToInt] == 0)
            {
                if (posToInt > halfmap)
                    count += 1L;
                else
                    countBelow += 1L;
                blocksArray[posToInt] = 1;
                blockCheck.Add(new bCheck
                {
                    xChk = x,
                    yChk = y,
                    zChk = z + 1
                });
            }

            posToInt = who.level.PosToInt(x, y, z - 1);
            b = who.level.GetTile(posToInt);
            if (b == 0 && blocksArray[posToInt] == 0)
            {
                if (posToInt > halfmap)
                    count += 1L;
                else
                    countBelow += 1L;
                blocksArray[posToInt] = 1;
                blockCheck.Add(new bCheck
                {
                    xChk = x,
                    yChk = y,
                    zChk = z - 1
                });
            }
        }

        // Token: 0x060016B0 RID: 5808 RVA: 0x0008B6D0 File Offset: 0x000898D0
        public static void Test(ushort x, ushort y, ushort z, Player who)
        {
            if (count > 1500) return;
            if (who.level.GetTile((ushort) (x + 1), y, z) == 0 &&
                !blocks.Contains(who.level.PosToInt((ushort) (x + 1), y, z)))
            {
                if (who.level.PosToInt((ushort) (x + 1), y, z) > halfmap)
                    count++;
                else
                    countBelow++;
                blocks.Add(who.level.PosToInt((ushort) (x + 1), y, z));
                Test((ushort) (x + 1), y, z, who);
            }

            if (who.level.GetTile((ushort) (x - 1), y, z) == 0 &&
                !blocks.Contains(who.level.PosToInt((ushort) (x - 1), y, z)))
            {
                if (who.level.PosToInt((ushort) (x - 1), y, z) > halfmap)
                    count++;
                else
                    countBelow++;
                blocks.Add(who.level.PosToInt((ushort) (x - 1), y, z));
                Test((ushort) (x - 1), y, z, who);
            }

            if (who.level.GetTile(x, (ushort) (y + 1), z) == 0 &&
                !blocks.Contains(who.level.PosToInt(x, (ushort) (y + 1), z)))
            {
                if (who.level.PosToInt(x, (ushort) (y + 1), z) > halfmap)
                    count++;
                else
                    countBelow++;
                blocks.Add(who.level.PosToInt(x, (ushort) (y + 1), z));
                Test(x, (ushort) (y + 1), z, who);
            }

            if (who.level.GetTile(x, (ushort) (y - 1), z) == 0 &&
                !blocks.Contains(who.level.PosToInt(x, (ushort) (y - 1), z)))
            {
                if (who.level.PosToInt(x, (ushort) (y - 1), z) > halfmap)
                    count++;
                else
                    countBelow++;
                blocks.Add(who.level.PosToInt(x, (ushort) (y - 1), z));
                Test(x, (ushort) (y - 1), z, who);
            }

            switch (who.level.GetTile(x, y, (ushort) (z + 1)))
            {
                case 194:
                    count = 2L;
                    countBelow = 2L;
                    stopTest = true;
                    return;
                case 0:
                    if (!blocks.Contains(who.level.PosToInt(x, y, (ushort) (z + 1))))
                    {
                        if (who.level.PosToInt(x, y, (ushort) (z + 1)) > halfmap)
                            count++;
                        else
                            countBelow++;
                        blocks.Add(who.level.PosToInt(x, y, (ushort) (z + 1)));
                        Test(x, y, (ushort) (z + 1), who);
                    }

                    break;
            }

            if (who.level.GetTile(x, y, (ushort) (z - 1)) == 0 &&
                !blocks.Contains(who.level.PosToInt(x, y, (ushort) (z - 1))))
            {
                if (who.level.PosToInt(x, y, (ushort) (z - 1)) > halfmap)
                    count++;
                else
                    countBelow++;
                blocks.Add(who.level.PosToInt(x, y, (ushort) (z - 1)));
                Test(x, y, (ushort) (z - 1), who);
            }
        }

        // Token: 0x060016B1 RID: 5809 RVA: 0x0008BA58 File Offset: 0x00089C58
        public static int Voting()
        {
            if (lavaMaps.Count < 4) return -1;
            var array = new int[lavaMaps.Count];
            for (var i = 0; i < array.Length; i++) array[i] = i;
            var random = new Random();
            var array2 = new int[3];
            var num = random.Next(0, lavaMaps.Count);
            array2[0] = array[num];
            array[num] = array[array.Length - 1];
            num = random.Next(0, lavaMaps.Count - 1);
            array2[1] = array[num];
            array[num] = array[array.Length - 2];
            num = random.Next(0, lavaMaps.Count - 2);
            array2[2] = array[num];
            Server.LavaLevel.ChatLevel(Lang.LavaSystem.VoteForNextMap);
            Server.LavaLevel.ChatLevel(string.Format(Lang.LavaSystem.VoteOptions, Server.DefaultColor,
                lavaMaps[array2[0]].Name, lavaMaps[array2[1]].Name, lavaMaps[array2[2]].Name));
            Server.s.Log("Vote for the next map");
            Server.s.Log("Write 1 for " + lavaMaps[array2[0]].Name + ", 2 for " + lavaMaps[array2[1]].Name +
                         ", 3 for " + lavaMaps[array2[2]].Name);
            Server.voteMode = true;
            Thread.Sleep(25000);
            Server.voteMode = false;
            Server.LavaLevel.ChatLevel(Lang.LavaSystem.VoteResults);
            Server.LavaLevel.ChatLevel(string.Format(Lang.LavaSystem.VoteResults2, lavaMaps[array2[0]].Name, votes[0],
                lavaMaps[array2[1]].Name, votes[1], lavaMaps[array2[2]].Name, votes[2]));
            Server.s.Log("Results of voting:");
            Server.s.Log("Map: " + lavaMaps[array2[0]].Name + " - " + votes[0] + ", " + lavaMaps[array2[1]].Name +
                         " - " + votes[1] + ", " + lavaMaps[array2[2]].Name + " - " + votes[2] + " votes.");
            var num2 = votes[0] >= votes[1] ? votes[0] < votes[2] ? 2 : 0 : votes[1] >= votes[2] ? 1 : 2;
            Thread.Sleep(4000);
            Server.LavaLevel.ChatLevel(string.Format(Lang.LavaSystem.MapNext, lavaMaps[array2[num2]].Name));
            Server.s.Log("The next map is: " + lavaMaps[array2[num2]].Name);
            votes[0] = 0;
            votes[1] = 0;
            votes[2] = 0;
            votersList.Clear();
            return array2[num2];
        }

        // Token: 0x060016B2 RID: 5810 RVA: 0x0008BE60 File Offset: 0x0008A060
        public static bool CountVotes(string vote, Player p)
        {
            if (votersList.Contains(p.name.ToLower()))
            {
                if (p.group.Permission >= LevelPermission.Operator) return false;
                Player.SendMessage(p, Lang.LavaSystem.WarningAlreadyVoted);
            }
            else
            {
                switch (vote)
                {
                    case "1":
                        votes[0]++;
                        votersList.Add(p.name.ToLower());
                        Player.SendMessage(p, Lang.LavaSystem.Voted1);
                        break;
                    case "2":
                        votes[1]++;
                        votersList.Add(p.name.ToLower());
                        Player.SendMessage(p, Lang.LavaSystem.Voted2);
                        break;
                    case "3":
                        votes[2]++;
                        votersList.Add(p.name.ToLower());
                        Player.SendMessage(p, Lang.LavaSystem.Voted3);
                        break;
                    default:
                        Player.SendMessage(p, Lang.LavaSystem.VoteTip);
                        break;
                }
            }

            return true;
        }

        // Token: 0x060016B3 RID: 5811 RVA: 0x0008BF94 File Offset: 0x0008A194
        public static void EarthQuake(byte opt = 1)
        {
            var thread = new Thread(StartQuake);
            if (opt == 2) thread = new Thread(StartQuake2);
            thread.Start();
        }

        // Token: 0x060016B4 RID: 5812 RVA: 0x0008BFD0 File Offset: 0x0008A1D0
        private static void StartQuake()
        {
            var num = 0;
            var num2 = Server.LavaLevel.PosToInt(0, Server.LavaLevel.height / 2 - 1, 0);
            var t = DateTime.Now.AddMinutes(3.0);
            var now = DateTime.Now;
            var num3 = num2;
            while (num3 < Server.LavaLevel.blocks.Length && !(t < DateTime.Now))
            {
                if (rand.Next(1, 16) == 5)
                {
                    var b = Server.LavaLevel.blocks[num3];
                    if (b != 105 && b != 7 && b != 0 && b != 194)
                    {
                        Server.LavaLevel.AddUpdate(num3, 0, true);
                        num++;
                    }
                }

                if (num > 500)
                {
                    Thread.Sleep(250);
                    num = 0;
                }

                num3++;
            }

            Server.s.Log("EarthQuake Time taken: " + (DateTime.Now - now).TotalMilliseconds);
        }

        // Token: 0x060016B5 RID: 5813 RVA: 0x0008C0D8 File Offset: 0x0008A2D8
        private static void StartQuake2()
        {
            var num = Server.LavaLevel.PosToInt(0, Server.LavaLevel.height / 2 - 1, 0);
            var num2 = num / 15;
            var maxValue = Server.LavaLevel.blocks.Length;
            var num3 = 0;
            var t = DateTime.Now.AddMinutes(3.0);
            var now = DateTime.Now;
            Player.players.ForEach(delegate(Player who)
            {
                if (who.level == Server.LavaLevel) who.frozen = true;
            });
            Thread.Sleep(2500);
            Player.players.ForEach(delegate(Player who) { who.frozen = false; });
            Thread.Sleep(1500);
            var num4 = 0;
            while (num4 < num2 && !(t < DateTime.Now))
            {
                var num5 = rand.Next(num, maxValue);
                var b = Server.LavaLevel.blocks[num5];
                if (b != 105 && b != 7 && b != 0 && b != 194)
                {
                    Server.LavaLevel.AddUpdate(num5, 0, true);
                    num3++;
                }

                if (num3 > 500)
                {
                    Thread.Sleep(250);
                    num3 = 0;
                }

                num4++;
            }

            Server.s.Log("EarthQuake Time taken: " + (DateTime.Now - now).TotalMilliseconds);
        }

        // Token: 0x060016B6 RID: 5814 RVA: 0x0008C248 File Offset: 0x0008A448
        private static void PrepareCreepingLava(LavaMap lMap)
        {
            foreach (var lavaSource in lMap.LavaSources)
                if (lavaSource.Block == "creeping")
                {
                    iOffset = 1;
                    while (currentlvl.GetTile(lavaSource.X, lavaSource.Y + iOffset, lavaSource.Z) == 0)
                    {
                        currentlvl.Blockchange((ushort) lavaSource.X, (ushort) (lavaSource.Y + iOffset),
                            (ushort) lavaSource.Z, 105);
                        iOffset++;
                    }

                    break;
                }
        }

        // Token: 0x060016B7 RID: 5815 RVA: 0x0008C314 File Offset: 0x0008A514
        private static void PlaceCreepingLava(object lavaSource)
        {
            var lavaSource2 = (LavaSource) lavaSource;
            var millisecondsTimeout = 30000;
            var i = 0;
            while (i < iOffset)
            {
                for (var j = 0; j < amountOffset; j++)
                {
                    if (i < iOffset)
                    {
                        currentlvl.Blockchange((ushort) lavaSource2.X, (ushort) (lavaSource2.Y + i),
                            (ushort) lavaSource2.Z, 194);
                        if (!phase2holder) return;
                    }

                    i++;
                }

                Thread.Sleep(millisecondsTimeout);
            }
        }

        // Token: 0x060016B8 RID: 5816 RVA: 0x0008C394 File Offset: 0x0008A594
        private static void CreepingLava(LavaSource lSource)
        {
            new Thread(PlaceCreepingLava).Start(lSource);
        }

        // Token: 0x060016B9 RID: 5817 RVA: 0x0008C3B0 File Offset: 0x0008A5B0
        public List<string> LoadVipList()
        {
            FileUtil.CreateIfNotExists("ranks/viplist.txt");
            var source = File.ReadAllLines("ranks/viplist.txt");
            return source.ToList();
        }

        // Token: 0x060016BA RID: 5818 RVA: 0x0008C3DC File Offset: 0x0008A5DC
        public void SaveVipList(List<string> vips)
        {
            using (var streamWriter = new StreamWriter("ranks/viplist.txt"))
            {
                foreach (var value in vips) streamWriter.WriteLine(value);
            }
        }

        // Token: 0x060016BB RID: 5819 RVA: 0x0008C450 File Offset: 0x0008A650
        public static void LoadHeaven()
        {
            Server.heavenMap = Level.Load(Server.heavenMapName, 4, true);
            if (Server.heavenMap == null)
            {
                Server.s.Log("Could not find the heaven map.");
                Server.useHeaven = false;
                return;
            }

            Server.AddLevel(Server.heavenMap);
            Server.heavenMap.unload = false;
        }

        // Token: 0x060016BC RID: 5820 RVA: 0x0008C4A4 File Offset: 0x0008A6A4
        public static void StartServerMessage()
        {
            if (Server.serverMessageInterval > 0)
            {
                if (!serverMessageTimer.Enabled && Server.serverMessage != "")
                {
                    serverMessageTimer.Elapsed += delegate
                    {
                        var array = Server.serverMessage.Split(new[]
                        {
                            "\r\n"
                        }, 10, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var message in array) Player.GlobalChatWorld(null, message, false);
                    };
                    serverMessageTimer.Start();
                }

                serverMessageTimer.Interval = Server.serverMessageInterval * 60000;
            }
        }

        // Token: 0x060016BD RID: 5821 RVA: 0x0008C520 File Offset: 0x0008A720
        public static void UpdateServerMessageInterval()
        {
            if (Server.serverMessageInterval > 0) serverMessageTimer.Interval = Server.serverMessageInterval * 60000;
        }

        // Token: 0x060016BE RID: 5822 RVA: 0x0008C540 File Offset: 0x0008A740
        public static void StopServerMessage()
        {
            serverMessageTimer.Stop();
        }

        // Token: 0x060016BF RID: 5823 RVA: 0x0008C54C File Offset: 0x0008A74C
        public static void SetMapIndex(int index)
        {
            ii = index;
        }

        // Token: 0x060016C0 RID: 5824 RVA: 0x0008C554 File Offset: 0x0008A754
        public static bool RankUp(Player p)
        {
            try
            {
                var list = new List<Group>();
                list.AddRange(Group.groupList);
                for (var i = 0; i < list.Count; i++)
                    if (list[i].Permission == p.group.Permission)
                    {
                        if (list.Count > i + 1)
                        {
                            Command.all.Find("setrank").Use(null, p.name + " " + list[i + 1].name);
                            return true;
                        }

                        Player.SendMessage(p, Lang.LavaSystem.YouHaveTheHighestRank);
                        return false;
                    }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }

            return false;
        }

        // Token: 0x060016C1 RID: 5825 RVA: 0x0008C60C File Offset: 0x0008A80C
        public static void FoundTreasure(Player p, int x, int y, int z)
        {
            p.money += LavaSettings.All.AmountOfMoneyInTreasure;
            Player.SendMessage(p, MessagesManager.GetString("TreasureFound"));
            Player.SendMessage(p,
                string.Format(MessagesManager.GetString("TreasureGain"), LavaSettings.All.AmountOfMoneyInTreasure,
                    Server.moneys));
        }

        // Token: 0x0200030F RID: 783
        private class bCheck
        {
            // Token: 0x04000B3F RID: 2879
            public int xChk;

            // Token: 0x04000B40 RID: 2880
            public int yChk;

            // Token: 0x04000B41 RID: 2881
            public int zChk;
        }

        // Token: 0x02000310 RID: 784
        public struct lmaps
        {
            // Token: 0x04000B42 RID: 2882
            public string mapName;

            // Token: 0x04000B43 RID: 2883
            public int lavaAmount;

            // Token: 0x04000B44 RID: 2884
            public int lavaDelay;

            // Token: 0x04000B45 RID: 2885
            public ushort x;

            // Token: 0x04000B46 RID: 2886
            public ushort y;

            // Token: 0x04000B47 RID: 2887
            public ushort z;

            // Token: 0x04000B48 RID: 2888
            public ushort timeBefore;

            // Token: 0x04000B49 RID: 2889
            public ushort timeAfter;

            // Token: 0x04000B4A RID: 2890
            public byte rage;

            // Token: 0x04000B4B RID: 2891
            public byte lavaUp;
        }

        // Token: 0x02000311 RID: 785
        public struct Winner
        {
            // Token: 0x04000B4C RID: 2892
            public string name;

            // Token: 0x04000B4D RID: 2893
            public int score;
        }

        // Token: 0x02000314 RID: 788
        public class LavaSource
        {
            // Token: 0x060016E1 RID: 5857 RVA: 0x0008CA1C File Offset: 0x0008AC1C
            public LavaSource()
            {
            }

            // Token: 0x060016E2 RID: 5858 RVA: 0x0008CA24 File Offset: 0x0008AC24
            public LavaSource(string type, string block, int x, int y, int z, int delay)
            {
                Type = type;
                Block = block;
                X = x;
                Y = y;
                Z = z;
                Delay = delay;
            }

            // Token: 0x170008B0 RID: 2224
            // (get) Token: 0x060016D5 RID: 5845 RVA: 0x0008C9A4 File Offset: 0x0008ABA4
            // (set) Token: 0x060016D6 RID: 5846 RVA: 0x0008C9AC File Offset: 0x0008ABAC
            public string Type { get; set; }

            // Token: 0x170008B1 RID: 2225
            // (get) Token: 0x060016D7 RID: 5847 RVA: 0x0008C9B8 File Offset: 0x0008ABB8
            // (set) Token: 0x060016D8 RID: 5848 RVA: 0x0008C9C0 File Offset: 0x0008ABC0
            public string Block { get; set; }

            // Token: 0x170008B2 RID: 2226
            // (get) Token: 0x060016D9 RID: 5849 RVA: 0x0008C9CC File Offset: 0x0008ABCC
            // (set) Token: 0x060016DA RID: 5850 RVA: 0x0008C9D4 File Offset: 0x0008ABD4
            public int X { get; set; }

            // Token: 0x170008B3 RID: 2227
            // (get) Token: 0x060016DB RID: 5851 RVA: 0x0008C9E0 File Offset: 0x0008ABE0
            // (set) Token: 0x060016DC RID: 5852 RVA: 0x0008C9E8 File Offset: 0x0008ABE8
            public int Y { get; set; }

            // Token: 0x170008B4 RID: 2228
            // (get) Token: 0x060016DD RID: 5853 RVA: 0x0008C9F4 File Offset: 0x0008ABF4
            // (set) Token: 0x060016DE RID: 5854 RVA: 0x0008C9FC File Offset: 0x0008ABFC
            public int Z { get; set; }

            // Token: 0x170008B5 RID: 2229
            // (get) Token: 0x060016DF RID: 5855 RVA: 0x0008CA08 File Offset: 0x0008AC08
            // (set) Token: 0x060016E0 RID: 5856 RVA: 0x0008CA10 File Offset: 0x0008AC10
            public int Delay { get; set; }
        }

        // Token: 0x02000315 RID: 789
        public class LavaCommand
        {
            // Token: 0x170008B6 RID: 2230
            // (get) Token: 0x060016E3 RID: 5859 RVA: 0x0008CA5C File Offset: 0x0008AC5C
            // (set) Token: 0x060016E4 RID: 5860 RVA: 0x0008CA64 File Offset: 0x0008AC64
            public string Command { get; set; }

            // Token: 0x170008B7 RID: 2231
            // (get) Token: 0x060016E5 RID: 5861 RVA: 0x0008CA70 File Offset: 0x0008AC70
            // (set) Token: 0x060016E6 RID: 5862 RVA: 0x0008CA78 File Offset: 0x0008AC78
            public int Delay { get; set; }

            // Token: 0x170008B8 RID: 2232
            // (get) Token: 0x060016E7 RID: 5863 RVA: 0x0008CA84 File Offset: 0x0008AC84
            // (set) Token: 0x060016E8 RID: 5864 RVA: 0x0008CA8C File Offset: 0x0008AC8C
            public byte Phase { get; set; }
        }

        // Token: 0x02000316 RID: 790
        public class LavaMap
        {
            // Token: 0x060016F6 RID: 5878 RVA: 0x0008CB18 File Offset: 0x0008AD18
            public LavaMap()
            {
            }

            // Token: 0x060016F7 RID: 5879 RVA: 0x0008CB20 File Offset: 0x0008AD20
            public LavaMap(string name, int phase1, int phase2, List<LavaSource> lavaSources)
            {
                Name = name;
                Phase1 = phase1;
                Phase2 = phase2;
                LavaSources = lavaSources;
            }

            // Token: 0x060016F8 RID: 5880 RVA: 0x0008CB48 File Offset: 0x0008AD48
            public LavaMap(string name, int phase1, int phase2, List<LavaSource> lavaSources,
                List<LavaCommand> lavaCommands)
            {
                Name = name;
                Phase1 = phase1;
                Phase2 = phase2;
                LavaSources = lavaSources;
                LavaCommands = lavaCommands;
            }

            // Token: 0x170008B9 RID: 2233
            // (get) Token: 0x060016EA RID: 5866 RVA: 0x0008CAA0 File Offset: 0x0008ACA0
            // (set) Token: 0x060016EB RID: 5867 RVA: 0x0008CAA8 File Offset: 0x0008ACA8
            public string Name { get; set; }

            // Token: 0x170008BA RID: 2234
            // (get) Token: 0x060016EC RID: 5868 RVA: 0x0008CAB4 File Offset: 0x0008ACB4
            // (set) Token: 0x060016ED RID: 5869 RVA: 0x0008CABC File Offset: 0x0008ACBC
            public string Author { get; set; }

            // Token: 0x170008BB RID: 2235
            // (get) Token: 0x060016EE RID: 5870 RVA: 0x0008CAC8 File Offset: 0x0008ACC8
            // (set) Token: 0x060016EF RID: 5871 RVA: 0x0008CAD0 File Offset: 0x0008ACD0
            public int Phase1 { get; set; }

            // Token: 0x170008BC RID: 2236
            // (get) Token: 0x060016F0 RID: 5872 RVA: 0x0008CADC File Offset: 0x0008ACDC
            // (set) Token: 0x060016F1 RID: 5873 RVA: 0x0008CAE4 File Offset: 0x0008ACE4
            public int Phase2 { get; set; }

            // Token: 0x170008BD RID: 2237
            // (get) Token: 0x060016F2 RID: 5874 RVA: 0x0008CAF0 File Offset: 0x0008ACF0
            // (set) Token: 0x060016F3 RID: 5875 RVA: 0x0008CAF8 File Offset: 0x0008ACF8
            public List<LavaSource> LavaSources { get; set; }

            // Token: 0x170008BE RID: 2238
            // (get) Token: 0x060016F4 RID: 5876 RVA: 0x0008CB04 File Offset: 0x0008AD04
            // (set) Token: 0x060016F5 RID: 5877 RVA: 0x0008CB0C File Offset: 0x0008AD0C
            public List<LavaCommand> LavaCommands { get; set; }
        }
    }
}