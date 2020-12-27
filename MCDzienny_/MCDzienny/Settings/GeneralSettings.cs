using System.ComponentModel;
using MCDzienny.SettingsFrame;

namespace MCDzienny.Settings
{
    // Token: 0x02000229 RID: 553
    [SettingsPath("properties/general.properties")]
    internal sealed class GeneralSettings : SettingsFrame.SettingsFrame
    {
        // Token: 0x04000882 RID: 2178
        private static readonly GeneralSettings defaultInstance = new GeneralSettings();

        // Token: 0x17000572 RID: 1394
        // (get) Token: 0x06000F5E RID: 3934 RVA: 0x00053EB8 File Offset: 0x000520B8
        public static GeneralSettings All
        {
            get { return defaultInstance; }
        }

        // Token: 0x17000573 RID: 1395
        // (get) Token: 0x06000F5F RID: 3935 RVA: 0x00053EC0 File Offset: 0x000520C0
        // (set) Token: 0x06000F60 RID: 3936 RVA: 0x00053ED4 File Offset: 0x000520D4
        [Description("Defines the minimum permission level that allows to review other players.")]
        [SettingsFrame.DefaultValue("80")]
        [Category("Review")]
        [Setting]
        public int MinPermissionForReview
        {
            get { return (int) this["MinPermissionForReview"]; }
            set { this["MinPermissionForReview"] = value; }
        }

        // Token: 0x17000574 RID: 1396
        // (get) Token: 0x06000F61 RID: 3937 RVA: 0x00053EE8 File Offset: 0x000520E8
        // (set) Token: 0x06000F62 RID: 3938 RVA: 0x00053EFC File Offset: 0x000520FC
        [Setting]
        [SettingsFrame.DefaultValue("False")]
        [Category("Basic")]
        [Description("Indicates if the server use the system that reduces CPU usage.")]
        public bool IntelliSys
        {
            get { return (bool) this["IntelliSys"]; }
            set { this["IntelliSys"] = value; }
        }

        // Token: 0x17000575 RID: 1397
        // (get) Token: 0x06000F63 RID: 3939 RVA: 0x00053F10 File Offset: 0x00052110
        // (set) Token: 0x06000F64 RID: 3940 RVA: 0x00053F24 File Offset: 0x00052124
        [Category("Basic")]
        [Setting]
        [Description("Indicates if player will be kicked when he lags heavily.")]
        [SettingsFrame.DefaultValue("False")]
        public bool KickSlug
        {
            get { return (bool) this["KickSlug"]; }
            set { this["KickSlug"] = value; }
        }

        // Token: 0x17000576 RID: 1398
        // (get) Token: 0x06000F65 RID: 3941 RVA: 0x00053F38 File Offset: 0x00052138
        // (set) Token: 0x06000F66 RID: 3942 RVA: 0x00053F4C File Offset: 0x0005214C
        [Description("Indicates whether the average amount of pending packets is being shown in GUI.")]
        [Setting]
        [SettingsFrame.DefaultValue("False")]
        [Category("Basic")]
        public bool ShowServerLag
        {
            get { return (bool) this["ShowServerLag"]; }
            set { this["ShowServerLag"] = value; }
        }

        // Token: 0x17000577 RID: 1399
        // (get) Token: 0x06000F67 RID: 3943 RVA: 0x00053F60 File Offset: 0x00052160
        // (set) Token: 0x06000F68 RID: 3944 RVA: 0x00053F74 File Offset: 0x00052174
        [Browsable(false)]
        [Category("Basic")]
        [Setting]
        [SettingsFrame.DefaultValue("60")]
        [Description("Defines the threshold1.")]
        public int Threshold1
        {
            get { return (int) this["Threshold1"]; }
            set { this["Threshold1"] = value; }
        }

        // Token: 0x17000578 RID: 1400
        // (get) Token: 0x06000F69 RID: 3945 RVA: 0x00053F88 File Offset: 0x00052188
        // (set) Token: 0x06000F6A RID: 3946 RVA: 0x00053F9C File Offset: 0x0005219C
        [SettingsFrame.DefaultValue("10")]
        [Category("Basic")]
        [Description("Defines the threshold2 multiplier.")]
        [Browsable(false)]
        [Setting]
        public int Threshold2
        {
            get { return (int) this["Threshold2"]; }
            set { this["Threshold2"] = value; }
        }

        // Token: 0x17000579 RID: 1401
        // (get) Token: 0x06000F6B RID: 3947 RVA: 0x00053FB0 File Offset: 0x000521B0
        // (set) Token: 0x06000F6C RID: 3948 RVA: 0x00053FC4 File Offset: 0x000521C4
        [Category("Basic")]
        [Description("Defines the physics stop point.")]
        [SettingsFrame.DefaultValue("50")]
        [Setting]
        public int AvgStop
        {
            get { return (int) this["AvgStop"]; }
            set { this["AvgStop"] = value; }
        }

