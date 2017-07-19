using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DNANET.Data;
using System.Collections.Generic;

namespace NetDLims.Services
{
    public static class UserEx
    {
        
        public static string GetJID(this UserInformation userInfo)
        {
            string jid = string.Empty;
            for (int i = 0; i < userInfo.Services.Length; i++)
            {
                UserServiceInformation ui = userInfo.Services[i];
                if (ui.Service == "im")
                {
                    jid = ui.Data.ToString();
                    //info.SetJID(ui.Data.ToString());
                    break;
                }
            }
            return jid;
        }
    }
}
