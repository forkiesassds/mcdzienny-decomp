using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MCDzienny.Settings;

namespace MCDzienny.Gui
{
    public class ColorChatSettings : Form
    {

        Button button1;

        Button button2;

        string chatFont;

        ComboBox chatFontCombobox;

        float chatFontSize;

        ComboBox chatFontSizeCombo;

        string cName;

        IContainer components;

        TextBox consoleName;

        string customCName;

        TextBox customConsoleDelimiter;

        TextBox customConsoleName;

        string customDelimiter;

        bool doNotRestore;

        Label label1;

        Label label2;

        Label label3;

        Label label4;

        Label label5;

        RadioButton radioButton1;

        RadioButton radioButton2;
        bool useCustomName;

        public ColorChatSettings()
        {
            InitializeComponent();
            InitUpdateControls();
            CacheSettings();
        }

        public void ShowAt(Point location)
        {
            StartPosition = 0;
            int x = location.X - Width / 2;
            int y = location.Y - Height / 2;
            Location = new Point(x, y);
            Show();
        }

        void InitUpdateControls()
        {
            chatFontSizeCombo.Items.AddRange(new object[16]
            {
                8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72
            });
            radioButton2.Checked = GeneralSettings.All.UseCustomName;
            radioButton1.Checked = !radioButton2.Checked;
            customConsoleName.Text = GeneralSettings.All.CustomConsoleName;
            chatFontCombobox.Items.AddRange(AvailableFontsNames().ToArray());
            Font font = Window.thisWindow.GetFont();
            string name = font.FontFamily.Name;
            int num = chatFontCombobox.FindString(name);
            if (num != -1)
            {
                chatFontCombobox.SelectedIndex = num;
            }
            float size = font.Size;
            chatFontSizeCombo.Text = size.ToString();
            consoleName.Text = Server.ConsoleName;
            customConsoleDelimiter.Text = GeneralSettings.All.CustomConsoleNameDelimiter;
        }

        void CacheSettings()
        {
            Font font = Window.thisWindow.GetFont();
            cName = Server.ConsoleName;
            customCName = GeneralSettings.All.CustomConsoleName;
            chatFont = font.FontFamily.Name;
            chatFontSize = font.Size;
            useCustomName = GeneralSettings.All.UseCustomName;
            customDelimiter = GeneralSettings.All.CustomConsoleNameDelimiter;
        }

        List<string> AvailableFontsNames()
        {
            var list = new List<string>();
            FontFamily[] families = FontFamily.Families;
            foreach (FontFamily val in families)
            {
                list.Add(val.Name);
            }
            return list;
        }

        void chatFontSizeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //IL_0034: Unknown result type (might be due to invalid IL or missing references)
            //IL_003e: Expected O, but got Unknown
            Font font = Window.thisWindow.GetFont();
            if (chatFontSizeCombo.SelectedIndex != -1)
            {
                Window.thisWindow.SetFont(new Font(font.FontFamily, float.Parse(chatFontSizeCombo.Text)));
            }
        }

        void chatFontSizeCombo_Validating(object sender, CancelEventArgs e)
        {
            float result;
            if (!float.TryParse(chatFontSizeCombo.Text, out result))
            {
                chatFontSizeCombo.Text = Window.thisWindow.GetFont().Size.ToString();
                e.Cancel = true;
            }
            else
            {
                GeneralSettings.All.ChatFontSize = result;
            }
        }

        void consoleName_Validating(object sender, CancelEventArgs e)
        {
            if (!ServerProperties.ValidString(consoleName.Text, "%![]:.,{}~-+()?_/\\ "))
            {
                consoleName.Text = Server.ConsoleName;
                e.Cancel = true;
            }
            else
            {
                Server.ConsoleName = consoleName.Text;
            }
        }

