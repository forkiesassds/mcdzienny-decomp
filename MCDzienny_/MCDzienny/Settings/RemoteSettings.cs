using MCDzienny.SettingsFrame;

namespace MCDzienny.Settings
{
    [SettingsPath("properties/remote.properties")]
    sealed class RemoteSettings : SettingsFrame.SettingsFrame
    {
        static readonly RemoteSettings defaultInstance = new RemoteSettings();

        public static RemoteSettings All { get { return defaultInstance; } }

        [DefaultValue("33434")]
        [Setting]
        public int Port { get { return (int)this["Port"]; } set { this["Port"] = value; } }

        [DefaultValue("True")]
        [Setting]
        public bool AllowRemoteAccess { get { return (bool)this["AllowRemoteAccess"]; } set { this["AllowRemoteAccess"] = value; } }

        [DefaultValue("True")]
        [Setting]
        public bool ShowInBrowser { get { return (bool)this["ShowInBrowser"]; } set { this["ShowInBrowser"] = value; } }
    }
}