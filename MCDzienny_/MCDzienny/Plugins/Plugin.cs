using System.Windows.Forms;

namespace MCDzienny.Plugins
{
    // Token: 0x02000042 RID: 66
    public abstract class Plugin
    {
        // Token: 0x1700005F RID: 95
        // (get) Token: 0x0600016C RID: 364
        public abstract string Name { get; }

        // Token: 0x17000060 RID: 96
        // (get) Token: 0x0600016D RID: 365
        public abstract string Description { get; }

        // Token: 0x17000061 RID: 97
        // (get) Token: 0x0600016E RID: 366
        public abstract string Author { get; }

        // Token: 0x17000062 RID: 98
        // (get) Token: 0x0600016F RID: 367
        public abstract string Version { get; }

        // Token: 0x17000063 RID: 99
        // (get) Token: 0x06000170 RID: 368
        public abstract int VersionNumber { get; }

        // Token: 0x17000064 RID: 100
        // (get) Token: 0x06000171 RID: 369
        public abstract UserControl MainInterface { get; }

        // Token: 0x06000172 RID: 370
        public abstract void Initialize();

        // Token: 0x06000173 RID: 371
        public abstract void Terminate();
    }
}