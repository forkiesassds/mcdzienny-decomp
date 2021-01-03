using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MCDzienny.Misc
{
    // Token: 0x020001C8 RID: 456
    internal partial class SplashScreen2 : PerPixelAlphaForm
    {
        // Token: 0x06000CC1 RID: 3265 RVA: 0x00049AF8 File Offset: 0x00047CF8
        public override void SetBitmap(Bitmap bitmap)
        {
            Left = Screen.GetWorkingArea(this).Width / 2 - bitmap.Width / 2;
            Top = Screen.GetWorkingArea(this).Height / 2 - bitmap.Height / 2;
            var graphics = Graphics.FromImage(bitmap);
            var font = new Font("Tahoma", 12f, FontStyle.Bold);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.DrawString("Version: " + Server.Version, font, new SolidBrush(Color.White), 36f,
                bitmap.Height - 45);
            base.SetBitmap(bitmap);
        }
    }
}