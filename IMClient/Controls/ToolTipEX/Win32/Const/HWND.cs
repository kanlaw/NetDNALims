﻿using System;

namespace IMClient.Controls.ToolTipEX.Win32.Const
{
    /// <summary>
    /// Use for setwindowpos.
    /// </summary>
    internal static class HWND
    {
        public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        public static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        public static readonly IntPtr HWND_TOP = new IntPtr(0);
        public static readonly IntPtr HWND_BOTTOM = new IntPtr(1);
    }
}
