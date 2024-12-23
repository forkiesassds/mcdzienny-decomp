using System.ComponentModel;
using MCDzienny.SettingsFrame;

namespace MCDzienny.Settings
{
    [SettingsPath("properties/infection.properties")]
    sealed class InfectionSettings : SettingsFrame.SettingsFrame
    {
        static readonly InfectionSettings defaultInstance = new InfectionSettings();

        public static InfectionSettings All { get { return defaultInstance; } }

        [Description("Defines the zombie name.")]
        [Category("Basic")]
        [Setting]
        [SettingsFrame.DefaultValue("__Z0MBI3__")]
        public string ZombieAlias { get { return (string)this["ZombieAlias"]; } set { this["ZombieAlias"] = value; } }

        [SettingsFrame.DefaultValue("6")]
        [Description("The infection round time in minutes.")]
        [Category("Basic")]
        [Setting]
        public int RoundTime { get { return (int)this["RoundTime"]; } set { this["RoundTime"] = value; } }

        [Description("Indicates whether zomies are headless.")]
        [SettingsFrame.DefaultValue("True")]
        [Category("Basic")]
        [Setting]
        public bool BrokenNeckZombies { get { return (bool)this["BrokenNeckZombies"]; } set { this["BrokenNeckZombies"] = value; } }

        [SettingsFrame.DefaultValue("30")]
        [Category("Extended")]
        [Setting]
        [Browsable(false)]
        [Description("Defines the fixed amount of money that is given to humans when they win.")]
        public int RewardForHumansFixed { get { return (int)this["RewardForHumansFixed"]; } set { this["RewardForHumansFixed"] = value; } }

        [Setting]
        [Category("Extended")]
        [Description("Not available.")]
        [SettingsFrame.DefaultValue("2")]
        [Browsable(false)]
        public int RewardForHumansMultipiler { get { return (int)this["RewardForHumansMultipiler"]; } set { this["RewardForHumansMultipiler"] = value; } }

        [Setting]
        [SettingsFrame.DefaultValue("4")]
        [Category("Extended")]
        [Description(
            "Defines the value that multiplies the kill count of each zombie that won. The final result represents the amount of money that is given to the zombie.")]
        [Browsable(false)]
        public int RewardForZombiesMultipiler { get { return (int)this["RewardForZombiesMultipiler"]; } set { this["RewardForZombiesMultipiler"] = value; } }

        [Category("Extended")]
        [Setting]
        [Description("Defines the fixed amount of money that is given to zombies when they win.")]
        [SettingsFrame.DefaultValue("5")]
        [Browsable(false)]
        public int RewardForZombiesFixed { get { return (int)this["RewardForZombiesFixed"]; } set { this["RewardForZombiesFixed"] = value; } }

        [SettingsFrame.DefaultValue("True")]
        [Description("Indicates that the map rating should be displayed when the next map is loaded.")]
        [Category("Basic")]
        [Setting]
        public bool ShowMapRating { get { return (bool)this["ShowMapRating"]; } set { this["ShowMapRating"] = value; } }

        [Setting]
        [Category("Extended")]
        [Description("Indicates if the map author name is displayed on the beginning of the zombie survival round.")]
        [SettingsFrame.DefaultValue("True")]
        public bool ShowMapAuthor { get { return (bool)this["ShowMapAuthor"]; } set { this["ShowMapAuthor"] = value; } }

        [Description("Indicates whether the map voting system is active.")]
        [SettingsFrame.DefaultValue("True")]
        [Category("Basic")]
        [Setting]
        public bool VotingSystem { get { return (bool)this["VotingSystem"]; } set { this["VotingSystem"] = value; } }

        [SettingsFrame.DefaultValue("True")]
        [Description("In case of hack detection system warns player. If player keeps using hacks he gets kicked.")]
        [Setting]
        [Category("Hacks")]
        public bool DisallowHacksUseOnInfectionMap { get { return (bool)this["DisallowHacksUseOnInfectionMap"]; } set { this["DisallowHacksUseOnInfectionMap"] = value; } }

