using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using MCDzienny.Settings;
using MonoTorrent.Client;
using MySql.Data.MySqlClient;
using Timer = System.Timers.Timer;

namespace MCDzienny
{
    // Token: 0x02000208 RID: 520
    public sealed class Server
    {
        // Token: 0x0200020B RID: 523
        // (Invoke) Token: 0x06000EB1 RID: 3761
        public delegate void HeartBeatHandler();

        // Token: 0x0200020A RID: 522
        // (Invoke) Token: 0x06000EAD RID: 3757
        public delegate void LogHandler(string message);

        // Token: 0x0200020F RID: 527
        // (Invoke) Token: 0x06000EC1 RID: 3777
        public delegate void MapChangedHandler();

        // Token: 0x0200020C RID: 524
        // (Invoke) Token: 0x06000EB5 RID: 3765
        public delegate void MessageEventHandler(string message);

        // Token: 0x0200020D RID: 525
        // (Invoke) Token: 0x06000EB9 RID: 3769
        public delegate void PlayerListHandler();

        // Token: 0x0200020E RID: 526
        // (Invoke) Token: 0x06000EBD RID: 3773
        public delegate void VoidHandler();

        // Token: 0x0400079B RID: 1947
        public const byte version = 7;

        // Token: 0x0400079C RID: 1948
        public static readonly bool RunInRaceMode = false;

        // Token: 0x0400079D RID: 1949
        public static PluginManager Plugins = new PluginManager();

        // Token: 0x0400079E RID: 1950
        private static DevMessages devMessages;

        // Token: 0x040007A2 RID: 1954
        public static object levelListLocker = new object();

        // Token: 0x040007A3 RID: 1955
        public static ChatFilter chatFilter;

        // Token: 0x040007A7 RID: 1959
        public static bool restarting = false;

        // Token: 0x040007A8 RID: 1960
        public static volatile Mode mode = Mode.Lava;

        // Token: 0x040007A9 RID: 1961
        public static string zombieAlias = "zombie";

        // Token: 0x040007AA RID: 1962
        public static bool CLI = false;

        // Token: 0x040007AB RID: 1963
        public static int lavaSurvivalPlayerLimit = 0;

        // Token: 0x040007AD RID: 1965
        public static bool useHeaven = false;

        // Token: 0x040007AE RID: 1966
        public static string heavenMapName = "";

        // Token: 0x040007AF RID: 1967
        public static Level heavenMap;

        // Token: 0x040007B0 RID: 1968
        public static string serverMessage = "";

        // Token: 0x040007B1 RID: 1969
        public static int serverMessageInterval = 10;

        // Token: 0x040007B2 RID: 1970
        public static bool voteMode = false;

        // Token: 0x040007B3 RID: 1971
        public static byte vipSystem = 1;

        // Token: 0x040007B4 RID: 1972
        public static int packetSent;

        // Token: 0x040007B5 RID: 1973
        public static bool pause = false;

        // Token: 0x040007B6 RID: 1974
        public static DateTime serverLockTime;

        // Token: 0x040007B7 RID: 1975
        private static readonly ASCIIEncoding enc = new ASCIIEncoding();

        // Token: 0x040007C2 RID: 1986
        public static Timer locationChecker;

        // Token: 0x040007C3 RID: 1987
        public static Timer blockThread;

        // Token: 0x040007C4 RID: 1988
        public static List<MySqlCommand> mySQLCommands = new List<MySqlCommand>();

        // Token: 0x040007C5 RID: 1989
        public static int speedPhysics = 250;

        // Token: 0x040007C6 RID: 1990
        public static Socket listen;

        // Token: 0x040007C7 RID: 1991
        public static Process process = Process.GetCurrentProcess();

        // Token: 0x040007C8 RID: 1992
        public static Timer updateTimer = new Timer(100.0);

        // Token: 0x040007C9 RID: 1993
        private static readonly Timer messageTimer = new Timer(210000.0);

        // Token: 0x040007CA RID: 1994
        private static readonly Timer adTimer = new Timer(1500000.0);

        // Token: 0x040007CB RID: 1995
        public static Timer cloneTimer = new Timer(5000.0);

        // Token: 0x040007CC RID: 1996
        private static Timer packetUpdate = new Timer(5000.0);

        // Token: 0x040007CE RID: 1998
        public static List<CTFGame> CTFGames = new List<CTFGame>();

        // Token: 0x040007CF RID: 1999
        public static PlayerList bannedIP;

        // Token: 0x040007D0 RID: 2000
        public static PlayerList whiteList = new PlayerList();

        // Token: 0x040007D1 RID: 2001
        public static PlayerList greenList;

        // Token: 0x040007D2 RID: 2002
        public static PlayerList ircControllers;

        // Token: 0x040007D3 RID: 2003
        public static List<string> devs = new List<string>
        {
            "dzienny"
        };

        // Token: 0x040007D4 RID: 2004
        public static List<TempBan> tempBans = new List<TempBan>();

        // Token: 0x040007D5 RID: 2005
        public static MapGenerator MapGen;

        // Token: 0x040007D6 RID: 2006
        public static PerformanceCounter PCCounter = null;

        // Token: 0x040007D7 RID: 2007
        public static PerformanceCounter ProcessCounter = null;

        // Token: 0x040007D8 RID: 2008
        public static Level mainLevel;

        // Token: 0x040007D9 RID: 2009

        // Token: 0x040007DA RID: 2010
        public static LevelCollection levels;

        // Token: 0x040007DB RID: 2011
        public static HashSet<string> afkset = new HashSet<string>();

        // Token: 0x040007DC RID: 2012
        public static List<string> afkmessages = new List<string>();

        // Token: 0x040007DD RID: 2013
        public static List<string> messages = new List<string>();

        // Token: 0x040007DE RID: 2014
        public static List<string> lavaTimedMessages = new List<string>();

        // Token: 0x040007DF RID: 2015
        public static List<string> zombieTimedMessages = new List<string>();

        // Token: 0x040007E0 RID: 2016
        public static DateTime TimeOnline;

        // Token: 0x040007E1 RID: 2017
        public static bool autoupdate;

        // Token: 0x040007E2 RID: 2018
        public static bool autonotify;

        // Token: 0x040007E3 RID: 2019
        public static string restartcountdown = "";

        // Token: 0x040007E4 RID: 2020
        public static string selectedrevision = "";

        // Token: 0x040007E5 RID: 2021
        public static bool autorestart;

        // Token: 0x040007E6 RID: 2022
        public static DateTime restarttime;

        // Token: 0x040007E7 RID: 2023
        public static bool chatmod = false;

