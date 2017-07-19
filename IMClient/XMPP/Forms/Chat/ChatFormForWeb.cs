using CCWin.SkinControl;
using CefSharp;
using CefSharp.WinForms;
using ESPlus.Rapid;
using IMClient.Controls;
using IMClient.Controls.Base;
using IMClient.Logic;
using IMClient.Logic.Sql;
using IMClient.Native;
using IMClient.Properties;
using JustLib;
using JustLib.Controls;
using Matrix.Xmpp.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using NetDLims.Services;

namespace IMClient.XMPP.Forms
{
    public partial class ChatFormForWeb : FrameBase
    {
        #region 内部参数
        SqlLiteHelper sqliteHelper = new SqlLiteHelper();
        /// <summary>
        /// 是否已记录最近联系人
        /// </summary>
        private bool isSaveRecent = false;

        private string FriendName = string.Empty;

        /// <summary>
        /// 文件上传控制类
        /// </summary>
        FileUpLoadController fileController = null;

        Common comm = new Common();
        public Chat chatClass = null;
        //声明一个加载网页的WebBrowser
        public ChromiumWebBrowser browser;

        private string fileWebSite = "";

        private Font messageFont = new Font("微软雅黑", 9);
        
        public string MineID;

        public string FriendID_Small {
            get
            {
                string sendName = FriendID.Substring(0, FriendID.IndexOf('@'));
                return sendName;
            }
        }

        public string MineID_Small {
            get {
               string sendName = MineID.Substring(0, MineID.IndexOf('@'));
                return sendName;
            }
        }

        public string FriendID;

        /// <summary>
        /// 未读取消息
        /// </summary>
        public List<ChatBoxContent> unReadMessage = new List<ChatBoxContent>();

        public string FriendIDAndStaue { get; set; }
        public ChatListSubItem Friend;
        private IRapidPassiveEngine rapidPassiveEngine;
        private string Title_FileTransfer = "文件";

        XmppClient client;

        /// <summary>
        /// 右侧框提
        /// </summary>
        public Panel RightSendPanel {
            get {
                return this.skinPanel_right;
            }
        }
        #endregion
        #region 外部使用参数
        public ChatBoxContent ChatBoxSend
        {
            get {
                return this.chatBoxSend.GetContent();
            }
        }
        #endregion

        public ChatFormForWeb(ChatListSubItem friend, string mineID , ChatBoxContent content)
        {
          
            InitializeComponent();
            this.chatClass = new Chat(this);
            #region 添加浏览器
            string chatWebPath = AppDomain.CurrentDomain.BaseDirectory + SysParams.Sys_MessageShownHtml;
            if (File.Exists(chatWebPath))
            {
                browser = new ChromiumWebBrowser(chatWebPath)
                {
                    Dock = DockStyle.Fill,

                };
                browser.FrameLoadEnd += Browser_FrameLoadEnd;
                browser.Load(chatWebPath);
                //"jsOBJ"
                comm.RegObjectTOCEF(browser,comm, "jsOBJ");
                panelChat.Controls.Add(browser);
             
                browser.DragDrop += Control_DragDrop;
                browser.DragEnter += Control_DragEnter;
                browser.AllowDrop = true;
            }
            #endregion
           
            //this.Invoke(new Action(FileUploadPanelAdjust));
            chatBoxSend.DragDrop += Control_DragDrop;
            chatBoxSend.DragEnter += Control_DragEnter;
            chatBoxSend.AllowDrop = true;
            pbxFriend.Image = friend.HeadImage;

            FriendName=lblName.Text = friend.DisplayName;
            lblCompany.Text = friend.PersonalMsg;

            FriendIDAndStaue = ((GGUser)(friend.Tag)).JID.ToString();    
            Friend = friend;

            chatHeadState(friend);


            MineID = mineID;
            FriendID = friend.ID;
            fileController = new FileUpLoadController(FriendID,MineID);
            fileController.RemoveItem = this.RemoveProgressBar;
            fileController.sendMessage = this.SendMessage_File;
            fileController.addFileMessage = this.AddResult_OffLine_SendFile;
            fileController.AddProcessCotrol = this.AddProcessCotrol;
            //if (FriendID.Equals("yujunming192.168.1.95"))
            //{
            //    client = new XmppClient("chengle", "192.168.1.95", "111111");

            //}
            //else
            //{
            //    client = new XmppClient("yujunming", "192.168.1.95", "111111");
            //}
            //client.OnMessage += C_OnMessage;

            //client.Open();

            //Console.WriteLine("{0}", client);

        }


        

        private void ChatForm_Shown(object sender, EventArgs e)
        {
            this.chatBoxSend.Focus();
        }



        private void Browser_FrameLoadEnd(object sender, CefSharp.FrameLoadEndEventArgs e)
        {
            if (this.unReadMessage != null && this.unReadMessage.Count > 0)
            {
                while (unReadMessage.Count > 0)
                {
                    // bool isSys = SysParams.isSysInfo(unReadMessage[0].Text);
                    string messType = SysParams.retSysMessage(unReadMessage[0].Text);
                    this.chatClass.AppendChatBoxContentAll(this.browser, messType, this.FriendID, DateTime.Now, unReadMessage[0],
                        this.MineID, this.FriendID, Color.SeaGreen, true, true);
                    //this.AppendChatBoxContentAll(messType, this.FriendID, null, unReadMessage[0], Color.SeaGreen, true,true);
                    unReadMessage.RemoveAt(0);
                }
                this.Invoke(new Action(() =>
                {
                    FileUploadPanelAdjust();
                }));
            }
            this.Invoke(new Action(() => {
                this.ibtnSend.Enabled = true;
                //FileUploadPanelAdjust();
            }));

        }



        //private void C_OnMessage(object sender, MessageEventArgs e)
        //{

        //}

        /// <summary>
        /// 根据当前所聊天的好友状态更改聊天chat的头像状态
        /// </summary>
        public void chatHeadState(ChatListSubItem friend) 
        {
            switch (friend.Status)
            {
                case ChatListSubItem.UserStatus.QMe:
                    break;
                case ChatListSubItem.UserStatus.Online:
                    pbxFriend.Image = friend.HeadImage;
                    break;
                case ChatListSubItem.UserStatus.Away:
                    break;
                case ChatListSubItem.UserStatus.Busy:
                    break;
                case ChatListSubItem.UserStatus.DontDisturb:
                    break;
                case ChatListSubItem.UserStatus.OffLine:
                    pbxFriend.Image = ESBasic.Helpers.ImageHelper.ConvertToGrey(friend.HeadImage);
                    break;
                default:
                    break;
            }
        }




        private void Fm_OnProgress(object sender, Matrix.Xmpp.Client.FileTransferEventArgs e)
        {
        }

        #region 标准功能键 最大 最小 关闭
        /// <summary>
        /// 最小化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ibtnMin_Click(object sender, EventArgs e)
        {
            this.ShowInTaskbar = true;
            this.WindowState = FormWindowState.Minimized;
            //this.BringToFront();


        }

