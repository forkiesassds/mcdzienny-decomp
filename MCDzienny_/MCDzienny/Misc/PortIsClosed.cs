using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace MCDzienny.Misc
{
    // Token: 0x020001C4 RID: 452
    public partial class PortIsClosed : Form
    {
        // Token: 0x06000CAD RID: 3245 RVA: 0x000490FC File Offset: 0x000472FC
        public PortIsClosed()
        {
            InitializeComponent();
            var executingAssembly = Assembly.GetExecutingAssembly();
            var manifestResourceStream = executingAssembly.GetManifestResourceStream("MCDzienny.icon_wrong.png");
            pictureBox1.Image = new Bitmap(manifestResourceStream);
        }

        // Token: 0x06000CAE RID: 3246 RVA: 0x00049138 File Offset: 0x00047338
        public static void ShowBox()
        {
            new PortIsClosed
            {
                label1 =
                {
                    Text = "Port " + Server.port +
                           " is not accessible. No one can connect to your server from the internet. You have to port forward in order to let people join. For help visit: www.mcdzienny.cba.pl"
                },
                StartPosition = FormStartPosition.CenterScreen
            }.ShowDialog();
        }

        // Token: 0x06000CAF RID: 3247 RVA: 0x00049180 File Offset: 0x00047380
        private void okButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}