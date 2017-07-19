using IMClient.Properties;
using IMClient.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CCWin;

namespace IMClient.Controls.Base
{
    public partial class FrameBase : CCSkinMain
    {

        //定义delegate
        public delegate void WindowStateHandler(object sender, FormWindowState e);
        //用event 关键字声明事件对象
        /// <summary>
        /// 窗口状态发生变化时发生
        /// </summary>
        public event WindowStateHandler WindowStateChanged;


        internal const int WM_NCHITTEST = 0x84;
        internal const int WM_NCLBUTTONDBLCLK = 0xA3;
        internal const int WM_SYSCOMMAND = 0x112;
        internal const int SC_CLOSE = 0xF060;
        internal const int SC_MINIMIZE = 0xF020;
        internal const int SC_MAXIMIZE = 0xF030;
        internal const int SC_RESTORE = 0xF120;

        protected int CaptionPos = 38;
        public FrameBase()
        {
            this.ControlBox = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            InitializeComponent();
            
            //this.Icon = Resources.dna;
            //this.BackColor = FrmStyle.FormBackColor;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
               ControlStyles.OptimizedDoubleBuffer |
               ControlStyles.ResizeRedraw |
               ControlStyles.UserPaint, true);
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
        }


        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_SYSCOMMAND:
                    if (m.WParam.ToInt32() == SC_MINIMIZE)
                    {
                        if (WindowStateChanged != null)
                            WindowStateChanged(this, FormWindowState.Minimized);
                    }
                    else if (m.WParam.ToInt32() == SC_MAXIMIZE)
                    {
                        if (WindowStateChanged != null)
                            WindowStateChanged(this, FormWindowState.Maximized);
                    }
                    else if (m.WParam.ToInt32() == SC_RESTORE)
                    {
                        if (WindowStateChanged != null)
                            WindowStateChanged(this, FormWindowState.Normal);
                    }
                    base.WndProc(ref m);
                    break;
                case WM_NCHITTEST:
                    if (this.ParentForm == null && this.FormBorderStyle == FormBorderStyle.None)
                    {
                        this.WmNcHitTest(ref m);
                    }
                    else
                    {
                        base.WndProc(ref m);
                    }

                    break;
                case WM_NCLBUTTONDBLCLK://禁止双击最大化
                    //Console.WriteLine(this.WindowState);
                    return;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        /// <summary>
        /// 响应 WM_NCHITTEST 消息。
        /// </summary>
        /// <param name="m"></param>
        protected virtual void WmNcHitTest(ref Message m)
        {
            Point p = new Point(m.LParam.ToInt32());
            p = base.PointToClient(p);

            User32.HitTest result = User32.HitTest.HTCLIENT;

            if (p.Y < CaptionPos) result = User32.HitTest.HTCAPTION;

            m.Result = new IntPtr((int)result);
        }
    }
}
