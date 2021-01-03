using System.ComponentModel;
using MCDzienny.SettingsFrame;

namespace MCDzienny.Settings
{
    // Token: 0x0200022F RID: 559
    [SettingsPath("properties/infection.properties")]
    internal sealed class InfectionSettings : SettingsFrame.SettingsFrame
    {
        // Token: 0x0400088E RID: 2190
        private static readonly InfectionSettings defaultInstance = new InfectionSettings();

        // Token: 0x170005C5 RID: 1477
        // (get) Token: 0x0600101F RID: 4127 RVA: 0x00055704 File Offset: 0x00053904
        public static InfectionSettings All
        {
            get { return defaultInstance; }
        }

        // Token: 0x170005C6 RID: 1478
        // (get) Token: 0x06001020 RID: 4128 RVA: 0x0005570C File Offset: 0x0005390C
        // (set) Token: 0x06001021 RID: 4129 RVA: 0x00055720 File Offset: 0x00053920
        [Description("Defines the zombie name.")]
        [Category("Basic")]
        [Setting]
        [SettingsFrame.DefaultValue("__Z0MBI3__")]
        public string ZombieAlias
        {
            get { return (string) this["ZombieAlias"]; }
            set { this["ZombieAlias"] = value; }
        }

        // Token: 0x170005C7 RID: 1479
        // (get) Token: 0x06001022 RID: 4130 RVA: 0x00055730 File Offset: 0x00053930
        // (set) Token: 0x06001023 RID: 4131 RVA: 0x00055744 File Offset: 0x00053944
        [SettingsFrame.DefaultValue("6")]
        [Description("The infection round time in minutes.")]
        [Category("Basic")]
        [Setting]
        public int RoundTime
        {
            get { return (int) this["RoundTime"]; }
            set { this["RoundTime"] = value; }
        }

        // Token: 0x170005C8 RID: 1480
        // (get) Token: 0x06001024 RID: 4132 RVA: 0x00055758 File Offset: 0x00053958
        // (set) Token: 0x06001025 RID: 4133 RVA: 0x0005576C File Offset: 0x0005396C
        [Description("Indicates whether zomies are headless.")]
        [SettingsFrame.DefaultValue("True")]
        [Category("Basic")]
        [Setting]
        public bool BrokenNeckZombies
        {
            get { return (bool) this["BrokenNeckZombies"]; }
            set { this["BrokenNeckZombies"] = value; }
        }

        // Token: 0x170005C9 RID: 1481
        // (get) Token: 0x06001026 RID: 4134 RVA: 0x00055780 File Offset: 0x00053980
        // (set) Token: 0x06001027 RID: 4135 RVA: 0x00055794 File Offset: 0x00053994
        [SettingsFrame.DefaultValue("30")]
        [Category("Extended")]
        [Setting]
        [Browsable(false)]
        [Description("Defines the fixed amount of money that is given to humans when they win.")]
        public int RewardForHumansFixed
        {
            get { return (int) this["RewardForHumansFixed"]; }
            set { this["RewardForHumansFixed"] = value; }
        }

        // Token: 0x170005CA RID: 1482
        // (get) Token: 0x06001028 RID: 4136 RVA: 0x000557A8 File Offset: 0x000539A8
        // (set) Token: 0x06001029 RID: 4137 RVA: 0x000557BC File Offset: 0x000539BC
        [Setting]
        [Category("Extended")]
        [Description("Not available.")]
        [SettingsFrame.DefaultValue("2")]
        [Browsable(false)]
        public int RewardForHumansMultipiler
        {
            get { return (int) this["RewardForHumansMultipiler"]; }
            set { this["RewardForHumansMultipiler"] = value; }
        }

        // Token: 0x170005CB RID: 1483
        // (get) Token: 0x0600102A RID: 4138 RVA: 0x000557D0 File Offset: 0x000539D0
        // (set) Token: 0x0600102B RID: 4139 RVA: 0x000557E4 File Offset: 0x000539E4
        [Setting]
        [SettingsFrame.DefaultValue("4")]
        [Category("Extended")]
        [Description(
            "Defines the value that multiplies the kill count of each zombie that won. The final result represents the amount of money that is given to the zombie.")]
        [Browsable(false)]
        public int RewardForZombiesMultipiler
        {
            get { return (int) this["RewardForZombiesMultipiler"]; }
            set { this["RewardForZombiesMultipiler"] = value; }
        }

        // Token: 0x170005CC RID: 1484
        // (get) Token: 0x0600102C RID: 4140 RVA: 0x000557F8 File Offset: 0x000539F8
        // (set) Token: 0x0600102D RID: 4141 RVA: 0x0005580C File Offset: 0x00053A0C
        [Category("Extended")]
        [Setting]
        [Description("Defines the fixed amount of money that is given to zombies when they win.")]
        [SettingsFrame.DefaultValue("5")]
        [Browsable(false)]
        public int RewardForZombiesFixed
        {
            get { return (int) this["RewardForZombiesFixed"]; }
            set { this["RewardForZombiesFixed"] = value; }
        }

