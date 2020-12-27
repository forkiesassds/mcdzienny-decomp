namespace MCDzienny.Gui
{
	// Token: 0x02000096 RID: 150
	public partial class PopUpMessage : global::System.Windows.Forms.Form
	{
		// Token: 0x06000406 RID: 1030 RVA: 0x00016388 File Offset: 0x00014588
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x000163A8 File Offset: 0x000145A8
		private void InitializeComponent()
		{
			this.mainTextBox = new global::System.Windows.Forms.TextBox();
			this.label = new global::System.Windows.Forms.Label();
			this.button1 = new global::System.Windows.Forms.Button();
			base.SuspendLayout();
			this.mainTextBox.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.mainTextBox.BackColor = global::System.Drawing.Color.White;
			this.mainTextBox.CausesValidation = false;
			this.mainTextBox.Location = new global::System.Drawing.Point(12, 30);
			this.mainTextBox.Multiline = true;
			this.mainTextBox.Name = "mainTextBox";
			this.mainTextBox.ReadOnly = true;
			this.mainTextBox.ScrollBars = global::System.Windows.Forms.ScrollBars.Vertical;
			this.mainTextBox.Size = new global::System.Drawing.Size(483, 231);
			this.mainTextBox.TabIndex = 0;
			this.label.AutoSize = true;
			this.label.Location = new global::System.Drawing.Point(13, 11);
			this.label.Name = "label";
			this.label.Size = new global::System.Drawing.Size(107, 13);
			this.label.TabIndex = 1;
			this.label.Text = "Received Messages:";
			this.button1.Anchor = global::System.Windows.Forms.AnchorStyles.Bottom;
			this.button1.Location = new global::System.Drawing.Point(221, 267);
			this.button1.Name = "button1";
			this.button1.Size = new global::System.Drawing.Size(65, 23);
			this.button1.TabIndex = 2;
			this.button1.Text = "OK";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new global::System.EventHandler(this.button1_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(507, 300);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.label);
			base.Controls.Add(this.mainTextBox);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "PopUpMessage";
			base.ShowIcon = false;
			this.Text = "News";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000208 RID: 520
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000209 RID: 521
		private global::System.Windows.Forms.TextBox mainTextBox;

		// Token: 0x0400020A RID: 522
		private global::System.Windows.Forms.Label label;

		// Token: 0x0400020B RID: 523
		private global::System.Windows.Forms.Button button1;
	}
}
