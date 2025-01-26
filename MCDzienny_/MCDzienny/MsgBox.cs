using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace MCDzienny
{
    public class MsgBox : Form
    {

        static readonly int scale = 3;
        readonly Image img;

        bool announcement;

        IContainer components;

        MsgBox(bool isOpen)
        {
            //IL_0030: Unknown result type (might be due to invalid IL or missing references)
            //IL_0036: Expected O, but got Unknown
            //IL_006f: Unknown result type (might be due to invalid IL or missing references)
            //IL_0079: Expected O, but got Unknown
            InitializeComponent();
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            Stream stream = !isOpen ? executingAssembly.GetManifestResourceStream("MCDzienny.icon_wrong.png")
                : executingAssembly.GetManifestResourceStream("MCDzienny.icon_ok.png");
            Bitmap val = (Bitmap)(img = new Bitmap(stream));
            announcement = isOpen;
            ClientSize = new Size(val.Width * scale, val.Height * scale);
            Paint += msgBox_Paint;
            Invalidate();
        }

        public static void ShowBox(bool isOpen)
        {
            //IL_000f: Unknown result type (might be due to invalid IL or missing references)
            MsgBox msgBox = new MsgBox(isOpen);
            msgBox.StartPosition = FormStartPosition.CenterScreen;
            msgBox.ShowDialog();
        }

        void msgBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.InterpolationMode = (InterpolationMode)7;
            e.Graphics.DrawImage(img, new Rectangle(0, 0, img.Width * scale, img.Height * scale));
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
            components = new Container();
            AutoScaleMode = AutoScaleMode.Font;
            Text = "MsgBox";
        }
    }
}