        // Token: 0x040007E8 RID: 2024
        public static bool hardcore = false;

        // Token: 0x040007E9 RID: 2025
        public static volatile string salt;

        // Token: 0x040007EA RID: 2026
        private static readonly RNGCryptoServiceProvider rngCrypto = new RNGCryptoServiceProvider();

        // Token: 0x040007EB RID: 2027
        private static readonly MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

        // Token: 0x040007EC RID: 2028
        private static string salt2;

        // Token: 0x040007ED RID: 2029
        public static string description = "Come and join the fun!";

        // Token: 0x040007EE RID: 2030
        public static bool autoFlag = true;

        // Token: 0x040007EF RID: 2031
        private static string flag = "LAVA";

        // Token: 0x040007F0 RID: 2032
        public static string serverRealName = "Minecraft Server(MCDzienny)";

        // Token: 0x040007F1 RID: 2033
        public static string name = "Minecraft Server(MCDzienny)";

        // Token: 0x040007F2 RID: 2034
        public static string motd = "Welcome! &0 -hax +ophax";

        // Token: 0x040007F3 RID: 2035
        public static byte players = 15;

        // Token: 0x040007F4 RID: 2036
        public static byte maps = 5;

        // Token: 0x040007F5 RID: 2037
        public static int port = 25564;

        // Token: 0x040007F6 RID: 2038
        public static bool isPublic = true;

        // Token: 0x040007F7 RID: 2039
        public static bool verify = true;

        // Token: 0x040007F8 RID: 2040
        public static bool worldChat = true;

        // Token: 0x040007F9 RID: 2041
        public static bool guestGoto = false;

        // Token: 0x040007FA RID: 2042
        public static string ConsoleName = "Alive";

        // Token: 0x040007FB RID: 2043
        public static string level = "main";

        // Token: 0x040007FC RID: 2044
        public static bool console = false;

        // Token: 0x040007FD RID: 2045
        public static bool reportBack = true;

        // Token: 0x040007FE RID: 2046
        public static bool irc = false;

        // Token: 0x040007FF RID: 2047
        public static int ircPort = 6667;

        // Token: 0x04000800 RID: 2048
        public static string ircNick = "MCDzienny_Minecraft_Bot";

        // Token: 0x04000801 RID: 2049
        public static string ircServer = "irc.esper.net";

        // Token: 0x04000802 RID: 2050
        public static string ircChannel = "#changethis";

        // Token: 0x04000803 RID: 2051
        public static string ircOpChannel = "#changethistoo";

        // Token: 0x04000804 RID: 2052
        public static bool ircIdentify = false;

        // Token: 0x04000805 RID: 2053
        public static string ircPassword = "";

        // Token: 0x04000806 RID: 2054
        public static bool restartOnError = true;

        // Token: 0x04000807 RID: 2055
        public static bool antiTunnel = false;

        // Token: 0x04000808 RID: 2056
        public static byte maxDepth = 4;

        // Token: 0x04000809 RID: 2057
        public static int Overload = 99000;

        // Token: 0x0400080A RID: 2058
        public static int rpLimit = 500;

        // Token: 0x0400080B RID: 2059
        public static int rpNormLimit = 10000;

        // Token: 0x0400080C RID: 2060
        public static int backupInterval = 180;

        // Token: 0x0400080D RID: 2061
        public static int blockInterval = 60;

        // Token: 0x0400080E RID: 2062
        public static string backupLocation = Application.StartupPath + "/levels/backups";

        // Token: 0x0400080F RID: 2063
        public static bool physicsRestart = true;

        // Token: 0x04000810 RID: 2064
        public static bool deathcount = true;

        // Token: 0x04000811 RID: 2065
        public static bool AutoLoad = false;

        // Token: 0x04000812 RID: 2066
        public static int physUndo = 60000;

        // Token: 0x04000813 RID: 2067
        public static int totalUndo = 200;

        // Token: 0x04000814 RID: 2068
        public static bool rankSuper = true;

        // Token: 0x04000815 RID: 2069
        public static bool oldHelp = false;

        // Token: 0x04000816 RID: 2070
        public static bool parseSmiley = true;

        // Token: 0x04000817 RID: 2071
        public static bool useWhitelist = false;

        // Token: 0x04000818 RID: 2072
        public static bool forceCuboid = false;

        // Token: 0x04000819 RID: 2073
        public static bool repeatMessage = false;

        // Token: 0x0400081A RID: 2074
        public static bool checkUpdates = true;

        // Token: 0x0400081B RID: 2075
        public static bool useMySQL = true;

        // Token: 0x0400081C RID: 2076
        public static string MySQLHost = "127.0.0.1";

        // Token: 0x0400081D RID: 2077
        public static string MySQLPort = "3306";

        // Token: 0x0400081E RID: 2078
        public static string MySQLUsername = "root";

        // Token: 0x0400081F RID: 2079
        public static string MySQLPassword = "password";

        // Token: 0x04000820 RID: 2080
        public static string MySQLDatabaseName = "MCDziennyLava";

        // Token: 0x04000821 RID: 2081
        public static bool MySQLPooling = true;

        // Token: 0x04000822 RID: 2082
        public static string DefaultColor = "&e";

        // Token: 0x04000823 RID: 2083
        private static string secondaryColor = "&4";

        // Token: 0x04000824 RID: 2084
        public static string IRCColour = "&5";

        // Token: 0x04000825 RID: 2085
        public static int afkminutes = 10;

        // Token: 0x04000826 RID: 2086
        public static int afkkick = 45;

        // Token: 0x04000827 RID: 2087
        public static string defaultRank = "guest";

        // Token: 0x04000828 RID: 2088
        public static bool useDollarSign = true;

        // Token: 0x04000829 RID: 2089
        public static bool cheapMessage = true;

        // Token: 0x0400082A RID: 2090
        public static string cheapMessageGiven = " is now being cheap and being immortal";

        // Token: 0x0400082B RID: 2091
        public static bool customBan = false;

        // Token: 0x0400082C RID: 2092
        public static string customBanMessage = "You're banned!";

        // Token: 0x0400082D RID: 2093
        public static bool customShutdown = false;

        // Token: 0x0400082E RID: 2094
        public static string customShutdownMessage = "Server shutdown. Rejoin in 30 seconds.";

        // Token: 0x0400082F RID: 2095
        public static string moneys = "gold coins";

        // Token: 0x04000830 RID: 2096
        public static LevelPermission opchatperm = LevelPermission.Operator;

        // Token: 0x04000831 RID: 2097
        public static bool logbeat = false;

