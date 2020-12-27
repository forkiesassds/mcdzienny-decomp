using System;
using System.Drawing;
using System.Windows.Forms;

namespace MCDzienny.Gui.Util
{
    // Token: 0x0200009B RID: 155
    internal class ToggleButton : CheckBox
    {
        // Token: 0x06000424 RID: 1060 RVA: 0x00018144 File Offset: 0x00016344
        public ToggleButton()
        {
            Appearance = Appearance.Button;
            FlatStyle = FlatStyle.Flat;
        }

        // Token: 0x06000425 RID: 1061 RVA: 0x0001815C File Offset: 0x0001635C
        protected override void OnCheckedChanged(EventArgs e)
        {
            base.OnCheckedChanged(e);
            if (Checked)
            {
                BackColor = Color.Green;
                return;
            }

            BackColor = Color.LightGray;
        }
    }
}