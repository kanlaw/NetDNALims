using CCWin.SkinControl;
using DNANET.Data;
using ESBasic;
using IMClient.Controls;
using IMClient.Controls.Base;
using IMClient.Controls.Tools;
using IMClient.Logic;
using IMClient.Logic.Sql;
using IMClient.Properties;
using IMClient.XMPP;
using IMClient.XMPP.Forms;
using JustLib;
using JustLib.Controls;
using JustLib.UnitViews;
using Matrix.Xmpp.Client;
using Matrix.Xmpp.Roster;
using Model;
using NetDLims.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using mx = Matrix;

namespace IMClient
{
    public partial class IMForm : FrameBase
    {

        System.Action<mx.Jid, string> pAddSubscribe;
        private delegate void SetPos(FileTransferEventArgs e);//代理

        private SqlLiteHelper sqliteHelper = new SqlLiteHelper();

        public string UserUid { set; get; }

        public IMForm()
        {
            //Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();

            this.pAddSubscribe = this.AddSubscribe;
            MenuColorTable mct = new MenuColorTable();
            ToolStripRendererEx t = new ToolStripRendererEx(mct);
            cmsiAdd.Renderer = t;
          
        }


        RecentListBox recentListBox1 = new RecentListBox();
        UserListBox friendListBox1 =new UserListBox();
        UserListBox friendListBox2_Search = new UserListBox();
        IMType im_eu = IMType.recent;
        /// <summary>
        /// 查询条件
        /// </summary>
        private string search_val = string.Empty;
        //保存新闻数据对象
        [Serializable]
        public class NewsInfo
        {
            public string leftTop { get; set; }
            public string leftBottom { get; set; }
            public string rightTop { get; set; }
        }
        // Dictionary<string, ChatForm> openChatDic = new Dictionary<string, ChatForm>();
        Dictionary<string, ChatFormForWeb> openChatDic = new Dictionary<string, ChatFormForWeb>();


        // public ImageInfo UserInfoBar;
        // public UserListBox userListBox1;
        private void IMForm_Load(object sender, EventArgs e)
        {
            recentListBox1.Name = "recentListBox1";
            friendListBox1.Name = "friendListBox1";
            friendListBox2_Search.Name = "friendListBox2_Search";
            StaticClass.XMPPCrack();

            
            //ibtnSF.BringToFront();

           


        }

        /// <summary>
        /// 双击好友
        /// </summary>
        /// <param name="friend"></param>
        private void friendListBox1_UserDoubleClicked(ChatListSubItem friend)
        {
            judgeOpenform(friend);
           // judgeOpenform2(friend);
        }


      
        /// <summary>
        /// 判断有哪些聊天chatform已经打开过
        /// </summary>
        /// <param name="chatForm"></param>
        /// <param name="friend"></param>
        private void judgeOpenform(ChatListSubItem friend)
        {
            ChatFormForWeb chatForm = null;
            //if (openChatDic.Count != 0)
            //{

                if (openChatDic.ContainsKey(friend.ID) && !openChatDic[friend.ID].IsDisposed)
                {
                    chatForm = openChatDic[friend.ID];
                    if (chatForm.Visible == false)
                    {
                        chatForm.Visible = true;
                        chatForm.BringToFront();
                        chatForm.Show();
                        //chatForm.Focus();
                    }
                    else
                    {
                        //chatForm.BringToFront();
                    }
                    
                    //chatForm.Close();

                }
                else
                {
                    chatForm = new ChatFormForWeb(friend, mySelfID, null);
                    openChatDic[friend.ID]=chatForm;
                    chatForm.OpenChatClose += Chat_OpenChatClose;
                    chatForm.xmppSendMessageBtnClick += Chat_xmppSendMessageBtnClick;
                    chatForm.xmppSendFileOrFolderBtnClick += ChatForm_xmppSendFileOrFolderBtnClick;
                    chatForm.sendLanguage += ChatForm_sendLanguage;
                    //chatForm.BringToFront();
                    chatForm.Show();
                    //chatForm.Focus();
                }
            }


        /// <summary>
        /// 聊天Form发送语言
        /// </summary>
        /// <param name="languageStr"></param>
        private void ChatForm_sendLanguage(string languageStr, string toFriend)
        {
            Matrix.Xmpp.Client.Message msg = new Matrix.Xmpp.Client.Message(toFriend, languageStr, "", "");
            this.client.Send(msg);
        }

       

        /// <summary>
        /// 代理的聊天chatform关闭的方法
        /// </summary>
        /// <param name="chatform"></param>
        private void Chat_OpenChatClose(string frendID)
        {
            openChatDic.Remove(frendID);
        }

        List<string> userGroups;
        Dictionary<string, GGUser> allFriendDic;
        //List<GGUser> allFriendList;
        Dictionary<string, List<GGUser>> friendDic;


        UserServiceClient userServerClient;

        XmppClient client;
        PresenceManager pm;
        FileTransferManager fm;
        string m_name;

        string mySelfID;

        public delegate void IsLoginSuccessBlockHandle(bool isLoginSucc,string userName,byte[] userPhoto);
        public event IsLoginSuccessBlockHandle isLoginSuccessBlock;
        //判断用户登录XMPP
        public void adjustUserIsLoginSuccess(string userName,string passWord,string domain,string server)
        {
            userGroups = new List<string>();
            SysParams.AllFriendList = new List<GGUser>();
            allFriendDic = new Dictionary<string, GGUser>();
            friendDic = new Dictionary<string, List<GGUser>>();
            //SysParams.AllFriendList = allFriendList;
           
            XmppClient c = StaticClass.InitXmppClient(userName, passWord,domain ,server);

            c.OnLogin += C_OnLogin;
            c.OnReceiveXml += XmppClientOnReceiveXml;
            c.OnSendXml += new EventHandler<mx.TextEventArgs>(XmppClientOnSendXml);

            c.AutoRoster = true;
            c.OnMessage += XMPP_OnMessage;
            c.OnRosterStart += XMPP_OnRosterStart;
            c.OnRosterItem += XMPP_OnRosterItem;
            c.OnRosterEnd += XMPP_OnRosterEnd;
            
         
            c.UseSso = true;
            c.OnBind += C_OnBind;
            c.OnBindError += C_OnBindError;
          
            this.fm = StaticClass.fm;
            this.fm.XmppClient = c;

            this.fm.OnFile += XMPP_OnFile;
            this.fm.OnStart += Fm_OnStart;
            this.fm.OnError += Fm_OnError;
            this.fm.OnEnd += Fm_OnEnd;
            this.fm.OnProgress += Fm_OnProgress;
           

            this.pm = StaticClass.pm;
            this.pm.OnSubscribe += XMPP_OnSubscribe;
            this.pm.OnUnavailablePresence += Pm_OnUnavailablePresence;
            this.pm.OnSubscribed += Pm_OnSubscribed;
            this.pm.OnUnsubscribed += Pm_OnUnsubscribed;
            this.pm.OnAvailablePresence += Pm_OnAvailablePresence;
            this.pm.OnInvisiblePresence += Pm_OnInvisiblePresence;
            this.pm.OnPresenceError += Pm_OnPresenceError;



            c.Open();

            //TODO用户登录后把自己的状态发给服务器
            //None = -1 可用,Away = 0 离开,Chat = 1 可聊天,DoNotDisturb = 2 用户忙碌, ExtendedAway = 3 离开一段时间
            c.SendPresence(Matrix.Xmpp.Show.None);

            this.client = c;

            this.m_name = userName;
           




        }



