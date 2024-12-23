using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MCDzienny.GUI.RemoteAccess
{
    public class NewAccount : Form
    {

        Button button1;

        Button button2;
        IContainer components;

        Label label1;

        Label label2;

        TextBox name;

        TextBox pass;

        public NewAccount(Form parent)
        {
            InitializeComponent();
            CenterToForm(parent);
        }

        void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        void button1_Click(object sender, EventArgs e)
        {
            if (!Server.remoteAccounts.Accounts.ContainsKey(name.Text))
            {
                Server.remoteAccounts.Accounts.Add(name.Text, pass.Text);
                Server.remoteAccounts.Save();
            }
            Close();
        }

        void CenterToForm(Form form)
        {
            StartPosition = 0;
            Location = new Point(form.Location.X + form.Size.Width / 2 - Size.Width / 2, form.Location.Y + form.Size.Height / 2 - Size.Height / 2);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            Dispose();
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
            name = new TextBox();
            pass = new TextBox();
            label1 = new Label();
            label2 = new Label();
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            name.Location = new Point(100, 32);
            name.Name = "name";
            name.Size = new Size(117, 20);
            name.TabIndex = 0;
            pass.Location = new Point(100, 67);
            pass.Name = "pass";
            pass.Size = new Size(117, 20);
            pass.TabIndex = 1;
            pass.UseSystemPasswordChar = true;
            label1.AutoSize = true;
            label1.Location = new Point(40, 35);
            label1.Name = "label1";
            label1.Size = new Size(38, 13);
            label1.TabIndex = 2;
            label1.Text = "Name:";
            label2.AutoSize = true;
            label2.Location = new Point(22, 70);
            label2.Name = "label2";
            label2.Size = new Size(56, 13);
            label2.TabIndex = 3;
            label2.Text = "Password:";
            button1.Location = new Point(25, 123);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 4;
            button1.Text = "Create";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            button2.Location = new Point(142, 123);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 5;
            button2.Text = "Cancel";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            AutoScaleDimensions = new SizeF(6f, 13f);
            AutoScaleMode = (AutoScaleMode)1;
            ClientSize = new Size(253, 159);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pass);
            Controls.Add(name);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "NewAccount";
            ShowIcon = false;
            Text = "New Account";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}