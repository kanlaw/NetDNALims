using IMClient.Controls.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IMClient.Properties;
using System.Drawing.Text;


namespace IMClient.Controls.Tools
{

    public enum MsgKind
    {
        ok = 1,
        yes = 2,
        no = 4,
        abort = 8,
        cancel = 16,
        yes_no = 6,
        ok_cancel = 17,
        yes_ok_cancel = 19
    }
    public enum IconNum
    {
        confirm = 1,
        warn = 2,
        note = 3,
        choose = 4
    }
    public partial class FrmMsg : FrameBase
    {
        //文字布局信息格式
        private StringFormat sf = new StringFormat();

        //消息类型      1：确认    2：警告    3：提示    4：选择
        //private int msgType = 1;
        private IconNum iconNum;
        private Brush borderbrush;
        private Pen borderpen;
        private Brush stringbrush;
        private Font stringfont;

        //文本颜色刷
        private Brush textbrush;
        //文本字体
        private Font textfont;

        private Rectangle captionRect;
        private Rectangle borderRect;

        //消息内容
        public string Message;
        //消息窗口的标题
        public string Caption;
        //消息内容的标题
        public string Title;
        //消息提示图标
        private Image iconImg;
        //按钮集合
        private List<Button> btnList = new List<Button>();
        //按钮的起始位置集合
        private List<Point> spList = new List<Point>();
        //每个按钮的大小
        private Size btnSize = new Size(66, 27);
        private Button btnOk;       //1
        private Button btnYes;      //2
        private Button btnNo;       //4
        private Button btnAbort;    //8
        private Button btnCancel;   //16
        private MsgKind msgkind;
        //控制窗口显示自动关闭
        private bool autoHide = false;
        public FrmMsg()
        {
            InitializeComponent();
            CaptionPos = 30;
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Center;
            //btnClose.ColorBack = FrmStyle.FormTitleBackColor;
            //btnClose.Create();
        }

        public FrmMsg(int len)
        {
            InitializeComponent();
            if (len > 30)
            {
                int ck = (int)Math.Ceiling((double)len / 3);
                this.Width += (ck - 10) * 15;
            }
            CaptionPos = 30;
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Center;
            //btnClose.ColorBack = FrmStyle.FormTitleBackColor;
            //btnClose.Create();
        }

        private void FrmMsg_Load(object sender, EventArgs e)
        {
            btnClose.BringToFront();
            captionRect = new Rectangle(0, 0, this.Width, this.CaptionPos);
            borderRect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            //由于图标有色差，所以颜色自定义

            borderbrush = new SolidBrush(Color.FromArgb(0, 95, 175));
            stringbrush = new SolidBrush(FrmStyle.FormTitleFontColor);
            //borderbrush = new SolidBrush(Color.Transparent);
            //stringbrush = new SolidBrush(Color.Transparent);



            textbrush = new SolidBrush(Color.FromArgb(44, 44, 44));
            textfont = FrmStyle.TextFont;
            borderpen = new Pen(borderbrush);
            stringfont = new Font("微软雅黑", 12f);
            #region
            //判断kind参数创建按钮
            if (msgkind != 0)
            {
                int ck = (int)msgkind;
                if (ck - 16 >= 0)
                {
                    btnCancel = GetCancel();
                    ck -= 16;
                    btnList.Add(btnCancel);
                }
                if (ck - 8 >= 0)
                {
                    btnAbort = GetAbort();
                    ck -= 8;
                    btnList.Add(btnAbort);
                }
                if (ck - 2 >= 0)
                {
                    btnYes = GetYes();
                    ck -= 2;
                    btnList.Add(btnYes);
                }
                if (ck - 4 >= 0)
                {
                    btnNo = GetNo();
                    ck -= 4;
                    btnList.Add(btnNo);
                }

                if (ck - 1 >= 0)
                {
                    btnOk = GetOk();
                    ck -= 1;
                    btnList.Add(btnOk);
                }
            }
            else
            {
                return;
            }
            //计算每个按钮的位置
            int quantity = btnList.Count;
            switch (quantity)
            {
                case 1:
                    Point p1 = new Point((Width - 66) / 2, (Height - 30 - 23 - 40 - 27) / 2 + 30 + 23 + 40);
                    spList.Add(p1);
                    break;
                case 2:
                    Point p21 = new Point((Width - 66 * 2 + 18) / 2, (Height - 30 - 23 - 40 - 27) / 2 + 30 + 23 + 40);
                    Point p22 = new Point((Width - 66 * 2 + 18) / 2 + 18 + 66, (Height - 30 - 23 - 40 - 27) / 2 + 30 + 23 + 40);
                    spList.Add(p21);
                    spList.Add(p22);
                    break;
                case 3:
                    Point p31 = new Point((Width - 66 * 3 + 18 * 2) / 2, (Height - 30 - 23 - 40 - 27) / 2 + 30 + 23 + 40);
                    Point p32 = new Point((Width - 66 * 3 + 18 * 2) / 2 + 18 + 66, (Height - 30 - 23 - 40 - 27) / 2 + 30 + 23 + 40);
                    Point p33 = new Point((Width - 66 * 3 + 18 * 2) / 2 + 18 * 2 + 66 * 2, (Height - 30 - 23 - 40 - 27) / 2 + 30 + 23 + 40);
                    spList.Add(p31);
                    spList.Add(p32);
                    spList.Add(p33);
                    break;
            }
            #endregion
            //将控件加入界面
            for (int i = 0; i < btnList.Count; i++)
            {
                Button item = btnList[i];
                item.Location = spList[i];
                this.Controls.Add(item);
            }
        }

