namespace MCDzienny.Gui
{
	// Token: 0x02000094 RID: 148
	public partial class BadWordsEditor : global::System.Windows.Forms.Form
	{
		// Token: 0x060003FB RID: 1019 RVA: 0x00015D60 File Offset: 0x00013F60
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00015D80 File Offset: 0x00013F80
		private void InitializeComponent()
		{
			this.textBoxBadWords = new global::System.Windows.Forms.TextBox();
			this.buttonSave = new global::System.Windows.Forms.Button();
			this.label1 = new global::System.Windows.Forms.Label();
			base.SuspendLayout();
			this.textBoxBadWords.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.textBoxBadWords.Location = new global::System.Drawing.Point(12, 32);
			this.textBoxBadWords.Multiline = true;
			this.textBoxBadWords.Name = "textBoxBadWords";
			this.textBoxBadWords.ScrollBars = global::System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxBadWords.Size = new global::System.Drawing.Size(280, 317);
			this.textBoxBadWords.TabIndex = 0;
			this.buttonSave.Anchor = global::System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonSave.Location = new global::System.Drawing.Point(115, 355);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new global::System.Drawing.Size(75, 23);
			this.buttonSave.TabIndex = 1;
			this.buttonSave.Text = "Save";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new global::System.EventHandler(this.buttonSave_Click);
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(214, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Each bad word should be in a separate line.";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(305, 383);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.buttonSave);
			base.Controls.Add(this.textBoxBadWords);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "BadWordsEditor";
			base.ShowIcon = false;
			this.Text = "Bad Words Editor";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040001FF RID: 511
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000200 RID: 512
		private global::System.Windows.Forms.TextBox textBoxBadWords;

		// Token: 0x04000201 RID: 513
		private global::System.Windows.Forms.Button buttonSave;

		// Token: 0x04000202 RID: 514
		private global::System.Windows.Forms.Label label1;
	}
}
