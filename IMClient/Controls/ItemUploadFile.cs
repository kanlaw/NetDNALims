using IMClient.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IMClient.Controls
{

    /// <summary>
    /// 上传控件
    /// 1 
    /// </summary>
    public class ItemUploadFile: ItemBase
    {
        ~ItemUploadFile(){
            this.tm.Stop();
            if (icoImage != null)
            {
                icoImage.Dispose();
            }
            if (imgFileStatus != null)
            {
                imgFileStatus.Dispose();
            }
        }

        /// <summary>
        /// 文件发送状态
        /// </summary>
        private Image imgFileStatus = null;

        public Image ImgFileStatus
        {
            get
            {
                if (imgFileStatus == null && file != null)
                {
                    switch (file.IsSender)
                    {
                        case false:
                            this.imgFileStatus = Properties.Resources.transmission_closed_small_03;
                            break;
                        default:
                            this.imgFileStatus = Properties.Resources.transmission_send_small_03;
                            break;

                    }
                }
                else {
                    this.imgFileStatus = Properties.Resources.transmission_send_small_03;
                }
                return imgFileStatus;
            }
        }

        public FileClass File
        {
            get
            {
                return file;
            }

            set
            {
                file = value;
            }
        }

        /// <summary>
        /// 文件属性
        /// </summary>
        private FileClass file;


        /// <summary>
        /// 文件发送状态
        /// </summary>
        //public Logic.FileStatus FileStatus = Logic.FileStatus.send;

        public int OffSetX = 2;
        [Browsable(true), Description("最大值")]
        public int Maximum = 0;
        [Browsable(true), Description("最小值")]
        public int Minimum = 0;

        [Browsable(true), Description("百分比0到100")]
        public int value = 0;
        
        [Browsable(true), Description("文件信息")]
        public string Info = string.Empty;

        [Browsable(true), Description("次要信息")]
        public string Info_2 = string.Empty;

        public Size imgSize = new Size(40, 40);

        [Browsable(true), Description("展示图片")]
        public Image icoImage = null;

        [Browsable(true), Description("进度条背景颜色")]
        public Color LineBGColor = Color.FromArgb(179, 179, 179);

        [Browsable(true), Description("进度条当前进度颜色")]
        public Color LineActiveGColor = Color.FromArgb(69,197,64);//Color.FromArgb(50,69,100);

        [Browsable(true), Description("进度条宽度")]
        public int LineWidth = 5;

        [Browsable(true), Description("文件名称字体")]
        public Font titleFont = new Font("微软雅黑", 10.0f);

        [Browsable(true), Description("文件进度字体")]
        public Font SizeFont = new Font("微软雅黑", 9.0f);

        [Browsable(true), Description("文件进度字体颜色")]
        public Color SizeColor = Color.FromArgb(179, 179, 179);

        Timer tm = new Timer();

        #region 功能键可设定属性

        [Browsable(true), Description("功能键字体颜色")]
        public Color func_Font_Color = Color.FromArgb(38, 133, 227);

        [Browsable(true), Description("功能键字体-左")]
        public Font func_Font = new Font("微软雅黑", 9.0f);


        [Browsable(true), Description("功能键字体-左")]
        public Font func_Font_underline = new Font("微软雅黑", 9.0f,FontStyle.Underline);

        [Browsable(true), Description("功能键字体-左")]
        public Font func_Font_Left = new Font("微软雅黑", 9.0f);


        [Browsable(true), Description("功能键字体-中")]
        public Font func_Font_Center = new Font("微软雅黑", 9.0f);

        [Browsable(true), Description("功能键字体-右")]
        public Font func_Font_Right = new Font("微软雅黑", 9.0f);

        [Browsable(true), Description("左边功能键文本")]
        public string lefButton_text = "打开";

        [Browsable(true), Description("中间功能键文本")]
        public string centerButton_text = "打开文件夹";

        [Browsable(true), Description("右侧功能键文本")]
        public string rightButton_text ="取消";

        [Browsable(true), Description("左侧功能键是否显示")]
        public bool Visible_Left = true;

        [Browsable(true), Description("中间功能键是否显示")]
        public bool Visible_Center = true;

        [Browsable(true), Description("右侧功能键是否显示")]
        public bool Visible_Right = true;
        #endregion

        #region 功能键

       
        /// <summary>
        /// 左侧功能键区域
        /// </summary>
        private Rectangle leftRect = Rectangle.Empty;

        /// <summary>
        /// 中间功能键区域
        /// </summary>
        private Rectangle centerRect = Rectangle.Empty;

        /// <summary>
        /// 右侧功能键区域
        /// </summary>
        private Rectangle rightRect = Rectangle.Empty;

        #region 上传相关参数
        /// <summary>
        /// 本地保存的文件夹
        /// </summary>
        public string SavePath = string.Empty;

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName = string.Empty;


        /// <summary>
        /// 上传任务的Id
        /// </summary>
        public string missionId = string.Empty;

        #endregion

        /// <summary>
        /// 参数-左
        /// </summary>
        public object leftParams = null;
        /// <summary>
        /// 参数-中
        /// </summary>
        public object centerParams = null;
        /// <summary>
        /// 参数-右
        /// </summary>
        public object rightParams = null;

        /// <summary>
        /// 左侧功能键实现方法代理
        /// </summary>
        /// <param name="param"></param>
        public delegate void leftFunc(object intoParams);
        /// <summary>
        /// 中间功能键实现方法代理
        /// </summary>
        /// <param name="param"></param>
        public delegate void centerFunc(object intoParams);
        /// <summary>
        /// 右侧功能键实现方法代理
        /// </summary>
        /// <param name="param"></param>
        public delegate void RightFunc(object intoParams);

        /// <summary>
        /// 左侧功能键实现方法
        /// </summary>
        public leftFunc lfunc;
        /// <summary>
        /// 中间实现方法
        /// </summary>
        public centerFunc cfunc;
        /// <summary>
        /// 右侧实现方法
        /// </summary>
        public RightFunc rfunc;
        #endregion

        private Rectangle imgRect = new Rectangle(new Point(0, 0), new Size(40, 40));
        private Rectangle InfoRect = Rectangle.Empty;
        private Rectangle imgFileStatusRect = Rectangle.Empty;
        public ItemUploadFile() {
            //init();
            tm.Interval = 10;
            tm.Tick += Tm_Tick;
            tm.Start();
        }

        public ItemUploadFile(string fileName)
        {
            //this.icoImage = Image.FromFile(fileName);
            icoLoad(fileName);
            tm.Interval = 10;
            tm.Tick += Tm_Tick;
            tm.Start();
        }

        public void icoLoad(string fileName)
        {
            this.icoImage = Image.FromFile(Common.retIcoPathByFileName(fileName));
        }




        private void Tm_Tick(object sender, EventArgs e)
        {
            if (this.Parent!=null && this.Parent.Visible == true)
            {
                this.Parent.Invoke(new Action(
                funcButtonStyleChange));
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            if (this.icoImage != null)
            {
                g.DrawImage(this.icoImage, this.imgRect);
            }
            StringFormat sf = new StringFormat();
            sf.Trimming = StringTrimming.EllipsisCharacter;
            sf.FormatFlags = StringFormatFlags.NoClip;
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Center;
            int titleHeight = (int)(Math.Ceiling(g.MeasureString(Info, this.Font).Height));

            #region 状态图标
            imgFileStatusRect = new Rectangle(new Point(40 + OffSetX, 0), new Size(ImgFileStatus.Width, ImgFileStatus.Height));
            g.DrawImage(this.ImgFileStatus, this.imgFileStatusRect);
            #endregion

            #region 标题
            InfoRect = new Rectangle(new Point(this.imgFileStatusRect.X+this.imgFileStatusRect.Width, 0), new Size(this.Width-(this.imgFileStatusRect.X + this.imgFileStatusRect.Width), titleHeight));
            g.DrawString(Info, this.titleFont, new SolidBrush(this.ForeColor), InfoRect, sf);
            #endregion

           
            int lineLength = this.Width - this.imgRect.Width - OffSetX;
            int defaultLine = this.Width - this.imgRect.Width - OffSetX;
            Rectangle lineRect = new Rectangle(new Point(this.imgRect.Width + OffSetX, 20), new Size(defaultLine, this.LineWidth));
            g.FillRectangle(new SolidBrush(this.LineBGColor), lineRect);
            


            int x_info2 = imgRect.X + imgRect.Width + OffSetX;
            int y_info2 = lineRect.Y + lineRect.Height;

            int bottomHeight = this.Height - y_info2;
            Rectangle info2 = new Rectangle(new Point(x_info2, y_info2), new Size(this.Width - this.imgRect.Width, bottomHeight));
            if (Info_2.Length > SysParams.Limit_StrLength_FileSize)
            {
                Info_2 = Info_2.Substring(0, SysParams.Limit_StrLength_FileSize)+"..";
            }
            g.DrawString(Info_2, this.SizeFont, new SolidBrush(this.SizeColor), info2, sf);


            #region 功能键区域重绘
            SizeF f = g.MeasureString(rightButton_text, this.func_Font_Right);

            int f_x = this.Width - (int)(Math.Ceiling(f.Width));
            int f_y = y_info2;
            rightRect = new Rectangle(new Point(f_x, f_y), new Size((int)(Math.Ceiling(f.Width)), bottomHeight));
            if (Visible_Right)
            {
                g.DrawString(rightButton_text, this.func_Font_Right, new SolidBrush(this.func_Font_Color), rightRect, sf);
            }

            f = g.MeasureString(centerButton_text, this.func_Font_Center);
            f_x = f_x - (int)(Math.Ceiling(f.Width));
            centerRect = new Rectangle(new Point(f_x, f_y), new Size((int)(Math.Ceiling(f.Width)), bottomHeight));
            if (Visible_Center)
            {
                g.DrawString(centerButton_text, this.func_Font_Center, new SolidBrush(this.func_Font_Color), centerRect, sf);
            }

            f = g.MeasureString(lefButton_text, this.func_Font_Left);
            f_x = f_x - (int)(Math.Ceiling(f.Width));
            leftRect = new Rectangle(new Point(f_x, f_y), new Size((int)(Math.Ceiling(f.Width)), bottomHeight));
            if (Visible_Left)
            {
                g.DrawString(lefButton_text, this.func_Font_Left, new SolidBrush(this.func_Font_Color), leftRect, sf);
            }
          
            #endregion
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            int lineLength = this.Width - this.imgRect.Width;
            int currentLine = (this.Width - this.imgRect.Width - OffSetX)*value/100;
            Rectangle lineRect = new Rectangle(new Point(this.imgRect.Width + OffSetX, 20), new Size(currentLine, this.LineWidth));
            g.FillRectangle(new SolidBrush(this.LineActiveGColor), lineRect);

            Console.WriteLine("P:{0}/{1}", currentLine, lineLength);

        }


        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            
        }

     
        private bool isRight = true;
        private bool isLeft = false;
        private bool isCenter = false;


   

        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);

            
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            //funcButtonStyleChange();
            Point p = this.PointToClient(Control.MousePosition);
            if (Visible_Left && leftRect.Contains(p)&&this.lfunc!=null)
            {
                this.lfunc.Invoke(leftParams);
            }
            else if (Visible_Center && centerRect.Contains(p)&&cfunc!=null)
            {
                this.cfunc.Invoke(centerParams);
            }
            else if (Visible_Right && rightRect.Contains(p)&& rfunc!=null)
            {
                this.rfunc.Invoke(rightParams);
            }
        }
      

        private void funcButtonStyleChange()
        {
            if (this.IsDisposed)
            {
                tm.Stop();
                if (icoImage != null)
                {
                    icoImage.Dispose();
                }
                return;
            }
            int x = Visible_Left ? leftRect.X : centerRect.X;
            int y = centerRect.Y;
            int width = this.Width - x;
            Rectangle tmpRect = new Rectangle(new Point(x, y), new Size(width, centerRect.Height));
            Point p = Point.Empty;
            try
            {
                p = this.PointToClient(Control.MousePosition);


                if (Visible_Right && rightRect.Contains(p) && !isRight)
                {
                    func_Font_Right = func_Font_underline;
                    func_Font_Left = func_Font_Center = func_Font;
                    this.Cursor = Cursors.Hand;
                    isRight = true;
                    isCenter = false;
                    isLeft = false;
                    this.Invalidate(tmpRect);
                }
                else if (isRight && Visible_Right && !rightRect.Contains(p))
                {
                    func_Font_Right = func_Font;
                    this.Cursor = Cursors.Arrow;
                    isRight = false;

                    this.Invalidate(tmpRect);
                }

                if (Visible_Center && centerRect.Contains(p) && !isCenter)
                {
                    func_Font_Center = func_Font_underline;
                    func_Font_Right = func_Font_Left = func_Font;
                    this.Cursor = Cursors.Hand;
                    isCenter = true;
                    isLeft = false;
                    isRight = false;
                    this.Invalidate(tmpRect);
                }
                else if (isCenter && Visible_Center && !centerRect.Contains(p))
                {
                    func_Font_Center = func_Font;
                    this.Cursor = Cursors.Arrow;
                    isCenter = false;
                    this.Invalidate(tmpRect);
                }


                if (!isLeft && Visible_Left && leftRect.Contains(p))
                {
                    func_Font_Left = func_Font_underline;
                    func_Font_Center = func_Font_Right = func_Font;
                    this.Cursor = Cursors.Hand;
                    isLeft = true;
                    isRight = false;
                    isCenter = false;
                    this.Invalidate(tmpRect);
                }
                else if (isLeft && Visible_Left && !leftRect.Contains(p))
                {
                    func_Font_Left = func_Font;
                    this.Cursor = Cursors.Arrow;
                    isLeft = false;

                    this.Invalidate(tmpRect);
                }
            }
            catch (Exception ex)
            {
                
                string ms = ex.Message;
            }
             
           
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ItemUploadFile
            // 
            this.Visible = false;
            this.ResumeLayout(false);

        }
    }
}