        //openFireClient成功触发方法
        private void C_OnLogin(object sender, mx.EventArgs e)
        {
            UserServiceClient c = new UserServiceClient();
            c.Open();
            this.userServerClient = c;

            //登录验证xmpp成功之后，回调主窗口方法
            if (isLoginSuccessBlock != null)
            {
                //c.FindUserInformation(false,"");
                //UserInformation info = userServerClient.FindUserInformation(new string[e.]);
                isLoginSuccessBlock(true, this.m_name, null);
            }

        }

        private void C_OnBindError(object sender, IqEventArgs e)
        {
            
        }

        private void C_OnBind(object sender, mx.JidEventArgs e)
        {
            //MessageBox.Show(e.Jid.Bare);
            mySelfID = e.Jid.Bare;

        }

        private void Pm_OnPresenceError(object sender, PresenceEventArgs e)
        {
            
        }

        private void Pm_OnInvisiblePresence(object sender, PresenceEventArgs e)
        {
            
        }

        //TODO:好友状态回调
        private void Pm_OnAvailablePresence(object sender, PresenceEventArgs e)
        {
            //< presence from = "yuxh@192.168.1.95/MatriX" to = "yujm@192.168.1.95/MatriX" xmlns = "jabber:client" >
            //    < show > dnd </ show >
            //        < status > Login </ status >
            //        < priority > 0 </ priority >
            //</ presence >
            

            Matrix.Xmpp.Show show = e.Presence.Show;
            Matrix.Jid from = e.Presence.From;
            string userName = from.User;
            #if DEBUG
            MessageBox.Show(string.Format(from + show));
            #endif

            switch (show)
            {
                case Matrix.Xmpp.Show.None:
                    {
                        adjustFriendStatus(userName,JustLib.UserStatus.Online);
                        chatformHeadStatus(from.Bare);
                    }
                    break;
                case Matrix.Xmpp.Show.Away:
                    {
                        adjustFriendStatus(userName, JustLib.UserStatus.Away);
                        chatformHeadStatus(from.Bare);
                    }
                    break;
                case Matrix.Xmpp.Show.Chat:
                    {
                        adjustFriendStatus(userName, JustLib.UserStatus.Online);
                        chatformHeadStatus(from.Bare);
                    }
                    break;
                case Matrix.Xmpp.Show.DoNotDisturb:
                    {
                        adjustFriendStatus(userName, JustLib.UserStatus.DontDisturb);
                        chatformHeadStatus(from.Bare);
                    }
                    break;
                case Matrix.Xmpp.Show.ExtendedAway:
                    {
                        adjustFriendStatus(userName, JustLib.UserStatus.OffLine);
                        chatformHeadStatus(from.Bare);
                    }
                    break;
                default:
                    break;
            }
            FrienddictionaryChange(from);

        }

        //TODO:判断好友的状态
        private void adjustFriendStatus(string friendName,JustLib.UserStatus userStatus)
        {
            foreach (KeyValuePair<string, List<GGUser>> userDic in friendDic)
            {

                foreach (GGUser user in userDic.Value)
                {
                    if (user.JID.User == friendName)
                    {
                        user.UserStatus = userStatus;
                        if (friendListBox1 != null)
                        {
                            friendListBox1.UserStatusChanged(user, userDic.Key);
                        }
                        if (recentListBox1 != null)
                        {
                            recentListBox1.UserStatusChanged(user);
                        }

                    }
                }

            }
        }

        /// <summary>
        /// 用户上线时更改用户jid的resource（文件传输使用）
        /// </summary>
        /// <param name="from"></param>
        void FrienddictionaryChange(Matrix.Jid from)
        {


            foreach (KeyValuePair<string, List<GGUser>> userDic in friendDic)
            {
                foreach (GGUser user in userDic.Value)
                {
                    if (user.JID.Bare == from.Bare)
                    {
                       
                        if (friendListBox1 != null)
                        {
                            friendListBox1.UserInfoChanged(from.Bare, user);
                            if (openChatDic.ContainsKey(from.Bare))
                            {
                                openChatDic[from.Bare].FriendIDAndStaue = from.ToString();
                            }
                        }
                        else
                        {
                            user.JID = from;
                        }

                    }
                }

            }




        }

        //TODO:拒绝好友请求
        private void Pm_OnUnsubscribed(object sender, PresenceEventArgs e)
        {
            string from = e.Presence.From;
            FrmMsg.Show(string.Format("{0}拒绝了你的好友请求",from), "提示");
        }

        //TODO:同意好友请求
        private void Pm_OnSubscribed(object sender, PresenceEventArgs e)
        {
            mx.Jid from = e.Presence.From;
            StaticClass.rm.Add(from);
        }

