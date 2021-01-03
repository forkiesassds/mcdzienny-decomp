using MCDzienny.SettingsFrame;

namespace MCDzienny.Settings
{
    // Token: 0x02000228 RID: 552
    [SettingsPath("properties/chatfilter.properties")]
    internal sealed class ChatFilterSettings : SettingsFrame.SettingsFrame
    {
        // Token: 0x04000881 RID: 2177
        private static readonly ChatFilterSettings defaultInstance = new ChatFilterSettings();

        // Token: 0x17000559 RID: 1369
        // (get) Token: 0x06000F2B RID: 3883 RVA: 0x00053AF8 File Offset: 0x00051CF8
        public static ChatFilterSettings All
        {
            get { return defaultInstance; }
        }

        // Token: 0x1700055A RID: 1370
        // (get) Token: 0x06000F2C RID: 3884 RVA: 0x00053B00 File Offset: 0x00051D00
        // (set) Token: 0x06000F2D RID: 3885 RVA: 0x00053B14 File Offset: 0x00051D14
        [Setting]
        [DefaultValue("True")]
        public bool RemoveCaps
        {
            get { return (bool) this["RemoveCaps"]; }
            set { this["RemoveCaps"] = value; }
        }

        // Token: 0x1700055B RID: 1371
        // (get) Token: 0x06000F2E RID: 3886 RVA: 0x00053B28 File Offset: 0x00051D28
        // (set) Token: 0x06000F2F RID: 3887 RVA: 0x00053B3C File Offset: 0x00051D3C
        [DefaultValue("True")]
        [Setting]
        public bool ShortenRepetitions
        {
            get { return (bool) this["ShortenRepetitions"]; }
            set { this["ShortenRepetitions"] = value; }
        }

        // Token: 0x1700055C RID: 1372
        // (get) Token: 0x06000F30 RID: 3888 RVA: 0x00053B50 File Offset: 0x00051D50
        // (set) Token: 0x06000F31 RID: 3889 RVA: 0x00053B64 File Offset: 0x00051D64
        [Setting]
        [DefaultValue("True")]
        public bool RemoveBadWords
        {
            get { return (bool) this["RemoveBadWords"]; }
            set { this["RemoveBadWords"] = value; }
        }

        // Token: 0x1700055D RID: 1373
        // (get) Token: 0x06000F32 RID: 3890 RVA: 0x00053B78 File Offset: 0x00051D78
        // (set) Token: 0x06000F33 RID: 3891 RVA: 0x00053B8C File Offset: 0x00051D8C
        [Setting]
        [DefaultValue("True")]
        public bool MessagesCooldown
        {
            get { return (bool) this["MessagesCooldown"]; }
            set { this["MessagesCooldown"] = value; }
        }

        // Token: 0x1700055E RID: 1374
        // (get) Token: 0x06000F34 RID: 3892 RVA: 0x00053BA0 File Offset: 0x00051DA0
        // (set) Token: 0x06000F35 RID: 3893 RVA: 0x00053BB4 File Offset: 0x00051DB4
        [Setting]
        [DefaultValue("False")]
        public bool GuiShowAdvancedSettings
        {
            get { return (bool) this["GuiShowAdvancedSettings"]; }
            set { this["GuiShowAdvancedSettings"] = value; }
        }

        // Token: 0x1700055F RID: 1375
        // (get) Token: 0x06000F36 RID: 3894 RVA: 0x00053BC8 File Offset: 0x00051DC8
        // (set) Token: 0x06000F37 RID: 3895 RVA: 0x00053BDC File Offset: 0x00051DDC
        [Setting]
        [DefaultValue("4")]
        public int MaxCaps
        {
            get { return (int) this["MaxCaps"]; }
            set { this["MaxCaps"] = value; }
        }

        // Token: 0x17000560 RID: 1376
        // (get) Token: 0x06000F38 RID: 3896 RVA: 0x00053BF0 File Offset: 0x00051DF0
        // (set) Token: 0x06000F39 RID: 3897 RVA: 0x00053C04 File Offset: 0x00051E04
        [Setting]
        [DefaultValue("SendWarning")]
        public ChatFilter.CharacterSpamAction CharSpamAction
        {
            get { return (ChatFilter.CharacterSpamAction) this["CharSpamAction"]; }
            set { this["CharSpamAction"] = value; }
        }

