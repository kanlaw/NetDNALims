﻿using System;
using System.Runtime.InteropServices;

namespace NetDLims.Controls.ToolTipEX.Win32.Struct
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct NMCUSTOMDRAW
    {
        internal NMHDR hdr;
        internal uint dwDrawStage;
        internal IntPtr hdc;
        internal RECT rc;
        internal IntPtr dwItemSpec;
        internal uint uItemState;
        internal IntPtr lItemlParam;
    }
}