        // Token: 0x04000832 RID: 2098
        public static bool mono = Environment.OSVersion.Platform == PlatformID.Unix;

        // Token: 0x04000833 RID: 2099
        public static bool flipHead = false;

        // Token: 0x04000834 RID: 2100
        public static volatile bool shuttingDown;

        // Token: 0x04000835 RID: 2101
        private static readonly Random rand = new Random();

        // Token: 0x04000836 RID: 2102
        public static MainLoop mainLoop;

        // Token: 0x04000837 RID: 2103
        public static Server s;

        public static SimpleKeyValueManager.SimpleKeyValueManager constantsForChat;

        // Token: 0x040007AC RID: 1964
        private readonly object afkLocker = new object();

        // Token: 0x040007A0 RID: 1952
        private readonly Timer afkTimer = new Timer(2000.0);

        // Token: 0x0400079F RID: 1951
        private BugPlaceType bugPlace;

        // Token: 0x040007A1 RID: 1953
        private readonly Timer pingTimer = new Timer(2000.0);

        // Token: 0x040007CD RID: 1997
        private RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

        // Token: 0x06000E79 RID: 3705 RVA: 0x00050DDC File Offset: 0x0004EFDC
        public Server()
        {
            s = this;
            mainLoop = new MainLoop("server");
            var initialContent =
                "# This file contains chat $constants. These constants are substituted\r\n# in a chat for a defined text. For example you can define\r\n# $web constant that when used in text will change into your\r\n# webpage address. \r\n# Format:\r\n# constant_name : text_for_substitution\r\n# Example:\r\n# web : www.toBeDefinedWebpageAddress.com\r\ntest : test passed!";
            constantsForChat =
                new SimpleKeyValueManager.SimpleKeyValueManager("text/chat_constant$.txt", initialContent);
        }

        // Token: 0x1700052E RID: 1326
        // (get) Token: 0x06000E4D RID: 3661 RVA: 0x00050118 File Offset: 0x0004E318
        public static int UniqueVisits
        {
            get
            {
                int result;
                using (var dataTable = DBInterface.fillData("SELECT COUNT(Name) FROM Players"))
                {
                    int.TryParse(dataTable.Rows[0]["COUNT(Name)"].ToString(), out result);
                }

                return result;
            }
        }

        // Token: 0x1700052F RID: 1327
        // (get) Token: 0x06000E4E RID: 3662 RVA: 0x00050170 File Offset: 0x0004E370
        // (set) Token: 0x06000E4F RID: 3663 RVA: 0x00050178 File Offset: 0x0004E378
        public static string ServerHash { get; set; }

        // Token: 0x17000530 RID: 1328
        // (get) Token: 0x06000E64 RID: 3684 RVA: 0x000505E0 File Offset: 0x0004E7E0
        public static string Version
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }

        // Token: 0x17000531 RID: 1329
        // (get) Token: 0x06000E65 RID: 3685 RVA: 0x000505F8 File Offset: 0x0004E7F8
        public static Level DefaultLevel
        {
            get
            {
                if (mode == Mode.Lava) return LavaLevel;
                if (mode == Mode.Freebuild || mode == Mode.LavaFreebuild || mode == Mode.ZombieFreebuild)
                    return mainLevel;
                return InfectionLevel;
            }
        }

        // Token: 0x17000532 RID: 1330
        // (get) Token: 0x06000E66 RID: 3686 RVA: 0x00050634 File Offset: 0x0004E834
        // (set) Token: 0x06000E67 RID: 3687 RVA: 0x0005063C File Offset: 0x0004E83C
        public static Level LavaLevel { get; set; }

        // Token: 0x17000533 RID: 1331
        // (get) Token: 0x06000E68 RID: 3688 RVA: 0x00050644 File Offset: 0x0004E844
        // (set) Token: 0x06000E69 RID: 3689 RVA: 0x0005064C File Offset: 0x0004E84C
        public static Level InfectionLevel { get; set; }

        // Token: 0x17000534 RID: 1332
        // (get) Token: 0x06000E6A RID: 3690 RVA: 0x00050654 File Offset: 0x0004E854
        [Browsable(false)]
        public static int FreebuildCount
        {
            get
            {
                var tempCount = 0;
                levels.ForEach(delegate(Level l)
                {
                    if (l.mapType == MapType.Freebuild) tempCount++;
                });
                return tempCount;
            }
        }

