using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Resources;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using MCDzienny.Communication;
using MCDzienny.Cpe;
using MCDzienny.Database;
using MCDzienny.Gui;
using MCDzienny.InfectionSystem;
using MCDzienny.Levels.Effects;
using MCDzienny.Misc;
using MCDzienny.MultiMessages;
using MCDzienny.Plugins;
using MCDzienny.Plugins.KeyboardShortcuts;
using MCDzienny.RemoteAccess;
using MCDzienny.Settings;
using Environment = MCDzienny.Levels.Effects.Environment;
using Timer = System.Timers.Timer;

namespace MCDzienny
{
    public sealed class Player : Entity
    {

        public delegate void BlockchangeEventHandler(Player p, ushort x, ushort y, ushort z, byte type);

        public delegate void BlockchangeEventHandler2(Player p, ushort x, ushort y, ushort z, byte type, byte action);

        public delegate void BlockPlacedUnderHandler(Player p, int x, int y, int z, ref bool stopChange);

        public delegate void PlayerChatEventHandler(Player p, ref string message, ref bool passIt);

        public delegate void PlayerRegisteredCheckEventHandler(Player p, ref bool isRegistered);

        public delegate void ProperBlockChangeEH(Player p, ushort x, ushort y, ushort z, byte type, byte action, ref bool stopChange);

        public const int ActionDestroyBlock = 0;

        public const int ActionBuildBlock = 1;

        const string KeyRecentCommandsTimes = "RecentCommandsTimes";

        public static readonly bool Debug_NotifyConsolAboutPlayersClient;

        internal static object playersThatLeftLocker;

        public static PlayerCollection players;

        public static Dictionary<string, string> playersThatLeft;

        public static List<string> emoteList;

        public static int totalMySQLFailed;

        static readonly ASCIIEncoding enc;

        public static bool storeHelp;

        public static string storedHelp;

        public static int spamBlockCount;

        public static int spamBlockTimer;

        public static int spamChatCount;

        public static int spamChatTimer;

        internal static ResourceManager rm;

        static byte lastID;

        readonly List<int> analyse = new List<int>();

        readonly ushort[] basepos = new ushort[3];

        readonly Dictionary<int, DateTime> blockCommandCooldowns = new Dictionary<int, DateTime>();

        readonly Timer extraTimer = new Timer(22000.0);

        readonly List<int> fallPositionBuffer = new List<int>();

        readonly object fallSynchronize = new object();

        readonly object hacksDetectionSync = new object();

        readonly Timer loginTimer = new Timer(1000.0);

        readonly Queue<DateTime> spamBlockLog = new Queue<DateTime>(spamBlockCount);

        readonly byte[] tempbuffer = new byte[255];

        readonly List<byte[]> virtualBlocks = new List<byte[]>();

        public int afkCount;

        public DateTime afkStart;

        public bool aiming;

        DateTime armorTime = DateTime.Now;

        public int bestScore;

        public byte[] bindings = new byte[128];

        public byte BlockAction;

        public object blockchangeObject;

        public BlockChanges BlockChanges;

        public bool boughtColor;

        public bool boughtFarewell;

        public bool boughtTColor;

        public bool boughtTitle;

        public bool boughtWelcome;

        byte[] buffer = new byte[0];

        public volatile bool canBuild = true;

        public bool carryingFlag;

        internal volatile int clippingTime;

        public string[] cmdBind = new string[10];

        public bool cmdTimer;

        public string color;

        public Thread commThread;

        public bool commUse;

        public short connectionSpeed = -1;

        public bool copyAir;

        public List<CopyPos> CopyBuffer = new List<CopyPos>();

        public int[] copyoffset = new int[3];

        public ushort[] copystart = new ushort[3];

        public Core core;

        public bool countToAve;

        public Support Cpe = new Support();

        public string CTFtempcolor;

        public string CTFtempprefix;

        bool databaseLoadFailure;

        public byte deathBlock;

        public ushort deathCount;

        public bool deleteMode;

        volatile int direction;

        public volatile bool disconnected;

        public DisconnectionReason disconnectionReason = DisconnectionReason.Unknown;

        bool doCommand;

        public Dictionary<string, object> extraData = new Dictionary<string, object>();

        int extToRead;

        string farewellMessage_ = "";

        public DateTime firstLogin;

        int flags;

        PlayersFlags flagsCollection = new PlayersFlags();

        internal volatile int flyingTime;

        public string following = "";

        public bool frozen;

        public volatile bool fullylogged;

        DateTime fullyloggedtime;

        public byte GreenYellowRed;

        public Group group;

        volatile bool hackDetected;

        public int hackWarnings;

        public Team hasflag;

        public bool hasTeleport;

        public int health = 100;

        public byte id;

        public bool ignoreGrief;

        public bool ignorePermission;

        public bool inHeaven;

        public bool isFlying;

        bool isUsingTextures = true;

        public bool isZombie;

        public bool jailed;

        public ushort[] lastClick = new ushort[3];

        public string lastCMD = "";

        public DateTime lastDeath = DateTime.Now;

        byte lives_ = 3;

        public bool Loading = true;

        public bool loggedIn;

        public int loginBlocks;

        internal DateTime mapLoadedTime = DateTime.MaxValue;

        public bool mapLoading;

        DateTime mapRatingCooldown = DateTime.Now;

        public bool megaBoid;

        public string[] messageBind = new string[10];

        string modelName;

        public byte modeType;

        volatile int msgDown;

        volatile int msgUp;

        public bool muted;

        public DateTime muteTime = DateTime.MinValue;

        public string name;

        public ushort oldBlock;

        internal volatile ushort[] oldpos = new ushort[3];

        internal byte[] oldrot = new byte[2];

        volatile int oldRotX;

        volatile int oldRotY;

        volatile int oldx = -1;

        volatile int oldy = -1;

        volatile int oldz = -1;

        volatile bool omitHackDetection;

        public bool onTrain;

        public bool onWhitelist;

        public bool opchat;

        BlockPos originalFallPos;

        internal volatile int outsideMapTime;

        public long overallBlocks;

        public bool painting;

        public bool parseSmiley = true;

        public int pendingPackets;

        int playerExperienceOnZombie;

        Level pLevel;

        public volatile ushort[] pos = new ushort[3];

        public byte[] posBuffer;

        public string possess = "";

        string prefix_ = "";

        public string prevMsg = "";

        public bool readyForAve;

        public string realName;

        public List<UndoPos> RedoBuffer = new List<UndoPos>();

        public byte[] rot = new byte[2];

        int roundsOnZombie;

        public string savedcolor = "";

        public int score;

        internal bool sendLock;

        volatile bool shouldFall;

        bool showAlias = true;

        public bool showMBs;

        public bool showPortals;

        string skinName;

        public bool smileySaved = true;

        public Socket socket;

        Queue<DateTime> spamChatLog = new Queue<DateTime>(spamChatCount);

        public bool spawning;

        string starsTag = "";

        public bool staticCommands;

        bool stopTheText;

        public string storedMessage = "";

        public Team team;

        public bool teamchat;

        int tier_ = 1;

        public DateTime timeLogged;

        public int timesWon;

        public string title = "";

        public string titlecolor;

        public bool toBeMoved;

        public int totalKicked;

        public int totalLogins;

        int totalMinutesPlayed;

        public bool trainGrab;

        public UndoBufferCollection UndoBuffer;

        public volatile bool updatePosition = true;

        public int userID = -1;

        public bool usesWom;

        public bool voice;

        public string voicestring = "";

        public VotingSystem.VotingChoice votingChoice;

        volatile bool waitForFall;

        int warnedForHacksTimes;

        string welcomeMessage_ = "";

        public bool whisper;

        public string whisperTo = "";

        internal int winningStreak;

        public string WomVersion = "";

        int wonAsHumanTimes;

        int wonAsZombieTimes;

        int zombifiedCount;

        public bool ZoneCheck;

        public bool zoneDel;

        public DateTime ZoneSpam;

        static Player()
        {
            PlayerChatEvent = null;
            PlayerRegisteredCheck = null;
            Debug_NotifyConsolAboutPlayersClient = true;
            playersThatLeftLocker = new object();
            players = new PlayerCollection();
            playersThatLeft = new Dictionary<string, string>();
            emoteList = new List<string>();
            totalMySQLFailed = 0;
            enc = new ASCIIEncoding();
            storeHelp = false;
            storedHelp = "";
            spamBlockCount = 200;
            spamBlockTimer = 5;
            spamChatCount = 3;
            spamChatTimer = 4;
            rm = new ResourceManager("MCDzienny.Lang.Player", Assembly.GetExecutingAssembly());
            lastID = 0;
            PlayerPillared += Player_BlockPlacedUnder;
            players.PlayerAdded += players_PlayerAdded;
            players.PlayerRemoved += players_PlayerRemoved;
        }

        public Player(Level level)
            : base(level) {}

