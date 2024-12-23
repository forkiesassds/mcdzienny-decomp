using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using MCDzienny_.Properties;

namespace MCDzienny.Misc
{
    public class SplashScreen : Form
    {
        IContainer components;

        public SplashScreen()
        {
            AllowTransparency = true;
            InitializeComponent();
        }

        public void FadeOut()
        {
            Thread.Sleep(100);
            for (int i = 0; i < 10; i++)
            {
                Opacity = Opacity - 0.10000000149011612;
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
            SuspendLayout();
            AutoScaleDimensions = new SizeF(6f, 13f);
            AutoScaleMode = (AutoScaleMode)1;
            BackColor = Color.White;
            BackgroundImage = Resources.splashScreen;
            ClientSize = new Size(520, 260);
            FormBorderStyle = 0;
            Name = "SplashScreen";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = (FormStartPosition)1;
            Text = "SplashScreen";
            ResumeLayout(false);
        }
    }
}