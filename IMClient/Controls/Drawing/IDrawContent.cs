using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace IMClient.Controls.Drawing
{
    //绘制窗口接口
    public interface IDrawContent
    {
        Size BeginDraw(Graphics g, CssStyle.CssStyle style);
        void Draw(Graphics g);
        void EndDraw();
    }
}
