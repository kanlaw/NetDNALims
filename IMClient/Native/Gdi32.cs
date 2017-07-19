using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

public static class Gdi32
{
    public const int SRCCOPY = 0xCC0020;

    [DllImport("Gdi32")]
    public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth, int nHeight);

    [DllImport("Gdi32")]
    public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

    [DllImport("Gdi32")]
    public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

    [DllImport("Gdi32")]
    public static extern bool DeleteObject(IntPtr hObject);

    [DllImport("Gdi32")]
    public static extern bool DeleteDC(IntPtr hDC);

    [DllImport("Gdi32")]
    public static extern bool BitBlt(
        IntPtr hdcDest,
    int nXDest,
    int nYDest,
    int nWidth,
    int nHeight,
    IntPtr hdcSrc,
    int nXSrc,
    int nYSrc,
    int dwRop);
}