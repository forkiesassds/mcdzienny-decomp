namespace MCDzienny.Gui
{
	// Token: 0x02000095 RID: 149
	public partial class WhiteListEditor : global::System.Windows.Forms.Form
	{
		// Token: 0x06000401 RID: 1025 RVA: 0x00016088 File Offset: 0x00014288
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x000160A8 File Offset: 0x000142A8
		private void InitializeComponent()
		{
			this.textBoxWhiteWords = new global::System.Windows.Forms.TextBox();
			this.buttonSave = new global::System.Windows.Forms.Button();
			this.label1 = new global::System.Windows.Forms.Label();
			base.SuspendLayout();
			this.textBoxWhiteWords.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.textBoxWhiteWords.Location = new global::System.Drawing.Point(12, 47);
			this.textBoxWhiteWords.Multiline = true;
			this.textBoxWhiteWords.Name = "textBoxWhiteWords";
			this.textBoxWhiteWords.ScrollBars = global::System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxWhiteWords.Size = new global::System.Drawing.Size(280, 302);
			this.textBoxWhiteWords.TabIndex = 0;
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
			this.label1.MaximumSize = new global::System.Drawing.Size(280, 0);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(273, 26);
			this.label1.TabIndex = 2;
			this.label1.Text = "The white list applies only for the High detection level. The words on this list are skipped by the bad words filter.";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(305, 383);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.buttonSave);
			base.Controls.Add(this.textBoxWhiteWords);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "WhiteListEditor";
			base.ShowIcon = false;
			this.Text = "White Words Editor";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000204 RID: 516
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000205 RID: 517
		private global::System.Windows.Forms.TextBox textBoxWhiteWords;

		// Token: 0x04000206 RID: 518
		private global::System.Windows.Forms.Button buttonSave;

		// Token: 0x04000207 RID: 519
		private global::System.Windows.Forms.Label label1;
	}
}