        // Token: 0x1700057A RID: 1402
        // (get) Token: 0x06000F6D RID: 3949 RVA: 0x00053FD8 File Offset: 0x000521D8
        // (set) Token: 0x06000F6E RID: 3950 RVA: 0x00053FEC File Offset: 0x000521EC
        [Browsable(false)]
        [Setting]
        [SettingsFrame.DefaultValue("False")]
        [Description("Indicates whether there are special characters displayed in chat.")]
        [Category("Basic")]
        public bool ChatSpecialCharacters
        {
            get { return (bool) this["ChatSpecialCharacters"]; }
            set { this["ChatSpecialCharacters"] = value; }
        }

        // Token: 0x1700057B RID: 1403
        // (get) Token: 0x06000F6F RID: 3951 RVA: 0x00054000 File Offset: 0x00052200
        // (set) Token: 0x06000F70 RID: 3952 RVA: 0x00054014 File Offset: 0x00052214
        [Browsable(false)]
        [Category("Basic")]
        [SettingsFrame.DefaultValue("Calibri")]
        [Setting]
        [Description("Determines the chat font family.")]
        public string ChatFontFamily
        {
            get { return (string) this["ChatFontFamily"]; }
            set { this["ChatFontFamily"] = value; }
        }

        // Token: 0x1700057C RID: 1404
        // (get) Token: 0x06000F71 RID: 3953 RVA: 0x00054024 File Offset: 0x00052224
        // (set) Token: 0x06000F72 RID: 3954 RVA: 0x00054038 File Offset: 0x00052238
        [Browsable(false)]
        [Description("Sets the chat font size.")]
        [SettingsFrame.DefaultValue("12")]
        [Setting]
        [Category("Basic")]
        public float ChatFontSize
        {
            get { return (float) this["ChatFontSize"]; }
            set { this["ChatFontSize"] = value; }
        }

        // Token: 0x1700057D RID: 1405
        // (get) Token: 0x06000F73 RID: 3955 RVA: 0x0005404C File Offset: 0x0005224C
        // (set) Token: 0x06000F74 RID: 3956 RVA: 0x00054060 File Offset: 0x00052260
        [Category("Basic")]
        [Description("Sets the chat font size.")]
        [SettingsFrame.DefaultValue("False")]
        [Browsable(false)]
        [Setting]
        public bool UseChat
        {
            get { return (bool) this["UseChat"]; }
            set { this["UseChat"] = value; }
        }

        // Token: 0x1700057E RID: 1406
        // (get) Token: 0x06000F75 RID: 3957 RVA: 0x00054074 File Offset: 0x00052274
        // (set) Token: 0x06000F76 RID: 3958 RVA: 0x00054088 File Offset: 0x00052288
        [Category("Basic")]
        [Setting]
        [SettingsFrame.DefaultValue("False")]
        [Description("Sets the chat font size.")]
        [Browsable(false)]
        public bool UseCustomName
        {
            get { return (bool) this["UseCustomName"]; }
            set { this["UseCustomName"] = value; }
        }

        // Token: 0x1700057F RID: 1407
        // (get) Token: 0x06000F77 RID: 3959 RVA: 0x0005409C File Offset: 0x0005229C
        // (set) Token: 0x06000F78 RID: 3960 RVA: 0x000540B0 File Offset: 0x000522B0
        [Browsable(false)]
        [Category("Basic")]
        [Setting]
        [Description("Sets custom console name.")]
        [SettingsFrame.DefaultValue("%cLord of the Server")]
        public string CustomConsoleName
        {
            get { return (string) this["CustomConsoleName"]; }
            set { this["CustomConsoleName"] = value; }
        }

        // Token: 0x17000580 RID: 1408
        // (get) Token: 0x06000F79 RID: 3961 RVA: 0x000540C0 File Offset: 0x000522C0
        // (set) Token: 0x06000F7A RID: 3962 RVA: 0x000540D4 File Offset: 0x000522D4
        [Category("Basic")]
        [Setting]
        [Description("Sets custom console name delimiter.")]
        [SettingsFrame.DefaultValue(":%f")]
        [Browsable(false)]
        public string CustomConsoleNameDelimiter
        {
            get { return (string) this["CustomConsoleNameDelimiter"]; }
            set { this["CustomConsoleNameDelimiter"] = value; }
        }

