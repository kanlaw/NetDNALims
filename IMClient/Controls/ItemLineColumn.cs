using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Text;

namespace IMClient.Controls
{
    public class ItemLineColumn : Control
    {
        private Color lineColor = Color.Red;

        private int currentIndex = 0;
        private int maxIndex = 0;
        private int space = 5;
        private string measurement = "票";

        public ItemLineColumn()
        {
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.SetStyle(ControlStyles.Opaque, false);
            base.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

            base.BackColor = Color.Transparent;
           
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            int offSetX =  (int)Math.Ceiling(g.MeasureString(this.Text, this.Font).Width);
            int widthMax = this.Width - offSetX-this.space;
            string val = currentIndex + this.measurement;
            int offSetX_Right= (int)Math.Ceiling(g.MeasureString(val, this.Font).Width);
            widthMax = widthMax - offSetX - this.space;
            using (StringFormat sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Near;
                sf.LineAlignment = StringAlignment.Center;
                using (SolidBrush sb = new SolidBrush(this.ForeColor))
                {
                    g.DrawString(this.Text, this.Font, sb, this.ClientRectangle, sf);
                }
                sf.Alignment = StringAlignment.Far;
                using (SolidBrush sb = new SolidBrush(this.ForeColor))
                {
                    g.DrawString(val, this.Font, sb, this.ClientRectangle, sf);
                }
            }
            using (SolidBrush sb = new SolidBrush(this.lineColor))
            {
                int currentWidth = maxIndex == 0 ? 0 : this.currentIndex* widthMax / maxIndex;
                currentWidth = currentWidth > 100 ? 100 : currentWidth;
                g.FillRectangle(sb, new Rectangle(new Point(offSetX+ this.space, 0), new Size(currentWidth, this.Height)));
            }

        }

        [Browsable(true), Description("柱子颜色")]
        public Color LineColor { get => lineColor; set => lineColor = value; }
        [Browsable(true), Description("当前值")]
        public int CurrentIndex { get => currentIndex; set => currentIndex = value; }
        [Browsable(true), Description("最大值")]
        public int MaxIndex { get => maxIndex; set => maxIndex = value; }
        [Browsable(true), Description("文字与图形间隔")]
        public int Space { get => space; set => space = value; }
        [Browsable(true), Description("计量单位")]
        public string Measurement { get => measurement; set => measurement = value; }
    }
}
