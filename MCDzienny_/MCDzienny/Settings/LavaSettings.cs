using System.ComponentModel;
using MCDzienny.SettingsFrame;

namespace MCDzienny.Settings
{
    [SettingsPath("properties/lava.properties")]
    sealed class LavaSettings : SettingsFrame.SettingsFrame
    {
        static readonly LavaSettings defaultInstance = new LavaSettings();

        public static LavaSettings All { get { return defaultInstance; } }

        [Setting]
        [Category("Basic")]
        [SettingsFrame.DefaultValue("True")]
        [Description("Indicates whether players are moved to the spawn point on death.")]
        public bool SpawnOnDeath { get { return (bool)this["SpawnOnDeath"]; } set { this["SpawnOnDeath"] = value; } }

        [Category("Basic")]
        [Description("Indicates whether dead players are headless.")]
        [Setting]
        [SettingsFrame.DefaultValue("True")]
        public bool HeadlessGhosts { get { return (bool)this["HeadlessGhosts"]; } set { this["HeadlessGhosts"] = value; } }

        [Setting]
        [SettingsFrame.DefaultValue("True")]
        [Description("Indicates whether the map voting system is active.")]
        [Category("Basic")]
        public bool VotingSystem { get { return (bool)this["VotingSystem"]; } set { this["VotingSystem"] = value; } }

        [SettingsFrame.DefaultValue("True")]
        [Description("Indicates if name color changes according to player level.")]
        [Category("Basic")]
        [Setting]
        public bool AutoNameColoring { get { return (bool)this["AutoNameColoring"]; } set { this["AutoNameColoring"] = value; } }

        [Category("Basic")]
        [Setting]
        [SettingsFrame.DefaultValue("True")]
        [Description("Indicates if players are given stars for winning streak. One star gives 10% higher score, two starts 20%, and three 30%.")]
        public bool StarSystem { get { return (bool)this["StarSystem"]; } set { this["StarSystem"] = value; } }

        [Category("Basic")]
        [SettingsFrame.DefaultValue("True")]
        [Description("Indicates that the map rating should be displayed when the next map is loaded.")]
        [Setting]
        public bool ShowMapRating { get { return (bool)this["ShowMapRating"]; } set { this["ShowMapRating"] = value; } }

        [Category("Basic")]
        [SettingsFrame.DefaultValue("True")]
        [Description("Indicates whether lava mood changes according to random pattern.")]
        [Setting]
        public bool RandomLavaState { get { return (bool)this["RandomLavaState"]; } set { this["RandomLavaState"] = value; } }

        [Category("Basic")]
        [SettingsFrame.DefaultValue("Disturbed")]
        [Setting]
        [Description("Determines the lava state.")]
        public LavaState LavaState { get { return (LavaState)this["LavaState"]; } set { this["LavaState"] = value; } }

        [Setting]
        [SettingsFrame.DefaultValue("True")]
        [Description("Indicates whether the anti-score-abuse system is active.")]
        [Category("Extended")]
        public bool PreventScoreAbuse { get { return (bool)this["PreventScoreAbuse"]; } set { this["PreventScoreAbuse"] = value; } }

        [SettingsFrame.DefaultValue("False")]
        [Description("Indicates if lowlag mode is turned on when the lava flood starts. It turns off automatically after a certain amount of time.")]
        [Setting]
        [Category("Extended")]
        public bool OverloadProtection { get { return (bool)this["OverloadProtection"]; } set { this["OverloadProtection"] = value; } }

        [Description("Indicates whether the connection speed test for each player on log-in is performed. If a result indicates high latency, the player is kicked.")]
        [Category("Extended")]
        [Setting]
        [SettingsFrame.DefaultValue("False")]
        public bool ConnectionSpeedTest { get { return (bool)this["ConnectionSpeedTest"]; } set { this["ConnectionSpeedTest"] = value; } }

        [Setting]
        [Category("Extended")]
        [SettingsFrame.DefaultValue("False")]
        [Description(
            "Indicates if server is locked for 45 seconds when Buffer Overload Error happens. When the lock is active no one can enter the server. It may help in stabilizing the internet connection and system resources use, so that overall less players get kicked.")]
        public bool AutoServerLock { get { return (bool)this["AutoServerLock"]; } set { this["AutoServerLock"] = value; } }

        [Description("Determines the delay between each lava move. The default value is 4.")]
        [SettingsFrame.DefaultValue("4")]
        [Category("Extended")]
        [Setting]
        public int LavaMovementDelay { get { return (int)this["LavaMovementDelay"]; } set { this["LavaMovementDelay"] = value; } }

