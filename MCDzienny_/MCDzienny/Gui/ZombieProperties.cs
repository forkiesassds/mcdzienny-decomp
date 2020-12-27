using System;
using System.Windows.Forms;

namespace MCDzienny.Gui
{
    // Token: 0x02000157 RID: 343
    public partial class ZombieProperties : Form
    {
        // Token: 0x060009E5 RID: 2533 RVA: 0x00034AB8 File Offset: 0x00032CB8
        public ZombieProperties()
        {
            InitializeComponent();
        }

        // Token: 0x060009E6 RID: 2534 RVA: 0x00034AC8 File Offset: 0x00032CC8
        private void ZombieProperties_FormClosed(object sender, EventArgs e)
        {
            Window.zombieSettingsPrevLoaded = false;
        }
    }
}