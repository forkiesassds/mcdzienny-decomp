using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using MCDzienny.Database;
using MCDzienny.Games;
using MCDzienny.Gui;
using MCDzienny.InfectionSystem;
using MCDzienny.Misc;
using MCDzienny.Notification;
using MCDzienny.Plugins;
using MCDzienny.RemoteAccess;
using MCDzienny.Settings;
using MonoTorrent.Client;
using MySql.Data.MySqlClient;
using Timer = System.Timers.Timer;

namespace MCDzienny
{
    public sealed class Server
    {

        public delegate void HeartBeatHandler();

        public delegate void LogHandler(string message);

        public delegate void MapChangedHandler();

        public delegate void MessageEventHandler(string message);

        public delegate void PlayerListHandler();

        public delegate void VoidHandler();

        public const byte version = 7;

        public static readonly bool RunInRaceMode;

        public static PluginManager Plugins;

        static DevMessages devMessages;

        public static object levelListLocker;

        public static ChatFilter chatFilter;

        public static SimpleAccountManager remoteAccounts;

        public static SimpleKeyValueManager.SimpleKeyValueManager constantsForChat;

        public static BanListManager bannedRemoteAccounts;

        public static bool restarting;

        public static volatile Mode mode;

        public static string zombieAlias;

        public static bool CLI;

        public static int lavaSurvivalPlayerLimit;

        public static bool useHeaven;

        public static string heavenMapName;

        public static Level heavenMap;

        public static string serverMessage;

        public static int serverMessageInterval;

        public static bool voteMode;

        public static byte vipSystem;

        public static int packetSent;

        public static bool pause;

        public static DateTime serverLockTime;

        static readonly ASCIIEncoding enc;

        public static Timer locationChecker;

        public static Timer blockThread;

        public static List<MySqlCommand> mySQLCommands;

        public static int speedPhysics;

        public static Socket listen;

        public static Process process;

        public static Timer updateTimer;

        static readonly Timer messageTimer;

        static readonly Timer adTimer;

        public static Timer cloneTimer;

        static Timer packetUpdate;

        public static List<CTFGame> CTFGames;

        public static PlayerList bannedIP;

        public static PlayerList whiteList;

        public static PlayerList greenList;

        public static PlayerList ircControllers;

        public static List<string> devs;

        public static List<TempBan> tempBans;

        public static MapGenerator MapGen;

        public static PerformanceCounter PCCounter;

        public static PerformanceCounter ProcessCounter;

        public static Level mainLevel;

        public static LevelCollection levels;

        public static HashSet<string> afkset;

        public static List<string> afkmessages;

        public static List<string> messages;

        public static List<string> lavaTimedMessages;

        public static List<string> zombieTimedMessages;

        public static DateTime TimeOnline;

        public static bool autoupdate;

        public static bool autonotify;

        public static string restartcountdown;

        public static string selectedrevision;

        public static bool autorestart;

        public static DateTime restarttime;

        public static bool chatmod;

        public static bool hardcore;

        public static volatile string salt;

        static readonly RNGCryptoServiceProvider rngCrypto;

        static readonly MD5CryptoServiceProvider md5;

        static string salt2;

        public static string description;

        public static bool autoFlag;

        static string flag;

        public static string serverRealName;

        public static string name;

        public static string motd;

        public static byte players;

        public static byte maps;

        public static int port;

        public static bool isPublic;

        public static bool verify;

        public static bool worldChat;

        public static bool guestGoto;

        public static string ConsoleName;

        public static string level;

        public static bool console;

        public static bool reportBack;

        public static bool irc;

        public static int ircPort;

        public static string ircNick;

        public static string ircServer;

        public static string ircChannel;

        public static string ircOpChannel;

        public static bool ircIdentify;

        public static string ircPassword;

        public static bool restartOnError;

        public static bool antiTunnel;

        public static byte maxDepth;

        public static int Overload;

        public static int rpLimit;

        public static int rpNormLimit;

        public static int backupInterval;

        public static int blockInterval;

        public static string backupLocation;

        public static bool physicsRestart;

        public static bool deathcount;

        public static bool AutoLoad;

        public static int physUndo;

        public static int totalUndo;

        public static bool rankSuper;

        public static bool oldHelp;

        public static bool parseSmiley;

        public static bool useWhitelist;

        public static bool forceCuboid;

        public static bool repeatMessage;

        public static bool checkUpdates;

        public static bool useMySQL;

        public static string MySQLHost;

        public static string MySQLPort;

        public static string MySQLUsername;

        public static string MySQLPassword;

        public static string MySQLDatabaseName;

        public static bool MySQLPooling;

        public static string DefaultColor;

        public static string IRCColour;

        public static int afkminutes;

        public static int afkkick;

        public static string defaultRank;

        public static bool useDollarSign;

        public static bool cheapMessage;

        public static string cheapMessageGiven;

        public static bool customBan;

        public static string customBanMessage;

        public static bool customShutdown;

        public static string customShutdownMessage;

        public static string moneys;

        public static LevelPermission opchatperm;

        public static bool logbeat;

        public static bool mono;

        public static bool flipHead;

        public static volatile bool shuttingDown;

        static readonly Random rand;

        public static MainLoop mainLoop;

        public static Server s;

        readonly object afkLocker = new object();

        readonly Timer afkTimer = new Timer(2000.0);

        readonly Timer pingTimer = new Timer(2000.0);

        BugPlaceType bugPlace;

        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

