namespace IMClient.XMPP.Forms
{
    partial class ChatForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            CCWin.SkinControl.Animation animation1 = new CCWin.SkinControl.Animation();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatForm));
            this.imageButton3 = new IMClient.Controls.ImageButton();
            this.imageButton2 = new IMClient.Controls.ImageButton();
            this.ibtnMin = new IMClient.Controls.ImageButton();
            this.lblCompany = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.pbxFriend = new System.Windows.Forms.PictureBox();
            this.panelChat = new System.Windows.Forms.Panel();
            this.chatBox_history = new JustLib.Controls.ChatBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ibtnOther = new IMClient.Controls.ImageButton();
            this.ibtnVideo = new IMClient.Controls.ImageButton();
            this.ibtFile = new IMClient.Controls.ImageButton();
            this.ibtnScreenshots = new IMClient.Controls.ImageButton();
            this.ibtnPicture = new IMClient.Controls.ImageButton();
            this.ibtnVoice = new IMClient.Controls.ImageButton();
            this.ibtnFont = new IMClient.Controls.ImageButton();
            this.panelSend = new System.Windows.Forms.Panel();
            this.chatBoxSend = new JustLib.Controls.ChatBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.skinTabControl1 = new CCWin.SkinControl.SkinTabControl();
            this.ibtnSend = new IMClient.Controls.ImageButton();
            this.ibtnCancel = new IMClient.Controls.ImageButton();
            this.ibtnHialogue = new IMClient.Controls.ImageButton();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.skinPanel_right = new CCWin.SkinControl.SkinPanel();
            this.skinProgressBar = new CCWin.SkinControl.SkinProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.pbxFriend)).BeginInit();
            this.panelChat.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelSend.SuspendLayout();
            this.panel2.SuspendLayout();
            this.skinPanel_right.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageButton3
            // 
            this.imageButton3.Activepic = global::IMClient.Properties.Resources.close_over_02;
            this.imageButton3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.imageButton3.BackImage = null;
            this.imageButton3.ColorBack = System.Drawing.Color.Empty;
            this.imageButton3.Location = new System.Drawing.Point(672, 3);
            this.imageButton3.Margin = new System.Windows.Forms.Padding(0);
            this.imageButton3.Name = "imageButton3";
            this.imageButton3.Presspic = global::IMClient.Properties.Resources.close_down_02;
            this.imageButton3.ShowText = null;
            this.imageButton3.Size = new System.Drawing.Size(29, 24);
            this.imageButton3.Staticpic = global::IMClient.Properties.Resources.close_normal_02;
            this.imageButton3.Stretch = false;
            this.imageButton3.SupportToggle = false;
            this.imageButton3.TabIndex = 5;
            this.imageButton3.Text = "imageButton3";
            this.imageButton3.Toggle = false;
            this.imageButton3.ToolTipOpacity = 1D;
            this.imageButton3.ToolTipShown = false;
            this.imageButton3.TooltipX = 0;
            this.imageButton3.TooltipY = 0;
            this.imageButton3.Unablepic = null;
            this.imageButton3.UseVisualStyleBackColor = true;
            this.imageButton3.Click += new System.EventHandler(this.imageButton3_Click);
            // 
            // imageButton2
            // 
            this.imageButton2.Activepic = global::IMClient.Properties.Resources.max_over_02;
            this.imageButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.imageButton2.BackImage = null;
            this.imageButton2.ColorBack = System.Drawing.Color.Empty;
            this.imageButton2.Location = new System.Drawing.Point(638, 4);
            this.imageButton2.Name = "imageButton2";
            this.imageButton2.Presspic = global::IMClient.Properties.Resources.max_down_02;
            this.imageButton2.ShowText = null;
            this.imageButton2.Size = new System.Drawing.Size(29, 24);
            this.imageButton2.Staticpic = global::IMClient.Properties.Resources.max_normal_02;
            this.imageButton2.Stretch = false;
            this.imageButton2.SupportToggle = false;
            this.imageButton2.TabIndex = 4;
            this.imageButton2.Text = "imageButton2";
            this.imageButton2.Toggle = false;
            this.imageButton2.ToolTipOpacity = 1D;
            this.imageButton2.ToolTipShown = false;
            this.imageButton2.TooltipX = 0;
            this.imageButton2.TooltipY = 0;
            this.imageButton2.Unablepic = null;
            this.imageButton2.UseVisualStyleBackColor = true;
            this.imageButton2.Click += new System.EventHandler(this.imageButton2_Click);
            // 
            // ibtnMin
            // 
            this.ibtnMin.Activepic = global::IMClient.Properties.Resources.minimize_over_02;
            this.ibtnMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ibtnMin.BackImage = null;
            this.ibtnMin.ColorBack = System.Drawing.Color.Empty;
            this.ibtnMin.Location = new System.Drawing.Point(602, 4);
            this.ibtnMin.Name = "ibtnMin";
            this.ibtnMin.Presspic = global::IMClient.Properties.Resources.minimize_down_02;
            this.ibtnMin.ShowText = null;
            this.ibtnMin.Size = new System.Drawing.Size(29, 24);
            this.ibtnMin.Staticpic = global::IMClient.Properties.Resources.minimize_normal_02;
            this.ibtnMin.Stretch = false;
            this.ibtnMin.SupportToggle = false;
            this.ibtnMin.TabIndex = 3;
            this.ibtnMin.Text = "imageButton1";
            this.ibtnMin.Toggle = false;
            this.ibtnMin.ToolTipOpacity = 1D;
            this.ibtnMin.ToolTipShown = false;
            this.ibtnMin.TooltipX = 0;
            this.ibtnMin.TooltipY = 0;
            this.ibtnMin.Unablepic = null;
            this.ibtnMin.UseVisualStyleBackColor = true;
            this.ibtnMin.Click += new System.EventHandler(this.ibtnMin_Click);
            // 
            // lblCompany
            // 
            this.lblCompany.AutoSize = true;
            this.lblCompany.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCompany.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.lblCompany.Location = new System.Drawing.Point(80, 35);
            this.lblCompany.Margin = new System.Windows.Forms.Padding(0);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(32, 17);
            this.lblCompany.TabIndex = 2;
            this.lblCompany.Text = "单位";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblName.ForeColor = System.Drawing.Color.Transparent;
            this.lblName.Location = new System.Drawing.Point(80, 11);
            this.lblName.Margin = new System.Windows.Forms.Padding(0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(42, 22);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "姓名";
            // 
            // pbxFriend
            // 
            this.pbxFriend.Image = global::IMClient.Properties.Resources.Head_portrait02_index_32;
            this.pbxFriend.Location = new System.Drawing.Point(20, 10);
            this.pbxFriend.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.pbxFriend.Name = "pbxFriend";
            this.pbxFriend.Size = new System.Drawing.Size(45, 45);
            this.pbxFriend.TabIndex = 0;
            this.pbxFriend.TabStop = false;
            // 
            // panelChat
            // 
            this.panelChat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelChat.BackColor = System.Drawing.Color.White;
            this.panelChat.Controls.Add(this.chatBox_history);
            this.panelChat.Location = new System.Drawing.Point(0, 65);
            this.panelChat.Name = "panelChat";
            this.panelChat.Size = new System.Drawing.Size(520, 275);
            this.panelChat.TabIndex = 1;
            // 
            // chatBox_history
            // 
            this.chatBox_history.AllowDrop = true;
            this.chatBox_history.BackColor = System.Drawing.Color.White;
            this.chatBox_history.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chatBox_history.ContextMenuMode = JustLib.Controls.ChatBoxContextMenuMode.ForOutput;
            this.chatBox_history.Location = new System.Drawing.Point(0, 0);
            this.chatBox_history.Margin = new System.Windows.Forms.Padding(0);
            this.chatBox_history.Name = "chatBox_history";
            this.chatBox_history.PopoutImageWhenDoubleClick = true;
            this.chatBox_history.ReadOnly = true;
            this.chatBox_history.Size = new System.Drawing.Size(520, 275);
            this.chatBox_history.TabIndex = 139;
            this.chatBox_history.Text = "";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(236)))), ((int)(((byte)(236)))));
            this.panel1.Controls.Add(this.ibtnOther);
            this.panel1.Controls.Add(this.ibtnVideo);
            this.panel1.Controls.Add(this.ibtFile);
            this.panel1.Controls.Add(this.ibtnScreenshots);
            this.panel1.Controls.Add(this.ibtnPicture);
            this.panel1.Controls.Add(this.ibtnVoice);
            this.panel1.Controls.Add(this.ibtnFont);
            this.panel1.Location = new System.Drawing.Point(0, 340);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(520, 30);
            this.panel1.TabIndex = 2;
            // 
            // ibtnOther
            // 
            this.ibtnOther.Activepic = global::IMClient.Properties.Resources.button_38;
            this.ibtnOther.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ibtnOther.BackImage = null;
            this.ibtnOther.ColorBack = System.Drawing.Color.Empty;
            this.ibtnOther.Location = new System.Drawing.Point(445, 4);
            this.ibtnOther.Margin = new System.Windows.Forms.Padding(0);
            this.ibtnOther.Name = "ibtnOther";
            this.ibtnOther.Presspic = global::IMClient.Properties.Resources.button_38;
            this.ibtnOther.ShowText = null;
            this.ibtnOther.Size = new System.Drawing.Size(56, 20);
            this.ibtnOther.Staticpic = global::IMClient.Properties.Resources.button_36;
            this.ibtnOther.Stretch = false;
            this.ibtnOther.SupportToggle = false;
            this.ibtnOther.TabIndex = 6;
            this.ibtnOther.Text = "imageButton9";
            this.ibtnOther.Toggle = false;
            this.ibtnOther.ToolTipOpacity = 1D;
            this.ibtnOther.ToolTipShown = false;
            this.ibtnOther.TooltipX = 0;
            this.ibtnOther.TooltipY = 0;
            this.ibtnOther.Unablepic = null;
            this.ibtnOther.UseVisualStyleBackColor = true;
            // 
            // ibtnVideo
            // 
            this.ibtnVideo.Activepic = global::IMClient.Properties.Resources.video_im_over_03;
            this.ibtnVideo.BackImage = null;
            this.ibtnVideo.ColorBack = System.Drawing.Color.Empty;
            this.ibtnVideo.Location = new System.Drawing.Point(170, 5);
            this.ibtnVideo.Margin = new System.Windows.Forms.Padding(0);
            this.ibtnVideo.Name = "ibtnVideo";
            this.ibtnVideo.Presspic = global::IMClient.Properties.Resources.video_im_over_03;
            this.ibtnVideo.ShowText = null;
            this.ibtnVideo.Size = new System.Drawing.Size(20, 20);
            this.ibtnVideo.Staticpic = global::IMClient.Properties.Resources.video_im_normal_07;
            this.ibtnVideo.Stretch = false;
            this.ibtnVideo.SupportToggle = false;
            this.ibtnVideo.TabIndex = 5;
            this.ibtnVideo.Text = "imageButton8";
            this.ibtnVideo.Toggle = false;
            this.ibtnVideo.ToolTipOpacity = 1D;
            this.ibtnVideo.ToolTipShown = false;
            this.ibtnVideo.TooltipX = 0;
            this.ibtnVideo.TooltipY = 0;
            this.ibtnVideo.Unablepic = null;
            this.ibtnVideo.UseVisualStyleBackColor = true;
            // 
            // ibtFile
            // 
            this.ibtFile.Activepic = global::IMClient.Properties.Resources.file_im_over_03;
            this.ibtFile.BackImage = null;
            this.ibtFile.ColorBack = System.Drawing.Color.Empty;
            this.ibtFile.Location = new System.Drawing.Point(140, 5);
            this.ibtFile.Margin = new System.Windows.Forms.Padding(0);
            this.ibtFile.Name = "ibtFile";
            this.ibtFile.Presspic = global::IMClient.Properties.Resources.file_im_over_03;
            this.ibtFile.ShowText = null;
            this.ibtFile.Size = new System.Drawing.Size(20, 20);
            this.ibtFile.Staticpic = global::IMClient.Properties.Resources.file_im_normal_07;
            this.ibtFile.Stretch = false;
            this.ibtFile.SupportToggle = false;
            this.ibtFile.TabIndex = 4;
            this.ibtFile.Text = "imageButton7";
            this.ibtFile.Toggle = false;
            this.ibtFile.ToolTipOpacity = 1D;
            this.ibtFile.ToolTipShown = false;
            this.ibtFile.TooltipX = 0;
            this.ibtFile.TooltipY = 0;
            this.ibtFile.Unablepic = null;
            this.ibtFile.UseVisualStyleBackColor = true;
            this.ibtFile.Click += new System.EventHandler(this.ibtFile_Click);
            // 
            // ibtnScreenshots
            // 
            this.ibtnScreenshots.Activepic = global::IMClient.Properties.Resources.screenshots_im_over_03;
            this.ibtnScreenshots.BackImage = null;
            this.ibtnScreenshots.ColorBack = System.Drawing.Color.Empty;
            this.ibtnScreenshots.Location = new System.Drawing.Point(110, 5);
            this.ibtnScreenshots.Margin = new System.Windows.Forms.Padding(0);
            this.ibtnScreenshots.Name = "ibtnScreenshots";
            this.ibtnScreenshots.Presspic = global::IMClient.Properties.Resources.screenshots_im_over_03;
            this.ibtnScreenshots.ShowText = null;
            this.ibtnScreenshots.Size = new System.Drawing.Size(20, 20);
            this.ibtnScreenshots.Staticpic = global::IMClient.Properties.Resources.screenshots_im_normal_07;
            this.ibtnScreenshots.Stretch = false;
            this.ibtnScreenshots.SupportToggle = false;
            this.ibtnScreenshots.TabIndex = 3;
            this.ibtnScreenshots.Text = "imageButton6";
            this.ibtnScreenshots.Toggle = false;
            this.ibtnScreenshots.ToolTipOpacity = 1D;
            this.ibtnScreenshots.ToolTipShown = false;
            this.ibtnScreenshots.TooltipX = 0;
            this.ibtnScreenshots.TooltipY = 0;
            this.ibtnScreenshots.Unablepic = null;
            this.ibtnScreenshots.UseVisualStyleBackColor = true;
            // 
            // ibtnPicture
            // 
            this.ibtnPicture.Activepic = global::IMClient.Properties.Resources.picture_im_over_03;
            this.ibtnPicture.BackImage = null;
            this.ibtnPicture.ColorBack = System.Drawing.Color.Empty;
            this.ibtnPicture.Location = new System.Drawing.Point(80, 5);
            this.ibtnPicture.Margin = new System.Windows.Forms.Padding(0);
            this.ibtnPicture.Name = "ibtnPicture";
            this.ibtnPicture.Presspic = global::IMClient.Properties.Resources.picture_im_over_03;
            this.ibtnPicture.ShowText = null;
            this.ibtnPicture.Size = new System.Drawing.Size(20, 20);
            this.ibtnPicture.Staticpic = global::IMClient.Properties.Resources.picture_im_normal_07;
            this.ibtnPicture.Stretch = false;
            this.ibtnPicture.SupportToggle = false;
            this.ibtnPicture.TabIndex = 2;
            this.ibtnPicture.Text = "imageButton5";
            this.ibtnPicture.Toggle = false;
            this.ibtnPicture.ToolTipOpacity = 1D;
            this.ibtnPicture.ToolTipShown = false;
            this.ibtnPicture.TooltipX = 0;
            this.ibtnPicture.TooltipY = 0;
            this.ibtnPicture.Unablepic = null;
            this.ibtnPicture.UseVisualStyleBackColor = true;
            // 
            // ibtnVoice
            // 
            this.ibtnVoice.Activepic = global::IMClient.Properties.Resources.voice_im_over_03;
            this.ibtnVoice.BackImage = null;
            this.ibtnVoice.ColorBack = System.Drawing.Color.Empty;
            this.ibtnVoice.Location = new System.Drawing.Point(50, 5);
            this.ibtnVoice.Margin = new System.Windows.Forms.Padding(0);
            this.ibtnVoice.Name = "ibtnVoice";
            this.ibtnVoice.Presspic = global::IMClient.Properties.Resources.voice_im_over_03;
            this.ibtnVoice.ShowText = null;
            this.ibtnVoice.Size = new System.Drawing.Size(20, 20);
            this.ibtnVoice.Staticpic = global::IMClient.Properties.Resources.voice_im_normal_07;
            this.ibtnVoice.Stretch = false;
            this.ibtnVoice.SupportToggle = false;
            this.ibtnVoice.TabIndex = 1;
            this.ibtnVoice.Text = "imageButton4";
            this.ibtnVoice.Toggle = false;
            this.ibtnVoice.ToolTipOpacity = 1D;
            this.ibtnVoice.ToolTipShown = false;
            this.ibtnVoice.TooltipX = 0;
            this.ibtnVoice.TooltipY = 0;
            this.ibtnVoice.Unablepic = null;
            this.ibtnVoice.UseVisualStyleBackColor = true;
            this.ibtnVoice.Click += new System.EventHandler(this.ibtnVoice_Click);
            // 
            // ibtnFont
            // 
            this.ibtnFont.Activepic = global::IMClient.Properties.Resources.word_im_over_03;
            this.ibtnFont.BackImage = null;
            this.ibtnFont.ColorBack = System.Drawing.Color.Empty;
            this.ibtnFont.Location = new System.Drawing.Point(20, 5);
            this.ibtnFont.Margin = new System.Windows.Forms.Padding(0);
            this.ibtnFont.Name = "ibtnFont";
            this.ibtnFont.Presspic = global::IMClient.Properties.Resources.word_im_over_03;
            this.ibtnFont.ShowText = null;
            this.ibtnFont.Size = new System.Drawing.Size(20, 20);
            this.ibtnFont.Staticpic = global::IMClient.Properties.Resources.word_im_normal_07;
            this.ibtnFont.Stretch = false;
            this.ibtnFont.SupportToggle = false;
            this.ibtnFont.TabIndex = 0;
            this.ibtnFont.Text = "imageButton1";
            this.ibtnFont.Toggle = false;
            this.ibtnFont.ToolTipOpacity = 1D;
            this.ibtnFont.ToolTipShown = false;
            this.ibtnFont.TooltipX = 0;
            this.ibtnFont.TooltipY = 0;
            this.ibtnFont.Unablepic = null;
            this.ibtnFont.UseVisualStyleBackColor = true;
            this.ibtnFont.Click += new System.EventHandler(this.ibtnFont_Click);
            // 
            // panelSend
            // 
            this.panelSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panelSend.BackColor = System.Drawing.Color.White;
            this.panelSend.Controls.Add(this.chatBoxSend);
            this.panelSend.Location = new System.Drawing.Point(0, 370);
            this.panelSend.Name = "panelSend";
            this.panelSend.Size = new System.Drawing.Size(520, 130);
            this.panelSend.TabIndex = 3;
            // 
            // chatBoxSend
            // 
            this.chatBoxSend.AllowDrop = true;
            this.chatBoxSend.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chatBoxSend.ContextMenuMode = JustLib.Controls.ChatBoxContextMenuMode.ForInput;
            this.chatBoxSend.Location = new System.Drawing.Point(0, 0);
            this.chatBoxSend.Name = "chatBoxSend";
            this.chatBoxSend.PopoutImageWhenDoubleClick = false;
            this.chatBoxSend.Size = new System.Drawing.Size(520, 130);
            this.chatBoxSend.TabIndex = 140;
            this.chatBoxSend.Text = "";
            this.chatBoxSend.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chatBoxSend_KeyDown);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.skinTabControl1);
            this.panel2.Controls.Add(this.ibtnSend);
            this.panel2.Controls.Add(this.ibtnCancel);
            this.panel2.Controls.Add(this.ibtnHialogue);
            this.panel2.Location = new System.Drawing.Point(0, 500);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(520, 50);
            this.panel2.TabIndex = 4;
            // 
            // skinTabControl1
            // 
            this.skinTabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            animation1.AnimateOnlyDifferences = true;
            animation1.BlindCoeff = ((System.Drawing.PointF)(resources.GetObject("animation1.BlindCoeff")));
            animation1.LeafCoeff = 0F;
            animation1.MaxTime = 1F;
            animation1.MinTime = 0F;
            animation1.MosaicCoeff = ((System.Drawing.PointF)(resources.GetObject("animation1.MosaicCoeff")));
            animation1.MosaicShift = ((System.Drawing.PointF)(resources.GetObject("animation1.MosaicShift")));
            animation1.MosaicSize = 0;
            animation1.Padding = new System.Windows.Forms.Padding(0);
            animation1.RotateCoeff = 0F;
            animation1.RotateLimit = 0F;
            animation1.ScaleCoeff = ((System.Drawing.PointF)(resources.GetObject("animation1.ScaleCoeff")));
            animation1.SlideCoeff = ((System.Drawing.PointF)(resources.GetObject("animation1.SlideCoeff")));
            animation1.TimeCoeff = 0F;
            animation1.TransparencyCoeff = 0F;
            this.skinTabControl1.Animation = animation1;
            this.skinTabControl1.AnimationStart = false;
            this.skinTabControl1.AnimatorType = CCWin.SkinControl.AnimationType.HorizSlide;
            this.skinTabControl1.CloseRect = new System.Drawing.Rectangle(2, 2, 12, 12);
            this.skinTabControl1.ItemSize = new System.Drawing.Size(70, 36);
            this.skinTabControl1.Location = new System.Drawing.Point(272, 12);
            this.skinTabControl1.Name = "skinTabControl1";
            this.skinTabControl1.PageArrowDown = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageArrowDown")));
            this.skinTabControl1.PageArrowHover = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageArrowHover")));
            this.skinTabControl1.PageCloseHover = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageCloseHover")));
            this.skinTabControl1.PageCloseNormal = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageCloseNormal")));
            this.skinTabControl1.PageDown = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageDown")));
            this.skinTabControl1.PageHover = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageHover")));
            this.skinTabControl1.PageImagePosition = CCWin.SkinControl.SkinTabControl.ePageImagePosition.Left;
            this.skinTabControl1.PageNorml = ((System.Drawing.Image)(resources.GetObject("skinTabControl1.PageNorml")));
            this.skinTabControl1.SelectedIndex = 0;
            this.skinTabControl1.Size = new System.Drawing.Size(14, 20);
            this.skinTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.skinTabControl1.TabIndex = 9;
            this.skinTabControl1.Visible = false;
            // 
            // ibtnSend
            // 
            this.ibtnSend.Activepic = global::IMClient.Properties.Resources.button_44;
            this.ibtnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ibtnSend.BackImage = null;
            this.ibtnSend.ColorBack = System.Drawing.Color.Empty;
            this.ibtnSend.Location = new System.Drawing.Point(426, 12);
            this.ibtnSend.Name = "ibtnSend";
            this.ibtnSend.Presspic = global::IMClient.Properties.Resources.button_45;
            this.ibtnSend.ShowText = null;
            this.ibtnSend.Size = new System.Drawing.Size(72, 28);
            this.ibtnSend.Staticpic = global::IMClient.Properties.Resources.button_43;
            this.ibtnSend.Stretch = false;
            this.ibtnSend.SupportToggle = false;
            this.ibtnSend.TabIndex = 7;
            this.ibtnSend.Text = "imageButton4";
            this.ibtnSend.Toggle = false;
            this.ibtnSend.ToolTipOpacity = 1D;
            this.ibtnSend.ToolTipShown = false;
            this.ibtnSend.TooltipX = 0;
            this.ibtnSend.TooltipY = 0;
            this.ibtnSend.Unablepic = null;
            this.ibtnSend.UseVisualStyleBackColor = true;
            this.ibtnSend.Click += new System.EventHandler(this.ibtnSend_Click);
            // 
            // ibtnCancel
            // 
            this.ibtnCancel.Activepic = global::IMClient.Properties.Resources.button_41;
            this.ibtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ibtnCancel.BackImage = null;
            this.ibtnCancel.ColorBack = System.Drawing.Color.Empty;
            this.ibtnCancel.Location = new System.Drawing.Point(341, 12);
            this.ibtnCancel.Name = "ibtnCancel";
            this.ibtnCancel.Presspic = global::IMClient.Properties.Resources.button_42;
            this.ibtnCancel.ShowText = null;
            this.ibtnCancel.Size = new System.Drawing.Size(72, 28);
            this.ibtnCancel.Staticpic = global::IMClient.Properties.Resources.button_40;
            this.ibtnCancel.Stretch = false;
            this.ibtnCancel.SupportToggle = false;
            this.ibtnCancel.TabIndex = 6;
            this.ibtnCancel.Text = "imageButton1";
            this.ibtnCancel.Toggle = false;
            this.ibtnCancel.ToolTipOpacity = 1D;
            this.ibtnCancel.ToolTipShown = false;
            this.ibtnCancel.TooltipX = 0;
            this.ibtnCancel.TooltipY = 0;
            this.ibtnCancel.Unablepic = null;
            this.ibtnCancel.UseVisualStyleBackColor = true;
            // 
            // ibtnHialogue
            // 
            this.ibtnHialogue.Activepic = global::IMClient.Properties.Resources.button_39;
            this.ibtnHialogue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ibtnHialogue.BackImage = null;
            this.ibtnHialogue.ColorBack = System.Drawing.Color.Empty;
            this.ibtnHialogue.Location = new System.Drawing.Point(20, 15);
            this.ibtnHialogue.Margin = new System.Windows.Forms.Padding(0);
            this.ibtnHialogue.Name = "ibtnHialogue";
            this.ibtnHialogue.Presspic = global::IMClient.Properties.Resources.button_39;
            this.ibtnHialogue.ShowText = null;
            this.ibtnHialogue.Size = new System.Drawing.Size(82, 20);
            this.ibtnHialogue.Staticpic = global::IMClient.Properties.Resources.button_37;
            this.ibtnHialogue.Stretch = false;
            this.ibtnHialogue.SupportToggle = false;
            this.ibtnHialogue.TabIndex = 4;
            this.ibtnHialogue.Text = "imageButton1";
            this.ibtnHialogue.Toggle = false;
            this.ibtnHialogue.ToolTipOpacity = 1D;
            this.ibtnHialogue.ToolTipShown = false;
            this.ibtnHialogue.TooltipX = 0;
            this.ibtnHialogue.TooltipY = 0;
            this.ibtnHialogue.Unablepic = null;
            this.ibtnHialogue.UseVisualStyleBackColor = true;
            // 
            // fontDialog1
            // 
            this.fontDialog1.ShowColor = true;
            // 
            // skinPanel_right
            // 
            this.skinPanel_right.AutoSize = true;
            this.skinPanel_right.BackColor = System.Drawing.Color.White;
            this.skinPanel_right.Controls.Add(this.skinProgressBar);
            this.skinPanel_right.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinPanel_right.DownBack = null;
            this.skinPanel_right.Location = new System.Drawing.Point(520, 65);
            this.skinPanel_right.Margin = new System.Windows.Forms.Padding(0);
            this.skinPanel_right.MouseBack = null;
            this.skinPanel_right.Name = "skinPanel_right";
            this.skinPanel_right.NormlBack = null;
            this.skinPanel_right.Size = new System.Drawing.Size(185, 485);
            this.skinPanel_right.TabIndex = 140;
            // 
            // skinProgressBar
            // 
            this.skinProgressBar.Back = null;
            this.skinProgressBar.BackColor = System.Drawing.Color.Transparent;
            this.skinProgressBar.BarBack = null;
            this.skinProgressBar.ForeColor = System.Drawing.Color.Red;
            this.skinProgressBar.Location = new System.Drawing.Point(47, 20);
            this.skinProgressBar.Name = "skinProgressBar";
            this.skinProgressBar.Size = new System.Drawing.Size(100, 23);
            this.skinProgressBar.TabIndex = 0;
            // 
            // ChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(95)))), ((int)(((byte)(173)))));
            this.ClientSize = new System.Drawing.Size(705, 550);
            this.Controls.Add(this.skinPanel_right);
            this.Controls.Add(this.imageButton3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.imageButton2);
            this.Controls.Add(this.panelSend);
            this.Controls.Add(this.ibtnMin);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblCompany);
            this.Controls.Add(this.panelChat);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.pbxFriend);
            this.Name = "ChatForm";
            this.Text = "";
            this.Load += new System.EventHandler(this.ChatForm_Load);
            this.Shown += new System.EventHandler(this.ChatForm_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ChatForm_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.pbxFriend)).EndInit();
            this.panelChat.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panelSend.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.skinPanel_right.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panelChat;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelSend;
        private System.Windows.Forms.PictureBox pbxFriend;
        private System.Windows.Forms.Label lblCompany;
        private System.Windows.Forms.Label lblName;
        private Controls.ImageButton ibtnMin;
        private Controls.ImageButton imageButton3;
        private Controls.ImageButton imageButton2;
        private Controls.ImageButton ibtnFont;
        private Controls.ImageButton ibtnVideo;
        private Controls.ImageButton ibtFile;
        private Controls.ImageButton ibtnScreenshots;
        private Controls.ImageButton ibtnPicture;
        private Controls.ImageButton ibtnVoice;
        private Controls.ImageButton ibtnOther;
        private Controls.ImageButton ibtnHialogue;
        private System.Windows.Forms.Panel panel2;
        private Controls.ImageButton ibtnSend;
        private Controls.ImageButton ibtnCancel;
        private JustLib.Controls.ChatBox chatBox_history;
        private System.Windows.Forms.FontDialog fontDialog1;
        private JustLib.Controls.ChatBox chatBoxSend;
        private CCWin.SkinControl.SkinTabControl skinTabControl1;
        public CCWin.SkinControl.SkinProgressBar skinProgressBar;
        public CCWin.SkinControl.SkinPanel skinPanel_right;
    }
}