namespace Updater
{
	// Token: 0x02000005 RID: 5
	public partial class UpdaterWindow : global::System.Windows.Forms.Form
	{
		// Token: 0x06000009 RID: 9 RVA: 0x0000211C File Offset: 0x0000031C
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002154 File Offset: 0x00000354
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::Updater.UpdaterWindow));
			this.logBox = new global::System.Windows.Forms.RichTextBox();
			this.chkStartAfterUpdate = new global::System.Windows.Forms.CheckBox();
			this.btnUpdate = new global::System.Windows.Forms.Button();
			base.SuspendLayout();
			this.logBox.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 238);
			this.logBox.Location = new global::System.Drawing.Point(12, 12);
			this.logBox.Name = "logBox";
			this.logBox.Size = new global::System.Drawing.Size(394, 238);
			this.logBox.TabIndex = 0;
			this.logBox.Text = "";
			this.chkStartAfterUpdate.AutoSize = true;
			this.chkStartAfterUpdate.Location = new global::System.Drawing.Point(12, 282);
			this.chkStartAfterUpdate.Name = "chkStartAfterUpdate";
			this.chkStartAfterUpdate.Size = new global::System.Drawing.Size(166, 17);
			this.chkStartAfterUpdate.TabIndex = 1;
			this.chkStartAfterUpdate.Text = "Start MCDzienny after update";
			this.chkStartAfterUpdate.UseVisualStyleBackColor = true;
			this.chkStartAfterUpdate.Checked = true;
			this.btnUpdate.Location = new global::System.Drawing.Point(270, 278);
			this.btnUpdate.Name = "btnUpdate";
			this.btnUpdate.Size = new global::System.Drawing.Size(136, 23);
			this.btnUpdate.TabIndex = 2;
			this.btnUpdate.Text = "Check For Updates";
			this.btnUpdate.UseVisualStyleBackColor = true;
			this.btnUpdate.Click += new global::System.EventHandler(this.button1_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(418, 328);
			base.Controls.Add(this.btnUpdate);
			base.Controls.Add(this.chkStartAfterUpdate);
			base.Controls.Add(this.logBox);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "UpdaterWindow";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Updater";
			base.Load += new global::System.EventHandler(this.UpdaterWindow_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000004 RID: 4
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000005 RID: 5
		private global::System.Windows.Forms.RichTextBox logBox;

		// Token: 0x04000006 RID: 6
		private global::System.Windows.Forms.CheckBox chkStartAfterUpdate;

		// Token: 0x04000007 RID: 7
		private global::System.Windows.Forms.Button btnUpdate;
	}
}
