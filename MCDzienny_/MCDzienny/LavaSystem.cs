using System;
using System.Collections.Generic;
using System.Data;
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
using MCDzienny.RemoteAccess;
using MCDzienny.Settings;
using Timer = System.Timers.Timer;

namespace MCDzienny
{
    public class LavaSystem
    {

        public delegate void CountScoreDelegate(Player p, int airBlocksCount);

        public delegate void PayRewardDelegate(Player p, int rewardDifference);

        public static bool waterFlood;

        public static bool skipVoting;

        static int pcount;

        public static byte startingBlock;

        static readonly Timer printMapRating;

        static readonly Timer printLavaMood;

        static readonly Timer printMapAuthor;

        public static int chanceCalm;

        public static int chanceDisturbed;

        public static int chanceFurious;

        public static int chanceWild;

        static byte b;

        static bool stopTest;

        public static int immortalTime;

        static int iOffset;

        public static int amountOffset;

        static bool earthQuake;

        public static int lavaUpDelay;

        static int ii;

        static string currentMap;

        static string lastmap;

        public static int stime;

        public static int stime2;

        public static Level currentlvl;

        public static volatile bool phase1holder;

        public static volatile bool phase2holder;

        public static volatile bool nextMap;

        public static int time;

        public static int time2;

        static bool runonce;

        static int counter;

        public static Random rand;

        public static Random moodRandom;

        public static bool protectionHolder;

        public static LavaMap currentLavaMap;

        static int halfmap;

        static readonly List<int> blocks;

        static byte[] blocksArray;

        static long count;

        static long countBelow;

        public static int mapNumber;

        static readonly int[] votes;

        static readonly Timer serverMessageTimer;

        static readonly List<string> votersList;

        public static List<Winner> winnersList;

        public static List<string> deadplayers;

        public static List<lmaps> oldLavaMaps;

        static readonly List<bCheck> blockCheck;

        static Thread lavaloop;

        public static ResourceManager rm;

        public static List<LavaMap> lavaMaps;

        static int posToInt;

        static LavaSystem()
        {
            waterFlood = false;
            skipVoting = false;
            pcount = 0;
            startingBlock = 194;
            printMapRating = new Timer(25000.0);
            printLavaMood = new Timer(10000.0);
            printMapAuthor = new Timer(3000.0);
            chanceCalm = 35;
            chanceDisturbed = 45;
            chanceFurious = 15;
            chanceWild = 5;
            stopTest = false;
            immortalTime = 20;
            amountOffset = 2;
            earthQuake = false;
            lavaUpDelay = 30000;
            ii = 1;
            stime = 14;
            stime2 = 8;
            phase1holder = true;
            phase2holder = false;
            nextMap = false;
            time = 14;
            time2 = 8;
            runonce = true;
            counter = 0;
            rand = new Random();
            moodRandom = new Random();
            blocks = new List<int>();
            blocksArray = new byte[0];
            count = 0L;
            countBelow = 0L;
            votes = new int[3];
            serverMessageTimer = new Timer(600000.0);
            votersList = new List<string>();
            winnersList = new List<Winner>();
            deadplayers = new List<string>();
            oldLavaMaps = new List<lmaps>();
            blockCheck = new List<bCheck>();
            rm = new ResourceManager("MCDzienny.Ling.LavaSystem", Assembly.GetExecutingAssembly());
            lavaMaps = new List<LavaMap>();
            CountScore += CountScoreDefault;
            PayReward += PayRewardDefault;
        }

        public static event CountScoreDelegate CountScore;

        public static event PayRewardDelegate PayReward;

        public static void Start()
        {
            if (!Directory.Exists("lava"))
            {
                Directory.CreateDirectory("lava");
            }
            if (!Directory.Exists("lava/maps"))
            {
                Directory.CreateDirectory("lava/maps");
            }
            if (lavaloop == null || !lavaloop.IsAlive)
            {
                lavaloop = new Thread(LavaThread);
                lavaloop.IsBackground = true;
                lavaloop.Start();
            }
        }

        public static void LavaMapInitialization()
        {
            currentMap = lavaMaps[0].Name;
            currentLavaMap = lavaMaps[0];
            time = lavaMaps[0].Phase1 == 0 ? time : lavaMaps[0].Phase1;
            time2 = lavaMaps[0].Phase2 == 0 ? time2 : lavaMaps[0].Phase2;
            Server.LavaLevel = Level.Load(currentMap, 3, lavaSurv: true);
            Server.LavaLevel.unload = false;
            Server.LavaLevel.UnloadLock = true;
            Server.AddLevel(Server.LavaLevel);
            currentlvl = Server.LavaLevel;
        }

        internal static void UpdateTimeStatus()
        {
            RemoteClient.remoteClients.ForEach(delegate(RemoteClient rc) { rc.SendTimes(time + 1, time2 + time + 2); });
            if (!Server.CLI)
            {
                string timeToFlood;
                if (time >= 0)
                {
                    timeToFlood = time + 1 + "min";
                }
                else
                {
                    timeToFlood = "started!";
                }
                string timeToEnd;
                if (time2 >= 0 || time >= 0)
                {
                    timeToEnd = time + time2 + 2 + "min";
                }
                else
                {
                    timeToEnd = "finished!";
                }
                Window.thisWindow.toolStripStatusLabelLagometer.GetCurrentParent().Invoke((Action)delegate
                {
                    Window.thisWindow.toolStripStatusLabelRoundTime.Text =
                        string.Format("Flood starts in : {0}   Round ends in : {1}", timeToFlood, timeToEnd);
                });
            }
        }