        /// <summary>
        /// 最大化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageButton2_Click(object sender, EventArgs e)
        {
            //this.StartPosition = FormStartPosition.CenterScreen;
            ImageButton pbx = sender as ImageButton;
            this.WindowState = this.WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
            if (this.WindowState == FormWindowState.Maximized)
            {
                pbx.Activepic = Resources.Reducing_down_over_02;
                pbx.Staticpic = Resources.Reducing_down_normal_02;
                pbx.Presspic = Resources.Reducing_down_down_02;
            }
            else
            {
                pbx.Activepic = Resources.max_over_02;
                pbx.Staticpic = Resources.max_normal_02;
                pbx.Presspic = Resources.max_down_02;
            }
            setHistoryFormSize();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void openChatClose(string friendID);
        public event openChatClose OpenChatClose;
        private void imageButton3_Click(object sender, EventArgs e)
        {
            if (OpenChatClose != null)
            {
                OpenChatClose(FriendID);

            }
            
            if (this.skinPanel_right.Controls.Count > 0 &&
                MessageBox.Show("正在进行文件上传/接收工作，是否终止当前任务继续关闭?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Cancel_FileUpload(this.skinPanel_right);
            }
            fileController.Dispose();
            this.Close();
                   
        }
        #endregion





        #region 界面重绘部分
        /// <summary>
        /// 窗体加载阴影
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChatForm_Load(object sender, EventArgs e)
        {
            //FileUploadPanelAdjust();
            Win32Api.SetClassLong(this.Handle, Win32Api.GCL_STYLE, Win32Api.GetClassLong(this.Handle, Win32Api.GCL_STYLE) | Win32Api.CS_DropSHADOW);
        }

        private void ChatForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = Graphics.FromHwnd(base.Handle);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.AntiAlias;          
            //if (this.WindowState == FormWindowState.Maximized)
            //{
            //    imageButton2.Activepic = Resources.Reducing_down_over_02;
            //    imageButton2.Staticpic = Resources.Reducing_down_normal_02;
            //    imageButton2.Presspic = Resources.Reducing_down_down_02;      
            //}
            //else
            //{
            //    imageButton2.Activepic = Resources.max_over_02;
            //    imageButton2.Staticpic = Resources.max_normal_02;
            //    imageButton2.Presspic = Resources.max_down_02;             

            //}
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            #region 标题背景色填充
            Color bgColorTitle = Color.FromArgb(6, 95, 173);
            Rectangle r = new Rectangle(new Point(0, 0), new Size(this.Width, panelChat.Location.Y));
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            //using (LinearGradientBrush lgb = new LinearGradientBrush(r, Color.FromArgb(6, 165, 233),
            //bgColorTitle, LinearGradientMode.Vertical))
            //{
            //    g.FillRectangle(lgb, r);
            //}

            g.DrawImage(Properties.Resources.chat_im01_bg03_03, r);
            //g.DrawImage(Properties.Resources.chat_bg_03, r);
            //g.DrawImage(Properties.Resources.chat_bg_03, r);
            //g.DrawImage(Properties.Resources.chat_bg_03, r);
            //using (SolidBrush sb = new SolidBrush(bgColorTitle))
            //{
            //    g.FillRectangle(sb, r);
            //}


            Color bgColor = Color.FromArgb(216,216,216);
            r = new Rectangle(new Point(0, panelChat.Location.Y), new Size(this.Width,this.Height- panelChat.Location.Y));


            using (SolidBrush sb = new SolidBrush(bgColor))
            {
                g.FillRectangle(sb, r);
            }
            #endregion

            #region 边框线绘制
            //Color borderColor = Color.FromArgb(178, 178, 178);
            //g.DrawLine(new Pen(borderColor, 2.0f)
            //    , new Point(skinPanel_right.Location.X - 2, skinPanel_right.Location.Y),
            //    new Point(skinPanel_right.Location.X - 2, skinPanel_right.Location.Y + skinPanel_right.Height));

            //g.DrawLine(new Pen(borderColor, 1.0f)
            // , new Point(panel2.Location.X , panel2.Location.Y-2),
            // new Point(panel2.Location.X+panel2.Width , panel2.Location.Y-2 ));


            //g.DrawRectangle(new Pen(borderColor, 2.0f),
            //    new Rectangle(new Point(0, 0), new Size(this.Width - 1, this.Height - 1))); 
            #endregion

        }
        #endregion

        #region 标准文本消息发送
        /// <summary>
        /// 消息文本发送 功能
        /// </summary>
        public delegate void XmppSendMessageBtnClickHandle(ChatBoxContent sendcontent, string toFriend);
        /// <summary>
        /// 消息文本发送 功能
        /// </summary>
        public event XmppSendMessageBtnClickHandle xmppSendMessageBtnClick;
        
        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ibtnSend_Click(object sender, EventArgs e)
        {

            SaveRecentLinkManDelegate sd = SaveRecentLinkMan;
            sd.BeginInvoke(null, null);


            this.sendMessage();
        }

        


        /// <summary>
        /// 发送信息
        /// </summary>
        private void sendMessage()
        {
            try
            {
                ChatBoxContent content = this.chatBoxSend.GetContent();




                if (content.IsEmpty())
                {
                    return;
                }



                bool followingWords = false;


               string html= this.AppendChatBoxContent(false, MineID, DateTime.Now, content, Color.SeaGreen, followingWords);



                if (xmppSendMessageBtnClick != null)
                {
                    ChatBoxContent contenttmp = new ChatBoxContent();
                    contenttmp.Text = SysParams.Sys_Normal + html;
                    contenttmp.Font = content.Font;
                    contenttmp.Color = content.Color;
                    OPENFIRE_SendMessage(contenttmp, this.FriendID);
                    //xmppSendMessageBtnClick(content, FriendID);
                }

                //清空输入框
                this.chatBoxSend.Text = string.Empty;
                this.chatBoxSend.Focus();

            }
            catch (Exception ee)
            {
                this.AppendSysMessage("发送消息失败！" + ee.Message);
            }

        }
        #endregion



        private void AppendMessage(string userName, Color color, string msg)
        {
            DateTime showTime = DateTime.Now;
            //this.chatBox_history.AppendRichText(string.Format("{0}  {1}\n", userName, showTime.ToLongTimeString()), new Font(this.messageFont, FontStyle.Regular), color,null, MineID, FriendID);
            //this.chatBox_history.AppendText("    ");


            //this.chatBox_history.AppendText(msg);
            //this.chatBox_history.Select(this.chatBox_history.Text.Length, 0);
            //this.chatBox_history.ScrollToCaret();
        }

        public void AppendSysMessage(string msg)
        {
            this.AppendMessage("系统", Color.Gray, msg);
            //this.chatBox_history.AppendText("\n");
        }


        #region  消息发送处理展示模块 AppendMessage


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
        //public void AppendChatBoxContentAll(string messType,  string userID, DateTime? originTime, 
        //    ChatBoxContent content, Color color, bool followingWords,bool isOnload=false)
        //{
        //    if (this.browser.IsBrowserInitialized)
        //    {
        //        switch (messType)
        //        {
        //            case SysParams.Sys_Normal://标注聊天消息
        //                int subLength = SysParams.Sys_Normal.Length;
        //                if (!string.IsNullOrEmpty(content.Text))
        //                {
        //                    content.Text = content.Text.Substring(subLength);
        //                }
        //                this.AppendChatBoxContent(userID, originTime, content, color, followingWords, originTime != null);
        //                break;
        //            case SysParams.Sys_VibrationMessage://震动提示
        //                this.AppendSysContent(userID, originTime, content, color, followingWords, originTime != null);
        //                Common.Vibration(this);
        //                break;
        //            case SysParams.Sys_VoiceMessage://语音
        //                string fileName = content.Text.Replace(SysParams.Sys_VoiceMessage, "");
        //                if (!string.IsNullOrEmpty(fileName))
        //                {
        //                    AppendVoiceContent(userID, fileName, originTime);
        //                }
        //                break;
        //            case SysParams.Sys_SendFileMessage://文件发送-处理发送方消息
        //                string sendContent = string.Empty;
        //                if (!string.IsNullOrEmpty(content.Text))
        //                {
        //                    subLength = SysParams.Sys_SendFileMessage.Length;
        //                    sendContent = content.Text.Substring(subLength);
        //                    FileClass file = JsonConvert.DeserializeObject<FileClass>(sendContent);
        //                    if (file != null)
        //                    {
        //                        file.IsSender = false;
        //                        //添加控件
        //                        AddProcess_Receive(file, isOnload);
        //                    }
        //                }
        //                break;
        //            case SysParams.Sys_ReceiveFileMessage://文件发送-处理接收方 发送的同意请求消息
        //                if (!string.IsNullOrEmpty(content.Text))
        //                {
        //                    sendContent = content.Text.Substring(SysParams.Sys_ReceiveFileMessage.Length);
        //                    FileClass file = JsonConvert.DeserializeObject<FileClass>(sendContent);
        //                    if (xmppSendFileOrFolderBtnClick != null && file!=null)
        //                    {
        //                        //string ss = this.FriendID;//FriendIDAndStaue
        //                        xmppSendFileOrFolderBtnClick(file.FileName, file.FileSize, FriendIDAndStaue, file.FileId+","+this.FriendID);
        //                    }
        //                }
        //                break;
        //            case SysParams.Sys_File_Warming:
        //                this.AppendSysContent(userID, originTime, content, color, followingWords, originTime != null);
        //                break;
        //            case SysParams.Sys_File_Cancel://文件取消
        //                if (!string.IsNullOrEmpty(content.Text))
        //                {
        //                    //subLength = SysParams.Sys_SendFileMessage.Length;
        //                    sendContent = content.Text.Substring(SysParams.Sys_File_Cancel.Length);
                            
        //                    FileClass file = JsonConvert.DeserializeObject<FileClass>(sendContent);
        //                    if (file != null)
        //                    {
        //                        file.IsSender = false;
        //                        //删除控件
        //                        RemoveProgressBar(file, isOnload);
        //                    }
        //                }
        //                this.AppendSysContent(userID, originTime, content, color, followingWords, originTime != null);
        //                break;
        //            case SysParams.Sys_File_Success:
        //                this.AppendSysContent(userID, originTime, content, color, followingWords, originTime != null);
        //                break;
        //        }
        //    }
        //}

        /// <summary>
        /// 发送消息传当前自己登陆的ID，接收信息传当前点击朋友的ID
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="originTime"></param>
        /// <param name="content"></param>
        /// <param name="color"></param>
        /// <param name="followingWords"></param>
        public string AppendChatBoxContent(bool isSysMessage,string userID, DateTime originTime, ChatBoxContent content, Color color, bool followingWords)
        {
            string html = string.Empty;
            if (this.browser.IsBrowserInitialized)
            {
                if (!isSysMessage)
                {
                    html= this.chatClass.AppendChatBoxContent(this.browser, userID, originTime, content.Text,content.ForeignImageDictionary,
                        followingWords, this.MineID,this.FriendID, color,content.Font.FontFamily.Name,content.Font.Size.ToString(),  
                        originTime != null);
                    //this.AppendChatBoxContent(userID, originTime, content, color, followingWords, originTime != null);
                }
                else {
                    this.chatClass.AppendSysContent(this.browser, userID, originTime, content.Text,this.MineID,this.FriendID, color, followingWords, originTime != null);
                    //this.AppendSysContent(userID, originTime, content, color, followingWords, originTime != null);
                }
            }
            return html;
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
        //private void AppendChatBoxContent(string userID, DateTime? originTime, ChatBoxContent content, Color color, bool followingWords, bool offlineMessage)
        //{
        //    string showTime = DateTime.Now.ToLongTimeString();
        //    if (!offlineMessage && originTime != null)
        //    {
        //        showTime = originTime.Value.ToString();
        //    }

        //    #region 添加聊天历史记录
        //    bool isSelf = true;
        //    string sendName = MineID;
        //    string sendTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        //    if (userID == FriendID)
        //    {
        //        isSelf = false;
        //        sendName = FriendID;
        //    }
        //    if(!string.IsNullOrEmpty(sendName) && sendName.Contains("@"))
        //    {
        //        sendName = sendName.Substring(0, sendName.IndexOf('@'));

        //    }
            
        //    //this.chatBox_history.AppendRichText(string.Format("{0}  {1}\n", userID, showTime), new Font(this.messageFont, FontStyle.Regular), color, userID,MineID, FriendID);
        //    if (originTime != null && offlineMessage)
        //    {
        //        // this.chatBox_history.AppendText(string.Format("    [{0} 离线消息] ", originTime.Value.ToString()));
        //        string contentStr = string.Format("    [{0} 离线消息] ", originTime.Value.ToString());
                
        //        this.addHistory_Talk(isSelf, contentStr,sendName,sendTime);
        //    }
        //    //else
        //    //{
        //    //    //this.addTalkHistory(true, "    ");
        //    //    //this.chatBox_history.AppendText("    ");
        //    //}

        //    //this.chatBox_history.AppendChatBoxContent(content, userID, MineID, FriendID);
        //    //this.chatBox_history.AppendText("\n");
        //    //this.chatBox_history.Select(this.chatBox_history.Text.Length, 0);
        //    //this.chatBox_history.ScrollToCaret();
          


        //    //if (content.ForeignImageDictionary != null && content.ForeignImageDictionary.Keys.Count > 0)
        //    //{
        //    //    foreach(uint postion in content.ForeignImageDictionary.Keys)
        //    //    {
        //    //        Image img = content.ForeignImageDictionary[postion];
        //    //        string imgName = TempFile.saveTalkImg(img, sendName);
                   
        //    //        content.Text.Insert(postion - 1, htmlStr);
        //    //    }
              
        //    //}
        //    this.addHistory_Talk(isSelf, content, sendName,sendTime);
        //    #endregion

        //}


  


        /// <summary>
        /// 发送语音消息
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="fileName"></param>
        /// <param name="originTime"></param>
        /// <param name="FileName"></param>
        //private void AppendVoiceContent(string userID,string fileName, DateTime? originTime)
        //{

        //    string showTime = DateTime.Now.ToLongTimeString();

        //    #region 添加聊天历史记录
        //    bool isSelf = true;
        //    string sendName = MineID;
        //    string sendTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        //    if (userID == FriendID)
        //    {
        //        isSelf = false;
        //        sendName = FriendID;
        //    }
        //    if (!string.IsNullOrEmpty(sendName) && sendName.Contains("@"))
        //    {
        //        sendName = sendName.Substring(0, sendName.IndexOf('@'));

        //    }


        //    string voicePath = TempFile.FileUploadWebSite + "voice/" + fileName;
        //    string id = fileName.Contains(".")?fileName.Substring(0, fileName.LastIndexOf('.')):fileName;
        //    this.addHistory_VoiceTalk(isSelf, sendName, sendTime, id,voicePath);
        //    #endregion

        //    if (isSelf)
        //    {
        //        ChatBoxContent content = new ChatBoxContent();
        //        content.Text = SysParams.Sys_VoiceMessage + fileName;
        //        if (xmppSendMessageBtnClick != null)
        //        {
        //            xmppSendMessageBtnClick(content, FriendID);
        //        }
        //    }
        //}

        /// <summary>
        /// 系统消息
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="originTime"></param>
        /// <param name="content"></param>
        /// <param name="color"></param>
        /// <param name="followingWords"></param>
        /// <param name="offlineMessage"></param>
        //private void AppendSysContent(string userID, DateTime? originTime, ChatBoxContent content, 
        //    Color color, bool followingWords, bool offlineMessage)
        //{



        //    string showTime = DateTime.Now.ToLongTimeString();

        //    #region 添加聊天历史记录
        //    bool isSelf = true;
        //    string sendName = MineID;
        //    string sendTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        //    if (userID == FriendID)
        //    {
        //        isSelf = false;
        //        sendName = FriendID;
        //    }
        //    if (!string.IsNullOrEmpty(sendName) && sendName.Contains("@"))
        //    {
        //        sendName = sendName.Substring(0, sendName.IndexOf('@'));

        //    }



        //    this.addHistory_SysTalk(isSelf, content, sendName, sendTime);
        //    #endregion

        //}

        //Common comm = new Common();

        #region 追加聊天记录



        /// <summary>
        /// 含有图片的消息
        /// </summary>
        /// <param name="isSelf"></param>
        /// <param name="content"></param>
        /// <param name="sendName"></param>
        /// <param name="sendTime"></param>
        //public void addHistory_Talk(bool isSelf,ChatBoxContent content,string sendName,string sendTime)
        //{
        //    #region 追加图片标签
        //    string html = string.Empty;

        //    int index = 0;
        //    foreach (char c in content.Text)
        //    {
        //        uint indextmp = (uint)(index);
        //        if (content.ForeignImageDictionary.Keys.Contains(indextmp))
        //        {
        //            Image img = content.ForeignImageDictionary[indextmp];



        //            //缓存本地方式
        //            //string imgName = TempFile.saveTalkImg(img, sendName);
        //            //string htmlStr = string.Format("<img ondblclick=\"showPic_html(this)\" onload=\"AutoResizeImageForImg(this)\" src=\"Source/{0}/img/{1}\"/>", new string[] { sendName, imgName });

        //            //缓存远程服务器方式
        //            string imgFile = img.Tag != null ? img.Tag.ToString() : string.Empty;
        //            string imgName = string.Empty;
        //            if (File.Exists(imgFile))//本地所产生的图片
        //            {
        //                imgName = TempFile.UpLoad_Image(TempFile.FileUploadWebSite, img.Tag.ToString()); //TempFile.saveTalkImg(img, sendName);
        //            }
        //            else if (string.IsNullOrEmpty(imgFile))
        //            {
        //                imgName = TempFile.UpLoad_ImageStream(TempFile.FileUploadWebSite, img);
        //            }
        //            string htmlStr = string.Format("<img ondblclick=\"showPic_html_Remote(this)\" onload=\"AutoResizeImageForImg(this)\" src=\"{0}\"/>", new string[] { imgName });

        //            html += htmlStr;
        //            index++;
        //            continue;
        //        }
        //        //else if (c == '')
        //        //{

        //        //}
        //        html += c;
        //        index++;
        //    }
        //    #endregion

        //    #region 替换换行符,单引号，反斜杠 双引号
        //    html = html.Replace("\r\n", "<br //>").Replace("\n", "<br //>").Replace("\\","\\\\").Replace("'","\\'");
        //    #endregion

        //    string fontFamily = this.chatBoxSend.Font.FontFamily.Name;
        //    string fontSize = this.chatBoxSend.Font.Size + "px";
        //    string imgHead = TempFile.retImgHeadByUserName(sendName);

            
        //    content.Text = html;



        //    comm.sendMessage(this.browser,
        //        isSelf.ToString().ToLower(),
        //        html,
        //        sendName,
        //        sendTime,
        //        fontFamily,
        //        fontSize,
        //        imgHead);

        //    //comm.CallJS(this.browser, "add(" + isSelf.ToString().ToLower() + ",'"
        //    //    + content + "','"
        //    //    + sendName + "','"
        //    //    + sendTime + "','"
        //    //    + fontFamily + "','"
        //    //    + fontSize + "','"
        //    //    + imgHead + "')");

        //    //CefSharp.IFrame f = this.browser.GetBrowser().MainFrame;
          
        //    //f.ExecuteJavaScriptAsync("add("+isSelf.ToString().ToLower()+",'" 
        //    //    + content + "','"
        //    //    + sendName+"','"
        //    //    + sendTime+"','"
        //    //    + fontFamily + "','"
        //    //    + fontSize+"','"
        //    //    + imgHead + "')");
        //}


        /// <summary>
        /// 语音消息
        /// </summary>
        /// <param name="isSelf"></param>
        /// <param name="content"></param>
        /// <param name="sendName"></param>
        /// <param name="sendTime"></param>
        //public void addHistory_VoiceTalk(bool isSelf,  string sendName, 
        //    string sendTime,string fileName,string voiceUrl)
        //{


        //    #region 替换换行符
        //    //string html = "addVoice(false,'a2', 'sendname', '2017-01-01', '微软雅黑','12px','Source/yujunming/img/ae2d2f5c9ecc43dab71fb7d279f124b7.jpg','Source/default/voice/song.mp3')";
        //    #endregion

        //    string fontFamily = this.chatBoxSend.Font.FontFamily.Name;
        //    string fontSize = this.chatBoxSend.Font.Size + "px";
        //    string imgHead = TempFile.retImgHeadByUserName(sendName);


        //    //content.Text = html;



        //    string sendInfo=  comm.sendMessage_Voice(this.browser,
        //        isSelf.ToString().ToLower(),
        //        fileName,
        //        sendName,
        //        sendTime,
        //        fontFamily,
        //        fontSize,
        //        imgHead, 
        //        voiceUrl);
        
        //}


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
        //public void addHistory_File(bool isSelf, string strConent, string sendName, string sendTime,
        //   string fileIcoUrl, string fileName, long fileSize, string fileResult)
        //{

        //    string fileSaveName = fileName.Substring(fileName.LastIndexOf(@"\") + 1);
        //    if (fileSaveName.Length > SysParams.Limit_Html_StrLength_FileName)
        //    {
        //        fileSaveName = fileSaveName.Substring(0, SysParams.Limit_Html_StrLength_FileName);
        //    }

        //    string fileDir = fileName.Substring(0, fileName.LastIndexOf(@"\")+1);
        //    #region 替换换行符
        //    //string html = "addVoice(false,'a2', 'sendname', '2017-01-01', '微软雅黑','12px','Source/yujunming/img/ae2d2f5c9ecc43dab71fb7d279f124b7.jpg','Source/default/voice/song.mp3')";
        //    #endregion

        //    string fontFamily = this.chatBoxSend.Font.FontFamily.Name;
        //    string fontSize = this.chatBoxSend.Font.Size + "px";
        //    string imgHead = TempFile.retImgHeadByUserName(sendName);
        //    strConent = strConent.Replace("\r\n", "<br //>").Replace("\n", "<br //>").Replace("\\", "\\\\").Replace("'", "\\'");
        //    fileName = fileName.Replace("\r\n", "<br //>").Replace("\n", "<br //>").Replace("\\", "\\\\").Replace("'", "\\'");
        //    fileSaveName = fileSaveName.Replace("\r\n", "<br //>").Replace("\n", "<br //>").Replace("\\", "\\\\").Replace("'", "\\'");
        //    fileDir = fileDir.Replace("\r\n", "<br //>").Replace("\n", "<br //>").Replace("\\", "\\\\").Replace("'", "\\'");
        //    fileResult = fileResult.Replace("\r\n", "<br //>").Replace("\n", "<br //>").Replace("\\", "\\\\").Replace("'", "\\'");
        //    //content.Text = html;

        //    string intoFileSize = "(" + Common.FormatFileSize(fileSize) + ")";

        //    comm.sendFileMessage(this.browser,
        //        isSelf.ToString().ToLower(),
        //        fileName,
        //        sendName,
        //        sendTime,
        //        fontFamily,
        //        fontSize,
        //        imgHead, fileIcoUrl, fileSaveName,fileDir, intoFileSize, fileResult
        //        );

        //}

        /* 20170401 注释
        /// <summary>
        /// 发送系统消息
        /// </summary>
        /// <param name="isSelf"></param>
        /// <param name="content"></param>
        /// <param name="sendName"></param>
        /// <param name="sendTime"></param>
        
        public void addHistory_SysTalk(bool isSelf, ChatBoxContent content, string sendName, string sendTime)
        {
            string MessageStr = string.Empty;
            SysInfoType st = SysInfoType.OK;
            string typeStr = content.Text.Substring(0, content.Text.IndexOf("]")+1);
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
                    string fileContent = content.Text.Substring(content.Text.IndexOf("]") + 1);
                    FileClass file = JsonConvert.DeserializeObject<FileClass>(fileContent);
                    st = SysInfoType.Fail;
                    MessageStr = Common.File_Message(isSelf, file.SaveFileName,file.FileSize,file.IsSender);
                    break;
            }
            //string html = MessageStr;
            if (!string.IsNullOrEmpty(MessageStr))
            {
                comm.sendMessage_Sys(this.browser, MessageStr, st);
            }
        }
        */

        /// <summary>
        /// 纯文本消息
        /// </summary>
        /// <param name="isSelf"></param>
        /// <param name="html"></param>
        /// <param name="sendName"></param>
        /// <param name="sendTime"></param>
        //public void addHistory_Talk(bool isSelf, string  html, string sendName, string sendTime)
        //{
        
        //    #region 替换换行符
        //    html = html.Replace("\r\n", "<br //>").Replace("\n", "<br //>");
        //    #endregion

        //    string fontFamily = this.chatBoxSend.Font.FontFamily.Name;
        //    string fontSize = this.chatBoxSend.Font.Size + "px";
        //    string imgHead = TempFile.retImgHeadByUserName(sendName);




        //    comm.sendMessage(this.browser,
        //        isSelf.ToString().ToLower(),
        //        html,
        //        sendName,
        //        sendTime,
        //        fontFamily,
        //        fontSize,
        //        imgHead);

        //    //comm.CallJS(this.browser, "add(" + isSelf.ToString().ToLower() + ",'"
        //    //    + content + "','"
        //    //    + sendName + "','"
        //    //    + sendTime + "','"
        //    //    + fontFamily + "','"
        //    //    + fontSize + "','"
        //    //    + imgHead + "')");

        //    //CefSharp.IFrame f = this.browser.GetBrowser().MainFrame;

        //    //f.ExecuteJavaScriptAsync("add("+isSelf.ToString().ToLower()+",'" 
        //    //    + content + "','"
        //    //    + sendName+"','"
        //    //    + sendTime+"','"
        //    //    + fontFamily + "','"
        //    //    + fontSize+"','"
        //    //    + imgHead + "')");
        //}
        #endregion


        #endregion



        #region 字体调整功能
        /// <summary>
        /// 显示字体对话框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ibtnFont_Click(object sender, EventArgs e)
        {
            this.fontDialog1.Font = this.chatBoxSend.Font;
            this.fontDialog1.Color = this.chatBoxSend.ForeColor;
            if (DialogResult.OK == this.fontDialog1.ShowDialog())
            {
                this.chatBoxSend.Font = this.fontDialog1.Font;
                this.chatBoxSend.ForeColor = this.fontDialog1.Color;

                SystemSettings.Singleton.FontColor = this.fontDialog1.Color;
                SystemSettings.Singleton.Font = this.fontDialog1.Font;
                SystemSettings.Singleton.Save();
            }
        }
        #endregion

       
        #region 语音功能模块
        //语言按钮的Click事件
        private void ibtnVoice_Click(object sender, EventArgs e)
        {
            RecordForm recordForm = new RecordForm();
            recordForm.recordOverSend += RecordForm_recordOverSend;
            recordForm.ShowDialog();
            string fileName = recordForm.UploadFileName;
            if (!string.IsNullOrEmpty(fileName))
            {
                this.chatClass.AppendVoiceContent(this.browser, MineID, fileName, DateTime.Now,this.MineID,this.FriendID,
                    this.chatBoxSend.Font.Name,this.chatBoxSend.Font.Size.ToString());

            }
            //if (xmppSendMessageBtnClick != null)
            //{
            //    ChatBoxContent content = new ChatBoxContent();
            //    content.Text = SysParams.Sys_Normal + content.Text;
            //    xmppSendMessageBtnClick(content, FriendID);
            //}


        }


        public delegate void SendLanguageHandle(string languageStr,string toFriend);
        public event SendLanguageHandle sendLanguage;
        private void RecordForm_recordOverSend(string recoedStr)
        {
            string languageStr = "^([^]+)(([^$]+))?$" + recoedStr;
            if (sendLanguage != null)
            {
                sendLanguage(languageStr,FriendID); 
             }
        }


        //#region 语言播放
        //MemoryStream soundStream;
        //WaveDecoder decoder;
        //IAudioOutput output;
        ///// <summary>
        ///// 解码收到的语言
        ///// </summary>
        ///// <param name="outPutCodeData"></param>
        //private void decodeSoundStream(string outPutCodeData)
        //{
        //    byte[] raw = Convert.FromBase64String(outPutCodeData);

        //    this.soundStream = new MemoryStream(raw);
        //}

        ///// <summary>
        ///// 播放收到的语言
        ///// </summary>
        //private void playSoundStream()
        //{
        //    this.decoder = new WaveDecoder(this.soundStream);

        //    this.output = new AudioOutputDevice(this.Handle, decoder.SampleRate, decoder.Channels);

        //    this.output.FramePlayingStarted += Sound_FramePlayingStarted;
        //    this.output.NewFrameRequested += Sound_NewFrameRequested;
        //    this.output.Stopped += Sound_Stopped;

        //    this.output.Play();
        //}

        ///// <summary>
        ///// 停止播放语言
        ///// </summary>
        //private void stopPlaySoundStream()
        //{
        //    //TODO:停止播放

        //    this.output.SignalToStop();
        //    this.output.WaitForStop();
        //}

        //void Sound_FramePlayingStarted(object sender, PlayFrameEventArgs e)
        //{
        //    if (e.FrameIndex + e.Count < decoder.Frames)
        //    {
        //        int previous = decoder.Position;
        //        this.decoder.Seek(e.FrameIndex);

        //        Signal s = decoder.Decode(e.Count);
        //        this.decoder.Seek(previous);
        //    }
        //}

        //void Sound_NewFrameRequested(object sender, NewFrameRequestedEventArgs e)
        //{
        //    e.FrameIndex = decoder.Position;
        //    Signal signal = decoder.Decode(e.Frames);

        //    if (signal == null)
        //    {
        //        //TODO:播放完毕

        //        e.Stop = true;
        //        return;
        //    }

        //    e.Frames = signal.Length;
        //    signal.CopyTo(e.Buffer);
        //}

        //void Sound_Stopped(object sender, EventArgs e)
        //{
        //    //TODO:播放停止时触发
        //}
        //#endregion
        #endregion



        #region 图片发送
        private void ibtnPicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog openImageDlg = new OpenFileDialog();
            openImageDlg.Filter = "所有图片所有图片(*.bmp,*.gif,*.jpg,*.png)|*.bmp;*.gif;*.jpg;*.png";
            openImageDlg.Title = "选择图片";
            if (openImageDlg.ShowDialog() == DialogResult.OK)
            {
                string fileName = openImageDlg.FileName;
                if (null == fileName || fileName.Trim().Length == 0)
                    return;
                try
                {
                    //bmp = new Bitmap(fileName);  

                    //Bitmap bitmap = form.CurrentImage;
                    // bmp = this.CurrentImage;

                    #region  生成缩略图 暂时注释

                    ImageType imgType = Common.CheckImageType(fileName);
                    //using (Image img = Image.FromFile(fileName))
                    //{
                    //Image thumImage = Common.GetThumbnailImageKeepRatio(img);
                    //thumImage.Tag = Common.SaveThumbnailImageToTempFile(thumImage, imgType);
                         Image img = Image.FromFile(fileName);
                         img.Tag = fileName;
                         switch (imgType)
                         {
                             case ImageType.GIF:
                                 break;
                             default:
                                 break;
                         }
                         this.chatBoxSend.InsertImage(img);
                         this.chatBoxSend.Focus();
                         this.chatBoxSend.ScrollToCaret();
                     //}
                   
                    #endregion
                   
                }
                catch (Exception exc)
                {
                    MessageBox.Show("图片插入失败。" + exc.Message, "提示",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {


                }
            }
        }

        #endregion

        #region Vibration 震动


        //震动
        private void toolVibration_Click(object sender, EventArgs e)
        {
            ChatBoxContent cbx = new ChatBoxContent(SysParams.Sys_VibrationMessage,SysParams.sysFont,SysParams.sysFontColor);
            

            if (xmppSendMessageBtnClick != null)
            {
                xmppSendMessageBtnClick(cbx, FriendID);
            }
            //cbx.Text = Common.VibrationMessage(true, MineID); //"您发送了一个抖动提醒。\n";
            AppendChatBoxContent(true,MineID, DateTime.Now, cbx, SysParams.sysFontColor, false);
            //Common.Vibration(this);
            Common.VibrationThread(this);

        }
        #endregion

        #region 截屏
        /// <summary>
        /// 截屏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ibtnScreenshots_Click(object sender, EventArgs e)
        {
            Common.ScreenShot(this.chatBoxSend);
        }

        #endregion

        #region 手写板
        private void btnPaint_Click(object sender, EventArgs e)
        {
            PaintForm2 pf = new PaintForm2();
            if (pf.ShowDialog() == DialogResult.OK)
            {
                Image imgtmp = pf.CurrentImage;
                this.chatBoxSend.InsertImage(imgtmp);
                this.chatBoxSend.Focus();
                this.chatBoxSend.ScrollToCaret();
            }
        }
        #endregion

        #region 文件发送功能
        /// <summary>
        /// 发送文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void XmppSendFileOrFolderBtnClickHandle(string fileOrFolderPath
            , long fileOrFolderSize, string friendIDAndStaue, string fileOrFolderName);
        /// <summary>
        /// OpenFire 文件发送
        /// 实质方法
        /// </summary>
        public event XmppSendFileOrFolderBtnClickHandle xmppSendFileOrFolderBtnClick;

        /// <summary>
        /// 发送功能键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ibtFile_Click(object sender, EventArgs e)
        {


            string fileOrFolderPath = null;
            string fileOrFolderName = "";
            long fileOrFolderSize;
            if (ofDialog.ShowDialog() == DialogResult.OK)
            {
                AddProcess_Send(ofDialog.FileNames,ofDialog.SafeFileNames);
                return;
            }
            return;

           ESBasic.Helpers.FileHelper.GetFolderToOpen(true);

           fileOrFolderPath = ESBasic.Helpers.FileHelper.GetFileToOpen("请选择要发送的文件");

            if (!File.Exists(fileOrFolderPath))
            {
                return;
            }
            fileOrFolderName = ESBasic.Helpers.FileHelper.GetFileNameNoPath(fileOrFolderPath);
            fileOrFolderSize = Convert.ToInt32(ESBasic.Helpers.FileHelper.GetFileSize(fileOrFolderPath));

            if (xmppSendFileOrFolderBtnClick != null)
            {
                xmppSendFileOrFolderBtnClick(fileOrFolderPath, fileOrFolderSize, FriendIDAndStaue, fileOrFolderName);
            }

        }

        #region 接收增加显示控件方法
        /// <summary>
        /// 接收文件 系统展示
        /// </summary>
        /// <param name="file"></param>
        public void AddProcess_Receive(FileClass file,bool isLoad=false)
        {
            if (file != null)
            {
                file.IsSender = false;
                if (file != null && !string.IsNullOrEmpty(file.SaveFileName))
                {
                    AddProcessCotrol(file,isLoad);
                }
            }
           
        }


        #endregion


        #region 发送 增加控件提示方法

        /// <summary>
        /// 发送方 获取 接收方的保存路径
        /// </summary>
        /// <param name="file"></param>
        public void setFileReceivePath(FileClass file)
        {
            if (skinPanel_right.Controls.Count > 0 && file != null)
            {
                foreach (Control item in skinPanel_right.Controls)
                {
                    if (item is ItemUploadFile && ((ItemUploadFile)item).File.FileId == file.FileId)
                    {
                        ((ItemUploadFile)item).File.ReceivePath = file.ReceivePath;
                    }
                        
                }
            }
        }

        private void AddProcess_Send(string[] FilePathList,string[] FileSaveNameList)
        {
            if (FilePathList != null && FilePathList.Length > 0)
            {
                for (int i = 0; i < FilePathList.Length; i++)
                {
                    if (this.skinPanel_right.Controls.Count >= SysParams.Limit_StrLength_FileName)
                    {
                        MessageBox.Show("每次上传数量为"+SysParams.Limit_StrLength_FileName);
                        break;
                    }
                    string filePath = FilePathList[i];

                    string saveName = string.Empty;
                    if (FileSaveNameList == null)
                    {
                        saveName = filePath.Substring(filePath.LastIndexOf(@"\") + 1);
                    }
                    else {
                        saveName = FileSaveNameList[i];
                    }
                        //FileSaveNameList[i];
                    long fileSize = 0;
                    if (File.Exists(filePath))
                    {
                        if (Common.IsFileInUse(filePath))
                        {

                            MessageBox.Show("文件" + saveName + "正在被占用无法上传。");
                            continue;
                        }


                        FileInfo f = new FileInfo(filePath);
                       
                        fileSize = f.Length;
                        f = null;

                        #region 控件建立区域
                        FileClass file = new FileClass();
                        file.SaveFileName = saveName;
                        file.FileName = filePath;
                        file.FileSize = fileSize;
                        file.FolderName = filePath.Substring(0, filePath.LastIndexOf(@"\") - 1);
                        file.SendTime = DateTime.Now.ToString("yyyy-MM-dd");
                        file.IsSender = true;
                        AddProcessCotrol(file,false);
                        #endregion

                        #region 发送 文件发送 系统标准提示信息
                        //1 发送系统提示信息
                        string sendContent = SysParams.Sys_SendFileMessage + JsonConvert.SerializeObject(file);
                        ChatBoxContent cbx = new ChatBoxContent(sendContent, SysParams.sysFont, SysParams.sysFontColor);
                        this.OPENFIRE_SendMessage(cbx, this.FriendID);
                        //if (xmppSendMessageBtnClick != null)
                        //{
                        //    xmppSendMessageBtnClick(cbx, FriendID);
                        //}
                        #endregion
                    }



                }
                GC.Collect();
            }
        }


        
        #endregion

        #region 文件上传控件操作区域 增加 删除

        /// <summary>
        /// 调整任务区位置
        /// </summary>
        public void FileUploadPanelAdjust(bool isOnload=false)
        {
            if (this.skinPanel_right.Controls.Count > 0)
            {
                //this.skinPanel_right.Visible = true;
                if (this.WindowState != FormWindowState.Maximized)
                {
                    this.Width = SysParams.chatWidth_Max; 
                }
                this.panelChat.Width = this.Width - this.skinPanel_right.Width-1;
                this.pnlbutton.Width = this.Width - this.skinPanel_right.Width-1;
                this.pnlSend.Width = this.Width - this.skinPanel_right.Width-1;
                this.pnlSendContent.Width = this.Width - this.skinPanel_right.Width-1;
                this.skinPanel_right.Location = new Point(this.Width - this.skinPanel_right.Width ,
                this.skinPanel_right.Location.Y);

            }
            else if (this.skinPanel_right.Controls.Count == 0)
            {
                //this.skinPanel_right.Visible = false;

                if (this.WindowState != FormWindowState.Maximized)
                {
                    this.Width = SysParams.chatWidth_Normal;
                }
                this.panelChat.Width = this.Width;
                this.pnlbutton.Width = this.Width;
                this.pnlSend.Width = this.Width;
                this.pnlSendContent.Width = this.Width;
                this.skinPanel_right.Location = new Point(this.Width,
                    this.skinPanel_right.Location.Y);

            }
        }


        /// <summary>
        /// 追加控件
        /// </summary>
        /// <param name="file"></param>
        private void AddProcessCotrol(FileClass file,bool isLoad)
        {
            int width = 249;
            int height = 55;
            file.IcoUrl = Common.retIcoHtmlPathByFileName(file.FileName);
            ItemUploadFile uf = new ItemUploadFile(file.FileName);//D:\Project\NetDNALims\IMClient\htm\talkHistory.html
            uf.Name = file.FileId;
            uf.Width = width;
            uf.Height = height;
            uf.FileName = uf.Info = file.SaveFileName;
            uf.File = file;
            if (file.IsContinue&&file.SendFileType==SendFileType.OffLine)
            {
                FileInfo f = new FileInfo(file.ReceivePath);
                if (f.Exists)
                {
                    long currentSize = new FileInfo(file.ReceivePath).Length;
                    uf.value = (int)((currentSize * 100) / file.FileSize);//追加进度
                }
            }
            if (uf.Info.Length > SysParams.Limit_StrLength_FileName)
            {
                uf.Info = uf.Info.Substring(0, SysParams.Limit_StrLength_FileName) + "...  ";
            }
            uf.Info_2 = Common.FormatFileSize(file.FileSize);
            switch (file.IsSender)
            {
                case true:
                    uf.Visible_Left = false;
                    uf.centerButton_text = "转离线发送";
                    //离线事件 
                    uf.cfunc = centerSend_Offline;
                    uf.centerParams = uf;
                    break;
                case false:
                    uf.lefButton_text = "接收";
                    uf.centerButton_text = "另存为";
                    uf.lfunc = leftOpenFile;//本地默认路径
                    uf.leftParams = uf;
                    uf.cfunc = centerOpenDir;//另存为
                    uf.centerParams = uf; 
                    break;
            }


            uf.rightButton_text = "取消";
            uf.rfunc = rightCancel;
            uf.rightParams = uf;

            int count = skinPanel_right.Controls.Count;
            int OffSet_X = (skinPanel_right.Width - uf.Width) / 2;
            uf.Location = new Point(OffSet_X, count * (height + 5) + 15);
            this.Invoke(new Action(() =>
            {
                this.skinPanel_right.Controls.Add(uf);
                if (!isLoad)
                {
                    FileUploadPanelAdjust();
                }
            }));
        }

        /// <summary>
        /// 删除 加载的控件
        /// </summary>
        /// <param name="file"></param>
        public void RemoveProgressBar(FileClass file,bool isLoad=false)
        {
            if (file != null && !string.IsNullOrEmpty(file.FileId))
            {
                int index = 0;
                while (index < this.skinPanel_right.Controls.Count)
                {
                    if (this.skinPanel_right.Controls[index] is ItemUploadFile)
                    {

                        ItemUploadFile item = this.skinPanel_right.Controls[index] as ItemUploadFile;

                        if (item.File != null && item.File.FileId == file.FileId)
                        {
                            Point location = item.Location;
                            Control parent = this.skinPanel_right;
                            this.Invoke(new Action(() => {
                                this.skinPanel_right.Controls.RemoveAt(index);
                                for (int i = index; i < parent.Controls.Count; i++)
                                {
                                    Point tmplocation = location;
                                    location = parent.Controls[i].Location;
                                    parent.Controls[i].Location = tmplocation;
                                }
                            }));


                            break;
                        }
                    }
                    index++;
                }
                if (this.skinPanel_right.Controls.Count == 0 && !isLoad)
                {
                     this.Invoke(new Action(() => { FileUploadPanelAdjust(); }));
               }
            }
        }


        #endregion

        #region 接收方显示方法
        /// <summary>
        /// 接收方 发送接收文件请求
        /// </summary>
        /// <param name="fileParams"></param>
        public void leftOpenFile(object fileParams)
        {
            ItemUploadFile item = fileParams as ItemUploadFile;
            FileClass file = item.File;
            file.Operation = OperationType.Download;
            if (file != null)
            {
                string tmpDir = AppDomain.CurrentDomain.BaseDirectory + SysParams.Sys_tmpFile;
                runDownloadMission(tmpDir,item, file, this.fileController);
             

            }
        }

      

        /// <summary>
        /// 接收方 发送接收文件请求
        /// </summary>
        /// <param name="o"></param>
        public void centerOpenDir(object fileParams)
        {
            ItemUploadFile item = fileParams as ItemUploadFile;
            FileClass file = item.File;
            file.Operation = OperationType.Download;
            if (file != null &&SavefoldeDialog.ShowDialog()==DialogResult.OK)
            {
                item.lefButton_text = string.Empty;
                item.Visible_Left = false;
                item.centerButton_text = string.Empty;
                item.Visible_Center = false;
                string tmpDir = SavefoldeDialog.SelectedPath + "\\";
                runDownloadMission(tmpDir,item, file,this.fileController);
            }
        }

        /// <summary>
        /// 下载任务
        /// </summary>
        /// <param name="savePathDir">本地保存路径</param>
        /// <param name="item">任务控件</param>
        /// <param name="file">文件类</param>
        /// <param name="fileController">文件下载上传控制类</param>
        private void runDownloadMission(string savePathDir, ItemUploadFile item, FileClass file,FileUpLoadController fileController)
        {
            string tmpDir = savePathDir;
            if (!Directory.Exists(tmpDir))
            {
                Directory.CreateDirectory(tmpDir);
            }

            //判断文件是否存在
            file.ReceiveSaveFileName = Common.CheckAndChangeFileName(file.SaveFileName, tmpDir);

            //file.ReceiveSaveFileName = file.ReceiveSaveFileName + SysParams.tmpDownLoadName;

            if (file.SendFileType == SendFileType.OnLine)//在线
            {
                SendFile_Receive(file, tmpDir);
            }
            else if (file.SendFileType == SendFileType.OffLine)//离线
            {
                file.ReceiveDir = tmpDir;
                file.ReceivePath = file.ReceiveDir + file.ReceiveSaveFileName;
                fileController.dtMissionAdd(file, item, FriendID, MineID);
            }
        }



        /// <summary>
        /// 发送方 离线文件-文件上传
        /// </summary>
        /// <param name="o"></param>
        public void centerSend_Offline(object fileParams)
        {
            try
            {
                ItemUploadFile item = fileParams as ItemUploadFile;
                FileClass file = item.File;
                file.Operation = OperationType.UpLoad;
                file.SendFileType = SendFileType.OffLine;
                item.Visible_Center = false;
                file.IsSender = true;
                //if (!string.IsNullOrEmpty(file.SendId))//终止在线
                //{
                //    StaticClass.fm.Abort(file.SendId);
                //    file.SendId = string.Empty;
                //    item.value = 0;
                //    item.Invalidate();
                //}

                //取消在线任务
                SendCancelMessage(file);

                if (File.Exists(file.FileName) && !Common.IsFileInUse(file.FileName))
                {
                    //1 限制文件上传大小
                    if (file.FileSize <= SysParams.UPLoad_FileSzie)
                    {
                        //2 测试服务器状态
                        string ServerStatus = Common.retStatus_FileService();
                        if (ServerStatus == "OK")
                        {
                            //3 添加离线下载任务
                            fileController.dtMissionAdd(file, item, MineID, FriendID);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("离线发送失败，失败原因:" + ex.Message);
            }
           
        }



        /// <summary>
        /// 接收方-发送同意发送请求
        /// </summary>
        /// <param name="file">文件描述类</param>
        /// <param name="tmpDir">接收方保存目录</param>
        private void SendFile_Receive(FileClass file, string tmpDir)
        {
            file.ReceivePath = tmpDir + file.SaveFileName;
            string sendContent = SysParams.Sys_ReceiveFileMessage + JsonConvert.SerializeObject(file);
            ChatBoxContent cbx = new ChatBoxContent(sendContent, SysParams.sysFont, SysParams.sysFontColor);
            //界面显示取消信息
            this.OPENFIRE_SendMessage(cbx, this.FriendID);
        }

        /// <summary>
        /// 取消任务记录
        /// </summary>
        /// <param name="file"></param>
        public void SendCancelMessage(FileClass file)
        {

            if (!string.IsNullOrEmpty(file.SendId))//在线
            {
                bool isAbort = StaticClass.fm.Abort(file.SendId);
            }
            else//离线
            {
                this.fileController.Abort(file.FileId);
            }
            ChatBoxContent content = new ChatBoxContent();
            content.Text = SysParams.Sys_File_Cancel + JsonConvert.SerializeObject(file);

            //接收方 终止操作 20170401
            //this.addHistory_SysTalk(true, content, MineID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            this.chatClass.addHistory_SysTalk(this.browser, true, content.Text, MineID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            //提示对方
            string sendContent = SysParams.Sys_File_Cancel + JsonConvert.SerializeObject(file);
            ChatBoxContent cbx = new ChatBoxContent(sendContent, SysParams.sysFont, SysParams.sysFontColor);
            this.OPENFIRE_SendMessage(cbx, FriendID);
        }

        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="o"></param>
        public void rightCancel(object o)
        {
            ItemUploadFile iuf = o as ItemUploadFile;
            FileClass file = iuf.File;
            RemoveProgressBar(file);

            //界面显示取消信息
            SendCancelMessage(file);
            iuf.Dispose();
            GC.Collect();
        }



        /// <summary>
        /// 取消任务
        /// </summary>
        /// <param name="c"></param>
        public void Cancel_FileUpload(object c)
        {
            List<ItemUploadFile> itemlist = new List<ItemUploadFile>();
            if (c is ItemUploadFile)
            {
                itemlist.Add((ItemUploadFile)c);
            }
            else if (c is Panel)
            {
                foreach (Control citem in ((Control)c).Controls)
                {
                    if (citem is ItemUploadFile)
                    {
                        itemlist.Add((ItemUploadFile)citem);
                    }
                }
            }
            for (int i = 0; i < itemlist.Count; i++)
            {
                ItemUploadFile iuf = itemlist[i];
                FileClass file = iuf.File;
                if (!string.IsNullOrEmpty(file.SendId))//在线
                {
                    bool isAbort = StaticClass.fm.Abort(file.SendId);
                }
                //提示对方
                if (xmppSendMessageBtnClick != null)
                {
                    string sendContent = SysParams.Sys_File_Cancel + JsonConvert.SerializeObject(file);
                    ChatBoxContent cbx = new ChatBoxContent(sendContent, SysParams.sysFont, SysParams.sysFontColor);
                    this.OPENFIRE_SendMessage(cbx, this.FriendID);
                    //xmppSendMessageBtnClick(cbx, FriendID);
                }
                iuf.Dispose();
            }
        }

        /// <summary>
        ///  文件上传-传递消息
        /// </summary>
        /// <param name="file"></param>
        public void SendMessage_File(FileClass file)
        {

            string sendContent = SysParams.Sys_OffLine_SendFileMessage;// + JsonConvert.SerializeObject(file);
            if (!file.IsSender && file.Operation==OperationType.Download)
            {
                sendContent = SysParams.Sys_OffLine_Success;// + JsonConvert.SerializeObject(file);
            }
            sendContent += JsonConvert.SerializeObject(file);
            ChatBoxContent cbx = new ChatBoxContent(sendContent, SysParams.sysFont, SysParams.sysFontColor);
            this.OPENFIRE_SendMessage(cbx, FriendID);
        }

        public void AddResult_OffLine_SendFile(FileClass file)
        {
            string sendName = string.Empty; ;
            bool isSelf = true;
          
            //if (!string.IsNullOrEmpty(sendName) && sendName.Contains("@"))
            //{
                sendName = file.IsSender?MineID.Substring(0, MineID.IndexOf('@')):FriendID.Substring(0, FriendID.IndexOf('@'));
            //}
            if (file.IsSender)
            {
                this.chatClass.addHistory_File(browser, isSelf, "完成", sendName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                 file.IcoUrl, file.FileName, file.FileSize,
                 SysParams.File_OffLine_Send_Result, this.chatBoxSend.Font.Name, this.chatBoxSend.Font.Size.ToString());
            }
            else {
                this.chatClass.addHistory_File(browser, isSelf, "完成", sendName,
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), file.IcoUrl,
                file.ReceivePath, file.FileSize,
                string.Format(SysParams.File_Receive_Result, file.ReceivePath),
                this.chatBoxSend.Font.Name, this.chatBoxSend.Font.Size.ToString());
            }
        }

        #endregion


        #region 文件拖拽进入
        private void Control_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            }
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else e.Effect = DragDropEffects.None;
        }

        private void Control_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files != null && files.Length > 0)
                {
                    AddProcess_Send(files, null);
                }
            }
        }
        #endregion

        #region OpenFire 消息传递
        /// <summary>
        /// 客户端间传递消息
        /// </summary>
        /// <param name="content"></param>
        public void OPENFIRE_SendMessage(ChatBoxContent content,string FriendID)
        {
            if (xmppSendMessageBtnClick != null)
            {
                //content.Text = SysParams.Sys_Normal + content.Text;
                xmppSendMessageBtnClick(content, FriendID);
            }
        }

        /// <summary>
        /// OpenFile 文件传递
        /// </summary>
        /// <param name="file"></param>
        public void OPENFIRE_SendFile(FileClass file)
        {
            if (xmppSendFileOrFolderBtnClick != null && file != null)
            {
                //chengle@192.168.1.95 / MatriX

                //string ss = this.FriendID;//FriendIDAndStaue
               
                xmppSendFileOrFolderBtnClick(file.FileName, file.FileSize, FriendIDAndStaue, file.FileId + "," + this.FriendID);
            }
        }
    #endregion


     
        #endregion

        #region 快捷键 综合功能 复制黏贴 快捷发送 快捷发送
        /// <summary>
        /// 快捷键粘贴 仅支持文字
        /// 快捷发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chatBoxSend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                e.SuppressKeyPress = true;
                System.Windows.Forms.IDataObject iData = Clipboard.GetDataObject();
                if (iData.GetDataPresent(DataFormats.Text))
                {
                    chatBoxSend.Paste(DataFormats.GetFormat(DataFormats.Text));
                }
                //else if(iData.GetDataPresent(DataFormats.Bitmap))
                //{
                //    chatBoxSend.Paste(DataFormats.GetFormat(DataFormats.Bitmap));
                //}

            }
            else if (e.Control && e.KeyCode == Keys.Enter)
            {
                sendMessage();
            }
            else if (e.Control && e.Alt && e.KeyCode == Keys.A)
            {
                // sendMessage();
                Common.ScreenShot(this.chatBoxSend);
            }
        }






        #endregion

        private void ibtnVideo_Click(object sender, EventArgs e)
        {

        }

        private void ibtnOther_Click(object sender, EventArgs e)
        {

        }

        private void ChatFormForWeb_ResizeEnd(object sender, EventArgs e)
        {
              this.panelChat.Height = pnlbutton.Location.Y - this.panelChat.Location.Y;
               setHistoryFormSize();
            
        }

      
        private void ibtnHialogue_Click(object sender, EventArgs e)
        {

            Control[] items = this.Controls.Find("historyFrm", false);
            if (items.Length == 0)
            {
                HistoryForm f = new HistoryForm(this.MineID, this.FriendID, this);
                f.Name = "historyFrm";
                f.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                //pnlSend.Width = pnlSendContent.Width = panelChat.Width = pnlbutton.Width = this.Width - f.Width;
                f.TopLevel = false;
                this.Controls.Add(f);
                setHistoryFormSize();
                f.Show();
            }
            else {
                ((HistoryForm)items[0]).close();
            }
        }

        private void setHistoryFormSize()
        {
            Control[] items = this.Controls.Find("historyFrm", false);
            if (items.Length > 0)
            {
                HistoryForm f = (HistoryForm)items[0];
              
                f.Height = this.Height - this.panelChat.Location.Y;
                f.Location = new Point((this.WindowState == FormWindowState.Maximized ? this.Width / 2 : SysParams.chatWidth_Normal) + 1, this.panelChat.Location.Y);
                this.Width = this.WindowState == FormWindowState.Maximized ? this.Width : SysParams.chatWidth_Normal * 2; //this.Width * 2;
                f.Width = this.WindowState == FormWindowState.Maximized ? this.Width / 2 : (SysParams.chatWidth_Normal - 1);
                changeSize(this.Width, f.Width);
            }
        }

        public void changeSize(int mainWidth, int width2)
        {
            pnlSend.Width = pnlSendContent.Width = panelChat.Width = pnlbutton.Width = mainWidth - width2;
        }

        private void ChatFormForWeb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F12)
            {
                this.browser.ShowDevTools();
            }
        }

