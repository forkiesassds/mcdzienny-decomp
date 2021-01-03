namespace MCDzienny.Misc
{
	// Token: 0x020001C7 RID: 455
	public partial class SplashScreen : global::System.Windows.Forms.Form
	{
		// Token: 0x06000CBE RID: 3262 RVA: 0x00049A3C File Offset: 0x00047C3C
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x00049A5C File Offset: 0x00047C5C
		private void InitializeComponent()
		{
			base.SuspendLayout();
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = global::System.Drawing.Color.White;
			this.BackgroundImage = global::MCDzienny.Properties.Resources.splashScreen;
			base.ClientSize = new global::System.Drawing.Size(520, 260);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.None;
			base.Name = "SplashScreen";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "SplashScreen";
			base.ResumeLayout(false);
		}

		// Token: 0x040006A5 RID: 1701
		private global::System.ComponentModel.IContainer components;
	}
}
