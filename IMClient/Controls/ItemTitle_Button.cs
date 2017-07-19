using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Text;

namespace IMClient.Controls
{
    public class ItemTitle_Button : ItemBase
    {
        public ItemTitle_Button()
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

        private Rectangle btnRect;

        public Rectangle BtnRect
        {
            get { return btnRect; }
            set { btnRect = value; }
        }

        private string btnContent;

        public string BtnContent
        {
            get { return btnContent; }
            set { btnContent = value; }
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

        private SolidBrush itemFontColor;

        public SolidBrush ItemFontColor
        {
            get { return itemFontColor; }
            set { itemFontColor = value; }
        }
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

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            if (DesignMode)
                return;

            PaintBack(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (this.btnRect.Contains(e.Location))
            {
                //实现事件
                //MessageBox.Show("ok");
            }
            else {
                //MessageBox.Show("no");
            }
           
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
                Rectangle rect = new Rectangle(spos, spos, itemIcon.Width, itemIcon.Height);
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

                RectangleF TextRect = new RectangleF(spos, 0, this.Width - spos, this.Height);
                
                e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                e.Graphics.DrawString(itemstrTitle, itemFont, itemFontColor, TextRect, sf);
                e.Graphics.DrawString(btnContent, itemFont2, itemFontColor, btnRect, sf);
            }
        }
    }
}