        private void ChatFormForWeb_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.chatBoxSend != null && this.chatBoxSend.GetContent().ForeignImageDictionary != null)
            {
                Dictionary<uint, Image> dict = this.chatBoxSend.GetContent().ForeignImageDictionary;
                foreach (var v in dict.Keys)
                {
                    try {
                        dict[v].Dispose();//释放 内存中的图片
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }

        /// <summary>
        /// 点击聊天窗体右下角的关闭按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ibtnCancel_Click(object sender, EventArgs e)
        {
            if (OpenChatClose != null)
            {
                OpenChatClose(FriendID);

            }

            if (this.skinPanel_right.Controls.Count > 0 &&
                MessageBox.Show("正在进行文件上传/接收工作，是否终止当前任务继续关闭?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Cancel_FileUpload(this.skinPanel_right);
            }
            fileController.Dispose();
            this.Close();
        }


        #region 最近联系人添加

        public delegate void SaveRecentLinkManDelegate();

        public void SaveRecentLinkMan()
        {
            try
            {
                if (!isSaveRecent)
                {
                    //1检查是否是最近联系人
                    bool isExist = false;
                    if (this.Friend != null)
                    {
                        isExist = sqliteHelper.existRecent(SysParams.LoginUser.GetJID(), this.FriendID);
                    }

                    if (!isExist)
                    {
                        //2 未记录为最近联系人-记录 
                        sqliteHelper.AddRecent(Guid.NewGuid().ToString().Replace("-", ""), SysParams.LoginUser.GetJID(),
                            SysParams.LoginUser.Name, this.FriendID, this.Friend.DisplayName);
                        isSaveRecent = true;
                    }
                    else {
                        isSaveRecent = true;
                    }

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("最近联系人添加失败。", "提示");
            }

        }
        #endregion
    }
}
