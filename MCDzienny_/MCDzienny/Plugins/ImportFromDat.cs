using System.Windows.Forms;

namespace MCDzienny.Plugins
{
    // Token: 0x020001E7 RID: 487
    public class ImportFromDat : Plugin
    {
        // Token: 0x0400071C RID: 1820
        private readonly string author = "Dzienny";

        // Token: 0x0400071B RID: 1819
        private readonly string description = "Imports a map that is a dat file and saves it as MCDzienny map type.";

        // Token: 0x0400071F RID: 1823
        private readonly UserControl gui = new GuiImportFromDat();

        // Token: 0x0400071A RID: 1818
        private readonly string name = "Import From Dat";

        // Token: 0x0400071D RID: 1821
        private readonly string version = "1.0";

        // Token: 0x0400071E RID: 1822
        private readonly int versionNumber = 1;

        // Token: 0x17000507 RID: 1287
        // (get) Token: 0x06000D80 RID: 3456 RVA: 0x0004CEB0 File Offset: 0x0004B0B0
        public override string Description
        {
            get { return description; }
        }

        // Token: 0x17000508 RID: 1288
        // (get) Token: 0x06000D81 RID: 3457 RVA: 0x0004CEB8 File Offset: 0x0004B0B8
        public override string Author
        {
            get { return author; }
        }

        // Token: 0x17000509 RID: 1289
        // (get) Token: 0x06000D82 RID: 3458 RVA: 0x0004CEC0 File Offset: 0x0004B0C0
        public override string Name
        {
            get { return name; }
        }

        // Token: 0x1700050A RID: 1290
        // (get) Token: 0x06000D83 RID: 3459 RVA: 0x0004CEC8 File Offset: 0x0004B0C8
        public override UserControl MainInterface
        {
            get { return gui; }
        }

        // Token: 0x1700050B RID: 1291
        // (get) Token: 0x06000D84 RID: 3460 RVA: 0x0004CED0 File Offset: 0x0004B0D0
        public override string Version
        {
            get { return version; }
        }

        // Token: 0x1700050C RID: 1292
        // (get) Token: 0x06000D85 RID: 3461 RVA: 0x0004CED8 File Offset: 0x0004B0D8
        public override int VersionNumber
        {
            get { return versionNumber; }
        }

        // Token: 0x06000D86 RID: 3462 RVA: 0x0004CEE0 File Offset: 0x0004B0E0
        public override void Initialize()
        {
        }

        // Token: 0x06000D87 RID: 3463 RVA: 0x0004CEE4 File Offset: 0x0004B0E4
        public override void Terminate()
        {
        }
    }
}