using System;
using System.Runtime.InteropServices;

namespace IMClient.Controls.ToolTipEX.Win32.Struct
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct POINT
    {
        public int X;
        public int Y;

        public POINT(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
