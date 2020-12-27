using System.ComponentModel;
using MCDzienny.SettingsFrame;

namespace MCDzienny.Settings
{
    // Token: 0x0200022C RID: 556
    [SettingsPath("properties/lava.properties")]
    internal sealed class LavaSettings : SettingsFrame.SettingsFrame
    {
        // Token: 0x04000887 RID: 2183
        private static readonly LavaSettings defaultInstance = new LavaSettings();

        // Token: 0x1700059A RID: 1434
        // (get) Token: 0x06000FB8 RID: 4024 RVA: 0x000548A8 File Offset: 0x00052AA8
        public static LavaSettings All
        {
            get { return defaultInstance; }
        }

        // Token: 0x1700059B RID: 1435
        // (get) Token: 0x06000FB9 RID: 4025 RVA: 0x000548B0 File Offset: 0x00052AB0
        // (set) Token: 0x06000FBA RID: 4026 RVA: 0x000548C4 File Offset: 0x00052AC4
        [Setting]
        [Category("Basic")]
        [SettingsFrame.DefaultValue("True")]
        [Description("Indicates whether players are moved to the spawn point on death.")]
        public bool SpawnOnDeath
        {
            get { return (bool) this["SpawnOnDeath"]; }
            set { this["SpawnOnDeath"] = value; }
        }

        // Token: 0x1700059C RID: 1436
        // (get) Token: 0x06000FBB RID: 4027 RVA: 0x000548D8 File Offset: 0x00052AD8
        // (set) Token: 0x06000FBC RID: 4028 RVA: 0x000548EC File Offset: 0x00052AEC
        [Category("Basic")]
        [Description("Indicates whether dead players are headless.")]
        [Setting]
        [SettingsFrame.DefaultValue("True")]
        public bool HeadlessGhosts
        {
            get { return (bool) this["HeadlessGhosts"]; }
            set { this["HeadlessGhosts"] = value; }
        }

        // Token: 0x1700059D RID: 1437
        // (get) Token: 0x06000FBD RID: 4029 RVA: 0x00054900 File Offset: 0x00052B00
        // (set) Token: 0x06000FBE RID: 4030 RVA: 0x00054914 File Offset: 0x00052B14
        [Setting]
        [SettingsFrame.DefaultValue("True")]
        [Description("Indicates whether the map voting system is active.")]
        [Category("Basic")]
        public bool VotingSystem
        {
            get { return (bool) this["VotingSystem"]; }
            set { this["VotingSystem"] = value; }
        }

        // Token: 0x1700059E RID: 1438
        // (get) Token: 0x06000FBF RID: 4031 RVA: 0x00054928 File Offset: 0x00052B28
        // (set) Token: 0x06000FC0 RID: 4032 RVA: 0x0005493C File Offset: 0x00052B3C
        [SettingsFrame.DefaultValue("True")]
        [Description("Indicates if name color changes according to player level.")]
        [Category("Basic")]
        [Setting]
        public bool AutoNameColoring
        {
            get { return (bool) this["AutoNameColoring"]; }
            set { this["AutoNameColoring"] = value; }
        }

        // Token: 0x1700059F RID: 1439
        // (get) Token: 0x06000FC1 RID: 4033 RVA: 0x00054950 File Offset: 0x00052B50
        // (set) Token: 0x06000FC2 RID: 4034 RVA: 0x00054964 File Offset: 0x00052B64
        [Category("Basic")]
        [Setting]
        [SettingsFrame.DefaultValue("True")]
        [Description(
            "Indicates if players are given stars for winning streak. One star gives 10% higher score, two starts 20%, and three 30%.")]
        public bool StarSystem
        {
            get { return (bool) this["StarSystem"]; }
            set { this["StarSystem"] = value; }
        }

        // Token: 0x170005A0 RID: 1440
        // (get) Token: 0x06000FC3 RID: 4035 RVA: 0x00054978 File Offset: 0x00052B78
        // (set) Token: 0x06000FC4 RID: 4036 RVA: 0x0005498C File Offset: 0x00052B8C
        [Category("Basic")]
        [SettingsFrame.DefaultValue("True")]
        [Description("Indicates that the map rating should be displayed when the next map is loaded.")]
        [Setting]
        public bool ShowMapRating
        {
            get { return (bool) this["ShowMapRating"]; }
            set { this["ShowMapRating"] = value; }
        }

