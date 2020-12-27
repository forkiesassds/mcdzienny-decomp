namespace MCDzienny.Misc
{
	// Token: 0x020001C5 RID: 453
	public partial class PortIsOpen : global::System.Windows.Forms.Form
	{
		// Token: 0x06000CB5 RID: 3253 RVA: 0x000494D0 File Offset: 0x000476D0
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x000494F0 File Offset: 0x000476F0
		private void InitializeComponent()
		{
			this.pictureBox1 = new global::System.Windows.Forms.PictureBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.okButton = new global::System.Windows.Forms.Button();
			this.label2 = new global::System.Windows.Forms.Label();
			this.checkBox1 = new global::System.Windows.Forms.CheckBox();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
			base.SuspendLayout();
			this.pictureBox1.Location = new global::System.Drawing.Point(333, 36);
			this.pictureBox1.MaximumSize = new global::System.Drawing.Size(100, 100);
			this.pictureBox1.MinimumSize = new global::System.Drawing.Size(100, 100);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new global::System.Drawing.Size(100, 100);
			this.pictureBox1.SizeMode = global::System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.label1.AutoSize = true;
			this.label1.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 12f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 238);
			this.label1.Location = new global::System.Drawing.Point(22, 76);
			this.label1.MaximumSize = new global::System.Drawing.Size(250, 0);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(250, 60);
			this.label1.TabIndex = 1;
			this.label1.Text = "Port 25565 is open. It means that other people can connect to your server.";
			this.okButton.Location = new global::System.Drawing.Point(207, 215);
			this.okButton.Name = "okButton";
			this.okButton.Size = new global::System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 2;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new global::System.EventHandler(this.okButton_Click);
			this.label2.AutoSize = true;
			this.label2.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 12f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 238);
			this.label2.Location = new global::System.Drawing.Point(22, 36);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(82, 20);
			this.label2.TabIndex = 3;
			this.label2.Text = "Success!";
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new global::System.Drawing.Point(26, 178);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new global::System.Drawing.Size(176, 17);
			this.checkBox1.TabIndex = 4;
			this.checkBox1.Text = "Don't show this message again.";
			this.checkBox1.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(488, 252);
			base.Controls.Add(this.checkBox1);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.okButton);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.pictureBox1);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "PortIsOpen";
			base.ShowIcon = false;
			this.Text = "Port is Open!";
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400069E RID: 1694
		private global::System.ComponentModel.IContainer components;

		// Token: 0x0400069F RID: 1695
		private global::System.Windows.Forms.PictureBox pictureBox1;

		// Token: 0x040006A0 RID: 1696
		private global::System.Windows.Forms.Label label1;

		// Token: 0x040006A1 RID: 1697
		private global::System.Windows.Forms.Button okButton;

		// Token: 0x040006A2 RID: 1698
		private global::System.Windows.Forms.Label label2;

		// Token: 0x040006A3 RID: 1699
		private global::System.Windows.Forms.CheckBox checkBox1;
	}
}
