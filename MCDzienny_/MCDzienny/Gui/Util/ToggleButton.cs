using System;
using System.Drawing;
using System.Windows.Forms;

namespace MCDzienny.Gui.Util
{
    class ToggleButton : CheckBox
    {
        public ToggleButton()
        {
            Appearance = (Appearance)1;
            FlatStyle = 0;
        }

        protected override void OnCheckedChanged(EventArgs e)
        {
            OnCheckedChanged(e);
            if (Checked)
            {
                BackColor = Color.Green;
            }
            else
            {
                BackColor = Color.LightGray;
            }
        }
    }
}