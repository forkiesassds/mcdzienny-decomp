using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MCDzienny.Plugins
{
    public class mainGUI : UserControl
    {

        IContainer components;
        public stuffWithGui g;

        public TextBox intervalTextBox;

        Label label1;

        Label label2;

        public TextBox msgTextBox;

        public Timer timer;

        public mainGUI()
        {
            InitializeComponent();
            g = new stuffWithGui(timer, msgTextBox, intervalTextBox);
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
            //IL_000c: Unknown result type (might be due to invalid IL or missing references)
            //IL_0016: Expected O, but got Unknown
            //IL_0017: Unknown result type (might be due to invalid IL or missing references)
            //IL_0021: Expected O, but got Unknown
            //IL_0022: Unknown result type (might be due to invalid IL or missing references)
            //IL_002c: Expected O, but got Unknown
            //IL_002d: Unknown result type (might be due to invalid IL or missing references)
            //IL_0037: Expected O, but got Unknown
            //IL_003e: Unknown result type (might be due to invalid IL or missing references)
            //IL_0048: Expected O, but got Unknown
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