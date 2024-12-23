using System;
using System.Drawing;
using System.Windows.Forms;

namespace MCDzienny.Misc
{
    class PerPixelAlphaForm : Form
    {

        public PerPixelAlphaForm()
        {
            FormBorderStyle = 0;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.ExStyle |= 0x80000;
                return createParams;
            }
        }

        public virtual void SetBitmap(Bitmap bitmap)
        {
            SetBitmap(bitmap, byte.MaxValue);
        }

        public virtual void SetBitmap(Bitmap bitmap, byte opacity)
        {
            //IL_0001: Unknown result type (might be due to invalid IL or missing references)
            //IL_000b: Invalid comparison between Unknown and I4
            if ((int)bitmap.PixelFormat != 2498570)
            {
                throw new ApplicationException("The bitmap must be 32ppp with alpha-channel.");
            }
            IntPtr dC = Win32.GetDC(IntPtr.Zero);
            IntPtr intPtr = Win32.CreateCompatibleDC(dC);
            IntPtr intPtr2 = IntPtr.Zero;
            IntPtr hObject = IntPtr.Zero;
            try
            {
                intPtr2 = bitmap.GetHbitmap(Color.FromArgb(0));
                hObject = Win32.SelectObject(intPtr, intPtr2);
                Win32.Size psize = new Win32.Size(bitmap.Width, bitmap.Height);
                Win32.Point pprSrc = new Win32.Point(0, 0);
                Win32.Point pptDst = new Win32.Point(Left, Top);
                Win32.BLENDFUNCTION pblend = default(Win32.BLENDFUNCTION);
                pblend.BlendOp = 0;
                pblend.BlendFlags = 0;
                pblend.SourceConstantAlpha = opacity;
                pblend.AlphaFormat = 1;
                Win32.UpdateLayeredWindow(Handle, dC, ref pptDst, ref psize, intPtr, ref pprSrc, 0, ref pblend, 2);
            }
            finally
            {
                Win32.ReleaseDC(IntPtr.Zero, dC);
                if (intPtr2 != IntPtr.Zero)
                {
                    Win32.SelectObject(intPtr, hObject);
                    Win32.DeleteObject(intPtr2);
                }
                Win32.DeleteDC(intPtr);
            }
        }
    }
}