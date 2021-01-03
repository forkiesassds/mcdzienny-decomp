using System.Windows.Forms;

namespace MCDzienny.Plugins
{
    // Token: 0x020001F0 RID: 496
    public class RemovePlugin : Plugin
    {
        // Token: 0x04000734 RID: 1844
        private readonly string myAuthor = "Dzienny";

        // Token: 0x04000733 RID: 1843
        private readonly string myDescription = "A plugin that allows you to remove plugins.";

        // Token: 0x04000737 RID: 1847
        private readonly UserControl myMainInterface = new PluginControlRemovePlugin();

        // Token: 0x04000732 RID: 1842
        private readonly string myName = "Remove Plugin";

        // Token: 0x04000735 RID: 1845
        private readonly string myVersion = "1.0";

        // Token: 0x04000736 RID: 1846
        private readonly int versionNumber = 1;

        // Token: 0x17000514 RID: 1300
        // (get) Token: 0x06000DB2 RID: 3506 RVA: 0x0004DB08 File Offset: 0x0004BD08
        public override string Description
        {
            get { return myDescription; }
        }

        // Token: 0x17000515 RID: 1301
        // (get) Token: 0x06000DB3 RID: 3507 RVA: 0x0004DB10 File Offset: 0x0004BD10
        public override string Author
        {
            get { return myAuthor; }
        }

        // Token: 0x17000516 RID: 1302
        // (get) Token: 0x06000DB4 RID: 3508 RVA: 0x0004DB18 File Offset: 0x0004BD18
        public override string Name
        {
            get { return myName; }
        }

        // Token: 0x17000517 RID: 1303
        // (get) Token: 0x06000DB5 RID: 3509 RVA: 0x0004DB20 File Offset: 0x0004BD20
        public override UserControl MainInterface
        {
            get { return myMainInterface; }
        }

        // Token: 0x17000518 RID: 1304
        // (get) Token: 0x06000DB6 RID: 3510 RVA: 0x0004DB28 File Offset: 0x0004BD28
        public override string Version
        {
            get { return myVersion; }
        }

        // Token: 0x17000519 RID: 1305
        // (get) Token: 0x06000DB7 RID: 3511 RVA: 0x0004DB30 File Offset: 0x0004BD30
        public override int VersionNumber
        {
            get { return versionNumber; }
        }

        // Token: 0x06000DB9 RID: 3513 RVA: 0x0004DB8C File Offset: 0x0004BD8C
        public override void Initialize()
        {
        }

        // Token: 0x06000DBA RID: 3514 RVA: 0x0004DB90 File Offset: 0x0004BD90
        public override void Terminate()
        {
        }
    }
}