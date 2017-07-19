using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;

namespace IMClient.Controls
{
    public class ItemLine : ItemBase
    {
        private bool autoLine2 = false;

        [Browsable(true), Description("是否自动绘制第二根线")]
        public bool AutoLine2
        {
            get { return autoLine2; }
            set { autoLine2 = value; }
        }

        private bool isShowTwoLine = false;

        [Browsable(true), Description("是否画两根分割线")]
        public bool IsShowTwoLine
        {
            get { return isShowTwoLine; }
            set { isShowTwoLine = value; }
        }

        /// <summary>
        /// 是否显示标题图片
        /// </summary>
        private bool isShowImage = false;

        [Browsable(true), Description("是否显示标题图片")]
        public bool IsShowImage
        {
            get { return isShowImage; }
            set { isShowImage = value; }
        }

        private bool isShowFont = false;

        [Browsable(true), Description("是否显示标题")]
        public bool IsShowFont
        {
            get { return isShowFont; }
            set { isShowFont = value; }
        }

        private Image titleImage = Properties.Resources.add;

        [Browsable(true), Description("标题栏")]
        public Image TitleImage
        {
            get { return titleImage; }
            set { titleImage = value; }
        }

        /// <summary>
        /// 图片大小
        /// </summary>
        private Size imageSize = new Size(20, 20);

        [Browsable(true), Description("图片大小")]
        public Size ImageSize
        {
            get { return imageSize; }
            set { imageSize = value; }
        }

        /// <summary>
        /// 分割线位置
        /// </summary>
        private lineLocationType lineLocation = lineLocationType.center;

        [Browsable(true), Description("分割线位置")]
        public lineLocationType LineLocation
        {
            get { return lineLocation; }
            set { lineLocation = value; }
        }


        private lineType linetype = lineType.solid;
        [Browsable(true), Description("分割线类型")]
        public lineType Linetype
        {
            get { return linetype; }
            set { linetype = value; }
        }

        private bool isVertocal = true;

        [Browsable(true), Description("是否垂直")]
        public bool IsVertocal
        {
            get { return isVertocal; }
            set { isVertocal = value; }
        }

        /// <summary>
        /// 主分割线
        /// </summary>
        private Pen pen = new Pen(FrmStyle.BorderColor);

        [Browsable(true), Description("主分割线")]
        public Pen Pen
        {
            get { return pen; }
            set { pen = value; }
        }

        private float penWidth = 1.0f;

        [Browsable(true), Description("主分割线宽度")]
        public float PenWidth
        {
            get { return penWidth; }
            set { penWidth = value; }
        }

        private float penWidth2 = 1.0f;
        [Browsable(true), Description("副分割线宽度")]
        public float PenWidth2
        {
            get { return penWidth2; }
            set { penWidth2 = value; }
        }

        private Color penColor = FrmStyle.BorderColor;

        [Browsable(true), Description("主分割线颜色")]
        public Color PenColor
        {
            get { return penColor; }
            set { penColor = value; }
        }

        private Color penColor2 = FrmStyle.BorderColor;
        [Browsable(true), Description("副分割线颜色")]
        public Color PenColor2
        {
            get { return penColor2; }
            set { penColor2 = value; }
        }

        /// <summary>
        /// 副分割线
        /// </summary>
        private Pen pen2 = new Pen(FrmStyle.TitleColor2);

        [Browsable(true), Description("副分割线")]
        public Pen Pen2
        {
            get { return pen2; }
            set { pen2 = value; }
        }

        /// <summary>
        /// 第二分割线长度
        /// </summary>
        private int lineLength = 20;

        [Browsable(true), Description("副分割线长度")]
        public int LineLength
        {
            get { return lineLength; }
            set { lineLength = value; }
        }

        /// <summary>
        /// 分割线与字间隔
        /// </summary>
        private int fontLineMargin = 5;

        [Browsable(true), Description("字与分割线间隔")]
        public int FontLineMargin
        {
            get { return fontLineMargin; }
            set { fontLineMargin = value; }
        }

        private int fontImageMargin = 2;

        [Browsable(true), Description("字与图片间隔")]
        public int FontImageMargin
        {
            get { return fontImageMargin; }
            set { fontImageMargin = value; }
        }

        /// <summary>
        /// 分割线与字间隔
        /// </summary>
        private int imageLineMargin = 5;

        [Browsable(true), Description("图片与分割线间隔")]
        public int ImageLineMargin
        {
            get { return imageLineMargin; }
            set { imageLineMargin = value; }
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {

            base.OnPaint(e);
            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            if (this.linetype == lineType.spash)
            {
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
                pen.DashPattern = new float[] { 5, 5 };
                //pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            }
            else
            {
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            }
            if (isVertocal)//垂直
            {
                int x = this.Width / 2;

                e.Graphics.DrawLine(pen, new Point(x, 0), new Point(x, this.Height));
            }
            else//水平 
            {
                //int y = this.Height / 2;
                //e.Graphics.DrawLine(pen, new Point(0, y), new Point(this.Width, y));
                drawHorizon(e.Graphics);
            }
        }

        public void drawHorizon(Graphics g)
        {
            int y = this.Height / 2;
            if (lineLocation == lineLocationType.Far)
            {
                y = this.Height - this.Margin.Bottom;
            }
            else if (lineLocation == lineLocationType.near)
            {
                y = this.Margin.Top;
            }

            pen.Color = this.penColor;
            pen.Width = this.penWidth;
            pen2.Color = this.penColor2;
            pen2.Width = this.penWidth2;

            g.DrawLine(pen, new Point(0, y), new Point(this.Width, y));

            int font_X = this.Margin.Left;
            int font_Y = y - this.fontLineMargin - this.Font.Height;
            if (isShowFont)
            {
                if (lineLocation == lineLocationType.near)
                {
                    font_Y = y + this.fontLineMargin + this.Font.Height;
                }

                if (isShowImage)
                {
                    int image_X = font_X;
                    int image_Y = y - imageLineMargin - this.imageSize.Height;
                    if (lineLocation == lineLocationType.near)
                    {
                        image_Y = y + imageLineMargin + this.imageSize.Height;
                    }
                    Rectangle rect = new Rectangle(new Point(image_X, image_Y), imageSize);
                    g.DrawImage(this.titleImage, rect);
                    font_X = this.imageSize.Width + font_X + fontImageMargin;
                }
                g.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), new Point(font_X, font_Y));
            }

            if (isShowTwoLine)
            {
                if (!autoLine2)
                {
                    g.DrawLine(pen2, new Point(0, y), new Point(this.lineLength, y));
                }
                else
                {
                    int length = Convert.ToInt32(g.MeasureString(this.Text, this.Font).Width + 1) + font_X;
                    g.DrawLine(pen2, new Point(0, y), new Point(length, y));
                }
            }
        }

    }
}
