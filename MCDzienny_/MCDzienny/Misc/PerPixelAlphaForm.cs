using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace MCDzienny.Misc
{
    // Token: 0x020001BD RID: 445
    internal partial class PerPixelAlphaForm : Form
    {
        // Token: 0x06000C9F RID: 3231 RVA: 0x00048F74 File Offset: 0x00047174
        public PerPixelAlphaForm()
        {
            FormBorderStyle = FormBorderStyle.None;
        }

        // Token: 0x170004E5 RID: 1253
        // (get) Token: 0x06000CA2 RID: 3234 RVA: 0x000490AC File Offset: 0x000472AC
        protected override CreateParams CreateParams
        {
            get
            {
                var createParams = base.CreateParams;
                createParams.ExStyle |= 524288;
                return createParams;
            }
        }

        // Token: 0x06000CA0 RID: 3232 RVA: 0x00048F84 File Offset: 0x00047184
        public virtual void SetBitmap(Bitmap bitmap)
        {
            SetBitmap(bitmap, byte.MaxValue);
        }

        // Token: 0x06000CA1 RID: 3233 RVA: 0x00048F94 File Offset: 0x00047194
        public virtual void SetBitmap(Bitmap bitmap, byte opacity)
        {
            if (bitmap.PixelFormat != PixelFormat.Format32bppArgb)
                throw new ApplicationException("The bitmap must be 32ppp with alpha-channel.");
            var dc = Win32.GetDC(IntPtr.Zero);
            var intPtr = Win32.CreateCompatibleDC(dc);
            var intPtr2 = IntPtr.Zero;
            var hObject = IntPtr.Zero;
            try
            {
                intPtr2 = bitmap.GetHbitmap(Color.FromArgb(0));
                hObject = Win32.SelectObject(intPtr, intPtr2);
                var size = new Win32.Size(bitmap.Width, bitmap.Height);
                var point = new Win32.Point(0, 0);
                var point2 = new Win32.Point(Left, Top);
                var blendfunction = default(Win32.BLENDFUNCTION);
                blendfunction.BlendOp = 0;
                blendfunction.BlendFlags = 0;
                blendfunction.SourceConstantAlpha = opacity;
                blendfunction.AlphaFormat = 1;
                Win32.UpdateLayeredWindow(Handle, dc, ref point2, ref size, intPtr, ref point, 0, ref blendfunction, 2);
            }
            finally
            {
                Win32.ReleaseDC(IntPtr.Zero, dc);
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