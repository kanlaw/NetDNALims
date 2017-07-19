using CCWin.SkinControl;
using IMClient.Controls;
using IMClient.Controls.Base;
using IMClient.Logic;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IMClient.XMPP.Forms
{
    public partial class CreateMeetingForm : FrameBase
    {

        private MeetingLogic ml = new MeetingLogic();

        /// <summary>
        ///  会议室
        /// </summary>
        Meeting meeting = new Meeting();

        private string titleStr = "创建会议室";

        private string voteStr = "请输入投票内容";

        private List<string> pointlist = new List<string>();

        private List<string> votelist = new List<string>();

        private int height = 0;

        private string[] letterList = new string[] { "A", "B", "C", "D", "E", "F", "G",
            "H", "I", "J", "K", "L", "M", "N", "O", "P","Q",
            "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};

        //ChatListSubItem cm

        public CreateMeetingForm()
        {
            InitializeComponent();
            ChatListItem cli = new ChatListItem("主持人");

            height = this.Height;
            this.Height = this.cbxVote.Location.Y + 20;
            meetGrouplist.Location = new Point(meetGrouplist.Location.X, label1.Location.Y);


            btnAdd.Location = new Point(btnAdd.Location.X, this.Height - btnAdd.Height-5);
            MenuColorTable mct = new MenuColorTable();
            ToolStripRendererEx t = new ToolStripRendererEx(mct);
            cmsiAdd.Renderer = t;

            ChatListSubItem clsl = new ChatListSubItem(SysParams.LoginUser.UID.ToString(), SysParams.LoginUser.Name, (SysParams.LoginUser.Company));
            clsl.ID = SysParams.LoginUser.UID.ToString();//给初始化网络会议室的默认主持人ID复制
           
            clsl.HeadImage = SysParams.loginUserImage != SysParams.defaultHead ? (Image)SysParams.loginUserImage.Clone() : SysParams.loginUserImage;
            cli.SubItems.Add(clsl);
            cli.IsOpen = true;

            meetGrouplist.ChatListBox_group.Items.Insert(0, cli);
            meetGrouplist.SkinContextMenuStrip_Group.Items.Clear();
            meetGrouplist.SkinContextMenuStrip_Group.Items.Add("删除成员");
            meetGrouplist.SkinContextMenuStrip_Group.Items[0].Click += CreateMeetingForm_Click;
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


            //int x = pnlleft.Width + (pnlCenter.Location.X - pnlleft.Width) / 2;
            //int y = pnlleft.Location.Y;
            //g.DrawLine(new Pen(SysParams.Paint_SplitLine_Color, 1.5f), new Point(x, y), new Point(x, this.Height));

            //x = pnlCenter.Location.X + pnlCenter.Width + (pnlRight.Location.X - (pnlCenter.Location.X + pnlCenter.Width)) / 2;
            //g.DrawLine(new Pen(SysParams.Paint_title_Color, 1.5f), new Point(x, y), new Point(x, this.Height));


            g.DrawString(this.titleStr, SysParams.Paint_title_Font,
                new SolidBrush(SysParams.Paint_title_Font_Color), SysParams.Paint_title_Point);
        }


        private void CreateMeetingForm_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CreateMeetingForm_Load(object sender, EventArgs e)
        {

        }


        private void rbtnEncrypt_CheckedChanged(object sender, System.EventArgs e)
        {
            if (rbtnEncrypt.Checked)
            {
                txtPwd.Visible = true;
            }
            else {
                txtPwd.Visible = false;
            }
        }

        #region 投票
        private void btnVoteAdd_Click(object sender, EventArgs e)
        {
            int index = flowLayoutPanel1.Controls.Count;
            flowLayoutPanel1.HorizontalScroll.Visible = false;
            if (index <= letterList.Length)
            {
                Panel p = new Panel();
                p.Width = flowLayoutPanel1.Width - 30;
                p.Height = 20;
                Label lbl = new Label();
                lbl.Text = letterList[index] + "." + txtSelectVote.Text;
                lbl.Font = new Font("微软雅黑", 12f);
                lbl.AutoSize = true;
                lbl.Location = new Point(10, 0);
                p.Location = new Point(0, p.Height * index);
                p.Controls.Add(lbl);
                //PnlVote.Controls.Add(p);
                //PnlVote.HorizontalScroll.Enabled = false;
                //PnlVote.HorizontalScroll.Visible = false;
                //PnlVote.AutoScroll = true;

                //PnlVote.VerticalScroll.Enabled = true;
                //PnlVote.VerticalScroll.Visible = true;
                flowLayoutPanel1.Controls.Add(p);


            }
            //this.PnlVote.Controls.Add(txtMeetingPoint.Text);
        }


        private void cbxVote_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxVote.Checked)
            {
                this.lblVote.Visible = true;
                int tmp = this.Height;
                this.Height = this.height;
                this.height = tmp;
                btnAdd.Location = new Point(btnAdd.Location.X, this.Height - btnAdd.Height);
                meetGrouplist.Height = btnAdd.Location.Y;

                //iline1.Location = new Point(iline1.Location.X, lblVote.Location.Y);
                iline1.Visible = true;
                lblVote.Location = new Point(lblVote.Location.X, iline1.Location.Y + iline1.Height + 5);
                cbxVote.Location = new Point(cbxVote.Location.X, lblVote.Location.Y + 5);
                btnEnter.Location = new Point(btnEnter.Location.X, this.Height - 5 - btnEnter.Height);
                btnCancel.Location = new Point(btnCancel.Location.X, this.Height - 5 - btnEnter.Height);
            }
            else
            {
                int tmp = this.Height;
                this.Height = this.height;
                this.height = tmp;
                btnAdd.Location = new Point(btnAdd.Location.X, this.Height - btnAdd.Height);
                meetGrouplist.Height = btnAdd.Location.Y;
                btnEnter.Location = new Point(btnEnter.Location.X, this.Height - 5 - btnEnter.Height);
                btnCancel.Location = new Point(btnCancel.Location.X, this.Height - 5 - btnEnter.Height);
                iline1.Visible = false;
                lblVote.Location = new Point(lblVote.Location.X, iline1.Location.Y);
                cbxVote.Location = new Point(cbxVote.Location.X, lblVote.Location.Y + 5);
            }
        }
        #endregion

        #region 数据保存
        private void btnEnter_Click(object sender, System.EventArgs e)
        {
            if (SysParams.LoginUser != null)
            {
                try
                {
                    string result = meetingAdd();
                    if (result == "1")
                    {

                        //创建会议室
                        if (this.meeting.Emcee == SysParams.LoginUser.UID && MessageBox.Show("会议室创建完毕，是否进入会议室?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            //进入会议室
                            meeting.MeetingStatus = (int)MeetingStatus.opening;
                            //ml.creatMeeting(meeting.MeetingGuid);
                            MeetingForm mf = new MeetingForm(meeting.MeetingGuid, true);
                            mf.Show();
                        }
                        else {
                            MessageBox.Show("会议室创建完毕,等待主持人进入。", "提示");
                        }
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("会议室添加失败");
                    //throw;
                }
            }
            else
            {
                MessageBox.Show("用户还未登录，无法注册！");
            }

        }

        /// <summary>
        /// 会议室创建
        /// </summary>
        /// <returns></returns>
        private string meetingAdd()
        {
           // = new Meeting();
            //SysParams.LoginUser
            if (meetGrouplist.ChatListBox_group.Items[0].SubItems.Count > 0)
            {

                this.meeting = new Meeting();
                this.meeting.Subject = txtSubject.Text;
                this.meeting.RoomName = txtRoomName.Text;
                this.meeting.MeetingGuid = Guid.NewGuid().ToString().Replace("-", "");
                this.meeting.Content = txtMeetingPoint.Text;
                this.meeting.CreateUser = SysParams.LoginUser.UID;
                this.meeting.IsPublic = rbtnEncrypt.Checked ? 0 : 1;
                this.meeting.Pwd = rbtnEncrypt.Checked ? txtPwd.Text : string.Empty;
                this.meeting.Emcee = int.Parse(meetGrouplist.ChatListBox_group.Items[0].SubItems[0].ID);//主持人
                this.meeting.MemberLimit = cbxPerson.Text != "无限制" ? int.Parse(cbxPerson.Text) : 0;
                //主持人添加
                MeetingMember mmE = new MeetingMember();
                mmE.Mmid = Guid.NewGuid().ToString().Replace("-", "");
                mmE.MeetingId = this.meeting.MeetingGuid;
                mmE.IsJoin = 0;
                mmE.MemberId = this.meeting.Emcee;
                this.meeting.Members.Add(mmE);

                //参与人员
                foreach (ChatListSubItem item in meetGrouplist.ChatListBox_group.Items[1].SubItems)
                {
                    MeetingMember mm = new MeetingMember();
                    mm.Mmid = Guid.NewGuid().ToString().Replace("-", "");
                    mm.MeetingId = this.meeting.MeetingGuid;
                    mm.IsJoin = 0;
                    mm.MemberId = int.Parse(item.ID);
                    this.meeting.Members.Add(mm);
                }

                //选项
                if (cbxVote.Checked)
                {
                    foreach (Control item in flowLayoutPanel1.Controls)
                    {
                        if (item is Panel)
                        {
                            foreach (Control item2 in item.Controls)
                            {
                                if (item2 is Label)
                                {
                                    Label l = (Label)item2;
                                    string letter = l.Text.Substring(0, l.Text.IndexOf("."));
                                    MeetingOption o = new MeetingOption();
                                    o.MoId = Guid.NewGuid().ToString().Replace("-", "");
                                    o.MeetingId = this.meeting.MeetingGuid;
                                    o.Content = l.Text.Substring(l.Text.IndexOf(".") + 1);
                                    o.Letter = letter;
                                    o.Type = 1;
                                    this.meeting.Options.Add(o);
                                }
                            }
                        }
                    }
                }
                string result = ml.AddMeeting(this.meeting);
                return result;
            }
            else
            {
                MessageBox.Show("请添加会议主持人");
                return "-1";
            }
        }



        #endregion

        #region 添加成员


        #region 成员添加-界面
        private void CreateMeetingForm_Click(object sender, EventArgs e)
        {

            ChatListSubItem item = meetGrouplist.ChatListBox_group.SelectSubItem;
            if (item != null)
            {

                if (meetGrouplist.ChatListBox_group.Items[0].SubItems.Count > 0
                     && meetGrouplist.ChatListBox_group.Items[0].SubItems[0] == item)
                {
                    meetGrouplist.ChatListBox_group.Items[0].SubItems.Remove(item);
                }
                else
                {
                    foreach (ChatListSubItem temp in meetGrouplist.ChatListBox_group.Items[1].SubItems)
                    {
                        if (temp == item)
                        {
                            meetGrouplist.ChatListBox_group.Items[1].SubItems.Remove(item);
                            break;
                        }
                    }
                }
            }
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
        /// <param name="personLimit">人数限制</param>
        public void addUser(string uid, string nickName, string displayName,
            string personMsg, ChatListSubItem.UserStatus uStatus,
            PlatformType pt, Image headImage, UserListType u,string userJid)
        {
            #region 人数限制控制
            int emceeCount = meetGrouplist.ChatListBox_group.Items[0].SubItems.Count;
            int memberCount = meetGrouplist.ChatListBox_group.Items[1].SubItems.Count;
            int limit = 0;
            if (!string.IsNullOrEmpty(cbxPerson.Text) && cbxPerson.Text != "无限制")
            {
                limit = int.Parse(cbxPerson.Text);
            }
            if (limit!=0 && limit <= (emceeCount + memberCount))
            {
                MessageBox.Show("参加人员已达到上线，无法继续添加。", "提示");
                return;
            }
            #endregion;

            if (headImage != SysParams.defaultHead)
            {
                headImage = (Image)headImage.Clone();
            }
            ChatListSubItem clsl = new ChatListSubItem(uid, nickName, displayName, personMsg, uStatus, headImage);
            if (u == UserListType.findEmcee)
            {
                this.meeting.EmceeJid = userJid;
                //主持人限定一个
                if (meetGrouplist.ChatListBox_group.Items[0].SubItems.Count == 0)
                {
                    meetGrouplist.ChatListBox_group.Items[0].SubItems.Add(clsl);
                }
                else
                {
                    ChatListSubItem clsl_tmp = meetGrouplist.ChatListBox_group.Items[0].SubItems[0];
                    meetGrouplist.ChatListBox_group.Items[0].SubItems[0] = clsl;
                    if (clsl_tmp.HeadImage != SysParams.defaultHead)
                    {
                        clsl_tmp.HeadImage.Dispose();
                    }
                    clsl_tmp = null;
                }

                meetGrouplist.ChatListBox_group.Items[0].IsOpen = true;
            }
            else if (u == UserListType.findMember)
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

        }
        #endregion


        private void btnAddEmcee_Click(object sender, EventArgs e)
        {
            Point p = new Point(btnAddEmcee.Location.X, btnAddEmcee.Location.Y + btnAddEmcee.Height);
            p = this.PointToScreen(p);
            cmsiAdd.Show(p);
        }

        private void tsmiEmcee_Click(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                Button tsmi = (Button)sender;
                UserListType ut = UserListType.findMember;
                UserListForm ul = new UserListForm(ut, addUser);
                ul.ShowDialog();
            }
            else
            {
                ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
                UserListType ut = UserListType.findEmcee;
                if (tsmi != null && tsmi.Tag != null)
                {

                    switch (Convert.ToInt32(tsmi.Tag))
                    {
                        case 1:
                            ut = UserListType.findEmcee;
                            break;
                        case 2:
                            ut = UserListType.findMember;
                            break;
                    }
                }
                UserListForm ul = new UserListForm(ut, addUser);
                ul.ShowDialog();
            }

            

           
        }

        #endregion

        private void PnlVote_Paint(object sender, PaintEventArgs e)
        {
           // Graphics g = e.Graphics;

        }




        #region 资源释放
        private void CreateMeetingForm_FormClosed(object sender, FormClosedEventArgs e)
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
            }
            catch (Exception ex)
            {

            }
        }
        #endregion


        #region 窗体标准功能建
        private void ibtnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        #endregion

    }
}