        // Token: 0x170005A1 RID: 1441
        // (get) Token: 0x06000FC5 RID: 4037 RVA: 0x000549A0 File Offset: 0x00052BA0
        // (set) Token: 0x06000FC6 RID: 4038 RVA: 0x000549B4 File Offset: 0x00052BB4
        [Category("Basic")]
        [SettingsFrame.DefaultValue("True")]
        [Description("Indicates whether lava mood changes according to random pattern.")]
        [Setting]
        public bool RandomLavaState
        {
            get { return (bool) this["RandomLavaState"]; }
            set { this["RandomLavaState"] = value; }
        }

        // Token: 0x170005A2 RID: 1442
        // (get) Token: 0x06000FC7 RID: 4039 RVA: 0x000549C8 File Offset: 0x00052BC8
        // (set) Token: 0x06000FC8 RID: 4040 RVA: 0x000549DC File Offset: 0x00052BDC
        [Category("Basic")]
        [SettingsFrame.DefaultValue("Disturbed")]
        [Setting]
        [Description("Determines the lava state.")]
        public LavaState LavaState
        {
            get { return (LavaState) this["LavaState"]; }
            set { this["LavaState"] = value; }
        }

        // Token: 0x170005A3 RID: 1443
        // (get) Token: 0x06000FC9 RID: 4041 RVA: 0x000549F0 File Offset: 0x00052BF0
        // (set) Token: 0x06000FCA RID: 4042 RVA: 0x00054A04 File Offset: 0x00052C04
        [Setting]
        [SettingsFrame.DefaultValue("True")]
        [Description("Indicates whether the anti-score-abuse system is active.")]
        [Category("Extended")]
        public bool PreventScoreAbuse
        {
            get { return (bool) this["PreventScoreAbuse"]; }
            set { this["PreventScoreAbuse"] = value; }
        }

        // Token: 0x170005A4 RID: 1444
        // (get) Token: 0x06000FCB RID: 4043 RVA: 0x00054A18 File Offset: 0x00052C18
        // (set) Token: 0x06000FCC RID: 4044 RVA: 0x00054A2C File Offset: 0x00052C2C
        [SettingsFrame.DefaultValue("False")]
        [Description(
            "Indicates if lowlag mode is turned on when the lava flood starts. It turns off automatically after a certain amount of time.")]
        [Setting]
        [Category("Extended")]
        public bool OverloadProtection
        {
            get { return (bool) this["OverloadProtection"]; }
            set { this["OverloadProtection"] = value; }
        }

        // Token: 0x170005A5 RID: 1445
        // (get) Token: 0x06000FCD RID: 4045 RVA: 0x00054A40 File Offset: 0x00052C40
        // (set) Token: 0x06000FCE RID: 4046 RVA: 0x00054A54 File Offset: 0x00052C54
        [Description(
            "Indicates whether the connection speed test for each player on log-in is performed. If a result indicates high latency, the player is kicked.")]
        [Category("Extended")]
        [Setting]
        [SettingsFrame.DefaultValue("False")]
        public bool ConnectionSpeedTest
        {
            get { return (bool) this["ConnectionSpeedTest"]; }
            set { this["ConnectionSpeedTest"] = value; }
        }

        // Token: 0x170005A6 RID: 1446
        // (get) Token: 0x06000FCF RID: 4047 RVA: 0x00054A68 File Offset: 0x00052C68
        // (set) Token: 0x06000FD0 RID: 4048 RVA: 0x00054A7C File Offset: 0x00052C7C
        [Setting]
        [Category("Extended")]
        [SettingsFrame.DefaultValue("False")]
        [Description(
            "Indicates if server is locked for 45 seconds when Buffer Overload Error happens. When the lock is active no one can enter the server. It may help in stabilizing the internet connection and system resources use, so that overall less players get kicked.")]
        public bool AutoServerLock
        {
            get { return (bool) this["AutoServerLock"]; }
            set { this["AutoServerLock"] = value; }
        }

