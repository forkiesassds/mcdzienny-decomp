using MCDzienny.SettingsFrame;

namespace MCDzienny.Settings
{
    [SettingsPath("properties/gui.properties")]
    sealed class GuiSettings : SettingsFrame.SettingsFrame
    {
        static readonly GuiSettings defaultInstance = new GuiSettings();

        public static GuiSettings All { get { return defaultInstance; } }

        [Setting]
        [DefaultValue("396")]
        public int MainSplitter3Distance { get { return (int)this["MainSplitter3Distance"]; } set { this["MainSplitter3Distance"] = value; } }

        [DefaultValue("221")]
        [Setting]
        public int MainSplitter2Distance { get { return (int)this["MainSplitter2Distance"]; } set { this["MainSplitter2Distance"] = value; } }

        [DefaultValue("246")]
        [Setting]
        public int MainSplitter4Distance { get { return (int)this["MainSplitter4Distance"]; } set { this["MainSplitter4Distance"] = value; } }

        [DefaultValue("440")]
        [Setting]
        public int MainSplitter5Distance { get { return (int)this["MainSplitter5Distance"]; } set { this["MainSplitter5Distance"] = value; } }

        [Setting]
        [DefaultValue("722")]
        public int WindowWidth { get { return (int)this["WindowWidth"]; } set { this["WindowWidth"] = value; } }

        [Setting]
        [DefaultValue("560")]
        public int WindowHeight { get { return (int)this["WindowHeight"]; } set { this["WindowHeight"] = value; } }
    }
}