using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace MCDzienny
{
    // Token: 0x020001BC RID: 444
    public partial class MsgBox : Form
    {
        // Token: 0x04000684 RID: 1668
        private static readonly int scale = 3;

        // Token: 0x04000683 RID: 1667
        private bool announcement;

        // Token: 0x04000682 RID: 1666
        private readonly Image img;

        // Token: 0x06000C99 RID: 3225 RVA: 0x00048E28 File Offset: 0x00047028
        private MsgBox(bool isOpen)
        {
            InitializeComponent();
            var executingAssembly = Assembly.GetExecutingAssembly();
            Stream manifestResourceStream;
            if (isOpen)
                manifestResourceStream = executingAssembly.GetManifestResourceStream("MCDzienny.icon_ok.png");
            else
                manifestResourceStream = executingAssembly.GetManifestResourceStream("MCDzienny.icon_wrong.png");
            var bitmap = new Bitmap(manifestResourceStream);
            img = bitmap;
            announcement = isOpen;
            ClientSize = new Size(bitmap.Width * scale, bitmap.Height * scale);
            Paint += msgBox_Paint;
            Invalidate();
        }

        // Token: 0x06000C9A RID: 3226 RVA: 0x00048EB4 File Offset: 0x000470B4
        public static void ShowBox(bool isOpen)
        {
            new MsgBox(isOpen)
            {
                StartPosition = FormStartPosition.CenterScreen
            }.ShowDialog();
        }

        // Token: 0x06000C9B RID: 3227 RVA: 0x00048ED8 File Offset: 0x000470D8
        private void msgBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImage(img, new Rectangle(0, 0, img.Width * scale, img.Height * scale));
        }
    }
}