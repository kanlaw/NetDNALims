using CefSharp.WinForms;
using IMClient.Controls;
using IMClient.Controls.Base;
using IMClient.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using CefSharp;
using CCWin.SkinControl;
using Model;
using Newtonsoft.Json;
using JustLib;
using NetDLims.Services;
using Matrix.Xmpp.Client;
using Matrix.Xmpp;
using JustLib.Controls;
using System.Threading;

namespace IMClient.XMPP.Forms
{
    public partial class MeetingForm : FrameBase
    {
        #region
        //private int titleHeight = 40;
        private int titleContentHeight = 25;
        private string titleStr = "网络会议室-开放型";

        private string titleContentStr = "《关于DNA实验室建设》";
        private string titleContentTitleStr = "会议主题";
        private string titleContentKeyPointTitleStr = "讨论要点";
        private string titleContentVoteTitleStr = "投票";
        private string titleStr_Emcee = "主持人";
        private int titleStr_EmceeHeight = 30;

        public Meeting meeting = null;
        /// <summary>
        /// 参与成员列表
        /// </summary>
        public List<MeetingMember> mmlist = new List<MeetingMember>();

        /// <summary>
        /// 会议主持人
        /// </summary>
        public MeetingMember emceeMember = new MeetingMember();


        public List<MeetingOption> molist = new List<MeetingOption>();
        public MeetingLogic meetingbll = new MeetingLogic();
        private string meetingGuid = string.Empty;
        private int MemberCount = 1;
        /// <summary>
        /// 会议要点
        /// </summary>
        public List<string> keyPointStr = new List<string>();
        public List<MeetingOption> voteList = new List<MeetingOption>();
        public List<ItemLineColumn> voteResultList = new List<ItemLineColumn>();
        //public Dictionary<string, int> voteSelect = new Dictionary<string, int>();
        //string[] tmpval = new string[] { "A", "B", "C", "D" };
        Color[] Colorlist = new Color[] { Color.Orange, Color.Chocolate, Color.DarkOrchid, Color.Indigo };

        Panel pnlContentStr = new Panel();

        List<ItemCheckBox> optionlist = new List<ItemCheckBox>();

        #endregion

        //声明一个加载网页的WebBrowser
        public ChromiumWebBrowser browser_Emcee;
        public ChromiumWebBrowser browser_Actor;
        private bool isRuning_browser_Emcee = false;
        private bool isRuning_browser_Actor = false;
        Common comm = new Common();
        Matrix.Jid meeting_JId = null;

        /// <summary>
        /// 关闭会议室
        /// </summary>
        public delegate void MeetingCloseDelegate();


        public MeetingForm(string meetingGuid, bool isEmcee)
        {
            this.meetingGuid = meetingGuid;
           
            //this.Init(this.meetingGuid);
            InitializeComponent();

            ChatListItem cli = new ChatListItem("主持人");
            //ChatListSubItem clsl = new ChatListSubItem("王斌", "王斌", "江苏省公安厅:处长");
            //clsl.HeadImage = Image.FromFile(@"C:\head.png");
            //cli.SubItems.Add(clsl);
            cli.IsOpen = true;
            meetGrouplist.ChatListBox_group.Items.Insert(0, cli);

            #region 添加浏览器
            pnlUp.Size = new Size(this.pnlCenter.Width, (this.pnlCenter.Height - titleStr_EmceeHeight) / 3);
            pnlUp.Location = new Point(0, titleStr_EmceeHeight);
            pnlBottom.Size = new Size(this.pnlCenter.Width, this.pnlCenter.Height - pnlUp.Height);
            pnlBottom.Location = new Point(0, titleStr_EmceeHeight + pnlUp.Height + 2);



            string chatWebPath = AppDomain.CurrentDomain.BaseDirectory + SysParams.Sys_MeetingMessageShownHtml;
            if (File.Exists(chatWebPath))
            {
                //if (SysParams.browser_Emcee == null)
                //{
                this.browser_Emcee = new ChromiumWebBrowser(chatWebPath);
                {
                    Dock = DockStyle.Fill;
                }
                //browser_Emcee.Margin = new Padding(0);
                //browser_Emcee.Padding = new Padding(0);
                //browser_Emcee.Size = new Size(this.pnlCenter.Width / 2, this.pnlCenter.Height / 2);
                //browser_Emcee.Location = new Point(0, 0);
                this.browser_Emcee.IsBrowserInitializedChanged += Browser_Emcee_IsBrowserInitializedChanged;
                this.browser_Emcee.Disposed += Browser_Emcee_Disposed;
                this.browser_Emcee.FrameLoadEnd += Browser_FrameLoadEnd;
                //browser_Emcee.Load(chatWebPath);
                comm.RegObjectTOCEF(this.browser_Emcee, comm, "jsOBJ");

                //"jsOBJ"
                //}
                pnlUp.Controls.Add(this.browser_Emcee);


            }

            chatWebPath = AppDomain.CurrentDomain.BaseDirectory + SysParams.Sys_Meeting_ActorMessageShownHtml;
            if (File.Exists(chatWebPath))
            {
                //if (SysParams.browser_Actor == null)
                //{
                this.browser_Actor = new ChromiumWebBrowser(chatWebPath);
                {
                    Dock = DockStyle.Fill;
                }
                //browser_Actor.Margin = new Padding(0);
                //browser_Actor.Padding = new Padding(0);
                //browser_Emcee.Size = new Size(this.pnlCenter.Width / 2, this.pnlCenter.Height / 2);
                //browser_Emcee.Location = new Point(0, 0);
                this.browser_Actor.IsBrowserInitializedChanged += Browser_Emcee_IsBrowserInitializedChanged;
                this.browser_Actor.FrameLoadEnd += Browser_Actor_FrameLoadEnd;
                //browser_Actor.Load(chatWebPath);
                comm.RegObjectTOCEF(this.browser_Actor, comm, "jsOBJ");

                //"jsOBJ"
                //}
                pnlBottom.Controls.Add(this.browser_Actor);
            }


            #endregion

            EnterCreatMeetingDelegate ecd = EnterCreatMeeting;
            ecd.BeginInvoke(meetingGuid, isEmcee, null, null);

            InitDelegate idelegete = this.Init;
            idelegete.BeginInvoke(this.meetingGuid, this.CompleteInit, idelegete);

        }