        static Server()
        {
            RunInRaceMode = false;
            Plugins = new PluginManager();
            levelListLocker = new object();
            restarting = false;
            mode = Mode.Lava;
            zombieAlias = "zombie";
            CLI = false;
            lavaSurvivalPlayerLimit = 0;
            useHeaven = false;
            heavenMapName = "";
            serverMessage = "";
            serverMessageInterval = 10;
            voteMode = false;
            vipSystem = 1;
            pause = false;
            enc = new ASCIIEncoding();
            mySQLCommands = new List<MySqlCommand>();
            speedPhysics = 250;
            process = Process.GetCurrentProcess();
            updateTimer = new Timer(100.0);
            messageTimer = new Timer(210000.0);
            adTimer = new Timer(1500000.0);
            cloneTimer = new Timer(5000.0);
            packetUpdate = new Timer(5000.0);
            CTFGames = new List<CTFGame>();
            whiteList = new PlayerList();
            devs = new List<string>
            {
                "dzienny"
            };
            tempBans = new List<TempBan>();
            PCCounter = null;
            ProcessCounter = null;
            afkset = new HashSet<string>();
            afkmessages = new List<string>();
            messages = new List<string>();
            lavaTimedMessages = new List<string>();
            zombieTimedMessages = new List<string>();
            restartcountdown = "";
            selectedrevision = "";
            chatmod = false;
            hardcore = false;
            salt = null;
            rngCrypto = new RNGCryptoServiceProvider();
            md5 = new MD5CryptoServiceProvider();
            description = "Come and join the fun!";
            autoFlag = true;
            flag = "LAVA";
            serverRealName = "Minecraft Server(MCDzienny)";
            name = "Minecraft Server(MCDzienny)";
            motd = "Welcome! &0 -hax +ophax";
            players = 15;
            maps = 5;
            port = 25564;
            isPublic = true;
            verify = true;
            worldChat = true;
            guestGoto = false;
            ConsoleName = "Alive";
            level = "main";
            console = false;
            reportBack = true;
            irc = false;
            ircPort = 6667;
            ircNick = "MCDzienny_Minecraft_Bot";
            ircServer = "irc.esper.net";
            ircChannel = "#changethis";
            ircOpChannel = "#changethistoo";
            ircIdentify = false;
            ircPassword = "";
            restartOnError = true;
            antiTunnel = false;
            maxDepth = 4;
            Overload = 99000;
            rpLimit = 500;
            rpNormLimit = 10000;
            backupInterval = 180;
            blockInterval = 60;
            backupLocation = Application.StartupPath + "/levels/backups";
            physicsRestart = true;
            deathcount = true;
            AutoLoad = false;
            physUndo = 60000;
            totalUndo = 200;
            rankSuper = true;
            oldHelp = false;
            parseSmiley = true;
            useWhitelist = false;
            forceCuboid = false;
            repeatMessage = false;
            checkUpdates = true;
            useMySQL = true;
            MySQLHost = "127.0.0.1";
            MySQLPort = "3306";
            MySQLUsername = "root";
            MySQLPassword = "password";
            MySQLDatabaseName = "MCDziennyLava";
            MySQLPooling = true;
            DefaultColor = "&e";
            SecondaryColor = "&4";
            IRCColour = "&5";
            afkminutes = 10;
            afkkick = 45;
            defaultRank = "guest";
            useDollarSign = true;
            cheapMessage = true;
            cheapMessageGiven = " is now being cheap and being immortal";
            customBan = false;
            customBanMessage = "You're banned!";
            customShutdown = false;
            customShutdownMessage = "Server shutdown. Rejoin in 30 seconds.";
            moneys = "gold coins";
            opchatperm = LevelPermission.Operator;
            logbeat = false;
            mono = Environment.OSVersion.Platform == PlatformID.Unix;
            flipHead = false;
            shuttingDown = false;
            rand = new Random();
        }

        public Server()
        {
            s = this;
            mainLoop = new MainLoop("server");
            remoteAccounts = new SimpleAccountManager("remote/accounts.txt");
            bannedRemoteAccounts = new BanListManager("remote/banned.txt");
            string initialContent =
                "# This file contains chat $constants. These constants are substituted\r\n# in a chat for a defined text. For example you can define\r\n# $web constant that when used in text will change into your\r\n# webpage address. \r\n# Format:\r\n# constant_name : text_for_substitution\r\n# Example:\r\n# web : www.toBeDefinedWebpageAddress.com\r\ntest : test passed!";
            constantsForChat = new SimpleKeyValueManager.SimpleKeyValueManager("text/chat_constant$.txt", initialContent);
        }

        public static int UniqueVisits
        {
            get
            {
                using (DataTable dataTable = DBInterface.fillData("SELECT COUNT(Name) FROM Players"))
                {
                    int result;
                    int.TryParse(dataTable.Rows[0]["COUNT(Name)"].ToString(), out result);
                    return result;
                }
            }
        }

        public static string ServerHash { get; set; }

