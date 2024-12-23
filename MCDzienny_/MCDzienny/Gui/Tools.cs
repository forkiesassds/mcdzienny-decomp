using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MCDzienny.Gui
{
    public class Tools : Form
    {

        ComboBox comboBoxDepth;

        ComboBox comboBoxHeight;

        ComboBox comboBoxWidth;
        IContainer components;

        Label label1;

        Label label10;

        Label label11;

        Label label2;

        Label label3;

        Label label4;

        Label label5;

        Label label6;

        Label label7;

        Label label8;

        Label label9;

        TabControl tabControl1;

        TabPage tabPage1;

        TextBox textMaxMaps;

        TextBox textRamRequired;

        public Tools()
        {
            InitializeComponent();
            UpdateControls();
        }

        void UpdateControls()
        {
            comboBoxDepth.SelectedIndex = 2;
            comboBoxWidth.SelectedIndex = 2;
            comboBoxHeight.SelectedIndex = 2;
        }

        public void ShowAt(Point location)
        {
            StartPosition = 0;
            int x = location.X - Width / 2;
            int y = location.Y - Height / 2;
            Location = new Point(x, y);
            Show();
        }

        void Recalcuate()
        {
            if (comboBoxHeight.SelectedIndex != -1 && comboBoxDepth.SelectedIndex != -1 && comboBoxWidth.SelectedIndex != -1)
            {
                int result;
                int result2;
                int result3;
                int.TryParse(comboBoxWidth.Items[comboBoxWidth.SelectedIndex].ToString(), out result);
                int.TryParse(comboBoxHeight.Items[comboBoxHeight.SelectedIndex].ToString(), out result2);
                int.TryParse(comboBoxDepth.Items[comboBoxDepth.SelectedIndex].ToString(), out result3);
                long num = result * (long)result2 * result3;
                decimal num2 = num / 1024m / 1024m;
                if (num2 > 16m)
                {
                    textRamRequired.Text = num2.ToString("##,#");
                }
                else if (num2 > 0.01m)
                {
                    textRamRequired.Text = num2.ToString("0.##");
                }
                else
                {
                    textRamRequired.Text = num2.ToString("0.####");
                }
                decimal num3 = 1024m / num2;
                if (num3 >= 1m)
                {
                    textMaxMaps.Text = num3.ToString("##,#");
                }
                else
                {
                    textMaxMaps.Text = num3.ToString("0.####");
                }
            }
        }

        void comboBoxWidth_SelectedIndexChanged(object sender, EventArgs e)
        {
            Recalcuate();
        }

        void comboBoxHeight_SelectedIndexChanged(object sender, EventArgs e)
        {
            Recalcuate();
        }

        void comboBoxDepth_SelectedIndexChanged(object sender, EventArgs e)
        {
            Recalcuate();
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
            //IL_006f: Unknown result type (might be due to invalid IL or missing references)
            //IL_0079: Expected O, but got Unknown
            //IL_007a: Unknown result type (might be due to invalid IL or missing references)
            //IL_0084: Expected O, but got Unknown
            //IL_0085: Unknown result type (might be due to invalid IL or missing references)
            //IL_008f: Expected O, but got Unknown
            //IL_0090: Unknown result type (might be due to invalid IL or missing references)
            //IL_009a: Expected O, but got Unknown
            //IL_009b: Unknown result type (might be due to invalid IL or missing references)
            //IL_00a5: Expected O, but got Unknown
            //IL_00a6: Unknown result type (might be due to invalid IL or missing references)
            //IL_00b0: Expected O, but got Unknown
            //IL_00b1: Unknown result type (might be due to invalid IL or missing references)
            //IL_00bb: Expected O, but got Unknown
            //IL_00bc: Unknown result type (might be due to invalid IL or missing references)
            //IL_00c6: Expected O, but got Unknown
            //IL_03c4: Unknown result type (might be due to invalid IL or missing references)
            //IL_0bcf: Unknown result type (might be due to invalid IL or missing references)
            //IL_0bd9: Expected O, but got Unknown
            comboBoxWidth = new ComboBox();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            textMaxMaps = new TextBox();
            label10 = new Label();
            label11 = new Label();
            label9 = new Label();
            label8 = new Label();
            label7 = new Label();
            textRamRequired = new TextBox();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            comboBoxDepth = new ComboBox();
            label3 = new Label();
            comboBoxHeight = new ComboBox();
            label2 = new Label();
            label1 = new Label();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            SuspendLayout();
            comboBoxWidth.DropDownStyle = (ComboBoxStyle)2;
            comboBoxWidth.FormattingEnabled = true;
            comboBoxWidth.Items.AddRange(new object[10]
            {
                "16", "32", "64", "128", "256", "512", "1024", "2048", "4096", "8192"
            });
            comboBoxWidth.Location = new Point(28, 71);
            comboBoxWidth.Name = "comboBoxWidth";
            comboBoxWidth.Size = new Size(70, 21);
            comboBoxWidth.TabIndex = 0;
            comboBoxWidth.SelectedIndexChanged += comboBoxWidth_SelectedIndexChanged;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Location = new Point(2, 12);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(289, 255);
            tabControl1.TabIndex = 1;
            tabPage1.BackColor = SystemColors.Control;
            tabPage1.Controls.Add(textMaxMaps);
            tabPage1.Controls.Add(label10);
            tabPage1.Controls.Add(label11);
            tabPage1.Controls.Add(label9);
            tabPage1.Controls.Add(label8);
            tabPage1.Controls.Add(label7);
            tabPage1.Controls.Add(textRamRequired);
            tabPage1.Controls.Add(label6);
            tabPage1.Controls.Add(label5);
            tabPage1.Controls.Add(label4);
            tabPage1.Controls.Add(comboBoxDepth);
            tabPage1.Controls.Add(label3);
            tabPage1.Controls.Add(comboBoxHeight);
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(label1);
            tabPage1.Controls.Add(comboBoxWidth);
            tabPage1.Location = new Point(4, 22);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(281, 229);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Map RAM";
            textMaxMaps.Location = new Point(106, 176);
            textMaxMaps.Name = "textMaxMaps";
            textMaxMaps.ReadOnly = true;
            textMaxMaps.Size = new Size(54, 20);
            textMaxMaps.TabIndex = 12;
            textMaxMaps.Text = "4096";
            textMaxMaps.TextAlign = (HorizontalAlignment)1;
            label10.AutoSize = true;
            label10.Location = new Point(166, 179);
            label10.MaximumSize = new Size(230, 0);
            label10.Name = "label10";
            label10.Size = new Size(67, 13);
            label10.TabIndex = 11;
            label10.Text = "maps loaded";
            label11.AutoSize = true;
            label11.Location = new Point(31, 200);
            label11.MaximumSize = new Size(230, 0);
            label11.Name = "label11";
            label11.Size = new Size(87, 13);
            label11.TabIndex = 11;
            label11.Text = "at the same time.";
            label9.AutoSize = true;
            label9.Location = new Point(31, 179);
            label9.MaximumSize = new Size(230, 0);
            label9.Name = "label9";
            label9.Size = new Size(72, 13);
            label9.TabIndex = 11;
            label9.Text = "you can have";
            label8.AutoSize = true;
            label8.Location = new Point(31, 158);
            label8.MaximumSize = new Size(230, 0);
            label8.Name = "label8";
            label8.Size = new Size(204, 13);
            label8.TabIndex = 11;
            label8.Text = "Assuming that you have 1GB of free RAM";
            label7.AutoSize = true;
            label7.Location = new Point(201, 127);
            label7.Name = "label7";
            label7.Size = new Size(62, 13);
            label7.TabIndex = 10;
            label7.Text = "MB of RAM";
            textRamRequired.Location = new Point(127, 124);
            textRamRequired.Name = "textRamRequired";
            textRamRequired.ReadOnly = true;
            textRamRequired.Size = new Size(68, 20);
            textRamRequired.TabIndex = 9;
            textRamRequired.Text = "0,25";
            textRamRequired.TextAlign = (HorizontalAlignment)1;
            label6.AutoSize = true;
            label6.Location = new Point(31, 127);
            label6.Name = "label6";
            label6.Size = new Size(90, 13);
            label6.TabIndex = 8;
            label6.Text = "One map requires";
            label5.AutoSize = true;
            label5.Location = new Point(28, 106);
            label5.Name = "label5";
            label5.Size = new Size(45, 13);
            label5.TabIndex = 7;
            label5.Text = "Results:";
            label4.AutoSize = true;
            label4.Location = new Point(180, 52);
            label4.Name = "label4";
            label4.Size = new Size(36, 13);
            label4.TabIndex = 6;
            label4.Text = "Depth";
            comboBoxDepth.DropDownStyle = (ComboBoxStyle)2;
            comboBoxDepth.FormattingEnabled = true;
            comboBoxDepth.Items.AddRange(new object[10]
            {
                "16", "32", "64", "128", "256", "512", "1024", "2048", "4096", "8192"
            });
            comboBoxDepth.Location = new Point(180, 71);
            comboBoxDepth.Name = "comboBoxDepth";
            comboBoxDepth.Size = new Size(70, 21);
            comboBoxDepth.TabIndex = 5;
            comboBoxDepth.SelectedIndexChanged += comboBoxDepth_SelectedIndexChanged;
            label3.AutoSize = true;
            label3.Location = new Point(104, 52);
            label3.Name = "label3";
            label3.Size = new Size(38, 13);
            label3.TabIndex = 4;
            label3.Text = "Height";
            comboBoxHeight.DropDownStyle = (ComboBoxStyle)2;
            comboBoxHeight.FormattingEnabled = true;
            comboBoxHeight.Items.AddRange(new object[10]
            {
                "16", "32", "64", "128", "256", "512", "1024", "2048", "4096", "8192"
            });
            comboBoxHeight.Location = new Point(104, 71);
            comboBoxHeight.Name = "comboBoxHeight";
            comboBoxHeight.Size = new Size(70, 21);
            comboBoxHeight.TabIndex = 3;
            comboBoxHeight.SelectedIndexChanged += comboBoxHeight_SelectedIndexChanged;
            label2.AutoSize = true;
            label2.Location = new Point(28, 52);
            label2.Name = "label2";
            label2.Size = new Size(35, 13);
            label2.TabIndex = 2;
            label2.Text = "Width";
            label1.AutoSize = true;
            label1.Location = new Point(15, 13);
            label1.MaximumSize = new Size(250, 0);
            label1.Name = "label1";
            label1.Size = new Size(240, 26);
            label1.TabIndex = 1;
            label1.Text = "This tool calculates the approx. RAM usage for a map of given dimensions.";
            AutoScaleDimensions = new SizeF(6f, 13f);
            AutoScaleMode = (AutoScaleMode)1;
            ClientSize = new Size(292, 269);
            Controls.Add(tabControl1);
            FormBorderStyle = (FormBorderStyle)2;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Tools";
            ShowIcon = false;
            StartPosition = (FormStartPosition)4;
            Text = "Tools";
            FormClosing += Tools_FormClosing;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ResumeLayout(false);
        }

        void Tools_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }
    }
}