        void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                customConsoleName.ReadOnly = false;
                GeneralSettings.All.UseCustomName = true;
            }
            else
            {
                customConsoleName.ReadOnly = true;
            }
        }

        void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                consoleName.ReadOnly = false;
                GeneralSettings.All.UseCustomName = false;
            }
            else
            {
                consoleName.ReadOnly = true;
            }
        }

        void customConsoleName_Validating(object sender, CancelEventArgs e)
        {
            if (!ServerProperties.ValidString(customConsoleName.Text, "%![]:.,{}~-+()?_/\\ "))
            {
                customConsoleName.Text = GeneralSettings.All.CustomConsoleName;
                e.Cancel = true;
            }
            else
            {
                GeneralSettings.All.CustomConsoleName = customConsoleName.Text;
            }
        }

        void chatFontCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //IL_0042: Unknown result type (might be due to invalid IL or missing references)
            //IL_0048: Expected O, but got Unknown
            if (chatFontCombobox.SelectedIndex != -1)
            {
                Font font = Window.thisWindow.GetFont();
                string text = chatFontCombobox.Items[chatFontCombobox.SelectedIndex].ToString();
                Font font2 = new Font(text, font.Size);
                Window.thisWindow.SetFont(font2);
                GeneralSettings.All.ChatFontFamily = text;
            }
        }

        void RestoreSettings()
        {
            //IL_0021: Unknown result type (might be due to invalid IL or missing references)
            //IL_002b: Expected O, but got Unknown
            GeneralSettings.All.UseCustomName = useCustomName;
            Window.thisWindow.SetFont(new Font(chatFont, chatFontSize));
            Server.ConsoleName = cName;
            GeneralSettings.All.CustomConsoleName = customCName;
            GeneralSettings.All.CustomConsoleNameDelimiter = customDelimiter;
        }

        void button1_Click(object sender, EventArgs e)
        {
            doNotRestore = true;
            Close();
        }

        void button2_Click(object sender, EventArgs e)
        {
            RestoreSettings();
            Close();
        }

        void ColorChatSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!doNotRestore)
            {
                RestoreSettings();
            }
        }

        void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (!ServerProperties.ValidString(consoleName.Text, "%![]:.,{}~-+()?_/\\ "))
            {
                customConsoleDelimiter.Text = GeneralSettings.All.CustomConsoleNameDelimiter;
                e.Cancel = true;
            }
            else
            {
                GeneralSettings.All.CustomConsoleNameDelimiter = customConsoleDelimiter.Text;
            }
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
            //IL_07e5: Unknown result type (might be due to invalid IL or missing references)
            //IL_07ef: Expected O, but got Unknown
            chatFontCombobox = new ComboBox();
            chatFontSizeCombo = new ComboBox();
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
            customConsoleName = new TextBox();
            consoleName = new TextBox();
            customConsoleDelimiter = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            chatFontCombobox.DropDownStyle = ComboBoxStyle.DropDownList;
            chatFontCombobox.FormattingEnabled = true;
            chatFontCombobox.Location = new Point(21, 38);
            chatFontCombobox.Name = "chatFontCombobox";
            chatFontCombobox.Size = new Size(155, 21);
            chatFontCombobox.TabIndex = 44;
            chatFontCombobox.SelectedIndexChanged += chatFontCombobox_SelectedIndexChanged;
            chatFontSizeCombo.FormattingEnabled = true;
            chatFontSizeCombo.Location = new Point(192, 38);
            chatFontSizeCombo.Name = "chatFontSizeCombo";
            chatFontSizeCombo.Size = new Size(74, 21);
            chatFontSizeCombo.TabIndex = 45;
            chatFontSizeCombo.SelectedIndexChanged += chatFontSizeCombo_SelectedIndexChanged;
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(15, 99);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(14, 13);
            radioButton1.TabIndex = 46;
            radioButton1.TabStop = true;
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.CheckedChanged += radioButton1_CheckedChanged;
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(15, 140);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(14, 13);
            radioButton2.TabIndex = 47;
            radioButton2.TabStop = true;
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += radioButton2_CheckedChanged;
            customConsoleName.Location = new Point(35, 140);
            customConsoleName.Name = "customConsoleName";
            customConsoleName.Size = new Size(144, 20);
            customConsoleName.TabIndex = 48;
            customConsoleName.Validating += customConsoleName_Validating;
            consoleName.Location = new Point(35, 96);
            consoleName.Name = "consoleName";
            consoleName.Size = new Size(144, 20);
            consoleName.TabIndex = 49;
            consoleName.Validating += consoleName_Validating;
            customConsoleDelimiter.Location = new Point(195, 140);
            customConsoleDelimiter.Name = "customConsoleDelimiter";
            customConsoleDelimiter.Size = new Size(63, 20);
            customConsoleDelimiter.TabIndex = 50;
            customConsoleDelimiter.Validating += textBox1_Validating;
            label1.AutoSize = true;
            label1.Location = new Point(18, 22);
            label1.Name = "label1";
            label1.Size = new Size(31, 13);
            label1.TabIndex = 51;
            label1.Text = "Font:";
            label2.AutoSize = true;
            label2.Location = new Point(189, 22);
            label2.Name = "label2";
            label2.Size = new Size(30, 13);
            label2.TabIndex = 52;
            label2.Text = "Size:";
            label3.AutoSize = true;
            label3.Location = new Point(32, 80);
            label3.Name = "label3";
            label3.Size = new Size(73, 13);
            label3.TabIndex = 53;
            label3.Text = "Default name:";
            label4.AutoSize = true;
            label4.Location = new Point(32, 124);
            label4.Name = "label4";
            label4.Size = new Size(74, 13);
            label4.TabIndex = 54;
            label4.Text = "Custom name:";
            label5.AutoSize = true;
            label5.Location = new Point(192, 124);
            label5.Name = "label5";
            label5.Size = new Size(50, 13);
            label5.TabIndex = 55;
            label5.Text = "Delimiter:";
            button1.Location = new Point(72, 189);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 56;
            button1.Text = "OK";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            button2.Location = new Point(154, 189);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 57;
            button2.Text = "Cancel";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            AutoScaleDimensions = new SizeF(6f, 13f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(292, 219);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(customConsoleDelimiter);
            Controls.Add(consoleName);
            Controls.Add(customConsoleName);
            Controls.Add(radioButton2);
            Controls.Add(radioButton1);
            Controls.Add(chatFontSizeCombo);
            Controls.Add(chatFontCombobox);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ColorChatSettings";
            ShowIcon = false;
            Text = "ColorChatSettings";
            FormClosing += ColorChatSettings_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}