        public static string Version { get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); } }

        public static Level DefaultLevel
        {
            get
            {
                if (mode == Mode.Lava)
                {
                    return LavaLevel;
                }
                if (mode == Mode.Freebuild || mode == Mode.LavaFreebuild || mode == Mode.ZombieFreebuild)
                {
                    return mainLevel;
                }
                return InfectionLevel;
            }
        }

        public static Level LavaLevel { get; set; }

        public static Level InfectionLevel { get; set; }

        [Browsable(false)]
        public static int FreebuildCount
        {
            get
            {
                int tempCount = 0;
                levels.ForEach(delegate(Level l)
                {
                    if (l.mapType == MapType.Freebuild)
                    {
                        tempCount++;
                    }
                });
                return tempCount;
            }
        }

        public static string Salt
        {
            get
            {
                if (salt != null && salt != "")
                {
                    return salt;
                }
                byte[] array = new byte[16];
                rngCrypto.GetBytes(array);
                array = md5.ComputeHash(array);
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < array.Length; i++)
                {
                    stringBuilder.Append(array[i].ToString("X2"));
                }
                salt = stringBuilder.ToString();
                return salt;
            }
            set
            {
                s.Log("Warning: the salt of the server was explicitly set.");
                s.Log("This action may expose your server to hackers attacks.");
                salt = value;
            }
        }

        public static string SaltClassiCube
        {
            get
            {
                if (salt2 != null && salt2 != "")
                {
                    return salt2;
                }
                byte[] array = new byte[16];
                rngCrypto.GetBytes(array);
                array = md5.ComputeHash(array);
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < array.Length; i++)
                {
                    stringBuilder.Append(array[i].ToString("X2"));
                }
                salt2 = stringBuilder.ToString();
                return salt2;
            }
            set
            {
                s.Log("Warning: the salt2 of the server was explicitly set.");
                s.Log("This action may expose your server to hackers attacks.");
                salt2 = value;
            }
        }

        public static string Flag
        {
            get
            {
                if (autoFlag)
                {
                    if (mode == Mode.Lava)
                    {
                        return "LAVA";
                    }
                    if (mode == Mode.Freebuild)
                    {
                        return "FREEBUILD";
                    }
                    if (mode == Mode.LavaFreebuild)
                    {
                        return "LAVA/FREEBUILD";
                    }
                    if (mode == Mode.Zombie)
                    {
                        return "ZOMBIE";
                    }
                    return "LAVA";
                }
                return flag;
            }
            set { flag = value; }
        }

        public static string IP
        {
            get
            {
                string result = "?";
                IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress[] addressList = hostEntry.AddressList;
                foreach (IPAddress iPAddress in addressList)
                {
                    if (iPAddress.AddressFamily == AddressFamily.InterNetwork)
                    {
                        result = iPAddress.ToString();
                    }
                }
                return result;
            }
        }

        public static string ConsoleRealName
        {
            get
            {
                if (GeneralSettings.All.UseCustomName)
                {
                    return GeneralSettings.All.CustomConsoleName + GeneralSettings.All.CustomConsoleNameDelimiter + " ";
                }
                return "Console [&a" + ConsoleName + DefaultColor + "]: &f";
            }
        }

        public static string ConsoleRealNameIRC
        {
            get
            {
                if (GeneralSettings.All.UseCustomName)
                {
                    return GeneralSettings.All.CustomConsoleName + GeneralSettings.All.CustomConsoleNameDelimiter + " ";
                }
                return "Console [" + ConsoleName + DefaultColor + "]: ";
            }
        }

        public static string SecondaryColor { get; set; }

        public event LogHandler OnLog;

        public event LogHandler OnSystem;

        public event LogHandler OnCommand;

        public event LogHandler OnError;

        public event HeartBeatHandler HeartBeatFail;

        public event MessageEventHandler OnURLChange;

        public event PlayerListHandler OnPlayerListChange;

        public event VoidHandler OnSettingsUpdate;

        public event MapChangedHandler MapAdded;

        public event MapChangedHandler MapRemoved;

        public static void ChangeModeToFreebuild()
        {
            string text = "";
            if (mode == Mode.Lava || mode == Mode.LavaFreebuild)
            {
                text = LavaSystem.currentlvl.name;
                LavaSystem.phase1holder = false;
                LavaSystem.phase2holder = false;
                LavaSystem.currentlvl.UnloadLock = false;
                Command.all.Find("unload").Use(null, LavaSystem.currentlvl.name);
            }
            else if (mode == Mode.Zombie)
            {
                InfectionSystem.InfectionSystem.Stop();
            }
            if (Level.Find(level) == null)
            {
                Command.all.Find("load").Use(null, level);
                mainLevel = Level.Find(level);
            }
            int count = Player.players.Count;
            if (mode != Mode.Freebuild)
            {
                mode = Mode.Freebuild;
                for (int num = count - 1; num >= 0; num--)
                {
                    try
                    {
                        if (Player.players[num].level.name == text)
                        {
                            Command.all.Find("goto").Use(Player.players[num], mainLevel.name);
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorLog(ex);
                    }
                }
            }
            mode = Mode.Freebuild;
            if (!CLI)
            {
                Window.thisWindow.toolStripStatusLabelRoundTime.Visible = false;
            }
        }

        public static void ChangeModeToLava()
        {
            string text = "";
            if (mode == Mode.Freebuild)
            {
                text = mainLevel.name;
                LavaSystem.LoadLavaMapsXML();
                LavaSystem.LavaMapInitialization();
                mode = Mode.Lava;
                LavaSystem.phase1holder = true;
                LavaSystem.Start();
                string toMap = LavaSystem.currentLavaMap.Name;
                MoveAllPlayers(text, toMap);
            }
            else if (mode == Mode.LavaFreebuild)
            {
                text = mainLevel.name;
                mode = Mode.Lava;
                MoveAllPlayers(text, LavaSystem.currentLavaMap.Name);
            }
            else if (mode == Mode.Zombie)
            {
                text = DefaultLevel.name;
                LavaSystem.LoadLavaMapsXML();
                LavaSystem.LavaMapInitialization();
                mode = Mode.Lava;
                LavaSystem.phase1holder = true;
                LavaSystem.Start();
                MoveAllPlayers(text, DefaultLevel.name);
                InfectionSystem.InfectionSystem.Stop();
                Command.all.Find("unload").Use(null, text);
            }
            if (!CLI)
            {
                Window.thisWindow.toolStripStatusLabelRoundTime.Visible = true;
            }
        }

        static void MoveAllPlayers(string fromMap, string toMap)
        {
            int count = Player.players.Count;
            for (int num = count - 1; num >= 0; num--)
            {
                try
                {
                    if (Player.players[num].level.name == fromMap)
                    {
                        Command.all.Find("goto").Use(Player.players[num], toMap);
                    }
                }
                catch (Exception ex)
                {
                    ErrorLog(ex);
                }
            }
        }

        public static void ChangeModeToLavaFreebuild()
        {
            if (Level.Find(level) == null)
            {
                Command.all.Find("load").Use(null, level);
                mainLevel = Level.Find(level);
            }
            if (mode == Mode.Freebuild)
            {
                LavaSystem.LoadLavaMapsXML();
                LavaSystem.LavaMapInitialization();
                mode = Mode.LavaFreebuild;
                LavaSystem.phase1holder = true;
                LavaSystem.Start();
            }
            else if (mode == Mode.Lava)
            {
                mode = Mode.LavaFreebuild;
            }
            else
            {
                mode = Mode.LavaFreebuild;
            }
            if (!CLI)
            {
                Window.thisWindow.toolStripStatusLabelRoundTime.Visible = true;
            }
        }

        public static void ChangeModeToZombie()
        {
            if (mode != Mode.Zombie)
            {
                mode = Mode.Zombie;
                s.StartZombie();
            }
            if (!CLI)
            {
                Window.thisWindow.toolStripStatusLabelRoundTime.Visible = false;
            }
        }

        public static void ChangeModeToZombieFreebuild()
        {
            mode = Mode.ZombieFreebuild;
            if (!CLI)
            {
                Window.thisWindow.toolStripStatusLabelRoundTime.Visible = false;
            }
        }

        static void levels_LevelRemoved(object sender, LevelEventArgs e)
        {
            RemoteClient.remoteClients.ForEach(delegate(RemoteClient rc) { rc.RemoveMap(e.level); });
        }

        static void levels_LevelAdded(object sender, LevelEventArgs e)
        {
            RemoteClient.remoteClients.ForEach(delegate(RemoteClient rc) { rc.AddMap(e.level); });
        }

        public void StartZombie()
        {
            InfectionMaps.LoadInfectionMapsXML();
            InfectionMaps.SaveInfectionMapsXML();
            InfectionSystem.InfectionSystem.InfectionMapInitialization();
            InfectionTiers.InitTierSystem();
            InfectionSystem.InfectionSystem.Start();
            Log("Infection System started.");
        }

        public void StartLava()
        {
            if (LavaSystem.lavaMaps.Count > 0)
            {
                LavaSystem.LavaMapInitialization();
            }
            else
            {
                s.Log("No lava map found. Creating a new map.");
                LavaLevel = new Level(level, 128, 64, 128, "flat");
                LavaLevel.permissionvisit = LevelPermission.Guest;
                LavaLevel.permissionbuild = LevelPermission.Guest;
                LavaLevel.Save();
                AddLevel(LavaLevel);
            }
            LavaSystem.Start();
            Log("Lava System started.");
        }

        public void StartFreebuild()
        {
            string text = "levels/" + level + ".lvl";
            string text2 = text + ".backup";
            if (File.Exists(text))
            {
                mainLevel = Level.Load(level);
                mainLevel.unload = false;
                if (mainLevel == null)
                {
                    if (File.Exists(text2))
                    {
                        Log("Attempting to load backup.");
                        File.Copy(text2, text, overwrite: true);
                        mainLevel = Level.Load(level);
                        if (mainLevel == null)
                        {
                            Log("BACKUP FAILED!");
                            Console.ReadLine();
                            return;
                        }
                    }
                    else
                    {
                        Log("mainlevel not found");
                        mainLevel = new Level(level, 128, 64, 128, "flat");
                        mainLevel.permissionvisit = LevelPermission.Guest;
                        mainLevel.permissionbuild = LevelPermission.Guest;
                        mainLevel.Save();
                    }
                }
            }
            else
            {
                Log("mainlevel not found");
                mainLevel = new Level(level, 128, 64, 128, "flat");
                mainLevel.permissionvisit = LevelPermission.Guest;
                mainLevel.permissionbuild = LevelPermission.Guest;
                mainLevel.Save();
            }
            AddLevel(mainLevel);
        }

        public void Start()
        {
            shuttingDown = false;
            Log("Starting Server");
            DirectoryUtil.CreateIfNotExists("properties");
            DirectoryUtil.CreateIfNotExists("bots");
            DirectoryUtil.CreateIfNotExists("text");
            DirectoryUtil.CreateIfNotExists("Database");
            DirectoryUtil.CreateIfNotExists("extra");
            DirectoryUtil.CreateIfNotExists("extra/undo");
            DirectoryUtil.CreateIfNotExists("extra/undoPrevious");
            DirectoryUtil.CreateIfNotExists("extra/copy/");
            DirectoryUtil.CreateIfNotExists("extra/copyBackup/");
            DirectoryUtil.CreateIfNotExists("infection");
            DirectoryUtil.CreateIfNotExists("infection/maps");
            DirectoryUtil.CreateIfNotExists("maps/home/backup");
            DirectoryUtil.CreateIfNotExists(Scripting.PathDll);
            DirectoryUtil.CreateIfNotExists(Scripting.PathSource);
            try
            {
                if (File.Exists("Changelog.txt"))
                {
                    File.Move("Changelog.txt", "extra/Changelog.txt");
                }
                if (File.Exists("server.properties"))
                {
                    File.Move("server.properties", "properties/server.properties");
                }
                if (File.Exists("rules.txt"))
                {
                    File.Move("rules.txt", "text/rules.txt");
                }
                if (File.Exists("welcome.txt"))
                {
                    File.Move("welcome.txt", "text/welcome.txt");
                }
                if (File.Exists("messages.txt"))
                {
                    File.Move("messages.txt", "text/messages.txt");
                }
                if (File.Exists("externalurl.txt"))
                {
                    File.Move("externalurl.txt", "text/externalurl.txt");
                }
                if (File.Exists("autoload.txt"))
                {
                    File.Move("autoload.txt", "text/autoload.txt");
                }
                if (File.Exists("IRC_Controllers.txt"))
                {
                    File.Move("IRC_Controllers.txt", "ranks/IRC_Controllers.txt");
                }
                if (useWhitelist && File.Exists("whitelist.txt"))
                {
                    File.Move("whitelist.txt", "ranks/whitelist.txt");
                }
            }
            catch {}
            ServerProperties.Load("properties/server.properties");
            Updater.Load("properties/update.properties");
            LavaSystem.ConvertLavaMaps();
            Updater.InitUpdate();
            LavaSystem.LoadLavaMapsXML();
            LavaSystem.SaveLavaMapsXML();
            devMessages = new DevMessages();
            devMessages.Start();
            TierSystem.InitTierSystem();
            Store.InitStorePrices();
            Store.LoadPricesXML();
            Group.InitAll();
            VipList.Init();
            StoreSystem.Store.InitAll();
            if (!CLI)
            {
                Window.thisWindow.UpdateProperties();
            }
            Command.InitAll();
            GrpCommands.fillRanks();
            Block.SetBlocks();
            Awards.Load();
            Language.Init();
            chatFilter = new ChatFilter();
            chatFilter.Initialize();
            Timer timer = new Timer(900000.0);
            timer.Elapsed += delegate
            {
                Player.players.ForEach(delegate(Player p)
                {
                    if (!p.IsCpeSupported && !p.IsUsingWom)
                    {
                        Player.SendMessage(p, "------------- " + p.PublicName + " -------------");
                        Player.SendMessage(p, "|%c Please play Minecraft Classic on classicube.net");
                        Player.SendMessage(p, "|%c minecraft.net/classic will stop to work soon!");
                        Player.SendMessage(p, "|%c classicube.net is free, so enjoy :)");
                    }
                });
            };
            timer.Start();
            ThreadPool.SetMaxThreads(70, 70);
            LavaSystem.StartServerMessage();
            if (File.Exists("text/emotelist.txt"))
            {
                string[] array = File.ReadAllLines("text/emotelist.txt");
                foreach (string item in array)
                {
                    Player.emoteList.Add(item);
                }
            }
            else
            {
                File.Create("text/emotelist.txt");
            }
            TimeOnline = DateTime.Now;
            DBManager.Initialization();
            if (levels != null)
            {
                levels.ForEach(delegate(Level l) { l.Unload(); });
            }
            mainLoop.Queue(delegate
            {
                try
                {
                    levels = new LevelCollection(maps);
                    levels.LevelAdded += levels_LevelAdded;
                    levels.LevelRemoved += levels_LevelRemoved;
                    MapGen = new MapGenerator();
                    if (RunInRaceMode)
                    {
                        Race race = new Race();
                        race.Start();
                    }
                    else if (mode == Mode.Lava)
                    {
                        StartLava();
                    }
                    else if (mode == Mode.Freebuild)
                    {
                        StartFreebuild();
                    }
                    else if (mode == Mode.LavaFreebuild)
                    {
                        StartLava();
                        StartFreebuild();
                    }
                    else
                    {
                        StartZombie();
                    }
                }
                catch (Exception ex)
                {
                    ErrorLog(ex);
                }
            });
            mainLoop.Queue(delegate
            {
                bannedIP = PlayerList.Load("banned-ip.txt", null);
                greenList = PlayerList.Load("greenlist.txt", null);
                ircControllers = PlayerList.Load("IRC_Controllers.txt", null);
                foreach (Group group in Group.groupList)
                {
                    group.playerList = PlayerList.Load(group.fileName, group);
                }
                if (useWhitelist)
                {
                    whiteList = PlayerList.Load("whitelist.txt", null);
                }
            });
            mainLoop.Queue(delegate
            {
                if (File.Exists("text/autoload.txt"))
                {
                    try
                    {
                        string[] array2 = File.ReadAllLines("text/autoload.txt");
                        string[] array3 = array2;
                        foreach (string text in array3)
                        {
                            string text2 = text.Trim();
                            try
                            {
                                if (!(text2 == "") && text2[0] != '#')
                                {
                                    text2.IndexOf("=");
                                    string text3 = text2.Split('=')[0].Trim();
                                    string text4;
                                    try
                                    {
                                        text4 = text2.Split('=')[1].Trim();
                                    }
                                    catch
                                    {
                                        text4 = "0";
                                    }
                                    if (!text3.Equals(mainLevel.name))
                                    {
                                        Command.all.Find("load").Use(null, text3 + " " + text4);
                                        Level.FindExact(text3);
                                    }
                                    else
                                    {
                                        try
                                        {
                                            int num = int.Parse(text4);
                                            if (num >= 0 && num <= 4)
                                            {
                                                mainLevel.setPhysics(num);
                                            }
                                        }
                                        catch
                                        {
                                            s.Log("Physics variable invalid");
                                        }
                                    }
                                }
                            }
                            catch
                            {
                                s.Log(text2 + " map load fail.");
                            }
                        }
                    }
                    catch
                    {
                        s.Log("autoload.txt error");
                    }
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
                else
                {
                    Log("autoload.txt does not exist");
                }
            });
            mainLoop.Queue(delegate
            {
                Log("Creating a listening socket on the port " + port + "... ");
                if (Setup())
                {
                    s.Log("Done.");
                    if (RemoteSettings.All.AllowRemoteAccess)
                    {
                        Listener.Start(RemoteSettings.All.Port);
                    }
                }
                else
                {
                    s.Log("Could not create a socket connection.  Shutting down...");
                }
            });
            mainLoop.Queue(delegate
            {
                updateTimer.Elapsed += delegate
                {
                    if (GeneralSettings.All.IntelliSys)
                    {
                        Player.GlobalUpdate();
                    }
                    else
                    {
                        Player.GlobalUpdateOld();
                    }
                    PlayerBot.GlobalUpdatePosition();
                };
                updateTimer.Start();
            });
            mainLoop.Queue(delegate
            {
                try
                {
                    Heartbeat.Init();
                }
                catch (Exception ex2)
                {
                    ErrorLog(ex2);
                }
            });
            mainLoop.Queue(delegate
            {
                if (useHeaven)
                {
                    LavaSystem.LoadHeaven();
                }
                messageTimer.Elapsed += delegate { RandomMessage(); };
                messageTimer.Start();
                adTimer.Elapsed += delegate
                {
                    if (Player.players.Count >= 2)
                    {
                        Player.GlobalChatWorld(null, "%0(sun)%8 This server is powered by MCDzienny software.", showname: false);
                    }
                };
                adTimer.Start();
                process = Process.GetCurrentProcess();
                if (File.Exists("text/messages.txt"))
                {
                    StreamReader streamReader = File.OpenText("text/messages.txt");
                    while (!streamReader.EndOfStream)
                    {
                        messages.Add(streamReader.ReadLine());
                    }
                    streamReader.Dispose();
                }
                else
                {
                    File.Create("text/messages.txt").Close();
                }
                if (File.Exists("lava/messages.txt"))
                {
                    StreamReader streamReader2 = File.OpenText("lava/messages.txt");
                    while (!streamReader2.EndOfStream)
                    {
                        lavaTimedMessages.Add(streamReader2.ReadLine());
                    }
                    streamReader2.Dispose();
                }
                else
                {
                    File.Create("lava/messages.txt").Close();
                }
                if (File.Exists("infection/messages.txt"))
                {
                    StreamReader streamReader3 = File.OpenText("infection/messages.txt");
                    while (!streamReader3.EndOfStream)
                    {
                        zombieTimedMessages.Add(streamReader3.ReadLine());
                    }
                    streamReader3.Dispose();
                }
                else
                {
                    File.Create("infection/messages.txt").Close();
                }
                if (irc)
                {
                    new IRCBot();
                }
                if (backupInterval > 0)
                {
                    new AutoSaver(backupInterval);
                }
                blockThread = new Timer(blockInterval * 1000);
                blockThread.AutoReset = false;
                blockThread.Elapsed += delegate
                {
                    try
                    {
                        levels.ForEach(delegate(Level l) { l.SaveChanges(); });
                    }
                    catch {}
                    blockThread.Start();
                };
                blockThread.Start();
                Timer timer2 = new Timer(60000.0);
                timer2.Elapsed += delegate
                {
                    lock (Player.playersThatLeftLocker)
                    {
                        if (Player.playersThatLeft.Count > 100)
                        {
                            Player.playersThatLeft.Clear();
                        }
                    }
                };
                locationChecker = new Timer(35.0);
                locationChecker.AutoReset = false;
                locationChecker.Elapsed += PlayerLocationCheck;
                locationChecker.Start();
                afkTimer.Elapsed += afkTimer_Elapsed;
                afkTimer.AutoReset = false;
                afkTimer.Start();
                pingTimer.Elapsed += pingTimer_Elapsed;
                pingTimer.Start();
                if (!CLI)
                {
                    Timer timer3 = new Timer(60000.0);
                    timer3.Elapsed += delegate { PlayerListUpdate(); };
                    timer3.Start();
                }
                Log("Setting up server is finished.");
                ThreadPool.QueueUserWorkItem(delegate
                {
                    if (GeneralSettings.All.CheckPortOnStart)
                    {
                        bool? flag = null;
                        try
                        {
                            using (Stream stream = ((HttpWebRequest)WebRequest.Create(Report.Y)).GetResponse().GetResponseStream())
                            {
                                using (StreamReader streamReader4 = new StreamReader(stream))
                                {
                                    string text5 = streamReader4.ReadLine().Trim().ToLower();
                                    flag = !(text5 == "ok") ? false : true;
                                }
                            }
                        }
                        catch {}
                        if (flag.HasValue)
                        {
                            if (CLI)
                            {
                                s.Log("Port " + port + " is " + (flag.Value ? "open!" : "closed!"));
                            }
                            else if (flag.Value)
                            {
                                PortIsOpen.ShowBox();
                            }
                            else
                            {
                                PortIsClosed.ShowBox();
                            }
                        }
                    }
                });
            });
            mainLoop.Queue(InteliSys.PacketsMonitoring);
        }

        void PlayerLocationCheck(object sender, ElapsedEventArgs e)
        {
            try
            {
                bugPlace = BugPlaceType.None;
                Player.players.ForEachSync(delegate(Player p)
                {
                    try
                    {
                        bugPlace = BugPlaceType.One;
                        if (p.frozen)
                        {
                            byte rotx = (byte)rand.Next(170, 185);
                            byte roty = (byte)rand.Next(170, 185);
                            p.SendPos(byte.MaxValue, p.pos[0], p.pos[1], p.pos[2], rotx, roty);
                        }
                        else
                        {
                            if (p.following != "")
                            {
                                bugPlace = BugPlaceType.Two;
                                Player player = Player.Find(p.following);
                                if (player == null || player.level != p.level)
                                {
                                    bugPlace = BugPlaceType.Three;
                                    p.following = "";
                                    if (!p.canBuild)
                                    {
                                        p.canBuild = true;
                                    }
                                    if (player != null && player.possess == p.name)
                                    {
                                        player.possess = "";
                                    }
                                    return;
                                }
                                if (p.canBuild)
                                {
                                    bugPlace = BugPlaceType.Four;
                                    p.SendPos(byte.MaxValue, player.pos[0], (ushort)(player.pos[1] - 16), player.pos[2], player.rot[0], player.rot[1]);
                                }
                                else
                                {
                                    p.SendPos(byte.MaxValue, player.pos[0], player.pos[1], player.pos[2], player.rot[0], player.rot[1]);
                                }
                            }
                            else if (p.possess != "")
                            {
                                bugPlace = BugPlaceType.Five;
                                Player player2 = Player.Find(p.possess);
                                if (player2 == null || player2.level != p.level)
                                {
                                    p.possess = "";
                                }
                            }
                            bugPlace = BugPlaceType.Six;
                            bugPlace = BugPlaceType.Eight;
                            ushort num = (ushort)(p.pos[0] / 32);
                            ushort num2 = (ushort)(p.pos[1] / 32);
                            ushort num3 = (ushort)(p.pos[2] / 32);
                            if (!p.Loading && p.level.Death)
                            {
                                p.RealDeath(num, num2, num3);
                            }
                            bugPlace = BugPlaceType.Seven;
                            p.CheckBlock(num, num2, num3);
                            p.oldBlock = (ushort)(num + num2 + num3);
                        }
                    }
                    catch (Exception ex)
                    {
                        s.Log("!!! BUG PLACE: " + bugPlace);
                        ErrorLog(ex);
                    }
                });
            }
            catch {}
            locationChecker.Start();
        }

        void pingTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                Player.players.ForEachSync(delegate(Player p) { p.SendPing(); });
            }
            catch (Exception ex)
            {
                ErrorLog(ex);
            }
        }

        void afkTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            lock (afkLocker)
            {
                try
                {
                    Player.players.GetCopy().ForEach(delegate(Player p)
                    {
                        if (p == null)
                        {
                            s.Log("Error afk: p == null");
                        }
                        else if (p.group == null)
                        {
                            s.Log("Error afk: p.group == null");
                        }
                        else if (p.name == null)
                        {
                            s.Log("Error afk: p.name == null");
                        }
                        else if (p.pos == null)
                        {
                            s.Log("Error afk: p.pos == null");
                        }
                        else if (p.group.Permission < LevelPermission.Operator && p.afkCount > afkkick * 30)
                        {
                            p.disconnectionReason = DisconnectionReason.AutoKicked;
                            afkset.Remove(p.name);
                            p.Kick(string.Format(Player.rm.GetString("KickAfk"), afkkick));
                        }
                        else if (afkset.Contains(p.name))
                        {
                            if (IsPlayerMoving(p))
                            {
                                p.afkCount = 0;
                                Command.all.Find("afk").Use(p, "");
                            }
                            else
                            {
                                p.afkCount++;
                            }
                        }
                        else
                        {
                            if (!IsPlayerMoving(p))
                            {
                                p.afkCount++;
                            }
                            else
                            {
                                p.afkCount = 0;
                            }
                            if (p.afkCount > afkminutes * 30)
                            {
                                Command.all.Find("afk").Use(p, string.Format(Player.rm.GetString("AfkMessage"), afkminutes));
                            }
                        }
                    });
                }
                catch (Exception ex)
                {
                    ErrorLog(ex);
                }
            }
            if (afkminutes > 0)
            {
                afkTimer.Start();
            }
        }

        static bool IsPlayerMoving(Player p)
        {
            if (p.oldpos[0] != p.pos[0] || p.oldpos[1] != p.pos[1] || p.oldpos[2] != p.pos[2])
            {
                if (p.oldrot[0] == p.rot[0])
                {
                    return p.oldrot[1] != p.rot[1];
                }
                return true;
            }
            return false;
        }

        public static bool Setup()
        {
            try
            {
                IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, port);
                listen = new Socket(iPEndPoint.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                listen.Bind(iPEndPoint);
                listen.Listen(int.MaxValue);
                listen.BeginAccept(Accept, null);
                return true;
            }
            catch (SocketException ex)
            {
                ErrorLog(ex);
                return false;
            }
            catch (Exception ex2)
            {
                ErrorLog(ex2);
                return false;
            }
        }

        static void Accept(IAsyncResult result)
        {
            if (shuttingDown)
            {
                return;
            }
            Player player = null;
            if (DateTime.Now < serverLockTime)
            {
                Socket socket = listen.EndAccept(result);
                string text = "Server is Locked. Lock release in: " + (int)(serverLockTime - DateTime.Now).TotalSeconds + "s";
                byte[] array = new byte[64];
                array = enc.GetBytes(text.PadRight(64).Substring(0, 64));
                byte[] array2 = new byte[array.Length + 1];
                array2[0] = 14;
                Buffer.BlockCopy(array, 0, array2, 1, text.Length);
                socket.BeginSend(array2, 0, array2.Length, SocketFlags.None, delegate {}, null);
                socket.Close();
            }
            else
            {
                try
                {
                    player = new Player(listen.EndAccept(result));
                }
                catch {}
            }
            while (true)
            {
                try
                {
                    listen.BeginAccept(Accept, null);
                    break;
                }
                catch (SocketException)
                {
                    if (player != null)
                    {
                        player.Disconnect();
                    }
                    serverLockTime = DateTime.Now.AddSeconds(45.0);
                    s.Log("Error: Socket exception. Server lock for 45 sec.");
                    Thread.Sleep(1000);
                }
                catch (Exception ex2)
                {
                    ErrorLog(ex2);
                    if (player != null)
                    {
                        player.Disconnect();
                    }
                    Thread.Sleep(1000);
                }
            }
        }

        public static void StartExiting()
        {
            var players = new List<string>();
            Player.players.ForEachSync(delegate(Player p)
            {
                p.Save();
                players.Add(p.name);
            });
            Player.players.GetCopy().ForEach(delegate(Player p)
            {
                if (!customShutdown)
                {
                    p.Kick("Server shutdown. Rejoin in 10 seconds.");
                }
                else
                {
                    p.Kick(customShutdownMessage);
                }
            });
            shuttingDown = true;
            if (listen != null)
            {
                listen.Close();
            }
        }

        public static void AddLevel(Level level)
        {
            lock (levelListLocker)
            {
                levels.Add(level);
            }
            if (!CLI)
            {
                Window.thisWindow.UpdateMainMapListView();
            }
        }

        public static void RemoveLevel(Level level)
        {
            lock (levelListLocker)
            {
                levels.Remove(level);
            }
            if (!CLI)
            {
                Window.thisWindow.UpdateMainMapListView();
            }
        }

        public void PlayerListUpdate()
        {
            if (s.OnPlayerListChange != null)
            {
                s.OnPlayerListChange();
            }
            try
            {
                var playerItem = new List<string[]>();
                Player.players.ForEachSync(delegate(Player p)
                {
                    if (p == null)
                    {
                        ErrorLog(new string('-', 25) + Environment.NewLine + "Error: PlayerListUpdate() p == null");
                    }
                    else if (p.group == null)
                    {
                        ErrorLog(new string('-', 25) + Environment.NewLine + "Error: PlayerListUpdate() p.group == null");
                    }
                    else if (p.name == null)
                    {
                        ErrorLog(new string('-', 25) + Environment.NewLine + "Error: PlayerListUpdate() p.name == null");
                    }
                    else if (p.playerLevelName == null)
                    {
                        ErrorLog(new string('-', 25) + Environment.NewLine + "Error: PlayerListUpdate() p.playerLevelName == null");
                    }
                    else
                    {
                        string[] item = new string[4]
                        {
                            p.group.trueName, p.name, p.playerLevelName, p.afkCount / 30 == 0 ? "" : p.afkCount / 30 + " min."
                        };
                        playerItem.Add(item);
                    }
                });
                Window.thisWindow.UpdatePlayerListView(playerItem);
            }
            catch (Exception ex)
            {
                ErrorLog(ex);
            }
        }

        public void FailBeat()
        {
            if (HeartBeatFail != null)
            {
                HeartBeatFail();
            }
        }

        public void UpdateUrl(string url)
        {
            if (OnURLChange != null)
            {
                OnURLChange(url);
            }
        }

        public void Log(string message, bool systemMsg = false)
        {
            if (OnLog != null)
            {
                if (!systemMsg)
                {
                    string msg = DateTime.Now.ToString("(HH:mm:ss) ") + message;
                    OnLog(msg);
                    RemoteClient.remoteClients.ForEach(delegate(RemoteClient rc) { rc.SendLog(msg); });
                }
                else
                {
                    OnSystem(DateTime.Now.ToString("(HH:mm:ss) ") + message);
                }
            }
            if (!systemMsg)
            {
                Logger.Write(DateTime.Now.ToString("(HH:mm:ss) ") + message + Environment.NewLine);
            }
        }

        public void ErrorCase(string message)
        {
            if (OnError != null)
            {
                OnError(message);
            }
        }

        public void CommandUsed(string message)
        {
            if (OnCommand != null)
            {
                OnCommand(DateTime.Now.ToString("(HH:mm:ss) ") + message);
            }
            RemoteClient.remoteClients.ForEach(delegate(RemoteClient rc) { rc.SendCommandLog(message); });
            Logger.WriteCommand(DateTime.Now.ToString("(HH:mm:ss) ") + message + Environment.NewLine);
        }

        public static void ErrorLog(Exception ex)
        {
            Logger.WriteError(ex);
            try
            {
                s.Log("!!!Error! See " + Logger.ErrorLogPath + " for more information.");
            }
            catch {}
        }

        public static void ErrorLog(string message)
        {
            Logger.WriteError(message);
            try
            {
                s.Log("!!!Error! See " + Logger.ErrorLogPath + " for more information.");
            }
            catch {}
        }

        public static void RandomMessage()
        {
            if (Player.number != 0)
            {
                if (messages.Count > 0)
                {
                    Player.GlobalMessage(messages[new Random().Next(0, messages.Count)]);
                }
                if (lavaTimedMessages.Count > 0 && LavaLevel != null)
                {
                    Player.GlobalMessageLevel(LavaLevel, lavaTimedMessages[new Random().Next(0, lavaTimedMessages.Count)]);
                }
                if (zombieTimedMessages.Count > 0 && InfectionLevel != null)
                {
                    Player.GlobalMessageLevel(InfectionLevel, zombieTimedMessages[new Random().Next(0, zombieTimedMessages.Count)]);
                }
            }
        }

        internal void SettingsUpdate()
        {
            if (OnSettingsUpdate != null)
            {
                OnSettingsUpdate();
            }
        }

        public static string FindColor(string Username)
        {
            foreach (Group group in Group.groupList)
            {
                if (group.playerList.Contains(Username))
                {
                    return group.color;
                }
            }
            return Group.standard.color;
        }

        public static bool IsFreebuildModeOn()
        {
            if (mode != Mode.Freebuild && mode != Mode.LavaFreebuild)
            {
                return mode == Mode.ZombieFreebuild;
            }
            return true;
        }

        public static bool IsZombieModeOn()
        {
            if (mode != Mode.Zombie)
            {
                return mode == Mode.ZombieFreebuild;
            }
            return true;
        }

        public static bool IsLavaModeOn()
        {
            if (mode != 0)
            {
                return mode == Mode.LavaFreebuild;
            }
            return true;
        }

        enum BugPlaceType
        {
            None,
            One,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Eleven,
            Twelve
        }

        public struct TempBan
        {
            public string name;

            public DateTime allowedJoin;
        }

        public struct levelID
        {
            public int ID;

            public string name;
        }
    }
}