using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Text;
using IMClient.Controls;

namespace IMClient.Controls
{
    public class ItemTitle : ItemBase
    {
        public ItemTitle()
        {
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Center;
        }

        private StringFormat sf = new StringFormat();
        private bool stretch = false;


        private Color itembackColor;

        public Color ItembackColor
        {
            get { return itembackColor; }
            set { itembackColor = value; }
        }
        private Image itembackImage;

        public Image ItembackImage
        {
            get { return itembackImage; }
            set { itembackImage = value; }
        }
        private string itemstrTitle;

        public string ItemstrTitle
        {
            get { return itemstrTitle; }
            set { itemstrTitle = value; }
        }
        private Image itemIcon;

        public Image ItemIcon
        {
            get { return itemIcon; }
            set { itemIcon = value; }
        }

        private SolidBrush itemFontColor=new SolidBrush(FrmStyle.TitleColor2);

        public SolidBrush ItemFontColor
        {
            get { return itemFontColor; }
            set { itemFontColor = value; }
        }
        private Font itemFont=new Font("微软雅黑",12f);

        public Font ItemFont
        {
            get { return itemFont; }
            set { itemFont = value; }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            if (DesignMode)
                return;

            PaintBack(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (DesignMode)
                return;

            PaintIcon(e);

            PaintTitle(e);

        }

        /// <summary>
        /// 根据情况绘制背景
        /// </summary>
        /// <param name="e"></param>
        private void PaintBack(PaintEventArgs e)
        {
            Rectangle rect = e.ClipRectangle;
            rect.Width = rect.Width + 1;
            rect.Height = rect.Height + 1;
            if (itembackImage != null)
            {
                
                if (stretch)
                    e.Graphics.DrawImage(itembackImage, rect);
                else
                    e.Graphics.DrawImage(itembackImage, rect, rect, GraphicsUnit.Pixel);

            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(itembackColor), rect);
            }
        }

        /// <summary>
        /// 绘制图标
        /// </summary>
        /// <param name="g"></param>
        private void PaintIcon(PaintEventArgs e)
        {
            if(itemIcon!=null)
            {
                int spos=(this.Height-itemIcon.Height)/2;
                Rectangle rect = new Rectangle(
                    new Point(  spos, spos),new Size(itemIcon.Width, itemIcon.Height) );
                e.Graphics.DrawImage(itemIcon, rect);
            }
        }

        /// <summary>
        /// 绘制标题
        /// </summary>
        /// <param name="g"></param>
        private void PaintTitle(PaintEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(itemstrTitle))
            {
                int spos = 0;
                if (itemIcon != null)
                {
                    spos = (this.Height - itemIcon.Height) / 2 + itemIcon.Width;
                }
                spos += 5;      //固定间隔

                Rectangle rect=Rectangle.Empty;
                //if (itemIcon != null)
                //{
                //    int spos2 = (this.Height - itemIcon.Height) / 2;
                //    rect = new Rectangle(
                //        new Point(spos2, spos2), new Size(itemIcon.Width, itemIcon.Height));
                //}
                RectangleF TextRect = new RectangleF(
                    new Point(spos + rect.Location.X + rect.Width, 0), new Size(this.Width - spos, this.Height));
                e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                if (itemFontColor == null)
                {
                    itemFontColor = new SolidBrush(FrmStyle.TitleColor2);
                }
                e.Graphics.DrawString(itemstrTitle, itemFont, itemFontColor, TextRect, sf);
            }
        }
    }
}
