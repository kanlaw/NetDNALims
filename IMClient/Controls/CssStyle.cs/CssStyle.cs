using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace IMClient.Controls.CssStyle
{
    public partial class CssStyle : IDisposable
    {
        CssStyle m_inheritStyle;
        string m_resourceRoot;

        Pen m_border;
        Brush m_background;
        Font m_font;
        Brush m_color;

        Padding? m_padding;
        Padding? m_margin;
        int? m_width;
        int? m_minWidth;
        int? m_maxWidth;
        int? m_height;
        int? m_minHeight;
        int? m_maxHeight;

        public CssStyle(CssStyle inheritStyle, string resourceRoot)
        {
            this.m_inheritStyle = inheritStyle;
            this.m_resourceRoot = resourceRoot;

            this.m_border = null;
            this.m_background = null;
            this.m_font = null;
            this.m_color = null;

            this.m_padding = null;
            this.m_margin = null;
        }

        public CssStyle(string resourceRoot)
        {
            this.m_inheritStyle = null;
            this.m_resourceRoot = resourceRoot;

            this.m_border = null;
            this.m_background = null;
            this.m_font = null;
            this.m_color = null;

            this.m_padding = null;
            this.m_margin = null;
        }

        public Pen Border
        {
            get
            {
                if (this.m_border != null) return this.m_border;
                if (this.m_inheritStyle != null) return this.m_inheritStyle.Border;

                return null;
            }
            set
            {
                this.m_border = value;
            }
        }

        public Brush Background
        {
            get
            {
                if (this.m_background != null) return this.m_background;
                if (this.m_inheritStyle != null) return this.m_inheritStyle.Background;

                return null;
            }
            set
            {
                this.m_background = value;
            }
        }

        public Brush Color
        {
            get
            {
                if (this.m_color != null) return this.m_color;
                if (this.m_inheritStyle != null) return this.m_inheritStyle.Color;

                return null;
            }
            set
            {
                this.m_color = value;
            }
        }

        public Font Font
        {
            get
            {
                if (this.m_font != null) return this.m_font;
                if (this.m_inheritStyle != null) return this.m_inheritStyle.Font;

                return null;
            }
            set
            {
                this.m_font = value;
            }
        }

        public Padding Padding
        {
            get
            {
                if (this.m_padding != null) return this.m_padding.Value;
                if (this.m_inheritStyle != null) return this.m_inheritStyle.Padding;

                return new Padding(0);
            }
            set
            {
                this.m_padding = value;
            }
        }

        public Padding Margin
        {
            get
            {
                if (this.m_margin != null) return this.m_margin.Value;
                if (this.m_inheritStyle != null) return this.m_inheritStyle.Margin;

                return new Padding(0);
            }
            set
            {
                this.m_margin = value;
            }
        }

        public int Width
        {
            get
            {
                if (this.m_width != null) return this.m_width.Value;
                if (this.m_inheritStyle != null) return this.m_inheritStyle.Width;

                return 0;
            }
            set
            {
                this.m_width = value;
            }
        }

        public int MinWidth
        {
            get
            {
                if (this.m_minWidth != null) return this.m_minWidth.Value;
                if (this.m_inheritStyle != null) return this.m_inheritStyle.MinWidth;

                return 0;
            }
            set
            {
                this.m_minWidth = value;
            }
        }

        public int MaxWidth
        {
            get
            {
                if (this.m_maxWidth != null) return this.m_maxWidth.Value;
                if (this.m_inheritStyle != null) return this.m_inheritStyle.MaxWidth;

                return 0;
            }
            set
            {
                this.m_maxWidth = value;
            }
        }

        public int Height
        {
            get
            {
                if (this.m_height != null) return this.m_height.Value;
                if (this.m_inheritStyle != null) return this.m_inheritStyle.Height;

                return 0;
            }
            set
            {
                this.m_height = value;
            }
        }

        public int MinHeight
        {
            get
            {
                if (this.m_minHeight != null) return this.m_minHeight.Value;
                if (this.m_inheritStyle != null) return this.m_inheritStyle.MinHeight;

                return 0;
            }
            set
            {
                this.m_minHeight = value;
            }
        }

        public int MaxHeight
        {
            get
            {
                if (this.m_maxHeight != null) return this.m_maxHeight.Value;
                if (this.m_inheritStyle != null) return this.m_inheritStyle.MaxHeight;

                return 0;
            }
            set
            {
                this.m_maxHeight = value;
            }
        }

        public void Dispose()
        {
            if (this.m_border != null) this.m_border.Dispose();
            if (this.m_background != null) this.m_background.Dispose();
            if (this.m_font != null) this.m_font.Dispose();
            if (this.m_color != null) this.m_color.Dispose();
        }
    }
}