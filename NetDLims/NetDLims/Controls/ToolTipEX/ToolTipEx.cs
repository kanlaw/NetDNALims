using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

using NetDLims.Controls.ToolTipEX.Win32.Struct;
using NetDLims.Controls.ToolTipEX.Win32;
using NetDLims.Controls.ToolTipEX.Win32.Const;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using IMClient.Properties;
using NetDLims.Properties;

namespace NetDLims.Controls.ToolTipEX
{
    /* 作者：Starts_2000
     * 日期：2010-01-09
     * 网站：http://www.RX.DNA.LimsBrowser.Controls.ToolTipRX.com CS 程序员之窗。
     * 你可以免费使用或修改以下代码，但请保留版权信息。
     * 具体请查看 CS程序员之窗开源协议（http://www.RX.DNA.LimsBrowser.Controls.ToolTipRX.com/csol.html）。
     */

    public enum dur { 
    top,left,right,bottom,normal
    }
    [ToolboxBitmap(typeof(ToolTip))]
    public class ToolTipEx : ToolTip
    {
        #region Fields
        /// <summary>
        /// 每行字符数量
        /// </summary>
       // private int lineStrlength = 15;
        private ImageDc _backDc;
        private Image _image= Resources.info_grey;
        private double _opacity = 1d;
        private ToolTipColorTable _colorTable;
        private Font _titleFont = new Font("微软雅黑", 11f, FontStyle.Bold);
        private Font _tipFont = new Font("微软雅黑", 9f);

        private Size _imageSize = SystemInformation.SmallIconSize;
        private dur d=dur.normal;

        [Browsable(true)]
        public Font TipFont
        {
            get { return _tipFont; }
            set { _tipFont = value; }
        }

       [Browsable(true)]
        public dur D
        {
            get { return d; }
            set { d = value; }
        }
        #endregion

        #region Constructors

        public ToolTipEx()
            : base()
        {
            InitOwnerDraw();
        }

        public ToolTipEx(IContainer cont)
            : base(cont)
        {
            InitOwnerDraw();
        }

        #endregion

        #region Properties

        private Size tooltipSize = new Size(30, 30);

        [Browsable(true)]
        public Size TooltipSize
        {
            get { return tooltipSize; }
            set { tooltipSize = value; }
        }
        private  Color _base = Color.FromArgb(105, 200, 254);
        private  Color _border = Color.FromArgb(169, 169, 169);
        private  Color _backNormal = Color.FromArgb(250, 250, 250);
        private  Color _backHover = Color.FromArgb(250, 250, 250);
        private  Color _backPressed = Color.FromArgb(226, 176, 0);
        private  Color _titleFore = Color.FromArgb(146, 146, 146);
        private Color _tipFore =FrmStyle.BorderColor;

         [Browsable(true)]
        public  Color Base
        {
            get { return _base; }
            set
            {
                _base = value;
            }
        }
         [Browsable(true)]
        public  Color Border
        {
            get { return _border; }
            set
            {
                _border = value;
            }
        }
         [Browsable(true)]
        public virtual Color BackNormal
        {
            get { return  _backNormal; }
            set
            {
                _backNormal = value;
            }
        }
         [Browsable(true)]
        public  Color BackHover
        {
            get { return _backHover; }
            set
            {
                _backHover = value;
            }
        }
         [Browsable(true)]
        public  Color BackPressed
        {
            get { return _backPressed; }
            set
            {
                _backPressed = value;
            }
        }
         [Browsable(true)]
        public  Color TitleFore
        {
            get { return _titleFore; }
            set
            {
                _titleFore = value;
            }
        }
        [Browsable(true)]
        public  Color TipFore
        {
            get { return _tipFore; }
            set
            {
                _tipFore = value;
            }
        }



        [Browsable(false)]
        public ToolTipColorTable ColorTable
        {
            get
            {
                if (_colorTable == null)
                {
                    _colorTable = new ToolTipColorTable();
                    _colorTable.BackHover = this.BackHover;
                    _colorTable.BackNormal = this.BackNormal;
                    _colorTable.BackPressed = this.BackPressed;
                    _colorTable.TipFore = this.TipFore;
                    _colorTable.TitleFore = this.TitleFore;
                    _colorTable.Border = this.Border;
                }
                return _colorTable;
            }
        }

        [DefaultValue(typeof(Font), "微软雅黑, 12pt, style=Bold")]
        public Font TitleFont
        {
            get { return _titleFont; }
            set 
            {
                if (_titleFont == null)
                {
                    throw new ArgumentNullException("TitleFont");
                }

                if (!_titleFont.IsSystemFont)
                {
                    _titleFont.Dispose();
                }

                _titleFont = value; 
            }
        }

