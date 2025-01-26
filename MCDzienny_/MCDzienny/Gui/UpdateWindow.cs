using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace MCDzienny.Gui
{
    public class UpdateWindow : Form
    {

        Button button1;

        CheckBox chkAutoUpdate;

        CheckBox chkNotify;

        Button cmdDiscard;

        Button cmdUpdate;
        IContainer components;

        Label label1;

        ListBox listRevisions;

        Panel panel1;

        Panel panel2;

        TextBox txtCountdown;

        public UpdateWindow()
        {
            InitializeComponent();
        }

        void UpdateWindow_Load(object sender, EventArgs e)
        {
            UpdLoadProp("properties/update.properties");
            WebClient webClient = new WebClient();
            webClient.DownloadFile("http://www.mclawl.tk/revs.txt", "text/revs.txt");
            listRevisions.Items.Clear();
            FileInfo fileInfo = new FileInfo("text/revs.txt");
            StreamReader streamReader = fileInfo.OpenText();
            if (File.Exists("text/revs.txt"))
            {
                while (!streamReader.EndOfStream)
                {
                    listRevisions.Items.Add(streamReader.ReadLine());
                }
            }
            streamReader.Close();
            streamReader.Dispose();
            fileInfo.Delete();
            webClient.Dispose();
        }

        public void UpdSave(string givenPath)
        {
            StreamWriter streamWriter = new StreamWriter(File.Create(givenPath));
            streamWriter.WriteLine("#This file manages the update process");
            streamWriter.WriteLine("#Toggle AutoUpdate to true for the server to automatically update");
            streamWriter.WriteLine("#Notify notifies players in-game of impending restart");
            streamWriter.WriteLine("#Restart Countdown is how long in seconds the server will count before restarting and updating");
            streamWriter.WriteLine();
            streamWriter.WriteLine("autoupdate= " + chkAutoUpdate.Checked);
            streamWriter.WriteLine("notify = " + chkNotify.Checked);
            streamWriter.WriteLine("restartcountdown = " + txtCountdown.Text);
            streamWriter.Flush();
            streamWriter.Close();
            streamWriter.Dispose();
            Close();
        }

        public void UpdLoadProp(string givenPath)
        {
            if (!File.Exists(givenPath))
            {
                return;
            }
            string[] array = File.ReadAllLines(givenPath);
            string[] array2 = array;
            foreach (string text in array2)
            {
                if (text != "" && text[0] != '#')
                {
                    string text2 = text.Split('=')[0].Trim();
                    string text3 = text.Split('=')[1].Trim();
                    switch (text2.ToLower())
                    {
                        case "autoupdate":
                            chkAutoUpdate.Checked = text3.ToLower() == "true";
                            break;
                        case "notify":
                            chkNotify.Checked = text3.ToLower() == "true";
                            break;
                        case "restartcountdown":
                            txtCountdown.Text = text3;
                            break;
                    }
                }
            }
        }

        void button1_Click(object sender, EventArgs e)
        {
            //IL_0039: Unknown result type (might be due to invalid IL or missing references)
            string s = txtCountdown.Text.Trim();
            double d;
            if (!double.TryParse(s, out d) || txtCountdown.Text == "")
            {
                MessageBox.Show("You must enter a number for the countdown");
                return;
            }
            UpdSave("properties/update.properties");
            Server.autoupdate = chkAutoUpdate.Checked;
        }

        void cmdDiscard_Click(object sender, EventArgs e)
        {
            UpdLoadProp("properties/update.properties");
            Close();
        }

        void listRevisions_SelectedValueChanged(object sender, EventArgs e)
        {
            Server.selectedrevision = listRevisions.SelectedItem.ToString();
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
            //IL_0038: Unknown result type (might be due to invalid IL or missing references)
            //IL_0042: Expected O, but got Unknown
            //IL_0043: Unknown result type (might be due to invalid IL or missing references)
            //IL_004d: Expected O, but got Unknown
            //IL_004e: Unknown result type (might be due to invalid IL or missing references)
            //IL_0058: Expected O, but got Unknown
            //IL_0059: Unknown result type (might be due to invalid IL or missing references)
            //IL_0063: Expected O, but got Unknown
            //IL_0064: Unknown result type (might be due to invalid IL or missing references)
            //IL_006e: Expected O, but got Unknown
            //IL_0111: Unknown result type (might be due to invalid IL or missing references)
            //IL_011b: Expected O, but got Unknown
            //IL_018d: Unknown result type (might be due to invalid IL or missing references)
            //IL_0197: Expected O, but got Unknown
            //IL_0220: Unknown result type (might be due to invalid IL or missing references)
            //IL_022a: Expected O, but got Unknown
            //IL_029f: Unknown result type (might be due to invalid IL or missing references)
            //IL_02a9: Expected O, but got Unknown
            //IL_0336: Unknown result type (might be due to invalid IL or missing references)
            //IL_0340: Expected O, but got Unknown
            //IL_04c1: Unknown result type (might be due to invalid IL or missing references)
            //IL_04cb: Expected O, but got Unknown
            //IL_0540: Unknown result type (might be due to invalid IL or missing references)
            //IL_054a: Expected O, but got Unknown
            panel1 = new Panel();
            cmdUpdate = new Button();
            listRevisions = new ListBox();
            chkAutoUpdate = new CheckBox();
            cmdDiscard = new Button();
            button1 = new Button();
            panel2 = new Panel();
            txtCountdown = new TextBox();
            label1 = new Label();
            chkNotify = new CheckBox();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            panel1.Controls.Add(cmdUpdate);
            panel1.Controls.Add(listRevisions);
            panel1.Location = new Point(8, 7);
            panel1.Name = "panel1";
            panel1.Size = new Size(148, 173);
            panel1.TabIndex = 0;
            cmdUpdate.Font = new Font("Calibri", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmdUpdate.Location = new Point(4, 70);
            cmdUpdate.Name = "cmdUpdate";
            cmdUpdate.Size = new Size(82, 23);
            cmdUpdate.TabIndex = 2;
            cmdUpdate.Text = "Update";
            cmdUpdate.UseVisualStyleBackColor = true;
            listRevisions.Font = new Font("Calibri", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            listRevisions.FormattingEnabled = true;
            listRevisions.Location = new Point(89, 10);
            listRevisions.Name = "listRevisions";
            listRevisions.Size = new Size(53, 147);
            listRevisions.TabIndex = 0;
            listRevisions.SelectedValueChanged += listRevisions_SelectedValueChanged;
            chkAutoUpdate.AutoSize = true;
            chkAutoUpdate.Font = new Font("Calibri", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkAutoUpdate.Location = new Point(28, 4);
            chkAutoUpdate.Name = "chkAutoUpdate";
            chkAutoUpdate.Size = new Size(133, 17);
            chkAutoUpdate.TabIndex = 1;
            chkAutoUpdate.Text = "Auto update to newest";
            chkAutoUpdate.UseVisualStyleBackColor = true;
            cmdDiscard.Font = new Font("Calibri", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmdDiscard.Location = new Point(111, 280);
            cmdDiscard.Name = "cmdDiscard";
            cmdDiscard.Size = new Size(59, 23);
            cmdDiscard.TabIndex = 2;
            cmdDiscard.Text = "Discard";
            cmdDiscard.UseVisualStyleBackColor = true;
            cmdDiscard.Click += cmdDiscard_Click;
            button1.Font = new Font("Calibri", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.Location = new Point(35, 279);
            button1.Name = "button1";
            button1.Size = new Size(59, 23);
            button1.TabIndex = 3;
            button1.Text = "Save";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            panel2.Controls.Add(txtCountdown);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(chkNotify);
            panel2.Controls.Add(chkAutoUpdate);
            panel2.Location = new Point(8, 189);
            panel2.Name = "panel2";
            panel2.Size = new Size(209, 82);
            panel2.TabIndex = 4;
            txtCountdown.Location = new Point(161, 45);
            txtCountdown.Name = "txtCountdown";
            txtCountdown.Size = new Size(42, 20);
            txtCountdown.TabIndex = 4;
            label1.AutoSize = true;
            label1.Font = new Font("Calibri", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(2, 49);
            label1.Name = "label1";
            label1.Size = new Size(158, 13);
            label1.TabIndex = 3;
            label1.Text = "Time (in seconds) to countdown:";
            chkNotify.AutoSize = true;
            chkNotify.Font = new Font("Calibri", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkNotify.Location = new Point(28, 23);
            chkNotify.Name = "chkNotify";
            chkNotify.Size = new Size(139, 17);
            chkNotify.TabIndex = 2;
            chkNotify.Text = "Notify in-game of restart";
            chkNotify.UseVisualStyleBackColor = true;
            AutoScaleDimensions = new SizeF(6f, 13f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(223, 318);
            Controls.Add(panel2);
            Controls.Add(button1);
            Controls.Add(cmdDiscard);
            Controls.Add(panel1);
            Name = "UpdateWindow";
            Text = "Update";
            Load += UpdateWindow_Load;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }
    }
}