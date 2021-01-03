using System;
using System.Windows.Forms;

namespace MCDzienny.Gui
{
    // Token: 0x02000096 RID: 150
    public partial class PopUpMessage : Form
    {
        // Token: 0x06000403 RID: 1027 RVA: 0x000162E8 File Offset: 0x000144E8
        public PopUpMessage(string message)
        {
            InitializeComponent();
            mainTextBox.Text = message;
            mainTextBox.SelectionStart = mainTextBox.Text.Length;
            CenterToScreen();
        }

        // Token: 0x06000404 RID: 1028 RVA: 0x00016324 File Offset: 0x00014524
        public PopUpMessage(string message, string title, string label)
        {
            InitializeComponent();
            Text = title;
            this.label.Text = label;
            mainTextBox.Text = message;
            mainTextBox.SelectionStart = mainTextBox.Text.Length;
            CenterToScreen();
        }

        // Token: 0x06000405 RID: 1029 RVA: 0x00016380 File Offset: 0x00014580
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}