        public new ToolTipIcon ToolTipIcon
        {
            get { return base.ToolTipIcon; }
            set
            {
                if (_image != null)
                {
                    base.ToolTipIcon = ToolTipIcon.Info;
                }
                else
                {
                    base.ToolTipIcon = value;
                }
            }
        }

        [DefaultValue(null)]
        public Image Image
        {
            get { return _image; }
            set
            {
                _image = value;
                if (_image == null)
                {
                    base.ToolTipIcon = ToolTipIcon.None;
                }
                else
                {
                    base.ToolTipIcon = ToolTipIcon.Info;
                }
            }
        }

        [DefaultValue(typeof(Size), "16, 16")]
        public Size ImageSize
        {
            get { return _imageSize; }
            set 
            {
                if (_imageSize != value)
                {
                    _imageSize = value;
                    if (_imageSize.Width > 32)
                    {
                        _imageSize.Width = 32;
                    }

                    if (_imageSize.Height > 32)
                    {
                        _imageSize.Height = 32;
                    }
                }
            }
        }

        [DefaultValue(1d)]
        [Browsable(true),TypeConverter(typeof(OpacityConverter))]
        public double Opacity
        {
            get { return _opacity; }
            set
            {
                if (value < 0 && value > 1)
                {
                    throw new ArgumentOutOfRangeException("Opacity");
                }

                _opacity = value;
            }
        }

        protected IntPtr Handle
        {
            get 
            {
                if (!DesignMode)
                {
                    Type t = typeof(ToolTip);
                    PropertyInfo pi = t.GetProperty(
                        "Handle",
                         BindingFlags.NonPublic | BindingFlags.Instance);

                    IntPtr handle = (IntPtr)pi.GetValue(this, null);
                   
                    return handle;
                }

                return IntPtr.Zero;
            }
        }

        #endregion

        #region Dispose

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                if (_backDc != null)
                {
                    _backDc.Dispose();
                    _backDc = null;
                }

                if (!_titleFont.IsSystemFont)
                {
                    _titleFont.Dispose();
                }
                _titleFont = null;

