using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace IMClient.Logic.IDataTable
{
    public class SpecialContent:IDisposable
    {
        public SpecialContentType scType = SpecialContentType.font;

        public ImageAlign imageAlign = ImageAlign.center;

        public ImageAlign imageLineAlign = ImageAlign.center;

        public Image drawImage = SysParams.defaultHead;

        public Image drawImage_Hover= SysParams.defaultHead;


        public Size imageSize = new Size(40, 40);

        public List<SpecialFont> sfontlist = new List<SpecialFont>();

        public void AddsFontlist(Font font,Color fColor,StringFormat sf,string content)
        {
            SpecialFont sfont = new SpecialFont();
            sfont.sFont = font;
            sfont.FontColor = fColor;
            sfont.Sf = sf;
            sfont.StrContent = content;
            this.sfontlist.Add(sfont);
        }

        public void Dispose()
        {
            if (drawImage != null && drawImage != SysParams.defaultHead)
            {
                drawImage.Dispose();
            }
            if (drawImage_Hover != null && drawImage_Hover != SysParams.defaultHead)
            {
                drawImage_Hover.Dispose();
            }

            if (sfontlist != null && sfontlist.Count > 0)
            {
                for (int i = 0; i < sfontlist.Count; i++)
                {
                    sfontlist[i].Dispose();
                }
            }
        }
    }
}
