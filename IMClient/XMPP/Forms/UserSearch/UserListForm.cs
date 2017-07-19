using CCWin.SkinControl;
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
    public partial class UserListForm : FrameBase
    {
        DataTable dt = null;
        private string titleStr = "用户查询";
        Point pt = Point.Empty;
        private string titleStr_Search = "通讯录";
        Font f_Search = new Font("微软雅黑", 15.0f, FontStyle.Bold | FontStyle.Italic);
        StringFormat sf_Search = new StringFormat();
        private DataTable searchDt = null;
        private int FormModel = 0;

        public UserListType ulistType = UserListType.findAUser;

        public delegate void addUser(string uid, string nickName, string displayName,
            string personMsg, ChatListSubItem.UserStatus uStatus,
            PlatformType pt, Image headImage, UserListType u,string userJid);

        public addUser audelegate = null;
        public UserListForm()
        {
            InitializeComponent();
            dt = retDataTableTest();
            InitDataTable();
            SetScroll();
        }

        public UserListForm(int formModel)
        {
            InitializeComponent();
            dt = retDataTableTest();
            InitDataTable();
            SetScroll();
           
        }



        public UserListForm(UserListType ulistType,addUser au)
        {
            this.ulistType = ulistType;
            this.audelegate = au;
            InitializeComponent();
            dt = retDataTableTest();
            InitDataTable();
            SetScroll();
            this.FormModel = 1;
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

        public DataTable retDataTableTest()
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
                   Color.FromArgb(43, 145, 175), sf,  "uid" + i);

                sctmp.AddsFontlist(new Font("微软雅黑", 9.0f),
               Color.FromArgb(43, 145, 175), sf, "username" + i);
                dr["username"] = sctmp;
             
                dr["gender"] = r.Next(0,1);
                dr["Phone"] = "Phone" + i;
                dr["company"] = "company" + i;
                dr["age"] = r.Next(0, 99);
                dr["btnF"] = "btnF" + i;
                dt.Rows.Add(dr);
            }
            return dt;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public UserInformation[] GetData(string userInfo)
        {

            UserServiceClient c = new UserServiceClient();
            UserInformation[] info = null;
            try
            {
                c.Open();
                info = c.FindUserInformation(false, userInfo);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                c.Close();
            }
            return info;
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
            this.idtUserinfo.ColHeight = 60;
            //this.idtUserinfo.IsTitle = false;
            this.idtUserinfo.Bg_SelectColor = Color.FromArgb(200,200,200);
            this.idtUserinfo.Bg_MoveColor = Color.FromArgb(190,190,190);
            this.idtUserinfo.borderColor = Color.FromArgb(230, 230, 230);
            this.idtUserinfo.Font_title.Dispose();
            this.idtUserinfo.Font_title = new Font("微软雅黑",11.0f,FontStyle.Bold);
            this.idtUserinfo.borderStyle = IMClient.Controls.borderStyleSelf.bordrNone;
            this.idtUserinfo.B_TitleColor = new LinearGradientBrush(new Rectangle(0, 0, this.idtUserinfo.Width, this.idtUserinfo.TitleHeight),
                Color.FromArgb(251, 251, 251), Color.FromArgb(240, 240, 240),LinearGradientMode.Vertical);
            ColClass col = new ColClass("序号", "number", 5);
            this.idtUserinfo.colList.Add(col);

            col = new ColClass("头像", "head", 10);
            this.idtUserinfo.SplitLine_Content_Length = this.idtUserinfo.Width - 24;
            this.idtUserinfo.SplitLine_Title_H_Length = this.idtUserinfo.Width ;
            this.idtUserinfo.SplitLine_Title_V_Length = this.idtUserinfo.TitleHeight-10;


            this.idtUserinfo.colList.Add(col);
            col = new ColClass("姓名", "username", 10);
            this.idtUserinfo.colList.Add(col);
            col = new ColClass("性别", "gender", 5);
            this.idtUserinfo.colList.Add(col);
            col = new ColClass("单位", "company", 20);
            this.idtUserinfo.colList.Add(col);
            col = new ColClass("职务", "age", 5);
            this.idtUserinfo.colList.Add(col);
            //col = new ColClass("年龄", "age", 5);
            //this.idtUserinfo.colList.Add(col);
            col = new ColClass("联系方式", "Phone", 15);
            this.idtUserinfo.colList.Add(col);
            col = new ColClass("邮箱", "mail", 20);
            this.idtUserinfo.colList.Add(col);
            col = new ColClass("", "btnF", 10);
            this.idtUserinfo.colList.Add(col);

            this.idtUserinfo.Height = dt.Rows.Count * this.idtUserinfo.ColHeight + this.idtUserinfo.TitleHeight;
            this.idtUserinfo.Invalidate();
        }

        private void UserListForm_Shown(object sender, EventArgs e)
        {
            //this.idtUserinfo.BindData(dt);
            SetScroll();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            lblCity.Text = "上海";
            lblCity2.Text = "上海市";
            lblCity3.Text = "全部";

            //lblCity.Text = "全部";
            //lblCity2.Text = "全部";
            //lblCity3.Text = "全部";


            if (this.searchDt != null)
            {
                this.idtDispose(this.searchDt);
                this.searchDt.Clear();
            }
            retUserInfo(txtUserInfo.Text);

        }

        private void retUserInfo(string query)
        {
            UserInformation[] userlist = GetData(query);
            if (userlist != null && userlist.Length > 0)
            {
                this.searchDt = InitDt();
                int index_tmp = 1;
                for (int i = 0; i < userlist.Length; i++)
                {
                    if (SysParams.AllFriendList == null)
                    {
                        SysParams.AllFriendList = new List<JustLib.GGUser>();
                    }
                    UserInformation u = userlist[i];
                    DataRow dr = this.searchDt.NewRow();

                    dr["headurl"] = u.Head;

                    string jid = string.Empty;
                    string userName = string.Empty;
                    string mail = string.Empty;
                    if (u.Services != null && u.Services.Length > 0)
                    {
                        foreach (UserServiceInformation us in u.Services)
                        {
                            switch (us.Service)
                            {
                                case "phone":
                                    dr["Phone"] = us.Data;
                                    break;
                                case "user":
                                    userName = us.Data.ToString();
                                    break;
                                case "im":
                                    jid = us.Data.ToString();
                                    break;
                                case "mail":
                                    mail = us.Data.ToString();
                                    //mail = mail.Replace("a.com", "DNA.com");
                                    break;
                            }
                        }
                    }

                    //判断用户是否已是好友
                    if (this.FormModel==0 && (string.IsNullOrEmpty(jid) || SysParams.LoginUser.GetJID() == jid || SysParams.AllFriendList.Exists(item => item.ID == jid)))
                    {
                        continue;
                    }

                    dr["jid"] = jid;
                    dr["uid"] = u.UID;
                    SpecialContent sctmp = new SpecialContent();
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    sctmp.AddsFontlist(new Font("微软雅黑", 11.0f),
                 Color.FromArgb(99, 145, 175), sf, u.Name);
                    //  sctmp.AddsFontlist(new Font("微软雅黑", 9.0f),
                    //Color.FromArgb(43, 145, 175), sf, userName);

                    //sctmp.AddsFontlist(new Font("微软雅黑", 9.0f),
                    //    Color.FromArgb(43, 145, 175), sf, u.UID);


                    dr["username"] = sctmp;
                    dr["usernameSelf"] = userName;
                    dr["realname"] = u.Name;
                    string sex = u.Sex;
                    if (!string.IsNullOrEmpty(mail))
                    {
                        Color c = this.idtUserinfo.Color_font;
                        if (sex.Trim() == "女")
                        {
                            c = Color.Red;
                        }
                        sctmp = new SpecialContent();
                        sf = new StringFormat();
                        sf.Alignment = StringAlignment.Center;
                        sf.LineAlignment = StringAlignment.Center;
                        sctmp.AddsFontlist(new Font("微软雅黑", 10.0f),
                     c, sf, sex);

                        dr["gender"] = sctmp;
                    }

                    dr["company"] = u.Company;

                    dr["age"] = "DNA室主任";
                    //dr["btnF"] = "btnF" + i;
                    if (!string.IsNullOrEmpty(mail))
                    {
                        sctmp = new SpecialContent();
                        sf = new StringFormat();
                        sf.Alignment = StringAlignment.Center;
                        sf.LineAlignment = StringAlignment.Center;
                        sctmp.AddsFontlist(new Font("微软雅黑", 11.0f, FontStyle.Underline),
                     Color.Blue, sf, mail);

                        dr["mail"] = sctmp;
                    }
                    sctmp = new SpecialContent();
                    sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    sctmp.AddsFontlist(new Font("微软雅黑", 11.0f, FontStyle.Bold | FontStyle.Italic),
                  Color.FromArgb(43, 145, 175), sf, index_tmp.ToString());
                    dr["number"] = sctmp;
                    this.searchDt.Rows.Add(dr);
                    index_tmp++;


                }
                int height = this.searchDt.Rows.Count * this.idtUserinfo.ColHeight + this.idtUserinfo.TitleHeight;
                this.idtUserinfo.Height = height;
                this.idtUserinfo.BindData(this.searchDt);
                SetScroll();
                DownLoadHeadImageAll();//加载头像
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
            dt.Columns.Add("gender", typeof(SpecialContent));
            dt.Columns.Add("company", typeof(string));
            dt.Columns.Add("Phone", typeof(string));
            dt.Columns.Add("age", typeof(string));
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
            AddUser();
        }

        private void idtUserinfo_MouseClick(object sender, MouseEventArgs e)
        {
            //MessageBox.Show(this.idtUserinfo.selectedIndex_Col.ToString());
            if (this.idtUserinfo.selectedIndex_Col == this.idtUserinfo.colList.Count - 1)
            {
               
                    AddUser();
               
            }
        }

        /// <summary>
        /// 显示用户详细信息
        /// </summary>
        private void AddUser()
        {
            DataRow dr = idtUserinfo.retSelectedRow();
            if (dr != null)
            {
                string jid = dr["jid"].ToString();
                string userName = dr["userNameSelf"].ToString();
                string realName = dr["realname"].ToString();
                string company = dr["company"].ToString();
                Image img = ((SpecialContent)dr["head"]).drawImage;
                string uid = dr["uid"].ToString();
                if (this.ulistType == UserListType.findAUser)
                {
                   
                    AddUserForm u = new AddUserForm(img, userName, realName, "LV", jid, company);
                    u.Show();
                }
                else 
                {
                    this.audelegate(uid, realName, realName, company, ChatListSubItem.UserStatus.OffLine, 
                        PlatformType.PC, img, this.ulistType, jid);
                    if (this.ulistType == UserListType.findEmcee)
                    {
                        this.Close();
                    }
                }
                
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

                //using (SolidBrush sbf = new SolidBrush(Color.Red))
                //{
                //    g.FillPolygon(sbf,new Point[] { new Point(0,)}
                //}
               
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
                            sc.drawImage_Hover = img;
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

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
