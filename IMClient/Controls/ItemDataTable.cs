using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using IMClient.Logic.Contorl;
using System.Drawing.Text;
using IMClient.Logic.IDataTable;

namespace IMClient.Controls
{

     public enum borderStyleSelf
    {
        borderAll, //全
        bordrNone, //无
        /// <summary>
        /// 垂直
        /// </summary>
        borderVertocal, //垂直
        /// <summary>
        /// 水平
        /// </summary>
        borderHoRizon//水平
    }

    public enum status
    {
        Hover, //悬停
        Select,//选中
        Normal //正常
    }


    public class ItemDataTable : ItemBase
    {
        #region 行 列
        private List<Type> typelist_Str = new List<Type>();

        private void InitTypeList_string()
        {
            typelist_Str.Add(typeof(string));
            typelist_Str.Add(typeof(int));
            typelist_Str.Add(typeof(double));
            typelist_Str.Add(typeof(long));
            typelist_Str.Add(typeof(DateTime));
        }
        /// <summary>
        /// 全部行 含标题
        /// </summary>
        private List<Rectangle> rowRect_All = new List<Rectangle>();

        /// <summary>
        /// 全部列
        /// </summary>
        private List<Rectangle> colRect_All = new List<Rectangle>();

        /// <summary>
        /// 行 不含标题
        /// </summary>
        private List<Rectangle> rowRect_Actual = new List<Rectangle>();

      

        public Rectangle selectRect = Rectangle.Empty;

        #endregion

        public ItemDataTable()
        {

            InitTypeList_string();
        }



        #region 绘制信息

        #region 分割线
        [Browsable(true), Description("标题分割线水平长度")]
        public int SplitLine_Title_H_Length = 0;

        [Browsable(true), Description("标题分割线垂直长度")]
        public int SplitLine_Title_V_Length = 0;

        [Browsable(true), Description("内容分割线长度")]
        public int SplitLine_Content_Length = 0;

        [Browsable(true), Description("标题分割线宽度")]
        public float SplitLine_Title_Width = 0;

        [Browsable(true), Description("内容分割线宽度")]
        public float SplitLine_Content_Width = 0;


        [Browsable(true), Description("标题分割线颜色")]
        public Color SplitLine_Title_H_Color = Color.FromArgb(0, 113, 188);

        [Browsable(true), Description("标题分割线颜色")]
        public Color SplitLine_Title_V_Color = Color.FromArgb(230, 230, 230);

        [Browsable(true), Description("内容分割线颜色")]
        public Color SplitLine_Color_Color = Color.FromArgb(230,230,230);
        #endregion

        /// <summary>
        /// 标题高度
        /// </summary>
        private int titleHeight = 30;

        [Browsable(true), Description("边框样式")]
        public borderStyleSelf borderStyle = borderStyleSelf.borderAll;

        [Browsable(true), Description("标题高度")]
        public int TitleHeight { get => titleHeight; set => titleHeight = value; }

        /// <summary>
        /// 列高
        /// </summary>
        private int colHeight = 30;


        [Browsable(true), Description("行高")]
        public int ColHeight { get => colHeight; set => colHeight = value; }

        [Browsable(true), Description("边框-外边框")]
        public bool isBorder = true;

        [Browsable(true), Description("背景色-标题")]
        public Color Bg_TitleColor = Color.White;

        /// <summary>
        /// 标题画刷
        /// </summary>
        public Brush B_TitleColor = null;

        [Browsable(true), Description("背景色-内容")]
        public Color Bg_ContentColor = Color.White;

        [Browsable(true), Description("背景色-选中内容背景色")]
        public Color Bg_SelectColor = Color.FromArgb(150,150,150);

        [Browsable(true), Description("背景色-悬停选中")]
        public Color Bg_MoveColor = Color.Tomato;


        /// <summary>
        /// 是否绘制 标题
        /// </summary>
        private bool isTitle = true;
        /// <summary>
        /// 是否绘制 标题
        /// </summary>
        [Browsable(true), Description("是否绘制 标题")]
        public bool IsTitle
        {
            get { return isTitle; }
            set { isTitle = value; }
        }


        public Color borderColor = Color.FromArgb(43, 145, 175);

        private Font font_Content = new Font("微软雅黑", 9.0f);


        private Font font_title = new Font("微软雅黑", 9.0f);

        private Font font_Select = new Font("微软雅黑", 9.0f);

        /// <summary>
        /// 字体颜色-内容
        /// </summary>
        private Color color_font = Color.FromArgb(43, 145, 173);