        // Token: 0x170005CD RID: 1485
        // (get) Token: 0x0600102E RID: 4142 RVA: 0x00055820 File Offset: 0x00053A20
        // (set) Token: 0x0600102F RID: 4143 RVA: 0x00055834 File Offset: 0x00053A34
        [SettingsFrame.DefaultValue("True")]
        [Description("Indicates that the map rating should be displayed when the next map is loaded.")]
        [Category("Basic")]
        [Setting]
        public bool ShowMapRating
        {
            get { return (bool) this["ShowMapRating"]; }
            set { this["ShowMapRating"] = value; }
        }

        // Token: 0x170005CE RID: 1486
        // (get) Token: 0x06001030 RID: 4144 RVA: 0x00055848 File Offset: 0x00053A48
        // (set) Token: 0x06001031 RID: 4145 RVA: 0x0005585C File Offset: 0x00053A5C
        [Setting]
        [Category("Extended")]
        [Description("Indicates if the map author name is displayed on the beginning of the zombie survival round.")]
        [SettingsFrame.DefaultValue("True")]
        public bool ShowMapAuthor
        {
            get { return (bool) this["ShowMapAuthor"]; }
            set { this["ShowMapAuthor"] = value; }
        }

        // Token: 0x170005CF RID: 1487
        // (get) Token: 0x06001032 RID: 4146 RVA: 0x00055870 File Offset: 0x00053A70
        // (set) Token: 0x06001033 RID: 4147 RVA: 0x00055884 File Offset: 0x00053A84
        [Description("Indicates whether the map voting system is active.")]
        [SettingsFrame.DefaultValue("True")]
        [Category("Basic")]
        [Setting]
        public bool VotingSystem
        {
            get { return (bool) this["VotingSystem"]; }
            set { this["VotingSystem"] = value; }
        }

        // Token: 0x170005D0 RID: 1488
        // (get) Token: 0x06001034 RID: 4148 RVA: 0x00055898 File Offset: 0x00053A98
        // (set) Token: 0x06001035 RID: 4149 RVA: 0x000558AC File Offset: 0x00053AAC
        [SettingsFrame.DefaultValue("True")]
        [Description("In case of hack detection system warns player. If player keeps using hacks he gets kicked.")]
        [Setting]
        [Category("Hacks")]
        public bool DisallowHacksUseOnInfectionMap
        {
            get { return (bool) this["DisallowHacksUseOnInfectionMap"]; }
            set { this["DisallowHacksUseOnInfectionMap"] = value; }
        }

        // Token: 0x170005D1 RID: 1489
        // (get) Token: 0x06001036 RID: 4150 RVA: 0x000558C0 File Offset: 0x00053AC0
        // (set) Token: 0x06001037 RID: 4151 RVA: 0x000558D4 File Offset: 0x00053AD4
        [SettingsFrame.DefaultValue("150")]
        [Description(
            "Sets minimum permission level for using hacks on infection map. For example: Builder = 30, Operator = 80, Admin = 100.")]
        [Category("Hacks")]
        [Setting]
        public int HacksOnInfectionMapPermission
        {
            get { return (int) this["HacksOnInfectionMapPermission"]; }
            set { this["HacksOnInfectionMapPermission"] = value; }
        }

        // Token: 0x170005D2 RID: 1490
        // (get) Token: 0x06001038 RID: 4152 RVA: 0x000558E8 File Offset: 0x00053AE8
        // (set) Token: 0x06001039 RID: 4153 RVA: 0x000558FC File Offset: 0x00053AFC
        [SettingsFrame.DefaultValue("2")]
        [Category("Basic")]
        [Description("Sets a minimum amount of players required for a round to start.")]
        [Setting]
        public int MinimumPlayers
        {
            get { return (int) this["MinimumPlayers"]; }
            set { this["MinimumPlayers"] = value; }
        }

        // Token: 0x170005D3 RID: 1491
        // (get) Token: 0x0600103A RID: 4154 RVA: 0x00055910 File Offset: 0x00053B10
        // (set) Token: 0x0600103B RID: 4155 RVA: 0x00055924 File Offset: 0x00053B24
        [Description("Defines the human tag.")]
        [SettingsFrame.DefaultValue("%e[human] ")]
        [Category("Basic")]
        [Setting]
        public string HumanTag
        {
            get { return (string) this["HumanTag"]; }
            set { this["HumanTag"] = value; }
        }

        // Token: 0x170005D4 RID: 1492
        // (get) Token: 0x0600103C RID: 4156 RVA: 0x00055934 File Offset: 0x00053B34
        // (set) Token: 0x0600103D RID: 4157 RVA: 0x00055948 File Offset: 0x00053B48
        [Setting]
        [Description("Defines the zombie tag.")]
        [Category("Basic")]
        [SettingsFrame.DefaultValue("&c[zombie] ")]
        public string ZombieTag
        {
            get { return (string) this["ZombieTag"]; }
            set { this["ZombieTag"] = value; }
        }

