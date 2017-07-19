using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace NetDLims.Controls.ToolTipEX
{
   /* 作者：Starts_2000
    * 日期：2010-01-09
    * 网站：http://www.RX.DNA.LimsBrowser.Controls.ToolTipRX.com CS 程序员之窗。
    * 你可以免费使用或修改以下代码，但请保留版权信息。
    * 具体请查看 CS程序员之窗开源协议（http://www.RX.DNA.LimsBrowser.Controls.ToolTipRX.com/csol.html）。
    */

    public class ToolTipColorTable
    {
        private static  Color _base = Color.FromArgb(105, 200, 254);
        private static  Color _border = Color.FromArgb(169, 169, 169);
        private static  Color _backNormal = Color.FromArgb(250, 250, 250);
        private static  Color _backHover = Color.FromArgb(250, 250, 250);
        private static  Color _backPressed = Color.FromArgb(226, 176, 0);
        private static  Color _titleFore = Color.Brown;
        private static  Color _tipFore = Color.Chocolate;

        public ToolTipColorTable() { }

        public virtual Color Base
        {
            get { return _base; }
            set
            {
                _base = value;
            }
        }

        public virtual Color Border
        {
            get { return _border; }
            set {
                _border = value;
            }
        }

        public virtual Color BackNormal
        {
            get { return _backNormal; }
            set
            {
                _backNormal = value;
            }
        }

        public virtual Color BackHover
        {
            get { return _backHover; }
            set
            {
                _backHover = value;
            }
        }

        public virtual Color BackPressed
        {
            get { return _backPressed; }
            set
            {
                _backPressed = value;
            }
        }

        public virtual Color TitleFore
        {
            get { return _titleFore; }
            set
            {
                _titleFore = value;
            }
        }

        public virtual Color TipFore
        {
            get { return _tipFore; }
            set
            {
                _tipFore = value;
            }
        }
    }
}