        // Token: 0x17000581 RID: 1409
        // (get) Token: 0x06000F7B RID: 3963 RVA: 0x000540E4 File Offset: 0x000522E4
        // (set) Token: 0x06000F7C RID: 3964 RVA: 0x000540F8 File Offset: 0x000522F8
        [Category("Extra")]
        [SettingsFrame.DefaultValue("-1")]
        [Description(
            "Defines the maximum height of the pillar that a player can create by placing blocks under himself. It only limits ranks that are less than OP. If the value is negative the maximum height is unlimited.")]
        [Setting]
        public int PillarMaxHeight
        {
            get { return (int) this["PillarMaxHeight"]; }
            set { this["PillarMaxHeight"] = value; }
        }

        // Token: 0x17000582 RID: 1410
        // (get) Token: 0x06000F7D RID: 3965 RVA: 0x0005410C File Offset: 0x0005230C
        // (set) Token: 0x06000F7E RID: 3966 RVA: 0x00054120 File Offset: 0x00052320
        [Description("Check port at start.")]
        [Setting]
        [Browsable(false)]
        [Category("Basic")]
        [SettingsFrame.DefaultValue("True")]
        public bool CheckPortOnStart
        {
            get { return (bool) this["CheckPortOnStart"]; }
            set { this["CheckPortOnStart"] = value; }
        }

        // Token: 0x17000583 RID: 1411
        // (get) Token: 0x06000F7F RID: 3967 RVA: 0x00054134 File Offset: 0x00052334
        // (set) Token: 0x06000F80 RID: 3968 RVA: 0x00054148 File Offset: 0x00052348
        [SettingsFrame.DefaultValue("64")]
        [Description("Sets default width of a home map.")]
        [Category("Home")]
        [Setting]
        public int HomeMapWidth
        {
            get { return (int) this["HomeMapWidth"]; }
            set { this["HomeMapWidth"] = value; }
        }

        // Token: 0x17000584 RID: 1412
        // (get) Token: 0x06000F81 RID: 3969 RVA: 0x0005415C File Offset: 0x0005235C
        // (set) Token: 0x06000F82 RID: 3970 RVA: 0x00054170 File Offset: 0x00052370
        [Category("Home")]
        [Setting]
        [SettingsFrame.DefaultValue("64")]
        [Description("Sets default height of a home map.")]
        public int HomeMapHeight
        {
            get { return (int) this["HomeMapHeight"]; }
            set { this["HomeMapHeight"] = value; }
        }

        // Token: 0x17000585 RID: 1413
        // (get) Token: 0x06000F83 RID: 3971 RVA: 0x00054184 File Offset: 0x00052384
        // (set) Token: 0x06000F84 RID: 3972 RVA: 0x00054198 File Offset: 0x00052398
        [Category("Home")]
        [Description("Sets default depth of a home map.")]
        [SettingsFrame.DefaultValue("64")]
        [Setting]
        public int HomeMapDepth
        {
            get { return (int) this["HomeMapDepth"]; }
            set { this["HomeMapDepth"] = value; }
        }

        // Token: 0x17000586 RID: 1414
        // (get) Token: 0x06000F85 RID: 3973 RVA: 0x000541AC File Offset: 0x000523AC
        // (set) Token: 0x06000F86 RID: 3974 RVA: 0x000541C0 File Offset: 0x000523C0
        [Description("Indicates whether a player that is connected from local ip range is verified.")]
        [SettingsFrame.DefaultValue("False")]
        [Category("Basic")]
        [Setting]
        public bool VerifyNameForLocalIPs
        {
            get { return (bool) this["VerifyNameForLocalIPs"]; }
            set { this["VerifyNameForLocalIPs"] = value; }
        }

        // Token: 0x17000587 RID: 1415
        // (get) Token: 0x06000F87 RID: 3975 RVA: 0x000541D4 File Offset: 0x000523D4
        // (set) Token: 0x06000F88 RID: 3976 RVA: 0x000541E8 File Offset: 0x000523E8
        [Description("Defines the minimum permission level required for receiving Developer's messages to inbox.")]
        [SettingsFrame.DefaultValue("80")]
        [Category("Basic")]
        [Setting]
        public int DevMessagePermission
        {
            get { return (int) this["DevMessagePermission"]; }
            set { this["DevMessagePermission"] = value; }
        }

        // Token: 0x17000588 RID: 1416
        // (get) Token: 0x06000F89 RID: 3977 RVA: 0x000541FC File Offset: 0x000523FC
        // (set) Token: 0x06000F8A RID: 3978 RVA: 0x00054210 File Offset: 0x00052410
        [Setting]
        [Browsable(false)]
        [SettingsFrame.DefaultValue("True")]
        public bool CooldownCmdUse
        {
            get { return (bool) this["CooldownCmdUse"]; }
            set { this["CooldownCmdUse"] = value; }
        }

