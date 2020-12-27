namespace MCDzienny.Gui
{
	// Token: 0x02000157 RID: 343
	public partial class ZombieProperties : global::System.Windows.Forms.Form
	{
		// Token: 0x060009E7 RID: 2535 RVA: 0x00034AD0 File Offset: 0x00032CD0
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x00034AF0 File Offset: 0x00032CF0
		private void InitializeComponent()
		{
			base.SuspendLayout();
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(451, 457);
			base.Name = "ZombieProperties";
			this.Text = "ZombieProperties";
			base.FormClosed += new global::System.Windows.Forms.FormClosedEventHandler(this.ZombieProperties_FormClosed);
			base.ResumeLayout(false);
		}

		// Token: 0x04000472 RID: 1138
		private global::System.ComponentModel.IContainer components;
	}
}