        /// <summary>
        /// 发送文件
        /// </summary>
        /// <param name="fileOrFolderPath">文件保存路径</param>
        /// <param name="fileOrFolderSize"></param>
        /// <param name="friendID"></param>
        /// <param name="description">情况描述</param>
        private void ChatForm_xmppSendFileOrFolderBtnClick(string fileOrFolderPath, long fileOrFolderSize, string friendID,string description)
        {

            //Matrix.Xmpp.Client.FileTransferManager ma = new Matrix.Xmpp.Client.FileTransferManager();
            //ma.XmppClient = this.client;
            //this.fm.OnFile 67e3cea9-4534-4388-9c06-eecc4ab64047

            try
            {
                string[] paramsStr = description.Split(',');
                string fileId = paramsStr[0];
                string friendId = paramsStr[1];
                // 获取fileclass 类
                ChatFormForWeb chat = openChatDic[friendId];
                ItemUploadFile item = (ItemUploadFile)chat.RightSendPanel.Controls[fileId];
                description = fileId + "," + this.mySelfID;
                string tmp_friendJid = friendID;
                bool isresult = true;
                if (tmp_friendJid.Contains("/"))
                {
                    string val = tmp_friendJid.Substring(tmp_friendJid.IndexOf("/") + 1);
                    if (val != "MatriX")
                    {
                        isresult = false;
                    }
                }
                else
                {
                    isresult = false;
                }
                if (!isresult)
                {
                    tmp_friendJid += "/MatriX";
                }
                string sendId = this.fm.Send(tmp_friendJid, fileOrFolderPath, description);
                item.File.SendId = sendId;
            }
            catch (Exception ex)
            {

                MessageBox.Show("文件发送异常："+ex.Message,"提示");
            }
            
        }

        //TODO:发送消息
        private void Chat_xmppSendMessageBtnClick(ChatBoxContent sendcontent, string toFriend)
        {
            string sendAll = sendcontent.Text;
            #region 消息组合
            //string sendStr = sendcontent.Text;
            //Dictionary<int, string> sendDir = new Dictionary<int, string>();
            //sendDir[-1] = sendStr;
            //if (sendcontent.ForeignImageDictionary != null && sendcontent.ForeignImageDictionary.Keys.Count > 0)
            //{
            //    foreach (uint key in sendcontent.ForeignImageDictionary.Keys)
            //    {
            //        //using (MemoryStream m = new MemoryStream())
            //        //{
            //            MemoryStream m = new MemoryStream();
            //            Image img = sendcontent.ForeignImageDictionary[key];
            //            img.Save(m, System.Drawing.Imaging.ImageFormat.Jpeg);
            //            byte[] b2 = new byte[m.Length];
            //            m.Seek(0, SeekOrigin.Begin);
            //            m.Read(b2, 0, b2.Length);
            //            sendDir[(int)key]  = Convert.ToBase64String(b2);
            //        //}
                    
            //    }
            //}
            //string sendAll = JsonConvert.SerializeObject(sendDir);
            #endregion

            //Matrix.Xmpp.Client.Message msg = new Matrix.Xmpp.Client.Message(toFriend, sendcontent.Text, "", "");

            Matrix.Xmpp.Client.Message msg = new Matrix.Xmpp.Client.Message(toFriend, sendAll, "", "");
            //msg.XHtml.Add(
            this.client.Send(msg);
        }

