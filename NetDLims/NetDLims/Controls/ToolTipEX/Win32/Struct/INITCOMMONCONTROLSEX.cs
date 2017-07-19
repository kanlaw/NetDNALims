using System;
using System.Runtime.InteropServices;

namespace NetDLims.Controls.ToolTipEX.Win32.Struct
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct INITCOMMONCONTROLSEX
    {
        internal INITCOMMONCONTROLSEX(int flags)
        {
            this.dwSize = Marshal.SizeOf(typeof(INITCOMMONCONTROLSEX));
            this.dwICC = flags;
        }

        internal int dwSize;
        internal int dwICC;
    }
}