        /// <summary>
        /// 字体颜色-标题
        /// </summary>
        private Color color_title = Color.FromArgb(43, 145, 173);
        /// <summary>
        /// 字体颜色-选中
        /// </summary>
        private Color color_Select = Color.White;


        [Browsable(true), Description("内容文字")]
        public Font Font_Content { get => font_Content; set => font_Content = value; }
        [Browsable(true), Description("标题文字")]
        public Font Font_title { get => font_title; set => font_title = value; }
        [Browsable(true), Description("选中字体")]
        public Font Font_Select { get => font_Select; set => font_Select = value; }
        [Browsable(true), Description("字体颜色")]
        public Color Color_font { get => color_font; set => color_font = value; }

        /// <summary>
        /// 字体颜色-选中字体颜色
        /// </summary>
        public Color Color_Select { get => color_Select; set => color_Select = value; }

        /// <summary>
        /// 字体颜色-标题
        /// </summary>
        public Color Color_title { get => color_title; set => color_title = value; }

        /// <summary>
        /// 内容的格式
        /// </summary>
        public StringFormat Sf_Content { get => sf_Content; set => sf_Content = value; }

        /// <summary>
        /// 标题的格式
        /// </summary>
        public StringFormat Sf_Title { get => sf_Title; set => sf_Title = value; }


        private StringFormat sf_Content = new StringFormat();
        private StringFormat sf_Title = new StringFormat();

        public List<ColClass> colList = new List<ColClass>();


        #endregion

        #region

        /// <summary>
        /// 数据集
        /// </summary>
        public DataTable dataTable = null;

        public void BindData(DataTable dt)
        {
            this.dataTable = dt;
            //this.SetRect_Row();
            //SetRect_Col();
            this.Invoke(new Action(() =>
            {
                this.Invalidate();
            }) );
        }

        /// <summary>
        /// 设置行
        /// </summary>
        public void SetRect_Row()
        {
            int height = 0;
            int index_row = 0;
            int offSet_Y = 0;

            if (this.rowRect_All != null)
                this.rowRect_All.Clear();
            else {
                this.rowRect_All = new List<Rectangle>();
            }
            if (this.rowRect_Actual != null)
                this.rowRect_Actual.Clear();
            else {
                this.rowRect_Actual = new List<Rectangle>();
            }

            while (true)
            {
                if ((height > this.Height) || (this.dataTable!=null &&this.dataTable.Rows.Count<index_row))
                    break;
                if (isTitle && index_row==0)
                {
                    Rectangle fr = new Rectangle(new Point(0, offSet_Y),
                  new Size(this.Width, this.titleHeight));
                    this.rowRect_All.Add(fr);// [index_row] = fr;
                    index_row++;
                    height += this.titleHeight;
                    offSet_Y += this.titleHeight;
                  
                    continue;
                }

                Rectangle f = new Rectangle(new Point(0, offSet_Y),
                    new Size(this.Width, this.colHeight));
                offSet_Y += this.colHeight;
                this.rowRect_All.Add(f);
                this.rowRect_Actual.Add(f);
                //this.rowRect_Actual[isTitle?index_row-1:index_row] = f;
                height += this.colHeight;
                index_row++;

            }

        }

        /// <summary>
        /// 设置列
        /// </summary>
        public void SetRect_Col()
        {
            int OffSet_X = 0;
            if (this.colRect_All != null)
                this.colRect_All.Clear();
            else {
                this.colRect_All = new List<Rectangle>();
            }
            if (this.colList != null && this.colList.Count > 0 && this.rowRect_All.Count>0)
            {
                int height = this.rowRect_All[0].Location.Y + this.rowRect_All[0].Height;
                if (this.rowRect_All[this.rowRect_All.Count - 1].Location.Y + this.rowRect_All[this.rowRect_All.Count - 1].Height > height)
                {
                    height = this.rowRect_All[this.rowRect_All.Count - 1].Location.Y + this.rowRect_All[this.rowRect_All.Count - 1].Height;
                }
                for (int i = 0; i < this.colList.Count; i++)
                {
                    int width = (int)Math.Ceiling( (double)(this.Width * this.colList[i].ColWidth)/ (double)100);
                    this.colRect_All.Add(
                        new Rectangle(new Point(OffSet_X, 0), new Size(width, height))
                        );
                    OffSet_X += width;
                }
            }
        }