        //TODO:收到消息时触发
        /// <summary>
        /// 收到消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XMPP_OnMessage(object sender, MessageEventArgs e)
        {
            //对方正在输入也会产生Message，只有具有body的Message才是聊天消息
          
            if (e.Message.HasTag("body") && e.Message.Type!=mx.Xmpp.MessageType.Error)
            {
                if (e.Message.Type == mx.Xmpp.MessageType.GroupChat)//网络会议室
                {
                    
                }
                else //点对点聊天
                {

                    string type = e.Message.Body.Substring(0, e.Message.Body.IndexOf("]") + 1);
                    switch (type)
                    {
                        case SysParams.Sys_Meeting_Invite:
                            if (StaticClass.getStandardJid(e.Message.From) != SysParams.LoginUser.GetJID())
                            {
                                string content = e.Message.Body.Substring(e.Message.Body.IndexOf("]") + 1);
                                Meeting meeting = JsonConvert.DeserializeObject<Meeting>(content);
                                if (meeting != null)
                                {
                                    if (MessageBox.Show("是否同意来自" + meeting.RoomName + "聊天室的邀请？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                    {
                                        bool isEmcee = meeting.Emcee == SysParams.LoginUser.UID ? true : false;
                                        MeetingForm mf = new MeetingForm(meeting.MeetingGuid, isEmcee);
                                        mf.ShowDialog();
                                        //this.Close();
                                    }
                                }
                            }
                            return;
                    }
                    #region
                    if (e.Message.Body.Contains("^([^]+)(([^$]+))?$"))
                    {
                        //TODO:语言字符串
                        int a = "^([^]+)(([^$]+))?$".Length;
                        string outPutCode = e.Message.Body.Substring(0, a);

                    }
                    else
                    {
                        if (e.Message.Type == mx.Xmpp.MessageType.GroupChat)//会议室
                        {

                        }
                        else //普通聊条 点对点
                        {
                            //Match mFrom = Regex.Match(e.Message.From, @"^([^\/]+)(\/([^$]+))?$", RegexOptions.IgnoreCase);

                            //Matrix.Jid useJid = new Matrix.Jid(e.Message.From);
                            //string from = useJid.Bare;

                            //获取标准的JID
                            string from = StaticClass.getStandardJid(e.Message.From);
                            string messageStr = e.Message.Body;
                            ChatBoxContent chatcontent = new ChatBoxContent();
                            chatcontent.Text = messageStr;
                            string messageStrFlag = SysParams.retSysMessage(messageStr);
                            if (messageStrFlag == SysParams.Sys_AddFriendMessage)
                            {
                                AddUserForm af = new AddUserForm(from, messageStr);
                                if (af.ShowDialog() == DialogResult.Yes)
                                {
                                    //this.pm.ApproveSubscriptionRequest(from);
                                    this.pm.Subscribe(from);
                                }
                                return;
                            }
                            //bool isSysMessage = SysParams.isSysInfo(messageStr);

                            #region 使用本地图片方式

                            //string messageStr = e.Message.Body;
                            //Dictionary<int, string> dict = JsonConvert.DeserializeObject<Dictionary<int, string>>(messageStr);
                            //ChatBoxContent chatcontent = new ChatBoxContent();

                            //chatcontent.Text = dict[-1];
                            //foreach (int key in dict.Keys)
                            //{
                            //    if (key >= 0)
                            //    {
                            //        byte[] b = Convert.FromBase64String(dict[key]);
                            //        //File.WriteAllBytes(@"C:\1.jpg", b);
                            //        //using (MemoryStream ms = new MemoryStream(b))
                            //        //{
                            //            MemoryStream ms = new MemoryStream(b);
                            //            Image img = Image.FromStream(ms);
                            //            chatcontent.ForeignImageDictionary[(uint)key] = Image.FromStream(ms);
                            //        //}
                            //    }
                            //}
                            #endregion

                            if (openChatDic.ContainsKey(from))
                            {
                                ChatFormForWeb chatOpen = openChatDic[from];

                                if (chatOpen.unReadMessage == null)
                                {
                                    chatOpen.unReadMessage = new List<ChatBoxContent>();
                                }


                                if (chatOpen.Visible == true)
                                {
                                    chatOpen.chatClass.AppendChatBoxContentAll(chatOpen.browser, messageStrFlag, from,
                                        DateTime.Now, chatcontent, chatOpen.MineID, chatOpen.FriendID, Color.Blue, false);
                                    // chatOpen.AppendChatBoxContentAll(messageStrFlag, from, null, chatcontent, Color.Blue, false);
                                    chatOpen.Focus();
                                }
                                else
                                {
                                    chatOpen.unReadMessage.Add(chatcontent);
                                }


                                //接收信息
                                //chatOpen.AppendChatBoxContent(from, null, chatcontent, Color.Blue, false);


                            }
                            else
                            {
                                //最近联系人 或者 正常联系列表
                                if (friendListBox2_Search!=null || friendListBox1 != null || recentListBox1 != null)
                                {
                                    ChatListSubItem[] items = null;
                                    items = friendListBox2_Search != null && friendListBox2_Search.chatListBox.Items.Count > 0 ? (friendListBox2_Search.chatListBox.GetSubItemsById(from)):( friendListBox1 != null && friendListBox1.chatListBox.Items.Count>0 ? (friendListBox1.chatListBox.GetSubItemsById(from)) :
                                        (recentListBox1 != null ? recentListBox1.chatListBox.GetSubItemsById(from) : null));
                                    if (items != null && items.Length>0)
                                    {
                                     
                                        items[0].IsTwinkle = true;
                                        ChatFormForWeb chatForm = new ChatFormForWeb(items[0], mySelfID, null);
                                        openChatDic.Add(items[0].ID, chatForm);
                                        chatForm.OpenChatClose += Chat_OpenChatClose;
                                        chatForm.xmppSendMessageBtnClick += Chat_xmppSendMessageBtnClick;
                                        chatForm.xmppSendFileOrFolderBtnClick += ChatForm_xmppSendFileOrFolderBtnClick;
                                        chatForm.sendLanguage += ChatForm_sendLanguage;
                                        //chatForm.AppendChatBoxContent(from, null, chatcontent, Color.Blue, false);


                                        if (chatForm.unReadMessage == null)
                                        {
                                            chatForm.unReadMessage = new List<ChatBoxContent>();
                                        }
                                        if (messageStr == SysParams.Sys_VoiceMessage)
                                        {
                                            chatForm.Visible = true;
                                            chatForm.Show();
                                            chatForm.TopLevel = true;
                                            chatForm.TopMost = true;
                                            //chatForm.AppendChatBoxContent(isSysMessage, from, null, chatcontent, Color.Blue, false);
                                            items[0].IsTwinkle = false;
                                            //Common.Vibration(chatForm);
                                            Common.SysMessageManage(chatForm, from, chatcontent.Text, chatcontent);
                                            chatForm.Focus();
                                        }
                                        else
                                        {
                                            chatForm.Visible = false;
                                            chatForm.unReadMessage.Add(chatcontent);
                                        }
                                    }

#if DEBUG
                                    MessageBox.Show(e.Message.Body, String.Format("来自{0}的消息", e.Message.From));
#endif
                                }
                            }
                        }
                    }
                    #endregion;
                }


            }
        }
        

        //TODO:好友下线回调的方法
        private void Pm_OnUnavailablePresence(object sender, PresenceEventArgs e)
        {
            /*< presence type = "unavailable" from = "yuxh@192.168.1.95/MatriX" to = "yujm@192.168.1.95" xmlns = "jabber:client" />*/
            string friendName = e.Presence.From.User;

            adjustFriendStatus(friendName, JustLib.UserStatus.OffLine);


            //获取标准的JID
            string from = StaticClass.getStandardJid(e.Presence.From);
            chatformHeadStatus(from);



        }

        /// <summary>
        /// 判断当聊天框存在时，聊天框的好友头像状态根据当前好友的状态而改变
        /// </summary>
        /// <param name="fridenID"></param>
        private void chatformHeadStatus(string fridenID)
        {
            if (openChatDic.ContainsKey(fridenID))
            {
                ChatFormForWeb chatform = openChatDic[fridenID];
                chatform.chatHeadState(chatform.Friend);
            }
        }

        //
        private void XMPP_OnRosterStart(object sender, mx.EventArgs e)
        {
            
        }
        /// <summary>
        /// 获取联系人列表，单个联系人的增删改都会调用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XMPP_OnRosterItem(object sender, RosterEventArgs e)
        {
            string groupName = e.RosterItem.Value;
            mx.Jid friendJid = e.RosterItem.Jid;


            //both：互为好友；from：对方请求你为好友，你同意但没添加对方好友；to：对方同意你的好友请求，但没添加你为他的好友；
            if (e.RosterItem.Subscription == Subscription.Both)
            {

                GGUser user = new GGUser(friendJid.Bare, "", friendJid.User, "", "签名", 1, groupName, friendJid, JustLib.UserStatus.OffLine);

                string friendName = e.RosterItem.Name;
                if (!userGroups.Contains(groupName))
                {
                    userGroups.Add(groupName);
                    List<GGUser> friendList = new List<GGUser>();
                    
                    friendList.Add(user);

                    friendDic.Add(groupName, friendList);

                }
                else
                {
                    if (!friendDic[groupName].Exists(item => item.ID == user.ID))
                    {
                        friendDic[groupName].Add(user);
                    }
                }
                if (!SysParams.AllFriendList.Exists(item => item.ID == user.ID))
                {
                    //用一个全局的List装所有好友
                    SysParams.AllFriendList.Add(user);
                }

                //用一个全局的dic
                allFriendDic[friendJid.Bare] = user;
                //allFriendDic.Add(friendJid.Bare, user);


            }
          
        }


        #region 加载头像
        public delegate void loadUserHeadDelgate(GGUser user);

        public void loadUserHead(GGUser user)
        {
            lock(user)
            {
                byte[] headData = null;
                try
                {

                    string imgurl = SysParams.Head_ImageInfo + user.HeadImageUrl;
                    //headData =  TempFile.DownLoad_Head_2(imgurl, user.Name);
                    //{
                    //    if (headData != null)
                    //    {
                    //        user.HeadImageData = headData;
                    //    }
                    //}
                    headData = TempFile.DownLoad_Head_3(imgurl, user.Name,user.HeadImageUrl);
                    {
                        if (headData != null)
                        {
                            user.HeadImageData = headData;
                        }
                    }
                    user.IsRead = true;
                    if (this.recentListBox1 != null)
                    {
                        this.recentListBox1.SetUserHead(user.JID.Bare, headData);
                    }
                     if (this.friendListBox1 != null)
                    {
                        this.friendListBox1.SetUserHead(user.JID, headData);
                    }
                    if (this.friendListBox2_Search != null)
                    {
                        this.friendListBox2_Search.SetUserHead(user.JID, headData);
                    }
                }
                catch (Exception ex)
                {

                }
                
            }
        }
        #endregion

       
        
        
        private void XMPP_OnRosterEnd(object sender, mx.EventArgs e)
        {
            SysParams.userFriendUids = new List<string>();
            string[] friends = new string[this.allFriendDic.Count];
            this.allFriendDic.Keys.CopyTo(friends, 0);
            List<GGUser> guserlist = new List<GGUser>();
            foreach (UserInformation info in userServerClient.FindUserInformation(false,friends))
            {
                string jid = "";
                foreach (UserServiceInformation s in info.Services)
                {
                   
                    if (Regex.IsMatch(s.Service, "^im$", RegexOptions.IgnoreCase))
                    {
                        jid = (string)s.Data;

                        break;
                    } 
                }
                SysParams.userFriendUids.Add(info.UID.ToString());
                
                if (!String.IsNullOrWhiteSpace(jid) && this.allFriendDic.ContainsKey(jid))
                {
                    GGUser user = this.allFriendDic[jid];
                    user.UserUid = info.UID.ToString();
                    user.HeadImageData = SysParams.defualtHead_byte;
                    //user.HeadImageData = info.Head;



                    //user.HeadImageData = TempFile.GetDefaultHeadImage();

                    //luserD.BeginInvoke(
                    //byte[] headData = null;
                    //bool result = TempFile.Download_Head(@"http://192.168.1.93/DNALIMS/upload/148835977589691%E7%99%BB%E5%BD%9502.png", user.Name, ref headData);
                    //if (result == true)
                    //{
                    //user.HeadImageData = headData;
                    //}
                    user.HeadImageUrl = info.Head;
                    //user.UserAge = info.Age.ToString();
                    user.UserCompany = info.Company;
                    user.UserRole = info.Role.ToString();
                    user.UserSex = info.Sex;
                    user.UserName = info.Name;
                    user.Name = info.Name;//显示好友的真实姓名
                    guserlist.Add(user);
                    
                }
            }
            userServerClient.Close();
            //创建最近联系人
            recentFriend();
        }

        /// <summary>
        /// 创建好友列表
        /// </summary>
        private void createFriend()
        {

            for (int i = 0; i < panel1.Controls.Count; i++)
            {
                if (panel1.Controls[i] as UserListBox != null && panel1.Controls[i].Name== "friendListBox1")
                {
                    panel1.Controls.Remove(panel1.Controls[i]);
                    break;
                }
            }
           
            //friendListBox1 = new UserListBox();
            friendListBox1.Dock = DockStyle.Fill;
            friendListBox1.UserDoubleClicked += new CbGeneric<ChatListSubItem>(friendListBox1_UserDoubleClicked);
            this.friendListBox1.AddCatalogClicked += new CbGeneric(friendListBox1_AddCatalogClicked);
            if (friendDic.Keys.Count != 0)
            {
                foreach (KeyValuePair<string, List<GGUser>> userDic in friendDic)
                {

                    foreach (GGUser friend in userDic.Value)
                    {
                        bool isExist = false;
                        if (friendListBox1.chatListBox.Items.Count > 0)
                        {
                            ChatListItem.ChatListSubItemCollection cli = friendListBox1.chatListBox.Items[0].SubItems;
                            
                            foreach (ChatListSubItem subItem in cli)
                            {

                                if (subItem.Tag == friend)
                                {
                                    isExist = true;
                                }
                            }
                        }
                        if (!isExist)
                        {
                            friendListBox1.AddUser(friend, userDic.Key);
                            loadUserHeadDelgate luserD = loadUserHead;
                            luserD.BeginInvoke(friend, null, null);
                        }



                    }

                }
            }
            else
            {
                friendListBox1.AddUser(null, "我的好友");
            }
           
            panel1.Controls.Add(friendListBox1);
            friendListBox1.BringToFront();
            ibtnSF.BringToFront();
            friendListBox1.SortAllUser();
            friendListBox1.ExpandRoot();

           
           
            
           
        }


        /// <summary>
        /// 创建好友列表
        /// </summary>
        private void createSearchFriend()
        {
            for (int i = 0; i < panel1.Controls.Count; i++)
            {
                if (panel1.Controls[i] as UserListBox != null && panel1.Controls[i].Name == "friendListBox2_Search")
                {
                    panel1.Controls.Remove(panel1.Controls[i]);
                    break;
                }
            }
            if (friendListBox2_Search.chatListBox.Items.Count > 0)
            {
                ChatListItem.ChatListSubItemCollection cli2 = friendListBox2_Search.chatListBox.Items[0].SubItems;
                foreach (ChatListSubItem subItem in cli2)
                {
                    if (subItem!=null && subItem.HeadImage != SysParams.defaultHead)
                    {
                        subItem.HeadImage.Dispose();
                    }
                    cli2.Remove(subItem);
                }
            }
            //friendListBox1 = new UserListBox();
            friendListBox2_Search.Dock = DockStyle.Fill;
            friendListBox2_Search.UserDoubleClicked += new CbGeneric<ChatListSubItem>(friendListBox1_UserDoubleClicked);
            this.friendListBox2_Search.AddCatalogClicked += new CbGeneric(friendListBox1_AddCatalogClicked);
            if (friendDic.Keys.Count != 0)
            {
                foreach (KeyValuePair<string, List<GGUser>> userDic in friendDic)
                {

                    foreach (GGUser friend in userDic.Value)
                    {
                        if (friend.Name.Contains( search_val) || friend.JID.Bare.Contains(search_val))
                        {
                            bool isExist = false;
                            if (friendListBox2_Search.chatListBox.Items.Count > 0)
                            {
                                ChatListItem.ChatListSubItemCollection cli = friendListBox2_Search.chatListBox.Items[0].SubItems;

                                foreach (ChatListSubItem subItem in cli)
                                {

                                    if (subItem.Tag == friend)
                                    {
                                        isExist = true;
                                    }
                                }
                            }
                            if (!isExist)
                            {
                                friendListBox2_Search.AddUser(friend, userDic.Key);
                                loadUserHeadDelgate luserD = loadUserHead;
                                luserD.BeginInvoke(friend, null, null);
                            }

                        }
                    }

                }
            }
            else
            {
                friendListBox2_Search.AddUser(null, "");
            }

            panel1.Controls.Add(friendListBox2_Search);
            friendListBox2_Search.BringToFront();
            ibtnSF.BringToFront();
            friendListBox2_Search.SortAllUser();
            friendListBox2_Search.ExpandRoot();





        }


        /// <summary>
        /// 添加分组
        /// </summary>
        private void friendListBox1_AddCatalogClicked()
        {
            EditCatelogNameForm form = new EditCatelogNameForm();
           
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.friendListBox1.AddCatalog(form.NewName);
            }
        }

