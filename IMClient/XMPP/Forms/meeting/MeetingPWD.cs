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
    public partial class MeetingPWD : FrameBase
    {
        private string titleStr = "网络会议室";

        /// <summary>
        /// 查询条件
        /// </summary>
        public string pwd = string.Empty;


        public MeetingPWD()
        {
            InitializeComponent();
        }

        public MeetingPWD(string roomName)
        {
            if (!string.IsNullOrEmpty(roomName))
            {
                titleStr += "-" + roomName;
            }
            InitializeComponent();

        }



        private void UserListForm_Shown(object sender, EventArgs e)
        {
            //this.idtUserinfo.BindData(dt);
           
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            
          
        }

    


       

      

        private void idtUserinfo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //AddUser();
        }

      

 

        private void btnClose_Click(object sender, EventArgs e)
        {
            txtPwd.Text = string.Empty;
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            pwd = txtPwd.Text.Trim();
            this.Close();
        }
    }
}