        /// <summary>
        /// 背景色-标题
        /// </summary>
        [Browsable(true), Description("背景色-标题")]
        public Color BG_titleColor = Color.FromArgb(195, 195, 195);

        #endregion


        #region 绘制


        #region 绘制边框及背景;
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            #region 计算 行 列;
            SetRect_Row();
            SetRect_Col();
            #endregion;


            #region 背景色填充
            DrawBG(e.Graphics);


            if (isTitle)
            {
                this.DrawTitleBg(e.Graphics);
            }
            if (selectRect != Rectangle.Empty)
            {
                this.DrawSelectBg(e.Graphics);
            }
            if (moveRectangle != Rectangle.Empty)
            {
                this.DrawMoveBg(e.Graphics);
            }
            #endregion

            #region 边框
         
            switch (this.borderStyle)
            {
                case borderStyleSelf.borderAll:
                    this.drawBorder_H(e.Graphics);
                    this.drawBorder_V(e.Graphics);
                    break;
                case borderStyleSelf.borderHoRizon:
                    this.drawBorder_H(e.Graphics);
                    break;
                case borderStyleSelf.borderVertocal:
                    this.drawBorder_V(e.Graphics);
                    break;
            }

            if (this.SplitLine_Content_Length > 0)
            {
                drawBorder_SplitH(e.Graphics);
            }

            if (IsTitle)
            {
                if (this.SplitLine_Title_V_Length > 0)
                {
                    drawBorder_SplitTitleV(e.Graphics);
                }
                if (this.SplitLine_Title_H_Length > 0)
                {
                    drawBorder_SplitTitleH(e.Graphics);
                }
            }

            if (isBorder)
            {
                drawBorder_Outer(e.Graphics);
            }
            #endregion
        }

        /// <summary>
        /// 水平
        /// </summary>
        /// <param name="g"></param>
        public void drawBorder_H(Graphics g)
        {
            if (this.rowRect_All != null && this.rowRect_All.Count > 0)
            {
                for (int i = 0; i < this.rowRect_All.Count; i++)
                {
                    Rectangle r = this.rowRect_All[i];
                    using (Pen p = new Pen(this.borderColor))
                    {
                        g.DrawLine(p, new Point(r.Location.X, r.Location.Y),
                            new Point(r.Location.X + r.Width, r.Location.Y));
                        g.DrawLine(p, new Point(r.Location.X, r.Location.Y+r.Height),
                            new Point(r.Location.X + r.Width, r.Location.Y + r.Height));
                    }
                }
            }
        }

        /// <summary>
        /// 垂直
        /// </summary>
        /// <param name="g"></param>
        public void drawBorder_V(Graphics g)
        {
            if (this.colRect_All != null && this.colRect_All.Count > 0)
            {
                for (int i = 0; i < this.colRect_All.Count; i++)
                {
                    Rectangle r = this.colRect_All[i];
                    using (Pen p = new Pen(this.borderColor))
                    {
                        g.DrawLine(p, new Point(r.Location.X, r.Location.Y),
                            new Point(r.Location.X , r.Location.Y+r.Height));
                        g.DrawLine(p, new Point(r.Location.X+r.Width, r.Location.Y ),
                            new Point(r.Location.X + r.Width, r.Location.Y + r.Height));
                    }
                }
            }
        }

        /// <summary>
        /// 外边框
        /// </summary>
        /// <param name="g"></param>
        public void drawBorder_Outer(Graphics g)
        {
            
            using (Pen p = new Pen(borderColor))
            {
                g.DrawRectangle(p,
                    new Rectangle(this.ClientRectangle.Location,
                    new Size(this.ClientRectangle.Width-1,this.ClientRectangle.Height-1)));
            }
        }


        /// <summary>
        /// 水平
        /// </summary>
        /// <param name="g"></param>
        public void drawBorder_SplitH(Graphics g)
        {
            if (this.rowRect_All != null && this.rowRect_All.Count > 0)
            {
                for (int i = 0; i < this.rowRect_All.Count; i++)
                {
                    Rectangle r = this.rowRect_All[i];
                    using (Pen p = new Pen(this.SplitLine_Color_Color))
                    {
                        //g.DrawLine(p, new Point(r.Location.X, r.Location.Y),
                        //    new Point(r.Location.X + r.Width, r.Location.Y));
                        int offSet_X = (r.Width - this.SplitLine_Content_Length) / 2;
                        g.DrawLine(p, new Point(r.Location.X+ offSet_X, r.Location.Y + r.Height),
                            new Point(r.Location.X + this.SplitLine_Content_Length, r.Location.Y + r.Height));
                    }
                }
            }
        }


