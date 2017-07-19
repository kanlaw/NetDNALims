using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Text;
using System.Windows.Forms;

using IMClient.Controls.ToolTipEX;
namespace IMClient.Controls
{
    public partial class ImageButton : Button
    {
        private StringFormat sf = new StringFormat();
        private SolidBrush TitleColor;
        private Pen penForeColor;
        private bool stretch;  //确定背景图片是否拉伸
        private string showText; //显示的文字
        private Image backpic;   //背景图片
        private Color colorBack;    //用于填充背景的颜色
        private ToolTipEx tooltip;

        private Image staticpic; //静态图片
        private Image activepic; //鼠标移动时的图片
        private Image presspic;  //鼠标按下的图片
        private Image unablepic; //禁用时的图片
        private bool active = false; //鼠标是否经过按钮
        private bool press = false;  //鼠标是否按下
        private bool toggle = false;    //是否开关状态
        private bool Supporttoggle = false;    //是否开关状态
        private bool toolTipShown = false;//是否开启显示ToolTip
        private int tooltipX = 0;
        private double toolTipOpacity = 1.0d;

        [Browsable(true), Description("toolTip透明度")]
        public double ToolTipOpacity
        {
            get { return toolTipOpacity; }
            set { toolTipOpacity = value; }
        }

        [Browsable(true), Description("toolTip显示时X 坐标")]
        public int TooltipX
        {
            get {
                
                return tooltipX; }
            set { tooltipX = value; }
        }
        private int tooltipY = 0;

        [Browsable(true), Description("toolTip显示时Y 坐标")]
        public int TooltipY
        {
            get { return tooltipY; }
            set { tooltipY = value; }
        }



        [Browsable(true), Description("是否启用显示ToolTip")]
        public bool ToolTipShown
        {
            get { return toolTipShown; }
            set { toolTipShown = value; }
        }
        private RectangleF TextRect;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ImageButton()
        {
            stretch = false;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor |
                          ControlStyles.UserPaint, true);
            this.colorBack = Color.Transparent;
            SetPara();
        }

        public bool Create()
        {
            if (this.Parent.BackgroundImage != null)
            {
                backpic = new Bitmap(this.Width, this.Height);
                Graphics gb = Graphics.FromImage(backpic);
                gb.DrawImage(this.Parent.BackgroundImage, new Rectangle(0, 0, this.Width, this.Height), new Rectangle(this.Location, this.Size), GraphicsUnit.Pixel);
                gb.Dispose();
                return true;
            }
            return false;
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);

            if (DesignMode)
                return;

            
        }

        public void SetPara()
        {
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            
            penForeColor = new Pen(ForeColor);
            TitleColor = new SolidBrush(Color.White);
            TextRect = new RectangleF(2f, 55f, (float)Width, (float)Height);
        }

        /// <summary>
        /// 设定新的文本显示区域
        /// </summary>
        /// <param name="newRect"></param>
        public void SetTextRect(RectangleF newRect)
        {
            TitleColor = new SolidBrush(ForeColor);
            TextRect = newRect;
        }

        [Browsable(true), Description("用于填充背景的颜色")]
        public Color ColorBack
        {
            get { return colorBack; }
            set { colorBack = value; }
        }

        [Browsable(true), Description("是否支持开关状态")]
        public bool SupportToggle
        {
            get { return Supporttoggle; }
            set { Supporttoggle = value; }
        }

        [Browsable(true), Description("是否处于开关状态")]
        public bool Toggle
        {
            get { return toggle; }
            set
            {
                if (toggle != value)
                {
                    toggle = value;
                    this.Invalidate();
                }
                else
                    toggle = value;
            }
        }

        [Browsable(true), Description("显示的文字")]
        public string ShowText
        {
            get { return showText; }
            set { showText = value; }
        }

        [Browsable(true), Description("背景图片是否拉伸")]
        public bool Stretch
        {
            get { return stretch; }
            set { stretch = value; }
        }

        [Browsable(true), Description("背景图片")]
        public Image BackImage
        {
            get { return backpic; }
            set { backpic = value; }
        }

        [Browsable(true), Description("静态的图片")]
        public Image Staticpic
        {
            get { return staticpic; }
            set { staticpic = value; }
        }

        [Browsable(true), Description("鼠标移动时的图片")]
        public Image Activepic
        {
            get { return activepic; }
            set { activepic = value; }
        }

        [Browsable(true), Description("按下时的图片")]
        public Image Presspic
        {
            get { return presspic; }
            set { presspic = value; }
        }

        [Browsable(true), Description("禁用时的图片")]
        public Image Unablepic
        {
            get { return unablepic; }
            set { unablepic = value; }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            press = false;
            this.Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
            base.OnMouseDown(e);
            press = true;
            this.Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            if (active == false)
            {
                active = true;
                this.Invalidate();
            }
        }

        protected override void OnMouseMove(MouseEventArgs mevent)
        {
            this.Cursor = Cursors.Hand;
            if (this.toolTipShown)
            {
                if (this.tooltip == null)
                {
                    this.tooltip = new ToolTipEx();
                }
                int x = tooltipX;
                int y = tooltipY;
                this.tooltip.Opacity = toolTipOpacity;
                this.tooltip.Image = this.staticpic;
                this.tooltip.Show(this.Text, this, new Point(x, y), 500);
            }
            base.OnMouseMove(mevent);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (active == true)
            {
                this.Cursor = Cursors.Arrow;
                active = false;
                this.Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (DesignMode)
                return;

            Rectangle rect = e.ClipRectangle;
            if (backpic != null)
            {
                e.Graphics.DrawImage(backpic, rect);
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(BackColor), rect);
            }

            if (this.Enabled)
            {
                if (press || toggle)
                {
                    if (presspic != null)
                    {
                        
                        if (stretch)
                            e.Graphics.DrawImage(presspic, rect);
                        else
                            e.Graphics.DrawImage(presspic, rect, rect, GraphicsUnit.Pixel);
                    }
                }
                else if (active)
                {
                    if (activepic != null)
                    {
                        if (stretch)
                            e.Graphics.DrawImage(activepic, rect);
                        else
                            e.Graphics.DrawImage(activepic, rect, rect, GraphicsUnit.Pixel);
                    }
                }
                else
                {
                    if (staticpic != null)
                    {
                        //Bitmap bitmap = new Bitmap(staticpic); 
                        //bitmap.MakeTransparent(Color.Transparent);//透明背景
                        //if (stretch)
                        //    e.Graphics.DrawImage(bitmap, rect);
                        //else
                        //    e.Graphics.DrawImage(bitmap, rect, rect, GraphicsUnit.Pixel);
                        //bitmap.Save(pngfile, System.Drawing.Imaging.ImageFormat.Png)
                        if (stretch)
                            e.Graphics.DrawImage(staticpic, rect);
                        else
                            e.Graphics.DrawImage(staticpic, rect, rect, GraphicsUnit.Pixel);
                    }
                }
            }
            else
            {
                if (unablepic != null)
                {
                    if (stretch)
                        e.Graphics.DrawImage(unablepic, rect);
                    else
                        e.Graphics.DrawImage(unablepic, rect, rect, GraphicsUnit.Pixel);
                }
                else
                {
                    if (staticpic != null)
                    {
                        if (stretch)
                            e.Graphics.DrawImage(staticpic, rect);
                        else
                            e.Graphics.DrawImage(staticpic, rect, rect, GraphicsUnit.Pixel);
                    }
                }

            }
            if (!string.IsNullOrWhiteSpace(showText))
            {
                e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                e.Graphics.DrawString(showText, Font, TitleColor, TextRect, sf);
            }
        }
    }
}
