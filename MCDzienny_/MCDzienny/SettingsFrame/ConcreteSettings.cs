using System.ComponentModel;

namespace MCDzienny.SettingsFrame
{
    // Token: 0x0200021F RID: 543
    [SettingsPath("properties/test2.properties")]
    internal sealed class ConcreteSettings : SettingsFrame
    {
        // Token: 0x04000873 RID: 2163
        private static readonly ConcreteSettings defaultInstance = new ConcreteSettings();

        // Token: 0x17000548 RID: 1352
        // (get) Token: 0x06000EFD RID: 3837 RVA: 0x00053338 File Offset: 0x00051538
        public static ConcreteSettings All
        {
            get { return defaultInstance; }
        }

        // Token: 0x17000549 RID: 1353
        // (get) Token: 0x06000EFE RID: 3838 RVA: 0x00053340 File Offset: 0x00051540
        // (set) Token: 0x06000EFF RID: 3839 RVA: 0x00053354 File Offset: 0x00051554
        [Setting]
        [Description("TESTS new class.")]
        [Category("New")]
        [DefaultValue("99")]
        public int Test
        {
            get { return (int) this["Test"]; }
            set { this["Test"] = value; }
        }

        // Token: 0x1700054A RID: 1354
        // (get) Token: 0x06000F00 RID: 3840 RVA: 0x00053368 File Offset: 0x00051568
        // (set) Token: 0x06000F01 RID: 3841 RVA: 0x0005337C File Offset: 0x0005157C
        [Setting]
        [Category("New")]
        [Description("TESTS new class.")]
        [DefaultValue("BasedOnAir")]
        public ScoreSystem Test2
        {
            get { return (ScoreSystem) this["Test2"]; }
            set { this["Test2"] = value; }
        }

        // Token: 0x1700054B RID: 1355
        // (get) Token: 0x06000F02 RID: 3842 RVA: 0x00053390 File Offset: 0x00051590
        // (set) Token: 0x06000F03 RID: 3843 RVA: 0x000533A4 File Offset: 0x000515A4
        [Description("TESTS new class.")]
        [Category("New")]
        [Setting]
        public int Test3
        {
            get { return (int) this["Test3"]; }
            set { this["Test3"] = value; }
        }
    }
}