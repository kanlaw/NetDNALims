namespace NetDLims.Forms
{
    partial class BaseTemplateForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseTemplateForm));
            this.panelWeb = new System.Windows.Forms.Panel();
            this.ibtnLK = new NetDLims.Controls.ImageButton();
            this.panel_Logined = new System.Windows.Forms.Panel();
            this.pictureBox_Grade = new System.Windows.Forms.PictureBox();
            this.label_UserName = new System.Windows.Forms.Label();
            this.pleaseLoginBtn = new System.Windows.Forms.Button();
            this.panelIM = new System.Windows.Forms.Panel();
            this.ibtnClose = new NetDLims.Controls.ImageButton();
            this.ibtnMin = new NetDLims.Controls.ImageButton();
            this.ibtnMax = new NetDLims.Controls.ImageButton();
            this.userPhotoBtn = new NetDLims.Controls.ImageButton();
            this.btnXTGL = new NetDLims.Controls.ImageButton();
            this.btnJSZD = new NetDLims.Controls.ImageButton();
            this.btnKJCX = new NetDLims.Controls.ImageButton();
            this.btnJSZN = new NetDLims.Controls.ImageButton();
            this.btnXXJL = new NetDLims.Controls.ImageButton();
            this.btnMain = new NetDLims.Controls.ImageButton();
            this.btnSZXT = new NetDLims.Controls.ImageButton();
            this.panelWeb.SuspendLayout();
            this.panel_Logined.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Grade)).BeginInit();
            this.SuspendLayout();
            // 
            // panelWeb
            // 
            this.panelWeb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelWeb.BackColor = System.Drawing.Color.White;
            this.panelWeb.Controls.Add(this.ibtnLK);
            this.panelWeb.Location = new System.Drawing.Point(0, 90);
            this.panelWeb.Name = "panelWeb";
            this.panelWeb.Size = new System.Drawing.Size(1086, 678);
            this.panelWeb.TabIndex = 44;
            // 
            // ibtnLK
            // 
            this.ibtnLK.Activepic = ((System.Drawing.Image)(resources.GetObject("ibtnLK.Activepic")));
            this.ibtnLK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ibtnLK.BackImage = null;
            this.ibtnLK.ColorBack = System.Drawing.Color.Empty;
            this.ibtnLK.Location = new System.Drawing.Point(1076, 313);
            this.ibtnLK.Name = "ibtnLK";
            this.ibtnLK.Presspic = ((System.Drawing.Image)(resources.GetObject("ibtnLK.Presspic")));
            this.ibtnLK.ShowText = null;
            this.ibtnLK.Size = new System.Drawing.Size(10, 76);
            this.ibtnLK.Staticpic = ((System.Drawing.Image)(resources.GetObject("ibtnLK.Staticpic")));
            this.ibtnLK.Stretch = false;
            this.ibtnLK.SupportToggle = false;
            this.ibtnLK.TabIndex = 0;
            this.ibtnLK.Text = "imageButton1";
            this.ibtnLK.Toggle = false;
            this.ibtnLK.ToolTipOpacity = 1D;
            this.ibtnLK.ToolTipShown = false;
            this.ibtnLK.TooltipX = 0;
            this.ibtnLK.TooltipY = 0;
            this.ibtnLK.Unablepic = null;
            this.ibtnLK.UseVisualStyleBackColor = true;
            this.ibtnLK.Click += new System.EventHandler(this.ibtnLK_Click);
            // 
            // panel_Logined
            // 
            this.panel_Logined.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_Logined.BackColor = System.Drawing.Color.Transparent;
            this.panel_Logined.Controls.Add(this.pictureBox_Grade);
            this.panel_Logined.Controls.Add(this.label_UserName);
            this.panel_Logined.Location = new System.Drawing.Point(1229, 26);
            this.panel_Logined.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.panel_Logined.Name = "panel_Logined";
            this.panel_Logined.Size = new System.Drawing.Size(128, 38);
            this.panel_Logined.TabIndex = 59;
            this.panel_Logined.Visible = false;
            this.panel_Logined.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Logined_Paint);
            // 
            // pictureBox_Grade
            // 
            this.pictureBox_Grade.Image = global::NetDLims.Properties.Resources.level_03;
            this.pictureBox_Grade.Location = new System.Drawing.Point(66, 11);
            this.pictureBox_Grade.Name = "pictureBox_Grade";
            this.pictureBox_Grade.Size = new System.Drawing.Size(36, 14);
            this.pictureBox_Grade.TabIndex = 61;
            this.pictureBox_Grade.TabStop = false;
            this.pictureBox_Grade.Visible = false;
            // 
            // label_UserName
            // 
            this.label_UserName.AutoSize = true;
            this.label_UserName.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.label_UserName.ForeColor = System.Drawing.Color.White;
            this.label_UserName.Location = new System.Drawing.Point(7, 8);
            this.label_UserName.Name = "label_UserName";
            this.label_UserName.Size = new System.Drawing.Size(51, 19);
            this.label_UserName.TabIndex = 60;
            this.label_UserName.Text = "label1";
            this.label_UserName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label_UserName.Visible = false;
            // 
            // pleaseLoginBtn
            // 
            this.pleaseLoginBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pleaseLoginBtn.BackColor = System.Drawing.Color.Transparent;
            this.pleaseLoginBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pleaseLoginBtn.FlatAppearance.BorderSize = 0;
            this.pleaseLoginBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pleaseLoginBtn.ForeColor = System.Drawing.Color.White;
            this.pleaseLoginBtn.Location = new System.Drawing.Point(1224, 34);
            this.pleaseLoginBtn.Name = "pleaseLoginBtn";
            this.pleaseLoginBtn.Size = new System.Drawing.Size(75, 23);
            this.pleaseLoginBtn.TabIndex = 59;
            this.pleaseLoginBtn.Text = "请登陆账号";
            this.pleaseLoginBtn.UseVisualStyleBackColor = true;
            this.pleaseLoginBtn.Click += new System.EventHandler(this.pleaseLoginBtn_Click);
            this.pleaseLoginBtn.MouseEnter += new System.EventHandler(this.pleaseLoginBtn_MouseEnter);
            this.pleaseLoginBtn.MouseLeave += new System.EventHandler(this.pleaseLoginBtn_MouseLeave);
            // 
            // panelIM
            // 
            this.panelIM.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelIM.AutoSize = true;
            this.panelIM.Location = new System.Drawing.Point(1086, 90);
            this.panelIM.Name = "panelIM";
            this.panelIM.Size = new System.Drawing.Size(280, 678);
            this.panelIM.TabIndex = 61;
            // 
            // ibtnClose
            // 
            this.ibtnClose.Activepic = ((System.Drawing.Image)(resources.GetObject("ibtnClose.Activepic")));
            this.ibtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ibtnClose.BackImage = null;
            this.ibtnClose.ColorBack = System.Drawing.Color.Empty;
            this.ibtnClose.Location = new System.Drawing.Point(1337, 0);
            this.ibtnClose.Name = "ibtnClose";
            this.ibtnClose.Presspic = ((System.Drawing.Image)(resources.GetObject("ibtnClose.Presspic")));
            this.ibtnClose.ShowText = null;
            this.ibtnClose.Size = new System.Drawing.Size(29, 24);
            this.ibtnClose.Staticpic = ((System.Drawing.Image)(resources.GetObject("ibtnClose.Staticpic")));
            this.ibtnClose.Stretch = false;
            this.ibtnClose.SupportToggle = false;
            this.ibtnClose.TabIndex = 1;
            this.ibtnClose.Text = "ibtnClose";
            this.ibtnClose.Toggle = false;
            this.ibtnClose.ToolTipOpacity = 1D;
            this.ibtnClose.ToolTipShown = false;
            this.ibtnClose.TooltipX = 0;
            this.ibtnClose.TooltipY = 0;
            this.ibtnClose.Unablepic = null;
            this.ibtnClose.UseVisualStyleBackColor = true;
            this.ibtnClose.Click += new System.EventHandler(this.ibtnClose_Click);
            // 
            // ibtnMin
            // 
            this.ibtnMin.Activepic = ((System.Drawing.Image)(resources.GetObject("ibtnMin.Activepic")));
            this.ibtnMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ibtnMin.BackImage = null;
            this.ibtnMin.ColorBack = System.Drawing.Color.Empty;
            this.ibtnMin.Location = new System.Drawing.Point(1267, 0);
            this.ibtnMin.Name = "ibtnMin";
            this.ibtnMin.Presspic = ((System.Drawing.Image)(resources.GetObject("ibtnMin.Presspic")));
            this.ibtnMin.ShowText = null;
            this.ibtnMin.Size = new System.Drawing.Size(29, 24);
            this.ibtnMin.Staticpic = ((System.Drawing.Image)(resources.GetObject("ibtnMin.Staticpic")));
            this.ibtnMin.Stretch = false;
            this.ibtnMin.SupportToggle = false;
            this.ibtnMin.TabIndex = 60;
            this.ibtnMin.Text = "ibtnMin";
            this.ibtnMin.Toggle = false;
            this.ibtnMin.ToolTipOpacity = 1D;
            this.ibtnMin.ToolTipShown = false;
            this.ibtnMin.TooltipX = 0;
            this.ibtnMin.TooltipY = 0;
            this.ibtnMin.Unablepic = null;
            this.ibtnMin.UseVisualStyleBackColor = true;
            this.ibtnMin.Click += new System.EventHandler(this.ibtnMin_Click);
            // 
            // ibtnMax
            // 
            this.ibtnMax.Activepic = ((System.Drawing.Image)(resources.GetObject("ibtnMax.Activepic")));
            this.ibtnMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ibtnMax.BackImage = null;
            this.ibtnMax.ColorBack = System.Drawing.Color.Empty;
            this.ibtnMax.Location = new System.Drawing.Point(1302, 0);
            this.ibtnMax.Name = "ibtnMax";
            this.ibtnMax.Presspic = ((System.Drawing.Image)(resources.GetObject("ibtnMax.Presspic")));
            this.ibtnMax.ShowText = null;
            this.ibtnMax.Size = new System.Drawing.Size(29, 24);
            this.ibtnMax.Staticpic = ((System.Drawing.Image)(resources.GetObject("ibtnMax.Staticpic")));
            this.ibtnMax.Stretch = false;
            this.ibtnMax.SupportToggle = false;
            this.ibtnMax.TabIndex = 0;
            this.ibtnMax.Text = "ibtnMax";
            this.ibtnMax.Toggle = false;
            this.ibtnMax.ToolTipOpacity = 1D;
            this.ibtnMax.ToolTipShown = false;
            this.ibtnMax.TooltipX = 0;
            this.ibtnMax.TooltipY = 0;
            this.ibtnMax.Unablepic = null;
            this.ibtnMax.UseVisualStyleBackColor = true;
            this.ibtnMax.Click += new System.EventHandler(this.ibtnMax_Click);
            // 
            // userPhotoBtn
            // 
            this.userPhotoBtn.Activepic = ((System.Drawing.Image)(resources.GetObject("userPhotoBtn.Activepic")));
            this.userPhotoBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.userPhotoBtn.BackImage = null;
            this.userPhotoBtn.ColorBack = System.Drawing.Color.Empty;
            this.userPhotoBtn.Location = new System.Drawing.Point(1185, 26);
            this.userPhotoBtn.Name = "userPhotoBtn";
            this.userPhotoBtn.Presspic = ((System.Drawing.Image)(resources.GetObject("userPhotoBtn.Presspic")));
            this.userPhotoBtn.ShowText = null;
            this.userPhotoBtn.Size = new System.Drawing.Size(38, 38);
            this.userPhotoBtn.Staticpic = ((System.Drawing.Image)(resources.GetObject("userPhotoBtn.Staticpic")));
            this.userPhotoBtn.Stretch = false;
            this.userPhotoBtn.SupportToggle = false;
            this.userPhotoBtn.TabIndex = 58;
            this.userPhotoBtn.TabStop = false;
            this.userPhotoBtn.Text = "imageButton1";
            this.userPhotoBtn.Toggle = false;
            this.userPhotoBtn.ToolTipOpacity = 1D;
            this.userPhotoBtn.ToolTipShown = false;
            this.userPhotoBtn.TooltipX = 0;
            this.userPhotoBtn.TooltipY = 0;
            this.userPhotoBtn.Unablepic = null;
            this.userPhotoBtn.UseVisualStyleBackColor = true;
            this.userPhotoBtn.Click += new System.EventHandler(this.userPhotoBtn_Click);
            // 
            // btnXTGL
            // 
            this.btnXTGL.Activepic = ((System.Drawing.Image)(resources.GetObject("btnXTGL.Activepic")));
            this.btnXTGL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXTGL.BackImage = null;
            this.btnXTGL.ColorBack = System.Drawing.Color.Empty;
            this.btnXTGL.Location = new System.Drawing.Point(1028, 0);
            this.btnXTGL.Name = "btnXTGL";
            this.btnXTGL.Presspic = ((System.Drawing.Image)(resources.GetObject("btnXTGL.Presspic")));
            this.btnXTGL.ShowText = "系统管理";
            this.btnXTGL.Size = new System.Drawing.Size(78, 90);
            this.btnXTGL.Staticpic = ((System.Drawing.Image)(resources.GetObject("btnXTGL.Staticpic")));
            this.btnXTGL.Stretch = false;
            this.btnXTGL.SupportToggle = false;
            this.btnXTGL.TabIndex = 53;
            this.btnXTGL.Text = "系统管理";
            this.btnXTGL.Toggle = false;
            this.btnXTGL.ToolTipOpacity = 1D;
            this.btnXTGL.ToolTipShown = false;
            this.btnXTGL.TooltipX = 0;
            this.btnXTGL.TooltipY = 0;
            this.btnXTGL.Unablepic = null;
            this.btnXTGL.UseVisualStyleBackColor = true;
            this.btnXTGL.Visible = false;
            this.btnXTGL.Click += new System.EventHandler(this.btnXTGL_Click);
            // 
            // btnJSZD
            // 
            this.btnJSZD.Activepic = ((System.Drawing.Image)(resources.GetObject("btnJSZD.Activepic")));
            this.btnJSZD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnJSZD.BackColor = System.Drawing.Color.Transparent;
            this.btnJSZD.BackImage = null;
            this.btnJSZD.ColorBack = System.Drawing.Color.Empty;
            this.btnJSZD.Location = new System.Drawing.Point(636, 0);
            this.btnJSZD.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.btnJSZD.Name = "btnJSZD";
            this.btnJSZD.Presspic = ((System.Drawing.Image)(resources.GetObject("btnJSZD.Presspic")));
            this.btnJSZD.ShowText = "技术指导";
            this.btnJSZD.Size = new System.Drawing.Size(78, 90);
            this.btnJSZD.Staticpic = ((System.Drawing.Image)(resources.GetObject("btnJSZD.Staticpic")));
            this.btnJSZD.Stretch = false;
            this.btnJSZD.SupportToggle = false;
            this.btnJSZD.TabIndex = 52;
            this.btnJSZD.Text = "技术指导";
            this.btnJSZD.Toggle = false;
            this.btnJSZD.ToolTipOpacity = 1D;
            this.btnJSZD.ToolTipShown = false;
            this.btnJSZD.TooltipX = 0;
            this.btnJSZD.TooltipY = 0;
            this.btnJSZD.Unablepic = null;
            this.btnJSZD.UseVisualStyleBackColor = false;
            this.btnJSZD.Click += new System.EventHandler(this.imageButton3_Click);
            // 
            // btnKJCX
            // 
            this.btnKJCX.Activepic = ((System.Drawing.Image)(resources.GetObject("btnKJCX.Activepic")));
            this.btnKJCX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnKJCX.BackImage = null;
            this.btnKJCX.ColorBack = System.Drawing.Color.Empty;
            this.btnKJCX.Location = new System.Drawing.Point(734, 0);
            this.btnKJCX.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.btnKJCX.Name = "btnKJCX";
            this.btnKJCX.Presspic = ((System.Drawing.Image)(resources.GetObject("btnKJCX.Presspic")));
            this.btnKJCX.ShowText = "科技创新";
            this.btnKJCX.Size = new System.Drawing.Size(78, 90);
            this.btnKJCX.Staticpic = ((System.Drawing.Image)(resources.GetObject("btnKJCX.Staticpic")));
            this.btnKJCX.Stretch = false;
            this.btnKJCX.SupportToggle = false;
            this.btnKJCX.TabIndex = 54;
            this.btnKJCX.Text = "科技创新";
            this.btnKJCX.Toggle = false;
            this.btnKJCX.ToolTipOpacity = 1D;
            this.btnKJCX.ToolTipShown = false;
            this.btnKJCX.TooltipX = 0;
            this.btnKJCX.TooltipY = 0;
            this.btnKJCX.Unablepic = null;
            this.btnKJCX.UseVisualStyleBackColor = true;
            this.btnKJCX.Click += new System.EventHandler(this.btnKJCX_Click);
            // 
            // btnJSZN
            // 
            this.btnJSZN.Activepic = ((System.Drawing.Image)(resources.GetObject("btnJSZN.Activepic")));
            this.btnJSZN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnJSZN.BackImage = null;
            this.btnJSZN.ColorBack = System.Drawing.Color.Empty;
            this.btnJSZN.Location = new System.Drawing.Point(538, 0);
            this.btnJSZN.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.btnJSZN.Name = "btnJSZN";
            this.btnJSZN.Presspic = ((System.Drawing.Image)(resources.GetObject("btnJSZN.Presspic")));
            this.btnJSZN.ShowText = "建设指南";
            this.btnJSZN.Size = new System.Drawing.Size(78, 90);
            this.btnJSZN.Staticpic = ((System.Drawing.Image)(resources.GetObject("btnJSZN.Staticpic")));
            this.btnJSZN.Stretch = false;
            this.btnJSZN.SupportToggle = false;
            this.btnJSZN.TabIndex = 51;
            this.btnJSZN.Text = "建设指南";
            this.btnJSZN.Toggle = false;
            this.btnJSZN.ToolTipOpacity = 1D;
            this.btnJSZN.ToolTipShown = false;
            this.btnJSZN.TooltipX = 0;
            this.btnJSZN.TooltipY = 0;
            this.btnJSZN.Unablepic = null;
            this.btnJSZN.UseVisualStyleBackColor = true;
            this.btnJSZN.Click += new System.EventHandler(this.btnJSZN_Click);
            // 
            // btnXXJL
            // 
            this.btnXXJL.Activepic = ((System.Drawing.Image)(resources.GetObject("btnXXJL.Activepic")));
            this.btnXXJL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXXJL.BackImage = null;
            this.btnXXJL.ColorBack = System.Drawing.Color.Empty;
            this.btnXXJL.Location = new System.Drawing.Point(832, 0);
            this.btnXXJL.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.btnXXJL.Name = "btnXXJL";
            this.btnXXJL.Presspic = ((System.Drawing.Image)(resources.GetObject("btnXXJL.Presspic")));
            this.btnXXJL.ShowText = "学习交流";
            this.btnXXJL.Size = new System.Drawing.Size(78, 90);
            this.btnXXJL.Staticpic = ((System.Drawing.Image)(resources.GetObject("btnXXJL.Staticpic")));
            this.btnXXJL.Stretch = false;
            this.btnXXJL.SupportToggle = false;
            this.btnXXJL.TabIndex = 55;
            this.btnXXJL.Text = "学习交流";
            this.btnXXJL.Toggle = false;
            this.btnXXJL.ToolTipOpacity = 1D;
            this.btnXXJL.ToolTipShown = false;
            this.btnXXJL.TooltipX = 0;
            this.btnXXJL.TooltipY = 0;
            this.btnXXJL.Unablepic = null;
            this.btnXXJL.UseVisualStyleBackColor = true;
            this.btnXXJL.Click += new System.EventHandler(this.btnXXJL_Click);
            // 
            // btnMain
            // 
            this.btnMain.Activepic = ((System.Drawing.Image)(resources.GetObject("btnMain.Activepic")));
            this.btnMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMain.BackImage = null;
            this.btnMain.ColorBack = System.Drawing.Color.Empty;
            this.btnMain.Location = new System.Drawing.Point(440, 0);
            this.btnMain.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.btnMain.Name = "btnMain";
            this.btnMain.Presspic = ((System.Drawing.Image)(resources.GetObject("btnMain.Presspic")));
            this.btnMain.ShowText = "首页";
            this.btnMain.Size = new System.Drawing.Size(78, 90);
            this.btnMain.Staticpic = ((System.Drawing.Image)(resources.GetObject("btnMain.Staticpic")));
            this.btnMain.Stretch = false;
            this.btnMain.SupportToggle = false;
            this.btnMain.TabIndex = 50;
            this.btnMain.Text = "首页";
            this.btnMain.Toggle = false;
            this.btnMain.ToolTipOpacity = 1D;
            this.btnMain.ToolTipShown = false;
            this.btnMain.TooltipX = 0;
            this.btnMain.TooltipY = 0;
            this.btnMain.Unablepic = null;
            this.btnMain.UseVisualStyleBackColor = true;
            this.btnMain.Click += new System.EventHandler(this.btnMain_Click);
            // 
            // btnSZXT
            // 
            this.btnSZXT.Activepic = ((System.Drawing.Image)(resources.GetObject("btnSZXT.Activepic")));
            this.btnSZXT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSZXT.BackImage = null;
            this.btnSZXT.ColorBack = System.Drawing.Color.Empty;
            this.btnSZXT.Location = new System.Drawing.Point(930, 0);
            this.btnSZXT.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.btnSZXT.Name = "btnSZXT";
            this.btnSZXT.Presspic = ((System.Drawing.Image)(resources.GetObject("btnSZXT.Presspic")));
            this.btnSZXT.ShowText = "实战协同";
            this.btnSZXT.Size = new System.Drawing.Size(78, 90);
            this.btnSZXT.Staticpic = ((System.Drawing.Image)(resources.GetObject("btnSZXT.Staticpic")));
            this.btnSZXT.Stretch = false;
            this.btnSZXT.SupportToggle = false;
            this.btnSZXT.TabIndex = 56;
            this.btnSZXT.Text = "实战协同";
            this.btnSZXT.Toggle = false;
            this.btnSZXT.ToolTipOpacity = 1D;
            this.btnSZXT.ToolTipShown = false;
            this.btnSZXT.TooltipX = 0;
            this.btnSZXT.TooltipY = 0;
            this.btnSZXT.Unablepic = null;
            this.btnSZXT.UseVisualStyleBackColor = true;
            this.btnSZXT.Click += new System.EventHandler(this.btnSZXT_Click);
            // 
            // BaseTemplateForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1366, 768);
            this.Controls.Add(this.panelIM);
            this.Controls.Add(this.ibtnClose);
            this.Controls.Add(this.ibtnMin);
            this.Controls.Add(this.ibtnMax);
            this.Controls.Add(this.panel_Logined);
            this.Controls.Add(this.pleaseLoginBtn);
            this.Controls.Add(this.userPhotoBtn);
            this.Controls.Add(this.btnXTGL);
            this.Controls.Add(this.btnJSZD);
            this.Controls.Add(this.btnKJCX);
            this.Controls.Add(this.btnJSZN);
            this.Controls.Add(this.btnXXJL);
            this.Controls.Add(this.btnMain);
            this.Controls.Add(this.btnSZXT);
            this.Controls.Add(this.panelWeb);
            this.KeyPreview = true;
            this.Name = "BaseTemplateForm";
            this.Shadow = true;
            this.ShowIcon = true;
            this.Text = "全国DNA网络化实验室";
            this.TopMost = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.BaseTemplateForm_Load);
            this.Shown += new System.EventHandler(this.BaseTemplateForm_Shown);
            this.DoubleClick += new System.EventHandler(this.BaseTemplateForm_DoubleClick);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BaseTemplateForm_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BaseTemplateForm_KeyPress);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BaseTemplateForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BaseTemplateForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BaseTemplateForm_MouseUp);
            this.panelWeb.ResumeLayout(false);
            this.panel_Logined.ResumeLayout(false);
            this.panel_Logined.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Grade)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelWeb;
        private Controls.ImageButton btnMain;
        private Controls.ImageButton btnJSZN;
        private Controls.ImageButton btnJSZD;
        private Controls.ImageButton btnXTGL;
        private Controls.ImageButton btnKJCX;
        private Controls.ImageButton btnXXJL;
        private Controls.ImageButton btnSZXT;
        private Controls.ImageButton userPhotoBtn;
        private System.Windows.Forms.Panel panel_Logined;
        private System.Windows.Forms.Button pleaseLoginBtn;
        private System.Windows.Forms.PictureBox pictureBox_Grade;
        private System.Windows.Forms.Label label_UserName;
        private Controls.ImageButton ibtnClose;
        private Controls.ImageButton ibtnMax;
        private Controls.ImageButton ibtnMin;
        private Controls.ImageButton ibtnLK;
        private System.Windows.Forms.Panel panelIM;
    }
}