        /// <summary>
        /// 水平
        /// </summary>
        /// <param name="g"></param>
        public void drawBorder_SplitTitleH(Graphics g)
        {
            if (this.rowRect_All != null && this.rowRect_All.Count > 0)
            {

                    Rectangle r = this.rowRect_All[0];
                    using (Pen p = new Pen(this.SplitLine_Title_H_Color))
                    {

                        int offSet_X = (r.Width - this.SplitLine_Title_H_Length) / 2;
                        g.DrawLine(p, new Point(r.Location.X + offSet_X, r.Location.Y + r.Height),
                            new Point(r.Location.X + this.SplitLine_Title_H_Length, r.Location.Y + r.Height));
                    }
            }
        }
        /// <summary>
        /// 水平
        /// </summary>
        /// <param name="g"></param>
        public void drawBorder_SplitTitleV(Graphics g)
        {
            if (this.rowRect_All != null && this.rowRect_All.Count > 0)
            {

                Rectangle r = this.rowRect_All[0];
                using (Pen p = new Pen(this.SplitLine_Title_V_Color))
                {
                    for (int i = 0; i < colRect_All.Count; i++)
                    {
                        Rectangle r_Col = colRect_All[i];
                        int offSet_Y = (r.Height - this.SplitLine_Title_V_Length) / 2;
                        g.DrawLine(p, new Point(r_Col.Location.X + r_Col.Width, r.Location.Y+ offSet_Y ),
                            new Point(r_Col.Location.X + r_Col.Width, r.Location.Y + offSet_Y + this.SplitLine_Title_V_Length));
                    }

                }
            }
        }
        #endregion;



        #region 绘制内容
        public void DrawTitle(Graphics g)
        {
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            if (this.colList != null && this.colList.Count > 0)
            {
                int width = this.rowRect_All[0].Width;
                for (int i = 0; i<this.colList.Count; i++)
                {
                    ColClass col = this.colList[i];
                    Rectangle tmpRect = new Rectangle(
                        new Point(this.colRect_All[i].Location.X, this.colRect_All[i].Location.Y),
                        new Size(this.colRect_All[i].Width, this.titleHeight)
                        );
                    using (SolidBrush sb = new SolidBrush(this.color_title))
                    {
                        g.DrawString(col.ColName, this.font_title, sb, tmpRect, this.sf_Title);
                    }
                       

                }
            }
        }

        public void DrawContent(Graphics g)
        {
           
            for (int i = 0; i < this.rowRect_Actual.Count; i++)
            {
                if (i < this.dataTable.Rows.Count)
                {
                    DataRow dr = this.dataTable.Rows[i];
                    for (int c = 0; c < this.colRect_All.Count; c++)
                    {

                        ColClass col = this.colList[c];
                        bool isSelected_Font = false;
                        if ((selectedMoveIndex == i))
                        {
                            isSelected_Font = true;
                        }
                        if (this.dataTable != null
                            && this.dataTable.Rows.Count > 0
                            && this.dataTable.Columns.Contains(col.ColName_Table))
                        {

                            int x = this.colRect_All[c].Location.X;
                            int y = this.rowRect_Actual[i].Location.Y;
                            int height = this.rowRect_Actual[i].Height;
                            int width = this.colRect_All[c].Width;
                            //绘制区间
                            Rectangle rect = new Rectangle(new Point(x, y), new Size(width, height));
                            //文字
                            if (this.typelist_Str.Contains(dr[col.ColName_Table].GetType()))
                            {
                               
                                string val = dr[col.ColName_Table] != DBNull.Value ? dr[col.ColName_Table].ToString() : string.Empty;

                                this.DrawString(g, rect, val, isSelected_Font);
                            }
                            if (dr[col.ColName_Table]!=DBNull.Value && dr[col.ColName_Table] is SpecialContent)
                            {
                               
                                SpecialContent sc = (SpecialContent)dr[col.ColName_Table];
                                switch (sc.scType)
                                {
                                    case Logic.SpecialContentType.font:
                                        DrawSpecialContent_Font(g, sc, rect, isSelected_Font);
                                        break;
                                    case Logic.SpecialContentType.image:
                                        bool isSelected_Image = false;
                                        if ((selectedMoveIndex == i && selectedMoveIndex_Col == c))
                                        {
                                            isSelected_Image = true;
                                        }
                                        DrawSpecialContent_Image(g, sc, rect, isSelected_Image);
                                        break;
                                }
                            }
                        }
                    }
                }
                else break;
            }
        }

