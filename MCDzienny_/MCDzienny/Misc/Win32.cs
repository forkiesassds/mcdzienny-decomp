using System;
using System.Runtime.InteropServices;

namespace MCDzienny.Misc
{
    // Token: 0x020001BE RID: 446
    internal class Win32
    {
        // Token: 0x020001BF RID: 447
        public enum Bool
        {
            // Token: 0x0400068C RID: 1676
            False,

            // Token: 0x0400068D RID: 1677
            True
        }

        // Token: 0x04000686 RID: 1670
        public const int ULW_COLORKEY = 1;

        // Token: 0x04000687 RID: 1671
        public const int ULW_ALPHA = 2;

        // Token: 0x04000688 RID: 1672
        public const int ULW_OPAQUE = 4;

        // Token: 0x04000689 RID: 1673
        public const byte AC_SRC_OVER = 0;

        // Token: 0x0400068A RID: 1674
        public const byte AC_SRC_ALPHA = 1;

        // Token: 0x06000CA3 RID: 3235
        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern Bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref Point pptDst, ref Size psize,
            IntPtr hdcSrc, ref Point pprSrc, int crKey, ref BLENDFUNCTION pblend, int dwFlags);

        // Token: 0x06000CA4 RID: 3236
        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetDC(IntPtr hWnd);

        // Token: 0x06000CA5 RID: 3237
        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        // Token: 0x06000CA6 RID: 3238
        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        // Token: 0x06000CA7 RID: 3239
        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern Bool DeleteDC(IntPtr hdc);

        // Token: 0x06000CA8 RID: 3240
        [DllImport("gdi32.dll", ExactSpelling = true)]
        public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

        // Token: 0x06000CA9 RID: 3241
        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern Bool DeleteObject(IntPtr hObject);

        // Token: 0x020001C0 RID: 448
        public struct Point
        {
            // Token: 0x06000CAB RID: 3243 RVA: 0x000490DC File Offset: 0x000472DC
            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            // Token: 0x0400068E RID: 1678
            public int x;

            // Token: 0x0400068F RID: 1679
            public int y;
        }

        // Token: 0x020001C1 RID: 449
        public struct Size
        {
            // Token: 0x06000CAC RID: 3244 RVA: 0x000490EC File Offset: 0x000472EC
            public Size(int cx, int cy)
            {
                this.cx = cx;
                this.cy = cy;
            }

            // Token: 0x04000690 RID: 1680
            public int cx;

            // Token: 0x04000691 RID: 1681
            public int cy;
        }

        // Token: 0x020001C2 RID: 450
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct ARGB
        {
            // Token: 0x04000692 RID: 1682
            public readonly byte Blue;

            // Token: 0x04000693 RID: 1683
            public readonly byte Green;

            // Token: 0x04000694 RID: 1684
            public readonly byte Red;

            // Token: 0x04000695 RID: 1685
            public readonly byte Alpha;
        }

        // Token: 0x020001C3 RID: 451
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct BLENDFUNCTION
        {
            // Token: 0x04000696 RID: 1686
            public byte BlendOp;

            // Token: 0x04000697 RID: 1687
            public byte BlendFlags;

            // Token: 0x04000698 RID: 1688
            public byte SourceConstantAlpha;

            // Token: 0x04000699 RID: 1689
            public byte AlphaFormat;
        }
    }
}