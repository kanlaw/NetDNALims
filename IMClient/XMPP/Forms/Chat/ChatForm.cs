using CCWin.SkinControl;
using ESPlus.Rapid;
using IMClient.Controls;
using IMClient.Controls.Base;
using IMClient.Native;
using IMClient.Properties;
using JustLib;
using JustLib.Controls;
using Matrix.Xmpp.Client;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;



namespace IMClient.XMPP.Forms
{
    public partial class ChatForm : FrameBase
    {

        
        private Font messageFont = new Font("微软雅黑", 9);
        private string MineID;
        public string FrendID;
        public string FriendIDAndStaue { get; set; }
        public ChatListSubItem Friend;
        private IRapidPassiveEngine rapidPassiveEngine;
        private string Title_FileTransfer = "文件";

        XmppClient client;

        public ChatForm(ChatListSubItem friend, string mineID , ChatBoxContent content)
        {
            InitializeComponent();   

            lblName.Text = friend.DisplayName;
            lblCompany.Text = friend.PersonalMsg;

            FriendIDAndStaue = ((GGUser)(friend.Tag)).JID.ToString();    
            Friend = friend;

            chatHeadState(friend);


            MineID = mineID;
            FrendID = friend.ID;
            
            //if (FrendID.Equals("yujunming192.168.1.95"))
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

        /// <summary>
        /// 最小化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ibtnMin_Click(object sender, EventArgs e)
        {
            this.ShowInTaskbar = true;
            this.WindowState = FormWindowState.Minimized;
            this.BringToFront();


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
                OpenChatClose(FrendID);
            }
            this.Close();
                   
        }

        /// <summary>
        /// 窗体加载阴影
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChatForm_Load(object sender, EventArgs e)
        {
           Win32Api.SetClassLong(this.Handle,Win32Api.GCL_STYLE, Win32Api.GetClassLong(this.Handle, Win32Api.GCL_STYLE) | Win32Api.CS_DropSHADOW);
        }

       
      

        private void ChatForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = Graphics.FromHwnd(base.Handle);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.AntiAlias;          
            if (this.WindowState == FormWindowState.Maximized)
            {
                imageButton2.Activepic = Resources.Reducing_down_over_02;
                imageButton2.Staticpic = Resources.Reducing_down_normal_02;
                imageButton2.Presspic = Resources.Reducing_down_down_02;      
            }
            else
            {
                imageButton2.Activepic = Resources.max_over_02;
                imageButton2.Staticpic = Resources.max_normal_02;
                imageButton2.Presspic = Resources.max_down_02;             

            }



        }

        public delegate void XmppSendMessageBtnClickHandle(ChatBoxContent sendcontent, string toFriend);
        public event XmppSendMessageBtnClickHandle xmppSendMessageBtnClick;
        
        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ibtnSend_Click(object sender, EventArgs e)
        {
            try
            {
                ChatBoxContent content = this.chatBoxSend.GetContent();

                if (xmppSendMessageBtnClick != null)
                {
                    xmppSendMessageBtnClick(content, FrendID);

                    //Matrix.Xmpp.Client.Message msg = new Matrix.Xmpp.Client.Message(FrendID, content.Text, "", "");
                    //this.client.Send(msg);
                }

                
                if (content.IsEmpty())
                {
                    return;
                }

               
                
                bool followingWords = false;
               
                
                this.AppendChatBoxContent(MineID, null, content, Color.SeaGreen, followingWords);
              
                //清空输入框
                this.chatBoxSend.Text = string.Empty;
                this.chatBoxSend.Focus();

              
            }
            catch (Exception ee)
            {
                this.AppendSysMessage("发送消息失败！" + ee.Message);
            }

        }

        private void AppendMessage(string userName, Color color, string msg)
        {
            DateTime showTime = DateTime.Now;
            this.chatBox_history.AppendRichText(string.Format("{0}  {1}\n", userName, showTime.ToLongTimeString()), new Font(this.messageFont, FontStyle.Regular), color,null, MineID, FrendID);
            this.chatBox_history.AppendText("    ");

            this.chatBox_history.AppendText(msg);
            this.chatBox_history.Select(this.chatBox_history.Text.Length, 0);
            this.chatBox_history.ScrollToCaret();
        }

        public void AppendSysMessage(string msg)
        {
            this.AppendMessage("系统", Color.Gray, msg);
            this.chatBox_history.AppendText("\n");
        }

        #region AppendMessage
        /// <summary>
        /// 发送消息传当前自己登陆的ID，接收信息传当前点击朋友的ID
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="originTime"></param>
        /// <param name="content"></param>
        /// <param name="color"></param>
        /// <param name="followingWords"></param>
        public void AppendChatBoxContent(string userID, DateTime? originTime, ChatBoxContent content, Color color, bool followingWords)
        {
            this.AppendChatBoxContent(userID, originTime, content, color, followingWords, originTime != null);
        }

        private void AppendChatBoxContent(string userID, DateTime? originTime, ChatBoxContent content, Color color, bool followingWords, bool offlineMessage)
        {



            string showTime = DateTime.Now.ToLongTimeString();
            if (!offlineMessage && originTime != null)
            {
                showTime = originTime.Value.ToString();
            }
            this.chatBox_history.AppendRichText(string.Format("{0}  {1}\n", userID, showTime), new Font(this.messageFont, FontStyle.Regular), color, userID,MineID, FrendID);
            if (originTime != null && offlineMessage)
            {
                this.chatBox_history.AppendText(string.Format("    [{0} 离线消息] ", originTime.Value.ToString()));
            }
            else
            {
                //this.chatBox_history.AppendText("    ");
            }

            this.chatBox_history.AppendChatBoxContent(content, userID, MineID, FrendID);
            this.chatBox_history.AppendText("\n");
            this.chatBox_history.Select(this.chatBox_history.Text.Length, 0);
            this.chatBox_history.ScrollToCaret();


        }



        #endregion

        private void ChatForm_Shown(object sender, EventArgs e)
        {
            this.chatBoxSend.Focus();
        }


      
     
        /// <summary>
        /// 快捷键粘贴
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chatBoxSend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                e.SuppressKeyPress = true;
                chatBoxSend.Paste(DataFormats.GetFormat(DataFormats.Text));
            }
        }

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


        /// <summary>
        /// 发送文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void XmppSendFileOrFolderBtnClickHandle(string fileOrFolderPath
            , long fileOrFolderSize, string friendIDAndStaue,string fileOrFolderName);
        public event XmppSendFileOrFolderBtnClickHandle xmppSendFileOrFolderBtnClick;
        private void ibtFile_Click(object sender, EventArgs e)
        {
            string fileOrFolderPath = null;
            string fileOrFolderName = "";
            long fileOrFolderSize;


            fileOrFolderPath = ESBasic.Helpers.FileHelper.GetFileToOpen("请选择要发送的文件");
          
            if (fileOrFolderPath == null)
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

        //语言按钮的Click事件
        private void ibtnVoice_Click(object sender, EventArgs e)
        {
            RecordForm recordForm = new RecordForm();
            recordForm.recordOverSend += RecordForm_recordOverSend;
            recordForm.ShowDialog();
            recordForm.BringToFront();
        }


        public delegate void SendLanguageHandle(string languageStr,string toFriend);
        public event SendLanguageHandle sendLanguage;
        private void RecordForm_recordOverSend(string recoedStr)
        {
            string languageStr = "^([^]+)(([^$]+))?$" + recoedStr;
            if (sendLanguage != null)
            {
                sendLanguage(languageStr,FrendID); 
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
    }
}
