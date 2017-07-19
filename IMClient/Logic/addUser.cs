using IMClient.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMClient.Logic
{
    public class addUser
    {
        /// <summary>
        /// 添加用户请求
        /// </summary>
        /// <param name="jid"></param>
        /// <param name="msgStr"></param>
        public void sendAddUser(string jid,string msgStr)
        {
            Matrix.Xmpp.Client.Message msg = new Matrix.Xmpp.Client.Message(jid, SysParams.Sys_AddFriendMessage+msgStr, "", "");
            //msg.XHtml.Add(
            StaticClass.xmppClient.Send(msg);
        }
    }
}
