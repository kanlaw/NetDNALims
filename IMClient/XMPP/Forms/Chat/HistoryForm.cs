using CefSharp.WinForms;
using IMClient.Controls.Base;
using IMClient.Logic;
using JustLib.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IMClient.XMPP.Forms
{
    public partial class HistoryForm : FrameBase
    {
        ChatBoxContent chatBoxSend = null;
        Chat chatClass = null;
        Common comm = new Common();
        public ChromiumWebBrowser browser;

        private string user1 = string.Empty;
        private string user2 = string.Empty;
        private ChatFormForWeb mform = null;

        public HistoryForm(string user1,string user2,ChatFormForWeb mform)
        {
            InitializeComponent();
            chatClass = new Chat(this);
            this.chatBoxSend = mform.ChatBoxSend;
            #region 添加浏览器
            this.user1 = user1;
            this.user2 = user2;
            this.mform = mform; 
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
                comm.RegObjectTOCEF(browser, comm, "jsOBJ");
                pnlContent.Controls.Add(browser);

                //browser.DragDrop += Control_DragDrop;
                //browser.DragEnter += Control_DragEnter;
                //browser.AllowDrop = true;
            }
            #endregion
        }

        private void Browser_FrameLoadEnd(object sender, CefSharp.FrameLoadEndEventArgs e)
        {
            add(this.user1, this.user2);
        }

        public void add(string user1,string user2)
        {
            DataTable dt = Common.retTalkHistory(user1, user2);
            CefSharp.IFrame ifm = browser.GetBrowser().MainFrame;
            this.comm.CallJS(browser, " clearAll()");
            // this.CallJS(browser, addFunctionStr);
            if (dt != null && dt.Rows.Count > 0)
            {
                string messageDate = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    string fromid = dr["fromjid"].ToString();
                    string toid = dr["tojid"].ToString();
                    string contentStr = dr["body"].ToString();
                    Int64 tmpval = Convert.ToInt64(dr["SENTDATE"].ToString());
                    DateTime d= new DateTime(1970, 1, 1).ToLocalTime().AddMilliseconds(long.Parse(dr["SENTDATE"].ToString()));
                    string dateTime= d.ToString("yyyy-MM-dd HH:mm:ss");
                    ChatBoxContent content = new ChatBoxContent();
                    content.Text = contentStr;
                    string messType = SysParams.retSysMessage(contentStr);
                    if (messageDate != d.ToString("yyyy-MM-dd"))
                    {
                        //添加间隔线和日期

                        messageDate = d.ToString("yyyy-MM-dd");
                        this.chatClass.AppendHistoryLine(this.browser, messageDate);
                    }
                    this.chatClass.AppendChatBoxContentAll(this.browser, messType, fromid, d, content,
                        this.mform.MineID, this.mform.FriendID, Color.SeaGreen, true, true);

                    //this.AppendChatBoxContentAll(messType, fromid, d, content, Color.SeaGreen, true, true);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            close();
        }

        public void close()
        {
            try
            {
                this.mform.Width = this.mform.Width / 2;
                this.mform.Controls.Remove(this);
                this.mform.changeSize(this.mform.Width, 0);
                this.Dispose();
                GC.Collect();
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
        }

       

        private void HistoryForm_Shown(object sender, EventArgs e)
        {
            this.ibtnSF.Location = new Point(0, (this.Height - this.ibtnSF.Height) / 2);
        }

        private void HistoryForm_ResizeEnd(object sender, EventArgs e)
        {
            this.ibtnSF.Location = new Point(0, (this.Height - this.ibtnSF.Height) / 2);
        }
    }
}