        [SettingsFrame.DefaultValue("150")]
        [Description("Sets minimum permission level for using hacks on infection map. For example: Builder = 30, Operator = 80, Admin = 100.")]
        [Category("Hacks")]
        [Setting]
        public int HacksOnInfectionMapPermission { get { return (int)this["HacksOnInfectionMapPermission"]; } set { this["HacksOnInfectionMapPermission"] = value; } }

        [SettingsFrame.DefaultValue("2")]
        [Category("Basic")]
        [Description("Sets a minimum amount of players required for a round to start.")]
        [Setting]
        public int MinimumPlayers { get { return (int)this["MinimumPlayers"]; } set { this["MinimumPlayers"] = value; } }

        [Description("Defines the human tag.")]
        [SettingsFrame.DefaultValue("%e[human] ")]
        [Category("Basic")]
        [Setting]
        public string HumanTag { get { return (string)this["HumanTag"]; } set { this["HumanTag"] = value; } }

        [Setting]
        [Description("Defines the zombie tag.")]
        [Category("Basic")]
        [SettingsFrame.DefaultValue("&c[zombie] ")]
        public string ZombieTag { get { return (string)this["ZombieTag"]; } set { this["ZombieTag"] = value; } }

        [SettingsFrame.DefaultValue("&f[referee] ")]
        [Setting]
        [Description("Defines the refree tag.")]
        [Category("Basic")]
        public string RefreeTag { get { return (string)this["RefreeTag"]; } set { this["RefreeTag"] = value; } }

        [SettingsFrame.DefaultValue("35")]
        [Setting]
        [Category("Extended")]
        [Description(
            "Defines the threshold for speed hack detection. The higher the value the less sensitive is the detector, but it may be wise to set is higher for the servers with a significant lag. Default value = 35.")]
        public int SpeedHackDetectionThreshold { get { return (int)this["SpeedHackDetectionThreshold"]; } set { this["SpeedHackDetectionThreshold"] = value; } }

        [Setting]
        [Category("Extended")]
        [SettingsFrame.DefaultValue("True")]
        [Description("Defines whether a block glitch prevention is on. It only works with no-build zombie maps.")]
        public bool BlockGlitchPrevention { get { return (bool)this["BlockGlitchPrevention"]; } set { this["BlockGlitchPrevention"] = value; } }

        [SettingsFrame.DefaultValue("True")]
        [Description("Indicates whether a player level system is used in a zombie mode.")]
        [Category("Extended")]
        [Setting]
        public bool UsePlayerLevels { get { return (bool)this["UsePlayerLevels"]; } set { this["UsePlayerLevels"] = value; } }

        [Setting]
        [SettingsFrame.DefaultValue("True")]
        public bool DisallowSpleefing { get { return (bool)this["DisallowSpleefing"]; } set { this["DisallowSpleefing"] = value; } }

        [Setting]
        [SettingsFrame.DefaultValue("False")]
        public bool OpsBypassSpleefPrevention { get { return (bool)this["OpsBypassSpleefPrevention"]; } set { this["OpsBypassSpleefPrevention"] = value; } }

        [Category("Extended")]
        [SettingsFrame.DefaultValue("True")]
        [Description("Indicates whether a player can use /afk command during a map vote.")]
        [Setting]
        public bool IsAfkDuringVoteAllowed { get { return (bool)this["IsAfkDuringVoteAllowed"]; } set { this["IsAfkDuringVoteAllowed"] = value; } }

        [Setting]
        [SettingsFrame.DefaultValue("20")]
        [Category("Extended")]
        [Description("Defines how many seconds the next map vote lasts.")]
        public int MapVoteDurationSeconds { get { return (int)this["MapVoteDurationSeconds"]; } set { this["MapVoteDurationSeconds"] = value; } }
    }
}