namespace MCDzienny.Gui
{
	// Token: 0x02000307 RID: 775
	public partial class UpdateWindow : global::System.Windows.Forms.Form
	{
		// Token: 0x06001664 RID: 5732 RVA: 0x00086500 File Offset: 0x00084700
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x00086520 File Offset: 0x00084720
		private void InitializeComponent()
		{
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.cmdUpdate = new global::System.Windows.Forms.Button();
			this.listRevisions = new global::System.Windows.Forms.ListBox();
			this.chkAutoUpdate = new global::System.Windows.Forms.CheckBox();
			this.cmdDiscard = new global::System.Windows.Forms.Button();
			this.button1 = new global::System.Windows.Forms.Button();
			this.panel2 = new global::System.Windows.Forms.Panel();
			this.txtCountdown = new global::System.Windows.Forms.TextBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.chkNotify = new global::System.Windows.Forms.CheckBox();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			base.SuspendLayout();
			this.panel1.Controls.Add(this.cmdUpdate);
			this.panel1.Controls.Add(this.listRevisions);
			this.panel1.Location = new global::System.Drawing.Point(8, 7);
			this.panel1.Name = "panel1";
			this.panel1.Size = new global::System.Drawing.Size(148, 173);
			this.panel1.TabIndex = 0;
			this.cmdUpdate.Font = new global::System.Drawing.Font("Calibri", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.cmdUpdate.Location = new global::System.Drawing.Point(4, 70);
			this.cmdUpdate.Name = "cmdUpdate";
			this.cmdUpdate.Size = new global::System.Drawing.Size(82, 23);
			this.cmdUpdate.TabIndex = 2;
			this.cmdUpdate.Text = "Update";
			this.cmdUpdate.UseVisualStyleBackColor = true;
			this.listRevisions.Font = new global::System.Drawing.Font("Calibri", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.listRevisions.FormattingEnabled = true;
			this.listRevisions.Location = new global::System.Drawing.Point(89, 10);
			this.listRevisions.Name = "listRevisions";
			this.listRevisions.Size = new global::System.Drawing.Size(53, 147);
			this.listRevisions.TabIndex = 0;
			this.listRevisions.SelectedValueChanged += new global::System.EventHandler(this.listRevisions_SelectedValueChanged);
			this.chkAutoUpdate.AutoSize = true;
			this.chkAutoUpdate.Font = new global::System.Drawing.Font("Calibri", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.chkAutoUpdate.Location = new global::System.Drawing.Point(28, 4);
			this.chkAutoUpdate.Name = "chkAutoUpdate";
			this.chkAutoUpdate.Size = new global::System.Drawing.Size(133, 17);
			this.chkAutoUpdate.TabIndex = 1;
			this.chkAutoUpdate.Text = "Auto update to newest";
			this.chkAutoUpdate.UseVisualStyleBackColor = true;
			this.cmdDiscard.Font = new global::System.Drawing.Font("Calibri", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.cmdDiscard.Location = new global::System.Drawing.Point(111, 280);
			this.cmdDiscard.Name = "cmdDiscard";
			this.cmdDiscard.Size = new global::System.Drawing.Size(59, 23);
			this.cmdDiscard.TabIndex = 2;
			this.cmdDiscard.Text = "Discard";
			this.cmdDiscard.UseVisualStyleBackColor = true;
			this.cmdDiscard.Click += new global::System.EventHandler(this.cmdDiscard_Click);
			this.button1.Font = new global::System.Drawing.Font("Calibri", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.button1.Location = new global::System.Drawing.Point(35, 279);
			this.button1.Name = "button1";
			this.button1.Size = new global::System.Drawing.Size(59, 23);
			this.button1.TabIndex = 3;
			this.button1.Text = "Save";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new global::System.EventHandler(this.button1_Click);
			this.panel2.Controls.Add(this.txtCountdown);
			this.panel2.Controls.Add(this.label1);
			this.panel2.Controls.Add(this.chkNotify);
			this.panel2.Controls.Add(this.chkAutoUpdate);
			this.panel2.Location = new global::System.Drawing.Point(8, 189);
			this.panel2.Name = "panel2";
			this.panel2.Size = new global::System.Drawing.Size(209, 82);
			this.panel2.TabIndex = 4;
			this.txtCountdown.Location = new global::System.Drawing.Point(161, 45);
			this.txtCountdown.Name = "txtCountdown";
			this.txtCountdown.Size = new global::System.Drawing.Size(42, 20);
			this.txtCountdown.TabIndex = 4;
			this.label1.AutoSize = true;
			this.label1.Font = new global::System.Drawing.Font("Calibri", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label1.Location = new global::System.Drawing.Point(2, 49);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(158, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Time (in seconds) to countdown:";
			this.chkNotify.AutoSize = true;
			this.chkNotify.Font = new global::System.Drawing.Font("Calibri", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.chkNotify.Location = new global::System.Drawing.Point(28, 23);
			this.chkNotify.Name = "chkNotify";
			this.chkNotify.Size = new global::System.Drawing.Size(139, 17);
			this.chkNotify.TabIndex = 2;
			this.chkNotify.Text = "Notify in-game of restart";
			this.chkNotify.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(223, 318);
			base.Controls.Add(this.panel2);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.cmdDiscard);
			base.Controls.Add(this.panel1);
			base.Name = "UpdateWindow";
			this.Text = "Update";
			base.Load += new global::System.EventHandler(this.UpdateWindow_Load);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x04000AE1 RID: 2785
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000AE2 RID: 2786
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x04000AE3 RID: 2787
		private global::System.Windows.Forms.Button cmdUpdate;

		// Token: 0x04000AE4 RID: 2788
		private global::System.Windows.Forms.ListBox listRevisions;

		// Token: 0x04000AE5 RID: 2789
		private global::System.Windows.Forms.CheckBox chkAutoUpdate;

		// Token: 0x04000AE6 RID: 2790
		private global::System.Windows.Forms.Button cmdDiscard;

		// Token: 0x04000AE7 RID: 2791
		private global::System.Windows.Forms.Button button1;

		// Token: 0x04000AE8 RID: 2792
		private global::System.Windows.Forms.Panel panel2;

		// Token: 0x04000AE9 RID: 2793
		private global::System.Windows.Forms.CheckBox chkNotify;

		// Token: 0x04000AEA RID: 2794
		private global::System.Windows.Forms.TextBox txtCountdown;

		// Token: 0x04000AEB RID: 2795
		private global::System.Windows.Forms.Label label1;
	}
}
