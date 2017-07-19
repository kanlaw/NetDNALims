using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace IMClient.Controls
{
    public class ItemBase : Control
    {
        public ItemBase()
        {
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.SetStyle(ControlStyles.Opaque, false);
            base.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

            base.BackColor = Color.Transparent;
           
        }

        

    }
}
