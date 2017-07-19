using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using DNANET.Data;
using JustLib;

namespace IMClient.Logic
{
    public partial class SysParams
    {
        /// <summary>
        /// OpenFile 域地址
        /// </summary>
        public static string Domain
        {
            get
            {
                string ret = ConfigurationManager.AppSettings["Domain"];
                return ret;
            }
        }

        public static Font sysFont = new Font("微软雅黑", 12);

        public static Color sysFontColor = Color.FromArgb(200, 200, 200);


        /// <summary>
        /// 返回自定义的系统消息 类型主体
        /// </summary>
        /// <param name="MessageInfo"></param>
        /// <returns></returns>
        public static string retSysMessage(string MessageInfo)
        {
            string message = string.Empty;
            if (MessageInfo.Contains("[:") && MessageInfo.Contains("]"))
            {
                message = MessageInfo.Substring(0, MessageInfo.IndexOf(']') + 1);
            }
            return message;
        }

        #region systemInfo 系统特殊标记文本

        public static string[] messTypeStrList = new string[] {
            Sys_VibrationMessage,
            Sys_VoiceMessage,
            Sys_SendFileMessage,
            Sys_ReceiveFileMessage,
            Sys_Normal,
            Sys_File_Cancel,
            Sys_File_Success,
            Sys_File_Warming,
            Sys_OffLine_ReceiveFileMessage,
            Sys_OffLine_SendFileMessage,
            Sys_OffLine_Success,
            Sys_AddFriendMessage,
            Sys_Vote,
            Sys_Meeting_Invite
        };

      
        /// <summary>
        /// 添加用户请求
        /// </summary>
        public const string Sys_AddFriendMessage = "[:AddFriendMessage]";


        /// <summary>
        /// 抖动消息标准格式
        /// </summary>
        public const string Sys_VibrationMessage = "[:Vibration]";

        /// <summary>
        /// 语音消息
        /// </summary>
        public const string Sys_VoiceMessage = "[:Voice]";

        /// <summary>
        /// 文件发送-发送方发送消息
        /// </summary>
        public const string Sys_SendFileMessage = "[:SendFile]";

        /// <summary>
        /// 自身产生消息
        /// </summary>
        public const string Sys_Self_FileMessage = "[:SelfFile]";

        /// <summary>
        /// 离线文件发送-接收方反馈消息
        /// </summary>
        public const string Sys_OffLine_ReceiveFileMessage = "[:OffLineReceiveFile]";


        /// <summary>
        /// 离线文件发送-发送方发送消息
        /// </summary>
        public const string Sys_OffLine_SendFileMessage = "[:OffLineSendFile]";


        public const string Sys_OffLine_Success = "[:OffLineSuccess]";


        /// <summary>
        /// 文件发送-接收方反馈消息
        /// </summary>
        public const string Sys_ReceiveFileMessage = "[:ReceiveFile]";

        /// <summary>
        /// meeting 投票
        /// </summary>
        public const string Sys_Vote = "[:Vote]";


        /// <summary>
        /// 普通消息
        /// </summary>
        public const string Sys_Normal = "[:Normal]";


        /// <summary>
        /// 会议邀请
        /// </summary>
        public const string Sys_Meeting_Invite = "[:Meeting_Invite]";

        /// <summary>
        /// 普通消息
        /// </summary>
        public const string Sys_File_Cancel = "[:FileCancel]";

        /// <summary>
        /// 警告消息
        /// </summary>
        public const string Sys_File_Warming = "[:FileWarming]";

        /// <summary>
        /// 成功消息
        /// </summary>
        public const string Sys_File_Success = "[:FileSuccess]";


        /// <summary>
        /// 历史记录间隔线 
        /// </summary>
        public const string Sys_History_Line = "[:HistoryLine]";
        #endregion


        #region 标准系统文本

        #region 抖动标准消息文版
        public const string Vibration_Send = "您发送了一个窗口抖动。";
        public const string Vibration_Receive = "{0}给您发送了一个窗口抖动。";
        #endregion

        #region 文件收发 文本消息

        /// <summary>
        /// 成功接收离线文件
        /// </summary>
        public const string File_OFFLine_Success = "对方已成功接收了您发送的离线文件“{0}”({1})。";

        /// <summary>
        /// 接收方 完成接收 提示文本
        /// </summary>
        public const string File_Receive_Result = "成功发送至{0}";

        /// <summary>
        /// 发送方 完成发送 提示文本
        /// </summary>
        public const string File_Send_Result = "成功发送文件";

        /// <summary>
        /// 发送方 完成发送 提示文本
        /// </summary>
        public const string File_OffLine_Send_Result = "离线文件上传完毕";


        /// <summary>
        /// 系统反馈 接收方（发送方 取消发送）
        /// </summary>
        public const string File_Result_Cancel_Receive = "对方取消接收“{0}”({1})，文件发送失败。";

        /// <summary>
        /// 发送方 取消文件发送
        /// 0 文件名
        /// 1 文件大小
        /// </summary>
        public const string File_Cancel_Send = "您取消了“{0}”({1})的{2}，文件传输失败。";

        /// <summary>
        /// 接收方取消的文件发送消息
        /// 0 文件名
        /// 1 文件大小
        /// </summary>
        public const string File_Cancel_Receive = "对方取消{0}“{1}”({2})，文件发送失败。";
        #endregion

        #endregion
    }
}
