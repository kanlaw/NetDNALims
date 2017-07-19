using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace IMClient.Controls.Drawing
{
    public class TextContent : IDrawContent
    {
        Font m_font;
        Brush m_brush;

        string m_text;

        public TextContent()
        {
        }

        public string Text
        {
            get { return this.m_text; }
            set { this.m_text = value; }
        }

        public Size BeginDraw(Graphics g, CssStyle.CssStyle style)
        {
            this.m_font = style.Font;
            this.m_brush = style.Color;

            SizeF s = g.MeasureString(this.m_text, this.m_font);

            return s.ToSize();
        }

        public void Draw(Graphics g)
        {
            g.DrawString(this.m_text, this.m_font, this.m_brush, 0, 0);
        }

        public void EndDraw()
        {
        }
    }
}
