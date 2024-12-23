using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MCDzienny.Settings;

namespace MCDzienny.Gui
{
    public class LavaProperties : Form
    {

        public static LavaProperties thisWindow;

        IContainer components;

        public LavaProperties()
        {
            thisWindow = this;
            InitializeComponent();
        }

        void LavaProperties_Unload(object sender, EventArgs e)
        {
            LavaSettings.All.Save();
            Window.lavaSettingsPrevLoaded = false;
        }

        void LavaProperties_Load(object sender, EventArgs e) {}

        void propertyGrid1_Click(object sender, EventArgs e) {}

        void tabPage2_Click(object sender, EventArgs e) {}

        void timePanel_Paint(object sender, PaintEventArgs e) {}

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
            //IL_005c: Unknown result type (might be due to invalid IL or missing references)
            //IL_0066: Expected O, but got Unknown
            SuspendLayout();
            AutoScaleDimensions = new SizeF(6f, 13f);
            AutoScaleMode = (AutoScaleMode)1;
            ClientSize = new Size(430, 498);
            Name = "LavaProperties";
            ShowIcon = false;
            Text = "Lava Survival Settings";
            FormClosed += LavaProperties_Unload;
            Load += LavaProperties_Load;
            ResumeLayout(false);
        }

        class PropertyGrd : PropertyGrid {}
    }
}