        /// <summary>
        /// 创建最近联系人
        /// </summary>
        private void recentFriend()
        {
            for (int i = 0; i < panel1.Controls.Count; i++)
            {
                if (panel1.Controls[i] as RecentListBox != null)
                {
                    panel1.Controls.Remove(panel1.Controls[i]);
                }
            } 
            DataTable dt= sqliteHelper.retRecent(SysParams.LoginUser.GetJID());
           // recentListBox1 = new RecentListBox();
            recentListBox1.Dock = DockStyle.Fill;
            this.recentListBox1.UnitDoubleClicked += new CbGeneric<ChatListSubItem>(recentListBox1_UserDoubleClicked);
            if(dt!=null && dt.Rows.Count>0)//判断是否有最近联系人
            {
                foreach (List<GGUser> friend in friendDic.Values)
                {
                    ChatListItem.ChatListSubItemCollection cli = recentListBox1.chatListBox.Items[0].SubItems;
                    List<GGUser> tmp = new List<GGUser>();
                    for (int i = 0; i < friend.Count; i++)
                    {
                        GGUser g = friend[i];
                        foreach (ChatListSubItem subItem in cli)
                        {

                            if (subItem.Tag == friend[i])
                            {
                                continue;
                            }
                        }
                        DataRow[] drlist = dt.Select("linkUserid='" + g.JID.Bare + "'");
                        if (drlist.Length > 0)
                        {
                            tmp.Add(friend[i]);
                        }
                    }
                    if (tmp != null && tmp.Count > 0)
                    {
                        recentListBox1.AddRecentLinkman(tmp);
                        for (int i = 0; i < tmp.Count; i++)
                        {
                            loadUserHeadDelgate luserD = loadUserHead;
                            luserD.BeginInvoke(tmp[i], null, null);
                        }
                    }
                    /*
                    foreach (ChatListSubItem subItem in cli)
                    {

                        if (subItem.Tag == friend[0])
                        {
                            isExist = true;
                        }
                    }
                    if (!isExist)
                    {
                        recentListBox1.AddRecentLinkman(friend[0], 0);
                        loadUserHeadDelgate luserD = loadUserHead;
                        luserD.BeginInvoke(friend[0], null, null);
                    }*/
                }
                //if(recentListBox1.
               
                //if (friend[0].IsRead)
                //{
                    //异步加载头像
                   

                //}

                //recentListBox1.AddRecentUnit(friend[0], 0);

            }

               
            panel1.Controls.Add(recentListBox1);
            recentListBox1.BringToFront();

            ibtnSF.BringToFront();
        }
        /// <summary>
        /// 最近联系人双击好友
        /// </summary>
        /// <param name="obj"></param>
        private void recentListBox1_UserDoubleClicked(ChatListSubItem obj)
        {
            judgeOpenform(obj);
        }

