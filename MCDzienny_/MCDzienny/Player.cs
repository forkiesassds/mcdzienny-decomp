using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
using MCDzienny.Settings;
using Timer = System.Timers.Timer;

namespace MCDzienny
{
    // Token: 0x0200016B RID: 363
    public sealed class Player : Entity
    {
        // Token: 0x02000172 RID: 370
        // (Invoke) Token: 0x06000B79 RID: 2937
        public delegate void BlockchangeEventHandler(Player p, ushort x, ushort y, ushort z, byte type);

        // Token: 0x02000173 RID: 371
        // (Invoke) Token: 0x06000B7D RID: 2941
        public delegate void BlockchangeEventHandler2(Player p, ushort x, ushort y, ushort z, byte type, byte action);

        // Token: 0x02000175 RID: 373
        // (Invoke) Token: 0x06000B85 RID: 2949
        public delegate void BlockPlacedUnderHandler(Player p, int x, int y, int z, ref bool stopChange);

        // Token: 0x0200016D RID: 365
        // (Invoke) Token: 0x06000B6D RID: 2925
        public delegate void PlayerChatEventHandler(Player p, ref string message, ref bool passIt);

        // Token: 0x0200016E RID: 366
        // (Invoke) Token: 0x06000B71 RID: 2929
        public delegate void PlayerRegisteredCheckEventHandler(Player p, ref bool isRegistered);

        // Token: 0x02000174 RID: 372
        // (Invoke) Token: 0x06000B81 RID: 2945
        public delegate void ProperBlockChangeEH(Player p, ushort x, ushort y, ushort z, byte type, byte action,
            ref bool stopChange);

        // Token: 0x040004B8 RID: 1208
        public const int ActionDestroyBlock = 0;

        // Token: 0x040004B9 RID: 1209
        public const int ActionBuildBlock = 1;

        // Token: 0x040004BA RID: 1210
        private const string KeyRecentCommandsTimes = "RecentCommandsTimes";

        // Token: 0x040004C7 RID: 1223
        public static readonly bool Debug_NotifyConsolAboutPlayersClient;

        // Token: 0x040004D4 RID: 1236
        internal static object playersThatLeftLocker;

        // Token: 0x040004D8 RID: 1240
        public static PlayerCollection players;

        // Token: 0x040004D9 RID: 1241
        public static Dictionary<string, string> playersThatLeft;

        // Token: 0x040004DA RID: 1242
        public static List<string> emoteList;

        // Token: 0x040004DB RID: 1243
        public static int totalMySQLFailed;

        // Token: 0x040004DC RID: 1244
        private static readonly ASCIIEncoding enc;

        // Token: 0x040004DF RID: 1247
        public static bool storeHelp;

        // Token: 0x040004E0 RID: 1248
        public static string storedHelp;

        // Token: 0x0400056C RID: 1388
        public static int spamBlockCount;

        // Token: 0x0400056D RID: 1389
        public static int spamBlockTimer;

        // Token: 0x0400056F RID: 1391
        public static int spamChatCount;

        // Token: 0x04000570 RID: 1392
        public static int spamChatTimer;

        // Token: 0x04000584 RID: 1412
        private static byte lastID;

        // Token: 0x040004E5 RID: 1253
        public int afkCount;

        // Token: 0x040004E6 RID: 1254
        public DateTime afkStart;

        // Token: 0x04000529 RID: 1321
        public bool aiming;

        // Token: 0x0400057F RID: 1407
        private readonly List<int> analyse;

        // Token: 0x04000563 RID: 1379
        private DateTime armorTime;

        // Token: 0x04000552 RID: 1362
        private readonly ushort[] basepos;

        // Token: 0x04000567 RID: 1383
        public int bestScore;

        // Token: 0x04000545 RID: 1349
        public byte[] bindings;

        // Token: 0x04000543 RID: 1347
        public byte BlockAction;

        // Token: 0x0400054E RID: 1358
        public object blockchangeObject;

        // Token: 0x040004F4 RID: 1268
        public BlockChanges BlockChanges;

        // Token: 0x040004D1 RID: 1233
        private readonly Dictionary<int, DateTime> blockCommandCooldowns = new Dictionary<int, DateTime>();

        // Token: 0x0400055C RID: 1372
        public bool boughtColor;

        // Token: 0x0400055E RID: 1374
        public bool boughtFarewell;

        // Token: 0x0400055B RID: 1371
        public bool boughtTColor;

        // Token: 0x04000562 RID: 1378
        public bool boughtTitle;

        // Token: 0x0400055D RID: 1373
        public bool boughtWelcome;

        // Token: 0x040004EF RID: 1263
        private byte[] buffer = new byte[0];

        // Token: 0x04000518 RID: 1304
        public volatile bool canBuild = true;

        // Token: 0x04000532 RID: 1330
        public bool carryingFlag;

        // Token: 0x04000579 RID: 1401
        internal volatile int clippingTime;

        // Token: 0x04000546 RID: 1350
        public string[] cmdBind;

        // Token: 0x040004EA RID: 1258
        public bool cmdTimer;

        // Token: 0x040004FE RID: 1278
        public string color;

        // Token: 0x04000527 RID: 1319
        public Thread commThread;

        // Token: 0x04000528 RID: 1320
        public bool commUse;

        // Token: 0x0400056A RID: 1386
        public short connectionSpeed;

        // Token: 0x04000537 RID: 1335
        public bool copyAir;

        // Token: 0x04000536 RID: 1334
        public List<CopyPos> CopyBuffer = new List<CopyPos>();

        // Token: 0x04000538 RID: 1336
        public int[] copyoffset;

        // Token: 0x04000539 RID: 1337
        public ushort[] copystart;

        // Token: 0x040004EE RID: 1262
        public Core core;

        // Token: 0x040004EC RID: 1260
        public bool countToAve;

        // Token: 0x040004CC RID: 1228
        public Support Cpe = new Support();

        // Token: 0x04000530 RID: 1328
        public string CTFtempcolor;

        // Token: 0x04000531 RID: 1329
        public string CTFtempprefix;

        // Token: 0x040004D3 RID: 1235
        private bool databaseLoadFailure;

        // Token: 0x04000541 RID: 1345
        public byte deathBlock;

        // Token: 0x04000540 RID: 1344
        public ushort deathCount;

        // Token: 0x04000509 RID: 1289
        public bool deleteMode;

        // Token: 0x04000580 RID: 1408
        private volatile int direction;

        // Token: 0x040004F1 RID: 1265
        public volatile bool disconnected;

        // Token: 0x040004F7 RID: 1271
        public DisconnectionReason disconnectionReason = DisconnectionReason.Unknown;

        // Token: 0x04000569 RID: 1385
        private bool doCommand;

        // Token: 0x04000572 RID: 1394
        public Dictionary<string, object> extraData;

        // Token: 0x040004E4 RID: 1252
        private readonly Timer extraTimer = new Timer(22000.0);

        // Token: 0x0400057E RID: 1406
        private int extToRead;

        // Token: 0x040004CF RID: 1231
        private readonly List<int> fallPositionBuffer = new List<int>();

        // Token: 0x040004CE RID: 1230
        private readonly object fallSynchronize = new object();

        // Token: 0x04000557 RID: 1367

        // Token: 0x0400051E RID: 1310
        public DateTime firstLogin;

        // Token: 0x040004DD RID: 1245
        private int flags;

        // Token: 0x040004DE RID: 1246
        private PlayersFlags flagsCollection = new PlayersFlags();

        // Token: 0x04000564 RID: 1380

        // Token: 0x04000578 RID: 1400
        internal volatile int flyingTime;

        // Token: 0x04000516 RID: 1302
        public string following = "";

        // Token: 0x04000515 RID: 1301
        public bool frozen;

        // Token: 0x04000507 RID: 1287
        public volatile bool fullylogged;

        // Token: 0x04000508 RID: 1288
        private DateTime fullyloggedtime;

        // Token: 0x0400056B RID: 1387
        public byte GreenYellowRed;

        // Token: 0x040004FF RID: 1279
        public Group group;

        // Token: 0x040004C1 RID: 1217
        private volatile bool hackDetected;

        // Token: 0x04000558 RID: 1368

        // Token: 0x040004C3 RID: 1219
        private readonly object hacksDetectionSync = new object();

        // Token: 0x040004C0 RID: 1216
        public int hackWarnings;

        // Token: 0x0400052F RID: 1327
        public Team hasflag;

        // Token: 0x0400055F RID: 1375
        public bool hasTeleport;

        // Token: 0x04000535 RID: 1333
        public int health = 100;

        // Token: 0x04000500 RID: 1280

        // Token: 0x040004FC RID: 1276
        public byte id;

        // Token: 0x0400050B RID: 1291
        public bool ignoreGrief;

        // Token: 0x0400050A RID: 1290
        public bool ignorePermission;

        // Token: 0x04000560 RID: 1376
        public bool inHeaven;

        // Token: 0x0400052A RID: 1322
        public bool isFlying;

        // Token: 0x040004F2 RID: 1266

        // Token: 0x04000573 RID: 1395
        private bool isUsingTextures;

        // Token: 0x040004D5 RID: 1237
        public bool isZombie;

        // Token: 0x04000503 RID: 1283
        public bool jailed;

        // Token: 0x0400052B RID: 1323

        // Token: 0x0400054F RID: 1359
        public ushort[] lastClick;

        // Token: 0x04000548 RID: 1352
        public string lastCMD;

        // Token: 0x04000542 RID: 1346
        public DateTime lastDeath;

        // Token: 0x0400058A RID: 1418

        // Token: 0x04000565 RID: 1381

        // Token: 0x0400054A RID: 1354
        public bool Loading;

        // Token: 0x0400057C RID: 1404
        public bool loggedIn;

        // Token: 0x0400051C RID: 1308
        public int loginBlocks;

        // Token: 0x040004E3 RID: 1251
        private readonly Timer loginTimer = new Timer(1000.0);

        // Token: 0x040004E8 RID: 1256
        internal DateTime mapLoadedTime = DateTime.MaxValue;

        // Token: 0x040004E7 RID: 1255
        public bool mapLoading;

        // Token: 0x04000574 RID: 1396

        // Token: 0x040004E9 RID: 1257
        public bool megaBoid;

        // Token: 0x04000547 RID: 1351
        public string[] messageBind;

        // Token: 0x0400058B RID: 1419
        private string modelName;

        // Token: 0x04000544 RID: 1348
        public byte modeType;

        // Token: 0x0400051A RID: 1306

        // Token: 0x04000582 RID: 1410
        private volatile int msgDown;

        // Token: 0x04000581 RID: 1409
        private volatile int msgUp;

        // Token: 0x04000502 RID: 1282
        public bool muted;

        // Token: 0x040004E1 RID: 1249
        public DateTime muteTime = DateTime.MinValue;

        // Token: 0x040004F5 RID: 1269
        public string name;

        // Token: 0x0400053F RID: 1343
        public ushort oldBlock;

        // Token: 0x04000551 RID: 1361
        internal volatile ushort[] oldpos;

        // Token: 0x04000554 RID: 1364
        internal byte[] oldrot;

        // Token: 0x040004BE RID: 1214
        private volatile int oldRotX;

        // Token: 0x040004BF RID: 1215
        private volatile int oldRotY;

        // Token: 0x040004BB RID: 1211
        private volatile int oldx = -1;

        // Token: 0x040004BD RID: 1213
        private volatile int oldy = -1;

        // Token: 0x040004BC RID: 1212
        private volatile int oldz = -1;

        // Token: 0x040004C2 RID: 1218
        private volatile bool omitHackDetection;

        // Token: 0x04000514 RID: 1300
        public bool onTrain;

        // Token: 0x0400050F RID: 1295
        public bool onWhitelist;

        // Token: 0x0400050E RID: 1294
        public bool opchat;

        // Token: 0x040004D0 RID: 1232
        private BlockPos originalFallPos;

        // Token: 0x0400057A RID: 1402
        internal volatile int outsideMapTime;

        // Token: 0x0400051B RID: 1307
        public long overallBlocks;

        // Token: 0x04000521 RID: 1313

        // Token: 0x04000501 RID: 1281
        public bool painting;

        // Token: 0x0400050C RID: 1292
        public bool parseSmiley = true;

        // Token: 0x040004EB RID: 1259
        public int pendingPackets;

        // Token: 0x04000585 RID: 1413
        private int playerExperienceOnZombie;

        // Token: 0x04000549 RID: 1353
        private Level pLevel;

        // Token: 0x04000550 RID: 1360
        public volatile ushort[] pos;

        // Token: 0x04000559 RID: 1369
        public byte[] posBuffer;

        // Token: 0x04000517 RID: 1303
        public string possess = "";

        // Token: 0x04000504 RID: 1284
        private string prefix_ = "";

        // Token: 0x0400053E RID: 1342
        public string prevMsg;

        // Token: 0x040004F6 RID: 1270

        // Token: 0x040004ED RID: 1261
        public bool readyForAve;

        // Token: 0x040004FB RID: 1275
        public string realName;

        // Token: 0x0400053B RID: 1339
        public List<UndoPos> RedoBuffer;

        // Token: 0x04000553 RID: 1363
        public byte[] rot;

        // Token: 0x04000589 RID: 1417
        private int roundsOnZombie;

        // Token: 0x04000522 RID: 1314
        public string savedcolor = "";

        // Token: 0x04000566 RID: 1382
        public int score;

        // Token: 0x040004D2 RID: 1234
        internal bool sendLock;

        // Token: 0x040004CD RID: 1229
        private volatile bool shouldFall;

        // Token: 0x040004F3 RID: 1267
        private bool showAlias = true;

        // Token: 0x0400053D RID: 1341
        public bool showMBs;

        // Token: 0x0400053C RID: 1340
        public bool showPortals;

        // Token: 0x0400058C RID: 1420
        private string skinName;

        // Token: 0x0400050D RID: 1293
        public bool smileySaved = true;

        // Token: 0x040004E2 RID: 1250
        public Socket socket;

        // Token: 0x0400056E RID: 1390
        private readonly Queue<DateTime> spamBlockLog;

        // Token: 0x04000571 RID: 1393
        private Queue<DateTime> spamChatLog;

        // Token: 0x04000533 RID: 1331
        public bool spawning;

        // Token: 0x04000576 RID: 1398
        private string starsTag;

        // Token: 0x04000523 RID: 1315
        public bool staticCommands;

        // Token: 0x040004F8 RID: 1272
        private bool stopTheText;

        // Token: 0x04000512 RID: 1298
        public string storedMessage = "";

        // Token: 0x0400052E RID: 1326
        public Team team;

        // Token: 0x04000534 RID: 1332
        public bool teamchat;

        // Token: 0x040004F0 RID: 1264
        private readonly byte[] tempbuffer = new byte[255];

        // Token: 0x04000568 RID: 1384

        // Token: 0x0400051D RID: 1309
        public DateTime timeLogged;

        // Token: 0x04000555 RID: 1365
        public int timesWon;

        // Token: 0x04000505 RID: 1285
        public string title = "";

        // Token: 0x04000506 RID: 1286
        public string titlecolor;

        // Token: 0x04000561 RID: 1377
        public bool toBeMoved;

        // Token: 0x04000520 RID: 1312
        public int totalKicked;

        // Token: 0x0400051F RID: 1311
        public int totalLogins;

        // Token: 0x040004F9 RID: 1273
        private int totalMinutesPlayed;

        // Token: 0x04000513 RID: 1299
        public bool trainGrab;

        // Token: 0x0400053A RID: 1338
        public UndoBufferCollection UndoBuffer;

        // Token: 0x04000519 RID: 1305
        public volatile bool updatePosition = true;

        // Token: 0x040004FD RID: 1277
        public int userID = -1;

        // Token: 0x040004D6 RID: 1238
        public bool usesWom;

        // Token: 0x040004C6 RID: 1222
        private readonly List<byte[]> virtualBlocks = new List<byte[]>();

        // Token: 0x0400052C RID: 1324
        public bool voice;

        // Token: 0x0400052D RID: 1325
        public string voicestring = "";

        // Token: 0x0400055A RID: 1370
        public VotingSystem.VotingChoice votingChoice;

        // Token: 0x04000583 RID: 1411
        private volatile bool waitForFall;

        // Token: 0x04000575 RID: 1397
        private int warnedForHacksTimes;

        // Token: 0x04000556 RID: 1366

        // Token: 0x04000510 RID: 1296
        public bool whisper;

        // Token: 0x04000511 RID: 1297
        public string whisperTo = "";

        // Token: 0x04000577 RID: 1399
        internal int winningStreak;

        // Token: 0x040004D7 RID: 1239
        public string WomVersion = "";

        // Token: 0x04000587 RID: 1415
        private int wonAsHumanTimes;

        // Token: 0x04000586 RID: 1414
        private int wonAsZombieTimes;

        // Token: 0x04000588 RID: 1416
        private int zombifiedCount;

        // Token: 0x04000525 RID: 1317
        public bool ZoneCheck;

        // Token: 0x04000526 RID: 1318
        public bool zoneDel;

        // Token: 0x04000524 RID: 1316
        public DateTime ZoneSpam;

        // Token: 0x06000AB6 RID: 2742 RVA: 0x00037578 File Offset: 0x00035778
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
            //Player.rm = new ResourceManager("Lang.Player", typeof(Player).Assembly);
            lastID = 0;
            PlayerPillared += Player_BlockPlacedUnder;
            players.PlayerAdded += players_PlayerAdded;
            players.PlayerRemoved += players_PlayerRemoved;
        }

        // Token: 0x06000AB9 RID: 2745 RVA: 0x000376B4 File Offset: 0x000358B4
        public Player(Level level)
        {
            filterInput = delegate(ref string t, out bool h) { h = false; };
            userID = -1;
            prefix_ = "";
            title = "";
            parseSmiley = true;
            smileySaved = true;
            whisperTo = "";
            storedMessage = "";
            following = "";
            possess = "";
            canBuild = true;
            updatePosition = true;
            savedcolor = "";
            voicestring = "";
            health = 100;
            CopyBuffer = new List<CopyPos>();
            copyoffset = new int[3];
            copystart = new ushort[3];
            RedoBuffer = new List<UndoPos>();
            prevMsg = "";
            lastDeath = DateTime.Now;
            bindings = new byte[128];
            cmdBind = new string[10];
            messageBind = new string[10];
            lastCMD = "";
            Loading = true;
            lastClick = new ushort[3];
            pos = new ushort[3];
            oldpos = new ushort[3];
            basepos = new ushort[3];
            rot = new byte[2];
            oldrot = new byte[2];
            welcomeMessage = "";
            farewellMessage = "";
            armorTime = DateTime.Now;
            lives = 3;
            tier = 1;
            connectionSpeed = -1;
            spamBlockLog = new Queue<DateTime>(spamBlockCount);
            spamChatLog = new Queue<DateTime>(spamChatCount);
            extraData = new Dictionary<string, object>();
            isUsingTextures = true;
            MapRatingCooldown = DateTime.Now;
            starsTag = "";
            analyse = new List<int>();
            setLevel(level);
        }

        // Token: 0x06000ABA RID: 2746 RVA: 0x000379BC File Offset: 0x00035BBC
        public Player(Socket s)
        {
            filterInput = delegate(ref string t, out bool h) { h = false; };
            userID = -1;
            prefix_ = "";
            title = "";
            parseSmiley = true;
            smileySaved = true;
            whisperTo = "";
            storedMessage = "";
            following = "";
            possess = "";
            canBuild = true;
            updatePosition = true;
            savedcolor = "";
            voicestring = "";
            health = 100;
            CopyBuffer = new List<CopyPos>();
            copyoffset = new int[3];
            copystart = new ushort[3];
            RedoBuffer = new List<UndoPos>();
            prevMsg = "";
            lastDeath = DateTime.Now;
            bindings = new byte[128];
            cmdBind = new string[10];
            messageBind = new string[10];
            lastCMD = "";
            Loading = true;
            lastClick = new ushort[3];
            pos = new ushort[3];
            oldpos = new ushort[3];
            basepos = new ushort[3];
            rot = new byte[2];
            oldrot = new byte[2];
            welcomeMessage = "";
            farewellMessage = "";
            armorTime = DateTime.Now;
            lives = 3;
            tier = 1;
            connectionSpeed = -1;
            spamBlockLog = new Queue<DateTime>(spamBlockCount);
            spamChatLog = new Queue<DateTime>(spamChatCount);
            extraData = new Dictionary<string, object>();
            isUsingTextures = true;
            MapRatingCooldown = DateTime.Now;
            starsTag = "";
            analyse = new List<int>();
            setLevel(null);
            UndoBuffer = new UndoBufferCollection(this);
            BlockChanges = new BlockChanges(this);
            try
            {
                socket = s;
                socket.LingerState.Enabled = false;
                ip = socket.RemoteEndPoint.ToString().Split(':')[0];
                Server.s.Log(string.Format("{0} connected to the server.", ip));
                for (byte b = 0; b < 128; b = (byte) (b + 1)) bindings[b] = b;
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
                                using (var streamReader = File.OpenText("text/welcome.txt"))
                                {
                                    while (!streamReader.EndOfStream) list.Add(streamReader.ReadLine());
                                }

                                foreach (var item in list) SendMessage(item);
                            }
                            catch
                            {
                            }
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
                            using (var dataTable = DBInterface.fillData("SELECT * FROM `Inbox" + name + "`", true))
                            {
                                SendMessage(string.Format(Lang.Player.WelcomeInbox, dataTable.Rows.Count));
                            }
                    }
                    catch (Exception ex3)
                    {
                        Server.ErrorLog(ex3);
                    }

                    if (Server.updateTimer.Interval > 1000.0) SendMessage(Lang.Player.WelcomeLowLag);
                    try
                    {
                        SendMessage(string.Format(Lang.Player.WelcomeMoney,
                            money + Server.DefaultColor + " " + Server.moneys));
                    }
                    catch
                    {
                    }

                    SendMessage(string.Format(Lang.Player.WelcomeModified, overallBlocks + Server.DefaultColor));
                    if (players.Count == 1)
                        SendMessage(string.Format(Lang.Player.WelcomePlayerCount, players.Count));
                    else
                        SendMessage(string.Format(Lang.Player.WelcomePlayersCount, players.Count));
                    try
                    {
                        if (!Group.Find("Nobody").commands.Contains("award") &&
                            !Group.Find("Nobody").commands.Contains("awards") &&
                            !Group.Find("Nobody").commands.Contains("awardmod"))
                            SendMessage(string.Format(Lang.Player.WelcomeAwards, Awards.awardAmount(name)));
                    }
                    catch
                    {
                    }

