namespace MCDzienny.GUI
{
	// Token: 0x02000154 RID: 340
	public partial class CreateMap : global::System.Windows.Forms.Form
	{
		// Token: 0x060009D9 RID: 2521 RVA: 0x00033E5C File Offset: 0x0003205C
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x00033E7C File Offset: 0x0003207C
		private void InitializeComponent()
		{
            this.mapName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.mapX = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.mapY = new System.Windows.Forms.ComboBox();
            this.mapZ = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.mapGenerator = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.mapType = new System.Windows.Forms.ComboBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mapName
            // 
            this.mapName.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.mapName.Location = new System.Drawing.Point(79, 24);
            this.mapName.Name = "mapName";
            this.mapName.Size = new System.Drawing.Size(137, 22);
            this.mapName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(28, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name";
            // 
            // mapX
            // 
            this.mapX.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.mapX.FormattingEnabled = true;
            this.mapX.Items.AddRange(new object[] {
            "16",
            "32",
            "64",
            "128",
            "256",
            "512",
            "1024"});
            this.mapX.Location = new System.Drawing.Point(73, 100);
            this.mapX.Name = "mapX";
            this.mapX.Size = new System.Drawing.Size(77, 22);
            this.mapX.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(43, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "Dimensions";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(11, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 14);
            this.label3.TabIndex = 4;
            this.label3.Text = "Width [x]";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(8, 131);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 14);
            this.label4.TabIndex = 5;
            this.label4.Text = "Height [y]";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(10, 158);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 14);
            this.label5.TabIndex = 6;
            this.label5.Text = "Depth [z]";
            // 
            // mapY
            // 
            this.mapY.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.mapY.FormattingEnabled = true;
            this.mapY.Items.AddRange(new object[] {
            "16",
            "32",
            "64",
            "128",
            "256",
            "512",
            "1024"});
            this.mapY.Location = new System.Drawing.Point(73, 128);
            this.mapY.Name = "mapY";
            this.mapY.Size = new System.Drawing.Size(76, 22);
            this.mapY.TabIndex = 7;
            // 
            // mapZ
            // 
            this.mapZ.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.mapZ.FormattingEnabled = true;
            this.mapZ.Items.AddRange(new object[] {
            "16",
            "32",
            "64",
            "128",
            "256",
            "512",
            "1024"});
            this.mapZ.Location = new System.Drawing.Point(73, 155);
            this.mapZ.Name = "mapZ";
            this.mapZ.Size = new System.Drawing.Size(75, 22);
            this.mapZ.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(180, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 14);
            this.label6.TabIndex = 9;
            this.label6.Text = "Generator";
            // 
            // mapGenerator
            // 
            this.mapGenerator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mapGenerator.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.mapGenerator.FormattingEnabled = true;
            this.mapGenerator.Items.AddRange(new object[] {
            "Flat",
            "Island",
            "Mountains",
            "Forest",
            "Desert",
            "Ocean",
            "Pixel"});
            this.mapGenerator.Location = new System.Drawing.Point(172, 100);
            this.mapGenerator.Name = "mapGenerator";
            this.mapGenerator.Size = new System.Drawing.Size(97, 22);
            this.mapGenerator.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.Location = new System.Drawing.Point(199, 131);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 14);
            this.label7.TabIndex = 11;
            this.label7.Text = "Type";
            // 
            // mapType
            // 
            this.mapType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mapType.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.mapType.FormattingEnabled = true;
            this.mapType.Items.AddRange(new object[] {
            "Freebuild"});
            this.mapType.Location = new System.Drawing.Point(172, 155);
            this.mapType.Name = "mapType";
            this.mapType.Size = new System.Drawing.Size(97, 22);
            this.mapType.TabIndex = 12;
            //this.mapType.SelectedIndexChanged += new System.EventHandler(this.mapType_SelectedIndexChanged);
            // 
            // btnCreate
            // 
            this.btnCreate.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnCreate.Location = new System.Drawing.Point(62, 234);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 13;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnCancel.Location = new System.Drawing.Point(156, 234);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // CreateMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 269);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.mapType);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.mapGenerator);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.mapZ);
            this.Controls.Add(this.mapY);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.mapX);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mapName);
            this.Name = "CreateMap";
            this.ShowIcon = false;
            this.Text = "Create new map";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		// Token: 0x04000460 RID: 1120
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000461 RID: 1121
		private global::System.Windows.Forms.TextBox mapName;

		// Token: 0x04000462 RID: 1122
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000463 RID: 1123
		private global::System.Windows.Forms.ComboBox mapX;

		// Token: 0x04000464 RID: 1124
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04000465 RID: 1125
		private global::System.Windows.Forms.Label label3;

		// Token: 0x04000466 RID: 1126
		private global::System.Windows.Forms.Label label4;

		// Token: 0x04000467 RID: 1127
		private global::System.Windows.Forms.Label label5;

		// Token: 0x04000468 RID: 1128
		private global::System.Windows.Forms.ComboBox mapY;

		// Token: 0x04000469 RID: 1129
		private global::System.Windows.Forms.ComboBox mapZ;

		// Token: 0x0400046A RID: 1130
		private global::System.Windows.Forms.Label label6;

		// Token: 0x0400046B RID: 1131
		private global::System.Windows.Forms.ComboBox mapGenerator;

		// Token: 0x0400046C RID: 1132
		private global::System.Windows.Forms.Label label7;

		// Token: 0x0400046D RID: 1133
		private global::System.Windows.Forms.ComboBox mapType;

		// Token: 0x0400046E RID: 1134
		private global::System.Windows.Forms.Button btnCreate;

		// Token: 0x0400046F RID: 1135
		private global::System.Windows.Forms.Button btnCancel;
	}
}
