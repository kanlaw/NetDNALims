using CefSharp.WinForms;
using IMClient.XMPP.Forms;
using JustLib.Controls;
using Matrix.Xmpp.SecurityLabels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace IMClient.Logic
{
    public  class Chat
    {

        public addUser addu = new addUser();
        public object FormObj = null;
        public Chat() { }

        public Chat(object mainForm)
        {
            this.FormObj = mainForm;
        }

        Common comm = new Common();

        #region 消息控制
        /// <summary>
        /// 消息处理展示
        /// </summary>
        /// <param name="messType"></param>
        /// <param name="isSysMessage"></param>
        /// <param name="userID"></param>
        /// <param name="originTime"></param>
        /// <param name="content"></param>
        /// <param name="color"></param>
        /// <param name="followingWords"></param>
        public void AppendChatBoxContentAll(ChromiumWebBrowser browser, string messType,
            string userID, DateTime originTime,
            ChatBoxContent content, string MineID,string FriendID,
            System.Drawing.Color color, bool followingWords, bool isOnload = false)
        {
            if (browser.IsBrowserInitialized)
            {
                string fontName = content.Font==null?"宋体": content.Font.FontFamily.Name;
                string fontSize = content.Font == null ? "12":content.Font.Size.ToString();
                switch (messType)
                {
                    case SysParams.Sys_AddFriendMessage://添加好友消息
                       
                        //AddUserForm(string JID, string messageStr)
                        break;
                    case SysParams.Sys_Normal://标注聊天消息
                        int subLength = SysParams.Sys_Normal.Length;
                        if (!string.IsNullOrEmpty(content.Text))
                        {
                            content.Text = content.Text.Substring(subLength);
                        }
                        this.AppendChatBoxContent(browser, userID, originTime,
                            content.Text, content.ForeignImageDictionary, followingWords, MineID, FriendID
                            , color, fontName, fontSize, originTime != null);
                        break;
                    case SysParams.Sys_VibrationMessage://震动提示
                        this.AppendSysContent(browser,userID, originTime, content.Text,MineID,FriendID, color, followingWords, originTime != null);
                        if (FormObj is ChatFormForWeb)
                        {
                            Common.Vibration(FormObj);
                        }
                        break;
                    case SysParams.Sys_VoiceMessage://语音
                        string fileName = content.Text.Replace(SysParams.Sys_VoiceMessage, "");
                        if (!string.IsNullOrEmpty(fileName))
                        {
                            AppendVoiceContent(browser, userID, fileName, originTime,MineID,FriendID,fontName,fontSize);
                        }
                        break;
                    case SysParams.Sys_SendFileMessage://文件发送-处理发送方消息
                        string sendContent = string.Empty;
                        if (!string.IsNullOrEmpty(content.Text))
                        {
                            subLength = SysParams.Sys_SendFileMessage.Length;
                            sendContent = content.Text.Substring(subLength);
                            FileClass file = JsonConvert.DeserializeObject<FileClass>(sendContent);
                            if (FormObj is ChatFormForWeb && file != null)
                            {
                                file.IsSender = false;
                                
                                    //添加控件
                                    ((ChatFormForWeb)FormObj).AddProcess_Receive(file, isOnload);
                            }
                        }
                        break;
                    case SysParams.Sys_ReceiveFileMessage://文件发送-处理接收方 发送的同意请求消息
                        if (!string.IsNullOrEmpty(content.Text))
                        {
                            sendContent = content.Text.Substring(SysParams.Sys_ReceiveFileMessage.Length);
                            FileClass file = JsonConvert.DeserializeObject<FileClass>(sendContent);
                            if (FormObj is ChatFormForWeb && file != null)
                            {
                                file.IsSender = false;
                                ((ChatFormForWeb)FormObj).setFileReceivePath(file);
                                //添加控件
                                ((ChatFormForWeb)FormObj).OPENFIRE_SendFile(file);
                            }
                        }
                        break;
                    case SysParams.Sys_File_Warming:
                        this.AppendSysContent(browser, userID, originTime, content.Text,MineID,FriendID, color, followingWords, originTime != null);
                        break;
                    case SysParams.Sys_File_Cancel://文件取消
                        if (!string.IsNullOrEmpty(content.Text))
                        {
                            //subLength = SysParams.Sys_SendFileMessage.Length;
                            sendContent = content.Text.Substring(SysParams.Sys_File_Cancel.Length);

                            FileClass file = JsonConvert.DeserializeObject<FileClass>(sendContent);
                            if (FormObj is ChatFormForWeb && file != null)
                            {
                                file.IsSender = false;
                                //添加控件
                                ((ChatFormForWeb)FormObj).RemoveProgressBar(file, isOnload);
                            }
                        }
                        this.AppendSysContent(browser, userID, originTime, content.Text,MineID,FriendID, color, followingWords, originTime != null);
                        break;
                    case SysParams.Sys_File_Success:
                        if (!string.IsNullOrEmpty(content.Text))
                        {
                            //subLength = SysParams.Sys_SendFileMessage.Length;
                            sendContent = content.Text.Substring(SysParams.Sys_File_Success.Length);
                            FileClass file = JsonConvert.DeserializeObject<FileClass>(sendContent);
                            string sendName = userID;
                            bool isSelf = false;
                            if ( file != null)
                            {
                                if (FormObj is ChatFormForWeb)
                                {
                                    sendName = MineID;
                                    isSelf = true;
                                }
                                else if(FormObj is HistoryForm)
                                {
                                    sendName = userID;
                                    if (userID != MineID)
                                    {
                                        sendName = MineID;
                                        isSelf = true;
                                    }
                                    else {
                                        return;
                                    }
                                }
                            }
                            if (!string.IsNullOrEmpty(sendName) && sendName.Contains("@"))
                            {
                                sendName = sendName.Substring(0, sendName.IndexOf('@'));
                            }
                            if (file.IsSender)
                            {
                                this.addHistory_File(browser, isSelf, "完成", sendName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                 file.IcoUrl, file.FileName, file.FileSize,
                                 SysParams.File_Send_Result, fontName, fontSize);
                            }
                            else {
                                this.addHistory_File(browser, isSelf, "完成", sendName,
                                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), file.IcoUrl,
                                file.ReceivePath, file.FileSize,
                                string.Format(SysParams.File_Receive_Result, file.ReceivePath),
                                fontName, fontSize);
                            }
                        }


                            //this.AppendSysContent(browser, userID, originTime, content.Text,MineID,FriendID, color, followingWords, originTime != null);
                        break;
                        case SysParams.Sys_OffLine_SendFileMessage://离线消息
                            sendContent = string.Empty;
                            if (!string.IsNullOrEmpty(content.Text))
                            {
                                subLength = SysParams.Sys_OffLine_SendFileMessage.Length;
                                sendContent = content.Text.Substring(subLength);
                                FileClass file = JsonConvert.DeserializeObject<FileClass>(sendContent);
                                if (FormObj is ChatFormForWeb && file != null)
                                {
                                    file.IsSender = false;

                                    //添加控件
                                    ((ChatFormForWeb)FormObj).AddProcess_Receive(file, isOnload);
                                }
                            }
                        break;
                    case SysParams.Sys_OffLine_Success:
                        sendContent = string.Empty;
                        if (!string.IsNullOrEmpty(content.Text))
                        {
                            subLength = SysParams.Sys_OffLine_Success.Length;
                            sendContent = content.Text.Substring(subLength);
                            FileClass file = JsonConvert.DeserializeObject<FileClass>(sendContent);
                            this.AppendSysContent(browser, userID, originTime, content.Text, MineID, FriendID, color, followingWords, originTime != null);
                        }
                            break;

                }
            }
        }


        #endregion


        #region 历史记录

        /// <summary>
        /// 添加分割线
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="DateStr"></param>
        public void AppendHistoryLine(ChromiumWebBrowser browser,string DateStr)
        {
            comm.History_DateLine(browser, DateStr);
        }

        #endregion

        #region 语音
        /// <summary>
        /// 发送语音消息
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="fileName"></param>
        /// <param name="originTime"></param>
        /// <param name="FileName"></param>
        public void AppendVoiceContent(ChromiumWebBrowser browser, string userID, string fileName, DateTime originTime,
            string MineID,string FriendID,string fontName,string fontSize)
        {

            string showTime = DateTime.Now.ToLongTimeString();

            #region 添加聊天历史记录
            bool isSelf = true;
            string sendName = MineID;
            string sendTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (userID == FriendID)
            {
                isSelf = false;
                sendName = FriendID;
            }
            if (!string.IsNullOrEmpty(sendName) && sendName.Contains("@"))
            {
                sendName = sendName.Substring(0, sendName.IndexOf('@'));

            }


            string voicePath = TempFile.FileUploadWebSite + "voice/" + fileName;
            string id = fileName.Contains(".") ? fileName.Substring(0, fileName.LastIndexOf('.')) : fileName;
            this.addHistory_VoiceTalk(browser,isSelf, sendName, sendTime, id, voicePath, 
                fontName,fontSize);
            #endregion

            if (isSelf && FormObj is ChatFormForWeb)
            {
                ChatFormForWeb frm = (ChatFormForWeb)FormObj;
                ChatBoxContent content = new ChatBoxContent();
                content.Text = SysParams.Sys_VoiceMessage + fileName;
                frm.OPENFIRE_SendMessage(content,FriendID);
            }
        }


        /// <summary>
        /// 语音消息
        /// </summary>
        /// <param name="isSelf"></param>
        /// <param name="content"></param>
        /// <param name="sendName"></param>
        /// <param name="sendTime"></param>
        public void addHistory_VoiceTalk(ChromiumWebBrowser browser, bool isSelf, string sendName,
            string sendTime, string fileName, string voiceUrl,string fontName,string fontSize)
        {


            #region 替换换行符
            //string html = "addVoice(false,'a2', 'sendname', '2017-01-01', '微软雅黑','12px','Source/yujunming/img/ae2d2f5c9ecc43dab71fb7d279f124b7.jpg','Source/default/voice/song.mp3')";
            #endregion

            string fontFamily = fontName;
            fontSize = fontSize + "px";
            string imgHead = TempFile.retImgHeadByUserName(sendName);


            //content.Text = html;



            string sendInfo = comm.sendMessage_Voice(browser,
                isSelf.ToString().ToLower(),
                fileName,
                sendName,
                sendTime,
                fontFamily,
                fontSize,
                imgHead,
                voiceUrl);

        }


        #endregion

        #region 系统消息
        /// <summary>
        /// 系统消息
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="originTime"></param>
        /// <param name="content"></param>
        /// <param name="color"></param>
        /// <param name="followingWords"></param>
        /// <param name="offlineMessage"></param>
        public void AppendSysContent(ChromiumWebBrowser browser, string userID, DateTime originTime, 
            string contentStr,string MineID,string FriendID,
            System.Drawing.Color color, bool followingWords, bool offlineMessage)
        {
            #region 添加聊天历史记录
            bool isSelf = true;
            string sendName = MineID;
            string sendTime = originTime.ToString("yyyy-MM-dd HH:mm:ss");
            if (userID == FriendID)
            {
                isSelf = false;
                sendName = FriendID;
            }
            if (!string.IsNullOrEmpty(sendName) && sendName.Contains("@"))
            {
                sendName = sendName.Substring(0, sendName.IndexOf('@'));

            }



            this.addHistory_SysTalk(browser,isSelf, contentStr, sendName, sendTime);
            #endregion

        }


        public void addHistory_SysTalk(ChromiumWebBrowser browser, bool isSelf, string contentStr, 
            string sendName, string sendTime)
        {
            string MessageStr = string.Empty;
            SysInfoType st = SysInfoType.OK;
            string typeStr = contentStr.Substring(0, contentStr.IndexOf("]") + 1);
            switch (typeStr)
            {
                case SysParams.Sys_VibrationMessage:
                    MessageStr = Common.VibrationMessage(isSelf, sendName);
                    break;
                case SysParams.Sys_File_Success://系统成功的消息
                    MessageStr = Common.VibrationMessage(isSelf, sendName);
                    break;
                case SysParams.Sys_File_Warming://系统失败的消息
                    MessageStr = Common.VibrationMessage(isSelf, sendName);
                    break;
                case SysParams.Sys_File_Cancel:
                    string fileContent = contentStr.Substring(contentStr.IndexOf("]") + 1);
                    FileClass file = JsonConvert.DeserializeObject<FileClass>(fileContent);
                    st = SysInfoType.Fail;
                    MessageStr = Common.File_Message(isSelf, file.SaveFileName, file.FileSize, file.IsSender);
                    break;
                case SysParams.Sys_OffLine_Success:
                    fileContent = contentStr.Substring(contentStr.IndexOf("]") + 1);
                    file = JsonConvert.DeserializeObject<FileClass>(fileContent);
                    MessageStr = Common.File_OffLine_Message(isSelf, file.SaveFileName, file.FileSize, file.IsSender);
                    break;
            }
            //string html = MessageStr;
            if (!string.IsNullOrEmpty(MessageStr))
            {
                comm.sendMessage_Sys(browser, MessageStr, st);
            }
        }



        #endregion

        #region 标准聊天消息
        /// <summary>
        /// 标准聊天记录
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="userID">发送人</param>
        /// <param name="originTime">发送时间</param>
        /// <param name="contentStr">内容</param>
        /// <param name="DictImage">图片</param>
        /// <param name="followingWords"></param>
        /// <param name="offlineMessage">是否离线</param>
        /// <param name="MineID">自身Id</param>
        /// <param name="FriendID">对方Id</param>
        /// <param name="color">文字颜色</param>
        /// <param name="fontName">文字名称</param>
        /// <param name="fontSize">文字大小</param>
        /// <param name="isOffline">是否离线</param>
        public string AppendChatBoxContent(ChromiumWebBrowser browser, string userID,
            DateTime originTime,string contentStr,Dictionary<uint,Image> DictImage, 
          
            bool followingWords, string MineID,string FriendID,
            System.Drawing.Color color,string fontName, string fontSize, bool offlineMessage = false)
        {
            #region 添加聊天历史记录
            bool isSelf = true;
            string sendName = MineID;
            string sendTime = originTime.ToString("yyyy-MM-dd HH:mm:ss");
            if (userID == FriendID)
            {
                isSelf = false;
                sendName = FriendID;
            }
            if (!string.IsNullOrEmpty(sendName) && sendName.Contains("@"))
            {
                sendName = sendName.Substring(0, sendName.IndexOf('@'));

            }

            //this.chatBox_history.AppendRichText(string.Format("{0}  {1}\n", userID, showTime), new Font(this.messageFont, FontStyle.Regular), color, userID,MineID, FriendID);
            if (offlineMessage)
            {
                //this.addHistory_Talk(browser, isSelf, contentStr, null, sendName, sendTime,fontName, fontSize);
            }

           string html= this.addHistory_Talk(browser, isSelf, contentStr, DictImage, sendName, sendTime, fontName, fontSize);
            #endregion
            return html;
        }


        /// <summary>
        /// 纯文本消息
        /// </summary>
        /// <param name="isSelf"></param>
        /// <param name="html"></param>
        /// <param name="sendName"></param>
        /// <param name="sendTime"></param>
        public string addHistory_Talk(ChromiumWebBrowser browser, bool isSelf, string html,
            Dictionary<uint,Image> dictImage,
            string sendName, string sendTime, string FontName, string fontSize)
        {


            #region 追加图片标签
            if (dictImage != null && dictImage.Keys.Count > 0)
            {
                int index = 0;
                foreach (char c in html)
                {
                    uint indextmp = (uint)(index);
                    if (dictImage.Keys.Contains(indextmp))
                    {
                        Image img = dictImage[indextmp];

                        //缓存远程服务器方式
                        string imgFile = img.Tag != null ? img.Tag.ToString() : string.Empty;
                        string imgName = string.Empty;
                        if (File.Exists(imgFile))//本地所产生的图片
                        {
                            imgName = TempFile.UpLoad_Image(TempFile.FileUploadWebSite, img.Tag.ToString()); //TempFile.saveTalkImg(img, sendName);
                        }
                        else if (string.IsNullOrEmpty(imgFile))
                        {
                            imgName = TempFile.UpLoad_ImageStream(TempFile.FileUploadWebSite, img);
                        }
                        string htmlStr = string.Format("<img ondblclick=\"showPic_html_Remote(this)\" onload=\"AutoResizeImageForImg(this)\" src=\"{0}\"/>", new string[] { imgName });

                        img.Dispose();//释放图片

                        html += htmlStr;
                        index++;
                        continue;
                    }
                    //else if (c == '')
                    //{

                    //}
                    html += c;
                    index++;
                }
            }
            #endregion

            #region 替换换行符
            html = Common.ChatTextFilter(html);
            #endregion




            string fontFamily = FontName;
            fontSize = fontSize + "px";
            string imgHead = TempFile.retImgHeadByUserName(sendName);




            comm.sendMessage(browser,
                isSelf.ToString().ToLower(),
                html,
                sendName,
                sendTime,
                fontFamily,
                fontSize,
                imgHead);
            return html;
        }

        #endregion

        #region 文件发送
        /// <summary>
        /// 文件发送
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="isSelf"></param>
        /// <param name="strConent"></param>
        /// <param name="sendName"></param>
        /// <param name="sendTime"></param>
        /// <param name="fontFamily"></param>
        /// <param name="fontSize"></param>
        /// <param name="fileIcoUrl">文件缩略图</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="fileSize">文件大小</param>
        /// <param name="fileResult">传输结果</param>
        public void addHistory_File(ChromiumWebBrowser browser, bool isSelf, string strConent, string sendName, string sendTime,
           string fileIcoUrl, string fileName, long fileSize, string fileResult,string fontName,string fontSize)
        {
            fileName = fileName == null ? string.Empty : fileName;
            string fileSaveName = !string.IsNullOrEmpty(fileName)?fileName.Substring(fileName.LastIndexOf(@"\") + 1):string.Empty;
            if (fileSaveName.Length > SysParams.Limit_Html_StrLength_FileName)
            {
                fileSaveName = fileSaveName.Substring(0, SysParams.Limit_Html_StrLength_FileName)+"...";
            }

            string fileDir = !string.IsNullOrEmpty(fileName) ? fileName.Substring(0, fileName.LastIndexOf(@"\") + 1):string.Empty;
            #region 替换换行符
            //string html = "addVoice(false,'a2', 'sendname', '2017-01-01', '微软雅黑','12px','Source/yujunming/img/ae2d2f5c9ecc43dab71fb7d279f124b7.jpg','Source/default/voice/song.mp3')";
            #endregion

            string fontFamily = fontName;
            fontSize = fontSize + "px";
            string imgHead = TempFile.retImgHeadByUserName(sendName);
            strConent = Common.ChatTextFilter(strConent); //strConent.Replace("\r\n", "<br //>").Replace("\n", "<br //>").Replace("\\", "\\\\").Replace("'", "\\'");
            fileName = Common.ChatTextFilter(fileName); //fileName.Replace("\r\n", "<br //>").Replace("\n", "<br //>").Replace("\\", "\\\\").Replace("'", "\\'");
            fileSaveName = Common.ChatTextFilter(fileSaveName); //fileSaveName.Replace("\r\n", "<br //>").Replace("\n", "<br //>").Replace("\\", "\\\\").Replace("'", "\\'");
            fileDir = Common.ChatTextFilter(fileDir); // fileDir.Replace("\r\n", "<br //>").Replace("\n", "<br //>").Replace("\\", "\\\\").Replace("'", "\\'");
            fileResult = Common.ChatTextFilter(fileResult); //fileResult.Replace("\r\n", "<br //>").Replace("\n", "<br //>").Replace("\\", "\\\\").Replace("'", "\\'");
                                                            //content.Text = html;
         
            string intoFileSize = "(" + Common.FormatFileSize(fileSize) + ")";

            comm.sendFileMessage(browser,
                isSelf.ToString().ToLower(),
                fileName,
                sendName,
                sendTime,
                fontFamily,
                fontSize,
                imgHead, fileIcoUrl, fileSaveName, fileDir, intoFileSize, fileResult
                );

        }
        #endregion

        /*
        /// <summary>
        /// 消息处理展示
        /// </summary>
        /// <param name="messType"></param>
        /// <param name="isSysMessage"></param>
        /// <param name="userID"></param>
        /// <param name="originTime"></param>
        /// <param name="content"></param>
        /// <param name="color"></param>
        /// <param name="followingWords"></param>
        public void AppendChatBoxContentAll(string messType, string userID, DateTime? originTime,
            ChatBoxContent content, System.Drawing.Color color, bool followingWords, ChromiumWebBrowser browser,
            bool isOnload = false,string MINEID,string FriendID)
        {
            if (browser.IsBrowserInitialized)
            {
                switch (messType)
                {
                    case SysParams.Sys_Normal://标注聊天消息
                        int subLength = SysParams.Sys_Normal.Length;
                        if (!string.IsNullOrEmpty(content.Text))
                        {
                            content.Text = content.Text.Substring(subLength);
                        }
                        this.AppendChatBoxContent(browser,userID, originTime, content, color, followingWords, originTime != null, MINEID, FriendID);
                        break;
                    case SysParams.Sys_VibrationMessage://震动提示
                        this.AppendSysContent(userID, originTime, content, color, followingWords, originTime != null);
                        Common.Vibration(this);
                        break;
                    case SysParams.Sys_VoiceMessage://语音
                        string fileName = content.Text.Replace(SysParams.Sys_VoiceMessage, "");
                        if (!string.IsNullOrEmpty(fileName))
                        {
                            AppendVoiceContent(userID, fileName, originTime);
                        }
                        break;
                    case SysParams.Sys_SendFileMessage://文件发送-处理发送方消息
                        string sendContent = string.Empty;
                        if (!string.IsNullOrEmpty(content.Text))
                        {
                            subLength = SysParams.Sys_SendFileMessage.Length;
                            sendContent = content.Text.Substring(subLength);
                            FileClass file = JsonConvert.DeserializeObject<FileClass>(sendContent);
                            if (file != null)
                            {
                                file.IsSender = false;
                                //添加控件
                                AddProcess_Receive(file, isOnload);
                            }
                        }
                        break;
                    case SysParams.Sys_ReceiveFileMessage://文件发送-处理接收方 发送的同意请求消息
                        if (!string.IsNullOrEmpty(content.Text))
                        {
                            sendContent = content.Text.Substring(SysParams.Sys_ReceiveFileMessage.Length);
                            FileClass file = JsonConvert.DeserializeObject<FileClass>(sendContent);
                            if (xmppSendFileOrFolderBtnClick != null && file != null)
                            {
                                //string ss = this.FriendID;//FriendIDAndStaue
                                xmppSendFileOrFolderBtnClick(file.FileName, file.FileSize, FriendIDAndStaue, file.FileId + "," + this.FriendID);
                            }
                        }
                        break;
                    case SysParams.Sys_File_Warming:
                        this.AppendSysContent(userID, originTime, content, color, followingWords, originTime != null);
                        break;
                    case SysParams.Sys_File_Cancel://文件取消
                        if (!string.IsNullOrEmpty(content.Text))
                        {
                            //subLength = SysParams.Sys_SendFileMessage.Length;
                            sendContent = content.Text.Substring(SysParams.Sys_File_Cancel.Length);

                            FileClass file = JsonConvert.DeserializeObject<FileClass>(sendContent);
                            if (file != null)
                            {
                                file.IsSender = false;
                                //删除控件
                                RemoveProgressBar(file, isOnload);
                            }
                        }
                        this.AppendSysContent(userID, originTime, content, color, followingWords, originTime != null);
                        break;
                    case SysParams.Sys_File_Success:
                        this.AppendSysContent(userID, originTime, content, color, followingWords, originTime != null);
                        break;
                }
            }
        }



        


        /// <summary>
        /// 标准聊天消息
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="originTime"></param>
        /// <param name="content"></param>
        /// <param name="color"></param>
        /// <param name="followingWords"></param>
        /// <param name="offlineMessage"></param>
        private void AppendChatBoxContent(ChromiumWebBrowser browser, string userID, 
            DateTime? originTime, ChatBoxContent content, System.Drawing.Color color, 
            bool followingWords, bool offlineMessage,string MineID,string FriendID)
        {
            string showTime = DateTime.Now.ToLongTimeString();
            if (!offlineMessage && originTime != null)
            {
                showTime = originTime.Value.ToString();
            }

            #region 添加聊天历史记录
            bool isSelf = true;
            string sendName = MineID;
            string sendTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (userID == FriendID)
            {
                isSelf = false;
                sendName = FriendID;
            }
            if (!string.IsNullOrEmpty(sendName) && sendName.Contains("@"))
            {
                sendName = sendName.Substring(0, sendName.IndexOf('@'));

            }

            //this.chatBox_history.AppendRichText(string.Format("{0}  {1}\n", userID, showTime), new Font(this.messageFont, FontStyle.Regular), color, userID,MineID, FriendID);
            if (originTime != null && offlineMessage)
            {
                // this.chatBox_history.AppendText(string.Format("    [{0} 离线消息] ", originTime.Value.ToString()));
                string contentStr = string.Format("    [{0} 离线消息] ", originTime.Value.ToString());

                this.addHistory_Talk(browser,isSelf, contentStr, sendName, sendTime,content.Font.FontFamily.Name,content.Font.Size.ToString());
            }
     
            this.addHistory_Talk(browser, isSelf, content, sendName, sendTime);
            #endregion

        }

        /// <summary>
        /// 纯文本消息
        /// </summary>
        /// <param name="isSelf"></param>
        /// <param name="html"></param>
        /// <param name="sendName"></param>
        /// <param name="sendTime"></param>
        public void addHistory_Talk(ChromiumWebBrowser browser,bool isSelf, string html, 
            string sendName, string sendTime,string FontName,string fontSize)
        {

            #region 替换换行符
            html = html.Replace("\r\n", "<br //>").Replace("\n", "<br //>");
            #endregion

            string fontFamily = FontName;
            fontSize = fontSize + "px";
            string imgHead = TempFile.retImgHeadByUserName(sendName);




            comm.sendMessage(browser,
                isSelf.ToString().ToLower(),
                html,
                sendName,
                sendTime,
                fontFamily,
                fontSize,
                imgHead);

        }

        /// <summary>
        /// 含有图片的消息
        /// </summary>
        /// <param name="isSelf"></param>
        /// <param name="content"></param>
        /// <param name="sendName"></param>
        /// <param name="sendTime"></param>
        public void addHistory_Talk(ChromiumWebBrowser browser, bool isSelf, 
            ChatBoxContent content, string sendName, string sendTime)
        {
            #region 追加图片标签
            string html = string.Empty;

            int index = 0;
            foreach (char c in content.Text)
            {
                uint indextmp = (uint)(index);
                if (content.ForeignImageDictionary.Keys.Contains(indextmp))
                {
                    Image img = content.ForeignImageDictionary[indextmp];

                    //缓存远程服务器方式
                    string imgFile = img.Tag != null ? img.Tag.ToString() : string.Empty;
                    string imgName = string.Empty;
                    if (File.Exists(imgFile))//本地所产生的图片
                    {
                        imgName = TempFile.UpLoad_Image(TempFile.FileUploadWebSite, img.Tag.ToString()); //TempFile.saveTalkImg(img, sendName);
                    }
                    else if (string.IsNullOrEmpty(imgFile))
                    {
                        imgName = TempFile.UpLoad_ImageStream(TempFile.FileUploadWebSite, img);
                    }
                    string htmlStr = string.Format("<img ondblclick=\"showPic_html_Remote(this)\" onload=\"AutoResizeImageForImg(this)\" src=\"{0}\"/>", new string[] { imgName });

                    html += htmlStr;
                    index++;
                    continue;
                }
                //else if (c == '')
                //{

                //}
                html += c;
                index++;
            }
            #endregion

            #region 替换换行符,单引号，反斜杠 双引号
            html = html.Replace("\r\n", "<br //>").Replace("\n", "<br //>").Replace("\\", "\\\\").Replace("'", "\\'");
            #endregion

            string fontFamily = content.Font.FontFamily.Name;
            string fontSize = content.Font.Size + "px";
            string imgHead = TempFile.retImgHeadByUserName(sendName);


            content.Text = html;



            comm.sendMessage(browser,
                isSelf.ToString().ToLower(),
                html,
                sendName,
                sendTime,
                fontFamily,
                fontSize,
                imgHead);


        }
        */
    }
}
