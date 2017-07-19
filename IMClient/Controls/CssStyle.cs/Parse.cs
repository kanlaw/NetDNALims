using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Globalization;
using System.Resources;

namespace IMClient.Controls.CssStyle
{
    public partial class CssStyle
    {
        Padding ParsePadding(string value)
        {
            Match m4 = Regex.Match(value, @"^(\d+)\s+(\d+)\s+(\d+)\s+(\d+)$");
            Match m1 = Regex.Match(value, @"^(\d+)$");

            if (m4.Success)
            {
                int top = int.Parse(m4.Groups[1].Value);
                int right = int.Parse(m4.Groups[2].Value);
                int bottom = int.Parse(m4.Groups[3].Value);
                int left = int.Parse(m4.Groups[4].Value);

                return new Padding(left, top, right, bottom);
            }
            else if (m1.Success)
            {
                int padding = int.Parse(m1.Groups[1].Value);

                return new Padding(padding);
            }

            throw new FormatException();
        }

        Brush ParseBrush(string value)
        {
            Match mRgb = Regex.Match(value, @"(^|\s+)#([0-9A-F]{2})([0-9A-F]{2})([0-9A-F]{2})($|\s+)", RegexOptions.IgnoreCase);
            Match mColorName = Regex.Match(value, @"(^|\s+)([0-9A-Z]+)($|\s+)", RegexOptions.IgnoreCase);
            Match mUrlImage = Regex.Match(value, @"(^|\s+)url\(([^\)]+)\)($|\s+)", RegexOptions.IgnoreCase);
            Match mRes = Regex.Match(value, @"(^|\s+)res\(([^\)]+)\)($|\s+)", RegexOptions.IgnoreCase);

            if (mRgb.Success)
            {
                int r = int.Parse(mRgb.Groups[2].Value, NumberStyles.HexNumber);
                int g = int.Parse(mRgb.Groups[3].Value, NumberStyles.HexNumber);
                int b = int.Parse(mRgb.Groups[4].Value, NumberStyles.HexNumber);

                Color c = System.Drawing.Color.FromArgb(r, g, b);

                return new SolidBrush(c);
            }
            else if (mColorName.Success)
            {
                Color c = System.Drawing.Color.FromName(mColorName.Groups[2].Value);

                return new SolidBrush(c);
            }
            else if (mUrlImage.Success)
            {
                Match mNoRepeat = Regex.Match(value, @"(^|\s+)no\-repeat($|\s+)", RegexOptions.IgnoreCase);
                Match mRepeatX = Regex.Match(value, @"(^|\s+)repeat\-x($|\s+)", RegexOptions.IgnoreCase);
                Match mRepeatY = Regex.Match(value, @"(^|\s+)repeat\-y($|\s+)", RegexOptions.IgnoreCase);

                string path = Path.Combine(this.m_resourceRoot, mUrlImage.Groups[2].Value);
                Image img = Image.FromFile(path);
                WrapMode wrap = WrapMode.Tile;
                if (mNoRepeat.Success) wrap = WrapMode.Clamp;
                if (mRepeatX.Success) wrap = WrapMode.TileFlipX;
                if (mRepeatY.Success) wrap = WrapMode.TileFlipY;

                return new TextureBrush(img, wrap);
            }
            else if (mRes.Success)
            {
                string id = Path.Combine(this.m_resourceRoot, mUrlImage.Groups[2].Value);
                Brush brush = ResourceManager.ResourceManager.Current.Get<Brush>(id);

                return brush;
            }

            throw new FormatException();
        }

        Font ParseFont(string font)
        {
            string fontFamily = "黑体";
            float emSize = 8;
            FontStyle fontStyle = FontStyle.Regular;

            foreach (Match m in Regex.Matches(font, @"(^|\s*)([^\s$]+)($|\s+)", RegexOptions.IgnoreCase))
            {
                Match mBlob = Regex.Match(m.Groups[2].Value, @"^(bold)$", RegexOptions.IgnoreCase);
                Match mSize = Regex.Match(m.Groups[2].Value, @"^(\d+)(\.(\d+))?$");

                if (mSize.Success) emSize = float.Parse(mSize.Value);
                else if (mBlob.Success) fontStyle |= FontStyle.Bold;
                else fontFamily = m.Groups[2].Value;
            }

            return new Font(fontFamily, emSize, fontStyle);
        }

        Pen ParsePen(string value)
        {
            Color color = System.Drawing.Color.White;
            float width = 0;

            Match mRgb = Regex.Match(value, @"(^|\s+)#([0-9A-F]{2})([0-9A-F]{2})([0-9A-F]{2})($|\s+)", RegexOptions.IgnoreCase);
            Match mColorName = Regex.Match(value, @"(^|\s+)([0-9A-Z]+)($|\s+)", RegexOptions.IgnoreCase);
            Match mWidth = Regex.Match(value, @"(^|\s+)(\d+(\.(\d+))?)($|\s+)");
            
            if (mRgb.Success)
            {
                int r = int.Parse(mRgb.Groups[2].Value, NumberStyles.HexNumber);
                int g = int.Parse(mRgb.Groups[3].Value, NumberStyles.HexNumber);
                int b = int.Parse(mRgb.Groups[4].Value, NumberStyles.HexNumber);

                color = System.Drawing.Color.FromArgb(r, g, b);
            }
            else if (mColorName.Success)
            {
                color = System.Drawing.Color.FromName(mColorName.Groups[2].Value);
            }
            if (mWidth.Success) width = float.Parse(mWidth.Groups[2].Value);

            return new Pen(color, width);
        }

        int ParseInt(string value)
        {
            int width = 0;

            Match mWidth = Regex.Match(value, @"(^|\s+)(\d+)($|\s+)");
            
            if (mWidth.Success) width = int.Parse(mWidth.Groups[2].Value);

            return width;
        }

        public void Parse(string property, string value)
        {
            switch (property.ToLower())
            {
                case "margin":
                    this.m_margin = this.ParsePadding(value);

                    break;
                case "padding":
                    this.m_padding = this.ParsePadding(value);

                    break;
                case "color":
                    if (this.m_color != null) this.m_color.Dispose();
                    this.m_color = this.ParseBrush(value);

                    break;
                case "border":
                    if (this.m_border != null) this.m_border.Dispose();
                    this.m_border = this.ParsePen(value);

                    break;
                case "background":
                    if (this.m_background != null) this.m_background.Dispose();
                    this.m_background = this.ParseBrush(value);

                    break;
                case "font":
                    if (this.m_font != null) this.m_font.Dispose();
                    this.m_font = this.ParseFont(value);

                    break;
                case "width":
                    this.m_width = this.ParseInt(value);
                    
                    break;
                case "height":
                    this.m_height = this.ParseInt(value);

                    break;
                case "min-width":
                    this.m_minWidth = this.ParseInt(value);

                    break;
                case "min-height":
                    this.m_minHeight = this.ParseInt(value);

                    break;
                case "max-width":
                    this.m_maxWidth = this.ParseInt(value);

                    break;
                case "max-height":
                    this.m_maxHeight = this.ParseInt(value);

                    break;
            }
        }
    }
}
