using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MCDzienny.Plugins
{
    // Token: 0x020001E4 RID: 484
    public class GuiImportFromDat : UserControl
    {
        // Token: 0x04000715 RID: 1813
        private CheckBox checkBoxLoadAfterImport;

        // Token: 0x04000713 RID: 1811
        private CheckBox checkBoxSaveSameName;

        // Token: 0x04000710 RID: 1808
        private IContainer components;

        // Token: 0x04000714 RID: 1812
        private GroupBox groupBoxOptions;

        // Token: 0x04000712 RID: 1810
        private Label labelDropbox;

        // Token: 0x04000711 RID: 1809
        private Panel panelDropbox;

        // Token: 0x06000D77 RID: 3447 RVA: 0x0004C8E0 File Offset: 0x0004AAE0
        public GuiImportFromDat()
        {
            InitializeComponent();
        }

        // Token: 0x06000D78 RID: 3448 RVA: 0x0004C8F0 File Offset: 0x0004AAF0
        private void panelDropbox_DragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;
            var paths = (string[]) e.Data.GetData(DataFormats.FileDrop);
            ThreadPool.QueueUserWorkItem(delegate
            {
                var stringBuilder = new StringBuilder();
                var loadAfterImport = false;
                Invoke((Action) delegate { loadAfterImport = checkBoxLoadAfterImport.Checked; });
                var array = paths;
                foreach (var path in array)
                {
                    var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
                    if (ConvertDat.Load(new FileStream(path, FileMode.Open), fileNameWithoutExtension) != null)
                    {
                        if (loadAfterImport) new CmdLoad().Use(null, fileNameWithoutExtension);
                    }
                    else
                    {
                        stringBuilder.Append("Incorrect format. File: " + Path.GetFileName(path) + Environment.NewLine);
                    }
                }

                if (stringBuilder.Length > 0) MessageBox.Show(stringBuilder.ToString());
            });
        }

        // Token: 0x06000D79 RID: 3449 RVA: 0x0004C94C File Offset: 0x0004AB4C
        private void panelDropbox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
                return;
            }

            e.Effect = DragDropEffects.None;
        }

        // Token: 0x06000D7A RID: 3450 RVA: 0x0004C970 File Offset: 0x0004AB70
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        // Token: 0x06000D7B RID: 3451 RVA: 0x0004C990 File Offset: 0x0004AB90
        private void InitializeComponent()
        {
            panelDropbox = new Panel();
            labelDropbox = new Label();
            checkBoxSaveSameName = new CheckBox();
            groupBoxOptions = new GroupBox();
            checkBoxLoadAfterImport = new CheckBox();
            panelDropbox.SuspendLayout();
            groupBoxOptions.SuspendLayout();
            SuspendLayout();
            panelDropbox.AllowDrop = true;
            panelDropbox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelDropbox.BackColor = Color.PaleGoldenrod;
            panelDropbox.BorderStyle = BorderStyle.FixedSingle;
            panelDropbox.Controls.Add(labelDropbox);
            panelDropbox.Location = new Point(124, 87);
            panelDropbox.Name = "panelDropbox";
            panelDropbox.Size = new Size(250, 207);
            panelDropbox.TabIndex = 0;
            panelDropbox.DragDrop += panelDropbox_DragDrop;
            panelDropbox.DragEnter += panelDropbox_DragEnter;
            labelDropbox.BackColor = Color.FromArgb(220, 255, 220);
            labelDropbox.Dock = DockStyle.Fill;
            labelDropbox.Font = new Font("Microsoft Sans Serif", 14.25f, FontStyle.Italic, GraphicsUnit.Point, 238);
            labelDropbox.Location = new Point(0, 0);
            labelDropbox.Margin = new Padding(5);
            labelDropbox.Name = "labelDropbox";
            labelDropbox.Size = new Size(248, 205);
            labelDropbox.TabIndex = 0;
            labelDropbox.Text = "Drag and drop a dat file or files here";
            labelDropbox.TextAlign = ContentAlignment.MiddleCenter;
            checkBoxSaveSameName.AutoSize = true;
            checkBoxSaveSameName.Checked = true;
            checkBoxSaveSameName.CheckState = CheckState.Checked;
            checkBoxSaveSameName.Enabled = false;
            checkBoxSaveSameName.Location = new Point(6, 19);
            checkBoxSaveSameName.Name = "checkBoxSaveSameName";
            checkBoxSaveSameName.Size = new Size(157, 17);
            checkBoxSaveSameName.TabIndex = 1;
            checkBoxSaveSameName.Text = "Save under the same name";
            checkBoxSaveSameName.UseVisualStyleBackColor = true;
            groupBoxOptions.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
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
            checkBoxLoadAfterImport.CheckState = CheckState.Checked;
            checkBoxLoadAfterImport.Location = new Point(6, 42);
            checkBoxLoadAfterImport.Name = "checkBoxLoadAfterImport";
            checkBoxLoadAfterImport.Size = new Size(163, 17);
            checkBoxLoadAfterImport.TabIndex = 2;
            checkBoxLoadAfterImport.Text = "Load a map(s) after importing";
            checkBoxLoadAfterImport.UseVisualStyleBackColor = true;
            AutoScaleDimensions = new SizeF(6f, 13f);
            AutoScaleMode = AutoScaleMode.Font;
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