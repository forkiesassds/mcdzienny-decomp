namespace MCDzienny.Gui
{
	// Token: 0x02000155 RID: 341
	public partial class LavaProperties : global::System.Windows.Forms.Form
	{
		// Token: 0x060009E1 RID: 2529 RVA: 0x00034A00 File Offset: 0x00032C00
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x00034A20 File Offset: 0x00032C20
		private void InitializeComponent()
		{
			base.SuspendLayout();
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(430, 498);
			base.Name = "LavaProperties";
			base.ShowIcon = false;
			this.Text = "Lava Survival Settings";
			base.FormClosed += new global::System.Windows.Forms.FormClosedEventHandler(this.LavaProperties_Unload);
			base.Load += new global::System.EventHandler(this.LavaProperties_Load);
			base.ResumeLayout(false);
		}

		// Token: 0x04000471 RID: 1137
		private global::System.ComponentModel.IContainer components;
	}
}
