using System.ComponentModel;

namespace MCDzienny.SettingsFrame
{
    [SettingsPath("properties/test2.properties")]
    sealed class ConcreteSettings : SettingsFrame
    {
        static readonly ConcreteSettings defaultInstance = new ConcreteSettings();

        public static ConcreteSettings All { get { return defaultInstance; } }

        [Setting]
        [Description("TESTS new class.")]
        [Category("New")]
        [DefaultValue("99")]
        public int Test { get { return (int)this["Test"]; } set { this["Test"] = value; } }

        [Setting]
        [Category("New")]
        [Description("TESTS new class.")]
        [DefaultValue("BasedOnAir")]
        public ScoreSystem Test2 { get { return (ScoreSystem)this["Test2"]; } set { this["Test2"] = value; } }

        [Description("TESTS new class.")]
        [Category("New")]
        [Setting]
        public int Test3 { get { return (int)this["Test3"]; } set { this["Test3"] = value; } }
    }
}