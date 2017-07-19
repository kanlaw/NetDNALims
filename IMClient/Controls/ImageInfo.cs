using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using IMClient.Controls;
using IMClient.Controls.Tools;
//测试用的引用




namespace IMClient.Controls
{

    public class ImageInfo : FlowLayoutPanel
    {
        //测试数据类型
        private List<IMForm.NewsInfo> test;

        public List<IMForm.NewsInfo> Test
        {
            get { return test; }
            set { test = value; }
        }

        private DataTable newDt;

        public DataTable NewDt
        {
            get { return newDt; }
            set { newDt = value; }
        }

        public ImageInfo()
        {
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.SetStyle(ControlStyles.Opaque, false);
            base.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

            base.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            base.WrapContents = false;
        }

        //统计控件index
        private int lastItenIndex = 7;

        /// <summary>
        /// 新闻标题以上含有控件数（含）
        /// </summary>
        private int hasItemCount = 7;

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            //Load_Notices(test);
            LoadNews();
        }

        private void LoadNews()
        {
            if (newDt != null)
            {
                if (newDt.Rows.Count > 0)
                {
                    //删除原先数据项
                    if (hasItemCount > 7)
                    {
                        for (int i = hasItemCount; i > 7; i--)
                        {
                            this.Controls.RemoveAt(hasItemCount);
                            hasItemCount--;
                        }
                    }
                    //计算可显示的数据行数
                    int row = (int)((Height - 465) / 50);
                    //加载新数据项
                    if (row > 0)
                    {
                        row = newDt.Rows.Count > row ? row : newDt.Rows.Count;
                        for (int i = 0; i < row; i++)
                        {
                            string name = newDt.Rows[i]["name"].ToString();
                            name=name.Length>8?name.Substring(0, 8):name;
                            CreateInfo(newDt.Rows[i], name, 
                                newDt.Rows[i]["timecreated"].ToString().Substring(0, 10),
                                newDt.Rows[i]["note"].ToString());
                            hasItemCount++;
                        }
                    }
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawRectangle(new Pen(Color.White, 2), 0, 0, Width, Height + 2);
        }

      

        internal void CreateTitle(string title)
        {
            ItemTitle itemtitle = new ItemTitle();
            itemtitle.Margin = new Padding(0);
            itemtitle.ItemstrTitle = title;
            itemtitle.ItembackColor = FrmStyle.FormTitleBackColor;
            itemtitle.ItemFontColor = new System.Drawing.SolidBrush(FrmStyle.FormTitleFontColor);
            itemtitle.ItemFont = FrmStyle.TextFont;
            itemtitle.Width = this.Width - 2;
            itemtitle.Height = 25;

            this.Controls.Add(itemtitle);
        }

        /// <summary>
        /// 标题二
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="btnContent">more btn内容</param>
        internal void CreateTitle(string title, string btnContent)
        {
            ItemTitle_Button itemtitle = new ItemTitle_Button();
            itemtitle.Margin = new Padding(0);
            itemtitle.ItemstrTitle = title;
            itemtitle.ItembackColor = FrmStyle.FormTitleBackColor;
            itemtitle.ItemFontColor = new System.Drawing.SolidBrush(FrmStyle.FormTitleFontColor);
            itemtitle.ItemFont = FrmStyle.TextFont;
            itemtitle.MouseClick += new MouseEventHandler(itemtitle_MouseClick);
            itemtitle.MouseMove += new MouseEventHandler(itemtitle_MouseMove);
            itemtitle.Width = this.Width - 2;
            itemtitle.Height = 25;
            itemtitle.BtnContent = btnContent;
            itemtitle.BtnRect = new Rectangle(new Point(this.Width - 50, 0), new Size(50, 30));
            itemtitle.ItemFont2 = new System.Drawing.Font("微软雅黑", 9f);
            this.Controls.Add(itemtitle);
        }

        private void itemtitle_MouseClick(object sender, MouseEventArgs e)
        {
            ItemTitle_Button btn = sender as ItemTitle_Button;
            switch (btn.ItemstrTitle)
            {
                case "公告通知":
                    FrmMsg.Show("公告通知", "提示");
                    break;
            }
        }

        void itemtitle_MouseMove(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }


        /// <summary>
        /// 用户信息
        /// </summary>
        /// <param name="titleT">上标题</param>
        /// <param name="titleB">下标题</param>
        /// <param name="img">图标</param>
        internal void CreateInfo(string name, string titleT, string titleB, string titleZW, Image img, Image img2)
        {
            ItemInfo iteminfo = new ItemInfo();
            iteminfo.Name = name;
            iteminfo.MouseClick += new MouseEventHandler(iteminfo_MouseClick);
            iteminfo.ItemIcon = img;
            iteminfo.ItemIconMouseOn = img2;
            iteminfo.ItemstrTitleT = titleT;
            iteminfo.ItemstrTitleB = titleB;
            iteminfo.ItemstrTitleZW = titleZW;
            iteminfo.Margin = new Padding(0);
            iteminfo.ItembackColor = Color.FromArgb(252, 252, 252);
            iteminfo.ItembackColorMouseOn = Color.FromArgb(242, 242, 242);
            iteminfo.ItemFontColor = new SolidBrush(Color.FromArgb(105, 105, 105));
            iteminfo.ItemFont = new System.Drawing.Font("微软雅黑", 10f, FontStyle.Bold);

            iteminfo.ItemFontColor2 = new SolidBrush(Color.FromArgb(105, 105, 105));
            iteminfo.ItemFont2 = new System.Drawing.Font("微软雅黑", 10f);

            iteminfo.ItemFontColor3 = new SolidBrush(Color.FromArgb(105, 105, 105));
            iteminfo.ItemFont3 = new System.Drawing.Font("微软雅黑", 10f);

            iteminfo.Width = this.Width - 2;
            iteminfo.Height = 65;
            this.Controls.Add(iteminfo);
        }

        internal void CreateInfo(string name, string titleT, string titleB, string titleZW, Image img, Image img2,MouseEventHandler doubleClick,MouseEventHandler mouseRightClick)
        {
            ItemInfo iteminfo = new ItemInfo();
            iteminfo.Name = name;
            iteminfo.MouseClick += new MouseEventHandler(iteminfo_MouseClick);
            iteminfo.ItemIcon = img;
            iteminfo.ItemIconMouseOn = img2;
            iteminfo.ItemstrTitleT = titleT;
            iteminfo.ItemstrTitleB = titleB;
            iteminfo.ItemstrTitleZW = titleZW;
            iteminfo.Margin = new Padding(0);
            
            iteminfo.ItembackColor = Color.FromArgb(252, 252, 252);
            iteminfo.ItembackColorMouseOn = Color.FromArgb(242, 242, 242);
            iteminfo.ItemFontColor = new SolidBrush(Color.FromArgb(57, 57, 57));
            iteminfo.ItemFont = new System.Drawing.Font("微软雅黑", 10f, FontStyle.Bold);

            iteminfo.ItemFontColor2 = new SolidBrush(Color.FromArgb(159, 159, 159));
            iteminfo.ItemFont2 = new System.Drawing.Font("微软雅黑", 10f);

            iteminfo.ItemFontColor3 = new SolidBrush(Color.FromArgb(64, 117, 157));
            iteminfo.ItemFont3 = new System.Drawing.Font("微软雅黑", 10f);

            iteminfo.Width = this.Width - 2;
            iteminfo.Height = 65;
            iteminfo.MouseDoubleClick += doubleClick;
            iteminfo.MouseDown += mouseRightClick;
            this.Controls.Add(iteminfo);
            
        }

        //Iteminfo单击事件
        void iteminfo_MouseClick(object sender, MouseEventArgs e)
        {
            ItemInfo iteminfo = (ItemInfo)sender;
            //暂时不开启continue
            //switch (iteminfo.Name)
            //{
            //    case "Person":
            //        FrmMsg.Show(1, "Person", "测试");
            //        break;
            //    case "Time":
            //        FrmMsg.Show(2, "Time", "测试");
            //        break;
            //    case "Role":
            //        FrmMsg.Show(3, "Role", "测试");
            //        break;
            //}
        }





        /// <summary>
        /// 新闻内容
        /// </summary>
        /// <param name="dr">新闻数据行</param>
        /// <param name="titleT">左标题</param>
        /// <param name="titleB">右标题</param>
        /// <param name="content">内容</param>
        internal void CreateInfo(DataRow dr, string titleT, string titleB, string content)
        {
            ItemInfo_SelfNews iteminfo = new ItemInfo_SelfNews();
            iteminfo.NewDr = dr;
            iteminfo.ItemstrTitleT = titleT;
            iteminfo.ItemstrTitleB = titleB;
            iteminfo.Margin = new Padding(0);
            iteminfo.MouseClick += new MouseEventHandler(New_MouseClick);
            iteminfo.ItembackColor = Color.FromArgb(252, 252, 252);
            iteminfo.ItembackColorMouseOn = Color.FromArgb(242, 242, 242);
            iteminfo.ItemFontColor2 = new SolidBrush(Color.FromArgb(105, 105, 105));
            iteminfo.ItemFont2 = new System.Drawing.Font("微软雅黑", 10f, FontStyle.Bold);

            iteminfo.ItemFontColor = new SolidBrush(Color.FromArgb(105, 105, 105));
            iteminfo.ItemFont = new System.Drawing.Font("微软雅黑", 8f);

            iteminfo.ItemstrContent = content;

            iteminfo.Width = this.Width - 2;
            iteminfo.Height = 50;

            this.Controls.Add(iteminfo);
        }
        void New_MouseClick(object sender, MouseEventArgs e)
        {
            ItemInfo_SelfNews news = sender as ItemInfo_SelfNews;
            DataRow dr = news.NewDr;
            string note = dr["note"].ToString().Replace(" ", "").Replace((char)13, (char)0).Replace((char)10, (char)0);
            note = note.Length > 100 ? note.Substring(0, 100) : note;
            FrmMsg.Show(note, "提示");
        }

      

     
    }
}