        private void DrawSpecialContent_Image(Graphics g,SpecialContent sc,Rectangle rect,bool isSelected)
        {
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Image drawImage = sc.drawImage;
            if (sc.imageSize.Width <= rect.Width && sc.imageSize.Height <= rect.Height)
            {
                int OffSet_X = 0;
                int OffSet_Y = 0;
                switch (sc.imageAlign)
                {
                    case Logic.ImageAlign.left:
                        break;
                    case Logic.ImageAlign.center:
                        OffSet_X = (rect.Width - sc.imageSize.Width) / 2;
                        break;
                    case Logic.ImageAlign.right:
                        OffSet_X = rect.Width - sc.imageSize.Width;
                        break;
                }
                switch (sc.imageLineAlign)
                {
                    case Logic.ImageAlign.left:
                        break;
                    case Logic.ImageAlign.center:
                        OffSet_Y = (rect.Height - sc.imageSize.Height) / 2;
                        break;
                    case Logic.ImageAlign.right:
                        OffSet_Y = rect.Height - sc.imageSize.Height;
                        break;
                }
                
                if (sc.drawImage_Hover != null && isSelected)
                {
                    drawImage = sc.drawImage_Hover;
                }
                lock (drawImage)
                {
                    g.DrawImage(drawImage, new Rectangle(
                                            new Point(OffSet_X + rect.Location.X,
                                            OffSet_Y + rect.Location.Y), sc.imageSize));
                }
            }
            else {
                lock (drawImage)
                {
                    g.DrawImage(drawImage, rect);
                }
            }

        }


        private void DrawSpecialContent_Font(Graphics g, SpecialContent sc, Rectangle rect,bool isSelected)
        {
            if (sc.sfontlist != null && sc.sfontlist.Count > 0)
            {
                g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                int OffSet_Y = rect.Location.Y;
                int OffSet_X = rect.Location.X;
                using (SolidBrush sb = new SolidBrush(Color.Black)) {
                    int height = 0;
                    for (int i = 0; i < sc.sfontlist.Count; i++)
                    {
                        SpecialFont sfont = sc.sfontlist[i];
                        height += (int)Math.Ceiling(g.MeasureString(sfont.StrContent, sfont.sFont).Height);
                    }
                    if (height < rect.Height) {
                        switch (sc.imageLineAlign)
                        {
                            case Logic.ImageAlign.center:
                                OffSet_Y += (rect.Height - height) / 2;
                                break;
                             case   Logic.ImageAlign.right:
                                OffSet_Y += (rect.Height - height) ;
                                break;
                        }
                    }

                    for (int i = 0; i < sc.sfontlist.Count; i++)
                    {
                        SpecialFont sfont = sc.sfontlist[i];
                        int fontHeight = (int)Math.Ceiling(g.MeasureString(sfont.StrContent, sfont.sFont).Height);
                        Rectangle rect_tmp = new Rectangle(new Point(OffSet_X, OffSet_Y), new Size(rect.Width ,fontHeight));
                        sb.Color = sfont.FontColor;
                        if (isSelected)
                        {
                            sb.Color = sfont.Selected_FontColor;
                        }
                        g.DrawString(sfont.StrContent, sfont.sFont, sb, rect_tmp, sfont.Sf);
                        OffSet_Y += fontHeight;
                    }
                }
            }
            

        }



