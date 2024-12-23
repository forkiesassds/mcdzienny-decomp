using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MCDzienny.Gui
{
    public class ZombieProperties : Form
    {
        IContainer components;

        public ZombieProperties()
        {
            InitializeComponent();
        }

        void ZombieProperties_FormClosed(object sender, EventArgs e)
        {
            Window.zombieSettingsPrevLoaded = false;
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
            //IL_0055: Unknown result type (might be due to invalid IL or missing references)
            //IL_005f: Expected O, but got Unknown
            SuspendLayout();
            AutoScaleDimensions = new SizeF(6f, 13f);
            AutoScaleMode = (AutoScaleMode)1;
            ClientSize = new Size(451, 457);
            Name = "ZombieProperties";
            Text = "ZombieProperties";
            FormClosed += ZombieProperties_FormClosed;
            ResumeLayout(false);
        }
    }
}