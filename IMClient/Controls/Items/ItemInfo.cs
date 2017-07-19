using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace IMClient.Controls.Items
{
    public class ItemInfo : ItemBase
    {
        //构造
        public ItemInfo()
        {
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Center;
        }

        #region 字段、属性

        private bool active = false;    //鼠标是否经过控件

        //布局信息对象
        private StringFormat sf = new StringFormat();

        //获取三角图标
        //private Image triangle = ResMain.Triangle_more;

        //是否能拉伸图片
        private bool stretch = false;
        //背景颜色
        private Color itembackColor;
        [Browsable(true),Description("初始填充背景的颜色")]
        public Color ItembackColor
        {
            get { return itembackColor; }
            set { itembackColor = value; }
        }

        //鼠标移上的背景颜色
        private Color itembackColorMouseOn;
        [Browsable(true),Description("鼠标移上的背景颜色")]
        public Color ItembackColorMouseOn
        {
            get { return itembackColorMouseOn; }
            set { itembackColorMouseOn = value; }
        }

        //背景图像
        private Image itembackImage;

        public Image ItembackImage
        {
            get { return itembackImage; }
            set { itembackImage = value; }
        }
        //标题文字上
        private string itemstrTitleT;

        public string ItemstrTitleT
        {
            get { return itemstrTitleT; }
            set { itemstrTitleT = value; }
        }
        //标题文字下
        private string itemstrTitleB;

        public string ItemstrTitleB
        {
            get { return itemstrTitleB; }
            set { itemstrTitleB = value; }
        }

        //初始图标
        private Image itemIcon;
        [Browsable(true),Description("初始时的图标")]
        public Image ItemIcon
        {
            get { return itemIcon; }
            set { itemIcon = value; }
        }

        //选中图标
        private Image itemIconMouseOn;
        [Browsable(true),Description("鼠标选中时的图标")]
        public Image ItemIconMouseOn
        {
            get { return itemIconMouseOn; }
            set { itemIconMouseOn = value; }
        }

        //文字颜色刷上
        private SolidBrush itemFontColor;

        public SolidBrush ItemFontColor
        {
            get { return itemFontColor; }
            set { itemFontColor = value; }
        }
        //文字颜色刷下
        private SolidBrush itemFontColor2;

        public SolidBrush ItemFontColor2
        {
            get { return itemFontColor2; }
            set { itemFontColor2 = value; }
        }
        //文字样式上
        private Font itemFont;

        public Font ItemFont
        {
            get { return itemFont; }
            set { itemFont = value; }
        }

        //文字样式下
        private Font itemFont2;

        public Font ItemFont2
        {
            get { return itemFont2; }
            set { itemFont2 = value; }
        }

        //是否有底部虚线
        private bool bottomBorder = true;

        public bool BottomBorder
        {
            get { return bottomBorder; }
            set { bottomBorder = value; }
        }

        #endregion 字段、属性

        #region 重写方法

        //鼠标进入
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            if (active == false)
            {
                active = true;
                this.Invalidate();
            }
        }

        //鼠标移开
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (active == true)
            {
                active = false;
                this.Invalidate();
            }
        }


        //绘制背景
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
            //判断当前控件是否处于编辑模式
            if(DesignMode)
            {
                return;
            }
            PaintBack(pevent);
        }

        //绘制控件
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (DesignMode)
            {
                return;
            }
            PaintIcon(e);
            PaintTitle(e);
            PaintBottomBorder(e);
            PaintTriangle(e);
        }

        #endregion 重写方法

        #region 自定义方法
        /// <summary>
        /// 根据情况绘制背景
        /// </summary>
        /// <param name="e"></param>
        private void PaintBack(PaintEventArgs e)
        {
            //获取当前控件的矩形对象
            Rectangle rect = e.ClipRectangle;
            //判断是否有背景图片
            if (itembackImage != null)
            {
                //判断是否拉伸图片
                if(stretch)
                {
                    e.Graphics.DrawImage(itembackImage, rect);
                }
                else
                {
                    e.Graphics.DrawImage(itembackImage, rect, rect, GraphicsUnit.Pixel);
                }
            }
            else
            {
                //没有图片填充背景颜色
                //e.Graphics.FillRectangle(new SolidBrush(itembackColor), rect);
                if (!active)
                {
                    e.Graphics.FillRectangle(new SolidBrush(itembackColor), rect);
                }
                else
                {
                    e.Graphics.FillRectangle(new SolidBrush(itembackColorMouseOn), rect);
                }
            }
        }

        /// <summary>
        /// 绘制图标
        /// </summary>
        /// <param name="e"></param>
        private void PaintIcon(PaintEventArgs e)
        {

            if (!active)
            {
                //选中时图标
                if (itemIcon != null)
                {
                    //计算图标起始点坐标
                    int spos = (Height - itemIcon.Height) / 2;
                    //获取图标的画图位置
                    Rectangle rect = new Rectangle(spos, spos, itemIcon.Width, itemIcon.Height);
                    //根据位置画图标
                    e.Graphics.DrawImage(itemIcon, rect);
                }
            }
            else
            {
                //离开时图标
                if (itemIconMouseOn != null)
                {
                    //计算图标起始点坐标
                    int spos = (Height - itemIconMouseOn.Height) / 2;
                    //获取图标的画图位置
                    Rectangle rect = new Rectangle(spos, spos, itemIconMouseOn.Width, itemIconMouseOn.Height);
                    //根据位置画图标
                    e.Graphics.DrawImage(itemIconMouseOn, rect);
                }
            }

        }

        /// <summary>
        /// 绘制标题
        /// </summary>
        /// <param name="e"></param>
        private void PaintTitle(PaintEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(itemstrTitleT))
            {
                int spos = 0;
                if (itemIcon != null)
                {
                    spos = (Height - itemIcon.Height) / 2 + itemIcon.Width;
                }
                spos += 12;      //固定间隔
                RectangleF TextRect = new RectangleF(spos, 5, Width - spos, Height/2);
                //文本呈现高质量
                e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                //绘制文字(上)、指定文字格式
                e.Graphics.DrawString(itemstrTitleT, itemFont, itemFontColor, TextRect, sf);


                RectangleF TextRect2 = new RectangleF(spos, (Height / 2) - 5, Width - spos, Height / 2);
                //绘制文字(下)、指定文字格式
                e.Graphics.DrawString(itemstrTitleB, itemFont2, itemFontColor2, TextRect2, sf);
            }
        }

        //绘制底部边线
        private void PaintBottomBorder(PaintEventArgs e)
        {
            if (bottomBorder)
            {
                Pen p = new Pen(Color.DarkGray, 2);
                p.DashStyle = DashStyle.Dash;
                e.Graphics.DrawLine(p, new Point(0, Height - 2), new Point(Width, Height - 2));
            }
        }

        //绘制三角展开图标
        private void PaintTriangle(PaintEventArgs e)
        {
            ////计算图标起始点坐标
            //int x = Width - triangle.Width - 10;
            //int y = (Height - triangle.Height) / 2;
            ////获取图标的画图位置
            //Rectangle rect = new Rectangle(x, y, triangle.Width, triangle.Height);
            ////根据位置画图标
            //e.Graphics.DrawImage(triangle, rect);

            //画三角
            Point p1 = new Point(160, Height / 2 - 5);
            Point p2 = new Point(160, Height / 2 + 5);
            Point p3 = new Point(165, Height / 2);
            e.Graphics.FillPolygon(new SolidBrush(Color.FromArgb(169, 169, 169)), new Point[3] { p1, p2, p3 });
        }

        #endregion 自定义方法
    }
}
