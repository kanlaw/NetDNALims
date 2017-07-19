using System;
using System.Runtime.InteropServices;
using NetDLims.Controls.ToolTipEX.Win32.Enum;

namespace NetDLims.Controls.ToolTipEX.Win32.Struct
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct TRACKMOUSEEVENT
    {
        internal uint cbSize;
        internal TRACKMOUSEEVENT_FLAGS dwFlags;
        internal IntPtr hwndTrack;
        internal uint dwHoverTime;
    }
}