        // Token: 0x170005A7 RID: 1447
        // (get) Token: 0x06000FD1 RID: 4049 RVA: 0x00054A90 File Offset: 0x00052C90
        // (set) Token: 0x06000FD2 RID: 4050 RVA: 0x00054AA4 File Offset: 0x00052CA4
        [Description("Determines the delay between each lava move. The default value is 4.")]
        [SettingsFrame.DefaultValue("4")]
        [Category("Extended")]
        [Setting]
        public int LavaMovementDelay
        {
            get { return (int) this["LavaMovementDelay"]; }
            set { this["LavaMovementDelay"] = value; }
        }

        // Token: 0x170005A8 RID: 1448
        // (get) Token: 0x06000FD3 RID: 4051 RVA: 0x00054AB8 File Offset: 0x00052CB8
        // (set) Token: 0x06000FD4 RID: 4052 RVA: 0x00054ACC File Offset: 0x00052CCC
        [Setting]
        [SettingsFrame.DefaultValue("True")]
        [Category("Extended")]
        [Description("Indicates if Distance Offset Message is displayed.")]
        public bool ShowDistanceOffsetMessage
        {
            get { return (bool) this["ShowDistanceOffsetMessage"]; }
            set { this["ShowDistanceOffsetMessage"] = value; }
        }

        // Token: 0x170005A9 RID: 1449
        // (get) Token: 0x06000FD5 RID: 4053 RVA: 0x00054AE0 File Offset: 0x00052CE0
        // (set) Token: 0x06000FD6 RID: 4054 RVA: 0x00054AF4 File Offset: 0x00052CF4
        [Category("Extended")]
        [Description("Defines the reward that is given to winners when they are below the sea level.")]
        [SettingsFrame.DefaultValue("25")]
        [Setting]
        public int RewardBelowSeaLevel
        {
            get { return (int) this["RewardBelowSeaLevel"]; }
            set { this["RewardBelowSeaLevel"] = value; }
        }

        // Token: 0x170005AA RID: 1450
        // (get) Token: 0x06000FD7 RID: 4055 RVA: 0x00054B08 File Offset: 0x00052D08
        // (set) Token: 0x06000FD8 RID: 4056 RVA: 0x00054B1C File Offset: 0x00052D1C
        [SettingsFrame.DefaultValue("30")]
        [Setting]
        [Description("Defines the reward that is given to winners when they are above the sea level.")]
        [Category("Extended")]
        public int RewardAboveSeaLevel
        {
            get { return (int) this["RewardAboveSeaLevel"]; }
            set { this["RewardAboveSeaLevel"] = value; }
        }

        // Token: 0x170005AB RID: 1451
        // (get) Token: 0x06000FD9 RID: 4057 RVA: 0x00054B30 File Offset: 0x00052D30
        // (set) Token: 0x06000FDA RID: 4058 RVA: 0x00054B44 File Offset: 0x00052D44
        [Category("Extended")]
        [Description("Indicates whether variables such as: $name, $money, etc. can be used in game.")]
        [Setting]
        [SettingsFrame.DefaultValue("True")]
        public bool AllowInGameVariables
        {
            get { return (bool) this["AllowInGameVariables"]; }
            set { this["AllowInGameVariables"] = value; }
        }

        // Token: 0x170005AC RID: 1452
        // (get) Token: 0x06000FDB RID: 4059 RVA: 0x00054B58 File Offset: 0x00052D58
        // (set) Token: 0x06000FDC RID: 4060 RVA: 0x00054B6C File Offset: 0x00052D6C
        [SettingsFrame.DefaultValue("False")]
        [Setting]
        [Category("Extended")]
        [Description("Indicates if the map author name is displayed on the beginning of the lava survival round.")]
        public bool ShowMapAuthor
        {
            get { return (bool) this["ShowMapAuthor"]; }
            set { this["ShowMapAuthor"] = value; }
        }

        // Token: 0x170005AD RID: 1453
        // (get) Token: 0x06000FDD RID: 4061 RVA: 0x00054B80 File Offset: 0x00052D80
        // (set) Token: 0x06000FDE RID: 4062 RVA: 0x00054B94 File Offset: 0x00052D94
        [SettingsFrame.DefaultValue("BasedOnAir")]
        [Description(
            "Score mode: BasedOnAir - standard mode, players get score based on the amount of air around them. Fixed - winners always get the same amount of points.")]
        [Setting]
        [Category("Extended")]
        public ScoreSystem ScoreMode
        {
            get { return (ScoreSystem) this["ScoreMode"]; }
            set { this["ScoreMode"] = value; }
        }

