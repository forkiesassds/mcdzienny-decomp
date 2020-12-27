using System.Windows.Forms;

namespace MCDzienny.Plugins
{
    // Token: 0x020001F1 RID: 497
    public class Plgn : Plugin
    {
        // Token: 0x04000739 RID: 1849
        private readonly mainGUI gui = new mainGUI();

        // Token: 0x04000738 RID: 1848
        private readonly versionInfo info = new versionInfo("Repeater",
            "A simple text repeater for all your daily messages.(test plugin)", "joppiesaus", "1.0", 1);

        // Token: 0x1700051A RID: 1306
        // (get) Token: 0x06000DBB RID: 3515 RVA: 0x0004DB94 File Offset: 0x0004BD94
        public override string Description
        {
            get { return info.descr; }
        }

        // Token: 0x1700051B RID: 1307
        // (get) Token: 0x06000DBC RID: 3516 RVA: 0x0004DBA4 File Offset: 0x0004BDA4
        public override string Author
        {
            get { return info.auth; }
        }

        // Token: 0x1700051C RID: 1308
        // (get) Token: 0x06000DBD RID: 3517 RVA: 0x0004DBB4 File Offset: 0x0004BDB4
        public override string Name
        {
            get { return info.name; }
        }

        // Token: 0x1700051D RID: 1309
        // (get) Token: 0x06000DBE RID: 3518 RVA: 0x0004DBC4 File Offset: 0x0004BDC4
        public override UserControl MainInterface
        {
            get { return gui; }
        }

        // Token: 0x1700051E RID: 1310
        // (get) Token: 0x06000DBF RID: 3519 RVA: 0x0004DBCC File Offset: 0x0004BDCC
        public override string Version
        {
            get { return info.ver; }
        }

        // Token: 0x1700051F RID: 1311
        // (get) Token: 0x06000DC0 RID: 3520 RVA: 0x0004DBDC File Offset: 0x0004BDDC
        public override int VersionNumber
        {
            get { return info.verNumber; }
        }

        // Token: 0x06000DC1 RID: 3521 RVA: 0x0004DBEC File Offset: 0x0004BDEC
        public override void Initialize()
        {
            var mg = gui;
            mg.timer.Start();
            mg.msgTextBox.Invoke(new MethodInvoker(delegate { mg.msgTextBox.Text = "My text"; }));
        }

        // Token: 0x06000DC2 RID: 3522 RVA: 0x0004DC38 File Offset: 0x0004BE38
        public override void Terminate()
        {
        }
    }
}