        private void Browser_Emcee_Disposed(object sender, EventArgs e)
        {
        }

        private void Browser_Emcee_IsBrowserInitializedChanged(object sender, IsBrowserInitializedChangedEventArgs e)
        {
        }

        public delegate void EnterCreatMeetingDelegate(string meetingGuid, bool isEmcee);

        /// <summary>
        /// 创建 / 进入会议室
        /// </summary>
        /// <param name="meetingGuid"></param>
        /// <param name="isEmcee"></param>
        public void EnterCreatMeeting(string meetingGuid, bool isEmcee)
        {
            try {
                if (isEmcee)//主持人 创建并进入
                {
                    this.meeting_JId = meetingbll.creatMeeting(meetingGuid);
                }
                else //不同参与人员 进入
                {
                    this.meeting_JId = meetingbll.meetingEnterCreateOpenFire(meetingGuid, SysParams.LoginUser.UID.ToString());
                }
                StaticClass.xmppClient.MessageFilter.Add(this.meeting_JId, new Matrix.BareJidComparer(), MessageCallback);
                // Setup new Presence Callback using the PresenceFilter
                StaticClass.xmppClient.PresenceFilter.Add(this.meeting_JId, new Matrix.BareJidComparer(), PresenceCallback);

            }
            catch (Exception ex)
            {
                MessageBox.Show("会议室获取失败！"+ex.Message, "提示");
            }
          


        }

        #region 会议室方法
        /// <summary>
        /// 消息接收
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MessageCallback(object sender, MessageEventArgs e)
        {
            if (e.Message.Type == Matrix.Xmpp.MessageType.GroupChat && e.Message.Type != Matrix.Xmpp.MessageType.Error)
            {
                while (true)
                {
                    if (isRuning_browser_Actor && isRuning_browser_Emcee)
                    {
                        break;
                    }
                }
                IncomingMessage(e.Message);
            }
        }


        /// <summary>
        /// 消息接收
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IncomingMessage(Matrix.Xmpp.Client.Message msg)
        {
            try {
                if (msg.Type == Matrix.Xmpp.MessageType.Error)
                {
                    //Handle errors here
                    // we dont handle them in this example
                    return;
                }

                if (msg.Subject != null)
                {
                    //txtSubject.Text = msg.Subject;

                    //rtfChat.SelectionColor = Color.DarkGreen;
                    //// The Nickname of the sender is in GroupChat in the Resource of the Jid
                    //rtfChat.AppendText(msg.From.Resource + " changed subject: ");
                    //rtfChat.SelectionColor = Color.Black;
                    //rtfChat.AppendText(msg.Subject);
                    //rtfChat.AppendText("\r\n");
                }
                else
                {
                    if (msg.Body == null)
                        return;

                    if (this.meeting != null)
                    {
                        bool isEmcee = false;
                        ChromiumWebBrowser browser = this.browser_Actor;
                        bool isSelf = false;
                        try
                        {
                            //Matrix.   msg.XMucUser
                            string type = msg.Body.Substring(0, msg.Body.IndexOf("]") + 1);
                            string content = msg.Body.Substring(msg.Body.IndexOf("]") + 1);
                            switch (type)
                            {
                                case SysParams.Sys_Normal:
                                    talkShown(msg, ref isEmcee, ref browser, ref isSelf);
                                    break;
                                case SysParams.Sys_Vote:
                                    if (voteResultList != null && voteResultList.Count > 0)
                                    {
                                        for (int i = 0; i < voteResultList.Count; i++)
                                        {
                                            ItemLineColumn ilc = voteResultList[i];
                                            MeetingOption mo = (MeetingOption)ilc.Tag;
                                            if (mo.MoId == content)
                                            {
                                                ilc.MaxIndex = this.mmlist.Count;
                                                ilc.CurrentIndex += 1;
                                                ilc.Invalidate();
                                                break;
                                            }
                                        }
                                    }
                                    break;
                            }

                        }
                        catch (Exception ex)
                        {

                            string m = ex.Message;
                        }

                    }


                    //MessageBox.Show("测试反馈！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("消息接收Message:"+ex.Message, "提示");
            }
          
        }

        /// <summary>
        /// 展示聊天记录
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="isEmcee"></param>
        /// <param name="browser"></param>
        /// <param name="isSelf"></param>
        private void talkShown(Matrix.Xmpp.Client.Message msg, ref bool isEmcee, ref ChromiumWebBrowser browser, ref bool isSelf)
        {
            if (this.meeting.EmceeJid == msg.XMucUser.Item.Jid.Bare)//如果是主持人
            {
                browser = this.browser_Emcee;
                isEmcee = true;
            }
            if (msg.XMucUser.Item.Jid.Bare == SysParams.LoginUser.GetJID())
            {
                isSelf = true;
            }
            // string imgHead = "source/chengle/Head/default.jpg";
            string imgHead = TempFile.retImgHeadByUserName(msg.XMucUser.Item.Jid.Bare);
            JustLib.Controls.ChatBox cb = new JustLib.Controls.ChatBox();
            cb.Text = msg.Body.Substring(msg.Body.IndexOf("]") + 1);
            this.comm.sendMessage_Meeting(browser, isSelf.ToString().ToLower(), cb.Text, msg.From.Resource, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), this.chatBoxSend.Font.Name, this.chatBoxSend.Font.Size.ToString(),
            imgHead, isEmcee);
        }


