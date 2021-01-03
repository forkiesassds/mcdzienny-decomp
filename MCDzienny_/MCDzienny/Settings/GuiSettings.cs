using MCDzienny.SettingsFrame;

namespace MCDzienny.Settings
{
    // Token: 0x0200022A RID: 554
    [SettingsPath("properties/gui.properties")]
    internal sealed class GuiSettings : SettingsFrame.SettingsFrame
    {
        // Token: 0x04000883 RID: 2179
        private static readonly GuiSettings defaultInstance = new GuiSettings();

        // Token: 0x17000591 RID: 1425
        // (get) Token: 0x06000F9D RID: 3997 RVA: 0x00054374 File Offset: 0x00052574
        public static GuiSettings All
        {
            get { return defaultInstance; }
        }

        // Token: 0x17000592 RID: 1426
        // (get) Token: 0x06000F9E RID: 3998 RVA: 0x0005437C File Offset: 0x0005257C
        // (set) Token: 0x06000F9F RID: 3999 RVA: 0x00054390 File Offset: 0x00052590
        [Setting]
        [DefaultValue("396")]
        public int MainSplitter3Distance
        {
            get { return (int) this["MainSplitter3Distance"]; }
            set { this["MainSplitter3Distance"] = value; }
        }

        // Token: 0x17000593 RID: 1427
        // (get) Token: 0x06000FA0 RID: 4000 RVA: 0x000543A4 File Offset: 0x000525A4
        // (set) Token: 0x06000FA1 RID: 4001 RVA: 0x000543B8 File Offset: 0x000525B8
        [DefaultValue("221")]
        [Setting]
        public int MainSplitter2Distance
        {
            get { return (int) this["MainSplitter2Distance"]; }
            set { this["MainSplitter2Distance"] = value; }
        }

        // Token: 0x17000594 RID: 1428
        // (get) Token: 0x06000FA2 RID: 4002 RVA: 0x000543CC File Offset: 0x000525CC
        // (set) Token: 0x06000FA3 RID: 4003 RVA: 0x000543E0 File Offset: 0x000525E0
        [DefaultValue("246")]
        [Setting]
        public int MainSplitter4Distance
        {
            get { return (int) this["MainSplitter4Distance"]; }
            set { this["MainSplitter4Distance"] = value; }
        }

        // Token: 0x17000595 RID: 1429
        // (get) Token: 0x06000FA4 RID: 4004 RVA: 0x000543F4 File Offset: 0x000525F4
        // (set) Token: 0x06000FA5 RID: 4005 RVA: 0x00054408 File Offset: 0x00052608
        [DefaultValue("440")]
        [Setting]
        public int MainSplitter5Distance
        {
            get { return (int) this["MainSplitter5Distance"]; }
            set { this["MainSplitter5Distance"] = value; }
        }

        // Token: 0x17000596 RID: 1430
        // (get) Token: 0x06000FA6 RID: 4006 RVA: 0x0005441C File Offset: 0x0005261C
        // (set) Token: 0x06000FA7 RID: 4007 RVA: 0x00054430 File Offset: 0x00052630
        [Setting]
        [DefaultValue("722")]
        public int WindowWidth
        {
            get { return (int) this["WindowWidth"]; }
            set { this["WindowWidth"] = value; }
        }

        // Token: 0x17000597 RID: 1431
        // (get) Token: 0x06000FA8 RID: 4008 RVA: 0x00054444 File Offset: 0x00052644
        // (set) Token: 0x06000FA9 RID: 4009 RVA: 0x00054458 File Offset: 0x00052658
        [Setting]
        [DefaultValue("560")]
        public int WindowHeight
        {
            get { return (int) this["WindowHeight"]; }
            set { this["WindowHeight"] = value; }
        }
    }
}