namespace MCDzienny.Gui
{
	// Token: 0x02000093 RID: 147
	public partial class ColorChatSettings : global::System.Windows.Forms.Form
	{
		// Token: 0x060003F5 RID: 1013 RVA: 0x00015454 File Offset: 0x00013654
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00015474 File Offset: 0x00013674
		private void InitializeComponent()
		{
			this.chatFontCombobox = new global::System.Windows.Forms.ComboBox();
			this.chatFontSizeCombo = new global::System.Windows.Forms.ComboBox();
			this.radioButton1 = new global::System.Windows.Forms.RadioButton();
			this.radioButton2 = new global::System.Windows.Forms.RadioButton();
			this.customConsoleName = new global::System.Windows.Forms.TextBox();
			this.consoleName = new global::System.Windows.Forms.TextBox();
			this.customConsoleDelimiter = new global::System.Windows.Forms.TextBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.label2 = new global::System.Windows.Forms.Label();
			this.label3 = new global::System.Windows.Forms.Label();
			this.label4 = new global::System.Windows.Forms.Label();
			this.label5 = new global::System.Windows.Forms.Label();
			this.button1 = new global::System.Windows.Forms.Button();
			this.button2 = new global::System.Windows.Forms.Button();
			base.SuspendLayout();
			this.chatFontCombobox.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.chatFontCombobox.FormattingEnabled = true;
			this.chatFontCombobox.Location = new global::System.Drawing.Point(21, 38);
			this.chatFontCombobox.Name = "chatFontCombobox";
			this.chatFontCombobox.Size = new global::System.Drawing.Size(155, 21);
			this.chatFontCombobox.TabIndex = 44;
			this.chatFontCombobox.SelectedIndexChanged += new global::System.EventHandler(this.chatFontCombobox_SelectedIndexChanged);
			this.chatFontSizeCombo.FormattingEnabled = true;
			this.chatFontSizeCombo.Location = new global::System.Drawing.Point(192, 38);
			this.chatFontSizeCombo.Name = "chatFontSizeCombo";
			this.chatFontSizeCombo.Size = new global::System.Drawing.Size(74, 21);
			this.chatFontSizeCombo.TabIndex = 45;
			this.chatFontSizeCombo.SelectedIndexChanged += new global::System.EventHandler(this.chatFontSizeCombo_SelectedIndexChanged);
			this.radioButton1.AutoSize = true;
			this.radioButton1.Location = new global::System.Drawing.Point(15, 99);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new global::System.Drawing.Size(14, 13);
			this.radioButton1.TabIndex = 46;
			this.radioButton1.TabStop = true;
			this.radioButton1.UseVisualStyleBackColor = true;
			this.radioButton1.CheckedChanged += new global::System.EventHandler(this.radioButton1_CheckedChanged);
			this.radioButton2.AutoSize = true;
			this.radioButton2.Location = new global::System.Drawing.Point(15, 140);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new global::System.Drawing.Size(14, 13);
			this.radioButton2.TabIndex = 47;
			this.radioButton2.TabStop = true;
			this.radioButton2.UseVisualStyleBackColor = true;
			this.radioButton2.CheckedChanged += new global::System.EventHandler(this.radioButton2_CheckedChanged);
			this.customConsoleName.Location = new global::System.Drawing.Point(35, 140);
			this.customConsoleName.Name = "customConsoleName";
			this.customConsoleName.Size = new global::System.Drawing.Size(144, 20);
			this.customConsoleName.TabIndex = 48;
			this.customConsoleName.Validating += new global::System.ComponentModel.CancelEventHandler(this.customConsoleName_Validating);
			this.consoleName.Location = new global::System.Drawing.Point(35, 96);
			this.consoleName.Name = "consoleName";
			this.consoleName.Size = new global::System.Drawing.Size(144, 20);
			this.consoleName.TabIndex = 49;
			this.consoleName.Validating += new global::System.ComponentModel.CancelEventHandler(this.consoleName_Validating);
			this.customConsoleDelimiter.Location = new global::System.Drawing.Point(195, 140);
			this.customConsoleDelimiter.Name = "customConsoleDelimiter";
			this.customConsoleDelimiter.Size = new global::System.Drawing.Size(63, 20);
			this.customConsoleDelimiter.TabIndex = 50;
			this.customConsoleDelimiter.Validating += new global::System.ComponentModel.CancelEventHandler(this.textBox1_Validating);
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(18, 22);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(31, 13);
			this.label1.TabIndex = 51;
			this.label1.Text = "Font:";
			this.label2.AutoSize = true;
			this.label2.Location = new global::System.Drawing.Point(189, 22);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(30, 13);
			this.label2.TabIndex = 52;
			this.label2.Text = "Size:";
			this.label3.AutoSize = true;
			this.label3.Location = new global::System.Drawing.Point(32, 80);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(73, 13);
			this.label3.TabIndex = 53;
			this.label3.Text = "Default name:";
			this.label4.AutoSize = true;
			this.label4.Location = new global::System.Drawing.Point(32, 124);
			this.label4.Name = "label4";
			this.label4.Size = new global::System.Drawing.Size(74, 13);
			this.label4.TabIndex = 54;
			this.label4.Text = "Custom name:";
			this.label5.AutoSize = true;
			this.label5.Location = new global::System.Drawing.Point(192, 124);
			this.label5.Name = "label5";
			this.label5.Size = new global::System.Drawing.Size(50, 13);
			this.label5.TabIndex = 55;
			this.label5.Text = "Delimiter:";
			this.button1.Location = new global::System.Drawing.Point(72, 189);
			this.button1.Name = "button1";
			this.button1.Size = new global::System.Drawing.Size(75, 23);
			this.button1.TabIndex = 56;
			this.button1.Text = "OK";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new global::System.EventHandler(this.button1_Click);
			this.button2.Location = new global::System.Drawing.Point(154, 189);
			this.button2.Name = "button2";
			this.button2.Size = new global::System.Drawing.Size(75, 23);
			this.button2.TabIndex = 57;
			this.button2.Text = "Cancel";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new global::System.EventHandler(this.button2_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(292, 219);
			base.Controls.Add(this.button2);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.customConsoleDelimiter);
			base.Controls.Add(this.consoleName);
			base.Controls.Add(this.customConsoleName);
			base.Controls.Add(this.radioButton2);
			base.Controls.Add(this.radioButton1);
			base.Controls.Add(this.chatFontSizeCombo);
			base.Controls.Add(this.chatFontCombobox);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ColorChatSettings";
			base.ShowIcon = false;
			this.Text = "ColorChatSettings";
			base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.ColorChatSettings_FormClosing);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040001EF RID: 495
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040001F0 RID: 496
		private global::System.Windows.Forms.ComboBox chatFontCombobox;

