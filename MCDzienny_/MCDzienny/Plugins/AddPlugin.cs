using System.Windows.Forms;

namespace MCDzienny.Plugins
{
    // Token: 0x020001EB RID: 491
    public class AddPlugin : Plugin
    {
        // Token: 0x04000727 RID: 1831
        private readonly string myAuthor = "Dzienny";

        // Token: 0x04000726 RID: 1830
        private readonly string myDescription = "A plugin that lets you add more plugins.";

        // Token: 0x04000729 RID: 1833
        private readonly UserControl myMainInterface = new PluginControlAddPlugin();

        // Token: 0x04000725 RID: 1829
        private readonly string myName = "Add Plugin";

        // Token: 0x04000728 RID: 1832
        private readonly string myVersion = "1.0";

        // Token: 0x1700050D RID: 1293
        // (get) Token: 0x06000D94 RID: 3476 RVA: 0x0004D340 File Offset: 0x0004B540
        public override string Description
        {
            get { return myDescription; }
        }

        // Token: 0x1700050E RID: 1294
        // (get) Token: 0x06000D95 RID: 3477 RVA: 0x0004D348 File Offset: 0x0004B548
        public override string Author
        {
            get { return myAuthor; }
        }

        // Token: 0x1700050F RID: 1295
        // (get) Token: 0x06000D96 RID: 3478 RVA: 0x0004D350 File Offset: 0x0004B550
        public override string Name
        {
            get { return myName; }
        }

        // Token: 0x17000510 RID: 1296
        // (get) Token: 0x06000D97 RID: 3479 RVA: 0x0004D358 File Offset: 0x0004B558
        public override UserControl MainInterface
        {
            get { return myMainInterface; }
        }

        // Token: 0x17000511 RID: 1297
        // (get) Token: 0x06000D98 RID: 3480 RVA: 0x0004D360 File Offset: 0x0004B560
        public override string Version
        {
            get { return myVersion; }
        }

        // Token: 0x17000512 RID: 1298
        // (get) Token: 0x06000D99 RID: 3481 RVA: 0x0004D368 File Offset: 0x0004B568
        public override int VersionNumber
        {
            get { return 1; }
        }

        // Token: 0x06000D9A RID: 3482 RVA: 0x0004D36C File Offset: 0x0004B56C
        public override void Initialize()
        {
        }

        // Token: 0x06000D9B RID: 3483 RVA: 0x0004D370 File Offset: 0x0004B570
        public override void Terminate()
        {
        }
    }
}