        /// <summary>
        /// 用户状态 调整
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PresenceCallback(object sender, PresenceEventArgs e)
        {
            try {

                var mucX = e.Presence.MucUser;

                // check for status code 201, this means the room is not ready to use yet
                // we request an instant room and accept the and accept the default configuration by the server
                if (mucX != null && mucX.HasStatus(201)) // 201 =  room is awaiting configuration.
                    StaticClass.muc.RequestInstantRoom(this.meeting_JId);

                if (e.Presence == null)
                {
                    MessageBox.Show("PresenceCallback:e.Presence 为空");
                }
                else if (mucX == null)
                {
                    MessageBox.Show("PresenceCallback:mucX 为空");
                }
                else if (mucX.Item == null)
                {
                    MessageBox.Show("PresenceCallback:mucX.Item 为空");
                }
                else if (mucX.Item.Jid == null)
                {
                    MessageBox.Show("PresenceCallback:mucX.Item.Jid 为空");
                }

                if (e.Presence.Type == PresenceType.Unavailable && ( mucX.Item.Jid==null || mucX.Item.Jid.Bare == SysParams.LoginUser.GetJID()))
                {

                    if (MessageBox.Show("会议室已关闭,是否退出？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        this.Close();
                    }
                    return;
                }
                string userId = e.Presence.From;

                string jid = e.Presence.MucUser.Item.Jid.Bare.ToString();

                string uid = jid;
                int GroupIndex = 1;
                if (jid == this.meeting.EmceeJid)
                {
                    GroupIndex = 0;
                }
                ChatListSubItem item = retSubItemByUserId(GroupIndex, uid);
                if (item != null)//存在用户
                {
                    if (e.Presence.Type == PresenceType.Unavailable)//下线
                    {
                        //item.IsTwinkle = true;//有消息
                        item.Status = ChatListSubItem.UserStatus.OffLine;
                    }
                    else if (e.Presence.Type == PresenceType.Available)//在线
                    {
                        //item.IsTwinkle = true;
                        item.Status = ChatListSubItem.UserStatus.Online;
                    }


                    this.meetGrouplist.ChatListBox_group.Refresh();
                }
                else //新加入成员
                {
                    //查询用户 根据JID
                    string tmp_uid = string.Empty;
                    string realName = e.Presence.Muc != null ? e.Presence.Muc.GetAttribute("REALNAME") : string.Empty;
                    string company = e.Presence.Muc != null ? e.Presence.Muc.GetAttribute("COMPANY") : string.Empty;
                    string imageUrl = e.Presence.Muc != null ? e.Presence.Muc.GetAttribute("HEAD") : string.Empty;
                    DataTable dt = meetingbll.retUserInfoByJID(jid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        try
                        {
                            tmp_uid = dr["useruid"].ToString();
                            realName = dr["truename"].ToString();
                            company = dr["company"].ToString();
                            imageUrl = dr["head"].ToString();
                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show("PresenceCallback:用户属性获取失败");
                        }
                     
                        this.AddUserInfo(GroupIndex, jid, jid, realName, company, ChatListSubItem.UserStatus.Online, SysParams.defaultHead, imageUrl);

                        //同步成员数据
                        if (jid != this.meeting.EmceeJid)
                        {
                            addMemberDelegate amd = addMember;
                            amd.BeginInvoke(jid, int.Parse(tmp_uid), addMemberComplete, null);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("用户状态获取。Presence:"+ex.Message, "提示");
            }
           
            //foreach (ChatListItemCollection itemC in meetGrouplist.ChatListBox_group.Items)
            //{
            //    foreach (var s in itemC)
            //    {

            //    }
            //}

            //e.Presence.Id

            //this.AddUserInfo(groupIndex, userJId, nickName, realName, Company, uStatus, null, ImageUrl);

            /*

            var lvi = FindListViewItem(e.Presence.From);
            if (lvi != null)
            {

            }
            else
            {
                if (treeView1.Nodes.Count == 0)
                {
                    TreeNode node = (new TreeNode(e.Presence.From.ToString()));
                    node.Tag = e.Presence.From.ToString();
                    treeView1.Nodes.Add(node);
                }

                TreeNode node2 = new TreeNode(e.Presence.MucUser.ToString());
                node2.Tag = e.Presence.MucUser.ToString();
                treeView1.Nodes[0].Nodes.Add(node2);

            }
            */

            /*   
           if (lvi != null)
           {
               if (e.Presence.Type == PresenceType.Unavailable)
               {
                   lvi.Remove();
               }
               else
               {
                   int imageIdx = Util.GetRosterImageIndex(e.Presence);
                   lvi.ImageIndex = imageIdx;
                   lvi.SubItems[1].Text = (e.Presence.Status ?? "");

                   var u = e.Presence.MucUser;
                   if (u != null)
                   {
                       lvi.SubItems[2].Text = u.Item.Affiliation.ToString();
                       lvi.SubItems[3].Text = u.Item.Role.ToString();
                   }
               }
           }
           else
           {
               int imageIdx = Util.GetRosterImageIndex(e.Presence);

               var lv = new TreeNode(e.Presence.From.Resource) { Tag = e.Presence.From.ToString() };

               lv.SubItems.Add(e.Presence.Status ?? "");

               var u = e.Presence.MucUser;
               if (u != null)
               {
                   lv.SubItems.Add(u.Item.Affiliation.ToString());
                   lv.SubItems.Add(u.Item.Role.ToString());
               }
               lv.ImageIndex = imageIdx;
               treeView1.Nodes.Add(lv);
           }*/
        }

        public delegate void addMemberDelegate(string jid, int uid);

        public void addMemberComplete(IAsyncResult iar)
        {
            voteResultAdjust();
        }

        /// <summary>
        /// 投票选项 总数调整
        /// </summary>
        private void voteResultAdjust()
        {
            if (this.voteResultList != null && this.voteResultList.Count > 0)
            {
                for (int i = 0; i < this.voteResultList.Count; i++)
                {
                    this.voteResultList[i].MaxIndex = this.mmlist.Count;
                    this.voteResultList[i].Invoke(new Action(() =>
                    {
                        this.voteResultList[i].Invalidate();
                    }));
                }
            }
        }

        /// <summary>
        /// 数据库 添加新成员
        /// </summary>
        /// <param name="jid"></param>
        /// <param name="uid"></param>
        public void addMember(string jid,int uid)
        {
            try
            {
                MeetingMember m = new MeetingMember();
                m.UserJid = jid;
                m.MeetingId = this.meeting.MeetingGuid;
                m.MemberId = uid;
                m.IsJoin = 0;
                m.Mmid = Guid.NewGuid().ToString().Replace("-", "");
                List<MeetingMember> mmlist_tmp = new List<MeetingMember>();
                mmlist_tmp.Add(m);
                this.meetingbll.AddMember(mmlist_tmp);
                this.mmlist.Add(m);
            }
            catch (Exception ex)
            {

                throw;
            }
         
        }

        #endregion;

        private void Browser_Actor_FrameLoadEnd(object sender, CefSharp.FrameLoadEndEventArgs e)
        {
            //throw new NotImplementedException();
            Console.WriteLine("Actor:{0},{1}", e.Browser.Identifier, e.Frame.Identifier);
            this.isRuning_browser_Actor = true;
        }

        private void Browser_FrameLoadEnd(object sender, CefSharp.FrameLoadEndEventArgs e)
        {
            //comm.setHeight(this.pnlCenter.Height);
            //throw new NotImplementedException();
            Console.WriteLine("Emecc:{0},{1}", e.Browser.Identifier, e.Frame.Identifier);
            this.isRuning_browser_Emcee = true;
        }

        #region 界面绘制区域
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.FillRectangle(new SolidBrush(SysParams.Paint_title_Color),
                new Rectangle(new Point(0, 0), new Size(this.Width, SysParams.Paint_title_Height)));

            int x = pnlleft.Width + (pnlCenter.Location.X - pnlleft.Width) / 2;
            int y = pnlleft.Location.Y;
            g.DrawLine(new Pen(SysParams.Paint_SplitLine_Color, 1.5f), new Point(x, y), new Point(x, this.Height));

            x = pnlCenter.Location.X + pnlCenter.Width + (pnlRight.Location.X - (pnlCenter.Location.X + pnlCenter.Width)) / 2;
            g.DrawLine(new Pen(SysParams.Paint_title_Color, 1.5f), new Point(x, y), new Point(x, this.Height));


            g.DrawString(this.titleStr, SysParams.Paint_title_Font,
                new SolidBrush(SysParams.Paint_title_Font_Color), SysParams.Paint_title_Point);
        }

        /// <summary>
        /// 会议标题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlContentTitle_Paint(object sender, PaintEventArgs e)
        {

            drawTitile(e.Graphics, this.titleContentTitleStr, this.pnlContentTitle, this.titleContentHeight);

            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;


            e.Graphics.DrawString(this.titleContentStr, new Font("微软雅黑", 10.0f, FontStyle.Bold)
                , new SolidBrush(Color.FromArgb(102, 153, 204)),
                new Rectangle(new Point(0, titleContentHeight),
                new Size(this.pnlContentTitle.Width, (this.pnlContentTitle.Height - titleContentHeight)))
                , sf);


        }

        private void drawTitile(Graphics g, string titleContent, Panel pnl, int titleContentHeight)
        {

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;

            Rectangle rect = new Rectangle(new Point(2, 1),
                new Size(pnl.Width - 2, (titleContentHeight - 1)));

            LinearGradientBrush lgb = new LinearGradientBrush(rect,
            Color.FromArgb(250, 250, 250), Color.FromArgb(220, 220, 220), LinearGradientMode.Vertical);

            rect = new Rectangle(new Point(1, 0),
                new Size(pnl.Width - 3, (titleContentHeight)));

            g.FillRectangle(lgb, rect);

            g.DrawRectangle(new Pen(Color.FromArgb(195, 195, 195)), rect);

            if (!string.IsNullOrEmpty(titleContent))
            {
                g.DrawString(titleContent, new Font("微软雅黑", 10.0f, FontStyle.Bold)
                    , new SolidBrush(Color.FromArgb(51, 51, 102)),
                     rect
                    , sf);
            }
        }

        /// <summary>
        /// 会议议题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlKeyPoint_Paint(object sender, PaintEventArgs e)
        {
            // 
            e.Graphics.TranslateTransform(this.pnlKeyPoint.AutoScrollPosition.X, this.pnlKeyPoint.AutoScrollPosition.Y);

            drawTitile(e.Graphics, this.titleContentKeyPointTitleStr, this.pnlKeyPoint, this.titleContentHeight);
           
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int ShowScrollBar(IntPtr hWnd, int bar, int show);

        /// <summary>
        /// 投票
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlVote_Paint(object sender, PaintEventArgs e)
        {
            Control c = sender as Control;
            drawTitile(e.Graphics, this.titleContentVoteTitleStr, this.pnlVote, this.titleContentHeight);

            Graphics g = e.Graphics;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            #region 虚线
            Pen pen2 = new Pen(Color.FromArgb(195, 195, 195), 1);
            pen2.DashStyle = DashStyle.Custom;
            pen2.DashPattern = new float[] { 2f, 2f };
            g.DrawLine(pen2, new Point(0, this.pnlOptionResult.Location.Y - 1), new Point(this.pnlVote.Width, this.pnlOptionResult.Location.Y-1));
            #endregion

            /*
            int all = meetGrouplist.ChatListBox_group.Items[1].SubItems.Count;
            if (all == 0) all = 10;

            Font f = new Font("微软雅黑", 10f, FontStyle.Bold);

            int height = 10;
            Y += 20;
            int index = 0;
            int offX = 10;
            int line = c.Width * 50 / 100;
            Random r = new Random();
            foreach (string key in voteSelect.Keys)
            {

                SizeF tmp = g.MeasureString(key, f);
                height = tmp.ToSize().Height;
                g.DrawString(key, f, Brushes.Black,
                    new Rectangle(new Point(offX, Y), tmp.ToSize()));

                int tmp_X = tmp.ToSize().Width + offX * 2;
                int p = r.Next(1, 10);
                g.FillRectangle(new SolidBrush(Colorlist[index]),
                    new Rectangle(new Point(tmp_X, Y),
                    new Size((int)(line * p / all), height)));

                string tmpStrVal = p + "票  ";
                tmp = g.MeasureString(tmpStrVal, f);
                g.DrawString(tmpStrVal, f, Brushes.Black,
                    new Rectangle(new Point(tmp_X + line, Y), new Size((int)tmp.Width + 1, (int)tmp.Height + 1)));
                Y += height + 15;
                index++;
            }
            */
        }

        private void pnlCenter_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.CompositingQuality = CompositingQuality.HighQuality;

            Font f = new Font("微软雅黑", 13.0f, FontStyle.Bold);
            SizeF fs = g.MeasureString(this.titleStr_Emcee, f);
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            RectangleF fs_rect = new RectangleF(new PointF((this.pnlCenter.Width - fs.Width) / 2, 0), new SizeF(fs.Width, this.titleStr_EmceeHeight));
            Color c = Color.FromArgb(248, 158, 54);
            g.DrawString(this.titleStr_Emcee, f, new SolidBrush(c), fs_rect);

            int offSet_X = 4;

            float lineWidth = (this.pnlCenter.Width - offSet_X - fs_rect.Width) / 2;

            g.DrawLine(new Pen(c, 2), new PointF(offSet_X, this.titleStr_EmceeHeight / 2),
                new PointF(lineWidth, this.titleStr_EmceeHeight / 2));

            g.DrawLine(new Pen(c, 2), new PointF(offSet_X + lineWidth + fs_rect.Width, this.titleStr_EmceeHeight / 2),
            new PointF(offSet_X + lineWidth * 2 + fs_rect.Width, this.titleStr_EmceeHeight / 2));

            int y = pnlUp.Height + pnlUp.Location.Y + 1;

            g.DrawLine(new Pen(Color.FromArgb(195, 195, 195), 2),
                new Point(0, y),
                new Point(this.pnlCenter.Width, y));
        }

        private void pnlRight_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int y = btnAdd.Location.Y;
            int height = pnlRight.Height - y;

            g.FillRectangle(new SolidBrush(Color.FromArgb(242, 242, 242)),
                new Rectangle(new Point(0, y),
                new Size(this.pnlRight.Width, height)));

            g.DrawLine(new Pen(Color.FromArgb(195, 195, 195), 2),
                new Point(0, y - 1),
                new Point(this.pnlRight.Width, y - 1));
        }
        private void pnlSend_Paint(object sender, PaintEventArgs e)
        {
            drawTitile(e.Graphics, string.Empty, this.pnlSend, this.pnlSend.Height);
        }

        #endregion


        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            //this.pnlKeyPoint.AutoScrollPosition.X, 
            // this.pnlKeyPoint.AutoScrollPosition.Y += 2;
        }

    
      

        #region 初始化数据加载
        public delegate void InitDelegate(string meetingGuid);
            
        public void Init(string meetingGuid)
        {
            try
            {
                Dictionary<string, string> dict = meetingbll.retMeetingInfoByGuid(meetingGuid);
                if (dict != null)
                {
                    string val = dict["MEETING"];
                    if (!string.IsNullOrEmpty(val))
                    {
                        val = val.Substring(1, val.Length - 2);
                        this.meeting = JsonConvert.DeserializeObject<Meeting>(val);
                    }
                    val = dict["MEMBER"];
                    if (!string.IsNullOrEmpty(val))
                    {
                        val = val.Substring(1, val.Length - 2);
                        this.mmlist = JsonConvert.DeserializeObject<List<MeetingMember>>(dict["MEMBER"]);
                        SetUserInfoByUserId(this.mmlist, this.meeting.Emcee);
                    }

                    val = dict["OPTION"];
                    if (!string.IsNullOrEmpty(val))
                    {
                        val = val.Substring(1, val.Length - 2);
                        this.molist = JsonConvert.DeserializeObject<List<MeetingOption>>(dict["OPTION"]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据初始化失败 Init()"+ex.Message, "提示");
                
            }
           
        }

        public void CompleteInit(IAsyncResult ia)
        {
            try
            {
                if (this.meeting != null)
                {
                    //主题
                    this.titleContentStr = this.meeting.Subject;

                    //会议要点
                    keyPointStr.Add(this.meeting.Content);
                    this.Invoke(new Action(() =>
                    {
                        this.pnlContentStr.Size = retHeight_Panel(this.pnlContentStr);
                        this.pnlContent.AddControl(this.pnlContentStr);
                        this.pnlContentStr.Paint += pnlContentMain_Paint;
                    }));
                }
                if (this.mmlist != null)//会议成员ID
                {
                    this.MemberCount = this.mmlist.Count;//+1 是主持人
                    try
                    {
                        MeetingMember mmSelf = this.mmlist.Find(item => item.MemberId == SysParams.LoginUser.UID);
                        if (mmSelf.IsJoin == 1)//检查是否有投票权限
                        {
                            btnVote.Tag = false;
                        }
                    }
                    catch (Exception ex)
                    {


                    }

                }
                if (this.molist != null)//投票选项
                {
                    for (int i = 0; i < this.molist.Count; i++)
                    {
                        if (this.molist[i].Letter != null)
                        {
                            this.voteList.Add(this.molist[i]);
                        }
                    }
                    this.Invoke(new Action(() =>
                    {
                        this.addVote(this.voteList);
                    }));
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("CompleteInit()错误：" + ex.Message, "提示");
            }
           
        }
        #endregion

        #region 会议要点
        private Size retHeight_Panel(Control c)
        {
            StringFormat sf = new StringFormat(StringFormatFlags.DisplayFormatControl | StringFormatFlags.FitBlackBox);
            Font f = new Font("微软雅黑", 9.0f);
            string s = string.Empty;
            int offSet_y = 5;
            int heightAll = 0;
            Size pnlSize = Size.Empty;


            using (Graphics g = c.CreateGraphics())
            {
                SizeF size = g.MeasureString("test", f, this.pnlContent.Width - this.pnlContent.selfScroll().Width);
                int height = (int)size.Height + 1;
                heightAll = (height + offSet_y) * this.keyPointStr.Count;
                int width = this.pnlContent.Width - this.pnlContent.selfScroll().Width;
                pnlSize = new Size(width, heightAll);

                f.Dispose();
                sf.Dispose();
            }
           
            return pnlSize;

        }

        private void pnlContentMain_Paint(object sender, PaintEventArgs e)
        {
            if (this.keyPointStr != null && this.keyPointStr.Count > 0)
            {
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                StringFormat sf = new StringFormat(StringFormatFlags.DisplayFormatControl | StringFormatFlags.FitBlackBox);
                Font f = new Font("微软雅黑", 9.0f);
                string s = string.Empty;
                int offSet_y = 5;
                int offSet_width = 2;
                int heightAll = 0;
                //g.TranslateTransform(this.pnlKeyPoint.AutoScrollPosition.X, this.pnlKeyPoint.AutoScrollPosition.Y);
                using (SolidBrush sb = new SolidBrush(Color.Black))
                {
                    for (int i = 0; i < this.keyPointStr.Count; i++)
                    {
                        SizeF size = g.MeasureString(this.keyPointStr[i], f, this.pnlContentStr.Width - offSet_width - pnlContent.selfScroll().Width, sf);
                        //s += this.keyPointStr[i]+@"/r/n";
                        int height = (int)size.Height + 1;
                        s = this.keyPointStr[i];
                        Rectangle rect = new Rectangle(new Point(5, offSet_y * (1 + i) + heightAll),
                               new Size(this.pnlContentStr.Width - offSet_width - pnlContent.selfScroll().Width, height));
                        g.DrawString(s, f, Brushes.Black, rect);
                        heightAll += height;
                    }
                }
                if (this.pnlContentStr.Height < heightAll)
                {
                    this.pnlContentStr.Height = heightAll + 10;
                    this.pnlContent.selfReSize();
                }
                f.Dispose();
                sf.Dispose();
            }
        }

        private void pnlContentMain_SizeChanged(object sender, EventArgs e)
        {
            pnlContent.chkScrollBar();
        }
        #endregion

        #region 投票
        /// <summary>
        /// 添加投票选项
        /// </summary>
        /// <param name="list"></param>
        public void addVote(List<MeetingOption> list)
        {
            if (list != null && list.Count > 0)
            {
                int index = 0;
                foreach (MeetingOption key in list)
                {
                    ItemCheckBox icb = new ItemCheckBox();
                    icb.ContentStr = key.Letter + "、" + key.Content;
                    icb.Width = pnlOption.Width - 20;
                    icb.Height = 30;
                    icb.IsChecked = false;
                    icb.Location = new Point(10, icb.Height * index + 10);
                    icb.FontColor = Color.Black;
                    icb.Name = key.Letter;
                    icb.Tag = key;
                    //voteSelect[key.Letter] = 0;
                    optionlist.Add(icb);
                    
                    pnlOption.AddControl(icb);

                    ItemLineColumn ilc = new ItemLineColumn();
                    ilc.Size = new Size(pnlVote.Width - 30, 20);
                    ilc.CurrentIndex = key.VoteCount;
                    ilc.MaxIndex = MemberCount;
                    ilc.Text = key.Letter + ". ";
                    ilc.Tag = key;
                    ilc.Location = new Point(0, ilc.Height * index + 10);
                    voteResultList.Add(ilc);
                    pnlOptionResult.AddControl(ilc);

                    index++;
                }

                //for (int i = 0; i < list.Count; i++)
                //{
                //    //if (list.Count > tmpval.Length)
                //    //{
                //    //    break;
                //    //}
                //    ItemCheckBox icb = new ItemCheckBox();
                //    icb.ContentStr = tmpval[i] + "、" + list[i];
                //    icb.Width = pnlOption.Width - 20;
                //    icb.Height = 30;
                //    icb.IsChecked = false;
                //    icb.Location = new Point(10, titleContentHeight + icb.Height * i + 10);
                //    icb.FontColor = Color.Black;
                //    icb.Name = tmpval[i];
                //    voteSelect[tmpval[i]] = 0;
                //    pnlOption.AddControl(icb);
                //}
            }

        }
        #endregion

        #region 用户数据获取

        /// <summary>
        /// 根据主持人
        /// </summary>
        /// <param name="memberlist"></param>
        /// <param name="EmceeId"></param>
        public void SetUserInfoByUserId(List<MeetingMember> memberlist,int EmceeId)
        {
            string userIdList = string.Empty;
            if (memberlist != null && memberlist.Count > 0)
            {
                memberlist.ForEach(item => userIdList +=item.MemberId + ",");
            }
            if (EmceeId != 0)
            {
                userIdList += EmceeId + ",";
            }

            if (!string.IsNullOrEmpty(userIdList))
            {
                userIdList = userIdList.Substring(0, userIdList.Length - 1);
                DataTable dtUser = meetingbll.retMeetingUserInfoByGuid(userIdList);
                if (dtUser != null && dtUser.Rows.Count > 0)
                {
                    for (int i = 0; i < dtUser.Rows.Count; i++)
                    {
                        DataRow dr = dtUser.Rows[i];
                        string uid = dr["useruid"].ToString();
                        string nickName = string.Empty;
                        string realName = dr["truename"] != DBNull.Value ? dr["truename"].ToString() : string.Empty; 
                        string Company = dr["Company"] != DBNull.Value ? dr["Company"].ToString() : string.Empty;
                        ChatListSubItem.UserStatus uStatus = ChatListSubItem.UserStatus.OffLine;
                        if (uid == SysParams.LoginUser.UID.ToString())
                        {
                            uStatus = ChatListSubItem.UserStatus.Online;
                        }
                     
                        string ImageUrl = dr["head"] != DBNull.Value ? dr["head"].ToString() : string.Empty;
                        string userJId=dr["user_server_account"] != DBNull.Value ? dr["user_server_account"].ToString() : string.Empty;
                        int groupIndex = 1;
                        if (uid == EmceeId.ToString())//主持人信息
                        {
                            groupIndex = 0;
                            this.meeting.EmceeJid = userJId;
                            this.meeting.Emcee=int.Parse(uid);
                            this.meeting.EmceeName = realName;
                            this.meeting.EmceeCompany = Company;
                        }
                        else //参与人员
                        {
                            MeetingMember m= memberlist.Find(item => item.MemberId == int.Parse(uid));
                            m.UserJid = userJId;
                           
                        }

                        this.AddUserInfo(groupIndex, userJId, nickName, realName, Company, uStatus, null, ImageUrl);
                    }
                }

            }
            //retMeetingUserInfoByGuid(string UserIdList)
        }

        /// <summary>
        /// 加载用户
        /// </summary>
        /// <param name="GroupIndex"></param>
        /// <param name="uid"></param>
        /// <param name="nickName"></param>
        /// <param name="realName"></param>
        /// <param name="Company"></param>
        /// <param name="uStatus"></param>
        /// <param name="headImage"></param>
        /// <param name="ImageUrl"></param>
        public void AddUserInfo(int GroupIndex, string uid, string nickName, string realName, string Company, ChatListSubItem.UserStatus uStatus, Image headImage, string ImageUrl)
        {
            /*
            ChatListSubItem item = new ChatListSubItem("id", "nicname", namelist[tmp_index], companylist[tmp_index],
              ChatListSubItem.UserStatus.Online, PlatformType.PC,
          Image.FromFile(TempFile.retImgHeadByUserName_Localhost("")));
            item.HeadImage = Image.FromFile(@"c:\head" + tmp_index + ".jpg");
            meetGrouplist.ChatListBox_group.Items[1].SubItems.Add(item);
            */
            Image userHead = SysParams.defaultHead;
            if (headImage != null)
            {
                userHead = headImage;
            }
            ChatListSubItem item = new ChatListSubItem(uid, nickName, realName, Company,
              uStatus, PlatformType.PC, userHead);
            meetGrouplist.ChatListBox_group.Items[GroupIndex].SubItems.Add(item);

            if (!string.IsNullOrEmpty(ImageUrl) && userHead == SysParams.defaultHead)
            {
                //下载头像
                loadUserHead lu = SetUserHeadByUserJid;
                lu.BeginInvoke(uid,  ImageUrl, GroupIndex, CompleteHead, GroupIndex);
            }


        }

        public delegate void loadUserHead(string userJid, string ImageUrl, int groupIndex);

        public void CompleteHead(IAsyncResult iar)
        {
            int GroupIndex = Convert.ToInt16(iar.AsyncState);
            this.Invoke(new Action(() => {
                meetGrouplist.ChatListBox_group.Invalidate();
            }));
        }

        /// <summary>
        /// 头像下载
        /// </summary>
        /// <param name="userJid"></param>
        /// <param name="ImageUrl"></param>
        /// <param name="groupIndex"></param>
        public void SetUserHeadByUserJid(string userJid, string ImageUrl,int groupIndex)
        {
            string imgurl = SysParams.Head_ImageInfo + ImageUrl;
            byte[]  headData = TempFile.DownLoad_Head_2(imgurl, userJid);
            if (headData != null)
            {
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream(headData))
                {
                    System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                    ChatListSubItem item = this.retSubItemByUserId(groupIndex, userJid);
                    lock (item)
                    {
                        if (item != null)
                        {
                            item.HeadImage = img;

                        }
                    }
                }
            }
        }

        /// <summary>
        /// 反馈相应的头像控件
        /// </summary>
        /// <param name="GroupIndex"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public ChatListSubItem retSubItemByUserId(int GroupIndex, string uid)
        {
            if (meetGrouplist != null)
            {
                foreach (ChatListSubItem item in meetGrouplist.ChatListBox_group.Items[GroupIndex].SubItems)
                {
                    if (item.ID == uid)
                    {
                        return item;
                    }
                }
            }
            return null;
        }

        #endregion;



        #region windows功能键
        private void btnClose_Click(object sender, EventArgs e)
        {
            if (SysParams.LoginUser.UID == this.meeting.Emcee)
            {
                if (MessageBox.Show("是否关闭会议室?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    //修改会议室状态为关闭
                    MeetingCloseDelegate mcd = CloseMeeting;
                    mcd.BeginInvoke(null, null);
                    this.Close();
                }
            }
            else {
                this.Close();
            }
        }

        public void CloseMeeting()
        {
            meetingbll.UpdateMeetingStatus(MeetingStatus.closed, this.meetingGuid);
            meetingbll.CloseMeeting(this.meeting_JId);
        }
        private void ibtnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        #endregion;

        #region 资源释放
        private void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            fDispose();
        }

        //资源释放
        private void fDispose()
        {
            try
            {
                foreach (ChatListSubItem item in meetGrouplist.ChatListBox_group.Items[0].SubItems)
                {
                    if (item.HeadImage != null && item.HeadImage != SysParams.defaultHead)
                    {
                        item.HeadImage.Dispose();
                    }
                }
                foreach (ChatListSubItem item in meetGrouplist.ChatListBox_group.Items[1].SubItems)
                {
                    if (item.HeadImage != null && item.HeadImage != SysParams.defaultHead)
                    {
                        item.HeadImage.Dispose();
                    }
                }
                meetGrouplist.Dispose();

                if (this.chatBoxSend != null && this.chatBoxSend.GetContent().ForeignImageDictionary != null)
                {
                    Dictionary<uint, Image> dict = this.chatBoxSend.GetContent().ForeignImageDictionary;
                    foreach (var v in dict.Keys)
                    {
                        try
                        {
                            dict[v].Dispose();//释放 内存中的图片
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }

                if (this.optionlist != null && this.optionlist.Count > 0)
                {
                    for (int i = 0; i < this.optionlist.Count; i++)
                    {
                        if (this.optionlist[i] != null)
                        {
                            this.optionlist[i].Dispose();
                        }
                    }
                    this.optionlist.Clear();
                }

                if (this.voteResultList != null && this.voteResultList.Count > 0)
                {
                    for (int i = 0; i < this.voteResultList.Count; i++)
                    {
                        this.voteResultList[i].Dispose();
                    }
                }

                meetingbll.ExitMeeting(this.meeting_JId, SysParams.LoginUser.UID.ToString());

                StaticClass.xmppClient.MessageFilter.Remove(this.meeting_JId);
                StaticClass.xmppClient.PresenceFilter.Remove(this.meeting_JId);
                //StaticClass.xmppClient.MessageFilter.Clear();//
                //StaticClass.xmppClient.PresenceFilter.Clear(); //
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region 测试数据

        private void btnSend_Click(object sender, EventArgs e)
        {
            // Make sure that the users send no empty messages
            if (chatBoxSend.Text.Length > 0)
            {


                #region 组织数据
                ChatBoxContent cbc = this.chatBoxSend.GetContent();
                Dictionary<uint, Image> dictImage = cbc.ForeignImageDictionary;
                string html = cbc.Text;
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
                            
                            string htmlStr = string.Format("<img ondblclick=\"showPic_html_Remote(this)\" onload=\"AutoResizeImageForImg(this)\" style=\"max-width:400px;\" src=\"{0}\"/>", new string[] { imgName });

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
                #endregion;
                //SysParams.Sys_Normal
                sendMessage_meeting(html, SysParams.Sys_Normal);

                chatBoxSend.Clear();
            }



        }

        private void sendMessage_meeting(string html,string type)
        {
            var msg = new Matrix.Xmpp.Client.Message
            {
                Type = Matrix.Xmpp.MessageType.GroupChat,
                To = this.meeting_JId,
                Body = type+html,
                From = SysParams.LoginUser.GetJID()

            };
            msg.XMucUser = new Matrix.Xmpp.Muc.User.X();
            msg.XMucUser.Item = new Matrix.Xmpp.Muc.User.Item();
            msg.XMucUser.Item.Jid = SysParams.LoginUser.GetJID();
            StaticClass.xmppClient.Send(msg);
        }

   



   
        private void btnAdd_Click(object sender, EventArgs e)
        {

            UserListForm ul = new UserListForm(UserListType.findMember, addUser);
            ul.ShowDialog();
        }
        /// <summary>
        /// 添加 参与人员
        /// </summary>
        /// <param name="uid">用户ID</param>
        /// <param name="nickName"></param>
        /// <param name="displayName">展示名称</param>
        /// <param name="personMsg">单位</param>
        /// <param name="uStatus">用户状态</param>
        /// <param name="pt">用户设备</param>
        /// <param name="headImage">用户头像</param>
        /// <param name="u">添加方式</param>
        public void addUser(string uid, string nickName, string displayName,
            string personMsg, ChatListSubItem.UserStatus uStatus,
            PlatformType pt, Image headImage, UserListType u, string userJid)
        {
            //校验人员是否重复
            if (mmlist != null && mmlist.Count > 0 && mmlist.Exists(item => item.MemberId == int.Parse(uid)))
            {
                return;
            }
           
            #region 数据库添加新成员
         
            MeetingMember m = new MeetingMember();
            m.Mmid = Guid.NewGuid().ToString().Replace("-", "");
            m.MemberId =int.Parse(uid);
            m.MeetingId = this.meeting.MeetingGuid;
            List<MeetingMember> mlist = new List<MeetingMember>();
            mlist.Add(m);
            meetingbll.AddMember(mlist);
            this.mmlist.Add(m);
            voteResultAdjust();
            #endregion;

            #region 界面添加新成员
            if (headImage != SysParams.defaultHead)
            {
                headImage = (Image)headImage.Clone();
            }
            ChatListSubItem clsl = new ChatListSubItem(userJid, nickName, displayName, personMsg, uStatus, headImage);
            if (u == UserListType.findMember)
            {
                bool isExist = false;
                foreach (ChatListSubItem item in meetGrouplist.ChatListBox_group.Items[1].SubItems)//去重
                {
                    if (item.ID == uid)
                    {
                        isExist = true;
                        break;
                    }
                }
                if ((meetGrouplist.ChatListBox_group.Items[0].SubItems.Count > 0 //不等于主持人
                    && meetGrouplist.ChatListBox_group.Items[0].SubItems[0].ID == uid))
                {
                    isExist = true;
                }
                if (!isExist)
                {
                    meetGrouplist.ChatListBox_group.Items[1].SubItems.Add(clsl);
                }
            }
            #endregion

            #region 发送邀请信息
            Meeting meeting_tmp = new Meeting();
            meeting_tmp.MeetingId = this.meeting.MeetingId;
            meeting_tmp.MeetingGuid = this.meeting.MeetingGuid;
            meeting_tmp.RoomName = this.meeting.RoomName;
            meeting_tmp.Emcee = this.meeting.Emcee;
            string html = JsonConvert.SerializeObject(meeting_tmp);
            Matrix.Xmpp.Client.Message msg = new Matrix.Xmpp.Client.Message(userJid, SysParams.Sys_Meeting_Invite+html, "", "");
            //msg.XHtml.Add(
            StaticClass.xmppClient.Send(msg);
            //sendMessage_meeting(html, SysParams.Sys_Meeting_Invite);
            #endregion;
        }
       
        
        #endregion;

        #region 基本功能区域 截图 图片 文字大小 手写板
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

        private void ibtnPicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog openImageDlg = new OpenFileDialog();
            openImageDlg.Filter = "所有图片(*.bmp,*.gif,*.jpg,*.png)|*.bmp;*.gif;*.jpg;*.png";
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

        private void ibtnScreenshots_Click(object sender, EventArgs e)
        {
            Common.ScreenShot(this.chatBoxSend);
        }

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
        #endregion;

        #region 投票
        private void btnVote_Click(object sender, EventArgs e)
        {
            //this.browser_Emcee.ShowDevTools();
            //return;
            if (btnVote.Tag!=null && Convert.ToBoolean(btnVote.Tag) == false)
            {
                MessageBox.Show("已参与过投票，无法再次进行投票。", "提示");
                return;
            }
            if (optionlist != null && optionlist.Count > 0)
            {
                if (MessageBox.Show("是否确认投票选项？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        for (int i = 0; i < optionlist.Count; i++)
                        {
                            if (optionlist[i] != null && optionlist[i].IsChecked)
                            {

                                MeetingOption mo = optionlist[i].Tag != null ? (MeetingOption)optionlist[i].Tag : null;
                                if (mo != null)
                                {
                                    //1 更新数据库
                                    meetingbll.UpdateVoteCount(mo.MoId, this.meeting.MeetingGuid, SysParams.LoginUser.UID.ToString());

                                    //2 更新界面
                                    this.sendMessage_meeting(mo.MoId, SysParams.Sys_Vote);
                                }

                            }
                        }
                        btnVote.Tag = false;
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("投票失败。", "提示");
                    }
                    
                   
                }
            }
        }

        public delegate void OptionVoteDelegate(MeetingOption mo);

        public void OptionVote(MeetingOption mo)
        {
            //1 更新数据库
            meetingbll.UpdateVoteCount(mo.MoId, this.meeting.MeetingGuid, SysParams.LoginUser.UID.ToString());

            //2 更新界面
            this.sendMessage_meeting(mo.MoId, SysParams.Sys_Vote);

        }


        #endregion;

        private void btnMeetingOver_Click(object sender, EventArgs e)
        {
            if (SysParams.LoginUser.UID == this.meeting.Emcee)
            {
                if (MessageBox.Show("是否关闭会议室?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    //修改会议室状态为关闭
                    MeetingCloseDelegate mcd = CloseMeeting;
                    mcd.BeginInvoke(null, null);
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }
    }
}
