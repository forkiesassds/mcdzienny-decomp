using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using MCDzienny.Settings;

namespace MCDzienny.Misc
{
    // Token: 0x020001C5 RID: 453
    public partial class PortIsOpen : Form
    {
        // Token: 0x06000CB2 RID: 3250 RVA: 0x0004942C File Offset: 0x0004762C
        public PortIsOpen()
        {
            InitializeComponent();
            var executingAssembly = Assembly.GetExecutingAssembly();
            var manifestResourceStream = executingAssembly.GetManifestResourceStream("MCDzienny.icon_ok.png");
            pictureBox1.Image = new Bitmap(manifestResourceStream);
        }

        // Token: 0x06000CB3 RID: 3251 RVA: 0x00049468 File Offset: 0x00047668
        public static void ShowBox()
        {
            new PortIsOpen
            {
                label1 =
                {
                    Text = "Port " + Server.port + " is open. It means that other people can connect to your server."
                },
                StartPosition = FormStartPosition.CenterScreen
            }.ShowDialog();
        }

        // Token: 0x06000CB4 RID: 3252 RVA: 0x000494B0 File Offset: 0x000476B0
        private void okButton_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked) GeneralSettings.All.CheckPortOnStart = false;
            Close();
        }
    }
}