        // Token: 0x170005AE RID: 1454
        // (get) Token: 0x06000FDF RID: 4063 RVA: 0x00054BA8 File Offset: 0x00052DA8
        // (set) Token: 0x06000FE0 RID: 4064 RVA: 0x00054BBC File Offset: 0x00052DBC
        [Category("Extended")]
        [Setting]
        [Description("Defines the score that is given to each winner in the fixed score mode.")]
        [SettingsFrame.DefaultValue("500")]
        public int ScoreRewardFixed
        {
            get { return (int) this["ScoreRewardFixed"]; }
            set { this["ScoreRewardFixed"] = value; }
        }

        // Token: 0x170005AF RID: 1455
        // (get) Token: 0x06000FE1 RID: 4065 RVA: 0x00054BD0 File Offset: 0x00052DD0
        // (set) Token: 0x06000FE2 RID: 4066 RVA: 0x00054BE4 File Offset: 0x00052DE4
        [Category("Extended")]
        [SettingsFrame.DefaultValue("5")]
        [Description(
            "Defines the minimum distance from the spawn point measured in blocks that will allow a player to win a round.")]
        [Setting]
        public int RequiredDistanceFromSpawn
        {
            get { return (int) this["RequiredDistanceFromSpawn"]; }
            set { this["RequiredDistanceFromSpawn"] = value; }
        }

        // Token: 0x170005B0 RID: 1456
        // (get) Token: 0x06000FE3 RID: 4067 RVA: 0x00054BF8 File Offset: 0x00052DF8
        // (set) Token: 0x06000FE4 RID: 4068 RVA: 0x00054C0C File Offset: 0x00052E0C
        [SettingsFrame.DefaultValue("False")]
        [Category("Basic")]
        [Description("Indicates whether the chat on a lava map is connected to other maps.")]
        [Setting]
        public bool LavaWorldChat
        {
            get { return (bool) this["LavaWorldChat"]; }
            set { this["LavaWorldChat"] = value; }
        }

        // Token: 0x170005B1 RID: 1457
        // (get) Token: 0x06000FE5 RID: 4069 RVA: 0x00054C20 File Offset: 0x00052E20
        // (set) Token: 0x06000FE6 RID: 4070 RVA: 0x00054C34 File Offset: 0x00052E34
        [Category("Basic")]
        [Description(
            "Defines the maximum amount of players that can stay on lava map at the same time. Value '0' means that there is no limit.")]
        [Setting]
        [SettingsFrame.DefaultValue("0")]
        public int LavaMapPlayerLimit
        {
            get { return (int) this["LavaMapPlayerLimit"]; }
            set { this["LavaMapPlayerLimit"] = value; }
        }

        // Token: 0x170005B2 RID: 1458
        // (get) Token: 0x06000FE7 RID: 4071 RVA: 0x00054C48 File Offset: 0x00052E48
        // (set) Token: 0x06000FE8 RID: 4072 RVA: 0x00054C5C File Offset: 0x00052E5C
        [Category("Hacks")]
        [SettingsFrame.DefaultValue("False")]
        [Description("In case of hack detection system warns player. If player keeps using hacks he gets kicked.")]
        [Setting]
        public bool DisallowHacksUseOnLavaMap
        {
            get { return (bool) this["DisallowHacksUseOnLavaMap"]; }
            set { this["DisallowHacksUseOnLavaMap"] = value; }
        }

        // Token: 0x170005B3 RID: 1459
        // (get) Token: 0x06000FE9 RID: 4073 RVA: 0x00054C70 File Offset: 0x00052E70
        // (set) Token: 0x06000FEA RID: 4074 RVA: 0x00054C84 File Offset: 0x00052E84
        [Setting]
        [Category("Hacks")]
        [SettingsFrame.DefaultValue("80")]
        [Description(
            "Sets minimum permission level for using hacks on lava map. For example: Builder = 30, Operator = 80, Admin = 100.")]
        public int HacksUseOnLavaMapPermission
        {
            get { return (int) this["HacksUseOnLavaMapPermission"]; }
            set { this["HacksUseOnLavaMapPermission"] = value; }
        }

