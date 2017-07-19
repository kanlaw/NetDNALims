using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IMClient.Controls
{
    public partial class MyScroll : UserControl
    {
        private const int WS_HSCROLL = 0x100000;
        private const int WS_VSCROLL = 0x200000;
        private const int GWL_STYLE = (-16);

        [System.Runtime.InteropServices.DllImport("user32", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern int GetWindowLong(IntPtr hwnd, int nIndex);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int ShowScrollBar(IntPtr hWnd, int iBar, int bShow);

        const int SB_HORZ = 0;
        const int SB_VERT = 1;

        /// <summary>
        /// 内容保存控件
        /// </summary>
        public Panel PnlContent
        {
            set {
                this.pnlContent = value;
            }
            get {
                return this.pnlContent;
            }
        }

        public CustomControls.CustomScrollbar selfScroll (){
            return this.ScrollDt;
        }



        protected override void WndProc(ref Message m)
        {
            ShowScrollBar(this.pnlContent.Handle, SB_HORZ, 0);
            base.WndProc(ref m);
        }


        /// <summary>
        /// 判断是否出现垂直滚动条
        /// </summary>
        /// <param name="ctrl">待测控件</param>
        /// <returns>出现垂直滚动条返回true，否则为false</returns>
        internal static bool IsVerticalScrollBarVisible(Control ctrl)
        {
            if (!ctrl.IsHandleCreated)
                return false;
            return (GetWindowLong(ctrl.Handle, GWL_STYLE) & WS_VSCROLL) != 0;
        }

        Point pt = Point.Empty;
        public MyScroll()
        {
            InitializeComponent();
            pnlOut.Width = this.Width;
        }
        private void SetScroll()
        {
            pt = new Point(this.pnlContent.AutoScrollPosition.X, this.pnlContent.AutoScrollPosition.Y);
            this.ScrollDt.Minimum = 0;
            this.ScrollDt.Maximum = this.pnlContent.VerticalScroll.Maximum;//.DisplayRectangle.Height;
            this.ScrollDt.LargeChange = this.pnlContent.VerticalScroll.LargeChange; //ScrollDt.Maximum / ScrollDt.Height + this.flowLayoutPanel1.Height;
            this.ScrollDt.SmallChange = 15;
            this.ScrollDt.Value = Math.Abs(this.pnlContent.AutoScrollPosition.Y);
        }

        private void ScrollDt_Scroll(object sender, EventArgs e)
        {
            this.pnlContent.AutoScrollPosition = new Point(0, ScrollDt.Value);
            //vScrollBar1.Value = ScrollDt.Value;
            ScrollDt.Invalidate();
            Application.DoEvents();
            //private void customScrollbar1_Scroll(object sender, EventArgs e)
            //{

            //    Debug.WriteLine("vscroll: " + vScrollBar1.Value.ToString() + "  custom: " + customScrollbar1.Value.ToString());
            //}
        }

        public void AddControl(Control item)
        {
            this.pnlContent.Controls.Add(item);
            selfReSize();
        }

        /// <summary>
        /// 调整布局 判断是否显示滚动条
        /// </summary>
        public void selfReSize()
        {
            if (IsVerticalScrollBarVisible(this.pnlContent))
            {
                pnlOut.Width = this.Width - this.ScrollDt.Width;
                this.ScrollDt.Location = new Point(pnlOut.Width, 0);
                SetScroll();
            }
        }

        /// <summary>
        /// 检查是否需要滚动条
        /// </summary>
        public void chkScrollBar()
        {
            if (IsVerticalScrollBarVisible(this.pnlContent))
            {
                pnlOut.Width = this.Width - this.ScrollDt.Width;
                this.ScrollDt.Location = new Point(pnlOut.Width, 0);
            }
        }
    }
}