        // Token: 0x17000589 RID: 1417
        // (get) Token: 0x06000F8B RID: 3979 RVA: 0x00054224 File Offset: 0x00052424
        // (set) Token: 0x06000F8C RID: 3980 RVA: 0x00054238 File Offset: 0x00052438
        [Setting]
        [SettingsFrame.DefaultValue("4")]
        [Browsable(false)]
        public int CooldownCmdMax
        {
            get { return (int) this["CooldownCmdMax"]; }
            set { this["CooldownCmdMax"] = value; }
        }

        // Token: 0x1700058A RID: 1418
        // (get) Token: 0x06000F8D RID: 3981 RVA: 0x0005424C File Offset: 0x0005244C
        // (set) Token: 0x06000F8E RID: 3982 RVA: 0x00054260 File Offset: 0x00052460
        [SettingsFrame.DefaultValue("8")]
        [Browsable(false)]
        [Setting]
        public int CooldownCmdMaxSeconds
        {
            get { return (int) this["CooldownCmdMaxSeconds"]; }
            set { this["CooldownCmdMaxSeconds"] = value; }
        }

        // Token: 0x1700058B RID: 1419
        // (get) Token: 0x06000F8F RID: 3983 RVA: 0x00054274 File Offset: 0x00052474
        // (set) Token: 0x06000F90 RID: 3984 RVA: 0x00054288 File Offset: 0x00052488
        [SettingsFrame.DefaultValue("%cWARNING: Slow down! You are using way too many commands per second.")]
        [Setting]
        [Browsable(false)]
        public string CooldownCmdWarning
        {
            get { return (string) this["CooldownCmdWarning"]; }
            set { this["CooldownCmdWarning"] = value; }
        }

        // Token: 0x1700058C RID: 1420
        // (get) Token: 0x06000F91 RID: 3985 RVA: 0x00054298 File Offset: 0x00052498
        // (set) Token: 0x06000F92 RID: 3986 RVA: 0x000542AC File Offset: 0x000524AC
        [Setting]
        [Category("Extra")]
        [SettingsFrame.DefaultValue("20")]
        [Description(
            "Defines how many seconds have to pass before a player can die again. It affects deaths caused by blocks e.g. lava, shark.")]
        public int DeathCooldown
        {
            get { return (int) this["DeathCooldown"]; }
            set { this["DeathCooldown"] = value; }
        }

        // Token: 0x1700058D RID: 1421
        // (get) Token: 0x06000F93 RID: 3987 RVA: 0x000542C0 File Offset: 0x000524C0
        // (set) Token: 0x06000F94 RID: 3988 RVA: 0x000542D4 File Offset: 0x000524D4
        [Setting]
        [Category("Extra")]
        [Description("Asks WoM users to use XWoM instead and kicks them.")]
        [SettingsFrame.DefaultValue("False")]
        public bool KickWomUsers
        {
            get { return (bool) this["KickWomUsers"]; }
            set { this["KickWomUsers"] = value; }
        }

        // Token: 0x1700058E RID: 1422
        // (get) Token: 0x06000F95 RID: 3989 RVA: 0x000542E8 File Offset: 0x000524E8
        // (set) Token: 0x06000F96 RID: 3990 RVA: 0x000542FC File Offset: 0x000524FC
        [Category("ClassiCube")]
        [Setting]
        [Description(
            "Determines whether a server sends a heartbeat to ClassiCube server list and whether players from there can join the server.")]
        [SettingsFrame.DefaultValue("True")]
        public bool AllowAndListOnClassiCube
        {
            get { return (bool) this["AllowAndListOnClassiCube"]; }
            set { this["AllowAndListOnClassiCube"] = value; }
        }

        // Token: 0x1700058F RID: 1423
        // (get) Token: 0x06000F97 RID: 3991 RVA: 0x00054310 File Offset: 0x00052510
        // (set) Token: 0x06000F98 RID: 3992 RVA: 0x00054324 File Offset: 0x00052524
        [Category("ClassiCube")]
        [Setting]
        [SettingsFrame.DefaultValue("True")]
        [Description("Indicates if the + sign is displayed at the end of a players' name.")]
        public bool PlusMarkerForClassiCubeAccount
        {
            get { return (bool) this["PlusMarkerForClassiCubeAccount"]; }
            set { this["PlusMarkerForClassiCubeAccount"] = value; }
        }

        // Token: 0x17000590 RID: 1424
        // (get) Token: 0x06000F99 RID: 3993 RVA: 0x00054338 File Offset: 0x00052538
        // (set) Token: 0x06000F9A RID: 3994 RVA: 0x0005434C File Offset: 0x0005254C
        [SettingsFrame.DefaultValue("True")]
        [Category("ClassiCube")]
        [Description("Indicates whether the server sends custom messages.")]
        [Setting]
        public bool ExperimentalMessages
        {
            get { return (bool) this["ExperimentalMessages"]; }
            set { this["ExperimentalMessages"] = value; }
        }
    }
}