        static void LavaThread()
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
                        {
                            Player.GlobalMessageLevel(currentlvl, MessagesManager.GetString("LavaIsComing"));
                        }
                        else
                        {
                            Player.GlobalMessageLevel(currentlvl, MessagesManager.GetString("WaterIsComing"));
                        }
                        Server.s.Log("Look out! Lava is coming!!!");
                        new Thread(StartLavaFlood).Start(currentLavaMap);
                        if (currentLavaMap.LavaCommands.Count > 0)
                        {
                            new Thread(StartLavaCommandsPhase2).Start(currentLavaMap);
                        }
                        if (currentlvl.physics < 3)
                        {
                            currentlvl.physics = 3;
                        }
                        phase1holder = false;
                        phase2holder = true;
                        time = -1;
                        UpdateTimeStatus();
                        if (LavaSettings.All.OverloadProtection && Player.players.Count > 5)
                        {
                            new Thread((ThreadStart)delegate
                            {
                                double interval = Server.updateTimer.Interval;
                                Server.updateTimer.Interval = 10000.0;
                                int num = 1;
                                protectionHolder = true;
                                while (protectionHolder)
                                {
                                    Thread.Sleep(30000);
                                    if (num > 9)
                                    {
                                        protectionHolder = false;
                                    }
                                    num++;
                                }
                                Server.updateTimer.Interval = interval;
                            }).Start();
                        }
                        Thread.Sleep(8000);
                        break;
                    }
                    if (time == 1)
                    {
                        if (!waterFlood)
                        {
                            Player.GlobalMessageLevel(currentlvl, string.Format(MessagesManager.GetString("MinuteToLavaFlood"), time));
                            Server.s.Log(time + " minute left to lava flood");
                        }
                        else
                        {
                            Player.GlobalMessageLevel(currentlvl, string.Format(MessagesManager.GetString("MinuteToWaterFlood"), time));
                            Server.s.Log(time + " minute left to water flood");
                        }
                        if (LavaSettings.All.RandomLavaState)
                        {
                            printLavaMood.Start();
                        }
                    }
                    else if (!waterFlood)
                    {
                        Player.GlobalMessageLevel(currentlvl, string.Format(MessagesManager.GetString("MinutesToLavaFlood"), time));
                        Server.s.Log(time + " minutes left to lava flood");
                    }
                    else
                    {
                        Player.GlobalMessageLevel(currentlvl, string.Format(MessagesManager.GetString("MinutesToWaterFlood"), time));
                        Server.s.Log(time + " minutes left to water flood");
                    }
                    time--;
                    UpdateTimeStatus();
                    for (int i = 0; i < 60; i++)
                    {
                        Thread.Sleep(1000);
                        if (!phase1holder)
                        {
                            break;
                        }
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
                        if (skipVoting)
                        {
                            break;
                        }
                        if (LavaSettings.All.VotingSystem)
                        {
                            Thread.Sleep(10000);
                            mapNumber = Voting();
                            if (mapNumber == -1)
                            {
                                Server.s.Log("Not enough maps for voting system to work. Voting turned off.");
                                LavaSettings.All.VotingSystem = false;
                                if (!Server.CLI)
                                {
                                    Window.thisWindow.UpdateProperties();
                                }
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
                            Player.GlobalMessageLevel(currentlvl, string.Format(MessagesManager.GetString("MinuteToLavaFloodEnd"), time2));
                            Server.s.Log(time2 + " minute left to end of lava flood");
                        }
                        else
                        {
                            Player.GlobalMessageLevel(currentlvl, string.Format(MessagesManager.GetString("MinuteToWaterFloodEnd"), time2));
                            Server.s.Log(time2 + " minute left to end of water flood");
                        }
                    }
                    else if (!waterFlood)
                    {
                        Player.GlobalMessageLevel(currentlvl, string.Format(MessagesManager.GetString("MinutesToLavaFloodEnd"), time2));
                        Server.s.Log(time2 + " minutes left to end of lava flood");
                    }
                    else
                    {
                        Player.GlobalMessageLevel(currentlvl, string.Format(MessagesManager.GetString("MinutesToWaterFloodEnd"), time2));
                        Server.s.Log(time2 + " minutes left to end of water flood");
                    }
                    time2--;
                    UpdateTimeStatus();
                    for (int j = 0; j < 60; j++)
                    {
                        Thread.Sleep(1000);
                        if (!phase2holder)
                        {
                            break;
                        }
                    }
                }
                if (nextMap)
                {
                    nextMap = false;
                    phase1holder = true;
                    phase2holder = true;
                    time = stime;
                    time2 = stime2;
                    if (!skipVoting && LavaSettings.All.VotingSystem)
                    {
                        ii = mapNumber;
                    }
                    skipVoting = false;
                    while (true)
                    {
                        if (ii < lavaMaps.Count)
                        {
                            if (currentMap == lavaMaps[ii].Name)
                            {
                                var playersToBeMoved = new List<Player>();
                                Player.players.ForEachSync(delegate(Player p)
                                {
                                    if (p.level == Server.LavaLevel)
                                    {
                                        playersToBeMoved.Add(p);
                                    }
                                });
                                currentlvl.UnloadLock = false;
                                currentlvl.Unload(reload: true);
                                Command.all.Find("loadlavamap").Use(null, currentMap);
                                currentlvl = Level.Find(currentMap);
                                Server.LavaLevel = currentlvl;
                                currentlvl.UnloadLock = true;
                                foreach (Player item in playersToBeMoved)
                                {
                                    try
                                    {
                                        Command.all.Find("goto").Use(item, currentMap);
                                    }
                                    catch {}
                                }
                            }
                            else
                            {
                                currentlvl.UnloadLock = false;
                                lastmap = currentMap;
                                currentMap = lavaMaps[ii].Name;
                                if (!IsMapLoaded(currentMap))
                                {
                                    Command.all.Find("loadlavamap").Use(null, currentMap);
                                }
                                currentlvl = Level.Find(currentMap);
                                currentLavaMap = lavaMaps[ii];
                                time = lavaMaps[ii].Phase1 <= 0 ? time : lavaMaps[ii].Phase1;
                                time2 = lavaMaps[ii].Phase2 <= 0 ? time2 : lavaMaps[ii].Phase2;
                                var playersToBeMoved2 = new List<Player>();
                                Player.players.ForEachSync(delegate(Player p)
                                {
                                    if (p.level == Server.LavaLevel)
                                    {
                                        playersToBeMoved2.Add(p);
                                    }
                                });
                                Server.LavaLevel = currentlvl;
                                currentlvl.UnloadLock = true;
                                foreach (Player item2 in playersToBeMoved2)
                                {
                                    try
                                    {
                                        Command.all.Find("goto").Use(item2, currentMap);
                                    }
                                    catch {}
                                }
                                Command.all.Find("unload").Use(null, lastmap);
                            }
                            ii++;
                            break;
                        }
                        if (currentMap == lavaMaps[0].Name)
                        {
                            currentlvl.UnloadLock = false;
                            currentlvl.Unload(reload: true);
                            Command.all.Find("loadlavamap").Use(null, currentMap);
                            pcount = Player.players.Count;
                            for (int num2 = pcount - 1; num2 >= 0; num2--)
                            {
                                try
                                {
                                    if (Player.players[num2].level.name == lastmap)
                                    {
                                        Command.all.Find("goto").Use(Player.players[num2], currentMap);
                                    }
                                }
                                catch (Exception ex2)
                                {
                                    Server.ErrorLog(ex2);
                                }
                            }
                            currentlvl = Level.Find(currentMap);
                            break;
                        }
                        ii = 0;
                    }
                    ResetLives();
                    if (Server.useHeaven)
                    {
                        BringPlayersFromHeaven();
                    }
                    Server.s.Log("Current map: " + currentMap);
                    PrepareCreepingLava(currentLavaMap);
                    if (LavaSettings.All.ShowMapRating)
                    {
                        printMapRating.Start();
                    }
                    if (LavaSettings.All.ShowMapAuthor && !string.IsNullOrEmpty(currentLavaMap.Author))
                    {
                        printMapAuthor.Start();
                    }
                    if (LavaSettings.All.RandomLavaState)
                    {
                        RandomizeMood();
                        printLavaMood.Start();
                    }
                    if (currentLavaMap.LavaCommands.Count > 0)
                    {
                        new Thread(StartLavaCommandsPhase1).Start(currentLavaMap);
                    }
                    currentlvl.unload = false;
                }
                Server.pause = false;
            }
        }

        static void OnMapRatingTimerElapsed(object source, ElapsedEventArgs e)
        {
            PrintMapRating();
        }

        static void OnMapAuthorTimerElapsed(object source, ElapsedEventArgs e)
        {
            PrintMapAuthor();
        }

        static void OnChangeMoodTimerElapsed(object source, ElapsedEventArgs e)
        {
            PrintLavaMood();
        }

        public static void PlaceLavaSource(LavaSource lSource)
        {
            if (lSource.Block.ToLower() == "creeping" || lSource.Block.ToLower() == "crawling")
            {
                CreepingLava(lSource);
                return;
            }
            byte b = Block.Byte(lSource.Block);
            if (b == byte.MaxValue)
            {
                b = 194;
            }
            if (waterFlood)
            {
                switch (b)
                {
                    case 80:
                    case 81:
                    case 82:
                    case 83:
                    case 194:
                    case 195:
                        b = 193;
                        break;
                }
                waterFlood = false;
            }
            currentlvl.Blockchange((ushort)lSource.X, (ushort)lSource.Y, (ushort)lSource.Z, b);
        }

        public static void DoLavaCommand(LavaCommand lCommand)
        {
            string text = lCommand.Command.TrimStart('/').Trim();
            string name = text.Split(' ')[0].ToLower();
            string message = text.Substring(text.IndexOf(' ') + 1);
            Command.all.Find(name).Use(null, message);
        }

        static int SortLavaSourceByDelay(LavaSource x, LavaSource y)
        {
            if (x.Delay > y.Delay)
            {
                return 1;
            }
            if (x.Delay < y.Delay)
            {
                return -1;
            }
            return 0;
        }

        static int SortLavaCommandByDelay(LavaCommand x, LavaCommand y)
        {
            if (x.Delay > y.Delay)
            {
                return 1;
            }
            if (x.Delay < y.Delay)
            {
                return -1;
            }
            return 0;
        }

        public static void StartLavaFlood(object lavaMap)
        {
            LavaMap lavaMap2 = (LavaMap)lavaMap;
            lavaMap2.LavaSources.Sort(SortLavaSourceByDelay);
            int num = 0;
            foreach (LavaSource lavaSource in lavaMap2.LavaSources)
            {
                Thread.Sleep((lavaSource.Delay - num) * 1000);
                if (!phase2holder)
                {
                    break;
                }
                PlaceLavaSource(lavaSource);
                num = lavaSource.Delay;
            }
        }

        public static void StartLavaCommandsPhase1(object lavaMap)
        {
            LavaMap lavaMap2 = (LavaMap)lavaMap;
            lavaMap2.LavaCommands.Sort(SortLavaCommandByDelay);
            int num = 0;
            foreach (LavaCommand lavaCommand in lavaMap2.LavaCommands)
            {
                if (lavaCommand.Phase == 1)
                {
                    Thread.Sleep((lavaCommand.Delay - num) * 1000);
                    if (!phase1holder)
                    {
                        break;
                    }
                    DoLavaCommand(lavaCommand);
                    num = lavaCommand.Delay;
                }
            }
        }

        public static void StartLavaCommandsPhase2(object lavaMap)
        {
            LavaMap lavaMap2 = (LavaMap)lavaMap;
            lavaMap2.LavaCommands.Sort(SortLavaCommandByDelay);
            int num = 0;
            foreach (LavaCommand lavaCommand in lavaMap2.LavaCommands)
            {
                if (lavaCommand.Phase != 1)
                {
                    Thread.Sleep((lavaCommand.Delay - num) * 1000);
                    if (!phase2holder)
                    {
                        break;
                    }
                    DoLavaCommand(lavaCommand);
                    num = lavaCommand.Delay;
                }
            }
        }

        public static void SaveLavaMapsXML()
        {
            //IL_0000: Unknown result type (might be due to invalid IL or missing references)
            //IL_0006: Expected O, but got Unknown
            XmlDocument val = new XmlDocument();
            val.AppendChild(val.CreateXmlDeclaration("1.0", "UTF-8", "yes"));
            val.AppendChild(val.CreateWhitespace("\r\n"));
            val.AppendChild(val.CreateComment(
                                "\r\n* As Source you can use any Blocks, for example:\r\nahl, lta, ltb, ltc, ltd, lava_up, ahw\r\n* Attributes x,y,z determines the place where lava block will be placed.\r\nTo find appropriate x,y,z values, use /about command in game and\r\nclick in the place where you want to lava to start from. Then read coordinates.\r\n* Delay attribute determines how much time in seconds has to pass before\r\nthe block/command will be placed/triggered.\r\n* Phase = \"1\" is time for building a shelter, phase =\"2\" is time when lava flood started\r\n"));
            val.AppendChild(val.CreateWhitespace("\r\n"));
            val.AppendChild(val.CreateComment("For more help visit http://mcdzienny.cba.pl and go to Help section."));
            val.AppendChild(val.CreateWhitespace("\r\n"));
            val.AppendChild(val.CreateComment("Lava maps list"));
            val.AppendChild(val.CreateWhitespace("\r\n"));
            XmlElement val2 = val.CreateElement("Maps");
            val.AppendChild(val2);
            foreach (LavaMap lavaMap in lavaMaps)
            {
                XmlElement val3 = val.CreateElement("Map");
                XmlAttribute val4 = val.CreateAttribute("name");
                XmlAttribute val5 = val.CreateAttribute("author");
                XmlAttribute val6 = val.CreateAttribute("phase1");
                XmlAttribute val7 = val.CreateAttribute("phase2");
                val4.Value = lavaMap.Name;
                val5.Value = lavaMap.Author;
                val6.Value = lavaMap.Phase1.ToString();
                val7.Value = lavaMap.Phase2.ToString();
                val3.SetAttributeNode(val4);
                val3.SetAttributeNode(val6);
                val3.SetAttributeNode(val7);
                val3.SetAttributeNode(val5);
                foreach (LavaSource lavaSource in lavaMap.LavaSources)
                {
                    XmlElement val8 = val.CreateElement("Source");
                    XmlAttribute val9 = val.CreateAttribute("x");
                    XmlAttribute val10 = val.CreateAttribute("y");
                    XmlAttribute val11 = val.CreateAttribute("z");
                    XmlAttribute val12 = val.CreateAttribute("type");
                    XmlAttribute val13 = val.CreateAttribute("block");
                    XmlAttribute val14 = val.CreateAttribute("delay");
                    val9.Value = lavaSource.X.ToString();
                    val10.Value = lavaSource.Y.ToString();
                    val11.Value = lavaSource.Z.ToString();
                    val12.Value = lavaSource.Type;
                    val13.Value = lavaSource.Block;
                    val14.Value = lavaSource.Delay.ToString();
                    val8.SetAttributeNode(val13);
                    val8.SetAttributeNode(val9);
                    val8.SetAttributeNode(val10);
                    val8.SetAttributeNode(val11);
                    val8.SetAttributeNode(val12);
                    val8.SetAttributeNode(val14);
                    val3.AppendChild(val8);
                }
                foreach (LavaCommand lavaCommand in lavaMap.LavaCommands)
                {
                    XmlElement val15 = val.CreateElement("Command");
                    XmlAttribute val16 = val.CreateAttribute("command");
                    XmlAttribute val17 = val.CreateAttribute("delay");
                    XmlAttribute val18 = val.CreateAttribute("phase");
                    val16.Value = lavaCommand.Command;
                    val17.Value = lavaCommand.Delay.ToString();
                    val18.Value = lavaCommand.Phase != 1 ? "2" : "1";
                    val15.SetAttributeNode(val16);
                    val15.SetAttributeNode(val17);
                    val15.SetAttributeNode(val18);
                    val3.AppendChild(val15);
                }
                val2.AppendChild(val3);
            }
            val.AppendChild(val2);
            val.Save("lava/maps.txt");
        }

        public static void LoadLavaMapsXML()
        {
            //IL_0030: Unknown result type (might be due to invalid IL or missing references)
            //IL_0036: Expected O, but got Unknown
            //IL_046d: Unknown result type (might be due to invalid IL or missing references)
            //IL_0088: Unknown result type (might be due to invalid IL or missing references)
            //IL_008f: Expected O, but got Unknown
            //IL_01d2: Unknown result type (might be due to invalid IL or missing references)
            //IL_01d9: Expected O, but got Unknown
            //IL_0332: Unknown result type (might be due to invalid IL or missing references)
            //IL_0339: Expected O, but got Unknown
            //IL_018b: Unknown result type (might be due to invalid IL or missing references)
            //IL_0192: Expected O, but got Unknown
            try
            {
                if (!File.Exists("lava/maps.txt"))
                {
                    File.Create("lava/maps.txt").Close();
                }
                lavaMaps.Clear();
                StreamReader streamReader = new StreamReader("lava/maps.txt");
                XmlDocument val = new XmlDocument();
                val.Load(streamReader);
                XmlNodeList elementsByTagName = val.GetElementsByTagName("Map");
                for (int i = 0; i < elementsByTagName.Count; i++)
                {
                    var list = new List<LavaSource>();
                    var list2 = new List<LavaCommand>();
                    LavaMap lavaMap = new LavaMap();
                    XmlAttributeCollection attributes = elementsByTagName[i].Attributes;
                    foreach (XmlAttribute item in attributes)
                    {
                        XmlAttribute val2 = item;
                        if (val2.Name.ToLower() == "name")
                        {
                            lavaMap.Name = val2.Value.ToLower();
                        }
                        else if (val2.Name.ToLower() == "phase1")
                        {
                            try
                            {
                                lavaMap.Phase1 = int.Parse(val2.Value);
                            }
                            catch {}
                        }
                        else if (val2.Name.ToLower() == "phase2")
                        {
                            try
                            {
                                lavaMap.Phase2 = int.Parse(val2.Value);
                            }
                            catch {}
                        }
                        else if (val2.Name.ToLower() == "author")
                        {
                            lavaMap.Author = val2.Value;
                        }
                    }
                    XmlNodeList childNodes = elementsByTagName[i].ChildNodes;
                    foreach (XmlNode item2 in childNodes)
                    {
                        XmlNode val3 = item2;
                        if (val3.Name.ToLower() == "source")
                        {
                            XmlAttributeCollection attributes2 = val3.Attributes;
                            LavaSource lavaSource = new LavaSource();
                            foreach (XmlAttribute item3 in attributes2)
                            {
                                XmlAttribute val4 = item3;
                                switch (val4.Name.ToLower())
                                {
                                    case "x":
                                        try
                                        {
                                            lavaSource.X = int.Parse(val4.Value);
                                        }
                                        catch {}
                                        break;
                                    case "y":
                                        try
                                        {
                                            lavaSource.Y = int.Parse(val4.Value);
                                        }
                                        catch {}
                                        break;
                                    case "z":
                                        try
                                        {
                                            lavaSource.Z = int.Parse(val4.Value);
                                        }
                                        catch {}
                                        break;
                                    case "type":
                                        lavaSource.Type = val4.Value;
                                        break;
                                    case "block":
                                        lavaSource.Block = val4.Value;
                                        break;
                                    case "delay":
                                        try
                                        {
                                            lavaSource.Delay = int.Parse(val4.Value);
                                        }
                                        catch {}
                                        break;
                                }
                            }
                            list.Add(lavaSource);
                        }
                        if (!(val3.Name.ToLower() == "command"))
                        {
                            continue;
                        }
                        XmlAttributeCollection attributes3 = val3.Attributes;
                        LavaCommand lavaCommand = new LavaCommand();
                        foreach (XmlAttribute item4 in attributes3)
                        {
                            XmlAttribute val5 = item4;
                            switch (val5.Name.ToLower())
                            {
                                case "command":
                                    lavaCommand.Command = val5.Value;
                                    break;
                                case "delay":
                                    try
                                    {
                                        lavaCommand.Delay = int.Parse(val5.Value);
                                    }
                                    catch {}
                                    break;
                                case "phase":
                                    try
                                    {
                                        lavaCommand.Phase = byte.Parse(val5.Value);
                                    }
                                    catch {}
                                    break;
                            }
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
                if (!Server.CLI)
                {
                    Window.thisWindow.UpdateLavaMaps();
                }
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

        public static void VerifyMapNames()
        {
            DirectoryInfo directoryInfo =
                new DirectoryInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location.Remove(Assembly.GetExecutingAssembly().Location.Length - 15) +
                                                        "/lava/maps/"));
            FileInfo[] files = directoryInfo.GetFiles();
            var list = new List<string>();
            FileInfo[] array = files;
            foreach (FileInfo fileInfo in array)
            {
                string text = fileInfo.Name.Substring(fileInfo.Name.LastIndexOf('.'));
                if (text == ".lvl")
                {
                    list.Add(fileInfo.Name.ToLower().Remove(fileInfo.Name.LastIndexOf('.')));
                }
            }
            var list2 = new List<LavaMap>();
            foreach (LavaMap lavaMap in lavaMaps)
            {
                if (!list.Contains(lavaMap.Name))
                {
                    list2.Add(lavaMap);
                }
            }
            foreach (LavaMap item in list2)
            {
                lavaMaps.Remove(item);
            }
            list2.Clear();
            list.Clear();
        }

        public static void ConvertLavaMaps()
        {
            if (File.Exists("lava/lavamaps.txt") || !LoadLavaMaps())
            {
                return;
            }
            foreach (lmaps oldLavaMap in oldLavaMaps)
            {
                lavaMaps.Add(new LavaMap(oldLavaMap.mapName, oldLavaMap.timeBefore, oldLavaMap.timeAfter, new List<LavaSource>
                {
                    new LavaSource("", oldLavaMap.lavaUp == 1 ? "creeping" : "ahl", oldLavaMap.x, oldLavaMap.y, oldLavaMap.z, 0)
                }));
            }
            SaveLavaMapsXML();
        }

        public static bool LoadLavaMaps()
        {
            if (File.Exists("lava/lavamaps.txt"))
            {
                StreamReader streamReader = new StreamReader("lava/lavamaps.txt");
                oldLavaMaps.Clear();
                string text;
                while ((text = streamReader.ReadLine()) != null)
                {
                    if (text.Split(':').Length > 3 && text.Split(':').Length < 8)
                    {
                        text = text.Replace(" ", "");
                        string[] array = text.Split(':');
                        lmaps item = default(lmaps);
                        try
                        {
                            item.mapName = array[0];
                            item.x = ushort.Parse(array[1]);
                            item.y = ushort.Parse(array[2]);
                            item.z = ushort.Parse(array[3]);
                            item.timeBefore = (ushort)(array.Length > 4 ? ushort.Parse(array[4]) : 0);
                            item.timeAfter = (ushort)(array.Length > 5 ? ushort.Parse(array[5]) : 0);
                            item.lavaUp = (byte)(array.Length <= 6 ? 3 :
                                array[6].ToLower() == "true" || array[6].ToLower() == "1" ? 1 :
                                !(array[6].ToLower() == "false") && !(array[6].ToLower() == "0") ? 3 : 0);
                            item.rage = (byte)(array.Length <= 7 ? 3 :
                                array[7].ToLower() == "true" || array[6].ToLower() == "1" ? 1 :
                                !(array[6].ToLower() == "false") && !(array[6].ToLower() == "0") ? 3 : 0);
                            item.lavaAmount = array.Length > 8 ? int.Parse(array[8]) : 0;
                            item.lavaDelay = array.Length > 9 ? int.Parse(array[9]) : 0;
                            oldLavaMaps.Add(item);
                        }
                        catch {}
                    }
                }
                streamReader.Dispose();
                streamReader.Close();
                return true;
            }
            return false;
        }

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
            int num = moodRandom.Next(0, 100);
            if (num < chanceCalm)
            {
                LavaSettings.All.LavaState = LavaState.Calm;
            }
            else if (num < chanceCalm + chanceDisturbed)
            {
                LavaSettings.All.LavaState = LavaState.Disturbed;
            }
            else if (num < chanceCalm + chanceDisturbed + chanceWild)
            {
                LavaSettings.All.LavaState = LavaState.Furious;
            }
            else
            {
                LavaSettings.All.LavaState = LavaState.Wild;
            }
        }

        public static void PrintLavaMood()
        {
            switch (LavaSettings.All.LavaState)
            {
                case LavaState.Calm:
                    Player.GlobalMessageLevel(Server.LavaLevel, MessagesManager.GetString("LavaStateCalm"));
                    break;
                case LavaState.Disturbed:
                    Player.GlobalMessageLevel(Server.LavaLevel, MessagesManager.GetString("LavaStateDisturbed"));
                    break;
                case LavaState.Furious:
                    Player.GlobalMessageLevel(Server.LavaLevel, MessagesManager.GetString("LavaStateFurious"));
                    break;
                case LavaState.Wild:
                    Player.GlobalMessageLevel(Server.LavaLevel, MessagesManager.GetString("LavaStateWild"));
                    break;
            }
        }

        public static void PrintMapRating()
        {
            using (DataTable dataTable = DBInterface.fillData("SELECT * FROM `Rating" + Server.LavaLevel.name + "` WHERE Vote = 1", skipError: true))
            {
                using (DataTable dataTable2 = DBInterface.fillData("SELECT * FROM `Rating" + Server.LavaLevel.name + "` WHERE Vote = 2", skipError: true))
                {
                    Player.GlobalMessageLevel(Server.LavaLevel, string.Format(Lang.LavaSystem.MapRatingResults, dataTable.Rows.Count, dataTable2.Rows.Count));
                    Player.GlobalMessageLevel(Server.LavaLevel, Lang.LavaSystem.MapRatingTip);
                }
            }
        }

        public static void PrintMapAuthor()
        {
            Player.GlobalMessageLevel(Server.LavaLevel, string.Format(Lang.LavaSystem.MapName, currentlvl.name));
            Player.GlobalMessageLevel(Server.LavaLevel, string.Format(Lang.LavaSystem.MapAuthor, currentLavaMap.Author));
        }

        public static void CountScoreDefault(Player p, int blocksAround)
        {
            p.score = 150 + blocksAround + p.lives * 20;
        }

        public static void PayRewardDefault(Player p, int rewardDifference)
        {
            if (p.IsAboveSeaLevel)
            {
                if (p.IronChallenge != 0)
                {
                    p.money += LavaSettings.All.RewardAboveSeaLevel * 2;
                    Player.SendMessage(p, string.Format(Lang.LavaSystem.RewardMessageAboveSea, LavaSettings.All.RewardAboveSeaLevel * 2, Server.moneys));
                }
                else
                {
                    p.money += LavaSettings.All.RewardAboveSeaLevel;
                    Player.SendMessage(p, string.Format(Lang.LavaSystem.RewardMessageAboveSea, LavaSettings.All.RewardAboveSeaLevel, Server.moneys));
                }
            }
            else if (p.IronChallenge != 0)
            {
                p.money += LavaSettings.All.RewardBelowSeaLevel * 2;
                Player.SendMessage(
                    p,
                    string.Format(Lang.LavaSystem.RewardMessageBelowSea, LavaSettings.All.RewardBelowSeaLevel * 2, Server.moneys,
                                  rewardDifference < 1 ? "" : string.Format(Lang.LavaSystem.RewardMessageBelowSea2, rewardDifference)));
            }
            else
            {
                p.money += LavaSettings.All.RewardBelowSeaLevel;
                Player.SendMessage(
                    p,
                    string.Format(Lang.LavaSystem.RewardMessageBelowSea, LavaSettings.All.RewardBelowSeaLevel, Server.moneys,
                                  rewardDifference < 1 ? "" : string.Format(Lang.LavaSystem.RewardMessageBelowSea2, rewardDifference)));
            }
        }

        public static void OnCountScore(Player p, int blocksAround)
        {
            if (CountScore != null)
            {
                CountScore(p, blocksAround);
            }
        }

        public static void OnPayReward(Player p, int rewardDifference)
        {
            if (PayReward != null)
            {
                PayReward(p, rewardDifference);
            }
        }

        public static void CheckWinners()
        {
            winnersList.Clear();
            lock (Player.players)
            {
                Player.players.ForEach(delegate(Player who)
                {
                    if (Server.LavaLevel == who.level && who.lives > 0 && !who.hidden && !who.invincible && CheckSpawn(who))
                    {
                        if (LavaSettings.All.ScoreMode == ScoreSystem.BasedOnAir)
                        {
                            OnCountScore(who, (int)CheckBlocksAround(who));
                            if (LavaSettings.All.StarSystem)
                            {
                                if (who.winningStreak == 1)
                                {
                                    who.score = (int)(who.score * 1.1f);
                                }
                                else if (who.winningStreak == 2)
                                {
                                    who.score = (int)(who.score * 1.2f);
                                }
                                else if (who.winningStreak >= 3)
                                {
                                    who.score = (int)(who.score * 1.3f);
                                }
                            }
                            who.totalScore += who.score;
                            if (who.score > who.bestScore)
                            {
                                who.bestScore = who.score;
                            }
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

        public static void AnnounceWinners()
        {
            if (winnersList.Count <= 0)
            {
                return;
            }
            int index = 0;
            if (LavaSettings.All.ScoreMode == ScoreSystem.BasedOnAir)
            {
                Player.GlobalMessageLevel(currentlvl, MessagesManager.GetString("TopWinners"));
                int num = 0;
                if (winnersList.Count > 6)
                {
                    num = winnersList.Count - 6;
                }
                for (int i = num; i < winnersList.Count - 1; i++)
                {
                    Player.GlobalMessageLevel(currentlvl, string.Format(MessagesManager.GetString("ScoreResult"), winnersList[i].name, winnersList[i].score));
                    Server.s.Log(winnersList[i].name + " (Score: " + winnersList[i].score + ")");
                }
                Player.GlobalMessageLevel(
                    currentlvl,
                    string.Format(MessagesManager.GetString("ScoreResultBest"), winnersList[winnersList.Count - 1].name, winnersList[winnersList.Count - 1].score));
                Server.s.Log(winnersList[index].name + " This round best! (Score: " + winnersList[winnersList.Count - 1].score + ")");
            }
            else
            {
                Player.GlobalMessageLevel(currentlvl, MessagesManager.GetString("WinnersList"));
                Server.s.Log("Winners list:");
                foreach (Winner winners in winnersList)
                {
                    Player.GlobalMessageLevel(currentlvl, "%c" + winners.name);
                    Server.s.Log(winners.name);
                }
            }
            int rewardDifference = 0;
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
                            {
                                Player.SendMessage(
                                    who, string.Format(MessagesManager.GetString("ExperienceGain"), LavaSettings.All.ScoreRewardFixed + Server.DefaultColor));
                            }
                            else
                            {
                                Player.SendMessage(who, string.Format(MessagesManager.GetString("ExperienceGain"), who.score + Server.DefaultColor));
                            }
                            who.winningStreak++;
                        }
                        else
                        {
                            Player.SendMessage(who, string.Format(Lang.LavaSystem.WarningTooCloseToSpawn, LavaSettings.All.RequiredDistanceFromSpawn));
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

        public static bool CheckSpawn(Player who)
        {
            ushort[] pos = who.pos;
            int num = Math.Abs(pos[0] / 32 - who.level.spawnx);
            int num2 = Math.Abs(pos[1] / 32 - who.level.spawny);
            int num3 = Math.Abs(pos[2] / 32 - who.level.spawnz);
            if (num < LavaSettings.All.RequiredDistanceFromSpawn && num2 < LavaSettings.All.RequiredDistanceFromSpawn && num3 < LavaSettings.All.RequiredDistanceFromSpawn)
            {
                return false;
            }
            return true;
        }

        static long CheckBlocksAround(Player who)
        {
            ushort[] pos = who.pos;
            ushort x = (ushort)(pos[0] / 32);
            ushort y = (ushort)(pos[1] / 32 - 1);
            ushort z = (ushort)(pos[2] / 32);
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
                    int num = 0;
                    int num2 = 0;
                    while (blockCheck.Count > 0)
                    {
                        num2 = blockCheck.Count;
                        num = 0;
                        for (int i = 0; i < num2; i++)
                        {
                            if (count > 1500)
                            {
                                goto end_IL_0141;
                            }
                            Test2(blockCheck[i].xChk, blockCheck[i].yChk, blockCheck[i].zChk, who);
                            num++;
                        }
                        blockCheck.RemoveRange(0, num);
                        continue;
                        end_IL_0141:
                        break;
                    }
                }
                else
                {
                    blocksArray = new byte[who.level.blocks.Length];
                    new List<bCheck>();
                    Test2(x, y, z, who);
                    int num3 = 0;
                    int num4 = 0;
                    while (blockCheck.Count > 0)
                    {
                        num4 = blockCheck.Count;
                        num3 = 0;
                        for (int j = 0; j < num4; j++)
                        {
                            if (stopTest)
                            {
                                goto end_IL_01f7;
                            }
                            Test3(blockCheck[j].xChk, blockCheck[j].yChk, blockCheck[j].zChk, who);
                            num3++;
                        }
                        blockCheck.RemoveRange(0, num3);
                        continue;
                        end_IL_01f7:
                        break;
                    }
                }
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
                    count = 1000 + (int)((count - 1000) * 0.3);
                }
                if (countBelow > 7000)
                {
                    countBelow = 7000L;
                }
                long result = count + countBelow / 7;
                blockCheck.Clear();
                blocks.Clear();
                count = 0L;
                countBelow = 0L;
                stopTest = false;
                return result;
            }
            return 0L;
        }

        public static string GetCheckBlocksCount()
        {
            return blockCheck.Count.ToString();
        }

        public static bool IsMapLoaded(string mapName)
        {
            foreach (Level level in Server.levels)
            {
                if (level.name == currentMap)
                {
                    Server.s.Log(currentMap + " is already loaded!");
                    return true;
                }
            }
            return false;
        }

        public static void Test2(int x, int y, int z, Player who)
        {
            posToInt = who.level.PosToInt(x + 1, y, z);
            b = who.level.GetTile(posToInt);
            if (b == 0)
            {
                if (blocksArray[posToInt] == 0)
                {
                    if (posToInt > halfmap)
                    {
                        count++;
                    }
                    else
                    {
                        countBelow++;
                    }
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
                    {
                        count++;
                    }
                    else
                    {
                        countBelow++;
                    }
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
                    {
                        count++;
                    }
                    else
                    {
                        countBelow++;
                    }
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
                {
                    count++;
                }
                else
                {
                    countBelow++;
                }
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
                    {
                        count++;
                    }
                    else
                    {
                        countBelow++;
                    }
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
                    {
                        count++;
                    }
                    else
                    {
                        countBelow++;
                    }
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

        public static void Test3(int x, int y, int z, Player who)
        {
            if (count > 1500)
            {
                return;
            }
            posToInt = who.level.PosToInt(x + 1, y, z);
            b = who.level.GetTile(posToInt);
            if (b == 0 && blocksArray[posToInt] == 0)
            {
                if (posToInt > halfmap)
                {
                    count++;
                }
                else
                {
                    countBelow++;
                }
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
                {
                    count++;
                }
                else
                {
                    countBelow++;
                }
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
                {
                    count++;
                }
                else
                {
                    countBelow++;
                }
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
                {
                    count++;
                }
                else
                {
                    countBelow++;
                }
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
                {
                    count++;
                }
                else
                {
                    countBelow++;
                }
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
                {
                    count++;
                }
                else
                {
                    countBelow++;
                }
                blocksArray[posToInt] = 1;
                blockCheck.Add(new bCheck
                {
                    xChk = x,
                    yChk = y,
                    zChk = z - 1
                });
            }
        }

        public static void Test(ushort x, ushort y, ushort z, Player who)
        {
            if (count > 1500)
            {
                return;
            }
            if (who.level.GetTile((ushort)(x + 1), y, z) == 0 && !blocks.Contains(who.level.PosToInt((ushort)(x + 1), y, z)))
            {
                if (who.level.PosToInt((ushort)(x + 1), y, z) > halfmap)
                {
                    count++;
                }
                else
                {
                    countBelow++;
                }
                blocks.Add(who.level.PosToInt((ushort)(x + 1), y, z));
                Test((ushort)(x + 1), y, z, who);
            }
            if (who.level.GetTile((ushort)(x - 1), y, z) == 0 && !blocks.Contains(who.level.PosToInt((ushort)(x - 1), y, z)))
            {
                if (who.level.PosToInt((ushort)(x - 1), y, z) > halfmap)
                {
                    count++;
                }
                else
                {
                    countBelow++;
                }
                blocks.Add(who.level.PosToInt((ushort)(x - 1), y, z));
                Test((ushort)(x - 1), y, z, who);
            }
            if (who.level.GetTile(x, (ushort)(y + 1), z) == 0 && !blocks.Contains(who.level.PosToInt(x, (ushort)(y + 1), z)))
            {
                if (who.level.PosToInt(x, (ushort)(y + 1), z) > halfmap)
                {
                    count++;
                }
                else
                {
                    countBelow++;
                }
                blocks.Add(who.level.PosToInt(x, (ushort)(y + 1), z));
                Test(x, (ushort)(y + 1), z, who);
            }
            if (who.level.GetTile(x, (ushort)(y - 1), z) == 0 && !blocks.Contains(who.level.PosToInt(x, (ushort)(y - 1), z)))
            {
                if (who.level.PosToInt(x, (ushort)(y - 1), z) > halfmap)
                {
                    count++;
                }
                else
                {
                    countBelow++;
                }
                blocks.Add(who.level.PosToInt(x, (ushort)(y - 1), z));
                Test(x, (ushort)(y - 1), z, who);
            }
            switch (who.level.GetTile(x, y, (ushort)(z + 1)))
            {
                case 194:
                    count = 2L;
                    countBelow = 2L;
                    stopTest = true;
                    return;
                case 0:
                    if (!blocks.Contains(who.level.PosToInt(x, y, (ushort)(z + 1))))
                    {
                        if (who.level.PosToInt(x, y, (ushort)(z + 1)) > halfmap)
                        {
                            count++;
                        }
                        else
                        {
                            countBelow++;
                        }
                        blocks.Add(who.level.PosToInt(x, y, (ushort)(z + 1)));
                        Test(x, y, (ushort)(z + 1), who);
                    }
                    break;
            }
            if (who.level.GetTile(x, y, (ushort)(z - 1)) == 0 && !blocks.Contains(who.level.PosToInt(x, y, (ushort)(z - 1))))
            {
                if (who.level.PosToInt(x, y, (ushort)(z - 1)) > halfmap)
                {
                    count++;
                }
                else
                {
                    countBelow++;
                }
                blocks.Add(who.level.PosToInt(x, y, (ushort)(z - 1)));
                Test(x, y, (ushort)(z - 1), who);
            }
        }

        public static int Voting()
        {
            if (lavaMaps.Count < 4)
            {
                return -1;
            }
            int[] array = new int[lavaMaps.Count];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = i;
            }
            Random random = new Random();
            int[] array2 = new int[3];
            int num = random.Next(0, lavaMaps.Count);
            array2[0] = array[num];
            array[num] = array[array.Length - 1];
            num = random.Next(0, lavaMaps.Count - 1);
            array2[1] = array[num];
            array[num] = array[array.Length - 2];
            num = random.Next(0, lavaMaps.Count - 2);
            array2[2] = array[num];
            Server.LavaLevel.ChatLevel(Lang.LavaSystem.VoteForNextMap);
            Server.LavaLevel.ChatLevel(string.Format(Lang.LavaSystem.VoteOptions, Server.DefaultColor, lavaMaps[array2[0]].Name, lavaMaps[array2[1]].Name,
                                                     lavaMaps[array2[2]].Name));
            Server.s.Log("Vote for the next map");
            Server.s.Log("Write 1 for " + lavaMaps[array2[0]].Name + ", 2 for " + lavaMaps[array2[1]].Name + ", 3 for " + lavaMaps[array2[2]].Name);
            Server.voteMode = true;
            Thread.Sleep(25000);
            Server.voteMode = false;
            Server.LavaLevel.ChatLevel(Lang.LavaSystem.VoteResults);
            Server.LavaLevel.ChatLevel(string.Format(Lang.LavaSystem.VoteResults2, lavaMaps[array2[0]].Name, votes[0], lavaMaps[array2[1]].Name, votes[1],
                                                     lavaMaps[array2[2]].Name, votes[2]));
            Server.s.Log("Results of voting:");
            Server.s.Log("Map: " + lavaMaps[array2[0]].Name + " - " + votes[0] + ", " + lavaMaps[array2[1]].Name + " - " + votes[1] + ", " + lavaMaps[array2[2]].Name +
                         " - " + votes[2] + " votes.");
            int num2 = votes[0] >= votes[1] ? votes[0] < votes[2] ? 2 : 0 : votes[1] >= votes[2] ? 1 : 2;
            Thread.Sleep(4000);
            Server.LavaLevel.ChatLevel(string.Format(Lang.LavaSystem.MapNext, lavaMaps[array2[num2]].Name));
            Server.s.Log("The next map is: " + lavaMaps[array2[num2]].Name);
            votes[0] = 0;
            votes[1] = 0;
            votes[2] = 0;
            votersList.Clear();
            return array2[num2];
        }

        public static bool CountVotes(string vote, Player p)
        {
            if (votersList.Contains(p.name.ToLower()))
            {
                if (p.group.Permission >= LevelPermission.Operator)
                {
                    return false;
                }
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

        public static void EarthQuake(byte opt = 1)
        {
            Thread thread = new Thread(StartQuake);
            if (opt == 2)
            {
                thread = new Thread(StartQuake2);
            }
            thread.Start();
        }

        static void StartQuake()
        {
            int num = 0;
            int num2 = Server.LavaLevel.PosToInt(0, Server.LavaLevel.height / 2 - 1, 0);
            DateTime dateTime = DateTime.Now.AddMinutes(3.0);
            DateTime now = DateTime.Now;
            for (int i = num2; i < Server.LavaLevel.blocks.Length; i++)
            {
                if (dateTime < DateTime.Now)
                {
                    break;
                }
                if (rand.Next(1, 16) == 5)
                {
                    byte b = Server.LavaLevel.blocks[i];
                    if (b != 105 && b != 7 && b != 0 && b != 194)
                    {
                        Server.LavaLevel.AddUpdate(i, 0, overRide: true);
                        num++;
                    }
                }
                if (num > 500)
                {
                    Thread.Sleep(250);
                    num = 0;
                }
            }
            Server.s.Log("EarthQuake Time taken: " + (DateTime.Now - now).TotalMilliseconds);
        }

        static void StartQuake2()
        {
            int num = Server.LavaLevel.PosToInt(0, Server.LavaLevel.height / 2 - 1, 0);
            int num2 = num / 15;
            int maxValue = Server.LavaLevel.blocks.Length;
            int num3 = 0;
            DateTime dateTime = DateTime.Now.AddMinutes(3.0);
            DateTime now = DateTime.Now;
            Player.players.ForEach(delegate(Player who)
            {
                if (who.level == Server.LavaLevel)
                {
                    who.frozen = true;
                }
            });
            Thread.Sleep(2500);
            Player.players.ForEach(delegate(Player who) { who.frozen = false; });
            Thread.Sleep(1500);
            for (int i = 0; i < num2; i++)
            {
                if (dateTime < DateTime.Now)
                {
                    break;
                }
                int num4 = rand.Next(num, maxValue);
                byte b = Server.LavaLevel.blocks[num4];
                if (b != 105 && b != 7 && b != 0 && b != 194)
                {
                    Server.LavaLevel.AddUpdate(num4, 0, overRide: true);
                    num3++;
                }
                if (num3 > 500)
                {
                    Thread.Sleep(250);
                    num3 = 0;
                }
            }
            Server.s.Log("EarthQuake Time taken: " + (DateTime.Now - now).TotalMilliseconds);
        }

        static void PrepareCreepingLava(LavaMap lMap)
        {
            foreach (LavaSource lavaSource in lMap.LavaSources)
            {
                if (lavaSource.Block == "creeping")
                {
                    iOffset = 1;
                    while (currentlvl.GetTile(lavaSource.X, lavaSource.Y + iOffset, lavaSource.Z) == 0)
                    {
                        currentlvl.Blockchange((ushort)lavaSource.X, (ushort)(lavaSource.Y + iOffset), (ushort)lavaSource.Z, 105);
                        iOffset++;
                    }
                    break;
                }
            }
        }

        static void PlaceCreepingLava(object lavaSource)
        {
            LavaSource lavaSource2 = (LavaSource)lavaSource;
            int millisecondsTimeout = 30000;
            int num = 0;
            while (num < iOffset)
            {
                for (int i = 0; i < amountOffset; i++)
                {
                    if (num < iOffset)
                    {
                        currentlvl.Blockchange((ushort)lavaSource2.X, (ushort)(lavaSource2.Y + num), (ushort)lavaSource2.Z, 194);
                        if (!phase2holder)
                        {
                            return;
                        }
                    }
                    num++;
                }
                Thread.Sleep(millisecondsTimeout);
            }
        }

        static void CreepingLava(LavaSource lSource)
        {
            new Thread(PlaceCreepingLava).Start(lSource);
        }

        public List<string> LoadVipList()
        {
            FileUtil.CreateIfNotExists("ranks/viplist.txt");
            string[] source = File.ReadAllLines("ranks/viplist.txt");
            return source.ToList();
        }

        public void SaveVipList(List<string> vips)
        {
            using (StreamWriter streamWriter = new StreamWriter("ranks/viplist.txt"))
            {
                foreach (string vip in vips)
                {
                    streamWriter.WriteLine(vip);
                }
            }
        }

        public static void LoadHeaven()
        {
            Server.heavenMap = Level.Load(Server.heavenMapName, 4, lavaSurv: true);
            if (Server.heavenMap == null)
            {
                Server.s.Log("Could not find the heaven map.");
                Server.useHeaven = false;
            }
            else
            {
                Server.AddLevel(Server.heavenMap);
                Server.heavenMap.unload = false;
            }
        }

        public static void StartServerMessage()
        {
            if (Server.serverMessageInterval <= 0)
            {
                return;
            }
            if (!serverMessageTimer.Enabled && Server.serverMessage != "")
            {
                serverMessageTimer.Elapsed += delegate
                {
                    string[] array = Server.serverMessage.Split(new string[1]
                    {
                        "\r\n"
                    }, 10, StringSplitOptions.RemoveEmptyEntries);
                    string[] array2 = array;
                    foreach (string message in array2)
                    {
                        Player.GlobalChatWorld(null, message, showname: false);
                    }
                };
                serverMessageTimer.Start();
            }
            serverMessageTimer.Interval = Server.serverMessageInterval * 60000;
        }

        public static void UpdateServerMessageInterval()
        {
            if (Server.serverMessageInterval > 0)
            {
                serverMessageTimer.Interval = Server.serverMessageInterval * 60000;
            }
        }

        public static void StopServerMessage()
        {
            serverMessageTimer.Stop();
        }

        public static void SetMapIndex(int index)
        {
            ii = index;
        }

        public static bool RankUp(Player p)
        {
            try
            {
                var list = new List<Group>();
                list.AddRange(Group.groupList);
                for (int i = 0; i < list.Count; i++)
                {
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
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
            return false;
        }

        public static void FoundTreasure(Player p, int x, int y, int z)
        {
            p.money += LavaSettings.All.AmountOfMoneyInTreasure;
            Player.SendMessage(p, MessagesManager.GetString("TreasureFound"));
            Player.SendMessage(p, string.Format(MessagesManager.GetString("TreasureGain"), LavaSettings.All.AmountOfMoneyInTreasure, Server.moneys));
        }

        class bCheck
        {
            public int xChk;

            public int yChk;

            public int zChk;
        }

        public struct lmaps
        {
            public string mapName;

            public int lavaAmount;

            public int lavaDelay;

            public ushort x;

            public ushort y;

            public ushort z;

            public ushort timeBefore;

            public ushort timeAfter;

            public byte rage;

            public byte lavaUp;
        }

        public struct Winner
        {
            public string name;

            public int score;
        }

        public class LavaSource
        {

            public LavaSource() {}

            public LavaSource(string type, string block, int x, int y, int z, int delay)
            {
                Type = type;
                Block = block;
                X = x;
                Y = y;
                Z = z;
                Delay = delay;
            }
            public string Type { get; set; }

            public string Block { get; set; }

            public int X { get; set; }

            public int Y { get; set; }

            public int Z { get; set; }

            public int Delay { get; set; }
        }

        public class LavaCommand
        {
            public string Command { get; set; }

            public int Delay { get; set; }

            public byte Phase { get; set; }
        }

        public class LavaMap
        {

            public LavaMap() {}

            public LavaMap(string name, int phase1, int phase2, List<LavaSource> lavaSources)
            {
                Name = name;
                Phase1 = phase1;
                Phase2 = phase2;
                LavaSources = lavaSources;
            }

            public LavaMap(string name, int phase1, int phase2, List<LavaSource> lavaSources, List<LavaCommand> lavaCommands)
            {
                Name = name;
                Phase1 = phase1;
                Phase2 = phase2;
                LavaSources = lavaSources;
                LavaCommands = lavaCommands;
            }
            public string Name { get; set; }

            public string Author { get; set; }

            public int Phase1 { get; set; }

            public int Phase2 { get; set; }

            public List<LavaSource> LavaSources { get; set; }

            public List<LavaCommand> LavaCommands { get; set; }
        }
    }
}