        // Token: 0x17000561 RID: 1377
        // (get) Token: 0x06000F3A RID: 3898 RVA: 0x00053C18 File Offset: 0x00051E18
        // (set) Token: 0x06000F3B RID: 3899 RVA: 0x00053C2C File Offset: 0x00051E2C
        [DefaultValue("3")]
        [Setting]
        public int CharSpamMaxChars
        {
            get { return (int) this["CharSpamMaxChars"]; }
            set { this["CharSpamMaxChars"] = value; }
        }

        // Token: 0x17000562 RID: 1378
        // (get) Token: 0x06000F3C RID: 3900 RVA: 0x00053C40 File Offset: 0x00051E40
        // (set) Token: 0x06000F3D RID: 3901 RVA: 0x00053C54 File Offset: 0x00051E54
        [DefaultValue("1")]
        [Setting]
        public int CharSpamMaxIllegalGroups
        {
            get { return (int) this["CharSpamMaxIllegalGroups"]; }
            set { this["CharSpamMaxIllegalGroups"] = value; }
        }

        // Token: 0x17000563 RID: 1379
        // (get) Token: 0x06000F3E RID: 3902 RVA: 0x00053C68 File Offset: 0x00051E68
        // (set) Token: 0x06000F3F RID: 3903 RVA: 0x00053C7C File Offset: 0x00051E7C
        [Setting]
        [DefaultValue("%f[%cSpam%f]")]
        public string CharSpamSubstitution
        {
            get { return (string) this["CharSpamSubstitution"]; }
            set { this["CharSpamSubstitution"] = value; }
        }

        // Token: 0x17000564 RID: 1380
        // (get) Token: 0x06000F40 RID: 3904 RVA: 0x00053C8C File Offset: 0x00051E8C
        // (set) Token: 0x06000F41 RID: 3905 RVA: 0x00053CA0 File Offset: 0x00051EA0
        [Setting]
        [DefaultValue("%cDon't spam!")]
        public string CharSpamWarning
        {
            get { return (string) this["CharSpamWarning"]; }
            set { this["CharSpamWarning"] = value; }
        }

        // Token: 0x17000565 RID: 1381
        // (get) Token: 0x06000F42 RID: 3906 RVA: 0x00053CB0 File Offset: 0x00051EB0
        // (set) Token: 0x06000F43 RID: 3907 RVA: 0x00053CC4 File Offset: 0x00051EC4
        [Setting]
        [DefaultValue("DisplaySubstitution, SendWarning")]
        public ChatFilter.BadLanguageAction BadLanguageAction
        {
            get { return (ChatFilter.BadLanguageAction) this["BadLanguageAction"]; }
            set { this["BadLanguageAction"] = value; }
        }

        // Token: 0x17000566 RID: 1382
        // (get) Token: 0x06000F44 RID: 3908 RVA: 0x00053CD8 File Offset: 0x00051ED8
        // (set) Token: 0x06000F45 RID: 3909 RVA: 0x00053CEC File Offset: 0x00051EEC
        [DefaultValue("Normal")]
        [Setting]
        public ChatFilter.BadLanguageDetectionLevel BadLanguageDetectionLevel
        {
            get { return (ChatFilter.BadLanguageDetectionLevel) this["BadLanguageDetectionLevel"]; }
            set { this["BadLanguageDetectionLevel"] = value; }
        }

        // Token: 0x17000567 RID: 1383
        // (get) Token: 0x06000F46 RID: 3910 RVA: 0x00053D00 File Offset: 0x00051F00
        // (set) Token: 0x06000F47 RID: 3911 RVA: 0x00053D14 File Offset: 0x00051F14
        [Setting]
        [DefaultValue("%f[%cRemoved%f]")]
        public string BadLanguageSubstitution
        {
            get { return (string) this["BadLanguageSubstitution"]; }
            set { this["BadLanguageSubstitution"] = value; }
        }

        // Token: 0x17000568 RID: 1384
        // (get) Token: 0x06000F48 RID: 3912 RVA: 0x00053D24 File Offset: 0x00051F24
        // (set) Token: 0x06000F49 RID: 3913 RVA: 0x00053D38 File Offset: 0x00051F38
        [DefaultValue("%cDon't use bad language!")]
        [Setting]
        public string BadLanguageWarning
        {
            get { return (string) this["BadLanguageWarning"]; }
            set { this["BadLanguageWarning"] = value; }
        }

        // Token: 0x17000569 RID: 1385
        // (get) Token: 0x06000F4A RID: 3914 RVA: 0x00053D48 File Offset: 0x00051F48
        // (set) Token: 0x06000F4B RID: 3915 RVA: 0x00053D5C File Offset: 0x00051F5C
        [DefaultValue("&cKick: Don't use bad language or you will get banned!")]
        [Setting]
        public string BadLanguageKickMessage
        {
            get { return (string) this["BadLanguageKickMessage"]; }
            set { this["BadLanguageKickMessage"] = value; }
        }

