using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MCDzienny.Gui
{
    public class CreateMap : Form
    {

        Button btnCancel;

        Button btnCreate;
        IContainer components;

        Label label1;

        Label label2;

        Label label3;

        Label label4;

        Label label5;

        Label label6;

        Label label7;

        ComboBox mapGenerator;

        TextBox mapName;

        ComboBox mapType;

        ComboBox mapX;

        ComboBox mapY;

        ComboBox mapZ;

        public CreateMap()
        {
            InitializeComponent();
            CenterToParent();
            MinimizeBox = false;
            MaximizeBox = false;
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        void btnCreate_Click(object sender, EventArgs e)
        {
            //IL_00b9: Unknown result type (might be due to invalid IL or missing references)
            try
            {
                Command.all.Find("newlvl").Use(null, mapName.Text + " " + mapX.Text + " " + mapY.Text + " " + mapZ.Text + " " + mapGenerator.Text.ToLower());
                Command.all.Find("load").Use(null, mapName.Text);
                Hide();
                MessageBox.Show("The map was created and loaded.");
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
            Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        void InitializeComponent()
        {
            //IL_0001: Unknown result type (might be due to invalid IL or missing references)
            //IL_000b: Expected O, but got Unknown
            //IL_000c: Unknown result type (might be due to invalid IL or missing references)
            //IL_0016: Expected O, but got Unknown
            //IL_0017: Unknown result type (might be due to invalid IL or missing references)
            //IL_0021: Expected O, but got Unknown
            //IL_0022: Unknown result type (might be due to invalid IL or missing references)
            //IL_002c: Expected O, but got Unknown
            //IL_002d: Unknown result type (might be due to invalid IL or missing references)
            //IL_0037: Expected O, but got Unknown
            //IL_0038: Unknown result type (might be due to invalid IL or missing references)
            //IL_0042: Expected O, but got Unknown
            //IL_0043: Unknown result type (might be due to invalid IL or missing references)
            //IL_004d: Expected O, but got Unknown
            //IL_004e: Unknown result type (might be due to invalid IL or missing references)
            //IL_0058: Expected O, but got Unknown
            //IL_0059: Unknown result type (might be due to invalid IL or missing references)
            //IL_0063: Expected O, but got Unknown
            //IL_0064: Unknown result type (might be due to invalid IL or missing references)
            //IL_006e: Expected O, but got Unknown
            //IL_006f: Unknown result type (might be due to invalid IL or missing references)
            //IL_0079: Expected O, but got Unknown
            //IL_007a: Unknown result type (might be due to invalid IL or missing references)
            //IL_0084: Expected O, but got Unknown
            //IL_0085: Unknown result type (might be due to invalid IL or missing references)
            //IL_008f: Expected O, but got Unknown
            //IL_0090: Unknown result type (might be due to invalid IL or missing references)
            //IL_009a: Expected O, but got Unknown
            //IL_009b: Unknown result type (might be due to invalid IL or missing references)
            //IL_00a5: Expected O, but got Unknown
            //IL_00c2: Unknown result type (might be due to invalid IL or missing references)
            //IL_00cc: Expected O, but got Unknown
            //IL_0136: Unknown result type (might be due to invalid IL or missing references)
            //IL_0140: Expected O, but got Unknown
            //IL_01ab: Unknown result type (might be due to invalid IL or missing references)
            //IL_01b5: Expected O, but got Unknown
            //IL_0284: Unknown result type (might be due to invalid IL or missing references)
            //IL_028e: Expected O, but got Unknown
            //IL_0305: Unknown result type (might be due to invalid IL or missing references)
            //IL_030f: Expected O, but got Unknown
            //IL_0386: Unknown result type (might be due to invalid IL or missing references)
            //IL_0390: Expected O, but got Unknown
            //IL_0409: Unknown result type (might be due to invalid IL or missing references)
            //IL_0413: Expected O, but got Unknown
            //IL_0481: Unknown result type (might be due to invalid IL or missing references)
            //IL_048b: Expected O, but got Unknown
            //IL_0551: Unknown result type (might be due to invalid IL or missing references)
            //IL_055b: Expected O, but got Unknown
            //IL_062d: Unknown result type (might be due to invalid IL or missing references)
            //IL_0637: Expected O, but got Unknown
            //IL_06b2: Unknown result type (might be due to invalid IL or missing references)
            //IL_06bc: Expected O, but got Unknown
            //IL_078f: Unknown result type (might be due to invalid IL or missing references)
            //IL_0799: Expected O, but got Unknown
            //IL_0817: Unknown result type (might be due to invalid IL or missing references)
            //IL_0821: Expected O, but got Unknown
            //IL_08be: Unknown result type (might be due to invalid IL or missing references)
            //IL_08c8: Expected O, but got Unknown
            //IL_095a: Unknown result type (might be due to invalid IL or missing references)
            //IL_0964: Expected O, but got Unknown
            mapName = new TextBox();
            label1 = new Label();
            mapX = new ComboBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            mapY = new ComboBox();
            mapZ = new ComboBox();
            label6 = new Label();
            mapGenerator = new ComboBox();
            label7 = new Label();
            mapType = new ComboBox();
            btnCreate = new Button();
            btnCancel = new Button();
            SuspendLayout();
            mapName.Font = new Font("Calibri", 9f, FontStyle.Regular, GraphicsUnit.Point, 238);
            mapName.Location = new Point(79, 24);
            mapName.Name = "mapName";
            mapName.Size = new Size(137, 22);
            mapName.TabIndex = 0;
            label1.AutoSize = true;
            label1.Font = new Font("Calibri", 9f, FontStyle.Regular, GraphicsUnit.Point, 238);
            label1.Location = new Point(28, 27);
            label1.Name = "label1";
            label1.Size = new Size(39, 14);
            label1.TabIndex = 1;
            label1.Text = "Name";
            mapX.Font = new Font("Calibri", 9f, FontStyle.Regular, GraphicsUnit.Point, 238);
            mapX.FormattingEnabled = true;
            mapX.Items.AddRange(new object[7]
            {
                "16", "32", "64", "128", "256", "512", "1024"
            });
            mapX.Location = new Point(73, 100);
            mapX.Name = "mapX";
            mapX.Size = new Size(77, 22);
            mapX.TabIndex = 2;
            mapX.SelectedIndex = 3;
            label2.AutoSize = true;
            label2.Font = new Font("Calibri", 9f, FontStyle.Regular, GraphicsUnit.Point, 238);
            label2.Location = new Point(43, 74);
            label2.Name = "label2";
            label2.Size = new Size(73, 14);
            label2.TabIndex = 3;
            label2.Text = "Dimensions";
            label3.AutoSize = true;
            label3.Font = new Font("Calibri", 9f, FontStyle.Regular, GraphicsUnit.Point, 238);
            label3.Location = new Point(11, 103);
            label3.Name = "label3";
            label3.Size = new Size(56, 14);
            label3.TabIndex = 4;
            label3.Text = "Width [x]";
            label4.AutoSize = true;
            label4.Font = new Font("Calibri", 9f, FontStyle.Regular, GraphicsUnit.Point, 238);
            label4.Location = new Point(8, 131);
            label4.Name = "label4";
            label4.Size = new Size(59, 14);
            label4.TabIndex = 5;
            label4.Text = "Height [y]";
            label5.AutoSize = true;
            label5.Font = new Font("Calibri", 9f, FontStyle.Regular, GraphicsUnit.Point, 238);
            label5.Location = new Point(10, 158);
            label5.Name = "label5";
            label5.Size = new Size(56, 14);
            label5.TabIndex = 6;
            label5.Text = "Depth [z]";
            mapY.Font = new Font("Calibri", 9f, FontStyle.Regular, GraphicsUnit.Point, 238);
            mapY.FormattingEnabled = true;
            mapY.Items.AddRange(new object[7]
            {
                "16", "32", "64", "128", "256", "512", "1024"
            });
            mapY.Location = new Point(73, 128);
            mapY.Name = "mapY";
            mapY.Size = new Size(76, 22);
            mapY.TabIndex = 7;
            mapY.SelectedIndex = 2;
            mapZ.Font = new Font("Calibri", 9f, FontStyle.Regular, GraphicsUnit.Point, 238);
            mapZ.FormattingEnabled = true;
            mapZ.Items.AddRange(new object[7]
            {
                "16", "32", "64", "128", "256", "512", "1024"
            });
            mapZ.Location = new Point(73, 155);
            mapZ.Name = "mapZ";
            mapZ.Size = new Size(75, 22);
            mapZ.TabIndex = 8;
            mapZ.SelectedIndex = 3;
            label6.AutoSize = true;
            label6.Font = new Font("Calibri", 9f, FontStyle.Regular, GraphicsUnit.Point, 238);
            label6.Location = new Point(180, 74);
            label6.Name = "label6";
            label6.Size = new Size(62, 14);
            label6.TabIndex = 9;
            label6.Text = "Generator";
            mapGenerator.DropDownStyle = ComboBoxStyle.DropDownList;
            mapGenerator.Font = new Font("Calibri", 9f, FontStyle.Regular, GraphicsUnit.Point, 238);
            mapGenerator.FormattingEnabled = true;
            mapGenerator.Items.AddRange(new object[7]
            {
                "Flat", "Island", "Mountains", "Forest", "Desert", "Ocean", "Pixel"
            });
            mapGenerator.Location = new Point(172, 100);
            mapGenerator.Name = "mapGenerator";
            mapGenerator.Size = new Size(97, 22);
            mapGenerator.TabIndex = 10;
            mapGenerator.SelectedIndex = 0;
            label7.AutoSize = true;
            label7.Font = new Font("Calibri", 9f, FontStyle.Regular, GraphicsUnit.Point, 238);
            label7.Location = new Point(199, 131);
            label7.Name = "label7";
            label7.Size = new Size(32, 14);
            label7.TabIndex = 11;
            label7.Text = "Type";
            mapType.DropDownStyle = ComboBoxStyle.DropDownList;
            mapType.Font = new Font("Calibri", 9f, FontStyle.Regular, GraphicsUnit.Point, 238);
            mapType.FormattingEnabled = true;
            mapType.Items.AddRange(new object[1]
            {
                "Freebuild"
            });
            mapType.Location = new Point(172, 155);
            mapType.Name = "mapType";
            mapType.Size = new Size(97, 22);
            mapType.TabIndex = 12;
            mapType.SelectedIndex = 0;
            btnCreate.Font = new Font("Calibri", 9f, FontStyle.Regular, GraphicsUnit.Point, 238);
            btnCreate.Location = new Point(62, 234);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(75, 23);
            btnCreate.TabIndex = 13;
            btnCreate.Text = "Create";
            btnCreate.UseVisualStyleBackColor = true;
            btnCreate.Click += btnCreate_Click;
            btnCancel.Font = new Font("Calibri", 9f, FontStyle.Regular, GraphicsUnit.Point, 238);
            btnCancel.Location = new Point(156, 234);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 14;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            AutoScaleDimensions = new SizeF(6f, 13f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(292, 269);
            Controls.Add(btnCancel);
            Controls.Add(btnCreate);
            Controls.Add(mapType);
            Controls.Add(label7);
            Controls.Add(mapGenerator);
            Controls.Add(label6);
            Controls.Add(mapZ);
            Controls.Add(mapY);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(mapX);
            Controls.Add(label1);
            Controls.Add(mapName);
            Name = "CreateMap";
            ShowIcon = false;
            Text = "Create new map";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}