        private void DrawString(Graphics g,Rectangle rect,string val,bool isSelected)
        {
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Color font_color = this.color_font;
            if (isSelected)
            {
                font_color = this.color_Select;
            }
            using (SolidBrush sb = new SolidBrush(font_color))
            {

                g.DrawString(val, this.font_Content, sb, rect, sf_Content);
            }


        }
       

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (isTitle)
            {
                DrawTitle(e.Graphics);
            }
            if (this.dataTable != null && this.dataTable.Rows.Count > 0)
            {
                DrawContent(e.Graphics);
            }
        }
        #endregion


        #endregion

        #region 绘制方法

        public void DrawBG(Graphics g)
        {
            using (SolidBrush sb = new SolidBrush(this.Bg_ContentColor))
            {
                g.FillRectangle(sb, this.ClientRectangle);
            }
        }

        public void DrawTitleBg(Graphics g)
        {
            RectangleF r = new RectangleF(new PointF(0, 0), new SizeF(this.Width, 
                this.titleHeight));
            if (B_TitleColor == null)
            {
                using (SolidBrush sb = new SolidBrush(Bg_TitleColor))
                {
                    g.FillRectangle(sb, r);
                }
            }
            else {
                g.FillRectangle(B_TitleColor, r);
            }
        }

        /// <summary>
        /// 绘制选中的Rect
        /// </summary>
        /// <param name="g"></param>
        public void DrawSelectBg(Graphics g)
        {
            RectangleF r = new RectangleF(new PointF(0, 0), new SizeF(this.Width,
               this.titleHeight));
            using (SolidBrush sb = new SolidBrush(Bg_SelectColor))
            {
                g.FillRectangle(sb, this.selectRect);
            }
        }


        public void DrawMoveBg(Graphics g)
        {
            RectangleF r = new RectangleF(new PointF(0, 0), new SizeF(this.Width,
               this.titleHeight));
            using (SolidBrush sb = new SolidBrush(Bg_MoveColor))
            {
                g.FillRectangle(sb, this.moveRectangle);
            }
        }


        #endregion


        #region 控制方法

        public Rectangle moveRectangle = Rectangle.Empty;

        /// <summary>
        /// 选中的行
        /// </summary>
        public int selectedIndex = -1;

        /// <summary>
        /// 鼠标移动
        /// </summary>
        public int selectedMoveIndex = -1;

        /// <summary>
        /// 选中的列
        /// </summary>
        public int selectedIndex_Col = -1;

        /// <summary>
        /// 鼠标移动选中列
        /// </summary>
        public int selectedMoveIndex_Col = -1;

        protected override void OnMouseMove(MouseEventArgs e)
        {
            changeSelectIndex(e.Location, ref this.selectedMoveIndex, ref this.selectedMoveIndex_Col);
            if (selectedMoveIndex >= 0 && (rowRect_Actual.Count > selectedMoveIndex && moveRectangle != rowRect_Actual[selectedMoveIndex]))
            {
                moveRectangle = rowRect_Actual[selectedMoveIndex];

            }
            base.OnMouseMove(e);
        
            this.Invalidate();
        }

        protected override void  OnMouseClick(MouseEventArgs e)
        {
            changeSelectIndex(e.Location, ref selectedIndex, ref this.selectedIndex_Col);
            if (selectedIndex >= 0 && (rowRect_Actual.Count > selectedIndex && selectRect != rowRect_Actual[selectedIndex]))
            {
                selectRect = rowRect_Actual[selectedIndex];
            }
            base.OnMouseClick(e);
            
            this.Invalidate();
        }


        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            changeSelectIndex(e.Location, ref selectedIndex, ref this.selectedIndex_Col);
            base.OnMouseDoubleClick(e);
          
        }

        /// <summary>
        /// 选中的索引
        /// 行和列
        /// </summary>
        /// <param name="hitPoint"></param>
        public void changeSelectIndex(Point hitPoint,ref int selectedIndex_Row,ref int selectedIndex_Col)
        {
           
            for (int i = 0; i < rowRect_Actual.Count; i++)
            {
                if (rowRect_Actual[i].Contains(hitPoint))
                {
                    selectedIndex_Row = i;
                    break;
                }
            }

            if (colRect_All != null && colRect_All.Count > 0)
            {
                for (int i = 0; i < colRect_All.Count; i++)
                {
                    if (colRect_All[i].Contains(hitPoint))
                    {
                        selectedIndex_Col = i;
                        break;
                    }
                }
            }
        }


        /// <summary>
        /// 反馈选中行数
        /// </summary>
        /// <returns></returns>
        public DataRow retSelectedRow()
        {
            DataRow result = null;
            if (this.dataTable != null && this.dataTable.Rows.Count > 0 
                && (selectedIndex>=0 && this.dataTable.Rows.Count>selectedIndex))
            {
                result = this.dataTable.Rows[selectedIndex];
            }

            return result;
        }



        #endregion

        #region 释放
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            this.font_Content.Dispose();
            this.font_title.Dispose();
            this.font_Select.Dispose();
            sf_Content.Dispose();
            sf_Title.Dispose();
            if (B_TitleColor != null)
            {
                B_TitleColor.Dispose();
            }
            if (this.colList != null)
            {
                this.colList.Clear();
            }
            if (this.dataTable != null)
            {
                this.dataTable.Dispose();
            }
            typelist_Str.Clear();

            GC.Collect();

        }
#endregion
    }
}
