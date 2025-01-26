using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MCDzienny.Gui
{
    public class WhiteListEditor : Form
    {
        readonly string whiteWordsPath = "text/whitewords.txt";

        Button buttonSave;

        IContainer components;

        Label label1;

        TextBox textBoxWhiteWords;

        public WhiteListEditor()
        {
            InitializeComponent();
            LoadList();
        }

        void LoadList()
        {
            try
            {
                if (!File.Exists(whiteWordsPath))
                {
                    File.Create(whiteWordsPath).Close();
                }
                textBoxWhiteWords.Text = File.ReadAllText(whiteWordsPath);
                textBoxWhiteWords.Select(0, 0);
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        void SaveList()
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

        void buttonSave_Click(object sender, EventArgs e)
        {
            SaveList();
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
            textBoxWhiteWords = new TextBox();
            buttonSave = new Button();
            label1 = new Label();
            SuspendLayout();
            textBoxWhiteWords.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxWhiteWords.Location = new Point(12, 47);
            textBoxWhiteWords.Multiline = true;
            textBoxWhiteWords.Name = "textBoxWhiteWords";
            textBoxWhiteWords.ScrollBars = ScrollBars.Vertical;
            textBoxWhiteWords.Size = new Size(280, 302);
            textBoxWhiteWords.TabIndex = 0;
            buttonSave.Anchor = AnchorStyles.Bottom;
            buttonSave.Location = new Point(115, 355);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(75, 23);
            buttonSave.TabIndex = 1;
            buttonSave.Text = "Save";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += buttonSave_Click;
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.MaximumSize = new Size(280, 0);
            label1.Name = "label1";
            label1.Size = new Size(273, 26);
            label1.TabIndex = 2;
            label1.Text = "The white list applies only for the High detection level. The words on this list are skipped by the bad words filter.";
            AutoScaleDimensions = new SizeF(6f, 13f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(305, 383);
            Controls.Add(label1);
            Controls.Add(buttonSave);
            Controls.Add(textBoxWhiteWords);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "WhiteListEditor";
            ShowIcon = false;
            Text = "White Words Editor";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}