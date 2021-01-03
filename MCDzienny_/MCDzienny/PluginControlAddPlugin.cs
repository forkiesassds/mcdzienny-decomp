using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MCDzienny
{
    // Token: 0x020001E9 RID: 489
    public class PluginControlAddPlugin : UserControl
    {
        // Token: 0x04000722 RID: 1826
        private Button buttonAddPlugin;

        // Token: 0x04000723 RID: 1827
        private Button buttonClearText;

        // Token: 0x04000724 RID: 1828
        private Container components;

        // Token: 0x04000721 RID: 1825
        private Label label1;

        // Token: 0x04000720 RID: 1824
        private TextBox textBoxPlugin;

        // Token: 0x06000D8D RID: 3469 RVA: 0x0004CFFC File Offset: 0x0004B1FC
        public PluginControlAddPlugin()
        {
            InitializeComponent();
        }

        // Token: 0x06000D8E RID: 3470 RVA: 0x0004D00C File Offset: 0x0004B20C
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        // Token: 0x06000D8F RID: 3471 RVA: 0x0004D02C File Offset: 0x0004B22C
        private void InitializeComponent()
        {
            textBoxPlugin = new TextBox();
            label1 = new Label();
            buttonAddPlugin = new Button();
            buttonClearText = new Button();
            SuspendLayout();
            textBoxPlugin.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxPlugin.BackColor = Color.White;
            textBoxPlugin.Location = new Point(8, 31);
            textBoxPlugin.Multiline = true;
            textBoxPlugin.Name = "textBoxPlugin";
            textBoxPlugin.ScrollBars = ScrollBars.Both;
            textBoxPlugin.Size = new Size(473, 313);
            textBoxPlugin.TabIndex = 0;
            label1.AutoSize = true;
            label1.Location = new Point(8, 12);
            label1.Name = "label1";
            label1.Size = new Size(135, 13);
            label1.TabIndex = 3;
            label1.Text = "Paste a plugin code below:";
            buttonAddPlugin.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonAddPlugin.Location = new Point(8, 350);
            buttonAddPlugin.Name = "buttonAddPlugin";
            buttonAddPlugin.Size = new Size(75, 23);
            buttonAddPlugin.TabIndex = 4;
            buttonAddPlugin.Text = "Add Plugin";
            buttonAddPlugin.UseVisualStyleBackColor = true;
            buttonAddPlugin.Click += buttonAddPlugin_Click;
            buttonClearText.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonClearText.Location = new Point(406, 350);
            buttonClearText.Name = "buttonClearText";
            buttonClearText.Size = new Size(75, 23);
            buttonClearText.TabIndex = 5;
            buttonClearText.Text = "Clear Text";
            buttonClearText.UseVisualStyleBackColor = true;
            buttonClearText.Click += buttonClearText_Click;
            BackColor = Color.White;
            Controls.Add(buttonClearText);
            Controls.Add(buttonAddPlugin);
            Controls.Add(label1);
            Controls.Add(textBoxPlugin);
            Name = "PluginControlAddPlugin";
            Size = new Size(488, 376);
            ResumeLayout(false);
            PerformLayout();
        }

        // Token: 0x06000D90 RID: 3472 RVA: 0x0004D2D8 File Offset: 0x0004B4D8
        private void buttonAddPlugin_Click(object sender, EventArgs e)
        {
            Server.Plugins.AddPluginFromString(textBoxPlugin.Text);
        }

        // Token: 0x06000D91 RID: 3473 RVA: 0x0004D2F0 File Offset: 0x0004B4F0
        private void buttonClearText_Click(object sender, EventArgs e)
        {
            textBoxPlugin.Clear();
        }
    }
}