        public Player(Socket s)
            : base(null)
        {
            UndoBuffer = new UndoBufferCollection(this);
            BlockChanges = new BlockChanges(this);
            try
            {
                socket = s;
                socket.LingerState.Enabled = false;
                ip = socket.RemoteEndPoint.ToString().Split(':')[0];
                Server.s.Log(string.Format("{0} connected to the server.", ip));
                for (byte b = 0; b < 128; b++)
                {
                    bindings[b] = b;
                }
                socket.BeginReceive(tempbuffer, 0, tempbuffer.Length, SocketFlags.None, Receive, this);
                loginTimer.Elapsed += delegate
                {
                    if (disconnected)
                    {
                        loginTimer.Close();
                        loginTimer.Dispose();
                    }
                    else if (!Loading)
                    {
                        loginTimer.Stop();
                        if (File.Exists("text/welcome.txt"))
                        {
                            try
                            {
                                var list = new List<string>();
                                using (StreamReader streamReader = File.OpenText("text/welcome.txt"))
                                {
                                    while (!streamReader.EndOfStream)
                                    {
                                        list.Add(streamReader.ReadLine());
                                    }
                                }
                                foreach (string item in list)
                                {
                                    SendMessage(item);
                                }
                            }
                            catch {}
                        }
                        else
                        {
                            Server.s.Log("Could not find Welcome.txt. Using default.");
                            File.WriteAllText("text/welcome.txt", "Welcome to my server!");
                        }
                        extraTimer.Start();
                        loginTimer.Close();
                        loginTimer.Dispose();
                    }
                    else
                    {
                        loginTimer.Start();
                    }
                };
                extraTimer.AutoReset = false;
                loginTimer.AutoReset = false;
                loginTimer.Start();
                extraTimer.Elapsed += delegate
                {
                    extraTimer.Stop();
                    try
                    {
                        if (Server.mode != 0)
                        {
                            using (DataTable dataTable = DBInterface.fillData("SELECT * FROM `Inbox" + name + "`", skipError: true))
                            {
                                SendMessage(string.Format(rm.GetString("WelcomeInbox"), dataTable.Rows.Count));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Server.ErrorLog(ex);
                    }
                    if (Server.updateTimer.Interval > 1000.0)
                    {
                        SendMessage(rm.GetString("WelcomeLowLag"));
                    }
                    try
                    {
                        SendMessage(string.Format(rm.GetString("WelcomeMoney"), money + Server.DefaultColor + " " + Server.moneys));
                    }
                    catch {}
                    SendMessage(string.Format(rm.GetString("WelcomeModified"), overallBlocks + Server.DefaultColor));
                    if (players.Count == 1)
                    {
                        SendMessage(string.Format(rm.GetString("WelcomePlayerCount"), players.Count));
                    }
                    else
                    {
                        SendMessage(string.Format(rm.GetString("WelcomePlayersCount"), players.Count));
                    }
                    try
                    {
                        if (!Group.Find("Nobody").commands.Contains("award") && !Group.Find("Nobody").commands.Contains("awards") &&
                            !Group.Find("Nobody").commands.Contains("awardmod"))
                        {
                            SendMessage(string.Format(rm.GetString("WelcomeAwards"), Awards.awardAmount(name)));
                        }
                    }
                    catch {}
                    extraTimer.Close();
                    extraTimer.Dispose();
                };
                BlockChangeProper += Player_BlockChangeProper;
            }
            catch (SocketException ex2)
            {
                Kick("Login failed!");
                if (ex2.ErrorCode != 10054)
                {
                    Server.ErrorLog(ex2);
                }
            }
            catch (Exception ex3)
            {
                Kick("Login failed!");
                Server.ErrorLog(ex3);
            }
        }

        public Pos3I HeadPosition
        {
            get
            {
                Pos3I result = default(Pos3I);
                result.X = (int)XFloat;
                float num = XFloat - (int)XFloat;
                if (num > 0.6875f)
                {
                    result.X = (int)XFloat + 1;
                }
                else if (num < 9f / 32f)
                {
                    result.X = (int)XFloat - 1;
                }
                result.Z = (int)ZFloat;
                num = ZFloat - (int)ZFloat;
                if (num > 0.6875f)
                {
                    result.Z = (int)ZFloat + 1;
                }
                else if (num < 9f / 32f)
                {
                    result.Z = (int)ZFloat - 1;
                }
                result.Y = (int)YFloat;
                num = YFloat - (int)YFloat;
                if (num < 3f / 32f)
                {
                    result.Y--;
                }
                return result;
            }
        }

        public byte HeadBlock
        {
            get
            {
                Pos3I headPosition = HeadPosition;
                return level.GetTile(headPosition.X, headPosition.Y, headPosition.Z);
            }
        }

        public byte FootBlock
        {
            get
            {
                Pos3I headPosition = HeadPosition;
                return level.GetTile(headPosition.X, headPosition.Y - 1, headPosition.Z);
            }
        }

        public AuthenticationProvider Authentication { get; private set; }

        public static byte number { get { return (byte)players.Count; } }

        public bool IsTempMuted { get { return muteTime > DateTime.Now; } }

        public bool ShowAlias { get { return showAlias; } set { showAlias = value; } }

        [Browsable(false)]
        public bool IsPrinting { get; set; }

        public string PublicName { get; set; }

        [Browsable(false)]
        public PlayersFlags FlagsCollection { get { return flagsCollection; } set { flagsCollection = value; } }

        public int TotalMinutesPlayed
        {
            get { return totalMinutesPlayed + (int)DateTime.Now.Subtract(timeLogged).TotalMinutes; }
            set
            {
                int num = value - (int)DateTime.Now.Subtract(timeLogged).TotalMinutes;
                totalMinutesPlayed = num >= 0 ? num : 0;
            }
        }

        [Category("General")]
        [ReadOnly(true)]
        [Description("Ip address.")]
        public string ip { get; set; }

        [Description("Describes wheter a player is hidden or not.")]
        [Category("General")]
        [ReadOnly(true)]
        public bool hidden { get; set; }

        [Description("Indicates wheter a player is invincible.")]
        [Category("General")]
        public bool invincible { get; set; }

        [ReadOnly(true)]
        [Description("Title of the player. Change made here will be temporary, it doesn't write the change to database.")]
        [Category("General")]
        public string prefix { get { return prefix_; } set { prefix_ = value; } }

        [Description("The amount of money.")]
        [Category("General")]
        public int money { get; set; }

        [Category("General")]
        [Description("Overall death counter.")]
        public int overallDeath { get; set; }

        [Description("Determines wheter player is in joker mode. Joker mode substitutes players words with the phrases from joker.txt file.")]
        [Category("Fun")]
        public bool joker { get; set; }

        [RefreshProperties(RefreshProperties.Repaint)]
        [ReadOnly(true)]
        [Description("Shows the name of the map that players is currently in.")]
        [Category("General")]
        public string playerLevelName { get { return level.name; } }

        [Browsable(false)]
        public new Level level
        {
            get
            {
                if (pLevel == null)
                {
                    if (Server.mode == Mode.Lava)
                    {
                        pLevel = Server.LavaLevel;
                        return pLevel;
                    }
                    if (Server.mode == Mode.Freebuild || Server.mode == Mode.LavaFreebuild || Server.mode == Mode.ZombieFreebuild)
                    {
                        pLevel = Server.mainLevel;
                        return pLevel;
                    }
                    pLevel = Server.InfectionLevel;
                    return pLevel;
                }
                return pLevel;
            }
            set { pLevel = value; }
        }

        [Description("The message that is being shown when a player logs in.")]
        [Category("General")]
        public string welcomeMessage { get { return welcomeMessage_; } set { welcomeMessage_ = value; } }

        [Description("The message that is being shown when a player logs out.")]
        [Category("General")]
        public string farewellMessage { get { return farewellMessage_; } set { farewellMessage_ = value; } }

        [Category("General")]
        [Description("Determines whether player is allowed to use hacks(WOM client).")]
        public bool hacksAllowed { get; set; }

        [Description("Determines wheter player has a fliped head or not.")]
        [Category("Fun")]
        public bool flipHead { get; set; }

        [Description("The amount of water blocks that the player currently has.")]
        [Category("LavaItems")]
        public int waterBlocks { get; set; }

        [Category("LavaItems")]
        [Description("The amount of door blocks that the player currently has.")]
        public int doorBlocks { get; set; }

        [Description("The amount of sponge blocks that the player currently has.")]
        [Category("LavaItems")]
        public int spongeBlocks { get; set; }

        [Category("Lava")]
        [Description("The amount of lives that the player currently has.")]
        public byte lives { get { return lives_; } set { lives_ = value; } }

        [Description("The player's total experience/score.")]
        [Category("Lava")]
        public int totalScore { get; set; }

        [ReadOnly(true)]
        [Browsable(false)]
        [Description("The player's tier.")]
        [Category("Lava")]
        public int tier { get { return tier_; } set { tier_ = value; } }

        [Category("LavaItems")]
        [Description("The amount of hammer that the player currently has.")]
        public int hammer { get; set; }

        public IronChallengeType IronChallenge { get; set; }

        [Browsable(false)]
        public Dictionary<string, object> ExtraData { get { return extraData; } set { extraData = value; } }

        [Browsable(false)]
        public string IronChallengeTag
        {
            get
            {
                if (level.mapType != MapType.Lava)
                {
                    return "";
                }
                if (IronChallenge == IronChallengeType.IronMan)
                {
                    return "%f(male) ";
                }
                if (IronChallenge == IronChallengeType.IronWoman)
                {
                    return "%f(female)   ";
                }
                return "";
            }
        }

        [Browsable(false)]
        public bool IsAboveSeaLevel
        {
            get
            {
                if (pos[1] / 32 - 1 > level.height / 2)
                {
                    return true;
                }
                return false;
            }
        }

        public bool IsUsingXWom { get; set; }

        public bool IsUsingWom { get; set; }

        public bool IsUsingTextures
        {
            get
            {
                if (IsUsingWom)
                {
                    return isUsingTextures;
                }
                return false;
            }
        }

        [Browsable(false)]
        public DateTime MapRatingCooldown { get { return mapRatingCooldown; } set { mapRatingCooldown = value; } }

        [Browsable(false)]
        public float XFloat { get { return pos[0] / 32f; } }

        [Browsable(false)]
        public float YFloat { get { return pos[1] / 32f; } }

        [Browsable(false)]
        public float ZFloat { get { return pos[2] / 32f; } }

        public int WarnedForHacksTimes
        {
            get { return warnedForHacksTimes; }
            set
            {
                warnedForHacksTimes = value;
                if (warnedForHacksTimes > 6)
                {
                    Kick(rm.GetString("KickHacks"));
                }
            }
        }

        public string StarsTag
        {
            get
            {
                if (level.mapType == MapType.Zombie)
                {
                    return starsTag;
                }
                if (level.mapType == MapType.Lava)
                {
                    return LavaPrefix;
                }
                return "";
            }
            set { starsTag = value; }
        }

        public string LavaPrefix
        {
            get
            {
                if (!LavaSettings.All.StarSystem)
                {
                    return "";
                }
                if (winningStreak == 1)
                {
                    return string.Concat(MCColor.DarkGray, "*");
                }
                if (winningStreak == 2)
                {
                    return string.Concat(MCColor.DarkGray, "**");
                }
                if (winningStreak >= 3)
                {
                    return string.Concat(MCColor.DarkGray, "***");
                }
                return "";
            }
        }

        public bool IsOutsideMap
        {
            get
            {
                if (level != null && ((int)XFloat < 0 || (int)YFloat < 1 || (int)ZFloat < 0 || (int)XFloat > level.width - 1 || (int)YFloat > level.height + 2 ||
                        (int)ZFloat > level.depth - 1))
                {
                    return true;
                }
                return false;
            }
        }

        public bool IsClipping
        {
            get
            {
                if (level != null && (!Block.Standable(level.GetTile((int)XFloat, (int)YFloat, (int)ZFloat)) ||
                        !Block.Standable(level.GetTile((int)XFloat, (int)YFloat - 1, (int)ZFloat))))
                {
                    return true;
                }
                return false;
            }
        }

        public bool IsAirborn
        {
            get
            {
                if (level != null)
                {
                    byte[] blocksBelow = BlocksBelow;
                    foreach (byte tile in blocksBelow)
                    {
                        if (!Block.IsAir(tile))
                        {
                            return false;
                        }
                    }
                    return true;
                }
                return false;
            }
        }

        public byte[] BlocksBelow
        {
            get
            {
                if (level != null)
                {
                    float num = XFloat - (int)XFloat;
                    int num2 = -1;
                    int num3 = -1;
                    int num4 = (int)(YFloat - 2.2f);
                    if (num > 0.6875f)
                    {
                        num2 = (int)XFloat + 1;
                    }
                    else if (num < 0.2813f)
                    {
                        num2 = (int)XFloat - 1;
                    }
                    num = ZFloat - (int)ZFloat;
                    if (num > 0.6875f)
                    {
                        num3 = (int)ZFloat + 1;
                    }
                    else if (num < 0.2813f)
                    {
                        num3 = (int)ZFloat - 1;
                    }
                    if (num2 != -1 && num3 == -1)
                    {
                        return new byte[2]
                        {
                            level.GetTile((int)XFloat, num4, (int)ZFloat), level.GetTile(num2, num4, (int)ZFloat)
                        };
                    }
                    if (num2 == -1 && num3 != -1)
                    {
                        return new byte[2]
                        {
                            level.GetTile((int)XFloat, num4, (int)ZFloat), level.GetTile((int)XFloat, num4, num3)
                        };
                    }
                    if (num2 == -1 && num3 == -1)
                    {
                        return new byte[1]
                        {
                            level.GetTile((int)XFloat, num4, (int)ZFloat)
                        };
                    }
                    return new byte[4]
                    {
                        level.GetTile((int)XFloat, num4, (int)ZFloat), level.GetTile(num2, num4, (int)ZFloat), level.GetTile((int)XFloat, num4, num3),
                        level.GetTile(num2, num4, num3)
                    };
                }
                return new byte[0];
            }
        }

        public bool IsCpeSupported { get; private set; }

        [Browsable(false)]
        public string Tag
        {
            get
            {
                if (level.mapType == MapType.Zombie)
                {
                    if (IsRefree)
                    {
                        return InfectionSettings.All.RefreeTag;
                    }
                    if (InfectionSystem.InfectionSystem.infected.Contains(this))
                    {
                        return InfectionSettings.All.ZombieTag;
                    }
                    return InfectionSettings.All.HumanTag;
                }
                return "";
            }
        }

        [ReadOnly(true)]
        public bool IsRefree { get; set; }

        [Description("A player's level. Displays player's level on lava when player is on a lava map, and zombie level when player is on a zombie map.")]
        [ReadOnly(true)]
        public string Tier
        {
            get
            {
                if (level.mapType == MapType.Zombie && InfectionSettings.All.UsePlayerLevels)
                {
                    return string.Format("%f({0})", ZombieTier);
                }
                if (level.mapType == MapType.Lava && LavaSettings.All.ScoreMode != ScoreSystem.NoScore)
                {
                    return string.Format("%f({0})", LavaTier);
                }
                return "";
            }
        }

        [ReadOnly(true)]
        [Category("Zombie")]
        public int ZombieTier { get { return InfectionTiers.GetTier(PlayerExperienceOnZombie); } }

        [Browsable(false)]
        [Category("Zombie")]
        public int LavaTier { get { return tier_; } set { tier_ = value; } }

        [ReadOnly(false)]
        [Category("Zombie")]
        public int PlayerExperienceOnZombie { get { return playerExperienceOnZombie; } set { playerExperienceOnZombie = value; } }

        [Category("Zombie")]
        [ReadOnly(true)]
        public int WonAsZombieTimes { get { return wonAsZombieTimes; } set { wonAsZombieTimes = value; } }

        [Category("Zombie")]
        [ReadOnly(true)]
        public int WonAsHumanTimes { get { return wonAsHumanTimes; } set { wonAsHumanTimes = value; } }

        [ReadOnly(true)]
        [Category("Zombie")]
        public int ZombifiedCount { get { return zombifiedCount; } set { zombifiedCount = value; } }

        [Category("Zombie")]
        [ReadOnly(true)]
        public int RoundsOnZombie { get { return roundsOnZombie; } set { roundsOnZombie = value; } }

        [Browsable(false)]
        public DateTime LastPillar { get; set; }

        public string ModelName
        {
            get { return modelName; }
            set
            {
                if (value == null || ValidName(value) && value.Length <= 64)
                {
                    modelName = value;
                }
            }
        }

        public string SkinName
        {
            get { return skinName; }
            set
            {
                if (value == null || ValidName(value) && value.Length <= 64)
                {
                    skinName = value;
                }
            }
        }

        [ReadOnly(true)]
        public int? DbId { get; set; }

        public static event PlayerChatEventHandler PlayerChatEvent;

        public static event PlayerRegisteredCheckEventHandler PlayerRegisteredCheck;

        public static event EventHandler<PlayerEventArgs> Joined;

        public static event EventHandler<PlayerEventArgs> Disconnected;

        public static event EventHandler<ChatOtherEventArgs> ChatOther;

        public static event EventHandler<MapChangeEventArgs> MapChanged;

        event FilterInput filterInput = delegate(ref string t, out bool h) { h = false; };

        public event ProperBlockChangeEH BlockChangeProper;

        public event BlockchangeEventHandler Blockchange;

        public event BlockchangeEventHandler2 Blockchange2;

        public static event BlockPlacedUnderHandler PlayerPillared;

        public event EventHandler<PositionChangedEventArgs> PositionChanged;

        public bool HacksDetection(ushort x, ushort y, ushort z, byte rotX, byte rotY)
        {
            lock (hacksDetectionSync)
            {
                if (IsRefree)
                {
                    return false;
                }
                if (level.mapType != MapType.Zombie && level.allowHacks || !InfectionSettings.All.DisallowHacksUseOnInfectionMap ||
                    (int)group.Permission >= InfectionSettings.All.HacksOnInfectionMapPermission || mapLoading || !fullylogged)
                {
                    return false;
                }
                if (oldx == -1 && oldy == -1 && oldz == -1)
                {
                    oldx = x;
                    oldy = y;
                    oldz = z;
                    return false;
                }
                hackDetected = false;
                if (Math.Sqrt(Math.Pow(x - oldx, 2.0) + Math.Pow(z - oldz, 2.0)) > InfectionSettings.All.SpeedHackDetectionThreshold ||
                    y - oldy > InfectionSettings.All.SpeedHackDetectionThreshold)
                {
                    hackDetected = true;
                }
                if (hackDetected && fullylogged && !mapLoading)
                {
                    Server.s.Log(name + " tried to respawn.", systemMsg: true);
                    SendPos(byte.MaxValue, (ushort)((oldx >> 5 << 5) + 16), (ushort)((oldy >> 5 << 5) + 16), (ushort)((oldz >> 5 << 5) + 16), (byte)oldRotX,
                            (byte)oldRotY);
                    return true;
                }
                if (InfectionSettings.All.BlockGlitchPrevention && level.mapType == MapType.Zombie &&
                    !InfectionSystem.InfectionSystem.currentInfectionMap.IsBuildingAllowed && IsInBlock(x, y, z))
                {
                    Server.s.Log(name + " tried to block-glitch.", systemMsg: true);
                    Pos3I pos3I = PlayerPosToBlockPos(oldx / 32f, oldy / 32f, oldz / 32f);
                    SendPos(byte.MaxValue, (ushort)((pos3I.X << 5) + 16), (ushort)((pos3I.Y << 5) + 16), (ushort)((pos3I.Z << 5) + 16), (byte)oldRotX, (byte)oldRotY);
                    return true;
                }
                oldx = x;
                oldy = y;
                oldz = z;
                oldRotX = rotX;
                oldRotY = rotY;
                if (IsOutsideMap)
                {
                    outsideMapTime++;
                    if (outsideMapTime > 100)
                    {
                        SendMessage("%c* You are outside of map bounds! *");
                        Server.s.Log(name + " is outside of map bounds!");
                        GlobalMessageOps(name + " is outside of the map bounds!");
                        outsideMapTime = 0;
                    }
                }
                else
                {
                    outsideMapTime = 0;
                    if (lives > 0 && IsClipping)
                    {
                        clippingTime++;
                        if (clippingTime > 400)
                        {
                            SendMessage("%c* You can't stand in blocks! *");
                            Server.s.Log(name + " is walking through blocks!");
                            GlobalMessageOps(name + " is walking through blocks!");
                            clippingTime = 0;
                        }
                    }
                    else
                    {
                        clippingTime = 0;
                    }
                }
                return false;
            }
        }

        public Pos3I PlayerPosToBlockPos(float x, float y, float z)
        {
            Pos3I result = default(Pos3I);
            result.X = (int)x;
            float num = x - (int)x;
            if (num > 0.6875f)
            {
                result.X = (int)x + 1;
            }
            else if (num < 9f / 32f)
            {
                result.X = (int)x - 1;
            }
            result.Z = (int)z;
            num = z - (int)z;
            if (num > 0.6875f)
            {
                result.Z = (int)z + 1;
            }
            else if (num < 9f / 32f)
            {
                result.Z = (int)z - 1;
            }
            result.Y = (int)y;
            num = y - (int)y;
            if (num < 3f / 32f)
            {
                result.Y--;
            }
            return result;
        }

        public bool IsInBlock(int x, int y, int z)
        {
            Pos3I pos3I = PlayerPosToBlockPos(x / 32f, y / 32f, z / 32f);
            byte tile = level.GetTile(pos3I.X, pos3I.Y, pos3I.Z);
            byte tile2 = level.GetTile(pos3I.X, pos3I.Y - 1, pos3I.Z);
            if (Block.IsDoor(tile) || Block.Walkthrough(tile) || tile == byte.MaxValue)
            {
                if (!Block.IsDoor(tile2) && !Block.Walkthrough(tile2) && tile2 != 50 && tile2 != 44)
                {
                    return tile2 != byte.MaxValue;
                }
                return false;
            }
            return true;
        }

        public void DistanceMeasure(int x, int z)
        {
            try
            {
                float num = (float)Math.Sqrt(Math.Pow(x - oldx, 2.0) + Math.Pow(z - oldz, 2.0));
                oldx = x;
                oldz = z;
                Server.s.Log(num.ToString());
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        public void JumpMeasure(int y)
        {
            try
            {
                float num = y - oldy;
                oldy = y;
                Server.s.Log(num.ToString());
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        public static byte[] Join(List<byte[]> bList)
        {
            byte[] array = new byte[8 * bList.Count];
            for (int i = 0; i < bList.Count; i++)
            {
                Buffer.BlockCopy(bList[i], 0, array, i * 8, 8);
            }
            return array;
        }

        public void AddVirtualBlock(int x, int y, int z, byte block)
        {
            AddVirtualBlock((ushort)x, (ushort)y, (ushort)z, block);
        }

        public void AddVirtualBlock(ushort x, ushort y, ushort z, byte block)
        {
            byte[] array = new byte[8]
            {
                6, 0, 0, 0, 0, 0, 0, 0
            };
            HTNO(x).CopyTo(array, 1);
            HTNO(y).CopyTo(array, 3);
            HTNO(z).CopyTo(array, 5);
            array[7] = Block.Convert(block);
            AddVirtualBlock(array);
        }

        public void AddVirtualBlock(byte[] block)
        {
            if (block.Length == 8)
            {
                virtualBlocks.Add(block);
            }
        }

        public void CommitVirtual()
        {
            SendRaw(Join(virtualBlocks));
            virtualBlocks.Clear();
        }

        public void ClearBlockchange()
        {
            Blockchange = null;
        }

        public void ClearBlockchange2()
        {
            Blockchange2 = null;
        }

        public bool HasBlockchange()
        {
            return Blockchange == null;
        }

        public bool HasBlockchange2()
        {
            return Blockchange2 == null;
        }

        public bool InitVariable<T>(string key, T value)
        {
            if (!extraData.ContainsKey(key))
            {
                ExtraData[key] = value;
                return true;
            }
            return false;
        }

        public T GetVariable<T>(string key)
        {
            return (T)ExtraData[key];
        }

        public void SetVariable<T>(string key, T value)
        {
            ExtraData[key] = value;
            extraData.Select(o => new
            {
                Name = o.Key
            });
        }

        public bool RemoveVariable(string key)
        {
            return ExtraData.Remove(key);
        }

        internal void OnPlayerPillared(Player p, int x, int y, int z, ref bool stopChange)
        {
            if (PlayerPillared != null)
            {
                PlayerPillared(p, x, y, z, ref stopChange);
            }
        }

        public static void OnChatOther(ChatOtherEventArgs e)
        {
            if (ChatOther != null)
            {
                ChatOther(null, e);
            }
        }

        static void Player_BlockPlacedUnder(Player p, int x, int y, int z, ref bool stopChange)
        {
            if (!p.level.IsPillaringAllowed && p.updatePosition)
            {
                lock (p.fallSynchronize)
                {
                    if (p.updatePosition)
                    {
                        p.canBuild = false;
                        p.updatePosition = false;
                        p.originalFallPos = new BlockPos(x, y, z);
                    }
                    stopChange = true;
                    SendMessage(p, "Pillaring is not allowed on this map!");
                    return;
                }
            }
            int num = y;
            int num2 = 0;
            while (p.level.IsSurroundedByAir(x, num, z))
            {
                num--;
                num2++;
            }
            if (GeneralSettings.All.PillarMaxHeight > 0 && GeneralSettings.All.PillarMaxHeight < num2 && p.group.Permission < LevelPermission.Operator)
            {
                stopChange = true;
                SendMessage(p, rm.GetString("WarningTooHighPillar"));
            }
        }

        void CheckIfFallsBack(BlockPos blockPos)
        {
            lock (fallSynchronize)
            {
                if (!shouldFall)
                {
                    shouldFall = true;
                    originalFallPos = blockPos;
                }
            }
        }

        public bool EqualsPlayerPosition(int x, int y, int z)
        {
            int num = -1;
            int num2 = -1;
            float num3 = XFloat - (int)XFloat;
            if (num3 > 0.6875f)
            {
                num = (int)XFloat + 1;
            }
            else if (num3 < 9f / 32f)
            {
                num = (int)XFloat - 1;
            }
            num3 = ZFloat - (int)ZFloat;
            if (num3 > 0.6875f)
            {
                num2 = (int)ZFloat + 1;
            }
            else if (num3 < 9f / 32f)
            {
                num2 = (int)ZFloat - 1;
            }
            if (num == -1 && num2 == -1 && (int)XFloat == x && (int)YFloat == y && (int)ZFloat == z)
            {
                return true;
            }
            if (num != -1 && num2 == -1)
            {
                if ((int)XFloat == x && (int)YFloat == y && (int)ZFloat == z)
                {
                    return true;
                }
                if (num == x && (int)YFloat == y && (int)ZFloat == z)
                {
                    return true;
                }
            }
            else if (num == -1 && num2 != -1)
            {
                if ((int)XFloat == x && (int)YFloat == y && (int)ZFloat == z)
                {
                    return true;
                }
                if ((int)XFloat == x && (int)YFloat == y && num2 == z)
                {
                    return true;
                }
            }
            else if (num == -1 && num2 == -1)
            {
                if ((int)XFloat == x && (int)YFloat == y && (int)ZFloat == z)
                {
                    return true;
                }
            }
            else
            {
                if ((int)XFloat == x && (int)YFloat == y && (int)ZFloat == z)
                {
                    return true;
                }
                if (num == x && (int)YFloat == y && (int)ZFloat == z)
                {
                    return true;
                }
                if ((int)XFloat == x && (int)YFloat == y && num2 == z)
                {
                    return true;
                }
                if (num == x && (int)YFloat == y && num2 == z)
                {
                    return true;
                }
            }
            return false;
        }

        public BlockPos[] GetBlocksUnder()
        {
            if (level != null)
            {
                int num = -1;
                int num2 = -1;
                int num3 = (int)(YFloat - 2.2f);
                float num4 = XFloat - (int)XFloat;
                if (num4 > 0.6875f)
                {
                    num = (int)XFloat + 1;
                }
                else if (num4 < 9f / 32f)
                {
                    num = (int)XFloat - 1;
                }
                num4 = ZFloat - (int)ZFloat;
                if (num4 > 0.6875f)
                {
                    num2 = (int)ZFloat + 1;
                }
                else if (num4 < 9f / 32f)
                {
                    num2 = (int)ZFloat - 1;
                }
                if (num != -1 && num2 == -1)
                {
                    var list = new List<BlockPos>();
                    if (!Block.IsAir(level.GetTile((int)XFloat, num3, (int)ZFloat)))
                    {
                        list.Add(new BlockPos((ushort)XFloat, (ushort)num3, (ushort)ZFloat));
                    }
                    if (!Block.IsAir(level.GetTile(num, num3, (int)ZFloat)))
                    {
                        list.Add(new BlockPos((ushort)num, (ushort)num3, (ushort)ZFloat));
                    }
                    return list.ToArray();
                }
                if (num == -1 && num2 != -1)
                {
                    var list2 = new List<BlockPos>();
                    if (!Block.IsAir(level.GetTile((int)XFloat, num3, (int)ZFloat)))
                    {
                        list2.Add(new BlockPos((ushort)XFloat, (ushort)num3, (ushort)ZFloat));
                    }
                    if (!Block.IsAir(level.GetTile((int)XFloat, num3, num2)))
                    {
                        list2.Add(new BlockPos((ushort)XFloat, (ushort)num3, (ushort)num2));
                    }
                    return list2.ToArray();
                }
                if (num == -1 && num2 == -1)
                {
                    var list3 = new List<BlockPos>();
                    if (!Block.IsAir(level.GetTile((ushort)XFloat, (ushort)num3, (ushort)ZFloat)))
                    {
                        list3.Add(new BlockPos((ushort)XFloat, (ushort)num3, (ushort)ZFloat));
                    }
                    return list3.ToArray();
                }
                var list4 = new List<BlockPos>();
                if (!Block.IsAir(level.GetTile((int)XFloat, num3, (int)ZFloat)))
                {
                    list4.Add(new BlockPos((ushort)XFloat, (ushort)num3, (ushort)ZFloat));
                }
                if (!Block.IsAir(level.GetTile(num, num3, (int)ZFloat)))
                {
                    list4.Add(new BlockPos((ushort)num, (ushort)num3, (ushort)ZFloat));
                }
                if (!Block.IsAir(level.GetTile((int)XFloat, num3, num2)))
                {
                    list4.Add(new BlockPos((int)XFloat, num3, num2));
                }
                if (!Block.IsAir(level.GetTile(num, num3, num2)))
                {
                    list4.Add(new BlockPos(num, num3, num2));
                }
                return list4.ToArray();
            }
            return new BlockPos[0];
        }

        internal static void OnPlayerJoined(object sender, PlayerEventArgs e)
        {
            EventHandler<PlayerEventArgs> joined = Joined;
            if (joined != null)
            {
                Joined(sender, e);
            }
        }

        internal static void OnPlayerDisconnected(object sender, PlayerEventArgs e)
        {
            EventHandler<PlayerEventArgs> eventHandler = Disconnected;
            if (eventHandler != null)
            {
                Joined(sender, e);
            }
        }

        public static void OnPlayerChatEvent(Player p, ref string message, ref bool stopIt)
        {
            if (PlayerChatEvent != null)
            {
                PlayerChatEvent(p, ref message, ref stopIt);
            }
        }

        internal static void OnPlayerRegisteredCheck(Player p, ref bool isRegistered)
        {
            if (PlayerRegisteredCheck != null)
            {
                PlayerRegisteredCheck(p, ref isRegistered);
            }
        }

        static void players_PlayerRemoved(object sender, PlayerEventArgs e)
        {
            RemoteClient.remoteClients.ForEach(delegate(RemoteClient rc) { rc.RemovePlayer(e.Player); });
        }

        static void players_PlayerAdded(object sender, PlayerEventArgs e)
        {
            RemoteClient.remoteClients.ForEach(delegate(RemoteClient rc) { rc.AddPlayer(e.Player); });
        }

        void Player_BlockChangeProper(Player p, ushort x, ushort y, ushort z, byte type, byte action, ref bool stopChange)
        {
            bool sc = false;
            if (p.level.mapType == MapType.Zombie && InfectionSettings.All.DisallowSpleefing &&
                (p.group.Permission < LevelPermission.Operator || !InfectionSettings.All.OpsBypassSpleefPrevention))
            {
                players.ForEach(delegate(Player pl)
                {
                    if (!sc && pl != p && pl.level.mapType == MapType.Zombie && p.isZombie == pl.isZombie)
                    {
                        BlockPos[] blocksUnder = pl.GetBlocksUnder();
                        for (int i = 0; i < blocksUnder.Length; i++)
                        {
                            BlockPos blockPos = blocksUnder[i];
                            if (blockPos.x == x && blockPos.y == y && blockPos.z == z)
                            {
                                sc = true;
                                break;
                            }
                        }
                    }
                });
                if (sc)
                {
                    SendMessage(p, "Don't spleef your team members.");
                }
            }
            if (p.level.mapType == MapType.Lava && LavaSettings.All.DisallowSpleefing &&
                (p.group.Permission < LevelPermission.Operator || !LavaSettings.All.OpsBypassSpleefPrevention))
            {
                players.ForEach(delegate(Player pl)
                {
                    if (!sc && pl != p && pl.level.mapType == MapType.Lava)
                    {
                        BlockPos[] blocksUnder2 = pl.GetBlocksUnder();
                        for (int j = 0; j < blocksUnder2.Length; j++)
                        {
                            BlockPos blockPos2 = blocksUnder2[j];
                            if (blockPos2.x == x && blockPos2.y == y && blockPos2.z == z)
                            {
                                sc = true;
                                break;
                            }
                        }
                    }
                });
                if (sc)
                {
                    SendMessage(p, "Don't spleef other players.");
                }
            }
            stopChange = sc;
        }

        public void Save()
        {
            if (databaseLoadFailure)
            {
                return;
            }
            flags = flagsCollection.FlagContainer;
            try
            {
                string queryString = "UPDATE Players SET IP='" + ip + "', LastLogin='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', totalLogin=" + totalLogins +
                    ", totalDeaths=" + overallDeath + ", Money=" + money + ", totalBlocks=" + overallBlocks + " + " + loginBlocks + ", totalKicked=" + totalKicked +
                    ", totalScore=" + totalScore + ", bestScore=" + bestScore + ", timesWon=" + timesWon + ", totalMinutesPlayed=" + TotalMinutesPlayed + ", flags=" +
                    flags + ", playerExperienceOnZombie=" + PlayerExperienceOnZombie + ", " + DBManager.KeyType_wonAsHumanTimes[0] + "=" + WonAsHumanTimes + ", " +
                    DBManager.KeyType_wonAsZombieTimes[0] + "=" + WonAsZombieTimes + ", " + DBManager.KeyType_zombifiedCount[0] + "=" + ZombifiedCount + ", " +
                    DBPlayerColumns.RoundsOnZombie.Name + "=" + RoundsOnZombie + " WHERE Name='" + name + "'";
                DBInterface.ExecuteQuery(queryString);
                if (smileySaved)
                {
                    return;
                }
                if (parseSmiley)
                {
                    emoteList.RemoveAll(s => s == name);
                }
                else
                {
                    emoteList.Add(name);
                }
                File.WriteAllLines("text/emotelist.txt", emoteList.ToArray());
                smileySaved = true;
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        static void Receive(IAsyncResult result)
        {
            Player player = (Player)result.AsyncState;
            if (player.disconnected || player.socket == null)
            {
                return;
            }
            try
            {
                int num = player.socket.EndReceive(result);
                if (num == 0)
                {
                    player.Disconnect();
                    return;
                }
                byte[] dst = new byte[player.buffer.Length + num];
                Buffer.BlockCopy(player.buffer, 0, dst, 0, player.buffer.Length);
                Buffer.BlockCopy(player.tempbuffer, 0, dst, player.buffer.Length, num);
                player.buffer = player.HandleMessage(dst);
                player.socket.BeginReceive(player.tempbuffer, 0, player.tempbuffer.Length, SocketFlags.None, Receive, player);
            }
            catch (SocketException ex)
            {
                if (ex.SocketErrorCode == SocketError.NoBufferSpaceAvailable && LavaSettings.All.AutoServerLock)
                {
                    Server.serverLockTime = DateTime.Now.AddSeconds(45.0);
                    Server.s.Log("Server Lock on Critical Connection Error. (45sec.)");
                }
                player.Disconnect();
            }
            catch (NullReferenceException) {}
            catch (Exception ex3)
            {
                Server.ErrorLog(ex3);
                player.Kick("Error!");
            }
        }

        byte[] HandleMessage(byte[] buffer)
        {
            try
            {
                int num = 0;
                byte b = buffer[0];
                switch (b)
                {
                    case 0:
                        num = 130;
                        break;
                    case 5:
                        if (loggedIn)
                        {
                            num = 8;
                            break;
                        }
                        goto default;
                    case 8:
                        if (loggedIn)
                        {
                            num = 9;
                            break;
                        }
                        goto default;
                    case 13:
                        if (loggedIn)
                        {
                            num = 65;
                            break;
                        }
                        goto default;
                    case 16:
                        num = 66;
                        break;
                    case 17:
                        num = 68;
                        break;
                    case 19:
                        num = 1;
                        break;
                    default:
                        Kick(string.Format("Unhandled message id \"{0}\"!", b));
                        return new byte[0];
                }
                if (buffer.Length > num)
                {
                    byte[] array = new byte[num];
                    Buffer.BlockCopy(buffer, 1, array, 0, num);
                    byte[] array2 = new byte[buffer.Length - num - 1];
                    Buffer.BlockCopy(buffer, num + 1, array2, 0, buffer.Length - num - 1);
                    buffer = array2;
                    switch (b)
                    {
                        case 0:
                            HandleLogin(array);
                            break;
                        case 5:
                            if (loggedIn)
                            {
                                HandleBlockchange(array);
                            }
                            break;
                        case 8:
                            if (loggedIn)
                            {
                                HandlePositionInfo(array);
                            }
                            break;
                        case 13:
                            if (loggedIn)
                            {
                                HandleChat(array);
                            }
                            break;
                        case 16:
                            HandleExtInfo(array);
                            break;
                        case 17:
                            HandleExtEntry(array);
                            break;
                    }
                    if (buffer.Length <= 0)
                    {
                        return new byte[0];
                    }
                    buffer = HandleMessage(buffer);
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
            return buffer;
        }

        void HandleExtEntry(byte[] message)
        {
            byte[] bytes = message.Take(64).ToArray();
            string text = Encoding.ASCII.GetString(bytes).Trim();
            byte[] value = message.Skip(64).Take(4).ToArray();
            int num = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(value, 0));
            Dictionary<string, int> dictionary = null;
            if (!ExtraData.ContainsKey("supported_extensions"))
            {
                dictionary = new Dictionary<string, int>();
                ExtraData["supported_extensions"] = dictionary;
            }
            else
            {
                dictionary = (Dictionary<string, int>)ExtraData["supported_extensions"];
            }
            dictionary[text] = num;
            switch (text)
            {
                case "ClickDistance":
                    Cpe.ClickDistance = num;
                    break;
                case "HeldBlock":
                    Cpe.HeldBlock = num;
                    break;
                case "TextHotKey":
                    Cpe.TextHotKey = num;
                    break;
                case "ExtPlayerList":
                    Cpe.ExtPlayerList = num;
                    break;
                case "EnvColors":
                    Cpe.EnvColors = num;
                    break;
                case "SelectionCuboid":
                    Cpe.SelectionCuboid = num;
                    break;
                case "BlockPermissions":
                    Cpe.BlockPermissions = num;
                    break;
                case "ChangeModel":
                    Cpe.BlockPermissions = num;
                    break;
                case "EnvMapAppearance":
                    Cpe.EnvMapAppearance = num;
                    break;
                case "EnvWeatherType":
                    Cpe.EnvWeatherType = num;
                    break;
                case "MessageTypes":
                    Cpe.MessageTypes = num;
                    break;
                case "Blockmension":
                    Cpe.Blockmension = num;
                    break;
            }
            if (text == "Blockmension")
            {
                V1.JsonData(this, "[{\"experimental\":{\"flags\":\"portal-blocks-enable\"}}]");
            }
            if (text == "TextHotKey" && Cpe.TextHotKey == 1)
            {
                foreach (AvailablePlugin availablePlugin in Server.Plugins.AvailablePlugins)
                {
                    if (availablePlugin.Instance.GetType() != typeof(PluginKeyboardShortcuts))
                    {
                        continue;
                    }
                    PluginKeyboardShortcuts pluginKeyboardShortcuts = (PluginKeyboardShortcuts)availablePlugin.Instance;
                    foreach (ShortcutInfo shortcut in pluginKeyboardShortcuts.GetShortcuts())
                    {
                        if (shortcut.Command.Length <= 64 && ServerProperties.ValidString(shortcut.Command, "%![]:.,{}~-+()?_/\\^*#@$~`\"'|=;<>& "))
                        {
                            CpeHotKeyInfo cpeHotKeyInfo = WpfToLwjglKeyMap.ToCpeHotKey(shortcut.Shortcut);
                            V1.SetTextHotKey(this, "Shortcut", shortcut.Command, cpeHotKeyInfo.KeyCode, cpeHotKeyInfo.KeyMod);
                        }
                    }
                }
            }
            if (Interlocked.Decrement(ref extToRead) == 0)
            {
                HandleLoginPart2();
            }
        }

        void HandleExtInfo(byte[] message)
        {
            byte[] bytes = message.Take(64).ToArray();
            string text = Encoding.ASCII.GetString(bytes).Trim();
            if (Debug_NotifyConsolAboutPlayersClient)
            {
                Server.s.Log("Player " + name + " uses " + text + " client.");
            }
            ExtraData["app_name"] = text;
            byte[] value = message.Skip(64).Take(2).ToArray();
            short num = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(value, 0));
            ExtraData["extensions_count"] = num;
            extToRead = num;
        }

        void HandleBlockSupport(byte[] message) {}

        void HandleLogin(byte[] message)
        {
            try
            {
                if (loggedIn)
                {
                    return;
                }
                byte b = message[0];
                name = enc.GetString(message, 1, 64).Trim();
                PublicName = RemoveEmailDomain(name);
                string verificationHash = enc.GetString(message, 65, 32).Trim();
                byte b2 = message[129];
                if (b2 == 66)
                {
                    IsCpeSupported = true;
                }
                if (b2 == 44)
                {
                    IsUsingXWom = true;
                }
                if (Server.useWhitelist)
                {
                    if (Server.verify)
                    {
                        if (Server.whiteList.Contains(name))
                        {
                            onWhitelist = true;
                        }
                    }
                    else
                    {
                        try
                        {
                            var dictionary = new Dictionary<string, object>();
                            dictionary.Add("@IP", ip);
                            using (DataTable dataTable = DBInterface.fillData("SELECT Name FROM Players WHERE IP = @IP", dictionary))
                            {
                                if (dataTable.Rows.Count > 0 && dataTable.Rows.Contains(name) && Server.whiteList.Contains(name))
                                {
                                    onWhitelist = true;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Server.ErrorLog(ex);
                        }
                    }
                }
                if (Server.bannedIP.Contains(ip))
                {
                    disconnectionReason = DisconnectionReason.IPBan;
                    if (!Server.useWhitelist)
                    {
                        Kick(Server.customBanMessage);
                        return;
                    }
                    if (!onWhitelist)
                    {
                        Kick(Server.customBanMessage);
                        return;
                    }
                }
                if (players.Count >= Server.players)
                {
                    if (Server.vipSystem == 1 && ip != "127.0.0.1" && !VipList.IsOnList(name))
                    {
                        disconnectionReason = DisconnectionReason.ServerFull;
                        Kick(rm.GetString("KickServerFull"));
                        return;
                    }
                    if (Server.vipSystem == 0 && ip != "127.0.0.1")
                    {
                        disconnectionReason = DisconnectionReason.ServerFull;
                        Kick(rm.GetString("KickServerFull"));
                        return;
                    }
                }
                if (b != 7)
                {
                    Kick(rm.GetString("KickWrongVersion"));
                    return;
                }
                if (name.Length > 63 || !ValidName(name))
                {
                    disconnectionReason = DisconnectionReason.IllegalName;
                    Kick(rm.GetString("KickIllegalName"));
                    return;
                }
                if (Server.verify)
                {
                    Authentication = VerifyPlayer(verificationHash);
                    if (Authentication == AuthenticationProvider.Unknown)
                    {
                        if (GeneralSettings.All.VerifyNameForLocalIPs)
                        {
                            disconnectionReason = DisconnectionReason.AuthenticationFailure;
                            Kick(rm.GetString("KickLoginFailed"));
                            return;
                        }
                        if (!IsIPLocal())
                        {
                            disconnectionReason = DisconnectionReason.AuthenticationFailure;
                            Kick(rm.GetString("KickLoginFailed"));
                            return;
                        }
                    }
                    if (Authentication == AuthenticationProvider.ClassiCube)
                    {
                        if (!GeneralSettings.All.AllowAndListOnClassiCube)
                        {
                            disconnectionReason = DisconnectionReason.AuthenticationFailure;
                            Kick("You can't connect to this server through ClassiCube.");
                            return;
                        }
                        string text = name;
                        name += "+";
                        if (GeneralSettings.All.PlusMarkerForClassiCubeAccount)
                        {
                            PublicName = name;
                        }
                        else
                        {
                            PublicName = text;
                        }
                    }
                }
                try
                {
                    Server.TempBan item = Server.tempBans.Find(tB => tB.name.ToLower() == name.ToLower());
                    if (item.allowedJoin < DateTime.Now)
                    {
                        Server.tempBans.Remove(item);
                    }
                    else
                    {
                        disconnectionReason = DisconnectionReason.TempBan;
                        Kick(rm.GetString("KickTempBan"));
                    }
                }
                catch {}
                if (Group.findPlayerGroup(name) == Group.findPerm(LevelPermission.Banned))
                {
                    disconnectionReason = DisconnectionReason.NameBan;
                    if (!Server.useWhitelist)
                    {
                        Kick(Server.customBanMessage);
                        return;
                    }
                    if (!onWhitelist)
                    {
                        Kick(Server.customBanMessage);
                        return;
                    }
                }
                try
                {
                    players.GetCopy().ForEach(delegate(Player p)
                    {
                        if (p.name == name)
                        {
                            if (Server.verify)
                            {
                                disconnectionReason = DisconnectionReason.AutoKicked;
                                p.Kick(rm.GetString("KickLoggedAsYou"));
                            }
                            else
                            {
                                Kick(rm.GetString("KickAlreadyLogged"));
                            }
                        }
                    });
                }
                catch {}
                try
                {
                    lock (playersThatLeftLocker)
                    {
                        playersThatLeft.Remove(name.ToLower());
                    }
                }
                catch {}
                group = Group.findPlayerGroup(name);
                if (IsCpeSupported)
                {
                    SendRaw(new Packets().MakeExtInfo("MCDzienny", 3));
                    SendRaw(new Packets().MakeExtEntry("CustomBlocks", 1));
                    SendRaw(new Packets().MakeExtEntry("MessageTypes", 1));
                    SendRaw(new Packets().MakeExtEntry("HeldBlock", 1));
                    SendRaw(new byte[2]
                    {
                        19, 1
                    });
                }
                else
                {
                    HandleLoginPart2();
                }
            }
            catch (Exception ex2)
            {
                Server.ErrorLog(ex2);
            }
        }

        void HandleLoginPart2()
        {
            try
            {
                SendMotd();
                SendMap();
                id = GetFreeId();
                if (disconnected)
                {
                    return;
                }
                loggedIn = true;
                IRCSay(string.Format(rm.GetString("JoinGlobalMessage"), PublicName));
                string text = rm.GetString("LatelyKnownAs");
                bool flag = false;
                if (ip != "127.0.0.1")
                {
                    lock (playersThatLeftLocker)
                    {
                        foreach (KeyValuePair<string, string> item in playersThatLeft)
                        {
                            if (item.Value == ip)
                            {
                                flag = true;
                                text = text + " " + item.Key;
                            }
                        }
                    }
                    if (flag)
                    {
                        GlobalMessageOps(text);
                        Server.s.Log(text);
                        IRCSay(text, opchat: true);
                    }
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
            if (!Server.useMySQL)
            {
                try
                {
                    using (DataTable dataTable = DBInterface.fillData("SELECT * FROM Players WHERE Name='" + name + "'"))
                    {
                        if (dataTable.Rows.Count == 0)
                        {
                            prefix = "";
                            titlecolor = "";
                            color = group.color;
                            money = 30;
                            firstLogin = DateTime.Now;
                            totalLogins = 1;
                            totalKicked = 0;
                            overallDeath = 0;
                            overallBlocks = 0L;
                            totalScore = 0;
                            bestScore = 0;
                            timeLogged = DateTime.Now;
                            SendMessage(string.Format(rm.GetString("WelcomeFirstVisit"), PublicName));
                            using (DataTable dataTable2 = DBInterface.fillData(
                                       "INSERT INTO Players (Name, IP, FirstLogin, LastLogin, totalLogin, Title, totalDeaths, Money, totalBlocks, totalKicked, totalScore, bestScore, timesWon, welcomeMessage, farewellMessage, totalMinutesPlayed, flags, playerExperienceOnZombie, " +
                                       DBManager.KeyType_wonAsHumanTimes[0] + ", " + DBManager.KeyType_wonAsZombieTimes[0] + ", " + DBManager.KeyType_zombifiedCount[0] +
                                       ")VALUES ('" + name + "', '" + ip + "', '" + firstLogin.ToString("yyyy-MM-dd HH:mm:ss") + "', '" +
                                       DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " + totalLogins + ", '" + prefix + "', " + overallDeath + ", " + money + ", " +
                                       loginBlocks + ", " + totalKicked + ", " + totalScore + ", " + bestScore + ", " + timesWon + ", '" + welcomeMessage + "', '" +
                                       farewellMessage + "', " + TotalMinutesPlayed + ", " + flags + ", " + PlayerExperienceOnZombie + ", " + WonAsHumanTimes + ", " +
                                       WonAsZombieTimes + ", " + ZombifiedCount + ");SELECT LAST_INSERT_ROWID() AS Id;"))
                            {
                                DbId = int.Parse(dataTable2.Rows[0]["Id"].ToString());
                            }
                        }
                        else
                        {
                            DbId = int.Parse(dataTable.Rows[0]["ID"].ToString());
                            timeLogged = DateTime.Now;
                            totalLogins = int.Parse(dataTable.Rows[0]["totalLogin"].ToString()) + 1;
                            firstLogin = DateTime.Parse(dataTable.Rows[0]["firstLogin"].ToString());
                            totalScore = int.Parse(dataTable.Rows[0]["totalScore"].ToString());
                            bestScore = int.Parse(dataTable.Rows[0]["bestScore"].ToString());
                            timesWon = int.Parse(dataTable.Rows[0]["timesWon"].ToString());
                            welcomeMessage = dataTable.Rows[0]["welcomeMessage"].ToString();
                            farewellMessage = dataTable.Rows[0]["farewellMessage"].ToString();
                            int.TryParse(dataTable.Rows[0]["totalMinutesPlayed"].ToString(), out totalMinutesPlayed);
                            int.TryParse(dataTable.Rows[0]["flags"].ToString(), out flags);
                            int.TryParse(dataTable.Rows[0]["playerExperienceOnZombie"].ToString(), out playerExperienceOnZombie);
                            int.TryParse(dataTable.Rows[0][DBManager.KeyType_wonAsHumanTimes[0]].ToString(), out wonAsHumanTimes);
                            int.TryParse(dataTable.Rows[0][DBManager.KeyType_wonAsZombieTimes[0]].ToString(), out wonAsZombieTimes);
                            int.TryParse(dataTable.Rows[0][DBManager.KeyType_zombifiedCount[0]].ToString(), out zombifiedCount);
                            int.TryParse(dataTable.Rows[0][DBPlayerColumns.RoundsOnZombie.Name].ToString(), out roundsOnZombie);
                            if (dataTable.Rows[0]["Title"].ToString().Trim() != "")
                            {
                                string text2 = dataTable.Rows[0]["Title"].ToString().Trim().Replace("[", "");
                                title = text2.Replace("]", "");
                            }
                            if (dataTable.Rows[0]["title_color"].ToString().Trim() != "")
                            {
                                titlecolor = c.Parse(dataTable.Rows[0]["title_color"].ToString().Trim());
                            }
                            else
                            {
                                titlecolor = "";
                            }
                            if (dataTable.Rows[0]["color"].ToString().Trim() != "")
                            {
                                color = c.Parse(dataTable.Rows[0]["color"].ToString().Trim());
                            }
                            else
                            {
                                color = group.color;
                            }
                            SetPrefix();
                            overallDeath = int.Parse(dataTable.Rows[0]["TotalDeaths"].ToString());
                            overallBlocks = int.Parse(dataTable.Rows[0]["totalBlocks"].ToString().Trim());
                            money = int.Parse(dataTable.Rows[0]["Money"].ToString());
                            totalKicked = int.Parse(dataTable.Rows[0]["totalKicked"].ToString());
                            TierSystem.TierSet(this);
                            TierSystem.ColorSet(this);
                            TierSystem.GiveItems(this);
                            SendMessage2(0, string.Format(rm.GetString("WelcomeAnotherVisit"), color + prefix + color + PublicName + Server.DefaultColor, totalLogins));
                        }
                    }
                }
                catch (Exception ex2)
                {
                    Server.ErrorLog(ex2);
                    databaseLoadFailure = true;
                }
                try
                {
                    using (DataTable dataTable3 = DBInterface.fillData("SELECT * FROM Stars WHERE Name='" + name + "'"))
                    {
                        if (dataTable3.Rows.Count == 0)
                        {
                            DBInterface.ExecuteQuery("INSERT INTO Stars (Name, GoldStars, SilverStars, BronzeStars, RottenStars) VALUES ('" + name + "', 0, 0, 0, 0);");
                            ExtraData["gold_stars_count"] = 0;
                            ExtraData["silver_stars_count"] = 0;
                            ExtraData["bronze_stars_count"] = 0;
                            ExtraData["rotten_stars_count"] = 0;
                        }
                        else
                        {
                            ExtraData["gold_stars_count"] = int.Parse(dataTable3.Rows[0]["GoldStars"].ToString());
                            ExtraData["silver_stars_count"] = int.Parse(dataTable3.Rows[0]["SilverStars"].ToString());
                            ExtraData["bronze_stars_count"] = int.Parse(dataTable3.Rows[0]["BronzeStars"].ToString());
                            ExtraData["rotten_stars_count"] = int.Parse(dataTable3.Rows[0]["RottenStars"].ToString());
                        }
                    }
                }
                catch (Exception ex3)
                {
                    Server.s.Log("Error while loading stars count from the database. This error may cause a loss of player's star stats.");
                    Server.ErrorLog(ex3);
                }
            }
            else
            {
                try
                {
                    using (DataTable dataTable4 = DBInterface.fillData("SELECT * FROM Players WHERE Name='" + name + "'"))
                    {
                        if (dataTable4.Rows.Count == 0)
                        {
                            prefix = "";
                            titlecolor = "";
                            color = group.color;
                            money = 30;
                            firstLogin = DateTime.Now;
                            totalLogins = 1;
                            totalKicked = 0;
                            overallDeath = 0;
                            overallBlocks = 0L;
                            totalScore = 0;
                            bestScore = 0;
                            timeLogged = DateTime.Now;
                            SendMessage(string.Format(rm.GetString("WelcomeFirstVisit"), PublicName));
                            using (DataTable dataTable5 = DBInterface.fillData(
                                       "INSERT INTO Players (Name, IP, FirstLogin, LastLogin, totalLogin, Title, totalDeaths, Money, totalBlocks, totalKicked, totalScore, bestScore, timesWon, welcomeMessage, farewellMessage, totalMinutesPlayed, flags)VALUES ('" +
                                       name + "', '" + ip + "', '" + firstLogin.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +
                                       "', " +
                                       totalLogins + ", '" + prefix + "', " + overallDeath + ", " + money + ", " + loginBlocks + ", " + totalKicked + ", " + totalScore +
                                       ", " +
                                       bestScore + ", " + timesWon + ", '" + welcomeMessage + "', '" + farewellMessage + "', " + totalMinutesPlayed + ", " + flags +
                                       ");SELECT LAST_INSERT_ID() AS Id;"))
                            {
                                DbId = int.Parse(dataTable5.Rows[0]["Id"].ToString());
                            }
                        }
                        else
                        {
                            DbId = int.Parse(dataTable4.Rows[0]["ID"].ToString());
                            timeLogged = DateTime.Now;
                            totalLogins = int.Parse(dataTable4.Rows[0]["totalLogin"].ToString()) + 1;
                            userID = int.Parse(dataTable4.Rows[0]["ID"].ToString());
                            firstLogin = DateTime.Parse(dataTable4.Rows[0]["firstLogin"].ToString());
                            totalScore = int.Parse(dataTable4.Rows[0]["totalScore"].ToString());
                            bestScore = int.Parse(dataTable4.Rows[0]["bestScore"].ToString());
                            timesWon = int.Parse(dataTable4.Rows[0]["timesWon"].ToString());
                            welcomeMessage = dataTable4.Rows[0]["welcomeMessage"].ToString();
                            farewellMessage = dataTable4.Rows[0]["farewellMessage"].ToString();
                            int.TryParse(dataTable4.Rows[0]["totalMinutesPlayed"].ToString(), out totalMinutesPlayed);
                            int.TryParse(dataTable4.Rows[0]["flags"].ToString(), out flags);
                            int.TryParse(dataTable4.Rows[0]["playerExperienceOnZombie"].ToString(), out playerExperienceOnZombie);
                            int.TryParse(dataTable4.Rows[0][DBManager.KeyType_wonAsHumanTimes[0]].ToString(), out wonAsHumanTimes);
                            int.TryParse(dataTable4.Rows[0][DBManager.KeyType_wonAsZombieTimes[0]].ToString(), out wonAsZombieTimes);
                            int.TryParse(dataTable4.Rows[0][DBManager.KeyType_zombifiedCount[0]].ToString(), out zombifiedCount);
                            int.TryParse(dataTable4.Rows[0][DBPlayerColumns.RoundsOnZombie.Name].ToString(), out roundsOnZombie);
                            if (dataTable4.Rows[0]["Title"].ToString().Trim() != "")
                            {
                                string text3 = dataTable4.Rows[0]["Title"].ToString().Trim().Replace("[", "");
                                title = text3.Replace("]", "");
                            }
                            if (dataTable4.Rows[0]["title_color"].ToString().Trim() != "")
                            {
                                titlecolor = c.Parse(dataTable4.Rows[0]["title_color"].ToString().Trim());
                            }
                            else
                            {
                                titlecolor = "";
                            }
                            if (dataTable4.Rows[0]["color"].ToString().Trim() != "")
                            {
                                color = c.Parse(dataTable4.Rows[0]["color"].ToString().Trim());
                            }
                            else
                            {
                                color = group.color;
                            }
                            SetPrefix();
                            overallDeath = int.Parse(dataTable4.Rows[0]["TotalDeaths"].ToString());
                            overallBlocks = int.Parse(dataTable4.Rows[0]["totalBlocks"].ToString().Trim());
                            money = int.Parse(dataTable4.Rows[0]["Money"].ToString());
                            totalKicked = int.Parse(dataTable4.Rows[0]["totalKicked"].ToString());
                            TierSystem.TierSet(this);
                            TierSystem.ColorSet(this);
                            TierSystem.GiveItems(this);
                            SendMessage(string.Format(rm.GetString("WelcomeAnotherVisit"), color + prefix + PublicName + Server.DefaultColor, totalLogins));
                        }
                    }
                }
                catch (Exception ex4)
                {
                    Server.ErrorLog(ex4);
                    databaseLoadFailure = true;
                }
                try
                {
                    using (DataTable dataTable6 = DBInterface.fillData("SELECT * FROM Stars WHERE Name='" + name + "'"))
                    {
                        if (dataTable6.Rows.Count == 0)
                        {
                            DBInterface.ExecuteQuery("INSERT INTO Stars (Name, GoldStars, SilverStars, BronzeStars, RottenStars) VALUES ('" + name + "', 0, 0, 0, 0);");
                            ExtraData["gold_stars_count"] = 0;
                            ExtraData["silver_stars_count"] = 0;
                            ExtraData["bronze_stars_count"] = 0;
                            ExtraData["rotten_stars_count"] = 0;
                        }
                        else
                        {
                            ExtraData["gold_stars_count"] = int.Parse(dataTable6.Rows[0]["GoldStars"].ToString());
                            ExtraData["silver_stars_count"] = int.Parse(dataTable6.Rows[0]["SilverStars"].ToString());
                            ExtraData["bronze_stars_count"] = int.Parse(dataTable6.Rows[0]["BronzeStars"].ToString());
                            ExtraData["rotten_stars_count"] = int.Parse(dataTable6.Rows[0]["RottenStars"].ToString());
                        }
                    }
                }
                catch (Exception ex5)
                {
                    Server.s.Log("Error while loading stars count from the database. This error may cause a loss of player's star stats.");
                    Server.ErrorLog(ex5);
                }
            }
            try
            {
                LoadPlayerAppearance();
            }
            catch (Exception ex6)
            {
                Server.ErrorLog(ex6);
            }
            try
            {
                ushort num = (ushort)((0.5 + level.spawnx) * 32.0);
                ushort num2 = (ushort)((1 + level.spawny) * 32);
                ushort num3 = (ushort)((0.5 + level.spawnz) * 32.0);
                pos = new ushort[3]
                {
                    num, num2, num3
                };
                rot = new byte[2]
                {
                    level.rotx, level.roty
                };
                GlobalSpawn(this, num, num2, num3, rot[0], rot[1], self: true);
                SendSpawn(byte.MaxValue, color + (IsRefree ? "[REF]" : "") + PublicName, ModelName, num, num2, num3, rot[0], rot[1]);
                SpawnPlayers();
                SpawnBots();
            }
            catch (Exception ex7)
            {
                Server.ErrorLog(ex7);
                Server.s.Log(string.Format("Error spawning player \"{0}\"", name));
            }
            flagsCollection.FlagContainer = flags;
            if (Server.devs.Contains(name.ToLower()))
            {
                if (color == Group.standard.color)
                {
                    color = "&9";
                }
                if (prefix == "")
                {
                    title = "Dev";
                }
                SetPrefix();
            }
            Loading = false;
            fullyloggedtime = DateTime.Now;
            fullylogged = true;
            if (!players.Contains(this))
            {
                players.Add(this);
            }
            Server.s.PlayerListUpdate();
            if (emoteList.Contains(name))
            {
                parseSmiley = false;
            }
            if (welcomeMessage == "")
            {
                GlobalChatWorld(
                    null, string.Format(MessagesManager.GetString("GlobalMessagePlayerJoined"), "&a+ " + color + prefix + color + PublicName + Server.DefaultColor),
                    showname: false);
            }
            else
            {
                GlobalChatWorld(null, "&a+ " + color + prefix + PublicName + Server.DefaultColor + " " + welcomeMessage, showname: false);
            }
            Server.s.Log(string.Format("{0} [{1}] has joined the server.", name, ip));
            OnPlayerJoined(null, new PlayerEventArgs(this));
        }

        AuthenticationProvider VerifyPlayer(string verificationHash)
        {
            if (VerifyPlayer(verificationHash, Server.Salt))
            {
                return AuthenticationProvider.Mojang;
            }
            if (VerifyPlayer(verificationHash, Server.SaltClassiCube))
            {
                return AuthenticationProvider.ClassiCube;
            }
            return AuthenticationProvider.Unknown;
        }

        public static string RemoveEmailDomain(string name)
        {
            if (name.Contains('@'))
            {
                name = name.Remove(name.IndexOf('@') + 1);
            }
            return name;
        }

        public bool VerifyPlayer(string hash, string salt)
        {
            if (salt.IsNullOrWhiteSpaced() || salt.Length < 16)
            {
                return false;
            }
            hash = hash.PadLeft(32, '0');
            string value = BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(enc.GetBytes(salt + name))).Replace("-", "");
            if (hash.Equals(value, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }

        public void TestConnection() {}

        public void SetPrefix()
        {
            prefix = title == "" ? "" : titlecolor == "" ? "[" + title + "]" : "[" + titlecolor + title + color + "]";
        }

        void HandleBlockchange(byte[] message)
        {
            try
            {
                if (loggedIn)
                {
                    ushort num = NTHO(message, 0);
                    ushort num2 = NTHO(message, 2);
                    ushort num3 = NTHO(message, 4);
                    byte action = message[6];
                    byte type = message[7];
                    manualChange(num, num2, num3, action, type);
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        public void manualChange(ushort x, ushort y, ushort z, byte action, byte type)
        {
            if (type > 80)
            {
                Kick("Unknown block type!");
                return;
            }
            byte tile = level.GetTile(x, y, z);
            bool flag = false;
            switch (action)
            {
                case 0:
                    if (level.CommandActionsHit != null && level.CommandActionsHit.ContainsKey(level.PosToInt(x, y, z)))
                    {
                        if (level.CommandActionsHit[level.PosToInt(x, y, z)].changeAction == ChangeAction.Restore && type != tile)
                        {
                            SendBlockchange(x, y, z, tile);
                            flag = true;
                        }
                        HandleCommandAction(level.CommandActionsHit[level.PosToInt(x, y, z)]);
                        if (flag)
                        {
                            return;
                        }
                    }
                    break;
                case 1:
                    if (level.CommandActionsBuild != null && level.CommandActionsBuild.ContainsKey(level.PosToInt(x, y, z)))
                    {
                        if (level.CommandActionsHit[level.PosToInt(x, y, z)].changeAction == ChangeAction.Restore && type != tile)
                        {
                            SendBlockchange(x, y, z, tile);
                            flag = true;
                        }
                        HandleCommandAction(level.CommandActionsBuild[level.PosToInt(x, y, z)]);
                        if (flag)
                        {
                            return;
                        }
                    }
                    break;
            }
            bool stopChange = false;
            if (action == 1 && IsBlockPlacedUnderPlayer(x, y, z) && IsPlacedBlockOnlyBlockUnderPlayer(y))
            {
                OnPlayerPillared(this, x, y, z, ref stopChange);
            }
            if (stopChange)
            {
                if (!flag)
                {
                    SendBlockchange(x, y, z, tile);
                }
            }
            else
            {
                if (tile == byte.MaxValue)
                {
                    return;
                }
                if (jailed)
                {
                    if (!flag)
                    {
                        SendBlockchange(x, y, z, tile);
                    }
                }
                else
                {
                    if (level.name.Contains("Museum " + Server.DefaultColor) && Blockchange == null)
                    {
                        return;
                    }
                    if (!deleteMode)
                    {
                        string text = level.foundInfo(x, y, z);
                        if (text.Contains("wait"))
                        {
                            return;
                        }
                    }
                    if (!canBuild)
                    {
                        if (!flag)
                        {
                            SendBlockchange(x, y, z, tile);
                        }
                        return;
                    }
                    if (level.mapType == MapType.MyMap && !level.IsPublic && group.Permission < LevelPermission.Operator && level.Owner.ToLower() != name.ToLower() &&
                        !level.AllowedPlayers.Contains(name.ToLower()))
                    {
                        SendMessage(this, "You are not allowed to build on this map. Ask the owner for permission.");
                        if (!flag)
                        {
                            SendBlockchange(x, y, z, tile);
                        }
                        return;
                    }
                    if (level.mapType == MapType.Zombie && Block.OPBlocks(tile))
                    {
                        if (!flag)
                        {
                            SendBlockchange(x, y, z, tile);
                        }
                        SendMessage("Cannot build here.");
                        return;
                    }
                    if (level.mapType == MapType.Zombie && !InfectionSystem.InfectionSystem.currentInfectionMap.IsBuildingAllowed && !Block.IsDoor(tile))
                    {
                        if (!flag)
                        {
                            SendBlockchange(x, y, z, tile);
                            SendMessage(this, "Building is not allowed on this map.");
                        }
                        return;
                    }
                    Level.BlockPos item = default(Level.BlockPos);
                    item.name = name;
                    item.TimePerformed = DateTime.Now;
                    item.x = x;
                    item.y = y;
                    item.z = z;
                    item.type = type;
                    lastClick[0] = x;
                    lastClick[1] = y;
                    lastClick[2] = z;
                    bool stopChange2 = false;
                    OnBlockChangeProper(this, x, y, z, type, action, ref stopChange2);
                    if (stopChange2)
                    {
                        SendBlockchange(x, y, z, level.GetTile(x, y, z));
                    }
                    else if (Blockchange != null && Blockchange2 != null)
                    {
                        if (Blockchange.Method.ToString().IndexOf("AboutBlockchange") == -1 && !level.name.Contains("Museum " + Server.DefaultColor))
                        {
                            item.deleted = true;
                            level.blockCache.Add(item);
                        }
                        Blockchange(this, x, y, z, type);
                        if (Blockchange2.Method.ToString().IndexOf("AboutBlockchange") == -1 && !level.name.Contains("Museum " + Server.DefaultColor))
                        {
                            item.deleted = true;
                            level.blockCache.Add(item);
                        }
                        Blockchange2(this, x, y, z, type, action);
                    }
                    else if (Blockchange != null)
                    {
                        if (Blockchange.Method.ToString().IndexOf("AboutBlockchange") == -1 && !level.name.Contains("Museum " + Server.DefaultColor))
                        {
                            item.deleted = true;
                            level.blockCache.Add(item);
                        }
                        Blockchange(this, x, y, z, type);
                    }
                    else if (Blockchange2 != null)
                    {
                        if (Blockchange2.Method.ToString().IndexOf("AboutBlockchange") == -1 && !level.name.Contains("Museum " + Server.DefaultColor))
                        {
                            item.deleted = true;
                            level.blockCache.Add(item);
                        }
                        Blockchange2(this, x, y, z, type, action);
                    }
                    else
                    {
                        if (group.Permission == LevelPermission.Banned)
                        {
                            return;
                        }
                        if (group.Permission == LevelPermission.Guest)
                        {
                            int num = 0;
                            num = Math.Abs(pos[0] / 32 - x);
                            num += Math.Abs(pos[1] / 32 - y);
                            num += Math.Abs(pos[2] / 32 - z);
                            if (num > 12 && LavaSettings.All.ShowDistanceOffsetMessage)
                            {
                                Server.s.Log(string.Format("{0} attempted to build with a {1} distance offset.", name, num));
                                SendMessage(rm.GetString("WarningBuiltTooFar"));
                                if (!flag)
                                {
                                    SendBlockchange(x, y, z, tile);
                                }
                                return;
                            }
                            if (Server.antiTunnel && !ignoreGrief && y < level.height / 2 - Server.maxDepth)
                            {
                                SendMessage(rm.GetString("WarningBuiltTooLow"));
                                if (!flag)
                                {
                                    SendBlockchange(x, y, z, tile);
                                }
                                return;
                            }
                        }
                        if (!Block.canPlace(this, tile) && !Block.BuildIn(tile) && !Block.AllowBreak(tile))
                        {
                            SendMessage(rm.GetString("WarningCantBuildHere"));
                            if (!flag)
                            {
                                SendBlockchange(x, y, z, tile);
                            }
                            return;
                        }
                        if (!Block.canPlace(this, type))
                        {
                            SendMessage(rm.GetString("WarningDisallowedBlockType"));
                            if (!flag)
                            {
                                SendBlockchange(x, y, z, tile);
                            }
                            return;
                        }
                        if (tile >= 200 && tile < 220)
                        {
                            SendMessage(rm.GetString("WarningCantDisturbBlock"));
                            if (!flag)
                            {
                                SendBlockchange(x, y, z, tile);
                            }
                            return;
                        }
                        if (action > 1)
                        {
                            Kick(rm.GetString("KickUnknownAction"));
                        }
                        if (level.GetTile(x, y, z) == 97)
                        {
                            LavaSystem.FoundTreasure(this, x, y, z);
                        }
                        byte b = type;
                        type = bindings[type];
                        if (tile == (byte)(painting || action == 1 ? type : 0))
                        {
                            if (painting || b != type)
                            {
                                SendBlockchange(x, y, z, tile);
                            }
                        }
                        else if (!painting && action == 0)
                        {
                            if (!deleteMode)
                            {
                                if (Block.portal(tile))
                                {
                                    HandlePortal(this, x, y, z, tile);
                                    return;
                                }
                                if (Block.mb(tile))
                                {
                                    HandleMsgBlock(this, x, y, z, tile);
                                    return;
                                }
                            }
                            item.deleted = true;
                            level.blockCache.Add(item);
                            deleteBlock(tile, type, x, y, z);
                        }
                        else
                        {
                            item.deleted = false;
                            level.blockCache.Add(item);
                            placeBlock(tile, type, x, y, z);
                        }
                    }
                }
            }
        }

        bool IsPlacedBlockOnlyBlockUnderPlayer(ushort y)
        {
            bool flag = false;
            BlockPos[] blocksUnder = GetBlocksUnder();
            for (int i = 0; i < blocksUnder.Length; i++)
            {
                BlockPos blockPos = blocksUnder[i];
                if (!Block.Walkthrough(level.GetTile(blockPos.x, y, blockPos.z)))
                {
                    flag = true;
                }
            }
            if (flag)
            {
                return false;
            }
            return true;
        }

        bool IsBlockPlacedUnderPlayer(ushort x, ushort y, ushort z)
        {
            if (!EqualsPlayerPosition(x, y + 2, z) && !EqualsPlayerPosition(x, y + 3, z))
            {
                return EqualsPlayerPosition(x, y + 4, z);
            }
            return true;
        }

        public void ManualChangeCheck(ushort x, ushort y, ushort z, byte action, byte type)
        {
            Level.BlockPos item = default(Level.BlockPos);
            item.name = name;
            item.TimePerformed = DateTime.Now;
            item.x = x;
            item.y = y;
            item.z = z;
            item.type = type;
            byte tile = level.GetTile(x, y, z);
            switch (action)
            {
                case 0:
                    if (level.CommandActionsHit != null && level.CommandActionsHit.ContainsKey(level.PosToInt(x, y, z)))
                    {
                        if (level.CommandActionsHit[level.PosToInt(x, y, z)].changeAction == ChangeAction.Restore && type != tile)
                        {
                            SendBlockchange(x, y, z, tile);
                        }
                        HandleCommandAction(level.CommandActionsHit[level.PosToInt(x, y, z)]);
                    }
                    break;
                case 1:
                    if (level.CommandActionsBuild != null && level.CommandActionsBuild.ContainsKey(level.PosToInt(x, y, z)))
                    {
                        if (level.CommandActionsHit[level.PosToInt(x, y, z)].changeAction == ChangeAction.Restore && type != tile)
                        {
                            SendBlockchange(x, y, z, tile);
                        }
                        HandleCommandAction(level.CommandActionsBuild[level.PosToInt(x, y, z)]);
                    }
                    break;
            }
            if (group.Permission == LevelPermission.Banned)
            {
                return;
            }
            if (group.Permission == LevelPermission.Guest)
            {
                int num = 0;
                num = Math.Abs(pos[0] / 32 - x);
                num += Math.Abs(pos[1] / 32 - y);
                num += Math.Abs(pos[2] / 32 - z);
                if (num > 12 && LavaSettings.All.ShowDistanceOffsetMessage)
                {
                    Server.s.Log(string.Format("{0} attempted to build with a {1} distance offset.", name, num));
                    GlobalMessageOps(string.Format("To Ops &f-{0}&f- attempted to build with a {1} distance offset.", color + name, num));
                    SendMessage(rm.GetString("WarningBuiltTooFar"));
                    SendBlockchange(x, y, z, tile);
                    return;
                }
                if (Server.antiTunnel && !ignoreGrief && y < level.height / 2 - Server.maxDepth)
                {
                    SendMessage(rm.GetString("WarningBuiltTooLow"));
                    SendBlockchange(x, y, z, tile);
                    return;
                }
            }
            if (!Block.canPlace(this, tile) && !Block.BuildIn(tile) && !Block.AllowBreak(tile))
            {
                SendMessage(rm.GetString("WarningCantBuildHere"));
                SendBlockchange(x, y, z, tile);
                return;
            }
            if (!Block.canPlace(this, type))
            {
                SendMessage(rm.GetString("WarningDisallowedBlockType"));
                SendBlockchange(x, y, z, tile);
                return;
            }
            if (tile >= 200 && tile < 220)
            {
                SendMessage(rm.GetString("WarningCantDisturbBlock"));
                SendBlockchange(x, y, z, tile);
                return;
            }
            if (action > 1)
            {
                Kick(rm.GetString("KickUnknownAction"));
            }
            if (level.GetTile(x, y, z) == 97)
            {
                LavaSystem.FoundTreasure(this, x, y, z);
            }
            byte b = type;
            type = bindings[type];
            if (tile == (byte)(painting || action == 1 ? type : 0))
            {
                if (painting || b != type)
                {
                    SendBlockchange(x, y, z, tile);
                }
            }
            else if (!painting && action == 0)
            {
                if (!deleteMode)
                {
                    if (Block.portal(tile))
                    {
                        HandlePortal(this, x, y, z, tile);
                        return;
                    }
                    if (Block.mb(tile))
                    {
                        HandleMsgBlock(this, x, y, z, tile);
                        return;
                    }
                }
                item.deleted = true;
                level.blockCache.Add(item);
                deleteBlock(tile, type, x, y, z);
            }
            else
            {
                item.deleted = false;
                level.blockCache.Add(item);
                placeBlock(tile, type, x, y, z);
            }
        }

        public void HandlePortal(Player p, ushort x, ushort y, ushort z, byte b)
        {
            try
            {
                string text = null;
                text = p.level.mapType != MapType.MyMap
                    ? "SELECT * FROM `Portals" + this.level.name + "` WHERE EntryX=" + (int)x + " AND EntryY=" + (int)y + " AND EntryZ=" + (int)z
                    : "SELECT * FROM `Portals` WHERE Map=" + p.level.MapDbId + " AND EntryX=" + x + " AND EntryY=" + y + " AND EntryZ=" + z;
                using (DataTable dataTable = DBInterface.fillData(text))
                {
                    int num = dataTable.Rows.Count - 1;
                    if (num > -1)
                    {
                        string text2 = dataTable.Rows[num]["ExitMap"].ToString();
                        if (this.level.name != text2)
                        {
                            Level level = this.level;
                            Command.all.Find("goto").Use(this, text2);
                            if (level == this.level)
                            {
                                SendMessage(p, rm.GetString("WarningPortalDestinationUnloaded"));
                                return;
                            }
                        }
                        else
                        {
                            SendBlockchange(x, y, z, b);
                        }
                        while (p.Loading)
                        {
                            Thread.Sleep(10);
                        }
                        if (text2 != "lava")
                        {
                            Command.all.Find("move")
                                .Use(this,
                                     name + " " + dataTable.Rows[num]["ExitX"] + " " + dataTable.Rows[num]["ExitY"] + " " +
                                     dataTable.Rows[num]["ExitZ"]);
                        }
                    }
                    else
                    {
                        Blockchange(this, x, y, z, 0);
                    }
                }
            }
            catch (Exception)
            {
                Server.s.Log(string.Format("Portal on map: {0}, coordinates(x,y,z) {1},{2},{3} has no exit.", level.name, x, y, z));
                SendMessage(p, rm.GetString("WarningPortalHasNoExit"));
            }
        }

        public void HandleMsgBlock(Player p, ushort x, ushort y, ushort z, byte b)
        {
            try
            {
                string text = null;
                text = p.level.mapType != MapType.MyMap ? "SELECT * FROM `Messages" + level.name + "` WHERE X=" + (int)x + " AND Y=" + (int)y + " AND Z=" + (int)z
                    : "SELECT * FROM `Messages` WHERE Map=" + p.level.MapDbId + " AND X=" + x + " AND Y=" + y + " AND Z=" + z;
                using (DataTable dataTable = DBInterface.fillData(text))
                {
                    int num = dataTable.Rows.Count - 1;
                    if (num > -1)
                    {
                        string text2 = dataTable.Rows[num]["Message"].ToString().Trim();
                        if (text2 != prevMsg || Server.repeatMessage)
                        {
                            SendMessage(p, text2);
                            prevMsg = text2;
                        }
                        SendBlockchange(x, y, z, b);
                    }
                    else
                    {
                        Blockchange(this, x, y, z, 0);
                    }
                }
            }
            catch
            {
                SendMessage(p, rm.GetString("WarningNoMessageStored"));
            }
        }

        bool checkOp()
        {
            return group.Permission < LevelPermission.Operator;
        }

        void deleteBlock(byte b, byte type, ushort x, ushort y, ushort z)
        {
            Random random = new Random();
            if (deleteMode)
            {
                level.Blockchange(this, x, y, z, 0);
                return;
            }
            if (Block.tDoor(b))
            {
                SendBlockchange(x, y, z, b);
                return;
            }
            if (Block.DoorAirs(b) != 0)
            {
                if (level.physics != 0)
                {
                    level.Blockchange(x, y, z, Block.DoorAirs(b));
                }
                else
                {
                    SendBlockchange(x, y, z, b);
                }
                return;
            }
            if (Block.odoor(b) != byte.MaxValue)
            {
                if (b == 155 || b == 177)
                {
                    level.Blockchange(this, x, y, z, Block.odoor(b));
                }
                else
                {
                    SendBlockchange(x, y, z, b);
                }
                return;
            }
            switch (b)
            {
                case 187:
                {
                    if (level.physics < 2)
                    {
                        SendBlockchange(x, y, z, b);
                        break;
                    }
                    int num3 = 0;
                    int num4 = 0;
                    int num5 = 0;
                    SendBlockchange(x, y, z, 187);
                    if (rot[0] < 48 || rot[0] > 208)
                    {
                        num3 = -1;
                    }
                    else if (rot[0] > 80 && rot[0] < 176)
                    {
                        num3 = 1;
                    }
                    if (rot[0] > 16 && rot[0] < 112)
                    {
                        num4 = 1;
                    }
                    else if (rot[0] > 144 && rot[0] < 240)
                    {
                        num4 = -1;
                    }
                    if (rot[1] >= 192 && rot[1] <= 224)
                    {
                        num5 = 1;
                    }
                    else if (rot[1] <= 64 && rot[1] >= 32)
                    {
                        num5 = -1;
                    }
                    if (192 <= rot[1] && rot[1] <= 196 || 60 <= rot[1] && rot[1] <= 64)
                    {
                        num4 = 0;
                        num3 = 0;
                    }
                    level.Blockchange((ushort)(x + num4 * 2), (ushort)(y + num5 * 2), (ushort)(z + num3 * 2), 188);
                    level.Blockchange((ushort)(x + num4), (ushort)(y + num5), (ushort)(z + num3), 185);
                    break;
                }
                case 189:
                    if (level.physics != 0)
                    {
                        int num = random.Next(0, 2);
                        int num2 = random.Next(0, 2);
                        level.Blockchange((ushort)(x + num - 1), (ushort)(y + 2), (ushort)(z + num2 - 1), 189);
                        level.Blockchange((ushort)(x + num - 1), (ushort)(y + 1), (ushort)(z + num2 - 1), 11, false, "wait 1 dissipate 100");
                    }
                    SendBlockchange(x, y, z, b);
                    break;
                default:
                    level.Blockchange(this, x, y, z, 0);
                    break;
                case 73:
                case 201:
                case 205:
                case 206:
                case 207:
                case 208:
                case 209:
                case 210:
                case 211:
                case 212:
                case 213:
                case 225:
                case 226:
                case 227:
                case 228:
                case 229:
                    break;
            }
        }

        public void placeBlock(byte b, byte type, ushort x, ushort y, ushort z)
        {
            if (Block.odoor(b) != byte.MaxValue)
            {
                SendMessage("oDoor here!");
                return;
            }
            switch (BlockAction)
            {
                case 0:
                    if (level.physics == 0)
                    {
                        switch (type)
                        {
                            case 3:
                                level.Blockchange(this, x, y, z, 2);
                                break;
                            case 44:
                                if (level.GetTile(x, (ushort)(y - 1), z) == 44)
                                {
                                    SendBlockchange(x, y, z, 0);
                                    level.Blockchange(this, x, (ushort)(y - 1), z, 43);
                                }
                                else
                                {
                                    level.Blockchange(this, x, y, z, type);
                                }
                                break;
                            default:
                                level.Blockchange(this, x, y, z, type);
                                break;
                        }
                    }
                    else
                    {
                        level.Blockchange(this, x, y, z, type);
                    }
                    break;
                case 6:
                    if (b == modeType)
                    {
                        SendBlockchange(x, y, z, b);
                    }
                    else
                    {
                        level.Blockchange(this, x, y, z, modeType);
                    }
                    break;
                case 13:
                    level.Blockchange(this, x, y, z, 182);
                    break;
                case 14:
                    level.Blockchange(this, x, y, z, 183);
                    break;
                default:
                    Server.s.Log(string.Format(rm.GetString("WarningBreakingBlocks"), name));
                    BlockAction = 0;
                    break;
            }
        }

        void Analyse(int x, int y, int z)
        {
            if (analyse.Count > 0)
            {
                if (analyse[analyse.Count - 1] < y)
                {
                    analyse.Add(y);
                    direction = 1;
                }
                else if (analyse[analyse.Count - 1] > y && direction != -1)
                {
                    msgUp = analyse.Count;
                    Server.s.Log("Messages when jumping up: " + msgUp);
                    analyse.Add(y);
                    direction = -1;
                }
                else if (analyse[analyse.Count - 1] > y && analyse[0] != y && direction == -1)
                {
                    analyse.Add(y);
                }
                else if (analyse[0] == y && direction == -1)
                {
                    msgDown = analyse.Count - msgUp;
                    Server.s.Log("Messages when falling down: " + msgDown);
                    analyse.Clear();
                    direction = 0;
                }
            }
            else
            {
                analyse.Add(y);
            }
        }

        void HandlePositionInfo(object m)
        {
            byte[] array = (byte[])m;
            if (!loggedIn || !fullylogged || trainGrab || following != "" || frozen)
            {
                return;
            }
            ushort num = NTHO(array, 1);
            ushort num2 = NTHO(array, 3);
            ushort num3 = NTHO(array, 5);
            byte b = array[7];
            byte b2 = array[8];
            if (waitForFall)
            {
                if (num2 > oldy)
                {
                    return;
                }
                waitForFall = false;
                canBuild = true;
            }
            lock (hacksDetectionSync)
            {
                if (!HacksDetection(num, num2, num3, b, b2) && updatePosition)
                {
                    pos = new ushort[3]
                    {
                        num, num2, num3
                    };
                    rot = new byte[2]
                    {
                        b, b2
                    };
                    setPos(XFloat, YFloat, ZFloat);
                    OnPositionChanged(new PositionChangedEventArgs(num, num2, num3, b, b2));
                }
            }
            if (updatePosition)
            {
                return;
            }
            lock (fallSynchronize)
            {
                if (updatePosition)
                {
                    return;
                }
                fallPositionBuffer.Add(num2);
                if (fallPositionBuffer.Count == 6)
                {
                    int oPos = originalFallPos.y * 32 + 53;
                    if (fallPositionBuffer.Where(n => n <= oPos).Count() == 0)
                    {
                        SendPos(byte.MaxValue, (ushort)(originalFallPos.x * 32 + 16), (ushort)(originalFallPos.y * 32 + 53), (ushort)(originalFallPos.z * 32 + 16), rot[0],
                                rot[1]);
                        oldx = (ushort)(originalFallPos.x * 32 + 16);
                        oldy = (ushort)(originalFallPos.y * 32 + 58);
                        oldz = (ushort)(originalFallPos.z * 32 + 16);
                        waitForFall = true;
                    }
                    else
                    {
                        canBuild = true;
                    }
                    updatePosition = true;
                    fallPositionBuffer.Clear();
                }
            }
        }

        public void RealDeath(int x, int y, int z)
        {
            byte tile = level.GetTile(x, (ushort)(y - 2), z);
            byte tile2 = level.GetTile(x, y, z);
            if (oldBlock != (ushort)(x + y + z))
            {
                if (Block.Convert(tile) == 0)
                {
                    deathCount++;
                    deathBlock = 0;
                    return;
                }
                if (deathCount > level.fall && deathBlock == 0)
                {
                    HandleDeath(deathBlock);
                    deathCount = 0;
                }
                else if (deathBlock != 8)
                {
                    deathCount = 0;
                }
            }
            switch (Block.Convert(tile2))
            {
                case 8:
                case 9:
                case 10:
                case 11:
                    deathCount++;
                    deathBlock = 8;
                    if (deathCount > level.drown * 200)
                    {
                        HandleDeath(deathBlock);
                        deathCount = 0;
                    }
                    break;
                default:
                    deathCount = 0;
                    break;
            }
        }

        public void OnBlockChangeProper(Player p, ushort x, ushort y, ushort z, byte type, byte action, ref bool stopChange)
        {
            if (BlockChangeProper != null)
            {
                BlockChangeProper(p, x, y, z, type, action, ref stopChange);
            }
        }

        public void HandleCommandAction(CommandActionPair commandAction)
        {
            DateTime now = DateTime.Now;
            foreach (CommandElement blockCommand in commandAction.blockCommands)
            {
                DateTime value;
                if (blockCommandCooldowns.TryGetValue(blockCommand.GetHashCode(), out value) && value.AddMilliseconds(blockCommand.cooldown.Value * 1000f) > now)
                {
                    return;
                }
                string commandString = blockCommand.commandString;
                commandString = commandString.Replace("{name}", name);
                Message message = new Message(commandString.Trim());
                string text = message.ReadString();
                string message2 = message.ReadToEnd() ?? "";
                try
                {
                    if (blockCommand.consoleUse.Value)
                    {
                        Command.all.Find(text).Use(null, message2);
                    }
                    else
                    {
                        Command.all.Find(text).Use(this, message2);
                    }
                }
                catch (Exception ex)
                {
                    Server.ErrorLog(ex);
                }
                if (!blockCommandCooldowns.ContainsKey(blockCommand.GetHashCode()))
                {
                    blockCommandCooldowns.Add(blockCommand.GetHashCode(), now);
                }
                else
                {
                    blockCommandCooldowns[blockCommand.GetHashCode()] = now;
                }
            }
            // foreach (ActionElement blockAction in commandAction.blockActions)
            // {
            //     _ = blockAction;
            // }
        }

        public void CheckBlock(ushort x, ushort y, ushort z)
        {
            y = (ushort)Math.Round((decimal)((y * 32 + 4) / 32));
            int key = level.PosToInt(x, y, z);
            int key2 = level.PosToInt(x, y - 1, z);
            byte tile = level.GetTile(x, y, z);
            byte tile2 = level.GetTile(x, y - 1, z);
            if (level.CommandActionsWalk != null)
            {
                CommandActionPair value;
                if (level.CommandActionsWalk.TryGetValue(key, out value))
                {
                    HandleCommandAction(value);
                }
                CommandActionPair value2;
                if (level.CommandActionsWalk.TryGetValue(key2, out value2))
                {
                    HandleCommandAction(value2);
                }
            }
            if (Block.Mover(tile) || Block.Mover(tile2))
            {
                if (Block.DoorAirs(tile) != 0)
                {
                    level.Blockchange(x, y, z, Block.DoorAirs(tile));
                }
                if (Block.DoorAirs(tile2) != 0)
                {
                    level.Blockchange(x, (ushort)(y - 1), z, Block.DoorAirs(tile2));
                }
                if (x + y + z != oldBlock)
                {
                    if (tile == 160 || tile == 161 || tile == 162)
                    {
                        HandlePortal(this, x, y, z, tile);
                    }
                    else if (tile2 == 160 || tile2 == 161 || tile2 == 162)
                    {
                        HandlePortal(this, x, (ushort)(y - 1), z, tile2);
                    }
                    if (tile == 132 || tile == 133 || tile == 134)
                    {
                        HandleMsgBlock(this, x, y, z, tile);
                    }
                    else
                    {
                        switch (tile2)
                        {
                            case 132:
                            case 133:
                            case 134:
                                HandleMsgBlock(this, x, (ushort)(y - 1), z, tile2);
                                break;
                            case 70:
                                if (team == null)
                                {
                                    break;
                                }
                                y--;
                                foreach (Team t in level.ctfgame.teams)
                                {
                                    if (t.flagLocation[0] != x || t.flagLocation[1] != y || t.flagLocation[2] != z)
                                    {
                                        continue;
                                    }
                                    if (t == this.team)
                                    {
                                        if (t.flagishome && carryingFlag)
                                        {
                                            level.ctfgame.CaptureFlag(this, t, hasflag);
                                        }
                                    }
                                    else
                                    {
                                        level.ctfgame.GrabFlag(this, t);
                                    }
                                }
                                break;
                        }
                    }
                }
            }
            if (Block.Death(tile))
            {
                HandleDeath(tile);
            }
            else if (Block.Death(tile2))
            {
                HandleDeath(tile2);
            }
        }

        public void SubtractLife()
        {
            if (level.mapType != MapType.Lava)
            {
                return;
            }
            if (IronChallenge != 0)
            {
                int num = money >= LavaSettings.All.RewardAboveSeaLevel ? LavaSettings.All.RewardAboveSeaLevel : money;
                money -= num;
                if (IronChallenge == IronChallengeType.IronMan)
                {
                    GlobalChatLevel(this, color + PublicName + "%c has failed Iron Man challenge.", showname: false);
                }
                else if (IronChallenge == IronChallengeType.IronWoman)
                {
                    GlobalChatLevel(this, color + PublicName + "%c has failed Iron Woman challenge.", showname: false);
                }
                SendMessage(this, "You have lost " + num + " " + Server.moneys + ".");
                IronChallenge = IronChallengeType.None;
            }
            if (lives == 1)
            {
                if (Server.useHeaven)
                {
                    lives--;
                    GlobalChatLevel(this, string.Format(MessagesManager.GetString("IsGhost"), color + prefix + PublicName + Server.DefaultColor), showname: false);
                    Command.all.Find("goto").Use(this, Server.heavenMapName);
                    invincible = true;
                    inHeaven = true;
                    winningStreak = 0;
                    SendMessage(MessagesManager.GetString("SentToHeaven"));
                    SendMessage(MessagesManager.GetString("HelpMessageInHeaven"));
                }
                else
                {
                    lives--;
                    invincible = true;
                    GlobalChatLevel(this, string.Format(MessagesManager.GetString("IsGhost"), color + prefix + PublicName + Server.DefaultColor), showname: false);
                    SendMessage(this, MessagesManager.GetString("HelpMessageGhost"));
                    winningStreak = 0;
                    if (LavaSettings.All.HeadlessGhosts)
                    {
                        flipHead = true;
                    }
                }
            }
            if (lives == 2)
            {
                lives--;
                SendMessage(this, "You have 1 life left");
                SendMessage(this, "You are immortal for the next 30 seconds.");
            }
            if (lives > 2)
            {
                lives--;
                SendMessage(this, string.Format("You have {0} lives left", lives));
                SendMessage(this, "You are immortal for the next 30 seconds.");
            }
        }

        public void HandleDeath(byte b, string customMessage = "", bool explode = false)
        {
            ushort num = (ushort)(pos[0] / 32);
            ushort num2 = (ushort)(pos[1] / 32);
            ushort num3 = (ushort)(pos[2] / 32);
            if (!(armorTime.AddSeconds(45.0) < DateTime.Now) && !(customMessage != "") ||
                !(lastDeath.AddSeconds(GeneralSettings.All.DeathCooldown) < DateTime.Now) && !(customMessage != ""))
            {
                return;
            }
            if (level.Killer && !invincible)
            {
                bool flag = players.Count < LavaSettings.All.HideDeathMessagesAmount;
                if (!hidden)
                {
                    switch (b)
                    {
                        case 184:
                            if (flag)
                            {
                                GlobalChatLevel(this, string.Format(MessagesManager.GetString("DeathTntExplosion"), color + prefix + PublicName + Server.DefaultColor),
                                                showname: false);
                            }
                            else
                            {
                                SendMessage(this, string.Format("@" + MessagesManager.GetString("DeathTntExplosion"), color + prefix + PublicName + Server.DefaultColor));
                            }
                            break;
                        case 192:
                            if (flag)
                            {
                                GlobalChatLevel(this, string.Format(MessagesManager.GetString("DeathAir"), color + prefix + PublicName + Server.DefaultColor),
                                                showname: false);
                            }
                            else
                            {
                                SendMessage(this, string.Format("@" + MessagesManager.GetString("DeathAir"), color + prefix + PublicName + Server.DefaultColor));
                            }
                            break;
                        case 74:
                        case 141:
                        case 191:
                        case 193:
                            if (flag)
                            {
                                GlobalChatLevel(this, string.Format(MessagesManager.GetString("DeathActiveColdWater"), color + prefix + PublicName + Server.DefaultColor),
                                                showname: false);
                            }
                            else
                            {
                                SendMessage(
                                    this, string.Format("@" + MessagesManager.GetString("DeathActiveColdWater"), color + prefix + PublicName + Server.DefaultColor));
                            }
                            if (!inHeaven)
                            {
                                SubtractLife();
                            }
                            break;
                        case 80:
                        case 81:
                        case 82:
                        case 83:
                        case 98:
                        case 112:
                        case 190:
                        case 194:
                            if (flag)
                            {
                                GlobalChatLevel(this, string.Format(MessagesManager.GetString("DeathActiveHotLava"), color + prefix + PublicName + Server.DefaultColor),
                                                showname: false);
                            }
                            else
                            {
                                SendMessage(this, string.Format("@" + MessagesManager.GetString("DeathActiveHotLava"), color + prefix + PublicName + Server.DefaultColor));
                            }
                            if (!inHeaven)
                            {
                                SubtractLife();
                            }
                            break;
                        case 195:
                            if (flag)
                            {
                                GlobalChatLevel(this, string.Format(MessagesManager.GetString("DeathMagma"), color + prefix + PublicName + Server.DefaultColor),
                                                showname: false);
                            }
                            else
                            {
                                SendMessage(this, string.Format("@" + MessagesManager.GetString("DeathMagma"), color + prefix + PublicName + Server.DefaultColor));
                            }
                            if (!inHeaven)
                            {
                                SubtractLife();
                            }
                            break;
                        case 196:
                            if (flag)
                            {
                                GlobalChatLevel(this, string.Format(MessagesManager.GetString("DeathGeyser"), color + prefix + PublicName + Server.DefaultColor),
                                                showname: false);
                            }
                            else
                            {
                                SendMessage(this, string.Format("@" + MessagesManager.GetString("DeathGeyser"), color + prefix + PublicName + Server.DefaultColor));
                            }
                            break;
                        case 242:
                            if (flag)
                            {
                                GlobalChatLevel(this, string.Format(MessagesManager.GetString("DeathBird"), color + prefix + PublicName + Server.DefaultColor),
                                                showname: false);
                            }
                            else
                            {
                                SendMessage(this, string.Format("@" + MessagesManager.GetString("DeathBird"), color + prefix + PublicName + Server.DefaultColor));
                            }
                            break;
                        case 230:
                            if (flag)
                            {
                                GlobalChatLevel(this, string.Format(MessagesManager.GetString("DeathTrain"), color + prefix + PublicName + Server.DefaultColor),
                                                showname: false);
                            }
                            else
                            {
                                SendMessage(this, string.Format("@" + MessagesManager.GetString("DeathTrain"), color + prefix + PublicName + Server.DefaultColor));
                            }
                            break;
                        case 247:
                            if (flag)
                            {
                                GlobalChatLevel(this, string.Format(MessagesManager.GetString("DeathShark"), color + prefix + PublicName + Server.DefaultColor),
                                                showname: false);
                            }
                            else
                            {
                                SendMessage(this, string.Format("@" + MessagesManager.GetString("DeathShark"), color + prefix + PublicName + Server.DefaultColor));
                            }
                            break;
                        case 185:
                            if (flag)
                            {
                                GlobalChatLevel(this, string.Format(MessagesManager.GetString("DeathFire"), color + prefix + PublicName + Server.DefaultColor),
                                                showname: false);
                            }
                            else
                            {
                                SendMessage(this, string.Format("@" + MessagesManager.GetString("DeathFire"), color + prefix + PublicName + Server.DefaultColor));
                            }
                            break;
                        case 188:
                            if (flag)
                            {
                                GlobalChatLevel(this, string.Format(MessagesManager.GetString("DeathRocket"), color + prefix + PublicName + Server.DefaultColor),
                                                showname: false);
                            }
                            else
                            {
                                SendMessage(this, string.Format("@" + MessagesManager.GetString("DeathRocket"), color + prefix + PublicName + Server.DefaultColor));
                            }
                            level.MakeExplosion(num, num2, num3, 0);
                            break;
                        case 232:
                            if (flag)
                            {
                                GlobalChatLevel(this, string.Format(MessagesManager.GetString("DeathZombie"), color + prefix + PublicName + Server.DefaultColor),
                                                showname: false);
                            }
                            else
                            {
                                SendMessage(this, string.Format("@" + MessagesManager.GetString("DeathZombie"), color + prefix + PublicName + Server.DefaultColor));
                            }
                            break;
                        case 231:
                            if (flag)
                            {
                                GlobalChatLevel(this, string.Format(MessagesManager.GetString("DeathCreeper"), color + prefix + PublicName + Server.DefaultColor),
                                                showname: false);
                            }
                            else
                            {
                                SendMessage(this, string.Format("@" + MessagesManager.GetString("DeathCreeper"), color + prefix + PublicName + Server.DefaultColor));
                            }
                            level.MakeExplosion(num, num2, num3, 1);
                            break;
                        case 0:
                            if (flag)
                            {
                                GlobalChatLevel(this, string.Format(MessagesManager.GetString("DeathFall"), color + prefix + PublicName + Server.DefaultColor),
                                                showname: false);
                            }
                            else
                            {
                                SendMessage(this, string.Format("@" + MessagesManager.GetString("DeathFall"), color + prefix + PublicName + Server.DefaultColor));
                            }
                            break;
                        case 8:
                            if (flag)
                            {
                                GlobalChatLevel(this, string.Format(MessagesManager.GetString("DeathDrawn"), color + prefix + PublicName + Server.DefaultColor),
                                                showname: false);
                            }
                            else
                            {
                                SendMessage(this, string.Format("@" + MessagesManager.GetString("DeathDrawn"), color + prefix + PublicName + Server.DefaultColor));
                            }
                            break;
                        case byte.MaxValue:
                            if (flag)
                            {
                                GlobalChatLevel(this, string.Format(MessagesManager.GetString("DeathTermination"), color + prefix + PublicName + Server.DefaultColor),
                                                showname: false);
                            }
                            else
                            {
                                SendMessage(this, string.Format("@" + MessagesManager.GetString("DeathTermination"), color + prefix + PublicName + Server.DefaultColor));
                            }
                            break;
                        case 250:
                            if (flag)
                            {
                                GlobalChatLevel(this, string.Format(MessagesManager.GetString("DeathLavaShark"), color + prefix + PublicName + Server.DefaultColor),
                                                showname: false);
                            }
                            else
                            {
                                SendMessage(this, string.Format("@" + MessagesManager.GetString("DeathLavaShark"), color + prefix + PublicName + Server.DefaultColor));
                            }
                            break;
                        case 1:
                            if (explode)
                            {
                                level.MakeExplosion(num, num2, num3, 1);
                            }
                            if (flag)
                            {
                                GlobalChat(this, color + prefix + PublicName + Server.DefaultColor + customMessage, showname: false);
                            }
                            else
                            {
                                SendMessage(this, "@" + color + prefix + PublicName + Server.DefaultColor + customMessage);
                            }
                            break;
                        case 4:
                            if (explode)
                            {
                                level.MakeExplosion(num, num2, num3, 1);
                            }
                            if (flag)
                            {
                                GlobalChatLevel(this, color + prefix + PublicName + Server.DefaultColor + customMessage, showname: false);
                            }
                            else
                            {
                                SendMessage(this, "@" + color + prefix + PublicName + Server.DefaultColor + customMessage);
                            }
                            break;
                    }
                }
                if (LavaSettings.All.SpawnOnDeath)
                {
                    Command.all.Find("spawn").Use(this, "");
                    Server.s.Log(string.Format("{0} lost a life and spawned.", name));
                }
                overallDeath++;
                if (Server.deathcount && overallDeath % 10 == 0 && !hidden && flag)
                {
                    GlobalChat(this, string.Format(rm.GetString("DiedTimesGlobalMessage"), color + prefix + PublicName + Server.DefaultColor, overallDeath),
                               showname: false);
                }
            }
            lastDeath = DateTime.Now;
        }

        void HandleChat(byte[] message)
        {
            try
            {
                if (!loggedIn)
                {
                    return;
                }
                string text = enc.GetString(message, 1, 64).Trim();
                string text2 = text;
                char[] trimChars = new char[1];
                text = text2.Trim(trimChars);
                bool handled = false;
                filterInput(ref text, out handled);
                if (handled)
                {
                    return;
                }
                if (storedMessage != "" && !text.EndsWith(">") && !text.EndsWith("<"))
                {
                    text = storedMessage.Replace("|>|", " ").Replace("|<|", "") + text;
                    storedMessage = "";
                }
                if (text.EndsWith(">"))
                {
                    storedMessage += text.Replace(">", "|>|");
                    SendMessage(rm.GetString("ChatAppended"));
                    return;
                }
                if (text.EndsWith("<"))
                {
                    storedMessage += text.Replace("<", "|<|");
                    SendMessage(rm.GetString("ChatAppended"));
                    return;
                }
                text = Regex.Replace(text, "\\s\\s+", " ");
                string text3 = text;
                foreach (char c2 in text3)
                {
                    if (c2 < ' ' || c2 >= '\u007f' || c2 == '&')
                    {
                        Kick(rm.GetString("ChatIllegalCharacter"));
                        return;
                    }
                }
                if (text.Length == 0)
                {
                    return;
                }
                afkCount = 0;
                if (text != "/afk" && Server.afkset.Contains(name))
                {
                    Server.afkset.Remove(name);
                    if (!Server.voteMode)
                    {
                        GlobalMessage(string.Format(rm.GetString("AfkNoLonger"), color + PublicName + Server.DefaultColor));
                    }
                    IRCSay(string.Format(rm.GetString("AfkNoLonger"), PublicName));
                }
                if (text[0] == '/' || text[0] == '!')
                {
                    text = text.Remove(0, 1);
                    int num = text.IndexOf(' ');
                    if (num == -1)
                    {
                        HandleCommand(text.ToLower(), "");
                        return;
                    }
                    string cmd = text.Substring(0, num).ToLower();
                    string message2 = text.Substring(num + 1);
                    HandleCommand(cmd, message2);
                    return;
                }
                if (Server.chatmod && !voice)
                {
                    SendMessage(rm.GetString("ChatModeration"));
                    return;
                }
                if (muted)
                {
                    SendMessage(rm.GetString("ChatMuted"));
                    return;
                }
                if (IsTempMuted)
                {
                    SendMessage(string.Format(rm.GetString("ChatTempMutedTime"), muteTime.Subtract(DateTime.Now).TotalSeconds.ToString("n0")));
                    return;
                }
                if (text[0] == '@' || whisper)
                {
                    string text4 = text;
                    if (text[0] == '@')
                    {
                        text4 = text.Remove(0, 1).Trim();
                    }
                    if (whisperTo == "")
                    {
                        int num2 = text4.IndexOf(' ');
                        if (num2 != -1)
                        {
                            string to = text4.Substring(0, num2);
                            string message3 = text4.Substring(num2 + 1);
                            HandleWhisper(to, message3);
                        }
                        else
                        {
                            SendMessage(rm.GetString("ChatNoMessageEntered"));
                        }
                    }
                    else
                    {
                        HandleWhisper(whisperTo, text4);
                    }
                    return;
                }
                if (text[0] == '#' || opchat)
                {
                    string text5 = text;
                    if (text[0] == '#')
                    {
                        text5 = text.Remove(0, 1).Trim();
                    }
                    GlobalMessageOps(string.Format(rm.GetString("ChatToOps"), color + name, text5));
                    if (group.Permission < Server.opchatperm && !Server.devs.Contains(name.ToLower()))
                    {
                        SendMessage(string.Format(rm.GetString("ChatToOps"), color + name, text5));
                    }
                    Server.s.Log("(OPs): " + name + ": " + text5);
                    IRCSay(name + ": " + text5, opchat: true);
                    return;
                }
                if (Server.voteMode)
                {
                    if (level == Server.LavaLevel)
                    {
                        if (LavaSystem.CountVotes(text.Trim(), this))
                        {
                            return;
                        }
                    }
                    else if (level == Server.InfectionLevel && InfectionUtils.CountVotes(text.Trim(), this))
                    {
                        return;
                    }
                }
                if (VotingSystem.votingInProgress)
                {
                    if (text.ToLower() == rm.GetString("VoteDecisionYesShortcut") || text.ToLower() == rm.GetString("VoteDecisionYes"))
                    {
                        votingChoice = VotingSystem.VotingChoice.Yes;
                        SendMessage(rm.GetString("VoteThanks"));
                        return;
                    }
                    if (text.ToLower() == rm.GetString("VoteDecisionNoShortcut") || text.ToLower() == rm.GetString("VoteDecisionNo"))
                    {
                        votingChoice = VotingSystem.VotingChoice.No;
                        SendMessage(rm.GetString("VoteThanks"));
                        return;
                    }
                }
                if (teamchat)
                {
                    if (team == null)
                    {
                        SendMessage(this, "You are not on a team.");
                        return;
                    }
                    {
                        foreach (Player player in team.players)
                        {
                            SendMessage(player, "(" + team.teamstring + ") " + color + PublicName + ":&f " + text);
                        }
                        return;
                    }
                }
                if (joker)
                {
                    if (File.Exists("text/joker.txt"))
                    {
                        Server.s.Log("<JOKER>: " + PublicName + ": " + text);
                        GlobalMessageOps(Server.DefaultColor + "<&aJ&bO&cK&5E&9R" + Server.DefaultColor + ">: " + color + PublicName + ":&f " + text);
                        FileInfo fileInfo = new FileInfo("text/joker.txt");
                        StreamReader streamReader = fileInfo.OpenText();
                        var list = new List<string>();
                        Random random = new Random();
                        int num3 = 0;
                        while (streamReader.Peek() != -1)
                        {
                            list.Add(streamReader.ReadLine());
                        }
                        num3 = random.Next(list.Count);
                        streamReader.Close();
                        streamReader.Dispose();
                        text = list[num3];
                    }
                    else
                    {
                        using (StreamWriter streamWriter = new StreamWriter(File.Create("text/joker.txt")))
                        {
                            streamWriter.WriteLine("'text/joker.txt' file is not set! You should probably fill it with funny lines.");
                        }
                    }
                }
                stopTheText = false;
                OnPlayerChatEvent(this, ref text, ref stopTheText);
                if (stopTheText)
                {
                    return;
                }
                if (level.mapType == MapType.Lava && !LavaSettings.All.LavaWorldChat)
                {
                    Server.s.Log("<" + name + ">[level] " + text);
                    GlobalChatLevel(this, text, showname: true);
                    return;
                }
                if (!level.worldChat)
                {
                    Server.s.Log("<" + name + ">[level] " + text);
                    GlobalChatLevel(this, text, showname: true);
                    return;
                }
                Server.s.Log("<" + name + "> " + text);
                if (Server.worldChat)
                {
                    GlobalChat(this, text);
                }
                else
                {
                    GlobalChatLevel(this, text, showname: true);
                }
                IRCSay(PublicName + ": " + text);
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        void CheckForCommandSpam(ref bool stopIt)
        {
            if (!ExtraData.ContainsKey("RecentCommandsTimes"))
            {
                ExtraData.Add("RecentCommandsTimes", new List<DateTime>());
                ((List<DateTime>)ExtraData["RecentCommandsTimes"]).Add(DateTime.Now);
                return;
            }
            var list = (List<DateTime>)ExtraData["RecentCommandsTimes"];
            list.Add(DateTime.Now);
            DateTime comparator = DateTime.Now.AddSeconds(-GeneralSettings.All.CooldownCmdMaxSeconds);
            var list2 = list.Where(t => t >= comparator).ToList();
            int num = list2.Count();
            if (num > GeneralSettings.All.CooldownCmdMax)
            {
                stopIt = true;
                SendMessage(this, GeneralSettings.All.CooldownCmdWarning);
            }
            else
            {
                ExtraData["RecentCommandsTimes"] = list2;
            }
        }

        public void HandleCommand(string cmd, string message)
        {
            try
            {
                if (cmd == "")
                {
                    SendMessage(rm.GetString("CommandNoEntered"));
                    return;
                }
                if (jailed)
                {
                    SendMessage(rm.GetString("CommandJailWarning"));
                    return;
                }
                if (GeneralSettings.All.CooldownCmdUse)
                {
                    bool stopIt = false;
                    CheckForCommandSpam(ref stopIt);
                    if (stopIt)
                    {
                        return;
                    }
                }
                if (cmd.ToLower() == "care")
                {
                    SendMessage("Corneria now loves you with all his heart.");
                    return;
                }
                try
                {
                    int num = int.Parse(cmd);
                    if (messageBind[num] == null)
                    {
                        SendMessage(string.Format(rm.GetString("CommandNoBind"), cmd));
                        return;
                    }
                    message = messageBind[num] + " " + message;
                    message = message.TrimEnd(' ');
                    cmd = cmdBind[num];
                }
                catch {}
                string commandLowercase = cmd.ToLower();
                IEnumerable<Command> source = Command.all.Where(c => c.name.ToLower() == commandLowercase || c.shortcut.ToLower() == commandLowercase);
                Command command = source.FirstOrDefault();
                Command command2 = source.FirstOrDefault(c => c.IsWithinScope(this));
                Command command3 = command2 != null ? command2 : command;
                if (command3 != null)
                {
                    if (lives < 1 && level.mapType == MapType.Lava && command3.name == "tp")
                    {
                        doCommand = true;
                    }
                    if (group.CanExecute(command3) || doCommand)
                    {
                        doCommand = false;
                        if (!command3.IsWithinScope(this))
                        {
                            SendMessage(this, string.Format("This command can be used only in {0} modes.", command3.Scope.ToString()));
                            return;
                        }
                        if (cmd != "repeat" && cmd != "m")
                        {
                            lastCMD = cmd + " " + message;
                        }
                        if (level.name.Contains("Museum " + Server.DefaultColor) && !command3.museumUsable)
                        {
                            SendMessage("Cannot use this command while in a museum!");
                            return;
                        }
                        if ((joker || muted) && cmd.ToLower() == "me")
                        {
                            SendMessage(string.Format(rm.GetString("CommandNoUseWhenMuted"), cmd.ToLower()));
                            return;
                        }
                        if (command3.HighSecurity)
                        {
                            Server.s.CommandUsed(string.Format("{0} used /{1} {2}", name, cmd, "***"));
                        }
                        else
                        {
                            Server.s.CommandUsed(string.Format("{0} used /{1} {2}", name, cmd, message));
                        }
                        commThread = new Thread((ThreadStart)delegate
                        {
                            try
                            {
                                command3.Use(this, message);
                            }
                            catch (Exception ex)
                            {
                                Server.ErrorLog(ex);
                                SendMessage(this, rm.GetString("ErrorCommand"));
                            }
                        });
                        commThread.Start();
                    }
                    else
                    {
                        SendMessage(string.Format(rm.GetString("CommandNotAllowedToUse"), cmd));
                    }
                    return;
                }
                if (Block.Byte(cmd.ToLower()) != byte.MaxValue)
                {
                    HandleCommand("mode", cmd.ToLower());
                    return;
                }
                bool flag = true;
                switch (cmd.ToLower())
                {
                    case "guest":
                        message = message + " " + cmd.ToLower();
                        cmd = "setrank";
                        break;
                    case "builder":
                        message = message + " " + cmd.ToLower();
                        cmd = "setrank";
                        break;
                    case "advbuilder":
                    case "adv":
                        message = message + " " + cmd.ToLower();
                        cmd = "setrank";
                        break;
                    case "operator":
                    case "op":
                        message = message + " " + cmd.ToLower();
                        cmd = "setrank";
                        break;
                    case "super":
                    case "superop":
                        message = message + " " + cmd.ToLower();
                        cmd = "setrank";
                        break;
                    case "cut":
                        cmd = "copy";
                        message = "cut";
                        break;
                    case "admins":
                        message = "superop";
                        cmd = "viewranks";
                        break;
                    case "ops":
                        message = "op";
                        cmd = "viewranks";
                        break;
                    case "banned":
                        message = cmd;
                        cmd = "viewranks";
                        break;
                    case "ps":
                        message = "ps " + message;
                        cmd = "map";
                        break;
                    case "item":
                    case "inventory":
                    case "inv":
                        cmd = "items";
                        break;
                    case "bhb":
                    case "hbox":
                        cmd = "cuboid";
                        message = "hollow";
                        break;
                    case "blb":
                    case "box":
                        cmd = "cuboid";
                        break;
                    case "sphere":
                        cmd = "spheroid";
                        break;
                    case "cmdlist":
                    case "commands":
                    case "cmdhelp":
                        cmd = "help";
                        break;
                    case "who":
                        cmd = "players";
                        break;
                    case "worlds":
                    case "maps":
                        cmd = "levels";
                        break;
                    case "mapsave":
                        cmd = "save";
                        break;
                    case "mapload":
                        cmd = "load";
                        break;
                    case "materials":
                        cmd = "blocks";
                        break;
                    default:
                        flag = false;
                        break;
                }
                if (flag)
                {
                    HandleCommand(cmd, message);
                }
                else
                {
                    SendMessage(string.Format(rm.GetString("CommandUnknown"), cmd));
                }
            }
            catch (Exception ex2)
            {
                Server.ErrorLog(ex2);
                SendMessage(rm.GetString("CommandFailed"));
            }
        }

        void HandleWhisper(string to, string message)
        {
            Player player = Find(to);
            if (player == this)
            {
                SendMessage(rm.GetString("CommandUsedOneself"));
            }
            else if (player != null && (!player.hidden || group.Permission >= LevelPermission.Operator))
            {
                ChatOtherEventArgs chatOtherEventArgs = new ChatOtherEventArgs(message, this, player, ChatType.Whisper);
                OnChatOther(chatOtherEventArgs);
                if (!chatOtherEventArgs.Handled)
                {
                    message = chatOtherEventArgs.Message;
                    Server.s.Log(name + " @" + player.name + ": " + message);
                    SendChat(this, Server.DefaultColor + "[<] " + player.color + player.prefix + player.PublicName + ": &f" + message);
                    SendChat(player, "&9[>] " + color + prefix + PublicName + ": &f" + message);
                }
            }
            else
            {
                SendMessage(string.Format(rm.GetString("CommandPlayerNonexistent"), to));
            }
        }

        public void SendRaw(int id)
        {
            SendRaw(id, new byte[0]);
        }

        public void SendCallback(IAsyncResult result)
        {
            try
            {
                Interlocked.Decrement(ref pendingPackets);
                Socket socket = (Socket)result.AsyncState;
                socket.EndSend(result);
            }
            catch {}
        }

        public void SendRaw(byte[] msg)
        {
            int num = 0;
            while (true)
            {
                try
                {
                    if (socket != null)
                    {
                        Interlocked.Increment(ref pendingPackets);
                        socket.BeginSend(msg, 0, msg.Length, SocketFlags.None, SendCallback, socket);
                        Server.packetSent++;
                    }
                    break;
                }
                catch (SocketException ex)
                {
                    num++;
                    if (ex.SocketErrorCode == SocketError.NoBufferSpaceAvailable)
                    {
                        if (LavaSettings.All.AutoServerLock)
                        {
                            Server.serverLockTime = DateTime.Now.AddSeconds(45.0);
                            Server.s.Log("'ERROR: NoBufferSpaceAvailable.' Server Lock for 45sec.");
                        }
                        else
                        {
                            Server.s.Log("'ERROR: NoBufferSpaceAvailable.'");
                        }
                        Disconnect();
                        break;
                    }
                    if (num > 3)
                    {
                        Disconnect();
                        break;
                    }
                }
            }
        }

        public void SendRaw(int id, byte[] send)
        {
            byte[] array = new byte[send.Length + 1];
            array[0] = (byte)id;
            Buffer.BlockCopy(send, 0, array, 1, send.Length);
            int num = 0;
            while (true)
            {
                try
                {
                    if (socket != null && socket.Connected)
                    {
                        Interlocked.Increment(ref pendingPackets);
                        socket.BeginSend(array, 0, array.Length, SocketFlags.None, SendCallback, socket);
                        Server.packetSent++;
                    }
                    break;
                }
                catch (SocketException ex)
                {
                    num++;
                    if (ex.SocketErrorCode == SocketError.NoBufferSpaceAvailable)
                    {
                        if (LavaSettings.All.AutoServerLock)
                        {
                            Server.serverLockTime = DateTime.Now.AddSeconds(45.0);
                            Server.s.Log("'ERROR: NoBufferSpaceAvailable.' Server Lock for 45sec.");
                        }
                        else
                        {
                            Server.s.Log("'ERROR: NoBufferSpaceAvailable.'");
                        }
                        Disconnect();
                        break;
                    }
                    if (num > 3)
                    {
                        Disconnect();
                        break;
                    }
                }
            }
        }

        public static void SendMessage(Player p, string message, params object[] objects)
        {
            SendMessage(p, 0, string.Format(message, objects));
        }

        public static void SendMessage(Player p, string message)
        {
            SendMessage(p, 0, message);
        }

        internal static void SendMessage(Player p, byte type, string message)
        {
            if (p == null)
            {
                if (storeHelp)
                {
                    storedHelp = storedHelp + message + "\r\n";
                    return;
                }
                Server.s.Log(message);
                IRCSay(message, opchat: true);
            }
            else
            {
                p.SendMessage(type, Server.DefaultColor + message);
            }
        }

        internal static void SendMessage(Player p, byte type, TimeSpan lifespan, string message)
        {
            if (p == null)
            {
                if (storeHelp)
                {
                    storedHelp = storedHelp + message + "\r\n";
                    return;
                }
                Server.s.Log(message);
                IRCSay(message, opchat: true);
                return;
            }
            p.SendMessage(type, Server.DefaultColor + message);
            if (type == 0)
            {
                return;
            }
            System.Threading.Timer timer = null;
            timer = new System.Threading.Timer(delegate
            {
                if (p.Cpe.MessageTypes == 1)
                {
                    SendMessage(p, type, "");
                }
                timer.Dispose();
            }, null, lifespan, TimeSpan.FromMilliseconds(-1.0));
        }

        public static void SendMessage2(Player p, string message)
        {
            if (p == null)
            {
                Server.s.Log(message);
                IRCSay(message, opchat: true);
            }
            else
            {
                p.SendMessage2(0, Server.DefaultColor + message);
            }
        }

        public void SendMessage(string message)
        {
            if (this == null)
            {
                Server.s.Log(message);
            }
            else
            {
                SendMessage(0, Server.DefaultColor + message);
            }
        }

        public void SendChat(Player p, string message)
        {
            if (this == null)
            {
                Server.s.Log(message);
            }
            else
            {
                SendMessage(p, message);
            }
        }

        public static void FilterMessageConsole(ref string message)
        {
            message = message.Replace("%s", Server.DefaultColor);
            for (int i = 0; i < 10; i++)
            {
                message = message.Replace("%" + i, "&" + i);
                message = message.Replace("&" + i + " &", " &");
                message = message.Replace("&" + i + "&", "&");
            }
            for (char c2 = 'a'; c2 <= 'f'; c2 = (char)(c2 + 1))
            {
                message = message.Replace("%" + c2, "&" + c2);
                message = message.Replace("&" + c2 + " &", " &");
                message = message.Replace("&" + c2 + "&", "&");
            }
        }

        public void FilterMessage(ref string message)
        {
            for (int i = 0; i < 10; i++)
            {
                message = message.Replace("%" + i, "&" + i);
                message = message.Replace("&" + i + " &", " &");
                message = message.Replace("&" + i + "&", "&");
            }
            for (char c2 = 'a'; c2 <= 'f'; c2 = (char)(c2 + 1))
            {
                message = message.Replace("%" + c2, "&" + c2);
                message = message.Replace("&" + c2 + " &", " &");
                message = message.Replace("&" + c2 + "&", "&");
            }
            message = message.Replace("%s", "&s");
            message = message.Replace("&s &", " &");
            message = message.Replace("&s&", "&");
            message = message.Replace("&s", Server.DefaultColor);
            if (LavaSettings.All.AllowInGameVariables)
            {
                foreach (KeyValuePair<string, string> keyValue in Server.constantsForChat.KeyValues)
                {
                    message = message.Replace("$" + keyValue.Key, keyValue.Value);
                }
                if (Server.useDollarSign)
                {
                    message = message.Replace("$name", "$" + PublicName);
                }
                else
                {
                    message = message.Replace("$name", PublicName);
                }
                message = message.Replace("$date", DateTime.Now.ToString("yyyy-MM-dd"));
                message = message.Replace("$time", DateTime.Now.ToString("HH:mm:ss"));
                message = message.Replace("$ip", ip);
                message = message.Replace("$color", color);
                message = message.Replace("$rank", group.name);
                message = message.Replace("$level", level.name);
                message = message.Replace("$deaths", overallDeath.ToString());
                message = message.Replace("$money", money.ToString());
                message = message.Replace("$blocks", overallBlocks.ToString());
                message = message.Replace("$first", firstLogin.ToString());
                message = message.Replace("$kicked", totalKicked.ToString());
                message = message.Replace("$server", Server.name);
                message = message.Replace("$motd", Server.motd);
                message = message.Replace("$irc", Server.ircServer + " > " + Server.ircChannel);
            }
            if (Server.parseSmiley && parseSmiley)
            {
                message = message.Replace(":)", "(darksmile)");
                message = message.Replace(":D", "(smile)");
                message = message.Replace("<3", "(heart)");
            }
            byte[] array = new byte[1]
            {
                0
            };
            message = message.Replace("(sign1)", enc.GetString(array));
            array[0] = 5;
            message = message.Replace("(sign2)", enc.GetString(array));
            array[0] = 6;
            message = message.Replace("(sign3)", enc.GetString(array));
            array[0] = 9;
            message = message.Replace("(sign4)", enc.GetString(array));
            array[0] = 10;
            message = message.Replace("(sign5)", enc.GetString(array));
            array[0] = 13;
            message = message.Replace("(sign6)", enc.GetString(array));
            array[0] = 14;
            message = message.Replace("(sign7)", enc.GetString(array));
            array[0] = 18;
            message = message.Replace("(sign8)", enc.GetString(array));
            array[0] = 20;
            message = message.Replace("(sign9)", enc.GetString(array));
            array[0] = 21;
            message = message.Replace("(sign10)", enc.GetString(array));
            array[0] = 23;
            message = message.Replace("(sign11)", enc.GetString(array));
            array[0] = 27;
            message = message.Replace("(sign12)", enc.GetString(array));
            array[0] = 28;
            message = message.Replace("(sign13)", enc.GetString(array));
            array[0] = 29;
            message = message.Replace("(sign14)", enc.GetString(array));
            array[0] = 1;
            message = message.Replace("(darksmile)", enc.GetString(array));
            array[0] = 2;
            message = message.Replace("(smile)", enc.GetString(array));
            array[0] = 3;
            message = message.Replace("(heart)", enc.GetString(array));
            array[0] = 4;
            message = message.Replace("(diamond)", enc.GetString(array));
            array[0] = 7;
            message = message.Replace("(bullet)", enc.GetString(array));
            array[0] = 8;
            message = message.Replace("(hole)", enc.GetString(array));
            array[0] = 11;
            message = message.Replace("(male)", enc.GetString(array));
            array[0] = 12;
            message = message.Replace("(female)", enc.GetString(array));
            array[0] = 15;
            message = message.Replace("(sun)", enc.GetString(array));
            array[0] = 16;
            message = message.Replace("(right)", enc.GetString(array));
            array[0] = 17;
            message = message.Replace("(left)", enc.GetString(array));
            array[0] = 19;
            message = message.Replace("(double)", enc.GetString(array));
            array[0] = 22;
            message = message.Replace("(half)", enc.GetString(array));
            array[0] = 24;
            message = message.Replace("(uparrow)", enc.GetString(array));
            array[0] = 25;
            message = message.Replace("(downarrow)", enc.GetString(array));
            array[0] = 26;
            message = message.Replace("(rightarrow)", enc.GetString(array));
            array[0] = 30;
            message = message.Replace("(up)", enc.GetString(array));
            array[0] = 31;
            message = message.Replace("(down)", enc.GetString(array));
        }

        public void SendMessage2(byte id, string message)
        {
            if (this == null)
            {
                Server.s.Log(message);
            }
            else
            {
                if (ZoneSpam.AddSeconds(2.0) > DateTime.Now && message.Contains("This zone belongs to "))
                {
                    return;
                }
                byte[] array = new byte[65];
                array[0] = id;
                FilterMessage(ref message);
                int num = 0;
                while (true)
                {
                    try
                    {
                        foreach (string item in Wordwrap2(message))
                        {
                            string text = item;
                            if (item[text.Length - 2] == '&' && "0123456789abcdef".IndexOf(text[text.Length - 1]) > 0)
                            {
                                text = text.Remove(text.Length - 2);
                            }
                            if (text.TrimEnd(' ')[text.TrimEnd(' ').Length - 1] < '!')
                            {
                                text += '\'';
                            }
                            StringFormat(text, 64).CopyTo(array, 1);
                            SendRaw(13, array);
                        }
                        break;
                    }
                    catch (Exception ex)
                    {
                        message = "&f" + message;
                        num++;
                        if (num < 10)
                        {
                            continue;
                        }
                        Server.ErrorLog(ex);
                        break;
                    }
                }
            }
        }

        public void SendMessage(byte type, string message)
        {
            if (this == null)
            {
                Server.s.Log(message);
            }
            else
            {
                if (ZoneSpam.AddSeconds(2.0) > DateTime.Now && message.Contains("This zone belongs to "))
                {
                    return;
                }
                byte[] array = new byte[65];
                array[0] = type;
                FilterMessage(ref message);
                int num = 0;
                while (true)
                {
                    try
                    {
                        if (type != 0 && message.Replace(Server.DefaultColor, "") == "")
                        {
                            StringFormat("", 64).CopyTo(array, 1);
                            SendRaw(13, array);
                            break;
                        }
                        foreach (string item in Wordwrap(message))
                        {
                            string text = item.TrimEnd();
                            while (text.Length >= 2 && text[text.Length - 2] == '&' && "0123456789abcdef".IndexOf(text[text.Length - 1]) > 0)
                            {
                                text = text.Remove(text.Length - 2);
                                text = text.Trim();
                            }
                            if (text.Length >= 1 && text[text.Length - 1] < '!')
                            {
                                text += '\'';
                            }
                            StringFormat(text, 64).CopyTo(array, 1);
                            SendRaw(13, array);
                        }
                        break;
                    }
                    catch (Exception ex)
                    {
                        message = "&f" + message;
                        num++;
                        if (num < 10)
                        {
                            continue;
                        }
                        Server.ErrorLog(ex);
                        break;
                    }
                }
            }
        }

        public void SendMotd()
        {
            byte[] array = new byte[130];
            array[0] = 8;
            StringFormat(Server.name, 64).CopyTo(array, 1);
            StringFormat(Server.motd, 64).CopyTo(array, 65);
            if (group.Permission >= LevelPermission.Operator)
            {
                array[129] = 100;
            }
            else
            {
                array[129] = 0;
            }
            SendRaw(0, array);
        }

        public void RemoveTextures()
        {
            string text = Server.motd.Split(new string[1]
            {
                "cfg="
            }, 2, StringSplitOptions.None)[0];
            byte[] array = new byte[130];
            array[0] = 8;
            StringFormat(Server.name, 64).CopyTo(array, 1);
            StringFormat(text, 64).CopyTo(array, 65);
            if (Block.canPlace(this, 7))
            {
                array[129] = 100;
            }
            else
            {
                array[129] = 0;
            }
            SendRaw(0, array);
            Server.s.Log(text);
            isUsingTextures = false;
        }

        public void SendTextures()
        {
            byte[] array = new byte[130];
            array[0] = 8;
            StringFormat(Server.name, 64).CopyTo(array, 1);
            StringFormat(Server.motd, 64).CopyTo(array, 65);
            if (Block.canPlace(this, 7))
            {
                array[129] = 100;
            }
            else
            {
                array[129] = 0;
            }
            SendRaw(0, array);
            isUsingTextures = true;
        }

        public void SendUserMOTD(bool home = false)
        {
            byte[] array = new byte[130];
            array[0] = 7;
            if (home)
            {
                StringFormat("Welcome Home! +hax", 128).CopyTo(array, 1);
            }
            else if (level.motd == "ignore")
            {
                StringFormat(Server.name, 64).CopyTo(array, 1);
                StringFormat(Server.motd, 64).CopyTo(array, 65);
            }
            else
            {
                StringFormat(level.motd, 128).CopyTo(array, 1);
            }
            if (group.Permission >= LevelPermission.Operator)
            {
                array[129] = 100;
            }
            else
            {
                array[129] = 0;
            }
            SendRaw(0, array);
        }

        public void SendMap()
        {
            mapLoading = true;
            sendLock = true;
            SendRaw(2, new byte[0]);
            byte[] array = new byte[level.blocks.Length + 4];
            BitConverter.GetBytes(IPAddress.HostToNetworkOrder(level.blocks.Length)).CopyTo(array, 0);
            for (int i = 0; i < level.blocks.Length; i++)
            {
                if (IsCpeSupported)
                {
                    if (Cpe.Blockmension == 1)
                    {
                        array[4 + i] = Block.Convert(level.blocks[i]);
                    }
                    else
                    {
                        array[4 + i] = Block.ConvertFromBlockmension(Block.Convert(level.blocks[i]));
                    }
                }
                else
                {
                    array[4 + i] = Block.ConvertExtended(Block.ConvertFromBlockmension(Block.Convert(level.blocks[i])));
                }
            }
            array = GZip(array);
            level.Weight = array.Length;
            int num = (int)Math.Ceiling(array.Length / 1024.0);
            int num2 = 1;
            while (array.Length > 0)
            {
                short num3 = (short)Math.Min(array.Length, 1024);
                byte[] array2 = new byte[1027];
                HTNO(num3).CopyTo(array2, 0);
                Buffer.BlockCopy(array, 0, array2, 2, num3);
                byte[] array3 = new byte[array.Length - num3];
                Buffer.BlockCopy(array, num3, array3, 0, array.Length - num3);
                array = array3;
                array2[1026] = (byte)(num2 * 100 / num);
                SendRaw(3, array2);
                if (Server.updateTimer.Interval > 1000.0 && ip != "127.0.0.1")
                {
                    Thread.Sleep(100);
                }
                else if (ip != "127.0.0.1")
                {
                    Thread.Sleep(1);
                }
                num2++;
            }
            array = new byte[6];
            HTNO((short)level.width).CopyTo(array, 0);
            HTNO((short)level.height).CopyTo(array, 2);
            HTNO((short)level.depth).CopyTo(array, 4);
            omitHackDetection = true;
            SendRaw(4, array);
            mapLoadedTime = DateTime.Now;
            mapLoading = false;
            sendLock = false;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            string environment = level.Info.Environment;
            if (environment != null)
            {
                EnvironmentHandler environmentHandler = new EnvironmentHandler();
                Environment environment2 = environmentHandler.Parse(environment);
                if (environment2 != null)
                {
                    environmentHandler.SendToPlayer(this, environment2);
                }
            }
            string weather = level.Info.Weather;
            if (weather != null)
            {
                WeatherHandler weatherHandler = new WeatherHandler();
                int weather2 = weatherHandler.Parse(weather);
                if (weather != null)
                {
                    weatherHandler.SendToPlayer(this, weather2);
                }
            }
            string texture = level.Info.Texture;
            if (texture != null)
            {
                TextureHandler textureHandler = new TextureHandler();
                Texture texture2 = textureHandler.Parse(texture);
                if (weather != null)
                {
                    textureHandler.SendToPlayer(this, texture2);
                }
            }
            else
            {
                short sideLevel = (short)(level.height / 2);
                if (Cpe.EnvMapAppearance == 1)
                {
                    V1.EnvSetMapAppearance(this, "", byte.MaxValue, byte.MaxValue, sideLevel);
                }
            }
            level.OnPlayerJoined(level, new PlayerJoinedEventArgs(this));
        }

        public void SendSpawn(Player p, string name)
        {
            SendSpawn(p.id, name, p.ModelName, p.pos[0], p.pos[1], p.pos[2], p.rot[0], p.rot[1]);
        }

        public void SendSpawn(byte id, string name, ushort x, ushort y, ushort z, byte rotx, byte roty)
        {
            SendSpawn(id, name, null, x, y, z, rotx, roty);
        }

        public void SendSpawn(byte id, string name, string modelName, ushort x, ushort y, ushort z, byte rotx, byte roty)
        {
            byte[] array = new byte[73];
            array[0] = id;
            StringFormat(name, 64).CopyTo(array, 1);
            HTNO(x).CopyTo(array, 65);
            HTNO(y).CopyTo(array, 67);
            HTNO(z).CopyTo(array, 69);
            array[71] = rotx;
            array[72] = roty;
            if (id == this.id || id == byte.MaxValue)
            {
                lock (hacksDetectionSync)
                {
                    oldx = x;
                    oldy = y;
                    oldz = z;
                    pos[0] = x;
                    pos[1] = y;
                    pos[2] = z;
                    rot[0] = rotx;
                    rot[1] = roty;
                }
            }
            omitHackDetection = true;
            SendRaw(7, array);
            if (IsCpeSupported && modelName != null)
            {
                SendRaw(new Packets().MakeChangeModel(id, modelName));
            }
        }

        public void SendPos(byte id, ushort x, ushort y, ushort z, byte rotx, byte roty)
        {
            if (x < 0)
            {
                x = 32;
            }
            if (y < 0)
            {
                y = 32;
            }
            if (z < 0)
            {
                z = 32;
            }
            if (x > level.width * 32)
            {
                x = (ushort)(level.width * 32 - 32);
            }
            if (z > level.depth * 32)
            {
                z = (ushort)(level.depth * 32 - 32);
            }
            if (x > 32767)
            {
                x = 32730;
            }
            if (y > 32767)
            {
                y = 32730;
            }
            if (z > 32767)
            {
                z = 32730;
            }
            if (id > 127)
            {
                lock (hacksDetectionSync)
                {
                    oldx = x;
                    oldy = y;
                    oldz = z;
                    pos[0] = x;
                    pos[1] = y;
                    pos[2] = z;
                    rot[0] = rotx;
                    rot[1] = roty;
                }
            }
            byte[] array = new byte[9]
            {
                id, 0, 0, 0, 0, 0, 0, 0, 0
            };
            HTNO(x).CopyTo(array, 1);
            HTNO(y).CopyTo(array, 3);
            HTNO(z).CopyTo(array, 5);
            array[7] = rotx;
            array[8] = roty;
            omitHackDetection = true;
            SendRaw(8, array);
        }

        public void SendDie(byte id)
        {
            SendRaw(12, new byte[1]
            {
                id
            });
        }

        public void SendCurrentMapTile(ushort x, ushort y, ushort z)
        {
            SendBlockchange(x, y, z, level.GetTile(x, y, z));
        }

        public void SendBlockchange(ushort x, ushort y, ushort z, byte type)
        {
            if (x >= 0 && y >= 0 && z >= 0 && x < level.width && y < level.height && z < level.depth)
            {
                byte[] array = new byte[7];
                HTNO(x).CopyTo(array, 0);
                HTNO(y).CopyTo(array, 2);
                HTNO(z).CopyTo(array, 4);
                byte b = Block.Convert(type);
                if (!IsCpeSupported)
                {
                    b = Block.ConvertExtended(Block.ConvertFromBlockmension(b));
                }
                else if (Cpe.Blockmension < 1)
                {
                    b = Block.ConvertFromBlockmension(b);
                }
                array[6] = b;
                SendRaw(6, array);
            }
        }

        void SendKick(string message)
        {
            SendRaw(14, StringFormat(message, 64));
        }

        internal void SendPing()
        {
            SendRaw(1);
        }

        void UpdatePosition()
        {
            byte b = 0;
            if (oldpos[0] != pos[0] || oldpos[1] != pos[1] || oldpos[2] != pos[2])
            {
                b |= 1;
            }
            if (oldrot[0] != rot[0] || oldrot[1] != rot[1])
            {
                b |= 2;
            }
            if (Math.Abs(pos[0] - basepos[0]) > 32 || Math.Abs(pos[1] - basepos[1]) > 32 || Math.Abs(pos[2] - basepos[2]) > 32)
            {
                b |= 4;
            }
            if (oldpos[0] == pos[0] && oldpos[1] == pos[1] && oldpos[2] == pos[2] && (basepos[0] != pos[0] || basepos[1] != pos[1] || basepos[2] != pos[2]))
            {
                b |= 4;
            }
            byte[] buffer = new byte[0];
            byte msg = 0;
            if ((b & 4) != 0)
            {
                msg = 8;
                buffer = new byte[9];
                buffer[0] = id;
                HTNO(pos[0]).CopyTo(buffer, 1);
                HTNO(pos[1]).CopyTo(buffer, 3);
                HTNO(pos[2]).CopyTo(buffer, 5);
                buffer[7] = rot[0];
                if (flipHead)
                {
                    if (rot[1] > 64 && rot[1] < 192)
                    {
                        buffer[8] = rot[1];
                    }
                    else
                    {
                        buffer[8] = (byte)(rot[1] - (rot[1] - 128));
                    }
                }
                else
                {
                    buffer[8] = rot[1];
                }
            }
            else
            {
                switch (b)
                {
                    case 1:
                        try
                        {
                            msg = 10;
                            buffer = new byte[4];
                            buffer[0] = id;
                            Buffer.BlockCopy(BitConverter.GetBytes((sbyte)(pos[0] - oldpos[0])), 0, buffer, 1, 1);
                            Buffer.BlockCopy(BitConverter.GetBytes((sbyte)(pos[1] - oldpos[1])), 0, buffer, 2, 1);
                            Buffer.BlockCopy(BitConverter.GetBytes((sbyte)(pos[2] - oldpos[2])), 0, buffer, 3, 1);
                        }
                        catch {}
                        break;
                    case 2:
                        msg = 11;
                        buffer = new byte[3];
                        buffer[0] = id;
                        buffer[1] = rot[0];
                        if (flipHead)
                        {
                            if (rot[1] > 64 && rot[1] < 192)
                            {
                                buffer[2] = rot[1];
                            }
                            else
                            {
                                buffer[2] = (byte)(rot[1] - (rot[1] - 128));
                            }
                        }
                        else
                        {
                            buffer[2] = rot[1];
                        }
                        break;
                    case 3:
                        try
                        {
                            msg = 9;
                            buffer = new byte[6];
                            buffer[0] = id;
                            Buffer.BlockCopy(BitConverter.GetBytes((sbyte)(pos[0] - oldpos[0])), 0, buffer, 1, 1);
                            Buffer.BlockCopy(BitConverter.GetBytes((sbyte)(pos[1] - oldpos[1])), 0, buffer, 2, 1);
                            Buffer.BlockCopy(BitConverter.GetBytes((sbyte)(pos[2] - oldpos[2])), 0, buffer, 3, 1);
                            buffer[4] = rot[0];
                            if (flipHead)
                            {
                                if (rot[1] > 64 && rot[1] < 192)
                                {
                                    buffer[5] = rot[1];
                                }
                                else
                                {
                                    buffer[5] = (byte)(rot[1] - (rot[1] - 128));
                                }
                            }
                            else
                            {
                                buffer[5] = rot[1];
                            }
                        }
                        catch {}
                        break;
                }
            }
            oldpos = pos;
            oldrot = rot;
            if (b == 0)
            {
                return;
            }
            try
            {
                players.ForEachSync(delegate(Player p)
                {
                    if (p != this && p.level == level && !p.Loading)
                    {
                        p.SendRaw(msg, buffer);
                    }
                });
            }
            catch {}
        }

        void UpdatePositionP()
        {
            byte b = 0;
            if (oldpos[0] != pos[0] || oldpos[1] != pos[1] || oldpos[2] != pos[2])
            {
                b |= 1;
            }
            if (oldrot[0] != rot[0] || oldrot[1] != rot[1])
            {
                b |= 2;
            }
            if (Math.Abs(pos[0] - basepos[0]) > 32 || Math.Abs(pos[1] - basepos[1]) > 32 || Math.Abs(pos[2] - basepos[2]) > 32)
            {
                b |= 4;
            }
            if (oldpos[0] == pos[0] && oldpos[1] == pos[1] && oldpos[2] == pos[2] && (basepos[0] != pos[0] || basepos[1] != pos[1] || basepos[2] != pos[2]))
            {
                b |= 4;
            }
            byte[] array = new byte[0];
            byte b2 = 0;
            if ((b & 4) != 0)
            {
                b2 = 8;
                array = new byte[9]
                {
                    id, 0, 0, 0, 0, 0, 0, 0, 0
                };
                HTNO(pos[0]).CopyTo(array, 1);
                HTNO(pos[1]).CopyTo(array, 3);
                HTNO(pos[2]).CopyTo(array, 5);
                array[7] = rot[0];
                if (flipHead)
                {
                    if (rot[1] > 64 && rot[1] < 192)
                    {
                        array[8] = rot[1];
                    }
                    else
                    {
                        array[8] = (byte)(rot[1] - (rot[1] - 128));
                    }
                }
                else
                {
                    array[8] = rot[1];
                }
            }
            else
            {
                switch (b)
                {
                    case 1:
                        try
                        {
                            b2 = 10;
                            array = new byte[4]
                            {
                                id, 0, 0, 0
                            };
                            Buffer.BlockCopy(BitConverter.GetBytes((sbyte)(pos[0] - oldpos[0])), 0, array, 1, 1);
                            Buffer.BlockCopy(BitConverter.GetBytes((sbyte)(pos[1] - oldpos[1])), 0, array, 2, 1);
                            Buffer.BlockCopy(BitConverter.GetBytes((sbyte)(pos[2] - oldpos[2])), 0, array, 3, 1);
                        }
                        catch {}
                        break;
                    case 2:
                        b2 = 11;
                        array = new byte[3]
                        {
                            id, rot[0], 0
                        };
                        if (flipHead)
                        {
                            if (rot[1] > 64 && rot[1] < 192)
                            {
                                array[2] = rot[1];
                            }
                            else
                            {
                                array[2] = (byte)(rot[1] - (rot[1] - 128));
                            }
                        }
                        else
                        {
                            array[2] = rot[1];
                        }
                        break;
                    case 3:
                        try
                        {
                            b2 = 9;
                            array = new byte[6]
                            {
                                id, 0, 0, 0, 0, 0
                            };
                            Buffer.BlockCopy(BitConverter.GetBytes((sbyte)(pos[0] - oldpos[0])), 0, array, 1, 1);
                            Buffer.BlockCopy(BitConverter.GetBytes((sbyte)(pos[1] - oldpos[1])), 0, array, 2, 1);
                            Buffer.BlockCopy(BitConverter.GetBytes((sbyte)(pos[2] - oldpos[2])), 0, array, 3, 1);
                            array[4] = rot[0];
                            if (flipHead)
                            {
                                if (rot[1] > 64 && rot[1] < 192)
                                {
                                    array[5] = rot[1];
                                }
                                else
                                {
                                    array[5] = (byte)(rot[1] - (rot[1] - 128));
                                }
                            }
                            else
                            {
                                array[5] = rot[1];
                            }
                        }
                        catch {}
                        break;
                }
            }
            oldpos = pos;
            oldrot = rot;
            if (b != 0)
            {
                posBuffer = new byte[array.Length + 1];
                posBuffer[0] = b2;
                Buffer.BlockCopy(array, 0, posBuffer, 1, array.Length);
            }
            else
            {
                posBuffer = null;
            }
        }

        public void DiePlayers()
        {
            for (byte b = 0; b < 128; b++)
            {
                SendDie(b);
            }
        }

        public void DieBots()
        {
            PlayerBot.playerbots.ForEach(delegate(PlayerBot b)
            {
                if (level == b.level)
                {
                    SendDie(b.id);
                }
            });
        }

        public void SpawnPlayers()
        {
            players.ForEachSync(delegate(Player pl)
            {
                if (pl.level == level && this != pl && !pl.hidden)
                {
                    SendSpawn(pl.id, pl.color + (pl.IsRefree ? "[REF]" : "") + pl.PublicName, pl.ModelName, pl.pos[0], pl.pos[1], pl.pos[2], pl.rot[0], pl.rot[1]);
                }
            });
        }

        public void SpawnBots()
        {
            PlayerBot.playerbots.ForEach(delegate(PlayerBot b)
            {
                if (b.level == level)
                {
                    SendSpawn(b.id, b.color + b.name, b.pos[0], b.pos[1], b.pos[2], b.rot[0], b.rot[1]);
                }
            });
        }

        public static void GlobalBlockchange(Level level, ushort x, ushort y, ushort z, byte type)
        {
            try
            {
                players.ForEachSync(delegate(Player p)
                {
                    if (p.level == level)
                    {
                        p.SendBlockchange(x, y, z, type);
                    }
                });
            }
            catch {}
        }

        public static void GlobalChat(Player from, string message)
        {
            GlobalChat(from, message, showname: true);
        }

        public static void GlobalChat(Player from, string message, bool showname)
        {
            if (showname)
            {
                StringBuilder stringBuilder = new StringBuilder();
                message = stringBuilder.Append(from.IronChallengeTag).Append(from.color).Append(from.voicestring)
                    .Append(from.Tag)
                    .Append(from.color)
                    .Append(from.prefix)
                    .Append(from.Tier)
                    .Append(from.StarsTag)
                    .Append(" ")
                    .Append(from.color)
                    .Append(from.PublicName)
                    .Append(": ")
                    .Replace("  ", " ")
                    .Append(MCColor.White)
                    .Append(message)
                    .ToString();
            }
            players.ForEachSync(delegate(Player p)
            {
                ChatOtherEventArgs chatOtherEventArgs = new ChatOtherEventArgs(message, from, p, ChatType.CrossMaps);
                OnChatOther(chatOtherEventArgs);
                if (!chatOtherEventArgs.Handled)
                {
                    message = chatOtherEventArgs.Message;
                    if (p.level.worldChat)
                    {
                        SendMessage(p, message);
                    }
                }
            });
            if (!Server.CLI)
            {
                Window.thisWindow.UpdateChat(message);
            }
        }

        public static void GlobalChatLevel(Player from, string message, bool showname)
        {
            if (showname)
            {
                StringBuilder stringBuilder = new StringBuilder();
                message = stringBuilder.Append(from.IronChallengeTag).Append(from.color).Append(from.voicestring)
                    .Append(from.Tag)
                    .Append(from.color)
                    .Append(from.prefix)
                    .Append(from.Tier)
                    .Append(from.StarsTag)
                    .Append(" ")
                    .Append(from.color)
                    .Append(from.PublicName)
                    .Append(": ")
                    .Append(MCColor.White)
                    .Append(message)
                    .ToString();
            }
            players.ForEachSync(delegate(Player p)
            {
                ChatOtherEventArgs chatOtherEventArgs = new ChatOtherEventArgs(message, from, p, ChatType.Map);
                OnChatOther(chatOtherEventArgs);
                if (!chatOtherEventArgs.Handled)
                {
                    message = chatOtherEventArgs.Message;
                    if (p.level == from.level)
                    {
                        SendMessage(p, Server.DefaultColor + message);
                    }
                }
            });
            if (!Server.CLI)
            {
                Window.thisWindow.UpdateChat(message);
            }
        }

        public static void GlobalChatWorld(Player from, string message, bool showname)
        {
            if (showname)
            {
                message = rm.GetString("GlobalChatWorldTag") + from.color + from.voicestring + from.color + from.prefix + from.PublicName + ": &f" + message;
            }
            try
            {
                players.ForEachSync(delegate(Player p)
                {
                    if (p.level.worldChat)
                    {
                        SendMessage(p, message);
                    }
                });
                if (!Server.CLI)
                {
                    Window.thisWindow.UpdateChat(message);
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        public static void GlobalMessage(string message, params string[] objects)
        {
            GlobalMessage(string.Format(message, objects));
        }

        public static void GlobalMessage(string message)
        {
            try
            {
                players.ForEachSync(delegate(Player p)
                {
                    if (p.level.worldChat)
                    {
                        SendMessage(p, message);
                    }
                });
            }
            catch {}
            if (!Server.CLI)
            {
                Window.thisWindow.UpdateChat(Server.DefaultColor + message);
            }
        }

        public static void GlobalMessageLevel(Level l, string message)
        {
            try
            {
                players.ForEachSync(delegate(Player p)
                {
                    if (p.level == l)
                    {
                        SendMessage(p, message);
                    }
                });
            }
            catch {}
            if (!Server.CLI)
            {
                Window.thisWindow.UpdateChat("{" + l.name + "} " + Server.DefaultColor + message);
            }
        }

        public static void GlobalMessageOps(string message)
        {
            try
            {
                players.ForEachSync(delegate(Player p)
                {
                    if (p.group.Permission >= Server.opchatperm || Server.devs.Contains(p.name.ToLower()))
                    {
                        SendMessage(p, message);
                    }
                });
                if (!Server.CLI)
                {
                    Window.thisWindow.UpdateChat("#OPs: " + message);
                }
            }
            catch
            {
                Server.s.Log("Error occured with Op Chat");
            }
        }

        public static void GlobalSpawn(Player from)
        {
            GlobalSpawn(from, from.pos[0], from.pos[1], from.pos[2], from.rot[0], from.rot[1], self: false);
        }

        public static void GlobalSpawn(Player from, ushort x, ushort y, ushort z, byte rotx, byte roty, bool self)
        {
            GlobalSpawn(from, x, y, z, rotx, roty, self, "");
        }

        public static void GlobalSpawn(Player from, ushort x, ushort y, ushort z, byte rotx, byte roty, bool self, string possession)
        {
            players.ForEachSync(delegate(Player p)
            {
                try
                {
                    if (!p.disconnected && p.socket != null && (!p.Loading || p == from) && p.level == from.level && (!from.hidden || self))
                    {
                        if (p != from)
                        {
                            p.SendSpawn(from.id, from.color + (from.IsRefree ? "[REF]" : "") + from.PublicName + possession, from.ModelName, x, y, z, rotx, roty);
                        }
                        else if (self)
                        {
                            p.SendSpawn(byte.MaxValue, from.color + (from.IsRefree ? "[REF]" : "") + from.PublicName + possession, from.ModelName, x, y, z, rotx, roty);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Server.ErrorLog(ex);
                }
            });
        }

        public void GlobalSpawn()
        {
            GlobalSpawnPlain(PublicName);
        }

        public void GlobalSpawn(string alias)
        {
            GlobalSpawnPlain("&c" + alias);
        }

        public void GlobalSpawnPlain(string alias)
        {
            players.ForEachSync(delegate(Player p)
            {
                if (!p.disconnected && p.socket != null && !p.Loading && p.level == level && !hidden && p != this)
                {
                    p.SendSpawn(id, alias, ModelName, pos[0], pos[1], pos[2], rot[0], rot[1]);
                }
            });
        }

        public static void GlobalDie(Player from, bool self)
        {
            players.ForEachSync(delegate(Player p)
            {
                if (p.level == from.level && (!from.hidden || self) && !p.Loading)
                {
                    if (p != from)
                    {
                        p.SendDie(from.id);
                    }
                    else if (self)
                    {
                        p.SendDie(byte.MaxValue);
                    }
                }
            });
        }

        public void DespawnAll()
        {
            players.ForEachSync(delegate(Player p)
            {
                if (p.level == level)
                {
                    SendDie(p.id);
                }
            });
        }

        public bool MarkPossessed(string marker = "")
        {
            if (marker != "")
            {
                Player player = Find(marker);
                if (player == null)
                {
                    return false;
                }
                marker = " (" + player.color + player.PublicName + color + ")";
            }
            GlobalDie(this, self: true);
            GlobalSpawn(this, pos[0], pos[1], pos[2], rot[0], rot[1], true, marker);
            return true;
        }

        public static void GlobalUpdateOld()
        {
            players.ForEach(delegate(Player p)
            {
                try
                {
                    if (!p.hidden)
                    {
                        p.UpdatePosition();
                    }
                }
                catch {}
            });
        }

        public static void GlobalUpdate()
        {
            players.ForEach(delegate(Player p)
            {
                try
                {
                    if (!p.hidden)
                    {
                        p.UpdatePositionP();
                    }
                }
                catch {}
            });
            players.ForEach(delegate(Player p1)
            {
                var bList = new List<byte[]>();
                int totalLength = 0;
                players.ForEach(delegate(Player p2)
                {
                    if (p2.posBuffer != null && p1 != p2)
                    {
                        bList.Add(p2.posBuffer);
                        totalLength += p2.posBuffer.Length;
                    }
                });
                byte[] toSend = new byte[totalLength];
                int iterator = 0;
                bList.ForEach(delegate(byte[] array)
                {
                    Buffer.BlockCopy(array, 0, toSend, iterator, array.Length);
                    iterator += array.Length;
                });
                p1.SendRaw(toSend);
            });
        }

        byte[] Combine(params byte[][] arrays)
        {
            byte[] array = new byte[arrays.Sum(a => a.Length)];
            int num = 0;
            foreach (byte[] array2 in arrays)
            {
                Buffer.BlockCopy(array2, 0, array, num, array2.Length);
                num += array2.Length;
            }
            return array;
        }

        public void Disconnect()
        {
            leftGame();
        }

        string ConvertColorCodes(string message)
        {
            return Regex.Replace(message, "%([0-9a-f])", "&$1");
        }

        public void Kick(string kickString)
        {
            leftGame(ConvertColorCodes(kickString));
        }

        public void leftGame(string kickString = "", bool skip = false)
        {
            try
            {
                players.Remove(this);
                Server.s.PlayerListUpdate();
                if (disconnected)
                {
                    if (socket != null)
                    {
                        socket.Close();
                        socket = null;
                    }
                    return;
                }
                disconnected = true;
                afkCount = 0;
                afkStart = DateTime.Now;
                if (Server.afkset.Contains(name))
                {
                    Server.afkset.Remove(name);
                }
                if (kickString == "")
                {
                    kickString = rm.GetString("KickDisconnected");
                }
                SendKick(kickString);
                if (loggedIn)
                {
                    isFlying = false;
                    aiming = false;
                    if (team != null)
                    {
                        team.RemoveMember(this);
                    }
                    GlobalDie(this, self: false);
                    if (kickString == rm.GetString("KickDisconnected") || kickString.IndexOf("Server shutdown") != -1 || kickString == Server.customShutdownMessage)
                    {
                        if (!hidden)
                        {
                            if (farewellMessage == "")
                            {
                                GlobalChat(
                                    this,
                                    string.Format(MessagesManager.GetString("GlobalMessagePlayerDisconnected"),
                                                  "&8- " + color + prefix + PublicName + Server.DefaultColor), showname: false);
                            }
                            else
                            {
                                GlobalChat(this, "&8- " + color + prefix + PublicName + Server.DefaultColor + " " + farewellMessage, showname: false);
                            }
                        }
                        IRCSay(string.Format(rm.GetString("GlobalMessageLeftGame"), PublicName));
                        Server.s.Log(name + " disconnected.");
                    }
                    else
                    {
                        totalKicked++;
                        GlobalChat(this, string.Format(rm.GetString("GlobalMessageKicked"), color + prefix + PublicName + Server.DefaultColor, kickString),
                                   showname: false);
                        IRCSay(string.Format(rm.GetString("IrcMessageKicked"), PublicName, kickString));
                        Server.s.Log(name + " kicked (" + kickString + ").");
                    }
                    try
                    {
                        Save();
                    }
                    catch (Exception ex)
                    {
                        Server.ErrorLog(ex);
                    }
                    OnPlayerDisconnected(null, new PlayerEventArgs(this));
                    Server.s.PlayerListUpdate();
                    lock (playersThatLeftLocker)
                    {
                        if (!playersThatLeft.ContainsKey(name.ToLower()))
                        {
                            playersThatLeft.Add(name.ToLower(), ip);
                        }
                    }
                    if (Server.AutoLoad && level.unload)
                    {
                        bool unload = true;
                        try
                        {
                            players.ForEach(delegate(Player pl)
                            {
                                if (pl.level == level)
                                {
                                    unload = false;
                                }
                            });
                        }
                        catch {}
                        if (unload && !level.name.Contains("Museum " + Server.DefaultColor))
                        {
                            level.Unload();
                        }
                    }
                    SaveUndoToNewFile();
                    return;
                }
                if (socket != null)
                {
                    socket.Close();
                    socket = null;
                }
                if (disconnectionReason == DisconnectionReason.Unknown)
                {
                    Server.s.Log(ip + " disconnected.");
                    return;
                }
                string text = "error, please report.";
                switch (disconnectionReason)
                {
                    case DisconnectionReason.TempBan:
                        text = "temp-ban.";
                        break;
                    case DisconnectionReason.NameBan:
                        text = "name-ban.";
                        break;
                    case DisconnectionReason.IPBan:
                        text = "ip-ban.";
                        break;
                    case DisconnectionReason.ServerFull:
                        text = "server is full.";
                        break;
                    case DisconnectionReason.Kicked:
                        text = "kick.";
                        break;
                    case DisconnectionReason.AutoKicked:
                        text = "auto-kick.";
                        break;
                    case DisconnectionReason.AuthenticationFailure:
                        text = "authentication failure.";
                        break;
                    case DisconnectionReason.IllegalName:
                        text = "illegal name.";
                        break;
                }
                Server.s.Log(ip + " disconnected, name: " + name + ", reason: " + text);
            }
            catch (Exception ex2)
            {
                Server.ErrorLog(ex2);
            }
        }

        internal void SaveUndoToNewFile()
        {
            try
            {
                if (UndoBuffer.Count == 0)
                {
                    return;
                }
                string text = "extra/undo";
                string text2 = "extra/undoPrevious";
                if (!Directory.Exists(text))
                {
                    Directory.CreateDirectory(text);
                }
                if (!Directory.Exists(text2))
                {
                    Directory.CreateDirectory(text2);
                }
                DirectoryInfo directoryInfo = new DirectoryInfo(text);
                if (directoryInfo.GetDirectories("*").Length >= Server.totalUndo)
                {
                    Directory.Delete(text2, recursive: true);
                    Directory.Move(text, text2);
                    Directory.CreateDirectory(text);
                }
                string text3 = text + "/" + name.ToLower();
                if (!Directory.Exists(text3))
                {
                    Directory.CreateDirectory(text3);
                }
                directoryInfo = new DirectoryInfo(text3);
                StreamWriter w = new StreamWriter(File.Create(text3 + "/" + directoryInfo.GetFiles("*.undo").Length + ".undo"));
                try
                {
                    UndoBuffer.ForEach(delegate(UndoPos uP)
                    {
                        w.Write(string.Format("{0} {1} {2} {3} {4} {5} {6} ", uP.mapName, uP.x, uP.y, uP.z, uP.timePlaced.ToString().Replace(' ', '&'), uP.type,
                                              uP.newtype));
                    });
                }
                finally
                {
                    if (w != null)
                    {
                        ((IDisposable)w).Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
            finally
            {
                UndoBuffer.Clear();
            }
        }

        public static void SendToInbox(string from, string to, string message)
        {
            DBInterface.ExecuteQuery(string.Format("CREATE TABLE if not exists `Inbox{0}` (PlayerFrom CHAR(20), TimeSent DATETIME, Contents VARCHAR(255));", to));
            string text = message.Replace("'", "''");
            if (text.Length > 255)
            {
                text = message.Substring(0, 255);
                text.TrimEnd('\'');
            }
            DBInterface.ExecuteQuery(string.Format("INSERT INTO `Inbox{0}` (PlayerFrom, TimeSent, Contents) VALUES ('{1}', '{2}', '{3}')", to, from,
                                                   DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), text));
        }

        public static List<Player> GetPlayers()
        {
            return new List<Player>(players);
        }

        public static bool Exists(string name)
        {
            lock (players)
            {
                foreach (Player player in players)
                {
                    if (player.name.ToLower() == name.ToLower())
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public static bool Exists(byte id)
        {
            lock (players)
            {
                foreach (Player player in players)
                {
                    if (player.id == id)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public static Player Find(string name)
        {
            var list = new List<Player>();
            list.AddRange(players);
            Player player = null;
            bool flag = false;
            foreach (Player item in list)
            {
                if (item.name.ToLower() == name.ToLower())
                {
                    return item;
                }
                if (item.name.ToLower().IndexOf(name.ToLower()) != -1)
                {
                    if (player == null)
                    {
                        player = item;
                    }
                    else
                    {
                        flag = true;
                    }
                }
            }
            if (flag)
            {
                return null;
            }
            if (player != null)
            {
                return player;
            }
            return null;
        }

        public static Player FindExact(string name)
        {
            var list = new List<Player>();
            list.AddRange(players);
            foreach (Player item in list)
            {
                if (item.name.ToLower() == name.ToLower())
                {
                    return item;
                }
            }
            return null;
        }

        public static Group GetGroup(string name)
        {
            return Group.findPlayerGroup(name);
        }

        public static string GetColor(string name)
        {
            return GetGroup(name).color;
        }

        public static void SendAllToSpawn(Level level)
        {
            players.ForEachSync(delegate(Player pl)
            {
                ThreadPool.QueueUserWorkItem(delegate
                {
                    if (pl.level == level)
                    {
                        Command.all.Find("spawn").Use(pl, "");
                    }
                });
            });
        }

        public static int PosToBlockPos(ushort pos)
        {
            return pos / 32;
        }

        public bool IsTouching(Player who)
        {
            if (Math.Abs(PosToBlockPos(who.pos[0]) - PosToBlockPos(pos[0])) <= 1 && Math.Abs(PosToBlockPos(who.pos[1]) - PosToBlockPos(pos[1])) <= 1)
            {
                return Math.Abs(PosToBlockPos(who.pos[2]) - PosToBlockPos(pos[2])) <= 1;
            }
            return false;
        }

        public int GetXZDistance(ushort[] position1, ushort[] position2)
        {
            return (position1[0] - position2[0]) * (position1[0] - position2[0]) + (position1[2] - position2[2]) * (position1[2] - position2[2]);
        }

        byte GetFreeId()
        {
            int i;
            for (i = lastID + 1; i < 128; i++)
            {
                bool flag = false;
                if (!players.Exists(p => p.id == i) && !PlayerBot.playerbots.Exists(b => b.id == i))
                {
                    lastID = (byte)i;
                    return (byte)i;
                }
            }
            int j;
            for (j = 0; j <= lastID; j++)
            {
                bool flag2 = false;
                if (!players.Exists(p => p.id == j) && !PlayerBot.playerbots.Exists(b => b.id == j))
                {
                    lastID = (byte)j;
                    return (byte)j;
                }
            }
            return 1;
        }

        static byte[] StringFormat(string str, int size)
        {
            byte[] array = new byte[size];
            return enc.GetBytes(str.PadRight(size).Substring(0, size));
        }

        static List<string> Wordwrap2(string message)
        {
            var list = new List<string>();
            message = Regex.Replace(message, "(&[0-9a-f])+(&[0-9a-f])", "$2");
            message = Regex.Replace(message, "(&[0-9a-f])+$", "");
            int num = 64;
            string text = "";
            while (message.Length > 0)
            {
                if (list.Count > 0)
                {
                    message = !(message[0].ToString() == "&") ? text + message.Trim() : message.Trim();
                }
                if (message.IndexOf("&") == message.IndexOf("&", message.IndexOf("&") + 1) - 2)
                {
                    message = message.Remove(message.IndexOf("&"), 2);
                }
                if (message.Length <= num)
                {
                    list.Add(message);
                    break;
                }
                int num2 = num - 1;
                while (true)
                {
                    if (num2 > num - 20)
                    {
                        if (message[num2] == ' ')
                        {
                            list.Add(message.Substring(0, num2));
                            break;
                        }
                        num2--;
                        continue;
                    }
                    while (true)
                    {
                        if (message.Length == 0 || num == 0)
                        {
                            return list;
                        }
                        try
                        {
                            if (message.Substring(num - 2, 1) == "&" || message.Substring(num - 1, 1) == "&")
                            {
                                message = message.Remove(num - 2, 1);
                                num -= 2;
                                continue;
                            }
                            if (message[num - 1] < ' ' || message[num - 1] > '\u007f')
                            {
                                message = message.Remove(num - 1, 1);
                                num--;
                            }
                        }
                        catch
                        {
                            return list;
                        }
                        break;
                    }
                    list.Add(message.Substring(0, num));
                    break;
                }
                message = message.Substring(list[list.Count - 1].Length);
                if (list.Count == 1)
                {
                    num = 60;
                }
                int num3 = list[list.Count - 1].LastIndexOf('&');
                if (num3 == -1)
                {
                    continue;
                }
                if (num3 < list[list.Count - 1].Length - 1)
                {
                    char c2 = list[list.Count - 1][num3 + 1];
                    if ("0123456789abcdef".IndexOf(c2) != -1)
                    {
                        text = "&" + c2;
                    }
                    if (num3 == list[list.Count - 1].Length - 1)
                    {
                        list[list.Count - 1] = list[list.Count - 1].Substring(0, list[list.Count - 1].Length - 2);
                    }
                }
                else if (message.Length != 0)
                {
                    char c3 = message[0];
                    if ("0123456789abcdef".IndexOf(c3) != -1)
                    {
                        text = "&" + c3;
                    }
                    list[list.Count - 1] = list[list.Count - 1].Substring(0, list[list.Count - 1].Length - 1);
                    message = message.Substring(1);
                }
            }
            return list;
        }

        static List<string> Wordwrap(string message)
        {
            var list = new List<string>();
            message = Regex.Replace(message, "(&[0-9a-f])+(&[0-9a-f])", "$2");
            message = Regex.Replace(message, "(&[0-9a-f])+$", "");
            int num = 63;
            string text = "";
            while (message.Length > 0)
            {
                if (list.Count > 0)
                {
                    message = !(message[0].ToString() == "&") ? "> " + text + message.Trim() : "> " + message.Trim();
                }
                if (message.IndexOf("&") == message.IndexOf("&", message.IndexOf("&") + 1) - 2)
                {
                    message = message.Remove(message.IndexOf("&"), 2);
                }
                if (message.Length <= num)
                {
                    list.Add(message);
                    break;
                }
                int num2 = num - 1;
                while (true)
                {
                    if (num2 > num - 20)
                    {
                        if (message[num2] == ' ')
                        {
                            list.Add(message.Substring(0, num2));
                            break;
                        }
                        num2--;
                        continue;
                    }
                    while (true)
                    {
                        if (message.Length == 0 || num == 0)
                        {
                            return list;
                        }
                        try
                        {
                            if (message.Substring(num - 2, 1) == "&" || message.Substring(num - 1, 1) == "&")
                            {
                                message = message.Remove(num - 2, 1);
                                num -= 2;
                                continue;
                            }
                            if (message[num - 1] < ' ' || message[num - 1] > '\u007f')
                            {
                                message = message.Remove(num - 1, 1);
                                num--;
                            }
                        }
                        catch
                        {
                            return list;
                        }
                        break;
                    }
                    list.Add(message.Substring(0, num));
                    break;
                }
                message = message.Substring(list[list.Count - 1].Length);
                if (list.Count == 1)
                {
                    num = 63;
                }
                int num3 = list[list.Count - 1].LastIndexOf('&');
                if (num3 == -1)
                {
                    continue;
                }
                if (num3 < list[list.Count - 1].Length - 1)
                {
                    char c2 = list[list.Count - 1][num3 + 1];
                    if ("0123456789abcdef".IndexOf(c2) != -1)
                    {
                        text = "&" + c2;
                    }
                    if (num3 == list[list.Count - 1].Length - 1)
                    {
                        list[list.Count - 1] = list[list.Count - 1].Substring(0, list[list.Count - 1].Length - 2);
                    }
                }
                else if (message.Length != 0)
                {
                    char c3 = message[0];
                    if ("0123456789abcdef".IndexOf(c3) != -1)
                    {
                        text = "&" + c3;
                    }
                    list[list.Count - 1] = list[list.Count - 1].Substring(0, list[list.Count - 1].Length - 1);
                    message = message.Substring(1);
                }
            }
            return list;
        }

        public static bool ValidName(string name)
        {
            string text = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz01234567890._-+@";
            foreach (char value in name)
            {
                if (text.IndexOf(value) == -1)
                {
                    return false;
                }
            }
            return true;
        }

        public static byte[] GZip(byte[] bytes)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (GZipStream gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, leaveOpen: true))
                {
                    gZipStream.Write(bytes, 0, bytes.Length);
                }
                memoryStream.Position = 0L;
                bytes = new byte[memoryStream.Length];
                memoryStream.Read(bytes, 0, (int)memoryStream.Length);
                return bytes;
            }
        }

        public static byte[] HTNO(ushort x)
        {
            byte[] bytes = BitConverter.GetBytes(x);
            Array.Reverse(bytes);
            return bytes;
        }

        public static ushort NTHO(byte[] x, int offset)
        {
            byte[] array = new byte[2];
            Buffer.BlockCopy(x, offset, array, 0, 2);
            Array.Reverse(array);
            return BitConverter.ToUInt16(array, 0);
        }

        public static byte[] HTNO(short x)
        {
            byte[] bytes = BitConverter.GetBytes(x);
            Array.Reverse(bytes);
            return bytes;
        }

        bool CheckBlockSpam()
        {
            if (spamBlockLog.Count >= spamBlockCount)
            {
                DateTime value = spamBlockLog.Dequeue();
                double totalSeconds = DateTime.Now.Subtract(value).TotalSeconds;
                if (totalSeconds < spamBlockTimer && !ignoreGrief)
                {
                    Kick("You were kicked by antigrief system. Slow down.");
                    SendMessage(string.Format("{0} was kicked for suspected griefing.", "&c" + PublicName));
                    Server.s.Log(name + " was kicked for block spam (" + spamBlockCount + " blocks in " + totalSeconds + " seconds)");
                    return true;
                }
            }
            spamBlockLog.Enqueue(DateTime.Now);
            return false;
        }

        public static void IRCSay(string message, bool opchat = false)
        {
            if (Server.irc)
            {
                IRCBot.Say(message, opchat);
            }
        }

        public void UseArmor()
        {
            armorTime = DateTime.Now;
        }

        bool IsIPLocal()
        {
            if (ip == "127.0.0.1" || ip.StartsWith("192.168.") || ip.StartsWith("10."))
            {
                return true;
            }
            if (ip.StartsWith("172."))
            {
                string[] array = ip.Split('.');
                if (array.Length >= 2)
                {
                    try
                    {
                        int num = Convert.ToInt32(array[1]);
                        return num >= 16 && num <= 31;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        public void SaveStarsCount()
        {
            try
            {
                DBInterface.ExecuteQuery(string.Concat("UPDATE Stars SET GoldStars = ", ExtraData["gold_stars_count"], ", SilverStars = ", ExtraData["silver_stars_count"],
                                                       ", BronzeStars = ", ExtraData["bronze_stars_count"], ", RottenStars = ", ExtraData["rotten_stars_count"],
                                                       " WHERE Name = '", name, "';"));
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        public void SendEmptyLine()
        {
            CmdClearChat.SendEmptyChatMessages(this);
        }

        public static void GlobalMessageLevelSendEmptyLine(Level level)
        {
            players.ForEachSync(delegate(Player p)
            {
                if (p.level == level)
                {
                    p.SendEmptyLine();
                }
            });
        }

        public string ReadLine()
        {
            EventWaitHandle wh = new EventWaitHandle(false, EventResetMode.ManualReset);
            string line = "";
            FilterInput value = delegate(ref string t, out bool h)
            {
                line = t;
                if (t.StartsWith("."))
                {
                    t = t.TrimStart('.');
                    h = false;
                }
                else
                {
                    h = true;
                    wh.Set();
                }
            };
            filterInput += value;
            wh.WaitOne();
            filterInput -= value;
            if (line.Trim().ToLower() == "/a")
            {
                return null;
            }
            return line;
        }

        protected void OnMapChanged(Level from, Level to)
        {
            if (MapChanged != null)
            {
                MapChanged(null, new MapChangeEventArgs(this, from, to));
            }
        }

        public void SendToMap(Level level)
        {
            Level level2 = this.level;
            Loading = true;
            DiePlayers();
            DieBots();
            GlobalDie(this, self: false);
            this.level = level;
            SendUserMOTD(level.mapType == MapType.Home);
            SendMap();
            Thread.Sleep(1);
            if (!hidden)
            {
                GlobalSpawn(this, (ushort)((0.5 + level.spawnx) * 32.0), (ushort)((1 + level.spawny) * 32), (ushort)((0.5 + level.spawnz) * 32.0), level.rotx, level.roty,
                            self: true);
            }
            SendSpawn(byte.MaxValue, color + (IsRefree ? "[REF]" : "") + PublicName, ModelName, (ushort)((0.5 + level.spawnx) * 32.0), (ushort)((1 + level.spawny) * 32),
                      (ushort)((0.5 + level.spawnz) * 32.0), level.rotx, level.roty);
            SpawnPlayers();
            SpawnBots();
            Loading = false;
            level2.NotifyPopulationChanged();
            OnMapChanged(level2, level);
        }

        public void LoadPlayerAppearance()
        {
            using (DataTable dataTable = DBInterface.fillData("SELECT Model, Skin FROM PlayerAppearance WHERE Player = " + DbId))
            {
                if (dataTable.Rows.Count != 0)
                {
                    ModelName = dataTable.Rows[0]["Model"].ToString();
                    SkinName = dataTable.Rows[0]["Skin"].ToString();
                }
            }
        }

        public void SavePlayerAppearance()
        {
            using (DataTable dataTable = DBInterface.fillData("SELECT Model FROM PlayerAppearance WHERE Player = " + DbId))
            {
                var dictionary = new Dictionary<string, object>();
                dictionary.Add("@Player", DbId);
                dictionary.Add("@Model", ModelName);
                dictionary.Add("@Skin", SkinName);
                if (dataTable.Rows.Count == 0)
                {
                    DBInterface.ExecuteQuery("INSERT INTO PlayerAppearance (Player, Model, Skin) VALUES (@Player, @Model, @Skin);", dictionary);
                }
                else
                {
                    DBInterface.ExecuteQuery("UPDATE PlayerAppearance SET Model = @Model, Skin = @Skin WHERE Player = @Player", dictionary);
                }
            }
        }

        public void OnPositionChanged(PositionChangedEventArgs e)
        {
            if (PositionChanged != null)
            {
                PositionChanged(this, e);
            }
        }

        public struct Pos3I
        {
            public int X;

            public int Y;

            public int Z;
        }

        delegate void FilterInput(ref string text, out bool handled);

        public struct CopyPos
        {
            public ushort x;

            public ushort y;

            public ushort z;

            public byte type;
        }

        public struct UndoPos
        {
            public ushort x;

            public ushort y;

            public ushort z;

            public byte type;

            public byte newtype;

            public string mapName;

            public DateTime timePlaced;
        }
    }
}