using System.ComponentModel;
using MCDzienny.SettingsFrame;

namespace MCDzienny.Settings
{
    [SettingsPath("properties/general.properties")]
    sealed class GeneralSettings : SettingsFrame.SettingsFrame
    {
        static readonly GeneralSettings defaultInstance = new GeneralSettings();

        public static GeneralSettings All { get { return defaultInstance; } }

        [Description("Defines the minimum permission level that allows to review other players.")]
        [SettingsFrame.DefaultValue("80")]
        [Category("Review")]
        [Setting]
        public int MinPermissionForReview { get { return (int)this["MinPermissionForReview"]; } set { this["MinPermissionForReview"] = value; } }

        [Setting]
        [SettingsFrame.DefaultValue("False")]
        [Category("Basic")]
        [Description("Indicates if the server use the system that reduces CPU usage.")]
        public bool IntelliSys { get { return (bool)this["IntelliSys"]; } set { this["IntelliSys"] = value; } }

        [Category("Basic")]
        [Setting]
        [Description("Indicates if player will be kicked when he lags heavily.")]
        [SettingsFrame.DefaultValue("False")]
        public bool KickSlug { get { return (bool)this["KickSlug"]; } set { this["KickSlug"] = value; } }

        [Description("Indicates whether the average amount of pending packets is being shown in GUI.")]
        [Setting]
        [SettingsFrame.DefaultValue("False")]
        [Category("Basic")]
        public bool ShowServerLag { get { return (bool)this["ShowServerLag"]; } set { this["ShowServerLag"] = value; } }

        [Browsable(false)]
        [Category("Basic")]
        [Setting]
        [SettingsFrame.DefaultValue("60")]
        [Description("Defines the threshold1.")]
        public int Threshold1 { get { return (int)this["Threshold1"]; } set { this["Threshold1"] = value; } }

        [SettingsFrame.DefaultValue("10")]
        [Category("Basic")]
        [Description("Defines the threshold2 multiplier.")]
        [Browsable(false)]
        [Setting]
        public int Threshold2 { get { return (int)this["Threshold2"]; } set { this["Threshold2"] = value; } }

        [Category("Basic")]
        [Description("Defines the physics stop point.")]
        [SettingsFrame.DefaultValue("50")]
        [Setting]
        public int AvgStop { get { return (int)this["AvgStop"]; } set { this["AvgStop"] = value; } }

        [Browsable(false)]
        [Setting]
        [SettingsFrame.DefaultValue("False")]
        [Description("Indicates whether there are special characters displayed in chat.")]
        [Category("Basic")]
        public bool ChatSpecialCharacters { get { return (bool)this["ChatSpecialCharacters"]; } set { this["ChatSpecialCharacters"] = value; } }

        [Browsable(false)]
        [Category("Basic")]
        [SettingsFrame.DefaultValue("Calibri")]
        [Setting]
        [Description("Determines the chat font family.")]
        public string ChatFontFamily { get { return (string)this["ChatFontFamily"]; } set { this["ChatFontFamily"] = value; } }

        [Browsable(false)]
        [Description("Sets the chat font size.")]
        [SettingsFrame.DefaultValue("12")]
        [Setting]
        [Category("Basic")]
        public float ChatFontSize { get { return (float)this["ChatFontSize"]; } set { this["ChatFontSize"] = value; } }

        [Category("Basic")]
        [Description("Sets the chat font size.")]
        [SettingsFrame.DefaultValue("False")]
        [Browsable(false)]
        [Setting]
        public bool UseChat { get { return (bool)this["UseChat"]; } set { this["UseChat"] = value; } }

        [Category("Basic")]
        [Setting]
        [SettingsFrame.DefaultValue("False")]
        [Description("Sets the chat font size.")]
        [Browsable(false)]
        public bool UseCustomName { get { return (bool)this["UseCustomName"]; } set { this["UseCustomName"] = value; } }

        [Browsable(false)]
        [Category("Basic")]
        [Setting]
        [Description("Sets custom console name.")]
        [SettingsFrame.DefaultValue("%cLord of the Server")]
        public string CustomConsoleName { get { return (string)this["CustomConsoleName"]; } set { this["CustomConsoleName"] = value; } }

        [Category("Basic")]
        [Setting]
        [Description("Sets custom console name delimiter.")]
        [SettingsFrame.DefaultValue(":%f")]
        [Browsable(false)]
        public string CustomConsoleNameDelimiter { get { return (string)this["CustomConsoleNameDelimiter"]; } set { this["CustomConsoleNameDelimiter"] = value; } }

        [Category("Extra")]
        [SettingsFrame.DefaultValue("-1")]
        [Description(
            "Defines the maximum height of the pillar that a player can create by placing blocks under himself. It only limits ranks that are less than OP. If the value is negative the maximum height is unlimited.")]
        [Setting]
        public int PillarMaxHeight { get { return (int)this["PillarMaxHeight"]; } set { this["PillarMaxHeight"] = value; } }

