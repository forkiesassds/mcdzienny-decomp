using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MCDzienny
{
    public class PluginControlAddPlugin : UserControl
    {

        Button buttonAddPlugin;

        Button buttonClearText;

        Container components;

        Label label1;
        TextBox textBoxPlugin;

        public PluginControlAddPlugin()
        {
            InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        void InitializeComponent()
        {
            //IL_0001: Unknown result type (might be due to invalid IL or missing references)
            //IL_000b: Expected O, but got Unknown
            //IL_000c: Unknown result type (might be due to invalid IL or missing references)
            //IL_0016: Expected O, but got Unknown
            //IL_0017: Unknown result type (might be due to invalid IL or missing references)
            //IL_0021: Expected O, but got Unknown
            //IL_0022: Unknown result type (might be due to invalid IL or missing references)
            //IL_002c: Expected O, but got Unknown
            textBoxPlugin = new TextBox();
            label1 = new Label();
            buttonAddPlugin = new Button();
            buttonClearText = new Button();
            SuspendLayout();
            textBoxPlugin.Anchor = (AnchorStyles)15;
            textBoxPlugin.BackColor = Color.White;
            textBoxPlugin.Location = new Point(8, 31);
            textBoxPlugin.Multiline = true;
            textBoxPlugin.Name = "textBoxPlugin";
            textBoxPlugin.ScrollBars = (ScrollBars)3;
            textBoxPlugin.Size = new Size(473, 313);
            textBoxPlugin.TabIndex = 0;
            label1.AutoSize = true;
            label1.Location = new Point(8, 12);
            label1.Name = "label1";
            label1.Size = new Size(135, 13);
            label1.TabIndex = 3;
            label1.Text = "Paste a plugin code below:";
            buttonAddPlugin.Anchor = (AnchorStyles)6;
            buttonAddPlugin.Location = new Point(8, 350);
            buttonAddPlugin.Name = "buttonAddPlugin";
            buttonAddPlugin.Size = new Size(75, 23);
            buttonAddPlugin.TabIndex = 4;
            buttonAddPlugin.Text = "Add Plugin";
            buttonAddPlugin.UseVisualStyleBackColor = true;
            buttonAddPlugin.Click += buttonAddPlugin_Click;
            buttonClearText.Anchor = (AnchorStyles)10;
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

        void buttonAddPlugin_Click(object sender, EventArgs e)
        {
            Server.Plugins.AddPluginFromString(textBoxPlugin.Text);
        }

        void buttonClearText_Click(object sender, EventArgs e)
        {
            textBoxPlugin.Clear();
        }
    }
}