        #region 创建ImageButton控件

        private ImageButton BaseBtn()
        {
            ImageButton btn = new ImageButton();
            btn.Size = new Size(66, 27);
            return btn;
        }


        private ImageButton GetOk()
        {
            ImageButton btn = BaseBtn();
            btn.Staticpic = Resources.determine_button_grey_normal;
            btn.Presspic = Resources.determine_button_grey_over;
            btn.Activepic = Resources.determine_button_grey_over;
            btn.Click += new EventHandler(OK_Click);
            return btn;
        }

        void OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private ImageButton GetYes()
        {
            ImageButton btn = BaseBtn();
            btn.Staticpic = Resources.yes_button_normal_03;
            btn.Presspic = Resources.yes_button_over_03;
            btn.Activepic = Resources.yes_button_over_03;
            btn.Click += new EventHandler(YES_Click);
            return btn;
        }

        void YES_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.Close();
        }

        private ImageButton GetNo()
        {
            ImageButton btn = BaseBtn();
            btn.Staticpic = Resources.no_button_normal_03;
            btn.Presspic = Resources.no_button_over_03;
            btn.Activepic = Resources.no_button_over_03;
            btn.Click += new EventHandler(NO_Click);
            return btn;
        }

        void NO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.No;
            this.Close();
        }

        private ImageButton GetAbort()
        {
            ImageButton btn = BaseBtn();
            btn.Staticpic = Resources.exit_button_grey_normal;
            btn.Presspic = Resources.exit_button_grey_over;
            btn.Activepic = Resources.exit_button_grey_over;
            btn.Click += new EventHandler(ABORT_Click);
            return btn;
        }

        void ABORT_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.Close();
        }

        private ImageButton GetCancel()
        {
            ImageButton btn = BaseBtn();
            btn.Staticpic = Resources.cancel_button_grey_normal;
            btn.Presspic = Resources.cancel_button_grey_over;
            btn.Activepic = Resources.cancel_button_grey_over;
            btn.Click += new EventHandler(CANCEL_Click);
            return btn;
        }

        void CANCEL_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.FillRectangle(borderbrush, captionRect);
            e.Graphics.DrawRectangle(borderpen, borderRect);
            //e.Graphics.DrawImage(ResOther.logo_81, new Rectangle(new Point(3, 1), ResOther.logo_81.Size));
            switch (iconNum)
            {
                case IconNum.confirm: iconImg = Resources.right_green_07; break;
                case IconNum.warn: iconImg = Resources.warm_red_07; break;
                case IconNum.note: iconImg = Resources.info_blue_07; break;
                case IconNum.choose: iconImg = Resources.select_yellow_07; break;
            }
            if (iconImg != null)
            {
                e.Graphics.DrawImage(iconImg, new Rectangle(23, 53, iconImg.Width, iconImg.Height));
            }
            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            if (Caption != null)
                e.Graphics.DrawString(Caption, stringfont, stringbrush, 10, 5);
            //if (Title != null)
            //    e.Graphics.DrawString(Title, stringfont, new SolidBrush(Color.Black), new RectangleF(65, 40, 180, 30));
            if (Message != null)
                //e.Graphics.DrawString(Message, textfont, textbrush, 100, 80);
                e.Graphics.DrawString(Message, textfont, textbrush, new RectangleF(46 + iconImg.Width, 48, Width - 23 * 3 - iconImg.Width, iconImg.Height + 10), sf);
        }


        #region 调用



        /// <summary>
        /// 弹出指定消息框
        /// </summary>
        /// <param name="kind">消息框种类</param>
        /// <param name="icon">显示大图标</param>
        /// <param name="caption">消息大标题</param>
        /// <param name="title">消息小标题</param>
        /// <param name="content">消息内容</param>
        /// <returns></returns>
        public static DialogResult Show(MsgKind kind, IconNum icon, string caption, string content)
        {
            Form wnd = (Form)AppDomain.CurrentDomain.GetData("MainWindow");

            return (DialogResult)wnd.Invoke(new Func<DialogResult>(delegate ()
            {
                FrmMsg fm = new FrmMsg(content.Length);
                fm.msgkind = kind;
                fm.Message = content;
                fm.Caption = caption;
                fm.iconNum = icon;
                return fm.ShowDialog();
            }));
        }

        /// <summary>
        /// 弹出指定消息框
        /// </summary>
        /// <param name="kind">消息框种类</param>
        /// <param name="icon">显示大图标</param>
        /// <param name="caption">消息大标题</param>
        /// <param name="title">消息小标题</param>
        /// <param name="content">消息内容</param>
        /// <returns></returns>
        public static DialogResult Show(MsgKind kind, IconNum icon, string caption, string content, Size size)
        {
            FrmMsg fm = new FrmMsg(content.Length);
            fm.Size = size;
            fm.msgkind = kind;
            fm.Message = content;
            fm.Caption = caption;
            fm.iconNum = icon;
            return fm.ShowDialog();
        }

        /// <summary>
        /// 弹出消息
        /// </summary>
        /// <param name="kind">消息框种类</param>
        /// <param name="caption">消息大标题</param>
        /// <param name="title">消息小标题</param>
        /// <param name="content">消息内容</param>
        /// <returns></returns>
        public static DialogResult Show(MsgKind kind, string caption, string title, string content)
        {
            return Show(kind, IconNum.note, caption, content);
        }

        public static DialogResult Show(MsgKind kind, string caption, string content)
        {
            return Show(kind, IconNum.note, caption, content);
        }

        public static DialogResult Show(MsgKind kind, string caption, string content, Size size)
        {
            return Show(kind, IconNum.note, caption, content, size);
        }

        /// <summary>
        /// 弹出消息
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="caption">标题</param>
        /// <returns></returns>
        public static DialogResult Show(string content, string caption)
        {
            //return Show(1, content, "", caption, null);
            return Show(MsgKind.ok, IconNum.note, caption, content);
        }
        /// <summary>
        /// 弹出消息
        /// </summary>
        /// <param name="type">显示大图标      1：确认    2：警告    3：提示  4：选择</param>
        /// <param name="content">内容</param>
        /// <param name="caption">标题</param>
        /// <returns></returns>
        public static DialogResult Show(int type, string content, string caption)
        {
            return Show(MsgKind.ok, IconNum.note, caption, content);

        }

        #endregion

        private void FrmMsg_Shown(object sender, EventArgs e)
        {
            timerHide.Enabled = true;
        }

        private void timerHide_Tick(object sender, EventArgs e)
        {
            if (autoHide)
            {
                if (msgkind == MsgKind.ok)
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();

        }
    }
}
