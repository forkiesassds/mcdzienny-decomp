using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MCDzienny.Gui
{
    public class BadWordsEditor : Form
    {
        readonly string badWordsPath = "text/badwords.txt";

        Button buttonSave;

        IContainer components;

        Label label1;

        TextBox textBoxBadWords;

        public BadWordsEditor()
        {
            InitializeComponent();
            LoadList();
        }

        void LoadList()
        {
            try
            {
                if (!File.Exists(badWordsPath))
                {
                    File.Create(badWordsPath).Close();
                }
                textBoxBadWords.Text = File.ReadAllText(badWordsPath);
                textBoxBadWords.Select(0, 0);
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
                File.WriteAllText(badWordsPath, textBoxBadWords.Text);
                Server.chatFilter.LoadBadWords();
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
            textBoxBadWords = new TextBox();
            buttonSave = new Button();
            label1 = new Label();
            SuspendLayout();
            textBoxBadWords.Anchor = (AnchorStyles)15;
            textBoxBadWords.Location = new Point(12, 32);
            textBoxBadWords.Multiline = true;
            textBoxBadWords.Name = "textBoxBadWords";
            textBoxBadWords.ScrollBars = (ScrollBars)2;
            textBoxBadWords.Size = new Size(280, 317);
            textBoxBadWords.TabIndex = 0;
            buttonSave.Anchor = (AnchorStyles)2;
            buttonSave.Location = new Point(115, 355);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(75, 23);
            buttonSave.TabIndex = 1;
            buttonSave.Text = "Save";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += buttonSave_Click;
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(214, 13);
            label1.TabIndex = 2;
            label1.Text = "Each bad word should be in a separate line.";
            AutoScaleDimensions = new SizeF(6f, 13f);
            AutoScaleMode = (AutoScaleMode)1;
            ClientSize = new Size(305, 383);
            Controls.Add(label1);
            Controls.Add(buttonSave);
            Controls.Add(textBoxBadWords);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "BadWordsEditor";
            ShowIcon = false;
            Text = "Bad Words Editor";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}