                    extraTimer.Close();
                    extraTimer.Dispose();
                };
                BlockChangeProper += Player_BlockChangeProper;
            }
            catch (SocketException ex)
            {
                Kick("Login failed!");
                if (ex.ErrorCode != 10054) Server.ErrorLog(ex);
            }
            catch (Exception ex2)
            {
                Kick("Login failed!");
                Server.ErrorLog(ex2);
            }
        }

        // Token: 0x17000484 RID: 1156
        // (get) Token: 0x06000A35 RID: 2613 RVA: 0x00035E18 File Offset: 0x00034018
        public Pos3I HeadPosition
        {
            get
            {
                Pos3I result;
                result.X = (int) XFloat;
                var num = XFloat - (int) XFloat;
                if (num > 0.6875f)
                    result.X = (int) XFloat + 1;
                else if (num < 0.28125f) result.X = (int) XFloat - 1;
                result.Z = (int) ZFloat;
                num = ZFloat - (int) ZFloat;
                if (num > 0.6875f)
                    result.Z = (int) ZFloat + 1;
                else if (num < 0.28125f) result.Z = (int) ZFloat - 1;
                result.Y = (int) YFloat;
                num = YFloat - (int) YFloat;
                if (num < 0.09375f) result.Y--;
                return result;
            }
        }

        // Token: 0x17000485 RID: 1157
        // (get) Token: 0x06000A36 RID: 2614 RVA: 0x00035EFC File Offset: 0x000340FC
        public byte HeadBlock
        {
            get
            {
                var headPosition = HeadPosition;
                return level.GetTile(headPosition.X, headPosition.Y, headPosition.Z);
            }
        }

        // Token: 0x17000486 RID: 1158
        // (get) Token: 0x06000A37 RID: 2615 RVA: 0x00035F30 File Offset: 0x00034130
        public byte FootBlock
        {
            get
            {
                var headPosition = HeadPosition;
                return level.GetTile(headPosition.X, headPosition.Y - 1, headPosition.Z);
            }
        }

        // Token: 0x17000487 RID: 1159
        // (get) Token: 0x06000A4C RID: 2636 RVA: 0x0003643C File Offset: 0x0003463C
        // (set) Token: 0x06000A4D RID: 2637 RVA: 0x00036444 File Offset: 0x00034644
        public AuthenticationProvider Authentication { get; private set; }

        // Token: 0x17000488 RID: 1160
        // (get) Token: 0x06000A4E RID: 2638 RVA: 0x00036450 File Offset: 0x00034650
        public static byte number
        {
            get { return (byte) players.Count; }
        }

        // Token: 0x17000489 RID: 1161
        // (get) Token: 0x06000A4F RID: 2639 RVA: 0x00036460 File Offset: 0x00034660
        public bool IsTempMuted
        {
            get { return muteTime > DateTime.Now; }
        }

        // Token: 0x1700048A RID: 1162
        // (get) Token: 0x06000A50 RID: 2640 RVA: 0x00036474 File Offset: 0x00034674
        // (set) Token: 0x06000A51 RID: 2641 RVA: 0x0003647C File Offset: 0x0003467C
        public bool ShowAlias
        {
            get { return showAlias; }
            set { showAlias = value; }
        }

        // Token: 0x1700048B RID: 1163
        // (get) Token: 0x06000A52 RID: 2642 RVA: 0x00036488 File Offset: 0x00034688
        // (set) Token: 0x06000A53 RID: 2643 RVA: 0x00036490 File Offset: 0x00034690
        [Browsable(false)] public bool IsPrinting { get; set; }

        // Token: 0x1700048C RID: 1164
        // (get) Token: 0x06000A54 RID: 2644 RVA: 0x0003649C File Offset: 0x0003469C
        // (set) Token: 0x06000A55 RID: 2645 RVA: 0x000364A4 File Offset: 0x000346A4
        public string PublicName { get; set; }

        // Token: 0x1700048D RID: 1165
        // (get) Token: 0x06000A56 RID: 2646 RVA: 0x000364B0 File Offset: 0x000346B0
        // (set) Token: 0x06000A57 RID: 2647 RVA: 0x000364B8 File Offset: 0x000346B8
        [Browsable(false)]
        public PlayersFlags FlagsCollection
        {
            get { return flagsCollection; }
            set { flagsCollection = value; }
        }

        // Token: 0x1700048E RID: 1166
        // (get) Token: 0x06000A58 RID: 2648 RVA: 0x000364C4 File Offset: 0x000346C4
        // (set) Token: 0x06000A59 RID: 2649 RVA: 0x000364F4 File Offset: 0x000346F4
        public int TotalMinutesPlayed
        {
            get { return totalMinutesPlayed + (int) DateTime.Now.Subtract(timeLogged).TotalMinutes; }
            set
            {
                var num = value - (int) DateTime.Now.Subtract(timeLogged).TotalMinutes;
                totalMinutesPlayed = num >= 0 ? num : 0;
            }
        }

        // Token: 0x1700048F RID: 1167
        // (get) Token: 0x06000A5C RID: 2652 RVA: 0x000365A0 File Offset: 0x000347A0
        // (set) Token: 0x06000A5D RID: 2653 RVA: 0x000365A8 File Offset: 0x000347A8
        [Category("General")]
        [ReadOnly(true)]
        [Description("Ip address.")]
        public string ip { get; set; }

        // Token: 0x17000490 RID: 1168
        // (get) Token: 0x06000A5E RID: 2654 RVA: 0x000365B4 File Offset: 0x000347B4
        // (set) Token: 0x06000A5F RID: 2655 RVA: 0x000365BC File Offset: 0x000347BC
        [Description("Describes wheter a player is hidden or not.")]
        [Category("General")]
        [ReadOnly(true)]
        public bool hidden { get; set; }

        // Token: 0x17000491 RID: 1169
        // (get) Token: 0x06000A60 RID: 2656 RVA: 0x000365C8 File Offset: 0x000347C8
        // (set) Token: 0x06000A61 RID: 2657 RVA: 0x000365D0 File Offset: 0x000347D0
        [Description("Indicates wheter a player is invincible.")]
        [Category("General")]
        public bool invincible { get; set; }

        // Token: 0x17000492 RID: 1170
        // (get) Token: 0x06000A62 RID: 2658 RVA: 0x000365DC File Offset: 0x000347DC
        // (set) Token: 0x06000A63 RID: 2659 RVA: 0x000365E4 File Offset: 0x000347E4
        [ReadOnly(true)]
        [Description(
            "Title of the player. Change made here will be temporary, it doesn't write the change to database.")]
        [Category("General")]
        public string prefix
        {
            get { return prefix_; }
            set { prefix_ = value; }
        }

        // Token: 0x17000493 RID: 1171
        // (get) Token: 0x06000A64 RID: 2660 RVA: 0x000365F0 File Offset: 0x000347F0
        // (set) Token: 0x06000A65 RID: 2661 RVA: 0x000365F8 File Offset: 0x000347F8
        [Description("The amount of money.")]
        [Category("General")]
        public int money { get; set; }

        // Token: 0x17000494 RID: 1172
        // (get) Token: 0x06000A66 RID: 2662 RVA: 0x00036604 File Offset: 0x00034804
        // (set) Token: 0x06000A67 RID: 2663 RVA: 0x0003660C File Offset: 0x0003480C
        [Category("General")]
        [Description("Overall death counter.")]
        public int overallDeath { get; set; }

        // Token: 0x17000495 RID: 1173
        // (get) Token: 0x06000A68 RID: 2664 RVA: 0x00036618 File Offset: 0x00034818
        // (set) Token: 0x06000A69 RID: 2665 RVA: 0x00036620 File Offset: 0x00034820
        [Description(
            "Determines wheter player is in joker mode. Joker mode substitutes players words with the phrases from joker.txt file.")]
        [Category("Fun")]
        public bool joker { get; set; }

        // Token: 0x17000496 RID: 1174
        // (get) Token: 0x06000A6A RID: 2666 RVA: 0x0003662C File Offset: 0x0003482C
        [RefreshProperties(RefreshProperties.Repaint)]
        [ReadOnly(true)]
        [Description("Shows the name of the map that players is currently in.")]
        [Category("General")]
        public string playerLevelName
        {
            get { return level.name; }
        }

        // Token: 0x17000497 RID: 1175
        // (get) Token: 0x06000A6B RID: 2667 RVA: 0x0003663C File Offset: 0x0003483C
        // (set) Token: 0x06000A6C RID: 2668 RVA: 0x000366B4 File Offset: 0x000348B4
        [Browsable(false)]
        public new Level level
        {
            get
            {
                if (pLevel != null) return pLevel;
                if (Server.mode == Mode.Lava)
                {
                    pLevel = Server.LavaLevel;
                    return pLevel;
                }

                if (Server.mode == Mode.Freebuild || Server.mode == Mode.LavaFreebuild ||
                    Server.mode == Mode.ZombieFreebuild)
                {
                    pLevel = Server.mainLevel;
                    return pLevel;
                }

                pLevel = Server.InfectionLevel;
                return pLevel;
            }
            set { pLevel = value; }
        }

        // Token: 0x17000498 RID: 1176
        // (get) Token: 0x06000A77 RID: 2679 RVA: 0x00036840 File Offset: 0x00034A40
        // (set) Token: 0x06000A78 RID: 2680 RVA: 0x00036848 File Offset: 0x00034A48
        [Description("The message that is being shown when a player logs in.")]
        [Category("General")]
        public string welcomeMessage { get; set; }

        // Token: 0x17000499 RID: 1177
        // (get) Token: 0x06000A79 RID: 2681 RVA: 0x00036854 File Offset: 0x00034A54
        // (set) Token: 0x06000A7A RID: 2682 RVA: 0x0003685C File Offset: 0x00034A5C
        [Description("The message that is being shown when a player logs out.")]
        [Category("General")]
        public string farewellMessage { get; set; }

        // Token: 0x1700049A RID: 1178
        // (get) Token: 0x06000A7B RID: 2683 RVA: 0x00036868 File Offset: 0x00034A68
        // (set) Token: 0x06000A7C RID: 2684 RVA: 0x00036870 File Offset: 0x00034A70
        [Category("General")]
        [Description("Determines whether player is allowed to use hacks(WOM client).")]
        public bool hacksAllowed { get; set; }

        // Token: 0x1700049B RID: 1179
        // (get) Token: 0x06000A7D RID: 2685 RVA: 0x0003687C File Offset: 0x00034A7C
        // (set) Token: 0x06000A7E RID: 2686 RVA: 0x00036884 File Offset: 0x00034A84
        [Description("Determines wheter player has a fliped head or not.")]
        [Category("Fun")]
        public bool flipHead { get; set; }

        // Token: 0x1700049C RID: 1180
        // (get) Token: 0x06000A7F RID: 2687 RVA: 0x00036890 File Offset: 0x00034A90
        // (set) Token: 0x06000A80 RID: 2688 RVA: 0x00036898 File Offset: 0x00034A98
        [Description("The amount of water blocks that the player currently has.")]
        [Category("LavaItems")]
        public int waterBlocks { get; set; }

        // Token: 0x1700049D RID: 1181
        // (get) Token: 0x06000A81 RID: 2689 RVA: 0x000368A4 File Offset: 0x00034AA4
        // (set) Token: 0x06000A82 RID: 2690 RVA: 0x000368AC File Offset: 0x00034AAC
        [Category("LavaItems")]
        [Description("The amount of door blocks that the player currently has.")]
        public int doorBlocks { get; set; }

        // Token: 0x1700049E RID: 1182
        // (get) Token: 0x06000A83 RID: 2691 RVA: 0x000368B8 File Offset: 0x00034AB8
        // (set) Token: 0x06000A84 RID: 2692 RVA: 0x000368C0 File Offset: 0x00034AC0
        [Description("The amount of sponge blocks that the player currently has.")]
        [Category("LavaItems")]
        public int spongeBlocks { get; set; }

        // Token: 0x1700049F RID: 1183
        // (get) Token: 0x06000A85 RID: 2693 RVA: 0x000368CC File Offset: 0x00034ACC
        // (set) Token: 0x06000A86 RID: 2694 RVA: 0x000368D4 File Offset: 0x00034AD4
        [Category("Lava")]
        [Description("The amount of lives that the player currently has.")]
        public byte lives { get; set; }

        // Token: 0x170004A0 RID: 1184
        // (get) Token: 0x06000A87 RID: 2695 RVA: 0x000368E0 File Offset: 0x00034AE0
        // (set) Token: 0x06000A88 RID: 2696 RVA: 0x000368E8 File Offset: 0x00034AE8
        [Description("The player's total experience/score.")]
        [Category("Lava")]
        public int totalScore { get; set; }

        // Token: 0x170004A1 RID: 1185
        // (get) Token: 0x06000A89 RID: 2697 RVA: 0x000368F4 File Offset: 0x00034AF4
        // (set) Token: 0x06000A8A RID: 2698 RVA: 0x000368FC File Offset: 0x00034AFC
        [ReadOnly(true)]
        [Browsable(false)]
        [Description("The player's tier.")]
        [Category("Lava")]
        public int tier { get; set; }

        // Token: 0x170004A2 RID: 1186
        // (get) Token: 0x06000A8B RID: 2699 RVA: 0x00036908 File Offset: 0x00034B08
        // (set) Token: 0x06000A8C RID: 2700 RVA: 0x00036910 File Offset: 0x00034B10
        [Category("LavaItems")]
        [Description("The amount of hammer that the player currently has.")]
        public int hammer { get; set; }

        // Token: 0x170004A3 RID: 1187
        // (get) Token: 0x06000A8D RID: 2701 RVA: 0x0003691C File Offset: 0x00034B1C
        // (set) Token: 0x06000A8E RID: 2702 RVA: 0x00036924 File Offset: 0x00034B24
        public IronChallengeType IronChallenge { get; set; }

        // Token: 0x170004A4 RID: 1188
        // (get) Token: 0x06000A8F RID: 2703 RVA: 0x00036930 File Offset: 0x00034B30
        // (set) Token: 0x06000A90 RID: 2704 RVA: 0x00036938 File Offset: 0x00034B38
        [Browsable(false)]
        public Dictionary<string, object> ExtraData
        {
            get { return extraData; }
            set { extraData = value; }
        }

        // Token: 0x170004A5 RID: 1189
        // (get) Token: 0x06000A91 RID: 2705 RVA: 0x00036944 File Offset: 0x00034B44
        [Browsable(false)]
        public string IronChallengeTag
        {
            get
            {
                if (level.mapType != MapType.Lava) return "";
                if (IronChallenge == IronChallengeType.IronMan) return "%f(male) ";
                if (IronChallenge == IronChallengeType.IronWoman) return "%f(female)   ";
                return "";
            }
        }

        // Token: 0x170004A6 RID: 1190
        // (get) Token: 0x06000A92 RID: 2706 RVA: 0x00036980 File Offset: 0x00034B80
        [Browsable(false)]
        public bool IsAboveSeaLevel
        {
            get { return pos[1] / 32 - 1 > level.height / 2; }
        }

        // Token: 0x170004A7 RID: 1191
        // (get) Token: 0x06000A93 RID: 2707 RVA: 0x000369A4 File Offset: 0x00034BA4
        // (set) Token: 0x06000A94 RID: 2708 RVA: 0x000369AC File Offset: 0x00034BAC
        public bool IsUsingXWom { get; set; }

        // Token: 0x170004A8 RID: 1192
        // (get) Token: 0x06000A95 RID: 2709 RVA: 0x000369B8 File Offset: 0x00034BB8
        // (set) Token: 0x06000A96 RID: 2710 RVA: 0x000369C0 File Offset: 0x00034BC0
        public bool IsUsingWom { get; set; }

        // Token: 0x170004A9 RID: 1193
        // (get) Token: 0x06000A97 RID: 2711 RVA: 0x000369CC File Offset: 0x00034BCC
        public bool IsUsingTextures
        {
            get { return IsUsingWom && isUsingTextures; }
        }

        // Token: 0x170004AA RID: 1194
        // (get) Token: 0x06000A98 RID: 2712 RVA: 0x000369E0 File Offset: 0x00034BE0
        // (set) Token: 0x06000A99 RID: 2713 RVA: 0x000369E8 File Offset: 0x00034BE8
        [Browsable(false)] public DateTime MapRatingCooldown { get; set; }

        // Token: 0x170004AB RID: 1195
        // (get) Token: 0x06000A9A RID: 2714 RVA: 0x000369F4 File Offset: 0x00034BF4
        [Browsable(false)]
        public float XFloat
        {
            get { return pos[0] / 32f; }
        }

        // Token: 0x170004AC RID: 1196
        // (get) Token: 0x06000A9B RID: 2715 RVA: 0x00036A08 File Offset: 0x00034C08
        [Browsable(false)]
        public float YFloat
        {
            get { return pos[1] / 32f; }
        }

        // Token: 0x170004AD RID: 1197
        // (get) Token: 0x06000A9C RID: 2716 RVA: 0x00036A1C File Offset: 0x00034C1C
        [Browsable(false)]
        public float ZFloat
        {
            get { return pos[2] / 32f; }
        }

        // Token: 0x170004AE RID: 1198
        // (get) Token: 0x06000A9D RID: 2717 RVA: 0x00036A30 File Offset: 0x00034C30
        // (set) Token: 0x06000A9E RID: 2718 RVA: 0x00036A38 File Offset: 0x00034C38
        public int WarnedForHacksTimes
        {
            get { return warnedForHacksTimes; }
            set
            {
                warnedForHacksTimes = value;
                if (warnedForHacksTimes > 6) Kick(Lang.Player.KickHacks);
            }
        }

        // Token: 0x170004AF RID: 1199
        // (get) Token: 0x06000A9F RID: 2719 RVA: 0x00036A60 File Offset: 0x00034C60
        // (set) Token: 0x06000AA0 RID: 2720 RVA: 0x00036A94 File Offset: 0x00034C94
        public string StarsTag
        {
            get
            {
                if (level.mapType == MapType.Zombie) return starsTag;
                if (level.mapType == MapType.Lava) return LavaPrefix;
                return "";
            }
            set { starsTag = value; }
        }

        // Token: 0x170004B0 RID: 1200
        // (get) Token: 0x06000AA1 RID: 2721 RVA: 0x00036AA0 File Offset: 0x00034CA0
        public string LavaPrefix
        {
            get
            {
                if (!LavaSettings.All.StarSystem) return "";
                if (winningStreak == 1) return MCColor.DarkGray + "*";
                if (winningStreak == 2) return MCColor.DarkGray + "**";
                if (winningStreak >= 3) return MCColor.DarkGray + "***";
                return "";
            }
        }

        // Token: 0x170004B1 RID: 1201
        // (get) Token: 0x06000AAC RID: 2732 RVA: 0x00036D70 File Offset: 0x00034F70
        public bool IsOutsideMap
        {
            get
            {
                return level != null && ((int) XFloat < 0 || (int) YFloat < 1 || (int) ZFloat < 0 ||
                                         (int) XFloat > level.width - 1 || (int) YFloat > level.height + 2 ||
                                         (int) ZFloat > level.depth - 1);
            }
        }

        // Token: 0x170004B2 RID: 1202
        // (get) Token: 0x06000AAD RID: 2733 RVA: 0x00036DE8 File Offset: 0x00034FE8
        public bool IsClipping
        {
            get
            {
                return level != null && (!Block.Standable(level.GetTile((int) XFloat, (int) YFloat, (int) ZFloat)) ||
                                         !Block.Standable(level.GetTile((int) XFloat, (int) YFloat - 1, (int) ZFloat)));
            }
        }

        // Token: 0x170004B3 RID: 1203
        // (get) Token: 0x06000AB0 RID: 2736 RVA: 0x000372F4 File Offset: 0x000354F4
        public bool IsAirborn
        {
            get
            {
                if (level != null)
                {
                    foreach (var tile in BlocksBelow)
                        if (!Block.IsAir(tile))
                            return false;
                    return true;
                }

                return false;
            }
        }

        // Token: 0x170004B4 RID: 1204
        // (get) Token: 0x06000AB1 RID: 2737 RVA: 0x00037334 File Offset: 0x00035534
        public byte[] BlocksBelow
        {
            get
            {
                if (level == null) return new byte[0];
                var num = XFloat - (int) XFloat;
                var num2 = -1;
                var num3 = -1;
                var y = (int) (YFloat - 2.2f);
                if (num > 0.6875f)
                    num2 = (int) XFloat + 1;
                else if (num < 0.2813f) num2 = (int) XFloat - 1;
                num = ZFloat - (int) ZFloat;
                if (num > 0.6875f)
                    num3 = (int) ZFloat + 1;
                else if (num < 0.2813f) num3 = (int) ZFloat - 1;
                if (num2 != -1 && num3 == -1)
                    return new[]
                    {
                        level.GetTile((int) XFloat, y, (int) ZFloat),
                        level.GetTile(num2, y, (int) ZFloat)
                    };
                if (num2 == -1 && num3 != -1)
                    return new[]
                    {
                        level.GetTile((int) XFloat, y, (int) ZFloat),
                        level.GetTile((int) XFloat, y, num3)
                    };
                if (num2 == -1 && num3 == -1)
                    return new[]
                    {
                        level.GetTile((int) XFloat, y, (int) ZFloat)
                    };
                return new[]
                {
                    level.GetTile((int) XFloat, y, (int) ZFloat),
                    level.GetTile(num2, y, (int) ZFloat),
                    level.GetTile((int) XFloat, y, num3),
                    level.GetTile(num2, y, num3)
                };
            }
        }

        // Token: 0x170004B5 RID: 1205
        // (get) Token: 0x06000AC2 RID: 2754 RVA: 0x000389E4 File Offset: 0x00036BE4
        // (set) Token: 0x06000AC3 RID: 2755 RVA: 0x000389EC File Offset: 0x00036BEC
        public bool IsCpeSupported { get; private set; }

        // Token: 0x170004B6 RID: 1206
        // (get) Token: 0x06000B37 RID: 2871 RVA: 0x00042E7C File Offset: 0x0004107C
        [Browsable(false)]
        public string Tag
        {
            get
            {
                if (level.mapType != MapType.Zombie) return "";
                if (IsRefree) return InfectionSettings.All.RefreeTag;
                if (InfectionSystem.InfectionSystem.infected.Contains(this)) return InfectionSettings.All.ZombieTag;
                return InfectionSettings.All.HumanTag;
            }
        }

        // Token: 0x170004B7 RID: 1207
        // (get) Token: 0x06000B38 RID: 2872 RVA: 0x00042ED4 File Offset: 0x000410D4
        // (set) Token: 0x06000B39 RID: 2873 RVA: 0x00042EDC File Offset: 0x000410DC
        [ReadOnly(true)] public bool IsRefree { get; set; }

        // Token: 0x170004B8 RID: 1208
        // (get) Token: 0x06000B3A RID: 2874 RVA: 0x00042EE8 File Offset: 0x000410E8
        [Description(
            "A player's level. Displays player's level on lava when player is on a lava map, and zombie level when player is on a zombie map.")]
        [ReadOnly(true)]
        public string Tier
        {
            get
            {
                if (level.mapType == MapType.Zombie && InfectionSettings.All.UsePlayerLevels)
                    return string.Format("%f({0})", ZombieTier);
                if (level.mapType == MapType.Lava && LavaSettings.All.ScoreMode != ScoreSystem.NoScore)
                    return string.Format("%f({0})", LavaTier);
                return "";
            }
        }

        // Token: 0x170004B9 RID: 1209
        // (get) Token: 0x06000B3B RID: 2875 RVA: 0x00042F5C File Offset: 0x0004115C
        [ReadOnly(true)]
        [Category("Zombie")]
        public int ZombieTier
        {
            get { return InfectionTiers.GetTier(PlayerExperienceOnZombie); }
        }

        // Token: 0x170004BA RID: 1210
        // (get) Token: 0x06000B3C RID: 2876 RVA: 0x00042F6C File Offset: 0x0004116C
        // (set) Token: 0x06000B3D RID: 2877 RVA: 0x00042F74 File Offset: 0x00041174
        [Browsable(false)]
        [Category("Zombie")]
        public int LavaTier
        {
            get { return tier; }
            set { tier = value; }
        }

        // Token: 0x170004BB RID: 1211
        // (get) Token: 0x06000B3E RID: 2878 RVA: 0x00042F80 File Offset: 0x00041180
        // (set) Token: 0x06000B3F RID: 2879 RVA: 0x00042F88 File Offset: 0x00041188
        [ReadOnly(false)]
        [Category("Zombie")]
        public int PlayerExperienceOnZombie
        {
            get { return playerExperienceOnZombie; }
            set { playerExperienceOnZombie = value; }
        }

        // Token: 0x170004BC RID: 1212
        // (get) Token: 0x06000B40 RID: 2880 RVA: 0x00042F94 File Offset: 0x00041194
        // (set) Token: 0x06000B41 RID: 2881 RVA: 0x00042F9C File Offset: 0x0004119C
        [Category("Zombie")]
        [ReadOnly(true)]
        public int WonAsZombieTimes
        {
            get { return wonAsZombieTimes; }
            set { wonAsZombieTimes = value; }
        }

        // Token: 0x170004BD RID: 1213
        // (get) Token: 0x06000B42 RID: 2882 RVA: 0x00042FA8 File Offset: 0x000411A8
        // (set) Token: 0x06000B43 RID: 2883 RVA: 0x00042FB0 File Offset: 0x000411B0
        [Category("Zombie")]
        [ReadOnly(true)]
        public int WonAsHumanTimes
        {
            get { return wonAsHumanTimes; }
            set { wonAsHumanTimes = value; }
        }

        // Token: 0x170004BE RID: 1214
        // (get) Token: 0x06000B44 RID: 2884 RVA: 0x00042FBC File Offset: 0x000411BC
        // (set) Token: 0x06000B45 RID: 2885 RVA: 0x00042FC4 File Offset: 0x000411C4
        [ReadOnly(true)]
        [Category("Zombie")]
        public int ZombifiedCount
        {
            get { return zombifiedCount; }
            set { zombifiedCount = value; }
        }

        // Token: 0x170004BF RID: 1215
        // (get) Token: 0x06000B46 RID: 2886 RVA: 0x00042FD0 File Offset: 0x000411D0
        // (set) Token: 0x06000B47 RID: 2887 RVA: 0x00042FD8 File Offset: 0x000411D8
        [Category("Zombie")]
        [ReadOnly(true)]
        public int RoundsOnZombie
        {
            get { return roundsOnZombie; }
            set { roundsOnZombie = value; }
        }

        // Token: 0x170004C0 RID: 1216
        // (get) Token: 0x06000B48 RID: 2888 RVA: 0x00042FE4 File Offset: 0x000411E4
        // (set) Token: 0x06000B49 RID: 2889 RVA: 0x00042FEC File Offset: 0x000411EC
        [Browsable(false)] public DateTime LastPillar { get; set; }

        // Token: 0x170004C1 RID: 1217
        // (get) Token: 0x06000B50 RID: 2896 RVA: 0x000432EC File Offset: 0x000414EC
        // (set) Token: 0x06000B51 RID: 2897 RVA: 0x000432F4 File Offset: 0x000414F4
        public string ModelName
        {
            get { return modelName; }
            set
            {
                if (value != null && (!ValidName(value) || value.Length > 64)) return;
                modelName = value;
            }
        }

        // Token: 0x170004C2 RID: 1218
        // (get) Token: 0x06000B52 RID: 2898 RVA: 0x00043314 File Offset: 0x00041514
        // (set) Token: 0x06000B53 RID: 2899 RVA: 0x0004331C File Offset: 0x0004151C
        public string SkinName
        {
            get { return skinName; }
            set
            {
                if (value != null && (!ValidName(value) || value.Length > 64)) return;
                skinName = value;
            }
        }

        // Token: 0x170004C3 RID: 1219
        // (get) Token: 0x06000B54 RID: 2900 RVA: 0x0004333C File Offset: 0x0004153C
        // (set) Token: 0x06000B55 RID: 2901 RVA: 0x00043344 File Offset: 0x00041544
        [ReadOnly(true)] public int? DbId { get; set; }

        // Token: 0x06000A33 RID: 2611 RVA: 0x0003598C File Offset: 0x00033B8C
        public bool HacksDetection(ushort x, ushort y, ushort z, byte rotX, byte rotY)
        {
            bool result;
            lock (hacksDetectionSync)
            {
                if (IsRefree)
                {
                    result = false;
                }
                else if (level.mapType != MapType.Zombie && level.allowHacks ||
                         !InfectionSettings.All.DisallowHacksUseOnInfectionMap ||
                         group.Permission >= (LevelPermission) InfectionSettings.All.HacksOnInfectionMapPermission ||
                         mapLoading || !fullylogged)
                {
                    result = false;
                }
                else if (oldx == -1 && oldy == -1 && oldz == -1)
                {
                    oldx = x;
                    oldy = y;
                    oldz = z;
                    result = false;
                }
                else
                {
                    hackDetected = false;
                    if (Math.Sqrt(Math.Pow(x - oldx, 2.0) + Math.Pow(z - oldz, 2.0)) >
                        InfectionSettings.All.SpeedHackDetectionThreshold ||
                        y - oldy > InfectionSettings.All.SpeedHackDetectionThreshold) hackDetected = true;
                    if (hackDetected && fullylogged && !mapLoading)
                    {
                        Server.s.Log(name + " tried to respawn.", true);
                        SendPos(byte.MaxValue, (ushort) (((oldx >> 5) << 5) + 16), (ushort) (((oldy >> 5) << 5) + 16),
                            (ushort) (((oldz >> 5) << 5) + 16), (byte) oldRotX, (byte) oldRotY);
                        result = true;
                    }
                    else if (InfectionSettings.All.BlockGlitchPrevention && level.mapType == MapType.Zombie &&
                             !InfectionSystem.InfectionSystem.currentInfectionMap.IsBuildingAllowed &&
                             IsInBlock(x, y, z))
                    {
                        Server.s.Log(name + " tried to block-glitch.", true);
                        var pos3I = PlayerPosToBlockPos(oldx / 32f, oldy / 32f, oldz / 32f);
                        SendPos(byte.MaxValue, (ushort) ((pos3I.X << 5) + 16), (ushort) ((pos3I.Y << 5) + 16),
                            (ushort) ((pos3I.Z << 5) + 16), (byte) oldRotX, (byte) oldRotY);
                        result = true;
                    }
                    else
                    {
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

                        result = false;
                    }
                }
            }

            return result;
        }

        // Token: 0x06000A34 RID: 2612 RVA: 0x00035D74 File Offset: 0x00033F74
        public Pos3I PlayerPosToBlockPos(float x, float y, float z)
        {
            Pos3I result;
            result.X = (int) x;
            var num = x - (int) x;
            if (num > 0.6875f)
                result.X = (int) x + 1;
            else if (num < 0.28125f) result.X = (int) x - 1;
            result.Z = (int) z;
            num = z - (int) z;
            if (num > 0.6875f)
                result.Z = (int) z + 1;
            else if (num < 0.28125f) result.Z = (int) z - 1;
            result.Y = (int) y;
            num = y - (int) y;
            if (num < 0.09375f) result.Y--;
            return result;
        }

        // Token: 0x06000A38 RID: 2616 RVA: 0x00035F68 File Offset: 0x00034168
        public bool IsInBlock(int x, int y, int z)
        {
            var pos3I = PlayerPosToBlockPos(x / 32f, y / 32f, z / 32f);
            var tile = level.GetTile(pos3I.X, pos3I.Y, pos3I.Z);
            var tile2 = level.GetTile(pos3I.X, pos3I.Y - 1, pos3I.Z);
            return !Block.IsDoor(tile) && !Block.Walkthrough(tile) && tile != byte.MaxValue || !Block.IsDoor(tile2) &&
                !Block.Walkthrough(tile2) && tile2 != 50 && tile2 != 44 && tile2 != byte.MaxValue;
        }

        // Token: 0x06000A39 RID: 2617 RVA: 0x0003601C File Offset: 0x0003421C
        public void DistanceMeasure(int x, int z)
        {
            try
            {
                var num = (float) Math.Sqrt(Math.Pow(x - oldx, 2.0) + Math.Pow(z - oldz, 2.0));
                oldx = x;
                oldz = z;
                Server.s.Log(num.ToString());
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        // Token: 0x06000A3A RID: 2618 RVA: 0x000360A4 File Offset: 0x000342A4
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

        // Token: 0x14000002 RID: 2
        // (add) Token: 0x06000A3B RID: 2619 RVA: 0x000360F4 File Offset: 0x000342F4
        // (remove) Token: 0x06000A3C RID: 2620 RVA: 0x00036128 File Offset: 0x00034328
        public static event PlayerChatEventHandler PlayerChatEvent;

        // Token: 0x14000003 RID: 3
        // (add) Token: 0x06000A3D RID: 2621 RVA: 0x0003615C File Offset: 0x0003435C
        // (remove) Token: 0x06000A3E RID: 2622 RVA: 0x00036190 File Offset: 0x00034390
        public static event PlayerRegisteredCheckEventHandler PlayerRegisteredCheck;

        // Token: 0x06000A3F RID: 2623 RVA: 0x000361C4 File Offset: 0x000343C4
        public static byte[] Join(List<byte[]> bList)
        {
            var array = new byte[8 * bList.Count];
            for (var i = 0; i < bList.Count; i++) Buffer.BlockCopy(bList[i], 0, array, i * 8, 8);
            return array;
        }

        // Token: 0x06000A40 RID: 2624 RVA: 0x00036204 File Offset: 0x00034404
        public void AddVirtualBlock(int x, int y, int z, byte block)
        {
            AddVirtualBlock((ushort) x, (ushort) y, (ushort) z, block);
        }

        // Token: 0x06000A41 RID: 2625 RVA: 0x00036214 File Offset: 0x00034414
        public void AddVirtualBlock(ushort x, ushort y, ushort z, byte block)
        {
            var array = new byte[8];
            array[0] = 6;
            HTNO(x).CopyTo(array, 1);
            HTNO(y).CopyTo(array, 3);
            HTNO(z).CopyTo(array, 5);
            array[7] = Block.Convert(block);
            AddVirtualBlock(array);
        }

        // Token: 0x06000A42 RID: 2626 RVA: 0x00036264 File Offset: 0x00034464
        public void AddVirtualBlock(byte[] block)
        {
            if (block.Length != 8) return;
            virtualBlocks.Add(block);
        }

        // Token: 0x06000A43 RID: 2627 RVA: 0x0003627C File Offset: 0x0003447C
        public void CommitVirtual()
        {
            SendRaw(Join(virtualBlocks));
            virtualBlocks.Clear();
        }

        // Token: 0x14000004 RID: 4
        // (add) Token: 0x06000A44 RID: 2628 RVA: 0x0003629C File Offset: 0x0003449C
        // (remove) Token: 0x06000A45 RID: 2629 RVA: 0x000362D0 File Offset: 0x000344D0
        public static event EventHandler<PlayerEventArgs> Joined;

        // Token: 0x14000005 RID: 5
        // (add) Token: 0x06000A46 RID: 2630 RVA: 0x00036304 File Offset: 0x00034504
        // (remove) Token: 0x06000A47 RID: 2631 RVA: 0x00036338 File Offset: 0x00034538
        public static event EventHandler<PlayerEventArgs> Disconnected;

        // Token: 0x14000006 RID: 6
        // (add) Token: 0x06000A48 RID: 2632 RVA: 0x0003636C File Offset: 0x0003456C
        // (remove) Token: 0x06000A49 RID: 2633 RVA: 0x000363A0 File Offset: 0x000345A0
        public static event EventHandler<ChatOtherEventArgs> ChatOther;

        // Token: 0x14000007 RID: 7
        // (add) Token: 0x06000A4A RID: 2634 RVA: 0x000363D4 File Offset: 0x000345D4
        // (remove) Token: 0x06000A4B RID: 2635 RVA: 0x00036408 File Offset: 0x00034608
        public static event EventHandler<MapChangeEventArgs> MapChanged;

        // Token: 0x14000008 RID: 8
        // (add) Token: 0x06000A5A RID: 2650 RVA: 0x00036530 File Offset: 0x00034730
        // (remove) Token: 0x06000A5B RID: 2651 RVA: 0x00036568 File Offset: 0x00034768
        private event FilterInput filterInput = delegate(ref string t, out bool h) { h = false; };

        // Token: 0x14000009 RID: 9
        // (add) Token: 0x06000A6D RID: 2669 RVA: 0x000366C0 File Offset: 0x000348C0
        // (remove) Token: 0x06000A6E RID: 2670 RVA: 0x000366F8 File Offset: 0x000348F8
        public event ProperBlockChangeEH BlockChangeProper;

        // Token: 0x1400000A RID: 10
        // (add) Token: 0x06000A6F RID: 2671 RVA: 0x00036730 File Offset: 0x00034930
        // (remove) Token: 0x06000A70 RID: 2672 RVA: 0x00036768 File Offset: 0x00034968
        public event BlockchangeEventHandler Blockchange;

        // Token: 0x1400000B RID: 11
        // (add) Token: 0x06000A71 RID: 2673 RVA: 0x000367A0 File Offset: 0x000349A0
        // (remove) Token: 0x06000A72 RID: 2674 RVA: 0x000367D8 File Offset: 0x000349D8
        public event BlockchangeEventHandler2 Blockchange2;

        // Token: 0x06000A73 RID: 2675 RVA: 0x00036810 File Offset: 0x00034A10
        public void ClearBlockchange()
        {
            Blockchange = null;
        }

        // Token: 0x06000A74 RID: 2676 RVA: 0x0003681C File Offset: 0x00034A1C
        public void ClearBlockchange2()
        {
            Blockchange2 = null;
        }

        // Token: 0x06000A75 RID: 2677 RVA: 0x00036828 File Offset: 0x00034A28
        public bool HasBlockchange()
        {
            return Blockchange == null;
        }

        // Token: 0x06000A76 RID: 2678 RVA: 0x00036834 File Offset: 0x00034A34
        public bool HasBlockchange2()
        {
            return Blockchange2 == null;
        }

        // Token: 0x1400000C RID: 12
        // (add) Token: 0x06000AA2 RID: 2722 RVA: 0x00036B20 File Offset: 0x00034D20
        // (remove) Token: 0x06000AA3 RID: 2723 RVA: 0x00036B54 File Offset: 0x00034D54
        public static event BlockPlacedUnderHandler PlayerPillared;

        // Token: 0x06000AA4 RID: 2724 RVA: 0x00036B88 File Offset: 0x00034D88
        public bool InitVariable<T>(string key, T value)
        {
            if (!extraData.ContainsKey(key))
            {
                ExtraData[key] = value;
                return true;
            }

            return false;
        }

        // Token: 0x06000AA5 RID: 2725 RVA: 0x00036BB0 File Offset: 0x00034DB0
        public T GetVariable<T>(string key)
        {
            return (T) ExtraData[key];
        }

        // Token: 0x06000AA6 RID: 2726 RVA: 0x00036BC4 File Offset: 0x00034DC4
        public void SetVariable<T>(string key, T value)
        {
            ExtraData[key] = value;
            extraData.Select(o => new
            {
                Name = o.Key
            });
        }

        // Token: 0x06000AA7 RID: 2727 RVA: 0x00036BF0 File Offset: 0x00034DF0
        public bool RemoveVariable(string key)
        {
            return ExtraData.Remove(key);
        }

        // Token: 0x06000AA8 RID: 2728 RVA: 0x00036C00 File Offset: 0x00034E00
        internal void OnPlayerPillared(Player p, int x, int y, int z, ref bool stopChange)
        {
            if (PlayerPillared != null) PlayerPillared(p, x, y, z, ref stopChange);
        }

        // Token: 0x06000AA9 RID: 2729 RVA: 0x00036C1C File Offset: 0x00034E1C
        public static void OnChatOther(ChatOtherEventArgs e)
        {
            var chatOther = ChatOther;
            if (chatOther != null) chatOther(null, e);
        }

        // Token: 0x06000AAA RID: 2730 RVA: 0x00036C3C File Offset: 0x00034E3C
        private static void Player_BlockPlacedUnder(Player p, int x, int y, int z, ref bool stopChange)
        {
            if (!p.level.IsPillaringAllowed && p.updatePosition)
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

            var num = y;
            var num2 = 0;
            while (p.level.IsSurroundedByAir(x, num, z))
            {
                num--;
                num2++;
            }

            if (GeneralSettings.All.PillarMaxHeight > 0 && GeneralSettings.All.PillarMaxHeight < num2 &&
                p.group.Permission < LevelPermission.Operator)
            {
                stopChange = true;
                SendMessage(p, Lang.Player.WarningTooHighPillar);
            }
        }

        // Token: 0x06000AAB RID: 2731 RVA: 0x00036D20 File Offset: 0x00034F20
        private void CheckIfFallsBack(BlockPos blockPos)
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

        // Token: 0x06000AAE RID: 2734 RVA: 0x00036E50 File Offset: 0x00035050
        public bool EqualsPlayerPosition(int x, int y, int z)
        {
            var num = -1;
            var num2 = -1;
            var num3 = XFloat - (int) XFloat;
            if (num3 > 0.6875f)
                num = (int) XFloat + 1;
            else if (num3 < 0.28125f) num = (int) XFloat - 1;
            num3 = ZFloat - (int) ZFloat;
            if (num3 > 0.6875f)
                num2 = (int) ZFloat + 1;
            else if (num3 < 0.28125f) num2 = (int) ZFloat - 1;
            if (num == -1 && num2 == -1 && (int) XFloat == x && (int) YFloat == y && (int) ZFloat == z) return true;
            if (num != -1 && num2 == -1)
            {
                if ((int) XFloat == x && (int) YFloat == y && (int) ZFloat == z) return true;
                if (num == x && (int) YFloat == y && (int) ZFloat == z) return true;
            }
            else if (num == -1 && num2 != -1)
            {
                if ((int) XFloat == x && (int) YFloat == y && (int) ZFloat == z) return true;
                if ((int) XFloat == x && (int) YFloat == y && num2 == z) return true;
            }
            else if (num == -1 && num2 == -1)
            {
                if ((int) XFloat == x && (int) YFloat == y && (int) ZFloat == z) return true;
            }
            else
            {
                if ((int) XFloat == x && (int) YFloat == y && (int) ZFloat == z) return true;
                if (num == x && (int) YFloat == y && (int) ZFloat == z) return true;
                if ((int) XFloat == x && (int) YFloat == y && num2 == z) return true;
                if (num == x && (int) YFloat == y && num2 == z) return true;
            }

            return false;
        }

        // Token: 0x06000AAF RID: 2735 RVA: 0x0003701C File Offset: 0x0003521C
        public BlockPos[] GetBlocksUnder()
        {
            if (level == null) return new BlockPos[0];
            var num = -1;
            var num2 = -1;
            var num3 = (int) (YFloat - 2.2f);
            var num4 = XFloat - (int) XFloat;
            if (num4 > 0.6875f)
                num = (int) XFloat + 1;
            else if (num4 < 0.28125f) num = (int) XFloat - 1;
            num4 = ZFloat - (int) ZFloat;
            if (num4 > 0.6875f)
                num2 = (int) ZFloat + 1;
            else if (num4 < 0.28125f) num2 = (int) ZFloat - 1;
            if (num != -1 && num2 == -1)
            {
                var list = new List<BlockPos>();
                if (!Block.IsAir(level.GetTile((int) XFloat, num3, (int) ZFloat)))
                    list.Add(new BlockPos((ushort) XFloat, (ushort) num3, (ushort) ZFloat));
                if (!Block.IsAir(level.GetTile(num, num3, (int) ZFloat)))
                    list.Add(new BlockPos((ushort) num, (ushort) num3, (ushort) ZFloat));
                return list.ToArray();
            }

            if (num == -1 && num2 != -1)
            {
                var list2 = new List<BlockPos>();
                if (!Block.IsAir(level.GetTile((int) XFloat, num3, (int) ZFloat)))
                    list2.Add(new BlockPos((ushort) XFloat, (ushort) num3, (ushort) ZFloat));
                if (!Block.IsAir(level.GetTile((int) XFloat, num3, num2)))
                    list2.Add(new BlockPos((ushort) XFloat, (ushort) num3, (ushort) num2));
                return list2.ToArray();
            }

            if (num == -1 && num2 == -1)
            {
                var list3 = new List<BlockPos>();
                if (!Block.IsAir(level.GetTile((ushort) XFloat, (ushort) num3, (ushort) ZFloat)))
                    list3.Add(new BlockPos((ushort) XFloat, (ushort) num3, (ushort) ZFloat));
                return list3.ToArray();
            }

            var list4 = new List<BlockPos>();
            if (!Block.IsAir(level.GetTile((int) XFloat, num3, (int) ZFloat)))
                list4.Add(new BlockPos((ushort) XFloat, (ushort) num3, (ushort) ZFloat));
            if (!Block.IsAir(level.GetTile(num, num3, (int) ZFloat)))
                list4.Add(new BlockPos((ushort) num, (ushort) num3, (ushort) ZFloat));
            if (!Block.IsAir(level.GetTile((int) XFloat, num3, num2)))
                list4.Add(new BlockPos((int) XFloat, num3, num2));
            if (!Block.IsAir(level.GetTile(num, num3, num2))) list4.Add(new BlockPos(num, num3, num2));
            return list4.ToArray();
        }

        // Token: 0x06000AB2 RID: 2738 RVA: 0x00037500 File Offset: 0x00035700
        internal static void OnPlayerJoined(object sender, PlayerEventArgs e)
        {
            var joined = Joined;
            if (joined != null) Joined(sender, e);
        }

        // Token: 0x06000AB3 RID: 2739 RVA: 0x00037524 File Offset: 0x00035724
        internal static void OnPlayerDisconnected(object sender, PlayerEventArgs e)
        {
            var eventHandler = Disconnected;
            if (eventHandler != null) Joined(sender, e);
        }

        // Token: 0x06000AB4 RID: 2740 RVA: 0x00037548 File Offset: 0x00035748
        public static void OnPlayerChatEvent(Player p, ref string message, ref bool stopIt)
        {
            if (PlayerChatEvent != null) PlayerChatEvent(p, ref message, ref stopIt);
        }

        // Token: 0x06000AB5 RID: 2741 RVA: 0x00037560 File Offset: 0x00035760
        internal static void OnPlayerRegisteredCheck(Player p, ref bool isRegistered)
        {
            if (PlayerRegisteredCheck != null) PlayerRegisteredCheck(p, ref isRegistered);
        }

        // Token: 0x06000AB7 RID: 2743 RVA: 0x00037654 File Offset: 0x00035854
        private static void players_PlayerRemoved(object sender, PlayerEventArgs e)
        {
        }

        // Token: 0x06000AB8 RID: 2744 RVA: 0x00037684 File Offset: 0x00035884
        private static void players_PlayerAdded(object sender, PlayerEventArgs e)
        {
        }

        // Token: 0x06000ABB RID: 2747 RVA: 0x00037E68 File Offset: 0x00036068
        private void Player_BlockChangeProper(Player p, ushort x, ushort y, ushort z, byte type, byte action,
            ref bool stopChange)
        {
            var sc = false;
            if (p.level.mapType == MapType.Zombie && InfectionSettings.All.DisallowSpleefing &&
                (p.group.Permission < LevelPermission.Operator || !InfectionSettings.All.OpsBypassSpleefPrevention))
            {
                players.ForEach(delegate(Player pl)
                {
                    if (sc) return;
                    if (pl != p && pl.level.mapType == MapType.Zombie && p.isZombie == pl.isZombie)
                        foreach (var blockPos in pl.GetBlocksUnder())
                            if (blockPos.x == x && blockPos.y == y && blockPos.z == z)
                            {
                                sc = true;
                                return;
                            }
                });
                if (sc) SendMessage(p, "Don't spleef your team members.");
            }

            if (p.level.mapType == MapType.Lava && LavaSettings.All.DisallowSpleefing &&
                (p.group.Permission < LevelPermission.Operator || !LavaSettings.All.OpsBypassSpleefPrevention))
            {
                players.ForEach(delegate(Player pl)
                {
                    if (sc) return;
                    if (pl != p && pl.level.mapType == MapType.Lava)
                        foreach (var blockPos in pl.GetBlocksUnder())
                            if (blockPos.x == x && blockPos.y == y && blockPos.z == z)
                            {
                                sc = true;
                                return;
                            }
                });
                if (sc) SendMessage(p, "Don't spleef other players.");
            }

            stopChange = sc;
        }

        // Token: 0x06000ABC RID: 2748 RVA: 0x00037F90 File Offset: 0x00036190
        public void Save()
        {
            if (databaseLoadFailure) return;
            flags = flagsCollection.FlagContainer;
            try
            {
                var queryString = string.Concat("UPDATE Players SET IP='", ip, "', LastLogin='",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "', totalLogin=", totalLogins, ", totalDeaths=",
                    overallDeath, ", Money=", money, ", totalBlocks=", overallBlocks, " + ", loginBlocks,
                    ", totalKicked=", totalKicked, ", totalScore=", totalScore, ", bestScore=", bestScore,
                    ", timesWon=", timesWon, ", totalMinutesPlayed=", TotalMinutesPlayed, ", flags=", flags,
                    ", playerExperienceOnZombie=", PlayerExperienceOnZombie, ", ", DBManager.KeyType_wonAsHumanTimes[0],
                    "=", WonAsHumanTimes, ", ", DBManager.KeyType_wonAsZombieTimes[0], "=", WonAsZombieTimes, ", ",
                    DBManager.KeyType_zombifiedCount[0], "=", ZombifiedCount, ", ", DBPlayerColumns.RoundsOnZombie.Name,
                    "=", RoundsOnZombie, " WHERE Name='", name, "'");
                DBInterface.ExecuteQuery(queryString);
                if (!smileySaved)
                {
                    if (parseSmiley)
                        emoteList.RemoveAll(s => s == name);
                    else
                        emoteList.Add(name);
                    File.WriteAllLines("text/emotelist.txt", emoteList.ToArray());
                    smileySaved = true;
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        // Token: 0x06000ABD RID: 2749 RVA: 0x00038268 File Offset: 0x00036468
        private static void Receive(IAsyncResult result)
        {
            var player = (Player) result.AsyncState;
            if (player.disconnected || player.socket == null) return;
            try
            {
                var num = player.socket.EndReceive(result);
                if (num == 0)
                {
                    player.Disconnect();
                }
                else
                {
                    var dst = new byte[player.buffer.Length + num];
                    Buffer.BlockCopy(player.buffer, 0, dst, 0, player.buffer.Length);
                    Buffer.BlockCopy(player.tempbuffer, 0, dst, player.buffer.Length, num);
                    player.buffer = player.HandleMessage(dst);
                    player.socket.BeginReceive(player.tempbuffer, 0, player.tempbuffer.Length, SocketFlags.None,
                        Receive, player);
                }
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
            catch (NullReferenceException)
            {
            }
            catch (Exception ex2)
            {
                Server.ErrorLog(ex2);
                player.Kick("Error!");
            }
        }

        // Token: 0x06000ABE RID: 2750 RVA: 0x000383B4 File Offset: 0x000365B4
        private byte[] HandleMessage(byte[] buffer)
        {
            try
            {
                var b = buffer[0];
                var b2 = b;
                int num;
                if (b2 <= 5)
                {
                    if (b2 == 0)
                    {
                        num = 130;
                        goto IL_A6;
                    }

                    if (b2 == 5)
                        if (loggedIn)
                        {
                            num = 8;
                            goto IL_A6;
                        }
                }
                else if (b2 != 8)
                {
                    switch (b2)
                    {
                        case 13:
                            if (loggedIn)
                            {
                                num = 65;
                                goto IL_A6;
                            }

                            break;
                        case 16:
                            num = 66;
                            goto IL_A6;
                        case 17:
                            num = 68;
                            goto IL_A6;
                        case 19:
                            num = 1;
                            goto IL_A6;
                    }
                }
                else if (loggedIn)
                {
                    num = 9;
                    goto IL_A6;
                }

                Kick(string.Format("Unhandled message id \"{0}\"!", b));
                return new byte[0];
                IL_A6:
                if (buffer.Length > num)
                {
                    var array = new byte[num];
                    Buffer.BlockCopy(buffer, 1, array, 0, num);
                    var array2 = new byte[buffer.Length - num - 1];
                    Buffer.BlockCopy(buffer, num + 1, array2, 0, buffer.Length - num - 1);
                    buffer = array2;
                    var b3 = b;
                    if (b3 <= 5)
                    {
                        if (b3 != 0)
                        {
                            if (b3 == 5)
                                if (loggedIn)
                                    HandleBlockchange(array);
                        }
                        else
                        {
                            HandleLogin(array);
                        }
                    }
                    else if (b3 != 8)
                    {
                        switch (b3)
                        {
                            case 13:
                                if (loggedIn) HandleChat(array);
                                break;
                            case 16:
                                HandleExtInfo(array);
                                break;
                            case 17:
                                HandleExtEntry(array);
                                break;
                        }
                    }
                    else if (loggedIn)
                    {
                        HandlePositionInfo(array);
                    }

                    if (buffer.Length <= 0) return new byte[0];
                    buffer = HandleMessage(buffer);
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }

            return buffer;
        }

        // Token: 0x06000ABF RID: 2751 RVA: 0x00038578 File Offset: 0x00036778
        private void HandleExtEntry(byte[] message)
        {
            var bytes = message.Take(64).ToArray();
            var text = Encoding.ASCII.GetString(bytes).Trim();
            var value = message.Skip(64).Take(4).ToArray();
            var num = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(value, 0));
            Dictionary<string, int> dictionary = null;
            if (!ExtraData.ContainsKey("supported_extensions"))
            {
                dictionary = new Dictionary<string, int>();
                ExtraData["supported_extensions"] = dictionary;
            }
            else
            {
                dictionary = (Dictionary<string, int>) ExtraData["supported_extensions"];
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

            if (text == "Blockmension") V1.JsonData(this, "[{\"experimental\":{\"flags\":\"portal-blocks-enable\"}}]");
            if (text == "TextHotKey" && Cpe.TextHotKey == 1)
                foreach (var availablePlugin in Server.Plugins.AvailablePlugins)
                {
                    if (availablePlugin.Instance.GetType() != typeof(PluginKeyboardShortcuts)) continue;
                    var pluginKeyboardShortcuts = (PluginKeyboardShortcuts) availablePlugin.Instance;
                    foreach (var shortcut in pluginKeyboardShortcuts.GetShortcuts())
                        if (shortcut.Command.Length <= 64 &&
                            ServerProperties.ValidString(shortcut.Command, "%![]:.,{}~-+()?_/\\^*#@$~`\"'|=;<>& "))
                        {
                            var cpeHotKeyInfo = WpfToLwjglKeyMap.ToCpeHotKey(shortcut.Shortcut);
                            V1.SetTextHotKey(this, "Shortcut", shortcut.Command, cpeHotKeyInfo.KeyCode,
                                cpeHotKeyInfo.KeyMod);
                        }
                }

            if (Interlocked.Decrement(ref extToRead) == 0) HandleLoginPart2();
        }

        // Token: 0x06000AC0 RID: 2752 RVA: 0x00038918 File Offset: 0x00036B18
        private void HandleExtInfo(byte[] message)
        {
            var bytes = message.Take(64).ToArray();
            var text = Encoding.ASCII.GetString(bytes).Trim();
            if (Debug_NotifyConsolAboutPlayersClient)
                Server.s.Log(string.Concat("Player ", name, " uses ", text, " client."));
            ExtraData["app_name"] = text;
            var value = message.Skip(64).Take(2).ToArray();
            var num = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(value, 0));
            ExtraData["extensions_count"] = num;
            extToRead = num;
        }

        // Token: 0x06000AC1 RID: 2753 RVA: 0x000389E0 File Offset: 0x00036BE0
        private void HandleBlockSupport(byte[] message)
        {
        }

        // Token: 0x06000AC4 RID: 2756 RVA: 0x000389F8 File Offset: 0x00036BF8
        private void HandleLogin(byte[] message)
        {
            try
            {
                if (!loggedIn)
                {
                    var b = message[0];
                    name = enc.GetString(message, 1, 64).Trim();
                    PublicName = RemoveEmailDomain(name);
                    var verificationHash = enc.GetString(message, 65, 32).Trim();
                    var b2 = message[129];
                    if (b2 == 66) IsCpeSupported = true;
                    if (b2 == 44) IsUsingXWom = true;
                    if (Server.useWhitelist)
                    {
                        if (Server.verify)
                        {
                            if (Server.whiteList.Contains(name)) onWhitelist = true;
                        }
                        else
                        {
                            try
                            {
                                using (var dataTable = DBInterface.fillData("SELECT Name FROM Players WHERE IP = @IP",
                                    new Dictionary<string, object>
                                    {
                                        {
                                            "@IP",
                                            ip
                                        }
                                    }))
                                {
                                    if (dataTable.Rows.Count > 0 && dataTable.Rows.Contains(name) &&
                                        Server.whiteList.Contains(name)) onWhitelist = true;
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
                            Kick(Lang.Player.KickServerFull);
                            return;
                        }

                        if (Server.vipSystem == 0 && ip != "127.0.0.1")
                        {
                            disconnectionReason = DisconnectionReason.ServerFull;
                            Kick(Lang.Player.KickServerFull);
                            return;
                        }
                    }

                    if (b != 7)
                    {
                        Kick(Lang.Player.KickWrongVersion);
                    }
                    else if (name.Length > 63 || !ValidName(name))
                    {
                        disconnectionReason = DisconnectionReason.IllegalName;
                        Kick(Lang.Player.KickIllegalName);
                    }
                    else
                    {
                        if (Server.verify)
                        {
                            Authentication = VerifyPlayer(verificationHash);
                            if (Authentication == AuthenticationProvider.Unknown)
                            {
                                if (GeneralSettings.All.VerifyNameForLocalIPs)
                                {
                                    disconnectionReason = DisconnectionReason.AuthenticationFailure;
                                    Kick(Lang.Player.KickLoginFailed);
                                    return;
                                }

                                if (!IsIPLocal())
                                {
                                    disconnectionReason = DisconnectionReason.AuthenticationFailure;
                                    Kick(Lang.Player.KickLoginFailed);
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

                                var text = name;
                                name += "+";
                                if (GeneralSettings.All.PlusMarkerForClassiCubeAccount)
                                    PublicName = name;
                                else
                                    PublicName = text;
                            }
                        }

                        try
                        {
                            var item = Server.tempBans.Find(tB => tB.name.ToLower() == name.ToLower());
                            if (item.allowedJoin < DateTime.Now)
                            {
                                Server.tempBans.Remove(item);
                            }
                            else
                            {
                                disconnectionReason = DisconnectionReason.TempBan;
                                Kick(Lang.Player.KickTempBan);
                            }
                        }
                        catch
                        {
                        }

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
                                        p.Kick(Lang.Player.KickLoggedAsYou);
                                        return;
                                    }

                                    Kick(Lang.Player.KickAlreadyLogged);
                                }
                            });
                        }
                        catch
                        {
                        }

                        try
                        {
                            lock (playersThatLeftLocker)
                            {
                                playersThatLeft.Remove(name.ToLower());
                            }
                        }
                        catch
                        {
                        }

                        group = Group.findPlayerGroup(name);
                        if (IsCpeSupported)
                        {
                            SendRaw(new Packets().MakeExtInfo("MCDzienny", 3));
                            SendRaw(new Packets().MakeExtEntry("CustomBlocks", 1));
                            SendRaw(new Packets().MakeExtEntry("MessageTypes", 1));
                            SendRaw(new Packets().MakeExtEntry("HeldBlock", 1));
                            SendRaw(new byte[]
                            {
                                19,
                                1
                            });
                        }
                        else
                        {
                            HandleLoginPart2();
                        }
                    }
                }
            }
            catch (Exception ex2)
            {
                Server.ErrorLog(ex2);
            }
        }

        // Token: 0x06000AC5 RID: 2757 RVA: 0x00038F94 File Offset: 0x00037194
        private void HandleLoginPart2()
        {
            try
            {
                SendMotd();
                SendMap();
                id = GetFreeId();
                if (disconnected) return;
                loggedIn = true;
                IRCSay(string.Format(Lang.Player.JoinGlobalMessage, PublicName));
                var text = Lang.Player.LatelyKnownAs;
                var flag = false;
                if (ip != "127.0.0.1")
                {
                    lock (playersThatLeftLocker)
                    {
                        foreach (var item in playersThatLeft)
                            if (item.Value == ip)
                            {
                                flag = true;
                                text = text + " " + item.Key;
                            }
                    }

                    if (flag)
                    {
                        GlobalMessageOps(text);
                        Server.s.Log(text);
                        IRCSay(text, true);
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
                    using (var dataTable = DBInterface.fillData("SELECT * FROM Players WHERE Name='" + name + "'"))
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
                            SendMessage(string.Format(Lang.Player.WelcomeFirstVisit, PublicName));
                            using (var dataTable2 = DBInterface.fillData(
                                "INSERT INTO Players (Name, IP, FirstLogin, LastLogin, totalLogin, Title, totalDeaths, Money, totalBlocks, totalKicked, totalScore, bestScore, timesWon, welcomeMessage, farewellMessage, totalMinutesPlayed, flags, playerExperienceOnZombie, " +
                                DBManager.KeyType_wonAsHumanTimes[0] + ", " + DBManager.KeyType_wonAsZombieTimes[0] +
                                ", " + DBManager.KeyType_zombifiedCount[0] + ")VALUES ('" + name + "', '" + ip +
                                "', '" + firstLogin.ToString("yyyy-MM-dd HH:mm:ss") + "', '" +
                                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " + totalLogins + ", '" + prefix +
                                "', " + overallDeath + ", " + money + ", " + loginBlocks + ", " + totalKicked + ", " +
                                totalScore + ", " + bestScore + ", " + timesWon + ", '" + welcomeMessage + "', '" +
                                farewellMessage + "', " + TotalMinutesPlayed + ", " + flags + ", " +
                                PlayerExperienceOnZombie + ", " + WonAsHumanTimes + ", " + WonAsZombieTimes + ", " +
                                ZombifiedCount + ");SELECT LAST_INSERT_ROWID() AS Id;"))
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
                            int.TryParse(dataTable.Rows[0]["playerExperienceOnZombie"].ToString(),
                                out playerExperienceOnZombie);
                            int.TryParse(dataTable.Rows[0][DBManager.KeyType_wonAsHumanTimes[0]].ToString(),
                                out wonAsHumanTimes);
                            int.TryParse(dataTable.Rows[0][DBManager.KeyType_wonAsZombieTimes[0]].ToString(),
                                out wonAsZombieTimes);
                            int.TryParse(dataTable.Rows[0][DBManager.KeyType_zombifiedCount[0]].ToString(),
                                out zombifiedCount);
                            int.TryParse(dataTable.Rows[0][DBPlayerColumns.RoundsOnZombie.Name].ToString(),
                                out roundsOnZombie);
                            if (dataTable.Rows[0]["Title"].ToString().Trim() != "")
                            {
                                var text2 = dataTable.Rows[0]["Title"].ToString().Trim().Replace("[", "");
                                title = text2.Replace("]", "");
                            }

                            if (dataTable.Rows[0]["title_color"].ToString().Trim() != "")
                                titlecolor = c.Parse(dataTable.Rows[0]["title_color"].ToString().Trim());
                            else
                                titlecolor = "";
                            if (dataTable.Rows[0]["color"].ToString().Trim() != "")
                                color = c.Parse(dataTable.Rows[0]["color"].ToString().Trim());
                            else
                                color = group.color;
                            SetPrefix();
                            overallDeath = int.Parse(dataTable.Rows[0]["TotalDeaths"].ToString());
                            overallBlocks = int.Parse(dataTable.Rows[0]["totalBlocks"].ToString().Trim());
                            money = int.Parse(dataTable.Rows[0]["Money"].ToString());
                            totalKicked = int.Parse(dataTable.Rows[0]["totalKicked"].ToString());
                            TierSystem.TierSet(this);
                            TierSystem.ColorSet(this);
                            TierSystem.GiveItems(this);
                            SendMessage2(0,
                                string.Format(Lang.Player.WelcomeAnotherVisit,
                                    color + prefix + color + PublicName + Server.DefaultColor, totalLogins));
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
                    using (var dataTable3 = DBInterface.fillData("SELECT * FROM Stars WHERE Name='" + name + "'"))
                    {
                        if (dataTable3.Rows.Count == 0)
                        {
                            DBInterface.ExecuteQuery(
                                "INSERT INTO Stars (Name, GoldStars, SilverStars, BronzeStars, RottenStars) VALUES ('" +
                                name + "', 0, 0, 0, 0);");
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
                    Server.s.Log(
                        "Error while loading stars count from the database. This error may cause a loss of player's star stats.");
                    Server.ErrorLog(ex3);
                }
            }
            else
            {
                try
                {
                    using (var dataTable4 = DBInterface.fillData("SELECT * FROM Players WHERE Name='" + name + "'"))
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
                            SendMessage(string.Format(Lang.Player.WelcomeFirstVisit, PublicName));
                            using (var dataTable5 = DBInterface.fillData(
                                "INSERT INTO Players (Name, IP, FirstLogin, LastLogin, totalLogin, Title, totalDeaths, Money, totalBlocks, totalKicked, totalScore, bestScore, timesWon, welcomeMessage, farewellMessage, totalMinutesPlayed, flags)VALUES ('" +
                                name + "', '" + ip + "', '" + firstLogin.ToString("yyyy-MM-dd HH:mm:ss") + "', '" +
                                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " + totalLogins + ", '" + prefix +
                                "', " + overallDeath + ", " + money + ", " + loginBlocks + ", " + totalKicked + ", " +
                                totalScore + ", " + bestScore + ", " + timesWon + ", '" + welcomeMessage + "', '" +
                                farewellMessage + "', " + totalMinutesPlayed + ", " + flags +
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
                            int.TryParse(dataTable4.Rows[0]["playerExperienceOnZombie"].ToString(),
                                out playerExperienceOnZombie);
                            int.TryParse(dataTable4.Rows[0][DBManager.KeyType_wonAsHumanTimes[0]].ToString(),
                                out wonAsHumanTimes);
                            int.TryParse(dataTable4.Rows[0][DBManager.KeyType_wonAsZombieTimes[0]].ToString(),
                                out wonAsZombieTimes);
                            int.TryParse(dataTable4.Rows[0][DBManager.KeyType_zombifiedCount[0]].ToString(),
                                out zombifiedCount);
                            int.TryParse(dataTable4.Rows[0][DBPlayerColumns.RoundsOnZombie.Name].ToString(),
                                out roundsOnZombie);
                            if (dataTable4.Rows[0]["Title"].ToString().Trim() != "")
                            {
                                var text3 = dataTable4.Rows[0]["Title"].ToString().Trim().Replace("[", "");
                                title = text3.Replace("]", "");
                            }

                            if (dataTable4.Rows[0]["title_color"].ToString().Trim() != "")
                                titlecolor = c.Parse(dataTable4.Rows[0]["title_color"].ToString().Trim());
                            else
                                titlecolor = "";
                            if (dataTable4.Rows[0]["color"].ToString().Trim() != "")
                                color = c.Parse(dataTable4.Rows[0]["color"].ToString().Trim());
                            else
                                color = group.color;
                            SetPrefix();
                            overallDeath = int.Parse(dataTable4.Rows[0]["TotalDeaths"].ToString());
                            overallBlocks = int.Parse(dataTable4.Rows[0]["totalBlocks"].ToString().Trim());
                            money = int.Parse(dataTable4.Rows[0]["Money"].ToString());
                            totalKicked = int.Parse(dataTable4.Rows[0]["totalKicked"].ToString());
                            TierSystem.TierSet(this);
                            TierSystem.ColorSet(this);
                            TierSystem.GiveItems(this);
                            SendMessage(string.Format(Lang.Player.WelcomeAnotherVisit,
                                color + prefix + PublicName + Server.DefaultColor, totalLogins));
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
                    using (var dataTable6 = DBInterface.fillData("SELECT * FROM Stars WHERE Name='" + name + "'"))
                    {
                        if (dataTable6.Rows.Count == 0)
                        {
                            DBInterface.ExecuteQuery(
                                "INSERT INTO Stars (Name, GoldStars, SilverStars, BronzeStars, RottenStars) VALUES ('" +
                                name + "', 0, 0, 0, 0);");
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
                    Server.s.Log(
                        "Error while loading stars count from the database. This error may cause a loss of player's star stats.");
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
                var num = (ushort) ((0.5 + level.spawnx) * 32.0);
                var num2 = (ushort) ((1 + level.spawny) * 32);
                var num3 = (ushort) ((0.5 + level.spawnz) * 32.0);
                pos = new ushort[3]
                {
                    num,
                    num2,
                    num3
                };
                rot = new byte[2]
                {
                    level.rotx,
                    level.roty
                };
                GlobalSpawn(this, num, num2, num3, rot[0], rot[1], true);
                SendSpawn(byte.MaxValue, color + (IsRefree ? "[REF]" : "") + PublicName, ModelName, num, num2, num3,
                    rot[0], rot[1]);
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
                if (color == Group.standard.color) color = "&9";
                if (prefix == "") title = "Dev";
                SetPrefix();
            }

            Loading = false;
            fullyloggedtime = DateTime.Now;
            fullylogged = true;
            if (!players.Contains(this)) players.Add(this);
            Server.s.PlayerListUpdate();
            if (emoteList.Contains(name)) parseSmiley = false;
            if (welcomeMessage == "")
                GlobalChatWorld(null,
                    string.Format(MessagesManager.GetString("GlobalMessagePlayerJoined"),
                        "&a+ " + color + prefix + color + PublicName + Server.DefaultColor), false);
            else
                GlobalChatWorld(null, "&a+ " + color + prefix + PublicName + Server.DefaultColor + " " + welcomeMessage,
                    false);
            Server.s.Log(string.Format("{0} [{1}] has joined the server.", name, ip));
            OnPlayerJoined(null, new PlayerEventArgs(this));
        }

        // Token: 0x06000AC6 RID: 2758 RVA: 0x0003A890 File Offset: 0x00038A90
        private AuthenticationProvider VerifyPlayer(string verificationHash)
        {
            if (VerifyPlayer(verificationHash, Server.Salt)) return AuthenticationProvider.Mojang;
            if (VerifyPlayer(verificationHash, Server.SaltClassiCube)) return AuthenticationProvider.ClassiCube;
            return AuthenticationProvider.Unknown;
        }

        // Token: 0x06000AC7 RID: 2759 RVA: 0x0003A8B4 File Offset: 0x00038AB4
        public static string RemoveEmailDomain(string name)
        {
            if (name.Contains('@')) name = name.Remove(name.IndexOf('@') + 1);
            return name;
        }

        // Token: 0x06000AC8 RID: 2760 RVA: 0x0003A8D4 File Offset: 0x00038AD4
        public bool VerifyPlayer(string hash, string salt)
        {
            if (salt.IsNullOrWhiteSpaced() || salt.Length < 16) return false;
            hash = hash.PadLeft(32, '0');
            var value = BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(enc.GetBytes(salt + name)))
                .Replace("-", "");
            return hash.Equals(value, StringComparison.OrdinalIgnoreCase);
        }

        // Token: 0x06000AC9 RID: 2761 RVA: 0x0003A944 File Offset: 0x00038B44
        public void TestConnection()
        {
        }

        // Token: 0x06000ACA RID: 2762 RVA: 0x0003A948 File Offset: 0x00038B48
        public void SetPrefix()
        {
            prefix = title == "" ? "" :
                titlecolor == "" ? "[" + title + "]" : string.Concat("[", titlecolor, title, color, "]");
        }

        // Token: 0x06000ACB RID: 2763 RVA: 0x0003A9D8 File Offset: 0x00038BD8
        private void HandleBlockchange(byte[] message)
        {
            try
            {
                if (loggedIn)
                {
                    var x = NTHO(message, 0);
                    var y = NTHO(message, 2);
                    var z = NTHO(message, 4);
                    var action = message[6];
                    var type = message[7];
                    manualChange(x, y, z, action, type);
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        // Token: 0x06000ACC RID: 2764 RVA: 0x0003AA3C File Offset: 0x00038C3C
        public void manualChange(ushort x, ushort y, ushort z, byte action, byte type)
        {
            if (type > 80)
            {
                Kick("Unknown block type!");
                return;
            }

            var tile = level.GetTile(x, y, z);
            var flag = false;
            if (action == 0)
            {
                if (level.CommandActionsHit != null && level.CommandActionsHit.ContainsKey(level.PosToInt(x, y, z)))
                {
                    if (level.CommandActionsHit[level.PosToInt(x, y, z)].changeAction == ChangeAction.Restore &&
                        type != tile)
                    {
                        SendBlockchange(x, y, z, tile);
                        flag = true;
                    }

                    HandleCommandAction(level.CommandActionsHit[level.PosToInt(x, y, z)]);
                    if (flag) return;
                }
            }
            else if (action == 1 && level.CommandActionsBuild != null &&
                     level.CommandActionsBuild.ContainsKey(level.PosToInt(x, y, z)))
            {
                if (level.CommandActionsHit[level.PosToInt(x, y, z)].changeAction == ChangeAction.Restore &&
                    type != tile)
                {
                    SendBlockchange(x, y, z, tile);
                    flag = true;
                }

                HandleCommandAction(level.CommandActionsBuild[level.PosToInt(x, y, z)]);
                if (flag) return;
            }

            var flag2 = false;
            if (action == 1 && IsBlockPlacedUnderPlayer(x, y, z) && IsPlacedBlockOnlyBlockUnderPlayer(y))
                OnPlayerPillared(this, x, y, z, ref flag2);
            if (flag2)
            {
                if (!flag) SendBlockchange(x, y, z, tile);
                return;
            }

            if (tile == 255) return;
            if (jailed)
            {
                if (!flag) SendBlockchange(x, y, z, tile);
                return;
            }

            if (level.name.Contains("Museum " + Server.DefaultColor) && Blockchange == null) return;
            if (!deleteMode)
            {
                var text = level.foundInfo(x, y, z);
                if (text.Contains("wait")) return;
            }

            if (!canBuild)
            {
                if (!flag) SendBlockchange(x, y, z, tile);
                return;
            }

            if (level.mapType == MapType.MyMap && !level.IsPublic && group.Permission < LevelPermission.Operator &&
                level.Owner.ToLower() != name.ToLower() && !level.AllowedPlayers.Contains(name.ToLower()))
            {
                SendMessage(this, "You are not allowed to build on this map. Ask the owner for permission.");
                if (!flag) SendBlockchange(x, y, z, tile);
                return;
            }

            if (level.mapType == MapType.Zombie && Block.OPBlocks(tile))
            {
                if (!flag) SendBlockchange(x, y, z, tile);
                SendMessage("Cannot build here.");
                return;
            }

            if (level.mapType == MapType.Zombie &&
                !InfectionSystem.InfectionSystem.currentInfectionMap.IsBuildingAllowed && !Block.IsDoor(tile))
            {
                if (!flag)
                {
                    SendBlockchange(x, y, z, tile);
                    SendMessage(this, "Building is not allowed on this map.");
                }

                return;
            }

            Level.BlockPos item;
            item.name = name;
            item.TimePerformed = DateTime.Now;
            item.x = x;
            item.y = y;
            item.z = z;
            item.type = type;
            lastClick[0] = x;
            lastClick[1] = y;
            lastClick[2] = z;
            var flag3 = false;
            OnBlockChangeProper(this, x, y, z, type, action, ref flag3);
            if (flag3)
            {
                SendBlockchange(x, y, z, level.GetTile(x, y, z));
                return;
            }

            if (Blockchange != null && Blockchange2 != null)
            {
                if (Blockchange.Method.ToString().IndexOf("AboutBlockchange") == -1 &&
                    !level.name.Contains("Museum " + Server.DefaultColor))
                {
                    item.deleted = true;
                    level.blockCache.Add(item);
                }

                Blockchange(this, x, y, z, type);
                if (Blockchange2.Method.ToString().IndexOf("AboutBlockchange") == -1 &&
                    !level.name.Contains("Museum " + Server.DefaultColor))
                {
                    item.deleted = true;
                    level.blockCache.Add(item);
                }

                Blockchange2(this, x, y, z, type, action);
                return;
            }

            if (Blockchange != null)
            {
                if (Blockchange.Method.ToString().IndexOf("AboutBlockchange") == -1 &&
                    !level.name.Contains("Museum " + Server.DefaultColor))
                {
                    item.deleted = true;
                    level.blockCache.Add(item);
                }

                Blockchange(this, x, y, z, type);
                return;
            }

            if (Blockchange2 != null)
            {
                if (Blockchange2.Method.ToString().IndexOf("AboutBlockchange") == -1 &&
                    !level.name.Contains("Museum " + Server.DefaultColor))
                {
                    item.deleted = true;
                    level.blockCache.Add(item);
                }

                Blockchange2(this, x, y, z, type, action);
                return;
            }

            if (group.Permission == LevelPermission.Banned) return;
            if (group.Permission == LevelPermission.Guest)
            {
                var num = Math.Abs(pos[0] / 32 - x);
                num += Math.Abs(pos[1] / 32 - y);
                num += Math.Abs(pos[2] / 32 - z);
                if (num > 12 && LavaSettings.All.ShowDistanceOffsetMessage)
                {
                    Server.s.Log(string.Format("{0} attempted to build with a {1} distance offset.", name, num));
                    SendMessage(Lang.Player.WarningBuiltTooFar);
                    if (!flag) SendBlockchange(x, y, z, tile);
                    return;
                }

                if (Server.antiTunnel && !ignoreGrief && y < level.height / 2 - Server.maxDepth)
                {
                    SendMessage(Lang.Player.WarningBuiltTooLow);
                    if (!flag) SendBlockchange(x, y, z, tile);
                    return;
                }
            }

            if (!Block.canPlace(this, tile) && !Block.BuildIn(tile) && !Block.AllowBreak(tile))
            {
                SendMessage(Lang.Player.WarningCantBuildHere);
                if (!flag) SendBlockchange(x, y, z, tile);
                return;
            }

            if (!Block.canPlace(this, type))
            {
                SendMessage(Lang.Player.WarningDisallowedBlockType);
                if (!flag) SendBlockchange(x, y, z, tile);
                return;
            }

            if (tile >= 200 && tile < 220)
            {
                SendMessage(Lang.Player.WarningCantDisturbBlock);
                if (flag) SendBlockchange(x, y, z, tile);
                return;
            }

            if (action > 1) Kick(Lang.Player.KickUnknownAction);
            if (level.GetTile(x, y, z) == 97) LavaSystem.FoundTreasure(this, x, y, z);
            var b = type;
            type = bindings[type];
            if (tile == (painting || action == 1 ? type : 0))
            {
                if (painting || b != type) SendBlockchange(x, y, z, tile);
                return;
            }

            if (!painting && action == 0)
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
                return;
            }

            item.deleted = false;
            level.blockCache.Add(item);
            placeBlock(tile, type, x, y, z);
        }

        // Token: 0x06000ACD RID: 2765 RVA: 0x0003B23C File Offset: 0x0003943C
        private bool IsPlacedBlockOnlyBlockUnderPlayer(ushort y)
        {
            var flag = false;
            foreach (var blockPos in GetBlocksUnder())
                if (!Block.Walkthrough(level.GetTile(blockPos.x, y, blockPos.z)))
                    flag = true;
            return !flag;
        }

        // Token: 0x06000ACE RID: 2766 RVA: 0x0003B298 File Offset: 0x00039498
        private bool IsBlockPlacedUnderPlayer(ushort x, ushort y, ushort z)
        {
            return EqualsPlayerPosition(x, y + 2, z) || EqualsPlayerPosition(x, y + 3, z) ||
                   EqualsPlayerPosition(x, y + 4, z);
        }

        // Token: 0x06000ACF RID: 2767 RVA: 0x0003B2C4 File Offset: 0x000394C4
        public void ManualChangeCheck(ushort x, ushort y, ushort z, byte action, byte type)
        {
            Level.BlockPos item;
            item.name = name;
            item.TimePerformed = DateTime.Now;
            item.x = x;
            item.y = y;
            item.z = z;
            item.type = type;
            var tile = level.GetTile(x, y, z);
            if (action == 0)
            {
                if (level.CommandActionsHit != null && level.CommandActionsHit.ContainsKey(level.PosToInt(x, y, z)))
                {
                    if (level.CommandActionsHit[level.PosToInt(x, y, z)].changeAction == ChangeAction.Restore &&
                        type != tile) SendBlockchange(x, y, z, tile);
                    HandleCommandAction(level.CommandActionsHit[level.PosToInt(x, y, z)]);
                }
            }
            else if (action == 1 && level.CommandActionsBuild != null &&
                     level.CommandActionsBuild.ContainsKey(level.PosToInt(x, y, z)))
            {
                if (level.CommandActionsHit[level.PosToInt(x, y, z)].changeAction == ChangeAction.Restore &&
                    type != tile) SendBlockchange(x, y, z, tile);
                HandleCommandAction(level.CommandActionsBuild[level.PosToInt(x, y, z)]);
            }

            if (group.Permission == LevelPermission.Banned) return;
            if (group.Permission == LevelPermission.Guest)
            {
                var num = Math.Abs(pos[0] / 32 - x);
                num += Math.Abs(pos[1] / 32 - y);
                num += Math.Abs(pos[2] / 32 - z);
                if (num > 12 && LavaSettings.All.ShowDistanceOffsetMessage)
                {
                    Server.s.Log(string.Format("{0} attempted to build with a {1} distance offset.", name, num));
                    GlobalMessageOps(string.Format("To Ops &f-{0}&f- attempted to build with a {1} distance offset.",
                        color + name, num));
                    SendMessage(Lang.Player.WarningBuiltTooFar);
                    SendBlockchange(x, y, z, tile);
                    return;
                }

                if (Server.antiTunnel && !ignoreGrief && y < level.height / 2 - Server.maxDepth)
                {
                    SendMessage(Lang.Player.WarningBuiltTooLow);
                    SendBlockchange(x, y, z, tile);
                    return;
                }
            }

            if (!Block.canPlace(this, tile) && !Block.BuildIn(tile) && !Block.AllowBreak(tile))
            {
                SendMessage(Lang.Player.WarningCantBuildHere);
                SendBlockchange(x, y, z, tile);
                return;
            }

            if (!Block.canPlace(this, type))
            {
                SendMessage(Lang.Player.WarningDisallowedBlockType);
                SendBlockchange(x, y, z, tile);
                return;
            }

            if (tile >= 200 && tile < 220)
            {
                SendMessage(Lang.Player.WarningCantDisturbBlock);
                SendBlockchange(x, y, z, tile);
                return;
            }

            if (action > 1) Kick(Lang.Player.KickUnknownAction);
            if (level.GetTile(x, y, z) == 97) LavaSystem.FoundTreasure(this, x, y, z);
            var b = type;
            type = bindings[type];
            if (tile == (painting || action == 1 ? type : 0))
            {
                if (painting || b != type) SendBlockchange(x, y, z, tile);
                return;
            }

            if (!painting && action == 0)
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
                return;
            }

            item.deleted = false;
            level.blockCache.Add(item);
            placeBlock(tile, type, x, y, z);
        }

        // Token: 0x06000AD0 RID: 2768 RVA: 0x0003B6EC File Offset: 0x000398EC
        public void HandlePortal(Player p, ushort x, ushort y, ushort z, byte b)
        {
            try
            {
                string queryString;
                if (p.level.mapType == MapType.MyMap)
                    queryString = string.Concat("SELECT * FROM `Portals` WHERE Map=", p.level.MapDbId, " AND EntryX=",
                        x, " AND EntryY=", y, " AND EntryZ=", z);
                else
                    queryString = string.Concat("SELECT * FROM `Portals", level.name, "` WHERE EntryX=", (int) x,
                        " AND EntryY=", (int) y, " AND EntryZ=", (int) z);
                using (var dataTable = DBInterface.fillData(queryString))
                {
                    var num = dataTable.Rows.Count - 1;
                    if (num > -1)
                    {
                        var text = dataTable.Rows[num]["ExitMap"].ToString();
                        if (this.level.name != text)
                        {
                            var level1 = this.level;
                            Command.all.Find("goto").Use(this, text);
                            if (level1 == this.level)
                            {
                                SendMessage(p, Lang.Player.WarningPortalDestinationUnloaded);
                                return;
                            }
                        }
                        else
                        {
                            SendBlockchange(x, y, z, b);
                        }

                        while (p.Loading) Thread.Sleep(10);
                        if (text != "lava")
                            Command.all.Find("move").Use(this,
                                string.Concat(name, " ", dataTable.Rows[num]["ExitX"].ToString(), " ",
                                    dataTable.Rows[num]["ExitY"].ToString(), " ",
                                    dataTable.Rows[num]["ExitZ"].ToString()));
                    }
                    else
                    {
                        Blockchange(this, x, y, z, 0);
                    }
                }
            }
            catch (Exception)
            {
                Server.s.Log(string.Format("Portal on map: {0}, coordinates(x,y,z) {1},{2},{3} has no exit.",
                    level.name, x, y, z));
                SendMessage(p, Lang.Player.WarningPortalHasNoExit);
            }
        }

        // Token: 0x06000AD1 RID: 2769 RVA: 0x0003B9F0 File Offset: 0x00039BF0
        public void HandleMsgBlock(Player p, ushort x, ushort y, ushort z, byte b)
        {
            try
            {
                string queryString;
                if (p.level.mapType == MapType.MyMap)
                    queryString = string.Concat("SELECT * FROM `Messages` WHERE Map=", p.level.MapDbId, " AND X=", x,
                        " AND Y=", y, " AND Z=", z);
                else
                    queryString = string.Concat("SELECT * FROM `Messages", level.name, "` WHERE X=", (int) x, " AND Y=",
                        (int) y, " AND Z=", (int) z);
                using (var dataTable = DBInterface.fillData(queryString))
                {
                    var num = dataTable.Rows.Count - 1;
                    if (num > -1)
                    {
                        var text = dataTable.Rows[num]["Message"].ToString().Trim();
                        if (text != prevMsg || Server.repeatMessage)
                        {
                            SendMessage(p, text);
                            prevMsg = text;
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
                SendMessage(p, Lang.Player.WarningNoMessageStored);
            }
        }

        // Token: 0x06000AD2 RID: 2770 RVA: 0x0003BBB0 File Offset: 0x00039DB0
        private bool checkOp()
        {
            return group.Permission < LevelPermission.Operator;
        }

        // Token: 0x06000AD3 RID: 2771 RVA: 0x0003BBC4 File Offset: 0x00039DC4
        private void deleteBlock(byte b, byte type, ushort x, ushort y, ushort z)
        {
            var random = new Random();
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
                    level.Blockchange(x, y, z, Block.DoorAirs(b));
                else
                    SendBlockchange(x, y, z, b);
                return;
            }

            if (Block.odoor(b) != byte.MaxValue)
            {
                if (b == 155 || b == 177)
                    level.Blockchange(this, x, y, z, Block.odoor(b));
                else
                    SendBlockchange(x, y, z, b);
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

                    var num3 = 0;
                    var num4 = 0;
                    var num5 = 0;
                    SendBlockchange(x, y, z, 187);
                    if (rot[0] < 48 || rot[0] > 208)
                        num3 = -1;
                    else if (rot[0] > 80 && rot[0] < 176) num3 = 1;
                    if (rot[0] > 16 && rot[0] < 112)
                        num4 = 1;
                    else if (rot[0] > 144 && rot[0] < 240) num4 = -1;
                    if (rot[1] >= 192 && rot[1] <= 224)
                        num5 = 1;
                    else if (rot[1] <= 64 && rot[1] >= 32) num5 = -1;
                    if (192 <= rot[1] && rot[1] <= 196 || 60 <= rot[1] && rot[1] <= 64)
                    {
                        num4 = 0;
                        num3 = 0;
                    }

                    level.Blockchange((ushort) (x + num4 * 2), (ushort) (y + num5 * 2), (ushort) (z + num3 * 2), 188);
                    level.Blockchange((ushort) (x + num4), (ushort) (y + num5), (ushort) (z + num3), 185);
                    break;
                }
                case 189:
                    if (level.physics != 0)
                    {
                        var num = random.Next(0, 2);
                        var num2 = random.Next(0, 2);
                        level.Blockchange((ushort) (x + num - 1), (ushort) (y + 2), (ushort) (z + num2 - 1), 189);
                        level.Blockchange((ushort) (x + num - 1), (ushort) (y + 1), (ushort) (z + num2 - 1), 11, false,
                            "wait 1 dissipate 100");
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

        // Token: 0x06000AD4 RID: 2772 RVA: 0x0003BF38 File Offset: 0x0003A138
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
                        switch (type)
                        {
                            case 3:
                                level.Blockchange(this, x, y, z, 2);
                                break;
                            case 44:
                                if (level.GetTile(x, (ushort) (y - 1), z) == 44)
                                {
                                    SendBlockchange(x, y, z, 0);
                                    level.Blockchange(this, x, (ushort) (y - 1), z, 43);
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
                    else
                        level.Blockchange(this, x, y, z, type);

                    break;
                case 6:
                    if (b == modeType)
                        SendBlockchange(x, y, z, b);
                    else
                        level.Blockchange(this, x, y, z, modeType);
                    break;
                case 13:
                    level.Blockchange(this, x, y, z, 182);
                    break;
                case 14:
                    level.Blockchange(this, x, y, z, 183);
                    break;
                default:
                    Server.s.Log(string.Format(Lang.Player.WarningBreakingBlocks, name));
                    BlockAction = 0;
                    break;
            }
        }

        // Token: 0x06000AD5 RID: 2773 RVA: 0x0003C0B4 File Offset: 0x0003A2B4
        private void Analyse(int x, int y, int z)
        {
            if (analyse.Count > 0)
            {
                if (analyse[analyse.Count - 1] < y)
                {
                    analyse.Add(y);
                    direction = 1;
                    return;
                }

                if (analyse[analyse.Count - 1] > y && direction != -1)
                {
                    msgUp = analyse.Count;
                    Server.s.Log("Messages when jumping up: " + msgUp);
                    analyse.Add(y);
                    direction = -1;
                    return;
                }

                if (analyse[analyse.Count - 1] > y && analyse[0] != y && direction == -1)
                {
                    analyse.Add(y);
                    return;
                }

                if (analyse[0] == y && direction == -1)
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

        // Token: 0x06000AD6 RID: 2774 RVA: 0x0003C230 File Offset: 0x0003A430
        private void HandlePositionInfo(object m)
        {
            var array = (byte[]) m;
            var b3 = array[0];
            if (!loggedIn || !fullylogged || trainGrab || following != "" || frozen) return;
            var num = NTHO(array, 1);
            var num2 = NTHO(array, 3);
            var num3 = NTHO(array, 5);
            var b = array[7];
            var b2 = array[8];
            if (waitForFall)
            {
                if (num2 > oldy) return;
                waitForFall = false;
                canBuild = true;
            }

            lock (hacksDetectionSync)
            {
                if (!HacksDetection(num, num2, num3, b, b2) && updatePosition)
                {
                    pos = new ushort[3]
                    {
                        num,
                        num2,
                        num3
                    };
                    rot = new byte[2]
                    {
                        b,
                        b2
                    };
                    setPos(XFloat, YFloat, ZFloat);
                    OnPositionChanged(new PositionChangedEventArgs(num, num2, num3, b, b2));
                }
            }

            if (updatePosition) return;
            lock (fallSynchronize)
            {
                if (updatePosition) return;
                fallPositionBuffer.Add(num2);
                if (fallPositionBuffer.Count == 6)
                {
                    var oPos = originalFallPos.y * 32 + 53;
                    if (fallPositionBuffer.Where(n => n <= oPos).Count() == 0)
                    {
                        SendPos(byte.MaxValue, (ushort) (originalFallPos.x * 32 + 16),
                            (ushort) (originalFallPos.y * 32 + 53), (ushort) (originalFallPos.z * 32 + 16), rot[0],
                            rot[1]);
                        oldx = (ushort) (originalFallPos.x * 32 + 16);
                        oldy = (ushort) (originalFallPos.y * 32 + 58);
                        oldz = (ushort) (originalFallPos.z * 32 + 16);
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

        // Token: 0x06000AD7 RID: 2775 RVA: 0x0003C4EC File Offset: 0x0003A6EC
        public void RealDeath(int x, int y, int z)
        {
            var tile = level.GetTile(x, (ushort) (y - 2), z);
            var tile2 = level.GetTile(x, y, z);
            if (oldBlock != (ushort) (x + y + z))
            {
                if (Block.Convert(tile) == 0)
                {
                    deathCount += 1;
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
                    deathCount += 1;
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

        // Token: 0x06000AD8 RID: 2776 RVA: 0x0003C600 File Offset: 0x0003A800
        public void OnBlockChangeProper(Player p, ushort x, ushort y, ushort z, byte type, byte action,
            ref bool stopChange)
        {
            var blockChangeProper = BlockChangeProper;
            if (blockChangeProper != null) blockChangeProper(p, x, y, z, type, action, ref stopChange);
        }

        // Token: 0x06000AD9 RID: 2777 RVA: 0x0003C628 File Offset: 0x0003A828
        public void HandleCommandAction(CommandActionPair commandAction)
        {
            var now = DateTime.Now;
            foreach (var commandElement in commandAction.blockCommands)
            {
                DateTime dateTime;
                if (blockCommandCooldowns.TryGetValue(commandElement.GetHashCode(), out dateTime) &&
                    dateTime.AddMilliseconds(commandElement.cooldown.Value * 1000f) > now) return;
                var text = commandElement.commandString;
                text = text.Replace("{name}", name);
                var message = new Message(text.Trim());
                var text2 = message.ReadString();
                var message2 = message.ReadToEnd() ?? "";
                try
                {
                    if (commandElement.consoleUse.Value)
                        Command.all.Find(text2).Use(null, message2);
                    else
                        Command.all.Find(text2).Use(this, message2);
                }
                catch (Exception ex)
                {
                    Server.ErrorLog(ex);
                }

                if (!blockCommandCooldowns.ContainsKey(commandElement.GetHashCode()))
                    blockCommandCooldowns.Add(commandElement.GetHashCode(), now);
                else
                    blockCommandCooldowns[commandElement.GetHashCode()] = now;
            }

            foreach (var actionElement in commandAction.blockActions)
            {
            }
        }

        // Token: 0x06000ADA RID: 2778 RVA: 0x0003C7E8 File Offset: 0x0003A9E8
        public void CheckBlock(ushort x, ushort y, ushort z)
        {
            y = (ushort) Math.Round((decimal) ((y * 32 + 4) / 32));
            var key = level.PosToInt(x, y, z);
            var key2 = level.PosToInt(x, y - 1, z);
            var tile = level.GetTile(x, y, z);
            var tile2 = level.GetTile(x, y - 1, z);
            if (level.CommandActionsWalk != null)
            {
                CommandActionPair value;
                if (level.CommandActionsWalk.TryGetValue(key, out value)) HandleCommandAction(value);
                CommandActionPair value2;
                if (level.CommandActionsWalk.TryGetValue(key2, out value2)) HandleCommandAction(value2);
            }

            if (Block.Mover(tile) || Block.Mover(tile2))
            {
                if (Block.DoorAirs(tile) != 0) level.Blockchange(x, y, z, Block.DoorAirs(tile));
                if (Block.DoorAirs(tile2) != 0) level.Blockchange(x, (ushort) (y - 1), z, Block.DoorAirs(tile2));
                if (x + y + z != oldBlock)
                {
                    if (tile == 160 || tile == 161 || tile == 162)
                        HandlePortal(this, x, y, z, tile);
                    else if (tile2 == 160 || tile2 == 161 || tile2 == 162)
                        HandlePortal(this, x, (ushort) (y - 1), z, tile2);
                    if (tile == 132 || tile == 133 || tile == 134)
                        HandleMsgBlock(this, x, y, z, tile);
                    else
                        switch (tile2)
                        {
                            case 132:
                            case 133:
                            case 134:
                                HandleMsgBlock(this, x, (ushort) (y - 1), z, tile2);
                                break;
                            case 70:
                                if (team == null) break;
                                y = (ushort) (y - 1);
                                foreach (var team2 in level.ctfgame.teams)
                                {
                                    if (team2.flagLocation[0] != x || team2.flagLocation[1] != y ||
                                        team2.flagLocation[2] != z) continue;
                                    if (team2 == team)
                                    {
                                        if (team2.flagishome && carryingFlag)
                                            level.ctfgame.CaptureFlag(this, team2, hasflag);
                                    }
                                    else
                                    {
                                        level.ctfgame.GrabFlag(this, team2);
                                    }
                                }

                                break;
                        }
                }
            }

            if (Block.Death(tile))
                HandleDeath(tile);
            else if (Block.Death(tile2)) HandleDeath(tile2);
        }

        // Token: 0x06000ADB RID: 2779 RVA: 0x0003CA9C File Offset: 0x0003AC9C
        public void SubtractLife()
        {
            if (level.mapType != MapType.Lava) return;
            if (IronChallenge != IronChallengeType.None)
            {
                var num = money >= LavaSettings.All.RewardAboveSeaLevel ? LavaSettings.All.RewardAboveSeaLevel : money;
                money -= num;
                if (IronChallenge == IronChallengeType.IronMan)
                    GlobalChatLevel(this, color + PublicName + "%c has failed Iron Man challenge.", false);
                else if (IronChallenge == IronChallengeType.IronWoman)
                    GlobalChatLevel(this, color + PublicName + "%c has failed Iron Woman challenge.", false);
                SendMessage(this, string.Concat("You have lost ", num, " ", Server.moneys, "."));
                IronChallenge = IronChallengeType.None;
            }

            if (lives == 1)
            {
                if (Server.useHeaven)
                {
                    lives -= 1;
                    GlobalChatLevel(this,
                        string.Format(MessagesManager.GetString("IsGhost"),
                            color + prefix + PublicName + Server.DefaultColor), false);
                    Command.all.Find("goto").Use(this, Server.heavenMapName);
                    invincible = true;
                    inHeaven = true;
                    winningStreak = 0;
                    SendMessage(MessagesManager.GetString("SentToHeaven"));
                    SendMessage(MessagesManager.GetString("HelpMessageInHeaven"));
                }
                else
                {
                    lives -= 1;
                    invincible = true;
                    GlobalChatLevel(this,
                        string.Format(MessagesManager.GetString("IsGhost"),
                            color + prefix + PublicName + Server.DefaultColor), false);
                    SendMessage(this, MessagesManager.GetString("HelpMessageGhost"));
                    winningStreak = 0;
                    if (LavaSettings.All.HeadlessGhosts) flipHead = true;
                }
            }

            if (lives == 2)
            {
                lives -= 1;
                SendMessage(this, "You have 1 life left");
                SendMessage(this, "You are immortal for the next 30 seconds.");
            }

            if (lives > 2)
            {
                lives -= 1;
                SendMessage(this, string.Format("You have {0} lives left", lives));
                SendMessage(this, "You are immortal for the next 30 seconds.");
            }
        }

        // Token: 0x06000ADC RID: 2780 RVA: 0x0003CD10 File Offset: 0x0003AF10
        public void HandleDeath(byte b, string customMessage = "", bool explode = false)
        {
            var x = (ushort) (pos[0] / 32);
            var y = (ushort) (pos[1] / 32);
            var z = (ushort) (pos[2] / 32);
            if (!(armorTime.AddSeconds(45.0) < DateTime.Now) && !(customMessage != "") ||
                !(lastDeath.AddSeconds(GeneralSettings.All.DeathCooldown) < DateTime.Now) &&
                !(customMessage != "")) return;
            if (level.Killer && !invincible)
            {
                var flag = players.Count < LavaSettings.All.HideDeathMessagesAmount;
                if (!hidden)
                    switch (b)
                    {
                        case 184:
                            if (flag)
                                GlobalChatLevel(this,
                                    string.Format(MessagesManager.GetString("DeathTntExplosion"),
                                        color + prefix + PublicName + Server.DefaultColor), false);
                            else
                                SendMessage(this,
                                    string.Format("@" + MessagesManager.GetString("DeathTntExplosion"),
                                        color + prefix + PublicName + Server.DefaultColor));
                            break;
                        case 192:
                            if (flag)
                                GlobalChatLevel(this,
                                    string.Format(MessagesManager.GetString("DeathAir"),
                                        color + prefix + PublicName + Server.DefaultColor), false);
                            else
                                SendMessage(this,
                                    string.Format("@" + MessagesManager.GetString("DeathAir"),
                                        color + prefix + PublicName + Server.DefaultColor));
                            break;
                        case 74:
                        case 141:
                        case 191:
                        case 193:
                            if (flag)
                                GlobalChatLevel(this,
                                    string.Format(MessagesManager.GetString("DeathActiveColdWater"),
                                        color + prefix + PublicName + Server.DefaultColor), false);
                            else
                                SendMessage(this,
                                    string.Format("@" + MessagesManager.GetString("DeathActiveColdWater"),
                                        color + prefix + PublicName + Server.DefaultColor));
                            if (!inHeaven) SubtractLife();
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
                                GlobalChatLevel(this,
                                    string.Format(MessagesManager.GetString("DeathActiveHotLava"),
                                        color + prefix + PublicName + Server.DefaultColor), false);
                            else
                                SendMessage(this,
                                    string.Format("@" + MessagesManager.GetString("DeathActiveHotLava"),
                                        color + prefix + PublicName + Server.DefaultColor));
                            if (!inHeaven) SubtractLife();
                            break;
                        case 195:
                            if (flag)
                                GlobalChatLevel(this,
                                    string.Format(MessagesManager.GetString("DeathMagma"),
                                        color + prefix + PublicName + Server.DefaultColor), false);
                            else
                                SendMessage(this,
                                    string.Format("@" + MessagesManager.GetString("DeathMagma"),
                                        color + prefix + PublicName + Server.DefaultColor));
                            if (!inHeaven) SubtractLife();
                            break;
                        case 196:
                            if (flag)
                                GlobalChatLevel(this,
                                    string.Format(MessagesManager.GetString("DeathGeyser"),
                                        color + prefix + PublicName + Server.DefaultColor), false);
                            else
                                SendMessage(this,
                                    string.Format("@" + MessagesManager.GetString("DeathGeyser"),
                                        color + prefix + PublicName + Server.DefaultColor));
                            break;
                        case 242:
                            if (flag)
                                GlobalChatLevel(this,
                                    string.Format(MessagesManager.GetString("DeathBird"),
                                        color + prefix + PublicName + Server.DefaultColor), false);
                            else
                                SendMessage(this,
                                    string.Format("@" + MessagesManager.GetString("DeathBird"),
                                        color + prefix + PublicName + Server.DefaultColor));
                            break;
                        case 230:
                            if (flag)
                                GlobalChatLevel(this,
                                    string.Format(MessagesManager.GetString("DeathTrain"),
                                        color + prefix + PublicName + Server.DefaultColor), false);
                            else
                                SendMessage(this,
                                    string.Format("@" + MessagesManager.GetString("DeathTrain"),
                                        color + prefix + PublicName + Server.DefaultColor));
                            break;
                        case 247:
                            if (flag)
                                GlobalChatLevel(this,
                                    string.Format(MessagesManager.GetString("DeathShark"),
                                        color + prefix + PublicName + Server.DefaultColor), false);
                            else
                                SendMessage(this,
                                    string.Format("@" + MessagesManager.GetString("DeathShark"),
                                        color + prefix + PublicName + Server.DefaultColor));
                            break;
                        case 185:
                            if (flag)
                                GlobalChatLevel(this,
                                    string.Format(MessagesManager.GetString("DeathFire"),
                                        color + prefix + PublicName + Server.DefaultColor), false);
                            else
                                SendMessage(this,
                                    string.Format("@" + MessagesManager.GetString("DeathFire"),
                                        color + prefix + PublicName + Server.DefaultColor));
                            break;
                        case 188:
                            if (flag)
                                GlobalChatLevel(this,
                                    string.Format(MessagesManager.GetString("DeathRocket"),
                                        color + prefix + PublicName + Server.DefaultColor), false);
                            else
                                SendMessage(this,
                                    string.Format("@" + MessagesManager.GetString("DeathRocket"),
                                        color + prefix + PublicName + Server.DefaultColor));
                            level.MakeExplosion(x, y, z, 0);
                            break;
                        case 232:
                            if (flag)
                                GlobalChatLevel(this,
                                    string.Format(MessagesManager.GetString("DeathZombie"),
                                        color + prefix + PublicName + Server.DefaultColor), false);
                            else
                                SendMessage(this,
                                    string.Format("@" + MessagesManager.GetString("DeathZombie"),
                                        color + prefix + PublicName + Server.DefaultColor));
                            break;
                        case 231:
                            if (flag)
                                GlobalChatLevel(this,
                                    string.Format(MessagesManager.GetString("DeathCreeper"),
                                        color + prefix + PublicName + Server.DefaultColor), false);
                            else
                                SendMessage(this,
                                    string.Format("@" + MessagesManager.GetString("DeathCreeper"),
                                        color + prefix + PublicName + Server.DefaultColor));
                            level.MakeExplosion(x, y, z, 1);
                            break;
                        case 0:
                            if (flag)
                                GlobalChatLevel(this,
                                    string.Format(MessagesManager.GetString("DeathFall"),
                                        color + prefix + PublicName + Server.DefaultColor), false);
                            else
                                SendMessage(this,
                                    string.Format("@" + MessagesManager.GetString("DeathFall"),
                                        color + prefix + PublicName + Server.DefaultColor));
                            break;
                        case 8:
                            if (flag)
                                GlobalChatLevel(this,
                                    string.Format(MessagesManager.GetString("DeathDrawn"),
                                        color + prefix + PublicName + Server.DefaultColor), false);
                            else
                                SendMessage(this,
                                    string.Format("@" + MessagesManager.GetString("DeathDrawn"),
                                        color + prefix + PublicName + Server.DefaultColor));
                            break;
                        case byte.MaxValue:
                            if (flag)
                                GlobalChatLevel(this,
                                    string.Format(MessagesManager.GetString("DeathTermination"),
                                        color + prefix + PublicName + Server.DefaultColor), false);
                            else
                                SendMessage(this,
                                    string.Format("@" + MessagesManager.GetString("DeathTermination"),
                                        color + prefix + PublicName + Server.DefaultColor));
                            break;
                        case 250:
                            if (flag)
                                GlobalChatLevel(this,
                                    string.Format(MessagesManager.GetString("DeathLavaShark"),
                                        color + prefix + PublicName + Server.DefaultColor), false);
                            else
                                SendMessage(this,
                                    string.Format("@" + MessagesManager.GetString("DeathLavaShark"),
                                        color + prefix + PublicName + Server.DefaultColor));
                            break;
                        case 1:
                            if (explode) level.MakeExplosion(x, y, z, 1);
                            if (flag)
                                GlobalChat(this, color + prefix + PublicName + Server.DefaultColor + customMessage,
                                    false);
                            else
                                SendMessage(this,
                                    "@" + color + prefix + PublicName + Server.DefaultColor + customMessage);
                            break;
                        case 4:
                            if (explode) level.MakeExplosion(x, y, z, 1);
                            if (flag)
                                GlobalChatLevel(this, color + prefix + PublicName + Server.DefaultColor + customMessage,
                                    false);
                            else
                                SendMessage(this,
                                    "@" + color + prefix + PublicName + Server.DefaultColor + customMessage);
                            break;
                    }

                if (LavaSettings.All.SpawnOnDeath)
                {
                    Command.all.Find("spawn").Use(this, "");
                    Server.s.Log(string.Format("{0} lost a life and spawned.", name));
                }

                overallDeath++;
                if (Server.deathcount && overallDeath % 10 == 0 && !hidden && flag)
                    GlobalChat(this,
                        string.Format(Lang.Player.DiedTimesGlobalMessage,
                            color + prefix + PublicName + Server.DefaultColor, overallDeath), false);
            }

            lastDeath = DateTime.Now;
        }

        // Token: 0x06000ADD RID: 2781 RVA: 0x0003D974 File Offset: 0x0003BB74
        private void HandleChat(byte[] message)
        {
            try
            {
                if (loggedIn)
                {
                    var text = enc.GetString(message, 1, 64).Trim();
                    var text2 = text;
                    var trimChars = new char[1];
                    text = text2.Trim(trimChars);
                    var flag = false;
                    filterInput(ref text, out flag);
                    if (!flag)
                    {
                        if (storedMessage != "" && !text.EndsWith(">") && !text.EndsWith("<"))
                        {
                            text = storedMessage.Replace("|>|", " ").Replace("|<|", "") + text;
                            storedMessage = "";
                        }

                        if (text.EndsWith(">"))
                        {
                            storedMessage += text.Replace(">", "|>|");
                            SendMessage(Lang.Player.ChatAppended);
                        }
                        else if (text.EndsWith("<"))
                        {
                            storedMessage += text.Replace("<", "|<|");
                            SendMessage(Lang.Player.ChatAppended);
                        }
                        else
                        {
                            text = Regex.Replace(text, "\\s\\s+", " ");
                            foreach (var c in text)
                                if (c < ' ' || c >= '\u007f' || c == '&')
                                {
                                    Kick(Lang.Player.ChatIllegalCharacter);
                                    return;
                                }

                            if (text.Length != 0)
                            {
                                afkCount = 0;
                                if (text != "/afk" && Server.afkset.Contains(name))
                                {
                                    Server.afkset.Remove(name);
                                    if (!Server.voteMode)
                                        GlobalMessage(string.Format(Lang.Player.AfkNoLonger,
                                            color + PublicName + Server.DefaultColor));
                                    IRCSay(string.Format(Lang.Player.AfkNoLonger, PublicName));
                                }

                                if (text[0] == '/' || text[0] == '!')
                                {
                                    text = text.Remove(0, 1);
                                    var num = text.IndexOf(' ');
                                    if (num == -1)
                                    {
                                        HandleCommand(text.ToLower(), "");
                                    }
                                    else
                                    {
                                        var cmd = text.Substring(0, num).ToLower();
                                        var message2 = text.Substring(num + 1);
                                        HandleCommand(cmd, message2);
                                    }
                                }
                                else if (Server.chatmod && !voice)
                                {
                                    SendMessage(Lang.Player.ChatModeration);
                                }
                                else if (muted)
                                {
                                    SendMessage(Lang.Player.ChatMuted);
                                }
                                else if (IsTempMuted)
                                {
                                    SendMessage(string.Format(Lang.Player.ChatTempMutedTime,
                                        muteTime.Subtract(DateTime.Now).TotalSeconds.ToString("n0")));
                                }
                                else if (text[0] == '@' || whisper)
                                {
                                    var text4 = text;
                                    if (text[0] == '@') text4 = text.Remove(0, 1).Trim();
                                    if (whisperTo == "")
                                    {
                                        var num2 = text4.IndexOf(' ');
                                        if (num2 != -1)
                                        {
                                            var to = text4.Substring(0, num2);
                                            var message3 = text4.Substring(num2 + 1);
                                            HandleWhisper(to, message3);
                                        }
                                        else
                                        {
                                            SendMessage(Lang.Player.ChatNoMessageEntered);
                                        }
                                    }
                                    else
                                    {
                                        HandleWhisper(whisperTo, text4);
                                    }
                                }
                                else if (text[0] == '#' || opchat)
                                {
                                    var text5 = text;
                                    if (text[0] == '#') text5 = text.Remove(0, 1).Trim();
                                    GlobalMessageOps(string.Format(Lang.Player.ChatToOps, color + name, text5));
                                    if (group.Permission < Server.opchatperm && !Server.devs.Contains(name.ToLower()))
                                        SendMessage(string.Format(Lang.Player.ChatToOps, color + name, text5));
                                    Server.s.Log("(OPs): " + name + ": " + text5);
                                    IRCSay(name + ": " + text5, true);
                                }
                                else
                                {
                                    if (Server.voteMode)
                                    {
                                        if (level == Server.LavaLevel)
                                        {
                                            if (LavaSystem.CountVotes(text.Trim(), this)) return;
                                        }
                                        else if (level == Server.InfectionLevel &&
                                                 InfectionUtils.CountVotes(text.Trim(), this))
                                        {
                                            return;
                                        }
                                    }

                                    if (VotingSystem.votingInProgress)
                                    {
                                        if (text.ToLower() == Lang.Player.VoteDecisionYesShortcut ||
                                            text.ToLower() == Lang.Player.VoteDecisionYes)
                                        {
                                            votingChoice = VotingSystem.VotingChoice.Yes;
                                            SendMessage(Lang.Player.VoteThanks);
                                            return;
                                        }

                                        if (text.ToLower() == Lang.Player.VoteDecisionNoShortcut ||
                                            text.ToLower() == Lang.Player.VoteDecisionNo)
                                        {
                                            votingChoice = VotingSystem.VotingChoice.No;
                                            SendMessage(Lang.Player.VoteThanks);
                                            return;
                                        }
                                    }

                                    if (teamchat)
                                    {
                                        if (team == null)
                                            SendMessage(this, "You are not on a team.");
                                        else
                                            foreach (var p in team.players)
                                                SendMessage(p,
                                                    string.Concat("(", team.teamstring, ") ", color, PublicName, ":&f ",
                                                        text));
                                    }
                                    else
                                    {
                                        if (joker)
                                        {
                                            if (File.Exists("text/joker.txt"))
                                            {
                                                Server.s.Log("<JOKER>: " + PublicName + ": " + text);
                                                GlobalMessageOps(string.Concat(Server.DefaultColor, "<&aJ&bO&cK&5E&9R",
                                                    Server.DefaultColor, ">: ", color, PublicName, ":&f ", text));
                                                var fileInfo = new FileInfo("text/joker.txt");
                                                var streamReader = fileInfo.OpenText();
                                                var list = new List<string>();
                                                var random = new Random();
                                                while (streamReader.Peek() != -1) list.Add(streamReader.ReadLine());
                                                var index = random.Next(list.Count);
                                                streamReader.Close();
                                                streamReader.Dispose();
                                                text = list[index];
                                            }
                                            else
                                            {
                                                using (var streamWriter =
                                                    new StreamWriter(File.Create("text/joker.txt")))
                                                {
                                                    streamWriter.WriteLine(
                                                        "'text/joker.txt' file is not set! You should probably fill it with funny lines.");
                                                }
                                            }
                                        }

                                        stopTheText = false;
                                        OnPlayerChatEvent(this, ref text, ref stopTheText);
                                        if (!stopTheText)
                                        {
                                            if (level.mapType == MapType.Lava && !LavaSettings.All.LavaWorldChat)
                                            {
                                                Server.s.Log("<" + name + ">[level] " + text);
                                                GlobalChatLevel(this, text, true);
                                            }
                                            else if (!level.worldChat)
                                            {
                                                Server.s.Log("<" + name + ">[level] " + text);
                                                GlobalChatLevel(this, text, true);
                                            }
                                            else
                                            {
                                                Server.s.Log("<" + name + "> " + text);
                                                if (Server.worldChat)
                                                    GlobalChat(this, text);
                                                else
                                                    GlobalChatLevel(this, text, true);
                                                IRCSay(PublicName + ": " + text);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        // Token: 0x06000ADE RID: 2782 RVA: 0x0003E288 File Offset: 0x0003C488
        private void CheckForCommandSpam(ref bool stopIt)
        {
            if (!ExtraData.ContainsKey("RecentCommandsTimes"))
            {
                ExtraData.Add("RecentCommandsTimes", new List<DateTime>());
                ((List<DateTime>) ExtraData["RecentCommandsTimes"]).Add(DateTime.Now);
                return;
            }

            var list = (List<DateTime>) ExtraData["RecentCommandsTimes"];
            list.Add(DateTime.Now);
            var comparator = DateTime.Now.AddSeconds(-(double) GeneralSettings.All.CooldownCmdMaxSeconds);
            var list2 = (from t in list
                where t >= comparator
                select t).ToList();
            var num = list2.Count();
            if (num > GeneralSettings.All.CooldownCmdMax)
            {
                stopIt = true;
                SendMessage(this, GeneralSettings.All.CooldownCmdWarning);
                return;
            }

            ExtraData["RecentCommandsTimes"] = list2;
        }

        // Token: 0x06000ADF RID: 2783 RVA: 0x0003E374 File Offset: 0x0003C574
        public void HandleCommand(string cmd, string message)
        {
            try
            {
                if (cmd == "")
                {
                    SendMessage(Lang.Player.CommandNoEntered);
                }
                else if (jailed)
                {
                    SendMessage(Lang.Player.CommandJailWarning);
                }
                else
                {
                    if (GeneralSettings.All.CooldownCmdUse)
                    {
                        var flag = false;
                        CheckForCommandSpam(ref flag);
                        if (flag) return;
                    }

                    if (cmd.ToLower() == "care")
                    {
                        SendMessage("Corneria now loves you with all his heart.");
                    }
                    else
                    {
                        try
                        {
                            var num = int.Parse(cmd);
                            if (messageBind[num] == null)
                            {
                                SendMessage(string.Format(Lang.Player.CommandNoBind, cmd));
                                return;
                            }

                            message = messageBind[num] + " " + message;
                            message = message.TrimEnd(' ');
                            cmd = cmdBind[num];
                        }
                        catch
                        {
                        }

                        var commandLowercase = cmd.ToLower();
                        var source = from c in Command.all
                            where c.name.ToLower() == commandLowercase || c.shortcut.ToLower() == commandLowercase
                            select c;
                        var command3 = source.FirstOrDefault();
                        var command2 = source.FirstOrDefault(c => c.IsWithinScope(this));
                        var command = command2 != null ? command2 : command3;
                        if (command != null)
                        {
                            if (lives < 1 && level.mapType == MapType.Lava && command.name == "tp") doCommand = true;
                            if (group.CanExecute(command) || doCommand)
                            {
                                doCommand = false;
                                if (!command.IsWithinScope(this))
                                {
                                    SendMessage(this,
                                        string.Format("This command can be used only in {0} modes.",
                                            command.Scope.ToString()));
                                }
                                else
                                {
                                    if (cmd != "repeat" && cmd != "m") lastCMD = cmd + " " + message;
                                    if (level.name.Contains("Museum " + Server.DefaultColor) && !command.museumUsable)
                                    {
                                        SendMessage("Cannot use this command while in a museum!");
                                    }
                                    else if ((joker || muted) && cmd.ToLower() == "me")
                                    {
                                        SendMessage(string.Format(Lang.Player.CommandNoUseWhenMuted, cmd.ToLower()));
                                    }
                                    else
                                    {
                                        if (command.HighSecurity)
                                            Server.s.CommandUsed(string.Format("{0} used /{1} {2}", name, cmd, "***"));
                                        else
                                            Server.s.CommandUsed(string.Format("{0} used /{1} {2}", name, cmd,
                                                message));
                                        commThread = new Thread(delegate()
                                        {
                                            try
                                            {
                                                command.Use(this, message);
                                            }
                                            catch (Exception ex2)
                                            {
                                                Server.ErrorLog(ex2);
                                                SendMessage(this, Lang.Player.ErrorCommand);
                                            }
                                        });
                                        commThread.Start();
                                    }
                                }
                            }
                            else
                            {
                                SendMessage(string.Format(Lang.Player.CommandNotAllowedToUse, cmd));
                            }
                        }
                        else if (Block.Byte(cmd.ToLower()) != 255)
                        {
                            HandleCommand("mode", cmd.ToLower());
                        }
                        else
                        {
                            var flag2 = true;
                            string key;
                            switch (key = cmd.ToLower())
                            {
                                case "guest":
                                    message = message + " " + cmd.ToLower();
                                    cmd = "setrank";
                                    goto IL_7AE;
                                case "builder":
                                    message = message + " " + cmd.ToLower();
                                    cmd = "setrank";
                                    goto IL_7AE;
                                case "advbuilder":
                                case "adv":
                                    message = message + " " + cmd.ToLower();
                                    cmd = "setrank";
                                    goto IL_7AE;
                                case "operator":
                                case "op":
                                    message = message + " " + cmd.ToLower();
                                    cmd = "setrank";
                                    goto IL_7AE;
                                case "super":
                                case "superop":
                                    message = message + " " + cmd.ToLower();
                                    cmd = "setrank";
                                    goto IL_7AE;
                                case "cut":
                                    cmd = "copy";
                                    message = "cut";
                                    goto IL_7AE;
                                case "admins":
                                    message = "superop";
                                    cmd = "viewranks";
                                    goto IL_7AE;
                                case "ops":
                                    message = "op";
                                    cmd = "viewranks";
                                    goto IL_7AE;
                                case "banned":
                                    message = cmd;
                                    cmd = "viewranks";
                                    goto IL_7AE;
                                case "ps":
                                    message = "ps " + message;
                                    cmd = "map";
                                    goto IL_7AE;
                                case "item":
                                case "inventory":
                                case "inv":
                                    cmd = "items";
                                    goto IL_7AE;
                                case "bhb":
                                case "hbox":
                                    cmd = "cuboid";
                                    message = "hollow";
                                    goto IL_7AE;
                                case "blb":
                                case "box":
                                    cmd = "cuboid";
                                    goto IL_7AE;
                                case "sphere":
                                    cmd = "spheroid";
                                    goto IL_7AE;
                                case "cmdlist":
                                case "commands":
                                case "cmdhelp":
                                    cmd = "help";
                                    goto IL_7AE;
                                case "who":
                                    cmd = "players";
                                    goto IL_7AE;
                                case "worlds":
                                case "maps":
                                    cmd = "levels";
                                    goto IL_7AE;
                                case "mapsave":
                                    cmd = "save";
                                    goto IL_7AE;
                                case "mapload":
                                    cmd = "load";
                                    goto IL_7AE;
                                case "materials":
                                    cmd = "blocks";
                                    goto IL_7AE;
                            }

                            flag2 = false;
                            IL_7AE:
                            if (flag2)
                                HandleCommand(cmd, message);
                            else
                                SendMessage(string.Format(Lang.Player.CommandUnknown, cmd));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
                SendMessage(Lang.Player.CommandFailed);
            }
        }

        // Token: 0x06000AE0 RID: 2784 RVA: 0x0003EBB4 File Offset: 0x0003CDB4
        private void HandleWhisper(string to, string message)
        {
            var player = Find(to);
            if (player == this)
            {
                SendMessage(Lang.Player.CommandUsedOneself);
                return;
            }

            if (player == null || player.hidden && group.Permission < LevelPermission.Operator)
            {
                SendMessage(string.Format(Lang.Player.CommandPlayerNonexistent, to));
                return;
            }

            var chatOtherEventArgs = new ChatOtherEventArgs(message, this, player, ChatType.Whisper);
            OnChatOther(chatOtherEventArgs);
            if (chatOtherEventArgs.Handled) return;
            message = chatOtherEventArgs.Message;
            Server.s.Log(string.Concat(name, " @", player.name, ": ", message));
            SendChat(this,
                string.Concat(Server.DefaultColor, "[<] ", player.color, player.prefix, player.PublicName, ": &f",
                    message));
            SendChat(player, string.Concat("&9[>] ", color, prefix, PublicName, ": &f", message));
        }

        // Token: 0x06000AE1 RID: 2785 RVA: 0x0003ED14 File Offset: 0x0003CF14
        public void SendRaw(int id)
        {
            SendRaw(id, new byte[0]);
        }

        // Token: 0x06000AE2 RID: 2786 RVA: 0x0003ED24 File Offset: 0x0003CF24
        public void SendCallback(IAsyncResult result)
        {
            try
            {
                Interlocked.Decrement(ref pendingPackets);
                var socket = (Socket) result.AsyncState;
                socket.EndSend(result);
            }
            catch
            {
            }
        }

        // Token: 0x06000AE3 RID: 2787 RVA: 0x0003ED68 File Offset: 0x0003CF68
        public void SendRaw(byte[] msg)
        {
            var num = 0;
            while (true)
                try
                {
                    if (socket != null)
                    {
                        Interlocked.Increment(ref pendingPackets);
                        socket.BeginSend(msg, 0, msg.Length, SocketFlags.None, SendCallback, socket);
                        Server.packetSent++;
                    }

                    return;
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
                        return;
                    }

                    if (num > 3)
                    {
                        Disconnect();
                        return;
                    }
                }
        }

        // Token: 0x06000AE4 RID: 2788 RVA: 0x0003EE48 File Offset: 0x0003D048
        public void SendRaw(int id, byte[] send)
        {
            var array = new byte[send.Length + 1];
            array[0] = (byte) id;
            Buffer.BlockCopy(send, 0, array, 1, send.Length);
            var num = 0;
            while (true)
                try
                {
                    if (socket != null && socket.Connected)
                    {
                        Interlocked.Increment(ref pendingPackets);
                        socket.BeginSend(array, 0, array.Length, SocketFlags.None, SendCallback, socket);
                        Server.packetSent++;
                    }

                    return;
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
                        return;
                    }

                    if (num > 3)
                    {
                        Disconnect();
                        return;
                    }
                }
        }

        // Token: 0x06000AE5 RID: 2789 RVA: 0x0003EF54 File Offset: 0x0003D154
        public static void SendMessage(Player p, string message, params object[] objects)
        {
            SendMessage(p, 0, string.Format(message, objects));
        }

        // Token: 0x06000AE6 RID: 2790 RVA: 0x0003EF64 File Offset: 0x0003D164
        public static void SendMessage(Player p, string message)
        {
            SendMessage(p, 0, message);
        }

        // Token: 0x06000AE7 RID: 2791 RVA: 0x0003EF70 File Offset: 0x0003D170
        internal static void SendMessage(Player p, byte type, string message)
        {
            if (p != null)
            {
                p.SendMessage(type, Server.DefaultColor + message);
                return;
            }

            if (storeHelp)
            {
                storedHelp = storedHelp + message + "\r\n";
                return;
            }

            Server.s.Log(message);
            IRCSay(message, true);
        }

        // Token: 0x06000AE8 RID: 2792 RVA: 0x0003EFC4 File Offset: 0x0003D1C4
        internal static void SendMessage(Player p, byte type, TimeSpan lifespan, string message)
        {
            if (p != null)
            {
                p.SendMessage(type, Server.DefaultColor + message);
                if (type != 0)
                {
                    System.Threading.Timer timer = null;
                    timer = new System.Threading.Timer(delegate
                    {
                        if (p.Cpe.MessageTypes == 1) SendMessage(p, type, "");
                        timer.Dispose();
                    }, null, lifespan, TimeSpan.FromMilliseconds(-1.0));
                }

                return;
            }

            if (storeHelp)
            {
                storedHelp = storedHelp + message + "\r\n";
                return;
            }

            Server.s.Log(message);
            IRCSay(message, true);
        }

        // Token: 0x06000AE9 RID: 2793 RVA: 0x0003F080 File Offset: 0x0003D280
        public static void SendMessage2(Player p, string message)
        {
            if (p == null)
            {
                Server.s.Log(message);
                IRCSay(message, true);
                return;
            }

            p.SendMessage2(0, Server.DefaultColor + message);
        }

        // Token: 0x06000AEA RID: 2794 RVA: 0x0003F0AC File Offset: 0x0003D2AC
        public void SendMessage(string message)
        {
            if (this == null)
            {
                Server.s.Log(message);
                return;
            }

            SendMessage(0, Server.DefaultColor + message);
        }

        // Token: 0x06000AEB RID: 2795 RVA: 0x0003F0D0 File Offset: 0x0003D2D0
        public void SendChat(Player p, string message)
        {
            if (this == null)
            {
                Server.s.Log(message);
                return;
            }

            SendMessage(p, message);
        }

        // Token: 0x06000AEC RID: 2796 RVA: 0x0003F0EC File Offset: 0x0003D2EC
        public static void FilterMessageConsole(ref string message)
        {
            message = message.Replace("%s", Server.DefaultColor);
            for (var i = 0; i < 10; i++)
            {
                message = message.Replace("%" + i, "&" + i);
                message = message.Replace("&" + i + " &", " &");
                message = message.Replace("&" + i + "&", "&");
            }

            for (var c = 'a'; c <= 'f'; c += '\u0001')
            {
                message = message.Replace("%" + c, "&" + c);
                message = message.Replace("&" + c + " &", " &");
                message = message.Replace("&" + c + "&", "&");
            }
        }

        // Token: 0x06000AED RID: 2797 RVA: 0x0003F208 File Offset: 0x0003D408
        public void FilterMessage(ref string message)
        {
            for (var i = 0; i < 10; i++)
            {
                message = message.Replace("%" + i, "&" + i);
                message = message.Replace("&" + i + " &", " &");
                message = message.Replace("&" + i + "&", "&");
            }

            for (var c = 'a'; c <= 'f'; c = (char) (c + 1))
            {
                message = message.Replace("%" + c, "&" + c);
                message = message.Replace("&" + c + " &", " &");
                message = message.Replace("&" + c + "&", "&");
            }

            message = message.Replace("%s", "&s");
            message = message.Replace("&s &", " &");
            message = message.Replace("&s&", "&");
            message = message.Replace("&s", Server.DefaultColor);
            if (LavaSettings.All.AllowInGameVariables)
            {
                foreach (var keyValue in Server.constantsForChat.KeyValues)
                    message = message.Replace("$" + keyValue.Key, keyValue.Value);
                if (Server.useDollarSign)
                    message = message.Replace("$name", "$" + PublicName);
                else
                    message = message.Replace("$name", PublicName);
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

            var array = new byte[1]
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

        // Token: 0x06000AEE RID: 2798 RVA: 0x0003F980 File Offset: 0x0003DB80
        public void SendMessage2(byte id, string message)
        {
            if (this == null)
            {
                Server.s.Log(message);
            }
            else
            {
                if (ZoneSpam.AddSeconds(2.0) > DateTime.Now && message.Contains("This zone belongs to ")) return;
                var array = new byte[65];
                array[0] = id;
                FilterMessage(ref message);
                var num = 0;
                while (true)
                    try
                    {
                        foreach (var item in Wordwrap2(message))
                        {
                            var text = item;
                            if (item[text.Length - 2] == '&' && "0123456789abcdef".IndexOf(text[text.Length - 1]) > 0)
                                text = text.Remove(text.Length - 2);
                            if (text.TrimEnd(' ')[text.TrimEnd(' ').Length - 1] < '!') text += '\'';
                            StringFormat(text, 64).CopyTo(array, 1);
                            SendRaw(13, array);
                        }

                        return;
                    }
                    catch (Exception ex)
                    {
                        message = "&f" + message;
                        num++;
                        if (num < 10) continue;
                        Server.ErrorLog(ex);
                        return;
                    }
            }
        }

        // Token: 0x06000AEF RID: 2799 RVA: 0x0003FAFC File Offset: 0x0003DCFC
        public void SendMessage(byte type, string message)
        {
            if (this == null)
            {
                Server.s.Log(message);
            }
            else
            {
                if (ZoneSpam.AddSeconds(2.0) > DateTime.Now && message.Contains("This zone belongs to ")) return;
                var array = new byte[65];
                array[0] = type;
                FilterMessage(ref message);
                var num = 0;
                while (true)
                    try
                    {
                        if (type != 0 && message.Replace(Server.DefaultColor, "") == "")
                        {
                            StringFormat("", 64).CopyTo(array, 1);
                            SendRaw(13, array);
                            return;
                        }

                        foreach (var item in Wordwrap(message))
                        {
                            var text = item.TrimEnd();
                            while (text.Length >= 2 && text[text.Length - 2] == '&' &&
                                   "0123456789abcdef".IndexOf(text[text.Length - 1]) > 0)
                            {
                                text = text.Remove(text.Length - 2);
                                text = text.Trim();
                            }

                            if (text.Length >= 1 && text[text.Length - 1] < '!') text += '\'';
                            StringFormat(text, 64).CopyTo(array, 1);
                            SendRaw(13, array);
                        }

                        return;
                    }
                    catch (Exception ex)
                    {
                        message = "&f" + message;
                        num++;
                        if (num < 10) continue;
                        Server.ErrorLog(ex);
                        return;
                    }
            }
        }

        // Token: 0x06000AF0 RID: 2800 RVA: 0x0003FCCC File Offset: 0x0003DECC
        public void SendMotd()
        {
            var array = new byte[130];
            array[0] = 8;
            StringFormat(Server.name, 64).CopyTo(array, 1);
            StringFormat(Server.motd, 64).CopyTo(array, 65);
            if (group.Permission >= LevelPermission.Operator)
                array[129] = 100;
            else
                array[129] = 0;
            SendRaw(0, array);
        }

        // Token: 0x06000AF1 RID: 2801 RVA: 0x0003FD3C File Offset: 0x0003DF3C
        public void RemoveTextures()
        {
            var text = Server.motd.Split(new[]
            {
                "cfg="
            }, 2, StringSplitOptions.None)[0];
            var array = new byte[130];
            array[0] = 8;
            StringFormat(Server.name, 64).CopyTo(array, 1);
            StringFormat(text, 64).CopyTo(array, 65);
            if (Block.canPlace(this, 7))
                array[129] = 100;
            else
                array[129] = 0;
            SendRaw(0, array);
            Server.s.Log(text);
            isUsingTextures = false;
        }

        // Token: 0x06000AF2 RID: 2802 RVA: 0x0003FDD4 File Offset: 0x0003DFD4
        public void SendTextures()
        {
            var array = new byte[130];
            array[0] = 8;
            StringFormat(Server.name, 64).CopyTo(array, 1);
            StringFormat(Server.motd, 64).CopyTo(array, 65);
            if (Block.canPlace(this, 7))
                array[129] = 100;
            else
                array[129] = 0;
            SendRaw(0, array);
            isUsingTextures = true;
        }

        // Token: 0x06000AF3 RID: 2803 RVA: 0x0003FE44 File Offset: 0x0003E044
        public void SendUserMOTD(bool home = false)
        {
            var array = new byte[130];
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
                array[129] = 100;
            else
                array[129] = 0;
            SendRaw(0, array);
        }

        // Token: 0x06000AF4 RID: 2804 RVA: 0x0003FF04 File Offset: 0x0003E104
        public void SendMap()
        {
            mapLoading = true;
            sendLock = true;
            SendRaw(2, new byte[0]);
            var array = new byte[level.blocks.Length + 4];
            BitConverter.GetBytes(IPAddress.HostToNetworkOrder(level.blocks.Length)).CopyTo(array, 0);
            for (var i = 0; i < level.blocks.Length; i++)
                if (IsCpeSupported)
                {
                    if (Cpe.Blockmension == 1)
                        array[4 + i] = Block.Convert(level.blocks[i]);
                    else
                        array[4 + i] = Block.ConvertFromBlockmension(Block.Convert(level.blocks[i]));
                }
                else
                {
                    array[4 + i] = Block.ConvertExtended(Block.ConvertFromBlockmension(Block.Convert(level.blocks[i])));
                }

            array = GZip(array);
            level.Weight = array.Length;
            var num = (int) Math.Ceiling(array.Length / 1024.0);
            var num2 = 1;
            while (array.Length > 0)
            {
                var num3 = (short) Math.Min(array.Length, 1024);
                var array2 = new byte[1027];
                HTNO(num3).CopyTo(array2, 0);
                Buffer.BlockCopy(array, 0, array2, 2, num3);
                var array3 = new byte[array.Length - num3];
                Buffer.BlockCopy(array, num3, array3, 0, array.Length - num3);
                array = array3;
                array2[1026] = (byte) (num2 * 100 / num);
                SendRaw(3, array2);
                if (Server.updateTimer.Interval > 1000.0 && ip != "127.0.0.1")
                    Thread.Sleep(100);
                else if (ip != "127.0.0.1") Thread.Sleep(1);
                num2++;
            }

            array = new byte[6];
            HTNO((short) level.width).CopyTo(array, 0);
            HTNO((short) level.height).CopyTo(array, 2);
            HTNO((short) level.depth).CopyTo(array, 4);
            omitHackDetection = true;
            SendRaw(4, array);
            mapLoadedTime = DateTime.Now;
            mapLoading = false;
            sendLock = false;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            var environment = level.Info.Environment;
            if (environment != null)
            {
                var environmentHandler = new EnvironmentHandler();
                var environment2 = environmentHandler.Parse(environment);
                if (environment2 != null) environmentHandler.SendToPlayer(this, environment2);
            }

            var weather = level.Info.Weather;
            if (weather != null)
            {
                var weatherHandler = new WeatherHandler();
                var weather2 = weatherHandler.Parse(weather);
                if (weather != null) weatherHandler.SendToPlayer(this, weather2);
            }

            var texture = level.Info.Texture;
            if (texture != null)
            {
                var textureHandler = new TextureHandler();
                var texture2 = textureHandler.Parse(texture);
                if (weather != null) textureHandler.SendToPlayer(this, texture2);
            }
            else
            {
                var sideLevel = (short) (level.height / 2);
                if (Cpe.EnvMapAppearance == 1)
                    V1.EnvSetMapAppearance(this, "", byte.MaxValue, byte.MaxValue, sideLevel);
            }

            level.OnPlayerJoined(level, new PlayerJoinedEventArgs(this));
        }

        // Token: 0x06000AF5 RID: 2805 RVA: 0x00040254 File Offset: 0x0003E454
        public void SendSpawn(Player p, string name)
        {
            SendSpawn(p.id, name, p.ModelName, p.pos[0], p.pos[1], p.pos[2], p.rot[0], p.rot[1]);
        }

        // Token: 0x06000AF6 RID: 2806 RVA: 0x000402A4 File Offset: 0x0003E4A4
        public void SendSpawn(byte id, string name, ushort x, ushort y, ushort z, byte rotx, byte roty)
        {
            SendSpawn(id, name, null, x, y, z, rotx, roty);
        }

        // Token: 0x06000AF7 RID: 2807 RVA: 0x000402C4 File Offset: 0x0003E4C4
        public void SendSpawn(byte id, string name, string modelName, ushort x, ushort y, ushort z, byte rotx,
            byte roty)
        {
            var array = new byte[73];
            array[0] = id;
            StringFormat(name, 64).CopyTo(array, 1);
            HTNO(x).CopyTo(array, 65);
            HTNO(y).CopyTo(array, 67);
            HTNO(z).CopyTo(array, 69);
            array[71] = rotx;
            array[72] = roty;
            if (id == this.id || id == 255)
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

            omitHackDetection = true;
            SendRaw(7, array);
            if (IsCpeSupported && modelName != null) SendRaw(new Packets().MakeChangeModel(id, modelName));
        }

        // Token: 0x06000AF8 RID: 2808 RVA: 0x000403E0 File Offset: 0x0003E5E0
        public void SendPos(byte id, ushort x, ushort y, ushort z, byte rotx, byte roty)
        {
            if (x < 0) x = 32;
            if (y < 0) y = 32;
            if (z < 0) z = 32;
            if (x > level.width * 32) x = (ushort) (level.width * 32 - 32);
            if (z > level.depth * 32) z = (ushort) (level.depth * 32 - 32);
            if (x > 32767) x = 32730;
            if (y > 32767) y = 32730;
            if (z > 32767) z = 32730;
            if (id > 127)
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

            var array = new byte[9]
            {
                id,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            };
            HTNO(x).CopyTo(array, 1);
            HTNO(y).CopyTo(array, 3);
            HTNO(z).CopyTo(array, 5);
            array[7] = rotx;
            array[8] = roty;
            omitHackDetection = true;
            SendRaw(8, array);
        }

        // Token: 0x06000AF9 RID: 2809 RVA: 0x0004054C File Offset: 0x0003E74C
        public void SendDie(byte id)
        {
            SendRaw(12, new[]
            {
                id
            });
        }

        // Token: 0x06000AFA RID: 2810 RVA: 0x00040570 File Offset: 0x0003E770
        public void SendCurrentMapTile(ushort x, ushort y, ushort z)
        {
            SendBlockchange(x, y, z, level.GetTile(x, y, z));
        }

        // Token: 0x06000AFB RID: 2811 RVA: 0x0004058C File Offset: 0x0003E78C
        public void SendBlockchange(ushort x, ushort y, ushort z, byte type)
        {
            if (x < 0 || y < 0 || z < 0) return;
            if (x >= level.width || y >= level.height || z >= level.depth) return;
            var array = new byte[7];
            HTNO(x).CopyTo(array, 0);
            HTNO(y).CopyTo(array, 2);
            HTNO(z).CopyTo(array, 4);
            var b = Block.Convert(type);
            if (!IsCpeSupported)
                b = Block.ConvertExtended(Block.ConvertFromBlockmension(b));
            else if (Cpe.Blockmension < 1) b = Block.ConvertFromBlockmension(b);
            array[6] = b;
            SendRaw(6, array);
        }

        // Token: 0x06000AFC RID: 2812 RVA: 0x00040640 File Offset: 0x0003E840
        private void SendKick(string message)
        {
            SendRaw(14, StringFormat(message, 64));
        }

        // Token: 0x06000AFD RID: 2813 RVA: 0x00040654 File Offset: 0x0003E854
        internal void SendPing()
        {
            SendRaw(1);
        }

        // Token: 0x06000AFE RID: 2814 RVA: 0x00040660 File Offset: 0x0003E860
        private void UpdatePosition()
        {
            byte b = 0;
            if (oldpos[0] != pos[0] || oldpos[1] != pos[1] || oldpos[2] != pos[2]) b = (byte) (b | 1u);
            if (oldrot[0] != rot[0] || oldrot[1] != rot[1]) b = (byte) (b | 2u);
            if (Math.Abs(pos[0] - basepos[0]) > 32 || Math.Abs(pos[1] - basepos[1]) > 32 ||
                Math.Abs(pos[2] - basepos[2]) > 32) b = (byte) (b | 4u);
            if (oldpos[0] == pos[0] && oldpos[1] == pos[1] && oldpos[2] == pos[2] &&
                (basepos[0] != pos[0] || basepos[1] != pos[1] || basepos[2] != pos[2])) b = (byte) (b | 4u);
            var buffer = new byte[0];
            byte msg = 0;
            if ((b & 4u) != 0)
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
                        buffer[8] = rot[1];
                    else
                        buffer[8] = (byte) (rot[1] - (rot[1] - 128));
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
                            Buffer.BlockCopy(BitConverter.GetBytes((sbyte) (pos[0] - oldpos[0])), 0, buffer, 1, 1);
                            Buffer.BlockCopy(BitConverter.GetBytes((sbyte) (pos[1] - oldpos[1])), 0, buffer, 2, 1);
                            Buffer.BlockCopy(BitConverter.GetBytes((sbyte) (pos[2] - oldpos[2])), 0, buffer, 3, 1);
                        }
                        catch
                        {
                        }

                        break;
                    case 2:
                        msg = 11;
                        buffer = new byte[3];
                        buffer[0] = id;
                        buffer[1] = rot[0];
                        if (flipHead)
                        {
                            if (rot[1] > 64 && rot[1] < 192)
                                buffer[2] = rot[1];
                            else
                                buffer[2] = (byte) (rot[1] - (rot[1] - 128));
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
                            Buffer.BlockCopy(BitConverter.GetBytes((sbyte) (pos[0] - oldpos[0])), 0, buffer, 1, 1);
                            Buffer.BlockCopy(BitConverter.GetBytes((sbyte) (pos[1] - oldpos[1])), 0, buffer, 2, 1);
                            Buffer.BlockCopy(BitConverter.GetBytes((sbyte) (pos[2] - oldpos[2])), 0, buffer, 3, 1);
                            buffer[4] = rot[0];
                            if (flipHead)
                            {
                                if (rot[1] > 64 && rot[1] < 192)
                                    buffer[5] = rot[1];
                                else
                                    buffer[5] = (byte) (rot[1] - (rot[1] - 128));
                            }
                            else
                            {
                                buffer[5] = rot[1];
                            }
                        }
                        catch
                        {
                        }

                        break;
                }
            }

            oldpos = pos;
            oldrot = rot;
            if (b == 0) return;
            try
            {
                players.ForEachSync(delegate(Player p)
                {
                    if (p != this && p.level == level && !p.Loading) p.SendRaw(msg, buffer);
                });
            }
            catch
            {
            }
        }

        // Token: 0x06000AFF RID: 2815 RVA: 0x00040BE0 File Offset: 0x0003EDE0
        private void UpdatePositionP()
        {
            byte b = 0;
            if (oldpos[0] != pos[0] || oldpos[1] != pos[1] || oldpos[2] != pos[2]) b = (byte) (b | 1u);
            if (oldrot[0] != rot[0] || oldrot[1] != rot[1]) b = (byte) (b | 2u);
            if (Math.Abs(pos[0] - basepos[0]) > 32 || Math.Abs(pos[1] - basepos[1]) > 32 ||
                Math.Abs(pos[2] - basepos[2]) > 32) b = (byte) (b | 4u);
            if (oldpos[0] == pos[0] && oldpos[1] == pos[1] && oldpos[2] == pos[2] &&
                (basepos[0] != pos[0] || basepos[1] != pos[1] || basepos[2] != pos[2])) b = (byte) (b | 4u);
            var array = new byte[0];
            byte b2 = 0;
            if ((b & 4u) != 0)
            {
                b2 = 8;
                array = new byte[9]
                {
                    id,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0
                };
                HTNO(pos[0]).CopyTo(array, 1);
                HTNO(pos[1]).CopyTo(array, 3);
                HTNO(pos[2]).CopyTo(array, 5);
                array[7] = rot[0];
                if (flipHead)
                {
                    if (rot[1] > 64 && rot[1] < 192)
                        array[8] = rot[1];
                    else
                        array[8] = (byte) (rot[1] - (rot[1] - 128));
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
                                id,
                                0,
                                0,
                                0
                            };
                            Buffer.BlockCopy(BitConverter.GetBytes((sbyte) (pos[0] - oldpos[0])), 0, array, 1, 1);
                            Buffer.BlockCopy(BitConverter.GetBytes((sbyte) (pos[1] - oldpos[1])), 0, array, 2, 1);
                            Buffer.BlockCopy(BitConverter.GetBytes((sbyte) (pos[2] - oldpos[2])), 0, array, 3, 1);
                        }
                        catch
                        {
                        }

                        break;
                    case 2:
                        b2 = 11;
                        array = new byte[3]
                        {
                            id,
                            rot[0],
                            0
                        };
                        if (flipHead)
                        {
                            if (rot[1] > 64 && rot[1] < 192)
                                array[2] = rot[1];
                            else
                                array[2] = (byte) (rot[1] - (rot[1] - 128));
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
                                id,
                                0,
                                0,
                                0,
                                0,
                                0
                            };
                            Buffer.BlockCopy(BitConverter.GetBytes((sbyte) (pos[0] - oldpos[0])), 0, array, 1, 1);
                            Buffer.BlockCopy(BitConverter.GetBytes((sbyte) (pos[1] - oldpos[1])), 0, array, 2, 1);
                            Buffer.BlockCopy(BitConverter.GetBytes((sbyte) (pos[2] - oldpos[2])), 0, array, 3, 1);
                            array[4] = rot[0];
                            if (flipHead)
                            {
                                if (rot[1] > 64 && rot[1] < 192)
                                    array[5] = rot[1];
                                else
                                    array[5] = (byte) (rot[1] - (rot[1] - 128));
                            }
                            else
                            {
                                array[5] = rot[1];
                            }
                        }
                        catch
                        {
                        }

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

        // Token: 0x06000B00 RID: 2816 RVA: 0x00041084 File Offset: 0x0003F284
        public void DiePlayers()
        {
            for (byte b = 0; b < 128; b += 1) SendDie(b);
        }

        // Token: 0x06000B01 RID: 2817 RVA: 0x000410AC File Offset: 0x0003F2AC
        public void DieBots()
        {
            PlayerBot.playerbots.ForEach(delegate(PlayerBot b)
            {
                if (level == b.level) SendDie(b.id);
            });
        }

        // Token: 0x06000B02 RID: 2818 RVA: 0x000410C4 File Offset: 0x0003F2C4
        public void SpawnPlayers()
        {
            players.ForEachSync(delegate(Player pl)
            {
                if (pl.level == level && this != pl && !pl.hidden)
                    SendSpawn(pl.id, pl.color + (pl.IsRefree ? "[REF]" : "") + pl.PublicName, pl.ModelName, pl.pos[0],
                        pl.pos[1], pl.pos[2], pl.rot[0], pl.rot[1]);
            });
        }

        // Token: 0x06000B03 RID: 2819 RVA: 0x000410DC File Offset: 0x0003F2DC
        public void SpawnBots()
        {
            PlayerBot.playerbots.ForEach(delegate(PlayerBot b)
            {
                if (b.level == level)
                    SendSpawn(b.id, b.color + b.name, b.pos[0], b.pos[1], b.pos[2], b.rot[0], b.rot[1]);
            });
        }

        // Token: 0x06000B04 RID: 2820 RVA: 0x000410F4 File Offset: 0x0003F2F4
        public static void GlobalBlockchange(Level level, ushort x, ushort y, ushort z, byte type)
        {
            try
            {
                players.ForEachSync(delegate(Player p)
                {
                    if (p.level == level) p.SendBlockchange(x, y, z, type);
                });
            }
            catch
            {
            }
        }

        // Token: 0x06000B05 RID: 2821 RVA: 0x00041160 File Offset: 0x0003F360
        public static void GlobalChat(Player from, string message)
        {
            GlobalChat(from, message, true);
        }

        // Token: 0x06000B06 RID: 2822 RVA: 0x0004116C File Offset: 0x0003F36C
        public static void GlobalChat(Player from, string message, bool showname)
        {
            if (showname)
            {
                var stringBuilder = new StringBuilder();
                message = stringBuilder.Append(from.IronChallengeTag).Append(from.color).Append(from.voicestring)
                    .Append(from.Tag).Append(from.color).Append(from.prefix).Append(from.Tier).Append(from.StarsTag)
                    .Append(" ").Append(from.color).Append(from.PublicName).Append(": ").Replace("  ", " ")
                    .Append(MCColor.White).Append(message).ToString();
            }

            players.ForEachSync(delegate(Player p)
            {
                var chatOtherEventArgs = new ChatOtherEventArgs(message, from, p, ChatType.CrossMaps);
                OnChatOther(chatOtherEventArgs);
                if (chatOtherEventArgs.Handled) return;
                message = chatOtherEventArgs.Message;
                if (p.level.worldChat) SendMessage(p, message);
            });
            if (!Server.CLI) Window.thisWindow.UpdateChat(message);
        }

        // Token: 0x06000B07 RID: 2823 RVA: 0x000412B0 File Offset: 0x0003F4B0
        public static void GlobalChatLevel(Player from, string message, bool showname)
        {
            if (showname)
            {
                var stringBuilder = new StringBuilder();
                message = stringBuilder.Append(from.IronChallengeTag).Append(from.color).Append(from.voicestring)
                    .Append(from.Tag).Append(from.color).Append(from.prefix).Append(from.Tier).Append(from.StarsTag)
                    .Append(" ").Append(from.color).Append(from.PublicName).Append(": ").Append(MCColor.White)
                    .Append(message).ToString();
            }

            players.ForEachSync(delegate(Player p)
            {
                var chatOtherEventArgs = new ChatOtherEventArgs(message, from, p, ChatType.Map);
                OnChatOther(chatOtherEventArgs);
                if (chatOtherEventArgs.Handled) return;
                message = chatOtherEventArgs.Message;
                if (p.level == from.level) SendMessage(p, Server.DefaultColor + message);
            });
            if (!Server.CLI) Window.thisWindow.UpdateChat(message);
        }

        // Token: 0x06000B08 RID: 2824 RVA: 0x000413E4 File Offset: 0x0003F5E4
        public static void GlobalChatWorld(Player from, string message, bool showname)
        {
            if (showname)
                message = string.Concat(Lang.Player.GlobalChatWorldTag, from.color, from.voicestring, from.color,
                    from.prefix, from.PublicName, ": &f", message);
            try
            {
                players.ForEachSync(delegate(Player p)
                {
                    if (p.level.worldChat) SendMessage(p, message);
                });
                if (!Server.CLI) Window.thisWindow.UpdateChat(message);
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        // Token: 0x06000B09 RID: 2825 RVA: 0x000414B4 File Offset: 0x0003F6B4
        public static void GlobalMessage(string message, params string[] objects)
        {
            GlobalMessage(string.Format(message, objects));
        }

        // Token: 0x06000B0A RID: 2826 RVA: 0x000414C4 File Offset: 0x0003F6C4
        public static void GlobalMessage(string message)
        {
            try
            {
                players.ForEachSync(delegate(Player p)
                {
                    if (p.level.worldChat) SendMessage(p, message);
                });
            }
            catch
            {
            }

            if (!Server.CLI) Window.thisWindow.UpdateChat(Server.DefaultColor + message);
        }

        // Token: 0x06000B0B RID: 2827 RVA: 0x00041534 File Offset: 0x0003F734
        public static void GlobalMessageLevel(Level l, string message)
        {
            try
            {
                players.ForEachSync(delegate(Player p)
                {
                    if (p.level == l) SendMessage(p, message);
                });
            }
            catch
            {
            }

            if (!Server.CLI)
                Window.thisWindow.UpdateChat(string.Concat("{", l.name, "} ", Server.DefaultColor, message));
        }

        // Token: 0x06000B0C RID: 2828 RVA: 0x000415D4 File Offset: 0x0003F7D4
        public static void GlobalMessageOps(string message)
        {
            try
            {
                players.ForEachSync(delegate(Player p)
                {
                    if (p.group.Permission >= Server.opchatperm || Server.devs.Contains(p.name.ToLower()))
                        SendMessage(p, message);
                });
                if (!Server.CLI) Window.thisWindow.UpdateChat("#OPs: " + message);
            }
            catch
            {
                Server.s.Log("Error occured with Op Chat");
            }
        }

        // Token: 0x06000B0D RID: 2829 RVA: 0x00041654 File Offset: 0x0003F854
        public static void GlobalSpawn(Player from)
        {
            GlobalSpawn(from, from.pos[0], from.pos[1], from.pos[2], from.rot[0], from.rot[1], false);
        }

        // Token: 0x06000B0E RID: 2830 RVA: 0x0004168C File Offset: 0x0003F88C
        public static void GlobalSpawn(Player from, ushort x, ushort y, ushort z, byte rotx, byte roty, bool self)
        {
            GlobalSpawn(from, x, y, z, rotx, roty, self, "");
        }

        // Token: 0x06000B0F RID: 2831 RVA: 0x000416A4 File Offset: 0x0003F8A4
        public static void GlobalSpawn(Player from, ushort x, ushort y, ushort z, byte rotx, byte roty, bool self,
            string possession)
        {
            players.ForEachSync(delegate(Player p)
            {
                try
                {
                    if (!p.disconnected && p.socket != null)
                        if (!p.Loading || p == from)
                            if (p.level == from.level && (!from.hidden || self))
                            {
                                if (p != from)
                                    p.SendSpawn(from.id,
                                        from.color + (from.IsRefree ? "[REF]" : "") + from.PublicName + possession,
                                        from.ModelName, x, y, z, rotx, roty);
                                else if (self)
                                    p.SendSpawn(byte.MaxValue,
                                        from.color + (from.IsRefree ? "[REF]" : "") + from.PublicName + possession,
                                        from.ModelName, x, y, z, rotx, roty);
                            }
                }
                catch (Exception ex)
                {
                    Server.ErrorLog(ex);
                }
            });
        }

        // Token: 0x06000B10 RID: 2832 RVA: 0x0004170C File Offset: 0x0003F90C
        public void GlobalSpawn()
        {
            GlobalSpawnPlain(PublicName);
        }

        // Token: 0x06000B11 RID: 2833 RVA: 0x0004171C File Offset: 0x0003F91C
        public void GlobalSpawn(string alias)
        {
            GlobalSpawnPlain("&c" + alias);
        }

        // Token: 0x06000B12 RID: 2834 RVA: 0x00041730 File Offset: 0x0003F930
        public void GlobalSpawnPlain(string alias)
        {
            players.ForEachSync(delegate(Player p)
            {
                if (p.disconnected || p.socket == null) return;
                if (p.Loading) return;
                if (p.level != level || hidden) return;
                if (p != this) p.SendSpawn(id, alias, ModelName, pos[0], pos[1], pos[2], rot[0], rot[1]);
            });
        }

        // Token: 0x06000B13 RID: 2835 RVA: 0x00041768 File Offset: 0x0003F968
        public static void GlobalDie(Player from, bool self)
        {
            players.ForEachSync(delegate(Player p)
            {
                if (p.level != from.level || from.hidden && !self) return;
                if (p.Loading) return;
                if (p != from)
                {
                    p.SendDie(from.id);
                    return;
                }

                if (self) p.SendDie(byte.MaxValue);
            });
        }

        // Token: 0x06000B14 RID: 2836 RVA: 0x000417A0 File Offset: 0x0003F9A0
        public void DespawnAll()
        {
            players.ForEachSync(delegate(Player p)
            {
                if (p.level == level) SendDie(p.id);
            });
        }

        // Token: 0x06000B15 RID: 2837 RVA: 0x000417B8 File Offset: 0x0003F9B8
        public bool MarkPossessed(string marker = "")
        {
            if (marker != "")
            {
                var player = Find(marker);
                if (player == null) return false;
                marker = string.Concat(" (", player.color, player.PublicName, color, ")");
            }

            GlobalDie(this, true);
            GlobalSpawn(this, pos[0], pos[1], pos[2], rot[0], rot[1], true, marker);
            return true;
        }

        // Token: 0x06000B16 RID: 2838 RVA: 0x00041858 File Offset: 0x0003FA58
        public static void GlobalUpdateOld()
        {
            players.ForEach(delegate(Player p)
            {
                try
                {
                    if (!p.hidden) p.UpdatePosition();
                }
                catch
                {
                }
            });
        }

        // Token: 0x06000B17 RID: 2839 RVA: 0x00041884 File Offset: 0x0003FA84
        public static void GlobalUpdate()
        {
            players.ForEach(delegate(Player p)
            {
                try
                {
                    if (!p.hidden) p.UpdatePositionP();
                }
                catch
                {
                }
            });
            players.ForEach(delegate(Player p1)
            {
                var bList = new List<byte[]>();
                var totalLength = 0;
                players.ForEach(delegate(Player p2)
                {
                    if (p2.posBuffer != null && p1 != p2)
                    {
                        bList.Add(p2.posBuffer);
                        totalLength += p2.posBuffer.Length;
                    }
                });
                var toSend = new byte[totalLength];
                var iterator = 0;
                bList.ForEach(delegate(byte[] array)
                {
                    Buffer.BlockCopy(array, 0, toSend, iterator, array.Length);
                    iterator += array.Length;
                });
                p1.SendRaw(toSend);
            });
        }

        // Token: 0x06000B18 RID: 2840 RVA: 0x000418E0 File Offset: 0x0003FAE0
        private byte[] Combine(params byte[][] arrays)
        {
            var array = new byte[arrays.Sum(a => a.Length)];
            var num = 0;
            foreach (var array2 in arrays)
            {
                Buffer.BlockCopy(array2, 0, array, num, array2.Length);
                num += array2.Length;
            }

            return array;
        }

        // Token: 0x06000B19 RID: 2841 RVA: 0x00041944 File Offset: 0x0003FB44
        public void Disconnect()
        {
            leftGame();
        }

        // Token: 0x06000B1A RID: 2842 RVA: 0x00041954 File Offset: 0x0003FB54
        private string ConvertColorCodes(string message)
        {
            return Regex.Replace(message, "%([0-9a-f])", "&$1");
        }

        // Token: 0x06000B1B RID: 2843 RVA: 0x00041968 File Offset: 0x0003FB68
        public void Kick(string kickString)
        {
            leftGame(ConvertColorCodes(kickString));
        }

        // Token: 0x06000B1C RID: 2844 RVA: 0x00041978 File Offset: 0x0003FB78
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
                }
                else
                {
                    disconnected = true;
                    afkCount = 0;
                    afkStart = DateTime.Now;
                    if (Server.afkset.Contains(name)) Server.afkset.Remove(name);
                    if (kickString == "") kickString = Lang.Player.KickDisconnected;
                    SendKick(kickString);
                    if (loggedIn)
                    {
                        isFlying = false;
                        aiming = false;
                        if (team != null) team.RemoveMember(this);
                        GlobalDie(this, false);
                        if (kickString == Lang.Player.KickDisconnected || kickString.IndexOf("Server shutdown") != -1 ||
                            kickString == Server.customShutdownMessage)
                        {
                            if (!hidden)
                            {
                                if (farewellMessage == "")
                                    GlobalChat(this,
                                        string.Format(MessagesManager.GetString("GlobalMessagePlayerDisconnected"),
                                            string.Concat("&8- ", color, prefix, PublicName, Server.DefaultColor)),
                                        false);
                                else
                                    GlobalChat(this,
                                        string.Concat("&8- ", color, prefix, PublicName, Server.DefaultColor, " ",
                                            farewellMessage), false);
                            }

                            IRCSay(string.Format(Lang.Player.GlobalMessageLeftGame, PublicName));
                            Server.s.Log(name + " disconnected.");
                        }
                        else
                        {
                            totalKicked++;
                            GlobalChat(this,
                                string.Format(Lang.Player.GlobalMessageKicked,
                                    color + prefix + PublicName + Server.DefaultColor, kickString), false);
                            IRCSay(string.Format(Lang.Player.IrcMessageKicked, PublicName, kickString));
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
                            if (!playersThatLeft.ContainsKey(name.ToLower())) playersThatLeft.Add(name.ToLower(), ip);
                        }

                        if (Server.AutoLoad && level.unload)
                        {
                            var unload = true;
                            try
                            {
                                players.ForEach(delegate(Player pl)
                                {
                                    if (pl.level == level) unload = false;
                                });
                            }
                            catch
                            {
                            }

                            if (unload && !level.name.Contains("Museum " + Server.DefaultColor)) level.Unload();
                        }

                        SaveUndoToNewFile();
                    }
                    else
                    {
                        if (socket != null)
                        {
                            socket.Close();
                            socket = null;
                        }

                        if (disconnectionReason == DisconnectionReason.Unknown)
                        {
                            Server.s.Log(ip + " disconnected.");
                        }
                        else
                        {
                            var text = "error, please report.";
                            switch (disconnectionReason)
                            {
                                case DisconnectionReason.IPBan:
                                    text = "ip-ban.";
                                    break;
                                case DisconnectionReason.NameBan:
                                    text = "name-ban.";
                                    break;
                                case DisconnectionReason.TempBan:
                                    text = "temp-ban.";
                                    break;
                                case DisconnectionReason.AuthenticationFailure:
                                    text = "authentication failure.";
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
                                case DisconnectionReason.IllegalName:
                                    text = "illegal name.";
                                    break;
                            }

                            Server.s.Log(string.Concat(ip, " disconnected, name: ", name, ", reason: ", text));
                        }
                    }
                }
            }
            catch (Exception ex2)
            {
                Server.ErrorLog(ex2);
            }
        }

        // Token: 0x06000B1D RID: 2845 RVA: 0x00041E94 File Offset: 0x00040094
        internal void SaveUndoToNewFile()
        {
            try
            {
                if (UndoBuffer.Count != 0)
                {
                    var text = "extra/undo";
                    var text2 = "extra/undoPrevious";
                    if (!Directory.Exists(text)) Directory.CreateDirectory(text);
                    if (!Directory.Exists(text2)) Directory.CreateDirectory(text2);
                    var directoryInfo = new DirectoryInfo(text);
                    if (directoryInfo.GetDirectories("*").Length >= Server.totalUndo)
                    {
                        Directory.Delete(text2, true);
                        Directory.Move(text, text2);
                        Directory.CreateDirectory(text);
                    }

                    var text3 = text + "/" + name.ToLower();
                    if (!Directory.Exists(text3)) Directory.CreateDirectory(text3);
                    directoryInfo = new DirectoryInfo(text3);
                    using (var w = new StreamWriter(File.Create(string.Concat(text3, "/",
                        directoryInfo.GetFiles("*.undo").Length, ".undo"))))
                    {
                        UndoBuffer.ForEach(delegate(UndoPos uP)
                        {
                            w.Write("{0} {1} {2} {3} {4} {5} {6} ", uP.mapName, uP.x, uP.y, uP.z,
                                uP.timePlaced.ToString().Replace(' ', '&'), uP.type, uP.newtype);
                        });
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

        // Token: 0x06000B1E RID: 2846 RVA: 0x00042030 File Offset: 0x00040230
        public static void SendToInbox(string from, string to, string message)
        {
            DBInterface.ExecuteQuery(string.Format(
                "CREATE TABLE if not exists `Inbox{0}` (PlayerFrom CHAR(20), TimeSent DATETIME, Contents VARCHAR(255));",
                to));
            var text = message.Replace("'", "''");
            if (text.Length > 255)
            {
                text = message.Substring(0, 255);
                text.TrimEnd('\'');
            }

            DBInterface.ExecuteQuery(string.Format(
                "INSERT INTO `Inbox{0}` (PlayerFrom, TimeSent, Contents) VALUES ('{1}', '{2}', '{3}')", to, from,
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), text));
        }

        // Token: 0x06000B1F RID: 2847 RVA: 0x000420C4 File Offset: 0x000402C4
        public static List<Player> GetPlayers()
        {
            return new List<Player>(players);
        }

        // Token: 0x06000B20 RID: 2848 RVA: 0x000420D0 File Offset: 0x000402D0
        public static bool Exists(string name)
        {
            bool result;
            lock (players)
            {
                foreach (var player in players)
                    if (player.name.ToLower() == name.ToLower())
                        return true;
                result = false;
            }

            return result;
        }

        // Token: 0x06000B21 RID: 2849 RVA: 0x0004215C File Offset: 0x0004035C
        public static bool Exists(byte id)
        {
            bool result;
            lock (players)
            {
                foreach (var player in players)
                    if (player.id == id)
                        return true;
                result = false;
            }

            return result;
        }

        // Token: 0x06000B22 RID: 2850 RVA: 0x000421D8 File Offset: 0x000403D8
        public static Player Find(string name)
        {
            var list = new List<Player>();
            list.AddRange(players);
            Player player = null;
            var flag = false;
            foreach (var player2 in list)
            {
                if (player2.name.ToLower() == name.ToLower()) return player2;
                if (player2.name.ToLower().IndexOf(name.ToLower()) != -1)
                {
                    if (player == null)
                        player = player2;
                    else
                        flag = true;
                }
            }

            if (flag) return null;
            if (player != null) return player;
            return null;
        }

        // Token: 0x06000B23 RID: 2851 RVA: 0x00042284 File Offset: 0x00040484
        public static Player FindExact(string name)
        {
            var list = new List<Player>();
            list.AddRange(players);
            foreach (var player in list)
                if (player.name.ToLower() == name.ToLower())
                    return player;
            return null;
        }

        // Token: 0x06000B24 RID: 2852 RVA: 0x000422FC File Offset: 0x000404FC
        public static Group GetGroup(string name)
        {
            return Group.findPlayerGroup(name);
        }

        // Token: 0x06000B25 RID: 2853 RVA: 0x00042304 File Offset: 0x00040504
        public static string GetColor(string name)
        {
            return GetGroup(name).color;
        }

        // Token: 0x06000B26 RID: 2854 RVA: 0x00042314 File Offset: 0x00040514
        public static void SendAllToSpawn(Level level)
        {
            players.ForEachSync(delegate(Player pl)
            {
                ThreadPool.QueueUserWorkItem(delegate
                {
                    if (pl.level == level) Command.all.Find("spawn").Use(pl, "");
                });
            });
        }

        // Token: 0x06000B27 RID: 2855 RVA: 0x00042344 File Offset: 0x00040544
        public static int PosToBlockPos(ushort pos)
        {
            return pos / 32;
        }

        // Token: 0x06000B28 RID: 2856 RVA: 0x0004234C File Offset: 0x0004054C
        public bool IsTouching(Player who)
        {
            return Math.Abs(PosToBlockPos(who.pos[0]) - PosToBlockPos(pos[0])) <= 1 &&
                   Math.Abs(PosToBlockPos(who.pos[1]) - PosToBlockPos(pos[1])) <= 1 &&
                   Math.Abs(PosToBlockPos(who.pos[2]) - PosToBlockPos(pos[2])) <= 1;
        }

        // Token: 0x06000B29 RID: 2857 RVA: 0x000423D4 File Offset: 0x000405D4
        public int GetXZDistance(ushort[] position1, ushort[] position2)
        {
            return (position1[0] - position2[0]) * (position1[0] - position2[0]) +
                   (position1[2] - position2[2]) * (position1[2] - position2[2]);
        }

        // Token: 0x06000B2A RID: 2858 RVA: 0x000423F8 File Offset: 0x000405F8
        private byte GetFreeId()
        {
            int j;
            for (j = lastID + 1; j < 128; j++)
            {
                bool flag;
                if (!players.Exists(p => (int) p.id == j))
                    flag = PlayerBot.playerbots.Exists(b => (int) b.id == j);
                else
                    flag = true;
                if (!flag)
                {
                    lastID = (byte) j;
                    return (byte) j;
                }
            }

            int i;
            for (i = 0; i <= (int) lastID; i++)
            {
                bool flag2;
                if (!players.Exists(p => (int) p.id == i))
                    flag2 = PlayerBot.playerbots.Exists(b => (int) b.id == i);
                else
                    flag2 = true;
                if (!flag2)
                {
                    lastID = (byte) i;
                    return (byte) i;
                }
            }

            return 1;
        }

        // Token: 0x06000B2B RID: 2859 RVA: 0x00042528 File Offset: 0x00040728
        private static byte[] StringFormat(string str, int size)
        {
            var array = new byte[size];
            return enc.GetBytes(str.PadRight(size).Substring(0, size));
        }

        // Token: 0x06000B2C RID: 2860 RVA: 0x00042558 File Offset: 0x00040758
        private static List<string> Wordwrap2(string message)
        {
            var list = new List<string>();
            message = Regex.Replace(message, "(&[0-9a-f])+(&[0-9a-f])", "$2");
            message = Regex.Replace(message, "(&[0-9a-f])+$", "");
            var num = 64;
            var str = "";
            while (message.Length > 0)
            {
                if (list.Count > 0) message = !(message[0].ToString() == "&") ? str + message.Trim() : message.Trim();
                if (message.IndexOf("&") == message.IndexOf("&", message.IndexOf("&") + 1) - 2)
                    message = message.Remove(message.IndexOf("&"), 2);
                if (message.Length <= num)
                {
                    list.Add(message);
                    break;
                }

                var num2 = num - 1;
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
                        if (message.Length == 0 || num == 0) return list;
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
                if (list.Count == 1) num = 60;
                var num3 = list[list.Count - 1].LastIndexOf('&');
                if (num3 == -1) continue;
                if (num3 < list[list.Count - 1].Length - 1)
                {
                    var c = list[list.Count - 1][num3 + 1];
                    if ("0123456789abcdef".IndexOf(c) != -1) str = "&" + c;
                    if (num3 == list[list.Count - 1].Length - 1)
                        list[list.Count - 1] = list[list.Count - 1].Substring(0, list[list.Count - 1].Length - 2);
                }
                else if (message.Length != 0)
                {
                    var c2 = message[0];
                    if ("0123456789abcdef".IndexOf(c2) != -1) str = "&" + c2;
                    list[list.Count - 1] = list[list.Count - 1].Substring(0, list[list.Count - 1].Length - 1);
                    message = message.Substring(1);
                }
            }

            return list;
        }

        // Token: 0x06000B2D RID: 2861 RVA: 0x0004286C File Offset: 0x00040A6C
        private static List<string> Wordwrap(string message)
        {
            var list = new List<string>();
            message = Regex.Replace(message, "(&[0-9a-f])+(&[0-9a-f])", "$2");
            message = Regex.Replace(message, "(&[0-9a-f])+$", "");
            var num = 63;
            var str = "";
            while (message.Length > 0)
            {
                if (list.Count > 0)
                    message = !(message[0].ToString() == "&") ? "> " + str + message.Trim() : "> " + message.Trim();
                if (message.IndexOf("&") == message.IndexOf("&", message.IndexOf("&") + 1) - 2)
                    message = message.Remove(message.IndexOf("&"), 2);
                if (message.Length <= num)
                {
                    list.Add(message);
                    break;
                }

                var num2 = num - 1;
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
                        if (message.Length == 0 || num == 0) return list;
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
                if (list.Count == 1) num = 63;
                var num3 = list[list.Count - 1].LastIndexOf('&');
                if (num3 == -1) continue;
                if (num3 < list[list.Count - 1].Length - 1)
                {
                    var c = list[list.Count - 1][num3 + 1];
                    if ("0123456789abcdef".IndexOf(c) != -1) str = "&" + c;
                    if (num3 == list[list.Count - 1].Length - 1)
                        list[list.Count - 1] = list[list.Count - 1].Substring(0, list[list.Count - 1].Length - 2);
                }
                else if (message.Length != 0)
                {
                    var c2 = message[0];
                    if ("0123456789abcdef".IndexOf(c2) != -1) str = "&" + c2;
                    list[list.Count - 1] = list[list.Count - 1].Substring(0, list[list.Count - 1].Length - 1);
                    message = message.Substring(1);
                }
            }

            return list;
        }

        // Token: 0x06000B2E RID: 2862 RVA: 0x00042B90 File Offset: 0x00040D90
        public static bool ValidName(string name)
        {
            var text = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz01234567890._-+@";
            foreach (var value in name)
                if (text.IndexOf(value) == -1)
                    return false;
            return true;
        }

        // Token: 0x06000B2F RID: 2863 RVA: 0x00042BD4 File Offset: 0x00040DD4
        public static byte[] GZip(byte[] bytes)
        {
            byte[] result;
            using (var memoryStream = new MemoryStream())
            {
                using (var gzipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
                {
                    gzipStream.Write(bytes, 0, bytes.Length);
                }

                memoryStream.Position = 0L;
                bytes = new byte[memoryStream.Length];
                memoryStream.Read(bytes, 0, (int) memoryStream.Length);
                result = bytes;
            }

            return result;
        }

        // Token: 0x06000B30 RID: 2864 RVA: 0x00042C58 File Offset: 0x00040E58
        public static byte[] HTNO(ushort x)
        {
            var bytes = BitConverter.GetBytes(x);
            Array.Reverse(bytes);
            return bytes;
        }

        // Token: 0x06000B31 RID: 2865 RVA: 0x00042C74 File Offset: 0x00040E74
        public static ushort NTHO(byte[] x, int offset)
        {
            var array = new byte[2];
            Buffer.BlockCopy(x, offset, array, 0, 2);
            Array.Reverse(array);
            return BitConverter.ToUInt16(array, 0);
        }

        // Token: 0x06000B32 RID: 2866 RVA: 0x00042CA0 File Offset: 0x00040EA0
        public static byte[] HTNO(short x)
        {
            var bytes = BitConverter.GetBytes(x);
            Array.Reverse(bytes);
            return bytes;
        }

        // Token: 0x06000B33 RID: 2867 RVA: 0x00042CBC File Offset: 0x00040EBC
        private bool CheckBlockSpam()
        {
            if (spamBlockLog.Count >= spamBlockCount)
            {
                var value = spamBlockLog.Dequeue();
                var totalSeconds = DateTime.Now.Subtract(value).TotalSeconds;
                if (totalSeconds < spamBlockTimer && !ignoreGrief)
                {
                    Kick("You were kicked by antigrief system. Slow down.");
                    SendMessage(string.Format("{0} was kicked for suspected griefing.", "&c" + PublicName));
                    Server.s.Log(string.Concat(name, " was kicked for block spam (", spamBlockCount, " blocks in ",
                        totalSeconds, " seconds)"));
                    return true;
                }
            }

            spamBlockLog.Enqueue(DateTime.Now);
            return false;
        }

        // Token: 0x06000B34 RID: 2868 RVA: 0x00042DB0 File Offset: 0x00040FB0
        public static void IRCSay(string message, bool opchat = false)
        {
            if (Server.irc) IRCBot.Say(message, opchat);
        }

        // Token: 0x06000B35 RID: 2869 RVA: 0x00042DC0 File Offset: 0x00040FC0
        public void UseArmor()
        {
            armorTime = DateTime.Now;
        }

        // Token: 0x06000B36 RID: 2870 RVA: 0x00042DD0 File Offset: 0x00040FD0
        private bool IsIPLocal()
        {
            if (ip == "127.0.0.1" || ip.StartsWith("192.168.") || ip.StartsWith("10.")) return true;
            if (ip.StartsWith("172."))
            {
                var array = ip.Split('.');
                if (array.Length >= 2)
                    try
                    {
                        var num = Convert.ToInt32(array[1]);
                        return num >= 16 && num <= 31;
                    }
                    catch
                    {
                        return false;
                    }
            }

            return false;
        }

        // Token: 0x06000B4A RID: 2890 RVA: 0x00042FF8 File Offset: 0x000411F8
        public void SaveStarsCount()
        {
            try
            {
                DBInterface.ExecuteQuery(string.Concat("UPDATE Stars SET GoldStars = ", ExtraData["gold_stars_count"],
                    ", SilverStars = ", ExtraData["silver_stars_count"], ", BronzeStars = ",
                    ExtraData["bronze_stars_count"], ", RottenStars = ", ExtraData["rotten_stars_count"],
                    " WHERE Name = '", name, "';"));
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        // Token: 0x06000B4B RID: 2891 RVA: 0x000430BC File Offset: 0x000412BC
        public void SendEmptyLine()
        {
            CmdClearChat.SendEmptyChatMessages(this);
        }

        // Token: 0x06000B4C RID: 2892 RVA: 0x000430C4 File Offset: 0x000412C4
        public static void GlobalMessageLevelSendEmptyLine(Level level)
        {
            players.ForEachSync(delegate(Player p)
            {
                if (p.level == level) p.SendEmptyLine();
            });
        }

        // Token: 0x06000B4D RID: 2893 RVA: 0x000430F4 File Offset: 0x000412F4
        public string ReadLine()
        {
            var wh = new EventWaitHandle(false, EventResetMode.ManualReset);
            var line = "";
            FilterInput value = delegate(ref string t, out bool h)
            {
                line = t;
                if (t.StartsWith("."))
                {
                    t = t.TrimStart('.');
                    h = false;
                    return;
                }

                h = true;
                wh.Set();
            };
            filterInput += value;
            wh.WaitOne();
            filterInput -= value;
            if (line.Trim().ToLower() == "/a") return null;
            return line;
        }

        // Token: 0x06000B4E RID: 2894 RVA: 0x0004316C File Offset: 0x0004136C
        protected void OnMapChanged(Level from, Level to)
        {
            var mapChanged = MapChanged;
            if (mapChanged != null) mapChanged(null, new MapChangeEventArgs(this, from, to));
        }

        // Token: 0x06000B4F RID: 2895 RVA: 0x00043194 File Offset: 0x00041394
        public void SendToMap(Level level)
        {
            var level2 = this.level;
            Loading = true;
            DiePlayers();
            DieBots();
            GlobalDie(this, false);
            this.level = level;
            SendUserMOTD(level.mapType == MapType.Home);
            SendMap();
            Thread.Sleep(1);
            if (!hidden)
                GlobalSpawn(this, (ushort) ((0.5 + level.spawnx) * 32.0), (ushort) ((1 + level.spawny) * 32),
                    (ushort) ((0.5 + level.spawnz) * 32.0), level.rotx, level.roty, true);
            SendSpawn(byte.MaxValue, color + (IsRefree ? "[REF]" : "") + PublicName, ModelName,
                (ushort) ((0.5 + level.spawnx) * 32.0), (ushort) ((1 + level.spawny) * 32),
                (ushort) ((0.5 + level.spawnz) * 32.0), level.rotx, level.roty);
            SpawnPlayers();
            SpawnBots();
            Loading = false;
            level2.NotifyPopulationChanged();
            OnMapChanged(level2, level);
        }

        // Token: 0x06000B56 RID: 2902 RVA: 0x00043350 File Offset: 0x00041550
        public void LoadPlayerAppearance()
        {
            using (var dataTable =
                DBInterface.fillData("SELECT Model, Skin FROM PlayerAppearance WHERE Player = " + DbId))
            {
                if (dataTable.Rows.Count != 0)
                {
                    ModelName = dataTable.Rows[0]["Model"].ToString();
                    SkinName = dataTable.Rows[0]["Skin"].ToString();
                }
            }
        }

        // Token: 0x06000B57 RID: 2903 RVA: 0x000433E8 File Offset: 0x000415E8
        public void SavePlayerAppearance()
        {
            using (var dataTable = DBInterface.fillData("SELECT Model FROM PlayerAppearance WHERE Player = " + DbId))
            {
                var dictionary = new Dictionary<string, object>();
                dictionary.Add("@Player", DbId);
                dictionary.Add("@Model", ModelName);
                dictionary.Add("@Skin", SkinName);
                if (dataTable.Rows.Count == 0)
                    DBInterface.ExecuteQuery(
                        "INSERT INTO PlayerAppearance (Player, Model, Skin) VALUES (@Player, @Model, @Skin);",
                        dictionary);
                else
                    DBInterface.ExecuteQuery(
                        "UPDATE PlayerAppearance SET Model = @Model, Skin = @Skin WHERE Player = @Player", dictionary);
            }
        }

        // Token: 0x06000B58 RID: 2904 RVA: 0x00043490 File Offset: 0x00041690
        public void OnPositionChanged(PositionChangedEventArgs e)
        {
            var positionChanged = PositionChanged;
            if (positionChanged != null) positionChanged(this, e);
        }

        // Token: 0x1400000D RID: 13
        // (add) Token: 0x06000B59 RID: 2905 RVA: 0x000434B0 File Offset: 0x000416B0
        // (remove) Token: 0x06000B5A RID: 2906 RVA: 0x000434E8 File Offset: 0x000416E8
        public event EventHandler<PositionChangedEventArgs> PositionChanged;

        // Token: 0x0200016C RID: 364
        public struct Pos3I
        {
            // Token: 0x040005A2 RID: 1442
            public int X;

            // Token: 0x040005A3 RID: 1443
            public int Y;

            // Token: 0x040005A4 RID: 1444
            public int Z;
        }

        // Token: 0x0200016F RID: 367
        // (Invoke) Token: 0x06000B75 RID: 2933
        private delegate void FilterInput(ref string text, out bool handled);

        // Token: 0x02000170 RID: 368
        public struct CopyPos
        {
            // Token: 0x040005A5 RID: 1445
            public ushort x;

            // Token: 0x040005A6 RID: 1446
            public ushort y;

            // Token: 0x040005A7 RID: 1447
            public ushort z;

            // Token: 0x040005A8 RID: 1448
            public byte type;
        }

        // Token: 0x02000171 RID: 369
        public struct UndoPos
        {
            // Token: 0x040005A9 RID: 1449
            public ushort x;

            // Token: 0x040005AA RID: 1450
            public ushort y;

            // Token: 0x040005AB RID: 1451
            public ushort z;

            // Token: 0x040005AC RID: 1452
            public byte type;

            // Token: 0x040005AD RID: 1453
            public byte newtype;

            // Token: 0x040005AE RID: 1454
            public string mapName;

            // Token: 0x040005AF RID: 1455
            public DateTime timePlaced;
        }
    }
}