        [Setting]
        [SettingsFrame.DefaultValue("True")]
        [Category("Extended")]
        [Description("Indicates if Distance Offset Message is displayed.")]
        public bool ShowDistanceOffsetMessage { get { return (bool)this["ShowDistanceOffsetMessage"]; } set { this["ShowDistanceOffsetMessage"] = value; } }

        [Category("Extended")]
        [Description("Defines the reward that is given to winners when they are below the sea level.")]
        [SettingsFrame.DefaultValue("25")]
        [Setting]
        public int RewardBelowSeaLevel { get { return (int)this["RewardBelowSeaLevel"]; } set { this["RewardBelowSeaLevel"] = value; } }

        [SettingsFrame.DefaultValue("30")]
        [Setting]
        [Description("Defines the reward that is given to winners when they are above the sea level.")]
        [Category("Extended")]
        public int RewardAboveSeaLevel { get { return (int)this["RewardAboveSeaLevel"]; } set { this["RewardAboveSeaLevel"] = value; } }

        [Category("Extended")]
        [Description("Indicates whether variables such as: $name, $money, etc. can be used in game.")]
        [Setting]
        [SettingsFrame.DefaultValue("True")]
        public bool AllowInGameVariables { get { return (bool)this["AllowInGameVariables"]; } set { this["AllowInGameVariables"] = value; } }

        [SettingsFrame.DefaultValue("False")]
        [Setting]
        [Category("Extended")]
        [Description("Indicates if the map author name is displayed on the beginning of the lava survival round.")]
        public bool ShowMapAuthor { get { return (bool)this["ShowMapAuthor"]; } set { this["ShowMapAuthor"] = value; } }

        [SettingsFrame.DefaultValue("BasedOnAir")]
        [Description(
            "Score mode: BasedOnAir - standard mode, players get score based on the amount of air around them. Fixed - winners always get the same amount of points.")]
        [Setting]
        [Category("Extended")]
        public ScoreSystem ScoreMode { get { return (ScoreSystem)this["ScoreMode"]; } set { this["ScoreMode"] = value; } }

        [Category("Extended")]
        [Setting]
        [Description("Defines the score that is given to each winner in the fixed score mode.")]
        [SettingsFrame.DefaultValue("500")]
        public int ScoreRewardFixed { get { return (int)this["ScoreRewardFixed"]; } set { this["ScoreRewardFixed"] = value; } }

        [Category("Extended")]
        [SettingsFrame.DefaultValue("5")]
        [Description("Defines the minimum distance from the spawn point measured in blocks that will allow a player to win a round.")]
        [Setting]
        public int RequiredDistanceFromSpawn { get { return (int)this["RequiredDistanceFromSpawn"]; } set { this["RequiredDistanceFromSpawn"] = value; } }

        [SettingsFrame.DefaultValue("False")]
        [Category("Basic")]
        [Description("Indicates whether the chat on a lava map is connected to other maps.")]
        [Setting]
        public bool LavaWorldChat { get { return (bool)this["LavaWorldChat"]; } set { this["LavaWorldChat"] = value; } }

        [Category("Basic")]
        [Description("Defines the maximum amount of players that can stay on lava map at the same time. Value '0' means that there is no limit.")]
        [Setting]
        [SettingsFrame.DefaultValue("0")]
        public int LavaMapPlayerLimit { get { return (int)this["LavaMapPlayerLimit"]; } set { this["LavaMapPlayerLimit"] = value; } }

        [Category("Hacks")]
        [SettingsFrame.DefaultValue("False")]
        [Description("In case of hack detection system warns player. If player keeps using hacks he gets kicked.")]
        [Setting]
        public bool DisallowHacksUseOnLavaMap { get { return (bool)this["DisallowHacksUseOnLavaMap"]; } set { this["DisallowHacksUseOnLavaMap"] = value; } }

        [Setting]
        [Category("Hacks")]
        [SettingsFrame.DefaultValue("80")]
        [Description("Sets minimum permission level for using hacks on lava map. For example: Builder = 30, Operator = 80, Admin = 100.")]
        public int HacksUseOnLavaMapPermission { get { return (int)this["HacksUseOnLavaMapPermission"]; } set { this["HacksUseOnLavaMapPermission"] = value; } }

