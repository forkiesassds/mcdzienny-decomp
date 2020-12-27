using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MCDzienny.Plugins
{
    // Token: 0x020001E3 RID: 483
    public class GuiExample : UserControl
    {
        // Token: 0x0400070E RID: 1806
        private IContainer components;

        // Token: 0x0400070F RID: 1807
        private Label labelMessage;

        // Token: 0x06000D74 RID: 3444 RVA: 0x0004C794 File Offset: 0x0004A994
        public GuiExample()
        {
            InitializeComponent();
        }

        // Token: 0x06000D75 RID: 3445 RVA: 0x0004C7A4 File Offset: 0x0004A9A4
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        // Token: 0x06000D76 RID: 3446 RVA: 0x0004C7C4 File Offset: 0x0004A9C4
        private void InitializeComponent()
        {
            labelMessage = new Label();
            SuspendLayout();
            labelMessage.AutoSize = true;
            labelMessage.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 238);
            labelMessage.ForeColor = Color.Brown;
            labelMessage.Location = new Point(173, 163);
            labelMessage.Name = "labelMessage";
            labelMessage.Size = new Size(106, 20);
            labelMessage.TabIndex = 0;
            labelMessage.Text = "Hello World!";
            AutoScaleDimensions = new SizeF(6f, 13f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            Controls.Add(labelMessage);
            Name = "GuiExample";
            Size = new Size(465, 378);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}