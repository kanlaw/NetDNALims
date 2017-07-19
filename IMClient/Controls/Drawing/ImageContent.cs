using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using IMClient.Controls.CssStyle;

namespace IMClient.Controls.Drawing
{
    public class ImageContext : IDrawContent
    {
        Image m_image;

        public ImageContext()
        { 
        }

        public Image Image
        {
            get { return this.m_image; }
            set { this.m_image = value; }
        }

        public Size BeginDraw(Graphics g, CssStyle.CssStyle style)
        {
            return this.m_image.Size;
        }

      

        public void Draw(Graphics g)
        {
            g.DrawImage(this.m_image, 0, 0);
        }

        public void EndDraw()
        {
        }
    }
}