        private void XmppClientOnSendXml(object sender, mx.TextEventArgs e)
        {
            
        }

        private void XmppClientOnReceiveXml(object sender, mx.TextEventArgs e)
        {
            //           < failure xmlns = "urn:ietf:params:xml:ns:xmpp-sasl" >
            //  < not - authorized />
            //</ failure >

            //< success xmlns = "urn:ietf:params:xml:ns:xmpp-sasl" > dj1valQySHE2OUVFdFFwcWxwUC84OVg4M1lUTGs9 </ success >

            try
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(e.Text);
                
                if ("success".Equals(xml.DocumentElement.Name))
                {
                    string result = xml.InnerText;
                    this.client.OnReceiveXml -= XmppClientOnReceiveXml;
                }
                else if ("failure".Equals(xml.DocumentElement.Name))
                {

                    XmlElement error = (XmlElement)xml.DocumentElement.FirstChild;
                    MessageBox.Show(error.Name);
                    this.client.OnReceiveXml -= XmppClientOnReceiveXml;

                    
                }
            }
            catch(XmlException)
            {
            }
        }



        #region 文件传输
        //文件传输结束时触发
        internal void Fm_OnEnd(object sender, FileTransferEventArgs e)
        {

            string jid = e.Jid;
            string sid = e.Sid;
            Thread fThread = new Thread(new ParameterizedThreadStart(FinishFile));
            fThread.Start(e);
            //MessageBox.Show("文件传输完毕");
        }

        //文件传输开始时触发-发送方 传输
        private void Fm_OnStart(object sender, FileTransferEventArgs e)
        {
           
        }


