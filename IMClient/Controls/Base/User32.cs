using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMClient.Controls.Base
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Runtime.InteropServices;
    using System.Drawing;

    public static class User32
    {
        public const int WM_NCCALCSIZE = 0x83;
        public const int WM_NCHITTEST = 0x84;
        public const int WM_NCPAINT = 0x85;

        public const int WM_NCMOUSEMOVE = 0xa0;
        public const int WM_NCLBUTTONDOWN = 0xa1;
        public const int WM_NCLBUTTONUP = 0xa2;
        public const int WM_NCLBUTTONDBLCLK = 0xa3;

        public const int WM_ERASEBKGND = 0x14;
        public const int WM_PAINT = 0xf;

        public enum HitTest
        {
            HTNOWHERE = 0,
            HTCLIENT = 1,
            HTCAPTION = 2,
            HTGROWBOX = 4,
            HTSIZE = HTGROWBOX,
            HTMINBUTTON = 8,
            HTMAXBUTTON = 9,
            HTLEFT = 10,
            HTRIGHT = 11,
            HTTOP = 12,
            HTTOPLEFT = 13,
            HTTOPRIGHT = 14,
            HTBOTTOM = 15,
            HTBOTTOMLEFT = 16,
            HTBOTTOMRIGHT = 17,
            HTREDUCE = HTMINBUTTON,
            HTZOOM = HTMAXBUTTON,
            HTSIZEFIRST = HTLEFT,
            HTSIZELAST = HTBOTTOMRIGHT,
            HTTRANSPARENT = -1,
            HTCLOSE = 20
        }

        public struct WINDOWPOS
        {
            [MarshalAs(UnmanagedType.SysInt)]
            public IntPtr hwnd;

            [MarshalAs(UnmanagedType.SysInt)]
            public IntPtr hwndInsertAfter;

            [MarshalAs(UnmanagedType.I4)]
            public int x;

            [MarshalAs(UnmanagedType.I4)]
            public int y;

            [MarshalAs(UnmanagedType.I4)]
            public int cx;

            [MarshalAs(UnmanagedType.I4)]
            public int cy;

            [MarshalAs(UnmanagedType.U4)]
            public uint flags;
        }

        public struct NCCALCSIZE_PARAMS
        {
            [MarshalAs(UnmanagedType.Struct)]
            public Native.RECT rgrc0;

            [MarshalAs(UnmanagedType.Struct)]
            public Native.RECT rgrc1;

            [MarshalAs(UnmanagedType.Struct)]
            public Native.RECT rgrc2;

            //[MarshalAs(UnmanagedType.LPStruct)]
            //public WINDOWPOS lppos;
        }

        public struct PAINTSTRUCT
        {
            [MarshalAs(UnmanagedType.SysInt)]
            public IntPtr hDC;

            [MarshalAs(UnmanagedType.I4)]
            public int fErase;

            [MarshalAs(UnmanagedType.Struct)]
            public Native.RECT rcPaint;

            [MarshalAs(UnmanagedType.Bool)]
            public bool fRestore;

            [MarshalAs(UnmanagedType.Bool)]
            public bool fIncUpdate;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] rgbReserved;
        }

        [DllImport("User32")]
        public static extern IntPtr BeginPaint(IntPtr hWnd, out PAINTSTRUCT lpPaint);

        [DllImport("User32")]
        public static extern bool EndPaint(IntPtr hWnd, ref PAINTSTRUCT lpPaint);

        [DllImport("User32")]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("User32")]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("User32")]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("User32")]
        public static extern bool GetWindowRect(IntPtr hWnd, ref Native.RECT lpRect);

        public delegate void PaintCoreProc(Graphics g);

        public static void DoubleBufferPaint(IntPtr hDC, int x, int y, int width, int height, PaintCoreProc paintCore)
        {
            IntPtr hDC2 = Gdi32.CreateCompatibleDC(hDC);

            IntPtr hBmp = Gdi32.CreateCompatibleBitmap(hDC, width, height);
            Gdi32.SelectObject(hDC2, hBmp);
            Gdi32.DeleteObject(hBmp);

            Graphics g = Graphics.FromHdc(hDC2);
            paintCore(g);
            g.Dispose();

            Gdi32.BitBlt(hDC, x, y, width, height, hDC2, 0, 0, Gdi32.SRCCOPY);

            Gdi32.DeleteDC(hDC2);
        }
    }
}
