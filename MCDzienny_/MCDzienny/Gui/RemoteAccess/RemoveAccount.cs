using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MCDzienny.GUI.RemoteAccess
{
    public class RemoveAccount : Form
    {
        readonly string accountName;

        Button button1;

        Button button2;

        IContainer components;

        Label label1;

        public RemoveAccount(Form owner, string accountName)
        {
            InitializeComponent();
            this.accountName = accountName;
            label1.Text = "Do you really want to remove \"" + accountName + "\" remote account?";
            CenterToForm(owner);
            ActiveControl = button2;
        }

        void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        void button1_Click(object sender, EventArgs e)
        {
            Server.remoteAccounts.Accounts.Remove(accountName);
            Server.remoteAccounts.Save();
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
            //IL_0056: Unknown result type (might be due to invalid IL or missing references)
            //IL_0060: Expected O, but got Unknown
            label1 = new Label();
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            label1.AutoEllipsis = true;
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 9f, 0, (GraphicsUnit)3, 238);
            label1.Location = new Point(26, 27);
            label1.MaximumSize = new Size(315, 0);
            label1.Name = "label1";
            label1.Size = new Size(119, 15);
            label1.TabIndex = 0;
            label1.Text = "Do you really want to";
            button1.Location = new Point(92, 87);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 1;
            button1.Text = "Yes";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            button2.Location = new Point(191, 87);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 2;
            button2.Text = "No";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            AutoScaleDimensions = new SizeF(6f, 13f);
            AutoScaleMode = (AutoScaleMode)1;
            ClientSize = new Size(369, 122);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RemoveAccount";
            ShowIcon = false;
            Text = "Remove Account";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}