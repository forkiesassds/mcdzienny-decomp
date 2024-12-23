using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace MCDzienny.Misc
{
    public class PortIsClosed : Form
    {
        IContainer components;

        Label label1;

        Button okButton;

        PictureBox pictureBox1;

        public PortIsClosed()
        {
            //IL_0025: Unknown result type (might be due to invalid IL or missing references)
            //IL_002f: Expected O, but got Unknown
            InitializeComponent();
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            Stream manifestResourceStream = executingAssembly.GetManifestResourceStream("MCDzienny.icon_wrong.png");
            pictureBox1.Image = new Bitmap(manifestResourceStream);
        }

        public static void ShowBox()
        {
            //IL_0032: Unknown result type (might be due to invalid IL or missing references)
            PortIsClosed portIsClosed = new PortIsClosed();
            portIsClosed.label1.Text = "Port " + Server.port +
                " is not accessible. No one can connect to your server from the internet. You have to port forward in order to let people join. For help visit: www.mcdzienny.cba.pl";
            portIsClosed.StartPosition = (FormStartPosition)1;
            portIsClosed.ShowDialog();
        }

        void okButton_Click(object sender, EventArgs e)
        {
            Close();
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
            //IL_00dc: Unknown result type (might be due to invalid IL or missing references)
            //IL_00e6: Expected O, but got Unknown
            pictureBox1 = new PictureBox();
            label1 = new Label();
            okButton = new Button();
            ((ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            pictureBox1.Location = new Point(333, 47);
            pictureBox1.MaximumSize = new Size(100, 100);
            pictureBox1.MinimumSize = new Size(100, 100);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(100, 100);
            pictureBox1.SizeMode = (PictureBoxSizeMode)1;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 12f, (FontStyle)1, (GraphicsUnit)3, 238);
            label1.Location = new Point(24, 35);
            label1.MaximumSize = new Size(250, 0);
            label1.Name = "label1";
            label1.Size = new Size(244, 120);
            label1.TabIndex = 1;
            label1.Text =
                "Port 25565 is not accessible. No one can connect to your server from the internet. You have to port forward in order to let people join. For help visit: www.mcdzienny.cba.pl";
            okButton.Location = new Point(207, 189);
            okButton.Name = "okButton";
            okButton.Size = new Size(75, 23);
            okButton.TabIndex = 2;
            okButton.Text = "OK";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
            AutoScaleDimensions = new SizeF(6f, 13f);
            AutoScaleMode = (AutoScaleMode)1;
            ClientSize = new Size(488, 224);
            Controls.Add(okButton);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PortIsClosed";
            ShowIcon = false;
            Text = "WARNING: Port is Closed";
            ((ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}