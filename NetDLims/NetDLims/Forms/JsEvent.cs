using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NetDLims.Forms
{
    public class JsEvent
    {
        public string MessageText = string.Empty;


        public void showTest()
        {
            #if DEBUG
            MessageBox.Show("ss");
            #endif
        }
    }
}
