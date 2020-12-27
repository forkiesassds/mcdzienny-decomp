namespace MCDzienny.Misc
{
	// Token: 0x020001C4 RID: 452
	public partial class PortIsClosed : global::System.Windows.Forms.Form
	{
		// Token: 0x06000CB0 RID: 3248 RVA: 0x00049188 File Offset: 0x00047388
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x000491A8 File Offset: 0x000473A8
		private void InitializeComponent()
		{
			this.pictureBox1 = new global::System.Windows.Forms.PictureBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.okButton = new global::System.Windows.Forms.Button();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
			base.SuspendLayout();
			this.pictureBox1.Location = new global::System.Drawing.Point(333, 47);
			this.pictureBox1.MaximumSize = new global::System.Drawing.Size(100, 100);
			this.pictureBox1.MinimumSize = new global::System.Drawing.Size(100, 100);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new global::System.Drawing.Size(100, 100);
			this.pictureBox1.SizeMode = global::System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.label1.AutoSize = true;
			this.label1.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 12f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 238);
			this.label1.Location = new global::System.Drawing.Point(24, 35);
			this.label1.MaximumSize = new global::System.Drawing.Size(250, 0);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(244, 120);
			this.label1.TabIndex = 1;
			this.label1.Text = "Port 25565 is not accessible. No one can connect to your server from the internet. You have to port forward in order to let people join. For help visit: www.mcdzienny.cba.pl";
			this.okButton.Location = new global::System.Drawing.Point(207, 189);
			this.okButton.Name = "okButton";
			this.okButton.Size = new global::System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 2;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new global::System.EventHandler(this.okButton_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(488, 224);
			base.Controls.Add(this.okButton);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.pictureBox1);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "PortIsClosed";
			base.ShowIcon = false;
			this.Text = "WARNING: Port is Closed";
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400069A RID: 1690
		private global::System.ComponentModel.IContainer components;

		// Token: 0x0400069B RID: 1691
		private global::System.Windows.Forms.PictureBox pictureBox1;

		// Token: 0x0400069C RID: 1692
		private global::System.Windows.Forms.Label label1;

		// Token: 0x0400069D RID: 1693
		private global::System.Windows.Forms.Button okButton;
	}
}
