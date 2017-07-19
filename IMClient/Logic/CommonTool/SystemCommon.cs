using IMClient.XMPP.Forms;
using JustLib.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

namespace IMClient.Logic
{
    /// <summary>
    /// 系统消息处理通用方法
    /// </summary>
    public partial class  Common
    {


        public static Thread threadVibration = null;
        #region
        /// <summary>
        /// 系统消息处理
        /// </summary>
        /// <param name="form"></param>
        /// <param name="message"></param>
        public static void SysMessageManage(ChatFormForWeb form, string userID, string message, ChatBoxContent chatcontent)
        {
            switch (message)
            {
                case SysParams.Sys_VibrationMessage://震动消息处理
                    form.AppendChatBoxContent(true, userID, DateTime.Now, chatcontent, Color.Blue, false);
                    Common.VibrationThread(form);

                    break;
            }
        }
        #endregion
    }
}
