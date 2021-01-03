using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    // Token: 0x0200031B RID: 795
    public sealed class Level
    {
        // Token: 0x04000B9A RID: 2970
        private static readonly Random randLRG = new Random();

        // Token: 0x04000B9D RID: 2973
        private static readonly Random rand_x = new Random();

        // Token: 0x04000BA0 RID: 2976
        private static readonly Random rand_xx = new Random();

        // Token: 0x04000BB4 RID: 2996
        private static readonly ResourceManager rm = new ResourceManager("MCDzienny_.MCDzienny.Lang.Level",
            Assembly.GetExecutingAssembly());

        // Token: 0x04000B82 RID: 2946

        // Token: 0x04000B84 RID: 2948
        public bool ai = true;

        // Token: 0x04000B77 RID: 2935
        private HashSet<string> allowedPlayers = new HashSet<string>();

        // Token: 0x04000B62 RID: 2914
        internal bool allowHacks = true;

        // Token: 0x04000BB2 RID: 2994
        public bool backDup;

        // Token: 0x04000B76 RID: 2934
        public List<BlockPos> blockCache = new List<BlockPos>();

        // Token: 0x04000B65 RID: 2917
        private int blockCacheCollect = 1;

        // Token: 0x04000BB5 RID: 2997
        public BlockMap blockMap;

        // Token: 0x04000BA7 RID: 2983
        public byte[] blocks;

        // Token: 0x04000B9C RID: 2972
        private int change;

        // Token: 0x04000BB1 RID: 2993
        public bool changed;

        // Token: 0x04000B95 RID: 2965
        private int changeLD;

        // Token: 0x04000B99 RID: 2969
        private int changeLRG;

        // Token: 0x04000B97 RID: 2967
        private int changeLW;

        // Token: 0x04000B9F RID: 2975
        private int changeO;

        // Token: 0x04000BAC RID: 2988
        private Check[] checks = new Check[0];

        // Token: 0x04000B6F RID: 2927
        private Dictionary<int, CommandActionPair> commandActionsBuild;

        // Token: 0x04000B70 RID: 2928
        private Dictionary<int, CommandActionPair> commandActionsHit;

        // Token: 0x04000B71 RID: 2929
        private Dictionary<int, CommandActionPair> commandActionsWalk;

        // Token: 0x04000B63 RID: 2915
        public bool creativeMode;

        // Token: 0x04000BAD RID: 2989
        public CTFGame ctfgame = new CTFGame();

        // Token: 0x04000BAE RID: 2990
        public bool ctfmode;

        // Token: 0x04000B73 RID: 2931
        public int currentUndo;

        // Token: 0x04000B85 RID: 2949
        public bool Death;

        // Token: 0x04000B6D RID: 2925
        internal string directoryPath = "levels";

        // Token: 0x04000B87 RID: 2951
        public int drown = 70;

        // Token: 0x04000B86 RID: 2950
        public int fall = 9;

        // Token: 0x04000BB6 RID: 2998
        public string fileName;

        // Token: 0x04000B8E RID: 2958
        public bool fishstill;

        // Token: 0x04000B8B RID: 2955
        private bool GrassDestroy_ = true;

        // Token: 0x04000B8C RID: 2956
        private bool GrassGrow_ = true;

        // Token: 0x04000B91 RID: 2961
        public bool hardcore;

        // Token: 0x04000B66 RID: 2918
        public int id;

        // Token: 0x04000B94 RID: 2964
        private int indexLD;

        // Token: 0x04000B9B RID: 2971
        private int indexLR;

        // Token: 0x04000B98 RID: 2968
        private int indexLRG;

        // Token: 0x04000B9E RID: 2974
        private int indexLRO;

        // Token: 0x04000B96 RID: 2966
        private int indexLW;

        // Token: 0x04000B75 RID: 2933

        // Token: 0x04000B78 RID: 2936
        private bool isPublic = true;

        // Token: 0x04000B7B RID: 2939
        public byte jailrotx;

        // Token: 0x04000B7C RID: 2940
        public byte jailroty;

        // Token: 0x04000B8A RID: 2954
        private bool Killer_ = true;

        // Token: 0x04000BAF RID: 2991
        public int lastCheck;

        // Token: 0x04000BB0 RID: 2992
        public int lastUpdate;

        // Token: 0x04000B92 RID: 2962
        public bool lavarage;

        // Token: 0x04000B90 RID: 2960
        private int lavaSpeed = LavaSettings.All.LavaMovementDelay;

        // Token: 0x04000B8F RID: 2959
        public bool lavaUp;

        // Token: 0x04000BA9 RID: 2985
        private List<Check> ListCheck = new List<Check>();

        // Token: 0x04000BAA RID: 2986
        private readonly List<Update> ListUpdate = new List<Update>();

        // Token: 0x04000B64 RID: 2916

        // Token: 0x04000B69 RID: 2921
        private string mapOwner = "";

        // Token: 0x04000B72 RID: 2930
        internal MapSettingsManager mapSettingsManager;

        // Token: 0x04000B68 RID: 2920
        public MapType mapType;

        // Token: 0x04000BA4 RID: 2980
        private string motd_ = "ignore";

        // Token: 0x04000BA2 RID: 2978
        public int overload = 90000;

        // Token: 0x04000B7E RID: 2942
        private readonly SynchronizedDictionary<int, OwnedBlockInfo> ownedBlocks =
            new SynchronizedDictionary<int, OwnedBlockInfo>();

        // Token: 0x04000BA5 RID: 2981

        // Token: 0x04000BA6 RID: 2982

        // Token: 0x04000BB3 RID: 2995
        private readonly Timer physicsTimer = new Timer();

        // Token: 0x04000B7F RID: 2943
        public bool physPause;

        // Token: 0x04000B80 RID: 2944
        public DateTime physResume;

        // Token: 0x04000B81 RID: 2945
        public Timer physTimer = new Timer(1000.0);

        // Token: 0x04000B6A RID: 2922
        public int playerLimit;

        // Token: 0x04000B6B RID: 2923
        public int playersCount;

        // Token: 0x04000B7D RID: 2941
        private readonly object playersLocker = new object();

        // Token: 0x04000B93 RID: 2963
        private readonly Random randLavaTypeC = new Random();

        // Token: 0x04000B83 RID: 2947
        public bool realistic = true;

        // Token: 0x04000B79 RID: 2937
        public byte rotx;

        // Token: 0x04000B7A RID: 2938
        public byte roty;

        // Token: 0x04000B89 RID: 2953
        public bool rp = true;

        // Token: 0x04000BA1 RID: 2977
        public int speedPhysics = 240;

        // Token: 0x04000BAB RID: 2987
        private List<int> tempRemove = new List<int>();

        // Token: 0x04000BA3 RID: 2979
        public string theme = "Normal";

        // Token: 0x04000B74 RID: 2932
        public List<UndoPos> UndoBuffer = new List<UndoPos>();

        // Token: 0x04000B88 RID: 2952
        public bool unload = true;

        // Token: 0x04000B67 RID: 2919
        private volatile bool unloaded;

        // Token: 0x04000B6C RID: 2924

        // Token: 0x04000B6E RID: 2926

        // Token: 0x04000B8D RID: 2957
        private bool worldChat_ = true;

        // Token: 0x04000BA8 RID: 2984
        public List<Zone> ZoneList;

        // Token: 0x06001747 RID: 5959 RVA: 0x0008D490 File Offset: 0x0008B690
        public Level(string name, ushort x, ushort y, ushort z, string type)
        {
            Info = new LevelInfo();
            IsPillaringAllowed = true;
            width = x;
            height = y;
            depth = z;
            if (width < 16) width = 16;
            if (height < 16) height = 16;
            if (depth < 16) depth = 16;
            this.name = name;
            blocks = new byte[width * height * depth];
            ZoneList = new List<Zone>();
            switch (type)
            {
                case "flat":
                case "pixel":
                {
                    var num = (ushort) (height / 2);
                    for (x = 0; x < width; x = (ushort) (x + 1))
                    for (z = 0; z < depth; z = (ushort) (z + 1))
                    for (y = 0; y < height; y = (ushort) (y + 1))
                        switch (type)
                        {
                            case "flat":
                                if (y != num)
                                    SetTile(x, y, z, (byte) (y < num ? 3 : 0));
                                else
                                    SetTile(x, y, z, 2);
                                break;
                            case "pixel":
                                if (y == 0)
                                    SetTile(x, y, z, 7);
                                else if (x == 0 || x == width - 1 || z == 0 || z == depth - 1) SetTile(x, y, z, 36);
                                break;
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

            spawnx = (ushort) (width / 2);
            spawny = (ushort) (height * 0.75f);
            spawnz = (ushort) (depth / 2);
            rotx = 0;
            roty = 0;
            physicsTimer.Interval = speedPhysics;
            physicsTimer.Elapsed += physicsTimer_Elapsed;
            physicsTimer.AutoReset = false;
            StartPhysics();
        }

        // Token: 0x170008BF RID: 2239
        // (get) Token: 0x06001701 RID: 5889 RVA: 0x0008CCFC File Offset: 0x0008AEFC
        // (set) Token: 0x06001702 RID: 5890 RVA: 0x0008CD04 File Offset: 0x0008AF04
        [Browsable(false)] public LevelInfo Info { get; set; }

        // Token: 0x170008C0 RID: 2240
        // (get) Token: 0x06001703 RID: 5891 RVA: 0x0008CD10 File Offset: 0x0008AF10
        public uint MapDbId { get; private set; }

        // Token: 0x170008C1 RID: 2241
        // (get) Token: 0x06001704 RID: 5892 RVA: 0x0008CD18 File Offset: 0x0008AF18
        [ReadOnly(true)]
        [Description("Displays the name of the map without email domain if such exists in the name.")]
        [Category("General")]
        public string PublicName
        {
            get
            {
                var num = name.IndexOf('@');
                if (num != -1) return name.Substring(0, num);
                return name;
            }
        }

        // Token: 0x170008C2 RID: 2242
        // (get) Token: 0x06001705 RID: 5893 RVA: 0x0008CD4C File Offset: 0x0008AF4C
        // (set) Token: 0x06001706 RID: 5894 RVA: 0x0008CD54 File Offset: 0x0008AF54
        [Category("General")]
        [ReadOnly(true)]
        [Description("Displays the name of the map.")]
        public string name { get; set; }

        // Token: 0x170008C3 RID: 2243
        // (get) Token: 0x06001707 RID: 5895 RVA: 0x0008CD60 File Offset: 0x0008AF60
        // (set) Token: 0x06001708 RID: 5896 RVA: 0x0008CD68 File Offset: 0x0008AF68
        [Description("Shows the width of the map.")]
        [Category("Dimensions")]
        [ReadOnly(true)]
        public ushort width { get; set; }

        // Token: 0x170008C4 RID: 2244
        // (get) Token: 0x06001709 RID: 5897 RVA: 0x0008CD74 File Offset: 0x0008AF74
        // (set) Token: 0x0600170A RID: 5898 RVA: 0x0008CD7C File Offset: 0x0008AF7C
        [Category("Dimensions")]
        [ReadOnly(true)]
        [Description("Shows the height of the map.")]
        public ushort height { get; set; }

        // Token: 0x170008C5 RID: 2245
        // (get) Token: 0x0600170B RID: 5899 RVA: 0x0008CD88 File Offset: 0x0008AF88
        // (set) Token: 0x0600170C RID: 5900 RVA: 0x0008CD90 File Offset: 0x0008AF90
        [Description("Shows the depth of the map.")]
        [Category("Dimensions")]
        [ReadOnly(true)]
        public ushort depth { get; set; }

        // Token: 0x170008C6 RID: 2246
        // (get) Token: 0x0600170D RID: 5901 RVA: 0x0008CD9C File Offset: 0x0008AF9C
        public string Owner
        {
            get
            {
                if (mapType == MapType.Home) return name;
                if (mapType == MapType.MyMap) return mapOwner;
                return "";
            }
        }

        // Token: 0x170008C7 RID: 2247
        // (get) Token: 0x0600170E RID: 5902 RVA: 0x0008CDC4 File Offset: 0x0008AFC4
        // (set) Token: 0x0600170F RID: 5903 RVA: 0x0008CDCC File Offset: 0x0008AFCC
        public bool UnloadLock { get; set; }

        // Token: 0x170008C8 RID: 2248
        // (get) Token: 0x06001710 RID: 5904 RVA: 0x0008CDD8 File Offset: 0x0008AFD8
        // (set) Token: 0x06001711 RID: 5905 RVA: 0x0008CDE0 File Offset: 0x0008AFE0
        [Browsable(false)] public int Weight { get; set; }

        // Token: 0x170008C9 RID: 2249
        // (get) Token: 0x06001712 RID: 5906 RVA: 0x0008CDEC File Offset: 0x0008AFEC
        [Browsable(false)]
        public List<CommandBlock> CommandBlocks
        {
            get
            {
                if (mapSettingsManager != null) return mapSettingsManager.commandBlocks;
                return null;
            }
        }

        // Token: 0x170008CA RID: 2250
        // (get) Token: 0x06001713 RID: 5907 RVA: 0x0008CE04 File Offset: 0x0008B004
        [Browsable(false)]
        public Dictionary<int, CommandActionPair> CommandActionsBuild
        {
            get
            {
                if (commandActionsBuild != null) return commandActionsBuild;
                if (mapSettingsManager != null)
                {
                    commandActionsBuild = new Dictionary<int, CommandActionPair>();
                    if (CommandBlocks != null)
                        foreach (var commandBlock in CommandBlocks)
                        {
                            var list = new List<CommandElement>();
                            var list2 = new List<ActionElement>();
                            foreach (var commandElement in commandBlock.commandElements)
                                if ((commandElement.blockTrigger.Value & BlockTrigger.Build) == BlockTrigger.Build)
                                    list.Add(commandElement);
                            foreach (var actionElement in commandBlock.actionElements)
                                if ((actionElement.blockTrigger.Value & BlockTrigger.Build) == BlockTrigger.Build)
                                    list2.Add(actionElement);
                            if (list.Count > 0 || list2.Count > 0)
                                commandActionsBuild.Add(PosToInt(commandBlock.x, commandBlock.y, commandBlock.z),
                                    new CommandActionPair(commandBlock.changeAction.Value, list, list2));
                        }

                    return commandActionsBuild;
                }

                return null;
            }
        }

        // Token: 0x170008CB RID: 2251
        // (get) Token: 0x06001714 RID: 5908 RVA: 0x0008CF8C File Offset: 0x0008B18C
        [Browsable(false)]
        public Dictionary<int, CommandActionPair> CommandActionsHit
        {
            get
            {
                if (commandActionsHit != null) return commandActionsHit;
                if (mapSettingsManager != null)
                {
                    commandActionsHit = new Dictionary<int, CommandActionPair>();
                    if (CommandBlocks != null)
                        foreach (var commandBlock in CommandBlocks)
                        {
                            var list = new List<CommandElement>();
                            var list2 = new List<ActionElement>();
                            foreach (var commandElement in commandBlock.commandElements)
                                if ((commandElement.blockTrigger.Value & BlockTrigger.Hit) == BlockTrigger.Hit)
                                    list.Add(commandElement);
                            foreach (var actionElement in commandBlock.actionElements)
                                if ((actionElement.blockTrigger.Value & BlockTrigger.Hit) == BlockTrigger.Hit)
                                    list2.Add(actionElement);
                            if (list.Count > 0 || list2.Count > 0)
                                commandActionsHit.Add(PosToInt(commandBlock.x, commandBlock.y, commandBlock.z),
                                    new CommandActionPair(commandBlock.changeAction.Value, list, list2));
                        }

                    return commandActionsHit;
                }

                return null;
            }
        }

        // Token: 0x170008CC RID: 2252
        // (get) Token: 0x06001715 RID: 5909 RVA: 0x0008D114 File Offset: 0x0008B314
        [Browsable(false)]
        public Dictionary<int, CommandActionPair> CommandActionsWalk
        {
            get
            {
                if (commandActionsWalk != null) return commandActionsWalk;
                if (mapSettingsManager != null)
                {
                    commandActionsWalk = new Dictionary<int, CommandActionPair>();
                    if (CommandBlocks != null)
                        foreach (var commandBlock in CommandBlocks)
                        {
                            var list = new List<CommandElement>();
                            var list2 = new List<ActionElement>();
                            foreach (var commandElement in commandBlock.commandElements)
                                if ((commandElement.blockTrigger.Value & BlockTrigger.Walk) == BlockTrigger.Walk)
                                    list.Add(commandElement);
                            foreach (var actionElement in commandBlock.actionElements)
                                if ((actionElement.blockTrigger.Value & BlockTrigger.Walk) == BlockTrigger.Walk)
                                    list2.Add(actionElement);
                            if (list.Count > 0 || list2.Count > 0)
                                commandActionsWalk.Add(PosToInt(commandBlock.x, commandBlock.y, commandBlock.z),
                                    new CommandActionPair(commandBlock.changeAction.Value, list, list2));
                        }

                    return commandActionsWalk;
                }

                return null;
            }
        }

        // Token: 0x170008CD RID: 2253
        // (get) Token: 0x06001716 RID: 5910 RVA: 0x0008D29C File Offset: 0x0008B49C
        // (set) Token: 0x06001717 RID: 5911 RVA: 0x0008D2A4 File Offset: 0x0008B4A4
        [Browsable(false)] public bool IsMapBeingBackuped { get; set; }

        // Token: 0x170008CE RID: 2254
        // (get) Token: 0x06001718 RID: 5912 RVA: 0x0008D2B0 File Offset: 0x0008B4B0
        // (set) Token: 0x06001719 RID: 5913 RVA: 0x0008D2B8 File Offset: 0x0008B4B8
        public HashSet<string> AllowedPlayers
        {
            get { return allowedPlayers; }
            set { allowedPlayers = value; }
        }

        // Token: 0x170008CF RID: 2255
        // (get) Token: 0x0600171A RID: 5914 RVA: 0x0008D2C4 File Offset: 0x0008B4C4
        // (set) Token: 0x0600171B RID: 5915 RVA: 0x0008D2CC File Offset: 0x0008B4CC
        public bool IsPublic
        {
            get { return isPublic; }
            set { isPublic = value; }
        }

        // Token: 0x170008D0 RID: 2256
        // (get) Token: 0x0600171C RID: 5916 RVA: 0x0008D2D8 File Offset: 0x0008B4D8
        // (set) Token: 0x0600171D RID: 5917 RVA: 0x0008D2E0 File Offset: 0x0008B4E0
        [Description("Describes the spawn point X coordinate.")]
        [Category("General")]
        public ushort spawnx { get; set; }

        // Token: 0x170008D1 RID: 2257
        // (get) Token: 0x0600171E RID: 5918 RVA: 0x0008D2EC File Offset: 0x0008B4EC
        // (set) Token: 0x0600171F RID: 5919 RVA: 0x0008D2F4 File Offset: 0x0008B4F4
        [Description("Describes the spawn point Y coordinate.")]
        [Category("General")]
        public ushort spawny { get; set; }

        // Token: 0x170008D2 RID: 2258
        // (get) Token: 0x06001720 RID: 5920 RVA: 0x0008D300 File Offset: 0x0008B500
        // (set) Token: 0x06001721 RID: 5921 RVA: 0x0008D308 File Offset: 0x0008B508
        [Category("General")]
        [Description("Describes the spawn point Z coordinate.")]
        public ushort spawnz { get; set; }

        // Token: 0x170008D3 RID: 2259
        // (get) Token: 0x06001722 RID: 5922 RVA: 0x0008D314 File Offset: 0x0008B514
        // (set) Token: 0x06001723 RID: 5923 RVA: 0x0008D31C File Offset: 0x0008B51C
        [Category("General")]
        [Description("Describes the spawn rotation X coordinate.")]
        public byte SpawnRotX
        {
            get { return rotx; }
            set { rotx = value; }
        }

        // Token: 0x170008D4 RID: 2260
        // (get) Token: 0x06001724 RID: 5924 RVA: 0x0008D328 File Offset: 0x0008B528
        // (set) Token: 0x06001725 RID: 5925 RVA: 0x0008D330 File Offset: 0x0008B530
        [Category("General")]
        [Description("Describes the spawn rotation Y coordinate.")]
        public byte SpawnRotY
        {
            get { return roty; }
            set { roty = value; }
        }

        // Token: 0x170008D5 RID: 2261
        // (get) Token: 0x06001726 RID: 5926 RVA: 0x0008D33C File Offset: 0x0008B53C
        // (set) Token: 0x06001727 RID: 5927 RVA: 0x0008D344 File Offset: 0x0008B544
        [Description("Describes the jail point X coordinate.")]
        [Category("General")]
        public ushort jailx { get; set; }

        // Token: 0x170008D6 RID: 2262
        // (get) Token: 0x06001728 RID: 5928 RVA: 0x0008D350 File Offset: 0x0008B550
        // (set) Token: 0x06001729 RID: 5929 RVA: 0x0008D358 File Offset: 0x0008B558
        [Category("General")]
        [Description("Describes the jail point X coordinate.")]
        public ushort jaily { get; set; }

        // Token: 0x170008D7 RID: 2263
        // (get) Token: 0x0600172A RID: 5930 RVA: 0x0008D364 File Offset: 0x0008B564
        // (set) Token: 0x0600172B RID: 5931 RVA: 0x0008D36C File Offset: 0x0008B56C
        [Description("Describes the jail point X coordinate.")]
        [Category("General")]
        public ushort jailz { get; set; }

        // Token: 0x170008D8 RID: 2264
        // (get) Token: 0x0600172C RID: 5932 RVA: 0x0008D378 File Offset: 0x0008B578
        // (set) Token: 0x0600172D RID: 5933 RVA: 0x0008D380 File Offset: 0x0008B580
        [Description("Indicates whether edge water spreads.")]
        [Category("General")]
        public bool edgeWater { get; set; }

        // Token: 0x170008D9 RID: 2265
        // (get) Token: 0x0600172E RID: 5934 RVA: 0x0008D38C File Offset: 0x0008B58C
        [Browsable(false)]
        public int PlayersCount
        {
            get { return Player.players.Count(pl => pl.level == this); }
        }

        // Token: 0x170008DA RID: 2266
        // (get) Token: 0x0600172F RID: 5935 RVA: 0x0008D3A4 File Offset: 0x0008B5A4
        [Browsable(false)]
        public object PlayersSynchronizationObject
        {
            get { return playersLocker; }
        }

        // Token: 0x170008DB RID: 2267
        // (get) Token: 0x06001730 RID: 5936 RVA: 0x0008D3AC File Offset: 0x0008B5AC
        [Browsable(false)]
        public SynchronizedDictionary<int, OwnedBlockInfo> OwnedBlocks
        {
            get { return ownedBlocks; }
        }

        // Token: 0x170008DC RID: 2268
        // (get) Token: 0x06001731 RID: 5937 RVA: 0x0008D3B4 File Offset: 0x0008B5B4
        // (set) Token: 0x06001732 RID: 5938 RVA: 0x0008D3BC File Offset: 0x0008B5BC
        [Description("Describes the physics level. Valid values 0,1,2,3,4. Levels from none to doors only.")]
        [Category("General")]
        public int physics { get; set; }

        // Token: 0x170008DD RID: 2269
        // (get) Token: 0x06001733 RID: 5939 RVA: 0x0008D3C8 File Offset: 0x0008B5C8
        // (set) Token: 0x06001734 RID: 5940 RVA: 0x0008D3D0 File Offset: 0x0008B5D0
        [Category("General")]
        [Description("Indicates whether liquids spreads finitly or infinitly.")]
        public bool finite { get; set; }

        // Token: 0x170008DE RID: 2270
        // (get) Token: 0x06001735 RID: 5941 RVA: 0x0008D3DC File Offset: 0x0008B5DC
        // (set) Token: 0x06001736 RID: 5942 RVA: 0x0008D3E4 File Offset: 0x0008B5E4
        [Description("Determines whether the instant building mode is on.")]
        [Category("General")]
        public bool Instant { get; set; }

        // Token: 0x170008DF RID: 2271
        // (get) Token: 0x06001737 RID: 5943 RVA: 0x0008D3F0 File Offset: 0x0008B5F0
        // (set) Token: 0x06001738 RID: 5944 RVA: 0x0008D3F8 File Offset: 0x0008B5F8
        [Category("General")]
        [Description("Turns killer blocks on or off.")]
        public bool Killer
        {
            get { return Killer_; }
            set { Killer_ = value; }
        }

        // Token: 0x170008E0 RID: 2272
        // (get) Token: 0x06001739 RID: 5945 RVA: 0x0008D404 File Offset: 0x0008B604
        // (set) Token: 0x0600173A RID: 5946 RVA: 0x0008D40C File Offset: 0x0008B60C
        [Category("General")]
        [Description("Determines whether a grass is changed into dirt when a block is placed over it.")]
        public bool GrassDestroy
        {
            get { return GrassDestroy_; }
            set { GrassDestroy_ = value; }
        }

        // Token: 0x170008E1 RID: 2273
        // (get) Token: 0x0600173B RID: 5947 RVA: 0x0008D418 File Offset: 0x0008B618
        // (set) Token: 0x0600173C RID: 5948 RVA: 0x0008D420 File Offset: 0x0008B620
        [Description("Determines whether the grass grows after placing a dirt block.")]
        [Category("General")]
        public bool GrassGrow
        {
            get { return GrassGrow_; }
            set { GrassGrow_ = value; }
        }

        // Token: 0x170008E2 RID: 2274
        // (get) Token: 0x0600173D RID: 5949 RVA: 0x0008D42C File Offset: 0x0008B62C
        // (set) Token: 0x0600173E RID: 5950 RVA: 0x0008D434 File Offset: 0x0008B634
        [Description("Determines whether the cross-maps or only map chat is enabled.")]
        [Category("General")]
        public bool worldChat
        {
            get { return worldChat_; }
            set { worldChat_ = value; }
        }

        // Token: 0x170008E3 RID: 2275
        // (get) Token: 0x0600173F RID: 5951 RVA: 0x0008D440 File Offset: 0x0008B640
        // (set) Token: 0x06001740 RID: 5952 RVA: 0x0008D448 File Offset: 0x0008B648
        [Description("It's the message that is showed during the map loading.")]
        [Category("General")]
        public string motd
        {
            get { return motd_; }
            set { motd_ = value; }
        }

        // Token: 0x170008E4 RID: 2276
        // (get) Token: 0x06001741 RID: 5953 RVA: 0x0008D454 File Offset: 0x0008B654
        // (set) Token: 0x06001742 RID: 5954 RVA: 0x0008D45C File Offset: 0x0008B65C
        [Category("General")]
        [Description("Defines permission required to visit the map.")]
        public LevelPermission permissionvisit { get; set; }

        // Token: 0x170008E5 RID: 2277
        // (get) Token: 0x06001743 RID: 5955 RVA: 0x0008D468 File Offset: 0x0008B668
        // (set) Token: 0x06001744 RID: 5956 RVA: 0x0008D470 File Offset: 0x0008B670
        [Category("General")]
        [Description("Defines permission required to build on the map.")]
        public LevelPermission permissionbuild { get; set; }

        // Token: 0x170008E6 RID: 2278
        // (get) Token: 0x06001745 RID: 5957 RVA: 0x0008D47C File Offset: 0x0008B67C
        // (set) Token: 0x06001746 RID: 5958 RVA: 0x0008D484 File Offset: 0x0008B684
        [Category("General")]
        [Description("Describes whether pillaring is allowed or not.")]
        public bool IsPillaringAllowed { get; set; }

        // Token: 0x06001748 RID: 5960 RVA: 0x0008D890 File Offset: 0x0008BA90
        public List<AABB> GetCubes(AABB aabb)
        {
            var list = new List<AABB>();
            var num = (int) aabb.x0;
            var num2 = (int) aabb.x1 + 1;
            var num3 = (int) aabb.y0;
            var num4 = (int) aabb.y1 + 1;
            var num5 = (int) aabb.z0;
            var num6 = (int) aabb.z1 + 1;
            if (aabb.x0 < 0f) num--;
            if (aabb.y0 < 0f) num3--;
            if (aabb.z0 < 0f) num5--;
            for (var i = num; i < num2; i++)
            for (var j = num3; j < num4; j++)
            for (var k = num5; k < num6; k++)
            {
                AABB collisionBox2;
                if (i >= 0 && j >= 0 && k >= 0 && i < width && j < height && k < depth)
                {
                    var tile = GetTile(i, j, k);
                    var collisionBox = Block.GetCollisionBox(tile, i, j, k);
                    if (tile > 0 && collisionBox != null && aabb.intersectsInner(collisionBox)) list.Add(collisionBox);
                }
                else if ((i < 0 || j < 0 || k < 0 || i >= width || k >= depth) &&
                         (collisionBox2 = Block.GetCollisionBox(7, i, j, k)) != null &&
                         aabb.intersectsInner(collisionBox2))
                {
                    list.Add(collisionBox2);
                }
            }

            return list;
        }

        // Token: 0x06001749 RID: 5961 RVA: 0x0008DA04 File Offset: 0x0008BC04
        public bool ContainsAnyLiquid(AABB aabb)
        {
            var num = (int) aabb.x0;
            var num2 = (int) aabb.x1 + 1;
            var num3 = (int) aabb.y0;
            var num4 = (int) aabb.y1 + 1;
            var num5 = (int) aabb.z0;
            var num6 = (int) aabb.z1 + 1;
            if (aabb.x0 < 0f) num--;
            if (aabb.y0 < 0f) num3--;
            if (aabb.z0 < 0f) num5--;
            if (num < 0) num = 0;
            if (num3 < 0) num3 = 0;
            if (num5 < 0) num5 = 0;
            if (num2 > width) num2 = width;
            if (num4 > depth) num4 = depth;
            if (num6 > height) num6 = height;
            for (var i = num; i < num2; i++)
            for (var j = num3; j < num4; j++)
            for (var k = num5; k < num6; k++)
            {
                var tile = GetTile(i, j, k);
                if (tile != 7 && Block.IsLiquid(tile)) return true;
            }

            return false;
        }

        // Token: 0x0600174A RID: 5962 RVA: 0x0008DB18 File Offset: 0x0008BD18
        public bool containsBlock(AABB aabb, byte block)
        {
            var num = (int) aabb.x0;
            var num2 = (int) aabb.x1 + 1;
            var num3 = (int) aabb.y0;
            var num4 = (int) aabb.y1 + 1;
            var num5 = (int) aabb.z0;
            var num6 = (int) aabb.z1 + 1;
            if (aabb.x0 < 0f) num--;
            if (aabb.y0 < 0f) num3--;
            if (aabb.z0 < 0f) num5--;
            if (num < 0) num = 0;
            if (num3 < 0) num3 = 0;
            if (num5 < 0) num5 = 0;
            if (num2 > width) num2 = width;
            if (num4 > depth) num4 = depth;
            if (num6 > height) num6 = height;
            for (var i = num; i < num2; i++)
            for (var j = num3; j < num4; j++)
            for (var k = num5; k < num6; k++)
            {
                var tile = GetTile(i, j, k);
                if (tile == block) return true;
            }

            return false;
        }

        // Token: 0x0600174B RID: 5963 RVA: 0x0008DC20 File Offset: 0x0008BE20
        public float getBrightness(int x, int y, int z)
        {
            if (!IsLit(x, y, z)) return 0.6f;
            return 1f;
        }

        // Token: 0x0600174C RID: 5964 RVA: 0x0008DC38 File Offset: 0x0008BE38
        public bool containsLiquid(AABB aabb, byte block)
        {
            var num = (int) aabb.x0;
            var num2 = (int) aabb.x1 + 1;
            var num3 = (int) aabb.y0;
            var num4 = (int) aabb.y1 + 1;
            var num5 = (int) aabb.z0;
            var num6 = (int) aabb.z1 + 1;
            if (aabb.x0 < 0f) num--;
            if (aabb.y0 < 0f) num3--;
            if (aabb.z0 < 0f) num5--;
            if (num < 0) num = 0;
            if (num3 < 0) num3 = 0;
            if (num5 < 0) num5 = 0;
            if (num2 > width) num2 = width;
            if (num4 > depth) num4 = depth;
            if (num6 > height) num6 = height;
            block = Block.ToMoving(block);
            for (var i = num; i < num2; i++)
            for (var j = num3; j < num4; j++)
            for (var k = num5; k < num6; k++)
            {
                var tile = GetTile(i, j, k);
                if (Block.ToMoving(tile) == block) return true;
            }

            return false;
        }

        // Token: 0x0600174D RID: 5965 RVA: 0x0008DD50 File Offset: 0x0008BF50
        private void physicsTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var num = speedPhysics;
            if (unloaded || physics == 0) return;
            if (ListCheck.Count != 0)
            {
                if (GeneralSettings.All.IntelliSys && InteliSys.pendingPacketsAvg > GeneralSettings.All.AvgStop)
                    num = 3000;
                else
                    try
                    {
                        var now = DateTime.Now;
                        CalcPhysics();
                        var timeSpan = DateTime.Now - now;
                        var num2 = speedPhysics - (int) timeSpan.TotalMilliseconds;
                        if (num2 < (int) (-(float) overload * 0.75f))
                        {
                            if (num2 < -overload)
                            {
                                if (!Server.physicsRestart) setPhysics(0);
                                ClearPhysics();
                                Player.GlobalMessage(string.Format(rm.GetString("PhysicsShutdownGlobalMessage"), name));
                                Server.s.Log("Physics shutdown on " + name);
                                num2 = speedPhysics;
                            }
                            else
                            {
                                Player.players.ForEach(delegate(Player p)
                                {
                                    if (p.level == this) Player.SendMessage(p, rm.GetString("PhysicsWarning"));
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

            physicsTimer.Interval = num;
            if (Server.mono) physicsTimer.Start();
        }

        // Token: 0x0600174E RID: 5966 RVA: 0x0008DED0 File Offset: 0x0008C0D0
        public void CopyBlocks(byte[] source, int offset)
        {
            blocks = new byte[width * height * depth];
            Array.Copy(source, offset, blocks, 0, blocks.Length);
            for (var i = 0; i < blocks.Length; i++)
            {
                if (blocks[i] >= 50) blocks[i] = 0;
                if (blocks[i] == 9)
                    blocks[i] = 8;
                else if (blocks[i] == 8)
                    blocks[i] = 9;
                else if (blocks[i] == 10)
                    blocks[i] = 11;
                else if (blocks[i] == 11) blocks[i] = 10;
            }
        }

        // Token: 0x0600174F RID: 5967 RVA: 0x0008DF98 File Offset: 0x0008C198
        public bool Unload(bool reload = false)
        {
            if (!reload)
            {
                if (Server.DefaultLevel == this) return false;
                if (UnloadLock) return false;
                if (name.Contains("&cMuseum ")) return false;
                var playersToBeMoved = new List<Player>();
                Player.players.ForEach(delegate(Player p)
                {
                    if (p.level == this) playersToBeMoved.Add(p);
                });
                foreach (var p2 in playersToBeMoved)
                    try
                    {
                        Command.all.Find("goto").Use(p2, Server.DefaultLevel.name);
                    }
                    catch
                    {
                    }

                if (changed && mapType != MapType.Lava && mapType != MapType.Zombie)
                {
                    Save();
                    SaveChanges();
                }
            }

            if (unloaded) return true;
            unloaded = true;
            physicsTimer.Stop();
            physicsTimer.Close();
            Server.RemoveLevel(this);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Player.GlobalMessageOps(string.Format(rm.GetString("MapUnloadedGlobalMessage"),
                name + Server.DefaultColor));
            Server.s.Log(name + " was unloaded.");
            RefreshUnloadedMapsInGUI();
            return true;
        }

        // Token: 0x06001750 RID: 5968 RVA: 0x0008E11C File Offset: 0x0008C31C
        private static void RefreshUnloadedMapsInGUI()
        {
            if (Server.CLI) return;
            Window.thisWindow.RefreshUnloadedMapsList();
        }

        // Token: 0x06001751 RID: 5969 RVA: 0x0008E130 File Offset: 0x0008C330
        public void SaveChanges()
        {
            if (blockCache.Count == 0) return;
            if (mapType == MapType.Lava || mapType == MapType.Zombie)
            {
                if (blockCacheCollect % 25 == 0) blockCache.Clear();
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
                        var stringBuilder = new StringBuilder();
                        stringBuilder.Append(
                            "INSERT INTO `Blocks` (Map, Username, TimePerformed, X, Y, Z, type, deleted) VALUES ");
                        foreach (var blockPos in list)
                        {
                            var stringBuilder2 = stringBuilder;
                            var array = new object[17];
                            array[0] = "(";
                            array[1] = MapDbId;
                            array[2] = ", '";
                            array[3] = blockPos.name;
                            array[4] = "', '";
                            var array2 = array;
                            var num = 5;
                            var timePerformed = blockPos.TimePerformed;
                            array2[num] = timePerformed.ToString("yyyy-MM-dd HH:mm:ss");
                            array[6] = "', ";
                            array[7] = (int) blockPos.x;
                            array[8] = ", ";
                            array[9] = (int) blockPos.y;
                            array[10] = ", ";
                            array[11] = (int) blockPos.z;
                            array[12] = ", ";
                            array[13] = blockPos.type;
                            array[14] = ", ";
                            array[15] = blockPos.deleted;
                            array[16] = "), ";
                            stringBuilder2.Append(string.Concat(array));
                        }

                        stringBuilder.Remove(stringBuilder.Length - 2, 2);
                        DBInterface.ExecuteQuery(stringBuilder.ToString());
                    }
                    else
                    {
                        var commandText =
                            "INSERT INTO `Blocks` (Map, Username, TimePerformed, X, Y, Z, Type, Deleted) VALUES ";
                        var array3 = new string[list.Count];
                        for (var i = 0; i < list.Count; i++)
                        {
                            var array4 = array3;
                            var num2 = i;
                            var array5 = new object[17];
                            array5[0] = "(";
                            array5[1] = MapDbId;
                            array5[2] = ", '";
                            array5[3] = list[i].name;
                            array5[4] = "', '";
                            var array6 = array5;
                            var num3 = 5;
                            var timePerformed2 = list[i].TimePerformed;
                            array6[num3] = timePerformed2.ToString("yyyy-MM-dd HH:mm:ss");
                            array5[6] = "', ";
                            array5[7] = (int) list[i].x;
                            array5[8] = ", ";
                            array5[9] = (int) list[i].y;
                            array5[10] = ", ";
                            array5[11] = (int) list[i].z;
                            array5[12] = ", ";
                            array5[13] = list[i].type;
                            array5[14] = ", ";
                            array5[15] = list[0].deleted ? 1 : 0;
                            array5[16] = ")";
                            array4[num2] = string.Concat(array5);
                        }

                        DBInterface.Transaction(commandText, array3);
                    }
                }
                else if (Server.useMySQL)
                {
                    var stringBuilder3 = new StringBuilder();
                    stringBuilder3.Append("INSERT INTO `Block" + name +
                                          "` (Username, TimePerformed, X, Y, Z, type, deleted) VALUES ");
                    foreach (var blockPos2 in list)
                    {
                        var stringBuilder4 = stringBuilder3;
                        var array7 = new object[15];
                        array7[0] = "('";
                        array7[1] = blockPos2.name;
                        array7[2] = "', '";
                        var array8 = array7;
                        var num4 = 3;
                        var timePerformed3 = blockPos2.TimePerformed;
                        array8[num4] = timePerformed3.ToString("yyyy-MM-dd HH:mm:ss");
                        array7[4] = "', ";
                        array7[5] = (int) blockPos2.x;
                        array7[6] = ", ";
                        array7[7] = (int) blockPos2.y;
                        array7[8] = ", ";
                        array7[9] = (int) blockPos2.z;
                        array7[10] = ", ";
                        array7[11] = blockPos2.type;
                        array7[12] = ", ";
                        array7[13] = blockPos2.deleted;
                        array7[14] = "), ";
                        stringBuilder4.Append(string.Concat(array7));
                    }

                    stringBuilder3.Remove(stringBuilder3.Length - 2, 2);
                    DBInterface.ExecuteQuery(stringBuilder3.ToString());
                }
                else
                {
                    var commandText = "INSERT INTO `Block" + name +
                                      "` (Username, TimePerformed, X, Y, Z, Type, Deleted) VALUES ";
                    var array9 = new string[list.Count];
                    for (var j = 0; j < list.Count; j++)
                    {
                        var array10 = array9;
                        var num5 = j;
                        var array11 = new object[15];
                        array11[0] = "('";
                        array11[1] = list[j].name;
                        array11[2] = "', '";
                        var array12 = array11;
                        var num6 = 3;
                        var timePerformed4 = list[j].TimePerformed;
                        array12[num6] = timePerformed4.ToString("yyyy-MM-dd HH:mm:ss");
                        array11[4] = "', ";
                        array11[5] = (int) list[j].x;
                        array11[6] = ", ";
                        array11[7] = (int) list[j].y;
                        array11[8] = ", ";
                        array11[9] = (int) list[j].z;
                        array11[10] = ", ";
                        array11[11] = list[j].type;
                        array11[12] = ", ";
                        array11[13] = list[0].deleted ? 1 : 0;
                        array11[14] = ")";
                        array10[num5] = string.Concat(array11);
                    }

                    DBInterface.Transaction(commandText, array9);
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }

            list.Clear();
        }

        // Token: 0x06001752 RID: 5970 RVA: 0x0008E76C File Offset: 0x0008C96C
        public byte GetTile(int x, int y, int z)
        {
            if (x < 0 || x >= width) return byte.MaxValue;
            if (y < 0 || y >= height) return byte.MaxValue;
            if (z < 0 || z >= depth) return byte.MaxValue;
            return blocks[PosToInt(x, y, z)];
        }

        // Token: 0x06001753 RID: 5971 RVA: 0x0008E7C4 File Offset: 0x0008C9C4
        public bool IsSurroundedByAir(int x, int y, int z)
        {
            return IsAir(x + 1, y, z) && IsAir(x - 1, y, z) && IsAir(x, y, z + 1) && IsAir(x, y, z - 1);
        }

        // Token: 0x06001754 RID: 5972 RVA: 0x0008E800 File Offset: 0x0008CA00
        public bool IsAir(int x, int y, int z)
        {
            return Block.IsAir(GetTile(x, y, z));
        }

        // Token: 0x06001755 RID: 5973 RVA: 0x0008E818 File Offset: 0x0008CA18
        public byte GetTile(int b)
        {
            ushort x = 0;
            ushort y = 0;
            ushort z = 0;
            IntToPos(b, out x, out y, out z);
            return GetTile(x, y, z);
        }

        // Token: 0x06001756 RID: 5974 RVA: 0x0008E844 File Offset: 0x0008CA44
        public void SetTile(ushort x, ushort y, ushort z, byte type)
        {
            blocks[x + width * z + width * depth * y] = type;
        }

        // Token: 0x06001757 RID: 5975 RVA: 0x0008E86C File Offset: 0x0008CA6C
        public static byte[] HTNO(ushort x)
        {
            var bytes = BitConverter.GetBytes(x);
            Array.Reverse(bytes);
            return bytes;
        }

        // Token: 0x06001758 RID: 5976 RVA: 0x0008E888 File Offset: 0x0008CA88
        public static ushort NTHO(byte[] x, int offset)
        {
            var array = new byte[2];
            Buffer.BlockCopy(x, offset, array, 0, 2);
            Array.Reverse(array);
            return BitConverter.ToUInt16(array, 0);
        }

        // Token: 0x06001759 RID: 5977 RVA: 0x0008E8B4 File Offset: 0x0008CAB4
        public static byte[] HTNO(short x)
        {
            var bytes = BitConverter.GetBytes(x);
            Array.Reverse(bytes);
            return bytes;
        }

        // Token: 0x0600175A RID: 5978 RVA: 0x0008E8D0 File Offset: 0x0008CAD0
        public Player GetClosestPlayer(float x, float y, float z)
        {
            var copy = Player.players.GetCopy();
            return (from p in copy
                where p.level == this
                orderby p.distanceToSq(x, y, z)
                select p).FirstOrDefault();
        }

        // Token: 0x0600175B RID: 5979 RVA: 0x0008E934 File Offset: 0x0008CB34
        public List<Entity> findEntities(Entity excluded, AABB aabb)
        {
            return blockMap.getEntities(excluded, aabb);
        }

        // Token: 0x0600175C RID: 5980 RVA: 0x0008E944 File Offset: 0x0008CB44
        public MovingObjectPosition clip(Vector3F a, Vector3F b)
        {
            if (float.IsNaN(a.X) || float.IsNaN(a.Y) || float.IsNaN(a.Z) || float.IsNaN(b.X) || float.IsNaN(b.Y) ||
                float.IsNaN(b.Z)) return null;
            var num = (int) Math.Floor(b.X);
            var num2 = (int) Math.Floor(b.Y);
            var num3 = (int) Math.Floor(b.Z);
            var num4 = (int) Math.Floor(a.X);
            var num5 = (int) Math.Floor(a.Y);
            var num6 = (int) Math.Floor(a.Z);
            var num7 = 20;
            while (num7-- >= 0)
            {
                if (float.IsNaN(a.X) || float.IsNaN(a.Y) || float.IsNaN(a.Z)) return null;
                if (num4 == num && num5 == num2 && num6 == num3) return null;
                var num8 = 999f;
                var num9 = 999f;
                var num10 = 999f;
                if (num > num4) num8 = num4 + 1f;
                if (num < num4) num8 = num4;
                if (num2 > num5) num9 = num5 + 1f;
                if (num2 < num5) num9 = num5;
                if (num3 > num6) num10 = num6 + 1f;
                if (num3 < num6) num10 = num6;
                var num11 = 999f;
                var num12 = 999f;
                var num13 = 999f;
                var num14 = b.X - a.X;
                var num15 = b.Y - a.Y;
                var num16 = b.Z - a.Z;
                if (num8 != 999f) num11 = (num8 - a.X) / num14;
                if (num9 != 999f) num12 = (num9 - a.Y) / num15;
                if (num10 != 999f) num13 = (num10 - a.Z) / num16;
                byte b2;
                if (num11 < num12 && num11 < num13)
                {
                    if (num > num4)
                        b2 = 4;
                    else
                        b2 = 5;
                    a.X = num8;
                    a.Y += num15 * num11;
                    a.Z += num16 * num11;
                }
                else if (num12 < num13)
                {
                    if (num2 > num5)
                        b2 = 0;
                    else
                        b2 = 1;
                    a.X += num14 * num12;
                    a.Y = num9;
                    a.Z += num16 * num12;
                }
                else
                {
                    if (num3 > num6)
                        b2 = 2;
                    else
                        b2 = 3;
                    a.X += num14 * num13;
                    a.Y += num15 * num13;
                    a.Z = num10;
                }

                var vector3F = new Vector3F(0f, 0f, 0f);
                vector3F.X = (float) Math.Floor(a.X);
                num4 = (int) vector3F.X;
                if (b2 == 5)
                {
                    num4--;
                    vector3F.X += 1f;
                }

                vector3F.Y = (float) Math.Floor(a.Y);
                num5 = (int) vector3F.Y;
                if (b2 == 1)
                {
                    num5--;
                    vector3F.Y += 1f;
                }

                vector3F.Z = (float) Math.Floor(a.Z);
                num6 = (int) vector3F.Z;
                if (b2 == 3)
                {
                    num6--;
                    vector3F.Z += 1f;
                }

                var tile = GetTile(num4, num5, num6);
                if (tile > 0 && !Block.IsLiquid(tile))
                {
                    var movingObjectPosition = Block.clip(tile, num4, num5, num6, a, b);
                    if (movingObjectPosition != null) return movingObjectPosition;
                }
            }

            return null;
        }

        // Token: 0x0600175D RID: 5981 RVA: 0x0008ECE8 File Offset: 0x0008CEE8
        public static Level ExactFind(string levelName)
        {
            levelName = levelName.ToLower();
            lock (Server.levels)
            {
                foreach (var level in Server.levels)
                    if (level.name.ToLower() == levelName)
                        return level;
            }

            return null;
        }

        // Token: 0x0600175E RID: 5982 RVA: 0x0008ED78 File Offset: 0x0008CF78
        public static Level Find(string levelName)
        {
            Level level = null;
            levelName = levelName.ToLower();
            var flag = false;
            var flag2 = false;
            lock (Server.levels)
            {
                foreach (var level2 in Server.levels)
                    if (!(level2.Owner != ""))
                    {
                        if (level2.name.ToLower() == levelName) return level2;
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

                foreach (var level3 in Server.levels)
                    if (!(level3.Owner == ""))
                    {
                        if (level3.name.ToLower() == levelName) return level3;
                        if (!flag2 && level3.name.ToLower().Contains(levelName.ToLower()) && level == null)
                            level = level3;
                    }
            }

            if (flag) return null;
            return level;
        }

        // Token: 0x0600175F RID: 5983 RVA: 0x0008EF00 File Offset: 0x0008D100
        public static Level FindExactMM(string levelName, string owner)
        {
            Level result;
            lock (Server.levels)
            {
                result = (from l in Server.levels
                    where l.Owner.ToLower() == owner.ToLower()
                    select l).FirstOrDefault();
            }

            return result;
        }

        // Token: 0x06001760 RID: 5984 RVA: 0x0008EF64 File Offset: 0x0008D164
        public static Level FindExact(string levelName)
        {
            Level level = null;
            lock (Server.levels)
            {
                level = (from lvl in Server.levels
                    where lvl.Owner == ""
                    select lvl).FirstOrDefault(lvl => levelName.ToLower() == lvl.name.ToLower());
                if (level == null)
                    level = (from lvl in Server.levels
                        where lvl.Owner != ""
                        select lvl).FirstOrDefault(lvl => levelName.ToLower() == lvl.name.ToLower());
            }

            return level;
        }

        // Token: 0x06001761 RID: 5985 RVA: 0x0008F02C File Offset: 0x0008D22C
        public bool BlockchangeChecks(Player p, ushort x, ushort y, ushort z, byte type, byte b)
        {
            var str = "start";
            bool result;
            try
            {
                if (x < 0 || y < 0 || z < 0)
                {
                    result = false;
                }
                else if (x >= width || y >= height || z >= depth)
                {
                    result = false;
                }
                else
                {
                    str = "Block rank checking";
                    if (!Block.AllowBreak(b) && !Block.canPlace(p, b) && !Block.BuildIn(b))
                    {
                        p.SendBlockchange(x, y, z, b);
                        result = false;
                    }
                    else if (p.level.mapType == MapType.MyMap && !p.level.IsPublic &&
                             p.group.Permission < LevelPermission.Operator &&
                             p.level.Owner.ToLower() != p.name.ToLower() &&
                             !p.level.AllowedPlayers.Contains(p.name.ToLower()))
                    {
                        Player.SendMessage(p,
                            "You are not allowed to build on this map. Ask the owner for permission.");
                        p.SendBlockchange(x, y, z, b);
                        result = false;
                    }
                    else
                    {
                        str = "Block water in survival checking";
                        if (p.level.mapType == MapType.Lava)
                        {
                            if (blocks[x + width * z + width * depth * y] == 97)
                            {
                                LavaSystem.FoundTreasure(p, x, y, z);
                                Blockchange(x, y, z, 0);
                                return false;
                            }

                            if (type == 14 && !LavaSettings.All.AllowGoldRockOnLavaMaps &&
                                p.group.Permission < LevelPermission.Admin)
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

                                    if (p.spongeBlocks > 0) p.spongeBlocks--;
                                }

                                if (type == 9)
                                {
                                    if (p.waterBlocks <= 0)
                                    {
                                        Player.SendMessage(p, rm.GetString("OutOfWater"));
                                        p.SendBlockchange(x, y, z, b);
                                        return false;
                                    }

                                    if (p.waterBlocks > 0) p.waterBlocks--;
                                }

                                if (type == 220)
                                {
                                    if (p.doorBlocks <= 0)
                                    {
                                        Player.SendMessage(p, rm.GetString("OutOfDoors"));
                                        p.SendBlockchange(x, y, z, b);
                                        return false;
                                    }

                                    if (p.doorBlocks > 0) p.doorBlocks--;
                                }

                                if (LavaSettings.All.DisallowSpongesNearLavaSpawn && (type == 19 || type == 78))
                                {
                                    var @return = false;
                                    LavaSystem.currentLavaMap.LavaSources.ForEach(delegate(LavaSystem.LavaSource ls)
                                    {
                                        if (!@return && x > ls.X - 4 && y > ls.Y - 4 && z > ls.Z - 4 && x < ls.X + 4 &&
                                            y < ls.Y + 4 && z < ls.Z + 4)
                                        {
                                            p.SendBlockchange(x, y, z, b);
                                            p.SendMessage("You are not allowed to place sponges here.");
                                            @return = true;
                                        }
                                    });
                                    if (@return) return false;
                                }
                                else if (LavaSettings.All.DisallowBuildingNearLavaSpawn)
                                {
                                    var @return = false;
                                    LavaSystem.currentLavaMap.LavaSources.ForEach(delegate(LavaSystem.LavaSource ls)
                                    {
                                        if (!@return && x > ls.X - 3 && y > ls.Y - 3 && z > ls.Z - 3 && x < ls.X + 3 &&
                                            y < ls.Y + 3 && z < ls.Z + 3)
                                        {
                                            p.SendBlockchange(x, y, z, b);
                                            p.SendMessage("You are not allowed to build that close to the lava spawn.");
                                            @return = true;
                                        }
                                    });
                                    if (@return) return false;
                                }
                            }

                            str = "If dead check";
                            if (!p.inHeaven && p.lives < 1)
                            {
                                p.SendBlockchange(x, y, z, b);
                                Player.SendMessage2(p, MessagesManager.GetString("WarningBlockChangeGhost"));
                                return false;
                            }

                            str = "Block permission checking";
                            if (p.group.Permission < LevelPermission.Operator && mapType == MapType.Lava &&
                                this != Server.heavenMap)
                            {
                                var num = x + width * z + width * depth * y;
                                if (LavaSettings.All.Antigrief == AntigriefType.BasedOnPlayersLevel)
                                {
                                    if (p.tier <= LavaSettings.All.UpperLevelOfBoplAntigrief &&
                                        ownedBlocks.ContainsKey(num) && p.tier < ownedBlocks[num].tier)
                                    {
                                        var b2 = blocks[num];
                                        if (b2 != 0 && b2 != 194 && b2 != 195 && b2 != 112 && b2 != 80 && b2 != 81 &&
                                            b2 != 82 && b2 != 83 && b2 != 98 && b2 != 74 && b2 != 112 && b2 != 193 &&
                                            b2 != 97)
                                        {
                                            p.SendBlockchange(x, y, z, b);
                                            Player.SendMessage2(p, rm.GetString("WarningBlockProtectedByLevel"));
                                            return false;
                                        }
                                    }
                                }
                                else if (ownedBlocks.ContainsKey(num) && p.name != ownedBlocks[num].playersName)
                                {
                                    var b3 = blocks[num];
                                    if (b3 != 0 && b3 != 194 && b3 != 195 && b3 != 112 && b3 != 80 && b3 != 81 &&
                                        b3 != 82 && b3 != 83 && b3 != 98 && b3 != 74 && b3 != 112 && b3 != 193 &&
                                        b3 != 97)
                                    {
                                        p.SendBlockchange(x, y, z, b);
                                        Player.SendMessage2(p,
                                            string.Format(rm.GetString("WarningBlockOwned"),
                                                ownedBlocks[num].ColoredName + Server.DefaultColor));
                                        return false;
                                    }
                                }
                            }
                        }

                        str = "Zone checking";
                        var flag = true;
                        var flag2 = false;
                        var flag3 = false;
                        var text = "";
                        var list = new List<Zone>();
                        if ((p.group.Permission < LevelPermission.Admin || p.ZoneCheck || p.zoneDel) &&
                            !Block.AllowBreak(b))
                        {
                            if (ZoneList.Count == 0)
                                flag = true;
                            else
                                foreach (var item in ZoneList)
                                    if (item.smallX <= x && x <= item.bigX && item.smallY <= y && y <= item.bigY &&
                                        item.smallZ <= z && z <= item.bigZ)
                                    {
                                        flag3 = true;
                                        if (p.zoneDel)
                                        {
                                            string queryString;
                                            if (p.level.mapType == MapType.MyMap)
                                                queryString = string.Concat("DELETE FROM Zones WHERE Map=",
                                                    p.level.MapDbId, " AND Owner='", item.Owner, "' AND SmallX='",
                                                    item.smallX, "' AND SmallY='", item.smallY, "' AND SmallZ='",
                                                    item.smallZ, "' AND BigX='", item.bigX, "' AND BigY='", item.bigY,
                                                    "' AND BigZ='", item.bigZ, "'");
                                            else
                                                queryString = string.Concat("DELETE FROM Zone", p.level.name,
                                                    " WHERE Owner='", item.Owner, "' AND SmallX='", item.smallX,
                                                    "' AND SmallY='", item.smallY, "' AND SmallZ='", item.smallZ,
                                                    "' AND BigX='", item.bigX, "' AND BigY='", item.bigY,
                                                    "' AND BigZ='", item.bigZ, "'");
                                            DBInterface.ExecuteQuery(queryString);
                                            list.Add(item);
                                            p.SendBlockchange(x, y, z, b);
                                            Player.SendMessage(p,
                                                string.Format(rm.GetString("ZoneDeleted"), item.Owner));
                                            flag2 = true;
                                        }
                                        else if (item.Owner.Substring(0, 3) == "grp")
                                        {
                                            if (Group.Find(item.Owner.Substring(3)).Permission <= p.group.Permission &&
                                                !p.ZoneCheck)
                                            {
                                                flag = true;
                                                break;
                                            }

                                            flag = false;
                                            text = text + ", " + item.Owner.Substring(3);
                                        }
                                        else
                                        {
                                            if (item.Owner.ToLower() == p.name.ToLower() && !p.ZoneCheck)
                                            {
                                                flag = true;
                                                break;
                                            }

                                            flag = false;
                                            text = text + ", " + item.Owner;
                                        }
                                    }

                            if (p.zoneDel)
                            {
                                if (!flag2)
                                    Player.SendMessage(p, rm.GetString("ZoneNotFoundNotDeleted"));
                                else
                                    foreach (var item2 in list)
                                        ZoneList.Remove(item2);
                                p.zoneDel = false;
                                return false;
                            }

                            if (!flag || p.ZoneCheck)
                            {
                                if (text != "")
                                    Player.SendMessage(p,
                                        string.Format(rm.GetString("ZoneBelongsTo"), text.Remove(0, 2)));
                                else
                                    Player.SendMessage(p, rm.GetString("ZoneBelongsToNoOne"));
                                p.ZoneSpam = DateTime.Now;
                                p.SendBlockchange(x, y, z, b);
                                if (p.ZoneCheck && !p.staticCommands) p.ZoneCheck = false;
                                return false;
                            }
                        }

                        str = "Map rank checking";
                        if (text == "" && p.group.Permission < permissionbuild && (!flag3 || !flag))
                        {
                            p.SendBlockchange(x, y, z, b);
                            Player.SendMessage(p,
                                string.Format(rm.GetString("WarningBuildPermission"),
                                    PermissionToName(permissionbuild)));
                            result = false;
                        }
                        else
                        {
                            if (b == 19 && physics > 0 && type != 19) PhysUniversalSpongeRemoved(PosToInt(x, y, z));
                            if (b == 78 && physics > 0 && type != 78) PhysUniversalSpongeRemoved(PosToInt(x, y, z));
                            result = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
                Server.s.Log("Error location: " + str);
                result = false;
            }

            return result;
        }

        // Token: 0x06001762 RID: 5986 RVA: 0x0008FFB4 File Offset: 0x0008E1B4
        public void BlockchangeAftercheck(Player p, ushort x, ushort y, ushort z, byte type, byte b)
        {
            var str = "Undo buffer filling";
            try
            {
                var item = default(Player.UndoPos);
                item.x = x;
                item.y = y;
                item.z = z;
                item.mapName = name;
                item.type = b;
                item.newtype = type;
                item.timePlaced = DateTime.Now;
                p.UndoBuffer.Add(item);
                str = "Setting tile";
                p.loginBlocks++;
                p.overallBlocks++;
                SetTile(x, y, z, type);
                if (mapType == MapType.Lava && this != Server.heavenMap)
                {
                    var key = x + width * z + width * depth * y;
                    if (!ownedBlocks.ContainsKey(key))
                    {
                        ownedBlocks.Add(key,
                            new OwnedBlockInfo(p.PublicName, p.color, "", (int) p.group.Permission, p.tier));
                    }
                    else
                    {
                        ownedBlocks.Remove(key);
                        ownedBlocks.Add(key,
                            new OwnedBlockInfo(p.PublicName, p.color, "", (int) p.group.Permission, p.tier));
                    }
                }

                str = "Changing grass into dirt";
                if (GetTile(x, (ushort) (y - 1), z) == 2 && GrassDestroy && !Block.LightPass(type))
                    Blockchange(p, x, (ushort) (y - 1), z, 3);
                str = "Adding physics";
                if (physics > 0 && Block.Physics(type)) AddCheck(PosToInt(x, y, z));
                changed = true;
                backDup = false;
            }
            catch (Exception ex)
            {
                Server.ErrorLog("Error location: " + str);
                Server.ErrorLog(ex);
            }
        }

        // Token: 0x06001763 RID: 5987 RVA: 0x000901A8 File Offset: 0x0008E3A8
        public void RemoveOwnedBlocks(string playersName)
        {
            lock (ownedBlocks.SyncRoot)
            {
                foreach (var keyValuePair in (from x in ownedBlocks
                    where x.Value.playersName == playersName
                    select x).ToList())
                    ownedBlocks.Remove(keyValuePair.Key);
            }
        }

        // Token: 0x06001764 RID: 5988 RVA: 0x0009025C File Offset: 0x0008E45C
        public void Blockchange(Player p, ushort x, ushort y, ushort z, byte type)
        {
            Blockchange(p, x, y, z, type, true);
        }

        // Token: 0x06001765 RID: 5989 RVA: 0x0009026C File Offset: 0x0008E46C
        public void Blockchange(Player p, ushort x, ushort y, ushort z, byte type, bool addaction)
        {
            var str = "start";
            bool return2;
            bool @return;
            byte[] buffer;
            byte blockmensionBlock;
            byte cpeBlock;
            byte classicBlock;
            var item = default(Player.UndoPos);
            while (true)
                try
                {
                    if (x < 0 || y < 0 || z < 0 || x >= width || y >= height || z >= depth) return;
                    var b = GetTile(x, y, z);
                    str = "Block rank checking";
                    if (!Block.AllowBreak(b) && !Block.canPlace(p, b) && !Block.BuildIn(b))
                    {
                        p.SendBlockchange(x, y, z, b);
                        return;
                    }

                    if (p.level.mapType == MapType.MyMap && !p.level.IsPublic &&
                        p.group.Permission < LevelPermission.Operator && p.level.Owner.ToLower() != p.name.ToLower() &&
                        !p.level.AllowedPlayers.Contains(p.name.ToLower()))
                    {
                        Player.SendMessage(p,
                            "You are not allowed to build on this map. Ask the owner for permission.");
                        p.SendBlockchange(x, y, z, b);
                        return;
                    }

                    str = "Block water in survival checking";
                    if (p.level.mapType == MapType.Lava)
                    {
                        if (type == 14 && !LavaSettings.All.AllowGoldRockOnLavaMaps &&
                            p.group.Permission < LevelPermission.Admin)
                        {
                            Player.SendMessage(p, rm.GetString("NotAllowedToPlaceGoldRock"));
                            p.SendBlockchange(x, y, z, 0);
                            return;
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

                                    if (p.spongeBlocks > 0) p.spongeBlocks--;
                                    break;
                                case 9:
                                    if (p.waterBlocks <= 0)
                                    {
                                        Player.SendMessage(p, rm.GetString("OutOfWater"));
                                        p.SendBlockchange(x, y, z, b);
                                        return;
                                    }

                                    if (p.waterBlocks > 0) p.waterBlocks--;
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

                                        if (p.doorBlocks > 0) p.doorBlocks--;
                                    }

                                    break;
                            }

                            if (LavaSettings.All.DisallowSpongesNearLavaSpawn && (type == 19 || type == 78))
                            {
                                return2 = false;
                                LavaSystem.currentLavaMap.LavaSources.ForEach(delegate(LavaSystem.LavaSource ls)
                                {
                                    if (!return2 && x > ls.X - 4 && y > ls.Y - 4 && z > ls.Z - 4 && x < ls.X + 4 &&
                                        y < ls.Y + 4 && z < ls.Z + 4)
                                    {
                                        p.SendBlockchange(x, y, z, b);
                                        p.SendMessage("You are not allowed to place sponges here.");
                                        return2 = true;
                                    }
                                });
                                if (return2) return;
                            }
                            else if (LavaSettings.All.DisallowBuildingNearLavaSpawn)
                            {
                                @return = false;
                                LavaSystem.currentLavaMap.LavaSources.ForEach(delegate(LavaSystem.LavaSource ls)
                                {
                                    if (!@return && x > ls.X - 3 && y > ls.Y - 3 && z > ls.Z - 3 && x < ls.X + 3 &&
                                        y < ls.Y + 3 && z < ls.Z + 3)
                                    {
                                        p.SendBlockchange(x, y, z, b);
                                        p.SendMessage("You are not allowed to build that close to the lava spawn.");
                                        @return = true;
                                    }
                                });
                                if (@return) return;
                            }
                        }

                        str = "If dead check";
                        if (!p.inHeaven && p.lives < 1)
                        {
                            p.SendBlockchange(x, y, z, b);
                            Player.SendMessage2(p, MessagesManager.GetString("WarningBlockChangeGhost"));
                            return;
                        }

                        str = "Block permission checking";
                        if (p.group.Permission < LevelPermission.Operator && mapType == MapType.Lava &&
                            this != Server.heavenMap)
                        {
                            var num = x + width * z + width * depth * y;
                            if (LavaSettings.All.Antigrief == AntigriefType.BasedOnPlayersLevel)
                            {
                                if (p.tier <= LavaSettings.All.UpperLevelOfBoplAntigrief &&
                                    ownedBlocks.ContainsKey(num) && p.tier < ownedBlocks[num].tier)
                                {
                                    var b2 = blocks[num];
                                    if (b2 != 0 && b2 != 194 && b2 != 195 && b2 != 112 && b2 != 80 && b2 != 81 &&
                                        b2 != 82 && b2 != 83 && b2 != 98 && b2 != 112 && b2 != 74 && b2 != 193 &&
                                        b2 != 97)
                                    {
                                        p.SendBlockchange(x, y, z, b);
                                        Player.SendMessage2(p, rm.GetString("WarningBlockProtectedByLevel"));
                                        return;
                                    }
                                }
                            }
                            else if (ownedBlocks.ContainsKey(num) && p.PublicName != ownedBlocks[num].playersName)
                            {
                                var b3 = blocks[num];
                                if (b3 != 0 && b3 != 194 && b3 != 195 && b3 != 112 && b3 != 80 && b3 != 81 &&
                                    b3 != 82 && b3 != 83 && b3 != 98 && b3 != 112 && b3 != 74 && b3 != 193 && b3 != 97)
                                {
                                    p.SendBlockchange(x, y, z, b);
                                    Player.SendMessage2(p,
                                        string.Format(rm.GetString("WarningBlockOwned"),
                                            ownedBlocks[num].ColoredName + Server.DefaultColor));
                                    return;
                                }
                            }
                        }
                    }

                    str = "Zone checking";
                    var flag = true;
                    var flag2 = false;
                    var flag3 = false;
                    var text = "";
                    var list = new List<Zone>();
                    if ((p.group.Permission < LevelPermission.Admin || p.ZoneCheck || p.zoneDel) &&
                        !Block.AllowBreak(b))
                    {
                        if (ZoneList.Count == 0)
                        {
                            flag = true;
                        }
                        else
                        {
                            var list2 = new List<Zone>(ZoneList);
                            foreach (var item2 in list2)
                            {
                                if (item2.smallX > x || x > item2.bigX || item2.smallY > y || y > item2.bigY ||
                                    item2.smallZ > z || z > item2.bigZ) continue;
                                flag3 = true;
                                if (p.zoneDel)
                                {
                                    try
                                    {
                                        DBInterface.ExecuteQuery("DELETE FROM `Zone" + p.level.name +
                                                                 "` WHERE Owner='" + item2.Owner + "' AND SmallX='" +
                                                                 item2.smallX + "' AND SMALLY='" + item2.smallY +
                                                                 "' AND SMALLZ='" + item2.smallZ + "' AND BIGX='" +
                                                                 item2.bigX + "' AND BIGY='" + item2.bigY +
                                                                 "' AND BIGZ='" + item2.bigZ + "'");
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
                                    if (Group.Find(item2.Owner.Substring(3)).Permission <= p.group.Permission &&
                                        !p.ZoneCheck)
                                    {
                                        flag = true;
                                        break;
                                    }

                                    flag = false;
                                    text = text + ", " + item2.Owner.Substring(3);
                                }
                                else
                                {
                                    if (item2.Owner.ToLower() == p.name.ToLower() && !p.ZoneCheck)
                                    {
                                        flag = true;
                                        break;
                                    }

                                    flag = false;
                                    text = text + ", " + item2.Owner;
                                }
                            }
                        }

                        if (p.zoneDel)
                        {
                            if (!flag2)
                                Player.SendMessage(p, rm.GetString("ZoneNotFoundNotDeleted"));
                            else
                                foreach (var item3 in list)
                                    ZoneList.Remove(item3);
                            p.zoneDel = false;
                            return;
                        }

                        if (!flag || p.ZoneCheck)
                        {
                            if (text != "")
                                Player.SendMessage(p, string.Format(rm.GetString("ZoneBelongsTo"), text.Remove(0, 2)));
                            else
                                Player.SendMessage(p, rm.GetString("ZoneBelongsToNoOne"));
                            p.ZoneSpam = DateTime.Now;
                            p.SendBlockchange(x, y, z, b);
                            if (p.ZoneCheck && !p.staticCommands) p.ZoneCheck = false;
                            return;
                        }
                    }

                    str = "Map rank checking";
                    if (text == "" && p.group.Permission < permissionbuild && (!flag3 || !flag))
                    {
                        p.SendBlockchange(x, y, z, b);
                        Player.SendMessage(p,
                            string.Format(rm.GetString("WarningBuildPermission"), PermissionToName(permissionbuild)));
                        return;
                    }

                    str = "Block sending";
                    if (Block.Convert(b) != Block.Convert(type) && !Instant)
                    {
                        buffer = new byte[7];
                        HTNO(x).CopyTo(buffer, 0);
                        HTNO(y).CopyTo(buffer, 2);
                        HTNO(z).CopyTo(buffer, 4);
                        blockmensionBlock = Block.Convert(type);
                        cpeBlock = Block.ConvertFromBlockmension(blockmensionBlock);
                        classicBlock = Block.ConvertExtended(cpeBlock);
                        Player.players.ForEachSync(delegate(Player pl)
                        {
                            try
                            {
                                if (pl.level == this)
                                {
                                    if (!pl.IsCpeSupported)
                                        buffer[6] = classicBlock;
                                    else if (pl.Cpe.Blockmension == 1)
                                        buffer[6] = blockmensionBlock;
                                    else
                                        buffer[6] = cpeBlock;
                                    pl.SendRaw(6, buffer);
                                }
                            }
                            catch
                            {
                            }
                        });
                    }

                    if (b == 19 && physics > 0 && type != 19) PhysUniversalSpongeRemoved(PosToInt(x, y, z));
                    if (b == 78 && physics > 0 && type != 78) PhysUniversalSpongeRemoved(PosToInt(x, y, z));
                    str = "Undo buffer filling";
                    item.x = x;
                    item.y = y;
                    item.z = z;
                    item.mapName = name;
                    item.type = b;
                    item.newtype = type;
                    item.timePlaced = DateTime.Now;
                    p.UndoBuffer.Add(item);
                    str = "Setting tile";
                    p.loginBlocks++;
                    p.overallBlocks++;
                    SetTile(x, y, z, type);
                    if (mapType == MapType.Lava && this != Server.heavenMap)
                    {
                        var key = x + width * z + width * depth * y;
                        if (!ownedBlocks.ContainsKey(key))
                        {
                            ownedBlocks.Add(key,
                                new OwnedBlockInfo(p.PublicName, p.color, "", (int) p.group.Permission, p.tier));
                        }
                        else
                        {
                            ownedBlocks.Remove(key);
                            ownedBlocks.Add(key,
                                new OwnedBlockInfo(p.PublicName, p.color, "", (int) p.group.Permission, p.tier));
                        }
                    }

                    str = "Growing grass";
                    if (GetTile(x, (ushort) (y - 1), z) == 2 && GrassDestroy && !Block.LightPass(type))
                        Blockchange(p, x, (ushort) (y - 1), z, 3);
                    str = "Adding physics";
                    if (physics > 0 && Block.Physics(type)) AddCheck(PosToInt(x, y, z));
                    changed = true;
                    backDup = false;
                    return;
                }
                catch (OutOfMemoryException)
                {
                    Player.SendMessage(p, "Undo buffer too big! Cleared!");
                    p.UndoBuffer.Clear();
                }
                catch (Exception ex3)
                {
                    Server.ErrorLog("Error location: " + str);
                    Server.ErrorLog(ex3);
                    return;
                }
        }

        // Token: 0x06001766 RID: 5990 RVA: 0x00091428 File Offset: 0x0008F628
        public void Blockchange(HashSet<BlockInfo> blocks)
        {
            var num = 1024;
            var count = blocks.Count;
            var num2 = count / num;
            if (count % num != 0) num2++;
            for (var i = 0; i < num2; i++)
            {
                var num3 = count - i * num;
                var num4 = num3 >= num ? num : num3;
                var array = new byte[num4 * 8];
                var array2 = new byte[num4 * 8];
                var array3 = new byte[num4 * 8];
                var num5 = 0;
                for (var j = 0; j < num4; j++)
                {
                    var blockInfo = blocks.ElementAt(j + i * num);
                    array[num5] = 6;
                    Player.HTNO(blockInfo.X).CopyTo(array, num5 + 1);
                    Player.HTNO(blockInfo.Y).CopyTo(array, num5 + 3);
                    Player.HTNO(blockInfo.Z).CopyTo(array, num5 + 5);
                    var b = Block.Convert(blockInfo.Type);
                    array[num5 + 7] = b;
                    array3[num5] = 6;
                    Player.HTNO(blockInfo.X).CopyTo(array3, num5 + 1);
                    Player.HTNO(blockInfo.Y).CopyTo(array3, num5 + 3);
                    Player.HTNO(blockInfo.Z).CopyTo(array3, num5 + 5);
                    var b2 = Block.ConvertFromBlockmension(b);
                    array3[num5 + 7] = b2;
                    array2[num5] = 6;
                    Player.HTNO(blockInfo.X).CopyTo(array2, num5 + 1);
                    Player.HTNO(blockInfo.Y).CopyTo(array2, num5 + 3);
                    Player.HTNO(blockInfo.Z).CopyTo(array2, num5 + 5);
                    array2[num5 + 7] = Block.ConvertExtended(b2);
                    num5 += 8;
                }

                Send(array, array3, array2);
            }
        }

        // Token: 0x06001767 RID: 5991 RVA: 0x000915E0 File Offset: 0x0008F7E0
        private void Send(byte[] blockmensionData, byte[] cpeData, byte[] noncpeData)
        {
            Player.players.ForEachSync(delegate(Player p)
            {
                if (p.level != this) return;
                var num = 0;
                while (true)
                    try
                    {
                        if (p.socket != null && !p.sendLock)
                        {
                            if (p.IsCpeSupported)
                            {
                                if (p.Cpe.Blockmension == 1)
                                    p.socket.BeginSend(blockmensionData, 0, blockmensionData.Length, SocketFlags.None,
                                        null, null);
                                else
                                    p.socket.BeginSend(cpeData, 0, cpeData.Length, SocketFlags.None, null, null);
                            }
                            else
                            {
                                p.socket.BeginSend(noncpeData, 0, noncpeData.Length, SocketFlags.None, null, null);
                            }
                        }

                        return;
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
                            return;
                        }

                        if (++num > 3)
                        {
                            p.Disconnect();
                            return;
                        }
                    }
            });
        }

        // Token: 0x06001768 RID: 5992 RVA: 0x00091628 File Offset: 0x0008F828
        public void Blockchange(byte[] blocks)
        {
            Player.players.ForEach(delegate(Player p)
            {
                try
                {
                    if (p.level == this)
                    {
                        var num = 0;
                        while (true)
                            try
                            {
                                if (p.socket != null && !p.sendLock)
                                    p.socket.BeginSend(blocks, 0, blocks.Length, SocketFlags.None, null, null);
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

                                    p.Disconnect();
                                    return;
                                }

                                if (num > 3)
                                {
                                    p.Disconnect();
                                    return;
                                }
                            }
                    }
                }
                catch
                {
                }
            });
        }

        // Token: 0x06001769 RID: 5993 RVA: 0x00091660 File Offset: 0x0008F860
        public void Blockchange(ushort x, ushort y, ushort z, byte type, bool overRide = false, string extraInfo = "")
        {
            if (x < 0 || y < 0 || z < 0) return;
            if (x >= width || y >= height || z >= depth) return;
            var tile = GetTile(x, y, z);
            try
            {
                if (overRide || !Block.OPBlocks(tile) && !Block.OPBlocks(type))
                {
                    if (Block.Convert(tile) != Block.Convert(type))
                    {
                        var buffer = new byte[7];
                        HTNO(x).CopyTo(buffer, 0);
                        HTNO(y).CopyTo(buffer, 2);
                        HTNO(z).CopyTo(buffer, 4);
                        var blockmensionBlock = Block.Convert(type);
                        var cpeBlock = Block.ConvertFromBlockmension(blockmensionBlock);
                        var classicBlock = Block.ConvertExtended(blockmensionBlock);
                        Player.players.ForEachSync(delegate(Player pl)
                        {
                            try
                            {
                                if (pl.level == this)
                                {
                                    if (!pl.IsCpeSupported)
                                        buffer[6] = classicBlock;
                                    else if (pl.Cpe.Blockmension == 1)
                                        buffer[6] = blockmensionBlock;
                                    else
                                        buffer[6] = cpeBlock;
                                    pl.SendRaw(6, buffer);
                                }
                            }
                            catch
                            {
                            }
                        });
                    }

                    if (tile == 78 && physics > 0 && type != 78) PhysUniversalSpongeRemoved(PosToInt(x, y, z));
                    if (tile == 19 && physics > 0 && type != 19) PhysUniversalSpongeRemoved(PosToInt(x, y, z));
                    try
                    {
                        UndoPos undoPos;
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
                    catch (Exception)
                    {
                    }

                    SetTile(x, y, z, type);
                    if (physics > 0 && (Block.Physics(type) || extraInfo != "")) AddCheck(PosToInt(x, y, z), extraInfo);
                }
            }
            catch (Exception)
            {
                SetTile(x, y, z, type);
            }
        }

        // Token: 0x0600176A RID: 5994 RVA: 0x000918D0 File Offset: 0x0008FAD0
        public void skipChange(ushort x, ushort y, ushort z, byte type)
        {
            if (x < 0 || y < 0 || z < 0) return;
            if (x >= width || y >= height || z >= depth) return;
            SetTile(x, y, z, type);
        }

        // Token: 0x0600176B RID: 5995 RVA: 0x00091908 File Offset: 0x0008FB08
        public void Save(bool Override = false)
        {
            Save(Override, false);
        }

        // Token: 0x0600176C RID: 5996 RVA: 0x00091914 File Offset: 0x0008FB14
        public void Save(bool Override, bool lavalvl)
        {
            var levelInfoManager = new LevelInfoManager();
            var levelInfoConverter = new LevelInfoConverter();
            var info = levelInfoConverter.ToRaw(Info);
            levelInfoManager.Save(this, info);
            if (lavalvl) mapType = MapType.Lava;
            var text = directoryPath + "/" + name + ".lvl";
            if (mapType == MapType.Lava) directoryPath = "lava/maps/" + name + ".lvl";
            try
            {
                if (!Directory.Exists("levels/level properties")) Directory.CreateDirectory("levels/level properties");
                if (mapType == MapType.Lava && !Directory.Exists("lava/maps")) Directory.CreateDirectory("lava/maps");
                if (changed || !File.Exists(text) || Override)
                {
                    using (var fileStream = File.Create(text + ".back"))
                    {
                        using (var gzipStream = new GZipStream(fileStream, CompressionMode.Compress))
                        {
                            var array = new byte[16];
                            BitConverter.GetBytes(1874).CopyTo(array, 0);
                            gzipStream.Write(array, 0, 2);
                            BitConverter.GetBytes(width).CopyTo(array, 0);
                            BitConverter.GetBytes(depth).CopyTo(array, 2);
                            BitConverter.GetBytes(height).CopyTo(array, 4);
                            BitConverter.GetBytes(spawnx).CopyTo(array, 6);
                            BitConverter.GetBytes(spawnz).CopyTo(array, 8);
                            BitConverter.GetBytes(spawny).CopyTo(array, 10);
                            array[12] = rotx;
                            array[13] = roty;
                            array[14] = (byte) permissionvisit;
                            array[15] = (byte) permissionbuild;
                            gzipStream.Write(array, 0, array.Length);
                            var array2 = new byte[blocks.Length];
                            for (var i = 0; i < blocks.Length; i++)
                                if (blocks[i] < 80)
                                    array2[i] = blocks[i];
                                else
                                    array2[i] = Block.SaveConvert(blocks[i]);
                            gzipStream.Write(array2, 0, array2.Length);
                        }
                    }

                    File.Delete(text + ".backup");
                    File.Copy(text + ".back", text + ".backup");
                    File.Delete(text);
                    File.Move(text + ".back", text);
                    StreamWriter streamWriter;
                    if (mapType == MapType.Lava)
                        streamWriter = new StreamWriter(File.Create("lava/maps/" + name + ".cfg"));
                    else if (mapType == MapType.Freebuild)
                        streamWriter = new StreamWriter(File.Create("levels/level properties/" + name + ".properties"));
                    else if (mapType == MapType.Home)
                        streamWriter = new StreamWriter(File.Create("maps/home/" + name + ".properties"));
                    else if (mapType == MapType.Zombie)
                        streamWriter = new StreamWriter(File.Create("infection/maps/" + name + ".properties"));
                    else
                        streamWriter = new StreamWriter(File.Create(directoryPath + "/" + name + ".properties"));
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
                        Server.s.Log("Map \"" + name + "\" was saved as lava map.");
                    else if (mapType == MapType.Freebuild || mapType == MapType.Zombie)
                        Server.s.Log(string.Concat("SAVED: Level \"", name, "\". (", PlayersCount, "/",
                            Player.players.Count, "/", Server.players, ")"));
                    else if (mapType == MapType.Home)
                        Server.s.Log("Map \"" + name + "\" was saved as a home map.");
                    else if (mapType == MapType.MyMap) Server.s.Log("Map \"" + name + "\" was saved.");
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

        // Token: 0x0600176D RID: 5997 RVA: 0x000920C0 File Offset: 0x000902C0
        public int Backup(bool Forced = false, string backupName = "")
        {
            if (!IsMapBeingBackuped)
            {
                Server.s.Log("You cannot create a backup of this type of map.");
                return -1;
            }

            if (!backDup || Forced)
            {
                var num = 1;
                var text = Server.backupLocation;
                if (mapType == MapType.Home)
                    text = "maps/home/backup";
                else if (mapType == MapType.MyMap) text = directoryPath + "/backup";
                if (Directory.Exists(text + "/" + name))
                    num = Directory.GetDirectories(text + "/" + name).Length + 1;
                else
                    Directory.CreateDirectory(text + "/" + name);
                var text2 = string.Concat(text, "/", name, "/", num);
                if (backupName != "") text2 = string.Concat(text, "/", name, "/", backupName);
                Directory.CreateDirectory(text2);
                var destFileName = text2 + "/" + name + ".lvl";
                var sourceFileName = "levels/" + name + ".lvl";
                if (mapType == MapType.Home || mapType == MapType.MyMap)
                    sourceFileName = directoryPath + "/" + name + ".lvl";
                try
                {
                    File.Copy(sourceFileName, destFileName, true);
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

        // Token: 0x0600176E RID: 5998 RVA: 0x000922B8 File Offset: 0x000904B8
        public int LavaMapBackup(string backupName = "")
        {
            var num = 1;
            var text = "lava/maps/backup";
            if (Directory.Exists(text + "/" + name))
                num = Directory.GetDirectories(text + "/" + name).Length + 1;
            else
                Directory.CreateDirectory(text + "/" + name);
            var text2 = string.Concat(text, "/", name, "/", num);
            if (backupName != "")
            {
                text2 = "lava/maps/";
                try
                {
                    File.Copy(text2 + name + ".lvl", text2 + backupName + ".lvl", true);
                    File.Copy(text2 + name + ".cfg", text2 + backupName + ".cfg", true);
                    backDup = true;
                    return 1;
                }
                catch (Exception ex)
                {
                    Server.ErrorLog(ex);
                    Server.s.Log(string.Concat("FAILED TO SAVE LAVA MAP \"", name, "\" AS \"", backupName, "\""));
                    return -1;
                }
            }

            Directory.CreateDirectory(text2);
            var destFileName = text2 + "/" + name + ".lvl";
            var sourceFileName = "lava/maps/" + name + ".lvl";
            int result;
            try
            {
                File.Copy(sourceFileName, destFileName, true);
                backDup = true;
                result = num;
            }
            catch (Exception ex2)
            {
                Server.ErrorLog(ex2);
                Server.s.Log("FAILED TO INCREMENTAL LAVA MAP BACKUP :" + name);
                result = -1;
            }

            return result;
        }

        // Token: 0x0600176F RID: 5999 RVA: 0x000924A8 File Offset: 0x000906A8
        public void NotifyPopulationChanged()
        {
            if (mapType == MapType.Lava || mapType == MapType.Zombie) return;
            if (!unload) return;
            var proceed = true;
            Player.players.ForEachSync(delegate(Player p)
            {
                if (p.level == this) proceed = false;
            });
            if (!proceed) return;
            Unload();
        }

        // Token: 0x06001770 RID: 6000 RVA: 0x0009250C File Offset: 0x0009070C
        public static Level Load(string givenName)
        {
            return Load(givenName, 0);
        }

        // Token: 0x06001771 RID: 6001 RVA: 0x00092518 File Offset: 0x00090718
        public static Level Load(string givenName, byte phys)
        {
            return Load(givenName, phys, false);
        }

        // Token: 0x06001772 RID: 6002 RVA: 0x00092524 File Offset: 0x00090724
        public static Level Load(string givenName, bool autoUnload)
        {
            return Load(givenName, 0, MapType.Freebuild, autoUnload);
        }

        // Token: 0x06001773 RID: 6003 RVA: 0x00092530 File Offset: 0x00090730
        public static Level Load(string givenName, byte phys, bool lavaSurv)
        {
            if (lavaSurv) return Load(givenName, phys, MapType.Lava);
            return Load(givenName, phys, MapType.Freebuild);
        }

        // Token: 0x06001774 RID: 6004 RVA: 0x00092548 File Offset: 0x00090748
        public static Level Load(string directoryPath, string mapName, string owner, MapType type, bool isAutoUnloading)
        {
            var str = mapName + ".lvl";
            var text = directoryPath + "\\" + str;
            if (!File.Exists(text)) return null;
            var dictionary = new Dictionary<string, object>();
            dictionary.Add("@MapName", mapName);
            dictionary.Add("@Owner", owner);
            var parameters = dictionary;
            var num = 0u;
            using (var dataTable =
                DBInterface.fillData("SELECT * FROM MapList WHERE MapName = @MapName AND Owner = @Owner;", parameters))
            {
                if (dataTable.Rows.Count == 0)
                {
                    string text2 = null;
                    text2 = !Server.useMySQL
                        ? "INSERT INTO MapList (MapName, Owner) VALUES (@MapName, @Owner); SELECT LAST_INSERT_ROWID() AS Id;"
                        : "INSERT INTO MapList (MapName, Owner) VALUES (@MapName, @Owner); SELECT LAST_INSERT_ID() AS Id;";
                    using (var dataTable2 = DBInterface.fillData(text2, parameters))
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
            var parameters2 = dictionary2;
            try
            {
                var level = ReadLevelFile(text);
                level.MapDbId = num;
                level.name = mapName;
                level.directoryPath = directoryPath;
                level.fileName = str;
                level.backDup = true;
                level.setPhysics(0);
                level.IsMapBeingBackuped = true;
                using (var dataTable3 = DBInterface.fillData("SELECT * FROM `Zones` WHERE Map = @Id", parameters2))
                {
                    try
                    {
                        var item = default(Zone);
                        for (var i = 0; i < dataTable3.Rows.Count; i++)
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

                level.jailx = (ushort) (level.spawnx * 32);
                level.jaily = (ushort) (level.spawny * 32);
                level.jailz = (ushort) (level.spawnz * 32);
                level.jailrotx = level.rotx;
                level.jailroty = level.roty;
                try
                {
                    if (Server.useMySQL)
                    {
                        using (var dataTable4 =
                            DBInterface.fillData("SELECT * FROM `Portals` WHERE Map = @Id", parameters2))
                        {
                            for (var j = 0; j < dataTable4.Rows.Count; j++)
                                if (!Block.portal(level.GetTile((ushort) dataTable4.Rows[j]["EntryX"],
                                    (ushort) dataTable4.Rows[j]["EntryY"], (ushort) dataTable4.Rows[j]["EntryZ"])))
                                    DBInterface.ExecuteQuery(
                                        string.Concat("DELETE FROM `Portals` WHERE EntryX=",
                                            dataTable4.Rows[j]["EntryX"], " AND EntryY=", dataTable4.Rows[j]["EntryY"],
                                            " AND EntryZ=", dataTable4.Rows[j]["EntryZ"], " AND Map = @Id"),
                                        parameters2);
                        }

                        using (var dataTable5 =
                            DBInterface.fillData("SELECT * FROM `Messages` WHERE Map = @Id", parameters2))
                        {
                            for (var k = 0; k < dataTable5.Rows.Count; k++)
                                if (!Block.mb(level.GetTile((ushort) dataTable5.Rows[k]["X"],
                                    (ushort) dataTable5.Rows[k]["Y"], (ushort) dataTable5.Rows[k]["Z"])))
                                    DBInterface.ExecuteQuery(
                                        string.Concat("DELETE FROM `Messages` WHERE Map = @Id AND X=",
                                            dataTable5.Rows[k]["X"], " AND Y=", dataTable5.Rows[k]["Y"], " AND Z=",
                                            dataTable5.Rows[k]["Z"]), parameters2);
                        }
                    }
                    else
                    {
                        using (var dataTable6 =
                            DBInterface.fillData("SELECT * FROM `Portals` WHERE Map = @Id", parameters2))
                        {
                            for (var l = 0; l < dataTable6.Rows.Count; l++)
                                if (!Block.portal(level.GetTile(ushort.Parse(dataTable6.Rows[l]["EntryX"].ToString()),
                                    ushort.Parse(dataTable6.Rows[l]["EntryY"].ToString()),
                                    ushort.Parse(dataTable6.Rows[l]["EntryZ"].ToString()))))
                                    DBInterface.ExecuteQuery(
                                        string.Concat("DELETE FROM `Portals` WHERE Map = @Id AND EntryX=",
                                            dataTable6.Rows[l]["EntryX"], " AND EntryY=", dataTable6.Rows[l]["EntryY"],
                                            " AND EntryZ=", dataTable6.Rows[l]["EntryZ"]), parameters2);
                        }

                        using (var dataTable7 =
                            DBInterface.fillData("SELECT * FROM `Messages` WHERE Map = @Id", parameters2))
                        {
                            for (var m = 0; m < dataTable7.Rows.Count; m++)
                                if (!Block.mb(level.GetTile(ushort.Parse(dataTable7.Rows[m]["X"].ToString()),
                                    ushort.Parse(dataTable7.Rows[m]["Y"].ToString()),
                                    ushort.Parse(dataTable7.Rows[m]["Z"].ToString()))))
                                    DBInterface.ExecuteQuery(string.Concat(
                                        "DELETE FROM `Messages` WHERE Map = @Id AND X=", dataTable7.Rows[m]["X"],
                                        " AND Y=", dataTable7.Rows[m]["Y"], " AND Z=", dataTable7.Rows[m]["Z"]));
                        }
                    }
                }
                catch (Exception ex2)
                {
                    Server.ErrorLog(ex2);
                }

                level.mapType = type;
                level.mapOwner = owner.ToLower();
                var mapPropertiesDirectory = text.Replace(".lvl", "") + ".properties";
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

        // Token: 0x06001775 RID: 6005 RVA: 0x00092EC0 File Offset: 0x000910C0
        private static void LoadLevelInfo(Level level)
        {
            var levelInfoManager = new LevelInfoManager();
            var levelInfoRaw = levelInfoManager.Load(level);
            if (levelInfoRaw != null)
            {
                var levelInfoConverter = new LevelInfoConverter();
                level.Info = levelInfoConverter.FromRaw(levelInfoRaw);
                return;
            }

            level.Info = new LevelInfo();
        }

        // Token: 0x06001776 RID: 6006 RVA: 0x00092F00 File Offset: 0x00091100
        public static Level OpenForRaceMode(string directory, string fileName)
        {
            return OpenForRaceMode(directory, fileName, null);
        }

        // Token: 0x06001777 RID: 6007 RVA: 0x00092F0C File Offset: 0x0009110C
        public static Level OpenForRaceMode(string directory, string fileName, LevelOptions levelOptions)
        {
            var text = directory + Path.DirectorySeparatorChar + fileName;
            if (!File.Exists(text)) return null;
            var name = fileName.Substring(0, fileName.LastIndexOf("."));
            var level = ReadLevelFile(text);
            level.name = name;
            level.directoryPath = directory;
            level.fileName = fileName;
            level.setPhysics(0);
            level.backDup = true;
            level.IsMapBeingBackuped = false;
            level.IsPillaringAllowed = false;
            if (levelOptions != null)
            {
                if (levelOptions.PublicName != null) level.name = levelOptions.PublicName;
                var physics = levelOptions.Physics;
                level.setPhysics(levelOptions.Physics);
            }

            return level;
        }

        // Token: 0x06001778 RID: 6008 RVA: 0x00092FAC File Offset: 0x000911AC
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
                    DBInterface.ExecuteQuery("CREATE TABLE if not exists `Rating" + givenName +
                                             "` (Username CHAR(64), Vote TINYINT, INDEX(Vote))");
                    DBInterface.ExecuteQuery("ALTER TABLE `Rating" + givenName + "` MODIFY Username CHAR(64)");
                }
                else
                {
                    DBInterface.ExecuteQuery("CREATE TABLE if not exists `Block" + givenName +
                                             "` (Username TEXT, TimePerformed DATETIME, X INTEGER, Y INTEGER, Z INTEGER, Type INTEGER, Deleted INTEGER)");
                    DBInterface.ExecuteQuery("CREATE INDEX if not exists `BlockIndex" + givenName + "` ON `Block" +
                                             givenName + "` (X, Y, Z)");
                    DBInterface.ExecuteQuery("CREATE TABLE if not exists `Portals" + givenName +
                                             "` (EntryX SMALLINT UNSIGNED, EntryY SMALLINT UNSIGNED, EntryZ SMALLINT UNSIGNED, ExitMap CHAR(20), ExitX SMALLINT UNSIGNED, ExitY SMALLINT UNSIGNED, ExitZ SMALLINT UNSIGNED)");
                    DBInterface.ExecuteQuery("CREATE TABLE if not exists `Messages" + givenName +
                                             "` (X SMALLINT UNSIGNED, Y SMALLINT UNSIGNED, Z SMALLINT UNSIGNED, Message CHAR(255));");
                    DBInterface.ExecuteQuery("CREATE TABLE if not exists `Zone" + givenName +
                                             "` (SmallX SMALLINT UNSIGNED, SmallY SMALLINT UNSIGNED, SmallZ SMALLINT UNSIGNED, BigX SMALLINT UNSIGNED, BigY SMALLINT UNSIGNED, BigZ SMALLINT UNSIGNED, Owner VARCHAR(64));");
                    DBInterface.ExecuteQuery("CREATE TABLE if not exists `Rating" + givenName +
                                             "` (Username CHAR(64), Vote TINYINT)");
                    DBInterface.ExecuteQuery("CREATE INDEX if not exists `RatingIndex" + givenName + "` ON `Rating" +
                                             givenName + "` (Vote)");
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }

            var text = "levels/" + givenName + ".lvl";
            var str = "levels";
            switch (mType)
            {
                case MapType.Lava:
                    str = "lava/maps";
                    text = "lava/maps/" + givenName + ".lvl";
                    break;
                case MapType.Zombie:
                    str = "infection/maps";
                    text = "infection/maps/" + givenName + ".lvl";
                    break;
                case MapType.Home:
                    str = "maps/home";
                    text = "maps/home/" + givenName + ".lvl";
                    break;
            }

            if (!File.Exists(text)) return null;
            try
            {
                var level = ReadLevelFile(text);
                level.name = givenName;
                level.directoryPath = str;
                level.fileName = givenName + ".lvl";
                level.setPhysics(phys);
                level.backDup = true;
                if (mType == MapType.Freebuild || mType == MapType.Home)
                    level.IsMapBeingBackuped = true;
                else
                    level.IsMapBeingBackuped = false;
                using (var dataTable = DBInterface.fillData("SELECT * FROM `Zone" + givenName + "`"))
                {
                    try
                    {
                        var item = default(Zone);
                        for (var i = 0; i < dataTable.Rows.Count; i++)
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

                level.jailx = (ushort) (level.spawnx * 32);
                level.jaily = (ushort) (level.spawny * 32);
                level.jailz = (ushort) (level.spawnz * 32);
                level.jailrotx = level.rotx;
                level.jailroty = level.roty;
                try
                {
                    if (Server.useMySQL)
                        try
                        {
                            using (var dataTable2 = DBInterface.fillData("SELECT * FROM `Portals" + givenName + "`"))
                            {
                                for (var j = 0; j < dataTable2.Rows.Count; j++)
                                    if (!Block.portal(level.GetTile((ushort) dataTable2.Rows[j]["EntryX"],
                                        (ushort) dataTable2.Rows[j]["EntryY"], (ushort) dataTable2.Rows[j]["EntryZ"])))
                                        DBInterface.ExecuteQuery(string.Concat("DELETE FROM `Portals", givenName,
                                            "` WHERE EntryX=", dataTable2.Rows[j]["EntryX"], " AND EntryY=",
                                            dataTable2.Rows[j]["EntryY"], " AND EntryZ=",
                                            dataTable2.Rows[j]["EntryZ"]));
                            }

                            using (var dataTable3 = DBInterface.fillData("SELECT * FROM `Messages" + givenName + "`"))
                            {
                                for (var k = 0; k < dataTable3.Rows.Count; k++)
                                    if (!Block.mb(level.GetTile((ushort) dataTable3.Rows[k]["X"],
                                        (ushort) dataTable3.Rows[k]["Y"], (ushort) dataTable3.Rows[k]["Z"])))
                                        DBInterface.ExecuteQuery(string.Concat("DELETE FROM `Messages", givenName,
                                            "` WHERE X=", dataTable3.Rows[k]["X"], " AND Y=", dataTable3.Rows[k]["Y"],
                                            " AND Z=", dataTable3.Rows[k]["Z"]));
                            }
                        }
                        catch (Exception ex3)
                        {
                            Server.ErrorLog(ex3);
                        }
                    else
                        try
                        {
                            using (var dataTable4 = DBInterface.fillData("SELECT * FROM `Portals" + givenName + "`"))
                            {
                                for (var l = 0; l < dataTable4.Rows.Count; l++)
                                    if (!Block.portal(level.GetTile(
                                        ushort.Parse(dataTable4.Rows[l]["EntryX"].ToString()),
                                        ushort.Parse(dataTable4.Rows[l]["EntryY"].ToString()),
                                        ushort.Parse(dataTable4.Rows[l]["EntryZ"].ToString()))))
                                        DBInterface.ExecuteQuery(string.Concat("DELETE FROM `Portals", givenName,
                                            "` WHERE EntryX=", dataTable4.Rows[l]["EntryX"], " AND EntryY=",
                                            dataTable4.Rows[l]["EntryY"], " AND EntryZ=",
                                            dataTable4.Rows[l]["EntryZ"]));
                            }

                            using (var dataTable5 = DBInterface.fillData("SELECT * FROM `Messages" + givenName + "`"))
                            {
                                for (var m = 0; m < dataTable5.Rows.Count; m++)
                                    if (!Block.mb(level.GetTile(ushort.Parse(dataTable5.Rows[m]["X"].ToString()),
                                        ushort.Parse(dataTable5.Rows[m]["Y"].ToString()),
                                        ushort.Parse(dataTable5.Rows[m]["Z"].ToString()))))
                                        DBInterface.ExecuteQuery(string.Concat("DELETE FROM `Messages", givenName,
                                            "` WHERE X=", dataTable5.Rows[m]["X"], " AND Y=", dataTable5.Rows[m]["Y"],
                                            " AND Z=", dataTable5.Rows[m]["Z"]));
                            }
                        }
                        catch (Exception ex4)
                        {
                            Server.ErrorLog(ex4);
                        }
                }
                catch (Exception ex5)
                {
                    Server.ErrorLog(ex5);
                }

                level.mapType = mType;
                var text2 = "levels/level properties/" + level.name + ".properties";
                if (!File.Exists(text2)) text2 = "levels/level properties/" + level.name;
                if (mType == MapType.Zombie)
                {
                    if (!Directory.Exists("infection/maps/maps properties"))
                        Directory.CreateDirectory("infection/maps/maps properties");
                    text2 = "infection/maps/maps properties/" + level.name + ".properties";
                }
                else if (mType == MapType.Home)
                {
                    text2 = "maps/home/" + level.name + ".properties";
                }
                else if (mType != 0)
                {
                    goto IL_09e5;
                }

                try
                {
                    LoadLevelPropertiesIfExist(level, text2);
                }
                catch (Exception ex6)
                {
                    Server.ErrorLog(ex6);
                }

                IL_09e5:
                if (mType == MapType.Zombie)
                    for (var n = 0; n < level.blocks.Length; n++)
                        if (level.blocks[n] == 9)
                            level.blocks[n] = 106;
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

                level.mapSettingsManager = new MapSettingsManager(str + "/" + givenName + ".cfg.txt");
                level.mapSettingsManager.DeployBlocks(level);
                Server.s.Log("Level \"" + level.name + "\" loaded.");
                if (!Server.CLI) RefreshUnloadedMapsInGUI();
                LoadLevelInfo(level);
                return level;
            }
            catch (Exception ex7)
            {
                Server.ErrorLog(ex7);
                return null;
            }
        }

        // Token: 0x06001779 RID: 6009 RVA: 0x00093B84 File Offset: 0x00091D84
        private static void LoadLevelPropertiesIfExist(Level level, string mapPropertiesDirectory)
        {
            if (File.Exists(mapPropertiesDirectory))
                foreach (var text in File.ReadAllLines(mapPropertiesDirectory))
                {
                    var text2 = text.Trim();
                    if (text2 != "" && text2[0] != '#')
                    {
                        var text3 = text.Substring(text.IndexOf(" = ") + 3);
                        string key;
                        switch (key = text.Substring(0, text.IndexOf(" = ")).ToLower())
                        {
                            case "theme":
                                level.theme = text3;
                                break;
                            case "physics":
                                level.setPhysics(int.Parse(text3));
                                break;
                            case "physics speed":
                                if (level.mapType != MapType.Lava) level.speedPhysics = int.Parse(text3);
                                break;
                            case "physics overload":
                                if (level.mapType == MapType.Lava)
                                    level.overload = 900000;
                                else
                                    level.overload = int.Parse(text3);
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
                                    level.permissionbuild = PermissionFromName(text3);
                                break;
                            case "pervisit":
                                if (PermissionFromName(text3) != LevelPermission.Null)
                                    level.permissionvisit = PermissionFromName(text3);
                                break;
                            case "allowed":
                                foreach (var text4 in text3.Split(new[]
                                {
                                    ','
                                }, StringSplitOptions.RemoveEmptyEntries))
                                    level.AllowedPlayers.Add(text4.Trim().ToLower());
                                break;
                            case "public":
                                level.IsPublic = bool.Parse(text3);
                                break;
                        }
                    }
                }
        }

        // Token: 0x0600177A RID: 6010 RVA: 0x00093F5C File Offset: 0x0009215C
        public static LevelFileInfo ReadLevelInfo(string fullPath)
        {
            var levelFileInfo = new LevelFileInfo();
            using (var fileStream = File.OpenRead(fullPath))
            {
                using (var gzipStream = new GZipStream(fileStream, CompressionMode.Decompress))
                {
                    var array = new byte[2];
                    gzipStream.Read(array, 0, array.Length);
                    var num = BitConverter.ToUInt16(array, 0);
                    if (num == 1874)
                    {
                        var array2 = new byte[16];
                        gzipStream.Read(array2, 0, array2.Length);
                        levelFileInfo.Width = BitConverter.ToUInt16(array2, 0);
                        levelFileInfo.Height = BitConverter.ToUInt16(array2, 2);
                        levelFileInfo.Depth = BitConverter.ToUInt16(array2, 4);
                        levelFileInfo.Spawn = new PlayerPosition(BitConverter.ToUInt16(array2, 6),
                            BitConverter.ToUInt16(array2, 8), BitConverter.ToUInt16(array2, 10), array2[12],
                            array2[13]);
                    }
                    else
                    {
                        var array3 = new byte[12];
                        gzipStream.Read(array3, 0, array3.Length);
                        levelFileInfo.Width = num;
                        levelFileInfo.Height = BitConverter.ToUInt16(array3, 0);
                        levelFileInfo.Depth = BitConverter.ToUInt16(array3, 2);
                        levelFileInfo.Spawn = new PlayerPosition(BitConverter.ToUInt16(array3, 4),
                            BitConverter.ToUInt16(array3, 6), BitConverter.ToUInt16(array3, 8), array3[10], array3[11]);
                    }
                }
            }

            return levelFileInfo;
        }

        // Token: 0x0600177B RID: 6011 RVA: 0x000940CC File Offset: 0x000922CC
        private static Level ReadLevelFile(string fullPath)
        {
            Level level;
            using (var fileStream = File.OpenRead(fullPath))
            {
                using (var gzipStream = new GZipStream(fileStream, CompressionMode.Decompress))
                {
                    var array = new byte[2];
                    gzipStream.Read(array, 0, array.Length);
                    var num = BitConverter.ToUInt16(array, 0);
                    if (num == 1874)
                    {
                        var array2 = new byte[16];
                        gzipStream.Read(array2, 0, array2.Length);
                        var x = BitConverter.ToUInt16(array2, 0);
                        var z = BitConverter.ToUInt16(array2, 2);
                        var y = BitConverter.ToUInt16(array2, 4);
                        level = new Level("temp", x, y, z, "empty");
                        level.spawnx = BitConverter.ToUInt16(array2, 6);
                        level.spawnz = BitConverter.ToUInt16(array2, 8);
                        level.spawny = BitConverter.ToUInt16(array2, 10);
                        level.rotx = array2[12];
                        level.roty = array2[13];
                    }
                    else
                    {
                        var array3 = new byte[12];
                        gzipStream.Read(array3, 0, array3.Length);
                        var x2 = num;
                        var z2 = BitConverter.ToUInt16(array3, 0);
                        var y2 = BitConverter.ToUInt16(array3, 2);
                        level = new Level("temp", x2, y2, z2, "grass");
                        level.spawnx = BitConverter.ToUInt16(array3, 4);
                        level.spawnz = BitConverter.ToUInt16(array3, 6);
                        level.spawny = BitConverter.ToUInt16(array3, 8);
                        level.rotx = array3[10];
                        level.roty = array3[11];
                    }

                    var array4 = new byte[level.width * level.depth * level.height];
                    gzipStream.Read(array4, 0, array4.Length);
                    level.blocks = array4;
                }
            }

            return level;
        }

        // Token: 0x0600177C RID: 6012 RVA: 0x000942A4 File Offset: 0x000924A4
        public static bool IsLevelLoaded(string mapName)
        {
            foreach (var level in Server.levels)
                if (level.name == mapName)
                    return true;
            return false;
        }

        // Token: 0x0600177D RID: 6013 RVA: 0x00094304 File Offset: 0x00092504
        public void ChatLevel(string message)
        {
            for (var i = 0; i < Player.players.Count; i++)
                try
                {
                    if (Player.players[i].level == this) Player.players[i].SendMessage(message);
                }
                catch (Exception ex)
                {
                    Server.ErrorLog(ex);
                }
        }

        // Token: 0x0600177E RID: 6014 RVA: 0x00094368 File Offset: 0x00092568
        internal void ChatLevel(byte type, string message)
        {
            var enumerable = from p in Player.players.GetCopy()
                where p.level == this
                select p;
            foreach (var player in enumerable)
                if (player.Cpe.MessageTypes == 1)
                    Player.SendMessage(player, type, message);
                else
                    Player.SendMessage(player, message);
        }

        // Token: 0x0600177F RID: 6015 RVA: 0x000943E4 File Offset: 0x000925E4
        internal void ChatLevelCpe(V1.MessageType type, V1.MessageOptions options, string message)
        {
            var list = (from p in Player.players.GetCopy()
                where p.level == this
                select p).ToList();
            foreach (var player in list)
                if (player.Cpe.MessageTypes == 1)
                    V1.SendMessage(player, type, options, message);
                else if (type == V1.MessageType.Announcement) Player.SendMessage(player, message);
        }

        // Token: 0x06001780 RID: 6016 RVA: 0x00094474 File Offset: 0x00092674
        internal void ChatLevel(byte type, TimeSpan delay, string message, bool includeNonCpe = true)
        {
            var playersHere = (from p in Player.players.GetCopy()
                where p.level == this
                select p).ToList();
            foreach (var player in playersHere)
                if (player.Cpe.MessageTypes == 1)
                    V1.SendMessage(player, (V1.MessageType) type, new V1.MessageOptions
                    {
                        MinDisplayTime = delay
                    }, message);
                else if (includeNonCpe) Player.SendMessage(player, message);
            System.Threading.Timer timer = null;
            timer = new System.Threading.Timer(delegate
            {
                foreach (var player2 in playersHere)
                    if (player2.Cpe.MessageTypes == 1)
                        Player.SendMessage(player2, type, "");
                timer.Dispose();
            }, null, delay, TimeSpan.FromMilliseconds(-1.0));
        }

        // Token: 0x06001781 RID: 6017 RVA: 0x00094560 File Offset: 0x00092760
        public void ReloadSettings()
        {
            mapSettingsManager.Reload();
            mapSettingsManager.DeployBlocks(this);
            commandActionsHit = null;
            commandActionsWalk = null;
            commandActionsBuild = null;
        }

        // Token: 0x06001782 RID: 6018 RVA: 0x00094590 File Offset: 0x00092790
        public static string GetPhysicsNameByNumber(int number)
        {
            if (number == 0) return "Off";
            if (number == 1) return "1";
            if (number == 2) return "2";
            if (number == 3) return "3";
            if (number == 4) return "Door";
            return "Unknown";
        }

        // Token: 0x06001783 RID: 6019 RVA: 0x000945C8 File Offset: 0x000927C8
        public void setPhysics(int physics)
        {
            if (this.physics == 0 && physics != 0)
            {
                for (var i = 0; i < blocks.Length; i++)
                    if (Block.NeedRestart(blocks[i]))
                        AddCheck(i);
                this.physics = physics;
                StartPhysics();
                return;
            }

            this.physics = physics;
        }

        // Token: 0x06001784 RID: 6020 RVA: 0x00094624 File Offset: 0x00092824
        public void StartPhysics()
        {
            physicsTimer.Start();
        }

        // Token: 0x06001785 RID: 6021 RVA: 0x00094634 File Offset: 0x00092834
        public void StopPhysics()
        {
            physicsTimer.Stop();
        }

        // Token: 0x06001786 RID: 6022 RVA: 0x00094644 File Offset: 0x00092844
        public void Physics()
        {
            var num = speedPhysics;
            while (!unloaded)
                try
                {
                    while (true)
                    {
                        if (num > 0) Thread.Sleep(num);
                        if (physics == 0)
                        {
                            Thread.Sleep(5000);
                        }
                        else if (ListCheck.Count != 0)
                        {
                            if (!GeneralSettings.All.IntelliSys ||
                                InteliSys.pendingPacketsAvg <= GeneralSettings.All.AvgStop) break;
                            Thread.Sleep(1000);
                        }
                    }

                    var now = DateTime.Now;
                    CalcPhysics();
                    var timeSpan = DateTime.Now - now;
                    num = speedPhysics - (int) timeSpan.TotalMilliseconds;
                    if (num >= (int) (-overload * 0.75f)) continue;
                    if (num < -overload)
                    {
                        if (!Server.physicsRestart) setPhysics(0);
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

        // Token: 0x06001787 RID: 6023 RVA: 0x000947B8 File Offset: 0x000929B8
        public int PosToInt(ushort x, ushort y, ushort z)
        {
            if (x < 0) return -1;
            if (x >= width) return -1;
            if (y < 0) return -1;
            if (y >= height) return -1;
            if (z < 0) return -1;
            if (z >= depth) return -1;
            return x + z * width + y * width * depth;
        }

        // Token: 0x06001788 RID: 6024 RVA: 0x00094814 File Offset: 0x00092A14
        public int PosToIntUnchecked(int x, int y, int z)
        {
            return x + z * width + y * width * depth;
        }

        // Token: 0x06001789 RID: 6025 RVA: 0x00094830 File Offset: 0x00092A30
        public int PosToInt(int x, int y, int z)
        {
            if (x < 0) return -1;
            if (x >= width) return -1;
            if (y < 0) return -1;
            if (y >= height) return -1;
            if (z < 0) return -1;
            if (z >= depth) return -1;
            return x + z * width + y * width * depth;
        }

        // Token: 0x0600178A RID: 6026 RVA: 0x0009488C File Offset: 0x00092A8C
        public void IntToPos(int pos, out ushort x, out ushort y, out ushort z)
        {
            y = (ushort) (pos / width / depth);
            pos -= y * width * depth;
            z = (ushort) (pos / width);
            pos -= z * width;
            x = (ushort) pos;
        }

        // Token: 0x0600178B RID: 6027 RVA: 0x000948E0 File Offset: 0x00092AE0
        public int IntOffset(int pos, int x, int y, int z)
        {
            return pos + x + z * width + y * width * depth;
        }

        // Token: 0x0600178C RID: 6028 RVA: 0x00094900 File Offset: 0x00092B00
        public string foundInfo(ushort x, ushort y, ushort z)
        {
            Check check = null;
            try
            {
                check = ListCheck.Find(Check => Check.b == PosToInt(x, y, z));
            }
            catch
            {
            }

            if (check != null) return check.extraInfo;
            return "";
        }

        // Token: 0x0600178D RID: 6029 RVA: 0x00094974 File Offset: 0x00092B74
        public void CalcPhysics()
        {
            try
            {
                if (physics <= 0 || Server.pause) return;
                var rand = new Random();
                lastCheck = ListCheck.Count;
                ushort x;
                ushort y;
                ushort z;
                var foundPlayer = default(Player);
                var foundNum = default(int);
                int mx;
                int my;
                int mz;
                var currentNum = default(int);
                ListCheck.ToList().ForEach(delegate(Check C)
                {
                    try
                    {
                        IntToPos(C.b, out x, out y, out z);
                        var flag = false;
                        var flag2 = false;
                        var num = 0;
                        foundPlayer = null;
                        foundNum = 75;
                        var text = C.extraInfo;
                        while (text != "")
                        {
                            try
                            {
                                if (GetTile(C.b) == byte.MaxValue)
                                {
                                    Server.s.Log("Out of bounds error!");
                                    ListCheck.Remove(C);
                                }
                                else
                                {
                                    var num2 = 0;
                                    try
                                    {
                                        if (!text.Contains("wait") &&
                                            (GetTile(C.b) == 0 || GetTile(C.b) == byte.MaxValue)) C.extraInfo = "";
                                    }
                                    catch (Exception ex4)
                                    {
                                        Server.s.Log("Damn you");
                                        Server.ErrorLog(ex4);
                                    }

                                    var flag3 = false;
                                    var num3 = 0;
                                    var flag4 = false;
                                    var num4 = 0;
                                    var flag5 = false;
                                    var num5 = 0;
                                    var flag6 = false;
                                    byte type = 0;
                                    var flag7 = false;
                                    var num6 = 0;
                                    var flag8 = false;
                                    var flag9 = false;
                                    var num7 = 0;
                                    var flag10 = false;
                                    try
                                    {
                                        try
                                        {
                                            var array = C.extraInfo.Split(' ');
                                            foreach (var text2 in array.ToList())
                                            {
                                                if (num2 % 2 == 0)
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

                                                num2++;
                                            }
                                        }
                                        catch (Exception ex5)
                                        {
                                            Server.s.Log("switch");
                                            Server.ErrorLog(ex5);
                                        }
                                    }
                                    catch (Exception ex6)
                                    {
                                        Server.s.Log("ee");
                                        Server.ErrorLog(ex6);
                                    }

                                    while (true)
                                    {
                                        if (!flag4)
                                        {
                                            if (flag8)
                                            {
                                                try
                                                {
                                                    finiteMovement(C, x, y, z);
                                                }
                                                catch (Exception ex7)
                                                {
                                                    Server.s.Log("finiteWater");
                                                    Server.ErrorLog(ex7);
                                                }
                                            }
                                            else if (flag9)
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
                                                            AddUpdate(C.b, 21, true);
                                                        else if (blocks[C.b] == 33)
                                                            AddUpdate(C.b, 21);
                                                        else
                                                            AddUpdate(C.b, (byte) (blocks[C.b] + 1));
                                                    }
                                                    else
                                                    {
                                                        AddUpdate(C.b, (byte) rand.Next(21, 33));
                                                    }
                                                }
                                                catch (Exception ex8)
                                                {
                                                    Server.s.Log("rainbow?");
                                                    Server.ErrorLog(ex8);
                                                }
                                            }
                                            else
                                            {
                                                if (flag6)
                                                    try
                                                    {
                                                        AddUpdate(C.b, type);
                                                        C.extraInfo = "";
                                                    }
                                                    catch (Exception ex9)
                                                    {
                                                        Server.s.Log("revert");
                                                        Server.ErrorLog(ex9);
                                                    }

                                                if (flag5)
                                                    try
                                                    {
                                                        if (rand.Next(1, 100) <= num5)
                                                        {
                                                            AddUpdate(C.b, 0);
                                                            C.extraInfo = "";
                                                        }
                                                    }
                                                    catch (Exception ex10)
                                                    {
                                                        Server.s.Log("dissipate");
                                                        Server.ErrorLog(ex10);
                                                    }

                                                if (flag7)
                                                    try
                                                    {
                                                        if (rand.Next(1, 100) <= num6)
                                                        {
                                                            MakeExplosion(x, y, z, 0);
                                                            C.extraInfo = "";
                                                        }
                                                    }
                                                    catch (Exception ex11)
                                                    {
                                                        Server.s.Log("explode");
                                                        Server.ErrorLog(ex11);
                                                    }

                                                if (flag3 && rand.Next(1, 100) <= num3 &&
                                                    (GetTile(x, (ushort) (y - 1), z) == 0 ||
                                                     GetTile(x, (ushort) (y - 1), z) == 10 ||
                                                     GetTile(x, (ushort) (y - 1), z) == 8) && rand.Next(1, 100) < num3)
                                                {
                                                    AddUpdate(PosToInt(x, (ushort) (y - 1), z), blocks[C.b], false,
                                                        C.extraInfo);
                                                    AddUpdate(C.b, 0);
                                                    C.extraInfo = "";
                                                }
                                            }

                                            break;
                                        }

                                        try
                                        {
                                            var num8 = 0;
                                            if (flag10 && C.time < 2)
                                            {
                                                num8 = IntOffset(C.b, -1, 0, 0);
                                                if (Block.tDoor(blocks[num8]))
                                                    AddUpdate(num8, 0, false, "wait 10 door 1 revert " + blocks[num8]);
                                                num8 = IntOffset(C.b, 1, 0, 0);
                                                if (Block.tDoor(blocks[num8]))
                                                    AddUpdate(num8, 0, false, "wait 10 door 1 revert " + blocks[num8]);
                                                num8 = IntOffset(C.b, 0, 1, 0);
                                                if (Block.tDoor(blocks[num8]))
                                                    AddUpdate(num8, 0, false, "wait 10 door 1 revert " + blocks[num8]);
                                                num8 = IntOffset(C.b, 0, -1, 0);
                                                if (Block.tDoor(blocks[num8]))
                                                    AddUpdate(num8, 0, false, "wait 10 door 1 revert " + blocks[num8]);
                                                num8 = IntOffset(C.b, 0, 0, 1);
                                                if (Block.tDoor(blocks[num8]))
                                                    AddUpdate(num8, 0, false, "wait 10 door 1 revert " + blocks[num8]);
                                                num8 = IntOffset(C.b, 0, 0, -1);
                                                if (Block.tDoor(blocks[num8]))
                                                    AddUpdate(num8, 0, false, "wait 10 door 1 revert " + blocks[num8]);
                                            }

                                            if (num4 > C.time)
                                            {
                                                C.time++;
                                                text = "";
                                                goto IL_0060;
                                            }

                                            flag4 = false;
                                            C.extraInfo = C.extraInfo.Substring(0, C.extraInfo.IndexOf("wait ")) +
                                                          C.extraInfo.Substring(C.extraInfo.IndexOf(' ',
                                                              C.extraInfo.IndexOf("wait ") + 5) + 1);
                                        }
                                        catch (Exception ex12)
                                        {
                                            Server.s.Log("wait");
                                            Server.ErrorLog(ex12);
                                            return;
                                        }
                                    }
                                }
                            }
                            catch (Exception ex13)
                            {
                                Server.s.Log("Error here");
                                Server.ErrorLog(ex13);
                            }

                            return;
                            IL_0060: ;
                        }

                        if (physics == 4 && !Block.IsDoor(blocks[C.b]))
                            C.time = byte.MaxValue;
                        else
                            switch (blocks[C.b])
                            {
                                case 16:
                                    PhysCoal(PosToInt((ushort) (x + 1), y, z));
                                    PhysCoal(PosToInt((ushort) (x - 1), y, z));
                                    PhysCoal(PosToInt(x, y, (ushort) (z + 1)));
                                    PhysCoal(PosToInt(x, y, (ushort) (z - 1)));
                                    PhysCoal(PosToInt(x, (ushort) (y + 1), z));
                                    if (Server.hardcore) PhysCoal(PosToInt(x, (ushort) (y - 1), z));
                                    break;
                                case 79:
                                    if (C.time > 16)
                                    {
                                        if (GetTile(x, y, z) == 79) AddUpdate(C.b, 0);
                                        C.time = byte.MaxValue;
                                    }

                                    C.time++;
                                    break;
                                case 254:
                                    PhysGlass(PosToInt((ushort) (x + 1), y, z));
                                    PhysGlass(PosToInt((ushort) (x - 1), y, z));
                                    PhysGlass(PosToInt(x, y, (ushort) (z + 1)));
                                    PhysGlass(PosToInt(x, y, (ushort) (z - 1)));
                                    PhysGlass(PosToInt(x, (ushort) (y + 1), z));
                                    C.time = byte.MaxValue;
                                    break;
                                case 0:
                                    PhysAir(PosToInt((ushort) (x + 1), y, z));
                                    PhysAir(PosToInt((ushort) (x - 1), y, z));
                                    PhysAir(PosToInt(x, y, (ushort) (z + 1)));
                                    PhysAir(PosToInt(x, y, (ushort) (z - 1)));
                                    PhysAir(PosToInt(x, (ushort) (y + 1), z));
                                    if (edgeWater && y < height / 2 && y >= height / 2 - 2 &&
                                        (x == 0 || x == width - 1 || z == 0 || z == depth - 1)) AddUpdate(C.b, 8);
                                    if (!C.extraInfo.Contains("wait")) C.time = byte.MaxValue;
                                    break;
                                case 3:
                                    if (!GrassGrow)
                                    {
                                        C.time = byte.MaxValue;
                                    }
                                    else if (C.time > 20)
                                    {
                                        if (Block.LightPass(GetTile(x, (ushort) (y + 1), z))) AddUpdate(C.b, 2);
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
                                            if (GetTile(x, (ushort) (y + 1), z) != byte.MaxValue)
                                                PhysSandCheck(PosToInt(x, (ushort) (y + 1), z));
                                            PhysWater(PosToInt((ushort) (x + 1), y, z), blocks[C.b]);
                                            PhysWater(PosToInt((ushort) (x - 1), y, z), blocks[C.b]);
                                            PhysWater(PosToInt(x, y, (ushort) (z + 1)), blocks[C.b]);
                                            PhysWater(PosToInt(x, y, (ushort) (z - 1)), blocks[C.b]);
                                            PhysWater(PosToInt(x, (ushort) (y - 1), z), blocks[C.b]);
                                        }
                                        else
                                        {
                                            AddUpdate(C.b, 0);
                                        }

                                        if (C.extraInfo.IndexOf("wait") == -1) C.time = byte.MaxValue;
                                        break;
                                    }

                                    goto case 145;
                                case 140:
                                    rand = new Random();
                                    if (GetTile(x, (ushort) (y - 1), z) == 0)
                                    {
                                        AddUpdate(PosToInt(x, (ushort) (y - 1), z), 140);
                                        if (C.extraInfo.IndexOf("wait") == -1) C.time = byte.MaxValue;
                                    }
                                    else if (GetTile(x, (ushort) (y - 1), z) != 203 &&
                                             GetTile(x, (ushort) (y - 1), z) != 9 &&
                                             GetTile(x, (ushort) (y - 1), z) != 11 &&
                                             GetTile(x, (ushort) (y - 1), z) != 140)
                                    {
                                        PhysWater(PosToInt((ushort) (x + 1), y, z), blocks[C.b]);
                                        PhysWater(PosToInt((ushort) (x - 1), y, z), blocks[C.b]);
                                        PhysWater(PosToInt(x, y, (ushort) (z + 1)), blocks[C.b]);
                                        PhysWater(PosToInt(x, y, (ushort) (z - 1)), blocks[C.b]);
                                        if (C.extraInfo.IndexOf("wait") == -1) C.time = byte.MaxValue;
                                    }

                                    break;
                                case 141:
                                    rand = new Random();
                                    if (GetTile(x, (ushort) (y - 1), z) == 0)
                                    {
                                        AddUpdate(PosToInt(x, (ushort) (y - 1), z), 141);
                                        if (C.extraInfo.IndexOf("wait") == -1) C.time = byte.MaxValue;
                                    }
                                    else if (GetTile(x, (ushort) (y - 1), z) != 203 &&
                                             GetTile(x, (ushort) (y - 1), z) != 9 &&
                                             GetTile(x, (ushort) (y - 1), z) != 11 &&
                                             GetTile(x, (ushort) (y - 1), z) != 141)
                                    {
                                        PhysLava(PosToInt((ushort) (x + 1), y, z), blocks[C.b]);
                                        PhysLava(PosToInt((ushort) (x - 1), y, z), blocks[C.b]);
                                        PhysLava(PosToInt(x, y, (ushort) (z + 1)), blocks[C.b]);
                                        PhysLava(PosToInt(x, y, (ushort) (z - 1)), blocks[C.b]);
                                        if (C.extraInfo.IndexOf("wait") == -1) C.time = byte.MaxValue;
                                    }

                                    break;
                                case 143:
                                    C.time++;
                                    if (C.time >= 2)
                                    {
                                        C.time = 0;
                                        if (GetTile(x, (ushort) (y - 1), z) == 0 ||
                                            GetTile(x, (ushort) (y - 1), z) == 140)
                                        {
                                            if (rand.Next(1, 10) > 7) AddUpdate(PosToInt(x, (ushort) (y - 1), z), 203);
                                        }
                                        else if (GetTile(x, (ushort) (y - 1), z) == 203 && rand.Next(1, 10) > 4)
                                        {
                                            AddUpdate(PosToInt(x, (ushort) (y - 1), z), 140);
                                        }
                                    }

                                    break;
                                case 144:
                                    C.time++;
                                    if (C.time >= 2)
                                    {
                                        C.time = 0;
                                        if (GetTile(x, (ushort) (y - 1), z) == 0 ||
                                            GetTile(x, (ushort) (y - 1), z) == 141)
                                        {
                                            if (rand.Next(1, 10) > 7) AddUpdate(PosToInt(x, (ushort) (y - 1), z), 203);
                                        }
                                        else if (GetTile(x, (ushort) (y - 1), z) == 203 && rand.Next(1, 10) > 4)
                                        {
                                            AddUpdate(PosToInt(x, (ushort) (y - 1), z), 141);
                                        }
                                    }

                                    break;
                                case 80:
                                    if (!PhysSpongeCheck(C.b))
                                    {
                                        if (C.time == 3)
                                        {
                                            PhysLava(PosToInt(x, (ushort) (y - 1), z), blocks[C.b]);
                                            C.time++;
                                        }
                                        else if (C.time == 7)
                                        {
                                            PhysLava(PosToInt((ushort) (x + 1), y, z), blocks[C.b]);
                                            PhysLava(PosToInt((ushort) (x - 1), y, z), blocks[C.b]);
                                            PhysLava(PosToInt(x, y, (ushort) (z + 1)), blocks[C.b]);
                                            PhysLava(PosToInt(x, y, (ushort) (z - 1)), blocks[C.b]);
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
                                            PhysLava(PosToInt(x, (ushort) (y - 1), z), blocks[C.b]);
                                            C.time++;
                                        }
                                        else if (C.time == 5)
                                        {
                                            PhysLava(PosToInt((ushort) (x + 1), y, z), blocks[C.b]);
                                            PhysLava(PosToInt((ushort) (x - 1), y, z), blocks[C.b]);
                                            PhysLava(PosToInt(x, y, (ushort) (z + 1)), blocks[C.b]);
                                            PhysLava(PosToInt(x, y, (ushort) (z - 1)), blocks[C.b]);
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
                                            C.time = (byte) randLavaTypeC.Next(1, 5);
                                        }
                                        else if (C.time == 1)
                                        {
                                            PhysLava(PosToInt(x, (ushort) (y - 1), z), blocks[C.b]);
                                            PhysLava(PosToInt((ushort) (x + 1), y, z), blocks[C.b]);
                                            PhysLava(PosToInt((ushort) (x - 1), y, z), blocks[C.b]);
                                            PhysLava(PosToInt(x, y, (ushort) (z + 1)), blocks[C.b]);
                                            PhysLava(PosToInt(x, y, (ushort) (z - 1)), blocks[C.b]);
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
                                            C.time = (byte) randLavaTypeC.Next(3, 5);
                                        }
                                        else if (C.time == 3)
                                        {
                                            PhysLava(PosToInt(x, (ushort) (y - 1), z), blocks[C.b]);
                                            C.time--;
                                        }
                                        else if (C.time == 1)
                                        {
                                            PhysLava(PosToInt((ushort) (x + 1), y, z), blocks[C.b]);
                                            PhysLava(PosToInt((ushort) (x - 1), y, z), blocks[C.b]);
                                            PhysLava(PosToInt(x, y, (ushort) (z + 1)), blocks[C.b]);
                                            PhysLava(PosToInt(x, y, (ushort) (z - 1)), blocks[C.b]);
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
                                    if (GetTile(x, (ushort) (y - 1), z) == 0)
                                    {
                                        AddUpdate(PosToInt(x, (ushort) (y + 1), z), 0);
                                        AddUpdate(PosToInt(x, y, z), 11);
                                        AddUpdate(PosToInt(x, (ushort) (y - 1), z), 99);
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
                                    if (GetTile(x, (ushort) (y - 1), z) == 0)
                                    {
                                        AddUpdate(PosToInt(x, y, z), 0);
                                        AddUpdate(PosToInt(x, (ushort) (y - 1), z), 97);
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
                                            if (finite) goto case 145;
                                            PhysWater(PosToInt((ushort) (x + 1), y, z), blocks[C.b]);
                                            PhysWater(PosToInt((ushort) (x - 1), y, z), blocks[C.b]);
                                            PhysWater(PosToInt(x, y, (ushort) (z + 1)), blocks[C.b]);
                                            PhysWater(PosToInt(x, y, (ushort) (z - 1)), blocks[C.b]);
                                            PhysWater(PosToInt(x, (ushort) (y - 1), z), blocks[C.b]);
                                            PhysWater(PosToInt(x, (ushort) (y + 1), z), blocks[C.b]);
                                        }

                                        if (C.extraInfo.IndexOf("wait") == -1) C.time = byte.MaxValue;
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
                                            if (finite) goto case 145;
                                            PhysLava(PosToInt((ushort) (x + 1), y, z), blocks[C.b]);
                                            PhysLava(PosToInt((ushort) (x - 1), y, z), blocks[C.b]);
                                            PhysLava(PosToInt(x, y, (ushort) (z + 1)), blocks[C.b]);
                                            PhysLava(PosToInt(x, y, (ushort) (z - 1)), blocks[C.b]);
                                            PhysLava(PosToInt(x, (ushort) (y - 1), z), blocks[C.b]);
                                            PhysLava(PosToInt(x, (ushort) (y + 1), z), blocks[C.b]);
                                        }

                                        if (C.extraInfo.IndexOf("wait") == -1) C.time = byte.MaxValue;
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
                                            if (finite) goto case 145;
                                            PhysLava(PosToInt((ushort) (x + 1), y, z), blocks[C.b]);
                                            PhysLava(PosToInt((ushort) (x - 1), y, z), blocks[C.b]);
                                            PhysLava(PosToInt(x, y, (ushort) (z + 1)), blocks[C.b]);
                                            PhysLava(PosToInt(x, y, (ushort) (z - 1)), blocks[C.b]);
                                            PhysLava(PosToInt(x, (ushort) (y - 1), z), blocks[C.b]);
                                            if (hardcore || Server.hardcore)
                                                PhysLava(PosToInt(x, (ushort) (y + 1), z), blocks[C.b]);
                                        }

                                        if (C.extraInfo.IndexOf("wait") == -1) C.time = byte.MaxValue;
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
                                            if (num <= 3 && GetTile((ushort) (x - 1), y, z) == 0)
                                                AddUpdate(PosToInt((ushort) (x - 1), y, z), 185);
                                            else if (num <= 6 && GetTile((ushort) (x + 1), y, z) == 0)
                                                AddUpdate(PosToInt((ushort) (x + 1), y, z), 185);
                                            else if (num <= 9 && GetTile(x, (ushort) (y - 1), z) == 0)
                                                AddUpdate(PosToInt(x, (ushort) (y - 1), z), 185);
                                            else if (num <= 12 && GetTile(x, (ushort) (y + 1), z) == 0)
                                                AddUpdate(PosToInt(x, (ushort) (y + 1), z), 185);
                                            else if (num <= 15 && GetTile(x, y, (ushort) (z - 1)) == 0)
                                                AddUpdate(PosToInt(x, y, (ushort) (z - 1)), 185);
                                            else if (num <= 18 && GetTile(x, y, (ushort) (z + 1)) == 0)
                                                AddUpdate(PosToInt(x, y, (ushort) (z + 1)), 185);
                                        }

                                        if (Block.LavaKill(GetTile((ushort) (x - 1), y, (ushort) (z - 1))))
                                        {
                                            if (GetTile((ushort) (x - 1), y, z) == 0)
                                                AddUpdate(PosToInt((ushort) (x - 1), y, z), 185);
                                            if (GetTile(x, y, (ushort) (z - 1)) == 0)
                                                AddUpdate(PosToInt(x, y, (ushort) (z - 1)), 185);
                                        }

                                        if (Block.LavaKill(GetTile((ushort) (x + 1), y, (ushort) (z - 1))))
                                        {
                                            if (GetTile((ushort) (x + 1), y, z) == 0)
                                                AddUpdate(PosToInt((ushort) (x + 1), y, z), 185);
                                            if (GetTile(x, y, (ushort) (z - 1)) == 0)
                                                AddUpdate(PosToInt(x, y, (ushort) (z - 1)), 185);
                                        }

                                        if (Block.LavaKill(GetTile((ushort) (x - 1), y, (ushort) (z + 1))))
                                        {
                                            if (GetTile((ushort) (x - 1), y, z) == 0)
                                                AddUpdate(PosToInt((ushort) (x - 1), y, z), 185);
                                            if (GetTile(x, y, (ushort) (z + 1)) == 0)
                                                AddUpdate(PosToInt(x, y, (ushort) (z + 1)), 185);
                                        }

                                        if (Block.LavaKill(GetTile((ushort) (x + 1), y, (ushort) (z + 1))))
                                        {
                                            if (GetTile((ushort) (x + 1), y, z) == 0)
                                                AddUpdate(PosToInt((ushort) (x + 1), y, z), 185);
                                            if (GetTile(x, y, (ushort) (z + 1)) == 0)
                                                AddUpdate(PosToInt(x, y, (ushort) (z + 1)), 185);
                                        }

                                        if (Block.LavaKill(GetTile(x, (ushort) (y - 1), (ushort) (z - 1))))
                                        {
                                            if (GetTile(x, (ushort) (y - 1), z) == 0)
                                                AddUpdate(PosToInt(x, (ushort) (y - 1), z), 185);
                                            if (GetTile(x, y, (ushort) (z - 1)) == 0)
                                                AddUpdate(PosToInt(x, y, (ushort) (z - 1)), 185);
                                        }
                                        else if (GetTile(x, (ushort) (y - 1), z) == 2)
                                        {
                                            AddUpdate(PosToInt(x, (ushort) (y - 1), z), 3);
                                        }

                                        if (Block.LavaKill(GetTile(x, (ushort) (y + 1), (ushort) (z - 1))))
                                        {
                                            if (GetTile(x, (ushort) (y + 1), z) == 0)
                                                AddUpdate(PosToInt(x, (ushort) (y + 1), z), 185);
                                            if (GetTile(x, y, (ushort) (z - 1)) == 0)
                                                AddUpdate(PosToInt(x, y, (ushort) (z - 1)), 185);
                                        }

                                        if (Block.LavaKill(GetTile(x, (ushort) (y - 1), (ushort) (z + 1))))
                                        {
                                            if (GetTile(x, (ushort) (y - 1), z) == 0)
                                                AddUpdate(PosToInt(x, (ushort) (y - 1), z), 185);
                                            if (GetTile(x, y, (ushort) (z + 1)) == 0)
                                                AddUpdate(PosToInt(x, y, (ushort) (z + 1)), 185);
                                        }

                                        if (Block.LavaKill(GetTile(x, (ushort) (y + 1), (ushort) (z + 1))))
                                        {
                                            if (GetTile(x, (ushort) (y + 1), z) == 0)
                                                AddUpdate(PosToInt(x, (ushort) (y + 1), z), 185);
                                            if (GetTile(x, y, (ushort) (z + 1)) == 0)
                                                AddUpdate(PosToInt(x, y, (ushort) (z + 1)), 185);
                                        }

                                        if (Block.LavaKill(GetTile((ushort) (x - 1), (ushort) (y - 1), z)))
                                        {
                                            if (GetTile(x, (ushort) (y - 1), z) == 0)
                                                AddUpdate(PosToInt(x, (ushort) (y - 1), z), 185);
                                            if (GetTile((ushort) (x - 1), y, z) == 0)
                                                AddUpdate(PosToInt((ushort) (x - 1), y, z), 185);
                                        }

                                        if (Block.LavaKill(GetTile((ushort) (x - 1), (ushort) (y + 1), z)))
                                        {
                                            if (GetTile(x, (ushort) (y + 1), z) == 0)
                                                AddUpdate(PosToInt(x, (ushort) (y + 1), z), 185);
                                            if (GetTile((ushort) (x - 1), y, z) == 0)
                                                AddUpdate(PosToInt((ushort) (x - 1), y, z), 185);
                                        }

                                        if (Block.LavaKill(GetTile((ushort) (x + 1), (ushort) (y - 1), z)))
                                        {
                                            if (GetTile(x, (ushort) (y - 1), z) == 0)
                                                AddUpdate(PosToInt(x, (ushort) (y - 1), z), 185);
                                            if (GetTile((ushort) (x + 1), y, z) == 0)
                                                AddUpdate(PosToInt((ushort) (x + 1), y, z), 185);
                                        }

                                        if (Block.LavaKill(GetTile((ushort) (x + 1), (ushort) (y + 1), z)))
                                        {
                                            if (GetTile(x, (ushort) (y + 1), z) == 0)
                                                AddUpdate(PosToInt(x, (ushort) (y + 1), z), 185);
                                            if (GetTile((ushort) (x + 1), y, z) == 0)
                                                AddUpdate(PosToInt((ushort) (x + 1), y, z), 185);
                                        }

                                        if (physics >= 2)
                                        {
                                            if (C.time < 4)
                                            {
                                                C.time++;
                                                break;
                                            }

                                            if (Block.LavaKill(GetTile((ushort) (x - 1), y, z)))
                                                AddUpdate(PosToInt((ushort) (x - 1), y, z), 185);
                                            else if (GetTile((ushort) (x - 1), y, z) == 46)
                                                MakeExplosion((ushort) (x - 1), y, z, -1);
                                            if (Block.LavaKill(GetTile((ushort) (x + 1), y, z)))
                                                AddUpdate(PosToInt((ushort) (x + 1), y, z), 185);
                                            else if (GetTile((ushort) (x + 1), y, z) == 46)
                                                MakeExplosion((ushort) (x + 1), y, z, -1);
                                            if (Block.LavaKill(GetTile(x, (ushort) (y - 1), z)))
                                                AddUpdate(PosToInt(x, (ushort) (y - 1), z), 185);
                                            else if (GetTile(x, (ushort) (y - 1), z) == 46)
                                                MakeExplosion(x, (ushort) (y - 1), z, -1);
                                            if (Block.LavaKill(GetTile(x, (ushort) (y + 1), z)))
                                                AddUpdate(PosToInt(x, (ushort) (y + 1), z), 185);
                                            else if (GetTile(x, (ushort) (y + 1), z) == 46)
                                                MakeExplosion(x, (ushort) (y + 1), z, -1);
                                            if (Block.LavaKill(GetTile(x, y, (ushort) (z - 1))))
                                                AddUpdate(PosToInt(x, y, (ushort) (z - 1)), 185);
                                            else if (GetTile(x, y, (ushort) (z - 1)) == 46)
                                                MakeExplosion(x, y, (ushort) (z - 1), -1);
                                            if (Block.LavaKill(GetTile(x, y, (ushort) (z + 1))))
                                                AddUpdate(PosToInt(x, y, (ushort) (z + 1)), 185);
                                            else if (GetTile(x, y, (ushort) (z + 1)) == 46)
                                                MakeExplosion(x, y, (ushort) (z + 1), -1);
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
                                    for (var num11 = 0; num11 < 6; num11++) list.Add(num11);
                                    for (var num12 = list.Count - 1; num12 > 1; num12--)
                                    {
                                        var index = rand.Next(num12);
                                        var value = list[num12];
                                        list[num12] = list[index];
                                        list[index] = value;
                                    }

                                    using (var enumerator = list.GetEnumerator())
                                    {
                                        while (enumerator.MoveNext())
                                        {
                                            switch (enumerator.Current)
                                            {
                                                case 0:
                                                    if (GetTile((ushort) (x - 1), y, z) == 0 &&
                                                        AddUpdate(PosToInt((ushort) (x - 1), y, z), 145)) flag = true;
                                                    break;
                                                case 1:
                                                    if (GetTile((ushort) (x + 1), y, z) == 0 &&
                                                        AddUpdate(PosToInt((ushort) (x + 1), y, z), 145)) flag = true;
                                                    break;
                                                case 2:
                                                    if (GetTile(x, (ushort) (y - 1), z) == 0 &&
                                                        AddUpdate(PosToInt(x, (ushort) (y - 1), z), 145)) flag = true;
                                                    break;
                                                case 3:
                                                    if (GetTile(x, (ushort) (y + 1), z) == 0 &&
                                                        AddUpdate(PosToInt(x, (ushort) (y + 1), z), 145)) flag = true;
                                                    break;
                                                case 4:
                                                    if (GetTile(x, y, (ushort) (z - 1)) == 0 &&
                                                        AddUpdate(PosToInt(x, y, (ushort) (z - 1)), 145)) flag = true;
                                                    break;
                                                case 5:
                                                    if (GetTile(x, y, (ushort) (z + 1)) == 0 &&
                                                        AddUpdate(PosToInt(x, y, (ushort) (z + 1)), 145)) flag = true;
                                                    break;
                                            }

                                            if (flag) break;
                                        }
                                    }

                                    break;
                                }
                                case 12:
                                    if (PhysSand(C.b, 12))
                                    {
                                        PhysAir(PosToInt((ushort) (x + 1), y, z));
                                        PhysAir(PosToInt((ushort) (x - 1), y, z));
                                        PhysAir(PosToInt(x, y, (ushort) (z + 1)));
                                        PhysAir(PosToInt(x, y, (ushort) (z - 1)));
                                        PhysAir(PosToInt(x, (ushort) (y + 1), z));
                                    }

                                    C.time = byte.MaxValue;
                                    break;
                                case 13:
                                    if (PhysSand(C.b, 13))
                                    {
                                        PhysAir(PosToInt((ushort) (x + 1), y, z));
                                        PhysAir(PosToInt((ushort) (x - 1), y, z));
                                        PhysAir(PosToInt(x, y, (ushort) (z + 1)));
                                        PhysAir(PosToInt(x, y, (ushort) (z - 1)));
                                        PhysAir(PosToInt(x, (ushort) (y + 1), z));
                                    }

                                    C.time = byte.MaxValue;
                                    break;
                                case 19:
                                    PhysUniversalSponge(C.b);
                                    if (C.time > 30)
                                    {
                                        if (GetTile(x, y, z) == 19) Blockchange(x, y, z, 0);
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
                                        PhysAir(PosToInt((ushort) (x + 1), y, z));
                                        PhysAir(PosToInt((ushort) (x - 1), y, z));
                                        PhysAir(PosToInt(x, y, (ushort) (z + 1)));
                                        PhysAir(PosToInt(x, y, (ushort) (z - 1)));
                                        PhysAir(PosToInt(x, (ushort) (y + 1), z));
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
                                    PhysLava(PosToInt((ushort) (x + 1), y, z), 112);
                                    PhysLava(PosToInt((ushort) (x - 1), y, z), 112);
                                    PhysLava(PosToInt(x, y, (ushort) (z + 1)), 112);
                                    PhysLava(PosToInt(x, y, (ushort) (z - 1)), 112);
                                    PhysLava(PosToInt(x, (ushort) (y - 1), z), 112);
                                    C.time = byte.MaxValue;
                                    break;
                                case 200:
                                    if (C.time < 1)
                                    {
                                        PhysAirFlood(PosToInt((ushort) (x + 1), y, z), 200);
                                        PhysAirFlood(PosToInt((ushort) (x - 1), y, z), 200);
                                        PhysAirFlood(PosToInt(x, y, (ushort) (z + 1)), 200);
                                        PhysAirFlood(PosToInt(x, y, (ushort) (z - 1)), 200);
                                        PhysAirFlood(PosToInt(x, (ushort) (y - 1), z), 200);
                                        PhysAirFlood(PosToInt(x, (ushort) (y + 1), z), 200);
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
                                    AnyDoor(C, x, y, z, 4, true);
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
                                        PhysAirFlood(PosToInt((ushort) (x + 1), y, z), 202);
                                        PhysAirFlood(PosToInt((ushort) (x - 1), y, z), 202);
                                        PhysAirFlood(PosToInt(x, y, (ushort) (z + 1)), 202);
                                        PhysAirFlood(PosToInt(x, y, (ushort) (z - 1)), 202);
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
                                        PhysAirFlood(PosToInt((ushort) (x + 1), y, z), 203);
                                        PhysAirFlood(PosToInt((ushort) (x - 1), y, z), 203);
                                        PhysAirFlood(PosToInt(x, y, (ushort) (z + 1)), 203);
                                        PhysAirFlood(PosToInt(x, y, (ushort) (z - 1)), 203);
                                        PhysAirFlood(PosToInt(x, (ushort) (y - 1), z), 203);
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
                                        PhysAirFlood(PosToInt((ushort) (x + 1), y, z), 204);
                                        PhysAirFlood(PosToInt((ushort) (x - 1), y, z), 204);
                                        PhysAirFlood(PosToInt(x, y, (ushort) (z + 1)), 204);
                                        PhysAirFlood(PosToInt(x, y, (ushort) (z - 1)), 204);
                                        PhysAirFlood(PosToInt(x, (ushort) (y + 1), z), 204);
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
                                    if (physics < 3) Blockchange(x, y, z, 0);
                                    if (physics == 3)
                                    {
                                        rand = new Random();
                                        if (C.time < 5 && physics == 3)
                                        {
                                            C.time++;
                                            if (GetTile(x, (ushort) (y + 1), z) == 11)
                                                Blockchange(x, (ushort) (y + 1), z, 0);
                                            else
                                                Blockchange(x, (ushort) (y + 1), z, 11);
                                        }
                                        else
                                        {
                                            try
                                            {
                                                MakeExplosion(x, y, z, 0);
                                            }
                                            catch (Exception ex14)
                                            {
                                                Server.s.Log("EXPLOSION");
                                                Server.ErrorLog(ex14);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Blockchange(x, y, z, 0);
                                    }

                                    break;
                                case 183:
                                    if (physics < 3) Blockchange(x, y, z, 0);
                                    if (physics == 3)
                                    {
                                        rand = new Random();
                                        if (C.time < 5 && physics == 3)
                                        {
                                            C.time++;
                                            if (GetTile(x, (ushort) (y + 1), z) == 11)
                                                Blockchange(x, (ushort) (y + 1), z, 0);
                                            else
                                                Blockchange(x, (ushort) (y + 1), z, 11);
                                            if (GetTile(x, (ushort) (y - 1), z) == 11)
                                                Blockchange(x, (ushort) (y - 1), z, 0);
                                            else
                                                Blockchange(x, (ushort) (y - 1), z, 11);
                                            if (GetTile((ushort) (x + 1), y, z) == 11)
                                                Blockchange((ushort) (x + 1), y, z, 0);
                                            else
                                                Blockchange((ushort) (x + 1), y, z, 11);
                                            if (GetTile((ushort) (x - 1), y, z) == 11)
                                                Blockchange((ushort) (x - 1), y, z, 0);
                                            else
                                                Blockchange((ushort) (x - 1), y, z, 11);
                                            if (GetTile(x, y, (ushort) (z + 1)) == 11)
                                                Blockchange(x, y, (ushort) (z + 1), 0);
                                            else
                                                Blockchange(x, y, (ushort) (z + 1), 11);
                                            if (GetTile(x, y, (ushort) (z - 1)) == 11)
                                                Blockchange(x, y, (ushort) (z - 1), 0);
                                            else
                                                Blockchange(x, y, (ushort) (z - 1), 11);
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
                                    if (rand.Next(1, 11) <= 7) AddUpdate(C.b, 0);
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
                                        mx = 1;
                                    else
                                        mx = -1;
                                    if (rand.Next(1, 10) <= 5)
                                        my = 1;
                                    else
                                        my = -1;
                                    if (rand.Next(1, 10) <= 5)
                                        mz = 1;
                                    else
                                        mz = -1;
                                    for (var m = -1 * mx; m != mx + mx; m += mx)
                                    for (var n = -1 * my; n != my + my; n += my)
                                    for (var num10 = -1 * mz; num10 != mz + mz; num10 += mz)
                                    {
                                        if (GetTile((ushort) (x + m), (ushort) (y + n - 1), (ushort) (z + num10)) ==
                                            21 && (GetTile((ushort) (x + m), (ushort) (y + n), (ushort) (z + num10)) ==
                                                0 || GetTile((ushort) (x + m), (ushort) (y + n),
                                                    (ushort) (z + num10)) == 8) && !flag)
                                        {
                                            AddUpdate(
                                                PosToInt((ushort) (x + m), (ushort) (y + n), (ushort) (z + num10)),
                                                230);
                                            AddUpdate(PosToInt(x, y, z), 0);
                                            AddUpdate(IntOffset(C.b, 0, -1, 0), 49, true, "wait 5 revert " + (byte) 21);
                                            flag = true;
                                            break;
                                        }

                                        if (GetTile((ushort) (x + m), (ushort) (y + n - 1), (ushort) (z + num10)) ==
                                            105 && (GetTile((ushort) (x + m), (ushort) (y + n), (ushort) (z + num10)) ==
                                                0 || GetTile((ushort) (x + m), (ushort) (y + n),
                                                    (ushort) (z + num10)) == 8) && !flag)
                                        {
                                            AddUpdate(
                                                PosToInt((ushort) (x + m), (ushort) (y + n), (ushort) (z + num10)),
                                                230);
                                            AddUpdate(PosToInt(x, y, z), 0);
                                            AddUpdate(IntOffset(C.b, 0, -1, 0), 20, true,
                                                "wait 5 revert " + (byte) 105);
                                            flag = true;
                                            break;
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
                                            if (GetTile(x, (ushort) (y - 1), z) == 0)
                                            {
                                                AddUpdate(PosToInt(x, (ushort) (y - 1), z), 195);
                                            }
                                            else if (GetTile(x, (ushort) (y - 1), z) != 195)
                                            {
                                                PhysLava(PosToInt((ushort) (x + 1), y, z), blocks[C.b]);
                                                PhysLava(PosToInt((ushort) (x - 1), y, z), blocks[C.b]);
                                                PhysLava(PosToInt(x, y, (ushort) (z + 1)), blocks[C.b]);
                                                PhysLava(PosToInt(x, y, (ushort) (z - 1)), blocks[C.b]);
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
                                    if (GetTile(x, (ushort) (y - 1), z) == 0)
                                    {
                                        AddUpdate(PosToInt(x, (ushort) (y - 1), z), 196);
                                    }
                                    else if (GetTile(x, (ushort) (y - 1), z) != 196)
                                    {
                                        PhysWater(PosToInt((ushort) (x + 1), y, z), blocks[C.b]);
                                        PhysWater(PosToInt((ushort) (x - 1), y, z), blocks[C.b]);
                                        PhysWater(PosToInt(x, y, (ushort) (z + 1)), blocks[C.b]);
                                        PhysWater(PosToInt(x, y, (ushort) (z - 1)), blocks[C.b]);
                                    }

                                    if (physics > 1 && C.time > 10)
                                    {
                                        C.time = 0;
                                        if (Block.WaterKill(GetTile((ushort) (x + 1), y, z)))
                                        {
                                            AddUpdate(PosToInt((ushort) (x + 1), y, z), 196);
                                            flag = true;
                                        }

                                        if (Block.WaterKill(GetTile((ushort) (x - 1), y, z)))
                                        {
                                            AddUpdate(PosToInt((ushort) (x - 1), y, z), 196);
                                            flag = true;
                                        }

                                        if (Block.WaterKill(GetTile(x, y, (ushort) (z + 1))))
                                        {
                                            AddUpdate(PosToInt(x, y, (ushort) (z + 1)), 196);
                                            flag = true;
                                        }

                                        if (Block.WaterKill(GetTile(x, y, (ushort) (z - 1))))
                                        {
                                            AddUpdate(PosToInt(x, y, (ushort) (z - 1)), 196);
                                            flag = true;
                                        }

                                        if (Block.WaterKill(GetTile(x, (ushort) (y - 1), z)))
                                        {
                                            AddUpdate(PosToInt(x, (ushort) (y - 1), z), 196);
                                            flag = true;
                                        }

                                        if (flag && Block.WaterKill(GetTile(x, (ushort) (y + 1), z)))
                                            AddUpdate(PosToInt(x, (ushort) (y + 1), z), 196);
                                    }

                                    break;
                                case 235:
                                case 236:
                                case 237:
                                case 238:
                                    switch (rand.Next(1, 15))
                                    {
                                        case 1:
                                            if (GetTile(x, (ushort) (y - 1), z) == 0)
                                            {
                                                AddUpdate(PosToInt(x, (ushort) (y - 1), z), blocks[C.b]);
                                                break;
                                            }

                                            goto case 3;
                                        case 2:
                                            if (GetTile(x, (ushort) (y + 1), z) == 0)
                                            {
                                                AddUpdate(PosToInt(x, (ushort) (y + 1), z), blocks[C.b]);
                                                break;
                                            }

                                            goto case 6;
                                        case 3:
                                        case 4:
                                        case 5:
                                            if (GetTile((ushort) (x - 1), y, z) == 0)
                                                AddUpdate(PosToInt((ushort) (x - 1), y, z), blocks[C.b]);
                                            else if (GetTile((ushort) (x - 1), y, z) != 105)
                                                AddUpdate(C.b, 21, false, "dissipate 25");
                                            break;
                                        case 6:
                                        case 7:
                                        case 8:
                                            if (GetTile((ushort) (x + 1), y, z) == 0)
                                                AddUpdate(PosToInt((ushort) (x + 1), y, z), blocks[C.b]);
                                            else if (GetTile((ushort) (x + 1), y, z) != 105)
                                                AddUpdate(C.b, 21, false, "dissipate 25");
                                            break;
                                        case 9:
                                        case 10:
                                        case 11:
                                            if (GetTile(x, y, (ushort) (z - 1)) == 0)
                                                AddUpdate(PosToInt(x, y, (ushort) (z - 1)), blocks[C.b]);
                                            else if (GetTile(x, y, (ushort) (z - 1)) != 105)
                                                AddUpdate(C.b, 21, false, "dissipate 25");
                                            break;
                                        default:
                                            if (GetTile(x, y, (ushort) (z + 1)) == 0)
                                                AddUpdate(PosToInt(x, y, (ushort) (z + 1)), blocks[C.b]);
                                            else if (GetTile(x, y, (ushort) (z + 1)) != 105)
                                                AddUpdate(C.b, 21, false, "dissipate 25");
                                            break;
                                    }

                                    AddUpdate(C.b, 0);
                                    C.time = byte.MaxValue;
                                    break;
                                case 252:
                                    if (GetTile(IntOffset(C.b, -1, 0, 0)) != 251 ||
                                        GetTile(IntOffset(C.b, 1, 0, 0)) != 251 ||
                                        GetTile(IntOffset(C.b, 0, 0, 1)) != 251 ||
                                        GetTile(IntOffset(C.b, 0, 0, -1)) != 251) C.extraInfo = "revert 0";
                                    break;
                                case 251:
                                    if (ai)
                                        Player.players.ForEach(delegate(Player p)
                                        {
                                            if (p.level == this && !p.invincible)
                                            {
                                                currentNum = Math.Abs(p.pos[0] / 32 - x) + Math.Abs(p.pos[1] / 32 - y) +
                                                             Math.Abs(p.pos[2] / 32 - z);
                                                if (currentNum < foundNum)
                                                {
                                                    foundNum = currentNum;
                                                    foundPlayer = p;
                                                }
                                            }
                                        });
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
                                                        var num9 = PosToInt(
                                                            (ushort) (x + Math.Sign(foundPlayer.pos[0] / 32 - x)), y,
                                                            z);
                                                        if (GetTile(num9) == 0 &&
                                                            (IntOffset(num9, -1, 0, 0) == 2 ||
                                                             IntOffset(num9, -1, 0, 0) == 3) &&
                                                            AddUpdate(num9, blocks[C.b])) break;
                                                    }

                                                    foundNum++;
                                                    if (foundNum >= 3) goto IL_60e2;
                                                    goto case 4;
                                                case 4:
                                                case 5:
                                                case 6:
                                                    if (foundPlayer.pos[1] / 32 - y != 0)
                                                    {
                                                        var num9 = PosToInt(x,
                                                            (ushort) (y + Math.Sign(foundPlayer.pos[1] / 32 - y)), z);
                                                        if (GetTile(num9) == 0)
                                                        {
                                                            if (num9 > 0)
                                                            {
                                                                if ((IntOffset(num9, 0, 1, 0) == 2 ||
                                                                     IntOffset(num9, 0, 1, 0) == 3 &&
                                                                     IntOffset(num9, 0, 2, 0) == 0) &&
                                                                    AddUpdate(num9, blocks[C.b])) break;
                                                            }
                                                            else if (num9 < 0 &&
                                                                     (IntOffset(num9, 0, -2, 0) == 2 ||
                                                                      IntOffset(num9, 0, -2, 0) == 3 &&
                                                                      IntOffset(num9, 0, -1, 0) == 0) &&
                                                                     AddUpdate(num9, blocks[C.b]))
                                                            {
                                                                break;
                                                            }
                                                        }
                                                    }

                                                    foundNum++;
                                                    if (foundNum >= 3) goto IL_60e2;
                                                    goto case 7;
                                                case 7:
                                                case 8:
                                                case 9:
                                                    if (foundPlayer.pos[2] / 32 - z != 0)
                                                    {
                                                        var num9 = PosToInt(x, y,
                                                            (ushort) (z + Math.Sign(foundPlayer.pos[2] / 32 - z)));
                                                        if (GetTile(num9) == 0 &&
                                                            (IntOffset(num9, 0, 0, -1) == 2 ||
                                                             IntOffset(num9, 0, 0, -1) == 3) &&
                                                            AddUpdate(num9, blocks[C.b])) break;
                                                    }

                                                    foundNum++;
                                                    if (foundNum >= 3) goto IL_60e2;
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
                                                    var num9 = IntOffset(C.b, -1, 0, 0);
                                                    var pos = PosToInt(x, y, z);
                                                    if (GetTile(IntOffset(num9, 0, -1, 0)) == 0 && GetTile(num9) == 0)
                                                    {
                                                        num9 = IntOffset(num9, 0, -1, 0);
                                                    }
                                                    else if (GetTile(num9) != 0 ||
                                                             GetTile(IntOffset(num9, 0, 1, 0)) != 0)
                                                    {
                                                        if (GetTile(IntOffset(num9, 0, 2, 0)) == 0 &&
                                                            GetTile(IntOffset(num9, 0, 1, 0)) == 0)
                                                            num9 = IntOffset(num9, 0, 1, 0);
                                                        else
                                                            flag2 = true;
                                                    }

                                                    if (AddUpdate(num9, blocks[C.b]))
                                                    {
                                                        AddUpdate(IntOffset(pos, 0, 0, 0), 252, true,
                                                            "wait 5 revert " + (byte) 0);
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
                                                    var num9 = IntOffset(C.b, 1, 0, 0);
                                                    var pos = PosToInt(x, y, z);
                                                    if (GetTile(IntOffset(num9, 0, -1, 0)) == 0 && GetTile(num9) == 0)
                                                    {
                                                        num9 = IntOffset(num9, 0, -1, 0);
                                                    }
                                                    else if (GetTile(num9) != 0 ||
                                                             GetTile(IntOffset(num9, 0, 1, 0)) != 0)
                                                    {
                                                        if (GetTile(IntOffset(num9, 0, 2, 0)) == 0 &&
                                                            GetTile(IntOffset(num9, 0, 1, 0)) == 0)
                                                            num9 = IntOffset(num9, 0, 1, 0);
                                                        else
                                                            flag2 = true;
                                                    }

                                                    if (AddUpdate(num9, blocks[C.b]))
                                                    {
                                                        AddUpdate(IntOffset(pos, 0, 0, 0), 252, true,
                                                            "wait 5 revert " + (byte) 0);
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
                                                    var num9 = IntOffset(C.b, 0, 0, 1);
                                                    var pos = PosToInt(x, y, z);
                                                    if (GetTile(IntOffset(num9, 0, -1, 0)) == 0 && GetTile(num9) == 0)
                                                    {
                                                        num9 = IntOffset(num9, 0, -1, 0);
                                                    }
                                                    else if (GetTile(num9) != 0 ||
                                                             GetTile(IntOffset(num9, 0, 1, 0)) != 0)
                                                    {
                                                        if (GetTile(IntOffset(num9, 0, 2, 0)) == 0 &&
                                                            GetTile(IntOffset(num9, 0, 1, 0)) == 0)
                                                            num9 = IntOffset(num9, 0, 1, 0);
                                                        else
                                                            flag2 = true;
                                                    }

                                                    if (AddUpdate(num9, blocks[C.b]))
                                                    {
                                                        AddUpdate(IntOffset(pos, 0, 0, 0), 252, true,
                                                            "wait 5 revert " + (byte) 0);
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
                                                    var num9 = IntOffset(C.b, 0, 0, -1);
                                                    var pos = PosToInt(x, y, z);
                                                    if (GetTile(IntOffset(num9, 0, -1, 0)) == 0 && GetTile(num9) == 0)
                                                    {
                                                        num9 = IntOffset(num9, 0, -1, 0);
                                                    }
                                                    else if (GetTile(num9) != 0 ||
                                                             GetTile(IntOffset(num9, 0, 1, 0)) != 0)
                                                    {
                                                        if (GetTile(IntOffset(num9, 0, 2, 0)) == 0 &&
                                                            GetTile(IntOffset(num9, 0, 1, 0)) == 0)
                                                            num9 = IntOffset(num9, 0, 1, 0);
                                                        else
                                                            flag2 = true;
                                                    }

                                                    if (AddUpdate(num9, blocks[C.b]))
                                                    {
                                                        AddUpdate(IntOffset(pos, 0, 0, 0), 252, true,
                                                            "wait 5 revert " + (byte) 0);
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

                                    if (!flag) AddUpdate(C.b, 0);
                                    break;
                                case 239:
                                case 240:
                                case 242:
                                    if (ai)
                                        Player.players.ForEach(delegate(Player p)
                                        {
                                            if (p.level == this && !p.invincible)
                                            {
                                                currentNum = Math.Abs(p.pos[0] / 32 - x) + Math.Abs(p.pos[1] / 32 - y) +
                                                             Math.Abs(p.pos[2] / 32 - z);
                                                if (currentNum < foundNum)
                                                {
                                                    foundNum = currentNum;
                                                    foundPlayer = p;
                                                }
                                            }
                                        });
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
                                                        var num9 = PosToInt(
                                                            (ushort) (x + Math.Sign(foundPlayer.pos[0] / 32 - x)), y,
                                                            z);
                                                        if (GetTile(num9) == 0 && AddUpdate(num9, blocks[C.b])) break;
                                                    }

                                                    foundNum++;
                                                    if (foundNum >= 3) goto IL_69a7;
                                                    goto case 4;
                                                case 4:
                                                case 5:
                                                case 6:
                                                    if (foundPlayer.pos[1] / 32 - y != 0)
                                                    {
                                                        var num9 = PosToInt(x,
                                                            (ushort) (y + Math.Sign(foundPlayer.pos[1] / 32 - y)), z);
                                                        if (GetTile(num9) == 0 && AddUpdate(num9, blocks[C.b])) break;
                                                    }

                                                    foundNum++;
                                                    if (foundNum >= 3) goto IL_69a7;
                                                    goto case 7;
                                                case 7:
                                                case 8:
                                                case 9:
                                                    if (foundPlayer.pos[2] / 32 - z != 0)
                                                    {
                                                        var num9 = PosToInt(x, y,
                                                            (ushort) (z + Math.Sign(foundPlayer.pos[2] / 32 - z)));
                                                        if (GetTile(num9) == 0 && AddUpdate(num9, blocks[C.b])) break;
                                                    }

                                                    foundNum++;
                                                    if (foundNum >= 3) goto IL_69a7;
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
                                                    if (GetTile(x, (ushort) (y - 1), z) == 0 &&
                                                        AddUpdate(PosToInt(x, (ushort) (y - 1), z), blocks[C.b])) break;
                                                    goto case 3;
                                                case 2:
                                                    if (GetTile(x, (ushort) (y + 1), z) == 0 &&
                                                        AddUpdate(PosToInt(x, (ushort) (y + 1), z), blocks[C.b])) break;
                                                    goto case 6;
                                                case 3:
                                                case 4:
                                                case 5:
                                                    if (GetTile((ushort) (x - 1), y, z) == 0 &&
                                                        AddUpdate(PosToInt((ushort) (x - 1), y, z), blocks[C.b])) break;
                                                    goto case 9;
                                                case 6:
                                                case 7:
                                                case 8:
                                                    if (GetTile((ushort) (x + 1), y, z) == 0 &&
                                                        AddUpdate(PosToInt((ushort) (x + 1), y, z), blocks[C.b])) break;
                                                    goto default;
                                                case 9:
                                                case 10:
                                                case 11:
                                                    if (GetTile(x, y, (ushort) (z - 1)) == 0)
                                                    {
                                                        if (!AddUpdate(PosToInt(x, y, (ushort) (z - 1)), blocks[C.b]))
                                                            flag = true;
                                                    }
                                                    else
                                                    {
                                                        flag = true;
                                                    }

                                                    break;
                                                default:
                                                    if (GetTile(x, y, (ushort) (z + 1)) == 0)
                                                    {
                                                        if (!AddUpdate(PosToInt(x, y, (ushort) (z + 1)), blocks[C.b]))
                                                            flag = true;
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

                                    if (!flag) AddUpdate(C.b, 0);
                                    break;
                                case 245:
                                case 246:
                                case 247:
                                case 248:
                                case 249:
                                    if (ai)
                                        Player.players.ForEach(delegate(Player p)
                                        {
                                            if (p.level == this && !p.invincible)
                                            {
                                                currentNum = Math.Abs(p.pos[0] / 32 - x) + Math.Abs(p.pos[1] / 32 - y) +
                                                             Math.Abs(p.pos[2] / 32 - z);
                                                if (currentNum < foundNum)
                                                {
                                                    foundNum = currentNum;
                                                    foundPlayer = p;
                                                }
                                            }
                                        });
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
                                                        var num9 = blocks[C.b] != 249 && blocks[C.b] != 247
                                                            ? PosToInt(
                                                                (ushort) (x - Math.Sign(foundPlayer.pos[0] / 32 - x)),
                                                                y, z)
                                                            : PosToInt(
                                                                (ushort) (x + Math.Sign(foundPlayer.pos[0] / 32 - x)),
                                                                y, z);
                                                        if (GetTile(num9) == 8 && AddUpdate(num9, blocks[C.b])) break;
                                                    }

                                                    foundNum++;
                                                    if (foundNum >= 3) goto IL_70da;
                                                    goto case 4;
                                                case 4:
                                                case 5:
                                                case 6:
                                                    if (foundPlayer.pos[1] / 32 - y != 0)
                                                    {
                                                        var num9 = blocks[C.b] != 249 && blocks[C.b] != 247
                                                            ? PosToInt(x,
                                                                (ushort) (y - Math.Sign(foundPlayer.pos[1] / 32 - y)),
                                                                z)
                                                            : PosToInt(x,
                                                                (ushort) (y + Math.Sign(foundPlayer.pos[1] / 32 - y)),
                                                                z);
                                                        if (GetTile(num9) == 8 && AddUpdate(num9, blocks[C.b])) break;
                                                    }

                                                    foundNum++;
                                                    if (foundNum >= 3) goto IL_70da;
                                                    goto case 7;
                                                case 7:
                                                case 8:
                                                case 9:
                                                    if (foundPlayer.pos[2] / 32 - z != 0)
                                                    {
                                                        var num9 = blocks[C.b] != 249 && blocks[C.b] != 247
                                                            ? PosToInt(x, y,
                                                                (ushort) (z - Math.Sign(foundPlayer.pos[2] / 32 - z)))
                                                            : PosToInt(x, y,
                                                                (ushort) (z + Math.Sign(foundPlayer.pos[2] / 32 - z)));
                                                        if (GetTile(num9) == 8 && AddUpdate(num9, blocks[C.b])) break;
                                                    }

                                                    foundNum++;
                                                    if (foundNum >= 3) goto IL_70da;
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
                                                    if (GetTile(x, (ushort) (y - 1), z) == 8 &&
                                                        AddUpdate(PosToInt(x, (ushort) (y - 1), z), blocks[C.b])) break;
                                                    goto case 3;
                                                case 2:
                                                    if (GetTile(x, (ushort) (y + 1), z) == 8 &&
                                                        AddUpdate(PosToInt(x, (ushort) (y + 1), z), blocks[C.b])) break;
                                                    goto case 6;
                                                case 3:
                                                case 4:
                                                case 5:
                                                    if (GetTile((ushort) (x - 1), y, z) == 8 &&
                                                        AddUpdate(PosToInt((ushort) (x - 1), y, z), blocks[C.b])) break;
                                                    goto case 9;
                                                case 6:
                                                case 7:
                                                case 8:
                                                    if (GetTile((ushort) (x + 1), y, z) == 8 &&
                                                        AddUpdate(PosToInt((ushort) (x + 1), y, z), blocks[C.b])) break;
                                                    goto default;
                                                case 9:
                                                case 10:
                                                case 11:
                                                    if (GetTile(x, y, (ushort) (z - 1)) == 8)
                                                    {
                                                        if (!AddUpdate(PosToInt(x, y, (ushort) (z - 1)), blocks[C.b]))
                                                            flag = true;
                                                    }
                                                    else
                                                    {
                                                        flag = true;
                                                    }

                                                    break;
                                                default:
                                                    if (GetTile(x, y, (ushort) (z + 1)) == 8)
                                                    {
                                                        if (!AddUpdate(PosToInt(x, y, (ushort) (z + 1)), blocks[C.b]))
                                                            flag = true;
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

                                    if (!flag) AddUpdate(C.b, 8);
                                    break;
                                case 250:
                                    if (ai)
                                        Player.players.ForEach(delegate(Player p)
                                        {
                                            if (p.level == this && !p.invincible)
                                            {
                                                currentNum = Math.Abs(p.pos[0] / 32 - x) + Math.Abs(p.pos[1] / 32 - y) +
                                                             Math.Abs(p.pos[2] / 32 - z);
                                                if (currentNum < foundNum)
                                                {
                                                    foundNum = currentNum;
                                                    foundPlayer = p;
                                                }
                                            }
                                        });
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
                                                        var num9 = blocks[C.b] != 250
                                                            ? PosToInt(
                                                                (ushort) (x - Math.Sign(foundPlayer.pos[0] / 32 - x)),
                                                                y, z)
                                                            : PosToInt(
                                                                (ushort) (x + Math.Sign(foundPlayer.pos[0] / 32 - x)),
                                                                y, z);
                                                        if (GetTile(num9) == 10 && AddUpdate(num9, blocks[C.b])) break;
                                                    }

                                                    foundNum++;
                                                    if (foundNum >= 3) goto IL_77cb;
                                                    goto case 4;
                                                case 4:
                                                case 5:
                                                case 6:
                                                    if (foundPlayer.pos[1] / 32 - y != 0)
                                                    {
                                                        var num9 = blocks[C.b] != 250
                                                            ? PosToInt(x,
                                                                (ushort) (y - Math.Sign(foundPlayer.pos[1] / 32 - y)),
                                                                z)
                                                            : PosToInt(x,
                                                                (ushort) (y + Math.Sign(foundPlayer.pos[1] / 32 - y)),
                                                                z);
                                                        if (GetTile(num9) == 10 && AddUpdate(num9, blocks[C.b])) break;
                                                    }

                                                    foundNum++;
                                                    if (foundNum >= 3) goto IL_77cb;
                                                    goto case 7;
                                                case 7:
                                                case 8:
                                                case 9:
                                                    if (foundPlayer.pos[2] / 32 - z != 0)
                                                    {
                                                        var num9 = blocks[C.b] != 250
                                                            ? PosToInt(x, y,
                                                                (ushort) (z - Math.Sign(foundPlayer.pos[2] / 32 - z)))
                                                            : PosToInt(x, y,
                                                                (ushort) (z + Math.Sign(foundPlayer.pos[2] / 32 - z)));
                                                        if (GetTile(num9) == 10 && AddUpdate(num9, blocks[C.b])) break;
                                                    }

                                                    foundNum++;
                                                    if (foundNum >= 3) goto IL_77cb;
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
                                                    if (GetTile(x, (ushort) (y - 1), z) == 10 &&
                                                        AddUpdate(PosToInt(x, (ushort) (y - 1), z), blocks[C.b])) break;
                                                    goto case 3;
                                                case 2:
                                                    if (GetTile(x, (ushort) (y + 1), z) == 10 &&
                                                        AddUpdate(PosToInt(x, (ushort) (y + 1), z), blocks[C.b])) break;
                                                    goto case 6;
                                                case 3:
                                                case 4:
                                                case 5:
                                                    if (GetTile((ushort) (x - 1), y, z) == 10 &&
                                                        AddUpdate(PosToInt((ushort) (x - 1), y, z), blocks[C.b])) break;
                                                    goto case 9;
                                                case 6:
                                                case 7:
                                                case 8:
                                                    if (GetTile((ushort) (x + 1), y, z) == 10 &&
                                                        AddUpdate(PosToInt((ushort) (x + 1), y, z), blocks[C.b])) break;
                                                    goto default;
                                                case 9:
                                                case 10:
                                                case 11:
                                                    if (GetTile(x, y, (ushort) (z - 1)) == 10)
                                                    {
                                                        if (!AddUpdate(PosToInt(x, y, (ushort) (z - 1)), blocks[C.b]))
                                                            flag = true;
                                                    }
                                                    else
                                                    {
                                                        flag = true;
                                                    }

                                                    break;
                                                default:
                                                    if (GetTile(x, y, (ushort) (z + 1)) == 10)
                                                    {
                                                        if (!AddUpdate(PosToInt(x, y, (ushort) (z + 1)), blocks[C.b]))
                                                            flag = true;
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

                                    if (!flag) AddUpdate(C.b, 10);
                                    break;
                                case 188:
                                {
                                    if (rand.Next(1, 10) <= 5)
                                        mx = 1;
                                    else
                                        mx = -1;
                                    if (rand.Next(1, 10) <= 5)
                                        my = 1;
                                    else
                                        my = -1;
                                    if (rand.Next(1, 10) <= 5)
                                        mz = 1;
                                    else
                                        mz = -1;
                                    for (var j = -1 * mx; j != mx + mx; j += mx)
                                    {
                                        if (flag) break;
                                        for (var k = -1 * my; k != my + my; k += my)
                                        {
                                            if (flag) break;
                                            for (var l = -1 * mz; l != mz + mz; l += mz)
                                            {
                                                if (flag) break;
                                                if (GetTile((ushort) (x + j), (ushort) (y + k), (ushort) (z + l)) ==
                                                    185)
                                                {
                                                    if (GetTile((ushort) (x - j), (ushort) (y - k), (ushort) (z - l)) ==
                                                        0 || GetTile((ushort) (x - j), (ushort) (y - k),
                                                            (ushort) (z - l)) == 187)
                                                    {
                                                        AddUpdate(
                                                            PosToInt((ushort) (x - j), (ushort) (y - k),
                                                                (ushort) (z - l)), 188);
                                                        AddUpdate(PosToInt(x, y, z), 185);
                                                    }
                                                    else if (GetTile((ushort) (x - j), (ushort) (y - k),
                                                        (ushort) (z - l)) != 185)
                                                    {
                                                        if (physics > 2)
                                                            MakeExplosion(x, y, z, 2);
                                                        else
                                                            AddUpdate(PosToInt(x, y, z), 185);
                                                    }

                                                    flag = true;
                                                }
                                            }
                                        }
                                    }

                                    break;
                                }
                                case 189:
                                    if (GetTile(x, (ushort) (y - 1), z) == 11)
                                    {
                                        if (GetTile(x, (ushort) (y + 1), z) == 0)
                                        {
                                            if (height / 100 * 80 < y)
                                                mx = rand.Next(1, 20);
                                            else
                                                mx = 5;
                                            if (mx > 1)
                                            {
                                                AddUpdate(PosToInt(x, (ushort) (y + 1), z), 189);
                                                AddUpdate(PosToInt(x, y, z), 11, false, "wait 1 dissipate 90");
                                                C.extraInfo = "wait 1 dissipate 100";
                                                break;
                                            }
                                        }

                                        Firework(x, y, z, 4);
                                    }

                                    break;
                                case 233:
                                    if (GetTile(IntOffset(C.b, 0, -1, 0)) != 232 &&
                                        GetTile(IntOffset(C.b, 0, -1, 0)) != 231) C.extraInfo = "revert 0";
                                    break;
                                case 231:
                                case 232:
                                    if (GetTile(x, (ushort) (y - 1), z) == 0)
                                    {
                                        AddUpdate(C.b, 233);
                                        AddUpdate(IntOffset(C.b, 0, -1, 0), blocks[C.b]);
                                        AddUpdate(IntOffset(C.b, 0, 1, 0), 0);
                                    }
                                    else
                                    {
                                        if (ai)
                                            Player.players.ForEach(delegate(Player p)
                                            {
                                                if (p.level == this && !p.invincible)
                                                {
                                                    currentNum = Math.Abs(p.pos[0] / 32 - x) +
                                                                 Math.Abs(p.pos[1] / 32 - y) +
                                                                 Math.Abs(p.pos[2] / 32 - z);
                                                    if (currentNum < foundNum)
                                                    {
                                                        foundNum = currentNum;
                                                        foundPlayer = p;
                                                    }
                                                }
                                            });
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
                                                            var num9 = PosToInt(
                                                                (ushort) (x + Math.Sign(foundPlayer.pos[0] / 32 - x)),
                                                                y, z);
                                                            if (GetTile(IntOffset(num9, 0, -1, 0)) == 0 &&
                                                                GetTile(num9) == 0)
                                                            {
                                                                num9 = IntOffset(num9, 0, -1, 0);
                                                            }
                                                            else if (GetTile(num9) != 0 ||
                                                                     GetTile(IntOffset(num9, 0, 1, 0)) != 0)
                                                            {
                                                                if (GetTile(IntOffset(num9, 0, 2, 0)) == 0 &&
                                                                    GetTile(IntOffset(num9, 0, 1, 0)) == 0)
                                                                    num9 = IntOffset(num9, 0, 1, 0);
                                                                else
                                                                    flag2 = true;
                                                            }

                                                            if (!flag2 && AddUpdate(num9, blocks[C.b]))
                                                            {
                                                                AddUpdate(IntOffset(num9, 0, 1, 0), 233);
                                                                break;
                                                            }
                                                        }

                                                        foundNum++;
                                                        if (foundNum >= 2) goto IL_835a;
                                                        goto case 4;
                                                    case 4:
                                                    case 5:
                                                    case 6:
                                                        if (foundPlayer.pos[2] / 32 - z != 0)
                                                        {
                                                            flag2 = false;
                                                            var num9 = PosToInt(x, y,
                                                                (ushort) (z + Math.Sign(foundPlayer.pos[2] / 32 - z)));
                                                            if (GetTile(IntOffset(num9, 0, -1, 0)) == 0 &&
                                                                GetTile(num9) == 0)
                                                            {
                                                                num9 = IntOffset(num9, 0, -1, 0);
                                                            }
                                                            else if (GetTile(num9) != 0 ||
                                                                     GetTile(IntOffset(num9, 0, 1, 0)) != 0)
                                                            {
                                                                if (GetTile(IntOffset(num9, 0, 2, 0)) == 0 &&
                                                                    GetTile(IntOffset(num9, 0, 1, 0)) == 0)
                                                                    num9 = IntOffset(num9, 0, 1, 0);
                                                                else
                                                                    flag2 = true;
                                                            }

                                                            if (!flag2 && AddUpdate(num9, blocks[C.b]))
                                                            {
                                                                AddUpdate(IntOffset(num9, 0, 1, 0), 233);
                                                                break;
                                                            }
                                                        }

                                                        foundNum++;
                                                        if (foundNum >= 2) goto IL_835a;
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
                                                        var num9 = IntOffset(C.b, -1, 0, 0);
                                                        if (GetTile(IntOffset(num9, 0, -1, 0)) == 0 &&
                                                            GetTile(num9) == 0)
                                                        {
                                                            num9 = IntOffset(num9, 0, -1, 0);
                                                        }
                                                        else if (GetTile(num9) != 0 ||
                                                                 GetTile(IntOffset(num9, 0, 1, 0)) != 0)
                                                        {
                                                            if (GetTile(IntOffset(num9, 0, 2, 0)) == 0 &&
                                                                GetTile(IntOffset(num9, 0, 1, 0)) == 0)
                                                                num9 = IntOffset(num9, 0, 1, 0);
                                                            else
                                                                flag2 = true;
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
                                                        var num9 = IntOffset(C.b, 1, 0, 0);
                                                        if (GetTile(IntOffset(num9, 0, -1, 0)) == 0 &&
                                                            GetTile(num9) == 0)
                                                        {
                                                            num9 = IntOffset(num9, 0, -1, 0);
                                                        }
                                                        else if (GetTile(num9) != 0 ||
                                                                 GetTile(IntOffset(num9, 0, 1, 0)) != 0)
                                                        {
                                                            if (GetTile(IntOffset(num9, 0, 2, 0)) == 0 &&
                                                                GetTile(IntOffset(num9, 0, 1, 0)) == 0)
                                                                num9 = IntOffset(num9, 0, 1, 0);
                                                            else
                                                                flag2 = true;
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
                                                        var num9 = IntOffset(C.b, 0, 0, 1);
                                                        if (GetTile(IntOffset(num9, 0, -1, 0)) == 0 &&
                                                            GetTile(num9) == 0)
                                                        {
                                                            num9 = IntOffset(num9, 0, -1, 0);
                                                        }
                                                        else if (GetTile(num9) != 0 ||
                                                                 GetTile(IntOffset(num9, 0, 1, 0)) != 0)
                                                        {
                                                            if (GetTile(IntOffset(num9, 0, 2, 0)) == 0 &&
                                                                GetTile(IntOffset(num9, 0, 1, 0)) == 0)
                                                                num9 = IntOffset(num9, 0, 1, 0);
                                                            else
                                                                flag2 = true;
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
                                                        var num9 = IntOffset(C.b, 0, 0, -1);
                                                        if (GetTile(IntOffset(num9, 0, -1, 0)) == 0 &&
                                                            GetTile(num9) == 0)
                                                        {
                                                            num9 = IntOffset(num9, 0, -1, 0);
                                                        }
                                                        else if (GetTile(num9) != 0 ||
                                                                 GetTile(IntOffset(num9, 0, 1, 0)) != 0)
                                                        {
                                                            if (GetTile(IntOffset(num9, 0, 2, 0)) == 0 &&
                                                                GetTile(IntOffset(num9, 0, 1, 0)) == 0)
                                                                num9 = IntOffset(num9, 0, 1, 0);
                                                            else
                                                                flag2 = true;
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
                                    if (!C.extraInfo.Contains("wait")) C.time = byte.MaxValue;
                                    break;
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
                    var count = ListUpdate.Count;
                    var trim = 0;
                    var iterator = 0;
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
                                var tile = GetTile(x, y, z);
                                if (Block.OPBlocks(tile))
                                {
                                    trim++;
                                }
                                else
                                {
                                    physBlockUpdateList.Add(6);
                                    var bytes = BitConverter.GetBytes(x);
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
                                        PhysUniversalSpongeRemoved(PosToInt(x, y, z));
                                    if (tile == 19 && physics > 0 && utemp.type != 19)
                                        PhysUniversalSpongeRemoved(PosToInt(x, y, z));
                                    try
                                    {
                                        var undoPos = default(UndoPos);
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
                                    catch (Exception ex2)
                                    {
                                        Server.s.Log("TUM TUM");
                                        Server.ErrorLog(ex2);
                                    }

                                    SetTile(x, y, z, utemp.type);
                                    if (physics > 0 && (Block.Physics(utemp.type) || utemp.extraInfo != ""))
                                        AddCheck(PosToInt(x, y, z), utemp.extraInfo);
                                }
                            }
                        }
                        catch (Exception ex3)
                        {
                            Server.ErrorLog(ex3);
                            Server.s.Log("Phys update issue");
                        }
                    });
                    ListUpdate.Clear();
                    if (physBlockUpdateList.Count > 0) Blockchange(physBlockUpdateList.ToArray());
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
            catch (Exception ex)
            {
                Server.s.Log("Level physics error");
                Server.ErrorLog(ex);
            }
        }

        // Token: 0x0600178E RID: 6030 RVA: 0x00094B04 File Offset: 0x00092D04
        public void AddCheck(int b, string extraInfo = "", bool overRide = false)
        {
            try
            {
                if (!ListCheck.Exists(Check => Check.b == b))
                    ListCheck.Add(new Check(b, extraInfo));
                else if (overRide)
                    foreach (var check in ListCheck)
                        if (check.b == b)
                        {
                            check.extraInfo = extraInfo;
                            break;
                        }
            }
            catch
            {
            }
        }

        // Token: 0x0600178F RID: 6031 RVA: 0x00094BC4 File Offset: 0x00092DC4
        public void LavaWild(int b, byte type)
        {
            if (indexLW == 0) changeLW = rand_x.Next(0, 10);
            if (indexLW == changeLW) AddUpdate(b, type);
            indexLW++;
            if (indexLW > 9) indexLW = 0;
        }

        // Token: 0x06001790 RID: 6032 RVA: 0x00094C28 File Offset: 0x00092E28
        public void LavaDisturbed(int b, byte type)
        {
            if (indexLD == 0) changeLD = rand_x.Next(0, 30);
            if (indexLD == changeLD) AddUpdate(b, type);
            indexLD++;
            if (indexLD > 29) indexLD = 0;
        }

        // Token: 0x06001791 RID: 6033 RVA: 0x00094C8C File Offset: 0x00092E8C
        public void LavaRageGlass(int b)
        {
            if (indexLRG == 0) changeLRG = randLRG.Next(0, 19);
            if (indexLRG == changeLRG)
                AddUpdate(b, 254);
            else
                AddUpdate(b, 20);
            indexLRG++;
            if (indexLRG > 20) indexLRG = 0;
        }

        // Token: 0x06001792 RID: 6034 RVA: 0x00094D08 File Offset: 0x00092F08
        public void LavaRage(int b, byte type)
        {
            if (indexLR == 0) change = rand_x.Next(0, 19);
            if (indexLR == change) AddUpdate(b, type);
            indexLR++;
            if (indexLR > 18) indexLR = 0;
        }

        // Token: 0x06001793 RID: 6035 RVA: 0x00094D6C File Offset: 0x00092F6C
        public void LavaRageObsidian(int b, byte type)
        {
            if (indexLRO == 0) changeO = rand_xx.Next(0, 22);
            if (indexLR == changeO) AddUpdate(b, type);
            indexLRO++;
            if (indexLRO > 21) indexLRO = 0;
        }

        // Token: 0x06001794 RID: 6036 RVA: 0x00094DD0 File Offset: 0x00092FD0
        public bool AddUpdate(int b, int type, bool overRide = false, string extraInfo = "")
        {
            bool result;
            try
            {
                if (overRide)
                {
                    ushort x;
                    ushort y;
                    ushort z;
                    IntToPos(b, out x, out y, out z);
                    AddCheck(b, extraInfo);
                    Blockchange(x, y, z, (byte) type, true);
                    result = true;
                }
                else if (!ListUpdate.Exists(Update => Update.b == b))
                {
                    ListUpdate.Add(new Update(b, (byte) type, extraInfo));
                    result = true;
                }
                else if (type == 12 || type == 13)
                {
                    ListUpdate.RemoveAll(Update => Update.b == b);
                    ListUpdate.Add(new Update(b, (byte) type, extraInfo));
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch
            {
                result = false;
            }

            return result;
        }

        // Token: 0x06001795 RID: 6037 RVA: 0x00094ED8 File Offset: 0x000930D8
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
                        var num = 0;
                        foreach (var a in C.extraInfo.Split(' '))
                        {
                            if (a == "revert")
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

        // Token: 0x06001796 RID: 6038 RVA: 0x00094F20 File Offset: 0x00093120
        private void PhysWater(int b, byte type)
        {
            if (b == -1) return;
            var b2 = blocks[b];
            if (b2 > 6)
            {
                switch (b2)
                {
                    case 10:
                        break;
                    case 11:
                        return;
                    case 12:
                    case 13:
                        goto IL_B7;
                    default:
                        switch (b2)
                        {
                            case 37:
                            case 38:
                            case 39:
                            case 40:
                                goto IL_95;
                            default:
                                switch (b2)
                                {
                                    case 110:
                                        goto IL_B7;
                                    case 111:
                                        return;
                                    case 112:
                                        break;
                                    default:
                                        return;
                                }

                                break;
                        }

                        break;
                }

                if (!PhysSpongeCheck(b))
                {
                    AddUpdate(b, 1);
                    return;
                }

                return;
                IL_B7:
                AddCheck(b);
                return;
            }

            if (b2 != 0)
            {
                if (b2 != 6) return;
            }
            else
            {
                if (!PhysSpongeCheck(b))
                {
                    AddUpdate(b, type);
                    return;
                }

                return;
            }

            IL_95:
            if (physics > 1 && !PhysSpongeCheck(b)) AddUpdate(b, 0);
        }

        // Token: 0x06001797 RID: 6039 RVA: 0x00094FF4 File Offset: 0x000931F4
        private void PhysLava(int b, byte type)
        {
            if (b == -1) return;
            var b2 = blocks[b];
            if (b2 <= 83)
            {
                if (b2 <= 39)
                {
                    switch (b2)
                    {
                        case 0:
                            AddUpdate(b, type);
                            return;
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 9:
                        case 10:
                        case 11:
                        case 14:
                        case 15:
                            goto IL_25F;
                        case 5:
                        case 6:
                        case 17:
                        case 18:
                            break;
                        case 7:
                            return;
                        case 8:
                            AddUpdate(b, 49);
                            return;
                        case 12:
                            if (physics > 1)
                            {
                                LavaRageGlass(b);
                                AddUpdate(b, 20);
                                return;
                            }

                            AddCheck(b);
                            return;
                        case 13:
                            AddCheck(b);
                            if (LavaSettings.All.LavaState == LavaState.Disturbed)
                            {
                                LavaDisturbed(b, 79);
                                return;
                            }

                            if (LavaSettings.All.LavaState == LavaState.Furious)
                            {
                                LavaRage(b, 79);
                                return;
                            }

                            if (LavaSettings.All.LavaState == LavaState.Wild)
                            {
                                LavaWild(b, 79);
                                return;
                            }

                            return;
                        case 16:
                            if (LavaSettings.All.LavaState > LavaState.Calm)
                            {
                                AddUpdate(b, 0);
                                return;
                            }

                            return;
                        default:
                            switch (b2)
                            {
                                case 37:
                                case 38:
                                case 39:
                                    break;
                                default:
                                    goto IL_25F;
                            }

                            break;
                    }

                    if (physics <= 1) return;
                    if (LavaSettings.All.LavaState == LavaState.Disturbed)
                    {
                        LavaDisturbed(b, 79);
                        return;
                    }

                    if (LavaSettings.All.LavaState == LavaState.Furious)
                    {
                        AddUpdate(b, 0);
                        return;
                    }

                    if (LavaSettings.All.LavaState != LavaState.Wild) return;
                    AddUpdate(b, 0);
                }
                else if (b2 != 49)
                {
                    switch (b2)
                    {
                        case 79:
                        case 80:
                        case 81:
                        case 82:
                        case 83:
                            break;
                        default:
                            goto IL_25F;
                    }
                }
                else
                {
                    if (LavaSettings.All.LavaState == LavaState.Disturbed) LavaDisturbed(b, 79);
                    if (LavaSettings.All.LavaState == LavaState.Furious) LavaRageObsidian(b, 79);
                    if (LavaSettings.All.LavaState == LavaState.Wild)
                    {
                        LavaWild(b, 79);
                        return;
                    }

                    return;
                }
            }
            else if (b2 <= 105)
            {
                if (b2 != 98 && b2 != 105) goto IL_25F;
            }
            else if (b2 != 112)
            {
                switch (b2)
                {
                    case 194:
                    case 195:
                        break;
                    default:
                        if (b2 != 220) goto IL_25F;
                        break;
                }
            }

            return;
            IL_25F:
            if (LavaSettings.All.LavaState == LavaState.Disturbed)
            {
                LavaDisturbed(b, 79);
                return;
            }

            if (LavaSettings.All.LavaState == LavaState.Furious)
            {
                LavaRage(b, 79);
                return;
            }

            if (LavaSettings.All.LavaState == LavaState.Wild) LavaWild(b, 79);
        }

        // Token: 0x06001798 RID: 6040 RVA: 0x000952A4 File Offset: 0x000934A4
        private void PhysAir(int b)
        {
            if (b == -1) return;
            if (Block.Convert(blocks[b]) == 8 || Block.Convert(blocks[b]) == 10)
            {
                AddCheck(b);
                return;
            }

            var b2 = blocks[b];
            switch (b2)
            {
                case 12:
                case 13:
                    break;
                default:
                    if (b2 != 110) return;
                    break;
            }

            AddCheck(b);
        }

        // Token: 0x06001799 RID: 6041 RVA: 0x00095314 File Offset: 0x00093514
        private void PhysCoal(int b)
        {
            if (b == -1) return;
            if (Block.Convert(blocks[b]) == 8 || Block.Convert(blocks[b]) == 10)
            {
                AddCheck(b);
                return;
            }

            var b2 = blocks[b];
            switch (b2)
            {
                case 12:
                case 13:
                    break;
                default:
                    if (b2 != 110) return;
                    break;
            }

            AddCheck(b);
        }

        // Token: 0x0600179A RID: 6042 RVA: 0x00095384 File Offset: 0x00093584
        private void PhysGlass(int b)
        {
            if (b == -1) return;
            if (Block.Convert(blocks[b]) == 8 || Block.Convert(blocks[b]) == 10) AddCheck(b);
        }

        // Token: 0x0600179B RID: 6043 RVA: 0x000953BC File Offset: 0x000935BC
        private bool PhysSand(int b, byte type)
        {
            if (b == -1 || physics == 0) return false;
            var num = b;
            var flag = false;
            var flag2 = false;
            do
            {
                num = IntOffset(num, 0, -1, 0);
                if (GetTile(num) != 255)
                {
                    var b2 = blocks[num];
                    if (b2 == 0) goto IL_71;
                    switch (b2)
                    {
                        case 6:
                            break;
                        case 7:
                        case 9:
                            goto IL_86;
                        case 8:
                        case 10:
                            goto IL_71;
                        default:
                            switch (b2)
                            {
                                case 37:
                                case 38:
                                case 39:
                                case 40:
                                    break;
                                default:
                                    goto IL_86;
                            }

                            break;
                    }

                    if (physics > 1)
                    {
                        flag2 = true;
                        goto IL_88;
                    }

                    flag = true;
                    goto IL_88;
                    IL_86:
                    flag = true;
                    IL_88:
                    if (physics > 1) flag = true;
                    goto IL_97;
                    IL_71:
                    flag2 = true;
                    goto IL_88;
                }

                flag = true;
                IL_97: ;
            } while (!flag);

            if (flag2)
            {
                AddUpdate(b, 0);
                if (physics > 1)
                    AddUpdate(num, type);
                else
                    AddUpdate(IntOffset(num, 0, 1, 0), type);
            }

            return flag2;
        }

        // Token: 0x0600179C RID: 6044 RVA: 0x000954AC File Offset: 0x000936AC
        private void PhysSandCheck(int b)
        {
            if (b == -1) return;
            var b2 = blocks[b];
            switch (b2)
            {
                case 12:
                case 13:
                    break;
                default:
                    if (b2 != 110) return;
                    break;
            }

            AddCheck(b);
        }

        // Token: 0x0600179D RID: 6045 RVA: 0x000954EC File Offset: 0x000936EC
        private void PhysStair(int b)
        {
            var b2 = IntOffset(b, 0, -1, 0);
            if (GetTile(b2) != 255 && GetTile(b2) == 44)
            {
                AddUpdate(b, 0);
                AddUpdate(b2, 43);
            }
        }

        // Token: 0x0600179E RID: 6046 RVA: 0x0009553C File Offset: 0x0009373C
        private bool PhysSpongeCheck(int b)
        {
            for (var i = -3; i <= 3; i++)
            for (var j = -3; j <= 3; j++)
            for (var k = -3; k <= 3; k++)
            {
                var tile = GetTile(IntOffset(b, i, j, k));
                if (tile != 255 && (tile == 19 || tile == 78)) return true;
            }

            return false;
        }

        // Token: 0x0600179F RID: 6047 RVA: 0x00095598 File Offset: 0x00093798
        private bool PhysUniversalSpongeCheck(int b)
        {
            for (var i = -2; i <= 2; i++)
            for (var j = -2; j <= 2; j++)
            for (var k = -2; k <= 2; k++)
            {
                var tile = GetTile(IntOffset(b, i, j, k));
                if (tile != 255 && (tile == 19 || tile == 78)) return true;
            }

            return false;
        }

        // Token: 0x060017A0 RID: 6048 RVA: 0x000955F4 File Offset: 0x000937F4
        private void PhysSponge(int b)
        {
            for (var i = -2; i <= 2; i++)
            for (var j = -2; j <= 2; j++)
            for (var k = -2; k <= 2; k++)
            {
                var b2 = IntOffset(b, i, j, k);
                if (GetTile(b2) != 255 && GetTile(b2) == 8) AddUpdate(b2, 0);
            }
        }

        // Token: 0x060017A1 RID: 6049 RVA: 0x0009565C File Offset: 0x0009385C
        private void PhysUniversalSponge(int b)
        {
            for (var i = -2; i <= 2; i++)
            for (var j = -2; j <= 2; j++)
            for (var k = -2; k <= 2; k++)
            {
                var b2 = IntOffset(b, i, j, k);
                var tile = GetTile(b2);
                if (tile != 255)
                {
                    var b3 = tile;
                    if (b3 <= 83)
                        switch (b3)
                        {
                            case 8:
                            case 10:
                            case 11:
                                break;
                            case 9:
                                goto IL_B5;
                            default:
                                switch (b3)
                                {
                                    case 80:
                                    case 81:
                                    case 82:
                                    case 83:
                                        break;
                                    default:
                                        goto IL_B5;
                                }

                                break;
                        }
                    else if (b3 != 98)
                        switch (b3)
                        {
                            case 190:
                            case 193:
                            case 194:
                            case 195:
                                break;
                            case 191:
                            case 192:
                                goto IL_B5;
                            default:
                                goto IL_B5;
                        }

                    AddUpdate(b2, 0);
                }

                IL_B5: ;
            }
        }

        // Token: 0x060017A2 RID: 6050 RVA: 0x00095744 File Offset: 0x00093944
        public void PhysUniversalSpongeRemoved(int b)
        {
            for (var i = -3; i <= 3; i++)
            for (var j = -3; j <= 3; j++)
            for (var k = -3; k <= 3; k++)
            {
                var b2 = IntOffset(b, i, j, k);
                var tile = GetTile(b2);
                if (tile != 255)
                {
                    var b3 = tile;
                    switch (b3)
                    {
                        case 8:
                        case 10:
                        case 11:
                            break;
                        case 9:
                            goto IL_A5;
                        default:
                            switch (b3)
                            {
                                case 80:
                                case 81:
                                case 82:
                                case 83:
                                    break;
                                default:
                                    switch (b3)
                                    {
                                        case 190:
                                        case 193:
                                        case 194:
                                        case 195:
                                            break;
                                        case 191:
                                        case 192:
                                            goto IL_A5;
                                        default:
                                            goto IL_A5;
                                    }

                                    break;
                            }

                            break;
                    }

                    AddCheck(b2);
                }

                IL_A5: ;
            }
        }

        // Token: 0x060017A3 RID: 6051 RVA: 0x0009581C File Offset: 0x00093A1C
        public void PhysSpongeRemoved(int b)
        {
            for (var i = -3; i <= 3; i++)
            for (var j = -3; j <= 3; j++)
            for (var k = -3; k <= 3; k++)
            {
                var b2 = IntOffset(b, i, j, k);
                if (GetTile(b2) != 255 && GetTile(b2) == 8) AddCheck(b2);
            }
        }

        // Token: 0x060017A4 RID: 6052 RVA: 0x00095884 File Offset: 0x00093A84
        private void PhysFloatwood(int b)
        {
            var b2 = IntOffset(b, 0, -1, 0);
            if (GetTile(b2) != 255 && GetTile(b2) == 0)
            {
                AddUpdate(b, 0);
                AddUpdate(b2, 110);
                return;
            }

            b2 = IntOffset(b, 0, 1, 0);
            if (GetTile(b2) != 255 && GetTile(b2) == 8)
            {
                AddUpdate(b, 8);
                AddUpdate(b2, 110);
            }
        }

        // Token: 0x060017A5 RID: 6053 RVA: 0x00095918 File Offset: 0x00093B18
        private void PhysAirFlood(int b, byte type)
        {
            if (b == -1) return;
            if (Block.Convert(blocks[b]) == 8 || Block.Convert(blocks[b]) == 10) AddUpdate(b, type);
        }

        // Token: 0x060017A6 RID: 6054 RVA: 0x00095950 File Offset: 0x00093B50
        private void PhysFall(byte newBlock, ushort x, ushort y, ushort z, bool random)
        {
            var random2 = new Random();
            if (!random)
            {
                var tile = GetTile((ushort) (x + 1), y, z);
                if (tile == 0 || tile == 9) Blockchange((ushort) (x + 1), y, z, newBlock);
                tile = GetTile((ushort) (x - 1), y, z);
                if (tile == 0 || tile == 9) Blockchange((ushort) (x - 1), y, z, newBlock);
                tile = GetTile(x, y, (ushort) (z + 1));
                if (tile == 0 || tile == 9) Blockchange(x, y, (ushort) (z + 1), newBlock);
                tile = GetTile(x, y, (ushort) (z - 1));
                if (tile == 0 || tile == 9) Blockchange(x, y, (ushort) (z - 1), newBlock);
            }
            else
            {
                if (GetTile((ushort) (x + 1), y, z) == 0 && random2.Next(1, 10) < 3)
                    Blockchange((ushort) (x + 1), y, z, newBlock);
                if (GetTile((ushort) (x - 1), y, z) == 0 && random2.Next(1, 10) < 3)
                    Blockchange((ushort) (x - 1), y, z, newBlock);
                if (GetTile(x, y, (ushort) (z + 1)) == 0 && random2.Next(1, 10) < 3)
                    Blockchange(x, y, (ushort) (z + 1), newBlock);
                if (GetTile(x, y, (ushort) (z - 1)) == 0 && random2.Next(1, 10) < 3)
                    Blockchange(x, y, (ushort) (z - 1), newBlock);
            }
        }

        // Token: 0x060017A7 RID: 6055 RVA: 0x00095AD4 File Offset: 0x00093CD4
        private void PhysReplace(int b, byte typeA, byte typeB)
        {
            if (b == -1) return;
            if (blocks[b] == typeA) AddUpdate(b, typeB);
        }

        // Token: 0x060017A8 RID: 6056 RVA: 0x00095AF8 File Offset: 0x00093CF8
        public void odoor(Check C)
        {
            if (C.time == 0)
            {
                var b = Block.odoor(GetTile(IntOffset(C.b, -1, 0, 0)));
                if (b == blocks[C.b]) AddUpdate(IntOffset(C.b, -1, 0, 0), b, true);
                b = Block.odoor(GetTile(IntOffset(C.b, 1, 0, 0)));
                if (b == blocks[C.b]) AddUpdate(IntOffset(C.b, 1, 0, 0), b, true);
                b = Block.odoor(GetTile(IntOffset(C.b, 0, -1, 0)));
                if (b == blocks[C.b]) AddUpdate(IntOffset(C.b, 0, -1, 0), b, true);
                b = Block.odoor(GetTile(IntOffset(C.b, 0, 1, 0)));
                if (b == blocks[C.b]) AddUpdate(IntOffset(C.b, 0, 1, 0), b, true);
                b = Block.odoor(GetTile(IntOffset(C.b, 0, 0, -1)));
                if (b == blocks[C.b]) AddUpdate(IntOffset(C.b, 0, 0, -1), b, true);
                b = Block.odoor(GetTile(IntOffset(C.b, 0, 0, 1)));
                if (b == blocks[C.b]) AddUpdate(IntOffset(C.b, 0, 0, 1), b, true);
            }
            else
            {
                C.time = byte.MaxValue;
            }

            C.time += 1;
        }

        // Token: 0x060017A9 RID: 6057 RVA: 0x00095CDC File Offset: 0x00093EDC
        public void AnyDoor(Check C, ushort x, ushort y, ushort z, int timer, bool instaUpdate = false)
        {
            if (C.time == 0)
            {
                try
                {
                    PhysDoor((ushort) (x + 1), y, z, instaUpdate);
                }
                catch
                {
                }

                try
                {
                    PhysDoor((ushort) (x - 1), y, z, instaUpdate);
                }
                catch
                {
                }

                try
                {
                    PhysDoor(x, y, (ushort) (z + 1), instaUpdate);
                }
                catch
                {
                }

                try
                {
                    PhysDoor(x, y, (ushort) (z - 1), instaUpdate);
                }
                catch
                {
                }

                try
                {
                    PhysDoor(x, (ushort) (y - 1), z, instaUpdate);
                }
                catch
                {
                }

                try
                {
                    PhysDoor(x, (ushort) (y + 1), z, instaUpdate);
                }
                catch
                {
                }

                try
                {
                    if (blocks[C.b] == 211)
                        for (var i = -1; i <= 1; i++)
                        for (var j = -1; j <= 1; j++)
                        for (var k = -1; k <= 1; k++)
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
                                    MakeExplosion((ushort) (x + i), (ushort) (y + j), (ushort) (z + k), 0);
                                    break;
                            }
                }
                catch
                {
                }
            }

            if (C.time < timer)
            {
                C.time++;
                return;
            }

            AddUpdate(C.b, Block.SaveConvert(blocks[C.b]));
            C.time = byte.MaxValue;
        }

        // Token: 0x060017AA RID: 6058 RVA: 0x00095F88 File Offset: 0x00094188
        public void PhysDoor(ushort x, ushort y, ushort z, bool instaUpdate)
        {
            var num = PosToInt(x, y, z);
            var b = Block.DoorAirs(blocks[num]);
            if (b == 0)
            {
                if (Block.tDoor(blocks[num])) AddUpdate(num, 0, false, "wait 16 door 1 revert " + blocks[num]);
                if (Block.odoor(blocks[num]) != 255) AddUpdate(num, Block.odoor(blocks[num]), true);
                return;
            }

            if (!instaUpdate)
            {
                AddUpdate(num, b);
                return;
            }

            Blockchange(x, y, z, b);
        }

        // Token: 0x060017AB RID: 6059 RVA: 0x00096038 File Offset: 0x00094238
        public void MakeExplosion(ushort x, ushort y, ushort z, int size)
        {
            var random = new Random();
            if (physics < 2) return;
            AddUpdate(PosToInt(x, y, z), 184, true);
            for (var i = x - (size + 1); i <= (int) x + size + 1; i++)
            for (var j = y - (size + 1); j <= (int) y + size + 1; j++)
            for (var k = z - (size + 1); k <= (int) z + size + 1; k++)
            {
                var tile = GetTile((ushort) i, (ushort) j, (ushort) k);
                if (tile != 255)
                {
                    if (tile == 46)
                    {
                        AddUpdate(PosToInt((ushort) i, (ushort) j, (ushort) k), 182);
                    }
                    else if (tile != 182 && tile != 183)
                    {
                        if (random.Next(1, 11) <= 4)
                            AddUpdate(PosToInt((ushort) i, (ushort) j, (ushort) k), 184);
                        else if (random.Next(1, 11) <= 8)
                            AddUpdate(PosToInt((ushort) i, (ushort) j, (ushort) k), 0);
                        else
                            AddCheck(PosToInt((ushort) i, (ushort) j, (ushort) k), "drop 50 dissipate 8");
                    }
                    else
                    {
                        AddCheck(PosToInt((ushort) i, (ushort) j, (ushort) k));
                    }
                }
            }

            for (var i = x - (size + 2); i <= (int) x + size + 2; i++)
            for (var j = y - (size + 2); j <= (int) y + size + 2; j++)
            for (var k = z - (size + 2); k <= (int) z + size + 2; k++)
            {
                var tile = GetTile((ushort) i, (ushort) j, (ushort) k);
                if (tile != 255)
                {
                    if (random.Next(1, 10) < 7 && Block.Convert(tile) != 46)
                    {
                        if (random.Next(1, 11) <= 4)
                            AddUpdate(PosToInt((ushort) i, (ushort) j, (ushort) k), 184);
                        else if (random.Next(1, 11) <= 8)
                            AddUpdate(PosToInt((ushort) i, (ushort) j, (ushort) k), 0);
                        else
                            AddCheck(PosToInt((ushort) i, (ushort) j, (ushort) k), "drop 50 dissipate 8");
                    }

                    if (tile == 46)
                        AddUpdate(PosToInt((ushort) i, (ushort) j, (ushort) k), 182);
                    else if (tile == 182 || tile == 183) AddCheck(PosToInt((ushort) i, (ushort) j, (ushort) k));
                }
            }

            for (var i = x - (size + 3); i <= (int) x + size + 3; i++)
            for (var j = y - (size + 3); j <= (int) y + size + 3; j++)
            for (var k = z - (size + 3); k <= (int) z + size + 3; k++)
            {
                var tile = GetTile((ushort) i, (ushort) j, (ushort) k);
                if (tile != 255)
                {
                    if (random.Next(1, 10) < 3 && Block.Convert(tile) != 46)
                    {
                        if (random.Next(1, 11) <= 4)
                            AddUpdate(PosToInt((ushort) i, (ushort) j, (ushort) k), 184);
                        else if (random.Next(1, 11) <= 8)
                            AddUpdate(PosToInt((ushort) i, (ushort) j, (ushort) k), 0);
                        else
                            AddCheck(PosToInt((ushort) i, (ushort) j, (ushort) k), "drop 50 dissipate 8");
                    }

                    if (tile == 46)
                        AddUpdate(PosToInt((ushort) i, (ushort) j, (ushort) k), 182);
                    else if (tile == 182 || tile == 183) AddCheck(PosToInt((ushort) i, (ushort) j, (ushort) k));
                }
            }
        }

        // Token: 0x060017AC RID: 6060 RVA: 0x00096424 File Offset: 0x00094624
        public void DrawBall(int x, int y, int z, byte block, int radius)
        {
            var num = radius * radius;
            for (var i = x - radius; i <= x + radius; i++)
            for (var j = y - radius; j <= y + radius; j++)
            for (var k = z - radius; k <= z + radius; k++)
            {
                var tile = GetTile(i, j, k);
                if ((tile == 0 || tile == 96) &&
                    Math.Pow(i - x, 2.0) + Math.Pow(j - y, 2.0) + Math.Pow(k - z, 2.0) <= num)
                    AddUpdate(PosToInt(i, j, k), block);
            }
        }

        // Token: 0x060017AD RID: 6061 RVA: 0x000964E0 File Offset: 0x000946E0
        public void DrawExplosion(int x, int y, int z, byte block, int radius)
        {
            var num = radius * radius;
            var random = new Random();
            for (var i = x - radius; i <= x + radius; i++)
            for (var j = y - radius; j <= y + radius; j++)
            for (var k = z - radius; k <= z + radius; k++)
                if (Math.Pow(i - x, 2.0) + Math.Pow(j - y, 2.0) + Math.Pow(k - z, 2.0) <= num &&
                    random.NextDouble() <= 0.3)
                    AddUpdate(PosToInt(i, j, k), 184);
        }

        // Token: 0x060017AE RID: 6062 RVA: 0x000965AC File Offset: 0x000947AC
        public void DrawSmogBomb(int x, int y, int z, int radius)
        {
            var num = radius * radius;
            for (var i = x - radius; i <= x + radius; i++)
            for (var j = y - radius; j <= y + radius; j++)
            for (var k = z - radius; k <= z + radius; k++)
            {
                var tile = GetTile(i, j, k);
                if ((tile == 0 || tile == 77) &&
                    Math.Pow(i - x, 2.0) + Math.Pow(j - y, 2.0) + Math.Pow(k - z, 2.0) <= num)
                    AddUpdate(PosToInt(i, j, k), 76);
            }
        }

        // Token: 0x060017AF RID: 6063 RVA: 0x00096668 File Offset: 0x00094868
        public void Firework(ushort x, ushort y, ushort z, int size)
        {
            var random = new Random();
            if (physics < 1) return;
            var val = random.Next(21, 36);
            var val2 = random.Next(21, 36);
            AddUpdate(PosToInt(x, y, z), 0, true);
            for (var num = (ushort) (x - (size + 1)); num <= (ushort) ((int) x + size + 1); num += 1)
            for (var num2 = (ushort) (y - (size + 1)); num2 <= (ushort) ((int) y + size + 1); num2 += 1)
            for (var num3 = (ushort) (z - (size + 1)); num3 <= (ushort) ((int) z + size + 1); num3 += 1)
                if (GetTile(num, num2, num3) == 0 && random.Next(1, 40) < 2)
                    AddUpdate(PosToInt(num, num2, num3), (byte) random.Next(Math.Min(val, val2), Math.Max(val, val2)),
                        false, "drop 100 dissipate 25");
        }

        // Token: 0x060017B0 RID: 6064 RVA: 0x00096748 File Offset: 0x00094948
        public void finiteMovement(Check C, ushort x, ushort y, ushort z)
        {
            var random = new Random();
            var list = new List<int>();
            var list2 = new List<Pos>();
            if (GetTile(x, (ushort) (y - 1), z) == 0)
            {
                AddUpdate(PosToInt(x, (ushort) (y - 1), z), blocks[C.b], false, C.extraInfo);
                AddUpdate(C.b, 0);
                C.extraInfo = "";
                return;
            }

            if (GetTile(x, (ushort) (y - 1), z) == 9 || GetTile(x, (ushort) (y - 1), z) == 11)
            {
                AddUpdate(C.b, 0);
                C.extraInfo = "";
                return;
            }

            for (var i = 0; i < 25; i++) list.Add(i);
            for (var num = list.Count - 1; num > 1; num--)
            {
                var index = random.Next(num);
                var value = list[num];
                list[num] = list[index];
                list[index] = value;
            }

            var item = default(Pos);
            for (var num2 = (ushort) (x - 2); num2 <= x + 2; num2 = (ushort) (num2 + 1))
            for (var num3 = (ushort) (z - 2); num3 <= z + 2; num3 = (ushort) (num3 + 1))
            {
                item.x = num2;
                item.z = num3;
                list2.Add(item);
            }

            foreach (var item2 in list)
            {
                item = list2[item2];
                if (GetTile(item.x, (ushort) (y - 1), item.z) == 0 && GetTile(item.x, y, item.z) == 0)
                {
                    if (item.x < x)
                        item.x = (ushort) Math.Floor((item.x + x) / 2.0);
                    else
                        item.x = (ushort) Math.Ceiling((item.x + x) / 2.0);
                    if (item.z < z)
                        item.z = (ushort) Math.Floor((item.z + z) / 2.0);
                    else
                        item.z = (ushort) Math.Ceiling((item.z + z) / 2.0);
                    if (GetTile(item.x, y, item.z) == 0 &&
                        AddUpdate(PosToInt(item.x, y, item.z), blocks[C.b], false, C.extraInfo))
                    {
                        AddUpdate(C.b, 0);
                        C.extraInfo = "";
                        break;
                    }
                }
            }
        }

        // Token: 0x060017B1 RID: 6065 RVA: 0x00096A38 File Offset: 0x00094C38
        public static LevelPermission PermissionFromName(string name)
        {
            var group = Group.Find(name);
            if (group != null) return group.Permission;
            return LevelPermission.Null;
        }

        // Token: 0x060017B2 RID: 6066 RVA: 0x00096A5C File Offset: 0x00094C5C
        public static string PermissionToName(LevelPermission perm)
        {
            var group = Group.findPerm(perm);
            if (group != null) return group.name;
            var num = (int) perm;
            return num.ToString();
        }

        // Token: 0x060017B3 RID: 6067 RVA: 0x00096A84 File Offset: 0x00094C84
        public List<Player> getPlayers()
        {
            var foundPlayers = new List<Player>();
            Player.players.ForEach(delegate(Player p)
            {
                if (p.level == this) foundPlayers.Add(p);
            });
            return foundPlayers;
        }

        // Token: 0x060017B4 RID: 6068 RVA: 0x00096AC8 File Offset: 0x00094CC8
        public void SetLavaSpeed(int lavaSpeed)
        {
            this.lavaSpeed = lavaSpeed;
        }

        // Token: 0x060017B5 RID: 6069 RVA: 0x00096AD4 File Offset: 0x00094CD4
        public bool IsLit(int x, int y, int z)
        {
            if (x < 0 || y < 0 || z < 0 || x >= width || y >= height || z >= depth) return true;
            for (int i = height; i >= y; i--)
                if (!Block.LightPass(GetTile(x, i, z)))
                    return false;
            return true;
        }

        // Token: 0x060017B6 RID: 6070 RVA: 0x00096B30 File Offset: 0x00094D30
        internal void OnPlayerJoined(object sender, PlayerJoinedEventArgs e)
        {
            var playerJoined = PlayerJoined;
            if (playerJoined != null) playerJoined(sender, e);
        }

        // Token: 0x14000026 RID: 38
        // (add) Token: 0x060017B7 RID: 6071 RVA: 0x00096B50 File Offset: 0x00094D50
        // (remove) Token: 0x060017B8 RID: 6072 RVA: 0x00096B88 File Offset: 0x00094D88
        public event EventHandler<PlayerJoinedEventArgs> PlayerJoined;

        // Token: 0x0200031C RID: 796
        public struct UndoPos
        {
            // Token: 0x04000BCA RID: 3018
            public int location;

            // Token: 0x04000BCB RID: 3019
            public byte oldType;

            // Token: 0x04000BCC RID: 3020
            public byte newType;

            // Token: 0x04000BCD RID: 3021
            public DateTime timePerformed;
        }

        // Token: 0x0200031D RID: 797
        public struct BlockPos
        {
            // Token: 0x04000BCE RID: 3022
            public ushort x;

            // Token: 0x04000BCF RID: 3023
            public ushort y;

            // Token: 0x04000BD0 RID: 3024
            public ushort z;

            // Token: 0x04000BD1 RID: 3025
            public byte type;

            // Token: 0x04000BD2 RID: 3026
            public DateTime TimePerformed;

            // Token: 0x04000BD3 RID: 3027
            public bool deleted;

            // Token: 0x04000BD4 RID: 3028
            public string name;
        }

        // Token: 0x0200031E RID: 798
        public struct Zone
        {
            // Token: 0x04000BD5 RID: 3029
            public ushort smallX;

            // Token: 0x04000BD6 RID: 3030
            public ushort smallY;

            // Token: 0x04000BD7 RID: 3031
            public ushort smallZ;

            // Token: 0x04000BD8 RID: 3032
            public ushort bigX;

            // Token: 0x04000BD9 RID: 3033
            public ushort bigY;

            // Token: 0x04000BDA RID: 3034
            public ushort bigZ;

            // Token: 0x04000BDB RID: 3035
            public string Owner;
        }

        // Token: 0x0200031F RID: 799
        public class LevelOptions
        {
            // Token: 0x170008E7 RID: 2279
            // (get) Token: 0x060017C3 RID: 6083 RVA: 0x00096C88 File Offset: 0x00094E88
            // (set) Token: 0x060017C4 RID: 6084 RVA: 0x00096C90 File Offset: 0x00094E90
            public string PublicName { get; set; }

            // Token: 0x170008E8 RID: 2280
            // (get) Token: 0x060017C5 RID: 6085 RVA: 0x00096C9C File Offset: 0x00094E9C
            // (set) Token: 0x060017C6 RID: 6086 RVA: 0x00096CA4 File Offset: 0x00094EA4
            public int Physics { get; set; }
        }

        // Token: 0x02000320 RID: 800
        public struct Pos
        {
            // Token: 0x04000BDE RID: 3038
            public ushort x;

            // Token: 0x04000BDF RID: 3039
            public ushort z;
        }
    }
}