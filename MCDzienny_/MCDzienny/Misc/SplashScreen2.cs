using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MCDzienny.Misc
{
    class SplashScreen2 : PerPixelAlphaForm
    {
        public override void SetBitmap(Bitmap bitmap)
        {
            //IL_0050: Unknown result type (might be due to invalid IL or missing references)
            //IL_0056: Expected O, but got Unknown
            //IL_0073: Unknown result type (might be due to invalid IL or missing references)
            //IL_008c: Expected O, but got Unknown
            Left = Screen.GetWorkingArea(this).Width / 2 - bitmap.Width / 2;
            Top = Screen.GetWorkingArea(this).Height / 2 - bitmap.Height / 2;
            Graphics val = Graphics.FromImage(bitmap);
            Font val2 = new Font("Tahoma", 12f, FontStyle.Bold);
            val.SmoothingMode = (SmoothingMode)4;
            val.DrawString("Version: " + Server.Version, val2, new SolidBrush(Color.White), 36f, bitmap.Height - 45);
            base.SetBitmap(bitmap);
        }
    }
}