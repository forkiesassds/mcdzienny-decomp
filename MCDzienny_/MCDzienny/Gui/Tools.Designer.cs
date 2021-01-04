namespace MCDzienny.Gui
{
	// Token: 0x0200009A RID: 154
	public partial class Tools : global::System.Windows.Forms.Form
	{
		// Token: 0x06000421 RID: 1057 RVA: 0x00017504 File Offset: 0x00015704
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00017524 File Offset: 0x00015724
		private void InitializeComponent()
		{
            this.comboBoxWidth = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.textMaxMaps = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textRamRequired = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxDepth = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxHeight = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxWidth
            // 
            this.comboBoxWidth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxWidth.FormattingEnabled = true;
            this.comboBoxWidth.Items.AddRange(new object[] {
            "16",
            "32",
            "64",
            "128",
            "256",
            "512",
            "1024",
            "2048",
            "4096",
            "8192"});
            this.comboBoxWidth.Location = new System.Drawing.Point(28, 71);
            this.comboBoxWidth.Name = "comboBoxWidth";
            this.comboBoxWidth.Size = new System.Drawing.Size(70, 21);
            this.comboBoxWidth.TabIndex = 0;
            this.comboBoxWidth.SelectedIndexChanged += new System.EventHandler(this.comboBoxWidth_SelectedIndexChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(2, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(289, 255);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.textMaxMaps);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.textRamRequired);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.comboBoxDepth);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.comboBoxHeight);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.comboBoxWidth);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(281, 229);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Map RAM";
            // 
            // textMaxMaps
            // 
            this.textMaxMaps.Location = new System.Drawing.Point(106, 176);
            this.textMaxMaps.Name = "textMaxMaps";
            this.textMaxMaps.ReadOnly = true;
            this.textMaxMaps.Size = new System.Drawing.Size(54, 20);
            this.textMaxMaps.TabIndex = 12;
            this.textMaxMaps.Text = "4096";
            this.textMaxMaps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(166, 179);
            this.label10.MaximumSize = new System.Drawing.Size(230, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "maps loaded";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(31, 200);
            this.label11.MaximumSize = new System.Drawing.Size(230, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(87, 13);
            this.label11.TabIndex = 11;
            this.label11.Text = "at the same time.";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(31, 179);
            this.label9.MaximumSize = new System.Drawing.Size(230, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 13);
            this.label9.TabIndex = 11;
            this.label9.Text = "you can have";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(31, 158);
            this.label8.MaximumSize = new System.Drawing.Size(230, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(204, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Assuming that you have 1GB of free RAM";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(201, 127);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "MB of RAM";
            // 
            // textRamRequired
            // 
            this.textRamRequired.Location = new System.Drawing.Point(127, 124);
            this.textRamRequired.Name = "textRamRequired";
            this.textRamRequired.ReadOnly = true;
            this.textRamRequired.Size = new System.Drawing.Size(68, 20);
            this.textRamRequired.TabIndex = 9;
            this.textRamRequired.Text = "0,25";
            this.textRamRequired.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(31, 127);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "One map requires";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 106);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Results:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(180, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Depth";
            // 
            // comboBoxDepth
            // 
            this.comboBoxDepth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDepth.FormattingEnabled = true;
            this.comboBoxDepth.Items.AddRange(new object[] {
            "16",
            "32",
            "64",
            "128",
            "256",
            "512",
            "1024",
            "2048",
            "4096",
            "8192"});
            this.comboBoxDepth.Location = new System.Drawing.Point(180, 71);
            this.comboBoxDepth.Name = "comboBoxDepth";
            this.comboBoxDepth.Size = new System.Drawing.Size(70, 21);
            this.comboBoxDepth.TabIndex = 5;
            this.comboBoxDepth.SelectedIndexChanged += new System.EventHandler(this.comboBoxDepth_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(104, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Height";
            // 
            // comboBoxHeight
            // 
            this.comboBoxHeight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxHeight.FormattingEnabled = true;
            this.comboBoxHeight.Items.AddRange(new object[] {
            "16",
            "32",
            "64",
            "128",
            "256",
            "512",
            "1024",
            "2048",
            "4096",
            "8192"});
            this.comboBoxHeight.Location = new System.Drawing.Point(104, 71);
            this.comboBoxHeight.Name = "comboBoxHeight";
            this.comboBoxHeight.Size = new System.Drawing.Size(70, 21);
            this.comboBoxHeight.TabIndex = 3;
            this.comboBoxHeight.SelectedIndexChanged += new System.EventHandler(this.comboBoxHeight_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Width";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 13);
            this.label1.MaximumSize = new System.Drawing.Size(250, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 26);
            this.label1.TabIndex = 1;
            this.label1.Text = "This tool calculates the approx. RAM usage for a map of given dimensions.";
            // 
            // Tools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 269);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Tools";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tools";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Tools_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

		}

		// Token: 0x0400021F RID: 543
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000220 RID: 544
		private global::System.Windows.Forms.ComboBox comboBoxWidth;

		// Token: 0x04000221 RID: 545
		private global::System.Windows.Forms.TabControl tabControl1;

		// Token: 0x04000222 RID: 546
		private global::System.Windows.Forms.TabPage tabPage1;

		// Token: 0x04000223 RID: 547
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04000224 RID: 548
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000225 RID: 549
		private global::System.Windows.Forms.TextBox textMaxMaps;

		// Token: 0x04000226 RID: 550
		private global::System.Windows.Forms.Label label10;

		// Token: 0x04000227 RID: 551
		private global::System.Windows.Forms.Label label11;

		// Token: 0x04000228 RID: 552
		private global::System.Windows.Forms.Label label9;

		// Token: 0x04000229 RID: 553
		private global::System.Windows.Forms.Label label8;

		// Token: 0x0400022A RID: 554
		private global::System.Windows.Forms.Label label7;

		// Token: 0x0400022B RID: 555
		private global::System.Windows.Forms.TextBox textRamRequired;

		// Token: 0x0400022C RID: 556
		private global::System.Windows.Forms.Label label6;

		// Token: 0x0400022D RID: 557
		private global::System.Windows.Forms.Label label5;

		// Token: 0x0400022E RID: 558
		private global::System.Windows.Forms.Label label4;

		// Token: 0x0400022F RID: 559
		private global::System.Windows.Forms.ComboBox comboBoxDepth;

		// Token: 0x04000230 RID: 560
		private global::System.Windows.Forms.Label label3;

		// Token: 0x04000231 RID: 561
		private global::System.Windows.Forms.ComboBox comboBoxHeight;
	}
}
