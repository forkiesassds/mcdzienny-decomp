using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using MCDzienny.Settings;

namespace MCDzienny.Misc
{
    public class PortIsOpen : Form
    {

        CheckBox checkBox1;
        IContainer components;

        Label label1;

        Label label2;

        Button okButton;

        PictureBox pictureBox1;

        public PortIsOpen()
        {
            //IL_0025: Unknown result type (might be due to invalid IL or missing references)
            //IL_002f: Expected O, but got Unknown
            InitializeComponent();
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            Stream manifestResourceStream = executingAssembly.GetManifestResourceStream("MCDzienny.icon_ok.png");
            pictureBox1.Image = new Bitmap(manifestResourceStream);
        }

        public static void ShowBox()
        {
            //IL_0032: Unknown result type (might be due to invalid IL or missing references)
            PortIsOpen portIsOpen = new PortIsOpen();
            portIsOpen.label1.Text = "Port " + Server.port + " is open. It means that other people can connect to your server.";
            portIsOpen.StartPosition = FormStartPosition.CenterScreen;
            portIsOpen.ShowDialog();
        }

        void okButton_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                GeneralSettings.All.CheckPortOnStart = false;
            }
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
            //IL_0022: Unknown result type (might be due to invalid IL or missing references)
            //IL_002c: Expected O, but got Unknown
            //IL_002d: Unknown result type (might be due to invalid IL or missing references)
            //IL_0037: Expected O, but got Unknown
            //IL_00f2: Unknown result type (might be due to invalid IL or missing references)
            //IL_00fc: Expected O, but got Unknown
            //IL_0209: Unknown result type (might be due to invalid IL or missing references)
            //IL_0213: Expected O, but got Unknown
            pictureBox1 = new PictureBox();
            label1 = new Label();
            okButton = new Button();
            label2 = new Label();
            checkBox1 = new CheckBox();
            ((ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            pictureBox1.Location = new Point(333, 36);
            pictureBox1.MaximumSize = new Size(100, 100);
            pictureBox1.MinimumSize = new Size(100, 100);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(100, 100);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 238);
            label1.Location = new Point(22, 76);
            label1.MaximumSize = new Size(250, 0);
            label1.Name = "label1";
            label1.Size = new Size(250, 60);
            label1.TabIndex = 1;
            label1.Text = "Port 25565 is open. It means that other people can connect to your server.";
            okButton.Location = new Point(207, 215);
            okButton.Name = "okButton";
            okButton.Size = new Size(75, 23);
            okButton.TabIndex = 2;
            okButton.Text = "OK";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 238);
            label2.Location = new Point(22, 36);
            label2.Name = "label2";
            label2.Size = new Size(82, 20);
            label2.TabIndex = 3;
            label2.Text = "Success!";
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(26, 178);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(176, 17);
            checkBox1.TabIndex = 4;
            checkBox1.Text = "Don't show this message again.";
            checkBox1.UseVisualStyleBackColor = true;
            AutoScaleDimensions = new SizeF(6f, 13f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(488, 252);
            Controls.Add(checkBox1);
            Controls.Add(label2);
            Controls.Add(okButton);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PortIsOpen";
            ShowIcon = false;
            Text = "Port is Open!";
            ((ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}