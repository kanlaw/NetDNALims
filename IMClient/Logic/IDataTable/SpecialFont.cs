using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace IMClient.Logic.IDataTable
{
    public class SpecialFont:IDisposable
    {
        private Font font = new Font("微软雅黑", 8);

        private StringFormat sf = new StringFormat();

        /// <summary>
        /// 默认的字体颜色
        /// </summary>
        private Color fontColor = Color.FromArgb(195, 195, 195);

        /// <summary>
        /// 选中的字体颜色
        /// </summary>
        private Color selected_FontColor = Color.White;

        private string strContent = string.Empty;

        
        public Font sFont { get => font; set => font = value; }
        public StringFormat Sf { get => sf; set => sf = value; }
        public string StrContent { get => strContent; set => strContent = value; }
        public Color FontColor { get => fontColor; set => fontColor = value; }
        public Color Selected_FontColor { get => selected_FontColor; set => selected_FontColor = value; }

        public  void Dispose()
        {
            font.Dispose();
            sf.Dispose();
        }
        
    }
}
