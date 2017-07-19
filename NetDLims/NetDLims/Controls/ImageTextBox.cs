using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Text;
using System.ComponentModel;
using System.Collections;

using System.Text.RegularExpressions;


namespace NetDLims.Controls
{
    [ToolboxBitmap(typeof(TextBox))]
    public class ImageTextBox : TextBox
    {
        #region Fields

        /// <summary> 
        /// 是否鼠标MouseOver状态 
        /// </summary> 
        private bool _IsMouseOver = false;
        /// <summary> 
        /// 是否启用热点效果 
        /// </summary> 
        private bool _HotTrack = false;

        private const int Space = 3;

        #endregion



        private string _emptyTextTip;
        private Color _emptyTextTipColor = Color.DarkGray;
        private Color _bordercolor = Color.FromArgb(0, 0, 0);
        private Color _HotColor = Color.FromArgb(0, 0, 0);
        private ArrayList _controllist = new ArrayList();
        private bool userskincolor = true;

        [DefaultValue(typeof(ColumnHeader), "ColumnHeader")]
        public ArrayList Controllist
        {
            get { return _controllist; }
            set { _controllist = value; }
        }


        private const int WM_PAINT = 0xF;
        private const int WM_CTLCOLOREDIT = 0x133;
        public ImageTextBox()
            : base()
        {
            Init();
        }




        [DefaultValue("")]
        public string EmptyTextTip
        {
            get { return _emptyTextTip; }
            set
            {
                _emptyTextTip = value;
                base.Invalidate();
            }
        }

        private TextStatus tStatus = TextStatus.ALL;

        [Browsable(true), Description("输入框中类型")]
        public TextStatus TStatus
        {
            get { return tStatus; }
            set { tStatus = value; }
        }
          


        [DefaultValue(typeof(Color), "DarkGray")]
        public Color EmptyTextTipColor
        {
            get { return _emptyTextTipColor; }
            set
            {
                _emptyTextTipColor = value;
                base.Invalidate();
            }
        }

        [DefaultValue(typeof(Color), "0,0,0")]
        //[DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public Color Bordercolor
        {
            get { return _bordercolor; }
            set
            {
                if (_bordercolor != value)
                {
                    _bordercolor = value;
                    base.Invalidate();
                }
            }
        }
        // [DefaultValue(typeof(Color), "0,0,0")]
        //public Color HotColor
        //{
        //    get { return _HotColor; }
        //    set { _HotColor = value; }
        //}


        //返回hWnd参数所指定的窗口的设备环境。
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern IntPtr GetWindowDC(IntPtr hWnd);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        //函数释放设备上下文环境（DC）  
        static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);    

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_PAINT || m.Msg == WM_CTLCOLOREDIT)
            {
                //IntPtr hDC = GetWindowDC(m.HWnd);
                WmPaint(ref m);
               // ReleaseDC(m.HWnd, hDC);
            }
            
        }

        private void WmPaint(ref Message m)
        {
            //if (_bordercolor == DefaultTheme.BaseColor)
            //    CalcLayerBounds();
            using (Graphics graphics = Graphics.FromHwnd(base.Handle))
            {
                ////画背景
                //graphics.FillRectangle(new SolidBrush(this.BackColor), this.ClientRectangle);

                //只有在边框样式为FixedSingle时自定义边框样式才有效 
                if (this.BorderStyle == BorderStyle.FixedSingle)
                {
                    //边框Width为1个像素 
                    System.Drawing.Pen pen = new Pen(this.Bordercolor, 1);
                    //绘制边框 

                    if (this._IsMouseOver)
                    {
                        pen.Width = 2;
                    }
                    else
                    {
                        pen.Width = 1;
                    }
                    //绘制边框 
                    graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                    graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    graphics.DrawRectangle(pen, 0, 0, this.Width - 1, this.Height - 1);
                    pen.Dispose();
                }

              

                if (Text.Length == 0
                     && !string.IsNullOrEmpty(_emptyTextTip)
                     )
                {
                    TextFormatFlags format =
                       TextFormatFlags.EndEllipsis |
                       TextFormatFlags.VerticalCenter;

                    if (RightToLeft == RightToLeft.Yes)
                    {
                        format |= TextFormatFlags.RightToLeft | TextFormatFlags.Right;
                    }
                    graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                    TextRenderer.DrawText(
                        graphics,
                        _emptyTextTip,
                        Font,
                        base.ClientRectangle,
                        _emptyTextTipColor,
                        format);
                }
            }
        }

        #region Override Methods
        
        protected override void OnMouseEnter(EventArgs e)
        {
            //鼠标状态 
            this._IsMouseOver = true;
            //如果启用HotTrack，则开始重绘 
            //如果不加判断这里不加判断，则当不启用HotTrack， 
            //鼠标在控件上移动时，控件边框会不断重绘， 
            //导致控件边框闪烁。下同 
            //谁有更好的办法？Please tell me , Thanks。 
            if (this._HotTrack)
            {
                //重绘 
                this.Invalidate();
            }
            base.OnMouseEnter(e);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            //string pattern=@"^\d+.?\d+$";
            //bool result = Regex.IsMatch(this.Text, pattern);
            if (tStatus == TextStatus.Number)
            {
                //if ((!Regex.IsMatch(this.Text, pattern)))
                if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)46 && e.KeyChar != (char)13 && e.KeyChar != (char)8)
                {
                    e.Handled = true;
                }
                else if(e.KeyChar==(char)46)//小数点不能是第一位
                {
                    if (this.Text.Trim().Length <= 0 || this.Text.Contains("."))//判断小数点不能为1
                    {
                        e.Handled = true;
                    }
                }
            }
            else if (tStatus == TextStatus.Letter)
            {
                e.Handled = true;
                if ((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || e.KeyChar == 8)
                {
                    e.Handled = false;
                }
                else
                {
                    if ((e.KeyChar >= 'a' && e.KeyChar <= 'z') || e.KeyChar == 8)
                    {
                        e.Handled = false;
                    }
                }
            }
            else if (tStatus == TextStatus.NAndL)
            {
                e.Handled = true;
                if ((Char.IsNumber(e.KeyChar))  || e.KeyChar == (char)13 || e.KeyChar != (char)8)
                {
                    e.Handled = false;
                }
                else  if ((e.KeyChar >= 'A' && e.KeyChar <= 'Z'))
                {
                    e.Handled = false;
                }
                else if ((e.KeyChar >= 'a' && e.KeyChar <= 'z'))
                {
                    e.Handled = false;
                }
            }
            base.OnKeyPress(e);
        }

        /// <summary> 
        /// 当鼠标从该控件移开时 
        /// </summary> 
        /// <param name="e"></param> 
        protected override void OnMouseLeave(EventArgs e)
        {
            this._IsMouseOver = false;

            if (this._HotTrack)
            {
                //重绘 
                this.Invalidate();
            }
            base.OnMouseLeave(e);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            this._IsMouseOver = true;
            //重绘 
            this.Invalidate();

            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            
            this._IsMouseOver = false;
                //重绘 
            this.Invalidate();
            
            base.OnLostFocus(e);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        #endregion

        #region HelperMethods

        private void Init()
        {
            base.SetStyle(
               ControlStyles.AllPaintingInWmPaint |
               ControlStyles.OptimizedDoubleBuffer |
               ControlStyles.ResizeRedraw |
               ControlStyles.SupportsTransparentBackColor, true);
            //base.SetStyle(ControlStyles.Opaque, false);
            this.BorderStyle = BorderStyle.FixedSingle;
        }




        #endregion
    }
}
