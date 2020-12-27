using System;
using System.Windows.Forms;
using MCDzienny.Settings;

namespace MCDzienny.Gui
{
    // Token: 0x02000155 RID: 341
    public partial class LavaProperties : Form
    {
        // Token: 0x04000470 RID: 1136
        public static LavaProperties thisWindow;

        // Token: 0x060009DB RID: 2523 RVA: 0x000349C8 File Offset: 0x00032BC8
        public LavaProperties()
        {
            thisWindow = this;
            InitializeComponent();
        }

        // Token: 0x060009DC RID: 2524 RVA: 0x000349DC File Offset: 0x00032BDC
        private void LavaProperties_Unload(object sender, EventArgs e)
        {
            LavaSettings.All.Save();
            Window.lavaSettingsPrevLoaded = false;
        }

        // Token: 0x060009DD RID: 2525 RVA: 0x000349F0 File Offset: 0x00032BF0
        private void LavaProperties_Load(object sender, EventArgs e)
        {
        }

        // Token: 0x060009DE RID: 2526 RVA: 0x000349F4 File Offset: 0x00032BF4
        private void propertyGrid1_Click(object sender, EventArgs e)
        {
        }

        // Token: 0x060009DF RID: 2527 RVA: 0x000349F8 File Offset: 0x00032BF8
        private void tabPage2_Click(object sender, EventArgs e)
        {
        }

        // Token: 0x060009E0 RID: 2528 RVA: 0x000349FC File Offset: 0x00032BFC
        private void timePanel_Paint(object sender, PaintEventArgs e)
        {
        }

        // Token: 0x02000156 RID: 342
        private class PropertyGrd : PropertyGrid
        {
        }
    }
}