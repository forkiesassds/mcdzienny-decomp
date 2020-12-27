using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MCDzienny.Settings;

namespace MCDzienny.Gui
{
    // Token: 0x02000093 RID: 147
    public partial class ColorChatSettings : Form
    {
        // Token: 0x040001EB RID: 491
        private string chatFont;

        // Token: 0x040001EC RID: 492
        private float chatFontSize;

        // Token: 0x040001E9 RID: 489
        private string cName;

        // Token: 0x040001EA RID: 490
        private string customCName;

        // Token: 0x040001ED RID: 493
        private string customDelimiter;

        // Token: 0x040001EE RID: 494
        private bool doNotRestore;

        // Token: 0x040001E8 RID: 488
        private bool useCustomName;

        // Token: 0x060003E4 RID: 996 RVA: 0x00014E8C File Offset: 0x0001308C
        public ColorChatSettings()
        {
            InitializeComponent();
            InitUpdateControls();
            CacheSettings();
        }

        // Token: 0x060003E5 RID: 997 RVA: 0x00014EA8 File Offset: 0x000130A8
        public void ShowAt(Point location)
        {
            StartPosition = FormStartPosition.Manual;
            var x = location.X - Width / 2;
            var y = location.Y - Height / 2;
            Location = new Point(x, y);
            base.Show();
        }

        // Token: 0x060003E6 RID: 998 RVA: 0x00014EF4 File Offset: 0x000130F4
        private void InitUpdateControls()
        {
            chatFontSizeCombo.Items.AddRange(new object[]
            {
                8,
                9,
                10,
                11,
                12,
                14,
                16,
                18,
                20,
                22,
                24,
                26,
                28,
                36,
                48,
                72
            });
            radioButton2.Checked = GeneralSettings.All.UseCustomName;
            radioButton1.Checked = !radioButton2.Checked;
            customConsoleName.Text = GeneralSettings.All.CustomConsoleName;
            chatFontCombobox.Items.AddRange(AvailableFontsNames().ToArray());
            var font = Window.thisWindow.GetFont();
            var name = font.FontFamily.Name;
            var num = chatFontCombobox.FindString(name);
            if (num != -1) chatFontCombobox.SelectedIndex = num;
            var size = font.Size;
            chatFontSizeCombo.Text = size.ToString();
            consoleName.Text = Server.ConsoleName;
            customConsoleDelimiter.Text = GeneralSettings.All.CustomConsoleNameDelimiter;
        }

        // Token: 0x060003E7 RID: 999 RVA: 0x000150A4 File Offset: 0x000132A4
        private void CacheSettings()
        {
            var font = Window.thisWindow.GetFont();
            cName = Server.ConsoleName;
            customCName = GeneralSettings.All.CustomConsoleName;
            chatFont = font.FontFamily.Name;
            chatFontSize = font.Size;
            useCustomName = GeneralSettings.All.UseCustomName;
            customDelimiter = GeneralSettings.All.CustomConsoleNameDelimiter;
        }

        // Token: 0x060003E8 RID: 1000 RVA: 0x00015114 File Offset: 0x00013314
        private List<string> AvailableFontsNames()
        {
            var list = new List<string>();
            foreach (var fontFamily in FontFamily.Families) list.Add(fontFamily.Name);
            return list;
        }

        // Token: 0x060003E9 RID: 1001 RVA: 0x0001514C File Offset: 0x0001334C
        private void chatFontSizeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var font = Window.thisWindow.GetFont();
            if (chatFontSizeCombo.SelectedIndex != -1)
                Window.thisWindow.SetFont(new Font(font.FontFamily, float.Parse(chatFontSizeCombo.Text)));
        }

