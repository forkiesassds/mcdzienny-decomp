using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MCDzienny.Plugins
{
    public class GuiImportFromDat : UserControl
    {

        CheckBox checkBoxLoadAfterImport;

        CheckBox checkBoxSaveSameName;
        IContainer components;

        GroupBox groupBoxOptions;

        Label labelDropbox;

        Panel panelDropbox;

        public GuiImportFromDat()
        {
            InitializeComponent();
        }

        void panelDropbox_DragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                return;
            }
            string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop);
            ThreadPool.QueueUserWorkItem(delegate
            {
                //IL_00aa: Unknown result type (might be due to invalid IL or missing references)
                StringBuilder stringBuilder = new StringBuilder();
                bool loadAfterImport = false;
                Invoke((Action)delegate { loadAfterImport = checkBoxLoadAfterImport.Checked; });
                string[] array = paths;
                foreach (string path in array)
                {
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
                    if (ConvertDat.Load(new FileStream(path, FileMode.Open), fileNameWithoutExtension) != null)
                    {
                        if (loadAfterImport)
                        {
                            new CmdLoad().Use(null, fileNameWithoutExtension);
                        }
                    }
                    else
                    {
                        stringBuilder.Append("Incorrect format. File: " + Path.GetFileName(path) + Environment.NewLine);
                    }
                }
                if (stringBuilder.Length > 0)
                {
                    MessageBox.Show(stringBuilder.ToString());
                }
            });
        }

        void panelDropbox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = (DragDropEffects)1;
            }
            else
            {
                e.Effect = 0;
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
            //IL_00f5: Unknown result type (might be due to invalid IL or missing references)
            //IL_00ff: Expected O, but got Unknown
            //IL_010c: Unknown result type (might be due to invalid IL or missing references)
            //IL_0116: Expected O, but got Unknown
            //IL_0158: Unknown result type (might be due to invalid IL or missing references)
            //IL_0162: Expected O, but got Unknown
            //IL_017b: Unknown result type (might be due to invalid IL or missing references)
            panelDropbox = new Panel();
            labelDropbox = new Label();
            checkBoxSaveSameName = new CheckBox();
            groupBoxOptions = new GroupBox();
            checkBoxLoadAfterImport = new CheckBox();
            panelDropbox.SuspendLayout();
            groupBoxOptions.SuspendLayout();
            SuspendLayout();
            panelDropbox.AllowDrop = true;
            panelDropbox.Anchor = (AnchorStyles)15;
            panelDropbox.BackColor = Color.PaleGoldenrod;
            panelDropbox.BorderStyle = (BorderStyle)1;
            panelDropbox.Controls.Add(labelDropbox);
            panelDropbox.Location = new Point(124, 87);
            panelDropbox.Name = "panelDropbox";
            panelDropbox.Size = new Size(250, 207);
            panelDropbox.TabIndex = 0;
            panelDropbox.DragDrop += panelDropbox_DragDrop;
            panelDropbox.DragEnter += panelDropbox_DragEnter;
            labelDropbox.BackColor = Color.FromArgb(220, 255, 220);
            labelDropbox.Dock = (DockStyle)5;
            labelDropbox.Font = new Font("Microsoft Sans Serif", 14.25f, (FontStyle)2, (GraphicsUnit)3, 238);
            labelDropbox.Location = new Point(0, 0);
            labelDropbox.Margin = new Padding(5);
            labelDropbox.Name = "labelDropbox";
            labelDropbox.Size = new Size(248, 205);
            labelDropbox.TabIndex = 0;
            labelDropbox.Text = "Drag and drop a dat file or files here";
            labelDropbox.TextAlign = (ContentAlignment)32;
            checkBoxSaveSameName.AutoSize = true;
            checkBoxSaveSameName.Checked = true;
            checkBoxSaveSameName.CheckState = (CheckState)1;
            checkBoxSaveSameName.Enabled = false;
            checkBoxSaveSameName.Location = new Point(6, 19);
            checkBoxSaveSameName.Name = "checkBoxSaveSameName";
            checkBoxSaveSameName.Size = new Size(157, 17);
            checkBoxSaveSameName.TabIndex = 1;
            checkBoxSaveSameName.Text = "Save under the same name";
            checkBoxSaveSameName.UseVisualStyleBackColor = true;
            groupBoxOptions.Anchor = (AnchorStyles)14;
            groupBoxOptions.Controls.Add(checkBoxLoadAfterImport);
            groupBoxOptions.Controls.Add(checkBoxSaveSameName);
            groupBoxOptions.Location = new Point(31, 317);
            groupBoxOptions.Name = "groupBoxOptions";
            groupBoxOptions.Size = new Size(478, 127);
            groupBoxOptions.TabIndex = 2;
            groupBoxOptions.TabStop = false;
            groupBoxOptions.Text = "Options:";
            checkBoxLoadAfterImport.AutoSize = true;
            checkBoxLoadAfterImport.Checked = true;
            checkBoxLoadAfterImport.CheckState = (CheckState)1;
            checkBoxLoadAfterImport.Location = new Point(6, 42);
            checkBoxLoadAfterImport.Name = "checkBoxLoadAfterImport";
            checkBoxLoadAfterImport.Size = new Size(163, 17);
            checkBoxLoadAfterImport.TabIndex = 2;
            checkBoxLoadAfterImport.Text = "Load a map(s) after importing";
            checkBoxLoadAfterImport.UseVisualStyleBackColor = true;
            AutoScaleDimensions = new SizeF(6f, 13f);
            AutoScaleMode = (AutoScaleMode)1;
            BackColor = Color.White;
            Controls.Add(groupBoxOptions);
            Controls.Add(panelDropbox);
            Name = "GuiImportFromDat";
            Size = new Size(540, 462);
            panelDropbox.ResumeLayout(false);
            groupBoxOptions.ResumeLayout(false);
            groupBoxOptions.PerformLayout();
            ResumeLayout(false);
        }
    }
}