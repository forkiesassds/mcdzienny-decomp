using MCDzienny.SettingsFrame;

namespace MCDzienny.Settings
{
    [SettingsPath("properties/chatfilter.properties")]
    sealed class ChatFilterSettings : SettingsFrame.SettingsFrame
    {
        static readonly ChatFilterSettings defaultInstance = new ChatFilterSettings();

        public static ChatFilterSettings All { get { return defaultInstance; } }

        [Setting]
        [DefaultValue("True")]
        public bool RemoveCaps { get { return (bool)this["RemoveCaps"]; } set { this["RemoveCaps"] = value; } }

        [DefaultValue("True")]
        [Setting]
        public bool ShortenRepetitions { get { return (bool)this["ShortenRepetitions"]; } set { this["ShortenRepetitions"] = value; } }

        [Setting]
        [DefaultValue("True")]
        public bool RemoveBadWords { get { return (bool)this["RemoveBadWords"]; } set { this["RemoveBadWords"] = value; } }

        [Setting]
        [DefaultValue("True")]
        public bool MessagesCooldown { get { return (bool)this["MessagesCooldown"]; } set { this["MessagesCooldown"] = value; } }

        [Setting]
        [DefaultValue("False")]
        public bool GuiShowAdvancedSettings { get { return (bool)this["GuiShowAdvancedSettings"]; } set { this["GuiShowAdvancedSettings"] = value; } }

        [Setting]
        [DefaultValue("4")]
        public int MaxCaps { get { return (int)this["MaxCaps"]; } set { this["MaxCaps"] = value; } }

        [Setting]
        [DefaultValue("SendWarning")]
        public ChatFilter.CharacterSpamAction CharSpamAction
        {
            get { return (ChatFilter.CharacterSpamAction)this["CharSpamAction"]; }
            set { this["CharSpamAction"] = value; }
        }

        [DefaultValue("3")]
        [Setting]
        public int CharSpamMaxChars { get { return (int)this["CharSpamMaxChars"]; } set { this["CharSpamMaxChars"] = value; } }

        [DefaultValue("1")]
        [Setting]
        public int CharSpamMaxIllegalGroups { get { return (int)this["CharSpamMaxIllegalGroups"]; } set { this["CharSpamMaxIllegalGroups"] = value; } }

        [Setting]
        [DefaultValue("%f[%cSpam%f]")]
        public string CharSpamSubstitution { get { return (string)this["CharSpamSubstitution"]; } set { this["CharSpamSubstitution"] = value; } }

        [Setting]
        [DefaultValue("%cDon't spam!")]
        public string CharSpamWarning { get { return (string)this["CharSpamWarning"]; } set { this["CharSpamWarning"] = value; } }

        [Setting]
        [DefaultValue("DisplaySubstitution, SendWarning")]
        public ChatFilter.BadLanguageAction BadLanguageAction
        {
            get { return (ChatFilter.BadLanguageAction)this["BadLanguageAction"]; }
            set { this["BadLanguageAction"] = value; }
        }

        [DefaultValue("Normal")]
        [Setting]
        public ChatFilter.BadLanguageDetectionLevel BadLanguageDetectionLevel
        {
            get { return (ChatFilter.BadLanguageDetectionLevel)this["BadLanguageDetectionLevel"]; }
            set { this["BadLanguageDetectionLevel"] = value; }
        }

        [Setting]
        [DefaultValue("%f[%cRemoved%f]")]
        public string BadLanguageSubstitution { get { return (string)this["BadLanguageSubstitution"]; } set { this["BadLanguageSubstitution"] = value; } }

        [DefaultValue("%cDon't use bad language!")]
        [Setting]
        public string BadLanguageWarning { get { return (string)this["BadLanguageWarning"]; } set { this["BadLanguageWarning"] = value; } }

        [DefaultValue("&cKick: Don't use bad language or you will get banned!")]
        [Setting]
        public string BadLanguageKickMessage { get { return (string)this["BadLanguageKickMessage"]; } set { this["BadLanguageKickMessage"] = value; } }

        [DefaultValue("3")]
        [Setting]
        public int BadLanguageWarningLimit { get { return (int)this["BadLanguageWarningLimit"]; } set { this["BadLanguageWarningLimit"] = value; } }

        [Setting]
        [DefaultValue("4")]
        public int CooldownMaxMessages { get { return (int)this["CooldownMaxMessages"]; } set { this["CooldownMaxMessages"] = value; } }

        [Setting]
        [DefaultValue("12")]
        public int CooldownMaxMessagesSeconds { get { return (int)this["CooldownMaxMessagesSeconds"]; } set { this["CooldownMaxMessagesSeconds"] = value; } }

        [DefaultValue("2")]
        [Setting]
        public int CooldownMaxSameMessages { get { return (int)this["CooldownMaxSameMessages"]; } set { this["CooldownMaxSameMessages"] = value; } }

        [Setting]
        [DefaultValue("8")]
        public int CooldownMaxSameMessagesSeconds { get { return (int)this["CooldownMaxSameMessagesSeconds"]; } set { this["CooldownMaxSameMessagesSeconds"] = value; } }

        [DefaultValue("&cSlow down! Don't flood the chat with messages. Consider writing longer messages instead of many short.")]
        [Setting]
        public string CooldownMaxWarning { get { return (string)this["CooldownMaxWarning"]; } set { this["CooldownMaxWarning"] = value; } }

        [Setting]
        [DefaultValue("&cDon't spam!")]
        public string CooldownDuplicatesWarning { get { return (string)this["CooldownDuplicatesWarning"]; } set { this["CooldownDuplicatesWarning"] = value; } }

        [DefaultValue("False")]
        [Setting]
        public bool CooldownTempMute { get { return (bool)this["CooldownTempMute"]; } set { this["CooldownTempMute"] = value; } }
    }
}