        private void Fm_OnError(object sender, mx.ExceptionEventArgs e)
        {

        }
        private void FinishFile(object e)
        {
            //System.Threading.Thread.Sleep(10);
            FileCompleted(e as FileTransferEventArgs);

        }
        /// <summary>
        /// 在线文件完成上传后操作
        /// </summary>
        /// <param name="e"></param>
        public void FileCompleted(FileTransferEventArgs e)
        {


                string friendId = e.Jid.Bare;
                ChatFormForWeb chat = openChatDic[friendId]; //openChatDic[friendId.Split('/')[0]];
                string fileId = e.Description.Split(',')[0];
                long sendedSize = e.BytesTransmitted;
                long fileSize = e.FileSize;
                chat.RightSendPanel.Invoke(new Action(() => {
                    //删除已完成的控件
                    ItemUploadFile item = (ItemUploadFile)chat.RightSendPanel.Controls[fileId];
                    if (item != null)
                    {
                        FileClass file = item.File;
                        chat.RemoveProgressBar(file);

                        //完毕后收发方追加 完成记录
                        if (e.Direction == Direction.Outgoing)//发送者
                        {

                            //string sendContent = SysParams.Sys_File_Cancel + JsonConvert.SerializeObject(file);
                            //string icoUrl=SysParams.Html_SysImagePath+ file.FileName.Substring(file.FileName.LastIndexOf(@"\")+1);
                            file.IsSender = false;
                            string sendContent = SysParams.Sys_File_Success + JsonConvert.SerializeObject(file);
                            ChatBoxContent content = new ChatBoxContent();
                            content.Text = sendContent;
                            chat.OPENFIRE_SendMessage(content, chat.FriendID);
                            return;
                            chat.chatClass.addHistory_File(chat.browser,true, "完成", chat.MineID_Small, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 
                                file.IcoUrl, file.FileName, file.FileSize,
                                SysParams.File_Send_Result,chat.ChatBoxSend.Font.FontFamily.Name,chat.ChatBoxSend.Font.Size.ToString());
                        }
                        else if (e.Direction == Direction.Incoming)//接收者
                        {
                            file.IsSender = true;
                            string sendContent = SysParams.Sys_File_Success + JsonConvert.SerializeObject(file);
                            ChatBoxContent content = new ChatBoxContent();
                            content.Text = sendContent;
                            chat.OPENFIRE_SendMessage(content, chat.FriendID);
                            return;
                            chat.chatClass.addHistory_File(chat.browser, true, "完成", chat.MineID_Small,
                                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), file.IcoUrl,
                                file.ReceivePath, file.FileSize,
                                 string.Format(SysParams.File_Receive_Result, file.ReceivePath),
                                 chat.ChatBoxSend.Font.FontFamily.Name, chat.ChatBoxSend.Font.Size.ToString());
                        }
                    }
                }));

        }
        
      

        private void SetTextMesssage(FileTransferEventArgs e)
        {
            if (this.InvokeRequired)
            {
                SetPos setpos = new SetPos(SetTextMesssage);
                this.Invoke(setpos, new object[] { e });
            }
            else
            {

                string friendId = e.Jid.Bare;
                if (openChatDic.Count > 0)
                {
                    ChatFormForWeb chat = openChatDic[friendId]; //openChatDic[friendId.Split('/')[0]];
                    if (chat != null)
                    {
                        string fileId = e.Description.Split(',')[0];
                        long sendedSize = e.BytesTransmitted;
                        long fileSize = e.FileSize;
                        ItemUploadFile item = (ItemUploadFile)chat.RightSendPanel.Controls[fileId];
                        if (item != null)
                        {
                            item.Invoke(new Action(() =>
                            {
                                item.Info_2 = Common.FormatFileSize(sendedSize) + "/" + Common.FormatFileSize(fileSize);
                                item.value = e.FileSize != 0 ? (int)(sendedSize * 100 / e.FileSize) : 100;
                                item.Invalidate();
                            }));
                        }
                    }
                }
                //var a = e.BytesTransmitted;
                //string p = ((double)a / e.FileSize).ToString("P");

                //if (openChatDic.ContainsKey(e.Jid.Bare))
                //{
                //    ChatFormForWeb chatOpen = openChatDic[e.Jid.Bare];
                    
                //    chatOpen.skinProgressBar.Maximum = Convert.ToInt32(e.FileSize);
                //    chatOpen.skinProgressBar.Minimum = 0;
                //    chatOpen.skinProgressBar.Value = (int)a;
                //    chatOpen.Show();
                //    chatOpen.Focus();

                //}
            }
        }

        private void SleepT(object e)
        {         
            System.Threading.Thread.Sleep(10);
            SetTextMesssage(e as FileTransferEventArgs);
            
        }


        /// <summary>
        /// 接收文件 获取每次接收文件的进度情况
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Fm_OnProgress(object sender, FileTransferEventArgs e)
        {
            
            
            //return;
            Thread fThread = new Thread(new ParameterizedThreadStart(SleepT));
            fThread.Start(e);
        }

      
        /// <summary>
        /// 文件发送 接收方 处理请求
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XMPP_OnFile(object sender, FileTransferEventArgs e)
        {
            
            try
            {
                //string fileId = e.Filename;
                string[] paramsStr = e.Description.Split(',');
                string fileId = paramsStr[0];
                string friendId = paramsStr[1];
                string jid = e.Jid.Bare;
                ChatFormForWeb chat = openChatDic[friendId];
                ItemUploadFile item = (ItemUploadFile)chat.RightSendPanel.Controls[fileId];
                item.File.SendId = e.Sid;
                e.Accept = true;
                e.Filename = item.File.ReceivePath;
                return;

                if (e.Accept = MessageBox.Show(String.Format("File:{0}\r\nSize:{1}", e.Filename, e.FileSize), String.Format("来自{0}的文件", e.Jid), MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    //设置保存目录
                    //e.Directory = @"D:\Files";
                    string savePath = ESBasic.Helpers.FileHelper.GetPathToSave("保存", e.Filename, null);
                    if (!string.IsNullOrEmpty(savePath))
                    {
                        //e.Directory = savePath;
                        e.Filename = savePath;
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
        #endregion

        void AddSubscribe(mx.Jid from, string msg)
        {
            this.pm.ApproveSubscriptionRequest(from);
            this.pm.Subscribe(from);

            return;
            /*
            if ("对方已同意你的好友请求，是否允许对方添加你为好友".Equals(msg))
            {
                this.pm.ApproveSubscriptionRequest(from);
            }
            else if (MessageBox.Show(string.Format("{0}请求添加你为好友,是否同意?", from.User), "收到好友请求", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.pm.ApproveSubscriptionRequest(from);
                //this.pm.ApproveSubscriptionRequest(mySelfID);

                this.pm.Subscribe(from, "对方已同意你的好友请求，是否允许对方添加你为好友");

            }
            else
            {
                this.pm.DenySubscriptionRequest(from);
            }
            */
        }

        //收到好友请求时触发
        private void XMPP_OnSubscribe(object sender, PresenceEventArgs e)
        {
            mx.Jid from = e.Presence.From;
            string msg = e.Presence.Status;

            this.BeginInvoke(this.pAddSubscribe, from, msg);
        }
        

        private void MouseRightDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                
                ItemInfo iteminfo = (ItemInfo)sender;
                MessageBox.Show(iteminfo.ItemstrTitleT);

            }
        }

        /// <summary>
        /// 缩进按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void ibtnSJ();
        public event ibtnSJ ibtn_SJ;
        private void ibtnSF_Click(object sender, EventArgs e)
        {
            if (ibtn_SJ != null)
            {
                ibtn_SJ();
            }
        }

      

        private void panelList_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, panelList.ClientRectangle,
            Color.DarkGray, 1, ButtonBorderStyle.Solid, //左边
            Color.White, 1, ButtonBorderStyle.None, //上边
            Color.White, 1, ButtonBorderStyle.None, //右边
            Color.White, 1, ButtonBorderStyle.None);//底边
        }

       

        //TODO:好友列表上搜索框 搜索btn触发事件
        private void search_ImageBtn_Click(object sender, EventArgs e)
        {
            search_val = search_ImageTextBox.Text;
            if(!string.IsNullOrEmpty(search_val)){
                //搜索方法
                searchFriendList();
            }
        }
        
        //TODO:好友列表上搜索框 Enter键触发事件
        private void search_ImageTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //搜索方法
                searchFriendList();
            }
        }

        //TODO:根据搜索的text进行搜索
        private void searchFriendList()
        {
            createSearchFriend();

            //if (search_ImageTextBox.Text != "")
            //{
            //    mx.Jid jid = search_ImageTextBox.Text;

            //    this.pm.Subscribe(jid, String.Format("My name is {0}", this.m_name));
            //}
            //else
            //{
            //    MessageBox.Show("请输入你所需要搜索的内容");
            //}
        }

        /// <summary>
        /// 查看通讯录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ibtnAdress_Click(object sender, EventArgs e)
        {
           initImage();
           ibtnAdress.Staticpic = ibtnAdress.Presspic;
            im_eu = IMType.friends;
           createFriend();

           
        }
        /// <summary>
        /// 点击右边四个功能区初始化静态背景
        /// </summary>
        private void initImage()
        {
            this.ibtnMessage.Staticpic = Resources.message_normal_09;
            this.ibtnEmail.Staticpic = Resources.email_normal_13;
            this.ibtnMeeting.Staticpic = Resources.meeting_normal_14;
            this.ibtnAdress.Staticpic = Resources.address_book_normal_15;
            this.personPlatform.Staticpic = Resources.add_Users_normal_03;     
            this.Refresh();
        }

        /// <summary>
        /// 当前联系人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ibtnMessage_Click(object sender, EventArgs e)
        {
            initImage();
            ibtnMessage.Staticpic = ibtnMessage.Presspic;
            im_eu = IMType.recent;
            recentFriend();
        }
        /// <summary>
        /// 会议
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ibtnMeeting_Click(object sender, EventArgs e)
        {
            initImage();
            ibtnMeeting.Staticpic = ibtnMeeting.Presspic;
            Point p = new Point(ibtnMeeting.Location.X, ibtnMeeting.Location.Y + ibtnMeeting.Height);
            p = this.PointToScreen(p);
            cmsiAdd.Show(p);
            
            //MeetingForm m = new MeetingForm();
            //m.Show();
        }


        public delegate void RersonEmailClickHandle();
        public event RersonEmailClickHandle personEmailClick;
        /// <summary>
        /// 邮件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ibtnEmail_Click(object sender, EventArgs e)
        {
            personPlatformIsClick = true;

            initImage();
            ibtnEmail.Staticpic = ibtnEmail.Presspic;

            if(personEmailClick != null)
            {
                personEmailClick();
            }
        }


        private bool personPlatformIsClick;

        public bool PersonPlatformIsClick
        {
            get { return personPlatformIsClick; }
            set
            {
                personPlatformIsClick = value;

                if (personPlatformIsClick == false)
                {
                    initImage();
                }
            }
        }

        //public bool personPlatformIsClick;

        public delegate void PersonPlatfromClickHandle(string personUid);
        public event PersonPlatfromClickHandle personPlatfromClick;
        /// <summary>
        /// 个人工作平台
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void personPlatform_Click(object sender, EventArgs e)
        {
           

            //personPlatformIsClick = true; 

            //initImage();
            //personPlatform.Staticpic = personPlatform.Presspic;

            //if (personPlatfromClick != null)
            //{
            //    personPlatfromClick(this.UserUid);
            //}
        }

        private void imageButton1_Click(object sender, EventArgs e)
        {
            initImage();
            personPlatform.Staticpic = personPlatform.Presspic;
            UserListForm uf = new UserListForm();
            uf.Show();
            //string imgStr = @"E:\好友列表.bmp";
            //AddUserForm addFrm = new AddUserForm(Image.FromFile(imgStr), "userNameuserName", "LV","JIDJIDJID","单位单位单位单位");

            //addFrm.Show();
        }

        private void tsmiEmcee_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            if (item != null && item.Tag != null)
            {
                switch (Convert.ToInt32(item.Tag))
                {
                    case 1:
                        //MeetingForm m = new MeetingForm();
                        //m.Show();
                        CreateMeetingForm cf = new CreateMeetingForm();
                        cf.Show();
                        break;
                    case 2:
                        MeetingListForm ml = new MeetingListForm();
                        ml.Show();
                        break;
                }
            }
        }

        private void btnSearchCancel_Click(object sender, EventArgs e)
        {
            search_ImageTextBox.Text = string.Empty;
            switch (im_eu)
            {
                case IMType.recent:
                    recentFriend();
                    break;
                case IMType.friends:
                    createFriend();
                    break;
            }
        }
    }
}