        // Token: 0x170005B4 RID: 1460
        // (get) Token: 0x06000FEB RID: 4075 RVA: 0x00054C98 File Offset: 0x00052E98
        // (set) Token: 0x06000FEC RID: 4076 RVA: 0x00054CAC File Offset: 0x00052EAC
        [Category("Extended")]
        [Description("Determines whether players are allowed to use cuboid on lava maps.")]
        [Setting]
        [SettingsFrame.DefaultValue("False")]
        public bool AllowCuboidOnLavaMaps
        {
            get { return (bool) this["AllowCuboidOnLavaMaps"]; }
            set { this["AllowCuboidOnLavaMaps"] = value; }
        }

        // Token: 0x170005B5 RID: 1461
        // (get) Token: 0x06000FED RID: 4077 RVA: 0x00054CC0 File Offset: 0x00052EC0
        // (set) Token: 0x06000FEE RID: 4078 RVA: 0x00054CD4 File Offset: 0x00052ED4
        [Description("Defines the reward from breaking a treasure block.")]
        [Setting]
        [SettingsFrame.DefaultValue("5")]
        [Category("Extended")]
        public int AmountOfMoneyInTreasure
        {
            get { return (int) this["AmountOfMoneyInTreasure"]; }
            set { this["AmountOfMoneyInTreasure"] = value; }
        }

        // Token: 0x170005B6 RID: 1462
        // (get) Token: 0x06000FEF RID: 4079 RVA: 0x00054CE8 File Offset: 0x00052EE8
        // (set) Token: 0x06000FF0 RID: 4080 RVA: 0x00054CFC File Offset: 0x00052EFC
        [SettingsFrame.DefaultValue("False")]
        [Setting]
        [Description("Determines whether players are allowed to place a gold rock on lava maps.")]
        [Category("Extended")]
        public bool AllowGoldRockOnLavaMaps
        {
            get { return (bool) this["AllowGoldRockOnLavaMaps"]; }
            set { this["AllowGoldRockOnLavaMaps"] = value; }
        }

        // Token: 0x170005B7 RID: 1463
        // (get) Token: 0x06000FF1 RID: 4081 RVA: 0x00054D10 File Offset: 0x00052F10
        // (set) Token: 0x06000FF2 RID: 4082 RVA: 0x00054D24 File Offset: 0x00052F24
        [Category("Extended")]
        [Description(
            "Sets the minimum amount of players that leads to hide of death messages. If set to 0 the death messages are never showed.")]
        [SettingsFrame.DefaultValue("20")]
        [Setting]
        public int HideDeathMessagesAmount
        {
            get { return (int) this["HideDeathMessagesAmount"]; }
            set { this["HideDeathMessagesAmount"] = value; }
        }

        // Token: 0x170005B8 RID: 1464
        // (get) Token: 0x06000FF3 RID: 4083 RVA: 0x00054D38 File Offset: 0x00052F38
        // (set) Token: 0x06000FF4 RID: 4084 RVA: 0x00054D4C File Offset: 0x00052F4C
        [Setting]
        [Category("Extended")]
        [Description(
            "Sets upper level of BOPL antigrief system. Beyond the level players are not monitored by Based On Player's Level antigrief protection.")]
        [SettingsFrame.DefaultValue("8")]
        public int UpperLevelOfBoplAntigrief
        {
            get { return (int) this["UpperLevelOfBoplAntigrief"]; }
            set { this["UpperLevelOfBoplAntigrief"] = value; }
        }

        // Token: 0x170005B9 RID: 1465
        // (get) Token: 0x06000FF5 RID: 4085 RVA: 0x00054D60 File Offset: 0x00052F60
        // (set) Token: 0x06000FF6 RID: 4086 RVA: 0x00054D74 File Offset: 0x00052F74
        [SettingsFrame.DefaultValue("3")]
        [Description("Defines the amount of lives that every player starts with at the beginning of each round.")]
        [Setting]
        [Category("Extended")]
        public byte LivesAtStart
        {
            get { return (byte) this["LivesAtStart"]; }
            set { this["LivesAtStart"] = value; }
        }