        // Token: 0x170005D5 RID: 1493
        // (get) Token: 0x0600103E RID: 4158 RVA: 0x00055958 File Offset: 0x00053B58
        // (set) Token: 0x0600103F RID: 4159 RVA: 0x0005596C File Offset: 0x00053B6C
        [SettingsFrame.DefaultValue("&f[referee] ")]
        [Setting]
        [Description("Defines the refree tag.")]
        [Category("Basic")]
        public string RefreeTag
        {
            get { return (string) this["RefreeTag"]; }
            set { this["RefreeTag"] = value; }
        }

        // Token: 0x170005D6 RID: 1494
        // (get) Token: 0x06001040 RID: 4160 RVA: 0x0005597C File Offset: 0x00053B7C
        // (set) Token: 0x06001041 RID: 4161 RVA: 0x00055990 File Offset: 0x00053B90
        [SettingsFrame.DefaultValue("35")]
        [Setting]
        [Category("Extended")]
        [Description(
            "Defines the threshold for speed hack detection. The higher the value the less sensitive is the detector, but it may be wise to set is higher for the servers with a significant lag. Default value = 35.")]
        public int SpeedHackDetectionThreshold
        {
            get { return (int) this["SpeedHackDetectionThreshold"]; }
            set { this["SpeedHackDetectionThreshold"] = value; }
        }

        // Token: 0x170005D7 RID: 1495
        // (get) Token: 0x06001042 RID: 4162 RVA: 0x000559A4 File Offset: 0x00053BA4
        // (set) Token: 0x06001043 RID: 4163 RVA: 0x000559B8 File Offset: 0x00053BB8
        [Setting]
        [Category("Extended")]
        [SettingsFrame.DefaultValue("True")]
        [Description("Defines whether a block glitch prevention is on. It only works with no-build zombie maps.")]
        public bool BlockGlitchPrevention
        {
            get { return (bool) this["BlockGlitchPrevention"]; }
            set { this["BlockGlitchPrevention"] = value; }
        }

        // Token: 0x170005D8 RID: 1496
        // (get) Token: 0x06001044 RID: 4164 RVA: 0x000559CC File Offset: 0x00053BCC
        // (set) Token: 0x06001045 RID: 4165 RVA: 0x000559E0 File Offset: 0x00053BE0
        [SettingsFrame.DefaultValue("True")]
        [Description("Indicates whether a player level system is used in a zombie mode.")]
        [Category("Extended")]
        [Setting]
        public bool UsePlayerLevels
        {
            get { return (bool) this["UsePlayerLevels"]; }
            set { this["UsePlayerLevels"] = value; }
        }

        // Token: 0x170005D9 RID: 1497
        // (get) Token: 0x06001046 RID: 4166 RVA: 0x000559F4 File Offset: 0x00053BF4
        // (set) Token: 0x06001047 RID: 4167 RVA: 0x00055A08 File Offset: 0x00053C08
        [Setting]
        [SettingsFrame.DefaultValue("True")]
        public bool DisallowSpleefing
        {
            get { return (bool) this["DisallowSpleefing"]; }
            set { this["DisallowSpleefing"] = value; }
        }

        // Token: 0x170005DA RID: 1498
        // (get) Token: 0x06001048 RID: 4168 RVA: 0x00055A1C File Offset: 0x00053C1C
        // (set) Token: 0x06001049 RID: 4169 RVA: 0x00055A30 File Offset: 0x00053C30
        [Setting]
        [SettingsFrame.DefaultValue("False")]
        public bool OpsBypassSpleefPrevention
        {
            get { return (bool) this["OpsBypassSpleefPrevention"]; }
            set { this["OpsBypassSpleefPrevention"] = value; }
        }

        // Token: 0x170005DB RID: 1499
        // (get) Token: 0x0600104A RID: 4170 RVA: 0x00055A44 File Offset: 0x00053C44
        // (set) Token: 0x0600104B RID: 4171 RVA: 0x00055A58 File Offset: 0x00053C58
        [Category("Extended")]
        [SettingsFrame.DefaultValue("True")]
        [Description("Indicates whether a player can use /afk command during a map vote.")]
        [Setting]
        public bool IsAfkDuringVoteAllowed
        {
            get { return (bool) this["IsAfkDuringVoteAllowed"]; }
            set { this["IsAfkDuringVoteAllowed"] = value; }
        }

        // Token: 0x170005DC RID: 1500
        // (get) Token: 0x0600104C RID: 4172 RVA: 0x00055A6C File Offset: 0x00053C6C
        // (set) Token: 0x0600104D RID: 4173 RVA: 0x00055A80 File Offset: 0x00053C80
        [Setting]
        [SettingsFrame.DefaultValue("20")]
        [Category("Extended")]
        [Description("Defines how many seconds the next map vote lasts.")]
        public int MapVoteDurationSeconds
        {
            get { return (int) this["MapVoteDurationSeconds"]; }
            set { this["MapVoteDurationSeconds"] = value; }
        }
    }
}