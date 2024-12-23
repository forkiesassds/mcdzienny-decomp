using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using System.Timers;
using MCDzienny.CpeApi;
using MCDzienny.Gui;
using MCDzienny.Levels.Info;
using MCDzienny.MultiMessages;
using MCDzienny.Settings;
using Timer = System.Timers.Timer;

namespace MCDzienny
{
    public sealed class Level
    {

        static readonly Random randLRG = new Random();

        static readonly Random rand_x = new Random();

        static readonly Random rand_xx = new Random();

        static readonly ResourceManager rm = new ResourceManager("MCDzienny.Lang.Level", Assembly.GetExecutingAssembly());

        readonly List<Check> ListCheck = new List<Check>();

        readonly List<Update> ListUpdate = new List<Update>();

        readonly SynchronizedDictionary<int, OwnedBlockInfo> ownedBlocks = new SynchronizedDictionary<int, OwnedBlockInfo>();

        readonly Timer physicsTimer = new Timer();

        readonly object playersLocker = new object();

        readonly Random randLavaTypeC = new Random();

        public bool ai = true;

        HashSet<string> allowedPlayers = new HashSet<string>();

        internal bool allowHacks = true;

        public bool backDup;

        public List<BlockPos> blockCache = new List<BlockPos>();

        int blockCacheCollect = 1;

        public BlockMap blockMap;

        public byte[] blocks;

        int change;

        public bool changed;

        int changeLD;

        int changeLRG;

        int changeLW;

        int changeO;

        Check[] checks = new Check[0];

        Dictionary<int, CommandActionPair> commandActionsBuild;

        Dictionary<int, CommandActionPair> commandActionsHit;

        Dictionary<int, CommandActionPair> commandActionsWalk;

        public bool creativeMode;

        public CTFGame ctfgame = new CTFGame();

        public bool ctfmode;

        public int currentUndo;

        public bool Death;

        internal string directoryPath = "levels";

        public int drown = 70;

        public int fall = 9;

        public string fileName;

        public bool fishstill;

        bool GrassDestroy_ = true;

        bool GrassGrow_ = true;

        public bool hardcore;

        public int id;

        int indexLD;

        int indexLR;

        int indexLRG;

        int indexLRO;

        int indexLW;

        bool isPublic = true;

        public byte jailrotx;

        public byte jailroty;

        bool Killer_ = true;

        public int lastCheck;

        public int lastUpdate;

        public bool lavarage;

        int lavaSpeed = LavaSettings.All.LavaMovementDelay;

        public bool lavaUp;

        string mapOwner = "";

        internal MapSettingsManager mapSettingsManager;

        public MapType mapType;

        string motd_ = "ignore";

        public int overload = 90000;

        public bool physPause;

        public DateTime physResume;

        public Timer physTimer = new Timer(1000.0);

        public int playerLimit;

        public int playersCount;

        public bool realistic = true;

        public byte rotx;

        public byte roty;

        public bool rp = true;

        public int speedPhysics = 240;

        List<int> tempRemove = new List<int>();

        public string theme = "Normal";

        public List<UndoPos> UndoBuffer = new List<UndoPos>();

        public bool unload = true;

        volatile bool unloaded;

        bool worldChat_ = true;

        public List<Zone> ZoneList;

        public Level(string name, ushort x, ushort y, ushort z, string type)
        {
            Info = new LevelInfo();
            IsPillaringAllowed = true;
            width = x;
            height = y;
            depth = z;
            if (width < 16)
            {
                width = 16;
            }
            if (height < 16)
            {
                height = 16;
            }
            if (depth < 16)
            {
                depth = 16;
            }
            this.name = name;
            blocks = new byte[width * height * depth];
            ZoneList = new List<Zone>();
            switch (type)
            {
                case "flat":
                case "pixel":
                {
                    ushort num = (ushort)(height / 2);
                    for (x = 0; x < width; x++)
                    {
                        for (z = 0; z < depth; z++)
                        {
                            for (y = 0; y < height; y++)
                            {
                                switch (type)
                                {
                                    case "flat":
                                        if (y != num)
                                        {
                                            SetTile(x, y, z, (byte)(y < num ? 3 : 0));
                                        }
                                        else
                                        {
                                            SetTile(x, y, z, 2);
                                        }
                                        break;
                                    case "pixel":
                                        if (y == 0)
                                        {
                                            SetTile(x, y, z, 7);
                                        }
                                        else if (x == 0 || x == width - 1 || z == 0 || z == depth - 1)
                                        {
                                            SetTile(x, y, z, 36);
                                        }
                                        break;
                                }
                            }
                        }
                    }
                    break;
                }
                case "island":
                case "mountains":
                case "ocean":
                case "forest":
                case "desert":
                    Server.MapGen.GenerateMap(this, type);
                    break;
            }
            spawnx = (ushort)(width / 2);
            spawny = (ushort)(height * 0.75f);
            spawnz = (ushort)(depth / 2);
            rotx = 0;
            roty = 0;
            physicsTimer.Interval = speedPhysics;
            physicsTimer.Elapsed += physicsTimer_Elapsed;
            physicsTimer.AutoReset = false;
            StartPhysics();
        }

        [Browsable(false)]
        public LevelInfo Info { get; set; }

        public uint MapDbId { get; private set; }

        [ReadOnly(true)]
        [Description("Displays the name of the map without email domain if such exists in the name.")]
        [Category("General")]
        public string PublicName
        {
            get
            {
                int num = name.IndexOf('@');
                if (num != -1)
                {
                    return name.Substring(0, num);
                }
                return name;
            }
        }

        [Category("General")]
        [ReadOnly(true)]
        [Description("Displays the name of the map.")]
        public string name { get; set; }

        [Description("Shows the width of the map.")]
        [Category("Dimensions")]
        [ReadOnly(true)]
        public ushort width { get; set; }

        [Category("Dimensions")]
        [ReadOnly(true)]
        [Description("Shows the height of the map.")]
        public ushort height { get; set; }

        [Description("Shows the depth of the map.")]
        [Category("Dimensions")]
        [ReadOnly(true)]
        public ushort depth { get; set; }

        public string Owner
        {
            get
            {
                if (mapType == MapType.Home)
                {
                    return name;
                }
                if (mapType == MapType.MyMap)
                {
                    return mapOwner;
                }
                return "";
            }
        }

        public bool UnloadLock { get; set; }

        [Browsable(false)]
        public int Weight { get; set; }

        [Browsable(false)]
        public List<CommandBlock> CommandBlocks
        {
            get
            {
                if (mapSettingsManager != null)
                {
                    return mapSettingsManager.commandBlocks;
                }
                return null;
            }
        }

        [Browsable(false)]
        public Dictionary<int, CommandActionPair> CommandActionsBuild
        {
            get
            {
                if (commandActionsBuild != null)
                {
                    return commandActionsBuild;
                }
                if (mapSettingsManager != null)
                {
                    commandActionsBuild = new Dictionary<int, CommandActionPair>();
                    if (CommandBlocks != null)
                    {
                        foreach (CommandBlock commandBlock in CommandBlocks)
                        {
                            var list = new List<CommandElement>();
                            var list2 = new List<ActionElement>();
                            foreach (CommandElement commandElement in commandBlock.commandElements)
                            {
                                if ((commandElement.blockTrigger.Value & BlockTrigger.Build) == BlockTrigger.Build)
                                {
                                    list.Add(commandElement);
                                }
                            }
                            foreach (ActionElement actionElement in commandBlock.actionElements)
                            {
                                if ((actionElement.blockTrigger.Value & BlockTrigger.Build) == BlockTrigger.Build)
                                {
                                    list2.Add(actionElement);
                                }
                            }
                            if (list.Count > 0 || list2.Count > 0)
                            {
                                commandActionsBuild.Add(PosToInt(commandBlock.x, commandBlock.y, commandBlock.z),
                                                        new CommandActionPair(commandBlock.changeAction.Value, list, list2));
                            }
                        }
                    }
                    return commandActionsBuild;
                }
                return null;
            }
        }

        [Browsable(false)]
        public Dictionary<int, CommandActionPair> CommandActionsHit
        {
            get
            {
                if (commandActionsHit != null)
                {
                    return commandActionsHit;
                }
                if (mapSettingsManager != null)
                {
                    commandActionsHit = new Dictionary<int, CommandActionPair>();
                    if (CommandBlocks != null)
                    {
                        foreach (CommandBlock commandBlock in CommandBlocks)
                        {
                            var list = new List<CommandElement>();
                            var list2 = new List<ActionElement>();
                            foreach (CommandElement commandElement in commandBlock.commandElements)
                            {
                                if ((commandElement.blockTrigger.Value & BlockTrigger.Hit) == BlockTrigger.Hit)
                                {
                                    list.Add(commandElement);
                                }
                            }
                            foreach (ActionElement actionElement in commandBlock.actionElements)
                            {
                                if ((actionElement.blockTrigger.Value & BlockTrigger.Hit) == BlockTrigger.Hit)
                                {
                                    list2.Add(actionElement);
                                }
                            }
                            if (list.Count > 0 || list2.Count > 0)
                            {
                                commandActionsHit.Add(PosToInt(commandBlock.x, commandBlock.y, commandBlock.z),
                                                      new CommandActionPair(commandBlock.changeAction.Value, list, list2));
                            }
                        }
                    }
                    return commandActionsHit;
                }
                return null;
            }
        }

        [Browsable(false)]
        public Dictionary<int, CommandActionPair> CommandActionsWalk
        {
            get
            {
                if (commandActionsWalk != null)
                {
                    return commandActionsWalk;
                }
                if (mapSettingsManager != null)
                {
                    commandActionsWalk = new Dictionary<int, CommandActionPair>();
                    if (CommandBlocks != null)
                    {
                        foreach (CommandBlock commandBlock in CommandBlocks)
                        {
                            var list = new List<CommandElement>();
                            var list2 = new List<ActionElement>();
                            foreach (CommandElement commandElement in commandBlock.commandElements)
                            {
                                if ((commandElement.blockTrigger.Value & BlockTrigger.Walk) == BlockTrigger.Walk)
                                {
                                    list.Add(commandElement);
                                }
                            }
                            foreach (ActionElement actionElement in commandBlock.actionElements)
                            {
                                if ((actionElement.blockTrigger.Value & BlockTrigger.Walk) == BlockTrigger.Walk)
                                {
                                    list2.Add(actionElement);
                                }
                            }
                            if (list.Count > 0 || list2.Count > 0)
                            {
                                commandActionsWalk.Add(PosToInt(commandBlock.x, commandBlock.y, commandBlock.z),
                                                       new CommandActionPair(commandBlock.changeAction.Value, list, list2));
                            }
                        }
                    }
                    return commandActionsWalk;
                }
                return null;
            }
        }

        [Browsable(false)]
        public bool IsMapBeingBackuped { get; set; }

        public HashSet<string> AllowedPlayers { get { return allowedPlayers; } set { allowedPlayers = value; } }

        public bool IsPublic { get { return isPublic; } set { isPublic = value; } }

        [Description("Describes the spawn point X coordinate.")]
        [Category("General")]
        public ushort spawnx { get; set; }

        [Description("Describes the spawn point Y coordinate.")]
        [Category("General")]
        public ushort spawny { get; set; }

        [Category("General")]
        [Description("Describes the spawn point Z coordinate.")]
        public ushort spawnz { get; set; }

        [Category("General")]
        [Description("Describes the spawn rotation X coordinate.")]
        public byte SpawnRotX { get { return rotx; } set { rotx = value; } }

        [Category("General")]
        [Description("Describes the spawn rotation Y coordinate.")]
        public byte SpawnRotY { get { return roty; } set { roty = value; } }

        [Description("Describes the jail point X coordinate.")]
        [Category("General")]
        public ushort jailx { get; set; }

        [Category("General")]
        [Description("Describes the jail point X coordinate.")]
        public ushort jaily { get; set; }

        [Description("Describes the jail point X coordinate.")]
        [Category("General")]
        public ushort jailz { get; set; }

        [Description("Indicates whether edge water spreads.")]
        [Category("General")]
        public bool edgeWater { get; set; }

        [Browsable(false)]
        public int PlayersCount { get { return Player.players.Count(pl => pl.level == this); } }

        [Browsable(false)]
        public object PlayersSynchronizationObject { get { return playersLocker; } }

        [Browsable(false)]
        public SynchronizedDictionary<int, OwnedBlockInfo> OwnedBlocks { get { return ownedBlocks; } }

        [Description("Describes the physics level. Valid values 0,1,2,3,4. Levels from none to doors only.")]
        [Category("General")]
        public int physics { get; set; }

        [Category("General")]
        [Description("Indicates whether liquids spreads finitly or infinitly.")]
        public bool finite { get; set; }

        [Description("Determines whether the instant building mode is on.")]
        [Category("General")]
        public bool Instant { get; set; }

        [Category("General")]
        [Description("Turns killer blocks on or off.")]
        public bool Killer { get { return Killer_; } set { Killer_ = value; } }

        [Category("General")]
        [Description("Determines whether a grass is changed into dirt when a block is placed over it.")]
        public bool GrassDestroy { get { return GrassDestroy_; } set { GrassDestroy_ = value; } }

        [Description("Determines whether the grass grows after placing a dirt block.")]
        [Category("General")]
        public bool GrassGrow { get { return GrassGrow_; } set { GrassGrow_ = value; } }

        [Description("Determines whether the cross-maps or only map chat is enabled.")]
        [Category("General")]
        public bool worldChat { get { return worldChat_; } set { worldChat_ = value; } }

        [Description("It's the message that is showed during the map loading.")]
        [Category("General")]
        public string motd { get { return motd_; } set { motd_ = value; } }

        [Category("General")]
        [Description("Defines permission required to visit the map.")]
        public LevelPermission permissionvisit { get; set; }

        [Category("General")]
        [Description("Defines permission required to build on the map.")]
        public LevelPermission permissionbuild { get; set; }

        [Category("General")]
        [Description("Describes whether pillaring is allowed or not.")]
        public bool IsPillaringAllowed { get; set; }

        public event EventHandler<PlayerJoinedEventArgs> PlayerJoined;

        public List<AABB> GetCubes(AABB aabb)
        {
            var list = new List<AABB>();
            int num = (int)aabb.x0;
            int num2 = (int)aabb.x1 + 1;
            int num3 = (int)aabb.y0;
            int num4 = (int)aabb.y1 + 1;
            int num5 = (int)aabb.z0;
            int num6 = (int)aabb.z1 + 1;
            if (aabb.x0 < 0f)
            {
                num--;
            }
            if (aabb.y0 < 0f)
            {
                num3--;
            }
            if (aabb.z0 < 0f)
            {
                num5--;
            }
            for (int i = num; i < num2; i++)
            {
                for (int j = num3; j < num4; j++)
                {
                    for (int k = num5; k < num6; k++)
                    {
                        AABB collisionBox2;
                        if (i >= 0 && j >= 0 && k >= 0 && i < width && j < height && k < depth)
                        {
                            byte tile = GetTile(i, j, k);
                            AABB collisionBox = Block.GetCollisionBox(tile, i, j, k);
                            if (tile > 0 && collisionBox != null && aabb.intersectsInner(collisionBox))
                            {
                                list.Add(collisionBox);
                            }
                        }
                        else if ((i < 0 || j < 0 || k < 0 || i >= width || k >= depth) && (collisionBox2 = Block.GetCollisionBox(7, i, j, k)) != null &&
                                 aabb.intersectsInner(collisionBox2))
                        {
                            list.Add(collisionBox2);
                        }
                    }
                }
            }
            return list;
        }

