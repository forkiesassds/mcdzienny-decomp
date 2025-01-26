using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MCDzienny.Gui
{
    public class PopUpMessage : Form
    {

        Button button1;
        IContainer components;

        Label label;

        TextBox mainTextBox;

        public PopUpMessage(string message)
        {
            InitializeComponent();
            mainTextBox.Text = message;
            mainTextBox.SelectionStart = mainTextBox.Text.Length;
            CenterToScreen();
        }

        public PopUpMessage(string message, string title, string label)
        {
            InitializeComponent();
            Text = title;
            this.label.Text = label;
            mainTextBox.Text = message;
            mainTextBox.SelectionStart = mainTextBox.Text.Length;
            CenterToScreen();
        }

        void button1_Click(object sender, EventArgs e)
        {
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
            mainTextBox = new TextBox();
            label = new Label();
            button1 = new Button();
            SuspendLayout();
            mainTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mainTextBox.BackColor = Color.White;
            mainTextBox.CausesValidation = false;
            mainTextBox.Location = new Point(12, 30);
            mainTextBox.Multiline = true;
            mainTextBox.Name = "mainTextBox";
            mainTextBox.ReadOnly = true;
            mainTextBox.ScrollBars = ScrollBars.Vertical;
            mainTextBox.Size = new Size(483, 231);
            mainTextBox.TabIndex = 0;
            label.AutoSize = true;
            label.Location = new Point(13, 11);
            label.Name = "label";
            label.Size = new Size(107, 13);
            label.TabIndex = 1;
            label.Text = "Received Messages:";
            button1.Anchor = AnchorStyles.Bottom;
            button1.Location = new Point(221, 267);
            button1.Name = "button1";
            button1.Size = new Size(65, 23);
            button1.TabIndex = 2;
            button1.Text = "OK";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            AutoScaleDimensions = new SizeF(6f, 13f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(507, 300);
            Controls.Add(button1);
            Controls.Add(label);
            Controls.Add(mainTextBox);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PopUpMessage";
            ShowIcon = false;
            Text = "News";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}