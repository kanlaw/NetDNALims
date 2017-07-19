using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

public static class Native
{
    public struct RECT
    {
        [MarshalAs(UnmanagedType.I4)]
        public int left;

        [MarshalAs(UnmanagedType.I4)]
        public int top;

        [MarshalAs(UnmanagedType.I4)]
        public int right;

        [MarshalAs(UnmanagedType.I4)]
        public int bottom;
    }

}