        public bool ContainsAnyLiquid(AABB aabb)
        {
            int num = (int)aabb.x0;
            int num2 = (int)aabb.x1 + 1;
            int num3 = (int)aabb.y0;
            int num4 = (int)aabb.y1 + 1;
            int num5 = (int)aabb.z0;
            int num6 = (int)aabb.z1 + 1;
            if (aabb.x0 < 0f)
            {
                num--;
            }
            if (aabb.y0 < 0f)
            {
                num3--;
            }
            if (aabb.z0 < 0f)
            {
                num5--;
            }
            if (num < 0)
            {
                num = 0;
            }
            if (num3 < 0)
            {
                num3 = 0;
            }
            if (num5 < 0)
            {
                num5 = 0;
            }
            if (num2 > width)
            {
                num2 = width;
            }
            if (num4 > depth)
            {
                num4 = depth;
            }
            if (num6 > height)
            {
                num6 = height;
            }
            for (int i = num; i < num2; i++)
            {
                for (int j = num3; j < num4; j++)
                {
                    for (int k = num5; k < num6; k++)
                    {
                        byte tile = GetTile(i, j, k);
                        if (tile != 7 && Block.IsLiquid(tile))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool containsBlock(AABB aabb, byte block)
        {
            int num = (int)aabb.x0;
            int num2 = (int)aabb.x1 + 1;
            int num3 = (int)aabb.y0;
            int num4 = (int)aabb.y1 + 1;
            int num5 = (int)aabb.z0;
            int num6 = (int)aabb.z1 + 1;
            if (aabb.x0 < 0f)
            {
                num--;
            }
            if (aabb.y0 < 0f)
            {
                num3--;
            }
            if (aabb.z0 < 0f)
            {
                num5--;
            }
            if (num < 0)
            {
                num = 0;
            }
            if (num3 < 0)
            {
                num3 = 0;
            }
            if (num5 < 0)
            {
                num5 = 0;
            }
            if (num2 > width)
            {
                num2 = width;
            }
            if (num4 > depth)
            {
                num4 = depth;
            }
            if (num6 > height)
            {
                num6 = height;
            }
            for (int i = num; i < num2; i++)
            {
                for (int j = num3; j < num4; j++)
                {
                    for (int k = num5; k < num6; k++)
                    {
                        byte tile = GetTile(i, j, k);
                        if (tile == block)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public float getBrightness(int x, int y, int z)
        {
            if (!IsLit(x, y, z))
            {
                return 0.6f;
            }
            return 1f;
        }

        public bool containsLiquid(AABB aabb, byte block)
        {
            int num = (int)aabb.x0;
            int num2 = (int)aabb.x1 + 1;
            int num3 = (int)aabb.y0;
            int num4 = (int)aabb.y1 + 1;
            int num5 = (int)aabb.z0;
            int num6 = (int)aabb.z1 + 1;
            if (aabb.x0 < 0f)
            {
                num--;
            }
            if (aabb.y0 < 0f)
            {
                num3--;
            }
            if (aabb.z0 < 0f)
            {
                num5--;
            }
            if (num < 0)
            {
                num = 0;
            }
            if (num3 < 0)
            {
                num3 = 0;
            }
            if (num5 < 0)
            {
                num5 = 0;
            }
            if (num2 > width)
            {
                num2 = width;
            }
            if (num4 > depth)
            {
                num4 = depth;
            }
            if (num6 > height)
            {
                num6 = height;
            }
            block = Block.ToMoving(block);
            for (int i = num; i < num2; i++)
            {
                for (int j = num3; j < num4; j++)
                {
                    for (int k = num5; k < num6; k++)
                    {
                        byte tile = GetTile(i, j, k);
                        if (Block.ToMoving(tile) == block)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        void physicsTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            int num = speedPhysics;
            if (unloaded || physics == 0)
            {
                return;
            }
            if (ListCheck.Count != 0)
            {
                if (GeneralSettings.All.IntelliSys && InteliSys.pendingPacketsAvg > GeneralSettings.All.AvgStop)
                {
                    num = 3000;
                }
                else
                {
                    try
                    {
                        DateTime now = DateTime.Now;
                        CalcPhysics();
                        TimeSpan timeSpan = DateTime.Now - now;
                        int num2 = speedPhysics - (int)timeSpan.TotalMilliseconds;
                        if (num2 < (int)(-overload * 0.75f))
                        {
                            if (num2 < -overload)
                            {
                                if (!Server.physicsRestart)
                                {
                                    setPhysics(0);
                                }
                                ClearPhysics();
                                Player.GlobalMessage(string.Format(rm.GetString("PhysicsShutdownGlobalMessage"), name));
                                Server.s.Log("Physics shutdown on " + name);
                                num2 = speedPhysics;
                            }
                            else
                            {
                                Player.players.ForEach(delegate(Player p)
                                {
                                    if (p.level == this)
                                    {
                                        Player.SendMessage(p, rm.GetString("PhysicsWarning"));
                                    }
                                });
                                Server.s.Log("Physics warning on " + name);
                            }
                            num = num2;
                        }
                    }
                    catch (Exception ex)
                    {
                        num = speedPhysics;
                        Server.ErrorLog(ex);
                    }
                }
            }
            physicsTimer.Interval = num;
            if (Server.mono)
            {
                physicsTimer.Start();
            }
        }

        public void CopyBlocks(byte[] source, int offset)
        {
            blocks = new byte[width * height * depth];
            Array.Copy(source, offset, blocks, 0, blocks.Length);
            for (int i = 0; i < blocks.Length; i++)
            {
                if (blocks[i] >= 50)
                {
                    blocks[i] = 0;
                }
                if (blocks[i] == 9)
                {
                    blocks[i] = 8;
                }
                else if (blocks[i] == 8)
                {
                    blocks[i] = 9;
                }
                else if (blocks[i] == 10)
                {
                    blocks[i] = 11;
                }
                else if (blocks[i] == 11)
                {
                    blocks[i] = 10;
                }
            }
        }

        public bool Unload(bool reload = false)
        {
            if (!reload)
            {
                if (Server.DefaultLevel == this)
                {
                    return false;
                }
                if (UnloadLock)
                {
                    return false;
                }
                if (name.Contains("&cMuseum "))
                {
                    return false;
                }
                var playersToBeMoved = new List<Player>();
                Player.players.ForEach(delegate(Player p)
                {
                    if (p.level == this)
                    {
                        playersToBeMoved.Add(p);
                    }
                });
                foreach (Player item in playersToBeMoved)
                {
                    try
                    {
                        Command.all.Find("goto").Use(item, Server.DefaultLevel.name);
                    }
                    catch {}
                }
                if (changed && mapType != MapType.Lava && mapType != MapType.Zombie)
                {
                    Save();
                    SaveChanges();
                }
            }
            if (unloaded)
            {
                return true;
            }
            unloaded = true;
            physicsTimer.Stop();
            physicsTimer.Close();
            Server.RemoveLevel(this);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Player.GlobalMessageOps(string.Format(rm.GetString("MapUnloadedGlobalMessage"), name + Server.DefaultColor));
            Server.s.Log(name + " was unloaded.");
            RefreshUnloadedMapsInGUI();
            return true;
        }

        static void RefreshUnloadedMapsInGUI()
        {
            if (!Server.CLI)
            {
                Window.thisWindow.RefreshUnloadedMapsList();
            }
        }

        public void SaveChanges()
        {
            if (blockCache.Count == 0)
            {
                return;
            }
            if (mapType == MapType.Lava || mapType == MapType.Zombie)
            {
                if (blockCacheCollect % 25 == 0)
                {
                    blockCache.Clear();
                }
                blockCacheCollect++;
                return;
            }
            var list = new List<BlockPos>(blockCache);
            blockCache.Clear();
            try
            {
                if (mapType == MapType.MyMap)
                {
                    if (Server.useMySQL)
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        stringBuilder.Append("INSERT INTO `Blocks` (Map, Username, TimePerformed, X, Y, Z, type, deleted) VALUES ");
                        foreach (BlockPos item in list)
                        {
                            object[] array = new object[17]
                            {
                                "(", MapDbId, ", '", item.name, "', '", null, null, null, null, null, null, null, null, null, null, null, null
                            };
                            DateTime timePerformed = item.TimePerformed;
                            array[5] = timePerformed.ToString("yyyy-MM-dd HH:mm:ss");
                            array[6] = "', ";
                            array[7] = (int)item.x;
                            array[8] = ", ";
                            array[9] = (int)item.y;
                            array[10] = ", ";
                            array[11] = (int)item.z;
                            array[12] = ", ";
                            array[13] = item.type;
                            array[14] = ", ";
                            array[15] = item.deleted;
                            array[16] = "), ";
                            stringBuilder.Append(string.Concat(array));
                        }
                        stringBuilder.Remove(stringBuilder.Length - 2, 2);
                        DBInterface.ExecuteQuery(stringBuilder.ToString());
                    }
                    else
                    {
                        string commandText = "INSERT INTO `Blocks` (Map, Username, TimePerformed, X, Y, Z, Type, Deleted) VALUES ";
                        string[] array2 = new string[list.Count];
                        for (int i = 0; i < list.Count; i++)
                        {
                            int num = i;
                            object[] array3 = new object[17]
                            {
                                "(", MapDbId, ", '", list[i].name, "', '", null, null, null, null, null, null, null, null, null, null, null, null
                            };
                            DateTime timePerformed2 = list[i].TimePerformed;
                            array3[5] = timePerformed2.ToString("yyyy-MM-dd HH:mm:ss");
                            array3[6] = "', ";
                            array3[7] = (int)list[i].x;
                            array3[8] = ", ";
                            array3[9] = (int)list[i].y;
                            array3[10] = ", ";
                            array3[11] = (int)list[i].z;
                            array3[12] = ", ";
                            array3[13] = list[i].type;
                            array3[14] = ", ";
                            array3[15] = list[0].deleted ? 1 : 0;
                            array3[16] = ")";
                            array2[num] = string.Concat(array3);
                        }
                        DBInterface.Transaction(commandText, array2);
                    }
                }
                else if (Server.useMySQL)
                {
                    StringBuilder stringBuilder2 = new StringBuilder();
                    stringBuilder2.Append("INSERT INTO `Block" + name + "` (Username, TimePerformed, X, Y, Z, type, deleted) VALUES ");
                    foreach (BlockPos item2 in list)
                    {
                        object[] array4 = new object[15]
                        {
                            "('", item2.name, "', '", null, null, null, null, null, null, null, null, null, null, null, null
                        };
                        DateTime timePerformed3 = item2.TimePerformed;
                        array4[3] = timePerformed3.ToString("yyyy-MM-dd HH:mm:ss");
                        array4[4] = "', ";
                        array4[5] = (int)item2.x;
                        array4[6] = ", ";
                        array4[7] = (int)item2.y;
                        array4[8] = ", ";
                        array4[9] = (int)item2.z;
                        array4[10] = ", ";
                        array4[11] = item2.type;
                        array4[12] = ", ";
                        array4[13] = item2.deleted;
                        array4[14] = "), ";
                        stringBuilder2.Append(string.Concat(array4));
                    }
                    stringBuilder2.Remove(stringBuilder2.Length - 2, 2);
                    DBInterface.ExecuteQuery(stringBuilder2.ToString());
                }
                else
                {
                    string commandText = "INSERT INTO `Block" + name + "` (Username, TimePerformed, X, Y, Z, Type, Deleted) VALUES ";
                    string[] array5 = new string[list.Count];
                    for (int j = 0; j < list.Count; j++)
                    {
                        int num2 = j;
                        object[] array6 = new object[15]
                        {
                            "('", list[j].name, "', '", null, null, null, null, null, null, null, null, null, null, null, null
                        };
                        DateTime timePerformed4 = list[j].TimePerformed;
                        array6[3] = timePerformed4.ToString("yyyy-MM-dd HH:mm:ss");
                        array6[4] = "', ";
                        array6[5] = (int)list[j].x;
                        array6[6] = ", ";
                        array6[7] = (int)list[j].y;
                        array6[8] = ", ";
                        array6[9] = (int)list[j].z;
                        array6[10] = ", ";
                        array6[11] = list[j].type;
                        array6[12] = ", ";
                        array6[13] = list[0].deleted ? 1 : 0;
                        array6[14] = ")";
                        array5[num2] = string.Concat(array6);
                    }
                    DBInterface.Transaction(commandText, array5);
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
            list.Clear();
        }

        public byte GetTile(int x, int y, int z)
        {
            if (x < 0 || x >= width)
            {
                return byte.MaxValue;
            }
            if (y < 0 || y >= height)
            {
                return byte.MaxValue;
            }
            if (z < 0 || z >= depth)
            {
                return byte.MaxValue;
            }
            return blocks[PosToInt(x, y, z)];
        }

        public bool IsSurroundedByAir(int x, int y, int z)
        {
            if (IsAir(x + 1, y, z) && IsAir(x - 1, y, z) && IsAir(x, y, z + 1) && IsAir(x, y, z - 1))
            {
                return true;
            }
            return false;
        }

        public bool IsAir(int x, int y, int z)
        {
            if (Block.IsAir(GetTile(x, y, z)))
            {
                return true;
            }
            return false;
        }

        public byte GetTile(int b)
        {
            ushort x = 0;
            ushort y = 0;
            ushort z = 0;
            IntToPos(b, out x, out y, out z);
            return GetTile(x, y, z);
        }

        public void SetTile(ushort x, ushort y, ushort z, byte type)
        {
            blocks[x + width * z + width * depth * y] = type;
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

        public Player GetClosestPlayer(float x, float y, float z)
        {
            List<Player> copy = Player.players.GetCopy();
            return (from p in copy
                where p.level == this
                orderby p.distanceToSq(x, y, z)
                select p).FirstOrDefault();
        }

        public List<Entity> findEntities(Entity excluded, AABB aabb)
        {
            return blockMap.getEntities(excluded, aabb);
        }

        public MovingObjectPosition clip(Vector3F a, Vector3F b)
        {
            if (float.IsNaN(a.X) || float.IsNaN(a.Y) || float.IsNaN(a.Z) || float.IsNaN(b.X) || float.IsNaN(b.Y) || float.IsNaN(b.Z))
            {
                return null;
            }
            int num = (int)Math.Floor(b.X);
            int num2 = (int)Math.Floor(b.Y);
            int num3 = (int)Math.Floor(b.Z);
            int num4 = (int)Math.Floor(a.X);
            int num5 = (int)Math.Floor(a.Y);
            int num6 = (int)Math.Floor(a.Z);
            int num7 = 20;
            while (num7-- >= 0)
            {
                if (float.IsNaN(a.X) || float.IsNaN(a.Y) || float.IsNaN(a.Z))
                {
                    return null;
                }
                if (num4 == num && num5 == num2 && num6 == num3)
                {
                    return null;
                }
                float num8 = 999f;
                float num9 = 999f;
                float num10 = 999f;
                if (num > num4)
                {
                    num8 = num4 + 1f;
                }
                if (num < num4)
                {
                    num8 = num4;
                }
                if (num2 > num5)
                {
                    num9 = num5 + 1f;
                }
                if (num2 < num5)
                {
                    num9 = num5;
                }
                if (num3 > num6)
                {
                    num10 = num6 + 1f;
                }
                if (num3 < num6)
                {
                    num10 = num6;
                }
                float num11 = 999f;
                float num12 = 999f;
                float num13 = 999f;
                float num14 = b.X - a.X;
                float num15 = b.Y - a.Y;
                float num16 = b.Z - a.Z;
                if (num8 != 999f)
                {
                    num11 = (num8 - a.X) / num14;
                }
                if (num9 != 999f)
                {
                    num12 = (num9 - a.Y) / num15;
                }
                if (num10 != 999f)
                {
                    num13 = (num10 - a.Z) / num16;
                }
                byte b2;
                if (num11 < num12 && num11 < num13)
                {
                    b2 = (byte)(num <= num4 ? 5 : 4);
                    a.X = num8;
                    a.Y += num15 * num11;
                    a.Z += num16 * num11;
                }
                else if (num12 < num13)
                {
                    b2 = (byte)(num2 <= num5 ? 1 : 0);
                    a.X += num14 * num12;
                    a.Y = num9;
                    a.Z += num16 * num12;
                }
                else
                {
                    b2 = (byte)(num3 <= num6 ? 3 : 2);
                    a.X += num14 * num13;
                    a.Y += num15 * num13;
                    a.Z = num10;
                }
                Vector3F vector3F = new Vector3F(0f, 0f, 0f);
                vector3F.X = (float)Math.Floor(a.X);
                num4 = (int)vector3F.X;
                if (b2 == 5)
                {
                    num4--;
                    vector3F.X += 1f;
                }
                vector3F.Y = (float)Math.Floor(a.Y);
                num5 = (int)vector3F.Y;
                if (b2 == 1)
                {
                    num5--;
                    vector3F.Y += 1f;
                }
                vector3F.Z = (float)Math.Floor(a.Z);
                num6 = (int)vector3F.Z;
                if (b2 == 3)
                {
                    num6--;
                    vector3F.Z += 1f;
                }
                byte tile = GetTile(num4, num5, num6);
                if (tile > 0 && !Block.IsLiquid(tile))
                {
                    MovingObjectPosition movingObjectPosition = Block.clip(tile, num4, num5, num6, a, b);
                    if (movingObjectPosition != null)
                    {
                        return movingObjectPosition;
                    }
                }
            }
            return null;
        }

        public static Level ExactFind(string levelName)
        {
            levelName = levelName.ToLower();
            lock (Server.levels)
            {
                foreach (Level level in Server.levels)
                {
                    if (level.name.ToLower() == levelName)
                    {
                        return level;
                    }
                }
            }
            return null;
        }

        public static Level Find(string levelName)
        {
            Level level = null;
            levelName = levelName.ToLower();
            bool flag = false;
            bool flag2 = false;
            lock (Server.levels)
            {
                foreach (Level level2 in Server.levels)
                {
                    if (level2.Owner != "")
                    {
                        continue;
                    }
                    if (level2.name.ToLower() == levelName)
                    {
                        return level2;
                    }
                    if (level2.name.ToLower().Contains(levelName.ToLower()))
                    {
                        if (level == null)
                        {
                            level = level2;
                            flag2 = true;
                        }
                        else
                        {
                            flag = true;
                        }
                    }
                }
                foreach (Level level3 in Server.levels)
                {
                    if (!(level3.Owner == ""))
                    {
                        if (level3.name.ToLower() == levelName)
                        {
                            return level3;
                        }
                        if (!flag2 && level3.name.ToLower().Contains(levelName.ToLower()) && level == null)
                        {
                            level = level3;
                        }
                    }
                }
            }
            if (flag)
            {
                return null;
            }
            return level;
        }

        public static Level FindExactMM(string levelName, string owner)
        {
            lock (Server.levels)
            {
                return Server.levels.Where(l => l.Owner.ToLower() == owner.ToLower()).FirstOrDefault();
            }
        }

        public static Level FindExact(string levelName)
        {
            Level level = null;
            lock (Server.levels)
            {
                level = Server.levels.Where(lvl => lvl.Owner == "").FirstOrDefault(lvl => levelName.ToLower() == lvl.name.ToLower());
                if (level == null)
                {
                    level = Server.levels.Where(lvl => lvl.Owner != "").FirstOrDefault(lvl => levelName.ToLower() == lvl.name.ToLower());
                }
            }
            return level;
        }

        public bool BlockchangeChecks(Player p, ushort x, ushort y, ushort z, byte type, byte b)
        {
            string text = "start";
            try
            {
                if (x < 0 || y < 0 || z < 0)
                {
                    return false;
                }
                if (x >= width || y >= height || z >= depth)
                {
                    return false;
                }
                text = "Block rank checking";
                if (!Block.AllowBreak(b) && !Block.canPlace(p, b) && !Block.BuildIn(b))
                {
                    p.SendBlockchange(x, y, z, b);
                    return false;
                }
                if (p.level.mapType == MapType.MyMap && !p.level.IsPublic && p.group.Permission < LevelPermission.Operator &&
                    p.level.Owner.ToLower() != p.name.ToLower() && !p.level.AllowedPlayers.Contains(p.name.ToLower()))
                {
                    Player.SendMessage(p, "You are not allowed to build on this map. Ask the owner for permission.");
                    p.SendBlockchange(x, y, z, b);
                    return false;
                }
                text = "Block water in survival checking";
                if (p.level.mapType == MapType.Lava)
                {
                    if (blocks[x + width * z + width * depth * y] == 97)
                    {
                        LavaSystem.FoundTreasure(p, x, y, z);
                        Blockchange(x, y, z, 0);
                        return false;
                    }
                    if (type == 14 && !LavaSettings.All.AllowGoldRockOnLavaMaps && p.group.Permission < LevelPermission.Admin)
                    {
                        Player.SendMessage(p, rm.GetString("NotAllowedToPlaceGoldRock"));
                        p.SendBlockchange(x, y, z, 0);
                        return false;
                    }
                    if (p.group.Permission < LevelPermission.Admin)
                    {
                        if (type == 19)
                        {
                            if (p.spongeBlocks <= 0)
                            {
                                Player.SendMessage(p, string.Format(rm.GetString("OutOfSponges")));
                                p.SendBlockchange(x, y, z, b);
                                return false;
                            }
                            if (p.spongeBlocks > 0)
                            {
                                p.spongeBlocks--;
                            }
                        }
                        if (type == 9)
                        {
                            if (p.waterBlocks <= 0)
                            {
                                Player.SendMessage(p, rm.GetString("OutOfWater"));
                                p.SendBlockchange(x, y, z, b);
                                return false;
                            }
                            if (p.waterBlocks > 0)
                            {
                                p.waterBlocks--;
                            }
                        }
                        if (type == 220)
                        {
                            if (p.doorBlocks <= 0)
                            {
                                Player.SendMessage(p, rm.GetString("OutOfDoors"));
                                p.SendBlockchange(x, y, z, b);
                                return false;
                            }
                            if (p.doorBlocks > 0)
                            {
                                p.doorBlocks--;
                            }
                        }
                        if (LavaSettings.All.DisallowSpongesNearLavaSpawn && (type == 19 || type == 78))
                        {
                            bool @return = false;
                            LavaSystem.currentLavaMap.LavaSources.ForEach(delegate(LavaSystem.LavaSource ls)
                            {
                                if (!@return && x > ls.X - 4 && y > ls.Y - 4 && z > ls.Z - 4 && x < ls.X + 4 && y < ls.Y + 4 && z < ls.Z + 4)
                                {
                                    p.SendBlockchange(x, y, z, b);
                                    p.SendMessage("You are not allowed to place sponges here.");
                                    @return = true;
                                }
                            });
                            if (@return)
                            {
                                return false;
                            }
                        }
                        else if (LavaSettings.All.DisallowBuildingNearLavaSpawn)
                        {
                            bool return2 = false;
                            LavaSystem.currentLavaMap.LavaSources.ForEach(delegate(LavaSystem.LavaSource ls)
                            {
                                if (!return2 && x > ls.X - 3 && y > ls.Y - 3 && z > ls.Z - 3 && x < ls.X + 3 && y < ls.Y + 3 && z < ls.Z + 3)
                                {
                                    p.SendBlockchange(x, y, z, b);
                                    p.SendMessage("You are not allowed to build that close to the lava spawn.");
                                    return2 = true;
                                }
                            });
                            if (return2)
                            {
                                return false;
                            }
                        }
                    }
                    text = "If dead check";
                    if (!p.inHeaven && p.lives < 1)
                    {
                        p.SendBlockchange(x, y, z, b);
                        Player.SendMessage2(p, MessagesManager.GetString("WarningBlockChangeGhost"));
                        return false;
                    }
                    text = "Block permission checking";
                    if (p.group.Permission < LevelPermission.Operator && mapType == MapType.Lava && this != Server.heavenMap)
                    {
                        int num = x + width * z + width * depth * y;
                        if (LavaSettings.All.Antigrief == AntigriefType.BasedOnPlayersLevel)
                        {
                            if (p.tier <= LavaSettings.All.UpperLevelOfBoplAntigrief && ownedBlocks.ContainsKey(num) && p.tier < ownedBlocks[num].tier)
                            {
                                byte b2 = blocks[num];
                                if (b2 != 0 && b2 != 194 && b2 != 195 && b2 != 112 && b2 != 80 && b2 != 81 && b2 != 82 && b2 != 83 && b2 != 98 && b2 != 74 && b2 != 112 &&
                                    b2 != 193 && b2 != 97)
                                {
                                    p.SendBlockchange(x, y, z, b);
                                    Player.SendMessage2(p, rm.GetString("WarningBlockProtectedByLevel"));
                                    return false;
                                }
                            }
                        }
                        else if (ownedBlocks.ContainsKey(num) && p.name != ownedBlocks[num].playersName)
                        {
                            byte b3 = blocks[num];
                            if (b3 != 0 && b3 != 194 && b3 != 195 && b3 != 112 && b3 != 80 && b3 != 81 && b3 != 82 && b3 != 83 && b3 != 98 && b3 != 74 && b3 != 112 &&
                                b3 != 193 && b3 != 97)
                            {
                                p.SendBlockchange(x, y, z, b);
                                Player.SendMessage2(p, string.Format(rm.GetString("WarningBlockOwned"), ownedBlocks[num].ColoredName + Server.DefaultColor));
                                return false;
                            }
                        }
                    }
                }
                text = "Zone checking";
                bool flag = true;
                bool flag2 = false;
                bool flag3 = false;
                string text2 = "";
                var list = new List<Zone>();
                if ((p.group.Permission < LevelPermission.Admin || p.ZoneCheck || p.zoneDel) && !Block.AllowBreak(b))
                {
                    if (ZoneList.Count == 0)
                    {
                        flag = true;
                    }
                    else
                    {
                        foreach (Zone zone in ZoneList)
                        {
                            if (zone.smallX > x || x > zone.bigX || zone.smallY > y || y > zone.bigY || zone.smallZ > z || z > zone.bigZ)
                            {
                                continue;
                            }
                            flag3 = true;
                            if (p.zoneDel)
                            {
                                string text3 = null;
                                text3 = p.level.mapType != MapType.MyMap
                                    ? "DELETE FROM Zone" + p.level.name + " WHERE Owner='" + zone.Owner + "' AND SmallX='" + zone.smallX + "' AND SmallY='" + zone.smallY +
                                    "' AND SmallZ='" + zone.smallZ + "' AND BigX='" + zone.bigX + "' AND BigY='" + zone.bigY + "' AND BigZ='" + zone.bigZ + "'"
                                    : "DELETE FROM Zones WHERE Map=" + p.level.MapDbId + " AND Owner='" + zone.Owner + "' AND SmallX='" + zone.smallX + "' AND SmallY='" +
                                    zone.smallY + "' AND SmallZ='" + zone.smallZ + "' AND BigX='" + zone.bigX + "' AND BigY='" + zone.bigY + "' AND BigZ='" + zone.bigZ +
                                    "'";
                                DBInterface.ExecuteQuery(text3);
                                list.Add(zone);
                                p.SendBlockchange(x, y, z, b);
                                Player.SendMessage(p, string.Format(rm.GetString("ZoneDeleted"), zone.Owner));
                                flag2 = true;
                            }
                            else if (zone.Owner.Substring(0, 3) == "grp")
                            {
                                if (Group.Find(zone.Owner.Substring(3)).Permission <= p.group.Permission && !p.ZoneCheck)
                                {
                                    flag = true;
                                    break;
                                }
                                flag = false;
                                text2 = text2 + ", " + zone.Owner.Substring(3);
                            }
                            else
                            {
                                if (zone.Owner.ToLower() == p.name.ToLower() && !p.ZoneCheck)
                                {
                                    flag = true;
                                    break;
                                }
                                flag = false;
                                text2 = text2 + ", " + zone.Owner;
                            }
                        }
                    }
                    if (p.zoneDel)
                    {
                        if (!flag2)
                        {
                            Player.SendMessage(p, rm.GetString("ZoneNotFoundNotDeleted"));
                        }
                        else
                        {
                            foreach (Zone item in list)
                            {
                                ZoneList.Remove(item);
                            }
                        }
                        p.zoneDel = false;
                        return false;
                    }
                    if (!flag || p.ZoneCheck)
                    {
                        if (text2 != "")
                        {
                            Player.SendMessage(p, string.Format(rm.GetString("ZoneBelongsTo"), text2.Remove(0, 2)));
                        }
                        else
                        {
                            Player.SendMessage(p, rm.GetString("ZoneBelongsToNoOne"));
                        }
                        p.ZoneSpam = DateTime.Now;
                        p.SendBlockchange(x, y, z, b);
                        if (p.ZoneCheck && !p.staticCommands)
                        {
                            p.ZoneCheck = false;
                        }
                        return false;
                    }
                }
                text = "Map rank checking";
                if (text2 == "" && p.group.Permission < permissionbuild && (!flag3 || !flag))
                {
                    p.SendBlockchange(x, y, z, b);
                    Player.SendMessage(p, string.Format(rm.GetString("WarningBuildPermission"), PermissionToName(permissionbuild)));
                    return false;
                }
                if (b == 19 && physics > 0 && type != 19)
                {
                    PhysUniversalSpongeRemoved(PosToInt(x, y, z));
                }
                if (b == 78 && physics > 0 && type != 78)
                {
                    PhysUniversalSpongeRemoved(PosToInt(x, y, z));
                }
                return true;
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
                Server.s.Log("Error location: " + text);
                return false;
            }
        }

        public void BlockchangeAftercheck(Player p, ushort x, ushort y, ushort z, byte type, byte b)
        {
            string text = "Undo buffer filling";
            try
            {
                Player.UndoPos item = default(Player.UndoPos);
                item.x = x;
                item.y = y;
                item.z = z;
                item.mapName = name;
                item.type = b;
                item.newtype = type;
                item.timePlaced = DateTime.Now;
                p.UndoBuffer.Add(item);
                text = "Setting tile";
                p.loginBlocks++;
                p.overallBlocks++;
                SetTile(x, y, z, type);
                if (mapType == MapType.Lava && this != Server.heavenMap)
                {
                    int key = x + width * z + width * depth * y;
                    if (!ownedBlocks.ContainsKey(key))
                    {
                        ownedBlocks.Add(key, new OwnedBlockInfo(p.PublicName, p.color, "", (int)p.group.Permission, p.tier));
                    }
                    else
                    {
                        ownedBlocks.Remove(key);
                        ownedBlocks.Add(key, new OwnedBlockInfo(p.PublicName, p.color, "", (int)p.group.Permission, p.tier));
                    }
                }
                text = "Changing grass into dirt";
                if (GetTile(x, (ushort)(y - 1), z) == 2 && GrassDestroy && !Block.LightPass(type))
                {
                    Blockchange(p, x, (ushort)(y - 1), z, 3);
                }
                text = "Adding physics";
                if (physics > 0 && Block.Physics(type))
                {
                    AddCheck(PosToInt(x, y, z));
                }
                changed = true;
                backDup = false;
            }
            catch (Exception ex)
            {
                Server.ErrorLog("Error location: " + text);
                Server.ErrorLog(ex);
            }
        }

        public void RemoveOwnedBlocks(string playersName)
        {
            lock (ownedBlocks.SyncRoot)
            {
                foreach (KeyValuePair<int, OwnedBlockInfo> item in ownedBlocks.Where(x => x.Value.playersName == playersName).ToList())
                {
                    ownedBlocks.Remove(item.Key);
                }
            }
        }

        public void Blockchange(Player p, ushort x, ushort y, ushort z, byte type)
        {
            Blockchange(p, x, y, z, type, addaction: true);
        }

        public void Blockchange(Player p, ushort x, ushort y, ushort z, byte type, bool addaction)
        {
            string text = "start";
            Player.UndoPos item = default(Player.UndoPos);
            while (true)
            {
                try
                {
                    if (x < 0 || y < 0 || z < 0 || x >= width || y >= height || z >= depth)
                    {
                        break;
                    }
                    byte b = GetTile(x, y, z);
                    text = "Block rank checking";
                    if (!Block.AllowBreak(b) && !Block.canPlace(p, b) && !Block.BuildIn(b))
                    {
                        p.SendBlockchange(x, y, z, b);
                        break;
                    }
                    if (p.level.mapType == MapType.MyMap && !p.level.IsPublic && p.group.Permission < LevelPermission.Operator &&
                        p.level.Owner.ToLower() != p.name.ToLower() && !p.level.AllowedPlayers.Contains(p.name.ToLower()))
                    {
                        Player.SendMessage(p, "You are not allowed to build on this map. Ask the owner for permission.");
                        p.SendBlockchange(x, y, z, b);
                        break;
                    }
                    text = "Block water in survival checking";
                    if (p.level.mapType == MapType.Lava)
                    {
                        if (type == 14 && !LavaSettings.All.AllowGoldRockOnLavaMaps && p.group.Permission < LevelPermission.Admin)
                        {
                            Player.SendMessage(p, rm.GetString("NotAllowedToPlaceGoldRock"));
                            p.SendBlockchange(x, y, z, 0);
                            break;
                        }
                        if (p.group.Permission < LevelPermission.Admin)
                        {
                            switch (type)
                            {
                                case 19:
                                    if (p.spongeBlocks <= 0)
                                    {
                                        Player.SendMessage(p, rm.GetString("OutOfSponges"));
                                        p.SendBlockchange(x, y, z, b);
                                        return;
                                    }
                                    if (p.spongeBlocks > 0)
                                    {
                                        p.spongeBlocks--;
                                    }
                                    break;
                                case 9:
                                    if (p.waterBlocks <= 0)
                                    {
                                        Player.SendMessage(p, rm.GetString("OutOfWater"));
                                        p.SendBlockchange(x, y, z, b);
                                        return;
                                    }
                                    if (p.waterBlocks > 0)
                                    {
                                        p.waterBlocks--;
                                    }
                                    break;
                                default:
                                    if (Block.IsDoor(type))
                                    {
                                        if (p.doorBlocks <= 0)
                                        {
                                            Player.SendMessage(p, rm.GetString("OutOfDoors"));
                                            p.SendBlockchange(x, y, z, b);
                                            return;
                                        }
                                        if (p.doorBlocks > 0)
                                        {
                                            p.doorBlocks--;
                                        }
                                    }
                                    break;
                            }
                            if (LavaSettings.All.DisallowSpongesNearLavaSpawn && (type == 19 || type == 78))
                            {
                                bool @return = false;
                                LavaSystem.currentLavaMap.LavaSources.ForEach(delegate(LavaSystem.LavaSource ls)
                                {
                                    if (!@return && x > ls.X - 4 && y > ls.Y - 4 && z > ls.Z - 4 && x < ls.X + 4 && y < ls.Y + 4 && z < ls.Z + 4)
                                    {
                                        p.SendBlockchange(x, y, z, b);
                                        p.SendMessage("You are not allowed to place sponges here.");
                                        @return = true;
                                    }
                                });
                                if (@return)
                                {
                                    break;
                                }
                            }
                            else if (LavaSettings.All.DisallowBuildingNearLavaSpawn)
                            {
                                bool return2 = false;
                                LavaSystem.currentLavaMap.LavaSources.ForEach(delegate(LavaSystem.LavaSource ls)
                                {
                                    if (!return2 && x > ls.X - 3 && y > ls.Y - 3 && z > ls.Z - 3 && x < ls.X + 3 && y < ls.Y + 3 && z < ls.Z + 3)
                                    {
                                        p.SendBlockchange(x, y, z, b);
                                        p.SendMessage("You are not allowed to build that close to the lava spawn.");
                                        return2 = true;
                                    }
                                });
                                if (return2)
                                {
                                    break;
                                }
                            }
                        }
                        text = "If dead check";
                        if (!p.inHeaven && p.lives < 1)
                        {
                            p.SendBlockchange(x, y, z, b);
                            Player.SendMessage2(p, MessagesManager.GetString("WarningBlockChangeGhost"));
                            break;
                        }
                        text = "Block permission checking";
                        if (p.group.Permission < LevelPermission.Operator && mapType == MapType.Lava && this != Server.heavenMap)
                        {
                            int num = x + width * z + width * depth * y;
                            if (LavaSettings.All.Antigrief == AntigriefType.BasedOnPlayersLevel)
                            {
                                if (p.tier <= LavaSettings.All.UpperLevelOfBoplAntigrief && ownedBlocks.ContainsKey(num) && p.tier < ownedBlocks[num].tier)
                                {
                                    byte b2 = blocks[num];
                                    if (b2 != 0 && b2 != 194 && b2 != 195 && b2 != 112 && b2 != 80 && b2 != 81 && b2 != 82 && b2 != 83 && b2 != 98 && b2 != 112 &&
                                        b2 != 74 && b2 != 193 && b2 != 97)
                                    {
                                        p.SendBlockchange(x, y, z, b);
                                        Player.SendMessage2(p, rm.GetString("WarningBlockProtectedByLevel"));
                                        break;
                                    }
                                }
                            }
                            else if (ownedBlocks.ContainsKey(num) && p.PublicName != ownedBlocks[num].playersName)
                            {
                                byte b3 = blocks[num];
                                if (b3 != 0 && b3 != 194 && b3 != 195 && b3 != 112 && b3 != 80 && b3 != 81 && b3 != 82 && b3 != 83 && b3 != 98 && b3 != 112 && b3 != 74 &&
                                    b3 != 193 && b3 != 97)
                                {
                                    p.SendBlockchange(x, y, z, b);
                                    Player.SendMessage2(p, string.Format(rm.GetString("WarningBlockOwned"), ownedBlocks[num].ColoredName + Server.DefaultColor));
                                    break;
                                }
                            }
                        }
                    }
                    text = "Zone checking";
                    bool flag = true;
                    bool flag2 = false;
                    bool flag3 = false;
                    string text2 = "";
                    var list = new List<Zone>();
                    if ((p.group.Permission < LevelPermission.Admin || p.ZoneCheck || p.zoneDel) && !Block.AllowBreak(b))
                    {
                        if (ZoneList.Count == 0)
                        {
                            flag = true;
                        }
                        else
                        {
                            var list2 = new List<Zone>(ZoneList);
                            foreach (Zone item2 in list2)
                            {
                                if (item2.smallX > x || x > item2.bigX || item2.smallY > y || y > item2.bigY || item2.smallZ > z || z > item2.bigZ)
                                {
                                    continue;
                                }
                                flag3 = true;
                                if (p.zoneDel)
                                {
                                    try
                                    {
                                        DBInterface.ExecuteQuery("DELETE FROM `Zone" + p.level.name + "` WHERE Owner='" + item2.Owner + "' AND SmallX='" + item2.smallX +
                                                                 "' AND SMALLY='" + item2.smallY + "' AND SMALLZ='" + item2.smallZ + "' AND BIGX='" + item2.bigX +
                                                                 "' AND BIGY='" + item2.bigY + "' AND BIGZ='" + item2.bigZ + "'");
                                    }
                                    catch (Exception ex)
                                    {
                                        Server.ErrorLog(ex);
                                    }
                                    list.Add(item2);
                                    p.SendBlockchange(x, y, z, b);
                                    Player.SendMessage(p, string.Format(rm.GetString("ZoneDeleted"), item2.Owner));
                                    flag2 = true;
                                }
                                else if (item2.Owner.Length > 3 && item2.Owner.Substring(0, 3) == "grp")
                                {
                                    if (Group.Find(item2.Owner.Substring(3)).Permission <= p.group.Permission && !p.ZoneCheck)
                                    {
                                        flag = true;
                                        break;
                                    }
                                    flag = false;
                                    text2 = text2 + ", " + item2.Owner.Substring(3);
                                }
                                else
                                {
                                    if (item2.Owner.ToLower() == p.name.ToLower() && !p.ZoneCheck)
                                    {
                                        flag = true;
                                        break;
                                    }
                                    flag = false;
                                    text2 = text2 + ", " + item2.Owner;
                                }
                            }
                        }
                        if (p.zoneDel)
                        {
                            if (!flag2)
                            {
                                Player.SendMessage(p, rm.GetString("ZoneNotFoundNotDeleted"));
                            }
                            else
                            {
                                foreach (Zone item3 in list)
                                {
                                    ZoneList.Remove(item3);
                                }
                            }
                            p.zoneDel = false;
                            break;
                        }
                        if (!flag || p.ZoneCheck)
                        {
                            if (text2 != "")
                            {
                                Player.SendMessage(p, string.Format(rm.GetString("ZoneBelongsTo"), text2.Remove(0, 2)));
                            }
                            else
                            {
                                Player.SendMessage(p, rm.GetString("ZoneBelongsToNoOne"));
                            }
                            p.ZoneSpam = DateTime.Now;
                            p.SendBlockchange(x, y, z, b);
                            if (p.ZoneCheck && !p.staticCommands)
                            {
                                p.ZoneCheck = false;
                            }
                            break;
                        }
                    }
                    text = "Map rank checking";
                    if (text2 == "" && p.group.Permission < permissionbuild && (!flag3 || !flag))
                    {
                        p.SendBlockchange(x, y, z, b);
                        Player.SendMessage(p, string.Format(rm.GetString("WarningBuildPermission"), PermissionToName(permissionbuild)));
                        break;
                    }
                    text = "Block sending";
                    if (Block.Convert(b) != Block.Convert(type) && !Instant)
                    {
                        byte[] buffer = new byte[7];
                        HTNO(x).CopyTo(buffer, 0);
                        HTNO(y).CopyTo(buffer, 2);
                        HTNO(z).CopyTo(buffer, 4);
                        byte blockmensionBlock = Block.Convert(type);
                        byte cpeBlock = Block.ConvertFromBlockmension(blockmensionBlock);
                        byte classicBlock = Block.ConvertExtended(cpeBlock);
                        Player.players.ForEachSync(delegate(Player pl)
                        {
                            try
                            {
                                if (pl.level == this)
                                {
                                    if (!pl.IsCpeSupported)
                                    {
                                        buffer[6] = classicBlock;
                                    }
                                    else if (pl.Cpe.Blockmension == 1)
                                    {
                                        buffer[6] = blockmensionBlock;
                                    }
                                    else
                                    {
                                        buffer[6] = cpeBlock;
                                    }
                                    pl.SendRaw(6, buffer);
                                }
                            }
                            catch {}
                        });
                    }
                    if (b == 19 && physics > 0 && type != 19)
                    {
                        PhysUniversalSpongeRemoved(PosToInt(x, y, z));
                    }
                    if (b == 78 && physics > 0 && type != 78)
                    {
                        PhysUniversalSpongeRemoved(PosToInt(x, y, z));
                    }
                    text = "Undo buffer filling";
                    item.x = x;
                    item.y = y;
                    item.z = z;
                    item.mapName = name;
                    item.type = b;
                    item.newtype = type;
                    item.timePlaced = DateTime.Now;
                    p.UndoBuffer.Add(item);
                    text = "Setting tile";
                    p.loginBlocks++;
                    p.overallBlocks++;
                    SetTile(x, y, z, type);
                    if (mapType == MapType.Lava && this != Server.heavenMap)
                    {
                        int key = x + width * z + width * depth * y;
                        if (!ownedBlocks.ContainsKey(key))
                        {
                            ownedBlocks.Add(key, new OwnedBlockInfo(p.PublicName, p.color, "", (int)p.group.Permission, p.tier));
                        }
                        else
                        {
                            ownedBlocks.Remove(key);
                            ownedBlocks.Add(key, new OwnedBlockInfo(p.PublicName, p.color, "", (int)p.group.Permission, p.tier));
                        }
                    }
                    text = "Growing grass";
                    if (GetTile(x, (ushort)(y - 1), z) == 2 && GrassDestroy && !Block.LightPass(type))
                    {
                        Blockchange(p, x, (ushort)(y - 1), z, 3);
                    }
                    text = "Adding physics";
                    if (physics > 0 && Block.Physics(type))
                    {
                        AddCheck(PosToInt(x, y, z));
                    }
                    changed = true;
                    backDup = false;
                    break;
                }
                catch (OutOfMemoryException)
                {
                    Player.SendMessage(p, "Undo buffer too big! Cleared!");
                    p.UndoBuffer.Clear();
                }
                catch (Exception ex3)
                {
                    Server.ErrorLog("Error location: " + text);
                    Server.ErrorLog(ex3);
                    break;
                }
            }
        }

        public void Blockchange(HashSet<BlockInfo> blocks)
        {
            int num = 1024;
            int count = blocks.Count;
            int num2 = count / num;
            if (count % num != 0)
            {
                num2++;
            }
            for (int i = 0; i < num2; i++)
            {
                int num3 = count - i * num;
                int num4 = num3 >= num ? num : num3;
                byte[] array = new byte[num4 * 8];
                byte[] array2 = new byte[num4 * 8];
                byte[] array3 = new byte[num4 * 8];
                int num5 = 0;
                for (int j = 0; j < num4; j++)
                {
                    BlockInfo blockInfo = blocks.ElementAt(j + i * num);
                    array[num5] = 6;
                    Player.HTNO(blockInfo.X).CopyTo(array, num5 + 1);
                    Player.HTNO(blockInfo.Y).CopyTo(array, num5 + 3);
                    Player.HTNO(blockInfo.Z).CopyTo(array, num5 + 5);
                    byte block = array[num5 + 7] = Block.Convert(blockInfo.Type);
                    array3[num5] = 6;
                    Player.HTNO(blockInfo.X).CopyTo(array3, num5 + 1);
                    Player.HTNO(blockInfo.Y).CopyTo(array3, num5 + 3);
                    Player.HTNO(blockInfo.Z).CopyTo(array3, num5 + 5);
                    byte block2 = array3[num5 + 7] = Block.ConvertFromBlockmension(block);
                    array2[num5] = 6;
                    Player.HTNO(blockInfo.X).CopyTo(array2, num5 + 1);
                    Player.HTNO(blockInfo.Y).CopyTo(array2, num5 + 3);
                    Player.HTNO(blockInfo.Z).CopyTo(array2, num5 + 5);
                    array2[num5 + 7] = Block.ConvertExtended(block2);
                    num5 += 8;
                }
                Send(array, array3, array2);
            }
        }

        void Send(byte[] blockmensionData, byte[] cpeData, byte[] noncpeData)
        {
            Player.players.ForEachSync(delegate(Player p)
            {
                if (p.level == this)
                {
                    int num = 0;
                    while (true)
                    {
                        try
                        {
                            if (p.socket != null && !p.sendLock)
                            {
                                if (p.IsCpeSupported)
                                {
                                    if (p.Cpe.Blockmension == 1)
                                    {
                                        p.socket.BeginSend(blockmensionData, 0, blockmensionData.Length, SocketFlags.None, null, null);
                                    }
                                    else
                                    {
                                        p.socket.BeginSend(cpeData, 0, cpeData.Length, SocketFlags.None, null, null);
                                    }
                                }
                                else
                                {
                                    p.socket.BeginSend(noncpeData, 0, noncpeData.Length, SocketFlags.None, null, null);
                                }
                            }
                            break;
                        }
                        catch (SocketException ex)
                        {
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
                                p.Disconnect();
                                break;
                            }
                            if (++num > 3)
                            {
                                p.Disconnect();
                                break;
                            }
                        }
                    }
                }
            });
        }

        public void Blockchange(byte[] blocks)
        {
            Player.players.ForEach(delegate(Player p)
            {
                try
                {
                    if (p.level == this)
                    {
                        int num = 0;
                        while (true)
                        {
                            try
                            {
                                if (p.socket != null && !p.sendLock)
                                {
                                    p.socket.BeginSend(blocks, 0, blocks.Length, SocketFlags.None, null, null);
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
                                    p.Disconnect();
                                    break;
                                }
                                if (num > 3)
                                {
                                    p.Disconnect();
                                    break;
                                }
                            }
                        }
                    }
                }
                catch {}
            });
        }

        public void Blockchange(ushort x, ushort y, ushort z, byte type, bool overRide = false, string extraInfo = "")
        {
            if (x < 0 || y < 0 || z < 0 || x >= width || y >= height || z >= depth)
            {
                return;
            }
            byte tile = GetTile(x, y, z);
            try
            {
                if (!overRide && (Block.OPBlocks(tile) || Block.OPBlocks(type)))
                {
                    return;
                }
                if (Block.Convert(tile) != Block.Convert(type))
                {
                    byte[] buffer = new byte[7];
                    HTNO(x).CopyTo(buffer, 0);
                    HTNO(y).CopyTo(buffer, 2);
                    HTNO(z).CopyTo(buffer, 4);
                    byte blockmensionBlock = Block.Convert(type);
                    byte cpeBlock = Block.ConvertFromBlockmension(blockmensionBlock);
                    byte classicBlock = Block.ConvertExtended(blockmensionBlock);
                    Player.players.ForEachSync(delegate(Player pl)
                    {
                        try
                        {
                            if (pl.level == this)
                            {
                                if (!pl.IsCpeSupported)
                                {
                                    buffer[6] = classicBlock;
                                }
                                else if (pl.Cpe.Blockmension == 1)
                                {
                                    buffer[6] = blockmensionBlock;
                                }
                                else
                                {
                                    buffer[6] = cpeBlock;
                                }
                                pl.SendRaw(6, buffer);
                            }
                        }
                        catch {}
                    });
                }
                if (tile == 78 && physics > 0 && type != 78)
                {
                    PhysUniversalSpongeRemoved(PosToInt(x, y, z));
                }
                if (tile == 19 && physics > 0 && type != 19)
                {
                    PhysUniversalSpongeRemoved(PosToInt(x, y, z));
                }
                try
                {
                    UndoPos undoPos = default(UndoPos);
                    undoPos.location = PosToInt(x, y, z);
                    undoPos.newType = type;
                    undoPos.oldType = tile;
                    undoPos.timePerformed = DateTime.Now;
                    if (currentUndo > Server.physUndo)
                    {
                        currentUndo = 0;
                        UndoBuffer[currentUndo] = undoPos;
                    }
                    else if (UndoBuffer.Count < Server.physUndo)
                    {
                        currentUndo++;
                        UndoBuffer.Add(undoPos);
                    }
                    else
                    {
                        currentUndo++;
                        UndoBuffer[currentUndo] = undoPos;
                    }
                }
                catch (Exception) {}
                SetTile(x, y, z, type);
                if (physics > 0 && (Block.Physics(type) || extraInfo != ""))
                {
                    AddCheck(PosToInt(x, y, z), extraInfo);
                }
            }
            catch (Exception)
            {
                SetTile(x, y, z, type);
            }
        }

        public void skipChange(ushort x, ushort y, ushort z, byte type)
        {
            if (x >= 0 && y >= 0 && z >= 0 && x < width && y < height && z < depth)
            {
                SetTile(x, y, z, type);
            }
        }

        public void Save(bool Override = false)
        {
            Save(Override, lavalvl: false);
        }

        public void Save(bool Override, bool lavalvl)
        {
            LevelInfoManager levelInfoManager = new LevelInfoManager();
            LevelInfoConverter levelInfoConverter = new LevelInfoConverter();
            LevelInfoRaw info = levelInfoConverter.ToRaw(Info);
            levelInfoManager.Save(this, info);
            if (lavalvl)
            {
                mapType = MapType.Lava;
            }
            string text = directoryPath + "/" + name + ".lvl";
            if (mapType == MapType.Lava)
            {
                directoryPath = "lava/maps/" + name + ".lvl";
            }
            try
            {
                if (!Directory.Exists("levels/level properties"))
                {
                    Directory.CreateDirectory("levels/level properties");
                }
                if (mapType == MapType.Lava && !Directory.Exists("lava/maps"))
                {
                    Directory.CreateDirectory("lava/maps");
                }
                if (changed || !File.Exists(text) || Override)
                {
                    using (FileStream stream = File.Create(text + ".back"))
                    {
                        using (GZipStream gZipStream = new GZipStream(stream, CompressionMode.Compress))
                        {
                            byte[] array = new byte[16];
                            BitConverter.GetBytes(1874).CopyTo(array, 0);
                            gZipStream.Write(array, 0, 2);
                            BitConverter.GetBytes(width).CopyTo(array, 0);
                            BitConverter.GetBytes(depth).CopyTo(array, 2);
                            BitConverter.GetBytes(height).CopyTo(array, 4);
                            BitConverter.GetBytes(spawnx).CopyTo(array, 6);
                            BitConverter.GetBytes(spawnz).CopyTo(array, 8);
                            BitConverter.GetBytes(spawny).CopyTo(array, 10);
                            array[12] = rotx;
                            array[13] = roty;
                            array[14] = (byte)permissionvisit;
                            array[15] = (byte)permissionbuild;
                            gZipStream.Write(array, 0, array.Length);
                            byte[] array2 = new byte[blocks.Length];
                            for (int i = 0; i < blocks.Length; i++)
                            {
                                if (blocks[i] < 80)
                                {
                                    array2[i] = blocks[i];
                                }
                                else
                                {
                                    array2[i] = Block.SaveConvert(blocks[i]);
                                }
                            }
                            gZipStream.Write(array2, 0, array2.Length);
                        }
                    }
                    File.Delete(text + ".backup");
                    File.Copy(text + ".back", text + ".backup");
                    File.Delete(text);
                    File.Move(text + ".back", text);
                    StreamWriter streamWriter = mapType == MapType.Lava ? new StreamWriter(File.Create("lava/maps/" + name + ".cfg")) :
                        mapType == MapType.Freebuild ? new StreamWriter(File.Create("levels/level properties/" + name + ".properties")) :
                        mapType == MapType.Home ? new StreamWriter(File.Create("maps/home/" + name + ".properties")) :
                        mapType != MapType.Zombie ? new StreamWriter(File.Create(directoryPath + "/" + name + ".properties")) :
                        new StreamWriter(File.Create("infection/maps/" + name + ".properties"));
                    streamWriter.WriteLine("#Level properties for " + name);
                    streamWriter.WriteLine("PlayerLimit = " + playerLimit);
                    streamWriter.WriteLine("Theme = " + theme);
                    streamWriter.WriteLine("Physics = " + physics);
                    streamWriter.WriteLine("Physics speed = " + speedPhysics);
                    streamWriter.WriteLine("Physics overload = " + overload);
                    streamWriter.WriteLine("Finite mode = " + finite);
                    streamWriter.WriteLine("Animal AI = " + ai);
                    streamWriter.WriteLine("Edge water = " + edgeWater);
                    streamWriter.WriteLine("Survival death = " + Death);
                    streamWriter.WriteLine("Fall = " + fall);
                    streamWriter.WriteLine("Drown = " + drown);
                    streamWriter.WriteLine("MOTD = " + motd);
                    streamWriter.WriteLine("JailX = " + jailx);
                    streamWriter.WriteLine("JailY = " + jaily);
                    streamWriter.WriteLine("JailZ = " + jailz);
                    streamWriter.WriteLine("Unload = " + unload);
                    streamWriter.WriteLine("PerBuild = " + PermissionToName(permissionbuild));
                    streamWriter.WriteLine("PerVisit = " + PermissionToName(permissionvisit));
                    streamWriter.WriteLine("Allowed = " + string.Join(",", AllowedPlayers.ToArray()));
                    streamWriter.WriteLine("Public = " + IsPublic);
                    streamWriter.Flush();
                    streamWriter.Close();
                    streamWriter.Dispose();
                    if (mapType == MapType.Lava)
                    {
                        Server.s.Log("Map \"" + name + "\" was saved as lava map.");
                    }
                    else if (mapType == MapType.Freebuild || mapType == MapType.Zombie)
                    {
                        Server.s.Log("SAVED: Level \"" + name + "\". (" + PlayersCount + "/" + Player.players.Count + "/" + Server.players + ")");
                    }
                    else if (mapType == MapType.Home)
                    {
                        Server.s.Log("Map \"" + name + "\" was saved as a home map.");
                    }
                    else if (mapType == MapType.MyMap)
                    {
                        Server.s.Log("Map \"" + name + "\" was saved.");
                    }
                    changed = false;
                }
                else
                {
                    Server.s.Log("Skipping level save for " + name + ".");
                }
            }
            catch (Exception ex)
            {
                Server.s.Log("FAILED TO SAVE :" + name);
                Player.GlobalMessage(string.Format(rm.GetString("ErrorMapSaveGlobalMessage"), name));
                Server.ErrorLog(ex);
                return;
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public int Backup(bool Forced = false, string backupName = "")
        {
            if (!IsMapBeingBackuped)
            {
                Server.s.Log("You cannot create a backup of this type of map.");
                return -1;
            }
            if (!backDup || Forced)
            {
                int num = 1;
                string text = Server.backupLocation;
                if (mapType == MapType.Home)
                {
                    text = "maps/home/backup";
                }
                else if (mapType == MapType.MyMap)
                {
                    text = directoryPath + "/backup";
                }
                if (Directory.Exists(text + "/" + name))
                {
                    num = Directory.GetDirectories(text + "/" + name).Length + 1;
                }
                else
                {
                    Directory.CreateDirectory(text + "/" + name);
                }
                string text2 = text + "/" + name + "/" + num;
                if (backupName != "")
                {
                    text2 = text + "/" + name + "/" + backupName;
                }
                Directory.CreateDirectory(text2);
                string destFileName = text2 + "/" + name + ".lvl";
                string sourceFileName = "levels/" + name + ".lvl";
                if (mapType == MapType.Home || mapType == MapType.MyMap)
                {
                    sourceFileName = directoryPath + "/" + name + ".lvl";
                }
                try
                {
                    File.Copy(sourceFileName, destFileName, overwrite: true);
                    backDup = true;
                    return num;
                }
                catch (Exception ex)
                {
                    Server.ErrorLog(ex);
                    Server.s.Log("FAILED TO INCREMENTAL BACKUP :" + name);
                    return -1;
                }
            }
            Server.s.Log("Level unchanged, skipping backup");
            return -1;
        }

        public int LavaMapBackup(string backupName = "")
        {
            int num = 1;
            string text = "lava/maps/backup";
            if (Directory.Exists(text + "/" + name))
            {
                num = Directory.GetDirectories(text + "/" + name).Length + 1;
            }
            else
            {
                Directory.CreateDirectory(text + "/" + name);
            }
            string text2 = text + "/" + name + "/" + num;
            string text3 = "";
            if (backupName != "")
            {
                text2 = "lava/maps/";
                try
                {
                    File.Copy(text2 + name + ".lvl", text2 + backupName + ".lvl", overwrite: true);
                    File.Copy(text2 + name + ".cfg", text2 + backupName + ".cfg", overwrite: true);
                    backDup = true;
                    return 1;
                }
                catch (Exception ex)
                {
                    Server.ErrorLog(ex);
                    Server.s.Log("FAILED TO SAVE LAVA MAP \"" + name + "\" AS \"" + backupName + "\"");
                    return -1;
                }
            }
            Directory.CreateDirectory(text2);
            text3 = text2 + "/" + name + ".lvl";
            string sourceFileName = "lava/maps/" + name + ".lvl";
            try
            {
                File.Copy(sourceFileName, text3, overwrite: true);
                backDup = true;
                return num;
            }
            catch (Exception ex2)
            {
                Server.ErrorLog(ex2);
                Server.s.Log("FAILED TO INCREMENTAL LAVA MAP BACKUP :" + name);
                return -1;
            }
        }

        public void NotifyPopulationChanged()
        {
            if (mapType == MapType.Lava || mapType == MapType.Zombie || !unload)
            {
                return;
            }
            bool proceed = true;
            Player.players.ForEachSync(delegate(Player p)
            {
                if (p.level == this)
                {
                    proceed = false;
                }
            });
            if (proceed)
            {
                Unload();
            }
        }

        public static Level Load(string givenName)
        {
            return Load(givenName, 0);
        }

        public static Level Load(string givenName, byte phys)
        {
            return Load(givenName, phys, lavaSurv: false);
        }

        public static Level Load(string givenName, bool autoUnload)
        {
            return Load(givenName, 0, MapType.Freebuild, autoUnload);
        }

        public static Level Load(string givenName, byte phys, bool lavaSurv)
        {
            if (lavaSurv)
            {
                return Load(givenName, phys, MapType.Lava);
            }
            return Load(givenName, phys, MapType.Freebuild);
        }

        public static Level Load(string directoryPath, string mapName, string owner, MapType type, bool isAutoUnloading)
        {
            string text = mapName + ".lvl";
            string text2 = directoryPath + "\\" + text;
            if (!File.Exists(text2))
            {
                return null;
            }
            var dictionary = new Dictionary<string, object>();
            dictionary.Add("@MapName", mapName);
            dictionary.Add("@Owner", owner);
            Dictionary<string, object> parameters = dictionary;
            uint num = 0u;
            using (DataTable dataTable = DBInterface.fillData("SELECT * FROM MapList WHERE MapName = @MapName AND Owner = @Owner;", parameters))
            {
                if (dataTable.Rows.Count == 0)
                {
                    string text3 = null;
                    text3 = !Server.useMySQL ? "INSERT INTO MapList (MapName, Owner) VALUES (@MapName, @Owner); SELECT LAST_INSERT_ROWID() AS Id;"
                        : "INSERT INTO MapList (MapName, Owner) VALUES (@MapName, @Owner); SELECT LAST_INSERT_ID() AS Id;";
                    using (DataTable dataTable2 = DBInterface.fillData(text3, parameters))
                    {
                        num = uint.Parse(dataTable2.Rows[0]["Id"].ToString());
                    }
                }
                else
                {
                    num = uint.Parse(dataTable.Rows[0]["Id"].ToString());
                }
            }
            var dictionary2 = new Dictionary<string, object>();
            dictionary2.Add("@Id", num);
            Dictionary<string, object> parameters2 = dictionary2;
            try
            {
                Level level = ReadLevelFile(text2);
                level.MapDbId = num;
                level.name = mapName;
                level.directoryPath = directoryPath;
                level.fileName = text;
                level.backDup = true;
                level.setPhysics(0);
                level.IsMapBeingBackuped = true;
                using (DataTable dataTable3 = DBInterface.fillData("SELECT * FROM `Zones` WHERE Map = @Id", parameters2))
                {
                    try
                    {
                        Zone item = default(Zone);
                        for (int i = 0; i < dataTable3.Rows.Count; i++)
                        {
                            item.smallX = ushort.Parse(dataTable3.Rows[i]["SmallX"].ToString());
                            item.smallY = ushort.Parse(dataTable3.Rows[i]["SmallY"].ToString());
                            item.smallZ = ushort.Parse(dataTable3.Rows[i]["SmallZ"].ToString());
                            item.bigX = ushort.Parse(dataTable3.Rows[i]["BigX"].ToString());
                            item.bigY = ushort.Parse(dataTable3.Rows[i]["BigY"].ToString());
                            item.bigZ = ushort.Parse(dataTable3.Rows[i]["BigZ"].ToString());
                            item.Owner = dataTable3.Rows[i]["Owner"].ToString();
                            level.ZoneList.Add(item);
                        }
                    }
                    catch (Exception ex)
                    {
                        Server.ErrorLog(ex);
                    }
                }
                level.jailx = (ushort)(level.spawnx * 32);
                level.jaily = (ushort)(level.spawny * 32);
                level.jailz = (ushort)(level.spawnz * 32);
                level.jailrotx = level.rotx;
                level.jailroty = level.roty;
                try
                {
                    if (Server.useMySQL)
                    {
                        using (DataTable dataTable4 = DBInterface.fillData("SELECT * FROM `Portals` WHERE Map = @Id", parameters2))
                        {
                            for (int j = 0; j < dataTable4.Rows.Count; j++)
                            {
                                if (!Block.portal(level.GetTile((ushort)dataTable4.Rows[j]["EntryX"], (ushort)dataTable4.Rows[j]["EntryY"],
                                                                (ushort)dataTable4.Rows[j]["EntryZ"])))
                                {
                                    DBInterface.ExecuteQuery(
                                        string.Concat("DELETE FROM `Portals` WHERE EntryX=", dataTable4.Rows[j]["EntryX"], " AND EntryY=", dataTable4.Rows[j]["EntryY"],
                                                      " AND EntryZ=", dataTable4.Rows[j]["EntryZ"], " AND Map = @Id"), parameters2);
                                }
                            }
                        }
                        using (DataTable dataTable5 = DBInterface.fillData("SELECT * FROM `Messages` WHERE Map = @Id", parameters2))
                        {
                            for (int k = 0; k < dataTable5.Rows.Count; k++)
                            {
                                if (!Block.mb(level.GetTile((ushort)dataTable5.Rows[k]["X"], (ushort)dataTable5.Rows[k]["Y"], (ushort)dataTable5.Rows[k]["Z"])))
                                {
                                    DBInterface.ExecuteQuery(
                                        string.Concat("DELETE FROM `Messages` WHERE Map = @Id AND X=", dataTable5.Rows[k]["X"], " AND Y=", dataTable5.Rows[k]["Y"],
                                                      " AND Z=", dataTable5.Rows[k]["Z"]), parameters2);
                                }
                            }
                        }
                    }
                    else
                    {
                        using (DataTable dataTable6 = DBInterface.fillData("SELECT * FROM `Portals` WHERE Map = @Id", parameters2))
                        {
                            for (int l = 0; l < dataTable6.Rows.Count; l++)
                            {
                                if (!Block.portal(level.GetTile(ushort.Parse(dataTable6.Rows[l]["EntryX"].ToString()),
                                                                ushort.Parse(dataTable6.Rows[l]["EntryY"].ToString()),
                                                                ushort.Parse(dataTable6.Rows[l]["EntryZ"].ToString()))))
                                {
                                    DBInterface.ExecuteQuery(
                                        string.Concat("DELETE FROM `Portals` WHERE Map = @Id AND EntryX=", dataTable6.Rows[l]["EntryX"], " AND EntryY=",
                                                      dataTable6.Rows[l]["EntryY"], " AND EntryZ=", dataTable6.Rows[l]["EntryZ"]), parameters2);
                                }
                            }
                        }
                        using (DataTable dataTable7 = DBInterface.fillData("SELECT * FROM `Messages` WHERE Map = @Id", parameters2))
                        {
                            for (int m = 0; m < dataTable7.Rows.Count; m++)
                            {
                                if (!Block.mb(level.GetTile(ushort.Parse(dataTable7.Rows[m]["X"].ToString()), ushort.Parse(dataTable7.Rows[m]["Y"].ToString()),
                                                            ushort.Parse(dataTable7.Rows[m]["Z"].ToString()))))
                                {
                                    DBInterface.ExecuteQuery(string.Concat("DELETE FROM `Messages` WHERE Map = @Id AND X=", dataTable7.Rows[m]["X"], " AND Y=",
                                                                           dataTable7.Rows[m]["Y"], " AND Z=", dataTable7.Rows[m]["Z"]));
                                }
                            }
                        }
                    }
                }
                catch (Exception ex2)
                {
                    Server.ErrorLog(ex2);
                }
                level.mapType = type;
                level.mapOwner = owner.ToLower();
                string mapPropertiesDirectory = text2.Replace(".lvl", "") + ".properties";
                try
                {
                    LoadLevelPropertiesIfExist(level, mapPropertiesDirectory);
                }
                catch (Exception ex3)
                {
                    Server.ErrorLog(ex3);
                }
                level.unload = isAutoUnloading;
                LoadLevelInfo(level);
                return level;
            }
            catch (Exception ex4)
            {
                Server.ErrorLog(ex4);
                return null;
            }
        }

        static void LoadLevelInfo(Level level)
        {
            LevelInfoManager levelInfoManager = new LevelInfoManager();
            LevelInfoRaw levelInfoRaw = levelInfoManager.Load(level);
            if (levelInfoRaw != null)
            {
                LevelInfoConverter levelInfoConverter = new LevelInfoConverter();
                level.Info = levelInfoConverter.FromRaw(levelInfoRaw);
            }
            else
            {
                level.Info = new LevelInfo();
            }
        }

        public static Level OpenForRaceMode(string directory, string fileName)
        {
            return OpenForRaceMode(directory, fileName, null);
        }

        public static Level OpenForRaceMode(string directory, string fileName, LevelOptions levelOptions)
        {
            string text = directory + Path.DirectorySeparatorChar + fileName;
            if (!File.Exists(text))
            {
                return null;
            }
            string text2 = fileName.Substring(0, fileName.LastIndexOf("."));
            Level level = ReadLevelFile(text);
            level.name = text2;
            level.directoryPath = directory;
            level.fileName = fileName;
            level.setPhysics(0);
            level.backDup = true;
            level.IsMapBeingBackuped = false;
            level.IsPillaringAllowed = false;
            if (levelOptions != null)
            {
                if (levelOptions.PublicName != null)
                {
                    level.name = levelOptions.PublicName;
                }
                level.setPhysics(levelOptions.Physics);
            }
            return level;
        }

        public static Level Load(string givenName, byte phys, MapType mType, bool autoUnload = false)
        {
            try
            {
                if (Server.useMySQL)
                {
                    DBInterface.ExecuteQuery("CREATE TABLE if not exists `Block" + givenName +
                                             "` (Username CHAR(64), TimePerformed DATETIME, X SMALLINT UNSIGNED, Y SMALLINT UNSIGNED, Z SMALLINT UNSIGNED, Type TINYINT UNSIGNED, Deleted BOOL, INDEX (X,Y,Z))");
                    DBInterface.ExecuteQuery("CREATE TABLE if not exists `Portals" + givenName +
                                             "` (EntryX SMALLINT UNSIGNED, EntryY SMALLINT UNSIGNED, EntryZ SMALLINT UNSIGNED, ExitMap CHAR(64), ExitX SMALLINT UNSIGNED, ExitY SMALLINT UNSIGNED, ExitZ SMALLINT UNSIGNED)");
                    DBInterface.ExecuteQuery("CREATE TABLE if not exists `Messages" + givenName +
                                             "` (X SMALLINT UNSIGNED, Y SMALLINT UNSIGNED, Z SMALLINT UNSIGNED, Message CHAR(255));");
                    DBInterface.ExecuteQuery("CREATE TABLE if not exists `Zone" + givenName +
                                             "` (SmallX SMALLINT UNSIGNED, SmallY SMALLINT UNSIGNED, SmallZ SMALLINT UNSIGNED, BigX SMALLINT UNSIGNED, BigY SMALLINT UNSIGNED, BigZ SMALLINT UNSIGNED, Owner VARCHAR(64));");
                    DBInterface.ExecuteQuery("CREATE TABLE if not exists `Rating" + givenName + "` (Username CHAR(64), Vote TINYINT, INDEX(Vote))");
                    DBInterface.ExecuteQuery("ALTER TABLE `Rating" + givenName + "` MODIFY Username CHAR(64)");
                }
                else
                {
                    DBInterface.ExecuteQuery("CREATE TABLE if not exists `Block" + givenName +
                                             "` (Username TEXT, TimePerformed DATETIME, X INTEGER, Y INTEGER, Z INTEGER, Type INTEGER, Deleted INTEGER)");
                    DBInterface.ExecuteQuery("CREATE INDEX if not exists `BlockIndex" + givenName + "` ON `Block" + givenName + "` (X, Y, Z)");
                    DBInterface.ExecuteQuery("CREATE TABLE if not exists `Portals" + givenName +
                                             "` (EntryX SMALLINT UNSIGNED, EntryY SMALLINT UNSIGNED, EntryZ SMALLINT UNSIGNED, ExitMap CHAR(20), ExitX SMALLINT UNSIGNED, ExitY SMALLINT UNSIGNED, ExitZ SMALLINT UNSIGNED)");
                    DBInterface.ExecuteQuery("CREATE TABLE if not exists `Messages" + givenName +
                                             "` (X SMALLINT UNSIGNED, Y SMALLINT UNSIGNED, Z SMALLINT UNSIGNED, Message CHAR(255));");
                    DBInterface.ExecuteQuery("CREATE TABLE if not exists `Zone" + givenName +
                                             "` (SmallX SMALLINT UNSIGNED, SmallY SMALLINT UNSIGNED, SmallZ SMALLINT UNSIGNED, BigX SMALLINT UNSIGNED, BigY SMALLINT UNSIGNED, BigZ SMALLINT UNSIGNED, Owner VARCHAR(64));");
                    DBInterface.ExecuteQuery("CREATE TABLE if not exists `Rating" + givenName + "` (Username CHAR(64), Vote TINYINT)");
                    DBInterface.ExecuteQuery("CREATE INDEX if not exists `RatingIndex" + givenName + "` ON `Rating" + givenName + "` (Vote)");
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
            string text = "levels/" + givenName + ".lvl";
            string text2 = "levels";
            switch (mType)
            {
                case MapType.Lava:
                    text2 = "lava/maps";
                    text = "lava/maps/" + givenName + ".lvl";
                    break;
                case MapType.Zombie:
                    text2 = "infection/maps";
                    text = "infection/maps/" + givenName + ".lvl";
                    break;
                case MapType.Home:
                    text2 = "maps/home";
                    text = "maps/home/" + givenName + ".lvl";
                    break;
            }
            if (!File.Exists(text))
            {
                return null;
            }
            try
            {
                Level level = ReadLevelFile(text);
                level.name = givenName;
                level.directoryPath = text2;
                level.fileName = givenName + ".lvl";
                level.setPhysics(phys);
                level.backDup = true;
                if (mType == MapType.Freebuild || mType == MapType.Home)
                {
                    level.IsMapBeingBackuped = true;
                }
                else
                {
                    level.IsMapBeingBackuped = false;
                }
                using (DataTable dataTable = DBInterface.fillData("SELECT * FROM `Zone" + givenName + "`"))
                {
                    try
                    {
                        Zone item = default(Zone);
                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            item.smallX = ushort.Parse(dataTable.Rows[i]["SmallX"].ToString());
                            item.smallY = ushort.Parse(dataTable.Rows[i]["SmallY"].ToString());
                            item.smallZ = ushort.Parse(dataTable.Rows[i]["SmallZ"].ToString());
                            item.bigX = ushort.Parse(dataTable.Rows[i]["BigX"].ToString());
                            item.bigY = ushort.Parse(dataTable.Rows[i]["BigY"].ToString());
                            item.bigZ = ushort.Parse(dataTable.Rows[i]["BigZ"].ToString());
                            item.Owner = dataTable.Rows[i]["Owner"].ToString();
                            level.ZoneList.Add(item);
                        }
                    }
                    catch (Exception ex2)
                    {
                        Server.ErrorLog(ex2);
                    }
                }
                level.jailx = (ushort)(level.spawnx * 32);
                level.jaily = (ushort)(level.spawny * 32);
                level.jailz = (ushort)(level.spawnz * 32);
                level.jailrotx = level.rotx;
                level.jailroty = level.roty;
                try
                {
                    if (Server.useMySQL)
                    {
                        try
                        {
                            using (DataTable dataTable2 = DBInterface.fillData("SELECT * FROM `Portals" + givenName + "`"))
                            {
                                for (int j = 0; j < dataTable2.Rows.Count; j++)
                                {
                                    if (!Block.portal(level.GetTile((ushort)dataTable2.Rows[j]["EntryX"], (ushort)dataTable2.Rows[j]["EntryY"],
                                                                    (ushort)dataTable2.Rows[j]["EntryZ"])))
                                    {
                                        DBInterface.ExecuteQuery(string.Concat("DELETE FROM `Portals", givenName, "` WHERE EntryX=", dataTable2.Rows[j]["EntryX"],
                                                                               " AND EntryY=", dataTable2.Rows[j]["EntryY"], " AND EntryZ=",
                                                                               dataTable2.Rows[j]["EntryZ"]));
                                    }
                                }
                            }
                            using (DataTable dataTable3 = DBInterface.fillData("SELECT * FROM `Messages" + givenName + "`"))
                            {
                                for (int k = 0; k < dataTable3.Rows.Count; k++)
                                {
                                    if (!Block.mb(level.GetTile((ushort)dataTable3.Rows[k]["X"], (ushort)dataTable3.Rows[k]["Y"], (ushort)dataTable3.Rows[k]["Z"])))
                                    {
                                        DBInterface.ExecuteQuery(string.Concat("DELETE FROM `Messages", givenName, "` WHERE X=", dataTable3.Rows[k]["X"], " AND Y=",
                                                                               dataTable3.Rows[k]["Y"], " AND Z=", dataTable3.Rows[k]["Z"]));
                                    }
                                }
                            }
                        }
                        catch (Exception ex3)
                        {
                            Server.ErrorLog(ex3);
                        }
                    }
                    else
                    {
                        try
                        {
                            using (DataTable dataTable4 = DBInterface.fillData("SELECT * FROM `Portals" + givenName + "`"))
                            {
                                for (int l = 0; l < dataTable4.Rows.Count; l++)
                                {
                                    if (!Block.portal(level.GetTile(ushort.Parse(dataTable4.Rows[l]["EntryX"].ToString()),
                                                                    ushort.Parse(dataTable4.Rows[l]["EntryY"].ToString()),
                                                                    ushort.Parse(dataTable4.Rows[l]["EntryZ"].ToString()))))
                                    {
                                        DBInterface.ExecuteQuery(string.Concat("DELETE FROM `Portals", givenName, "` WHERE EntryX=", dataTable4.Rows[l]["EntryX"],
                                                                               " AND EntryY=", dataTable4.Rows[l]["EntryY"], " AND EntryZ=",
                                                                               dataTable4.Rows[l]["EntryZ"]));
                                    }
                                }
                            }
                            using (DataTable dataTable5 = DBInterface.fillData("SELECT * FROM `Messages" + givenName + "`"))
                            {
                                for (int m = 0; m < dataTable5.Rows.Count; m++)
                                {
                                    if (!Block.mb(level.GetTile(ushort.Parse(dataTable5.Rows[m]["X"].ToString()), ushort.Parse(dataTable5.Rows[m]["Y"].ToString()),
                                                                ushort.Parse(dataTable5.Rows[m]["Z"].ToString()))))
                                    {
                                        DBInterface.ExecuteQuery(string.Concat("DELETE FROM `Messages", givenName, "` WHERE X=", dataTable5.Rows[m]["X"], " AND Y=",
                                                                               dataTable5.Rows[m]["Y"], " AND Z=", dataTable5.Rows[m]["Z"]));
                                    }
                                }
                            }
                        }
                        catch (Exception ex4)
                        {
                            Server.ErrorLog(ex4);
                        }
                    }
                }
                catch (Exception ex5)
                {
                    Server.ErrorLog(ex5);
                }
                level.mapType = mType;
                string text3 = "levels/level properties/" + level.name + ".properties";
                if (!File.Exists(text3))
                {
                    text3 = "levels/level properties/" + level.name;
                }
                if (mType == MapType.Zombie)
                {
                    if (!Directory.Exists("infection/maps/maps properties"))
                    {
                        Directory.CreateDirectory("infection/maps/maps properties");
                    }
                    text3 = "infection/maps/maps properties/" + level.name + ".properties";
                }
                else if (mType == MapType.Home)
                {
                    text3 = "maps/home/" + level.name + ".properties";
                }
                else if (mType != 0)
                {
                    goto IL_09e5;
                }
                try
                {
                    LoadLevelPropertiesIfExist(level, text3);
                }
                catch (Exception ex6)
                {
                    Server.ErrorLog(ex6);
                }
                IL_09e5:
                if (mType == MapType.Zombie)
                {
                    for (int n = 0; n < level.blocks.Length; n++)
                    {
                        if (level.blocks[n] == 9)
                        {
                            level.blocks[n] = 106;
                        }
                    }
                }
                switch (mType)
                {
                    case MapType.Freebuild:
                        level.unload = autoUnload;
                        break;
                    case MapType.Home:
                        level.unload = true;
                        break;
                    default:
                        level.unload = false;
                        break;
                }
                level.mapSettingsManager = new MapSettingsManager(text2 + "/" + givenName + ".cfg.txt");
                level.mapSettingsManager.DeployBlocks(level);
                Server.s.Log("Level \"" + level.name + "\" loaded.");
                if (!Server.CLI)
                {
                    RefreshUnloadedMapsInGUI();
                }
                LoadLevelInfo(level);
                return level;
            }
            catch (Exception ex7)
            {
                Server.ErrorLog(ex7);
                return null;
            }
        }

        static void LoadLevelPropertiesIfExist(Level level, string mapPropertiesDirectory)
        {
            if (!File.Exists(mapPropertiesDirectory))
            {
                return;
            }
            string[] array = File.ReadAllLines(mapPropertiesDirectory);
            foreach (string text in array)
            {
                string text2 = text.Trim();
                if (!(text2 != "") || text2[0] == '#')
                {
                    continue;
                }
                string text3 = text.Substring(text.IndexOf(" = ") + 3);
                switch (text.Substring(0, text.IndexOf(" = ")).ToLower())
                {
                    case "theme":
                        level.theme = text3;
                        break;
                    case "physics":
                        level.setPhysics(int.Parse(text3));
                        break;
                    case "physics speed":
                        if (level.mapType != MapType.Lava)
                        {
                            level.speedPhysics = int.Parse(text3);
                        }
                        break;
                    case "physics overload":
                        if (level.mapType == MapType.Lava)
                        {
                            level.overload = 900000;
                        }
                        else
                        {
                            level.overload = int.Parse(text3);
                        }
                        break;
                    case "finite mode":
                        level.finite = bool.Parse(text3);
                        break;
                    case "animal ai":
                        level.ai = bool.Parse(text3);
                        break;
                    case "edge water":
                        level.edgeWater = bool.Parse(text3);
                        break;
                    case "survival death":
                        level.Death = bool.Parse(text3);
                        break;
                    case "fall":
                        level.fall = int.Parse(text3);
                        break;
                    case "drown":
                        level.drown = int.Parse(text3);
                        break;
                    case "motd":
                        level.motd = text3;
                        break;
                    case "jailx":
                        level.jailx = ushort.Parse(text3);
                        break;
                    case "jaily":
                        level.jaily = ushort.Parse(text3);
                        break;
                    case "jailz":
                        level.jailz = ushort.Parse(text3);
                        break;
                    case "unload":
                        level.unload = bool.Parse(text3);
                        break;
                    case "playerlimit":
                        level.playerLimit = int.Parse(text3);
                        break;
                    case "perbuild":
                        if (PermissionFromName(text3) != LevelPermission.Null)
                        {
                            level.permissionbuild = PermissionFromName(text3);
                        }
                        break;
                    case "pervisit":
                        if (PermissionFromName(text3) != LevelPermission.Null)
                        {
                            level.permissionvisit = PermissionFromName(text3);
                        }
                        break;
                    case "allowed":
                    {
                        string[] array2 = text3.Split(new char[1]
                        {
                            ','
                        }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string text4 in array2)
                        {
                            level.AllowedPlayers.Add(text4.Trim().ToLower());
                        }
                        break;
                    }
                    case "public":
                        level.IsPublic = bool.Parse(text3);
                        break;
                }
            }
        }

        public static LevelFileInfo ReadLevelInfo(string fullPath)
        {
            LevelFileInfo levelFileInfo = new LevelFileInfo();
            using (FileStream stream = File.OpenRead(fullPath))
            {
                using (GZipStream gZipStream = new GZipStream(stream, CompressionMode.Decompress))
                {
                    byte[] array = new byte[2];
                    gZipStream.Read(array, 0, array.Length);
                    ushort num = BitConverter.ToUInt16(array, 0);
                    if (num == 1874)
                    {
                        byte[] array2 = new byte[16];
                        gZipStream.Read(array2, 0, array2.Length);
                        levelFileInfo.Width = BitConverter.ToUInt16(array2, 0);
                        levelFileInfo.Height = BitConverter.ToUInt16(array2, 2);
                        levelFileInfo.Depth = BitConverter.ToUInt16(array2, 4);
                        levelFileInfo.Spawn = new PlayerPosition(BitConverter.ToUInt16(array2, 6), BitConverter.ToUInt16(array2, 8), BitConverter.ToUInt16(array2, 10),
                                                                 array2[12], array2[13]);
                    }
                    else
                    {
                        byte[] array3 = new byte[12];
                        gZipStream.Read(array3, 0, array3.Length);
                        levelFileInfo.Width = num;
                        levelFileInfo.Height = BitConverter.ToUInt16(array3, 0);
                        levelFileInfo.Depth = BitConverter.ToUInt16(array3, 2);
                        levelFileInfo.Spawn = new PlayerPosition(BitConverter.ToUInt16(array3, 4), BitConverter.ToUInt16(array3, 6), BitConverter.ToUInt16(array3, 8),
                                                                 array3[10], array3[11]);
                    }
                }
            }
            return levelFileInfo;
        }

        static Level ReadLevelFile(string fullPath)
        {
            using (FileStream stream = File.OpenRead(fullPath))
            {
                using (GZipStream gZipStream = new GZipStream(stream, CompressionMode.Decompress))
                {
                    byte[] array = new byte[2];
                    gZipStream.Read(array, 0, array.Length);
                    ushort num = BitConverter.ToUInt16(array, 0);
                    Level level;
                    if (num == 1874)
                    {
                        byte[] array2 = new byte[16];
                        gZipStream.Read(array2, 0, array2.Length);
                        ushort x = BitConverter.ToUInt16(array2, 0);
                        ushort z = BitConverter.ToUInt16(array2, 2);
                        ushort y = BitConverter.ToUInt16(array2, 4);
                        level = new Level("temp", x, y, z, "empty");
                        level.spawnx = BitConverter.ToUInt16(array2, 6);
                        level.spawnz = BitConverter.ToUInt16(array2, 8);
                        level.spawny = BitConverter.ToUInt16(array2, 10);
                        level.rotx = array2[12];
                        level.roty = array2[13];
                    }
                    else
                    {
                        byte[] array3 = new byte[12];
                        gZipStream.Read(array3, 0, array3.Length);
                        ushort x2 = num;
                        ushort z2 = BitConverter.ToUInt16(array3, 0);
                        ushort y2 = BitConverter.ToUInt16(array3, 2);
                        level = new Level("temp", x2, y2, z2, "grass");
                        level.spawnx = BitConverter.ToUInt16(array3, 4);
                        level.spawnz = BitConverter.ToUInt16(array3, 6);
                        level.spawny = BitConverter.ToUInt16(array3, 8);
                        level.rotx = array3[10];
                        level.roty = array3[11];
                    }
                    byte[] array4 = new byte[level.width * level.depth * level.height];
                    gZipStream.Read(array4, 0, array4.Length);
                    level.blocks = array4;
                    return level;
                }
            }
        }

        public static bool IsLevelLoaded(string mapName)
        {
            foreach (Level level in Server.levels)
            {
                if (level.name == mapName)
                {
                    return true;
                }
            }
            return false;
        }

        public void ChatLevel(string message)
        {
            for (int i = 0; i < Player.players.Count; i++)
            {
                try
                {
                    if (Player.players[i].level == this)
                    {
                        Player.players[i].SendMessage(message);
                    }
                }
                catch (Exception ex)
                {
                    Server.ErrorLog(ex);
                }
            }
        }

        internal void ChatLevel(byte type, string message)
        {
            IEnumerable<Player> enumerable = from p in Player.players.GetCopy()
                where p.level == this
                select p;
            foreach (Player item in enumerable)
            {
                if (item.Cpe.MessageTypes == 1)
                {
                    Player.SendMessage(item, type, message);
                }
                else
                {
                    Player.SendMessage(item, message);
                }
            }
        }

        internal void ChatLevelCpe(V1.MessageType type, V1.MessageOptions options, string message)
        {
            var list = (from p in Player.players.GetCopy()
                where p.level == this
                select p).ToList();
            foreach (Player item in list)
            {
                if (item.Cpe.MessageTypes == 1)
                {
                    V1.SendMessage(item, type, options, message);
                }
                else if (type == V1.MessageType.Announcement)
                {
                    Player.SendMessage(item, message);
                }
            }
        }

        internal void ChatLevel(byte type, TimeSpan delay, string message, bool includeNonCpe = true)
        {
            var playersHere = (from p in Player.players.GetCopy()
                where p.level == this
                select p).ToList();
            foreach (Player item in playersHere)
            {
                if (item.Cpe.MessageTypes == 1)
                {
                    V1.SendMessage(item, (V1.MessageType)type, new V1.MessageOptions
                    {
                        MinDisplayTime = delay
                    }, message);
                }
                else if (includeNonCpe)
                {
                    Player.SendMessage(item, message);
                }
            }
            System.Threading.Timer timer = null;
            timer = new System.Threading.Timer(delegate
            {
                foreach (Player item2 in playersHere)
                {
                    if (item2.Cpe.MessageTypes == 1)
                    {
                        Player.SendMessage(item2, type, "");
                    }
                }
                timer.Dispose();
            }, null, delay, TimeSpan.FromMilliseconds(-1.0));
        }

        public void ReloadSettings()
        {
            mapSettingsManager.Reload();
            mapSettingsManager.DeployBlocks(this);
            commandActionsHit = null;
            commandActionsWalk = null;
            commandActionsBuild = null;
        }

        public static string GetPhysicsNameByNumber(int number)
        {
            switch (number)
            {
                case 0:
                    return "Off";
                case 1:
                    return "1";
                case 2:
                    return "2";
                case 3:
                    return "3";
                case 4:
                    return "Door";
                default:
                    return "Unknown";
            }
        }

        public void setPhysics(int physics)
        {
            if (this.physics == 0 && physics != 0)
            {
                for (int i = 0; i < blocks.Length; i++)
                {
                    if (Block.NeedRestart(blocks[i]))
                    {
                        AddCheck(i);
                    }
                }
                this.physics = physics;
                StartPhysics();
            }
            else
            {
                this.physics = physics;
            }
        }

        public void StartPhysics()
        {
            physicsTimer.Start();
        }

        public void StopPhysics()
        {
            physicsTimer.Stop();
        }

        public void Physics()
        {
            int num = speedPhysics;
            while (!unloaded)
            {
                try
                {
                    while (true)
                    {
                        if (num > 0)
                        {
                            Thread.Sleep(num);
                        }
                        if (physics == 0)
                        {
                            Thread.Sleep(5000);
                        }
                        else if (ListCheck.Count != 0)
                        {
                            if (!GeneralSettings.All.IntelliSys || InteliSys.pendingPacketsAvg <= GeneralSettings.All.AvgStop)
                            {
                                break;
                            }
                            Thread.Sleep(1000);
                        }
                    }
                    DateTime now = DateTime.Now;
                    CalcPhysics();
                    TimeSpan timeSpan = DateTime.Now - now;
                    num = speedPhysics - (int)timeSpan.TotalMilliseconds;
                    if (num >= (int)(-overload * 0.75f))
                    {
                        continue;
                    }
                    if (num < -overload)
                    {
                        if (!Server.physicsRestart)
                        {
                            setPhysics(0);
                        }
                        ClearPhysics();
                        Player.GlobalMessage(string.Format(rm.GetString("PhysicsShutdownGlobalMessage"), name));
                        Server.s.Log("Physics shutdown on " + name);
                        num = speedPhysics;
                    }
                    else
                    {
                        Player.GlobalMessageLevel(this, rm.GetString("PhysicsWarning"));
                        Server.s.Log("Physics warning on " + name);
                    }
                }
                catch
                {
                    num = speedPhysics;
                }
            }
        }

        public int PosToInt(ushort x, ushort y, ushort z)
        {
            if (x < 0)
            {
                return -1;
            }
            if (x >= width)
            {
                return -1;
            }
            if (y < 0)
            {
                return -1;
            }
            if (y >= height)
            {
                return -1;
            }
            if (z < 0)
            {
                return -1;
            }
            if (z >= depth)
            {
                return -1;
            }
            return x + z * width + y * width * depth;
        }

        public int PosToIntUnchecked(int x, int y, int z)
        {
            return x + z * width + y * width * depth;
        }

        public int PosToInt(int x, int y, int z)
        {
            if (x < 0)
            {
                return -1;
            }
            if (x >= width)
            {
                return -1;
            }
            if (y < 0)
            {
                return -1;
            }
            if (y >= height)
            {
                return -1;
            }
            if (z < 0)
            {
                return -1;
            }
            if (z >= depth)
            {
                return -1;
            }
            return x + z * width + y * width * depth;
        }

        public void IntToPos(int pos, out ushort x, out ushort y, out ushort z)
        {
            y = (ushort)(pos / width / depth);
            pos -= y * width * depth;
            z = (ushort)(pos / width);
            pos -= z * width;
            x = (ushort)pos;
        }

        public int IntOffset(int pos, int x, int y, int z)
        {
            return pos + x + z * width + y * width * depth;
        }

        public string foundInfo(ushort x, ushort y, ushort z)
        {
            Check check = null;
            try
            {
                check = ListCheck.Find(Check => Check.b == PosToInt(x, y, z));
            }
            catch {}
            if (check != null)
            {
                return check.extraInfo;
            }
            return "";
        }

        public void CalcPhysics()
        {
            try
            {
                if (physics <= 0 || Server.pause)
                {
                    return;
                }
                Random rand = new Random();
                lastCheck = ListCheck.Count;
                ushort x;
                ushort y;
                ushort z;
                int mx;
                int my;
                int mz;
                ListCheck.ForEach(delegate(Check C)
                {
                    try
                    {
                        IntToPos(C.b, out x, out y, out z);
                        bool flag = false;
                        bool flag2 = false;
                        int num = 0;
                        Player foundPlayer = null;
                        int foundNum = 75;
                        string text = C.extraInfo;
                        while (text != "")
                        {
                            try
                            {
                                if (GetTile(C.b) == byte.MaxValue)
                                {
                                    Server.s.Log("Out of bounds error!");
                                    ListCheck.Remove(C);
                                    return;
                                }
                                int num2 = 0;
                                try
                                {
                                    if (!text.Contains("wait") && (GetTile(C.b) == 0 || GetTile(C.b) == byte.MaxValue))
                                    {
                                        C.extraInfo = "";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Server.s.Log("Damn you");
                                    Server.ErrorLog(ex);
                                }
                                bool flag3 = false;
                                int num3 = 0;
                                bool flag4 = false;
                                int num4 = 0;
                                bool flag5 = false;
                                int num5 = 0;
                                bool flag6 = false;
                                byte type = 0;
                                bool flag7 = false;
                                int num6 = 0;
                                bool flag8 = false;
                                bool flag9 = false;
                                int num7 = 0;
                                bool flag10 = false;
                                try
                                {
                                    try
                                    {
                                        string[] array = C.extraInfo.Split(' ');
                                        foreach (string text2 in array)
                                        {
                                            if (num2 % 2 == 0)
                                            {
                                                switch (text2)
                                                {
                                                    case "wait":
                                                        flag4 = true;
                                                        num4 = int.Parse(C.extraInfo.Split(' ')[num2 + 1]);
                                                        break;
                                                    case "drop":
                                                        flag3 = true;
                                                        num3 = int.Parse(C.extraInfo.Split(' ')[num2 + 1]);
                                                        break;
                                                    case "dissipate":
                                                        flag5 = true;
                                                        num5 = int.Parse(C.extraInfo.Split(' ')[num2 + 1]);
                                                        break;
                                                    case "revert":
                                                        flag6 = true;
                                                        type = byte.Parse(C.extraInfo.Split(' ')[num2 + 1]);
                                                        break;
                                                    case "explode":
                                                        flag7 = true;
                                                        num6 = int.Parse(C.extraInfo.Split(' ')[num2 + 1]);
                                                        break;
                                                    case "finite":
                                                        flag8 = true;
                                                        break;
                                                    case "rainbow":
                                                        flag9 = true;
                                                        num7 = int.Parse(C.extraInfo.Split(' ')[num2 + 1]);
                                                        break;
                                                    case "door":
                                                        flag10 = true;
                                                        break;
                                                }
                                            }
                                            num2++;
                                        }
                                    }
                                    catch (Exception ex2)
                                    {
                                        Server.s.Log("switch");
                                        Server.ErrorLog(ex2);
                                    }
                                }
                                catch (Exception ex3)
                                {
                                    Server.s.Log("ee");
                                    Server.ErrorLog(ex3);
                                }
                                while (true)
                                {
                                    if (flag4)
                                    {
                                        try
                                        {
                                            int num8 = 0;
                                            if (flag10 && C.time < 2)
                                            {
                                                num8 = IntOffset(C.b, -1, 0, 0);
                                                if (Block.tDoor(blocks[num8]))
                                                {
                                                    AddUpdate(num8, 0, false, "wait 10 door 1 revert " + blocks[num8]);
                                                }
                                                num8 = IntOffset(C.b, 1, 0, 0);
                                                if (Block.tDoor(blocks[num8]))
                                                {
                                                    AddUpdate(num8, 0, false, "wait 10 door 1 revert " + blocks[num8]);
                                                }
                                                num8 = IntOffset(C.b, 0, 1, 0);
                                                if (Block.tDoor(blocks[num8]))
                                                {
                                                    AddUpdate(num8, 0, false, "wait 10 door 1 revert " + blocks[num8]);
                                                }
                                                num8 = IntOffset(C.b, 0, -1, 0);
                                                if (Block.tDoor(blocks[num8]))
                                                {
                                                    AddUpdate(num8, 0, false, "wait 10 door 1 revert " + blocks[num8]);
                                                }
                                                num8 = IntOffset(C.b, 0, 0, 1);
                                                if (Block.tDoor(blocks[num8]))
                                                {
                                                    AddUpdate(num8, 0, false, "wait 10 door 1 revert " + blocks[num8]);
                                                }
                                                num8 = IntOffset(C.b, 0, 0, -1);
                                                if (Block.tDoor(blocks[num8]))
                                                {
                                                    AddUpdate(num8, 0, false, "wait 10 door 1 revert " + blocks[num8]);
                                                }
                                            }
                                            if (num4 <= C.time)
                                            {
                                                flag4 = false;
                                                C.extraInfo = C.extraInfo.Substring(0, C.extraInfo.IndexOf("wait ")) +
                                                    C.extraInfo.Substring(C.extraInfo.IndexOf(' ', C.extraInfo.IndexOf("wait ") + 5) + 1);
                                                continue;
                                            }
                                            C.time++;
                                            text = "";
                                        }
                                        catch (Exception ex4)
                                        {
                                            Server.s.Log("wait");
                                            Server.ErrorLog(ex4);
                                            return;
                                        }
                                        break;
                                    }
                                    if (flag8)
                                    {
                                        try
                                        {
                                            finiteMovement(C, x, y, z);
                                            return;
                                        }
                                        catch (Exception ex5)
                                        {
                                            Server.s.Log("finiteWater");
                                            Server.ErrorLog(ex5);
                                            return;
                                        }
                                    }
                                    if (flag9)
                                    {
                                        try
                                        {
                                            if (C.time < 4)
                                            {
                                                C.time++;
                                            }
                                            else if (num7 > 2)
                                            {
                                                if (blocks[C.b] < 21 || blocks[C.b] > 33)
                                                {
                                                    AddUpdate(C.b, 21, true);
                                                }
                                                else if (blocks[C.b] == 33)
                                                {
                                                    AddUpdate(C.b, 21);
                                                }
                                                else
                                                {
                                                    AddUpdate(C.b, (byte)(blocks[C.b] + 1));
                                                }
                                            }
                                            else
                                            {
                                                AddUpdate(C.b, (byte)rand.Next(21, 33));
                                            }
                                            return;
                                        }
                                        catch (Exception ex6)
                                        {
                                            Server.s.Log("rainbow?");
                                            Server.ErrorLog(ex6);
                                            return;
                                        }
                                    }
                                    if (flag6)
                                    {
                                        try
                                        {
                                            AddUpdate(C.b, type);
                                            C.extraInfo = "";
                                        }
                                        catch (Exception ex7)
                                        {
                                            Server.s.Log("revert");
                                            Server.ErrorLog(ex7);
                                        }
                                    }
                                    if (flag5)
                                    {
                                        try
                                        {
                                            if (rand.Next(1, 100) <= num5)
                                            {
                                                AddUpdate(C.b, 0);
                                                C.extraInfo = "";
                                            }
                                        }
                                        catch (Exception ex8)
                                        {
                                            Server.s.Log("dissipate");
                                            Server.ErrorLog(ex8);
                                        }
                                    }
                                    if (flag7)
                                    {
                                        try
                                        {
                                            if (rand.Next(1, 100) <= num6)
                                            {
                                                MakeExplosion(x, y, z, 0);
                                                C.extraInfo = "";
                                            }
                                        }
                                        catch (Exception ex9)
                                        {
                                            Server.s.Log("explode");
                                            Server.ErrorLog(ex9);
                                        }
                                    }
                                    if (flag3 && rand.Next(1, 100) <= num3 &&
                                        (GetTile(x, (ushort)(y - 1), z) == 0 || GetTile(x, (ushort)(y - 1), z) == 10 || GetTile(x, (ushort)(y - 1), z) == 8) &&
                                        rand.Next(1, 100) < num3)
                                    {
                                        AddUpdate(PosToInt(x, (ushort)(y - 1), z), blocks[C.b], false, C.extraInfo);
                                        AddUpdate(C.b, 0);
                                        C.extraInfo = "";
                                    }
                                    return;
                                }
                            }
                            catch (Exception ex10)
                            {
                                Server.s.Log("Error here");
                                Server.ErrorLog(ex10);
                                return;
                            }
                        }
                        if (physics == 4 && !Block.IsDoor(blocks[C.b]))
                        {
                            C.time = byte.MaxValue;
                        }
                        else
                        {
                            int currentNum;
                            switch (blocks[C.b])
                            {
                                case 16:
                                    PhysCoal(PosToInt((ushort)(x + 1), y, z));
                                    PhysCoal(PosToInt((ushort)(x - 1), y, z));
                                    PhysCoal(PosToInt(x, y, (ushort)(z + 1)));
                                    PhysCoal(PosToInt(x, y, (ushort)(z - 1)));
                                    PhysCoal(PosToInt(x, (ushort)(y + 1), z));
                                    if (Server.hardcore)
                                    {
                                        PhysCoal(PosToInt(x, (ushort)(y - 1), z));
                                    }
                                    break;
                                case 79:
                                    if (C.time > 16)
                                    {
                                        if (GetTile(x, y, z) == 79)
                                        {
                                            AddUpdate(C.b, 0);
                                        }
                                        C.time = byte.MaxValue;
                                    }
                                    C.time++;
                                    break;
                                case 254:
                                    PhysGlass(PosToInt((ushort)(x + 1), y, z));
                                    PhysGlass(PosToInt((ushort)(x - 1), y, z));
                                    PhysGlass(PosToInt(x, y, (ushort)(z + 1)));
                                    PhysGlass(PosToInt(x, y, (ushort)(z - 1)));
                                    PhysGlass(PosToInt(x, (ushort)(y + 1), z));
                                    C.time = byte.MaxValue;
                                    break;
                                case 0:
                                    PhysAir(PosToInt((ushort)(x + 1), y, z));
                                    PhysAir(PosToInt((ushort)(x - 1), y, z));
                                    PhysAir(PosToInt(x, y, (ushort)(z + 1)));
                                    PhysAir(PosToInt(x, y, (ushort)(z - 1)));
                                    PhysAir(PosToInt(x, (ushort)(y + 1), z));
                                    if (edgeWater && y < height / 2 && y >= height / 2 - 2 && (x == 0 || x == width - 1 || z == 0 || z == depth - 1))
                                    {
                                        AddUpdate(C.b, 8);
                                    }
                                    if (!C.extraInfo.Contains("wait"))
                                    {
                                        C.time = byte.MaxValue;
                                    }
                                    break;
                                case 3:
                                    if (!GrassGrow)
                                    {
                                        C.time = byte.MaxValue;
                                    }
                                    else if (C.time > 20)
                                    {
                                        if (Block.LightPass(GetTile(x, (ushort)(y + 1), z)))
                                        {
                                            AddUpdate(C.b, 2);
                                        }
                                        C.time = byte.MaxValue;
                                    }
                                    else
                                    {
                                        C.time++;
                                    }
                                    break;
                                case 8:
                                case 193:
                                    if (!finite)
                                    {
                                        if (!PhysSpongeCheck(C.b))
                                        {
                                            if (GetTile(x, (ushort)(y + 1), z) != byte.MaxValue)
                                            {
                                                PhysSandCheck(PosToInt(x, (ushort)(y + 1), z));
                                            }
                                            PhysWater(PosToInt((ushort)(x + 1), y, z), blocks[C.b]);
                                            PhysWater(PosToInt((ushort)(x - 1), y, z), blocks[C.b]);
                                            PhysWater(PosToInt(x, y, (ushort)(z + 1)), blocks[C.b]);
                                            PhysWater(PosToInt(x, y, (ushort)(z - 1)), blocks[C.b]);
                                            PhysWater(PosToInt(x, (ushort)(y - 1), z), blocks[C.b]);
                                        }
                                        else
                                        {
                                            AddUpdate(C.b, 0);
                                        }
                                        if (C.extraInfo.IndexOf("wait") == -1)
                                        {
                                            C.time = byte.MaxValue;
                                        }
                                        break;
                                    }
                                    goto case 145;
                                case 140:
                                    rand = new Random();
                                    if (GetTile(x, (ushort)(y - 1), z) == 0)
                                    {
                                        AddUpdate(PosToInt(x, (ushort)(y - 1), z), 140);
                                        if (C.extraInfo.IndexOf("wait") == -1)
                                        {
                                            C.time = byte.MaxValue;
                                        }
                                    }
                                    else if (GetTile(x, (ushort)(y - 1), z) != 203 && GetTile(x, (ushort)(y - 1), z) != 9 && GetTile(x, (ushort)(y - 1), z) != 11 &&
                                             GetTile(x, (ushort)(y - 1), z) != 140)
                                    {
                                        PhysWater(PosToInt((ushort)(x + 1), y, z), blocks[C.b]);
                                        PhysWater(PosToInt((ushort)(x - 1), y, z), blocks[C.b]);
                                        PhysWater(PosToInt(x, y, (ushort)(z + 1)), blocks[C.b]);
                                        PhysWater(PosToInt(x, y, (ushort)(z - 1)), blocks[C.b]);
                                        if (C.extraInfo.IndexOf("wait") == -1)
                                        {
                                            C.time = byte.MaxValue;
                                        }
                                    }
                                    break;
                                case 141:
                                    rand = new Random();
                                    if (GetTile(x, (ushort)(y - 1), z) == 0)
                                    {
                                        AddUpdate(PosToInt(x, (ushort)(y - 1), z), 141);
                                        if (C.extraInfo.IndexOf("wait") == -1)
                                        {
                                            C.time = byte.MaxValue;
                                        }
                                    }
                                    else if (GetTile(x, (ushort)(y - 1), z) != 203 && GetTile(x, (ushort)(y - 1), z) != 9 && GetTile(x, (ushort)(y - 1), z) != 11 &&
                                             GetTile(x, (ushort)(y - 1), z) != 141)
                                    {
                                        PhysLava(PosToInt((ushort)(x + 1), y, z), blocks[C.b]);
                                        PhysLava(PosToInt((ushort)(x - 1), y, z), blocks[C.b]);
                                        PhysLava(PosToInt(x, y, (ushort)(z + 1)), blocks[C.b]);
                                        PhysLava(PosToInt(x, y, (ushort)(z - 1)), blocks[C.b]);
                                        if (C.extraInfo.IndexOf("wait") == -1)
                                        {
                                            C.time = byte.MaxValue;
                                        }
                                    }
                                    break;
                                case 143:
                                    C.time++;
                                    if (C.time >= 2)
                                    {
                                        C.time = 0;
                                        if (GetTile(x, (ushort)(y - 1), z) == 0 || GetTile(x, (ushort)(y - 1), z) == 140)
                                        {
                                            if (rand.Next(1, 10) > 7)
                                            {
                                                AddUpdate(PosToInt(x, (ushort)(y - 1), z), 203);
                                            }
                                        }
                                        else if (GetTile(x, (ushort)(y - 1), z) == 203 && rand.Next(1, 10) > 4)
                                        {
                                            AddUpdate(PosToInt(x, (ushort)(y - 1), z), 140);
                                        }
                                    }
                                    break;
                                case 144:
                                    C.time++;
                                    if (C.time >= 2)
                                    {
                                        C.time = 0;
                                        if (GetTile(x, (ushort)(y - 1), z) == 0 || GetTile(x, (ushort)(y - 1), z) == 141)
                                        {
                                            if (rand.Next(1, 10) > 7)
                                            {
                                                AddUpdate(PosToInt(x, (ushort)(y - 1), z), 203);
                                            }
                                        }
                                        else if (GetTile(x, (ushort)(y - 1), z) == 203 && rand.Next(1, 10) > 4)
                                        {
                                            AddUpdate(PosToInt(x, (ushort)(y - 1), z), 141);
                                        }
                                    }
                                    break;
                                case 80:
                                    if (!PhysSpongeCheck(C.b))
                                    {
                                        if (C.time == 3)
                                        {
                                            PhysLava(PosToInt(x, (ushort)(y - 1), z), blocks[C.b]);
                                            C.time++;
                                        }
                                        else if (C.time == 7)
                                        {
                                            PhysLava(PosToInt((ushort)(x + 1), y, z), blocks[C.b]);
                                            PhysLava(PosToInt((ushort)(x - 1), y, z), blocks[C.b]);
                                            PhysLava(PosToInt(x, y, (ushort)(z + 1)), blocks[C.b]);
                                            PhysLava(PosToInt(x, y, (ushort)(z - 1)), blocks[C.b]);
                                            C.time = byte.MaxValue;
                                        }
                                        else
                                        {
                                            C.time++;
                                        }
                                    }
                                    else
                                    {
                                        C.time = byte.MaxValue;
                                    }
                                    break;
                                case 81:
                                    if (!PhysSpongeCheck(C.b))
                                    {
                                        if (C.time == 1)
                                        {
                                            PhysLava(PosToInt(x, (ushort)(y - 1), z), blocks[C.b]);
                                            C.time++;
                                        }
                                        else if (C.time == 5)
                                        {
                                            PhysLava(PosToInt((ushort)(x + 1), y, z), blocks[C.b]);
                                            PhysLava(PosToInt((ushort)(x - 1), y, z), blocks[C.b]);
                                            PhysLava(PosToInt(x, y, (ushort)(z + 1)), blocks[C.b]);
                                            PhysLava(PosToInt(x, y, (ushort)(z - 1)), blocks[C.b]);
                                            C.time = byte.MaxValue;
                                        }
                                        else
                                        {
                                            C.time++;
                                        }
                                    }
                                    else
                                    {
                                        C.time = byte.MaxValue;
                                    }
                                    break;
                                case 82:
                                    if (!PhysSpongeCheck(C.b))
                                    {
                                        if (C.time == 0)
                                        {
                                            C.time = (byte)randLavaTypeC.Next(1, 5);
                                        }
                                        else if (C.time == 1)
                                        {
                                            PhysLava(PosToInt(x, (ushort)(y - 1), z), blocks[C.b]);
                                            PhysLava(PosToInt((ushort)(x + 1), y, z), blocks[C.b]);
                                            PhysLava(PosToInt((ushort)(x - 1), y, z), blocks[C.b]);
                                            PhysLava(PosToInt(x, y, (ushort)(z + 1)), blocks[C.b]);
                                            PhysLava(PosToInt(x, y, (ushort)(z - 1)), blocks[C.b]);
                                            C.time = byte.MaxValue;
                                        }
                                        else
                                        {
                                            C.time--;
                                        }
                                    }
                                    else
                                    {
                                        C.time = byte.MaxValue;
                                    }
                                    break;
                                case 83:
                                    if (!PhysSpongeCheck(C.b))
                                    {
                                        if (C.time == 0)
                                        {
                                            C.time = (byte)randLavaTypeC.Next(3, 5);
                                        }
                                        else if (C.time == 3)
                                        {
                                            PhysLava(PosToInt(x, (ushort)(y - 1), z), blocks[C.b]);
                                            C.time--;
                                        }
                                        else if (C.time == 1)
                                        {
                                            PhysLava(PosToInt((ushort)(x + 1), y, z), blocks[C.b]);
                                            PhysLava(PosToInt((ushort)(x - 1), y, z), blocks[C.b]);
                                            PhysLava(PosToInt(x, y, (ushort)(z + 1)), blocks[C.b]);
                                            PhysLava(PosToInt(x, y, (ushort)(z - 1)), blocks[C.b]);
                                            C.time = byte.MaxValue;
                                        }
                                        else
                                        {
                                            C.time--;
                                        }
                                    }
                                    else
                                    {
                                        C.time = byte.MaxValue;
                                    }
                                    break;
                                case 99:
                                    if (GetTile(x, (ushort)(y - 1), z) == 0)
                                    {
                                        AddUpdate(PosToInt(x, (ushort)(y + 1), z), 0);
                                        AddUpdate(PosToInt(x, y, z), 11);
                                        AddUpdate(PosToInt(x, (ushort)(y - 1), z), 99);
                                    }
                                    else
                                    {
                                        MakeExplosion(x, y, z, 1);
                                        C.time = byte.MaxValue;
                                    }
                                    break;
                                case 96:
                                    DrawBall(x, y, z, 3, 3);
                                    C.time = byte.MaxValue;
                                    break;
                                case 97:
                                    if (GetTile(x, (ushort)(y - 1), z) == 0)
                                    {
                                        AddUpdate(PosToInt(x, y, z), 0);
                                        AddUpdate(PosToInt(x, (ushort)(y - 1), z), 97);
                                    }
                                    break;
                                case 74:
                                    if (C.time < lavaSpeed)
                                    {
                                        C.time++;
                                    }
                                    else
                                    {
                                        if (!PhysSpongeCheck(C.b))
                                        {
                                            if (finite)
                                            {
                                                goto case 145;
                                            }
                                            PhysWater(PosToInt((ushort)(x + 1), y, z), blocks[C.b]);
                                            PhysWater(PosToInt((ushort)(x - 1), y, z), blocks[C.b]);
                                            PhysWater(PosToInt(x, y, (ushort)(z + 1)), blocks[C.b]);
                                            PhysWater(PosToInt(x, y, (ushort)(z - 1)), blocks[C.b]);
                                            PhysWater(PosToInt(x, (ushort)(y - 1), z), blocks[C.b]);
                                            PhysWater(PosToInt(x, (ushort)(y + 1), z), blocks[C.b]);
                                        }
                                        if (C.extraInfo.IndexOf("wait") == -1)
                                        {
                                            C.time = byte.MaxValue;
                                        }
                                    }
                                    break;
                                case 98:
                                    if (C.time < lavaSpeed)
                                    {
                                        C.time++;
                                    }
                                    else
                                    {
                                        if (!PhysSpongeCheck(C.b))
                                        {
                                            if (finite)
                                            {
                                                goto case 145;
                                            }
                                            PhysLava(PosToInt((ushort)(x + 1), y, z), blocks[C.b]);
                                            PhysLava(PosToInt((ushort)(x - 1), y, z), blocks[C.b]);
                                            PhysLava(PosToInt(x, y, (ushort)(z + 1)), blocks[C.b]);
                                            PhysLava(PosToInt(x, y, (ushort)(z - 1)), blocks[C.b]);
                                            PhysLava(PosToInt(x, (ushort)(y - 1), z), blocks[C.b]);
                                            PhysLava(PosToInt(x, (ushort)(y + 1), z), blocks[C.b]);
                                        }
                                        if (C.extraInfo.IndexOf("wait") == -1)
                                        {
                                            C.time = byte.MaxValue;
                                        }
                                    }
                                    break;
                                case 10:
                                case 194:
                                    if (C.time < lavaSpeed)
                                    {
                                        C.time++;
                                    }
                                    else
                                    {
                                        if (!PhysSpongeCheck(C.b))
                                        {
                                            if (finite)
                                            {
                                                goto case 145;
                                            }
                                            PhysLava(PosToInt((ushort)(x + 1), y, z), blocks[C.b]);
                                            PhysLava(PosToInt((ushort)(x - 1), y, z), blocks[C.b]);
                                            PhysLava(PosToInt(x, y, (ushort)(z + 1)), blocks[C.b]);
                                            PhysLava(PosToInt(x, y, (ushort)(z - 1)), blocks[C.b]);
                                            PhysLava(PosToInt(x, (ushort)(y - 1), z), blocks[C.b]);
                                            if (hardcore || Server.hardcore)
                                            {
                                                PhysLava(PosToInt(x, (ushort)(y + 1), z), blocks[C.b]);
                                            }
                                        }
                                        if (C.extraInfo.IndexOf("wait") == -1)
                                        {
                                            C.time = byte.MaxValue;
                                        }
                                    }
                                    break;
                                case 185:
                                    if (C.time < 2)
                                    {
                                        C.time++;
                                    }
                                    else
                                    {
                                        num = rand.Next(1, 20);
                                        if (num < 2 && C.time % 2 == 0)
                                        {
                                            num = rand.Next(1, 18);
                                            if (num <= 3 && GetTile((ushort)(x - 1), y, z) == 0)
                                            {
                                                AddUpdate(PosToInt((ushort)(x - 1), y, z), 185);
                                            }
                                            else if (num <= 6 && GetTile((ushort)(x + 1), y, z) == 0)
                                            {
                                                AddUpdate(PosToInt((ushort)(x + 1), y, z), 185);
                                            }
                                            else if (num <= 9 && GetTile(x, (ushort)(y - 1), z) == 0)
                                            {
                                                AddUpdate(PosToInt(x, (ushort)(y - 1), z), 185);
                                            }
                                            else if (num <= 12 && GetTile(x, (ushort)(y + 1), z) == 0)
                                            {
                                                AddUpdate(PosToInt(x, (ushort)(y + 1), z), 185);
                                            }
                                            else if (num <= 15 && GetTile(x, y, (ushort)(z - 1)) == 0)
                                            {
                                                AddUpdate(PosToInt(x, y, (ushort)(z - 1)), 185);
                                            }
                                            else if (num <= 18 && GetTile(x, y, (ushort)(z + 1)) == 0)
                                            {
                                                AddUpdate(PosToInt(x, y, (ushort)(z + 1)), 185);
                                            }
                                        }
                                        if (Block.LavaKill(GetTile((ushort)(x - 1), y, (ushort)(z - 1))))
                                        {
                                            if (GetTile((ushort)(x - 1), y, z) == 0)
                                            {
                                                AddUpdate(PosToInt((ushort)(x - 1), y, z), 185);
                                            }
                                            if (GetTile(x, y, (ushort)(z - 1)) == 0)
                                            {
                                                AddUpdate(PosToInt(x, y, (ushort)(z - 1)), 185);
                                            }
                                        }
                                        if (Block.LavaKill(GetTile((ushort)(x + 1), y, (ushort)(z - 1))))
                                        {
                                            if (GetTile((ushort)(x + 1), y, z) == 0)
                                            {
                                                AddUpdate(PosToInt((ushort)(x + 1), y, z), 185);
                                            }
                                            if (GetTile(x, y, (ushort)(z - 1)) == 0)
                                            {
                                                AddUpdate(PosToInt(x, y, (ushort)(z - 1)), 185);
                                            }
                                        }
                                        if (Block.LavaKill(GetTile((ushort)(x - 1), y, (ushort)(z + 1))))
                                        {
                                            if (GetTile((ushort)(x - 1), y, z) == 0)
                                            {
                                                AddUpdate(PosToInt((ushort)(x - 1), y, z), 185);
                                            }
                                            if (GetTile(x, y, (ushort)(z + 1)) == 0)
                                            {
                                                AddUpdate(PosToInt(x, y, (ushort)(z + 1)), 185);
                                            }
                                        }
                                        if (Block.LavaKill(GetTile((ushort)(x + 1), y, (ushort)(z + 1))))
                                        {
                                            if (GetTile((ushort)(x + 1), y, z) == 0)
                                            {
                                                AddUpdate(PosToInt((ushort)(x + 1), y, z), 185);
                                            }
                                            if (GetTile(x, y, (ushort)(z + 1)) == 0)
                                            {
                                                AddUpdate(PosToInt(x, y, (ushort)(z + 1)), 185);
                                            }
                                        }
                                        if (Block.LavaKill(GetTile(x, (ushort)(y - 1), (ushort)(z - 1))))
                                        {
                                            if (GetTile(x, (ushort)(y - 1), z) == 0)
                                            {
                                                AddUpdate(PosToInt(x, (ushort)(y - 1), z), 185);
                                            }
                                            if (GetTile(x, y, (ushort)(z - 1)) == 0)
                                            {
                                                AddUpdate(PosToInt(x, y, (ushort)(z - 1)), 185);
                                            }
                                        }
                                        else if (GetTile(x, (ushort)(y - 1), z) == 2)
                                        {
                                            AddUpdate(PosToInt(x, (ushort)(y - 1), z), 3);
                                        }
                                        if (Block.LavaKill(GetTile(x, (ushort)(y + 1), (ushort)(z - 1))))
                                        {
                                            if (GetTile(x, (ushort)(y + 1), z) == 0)
                                            {
                                                AddUpdate(PosToInt(x, (ushort)(y + 1), z), 185);
                                            }
                                            if (GetTile(x, y, (ushort)(z - 1)) == 0)
                                            {
                                                AddUpdate(PosToInt(x, y, (ushort)(z - 1)), 185);
                                            }
                                        }
                                        if (Block.LavaKill(GetTile(x, (ushort)(y - 1), (ushort)(z + 1))))
                                        {
                                            if (GetTile(x, (ushort)(y - 1), z) == 0)
                                            {
                                                AddUpdate(PosToInt(x, (ushort)(y - 1), z), 185);
                                            }
                                            if (GetTile(x, y, (ushort)(z + 1)) == 0)
                                            {
                                                AddUpdate(PosToInt(x, y, (ushort)(z + 1)), 185);
                                            }
                                        }
                                        if (Block.LavaKill(GetTile(x, (ushort)(y + 1), (ushort)(z + 1))))
                                        {
                                            if (GetTile(x, (ushort)(y + 1), z) == 0)
                                            {
                                                AddUpdate(PosToInt(x, (ushort)(y + 1), z), 185);
                                            }
                                            if (GetTile(x, y, (ushort)(z + 1)) == 0)
                                            {
                                                AddUpdate(PosToInt(x, y, (ushort)(z + 1)), 185);
                                            }
                                        }
                                        if (Block.LavaKill(GetTile((ushort)(x - 1), (ushort)(y - 1), z)))
                                        {
                                            if (GetTile(x, (ushort)(y - 1), z) == 0)
                                            {
                                                AddUpdate(PosToInt(x, (ushort)(y - 1), z), 185);
                                            }
                                            if (GetTile((ushort)(x - 1), y, z) == 0)
                                            {
                                                AddUpdate(PosToInt((ushort)(x - 1), y, z), 185);
                                            }
                                        }
                                        if (Block.LavaKill(GetTile((ushort)(x - 1), (ushort)(y + 1), z)))
                                        {
                                            if (GetTile(x, (ushort)(y + 1), z) == 0)
                                            {
                                                AddUpdate(PosToInt(x, (ushort)(y + 1), z), 185);
                                            }
                                            if (GetTile((ushort)(x - 1), y, z) == 0)
                                            {
                                                AddUpdate(PosToInt((ushort)(x - 1), y, z), 185);
                                            }
                                        }
                                        if (Block.LavaKill(GetTile((ushort)(x + 1), (ushort)(y - 1), z)))
                                        {
                                            if (GetTile(x, (ushort)(y - 1), z) == 0)
                                            {
                                                AddUpdate(PosToInt(x, (ushort)(y - 1), z), 185);
                                            }
                                            if (GetTile((ushort)(x + 1), y, z) == 0)
                                            {
                                                AddUpdate(PosToInt((ushort)(x + 1), y, z), 185);
                                            }
                                        }
                                        if (Block.LavaKill(GetTile((ushort)(x + 1), (ushort)(y + 1), z)))
                                        {
                                            if (GetTile(x, (ushort)(y + 1), z) == 0)
                                            {
                                                AddUpdate(PosToInt(x, (ushort)(y + 1), z), 185);
                                            }
                                            if (GetTile((ushort)(x + 1), y, z) == 0)
                                            {
                                                AddUpdate(PosToInt((ushort)(x + 1), y, z), 185);
                                            }
                                        }
                                        if (physics >= 2)
                                        {
                                            if (C.time < 4)
                                            {
                                                C.time++;
                                                break;
                                            }
                                            if (Block.LavaKill(GetTile((ushort)(x - 1), y, z)))
                                            {
                                                AddUpdate(PosToInt((ushort)(x - 1), y, z), 185);
                                            }
                                            else if (GetTile((ushort)(x - 1), y, z) == 46)
                                            {
                                                MakeExplosion((ushort)(x - 1), y, z, -1);
                                            }
                                            if (Block.LavaKill(GetTile((ushort)(x + 1), y, z)))
                                            {
                                                AddUpdate(PosToInt((ushort)(x + 1), y, z), 185);
                                            }
                                            else if (GetTile((ushort)(x + 1), y, z) == 46)
                                            {
                                                MakeExplosion((ushort)(x + 1), y, z, -1);
                                            }
                                            if (Block.LavaKill(GetTile(x, (ushort)(y - 1), z)))
                                            {
                                                AddUpdate(PosToInt(x, (ushort)(y - 1), z), 185);
                                            }
                                            else if (GetTile(x, (ushort)(y - 1), z) == 46)
                                            {
                                                MakeExplosion(x, (ushort)(y - 1), z, -1);
                                            }
                                            if (Block.LavaKill(GetTile(x, (ushort)(y + 1), z)))
                                            {
                                                AddUpdate(PosToInt(x, (ushort)(y + 1), z), 185);
                                            }
                                            else if (GetTile(x, (ushort)(y + 1), z) == 46)
                                            {
                                                MakeExplosion(x, (ushort)(y + 1), z, -1);
                                            }
                                            if (Block.LavaKill(GetTile(x, y, (ushort)(z - 1))))
                                            {
                                                AddUpdate(PosToInt(x, y, (ushort)(z - 1)), 185);
                                            }
                                            else if (GetTile(x, y, (ushort)(z - 1)) == 46)
                                            {
                                                MakeExplosion(x, y, (ushort)(z - 1), -1);
                                            }
                                            if (Block.LavaKill(GetTile(x, y, (ushort)(z + 1))))
                                            {
                                                AddUpdate(PosToInt(x, y, (ushort)(z + 1)), 185);
                                            }
                                            else if (GetTile(x, y, (ushort)(z + 1)) == 46)
                                            {
                                                MakeExplosion(x, y, (ushort)(z + 1), -1);
                                            }
                                        }
                                        C.time++;
                                        if (C.time > 5)
                                        {
                                            num = rand.Next(1, 10);
                                            if (num <= 2)
                                            {
                                                AddUpdate(C.b, 16);
                                                C.extraInfo = "drop 63 dissipate 10";
                                            }
                                            else if (num <= 4)
                                            {
                                                AddUpdate(C.b, 49);
                                                C.extraInfo = "drop 63 dissipate 10";
                                            }
                                            else if (num <= 8)
                                            {
                                                AddUpdate(C.b, 0);
                                            }
                                            else
                                            {
                                                C.time = 3;
                                            }
                                        }
                                    }
                                    break;
                                case 145:
                                case 146:
                                    finiteMovement(C, x, y, z);
                                    break;
                                case 147:
                                {
                                    var list = new List<int>();
                                    for (int num11 = 0; num11 < 6; num11++)
                                    {
                                        list.Add(num11);
                                    }
                                    for (int num12 = list.Count - 1; num12 > 1; num12--)
                                    {
                                        int index = rand.Next(num12);
                                        int value = list[num12];
                                        list[num12] = list[index];
                                        list[index] = value;
                                    }
                                    using (List<int>.Enumerator enumerator = list.GetEnumerator())
                                    {
                                        while (enumerator.MoveNext())
                                        {
                                            switch (enumerator.Current)
                                            {
                                                case 0:
                                                    if (GetTile((ushort)(x - 1), y, z) == 0 && AddUpdate(PosToInt((ushort)(x - 1), y, z), 145))
                                                    {
                                                        flag = true;
                                                    }
                                                    break;
                                                case 1:
                                                    if (GetTile((ushort)(x + 1), y, z) == 0 && AddUpdate(PosToInt((ushort)(x + 1), y, z), 145))
                                                    {
                                                        flag = true;
                                                    }
                                                    break;
                                                case 2:
                                                    if (GetTile(x, (ushort)(y - 1), z) == 0 && AddUpdate(PosToInt(x, (ushort)(y - 1), z), 145))
                                                    {
                                                        flag = true;
                                                    }
                                                    break;
                                                case 3:
                                                    if (GetTile(x, (ushort)(y + 1), z) == 0 && AddUpdate(PosToInt(x, (ushort)(y + 1), z), 145))
                                                    {
                                                        flag = true;
                                                    }
                                                    break;
                                                case 4:
                                                    if (GetTile(x, y, (ushort)(z - 1)) == 0 && AddUpdate(PosToInt(x, y, (ushort)(z - 1)), 145))
                                                    {
                                                        flag = true;
                                                    }
                                                    break;
                                                case 5:
                                                    if (GetTile(x, y, (ushort)(z + 1)) == 0 && AddUpdate(PosToInt(x, y, (ushort)(z + 1)), 145))
                                                    {
                                                        flag = true;
                                                    }
                                                    break;
                                            }
                                            if (flag)
                                            {
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                }
                                case 12:
                                    if (PhysSand(C.b, 12))
                                    {
                                        PhysAir(PosToInt((ushort)(x + 1), y, z));
                                        PhysAir(PosToInt((ushort)(x - 1), y, z));
                                        PhysAir(PosToInt(x, y, (ushort)(z + 1)));
                                        PhysAir(PosToInt(x, y, (ushort)(z - 1)));
                                        PhysAir(PosToInt(x, (ushort)(y + 1), z));
                                    }
                                    C.time = byte.MaxValue;
                                    break;
                                case 13:
                                    if (PhysSand(C.b, 13))
                                    {
                                        PhysAir(PosToInt((ushort)(x + 1), y, z));
                                        PhysAir(PosToInt((ushort)(x - 1), y, z));
                                        PhysAir(PosToInt(x, y, (ushort)(z + 1)));
                                        PhysAir(PosToInt(x, y, (ushort)(z - 1)));
                                        PhysAir(PosToInt(x, (ushort)(y + 1), z));
                                    }
                                    C.time = byte.MaxValue;
                                    break;
                                case 19:
                                    PhysUniversalSponge(C.b);
                                    if (C.time > 30)
                                    {
                                        if (GetTile(x, y, z) == 19)
                                        {
                                            Blockchange(x, y, z, 0);
                                        }
                                        C.time = byte.MaxValue;
                                    }
                                    else
                                    {
                                        C.time++;
                                    }
                                    break;
                                case 78:
                                    PhysUniversalSponge(C.b);
                                    C.time = byte.MaxValue;
                                    break;
                                case 253:
                                    PhysUniversalSponge(C.b);
                                    C.time = byte.MaxValue;
                                    break;
                                case 5:
                                case 6:
                                case 17:
                                case 18:
                                case 37:
                                case 38:
                                case 39:
                                case 40:
                                case 47:
                                case 51:
                                case 53:
                                case 54:
                                    if (physics > 1)
                                    {
                                        PhysAir(PosToInt((ushort)(x + 1), y, z));
                                        PhysAir(PosToInt((ushort)(x - 1), y, z));
                                        PhysAir(PosToInt(x, y, (ushort)(z + 1)));
                                        PhysAir(PosToInt(x, y, (ushort)(z - 1)));
                                        PhysAir(PosToInt(x, (ushort)(y + 1), z));
                                    }
                                    C.time = byte.MaxValue;
                                    break;
                                case 44:
                                    PhysStair(C.b);
                                    C.time = byte.MaxValue;
                                    break;
                                case 110:
                                    PhysFloatwood(C.b);
                                    C.time = byte.MaxValue;
                                    break;
                                case 112:
                                    PhysLava(PosToInt((ushort)(x + 1), y, z), 112);
                                    PhysLava(PosToInt((ushort)(x - 1), y, z), 112);
                                    PhysLava(PosToInt(x, y, (ushort)(z + 1)), 112);
                                    PhysLava(PosToInt(x, y, (ushort)(z - 1)), 112);
                                    PhysLava(PosToInt(x, (ushort)(y - 1), z), 112);
                                    C.time = byte.MaxValue;
                                    break;
                                case 200:
                                    if (C.time < 1)
                                    {
                                        PhysAirFlood(PosToInt((ushort)(x + 1), y, z), 200);
                                        PhysAirFlood(PosToInt((ushort)(x - 1), y, z), 200);
                                        PhysAirFlood(PosToInt(x, y, (ushort)(z + 1)), 200);
                                        PhysAirFlood(PosToInt(x, y, (ushort)(z - 1)), 200);
                                        PhysAirFlood(PosToInt(x, (ushort)(y - 1), z), 200);
                                        PhysAirFlood(PosToInt(x, (ushort)(y + 1), z), 200);
                                        C.time++;
                                    }
                                    else
                                    {
                                        AddUpdate(C.b, 0);
                                        C.time = byte.MaxValue;
                                    }
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
                                case 213:
                                case 215:
                                case 216:
                                case 225:
                                case 226:
                                case 227:
                                case 228:
                                case 229:
                                    AnyDoor(C, x, y, z, 16);
                                    break;
                                case 214:
                                case 217:
                                    AnyDoor(C, x, y, z, 4, instaUpdate: true);
                                    break;
                                case 212:
                                    AnyDoor(C, x, y, z, 4);
                                    break;
                                case 148:
                                case 149:
                                case 150:
                                case 151:
                                case 152:
                                case 153:
                                case 154:
                                case 155:
                                case 156:
                                case 157:
                                case 158:
                                case 159:
                                case 168:
                                case 169:
                                case 170:
                                case 171:
                                case 172:
                                case 173:
                                case 174:
                                case 177:
                                case 178:
                                case 179:
                                case 180:
                                case 181:
                                    odoor(C);
                                    break;
                                case 202:
                                    if (C.time < 1)
                                    {
                                        PhysAirFlood(PosToInt((ushort)(x + 1), y, z), 202);
                                        PhysAirFlood(PosToInt((ushort)(x - 1), y, z), 202);
                                        PhysAirFlood(PosToInt(x, y, (ushort)(z + 1)), 202);
                                        PhysAirFlood(PosToInt(x, y, (ushort)(z - 1)), 202);
                                        C.time++;
                                    }
                                    else
                                    {
                                        AddUpdate(C.b, 0);
                                        C.time = byte.MaxValue;
                                    }
                                    break;
                                case 203:
                                    if (C.time < 1)
                                    {
                                        PhysAirFlood(PosToInt((ushort)(x + 1), y, z), 203);
                                        PhysAirFlood(PosToInt((ushort)(x - 1), y, z), 203);
                                        PhysAirFlood(PosToInt(x, y, (ushort)(z + 1)), 203);
                                        PhysAirFlood(PosToInt(x, y, (ushort)(z - 1)), 203);
                                        PhysAirFlood(PosToInt(x, (ushort)(y - 1), z), 203);
                                        C.time++;
                                    }
                                    else
                                    {
                                        AddUpdate(C.b, 0);
                                        C.time = byte.MaxValue;
                                    }
                                    break;
                                case 204:
                                    if (C.time < 1)
                                    {
                                        PhysAirFlood(PosToInt((ushort)(x + 1), y, z), 204);
                                        PhysAirFlood(PosToInt((ushort)(x - 1), y, z), 204);
                                        PhysAirFlood(PosToInt(x, y, (ushort)(z + 1)), 204);
                                        PhysAirFlood(PosToInt(x, y, (ushort)(z - 1)), 204);
                                        PhysAirFlood(PosToInt(x, (ushort)(y + 1), z), 204);
                                        C.time++;
                                    }
                                    else
                                    {
                                        AddUpdate(C.b, 0);
                                        C.time = byte.MaxValue;
                                    }
                                    break;
                                case 77:
                                    DrawSmogBomb(x, y, z, 3);
                                    C.time = byte.MaxValue;
                                    break;
                                case 182:
                                    if (physics < 3)
                                    {
                                        Blockchange(x, y, z, 0);
                                    }
                                    if (physics == 3)
                                    {
                                        rand = new Random();
                                        if (C.time >= 5 || physics != 3)
                                        {
                                            try
                                            {
                                                MakeExplosion(x, y, z, 0);
                                                break;
                                            }
                                            catch (Exception ex11)
                                            {
                                                Server.s.Log("EXPLOSION");
                                                Server.ErrorLog(ex11);
                                                break;
                                            }
                                        }
                                        C.time++;
                                        if (GetTile(x, (ushort)(y + 1), z) == 11)
                                        {
                                            Blockchange(x, (ushort)(y + 1), z, 0);
                                        }
                                        else
                                        {
                                            Blockchange(x, (ushort)(y + 1), z, 11);
                                        }
                                    }
                                    else
                                    {
                                        Blockchange(x, y, z, 0);
                                    }
                                    break;
                                case 183:
                                    if (physics < 3)
                                    {
                                        Blockchange(x, y, z, 0);
                                    }
                                    if (physics == 3)
                                    {
                                        rand = new Random();
                                        if (C.time < 5 && physics == 3)
                                        {
                                            C.time++;
                                            if (GetTile(x, (ushort)(y + 1), z) == 11)
                                            {
                                                Blockchange(x, (ushort)(y + 1), z, 0);
                                            }
                                            else
                                            {
                                                Blockchange(x, (ushort)(y + 1), z, 11);
                                            }
                                            if (GetTile(x, (ushort)(y - 1), z) == 11)
                                            {
                                                Blockchange(x, (ushort)(y - 1), z, 0);
                                            }
                                            else
                                            {
                                                Blockchange(x, (ushort)(y - 1), z, 11);
                                            }
                                            if (GetTile((ushort)(x + 1), y, z) == 11)
                                            {
                                                Blockchange((ushort)(x + 1), y, z, 0);
                                            }
                                            else
                                            {
                                                Blockchange((ushort)(x + 1), y, z, 11);
                                            }
                                            if (GetTile((ushort)(x - 1), y, z) == 11)
                                            {
                                                Blockchange((ushort)(x - 1), y, z, 0);
                                            }
                                            else
                                            {
                                                Blockchange((ushort)(x - 1), y, z, 11);
                                            }
                                            if (GetTile(x, y, (ushort)(z + 1)) == 11)
                                            {
                                                Blockchange(x, y, (ushort)(z + 1), 0);
                                            }
                                            else
                                            {
                                                Blockchange(x, y, (ushort)(z + 1), 11);
                                            }
                                            if (GetTile(x, y, (ushort)(z - 1)) == 11)
                                            {
                                                Blockchange(x, y, (ushort)(z - 1), 0);
                                            }
                                            else
                                            {
                                                Blockchange(x, y, (ushort)(z - 1), 11);
                                            }
                                        }
                                        else
                                        {
                                            MakeExplosion(x, y, z, 1);
                                        }
                                    }
                                    else
                                    {
                                        Blockchange(x, y, z, 0);
                                    }
                                    break;
                                case 184:
                                    if (rand.Next(1, 11) <= 7)
                                    {
                                        AddUpdate(C.b, 0);
                                    }
                                    break;
                                case 76:
                                    C.time++;
                                    if (C.time > 200)
                                    {
                                        AddUpdate(C.b, 0);
                                        C.time = byte.MaxValue;
                                    }
                                    else if (C.time > 10 && rand.Next(1, 11) <= 7)
                                    {
                                        AddUpdate(C.b, 0);
                                        C.time = byte.MaxValue;
                                    }
                                    break;
                                case 230:
                                {
                                    if (rand.Next(1, 10) <= 5)
                                    {
                                        mx = 1;
                                    }
                                    else
                                    {
                                        mx = -1;
                                    }
                                    if (rand.Next(1, 10) <= 5)
                                    {
                                        my = 1;
                                    }
                                    else
                                    {
                                        my = -1;
                                    }
                                    if (rand.Next(1, 10) <= 5)
                                    {
                                        mz = 1;
                                    }
                                    else
                                    {
                                        mz = -1;
                                    }
                                    for (int m = -1 * mx; m != mx + mx; m += mx)
                                    {
                                        for (int n = -1 * my; n != my + my; n += my)
                                        {
                                            for (int num10 = -1 * mz; num10 != mz + mz; num10 += mz)
                                            {
                                                if (GetTile((ushort)(x + m), (ushort)(y + n - 1), (ushort)(z + num10)) == 21 &&
                                                    (GetTile((ushort)(x + m), (ushort)(y + n), (ushort)(z + num10)) == 0 ||
                                                        GetTile((ushort)(x + m), (ushort)(y + n), (ushort)(z + num10)) == 8) && !flag)
                                                {
                                                    AddUpdate(PosToInt((ushort)(x + m), (ushort)(y + n), (ushort)(z + num10)), 230);
                                                    AddUpdate(PosToInt(x, y, z), 0);
                                                    AddUpdate(IntOffset(C.b, 0, -1, 0), 49, true, "wait 5 revert " + (byte)21);
                                                    flag = true;
                                                    break;
                                                }
                                                if (GetTile((ushort)(x + m), (ushort)(y + n - 1), (ushort)(z + num10)) == 105 &&
                                                    (GetTile((ushort)(x + m), (ushort)(y + n), (ushort)(z + num10)) == 0 ||
                                                        GetTile((ushort)(x + m), (ushort)(y + n), (ushort)(z + num10)) == 8) && !flag)
                                                {
                                                    AddUpdate(PosToInt((ushort)(x + m), (ushort)(y + n), (ushort)(z + num10)), 230);
                                                    AddUpdate(PosToInt(x, y, z), 0);
                                                    AddUpdate(IntOffset(C.b, 0, -1, 0), 20, true, "wait 5 revert " + (byte)105);
                                                    flag = true;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    break;
                                }
                                case 195:
                                    if (!PhysSpongeCheck(C.b))
                                    {
                                        C.time++;
                                        if (C.time >= 3)
                                        {
                                            if (GetTile(x, (ushort)(y - 1), z) == 0)
                                            {
                                                AddUpdate(PosToInt(x, (ushort)(y - 1), z), 195);
                                            }
                                            else if (GetTile(x, (ushort)(y - 1), z) != 195)
                                            {
                                                PhysLava(PosToInt((ushort)(x + 1), y, z), blocks[C.b]);
                                                PhysLava(PosToInt((ushort)(x - 1), y, z), blocks[C.b]);
                                                PhysLava(PosToInt(x, y, (ushort)(z + 1)), blocks[C.b]);
                                                PhysLava(PosToInt(x, y, (ushort)(z - 1)), blocks[C.b]);
                                                C.time = byte.MaxValue;
                                            }
                                            else
                                            {
                                                C.time = byte.MaxValue;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        C.time = byte.MaxValue;
                                    }
                                    break;
                                case 196:
                                    C.time++;
                                    if (GetTile(x, (ushort)(y - 1), z) == 0)
                                    {
                                        AddUpdate(PosToInt(x, (ushort)(y - 1), z), 196);
                                    }
                                    else if (GetTile(x, (ushort)(y - 1), z) != 196)
                                    {
                                        PhysWater(PosToInt((ushort)(x + 1), y, z), blocks[C.b]);
                                        PhysWater(PosToInt((ushort)(x - 1), y, z), blocks[C.b]);
                                        PhysWater(PosToInt(x, y, (ushort)(z + 1)), blocks[C.b]);
                                        PhysWater(PosToInt(x, y, (ushort)(z - 1)), blocks[C.b]);
                                    }
                                    if (physics > 1 && C.time > 10)
                                    {
                                        C.time = 0;
                                        if (Block.WaterKill(GetTile((ushort)(x + 1), y, z)))
                                        {
                                            AddUpdate(PosToInt((ushort)(x + 1), y, z), 196);
                                            flag = true;
                                        }
                                        if (Block.WaterKill(GetTile((ushort)(x - 1), y, z)))
                                        {
                                            AddUpdate(PosToInt((ushort)(x - 1), y, z), 196);
                                            flag = true;
                                        }
                                        if (Block.WaterKill(GetTile(x, y, (ushort)(z + 1))))
                                        {
                                            AddUpdate(PosToInt(x, y, (ushort)(z + 1)), 196);
                                            flag = true;
                                        }
                                        if (Block.WaterKill(GetTile(x, y, (ushort)(z - 1))))
                                        {
                                            AddUpdate(PosToInt(x, y, (ushort)(z - 1)), 196);
                                            flag = true;
                                        }
                                        if (Block.WaterKill(GetTile(x, (ushort)(y - 1), z)))
                                        {
                                            AddUpdate(PosToInt(x, (ushort)(y - 1), z), 196);
                                            flag = true;
                                        }
                                        if (flag && Block.WaterKill(GetTile(x, (ushort)(y + 1), z)))
                                        {
                                            AddUpdate(PosToInt(x, (ushort)(y + 1), z), 196);
                                        }
                                    }
                                    break;
                                case 235:
                                case 236:
                                case 237:
                                case 238:
                                    switch (rand.Next(1, 15))
                                    {
                                        case 1:
                                            if (GetTile(x, (ushort)(y - 1), z) == 0)
                                            {
                                                AddUpdate(PosToInt(x, (ushort)(y - 1), z), blocks[C.b]);
                                                break;
                                            }
                                            goto case 3;
                                        case 2:
                                            if (GetTile(x, (ushort)(y + 1), z) == 0)
                                            {
                                                AddUpdate(PosToInt(x, (ushort)(y + 1), z), blocks[C.b]);
                                                break;
                                            }
                                            goto case 6;
                                        case 3:
                                        case 4:
                                        case 5:
                                            if (GetTile((ushort)(x - 1), y, z) == 0)
                                            {
                                                AddUpdate(PosToInt((ushort)(x - 1), y, z), blocks[C.b]);
                                            }
                                            else if (GetTile((ushort)(x - 1), y, z) != 105)
                                            {
                                                AddUpdate(C.b, 21, false, "dissipate 25");
                                            }
                                            break;
                                        case 6:
                                        case 7:
                                        case 8:
                                            if (GetTile((ushort)(x + 1), y, z) == 0)
                                            {
                                                AddUpdate(PosToInt((ushort)(x + 1), y, z), blocks[C.b]);
                                            }
                                            else if (GetTile((ushort)(x + 1), y, z) != 105)
                                            {
                                                AddUpdate(C.b, 21, false, "dissipate 25");
                                            }
                                            break;
                                        case 9:
                                        case 10:
                                        case 11:
                                            if (GetTile(x, y, (ushort)(z - 1)) == 0)
                                            {
                                                AddUpdate(PosToInt(x, y, (ushort)(z - 1)), blocks[C.b]);
                                            }
                                            else if (GetTile(x, y, (ushort)(z - 1)) != 105)
                                            {
                                                AddUpdate(C.b, 21, false, "dissipate 25");
                                            }
                                            break;
                                        default:
                                            if (GetTile(x, y, (ushort)(z + 1)) == 0)
                                            {
                                                AddUpdate(PosToInt(x, y, (ushort)(z + 1)), blocks[C.b]);
                                            }
                                            else if (GetTile(x, y, (ushort)(z + 1)) != 105)
                                            {
                                                AddUpdate(C.b, 21, false, "dissipate 25");
                                            }
                                            break;
                                    }
                                    AddUpdate(C.b, 0);
                                    C.time = byte.MaxValue;
                                    break;
                                case 252:
                                    if (GetTile(IntOffset(C.b, -1, 0, 0)) != 251 || GetTile(IntOffset(C.b, 1, 0, 0)) != 251 || GetTile(IntOffset(C.b, 0, 0, 1)) != 251 ||
                                        GetTile(IntOffset(C.b, 0, 0, -1)) != 251)
                                    {
                                        C.extraInfo = "revert 0";
                                    }
                                    break;
                                case 251:
                                    if (ai)
                                    {
                                        Player.players.ForEach(delegate(Player p)
                                        {
                                            if (p.level == this && !p.invincible)
                                            {
                                                currentNum = Math.Abs(p.pos[0] / 32 - x) + Math.Abs(p.pos[1] / 32 - y) + Math.Abs(p.pos[2] / 32 - z);
                                                if (currentNum < foundNum)
                                                {
                                                    foundNum = currentNum;
                                                    foundPlayer = p;
                                                }
                                            }
                                        });
                                    }
                                    while (true)
                                    {
                                        if (foundPlayer != null && rand.Next(1, 20) < 19)
                                        {
                                            currentNum = rand.Next(1, 10);
                                            foundNum = 0;
                                            switch (currentNum)
                                            {
                                                case 1:
                                                case 2:
                                                case 3:
                                                    if (foundPlayer.pos[0] / 32 - x != 0)
                                                    {
                                                        int num9 = PosToInt((ushort)(x + Math.Sign(foundPlayer.pos[0] / 32 - x)), y, z);
                                                        if (GetTile(num9) == 0 && (IntOffset(num9, -1, 0, 0) == 2 || IntOffset(num9, -1, 0, 0) == 3) &&
                                                            AddUpdate(num9, blocks[C.b]))
                                                        {
                                                            break;
                                                        }
                                                    }
                                                    foundNum++;
                                                    if (foundNum >= 3)
                                                    {
                                                        goto IL_60e2;
                                                    }
                                                    goto case 4;
                                                case 4:
                                                case 5:
                                                case 6:
                                                    if (foundPlayer.pos[1] / 32 - y != 0)
                                                    {
                                                        int num9 = PosToInt(x, (ushort)(y + Math.Sign(foundPlayer.pos[1] / 32 - y)), z);
                                                        if (GetTile(num9) == 0)
                                                        {
                                                            if (num9 > 0)
                                                            {
                                                                if ((IntOffset(num9, 0, 1, 0) == 2 || IntOffset(num9, 0, 1, 0) == 3 && IntOffset(num9, 0, 2, 0) == 0) &&
                                                                    AddUpdate(num9, blocks[C.b]))
                                                                {
                                                                    break;
                                                                }
                                                            }
                                                            else if (num9 < 0 && (IntOffset(num9, 0, -2, 0) == 2 ||
                                                                         IntOffset(num9, 0, -2, 0) == 3 && IntOffset(num9, 0, -1, 0) == 0) && AddUpdate(num9, blocks[C.b]))
                                                            {
                                                                break;
                                                            }
                                                        }
                                                    }
                                                    foundNum++;
                                                    if (foundNum >= 3)
                                                    {
                                                        goto IL_60e2;
                                                    }
                                                    goto case 7;
                                                case 7:
                                                case 8:
                                                case 9:
                                                    if (foundPlayer.pos[2] / 32 - z != 0)
                                                    {
                                                        int num9 = PosToInt(x, y, (ushort)(z + Math.Sign(foundPlayer.pos[2] / 32 - z)));
                                                        if (GetTile(num9) == 0 && (IntOffset(num9, 0, 0, -1) == 2 || IntOffset(num9, 0, 0, -1) == 3) &&
                                                            AddUpdate(num9, blocks[C.b]))
                                                        {
                                                            break;
                                                        }
                                                    }
                                                    foundNum++;
                                                    if (foundNum >= 3)
                                                    {
                                                        goto IL_60e2;
                                                    }
                                                    goto case 1;
                                                default:
                                                    goto IL_60e2;
                                            }
                                        }
                                        else
                                        {
                                            switch (rand.Next(1, 13))
                                            {
                                                case 1:
                                                case 2:
                                                case 3:
                                                {
                                                    int num9 = IntOffset(C.b, -1, 0, 0);
                                                    int pos = PosToInt(x, y, z);
                                                    if (GetTile(IntOffset(num9, 0, -1, 0)) == 0 && GetTile(num9) == 0)
                                                    {
                                                        num9 = IntOffset(num9, 0, -1, 0);
                                                    }
                                                    else if (GetTile(num9) != 0 || GetTile(IntOffset(num9, 0, 1, 0)) != 0)
                                                    {
                                                        if (GetTile(IntOffset(num9, 0, 2, 0)) == 0 && GetTile(IntOffset(num9, 0, 1, 0)) == 0)
                                                        {
                                                            num9 = IntOffset(num9, 0, 1, 0);
                                                        }
                                                        else
                                                        {
                                                            flag2 = true;
                                                        }
                                                    }
                                                    if (AddUpdate(num9, blocks[C.b]))
                                                    {
                                                        AddUpdate(IntOffset(pos, 0, 0, 0), 252, true, "wait 5 revert " + (byte)0);
                                                        break;
                                                    }
                                                    foundNum++;
                                                    if (foundNum >= 4)
                                                    {
                                                        flag = true;
                                                        break;
                                                    }
                                                    goto case 4;
                                                }
                                                case 4:
                                                case 5:
                                                case 6:
                                                {
                                                    int num9 = IntOffset(C.b, 1, 0, 0);
                                                    int pos = PosToInt(x, y, z);
                                                    if (GetTile(IntOffset(num9, 0, -1, 0)) == 0 && GetTile(num9) == 0)
                                                    {
                                                        num9 = IntOffset(num9, 0, -1, 0);
                                                    }
                                                    else if (GetTile(num9) != 0 || GetTile(IntOffset(num9, 0, 1, 0)) != 0)
                                                    {
                                                        if (GetTile(IntOffset(num9, 0, 2, 0)) == 0 && GetTile(IntOffset(num9, 0, 1, 0)) == 0)
                                                        {
                                                            num9 = IntOffset(num9, 0, 1, 0);
                                                        }
                                                        else
                                                        {
                                                            flag2 = true;
                                                        }
                                                    }
                                                    if (AddUpdate(num9, blocks[C.b]))
                                                    {
                                                        AddUpdate(IntOffset(pos, 0, 0, 0), 252, true, "wait 5 revert " + (byte)0);
                                                        break;
                                                    }
                                                    foundNum++;
                                                    if (foundNum >= 4)
                                                    {
                                                        flag = true;
                                                        break;
                                                    }
                                                    goto case 7;
                                                }
                                                case 7:
                                                case 8:
                                                case 9:
                                                {
                                                    int num9 = IntOffset(C.b, 0, 0, 1);
                                                    int pos = PosToInt(x, y, z);
                                                    if (GetTile(IntOffset(num9, 0, -1, 0)) == 0 && GetTile(num9) == 0)
                                                    {
                                                        num9 = IntOffset(num9, 0, -1, 0);
                                                    }
                                                    else if (GetTile(num9) != 0 || GetTile(IntOffset(num9, 0, 1, 0)) != 0)
                                                    {
                                                        if (GetTile(IntOffset(num9, 0, 2, 0)) == 0 && GetTile(IntOffset(num9, 0, 1, 0)) == 0)
                                                        {
                                                            num9 = IntOffset(num9, 0, 1, 0);
                                                        }
                                                        else
                                                        {
                                                            flag2 = true;
                                                        }
                                                    }
                                                    if (AddUpdate(num9, blocks[C.b]))
                                                    {
                                                        AddUpdate(IntOffset(pos, 0, 0, 0), 252, true, "wait 5 revert " + (byte)0);
                                                        break;
                                                    }
                                                    foundNum++;
                                                    if (foundNum >= 4)
                                                    {
                                                        flag = true;
                                                        break;
                                                    }
                                                    goto default;
                                                }
                                                default:
                                                {
                                                    int num9 = IntOffset(C.b, 0, 0, -1);
                                                    int pos = PosToInt(x, y, z);
                                                    if (GetTile(IntOffset(num9, 0, -1, 0)) == 0 && GetTile(num9) == 0)
                                                    {
                                                        num9 = IntOffset(num9, 0, -1, 0);
                                                    }
                                                    else if (GetTile(num9) != 0 || GetTile(IntOffset(num9, 0, 1, 0)) != 0)
                                                    {
                                                        if (GetTile(IntOffset(num9, 0, 2, 0)) == 0 && GetTile(IntOffset(num9, 0, 1, 0)) == 0)
                                                        {
                                                            num9 = IntOffset(num9, 0, 1, 0);
                                                        }
                                                        else
                                                        {
                                                            flag2 = true;
                                                        }
                                                    }
                                                    if (AddUpdate(num9, blocks[C.b]))
                                                    {
                                                        AddUpdate(IntOffset(pos, 0, 0, 0), 252, true, "wait 5 revert " + (byte)0);
                                                        break;
                                                    }
                                                    foundNum++;
                                                    if (foundNum >= 4)
                                                    {
                                                        flag = true;
                                                        break;
                                                    }
                                                    goto case 1;
                                                }
                                            }
                                        }
                                        break;
                                        IL_60e2:
                                        foundPlayer = null;
                                    }
                                    if (!flag)
                                    {
                                        AddUpdate(C.b, 0);
                                    }
                                    break;
                                case 239:
                                case 240:
                                case 242:
                                    if (ai)
                                    {
                                        Player.players.ForEach(delegate(Player p)
                                        {
                                            if (p.level == this && !p.invincible)
                                            {
                                                currentNum = Math.Abs(p.pos[0] / 32 - x) + Math.Abs(p.pos[1] / 32 - y) + Math.Abs(p.pos[2] / 32 - z);
                                                if (currentNum < foundNum)
                                                {
                                                    foundNum = currentNum;
                                                    foundPlayer = p;
                                                }
                                            }
                                        });
                                    }
                                    while (true)
                                    {
                                        if (foundPlayer != null && rand.Next(1, 20) < 19)
                                        {
                                            currentNum = rand.Next(1, 10);
                                            foundNum = 0;
                                            switch (currentNum)
                                            {
                                                case 1:
                                                case 2:
                                                case 3:
                                                    if (foundPlayer.pos[0] / 32 - x != 0)
                                                    {
                                                        int num9 = PosToInt((ushort)(x + Math.Sign(foundPlayer.pos[0] / 32 - x)), y, z);
                                                        if (GetTile(num9) == 0 && AddUpdate(num9, blocks[C.b]))
                                                        {
                                                            break;
                                                        }
                                                    }
                                                    foundNum++;
                                                    if (foundNum >= 3)
                                                    {
                                                        goto IL_69a7;
                                                    }
                                                    goto case 4;
                                                case 4:
                                                case 5:
                                                case 6:
                                                    if (foundPlayer.pos[1] / 32 - y != 0)
                                                    {
                                                        int num9 = PosToInt(x, (ushort)(y + Math.Sign(foundPlayer.pos[1] / 32 - y)), z);
                                                        if (GetTile(num9) == 0 && AddUpdate(num9, blocks[C.b]))
                                                        {
                                                            break;
                                                        }
                                                    }
                                                    foundNum++;
                                                    if (foundNum >= 3)
                                                    {
                                                        goto IL_69a7;
                                                    }
                                                    goto case 7;
                                                case 7:
                                                case 8:
                                                case 9:
                                                    if (foundPlayer.pos[2] / 32 - z != 0)
                                                    {
                                                        int num9 = PosToInt(x, y, (ushort)(z + Math.Sign(foundPlayer.pos[2] / 32 - z)));
                                                        if (GetTile(num9) == 0 && AddUpdate(num9, blocks[C.b]))
                                                        {
                                                            break;
                                                        }
                                                    }
                                                    foundNum++;
                                                    if (foundNum >= 3)
                                                    {
                                                        goto IL_69a7;
                                                    }
                                                    goto case 1;
                                                default:
                                                    goto IL_69a7;
                                            }
                                        }
                                        else
                                        {
                                            switch (rand.Next(1, 15))
                                            {
                                                case 1:
                                                    if (GetTile(x, (ushort)(y - 1), z) == 0 && AddUpdate(PosToInt(x, (ushort)(y - 1), z), blocks[C.b]))
                                                    {
                                                        break;
                                                    }
                                                    goto case 3;
                                                case 2:
                                                    if (GetTile(x, (ushort)(y + 1), z) == 0 && AddUpdate(PosToInt(x, (ushort)(y + 1), z), blocks[C.b]))
                                                    {
                                                        break;
                                                    }
                                                    goto case 6;
                                                case 3:
                                                case 4:
                                                case 5:
                                                    if (GetTile((ushort)(x - 1), y, z) == 0 && AddUpdate(PosToInt((ushort)(x - 1), y, z), blocks[C.b]))
                                                    {
                                                        break;
                                                    }
                                                    goto case 9;
                                                case 6:
                                                case 7:
                                                case 8:
                                                    if (GetTile((ushort)(x + 1), y, z) == 0 && AddUpdate(PosToInt((ushort)(x + 1), y, z), blocks[C.b]))
                                                    {
                                                        break;
                                                    }
                                                    goto default;
                                                case 9:
                                                case 10:
                                                case 11:
                                                    if (GetTile(x, y, (ushort)(z - 1)) == 0)
                                                    {
                                                        if (!AddUpdate(PosToInt(x, y, (ushort)(z - 1)), blocks[C.b]))
                                                        {
                                                            flag = true;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        flag = true;
                                                    }
                                                    break;
                                                default:
                                                    if (GetTile(x, y, (ushort)(z + 1)) == 0)
                                                    {
                                                        if (!AddUpdate(PosToInt(x, y, (ushort)(z + 1)), blocks[C.b]))
                                                        {
                                                            flag = true;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        flag = true;
                                                    }
                                                    break;
                                            }
                                        }
                                        break;
                                        IL_69a7:
                                        foundPlayer = null;
                                    }
                                    if (!flag)
                                    {
                                        AddUpdate(C.b, 0);
                                    }
                                    break;
                                case 245:
                                case 246:
                                case 247:
                                case 248:
                                case 249:
                                    if (ai)
                                    {
                                        Player.players.ForEach(delegate(Player p)
                                        {
                                            if (p.level == this && !p.invincible)
                                            {
                                                currentNum = Math.Abs(p.pos[0] / 32 - x) + Math.Abs(p.pos[1] / 32 - y) + Math.Abs(p.pos[2] / 32 - z);
                                                if (currentNum < foundNum)
                                                {
                                                    foundNum = currentNum;
                                                    foundPlayer = p;
                                                }
                                            }
                                        });
                                    }
                                    while (true)
                                    {
                                        if (foundPlayer != null && rand.Next(1, 20) < 19)
                                        {
                                            currentNum = rand.Next(1, 10);
                                            foundNum = 0;
                                            switch (currentNum)
                                            {
                                                case 1:
                                                case 2:
                                                case 3:
                                                    if (foundPlayer.pos[0] / 32 - x != 0)
                                                    {
                                                        int num9 = blocks[C.b] != 249 && blocks[C.b] != 247
                                                            ? PosToInt((ushort)(x - Math.Sign(foundPlayer.pos[0] / 32 - x)), y, z)
                                                            : PosToInt((ushort)(x + Math.Sign(foundPlayer.pos[0] / 32 - x)), y, z);
                                                        if (GetTile(num9) == 8 && AddUpdate(num9, blocks[C.b]))
                                                        {
                                                            break;
                                                        }
                                                    }
                                                    foundNum++;
                                                    if (foundNum >= 3)
                                                    {
                                                        goto IL_70da;
                                                    }
                                                    goto case 4;
                                                case 4:
                                                case 5:
                                                case 6:
                                                    if (foundPlayer.pos[1] / 32 - y != 0)
                                                    {
                                                        int num9 = blocks[C.b] != 249 && blocks[C.b] != 247
                                                            ? PosToInt(x, (ushort)(y - Math.Sign(foundPlayer.pos[1] / 32 - y)), z)
                                                            : PosToInt(x, (ushort)(y + Math.Sign(foundPlayer.pos[1] / 32 - y)), z);
                                                        if (GetTile(num9) == 8 && AddUpdate(num9, blocks[C.b]))
                                                        {
                                                            break;
                                                        }
                                                    }
                                                    foundNum++;
                                                    if (foundNum >= 3)
                                                    {
                                                        goto IL_70da;
                                                    }
                                                    goto case 7;
                                                case 7:
                                                case 8:
                                                case 9:
                                                    if (foundPlayer.pos[2] / 32 - z != 0)
                                                    {
                                                        int num9 = blocks[C.b] != 249 && blocks[C.b] != 247
                                                            ? PosToInt(x, y, (ushort)(z - Math.Sign(foundPlayer.pos[2] / 32 - z)))
                                                            : PosToInt(x, y, (ushort)(z + Math.Sign(foundPlayer.pos[2] / 32 - z)));
                                                        if (GetTile(num9) == 8 && AddUpdate(num9, blocks[C.b]))
                                                        {
                                                            break;
                                                        }
                                                    }
                                                    foundNum++;
                                                    if (foundNum >= 3)
                                                    {
                                                        goto IL_70da;
                                                    }
                                                    goto case 1;
                                                default:
                                                    goto IL_70da;
                                            }
                                        }
                                        else
                                        {
                                            switch (rand.Next(1, 15))
                                            {
                                                case 1:
                                                    if (GetTile(x, (ushort)(y - 1), z) == 8 && AddUpdate(PosToInt(x, (ushort)(y - 1), z), blocks[C.b]))
                                                    {
                                                        break;
                                                    }
                                                    goto case 3;
                                                case 2:
                                                    if (GetTile(x, (ushort)(y + 1), z) == 8 && AddUpdate(PosToInt(x, (ushort)(y + 1), z), blocks[C.b]))
                                                    {
                                                        break;
                                                    }
                                                    goto case 6;
                                                case 3:
                                                case 4:
                                                case 5:
                                                    if (GetTile((ushort)(x - 1), y, z) == 8 && AddUpdate(PosToInt((ushort)(x - 1), y, z), blocks[C.b]))
                                                    {
                                                        break;
                                                    }
                                                    goto case 9;
                                                case 6:
                                                case 7:
                                                case 8:
                                                    if (GetTile((ushort)(x + 1), y, z) == 8 && AddUpdate(PosToInt((ushort)(x + 1), y, z), blocks[C.b]))
                                                    {
                                                        break;
                                                    }
                                                    goto default;
                                                case 9:
                                                case 10:
                                                case 11:
                                                    if (GetTile(x, y, (ushort)(z - 1)) == 8)
                                                    {
                                                        if (!AddUpdate(PosToInt(x, y, (ushort)(z - 1)), blocks[C.b]))
                                                        {
                                                            flag = true;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        flag = true;
                                                    }
                                                    break;
                                                default:
                                                    if (GetTile(x, y, (ushort)(z + 1)) == 8)
                                                    {
                                                        if (!AddUpdate(PosToInt(x, y, (ushort)(z + 1)), blocks[C.b]))
                                                        {
                                                            flag = true;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        flag = true;
                                                    }
                                                    break;
                                            }
                                        }
                                        break;
                                        IL_70da:
                                        foundPlayer = null;
                                    }
                                    if (!flag)
                                    {
                                        AddUpdate(C.b, 8);
                                    }
                                    break;
                                case 250:
                                    if (ai)
                                    {
                                        Player.players.ForEach(delegate(Player p)
                                        {
                                            if (p.level == this && !p.invincible)
                                            {
                                                currentNum = Math.Abs(p.pos[0] / 32 - x) + Math.Abs(p.pos[1] / 32 - y) + Math.Abs(p.pos[2] / 32 - z);
                                                if (currentNum < foundNum)
                                                {
                                                    foundNum = currentNum;
                                                    foundPlayer = p;
                                                }
                                            }
                                        });
                                    }
                                    while (true)
                                    {
                                        if (foundPlayer != null && rand.Next(1, 20) < 19)
                                        {
                                            currentNum = rand.Next(1, 10);
                                            foundNum = 0;
                                            switch (currentNum)
                                            {
                                                case 1:
                                                case 2:
                                                case 3:
                                                    if (foundPlayer.pos[0] / 32 - x != 0)
                                                    {
                                                        int num9 = blocks[C.b] != 250 ? PosToInt((ushort)(x - Math.Sign(foundPlayer.pos[0] / 32 - x)), y, z)
                                                            : PosToInt((ushort)(x + Math.Sign(foundPlayer.pos[0] / 32 - x)), y, z);
                                                        if (GetTile(num9) == 10 && AddUpdate(num9, blocks[C.b]))
                                                        {
                                                            break;
                                                        }
                                                    }
                                                    foundNum++;
                                                    if (foundNum >= 3)
                                                    {
                                                        goto IL_77cb;
                                                    }
                                                    goto case 4;
                                                case 4:
                                                case 5:
                                                case 6:
                                                    if (foundPlayer.pos[1] / 32 - y != 0)
                                                    {
                                                        int num9 = blocks[C.b] != 250 ? PosToInt(x, (ushort)(y - Math.Sign(foundPlayer.pos[1] / 32 - y)), z)
                                                            : PosToInt(x, (ushort)(y + Math.Sign(foundPlayer.pos[1] / 32 - y)), z);
                                                        if (GetTile(num9) == 10 && AddUpdate(num9, blocks[C.b]))
                                                        {
                                                            break;
                                                        }
                                                    }
                                                    foundNum++;
                                                    if (foundNum >= 3)
                                                    {
                                                        goto IL_77cb;
                                                    }
                                                    goto case 7;
                                                case 7:
                                                case 8:
                                                case 9:
                                                    if (foundPlayer.pos[2] / 32 - z != 0)
                                                    {
                                                        int num9 = blocks[C.b] != 250 ? PosToInt(x, y, (ushort)(z - Math.Sign(foundPlayer.pos[2] / 32 - z)))
                                                            : PosToInt(x, y, (ushort)(z + Math.Sign(foundPlayer.pos[2] / 32 - z)));
                                                        if (GetTile(num9) == 10 && AddUpdate(num9, blocks[C.b]))
                                                        {
                                                            break;
                                                        }
                                                    }
                                                    foundNum++;
                                                    if (foundNum >= 3)
                                                    {
                                                        goto IL_77cb;
                                                    }
                                                    goto case 1;
                                                default:
                                                    goto IL_77cb;
                                            }
                                        }
                                        else
                                        {
                                            switch (rand.Next(1, 15))
                                            {
                                                case 1:
                                                    if (GetTile(x, (ushort)(y - 1), z) == 10 && AddUpdate(PosToInt(x, (ushort)(y - 1), z), blocks[C.b]))
                                                    {
                                                        break;
                                                    }
                                                    goto case 3;
                                                case 2:
                                                    if (GetTile(x, (ushort)(y + 1), z) == 10 && AddUpdate(PosToInt(x, (ushort)(y + 1), z), blocks[C.b]))
                                                    {
                                                        break;
                                                    }
                                                    goto case 6;
                                                case 3:
                                                case 4:
                                                case 5:
                                                    if (GetTile((ushort)(x - 1), y, z) == 10 && AddUpdate(PosToInt((ushort)(x - 1), y, z), blocks[C.b]))
                                                    {
                                                        break;
                                                    }
                                                    goto case 9;
                                                case 6:
                                                case 7:
                                                case 8:
                                                    if (GetTile((ushort)(x + 1), y, z) == 10 && AddUpdate(PosToInt((ushort)(x + 1), y, z), blocks[C.b]))
                                                    {
                                                        break;
                                                    }
                                                    goto default;
                                                case 9:
                                                case 10:
                                                case 11:
                                                    if (GetTile(x, y, (ushort)(z - 1)) == 10)
                                                    {
                                                        if (!AddUpdate(PosToInt(x, y, (ushort)(z - 1)), blocks[C.b]))
                                                        {
                                                            flag = true;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        flag = true;
                                                    }
                                                    break;
                                                default:
                                                    if (GetTile(x, y, (ushort)(z + 1)) == 10)
                                                    {
                                                        if (!AddUpdate(PosToInt(x, y, (ushort)(z + 1)), blocks[C.b]))
                                                        {
                                                            flag = true;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        flag = true;
                                                    }
                                                    break;
                                            }
                                        }
                                        break;
                                        IL_77cb:
                                        foundPlayer = null;
                                    }
                                    if (!flag)
                                    {
                                        AddUpdate(C.b, 10);
                                    }
                                    break;
                                case 188:
                                {
                                    if (rand.Next(1, 10) <= 5)
                                    {
                                        mx = 1;
                                    }
                                    else
                                    {
                                        mx = -1;
                                    }
                                    if (rand.Next(1, 10) <= 5)
                                    {
                                        my = 1;
                                    }
                                    else
                                    {
                                        my = -1;
                                    }
                                    if (rand.Next(1, 10) <= 5)
                                    {
                                        mz = 1;
                                    }
                                    else
                                    {
                                        mz = -1;
                                    }
                                    for (int j = -1 * mx; j != mx + mx; j += mx)
                                    {
                                        if (flag)
                                        {
                                            break;
                                        }
                                        for (int k = -1 * my; k != my + my; k += my)
                                        {
                                            if (flag)
                                            {
                                                break;
                                            }
                                            for (int l = -1 * mz; l != mz + mz; l += mz)
                                            {
                                                if (flag)
                                                {
                                                    break;
                                                }
                                                if (GetTile((ushort)(x + j), (ushort)(y + k), (ushort)(z + l)) == 185)
                                                {
                                                    if (GetTile((ushort)(x - j), (ushort)(y - k), (ushort)(z - l)) == 0 ||
                                                        GetTile((ushort)(x - j), (ushort)(y - k), (ushort)(z - l)) == 187)
                                                    {
                                                        AddUpdate(PosToInt((ushort)(x - j), (ushort)(y - k), (ushort)(z - l)), 188);
                                                        AddUpdate(PosToInt(x, y, z), 185);
                                                    }
                                                    else if (GetTile((ushort)(x - j), (ushort)(y - k), (ushort)(z - l)) != 185)
                                                    {
                                                        if (physics > 2)
                                                        {
                                                            MakeExplosion(x, y, z, 2);
                                                        }
                                                        else
                                                        {
                                                            AddUpdate(PosToInt(x, y, z), 185);
                                                        }
                                                    }
                                                    flag = true;
                                                }
                                            }
                                        }
                                    }
                                    break;
                                }
                                case 189:
                                    if (GetTile(x, (ushort)(y - 1), z) == 11)
                                    {
                                        if (GetTile(x, (ushort)(y + 1), z) == 0)
                                        {
                                            if (height / 100 * 80 < y)
                                            {
                                                mx = rand.Next(1, 20);
                                            }
                                            else
                                            {
                                                mx = 5;
                                            }
                                            if (mx > 1)
                                            {
                                                AddUpdate(PosToInt(x, (ushort)(y + 1), z), 189);
                                                AddUpdate(PosToInt(x, y, z), 11, false, "wait 1 dissipate 90");
                                                C.extraInfo = "wait 1 dissipate 100";
                                                break;
                                            }
                                        }
                                        Firework(x, y, z, 4);
                                    }
                                    break;
                                case 233:
                                    if (GetTile(IntOffset(C.b, 0, -1, 0)) != 232 && GetTile(IntOffset(C.b, 0, -1, 0)) != 231)
                                    {
                                        C.extraInfo = "revert 0";
                                    }
                                    break;
                                case 231:
                                case 232:
                                    if (GetTile(x, (ushort)(y - 1), z) == 0)
                                    {
                                        AddUpdate(C.b, 233);
                                        AddUpdate(IntOffset(C.b, 0, -1, 0), blocks[C.b]);
                                        AddUpdate(IntOffset(C.b, 0, 1, 0), 0);
                                    }
                                    else
                                    {
                                        if (ai)
                                        {
                                            Player.players.ForEach(delegate(Player p)
                                            {
                                                if (p.level == this && !p.invincible)
                                                {
                                                    currentNum = Math.Abs(p.pos[0] / 32 - x) + Math.Abs(p.pos[1] / 32 - y) + Math.Abs(p.pos[2] / 32 - z);
                                                    if (currentNum < foundNum)
                                                    {
                                                        foundNum = currentNum;
                                                        foundPlayer = p;
                                                    }
                                                }
                                            });
                                        }
                                        while (true)
                                        {
                                            if (foundPlayer != null && rand.Next(1, 20) < 18)
                                            {
                                                currentNum = rand.Next(1, 7);
                                                foundNum = 0;
                                                switch (currentNum)
                                                {
                                                    case 1:
                                                    case 2:
                                                    case 3:
                                                        if (foundPlayer.pos[0] / 32 - x != 0)
                                                        {
                                                            flag2 = false;
                                                            int num9 = PosToInt((ushort)(x + Math.Sign(foundPlayer.pos[0] / 32 - x)), y, z);
                                                            if (GetTile(IntOffset(num9, 0, -1, 0)) == 0 && GetTile(num9) == 0)
                                                            {
                                                                num9 = IntOffset(num9, 0, -1, 0);
                                                            }
                                                            else if (GetTile(num9) != 0 || GetTile(IntOffset(num9, 0, 1, 0)) != 0)
                                                            {
                                                                if (GetTile(IntOffset(num9, 0, 2, 0)) == 0 && GetTile(IntOffset(num9, 0, 1, 0)) == 0)
                                                                {
                                                                    num9 = IntOffset(num9, 0, 1, 0);
                                                                }
                                                                else
                                                                {
                                                                    flag2 = true;
                                                                }
                                                            }
                                                            if (!flag2 && AddUpdate(num9, blocks[C.b]))
                                                            {
                                                                AddUpdate(IntOffset(num9, 0, 1, 0), 233);
                                                                break;
                                                            }
                                                        }
                                                        foundNum++;
                                                        if (foundNum >= 2)
                                                        {
                                                            goto IL_835a;
                                                        }
                                                        goto case 4;
                                                    case 4:
                                                    case 5:
                                                    case 6:
                                                        if (foundPlayer.pos[2] / 32 - z != 0)
                                                        {
                                                            flag2 = false;
                                                            int num9 = PosToInt(x, y, (ushort)(z + Math.Sign(foundPlayer.pos[2] / 32 - z)));
                                                            if (GetTile(IntOffset(num9, 0, -1, 0)) == 0 && GetTile(num9) == 0)
                                                            {
                                                                num9 = IntOffset(num9, 0, -1, 0);
                                                            }
                                                            else if (GetTile(num9) != 0 || GetTile(IntOffset(num9, 0, 1, 0)) != 0)
                                                            {
                                                                if (GetTile(IntOffset(num9, 0, 2, 0)) == 0 && GetTile(IntOffset(num9, 0, 1, 0)) == 0)
                                                                {
                                                                    num9 = IntOffset(num9, 0, 1, 0);
                                                                }
                                                                else
                                                                {
                                                                    flag2 = true;
                                                                }
                                                            }
                                                            if (!flag2 && AddUpdate(num9, blocks[C.b]))
                                                            {
                                                                AddUpdate(IntOffset(num9, 0, 1, 0), 233);
                                                                break;
                                                            }
                                                        }
                                                        foundNum++;
                                                        if (foundNum >= 2)
                                                        {
                                                            goto IL_835a;
                                                        }
                                                        goto case 1;
                                                    default:
                                                        goto IL_835a;
                                                }
                                                break;
                                            }
                                            if (flag2 || C.time >= 3)
                                            {
                                                foundNum = 0;
                                                switch (rand.Next(1, 13))
                                                {
                                                    case 1:
                                                    case 2:
                                                    case 3:
                                                    {
                                                        flag2 = false;
                                                        int num9 = IntOffset(C.b, -1, 0, 0);
                                                        if (GetTile(IntOffset(num9, 0, -1, 0)) == 0 && GetTile(num9) == 0)
                                                        {
                                                            num9 = IntOffset(num9, 0, -1, 0);
                                                        }
                                                        else if (GetTile(num9) != 0 || GetTile(IntOffset(num9, 0, 1, 0)) != 0)
                                                        {
                                                            if (GetTile(IntOffset(num9, 0, 2, 0)) == 0 && GetTile(IntOffset(num9, 0, 1, 0)) == 0)
                                                            {
                                                                num9 = IntOffset(num9, 0, 1, 0);
                                                            }
                                                            else
                                                            {
                                                                flag2 = true;
                                                            }
                                                        }
                                                        if (!flag2 && AddUpdate(num9, blocks[C.b]))
                                                        {
                                                            AddUpdate(IntOffset(num9, 0, 1, 0), 233);
                                                            break;
                                                        }
                                                        foundNum++;
                                                        if (foundNum >= 4)
                                                        {
                                                            flag = true;
                                                            break;
                                                        }
                                                        goto case 4;
                                                    }
                                                    case 4:
                                                    case 5:
                                                    case 6:
                                                    {
                                                        flag2 = false;
                                                        int num9 = IntOffset(C.b, 1, 0, 0);
                                                        if (GetTile(IntOffset(num9, 0, -1, 0)) == 0 && GetTile(num9) == 0)
                                                        {
                                                            num9 = IntOffset(num9, 0, -1, 0);
                                                        }
                                                        else if (GetTile(num9) != 0 || GetTile(IntOffset(num9, 0, 1, 0)) != 0)
                                                        {
                                                            if (GetTile(IntOffset(num9, 0, 2, 0)) == 0 && GetTile(IntOffset(num9, 0, 1, 0)) == 0)
                                                            {
                                                                num9 = IntOffset(num9, 0, 1, 0);
                                                            }
                                                            else
                                                            {
                                                                flag2 = true;
                                                            }
                                                        }
                                                        if (!flag2 && AddUpdate(num9, blocks[C.b]))
                                                        {
                                                            AddUpdate(IntOffset(num9, 0, 1, 0), 233);
                                                            break;
                                                        }
                                                        foundNum++;
                                                        if (foundNum >= 4)
                                                        {
                                                            flag = true;
                                                            break;
                                                        }
                                                        goto case 7;
                                                    }
                                                    case 7:
                                                    case 8:
                                                    case 9:
                                                    {
                                                        flag2 = false;
                                                        int num9 = IntOffset(C.b, 0, 0, 1);
                                                        if (GetTile(IntOffset(num9, 0, -1, 0)) == 0 && GetTile(num9) == 0)
                                                        {
                                                            num9 = IntOffset(num9, 0, -1, 0);
                                                        }
                                                        else if (GetTile(num9) != 0 || GetTile(IntOffset(num9, 0, 1, 0)) != 0)
                                                        {
                                                            if (GetTile(IntOffset(num9, 0, 2, 0)) == 0 && GetTile(IntOffset(num9, 0, 1, 0)) == 0)
                                                            {
                                                                num9 = IntOffset(num9, 0, 1, 0);
                                                            }
                                                            else
                                                            {
                                                                flag2 = true;
                                                            }
                                                        }
                                                        if (!flag2 && AddUpdate(num9, blocks[C.b]))
                                                        {
                                                            AddUpdate(IntOffset(num9, 0, 1, 0), 233);
                                                            break;
                                                        }
                                                        foundNum++;
                                                        if (foundNum >= 4)
                                                        {
                                                            flag = true;
                                                            break;
                                                        }
                                                        goto default;
                                                    }
                                                    default:
                                                    {
                                                        flag2 = false;
                                                        int num9 = IntOffset(C.b, 0, 0, -1);
                                                        if (GetTile(IntOffset(num9, 0, -1, 0)) == 0 && GetTile(num9) == 0)
                                                        {
                                                            num9 = IntOffset(num9, 0, -1, 0);
                                                        }
                                                        else if (GetTile(num9) != 0 || GetTile(IntOffset(num9, 0, 1, 0)) != 0)
                                                        {
                                                            if (GetTile(IntOffset(num9, 0, 2, 0)) == 0 && GetTile(IntOffset(num9, 0, 1, 0)) == 0)
                                                            {
                                                                num9 = IntOffset(num9, 0, 1, 0);
                                                            }
                                                            else
                                                            {
                                                                flag2 = true;
                                                            }
                                                        }
                                                        if (!flag2 && AddUpdate(num9, blocks[C.b]))
                                                        {
                                                            AddUpdate(IntOffset(num9, 0, 1, 0), 233);
                                                            break;
                                                        }
                                                        foundNum++;
                                                        if (foundNum >= 4)
                                                        {
                                                            flag = true;
                                                            break;
                                                        }
                                                        goto case 1;
                                                    }
                                                }
                                                break;
                                            }
                                            C.time++;
                                            return;
                                            IL_835a:
                                            foundPlayer = null;
                                            flag2 = true;
                                        }
                                        if (!flag)
                                        {
                                            AddUpdate(C.b, 0);
                                            AddUpdate(IntOffset(C.b, 0, 1, 0), 0);
                                        }
                                    }
                                    break;
                                default:
                                    if (!C.extraInfo.Contains("wait"))
                                    {
                                        C.time = byte.MaxValue;
                                    }
                                    break;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        ListCheck.Remove(C);
                    }
                });
                ListCheck.RemoveAll(Check => Check.time == byte.MaxValue);
                lastUpdate = ListUpdate.Count;
                if (GeneralSettings.All.IntelliSys)
                {
                    var physBlockUpdateList = new List<byte>();
                    int trim = 0;
                    int iterator = 0;
                    ListUpdate.ForEach(delegate(Update utemp)
                    {
                        try
                        {
                            IntToPos(utemp.b, out x, out y, out z);
                            if (x < 0 || y < 0 || z < 0 || x >= width || y >= height || z >= depth)
                            {
                                trim++;
                            }
                            else
                            {
                                byte tile = GetTile(x, y, z);
                                if (Block.OPBlocks(tile))
                                {
                                    trim++;
                                }
                                else
                                {
                                    physBlockUpdateList.Add(6);
                                    byte[] bytes = BitConverter.GetBytes(x);
                                    Array.Reverse(bytes);
                                    physBlockUpdateList.Add(bytes[0]);
                                    physBlockUpdateList.Add(bytes[1]);
                                    bytes = BitConverter.GetBytes(y);
                                    Array.Reverse(bytes);
                                    physBlockUpdateList.Add(bytes[0]);
                                    physBlockUpdateList.Add(bytes[1]);
                                    bytes = BitConverter.GetBytes(z);
                                    Array.Reverse(bytes);
                                    physBlockUpdateList.Add(bytes[0]);
                                    physBlockUpdateList.Add(bytes[1]);
                                    physBlockUpdateList.Add(Block.Convert(utemp.type));
                                    iterator += 8;
                                    if (tile == 78 && physics > 0 && utemp.type != 78)
                                    {
                                        PhysUniversalSpongeRemoved(PosToInt(x, y, z));
                                    }
                                    if (tile == 19 && physics > 0 && utemp.type != 19)
                                    {
                                        PhysUniversalSpongeRemoved(PosToInt(x, y, z));
                                    }
                                    try
                                    {
                                        UndoPos undoPos = default(UndoPos);
                                        undoPos.location = PosToInt(x, y, z);
                                        undoPos.newType = utemp.type;
                                        undoPos.oldType = tile;
                                        undoPos.timePerformed = DateTime.Now;
                                        if (currentUndo >= Server.physUndo)
                                        {
                                            currentUndo = 0;
                                            UndoBuffer[currentUndo] = undoPos;
                                        }
                                        else if (UndoBuffer.Count <= Server.physUndo)
                                        {
                                            currentUndo++;
                                            UndoBuffer.Add(undoPos);
                                        }
                                        else
                                        {
                                            currentUndo++;
                                            UndoBuffer[currentUndo] = undoPos;
                                        }
                                    }
                                    catch (Exception ex13)
                                    {
                                        Server.s.Log("TUM TUM");
                                        Server.ErrorLog(ex13);
                                    }
                                    SetTile(x, y, z, utemp.type);
                                    if (physics > 0 && (Block.Physics(utemp.type) || utemp.extraInfo != ""))
                                    {
                                        AddCheck(PosToInt(x, y, z), utemp.extraInfo);
                                    }
                                }
                            }
                        }
                        catch (Exception ex14)
                        {
                            Server.ErrorLog(ex14);
                            Server.s.Log("Phys update issue");
                        }
                    });
                    ListUpdate.Clear();
                    if (physBlockUpdateList.Count > 0)
                    {
                        Blockchange(physBlockUpdateList.ToArray());
                    }
                    return;
                }
                ListUpdate.ForEach(delegate(Update utemp)
                {
                    try
                    {
                        IntToPos(utemp.b, out x, out y, out z);
                        Blockchange(x, y, z, utemp.type, false, utemp.extraInfo);
                    }
                    catch
                    {
                        Server.s.Log("Phys update issue");
                    }
                });
                ListUpdate.Clear();
            }
            catch (Exception ex15)
            {
                Server.s.Log("Level physics error");
                Server.ErrorLog(ex15);
            }
        }

        public void AddCheck(int b, string extraInfo = "", bool overRide = false)
        {
            try
            {
                if (!ListCheck.Exists(Check => Check.b == b))
                {
                    ListCheck.Add(new Check(b, extraInfo));
                }
                else
                {
                    if (!overRide)
                    {
                        return;
                    }
                    {
                        foreach (Check item in ListCheck)
                        {
                            if (item.b == b)
                            {
                                item.extraInfo = extraInfo;
                                break;
                            }
                        }
                    }
                }
            }
            catch {}
        }

        public void LavaWild(int b, byte type)
        {
            if (indexLW == 0)
            {
                changeLW = rand_x.Next(0, 10);
            }
            if (indexLW == changeLW)
            {
                AddUpdate(b, type);
            }
            indexLW++;
            if (indexLW > 9)
            {
                indexLW = 0;
            }
        }

        public void LavaDisturbed(int b, byte type)
        {
            if (indexLD == 0)
            {
                changeLD = rand_x.Next(0, 30);
            }
            if (indexLD == changeLD)
            {
                AddUpdate(b, type);
            }
            indexLD++;
            if (indexLD > 29)
            {
                indexLD = 0;
            }
        }

        public void LavaRageGlass(int b)
        {
            if (indexLRG == 0)
            {
                changeLRG = randLRG.Next(0, 19);
            }
            if (indexLRG == changeLRG)
            {
                AddUpdate(b, 254);
            }
            else
            {
                AddUpdate(b, 20);
            }
            indexLRG++;
            if (indexLRG > 20)
            {
                indexLRG = 0;
            }
        }

        public void LavaRage(int b, byte type)
        {
            if (indexLR == 0)
            {
                change = rand_x.Next(0, 19);
            }
            if (indexLR == change)
            {
                AddUpdate(b, type);
            }
            indexLR++;
            if (indexLR > 18)
            {
                indexLR = 0;
            }
        }

        public void LavaRageObsidian(int b, byte type)
        {
            if (indexLRO == 0)
            {
                changeO = rand_xx.Next(0, 22);
            }
            if (indexLR == changeO)
            {
                AddUpdate(b, type);
            }
            indexLRO++;
            if (indexLRO > 21)
            {
                indexLRO = 0;
            }
        }

        public bool AddUpdate(int b, int type, bool overRide = false, string extraInfo = "")
        {
            try
            {
                if (overRide)
                {
                    ushort x;
                    ushort y;
                    ushort z;
                    IntToPos(b, out x, out y, out z);
                    AddCheck(b, extraInfo);
                    Blockchange(x, y, z, (byte)type, true);
                    return true;
                }
                if (!ListUpdate.Exists(Update => Update.b == b))
                {
                    ListUpdate.Add(new Update(b, (byte)type, extraInfo));
                    return true;
                }
                if (type == 12 || type == 13)
                {
                    ListUpdate.RemoveAll(Update => Update.b == b);
                    ListUpdate.Add(new Update(b, (byte)type, extraInfo));
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public void ClearPhysics()
        {
            ushort x;
            ushort y;
            ushort z;
            ListCheck.ForEach(delegate(Check C)
            {
                IntToPos(C.b, out x, out y, out z);
                switch (blocks[C.b])
                {
                    case 200:
                    case 202:
                    case 203:
                        blocks[C.b] = 0;
                        break;
                    case 201:
                        Blockchange(x, y, z, 111);
                        break;
                    case 205:
                        Blockchange(x, y, z, 113);
                        break;
                    case 206:
                        Blockchange(x, y, z, 114);
                        break;
                    case 207:
                        Blockchange(x, y, z, 115);
                        break;
                }
                try
                {
                    if (C.extraInfo.Contains("revert"))
                    {
                        int num = 0;
                        string[] array = C.extraInfo.Split(' ');
                        foreach (string text in array)
                        {
                            if (text == "revert")
                            {
                                Blockchange(x, y, z, byte.Parse(C.extraInfo.Split(' ')[num + 1]));
                                break;
                            }
                            num++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Server.ErrorLog(ex);
                }
            });
            ListCheck.Clear();
            ListUpdate.Clear();
        }

        void PhysWater(int b, byte type)
        {
            if (b == -1)
            {
                return;
            }
            switch (blocks[b])
            {
                case 0:
                    if (!PhysSpongeCheck(b))
                    {
                        AddUpdate(b, type);
                    }
                    break;
                case 10:
                case 112:
                    if (!PhysSpongeCheck(b))
                    {
                        AddUpdate(b, 1);
                    }
                    break;
                case 6:
                case 37:
                case 38:
                case 39:
                case 40:
                    if (physics > 1 && !PhysSpongeCheck(b))
                    {
                        AddUpdate(b, 0);
                    }
                    break;
                case 12:
                case 13:
                case 110:
                    AddCheck(b);
                    break;
            }
        }

        void PhysLava(int b, byte type)
        {
            if (b == -1)
            {
                return;
            }
            switch (blocks[b])
            {
                case 0:
                    AddUpdate(b, type);
                    break;
                case 8:
                    AddUpdate(b, 49);
                    break;
                case 12:
                    if (physics > 1)
                    {
                        LavaRageGlass(b);
                        AddUpdate(b, 20);
                    }
                    else
                    {
                        AddCheck(b);
                    }
                    break;
                case 13:
                    AddCheck(b);
                    if (LavaSettings.All.LavaState == LavaState.Disturbed)
                    {
                        LavaDisturbed(b, 79);
                    }
                    else if (LavaSettings.All.LavaState == LavaState.Furious)
                    {
                        LavaRage(b, 79);
                    }
                    else if (LavaSettings.All.LavaState == LavaState.Wild)
                    {
                        LavaWild(b, 79);
                    }
                    break;
                case 16:
                    if ((int)LavaSettings.All.LavaState > 0)
                    {
                        AddUpdate(b, 0);
                    }
                    break;
                case 5:
                case 6:
                case 17:
                case 18:
                case 37:
                case 38:
                case 39:
                    if (physics > 1)
                    {
                        if (LavaSettings.All.LavaState == LavaState.Disturbed)
                        {
                            LavaDisturbed(b, 79);
                        }
                        else if (LavaSettings.All.LavaState == LavaState.Furious)
                        {
                            AddUpdate(b, 0);
                        }
                        else if (LavaSettings.All.LavaState == LavaState.Wild)
                        {
                            AddUpdate(b, 0);
                        }
                    }
                    break;
                case 7:
                case 79:
                case 80:
                case 81:
                case 82:
                case 83:
                case 98:
                case 105:
                case 112:
                case 194:
                case 195:
                case 220:
                    break;
                case 49:
                    if (LavaSettings.All.LavaState == LavaState.Disturbed)
                    {
                        LavaDisturbed(b, 79);
                    }
                    if (LavaSettings.All.LavaState == LavaState.Furious)
                    {
                        LavaRageObsidian(b, 79);
                    }
                    if (LavaSettings.All.LavaState == LavaState.Wild)
                    {
                        LavaWild(b, 79);
                    }
                    break;
                default:
                    if (LavaSettings.All.LavaState == LavaState.Disturbed)
                    {
                        LavaDisturbed(b, 79);
                    }
                    else if (LavaSettings.All.LavaState == LavaState.Furious)
                    {
                        LavaRage(b, 79);
                    }
                    else if (LavaSettings.All.LavaState == LavaState.Wild)
                    {
                        LavaWild(b, 79);
                    }
                    break;
            }
        }

        void PhysAir(int b)
        {
            if (b == -1)
            {
                return;
            }
            if (Block.Convert(blocks[b]) == 8 || Block.Convert(blocks[b]) == 10)
            {
                AddCheck(b);
                return;
            }
            switch (blocks[b])
            {
                case 12:
                case 13:
                case 110:
                    AddCheck(b);
                    break;
            }
        }

        void PhysCoal(int b)
        {
            if (b == -1)
            {
                return;
            }
            if (Block.Convert(blocks[b]) == 8 || Block.Convert(blocks[b]) == 10)
            {
                AddCheck(b);
                return;
            }
            switch (blocks[b])
            {
                case 12:
                case 13:
                case 110:
                    AddCheck(b);
                    break;
            }
        }

        void PhysGlass(int b)
        {
            if (b != -1 && (Block.Convert(blocks[b]) == 8 || Block.Convert(blocks[b]) == 10))
            {
                AddCheck(b);
            }
        }

        bool PhysSand(int b, byte type)
        {
            if (b == -1 || physics == 0)
            {
                return false;
            }
            int num = b;
            bool flag = false;
            bool flag2 = false;
            do
            {
                num = IntOffset(num, 0, -1, 0);
                if (GetTile(num) != byte.MaxValue)
                {
                    switch (blocks[num])
                    {
                        case 0:
                        case 8:
                        case 10:
                            flag2 = true;
                            break;
                        case 6:
                        case 37:
                        case 38:
                        case 39:
                        case 40:
                            if (physics > 1)
                            {
                                flag2 = true;
                            }
                            else
                            {
                                flag = true;
                            }
                            break;
                        default:
                            flag = true;
                            break;
                    }
                    if (physics > 1)
                    {
                        flag = true;
                    }
                }
                else
                {
                    flag = true;
                }
            } while (!flag);
            if (flag2)
            {
                AddUpdate(b, 0);
                if (physics > 1)
                {
                    AddUpdate(num, type);
                }
                else
                {
                    AddUpdate(IntOffset(num, 0, 1, 0), type);
                }
            }
            return flag2;
        }

        void PhysSandCheck(int b)
        {
            if (b != -1)
            {
                switch (blocks[b])
                {
                    case 12:
                    case 13:
                    case 110:
                        AddCheck(b);
                        break;
                }
            }
        }

        void PhysStair(int b)
        {
            int b2 = IntOffset(b, 0, -1, 0);
            if (GetTile(b2) != byte.MaxValue && GetTile(b2) == 44)
            {
                AddUpdate(b, 0);
                AddUpdate(b2, 43);
            }
        }

        bool PhysSpongeCheck(int b)
        {
            byte b2 = 0;
            for (int i = -3; i <= 3; i++)
            {
                for (int j = -3; j <= 3; j++)
                {
                    for (int k = -3; k <= 3; k++)
                    {
                        switch (GetTile(IntOffset(b, i, j, k)))
                        {
                            case 19:
                            case 78:
                                return true;
                        }
                    }
                }
            }
            return false;
        }

        bool PhysUniversalSpongeCheck(int b)
        {
            byte b2 = 0;
            for (int i = -2; i <= 2; i++)
            {
                for (int j = -2; j <= 2; j++)
                {
                    for (int k = -2; k <= 2; k++)
                    {
                        switch (GetTile(IntOffset(b, i, j, k)))
                        {
                            case 19:
                            case 78:
                                return true;
                        }
                    }
                }
            }
            return false;
        }

        void PhysSponge(int b)
        {
            int num = 0;
            for (int i = -2; i <= 2; i++)
            {
                for (int j = -2; j <= 2; j++)
                {
                    for (int k = -2; k <= 2; k++)
                    {
                        num = IntOffset(b, i, j, k);
                        if (GetTile(num) != byte.MaxValue && GetTile(num) == 8)
                        {
                            AddUpdate(num, 0);
                        }
                    }
                }
            }
        }

        void PhysUniversalSponge(int b)
        {
            byte b2 = byte.MaxValue;
            for (int i = -2; i <= 2; i++)
            {
                for (int j = -2; j <= 2; j++)
                {
                    for (int k = -2; k <= 2; k++)
                    {
                        int b3 = IntOffset(b, i, j, k);
                        b2 = GetTile(b3);
                        if (b2 != byte.MaxValue)
                        {
                            switch (b2)
                            {
                                case 8:
                                case 10:
                                case 11:
                                case 80:
                                case 81:
                                case 82:
                                case 83:
                                case 98:
                                case 190:
                                case 193:
                                case 194:
                                case 195:
                                    AddUpdate(b3, 0);
                                    break;
                            }
                        }
                    }
                }
            }
        }

        public void PhysUniversalSpongeRemoved(int b)
        {
            byte b2 = byte.MaxValue;
            for (int i = -3; i <= 3; i++)
            {
                for (int j = -3; j <= 3; j++)
                {
                    for (int k = -3; k <= 3; k++)
                    {
                        int b3 = IntOffset(b, i, j, k);
                        b2 = GetTile(b3);
                        if (b2 != byte.MaxValue)
                        {
                            switch (b2)
                            {
                                case 8:
                                case 10:
                                case 11:
                                case 80:
                                case 81:
                                case 82:
                                case 83:
                                case 190:
                                case 193:
                                case 194:
                                case 195:
                                    AddCheck(b3);
                                    break;
                            }
                        }
                    }
                }
            }
        }

        public void PhysSpongeRemoved(int b)
        {
            int num = 0;
            for (int i = -3; i <= 3; i++)
            {
                for (int j = -3; j <= 3; j++)
                {
                    for (int k = -3; k <= 3; k++)
                    {
                        num = IntOffset(b, i, j, k);
                        if (GetTile(num) != byte.MaxValue && GetTile(num) == 8)
                        {
                            AddCheck(num);
                        }
                    }
                }
            }
        }

        void PhysFloatwood(int b)
        {
            int b2 = IntOffset(b, 0, -1, 0);
            if (GetTile(b2) != byte.MaxValue && GetTile(b2) == 0)
            {
                AddUpdate(b, 0);
                AddUpdate(b2, 110);
                return;
            }
            b2 = IntOffset(b, 0, 1, 0);
            if (GetTile(b2) != byte.MaxValue && GetTile(b2) == 8)
            {
                AddUpdate(b, 8);
                AddUpdate(b2, 110);
            }
        }

        void PhysAirFlood(int b, byte type)
        {
            if (b != -1 && (Block.Convert(blocks[b]) == 8 || Block.Convert(blocks[b]) == 10))
            {
                AddUpdate(b, type);
            }
        }

        void PhysFall(byte newBlock, ushort x, ushort y, ushort z, bool random)
        {
            Random random2 = new Random();
            if (!random)
            {
                byte tile = GetTile((ushort)(x + 1), y, z);
                if (tile == 0 || tile == 9)
                {
                    Blockchange((ushort)(x + 1), y, z, newBlock);
                }
                tile = GetTile((ushort)(x - 1), y, z);
                if (tile == 0 || tile == 9)
                {
                    Blockchange((ushort)(x - 1), y, z, newBlock);
                }
                tile = GetTile(x, y, (ushort)(z + 1));
                if (tile == 0 || tile == 9)
                {
                    Blockchange(x, y, (ushort)(z + 1), newBlock);
                }
                tile = GetTile(x, y, (ushort)(z - 1));
                if (tile == 0 || tile == 9)
                {
                    Blockchange(x, y, (ushort)(z - 1), newBlock);
                }
            }
            else
            {
                if (GetTile((ushort)(x + 1), y, z) == 0 && random2.Next(1, 10) < 3)
                {
                    Blockchange((ushort)(x + 1), y, z, newBlock);
                }
                if (GetTile((ushort)(x - 1), y, z) == 0 && random2.Next(1, 10) < 3)
                {
                    Blockchange((ushort)(x - 1), y, z, newBlock);
                }
                if (GetTile(x, y, (ushort)(z + 1)) == 0 && random2.Next(1, 10) < 3)
                {
                    Blockchange(x, y, (ushort)(z + 1), newBlock);
                }
                if (GetTile(x, y, (ushort)(z - 1)) == 0 && random2.Next(1, 10) < 3)
                {
                    Blockchange(x, y, (ushort)(z - 1), newBlock);
                }
            }
        }

        void PhysReplace(int b, byte typeA, byte typeB)
        {
            if (b != -1 && blocks[b] == typeA)
            {
                AddUpdate(b, typeB);
            }
        }

        public void odoor(Check C)
        {
            if (C.time == 0)
            {
                byte b = Block.odoor(GetTile(IntOffset(C.b, -1, 0, 0)));
                if (b == blocks[C.b])
                {
                    AddUpdate(IntOffset(C.b, -1, 0, 0), b, true);
                }
                b = Block.odoor(GetTile(IntOffset(C.b, 1, 0, 0)));
                if (b == blocks[C.b])
                {
                    AddUpdate(IntOffset(C.b, 1, 0, 0), b, true);
                }
                b = Block.odoor(GetTile(IntOffset(C.b, 0, -1, 0)));
                if (b == blocks[C.b])
                {
                    AddUpdate(IntOffset(C.b, 0, -1, 0), b, true);
                }
                b = Block.odoor(GetTile(IntOffset(C.b, 0, 1, 0)));
                if (b == blocks[C.b])
                {
                    AddUpdate(IntOffset(C.b, 0, 1, 0), b, true);
                }
                b = Block.odoor(GetTile(IntOffset(C.b, 0, 0, -1)));
                if (b == blocks[C.b])
                {
                    AddUpdate(IntOffset(C.b, 0, 0, -1), b, true);
                }
                b = Block.odoor(GetTile(IntOffset(C.b, 0, 0, 1)));
                if (b == blocks[C.b])
                {
                    AddUpdate(IntOffset(C.b, 0, 0, 1), b, true);
                }
            }
            else
            {
                C.time = byte.MaxValue;
            }
            C.time++;
        }

        public void AnyDoor(Check C, ushort x, ushort y, ushort z, int timer, bool instaUpdate = false)
        {
            if (C.time == 0)
            {
                try
                {
                    PhysDoor((ushort)(x + 1), y, z, instaUpdate);
                }
                catch {}
                try
                {
                    PhysDoor((ushort)(x - 1), y, z, instaUpdate);
                }
                catch {}
                try
                {
                    PhysDoor(x, y, (ushort)(z + 1), instaUpdate);
                }
                catch {}
                try
                {
                    PhysDoor(x, y, (ushort)(z - 1), instaUpdate);
                }
                catch {}
                try
                {
                    PhysDoor(x, (ushort)(y - 1), z, instaUpdate);
                }
                catch {}
                try
                {
                    PhysDoor(x, (ushort)(y + 1), z, instaUpdate);
                }
                catch {}
                try
                {
                    if (blocks[C.b] == 211)
                    {
                        for (int i = -1; i <= 1; i++)
                        {
                            for (int j = -1; j <= 1; j++)
                            {
                                for (int k = -1; k <= 1; k++)
                                {
                                    switch (GetTile(IntOffset(C.b, i, j, k)))
                                    {
                                        case 187:
                                            AddUpdate(IntOffset(C.b, i * 3, j * 3, k * 3), 188);
                                            AddUpdate(IntOffset(C.b, i * 2, j * 2, k * 2), 185);
                                            break;
                                        case 189:
                                            AddUpdate(IntOffset(C.b, i, j + 1, k), 11, false, "dissipate 100");
                                            AddUpdate(IntOffset(C.b, i, j + 2, k), 189);
                                            break;
                                        case 46:
                                            MakeExplosion((ushort)(x + i), (ushort)(y + j), (ushort)(z + k), 0);
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
                catch {}
            }
            if (C.time < timer)
            {
                C.time++;
                return;
            }
            AddUpdate(C.b, Block.SaveConvert(blocks[C.b]));
            C.time = byte.MaxValue;
        }

        public void PhysDoor(ushort x, ushort y, ushort z, bool instaUpdate)
        {
            int num = PosToInt(x, y, z);
            byte b = Block.DoorAirs(blocks[num]);
            if (b != 0)
            {
                if (!instaUpdate)
                {
                    AddUpdate(num, b);
                }
                else
                {
                    Blockchange(x, y, z, b);
                }
                return;
            }
            if (Block.tDoor(blocks[num]))
            {
                AddUpdate(num, 0, false, "wait 16 door 1 revert " + blocks[num]);
            }
            if (Block.odoor(blocks[num]) != byte.MaxValue)
            {
                AddUpdate(num, Block.odoor(blocks[num]), true);
            }
        }

        public void MakeExplosion(ushort x, ushort y, ushort z, int size)
        {
            Random random = new Random();
            if (physics < 2)
            {
                return;
            }
            AddUpdate(PosToInt(x, y, z), 184, true);
            for (int i = x - (size + 1); i <= x + size + 1; i++)
            {
                for (int j = y - (size + 1); j <= y + size + 1; j++)
                {
                    for (int k = z - (size + 1); k <= z + size + 1; k++)
                    {
                        switch (GetTile((ushort)i, (ushort)j, (ushort)k))
                        {
                            case 46:
                                AddUpdate(PosToInt((ushort)i, (ushort)j, (ushort)k), 182);
                                continue;
                            case 182:
                            case 183:
                                AddCheck(PosToInt((ushort)i, (ushort)j, (ushort)k));
                                continue;
                            case byte.MaxValue:
                                continue;
                        }
                        if (random.Next(1, 11) <= 4)
                        {
                            AddUpdate(PosToInt((ushort)i, (ushort)j, (ushort)k), 184);
                        }
                        else if (random.Next(1, 11) <= 8)
                        {
                            AddUpdate(PosToInt((ushort)i, (ushort)j, (ushort)k), 0);
                        }
                        else
                        {
                            AddCheck(PosToInt((ushort)i, (ushort)j, (ushort)k), "drop 50 dissipate 8");
                        }
                    }
                }
            }
            for (int i = x - (size + 2); i <= x + size + 2; i++)
            {
                for (int j = y - (size + 2); j <= y + size + 2; j++)
                {
                    for (int k = z - (size + 2); k <= z + size + 2; k++)
                    {
                        byte tile = GetTile((ushort)i, (ushort)j, (ushort)k);
                        if (tile == byte.MaxValue)
                        {
                            continue;
                        }
                        if (random.Next(1, 10) < 7 && Block.Convert(tile) != 46)
                        {
                            if (random.Next(1, 11) <= 4)
                            {
                                AddUpdate(PosToInt((ushort)i, (ushort)j, (ushort)k), 184);
                            }
                            else if (random.Next(1, 11) <= 8)
                            {
                                AddUpdate(PosToInt((ushort)i, (ushort)j, (ushort)k), 0);
                            }
                            else
                            {
                                AddCheck(PosToInt((ushort)i, (ushort)j, (ushort)k), "drop 50 dissipate 8");
                            }
                        }
                        switch (tile)
                        {
                            case 46:
                                AddUpdate(PosToInt((ushort)i, (ushort)j, (ushort)k), 182);
                                break;
                            case 182:
                            case 183:
                                AddCheck(PosToInt((ushort)i, (ushort)j, (ushort)k));
                                break;
                        }
                    }
                }
            }
            for (int i = x - (size + 3); i <= x + size + 3; i++)
            {
                for (int j = y - (size + 3); j <= y + size + 3; j++)
                {
                    for (int k = z - (size + 3); k <= z + size + 3; k++)
                    {
                        byte tile = GetTile((ushort)i, (ushort)j, (ushort)k);
                        if (tile == byte.MaxValue)
                        {
                            continue;
                        }
                        if (random.Next(1, 10) < 3 && Block.Convert(tile) != 46)
                        {
                            if (random.Next(1, 11) <= 4)
                            {
                                AddUpdate(PosToInt((ushort)i, (ushort)j, (ushort)k), 184);
                            }
                            else if (random.Next(1, 11) <= 8)
                            {
                                AddUpdate(PosToInt((ushort)i, (ushort)j, (ushort)k), 0);
                            }
                            else
                            {
                                AddCheck(PosToInt((ushort)i, (ushort)j, (ushort)k), "drop 50 dissipate 8");
                            }
                        }
                        switch (tile)
                        {
                            case 46:
                                AddUpdate(PosToInt((ushort)i, (ushort)j, (ushort)k), 182);
                                break;
                            case 182:
                            case 183:
                                AddCheck(PosToInt((ushort)i, (ushort)j, (ushort)k));
                                break;
                        }
                    }
                }
            }
        }

        public void DrawBall(int x, int y, int z, byte block, int radius)
        {
            int num = radius * radius;
            for (int i = x - radius; i <= x + radius; i++)
            {
                for (int j = y - radius; j <= y + radius; j++)
                {
                    for (int k = z - radius; k <= z + radius; k++)
                    {
                        byte tile = GetTile(i, j, k);
                        if ((tile == 0 || tile == 96) && Math.Pow(i - x, 2.0) + Math.Pow(j - y, 2.0) + Math.Pow(k - z, 2.0) <= num)
                        {
                            AddUpdate(PosToInt(i, j, k), block);
                        }
                    }
                }
            }
        }

        public void DrawExplosion(int x, int y, int z, byte block, int radius)
        {
            int num = radius * radius;
            Random random = new Random();
            for (int i = x - radius; i <= x + radius; i++)
            {
                for (int j = y - radius; j <= y + radius; j++)
                {
                    for (int k = z - radius; k <= z + radius; k++)
                    {
                        if (Math.Pow(i - x, 2.0) + Math.Pow(j - y, 2.0) + Math.Pow(k - z, 2.0) <= num && random.NextDouble() <= 0.3)
                        {
                            AddUpdate(PosToInt(i, j, k), 184);
                        }
                    }
                }
            }
        }

        public void DrawSmogBomb(int x, int y, int z, int radius)
        {
            int num = radius * radius;
            for (int i = x - radius; i <= x + radius; i++)
            {
                for (int j = y - radius; j <= y + radius; j++)
                {
                    for (int k = z - radius; k <= z + radius; k++)
                    {
                        byte tile = GetTile(i, j, k);
                        if ((tile == 0 || tile == 77) && Math.Pow(i - x, 2.0) + Math.Pow(j - y, 2.0) + Math.Pow(k - z, 2.0) <= num)
                        {
                            AddUpdate(PosToInt(i, j, k), 76);
                        }
                    }
                }
            }
        }

        public void Firework(ushort x, ushort y, ushort z, int size)
        {
            Random random = new Random();
            if (physics < 1)
            {
                return;
            }
            int val = random.Next(21, 36);
            int val2 = random.Next(21, 36);
            AddUpdate(PosToInt(x, y, z), 0, true);
            for (ushort num = (ushort)(x - (size + 1)); num <= (ushort)(x + size + 1); num++)
            {
                for (ushort num2 = (ushort)(y - (size + 1)); num2 <= (ushort)(y + size + 1); num2++)
                {
                    for (ushort num3 = (ushort)(z - (size + 1)); num3 <= (ushort)(z + size + 1); num3++)
                    {
                        if (GetTile(num, num2, num3) == 0 && random.Next(1, 40) < 2)
                        {
                            AddUpdate(PosToInt(num, num2, num3), (byte)random.Next(Math.Min(val, val2), Math.Max(val, val2)), false, "drop 100 dissipate 25");
                        }
                    }
                }
            }
        }

        public void finiteMovement(Check C, ushort x, ushort y, ushort z)
        {
            Random random = new Random();
            var list = new List<int>();
            var list2 = new List<Pos>();
            if (GetTile(x, (ushort)(y - 1), z) == 0)
            {
                AddUpdate(PosToInt(x, (ushort)(y - 1), z), blocks[C.b], false, C.extraInfo);
                AddUpdate(C.b, 0);
                C.extraInfo = "";
                return;
            }
            if (GetTile(x, (ushort)(y - 1), z) == 9 || GetTile(x, (ushort)(y - 1), z) == 11)
            {
                AddUpdate(C.b, 0);
                C.extraInfo = "";
                return;
            }
            for (int i = 0; i < 25; i++)
            {
                list.Add(i);
            }
            for (int num = list.Count - 1; num > 1; num--)
            {
                int index = random.Next(num);
                int value = list[num];
                list[num] = list[index];
                list[index] = value;
            }
            Pos item = default(Pos);
            for (ushort num2 = (ushort)(x - 2); num2 <= x + 2; num2++)
            {
                for (ushort num3 = (ushort)(z - 2); num3 <= z + 2; num3++)
                {
                    item.x = num2;
                    item.z = num3;
                    list2.Add(item);
                }
            }
            foreach (int item2 in list)
            {
                item = list2[item2];
                if (GetTile(item.x, (ushort)(y - 1), item.z) == 0 && GetTile(item.x, y, item.z) == 0)
                {
                    if (item.x < x)
                    {
                        item.x = (ushort)Math.Floor((item.x + x) / 2.0);
                    }
                    else
                    {
                        item.x = (ushort)Math.Ceiling((item.x + x) / 2.0);
                    }
                    if (item.z < z)
                    {
                        item.z = (ushort)Math.Floor((item.z + z) / 2.0);
                    }
                    else
                    {
                        item.z = (ushort)Math.Ceiling((item.z + z) / 2.0);
                    }
                    if (GetTile(item.x, y, item.z) == 0 && AddUpdate(PosToInt(item.x, y, item.z), blocks[C.b], false, C.extraInfo))
                    {
                        AddUpdate(C.b, 0);
                        C.extraInfo = "";
                        break;
                    }
                }
            }
        }

        public static LevelPermission PermissionFromName(string name)
        {
            Group group = Group.Find(name);
            return group != null ? group.Permission : LevelPermission.Null;
        }

        public static string PermissionToName(LevelPermission perm)
        {
            Group group = Group.findPerm(perm);
            if (group != null)
            {
                return group.name;
            }
            int num = (int)perm;
            return num.ToString();
        }

        public List<Player> getPlayers()
        {
            var foundPlayers = new List<Player>();
            Player.players.ForEach(delegate(Player p)
            {
                if (p.level == this)
                {
                    foundPlayers.Add(p);
                }
            });
            return foundPlayers;
        }

        public void SetLavaSpeed(int lavaSpeed)
        {
            this.lavaSpeed = lavaSpeed;
        }

        public bool IsLit(int x, int y, int z)
        {
            if (x < 0 || y < 0 || z < 0 || x >= width || y >= height || z >= depth)
            {
                return true;
            }
            for (int num = height; num >= y; num--)
            {
                if (!Block.LightPass(GetTile(x, num, z)))
                {
                    return false;
                }
            }
            return true;
        }

        internal void OnPlayerJoined(object sender, PlayerJoinedEventArgs e)
        {
            if (PlayerJoined != null)
            {
                PlayerJoined.Invoke(sender, e);
            }
        }

        public struct UndoPos
        {
            public int location;

            public byte oldType;

            public byte newType;

            public DateTime timePerformed;
        }

        public struct BlockPos
        {
            public ushort x;

            public ushort y;

            public ushort z;

            public byte type;

            public DateTime TimePerformed;

            public bool deleted;

            public string name;
        }

        public struct Zone
        {
            public ushort smallX;

            public ushort smallY;

            public ushort smallZ;

            public ushort bigX;

            public ushort bigY;

            public ushort bigZ;

            public string Owner;
        }

        public class LevelOptions
        {
            public string PublicName { get; set; }

            public int Physics { get; set; }
        }

        public struct Pos
        {
            public ushort x;

            public ushort z;
        }
    }
}