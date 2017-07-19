using DNANET.Data;
using IMClient.Controls.Base;
using IMClient.Logic;
using IMClient.Logic.Contorl;
using IMClient.Logic.IDataTable;
using NetDLims.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace IMClient.XMPP.Forms
{
    public partial class MeetingListForm : FrameBase
    {
        DataTable dt = null;
        private string titleStr = "网络会议室";
        Point pt = Point.Empty;
        private string titleStr_Search = "网络会议室";
        Font f_Search = new Font("微软雅黑", 15.0f, FontStyle.Bold | FontStyle.Italic);
        StringFormat sf_Search = new StringFormat();
        private DataTable searchDt = null;
        MeetingLogic meetingbll = new MeetingLogic();
        /// <summary>
        /// 查询条件
        /// </summary>
        private string query = string.Empty;

        /// <summary>
        /// 每页显示数据量
        /// </summary>
        private int pageSize = 50;


        /// <summary>
        /// 当前页数
        /// </summary>
        private int currentSize = 0;

        public MeetingListForm()
        {

            InitializeComponent();
           

            InitDataTable();
            SetScroll();

        }

        private void SetScroll()
        {
            pt = new Point(this.flowLayoutPanel1.AutoScrollPosition.X, this.flowLayoutPanel1.AutoScrollPosition.Y);
            this.ScrollDt.Minimum = 0;
            this.ScrollDt.Maximum = this.flowLayoutPanel1.VerticalScroll.Maximum;//.DisplayRectangle.Height;
            this.ScrollDt.LargeChange = this.flowLayoutPanel1.VerticalScroll.LargeChange; //ScrollDt.Maximum / ScrollDt.Height + this.flowLayoutPanel1.Height;
            this.ScrollDt.SmallChange = 15;
            this.ScrollDt.Value = Math.Abs(this.flowLayoutPanel1.AutoScrollPosition.Y);
        }


        public void InitDataTable()
        {
          
            this.idtUserinfo.Sf_Title.Alignment = StringAlignment.Center;
            this.idtUserinfo.Sf_Title.LineAlignment = StringAlignment.Center;
            this.idtUserinfo.Sf_Title.Trimming = StringTrimming.EllipsisCharacter;

            this.idtUserinfo.Sf_Content.Alignment = StringAlignment.Center;
            this.idtUserinfo.Sf_Content.LineAlignment = StringAlignment.Center;
            this.idtUserinfo.Sf_Content.Trimming = StringTrimming.EllipsisCharacter;
            this.idtUserinfo.TitleHeight = 35;
            this.idtUserinfo.ColHeight = 50;
            //this.idtUserinfo.IsTitle = false;
            this.idtUserinfo.Bg_SelectColor = Color.FromArgb(200,200,200);
            this.idtUserinfo.Bg_MoveColor = Color.FromArgb(190,190,190);
            this.idtUserinfo.borderColor = Color.FromArgb(230, 230, 230);
            this.idtUserinfo.Font_title.Dispose();
            this.idtUserinfo.Font_title = new Font("微软雅黑", 11.0f, FontStyle.Bold);
            this.idtUserinfo.borderStyle = IMClient.Controls.borderStyleSelf.bordrNone;
            this.idtUserinfo.B_TitleColor = new LinearGradientBrush(new Rectangle(0, 0, this.idtUserinfo.Width, this.idtUserinfo.TitleHeight),
                Color.FromArgb(251, 251, 251), Color.FromArgb(240, 240, 240),LinearGradientMode.Vertical);
            ColClass col = new ColClass("序号", "rn", 5);
            this.idtUserinfo.colList.Add(col);
            col = new ColClass("会议室名称", "roomname", 20);
            this.idtUserinfo.SplitLine_Content_Length = this.idtUserinfo.Width - 24;
            this.idtUserinfo.SplitLine_Title_H_Length = this.idtUserinfo.Width ;
            this.idtUserinfo.SplitLine_Title_V_Length = this.idtUserinfo.TitleHeight-10;

            this.idtUserinfo.colList.Add(col);
            col = new ColClass("会议主题", "subject", 20);
            this.idtUserinfo.colList.Add(col);
            col = new ColClass("会议类型", "isPublicStr", 10);
            this.idtUserinfo.colList.Add(col);
            col = new ColClass("主持人", "truename", 10);
            this.idtUserinfo.colList.Add(col);
            col = new ColClass("参与人数", "MemberLimit", 10);
            this.idtUserinfo.colList.Add(col);

            col = new ColClass("状态", "meetingStatusStr", 10);
            this.idtUserinfo.colList.Add(col);

            col = new ColClass("创建时间", "createtime", 15);
            this.idtUserinfo.colList.Add(col);
          

            this.idtUserinfo.Height = pageSize * this.idtUserinfo.ColHeight + this.idtUserinfo.TitleHeight;
            this.idtUserinfo.Invalidate();
        }

        private void UserListForm_Shown(object sender, EventArgs e)
        {
            //this.idtUserinfo.BindData(dt);
            SetScroll();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            
            if (this.searchDt != null)
            {
                this.idtDispose(this.searchDt);
                this.searchDt.Clear();
            }
            query = txtMeetingInfo.Text;
            currentSize = 0;
            try
            {
                DataTable dt = meetingbll.retMeetingInfo(query, currentSize, pageSize);

                //this.pageSize = int.Parse(dt.TableName);
                MeetingdataFomat(dt);
                this.idtUserinfo.BindData(dt);
            }
            catch (Exception ex)
            {

                MessageBox.Show("查询失败。", "提示");
            }
           
        }

        private void MeetingdataFomat(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                if (!dt.Columns.Contains("ispublicStr"))
                {
                    dt.Columns.Add("ispublicStr", typeof(SpecialContent));
                }
                if (!dt.Columns.Contains("meetingStatusStr"))
                {
                    dt.Columns.Add("meetingStatusStr", typeof(SpecialContent));
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //会议室开放类型
                    DataRow dr = dt.Rows[i];
                    Color c = Color.FromArgb(43, 145, 175);
                    string val = "公开型";
                    if (dr["ispublic"].ToString() == "0")
                    {
                        c = Color.Red;
                        val = "加密型";
                    }
                    if (dr["MemberLimit"].ToString() == "0")
                    {
                        dr["MemberLimit"] = "无限制";
                    }
                    SpecialContent sc = new SpecialContent();
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    sc.AddsFontlist(new Font("微软雅黑", 9.0f),
                        c, sf, val);
                    dr["ispublicStr"] = sc;

                    //会议室 状态
                    if (dr["meetingstatus"].ToString() == "0")
                    {
                        c = Color.Orange;
                        val = "准备中";
                    }
                    else if (dr["meetingstatus"].ToString() == "1")
                    {
                        c = Color.Red;
                        val = "进行中";
                    }
                    else if (dr["meetingstatus"].ToString() == "2")
                    {
                        c = Color.Black;
                        val = "关闭";
                    }
                    sc = new SpecialContent();
                    sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    sc.AddsFontlist(new Font("微软雅黑", 9.0f),
                        c, sf, val);
                    dr["meetingStatusStr"] = sc;

                }
            }
        }


        private DataTable InitDt()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("number", typeof(SpecialContent));
            dt.Columns.Add("headurl", typeof(string));
            dt.Columns.Add("jid", typeof(string));
            dt.Columns.Add("uid", typeof(string));
            dt.Columns.Add("username", typeof(SpecialContent));
            dt.Columns.Add("usernameSelf", typeof(string));
            dt.Columns.Add("realname", typeof(string));
            dt.Columns.Add("gender", typeof(string));
            dt.Columns.Add("company", typeof(string));
            dt.Columns.Add("Phone", typeof(string));
            dt.Columns.Add("age", typeof(int));
            dt.Columns.Add("btnF", typeof(SpecialContent));
            dt.Columns.Add("head", typeof(SpecialContent));
            dt.Columns.Add("mail", typeof(SpecialContent));
            SpecialContent sc = new SpecialContent();
            sc.imageAlign = ImageAlign.center;
            sc.scType = SpecialContentType.image;
            dt.Columns["head"].DefaultValue = sc;

            SpecialContent sc2 = new SpecialContent();
            sc2.imageAlign = ImageAlign.center;
            sc2.scType = SpecialContentType.image;
            sc2.drawImage = (Image)Properties.Resources.add_Users_down_03.Clone();
            sc2.drawImage_Hover = (Image)Properties.Resources.add_Users_over_03.Clone();
            sc2.imageSize = sc2.drawImage.Size;
            dt.Columns["btnF"].DefaultValue = sc2;
            return dt;
        }

        public DataTable SetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("headurl", typeof(string));
            dt.Columns.Add("jid", typeof(string));
            dt.Columns.Add("uid", typeof(string));
            dt.Columns.Add("username", typeof(SpecialContent));
            dt.Columns.Add("gender", typeof(int));
            dt.Columns.Add("company", typeof(string));
            dt.Columns.Add("Phone", typeof(string));
            dt.Columns.Add("age", typeof(int));
            dt.Columns.Add("btnF", typeof(string));
            dt.Columns.Add("head", typeof(SpecialContent));
            SpecialContent sc = new SpecialContent();
            sc.imageAlign = ImageAlign.center;
            sc.scType = SpecialContentType.image;
            dt.Columns["head"].DefaultValue = sc;
            Random r = new Random();

            for (int i = 0; i < 10; i++)
            {
                DataRow dr = dt.NewRow();
                dr["headurl"] = @"E:\head-g.jpg";
                dr["jid"] = "jid" + i;
                dr["uid"] = "uid" + i;
                SpecialContent sctmp = new SpecialContent();
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                sctmp.AddsFontlist(new Font("微软雅黑", 9.0f),
                    Color.FromArgb(43, 145, 175), sf, "jid" + i);
                sctmp.AddsFontlist(new Font("微软雅黑", 9.0f),
                   Color.FromArgb(43, 145, 175), sf, "uid" + i);

                sctmp.AddsFontlist(new Font("微软雅黑", 9.0f),
               Color.FromArgb(43, 145, 175), sf, "username" + i);
                dr["username"] = sctmp;

                dr["gender"] = r.Next(0, 1);
                dr["Phone"] = "Phone" + i;
                dr["company"] = "company" + i;
                dr["age"] = r.Next(0, 99);
                dr["btnF"] = "btnF" + i;
                dt.Rows.Add(dr);
            }
            return dt;
        }

        private void idtUserinfo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.idtUserinfo.selectedIndex;
            if (this.idtUserinfo.dataTable != null
                && this.idtUserinfo.dataTable.Rows.Count > 0 && index >= 0)
            {
                DataRow dr = this.idtUserinfo.dataTable.Rows[index];
                enterMeeting(dr);
  
            }
        }

        //进入会议室
        public void enterMeeting(DataRow dr)
        {
            int ispublic = Convert.ToInt32(dr["ispublic"]);
            string roomName = dr["roomname"] != DBNull.Value ? dr["roomname"].ToString() : string.Empty;
            bool isCommit = true;
            bool isEmcee = false;
            int limit = 0;
            try
            {
                limit = Convert.ToInt32(dr["memberlimit"]);
            }
            catch (Exception ex)
            {

                
            }
              
            string meetingGuid = dr["meetingGuid"].ToString();
            if (limit > 0)//
            {
                bool isAttend= meetingbll.AttendMeeting(meetingGuid, SysParams.LoginUser.UID.ToString(), limit.ToString());
                if (!isAttend)
                {
                    MessageBox.Show("会议室人员已满，无法进入。", "提示");
                    return;
                }
                //int currentMemberCount = Convert.ToInt32(dr["memberCount"]);
                //if(
            }
            if (Convert.ToInt32(dr["meetingStatus"]) == 0)//准备中
            {
                if (Convert.ToInt32(dr["emceeid"]) == SysParams.LoginUser.UID)//主持人
                {
                    ispublic = 1;
                    isEmcee = true;
                    //meetingbll.creatMeeting(Convert.ToString(dr["meetingGuid"]));//创建会议室
                }
                else
                {
                    MessageBox.Show("会议正在准备中，无法进入。", "提示");
                    return;
                }
            }
            else if (Convert.ToInt32(dr["meetingStatus"]) == 1)//进行中
            {
                switch (ispublic)//会议室 是否公开
                {
                    case 0://加密型 判断密码
                        MeetingPWD mp = new MeetingPWD(roomName);
                        mp.ShowDialog();
                        if (!string.IsNullOrEmpty(mp.pwd))
                        {
                            string pwd_tmp = dr["pwd"] != DBNull.Value ? dr["pwd"].ToString() : string.Empty;
                            if (pwd_tmp != mp.pwd)
                            {
                                isCommit = false;
                            }
                        }
                        else
                        {
                            isCommit = false;
                        }
                        break;
                }
            }
            else
            {
                MessageBox.Show("会议已结束，无法进入。", "提示");
                return;
            }


            if (isCommit)//公开 根据 MeetingGuid 直接进入
            {

               
                //Dictionary<string,string> dict=  meetingbll.retMeetingInfoByGuid(meetingGuid);
                MeetingForm mf = new MeetingForm(meetingGuid, isEmcee);
                mf.ShowDialog();
                this.Close();
            }
        }


      


        private void idtUserinfo_MouseClick(object sender, MouseEventArgs e)
        {
            //MessageBox.Show(this.idtUserinfo.selectedIndex_Col.ToString());
            if (this.idtUserinfo.selectedIndex_Col == this.idtUserinfo.colList.Count - 1)
            {
                //AddUser();
            }
        }

 

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.FillRectangle(new SolidBrush(SysParams.Paint_title_Color),
                new Rectangle(new Point(0, 0), new Size(this.Width, SysParams.Paint_title_Height)));

            g.DrawString(this.titleStr, SysParams.Paint_title_Font,
                 new SolidBrush(SysParams.Paint_title_Font_Color), SysParams.Paint_title_Point);
            
        }

        private void ScrollDt_Scroll(object sender, EventArgs e)
        {
            flowLayoutPanel1.AutoScrollPosition = new Point(0, ScrollDt.Value);
            //vScrollBar1.Value = ScrollDt.Value;
            ScrollDt.Invalidate();
            Application.DoEvents();
            //private void customScrollbar1_Scroll(object sender, EventArgs e)
            //{
               
            //    Debug.WriteLine("vscroll: " + vScrollBar1.Value.ToString() + "  custom: " + customScrollbar1.Value.ToString());
            //}
        }

        private void pnlSearch_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            Rectangle r = new Rectangle(new Point(0, 0),
                new Size(115, this.pnlSearch.Height));
            using (LinearGradientBrush sb = new LinearGradientBrush(r,
                Color.FromArgb(41, 160, 219),
                Color.FromArgb(44, 105, 183),
                LinearGradientMode.Vertical))
            {
                g.FillRectangle(sb, r);
                sf_Search.Alignment = StringAlignment.Center;
                sf_Search.LineAlignment = StringAlignment.Center;
                using (SolidBrush sbf = new SolidBrush(Color.White))
                {
                    g.DrawString(titleStr_Search, this.f_Search, sbf, r, sf_Search);
                }

                r = new Rectangle(new Point(115, 0),
                    new Size(this.pnlSearch.Width - 115, this.pnlSearch.Height));
                sb.LinearColors = new Color[] { Color.FromArgb(254, 254, 254), Color.FromArgb(239, 239, 239) };

                g.FillRectangle(sb, r);

            }
            using (SolidBrush sb = new SolidBrush(Color.FromArgb(204, 204, 204)))
            {
                using (Pen p = new Pen(sb, 2.0f))
                {
                    g.DrawRectangle(p, new Rectangle(new Point(0, 0),
                        new Size(this.pnlSearch.Width - 1, this.pnlSearch.Height - 1)));
                }

            }
        }



        #region 下载头像

        /// <summary>
        /// 下载全部头像
        /// </summary>
        public void DownLoadHeadImageAll()
        {
            if (this.searchDt != null && this.searchDt.Rows.Count > 0)
            {
                for (int i = 0; i < this.searchDt.Rows.Count; i++)
                {
                    DataRow dr = this.searchDt.Rows[i];
                    string imgName = dr["headurl"] != DBNull.Value ? dr["headurl"].ToString() : string.Empty;
                    string jid = dr["jid"].ToString();
                    DownloadUserHead(SysParams.Head_ImageInfo + imgName, 2000, jid);
                }
            }
        }

        /// <summary>
        /// 下载头像
        /// </summary>
        /// <param name="headUrl"></param>
        /// <param name="wait"></param>
        /// <param name="jid"></param>
        /// <returns></returns>
        void DownloadUserHead(string headUrl,int wait,string jid)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(headUrl);
            req.Proxy = null;
            //object[] obj=new object []{ }
            IAsyncResult ar = req.BeginGetResponse(this.Complete, new { jid2 = jid, req2 = req });

            //return ar.AsyncWaitHandle.WaitOne(wait);
        }

        void Complete(IAsyncResult ar)
        {
            dynamic obj = ar.AsyncState;
            HttpWebRequest req = (HttpWebRequest)obj.req2;
            string jid = obj.jid2;
            HttpWebResponse res = null;

            //if (res.StatusCode == HttpStatusCode.OK && Regex.IsMatch(res.ContentType, @"image\/([^$]+)"))
            try
            {
                res = (HttpWebResponse)req.EndGetResponse(ar);
                Stream sImageCode = res.GetResponseStream();
                //GZipStream s = new GZipStream(sImageCode, CompressionMode.Decompress);
                if (sImageCode != null)
                {
                    lock (idtUserinfo.dataTable)
                    {
                        Image img = Image.FromStream(sImageCode);
                        DataRow[] drlist = idtUserinfo.dataTable.Select("jid='" + jid + "'");
                        if (drlist.Length > 0)
                        {
                            DataRow dr = drlist[0];
                           //SpecialContent s = (SpecialContent)dr["head"];
                            SpecialContent sc = new SpecialContent();
                            sc.imageAlign = ImageAlign.center;
                            sc.scType = SpecialContentType.image;
                            sc.drawImage = img;
                            dr["head"] = sc;
                            //img.Save("E:\\1.jpg");
                          
                            idtUserinfo.BeginInvoke(new Action(()=> {
                                idtUserinfo.BindData(idtUserinfo.dataTable);
                                //Application.DoEvents();
                            }));
                        }
                        
                    }
                }

            }
            catch (WebException ex)
            {
                if(ex.Response!=null)
                ex.Response.Close();
            }
            catch (SocketException)
            {
            }
        }

        #endregion;

        #region 数据释放
        /// <summary>
        /// idt数据释放
        /// </summary>
        /// <param name="dt"></param>
        public void idtDispose(DataTable dt)
        {
            if (dt != null && dt.Rows.Count>0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    for (int a = 0; a < dt.Columns.Count; a++)
                    {
                        string colName = dt.Columns[a].ColumnName;
                        if (dr[colName] is SpecialContent)
                        {
                            SpecialContent sc = (SpecialContent)dr[colName];
                            sc.Dispose();
                        }
                    }
                }
            }
        }

        private void UserListForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.searchDt != null)
            {
                idtDispose(this.searchDt);
                this.idtUserinfo.Dispose();
            }
        }
        #endregion;
    }
}
