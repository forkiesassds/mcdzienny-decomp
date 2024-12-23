using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MCDzienny.Plugins
{
    public class GuiExample : UserControl
    {
        IContainer components;

        Label labelMessage;

        public GuiExample()
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
            //IL_0034: Unknown result type (might be due to invalid IL or missing references)
            //IL_003e: Expected O, but got Unknown
            labelMessage = new Label();
            SuspendLayout();
            labelMessage.AutoSize = true;
            labelMessage.Font = new Font("Microsoft Sans Serif", 12f, (FontStyle)1, (GraphicsUnit)3, 238);
            labelMessage.ForeColor = Color.Brown;
            labelMessage.Location = new Point(173, 163);
            labelMessage.Name = "labelMessage";
            labelMessage.Size = new Size(106, 20);
            labelMessage.TabIndex = 0;
            labelMessage.Text = "Hello World!";
            AutoScaleDimensions = new SizeF(6f, 13f);
            AutoScaleMode = (AutoScaleMode)1;
            BackColor = Color.WhiteSmoke;
            Controls.Add(labelMessage);
            Name = "GuiExample";
            Size = new Size(465, 378);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}