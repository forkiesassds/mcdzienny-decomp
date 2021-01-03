using System;
using System.Drawing;
using System.Windows.Forms;

namespace MCDzienny.Gui
{
    // Token: 0x0200009A RID: 154
    public partial class Tools : Form
    {
        // Token: 0x0600041A RID: 1050 RVA: 0x000172C8 File Offset: 0x000154C8
        public Tools()
        {
            InitializeComponent();
            UpdateControls();
        }

        // Token: 0x0600041B RID: 1051 RVA: 0x000172DC File Offset: 0x000154DC
        private void UpdateControls()
        {
            comboBoxDepth.SelectedIndex = 2;
            comboBoxWidth.SelectedIndex = 2;
            comboBoxHeight.SelectedIndex = 2;
        }

        // Token: 0x0600041C RID: 1052 RVA: 0x00017304 File Offset: 0x00015504
        public void ShowAt(Point location)
        {
            StartPosition = FormStartPosition.Manual;
            var x = location.X - Width / 2;
            var y = location.Y - Height / 2;
            Location = new Point(x, y);
            base.Show();
        }

        // Token: 0x0600041D RID: 1053 RVA: 0x00017350 File Offset: 0x00015550
        private void Recalcuate()
        {
            if (comboBoxHeight.SelectedIndex == -1 || comboBoxDepth.SelectedIndex == -1 ||
                comboBoxWidth.SelectedIndex == -1) return;
            int num;
            int.TryParse(comboBoxWidth.Items[comboBoxWidth.SelectedIndex].ToString(), out num);
            int num2;
            int.TryParse(comboBoxHeight.Items[comboBoxHeight.SelectedIndex].ToString(), out num2);
            int num3;
            int.TryParse(comboBoxDepth.Items[comboBoxDepth.SelectedIndex].ToString(), out num3);
            var value = num * (long) num2 * num3;
            var num4 = value / 1024m / 1024m;
            if (num4 > 16m)
                textRamRequired.Text = num4.ToString("##,#");
            else if (num4 > 0.01m)
                textRamRequired.Text = num4.ToString("0.##");
            else
                textRamRequired.Text = num4.ToString("0.####");
            var d = 1024m / num4;
            if (d >= 1m)
            {
                textMaxMaps.Text = d.ToString("##,#");
                return;
            }

            textMaxMaps.Text = d.ToString("0.####");
        }

        // Token: 0x0600041E RID: 1054 RVA: 0x000174EC File Offset: 0x000156EC
        private void comboBoxWidth_SelectedIndexChanged(object sender, EventArgs e)
        {
            Recalcuate();
        }

        // Token: 0x0600041F RID: 1055 RVA: 0x000174F4 File Offset: 0x000156F4
        private void comboBoxHeight_SelectedIndexChanged(object sender, EventArgs e)
        {
            Recalcuate();
        }

        // Token: 0x06000420 RID: 1056 RVA: 0x000174FC File Offset: 0x000156FC
        private void comboBoxDepth_SelectedIndexChanged(object sender, EventArgs e)
        {
            Recalcuate();
        }

        // Token: 0x06000423 RID: 1059 RVA: 0x00018134 File Offset: 0x00016334
        private void Tools_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }
    }
}