        // Token: 0x17000535 RID: 1333
        // (get) Token: 0x06000E6B RID: 3691 RVA: 0x0005068C File Offset: 0x0004E88C
        // (set) Token: 0x06000E6C RID: 3692 RVA: 0x0005071C File Offset: 0x0004E91C
        public static string Salt
        {
            get
            {
                if (salt != null && salt != "") return salt;
                var array = new byte[16];
                rngCrypto.GetBytes(array);
                array = md5.ComputeHash(array);
                var stringBuilder = new StringBuilder();
                for (var i = 0; i < array.Length; i++) stringBuilder.Append(array[i].ToString("X2"));
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

        // Token: 0x17000536 RID: 1334
        // (get) Token: 0x06000E6D RID: 3693 RVA: 0x00050748 File Offset: 0x0004E948
        // (set) Token: 0x06000E6E RID: 3694 RVA: 0x000507D0 File Offset: 0x0004E9D0
        public static string SaltClassiCube
        {
            get
            {
                if (salt2 != null && salt2 != "") return salt2;
                var array = new byte[16];
                rngCrypto.GetBytes(array);
                array = md5.ComputeHash(array);
                var stringBuilder = new StringBuilder();
                for (var i = 0; i < array.Length; i++) stringBuilder.Append(array[i].ToString("X2"));
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

        // Token: 0x17000537 RID: 1335
        // (get) Token: 0x06000E6F RID: 3695 RVA: 0x000507F8 File Offset: 0x0004E9F8
        // (set) Token: 0x06000E70 RID: 3696 RVA: 0x00050858 File Offset: 0x0004EA58
        public static string Flag
        {
            get
            {
                if (!autoFlag) return flag;
                if (mode == Mode.Lava) return "LAVA";
                if (mode == Mode.Freebuild) return "FREEBUILD";
                if (mode == Mode.LavaFreebuild) return "LAVA/FREEBUILD";
                if (mode == Mode.Zombie) return "ZOMBIE";
                return "LAVA";
            }
            set { flag = value; }
        }

        // Token: 0x17000538 RID: 1336
        // (get) Token: 0x06000E71 RID: 3697 RVA: 0x00050860 File Offset: 0x0004EA60
        public static string IP
        {
            get
            {
                var result = "?";
                var hostEntry = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ipaddress in hostEntry.AddressList)
                    if (ipaddress.AddressFamily == AddressFamily.InterNetwork)
                        result = ipaddress.ToString();
                return result;
            }
        }

        // Token: 0x17000539 RID: 1337
        // (get) Token: 0x06000E72 RID: 3698 RVA: 0x000508B0 File Offset: 0x0004EAB0
        public static string ConsoleRealName
        {
            get
            {
                if (GeneralSettings.All.UseCustomName)
                    return GeneralSettings.All.CustomConsoleName + GeneralSettings.All.CustomConsoleNameDelimiter + " ";
                return "Console [&a" + ConsoleName + DefaultColor + "]: &f";
            }
        }

        // Token: 0x1700053A RID: 1338
        // (get) Token: 0x06000E73 RID: 3699 RVA: 0x00050904 File Offset: 0x0004EB04
        public static string ConsoleRealNameIRC
        {
            get
            {
                if (GeneralSettings.All.UseCustomName)
                    return GeneralSettings.All.CustomConsoleName + GeneralSettings.All.CustomConsoleNameDelimiter + " ";
                return "Console [" + ConsoleName + DefaultColor + "]: ";
            }
        }

        // Token: 0x1700053B RID: 1339
        // (get) Token: 0x06000E74 RID: 3700 RVA: 0x00050958 File Offset: 0x0004EB58
        // (set) Token: 0x06000E75 RID: 3701 RVA: 0x00050960 File Offset: 0x0004EB60
        public static string SecondaryColor
        {
            get { return secondaryColor; }
            set { secondaryColor = value; }
        }

        // Token: 0x06000E47 RID: 3655 RVA: 0x0004FD3C File Offset: 0x0004DF3C
        public static void ChangeModeToFreebuild()
        {
            var b = "";
            if (mode == Mode.Lava || mode == Mode.LavaFreebuild)
            {
                b = LavaSystem.currentlvl.name;
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

            var count = Player.players.Count;
            if (mode != Mode.Freebuild)
            {
                mode = Mode.Freebuild;
                for (var i = count - 1; i >= 0; i--)
                    try
                    {
                        if (Player.players[i].level.name == b)
                            Command.all.Find("goto").Use(Player.players[i], mainLevel.name);
                    }
                    catch (Exception ex)
                    {
                        ErrorLog(ex);
                    }
            }

            mode = Mode.Freebuild;
            if (!CLI) Window.thisWindow.toolStripStatusLabelRoundTime.Visible = false;
        }

        // Token: 0x06000E48 RID: 3656 RVA: 0x0004FE98 File Offset: 0x0004E098
        public static void ChangeModeToLava()
        {
            if (mode == Mode.Freebuild)
            {
                var text = mainLevel.name;
                LavaSystem.LoadLavaMapsXML();
                LavaSystem.LavaMapInitialization();
                mode = Mode.Lava;
                LavaSystem.phase1holder = true;
                LavaSystem.Start();
                var toMap = LavaSystem.currentLavaMap.Name;
                MoveAllPlayers(text, toMap);
            }
            else if (mode == Mode.LavaFreebuild)
            {
                var text = mainLevel.name;
                mode = Mode.Lava;
                MoveAllPlayers(text, LavaSystem.currentLavaMap.Name);
            }
            else if (mode == Mode.Zombie)
            {
                var text = DefaultLevel.name;
                LavaSystem.LoadLavaMapsXML();
                LavaSystem.LavaMapInitialization();
                mode = Mode.Lava;
                LavaSystem.phase1holder = true;
                LavaSystem.Start();
                MoveAllPlayers(text, DefaultLevel.name);
                InfectionSystem.InfectionSystem.Stop();
                Command.all.Find("unload").Use(null, text);
            }

            if (!CLI) Window.thisWindow.toolStripStatusLabelRoundTime.Visible = true;
        }

        // Token: 0x06000E49 RID: 3657 RVA: 0x0004FF9C File Offset: 0x0004E19C
        private static void MoveAllPlayers(string fromMap, string toMap)
        {
            var count = Player.players.Count;
            for (var i = count - 1; i >= 0; i--)
                try
                {
                    if (Player.players[i].level.name == fromMap) Command.all.Find("goto").Use(Player.players[i], toMap);
                }
                catch (Exception ex)
                {
                    ErrorLog(ex);
                }
        }

        // Token: 0x06000E4A RID: 3658 RVA: 0x0005001C File Offset: 0x0004E21C
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

            if (!CLI) Window.thisWindow.toolStripStatusLabelRoundTime.Visible = true;
        }

        // Token: 0x06000E4B RID: 3659 RVA: 0x000500BC File Offset: 0x0004E2BC
        public static void ChangeModeToZombie()
        {
            if (mode != Mode.Zombie)
            {
                mode = Mode.Zombie;
                s.StartZombie();
            }

            if (!CLI) Window.thisWindow.toolStripStatusLabelRoundTime.Visible = false;
        }

        // Token: 0x06000E4C RID: 3660 RVA: 0x000500F4 File Offset: 0x0004E2F4
        public static void ChangeModeToZombieFreebuild()
        {
            mode = Mode.ZombieFreebuild;
            if (!CLI) Window.thisWindow.toolStripStatusLabelRoundTime.Visible = false;
        }

        // Token: 0x14000019 RID: 25
        // (add) Token: 0x06000E50 RID: 3664 RVA: 0x00050180 File Offset: 0x0004E380
        // (remove) Token: 0x06000E51 RID: 3665 RVA: 0x000501B8 File Offset: 0x0004E3B8
        public event LogHandler OnLog;

        // Token: 0x1400001A RID: 26
        // (add) Token: 0x06000E52 RID: 3666 RVA: 0x000501F0 File Offset: 0x0004E3F0
        // (remove) Token: 0x06000E53 RID: 3667 RVA: 0x00050228 File Offset: 0x0004E428
        public event LogHandler OnSystem;

        // Token: 0x1400001B RID: 27
        // (add) Token: 0x06000E54 RID: 3668 RVA: 0x00050260 File Offset: 0x0004E460
        // (remove) Token: 0x06000E55 RID: 3669 RVA: 0x00050298 File Offset: 0x0004E498
        public event LogHandler OnCommand;

        // Token: 0x1400001C RID: 28
        // (add) Token: 0x06000E56 RID: 3670 RVA: 0x000502D0 File Offset: 0x0004E4D0
        // (remove) Token: 0x06000E57 RID: 3671 RVA: 0x00050308 File Offset: 0x0004E508
        public event LogHandler OnError;

        // Token: 0x1400001D RID: 29
        // (add) Token: 0x06000E58 RID: 3672 RVA: 0x00050340 File Offset: 0x0004E540
        // (remove) Token: 0x06000E59 RID: 3673 RVA: 0x00050378 File Offset: 0x0004E578
        public event HeartBeatHandler HeartBeatFail;

        // Token: 0x1400001E RID: 30
        // (add) Token: 0x06000E5A RID: 3674 RVA: 0x000503B0 File Offset: 0x0004E5B0
        // (remove) Token: 0x06000E5B RID: 3675 RVA: 0x000503E8 File Offset: 0x0004E5E8
        public event MessageEventHandler OnURLChange;

        // Token: 0x1400001F RID: 31
        // (add) Token: 0x06000E5C RID: 3676 RVA: 0x00050420 File Offset: 0x0004E620
        // (remove) Token: 0x06000E5D RID: 3677 RVA: 0x00050458 File Offset: 0x0004E658
        public event PlayerListHandler OnPlayerListChange;

        // Token: 0x14000020 RID: 32
        // (add) Token: 0x06000E5E RID: 3678 RVA: 0x00050490 File Offset: 0x0004E690
        // (remove) Token: 0x06000E5F RID: 3679 RVA: 0x000504C8 File Offset: 0x0004E6C8
        public event VoidHandler OnSettingsUpdate;

        // Token: 0x14000021 RID: 33
        // (add) Token: 0x06000E60 RID: 3680 RVA: 0x00050500 File Offset: 0x0004E700
        // (remove) Token: 0x06000E61 RID: 3681 RVA: 0x00050538 File Offset: 0x0004E738
        public event MapChangedHandler MapAdded;

        // Token: 0x14000022 RID: 34
        // (add) Token: 0x06000E62 RID: 3682 RVA: 0x00050570 File Offset: 0x0004E770
        // (remove) Token: 0x06000E63 RID: 3683 RVA: 0x000505A8 File Offset: 0x0004E7A8
        public event MapChangedHandler MapRemoved;

        // Token: 0x06000E77 RID: 3703 RVA: 0x00050D7C File Offset: 0x0004EF7C
        private static void levels_LevelRemoved(object sender, LevelEventArgs e)
        {
        }

        // Token: 0x06000E78 RID: 3704 RVA: 0x00050DAC File Offset: 0x0004EFAC
        private static void levels_LevelAdded(object sender, LevelEventArgs e)
        {
        }

        // Token: 0x06000E7A RID: 3706 RVA: 0x00050E78 File Offset: 0x0004F078
        public void StartZombie()
        {
            InfectionMaps.LoadInfectionMapsXML();
            InfectionMaps.SaveInfectionMapsXML();
            InfectionSystem.InfectionSystem.InfectionMapInitialization();
            InfectionTiers.InitTierSystem();
            InfectionSystem.InfectionSystem.Start();
            Log("Infection System started.");
        }

        // Token: 0x06000E7B RID: 3707 RVA: 0x00050EA0 File Offset: 0x0004F0A0
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

        // Token: 0x06000E7C RID: 3708 RVA: 0x00050F30 File Offset: 0x0004F130
        public void StartFreebuild()
        {
            var text = "levels/" + level + ".lvl";
            var text2 = text + ".backup";
            if (File.Exists(text))
            {
                mainLevel = Level.Load(level);
                mainLevel.unload = false;
                if (mainLevel == null)
                {
                    if (File.Exists(text2))
                    {
                        Log("Attempting to load backup.");
                        File.Copy(text2, text, true);
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

        // Token: 0x06000E7D RID: 3709 RVA: 0x0005107C File Offset: 0x0004F27C
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
                if (File.Exists("Changelog.txt")) File.Move("Changelog.txt", "extra/Changelog.txt");
                if (File.Exists("server.properties")) File.Move("server.properties", "properties/server.properties");
                if (File.Exists("rules.txt")) File.Move("rules.txt", "text/rules.txt");
                if (File.Exists("welcome.txt")) File.Move("welcome.txt", "text/welcome.txt");
                if (File.Exists("messages.txt")) File.Move("messages.txt", "text/messages.txt");
                if (File.Exists("externalurl.txt")) File.Move("externalurl.txt", "text/externalurl.txt");
                if (File.Exists("autoload.txt")) File.Move("autoload.txt", "text/autoload.txt");
                if (File.Exists("IRC_Controllers.txt")) File.Move("IRC_Controllers.txt", "ranks/IRC_Controllers.txt");
                if (useWhitelist && File.Exists("whitelist.txt")) File.Move("whitelist.txt", "ranks/whitelist.txt");
            }
            catch
            {
            }

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
            if (!CLI) Window.thisWindow.UpdateProperties();
            Command.InitAll();
            GrpCommands.fillRanks();
            Block.SetBlocks();
            Awards.Load();
            Language.Init();
            chatFilter = new ChatFilter();
            chatFilter.Initialize();
            var timer = new Timer(900000.0);
            timer.Elapsed += delegate
            {
                Player.players.ForEach(delegate(Player p)
                {
                    if (p.IsCpeSupported || p.IsUsingWom) return;
                    Player.SendMessage(p, "------------- " + p.PublicName + " -------------");
                    Player.SendMessage(p, "|%c Please play Minecraft Classic on classicube.net");
                    Player.SendMessage(p, "|%c minecraft.net/classic will stop to work soon!");
                    Player.SendMessage(p, "|%c classicube.net is free, so enjoy :)");
                });
            };
            timer.Start();
            ThreadPool.SetMaxThreads(70, 70);
            LavaSystem.StartServerMessage();
            if (File.Exists("text/emotelist.txt"))
                foreach (var item in File.ReadAllLines("text/emotelist.txt"))
                    Player.emoteList.Add(item);
            else
                File.Create("text/emotelist.txt");
            TimeOnline = DateTime.Now;
            DBManager.Initialization();
            if (levels != null)
                levels.ForEach(delegate(Level l) { l.Unload(); });
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
                        var race = new Race();
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
                foreach (var group in Group.groupList) group.playerList = PlayerList.Load(group.fileName, group);
                if (useWhitelist) whiteList = PlayerList.Load("whitelist.txt", null);
            });
            mainLoop.Queue(delegate
            {
                if (File.Exists("text/autoload.txt"))
                {
                    try
                    {
                        var array2 = File.ReadAllLines("text/autoload.txt");
                        foreach (var text in array2)
                        {
                            var text2 = text.Trim();
                            try
                            {
                                if (!(text2 == "") && text2[0] != '#')
                                {
                                    text2.IndexOf("=");
                                    var text3 = text2.Split('=')[0].Trim();
                                    string str;
                                    try
                                    {
                                        str = text2.Split('=')[1].Trim();
                                    }
                                    catch
                                    {
                                        str = "0";
                                    }

                                    if (!text3.Equals(mainLevel.name))
                                    {
                                        Command.all.Find("load").Use(null, text3 + " " + str);
                                        Level.FindExact(text3);
                                    }
                                    else
                                    {
                                        try
                                        {
                                            var num = int.Parse(str);
                                            if (num >= 0 && num <= 4) mainLevel.setPhysics(num);
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
                    return;
                }

                Log("autoload.txt does not exist");
            });
            mainLoop.Queue(delegate
            {
                Log("Creating a listening socket on the port " + port + "... ");
                if (Setup())
                {
                    s.Log("Done.");
                    return;
                }

                s.Log("Could not create a socket connection.  Shutting down...");
            });
            mainLoop.Queue(delegate
            {
                updateTimer.Elapsed += delegate
                {
                    if (GeneralSettings.All.IntelliSys)
                        Player.GlobalUpdate();
                    else
                        Player.GlobalUpdateOld();
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
                catch (Exception ex)
                {
                    ErrorLog(ex);
                }
            });
            mainLoop.Queue(delegate
            {
                if (useHeaven) LavaSystem.LoadHeaven();
                messageTimer.Elapsed += delegate { RandomMessage(); };
                messageTimer.Start();
                adTimer.Elapsed += delegate
                {
                    if (Player.players.Count >= 2)
                        Player.GlobalChatWorld(null, "%0(sun)%8 This server is powered by MCDzienny software.", false);
                };
                adTimer.Start();
                process = Process.GetCurrentProcess();
                if (File.Exists("text/messages.txt"))
                {
                    var streamReader = File.OpenText("text/messages.txt");
                    while (!streamReader.EndOfStream) messages.Add(streamReader.ReadLine());
                    streamReader.Dispose();
                }
                else
                {
                    File.Create("text/messages.txt").Close();
                }

                if (File.Exists("lava/messages.txt"))
                {
                    var streamReader2 = File.OpenText("lava/messages.txt");
                    while (!streamReader2.EndOfStream) lavaTimedMessages.Add(streamReader2.ReadLine());
                    streamReader2.Dispose();
                }
                else
                {
                    File.Create("lava/messages.txt").Close();
                }

                if (File.Exists("infection/messages.txt"))
                {
                    var streamReader3 = File.OpenText("infection/messages.txt");
                    while (!streamReader3.EndOfStream) zombieTimedMessages.Add(streamReader3.ReadLine());
                    streamReader3.Dispose();
                }
                else
                {
                    File.Create("infection/messages.txt").Close();
                }

                if (irc) new IRCBot();
                if (backupInterval > 0) new AutoSaver(backupInterval);
                blockThread = new Timer(blockInterval * 1000);
                blockThread.AutoReset = false;
                blockThread.Elapsed += delegate
                {
                    try
                    {
                        levels.ForEach(delegate(Level l) { l.SaveChanges(); });
                    }
                    catch
                    {
                    }

                    blockThread.Start();
                };
                blockThread.Start();
                var timer2 = new Timer(60000.0);
                timer2.Elapsed += delegate
                {
                    lock (Player.playersThatLeftLocker)
                    {
                        if (Player.playersThatLeft.Count > 100) Player.playersThatLeft.Clear();
                    }
                };
                Server.locationChecker = new System.Timers.Timer(35.0);
                Server.locationChecker.AutoReset = false;
                Server.locationChecker.Elapsed += this.PlayerLocationCheck;
                Server.locationChecker.Start();
                afkTimer.Elapsed += afkTimer_Elapsed;
                afkTimer.AutoReset = false;
                afkTimer.Start();
                pingTimer.Elapsed += pingTimer_Elapsed;
                pingTimer.Start();
                if (!CLI)
                {
                    var timer3 = new Timer(60000.0);
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
                            using (var responseStream = ((HttpWebRequest) WebRequest.Create(Report.Y)).GetResponse()
                                .GetResponseStream())
                            {
                                using (var streamReader4 = new StreamReader(responseStream))
                                {
                                    var a = streamReader4.ReadLine().Trim().ToLower();
                                    if (a == "ok")
                                        flag = true;
                                    else
                                        flag = false;
                                }
                            }
                        }
                        catch
                        {
                        }

                        if (flag != null)
                        {
                            if (CLI)
                            {
                                s.Log(string.Concat("Port ", port, " is ", flag.Value ? "open!" : "closed!"));
                                return;
                            }

                            if (flag.Value)
                            {
                                PortIsOpen.ShowBox();
                                return;
                            }

                            PortIsClosed.ShowBox();
                        }
                    }
                });
            });
            mainLoop.Queue(InteliSys.PacketsMonitoring);
        }

        // Token: 0x06000E7E RID: 3710 RVA: 0x0005147C File Offset: 0x0004F67C
        private void PlayerLocationCheck(object sender, ElapsedEventArgs e)
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
                            var rotx = (byte) rand.Next(170, 185);
                            var roty = (byte) rand.Next(170, 185);
                            p.SendPos(byte.MaxValue, p.pos[0], p.pos[1], p.pos[2], rotx, roty);
                        }
                        else
                        {
                            if (!(p.following != ""))
                            {
                                if (p.possess != "")
                                {
                                    bugPlace = BugPlaceType.Five;
                                    var player = Player.Find(p.possess);
                                    if (player == null || player.level != p.level) p.possess = "";
                                }

                                goto IL_01bb;
                            }

                            bugPlace = BugPlaceType.Two;
                            var player2 = Player.Find(p.following);
                            if (player2 != null && player2.level == p.level)
                            {
                                if (p.canBuild)
                                {
                                    bugPlace = BugPlaceType.Four;
                                    p.SendPos(byte.MaxValue, player2.pos[0], (ushort) (player2.pos[1] - 16),
                                        player2.pos[2], player2.rot[0], player2.rot[1]);
                                }
                                else
                                {
                                    p.SendPos(byte.MaxValue, player2.pos[0], player2.pos[1], player2.pos[2],
                                        player2.rot[0], player2.rot[1]);
                                }

                                goto IL_01bb;
                            }

                            bugPlace = BugPlaceType.Three;
                            p.following = "";
                            if (!p.canBuild) p.canBuild = true;
                            if (player2 != null && player2.possess == p.name) player2.possess = "";
                        }

                        goto end_IL_0000;
                        IL_01bb:
                        bugPlace = BugPlaceType.Six;
                        bugPlace = BugPlaceType.Eight;
                        var num = (ushort) (p.pos[0] / 32);
                        var num2 = (ushort) (p.pos[1] / 32);
                        var num3 = (ushort) (p.pos[2] / 32);
                        if (!p.Loading && p.level.Death) p.RealDeath(num, num2, num3);
                        bugPlace = BugPlaceType.Seven;
                        p.CheckBlock(num, num2, num3);
                        p.oldBlock = (ushort) (num + num2 + num3);
                        end_IL_0000: ;
                    }
                    catch (Exception ex)
                    {
                        s.Log("!!! BUG PLACE: " + bugPlace);
                        ErrorLog(ex);
                    }
                });
            }
            catch
            {
            }

            locationChecker.Start();
        }

        // Token: 0x06000E7F RID: 3711 RVA: 0x000514CC File Offset: 0x0004F6CC
        private void pingTimer_Elapsed(object sender, ElapsedEventArgs e)
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

        // Token: 0x06000E80 RID: 3712 RVA: 0x0005151C File Offset: 0x0004F71C
        private void afkTimer_Elapsed(object sender, ElapsedEventArgs e)
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
                            return;
                        }

