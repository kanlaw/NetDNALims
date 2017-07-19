using DNANET.Data;
using IMClient.Controls;
using IMClient.Controls.Base;
using IMClient.Logic;
using NetDLims.Services;
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
using mx = Matrix;
namespace IMClient.XMPP.Forms
{
    public partial class AddUserForm : FrameBase
    {
        private string titleStr = "用户添加";

        private int bottomHeight = 50;

        private int rightWidth = 150;
        private Color bottomColor = Color.FromArgb(250, 250, 250);
        private Color rightColor = Color.FromArgb(255, 255, 255);
        private Size headImageSize = SysParams.HeadImageDetailSize;


        private Image headImage = null;

        private string UserName = string.Empty;
        private string RealName = string.Empty;
        private string JId = string.Empty;
        private string LV = string.Empty;
        private string Company = string.Empty;

        private addUser addu = new addUser();

        public AddUserForm()
        {
            InitializeComponent();
        }

        public AddUserForm(string JID,string messageStr)
        {
            InitializeComponent();
            btnAgree.Click += this.btnAgree_Click;
            UserInformation[] ulist = GetData(JID);
            if (ulist.Length > 0)
            {
                Image img = (Image)SysParams.defaultHead.Clone();
                this.headImage = Common.FillRoundRectangle(img,
                new Rectangle(new Point(0, 0), headImageSize), headImageSize);
                this.UserName = JID.Substring(0,JID.LastIndexOf("@"));
                this.RealName = ulist[0].Name;
                this.LV = "LV";
                this.JId = JID;
                this.Company = ulist[0].Company;
                txtMessage.Text = messageStr;
                btnAgree.Visible = true;
                btnCancel.Visible = true;
                btnAdd.Visible = false;
            }
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



        public AddUserForm(Image headImage,string userName,string realName,
            string Lv,string JID,string Company)
        {
            InitializeComponent();

            
            btnAgree.Click +=this.btnAdd_Click;

            btnAgree.Staticpic = Properties.Resources.next_step_normal_03;
            btnAgree.Presspic=btnAgree.Activepic= Properties.Resources.next_step_over_03;
            btnCancel.Staticpic = Properties.Resources.cancel02_normal_03;
            btnCancel.Presspic = btnCancel.Activepic = Properties.Resources.cancel02_over_03;

            txtMessage.Text = "我是" + SysParams.LoginUser.Name;
            Image img = (Image)headImage.Clone();
            this.headImage = Common.FillRoundRectangle(img,
                new Rectangle(new Point(0, 0), headImageSize), headImageSize);
            //this.headImage = img;
            this.UserName = userName;
            this.RealName = realName;
            this.LV = Lv;
            this.JId = JID;
            this.Company = Company;
        }

        #region 初始绘制

        /// <summary>
        /// 绘制背景
        /// </summary>
        /// <param name="e"></param>
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


            g.FillRectangle(new SolidBrush(bottomColor),
                    new Rectangle(new Point(0, this.Height - bottomHeight), new Size(this.Width, this.bottomHeight)));


            g.FillRectangle(new SolidBrush(rightColor),
             new Rectangle(new Point(0, SysParams.Paint_title_Height), new Size(this.rightWidth, this.Height-SysParams.Paint_title_Height-this.bottomHeight)));


        }

        /// <summary>
        /// 绘制人员信息
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;


            int x = (this.rightWidth - this.headImage.Width) / 2;
            int y = txtMessage.Location.Y;
            g.DrawImage(this.headImage, new Point(x,y ));


            int offSet_Y = 20;
            y += this.headImage.Height+ offSet_Y;
          
           
            using (SolidBrush sb = new SolidBrush(Color.Black))
            {

                Font f = new Font("微软雅黑", 10.0f);
                int fontWidth = rightWidth - x -5;

                int fontHeight = (int)g.MeasureString(this.UserName, f, fontWidth).Height + 1;
                g.DrawString(this.RealName, f, sb,
                    new Rectangle(new Point(x, y),
                    new Size(fontWidth, fontHeight)));

             
                //y += fontHeight;
                //fontHeight = (int)g.MeasureString(this.JId, f, fontWidth).Height + 1;
                //g.DrawString(this.UserName, f, sb, new Rectangle(new Point(x, y),
                //    new Size(fontWidth, fontHeight)));
           

               
                y += fontHeight+ offSet_Y-10;
                string company_tmp =  this.Company;
                fontHeight = (int)g.MeasureString(company_tmp, f, fontWidth).Height + 1;
                StringFormat sf = new StringFormat();
                sf.FormatFlags = StringFormatFlags.FitBlackBox;
                sb.Color = Color.FromArgb(102, 102, 102);
                g.DrawString(company_tmp, f, sb, 
                    new Rectangle(new Point(x, y),
                    new Size(fontWidth, fontHeight)), sf);

                f.Dispose();
                //y += (int)g.MeasureString(this.JId, f).Height + 1;
                //g.DrawString(this.JId, f, sb, new Point(x, y));

            }

        }

        #endregion

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddUserForm_Load(object sender, EventArgs e)
        {

        }

        private void AddUserForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (headImage != null)
            {
                headImage.Dispose();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            addu.sendAddUser(this.JId, this.txtMessage.Text);
            this.Close();
        }

        private void btnAgree_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