        // Token: 0x1700056A RID: 1386
        // (get) Token: 0x06000F4C RID: 3916 RVA: 0x00053D6C File Offset: 0x00051F6C
        // (set) Token: 0x06000F4D RID: 3917 RVA: 0x00053D80 File Offset: 0x00051F80
        [DefaultValue("3")]
        [Setting]
        public int BadLanguageWarningLimit
        {
            get { return (int) this["BadLanguageWarningLimit"]; }
            set { this["BadLanguageWarningLimit"] = value; }
        }

        // Token: 0x1700056B RID: 1387
        // (get) Token: 0x06000F4E RID: 3918 RVA: 0x00053D94 File Offset: 0x00051F94
        // (set) Token: 0x06000F4F RID: 3919 RVA: 0x00053DA8 File Offset: 0x00051FA8
        [Setting]
        [DefaultValue("4")]
        public int CooldownMaxMessages
        {
            get { return (int) this["CooldownMaxMessages"]; }
            set { this["CooldownMaxMessages"] = value; }
        }

        // Token: 0x1700056C RID: 1388
        // (get) Token: 0x06000F50 RID: 3920 RVA: 0x00053DBC File Offset: 0x00051FBC
        // (set) Token: 0x06000F51 RID: 3921 RVA: 0x00053DD0 File Offset: 0x00051FD0
        [Setting]
        [DefaultValue("12")]
        public int CooldownMaxMessagesSeconds
        {
            get { return (int) this["CooldownMaxMessagesSeconds"]; }
            set { this["CooldownMaxMessagesSeconds"] = value; }
        }

        // Token: 0x1700056D RID: 1389
        // (get) Token: 0x06000F52 RID: 3922 RVA: 0x00053DE4 File Offset: 0x00051FE4
        // (set) Token: 0x06000F53 RID: 3923 RVA: 0x00053DF8 File Offset: 0x00051FF8
        [DefaultValue("2")]
        [Setting]
        public int CooldownMaxSameMessages
        {
            get { return (int) this["CooldownMaxSameMessages"]; }
            set { this["CooldownMaxSameMessages"] = value; }
        }

        // Token: 0x1700056E RID: 1390
        // (get) Token: 0x06000F54 RID: 3924 RVA: 0x00053E0C File Offset: 0x0005200C
        // (set) Token: 0x06000F55 RID: 3925 RVA: 0x00053E20 File Offset: 0x00052020
        [Setting]
        [DefaultValue("8")]
        public int CooldownMaxSameMessagesSeconds
        {
            get { return (int) this["CooldownMaxSameMessagesSeconds"]; }
            set { this["CooldownMaxSameMessagesSeconds"] = value; }
        }

        // Token: 0x1700056F RID: 1391
        // (get) Token: 0x06000F56 RID: 3926 RVA: 0x00053E34 File Offset: 0x00052034
        // (set) Token: 0x06000F57 RID: 3927 RVA: 0x00053E48 File Offset: 0x00052048
        [DefaultValue(
            "&cSlow down! Don't flood the chat with messages. Consider writing longer messages instead of many short.")]
        [Setting]
        public string CooldownMaxWarning
        {
            get { return (string) this["CooldownMaxWarning"]; }
            set { this["CooldownMaxWarning"] = value; }
        }

        // Token: 0x17000570 RID: 1392
        // (get) Token: 0x06000F58 RID: 3928 RVA: 0x00053E58 File Offset: 0x00052058
        // (set) Token: 0x06000F59 RID: 3929 RVA: 0x00053E6C File Offset: 0x0005206C
        [Setting]
        [DefaultValue("&cDon't spam!")]
        public string CooldownDuplicatesWarning
        {
            get { return (string) this["CooldownDuplicatesWarning"]; }
            set { this["CooldownDuplicatesWarning"] = value; }
        }

        // Token: 0x17000571 RID: 1393
        // (get) Token: 0x06000F5A RID: 3930 RVA: 0x00053E7C File Offset: 0x0005207C
        // (set) Token: 0x06000F5B RID: 3931 RVA: 0x00053E90 File Offset: 0x00052090
        [DefaultValue("False")]
        [Setting]
        public bool CooldownTempMute
        {
            get { return (bool) this["CooldownTempMute"]; }
            set { this["CooldownTempMute"] = value; }
        }
    }
}