        // Token: 0x060003EA RID: 1002 RVA: 0x00015198 File Offset: 0x00013398
        private void chatFontSizeCombo_Validating(object sender, CancelEventArgs e)
        {
            float num;
            if (!float.TryParse(chatFontSizeCombo.Text, out num))
            {
                chatFontSizeCombo.Text = Window.thisWindow.GetFont().Size.ToString();
                e.Cancel = true;
                return;
            }

            GeneralSettings.All.ChatFontSize = num;
        }

        // Token: 0x060003EB RID: 1003 RVA: 0x000151F0 File Offset: 0x000133F0
        private void consoleName_Validating(object sender, CancelEventArgs e)
        {
            if (!ServerProperties.ValidString(consoleName.Text, "%![]:.,{}~-+()?_/\\ "))
            {
                consoleName.Text = Server.ConsoleName;
                e.Cancel = true;
                return;
            }

            Server.ConsoleName = consoleName.Text;
        }

        // Token: 0x060003EC RID: 1004 RVA: 0x0001523C File Offset: 0x0001343C
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                customConsoleName.ReadOnly = false;
                GeneralSettings.All.UseCustomName = true;
                return;
            }

            customConsoleName.ReadOnly = true;
        }

        // Token: 0x060003ED RID: 1005 RVA: 0x00015270 File Offset: 0x00013470
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                consoleName.ReadOnly = false;
                GeneralSettings.All.UseCustomName = false;
                return;
            }

            consoleName.ReadOnly = true;
        }

        // Token: 0x060003EE RID: 1006 RVA: 0x000152A4 File Offset: 0x000134A4
        private void customConsoleName_Validating(object sender, CancelEventArgs e)
        {
            if (!ServerProperties.ValidString(customConsoleName.Text, "%![]:.,{}~-+()?_/\\ "))
            {
                customConsoleName.Text = GeneralSettings.All.CustomConsoleName;
                e.Cancel = true;
                return;
            }

            GeneralSettings.All.CustomConsoleName = customConsoleName.Text;
        }

        // Token: 0x060003EF RID: 1007 RVA: 0x000152FC File Offset: 0x000134FC
        private void chatFontCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chatFontCombobox.SelectedIndex == -1) return;
            var font = Window.thisWindow.GetFont();
            var text = chatFontCombobox.Items[chatFontCombobox.SelectedIndex].ToString();
            var font2 = new Font(text, font.Size);
            Window.thisWindow.SetFont(font2);
            GeneralSettings.All.ChatFontFamily = text;
        }

        // Token: 0x060003F0 RID: 1008 RVA: 0x00015368 File Offset: 0x00013568
        private void RestoreSettings()
        {
            GeneralSettings.All.UseCustomName = useCustomName;
            Window.thisWindow.SetFont(new Font(chatFont, chatFontSize));
            Server.ConsoleName = cName;
            GeneralSettings.All.CustomConsoleName = customCName;
            GeneralSettings.All.CustomConsoleNameDelimiter = customDelimiter;
        }

        // Token: 0x060003F1 RID: 1009 RVA: 0x000153CC File Offset: 0x000135CC
        private void button1_Click(object sender, EventArgs e)
        {
            doNotRestore = true;
            Close();
        }

        // Token: 0x060003F2 RID: 1010 RVA: 0x000153DC File Offset: 0x000135DC
        private void button2_Click(object sender, EventArgs e)
        {
            RestoreSettings();
            Close();
        }

        // Token: 0x060003F3 RID: 1011 RVA: 0x000153EC File Offset: 0x000135EC
        private void ColorChatSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!doNotRestore) RestoreSettings();
        }

        // Token: 0x060003F4 RID: 1012 RVA: 0x000153FC File Offset: 0x000135FC
        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (!ServerProperties.ValidString(consoleName.Text, "%![]:.,{}~-+()?_/\\ "))
            {
                customConsoleDelimiter.Text = GeneralSettings.All.CustomConsoleNameDelimiter;
                e.Cancel = true;
                return;
            }

            GeneralSettings.All.CustomConsoleNameDelimiter = customConsoleDelimiter.Text;
        }
    }
}