        // Token: 0x170005BA RID: 1466
        // (get) Token: 0x06000FF7 RID: 4087 RVA: 0x00054D88 File Offset: 0x00052F88
        // (set) Token: 0x06000FF8 RID: 4088 RVA: 0x00054D9C File Offset: 0x00052F9C
        [Setting]
        [Category("Basic")]
        [SettingsFrame.DefaultValue("BasedOnPlayersLevel")]
        [Description(
            "Describes the type of antigrief system that the server uses. Valid values are BasedOnName and BasedOnPlayersLevel.")]
        public AntigriefType Antigrief
        {
            get { return (AntigriefType) this["Antigrief"]; }
            set { this["Antigrief"] = value; }
        }

        // Token: 0x170005BB RID: 1467
        // (get) Token: 0x06000FF9 RID: 4089 RVA: 0x00054DB0 File Offset: 0x00052FB0
        // (set) Token: 0x06000FFA RID: 4090 RVA: 0x00054DC4 File Offset: 0x00052FC4
        [Setting]
        [Category("Extended")]
        [Description("Determines whether players are allowed to build near a lava spawn.")]
        [SettingsFrame.DefaultValue("True")]
        public bool DisallowBuildingNearLavaSpawn
        {
            get { return (bool) this["DisallowBuildingNearLavaSpawn"]; }
            set { this["DisallowBuildingNearLavaSpawn"] = value; }
        }

        // Token: 0x170005BC RID: 1468
        // (get) Token: 0x06000FFB RID: 4091 RVA: 0x00054DD8 File Offset: 0x00052FD8
        // (set) Token: 0x06000FFC RID: 4092 RVA: 0x00054DEC File Offset: 0x00052FEC
        [Description("Determines whether players are allowed to place sponges near a lava spawn.")]
        [SettingsFrame.DefaultValue("True")]
        [Setting]
        [Category("Extended")]
        public bool DisallowSpongesNearLavaSpawn
        {
            get { return (bool) this["DisallowSpongesNearLavaSpawn"]; }
            set { this["DisallowSpongesNearLavaSpawn"] = value; }
        }

        // Token: 0x170005BD RID: 1469
        // (get) Token: 0x06000FFD RID: 4093 RVA: 0x00054E00 File Offset: 0x00053000
        // (set) Token: 0x06000FFE RID: 4094 RVA: 0x00054E14 File Offset: 0x00053014
        [Setting]
        [Description(
            "Defines whether a player has to be registered (on the forum) before he can get a promotion. Don't turn it on if you don't know how it works!!!")]
        [SettingsFrame.DefaultValue("False")]
        [Category("Special")]
        public bool RequireRegistrationForPromotion
        {
            get { return (bool) this["RequireRegistrationForPromotion"]; }
            set { this["RequireRegistrationForPromotion"] = value; }
        }

        // Token: 0x170005BE RID: 1470
        // (get) Token: 0x06000FFF RID: 4095 RVA: 0x00054E28 File Offset: 0x00053028
        // (set) Token: 0x06001000 RID: 4096 RVA: 0x00054E3C File Offset: 0x0005303C
        [SettingsFrame.DefaultValue("False")]
        [Setting]
        public bool DisallowSpleefing
        {
            get { return (bool) this["DisallowSpleefing"]; }
            set { this["DisallowSpleefing"] = value; }
        }

        // Token: 0x170005BF RID: 1471
        // (get) Token: 0x06001001 RID: 4097 RVA: 0x00054E50 File Offset: 0x00053050
        // (set) Token: 0x06001002 RID: 4098 RVA: 0x00054E64 File Offset: 0x00053064
        [SettingsFrame.DefaultValue("False")]
        [Setting]
        public bool OpsBypassSpleefPrevention
        {
            get { return (bool) this["OpsBypassSpleefPrevention"]; }
            set { this["OpsBypassSpleefPrevention"] = value; }
        }

        // Token: 0x170005C0 RID: 1472
        // (get) Token: 0x06001003 RID: 4099 RVA: 0x00054E78 File Offset: 0x00053078
        // (set) Token: 0x06001004 RID: 4100 RVA: 0x00054E8C File Offset: 0x0005308C
        [Setting]
        [Category("Extended")]
        [SettingsFrame.DefaultValue("True")]
        [Description("Indicates whether a player can use /afk command during a map vote.")]
        public bool IsAfkDuringVoteAllowed
        {
            get { return (bool) this["IsAfkDuringVoteAllowed"]; }
            set { this["IsAfkDuringVoteAllowed"] = value; }
        }
    }
}