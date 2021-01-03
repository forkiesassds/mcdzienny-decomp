using System.Windows.Forms;

namespace MCDzienny.Plugins
{
    // Token: 0x020001E2 RID: 482
    public class Example : Plugin
    {
        // Token: 0x0400070A RID: 1802
        private readonly string author = "Dzienny";

        // Token: 0x04000709 RID: 1801
        private readonly string description = "It doesn't do anything.";

        // Token: 0x0400070D RID: 1805
        private readonly UserControl gui = new GuiExample();

        // Token: 0x04000708 RID: 1800
        private readonly string name = "Example";

        // Token: 0x0400070B RID: 1803
        private readonly string version = "1.0";

        // Token: 0x0400070C RID: 1804
        private readonly int versionNumber = 1;

        // Token: 0x17000501 RID: 1281
        // (get) Token: 0x06000D6B RID: 3435 RVA: 0x0004C708 File Offset: 0x0004A908
        public override string Description
        {
            get { return description; }
        }

        // Token: 0x17000502 RID: 1282
        // (get) Token: 0x06000D6C RID: 3436 RVA: 0x0004C710 File Offset: 0x0004A910
        public override string Author
        {
            get { return author; }
        }

        // Token: 0x17000503 RID: 1283
        // (get) Token: 0x06000D6D RID: 3437 RVA: 0x0004C718 File Offset: 0x0004A918
        public override string Name
        {
            get { return name; }
        }

        // Token: 0x17000504 RID: 1284
        // (get) Token: 0x06000D6E RID: 3438 RVA: 0x0004C720 File Offset: 0x0004A920
        public override UserControl MainInterface
        {
            get { return gui; }
        }

        // Token: 0x17000505 RID: 1285
        // (get) Token: 0x06000D6F RID: 3439 RVA: 0x0004C728 File Offset: 0x0004A928
        public override string Version
        {
            get { return version; }
        }

        // Token: 0x17000506 RID: 1286
        // (get) Token: 0x06000D70 RID: 3440 RVA: 0x0004C730 File Offset: 0x0004A930
        public override int VersionNumber
        {
            get { return versionNumber; }
        }

        // Token: 0x06000D71 RID: 3441 RVA: 0x0004C738 File Offset: 0x0004A938
        public override void Initialize()
        {
        }

        // Token: 0x06000D72 RID: 3442 RVA: 0x0004C73C File Offset: 0x0004A93C
        public override void Terminate()
        {
        }
    }
}