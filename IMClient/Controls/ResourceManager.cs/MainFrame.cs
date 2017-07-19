using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace IMClient.Controls.ResourceManager
{
    partial class ResourceManager
    {
        public static Brush TitleLBackground
        {
            get { return ResourceManager.Current.Get<Brush>("TitleLBackground"); }
        }

        public static Image TitleLBackgroundImage
        {
            get { return ResourceManager.Current.Get<Image>("TitleLBackground"); }
        }

        public static Brush TitleRBackground
        {
            get { return ResourceManager.Current.Get<Brush>("TitleRBackground"); }
        }
    }
}