		// Token: 0x040001F1 RID: 497
		private global::System.Windows.Forms.ComboBox chatFontSizeCombo;

		// Token: 0x040001F2 RID: 498
		private global::System.Windows.Forms.RadioButton radioButton1;

		// Token: 0x040001F3 RID: 499
		private global::System.Windows.Forms.RadioButton radioButton2;

		// Token: 0x040001F4 RID: 500
		private global::System.Windows.Forms.TextBox customConsoleName;

		// Token: 0x040001F5 RID: 501
		private global::System.Windows.Forms.TextBox consoleName;

		// Token: 0x040001F6 RID: 502
		private global::System.Windows.Forms.TextBox customConsoleDelimiter;

		// Token: 0x040001F7 RID: 503
		private global::System.Windows.Forms.Label label1;

		// Token: 0x040001F8 RID: 504
		private global::System.Windows.Forms.Label label2;

		// Token: 0x040001F9 RID: 505
		private global::System.Windows.Forms.Label label3;

		// Token: 0x040001FA RID: 506
		private global::System.Windows.Forms.Label label4;

		// Token: 0x040001FB RID: 507
		private global::System.Windows.Forms.Label label5;

		// Token: 0x040001FC RID: 508
		private global::System.Windows.Forms.Button button1;

		// Token: 0x040001FD RID: 509
		private global::System.Windows.Forms.Button button2;
	}
}