        [Category("Extended")]
        [Description("Determines whether players are allowed to use cuboid on lava maps.")]
        [Setting]
        [SettingsFrame.DefaultValue("False")]
        public bool AllowCuboidOnLavaMaps { get { return (bool)this["AllowCuboidOnLavaMaps"]; } set { this["AllowCuboidOnLavaMaps"] = value; } }

        [Description("Defines the reward from breaking a treasure block.")]
        [Setting]
        [SettingsFrame.DefaultValue("5")]
        [Category("Extended")]
        public int AmountOfMoneyInTreasure { get { return (int)this["AmountOfMoneyInTreasure"]; } set { this["AmountOfMoneyInTreasure"] = value; } }

        [SettingsFrame.DefaultValue("False")]
        [Setting]
        [Description("Determines whether players are allowed to place a gold rock on lava maps.")]
        [Category("Extended")]
        public bool AllowGoldRockOnLavaMaps { get { return (bool)this["AllowGoldRockOnLavaMaps"]; } set { this["AllowGoldRockOnLavaMaps"] = value; } }

        [Category("Extended")]
        [Description("Sets the minimum amount of players that leads to hide of death messages. If set to 0 the death messages are never showed.")]
        [SettingsFrame.DefaultValue("20")]
        [Setting]
        public int HideDeathMessagesAmount { get { return (int)this["HideDeathMessagesAmount"]; } set { this["HideDeathMessagesAmount"] = value; } }

        [Setting]
        [Category("Extended")]
        [Description("Sets upper level of BOPL antigrief system. Beyond the level players are not monitored by Based On Player's Level antigrief protection.")]
        [SettingsFrame.DefaultValue("8")]
        public int UpperLevelOfBoplAntigrief { get { return (int)this["UpperLevelOfBoplAntigrief"]; } set { this["UpperLevelOfBoplAntigrief"] = value; } }

        [SettingsFrame.DefaultValue("3")]
        [Description("Defines the amount of lives that every player starts with at the beginning of each round.")]
        [Setting]
        [Category("Extended")]
        public byte LivesAtStart { get { return (byte)this["LivesAtStart"]; } set { this["LivesAtStart"] = value; } }

        [Setting]
        [Category("Basic")]
        [SettingsFrame.DefaultValue("BasedOnPlayersLevel")]
        [Description("Describes the type of antigrief system that the server uses. Valid values are BasedOnName and BasedOnPlayersLevel.")]
        public AntigriefType Antigrief { get { return (AntigriefType)this["Antigrief"]; } set { this["Antigrief"] = value; } }

        [Setting]
        [Category("Extended")]
        [Description("Determines whether players are allowed to build near a lava spawn.")]
        [SettingsFrame.DefaultValue("True")]
        public bool DisallowBuildingNearLavaSpawn { get { return (bool)this["DisallowBuildingNearLavaSpawn"]; } set { this["DisallowBuildingNearLavaSpawn"] = value; } }

        [Description("Determines whether players are allowed to place sponges near a lava spawn.")]
        [SettingsFrame.DefaultValue("True")]
        [Setting]
        [Category("Extended")]
        public bool DisallowSpongesNearLavaSpawn { get { return (bool)this["DisallowSpongesNearLavaSpawn"]; } set { this["DisallowSpongesNearLavaSpawn"] = value; } }

        [Setting]
        [Description("Defines whether a player has to be registered (on the forum) before he can get a promotion. Don't turn it on if you don't know how it works!!!")]
        [SettingsFrame.DefaultValue("False")]
        [Category("Special")]
        public bool RequireRegistrationForPromotion
        {
            get { return (bool)this["RequireRegistrationForPromotion"]; }
            set { this["RequireRegistrationForPromotion"] = value; }
        }

        [SettingsFrame.DefaultValue("False")]
        [Setting]
        public bool DisallowSpleefing { get { return (bool)this["DisallowSpleefing"]; } set { this["DisallowSpleefing"] = value; } }

        [SettingsFrame.DefaultValue("False")]
        [Setting]
        public bool OpsBypassSpleefPrevention { get { return (bool)this["OpsBypassSpleefPrevention"]; } set { this["OpsBypassSpleefPrevention"] = value; } }

        [Setting]
        [Category("Extended")]
        [SettingsFrame.DefaultValue("True")]
        [Description("Indicates whether a player can use /afk command during a map vote.")]
        public bool IsAfkDuringVoteAllowed { get { return (bool)this["IsAfkDuringVoteAllowed"]; } set { this["IsAfkDuringVoteAllowed"] = value; } }
    }
}