using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MCDzienny.Plugins
{
    // Token: 0x020001F5 RID: 501
    public class mainGUI : UserControl
    {
        // Token: 0x04000745 RID: 1861
        private IContainer components;

        // Token: 0x04000744 RID: 1860
        public stuffWithGui g;

        // Token: 0x04000749 RID: 1865
        public TextBox intervalTextBox;

        // Token: 0x04000747 RID: 1863
        private Label label1;

        // Token: 0x04000748 RID: 1864
        private Label label2;

        // Token: 0x04000746 RID: 1862
        public TextBox msgTextBox;

        // Token: 0x0400074A RID: 1866
        public Timer timer;

        // Token: 0x06000DD7 RID: 3543 RVA: 0x0004DDAC File Offset: 0x0004BFAC
        public mainGUI()
        {
            InitializeComponent();
            g = new stuffWithGui(timer, msgTextBox, intervalTextBox);
        }

        // Token: 0x06000DD8 RID: 3544 RVA: 0x0004DDD8 File Offset: 0x0004BFD8
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        // Token: 0x06000DD9 RID: 3545 RVA: 0x0004DDF8 File Offset: 0x0004BFF8
        private void InitializeComponent()
        {
            components = new Container();
            msgTextBox = new TextBox();
            label1 = new Label();
            label2 = new Label();
            intervalTextBox = new TextBox();
            timer = new Timer(components);
            SuspendLayout();
            msgTextBox.Location = new Point(68, 13);
            msgTextBox.Name = "msgTextBox";
            msgTextBox.Size = new Size(204, 20);
            msgTextBox.TabIndex = 0;
            label1.AutoSize = true;
            label1.Location = new Point(12, 16);
            label1.Name = "label1";
            label1.Size = new Size(50, 13);
            label1.TabIndex = 1;
            label1.Text = "Message";
            label2.AutoSize = true;
            label2.Location = new Point(12, 40);
            label2.Name = "label2";
            label2.Size = new Size(42, 13);
            label2.TabIndex = 2;
            label2.Text = "Interval";
            intervalTextBox.Location = new Point(68, 37);
            intervalTextBox.Name = "intervalTextBox";
            intervalTextBox.Size = new Size(204, 20);
            intervalTextBox.TabIndex = 3;
            AutoScaleDimensions = new SizeF(6f, 13f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(284, 262);
            Controls.Add(intervalTextBox);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(msgTextBox);
            Name = "mainGUI";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}