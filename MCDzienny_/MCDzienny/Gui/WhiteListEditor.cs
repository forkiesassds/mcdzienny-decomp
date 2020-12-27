using System;
using System.IO;
using System.Windows.Forms;

namespace MCDzienny.Gui
{
    // Token: 0x02000095 RID: 149
    public partial class WhiteListEditor : Form
    {
        // Token: 0x04000203 RID: 515
        private readonly string whiteWordsPath = "text/whitewords.txt";

        // Token: 0x060003FD RID: 1021 RVA: 0x00015FA8 File Offset: 0x000141A8
        public WhiteListEditor()
        {
            InitializeComponent();
            LoadList();
        }

        // Token: 0x060003FE RID: 1022 RVA: 0x00015FC8 File Offset: 0x000141C8
        private void LoadList()
        {
            try
            {
                if (!File.Exists(whiteWordsPath)) File.Create(whiteWordsPath).Close();
                textBoxWhiteWords.Text = File.ReadAllText(whiteWordsPath);
                textBoxWhiteWords.Select(0, 0);
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        // Token: 0x060003FF RID: 1023 RVA: 0x00016030 File Offset: 0x00014230
        private void SaveList()
        {
            try
            {
                File.WriteAllText(whiteWordsPath, textBoxWhiteWords.Text);
                Server.chatFilter.LoadWhiteWords();
                Close();
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        // Token: 0x06000400 RID: 1024 RVA: 0x00016080 File Offset: 0x00014280
        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveList();
        }
    }
}