        [Description("Check port at start.")]
        [Setting]
        [Browsable(false)]
        [Category("Basic")]
        [SettingsFrame.DefaultValue("True")]
        public bool CheckPortOnStart { get { return (bool)this["CheckPortOnStart"]; } set { this["CheckPortOnStart"] = value; } }

        [SettingsFrame.DefaultValue("64")]
        [Description("Sets default width of a home map.")]
        [Category("Home")]
        [Setting]
        public int HomeMapWidth { get { return (int)this["HomeMapWidth"]; } set { this["HomeMapWidth"] = value; } }

        [Category("Home")]
        [Setting]
        [SettingsFrame.DefaultValue("64")]
        [Description("Sets default height of a home map.")]
        public int HomeMapHeight { get { return (int)this["HomeMapHeight"]; } set { this["HomeMapHeight"] = value; } }

        [Category("Home")]
        [Description("Sets default depth of a home map.")]
        [SettingsFrame.DefaultValue("64")]
        [Setting]
        public int HomeMapDepth { get { return (int)this["HomeMapDepth"]; } set { this["HomeMapDepth"] = value; } }

        [Description("Indicates whether a player that is connected from local ip range is verified.")]
        [SettingsFrame.DefaultValue("False")]
        [Category("Basic")]
        [Setting]
        public bool VerifyNameForLocalIPs { get { return (bool)this["VerifyNameForLocalIPs"]; } set { this["VerifyNameForLocalIPs"] = value; } }

        [Description("Defines the minimum permission level required for receiving Developer's messages to inbox.")]
        [SettingsFrame.DefaultValue("80")]
        [Category("Basic")]
        [Setting]
        public int DevMessagePermission { get { return (int)this["DevMessagePermission"]; } set { this["DevMessagePermission"] = value; } }

        [Setting]
        [Browsable(false)]
        [SettingsFrame.DefaultValue("True")]
        public bool CooldownCmdUse { get { return (bool)this["CooldownCmdUse"]; } set { this["CooldownCmdUse"] = value; } }

        [Setting]
        [SettingsFrame.DefaultValue("4")]
        [Browsable(false)]
        public int CooldownCmdMax { get { return (int)this["CooldownCmdMax"]; } set { this["CooldownCmdMax"] = value; } }

        [SettingsFrame.DefaultValue("8")]
        [Browsable(false)]
        [Setting]
        public int CooldownCmdMaxSeconds { get { return (int)this["CooldownCmdMaxSeconds"]; } set { this["CooldownCmdMaxSeconds"] = value; } }

        [SettingsFrame.DefaultValue("%cWARNING: Slow down! You are using way too many commands per second.")]
        [Setting]
        [Browsable(false)]
        public string CooldownCmdWarning { get { return (string)this["CooldownCmdWarning"]; } set { this["CooldownCmdWarning"] = value; } }

        [Setting]
        [Category("Extra")]
        [SettingsFrame.DefaultValue("20")]
        [Description("Defines how many seconds have to pass before a player can die again. It affects deaths caused by blocks e.g. lava, shark.")]
        public int DeathCooldown { get { return (int)this["DeathCooldown"]; } set { this["DeathCooldown"] = value; } }

        [Setting]
        [Category("Extra")]
        [Description("Asks WoM users to use XWoM instead and kicks them.")]
        [SettingsFrame.DefaultValue("False")]
        public bool KickWomUsers { get { return (bool)this["KickWomUsers"]; } set { this["KickWomUsers"] = value; } }

        [Category("ClassiCube")]
        [Setting]
        [Description("Determines whether a server sends a heartbeat to ClassiCube server list and whether players from there can join the server.")]
        [SettingsFrame.DefaultValue("True")]
        public bool AllowAndListOnClassiCube { get { return (bool)this["AllowAndListOnClassiCube"]; } set { this["AllowAndListOnClassiCube"] = value; } }

        [Category("ClassiCube")]
        [Setting]
        [SettingsFrame.DefaultValue("True")]
        [Description("Indicates if the + sign is displayed at the end of a players' name.")]
        public bool PlusMarkerForClassiCubeAccount { get { return (bool)this["PlusMarkerForClassiCubeAccount"]; } set { this["PlusMarkerForClassiCubeAccount"] = value; } }

        [SettingsFrame.DefaultValue("True")]
        [Category("ClassiCube")]
        [Description("Indicates whether the server sends custom messages.")]
        [Setting]
        public bool ExperimentalMessages { get { return (bool)this["ExperimentalMessages"]; } set { this["ExperimentalMessages"] = value; } }
    }
}