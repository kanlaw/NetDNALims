using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace IMClient.Controls.Drawing
{
    public delegate void DrawProc(Graphics g);

    public class CanvasContent : IDrawContent
    {
        Size m_canvasSize = new Size();
        DrawProc m_drawProc;

        public CanvasContent()
        {
        }

        public Size Canvas
        {
            get { return this.m_canvasSize; }
            set { this.m_canvasSize = value; }
        }

        public DrawProc DrawProc
        {
            get { return this.m_drawProc; }
            set { this.m_drawProc = value; }
        }

        public Size BeginDraw(Graphics g, CssStyle.CssStyle style)
        {
            return this.m_canvasSize;
        }

        public void Draw(Graphics g)
        {
            this.m_drawProc(g);
        }

        public void EndDraw()
        {
        }
    }
}
