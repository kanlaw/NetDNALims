using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Drawing.Text;
using IMClient.Controls;

namespace IMClient.Controls
{
    public class ItemCheckBox : ItemBase
    {
        private bool enableFont = false;
        [Browsable(true), Description("是否启用为选中字体色变化")]
        public bool EnableFont
        {
            get { return enableFont; }
            set { enableFont = value; }
        }

        StringFormat sf = new StringFormat();

        [Browsable(true), Description("字体格式化")]
        public StringFormat Sf
        {
            get { return sf; }
            set { sf = value; }
        }

        private Color enableColor = Color.FromArgb(220, 220, 220);

        [Browsable(true), Description("为选中时字体颜色")]
        public Color EnableColor
        {
            get { return enableColor; }
            set { enableColor = value; }
        }
  
        private Color fontColor = FrmStyle.TitleColor2;

        [Browsable(true), Description("字体颜色")]
        public Color FontColor
        {
            get { return fontColor; }
            set { fontColor = value; }
        }

        private string contentStr = string.Empty;

        [Browsable(true),Description("单选框所对应名称")]
        public string ContentStr
        {
            get { return contentStr; }
            set { contentStr = value; }
        }
        private Image unSelectImg = Properties.Resources.Check_box_no;

        private Image SelectedImg = Properties.Resources.Check_box_yes;
        public ItemCheckBox()
        {
            this.Size = this.unSelectImg.Size;
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Center;
            this.Invalidate();
        }

        private bool isChecked = true;

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsChecked
        {
            get { return isChecked; }
            set { isChecked = value; }
        }

        /// <summary>
        /// 存储临时变量
        /// </summary>
        private object tmpVal;

        public object TmpVal
        {
            get { return tmpVal; }
            set { tmpVal = value; }
        }

     

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            PaintChkImg(e.Graphics);
        }

        protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
        {
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            base.OnMouseLeave(e);
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (isChecked)
            {
                this.isChecked = false;
            }
            else {
                this.isChecked = true;
            }
            this.Invalidate();
        }


        #region  自定义事件

        private void PaintChkImg(Graphics g)
        {
           
            RectangleF rect = new RectangleF(new PointF(0, (this.Height-this.SelectedImg.Height)/2), this.SelectedImg.Size);
            Image drawImg = this.unSelectImg;
            if (isChecked)
            {
                drawImg = this.SelectedImg;
            }
            g.DrawImage(drawImg, rect);

            if (!string.IsNullOrEmpty(contentStr))
            {
                g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                float x = rect.Location.X + rect.Width+5;
                float y = 0;
                RectangleF r=new RectangleF(new PointF(x,y),new SizeF(this.Width-rect.Width,this.Height));

                Color tmp=this.fontColor;
                if (enableFont && !this.isChecked)
                    tmp = this.enableColor;
                g.DrawString(contentStr, this.Font, new SolidBrush(tmp), r, sf); ;
            }
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            this.sf.Dispose();
            base.Dispose(disposing);
        }
    }
}