                _image = null;
                _colorTable = null;
            }
        }

        #endregion

        #region Helper Methods

      

        private void InitOwnerDraw()
        {
            base.OwnerDraw = true;
            base.ReshowDelay = 800;
            base.InitialDelay = 500;
            base.ToolTipIcon = ToolTipIcon.Info;
            base.Draw += new DrawToolTipEventHandler(ToolTipExDraw);
            base.Popup += new PopupEventHandler(ToolTipExPopup);
           
            
        }
     
        
        private void ToolTipExPopup(
            object sender, PopupEventArgs e)
        {
            if (_opacity < 1D)
            {
              
                //如果使用背景透明，获取背景图。
                TipCapture();
            }
            ToolTipEx t = sender as ToolTipEx;
           // e.ToolTipSize = new Size(160,90);
            
         // e.AssociatedControl 绑定的控件
        } 

        private void ToolTipExDraw(
            object sender, DrawToolTipEventArgs e)
        {
             
            Graphics g = e.Graphics;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            g.SmoothingMode = SmoothingMode.HighQuality;
            Rectangle bounds = e.Bounds;
            
            int alpha = (int)(_opacity * 255);

            int defaultXOffset = 12;
            int defaultTopHeight = 36;

            int tipTextXOffset = 3;
            int tipTextYOffset = 3;

            if (Handle != IntPtr.Zero && _opacity < 1D)
            {
                IntPtr hDC = g.GetHdc();

                NativeMethodsToop.BitBlt(
                    hDC, 0, 0, bounds.Width, bounds.Height,
                    _backDc.Hdc, 0, 0, 0xCC0020);
                g.ReleaseHdc(hDC);
            }

            Color backNormalColor = Color.FromArgb(
               alpha, ColorTable.BackNormal);
            Color baseColor = Color.FromArgb(
                alpha, ColorTable.BackHover);
            Color borderColor = Color.FromArgb(
                alpha, ColorTable.Border);
            #region 自画三角
            //int spos = 3;
            //Rectangle tmp=new Rectangle(bounds.Location,new Size(bounds.Width-1,bounds.Height-1));
            //int tmpVal = 2;
            //PointF[] pitems = new PointF[]{
            //new Point(0,tmpVal),  new Point(tmpVal,0),
            //new Point(tmp.Width-tmpVal,0), new Point(tmp.Width,tmpVal),
            //new Point(tmp.Width,tmp.Height-tmpVal),   new Point(tmp.Width-tmpVal,tmp.Height),
            //new Point(tmpVal,tmp.Height), new Point(0,tmp.Height-tmpVal)
            //};
            //switch (d)
            //{
            //    case dur.left:
            //        //pitems_left
            //        pitems = new PointF[]{
            //        new Point(spos,tmpVal),new Point(spos+tmpVal,0),
            //        new Point(tmp.Width-tmpVal,0),   new Point(tmp.Width,tmpVal),
            //        new Point(tmp.Width,tmp.Height-tmpVal),new Point(tmp.Width-tmpVal,tmp.Height),
            //        new Point(spos+tmpVal,tmp.Height),  new Point(spos,tmp.Height-tmpVal),
            //        new PointF(spos,tmp.Height/2-3),new PointF(0,tmp.Height/2),new PointF(spos,tmp.Height/2+3)
            //};

            //        break;
            //    case dur.top:
            //        //pitems_Top
            //        pitems = new PointF[]{
            //        new Point(0,spos+tmpVal),new Point(tmpVal,spos),   
            //        new PointF((tmp.Width-spos)/2-3,spos),new PointF((tmp.Width-spos)/2,0),new PointF((tmp.Width-spos)/2+3,spos),
            //        //new PointF((tmp.Width-spos)*4/9,spos),new PointF((tmp.Width-spos)/2,0),new PointF((tmp.Width-spos)*5/9,spos),
            //        new Point(tmp.Width-tmpVal,spos), new Point(tmp.Width,spos+tmpVal),
            //        new Point(tmp.Width,tmp.Height-tmpVal),
            //        new Point(tmp.Width-tmpVal,tmp.Height),
            //        new Point(tmpVal,tmp.Height),
            //        new Point(0,tmp.Height-tmpVal)
            //};
            //        break;
            //    case dur.right:
            //        //pitems_Right
            //        pitems = new PointF[]{
            //        new Point(0,tmpVal),new Point(tmpVal,0),     
            //        new Point((tmp.Width-spos)-tmpVal,0), new Point((tmp.Width-spos),tmpVal),
            //        new PointF((tmp.Width-spos),tmp.Height/2-3),new PointF((tmp.Width),tmp.Height/2),new PointF((tmp.Width-spos),tmp.Height/2+3),
            //        //new PointF((tmp.Width-spos),tmp.Height*3/7),new PointF((tmp.Width),tmp.Height/2),new PointF((tmp.Width-spos),tmp.Height*4/7),
            //        new Point(tmp.Width-spos,tmp.Height-tmpVal),  new Point(tmp.Width-spos-tmpVal,tmp.Height),
            //        new Point(tmpVal,tmp.Height), new Point(0,tmp.Height-tmpVal)
            //};
            //        break;
            //    case dur.bottom:
            //        //pitems_Bottom
            //        pitems = new PointF[]{
            //        new Point(0,tmpVal),new Point(tmpVal,0),   
            //        new Point((tmp.Width)-tmpVal,0), new Point((tmp.Width),tmpVal),
            //        new PointF(tmp.Width,tmp.Height-spos-tmpVal),   new PointF(tmp.Width-tmpVal,tmp.Height-spos),
            //        new Point(tmp.Width/2-3,tmp.Height-spos), new Point(tmp.Width/2,tmp.Height),  new Point(tmp.Width/2+3,tmp.Height-spos),
            //        //new Point(tmp.Width*3/7,tmp.Height-spos), new Point(tmp.Width/2,tmp.Height),  new Point(tmp.Width*4/7,tmp.Height-spos),
            //        new Point(tmpVal,tmp.Height-spos),new Point(0,tmp.Height-spos-tmpVal)
            //        };
            //        break;
            //}
            #endregion
            Bitmap Img = Resources.DNAniu_Bottom;
            switch (d)
            { 
                case dur.bottom:
                    Img= Resources.DNAniu_Bottom;
                    break;
                case dur.left:
                    Img = Resources.left_82;
                    break;
                case dur.right:
                    Img = Resources.right_82;
                    break;
                case dur.top:
                    Img = Resources.down_82;
                    break;
                default:
                    Img = Resources.down_82;
                    break;
            }
            //背景
            using (LinearGradientBrush brush = new LinearGradientBrush(
                bounds,
                backNormalColor,
                baseColor,
                LinearGradientMode.Vertical))
            {
                if (_opacity == 1D)
                {
                    TipCapture();
                    IntPtr hDC = g.GetHdc();

                    NativeMethodsToop.BitBlt(
                        hDC, 0, 0, bounds.Width, bounds.Height,
                        _backDc.Hdc, 0, 0, 0xCC0020);
                    g.ReleaseHdc(hDC);

                }
                if (d != dur.normal)
                {
                    g.DrawImage(Img, bounds);
                }
                else
                {
                    //int spos = 3;
                    Rectangle tmp = new Rectangle(bounds.Location, new Size(bounds.Width - 1, bounds.Height - 1));
                    int tmpVal = 2;
                    PointF[] pitems = new PointF[]{
                    new Point(0,tmpVal),  new Point(tmpVal,0),
                    new Point(tmp.Width-tmpVal,0), new Point(tmp.Width,tmpVal),
                    new Point(tmp.Width,tmp.Height-tmpVal),   new Point(tmp.Width-tmpVal,tmp.Height),
                    new Point(tmpVal,tmp.Height), new Point(0,tmp.Height-tmpVal)
                    };
                     g.FillPolygon(new SolidBrush(Color.White),pitems);
                    g.DrawPolygon(new Pen(FrmStyle.BorderColor), pitems);
                }
            }

            #region border


           // g.DrawPolygon(new Pen(borderColor), pitems);
            #endregion
            //背景
            //using (LinearGradientBrush brush = new LinearGradientBrush(
            //   bounds,
            //   backNormalColor,
            //   baseColor,
            //   LinearGradientMode.Vertical))
            //{
            //    g.FillPolygon(brush, pitems);
            //}
          
           



            Rectangle imageRect = Rectangle.Empty;
            Rectangle titleRect;
            Rectangle tipRect;

            if (base.ToolTipIcon != ToolTipIcon.None)
            {
                tipTextXOffset = defaultXOffset;
                tipTextYOffset = defaultTopHeight;

                imageRect = new Rectangle(
                    bounds.X + defaultXOffset - (ImageSize.Width - 16) / 2,
                    bounds.Y + (defaultTopHeight - _imageSize.Height) / 2,
                    _imageSize.Width,
                    _imageSize.Height);

                Image image = _image;
                bool bDispose = false;

                if (image == null)
                {
                    Icon icon = GetIcon();
                    if (icon != null)
                    {
                        image = icon.ToBitmap();
                        bDispose = true;
                    }
                }

                if (image != null)
                {
                    using (InterpolationModeGraphics ig =
                        new InterpolationModeGraphics(g))
                    {
                        if (_opacity < 1D)
                        {
                            RenderHelper.RenderAlphaImage(
                                g,
                                image,
                                imageRect,
                                (float)_opacity);
                        }
                        else
                        {
                            g.DrawImage(
                                image,
                                imageRect,
                                0,
                                0,
                                image.Width,
                                image.Height,
                                GraphicsUnit.Pixel);
                        }
                    }

                    if (bDispose)
                    {
                        image.Dispose();
                    }
                }
            }

            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
           
            int sposTitle = 2;
            if (!string.IsNullOrEmpty(base.ToolTipTitle))
            {
                tipTextXOffset = defaultXOffset;
                tipTextYOffset = defaultTopHeight;

                int x = imageRect.IsEmpty ?
                    defaultXOffset : imageRect.Right + 3;

                titleRect = new Rectangle(
                    x + sposTitle,
                    bounds.Y + sposTitle,
                    bounds.Width - x,
                    defaultTopHeight);

                Color foreColor = Color.FromArgb(
                    alpha, ColorTable.TitleFore);

                using (Brush brush = new SolidBrush(foreColor))
                {
                    g.DrawString(
                        base.ToolTipTitle,
                        _titleFont,
                        brush,
                        titleRect,
                        sf);
                }
            }

            if (!string.IsNullOrEmpty(e.ToolTipText))
            {
                tipRect = new Rectangle(
                    bounds.X+sposTitle + tipTextXOffset,
                    bounds.Y + tipTextYOffset,
                    bounds.Width - tipTextXOffset * 2,
                    bounds.Height - tipTextYOffset);

                sf = StringFormat.GenericTypographic;

                Color foreColor = Color.FromArgb(
                   alpha, ColorTable.TipFore);
                //sf.Alignment = StringAlignment.Center;
                using (Brush brush = new SolidBrush(foreColor))
                {

                        g.DrawString(
                            e.ToolTipText,
                            _tipFont,
                            brush,
                            tipRect,
                            sf);
                }
            }
        }

        private void TipCapture()
        {
            IntPtr handle = Handle;
            if (handle == IntPtr.Zero)
            {
                return;
            }

            RECT rect = new RECT();

            NativeMethodsToop.GetWindowRect(handle, ref rect);

            Size size = new Size(
                rect.Right - rect.Left, 
                rect.Bottom - rect.Top);

            _backDc = new ImageDc(size.Width, size.Height);
            IntPtr pD = NativeMethodsToop.GetDesktopWindow();
            IntPtr pH = NativeMethodsToop.GetDC(pD);

            NativeMethodsToop.BitBlt(
                _backDc.Hdc, 
                0, 0, size.Width, size.Height, 
                pH, rect.Left, rect.Top, 0xCC0020);
            NativeMethodsToop.ReleaseDC(pD, pH);
        }

        private Icon GetIcon()
        {
            switch (base.ToolTipIcon)
            {
                case ToolTipIcon.Info:
                    return SystemIcons.Information;

                case ToolTipIcon.Warning:
                    return SystemIcons.Warning;
                case ToolTipIcon.Error:
                    return SystemIcons.Error;
                default:
                    return null;
            }
        }

        #endregion
    }
}