                        if (p.group == null)
                        {
                            s.Log("Error afk: p.group == null");
                            return;
                        }

                        if (p.name == null)
                        {
                            s.Log("Error afk: p.name == null");
                            return;
                        }

                        if (p.pos == null)
                        {
                            s.Log("Error afk: p.pos == null");
                            return;
                        }

                        if (p.group.Permission < LevelPermission.Operator && p.afkCount > afkkick * 30)
                        {
                            p.disconnectionReason = DisconnectionReason.AutoKicked;
                            afkset.Remove(p.name);
                            p.Kick(string.Format(Lang.Player.KickAfk, afkkick));
                            return;
                        }

                        if (!afkset.Contains(p.name))
                        {
                            if (!IsPlayerMoving(p))
                                p.afkCount++;
                            else
                                p.afkCount = 0;
                            if (p.afkCount > afkminutes * 30)
                                Command.all.Find("afk").Use(p, string.Format(Lang.Player.AfkMessage, afkminutes));
                            return;
                        }

                        if (IsPlayerMoving(p))
                        {
                            p.afkCount = 0;
                            Command.all.Find("afk").Use(p, "");
                            return;
                        }

                        p.afkCount++;
                    });
                }
                catch (Exception ex)
                {
                    ErrorLog(ex);
                }
            }

            if (afkminutes > 0) afkTimer.Start();
        }

        // Token: 0x06000E81 RID: 3713 RVA: 0x000515A8 File Offset: 0x0004F7A8
        private static bool IsPlayerMoving(Player p)
        {
            return (p.oldpos[0] != p.pos[0] || p.oldpos[1] != p.pos[1] || p.oldpos[2] != p.pos[2]) &&
                   (p.oldrot[0] != p.rot[0] || p.oldrot[1] != p.rot[1]);
        }

        // Token: 0x06000E82 RID: 3714 RVA: 0x00051624 File Offset: 0x0004F824
        public static bool Setup()
        {
            bool result;
            try
            {
                var ipendPoint = new IPEndPoint(IPAddress.Any, port);
                listen = new Socket(ipendPoint.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                listen.Bind(ipendPoint);
                listen.Listen(int.MaxValue);
                listen.BeginAccept(Accept, null);
                result = true;
            }
            catch (SocketException ex)
            {
                ErrorLog(ex);
                result = false;
            }
            catch (Exception ex2)
            {
                ErrorLog(ex2);
                result = false;
            }

            return result;
        }

        // Token: 0x06000E83 RID: 3715 RVA: 0x000516C4 File Offset: 0x0004F8C4
        private static void Accept(IAsyncResult result)
        {
            if (shuttingDown) return;
            Player player = null;
            if (DateTime.Now < serverLockTime)
            {
                var socket = listen.EndAccept(result);
                var text = "Server is Locked. Lock release in: " + (int) (serverLockTime - DateTime.Now).TotalSeconds +
                           "s";
                var array = new byte[64];
                array = enc.GetBytes(text.PadRight(64).Substring(0, 64));
                var array2 = new byte[array.Length + 1];
                array2[0] = 14;
                Buffer.BlockCopy(array, 0, array2, 1, text.Length);
                socket.BeginSend(array2, 0, array2.Length, SocketFlags.None, delegate { }, null);
                socket.Close();
            }
            else
            {
                try
                {
                    player = new Player(listen.EndAccept(result));
                }
                catch
                {
                }
            }

            while (true)
                try
                {
                    listen.BeginAccept(Accept, null);
                    return;
                }
                catch (SocketException)
                {
                    if (player != null) player.Disconnect();
                    serverLockTime = DateTime.Now.AddSeconds(45.0);
                    s.Log("Error: Socket exception. Server lock for 45 sec.");
                    Thread.Sleep(1000);
                }
                catch (Exception ex2)
                {
                    ErrorLog(ex2);
                    if (player != null) player.Disconnect();
                    Thread.Sleep(1000);
                }
        }

        // Token: 0x06000E84 RID: 3716 RVA: 0x00051864 File Offset: 0x0004FA64
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
                    return;
                }

                p.Kick(customShutdownMessage);
            });
            shuttingDown = true;
            if (listen != null) listen.Close();
        }

        // Token: 0x06000E85 RID: 3717 RVA: 0x000518E0 File Offset: 0x0004FAE0
        public static void AddLevel(Level level)
        {
            lock (levelListLocker)
            {
                levels.Add(level);
            }

            if (!CLI) Window.thisWindow.UpdateMainMapListView();
        }

        // Token: 0x06000E86 RID: 3718 RVA: 0x00051930 File Offset: 0x0004FB30
        public static void RemoveLevel(Level level)
        {
            lock (levelListLocker)
            {
                levels.Remove(level);
            }

            if (!CLI) Window.thisWindow.UpdateMainMapListView();
        }

        // Token: 0x06000E87 RID: 3719 RVA: 0x00051980 File Offset: 0x0004FB80
        public void PlayerListUpdate()
        {
            if (s.OnPlayerListChange != null) s.OnPlayerListChange();
            try
            {
                var playerItem = new List<string[]>();
                Player.players.ForEachSync(delegate(Player p)
                {
                    if (p == null)
                    {
                        ErrorLog(new string('-', 25) + Environment.NewLine + "Error: PlayerListUpdate() p == null");
                        return;
                    }

                    if (p.group == null)
                    {
                        ErrorLog(
                            new string('-', 25) + Environment.NewLine + "Error: PlayerListUpdate() p.group == null");
                        return;
                    }

                    if (p.name == null)
                    {
                        ErrorLog(new string('-', 25) + Environment.NewLine +
                                 "Error: PlayerListUpdate() p.name == null");
                        return;
                    }

                    if (p.playerLevelName == null)
                    {
                        ErrorLog(new string('-', 25) + Environment.NewLine +
                                 "Error: PlayerListUpdate() p.playerLevelName == null");
                        return;
                    }

                    string[] item =
                    {
                        p.group.trueName,
                        p.name,
                        p.playerLevelName,
                        p.afkCount / 30 == 0 ? "" : p.afkCount / 30 + " min."
                    };
                    playerItem.Add(item);
                });
                if (!CLI) Window.thisWindow.UpdatePlayerListView(playerItem);
            }
            catch (Exception ex)
            {
                ErrorLog(ex);
            }
        }

        // Token: 0x06000E88 RID: 3720 RVA: 0x000519FC File Offset: 0x0004FBFC
        public void FailBeat()
        {
            if (HeartBeatFail != null) HeartBeatFail();
        }

        // Token: 0x06000E89 RID: 3721 RVA: 0x00051A14 File Offset: 0x0004FC14
        public void UpdateUrl(string url)
        {
            if (OnURLChange != null) OnURLChange(url);
        }

        // Token: 0x06000E8A RID: 3722 RVA: 0x00051A2C File Offset: 0x0004FC2C
        public void Log(string message, bool systemMsg = false)
        {
            if (OnLog != null)
            {
                if (!systemMsg)
                {
                    var msg = DateTime.Now.ToString("(HH:mm:ss) ") + message;
                    OnLog(msg);
                }
                else
                {
                    OnSystem(DateTime.Now.ToString("(HH:mm:ss) ") + message);
                }
            }

            if (!systemMsg) Logger.Write(DateTime.Now.ToString("(HH:mm:ss) ") + message + Environment.NewLine);
        }

        // Token: 0x06000E8B RID: 3723 RVA: 0x00051ADC File Offset: 0x0004FCDC
        public void ErrorCase(string message)
        {
            if (OnError != null) OnError(message);
        }

        // Token: 0x06000E8C RID: 3724 RVA: 0x00051AF4 File Offset: 0x0004FCF4
        public void CommandUsed(string message)
        {
            if (OnCommand != null) OnCommand(DateTime.Now.ToString("(HH:mm:ss) ") + message);
            Logger.WriteCommand(DateTime.Now.ToString("(HH:mm:ss) ") + message + Environment.NewLine);
        }

        // Token: 0x06000E8D RID: 3725 RVA: 0x00051B7C File Offset: 0x0004FD7C
        public static void ErrorLog(Exception ex)
        {
            Logger.WriteError(ex);
            try
            {
                s.Log("!!!Error! See " + Logger.ErrorLogPath + " for more information.");
            }
            catch
            {
            }
        }

        // Token: 0x06000E8E RID: 3726 RVA: 0x00051BC4 File Offset: 0x0004FDC4
        public static void ErrorLog(string message)
        {
            Logger.WriteError(message);
            try
            {
                s.Log("!!!Error! See " + Logger.ErrorLogPath + " for more information.");
            }
            catch
            {
            }
        }

        // Token: 0x06000E8F RID: 3727 RVA: 0x00051C0C File Offset: 0x0004FE0C
        public static void RandomMessage()
        {
            if (Player.number != 0)
            {
                if (messages.Count > 0) Player.GlobalMessage(messages[new Random().Next(0, messages.Count)]);
                if (lavaTimedMessages.Count > 0 && LavaLevel != null)
                    Player.GlobalMessageLevel(LavaLevel,
                        lavaTimedMessages[new Random().Next(0, lavaTimedMessages.Count)]);
                if (zombieTimedMessages.Count > 0 && InfectionLevel != null)
                    Player.GlobalMessageLevel(InfectionLevel,
                        zombieTimedMessages[new Random().Next(0, zombieTimedMessages.Count)]);
            }
        }

        // Token: 0x06000E90 RID: 3728 RVA: 0x00051CD0 File Offset: 0x0004FED0
        internal void SettingsUpdate()
        {
            if (OnSettingsUpdate != null) OnSettingsUpdate();
        }

        // Token: 0x06000E91 RID: 3729 RVA: 0x00051CE8 File Offset: 0x0004FEE8
        public static string FindColor(string Username)
        {
            foreach (var group in Group.groupList)
                if (group.playerList.Contains(Username))
                    return group.color;
            return Group.standard.color;
        }

        // Token: 0x06000E92 RID: 3730 RVA: 0x00051D58 File Offset: 0x0004FF58
        public static bool IsFreebuildModeOn()
        {
            return mode == Mode.Freebuild || mode == Mode.LavaFreebuild || mode == Mode.ZombieFreebuild;
        }

        // Token: 0x06000E93 RID: 3731 RVA: 0x00051D7C File Offset: 0x0004FF7C
        public static bool IsZombieModeOn()
        {
            return mode == Mode.Zombie || mode == Mode.ZombieFreebuild;
        }

        // Token: 0x06000E94 RID: 3732 RVA: 0x00051D94 File Offset: 0x0004FF94
        public static bool IsLavaModeOn()
        {
            return mode == Mode.Lava || mode == Mode.LavaFreebuild;
        }

        // Token: 0x02000209 RID: 521
        private enum BugPlaceType
        {
            // Token: 0x0400084C RID: 2124
            None,

            // Token: 0x0400084D RID: 2125
            One,

            // Token: 0x0400084E RID: 2126
            Two,

            // Token: 0x0400084F RID: 2127
            Three,

            // Token: 0x04000850 RID: 2128
            Four,

            // Token: 0x04000851 RID: 2129
            Five,

            // Token: 0x04000852 RID: 2130
            Six,

            // Token: 0x04000853 RID: 2131
            Seven,

            // Token: 0x04000854 RID: 2132
            Eight,

            // Token: 0x04000855 RID: 2133
            Nine,

            // Token: 0x04000856 RID: 2134
            Ten,

            // Token: 0x04000857 RID: 2135
            Eleven,

            // Token: 0x04000858 RID: 2136
            Twelve
        }

        // Token: 0x02000210 RID: 528
        public struct TempBan
        {
            // Token: 0x04000859 RID: 2137
            public string name;

            // Token: 0x0400085A RID: 2138
            public DateTime allowedJoin;
        }

        // Token: 0x02000211 RID: 529
        public struct levelID
        {
            // Token: 0x0400085B RID: 2139
            public int ID;

            // Token: 0x0400085C RID: 2140
            public string name;
        }
    }
}
