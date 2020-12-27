using System;
using System.IO;
using System.Windows.Forms;

namespace MCDzienny.Gui
{
    // Token: 0x02000094 RID: 148
    public partial class BadWordsEditor : Form
    {
        // Token: 0x040001FE RID: 510
        private readonly string badWordsPath = "text/badwords.txt";

        // Token: 0x060003F7 RID: 1015 RVA: 0x00015C80 File Offset: 0x00013E80
        public BadWordsEditor()
        {
            InitializeComponent();
            LoadList();
        }

        // Token: 0x060003F8 RID: 1016 RVA: 0x00015CA0 File Offset: 0x00013EA0
        private void LoadList()
        {
            try
            {
                if (!File.Exists(badWordsPath)) File.Create(badWordsPath).Close();
                textBoxBadWords.Text = File.ReadAllText(badWordsPath);
                textBoxBadWords.Select(0, 0);
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        // Token: 0x060003F9 RID: 1017 RVA: 0x00015D08 File Offset: 0x00013F08
        private void SaveList()
        {
            try
            {
                File.WriteAllText(badWordsPath, textBoxBadWords.Text);
                Server.chatFilter.LoadBadWords();
                Close();
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        // Token: 0x060003FA RID: 1018 RVA: 0x00015D58 File Offset: 0x00013F58
        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveList();
        }
    }
}