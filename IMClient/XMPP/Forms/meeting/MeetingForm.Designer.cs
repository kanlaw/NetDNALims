namespace IMClient.XMPP.Forms
{
    partial class MeetingForm
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
            this.pnlSend = new System.Windows.Forms.Panel();
            this.btnPaint = new IMClient.Controls.ImageButton();
            this.ibtnScreenshots = new IMClient.Controls.ImageButton();
            this.ibtnPicture = new IMClient.Controls.ImageButton();
            this.ibtnFont = new IMClient.Controls.ImageButton();
            this.btnSend = new IMClient.Controls.ImageButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chatBoxSend = new JustLib.Controls.ChatBox();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.btnAdd = new IMClient.Controls.ImageButton();
            this.meetGrouplist = new JustLib.UnitViews.GroupListBox();
            this.pnlCenter = new System.Windows.Forms.Panel();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.pnlUp = new System.Windows.Forms.Panel();
            this.pnlleft = new System.Windows.Forms.Panel();
            this.pnlVote = new System.Windows.Forms.Panel();
            this.pnlOptionResult = new IMClient.Controls.MyScroll();
            this.pnlOption = new IMClient.Controls.MyScroll();
            this.label1 = new System.Windows.Forms.Label();
            this.btnMeetingOver = new IMClient.Controls.ImageButton();
            this.btnVote = new IMClient.Controls.ImageButton();
            this.pnlKeyPoint = new System.Windows.Forms.Panel();
            this.pnlContent = new IMClient.Controls.MyScroll();
            this.pnlContentTitle = new System.Windows.Forms.Panel();
            this.btnClose = new IMClient.Controls.ImageButton();
            this.ibtnMin = new IMClient.Controls.ImageButton();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.pnlSend.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.pnlCenter.SuspendLayout();
            this.pnlleft.SuspendLayout();
            this.pnlVote.SuspendLayout();
            this.pnlKeyPoint.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSend
            // 
            this.pnlSend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSend.Controls.Add(this.btnPaint);
            this.pnlSend.Controls.Add(this.ibtnScreenshots);
            this.pnlSend.Controls.Add(this.ibtnPicture);
            this.pnlSend.Controls.Add(this.ibtnFont);
            this.pnlSend.Controls.Add(this.btnSend);
            this.pnlSend.Controls.Add(this.panel1);
            this.pnlSend.Location = new System.Drawing.Point(202, 629);
            this.pnlSend.Name = "pnlSend";
            this.pnlSend.Size = new System.Drawing.Size(477, 68);
            this.pnlSend.TabIndex = 6;
            this.pnlSend.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlSend_Paint);
            // 
            // btnPaint
            // 
            this.btnPaint.Activepic = global::IMClient.Properties.Resources.handwritten_im_over_03;
            this.btnPaint.BackImage = null;
            this.btnPaint.ColorBack = System.Drawing.Color.Empty;
            this.btnPaint.Location = new System.Drawing.Point(50, 39);
            this.btnPaint.Margin = new System.Windows.Forms.Padding(0);
            this.btnPaint.Name = "btnPaint";
            this.btnPaint.Presspic = global::IMClient.Properties.Resources.handwritten_im_over_03;
            this.btnPaint.ShowText = null;
            this.btnPaint.Size = new System.Drawing.Size(20, 20);
            this.btnPaint.Staticpic = global::IMClient.Properties.Resources.handwritten_im_normal_03;
            this.btnPaint.Stretch = false;
            this.btnPaint.SupportToggle = false;
            this.btnPaint.TabIndex = 12;
            this.btnPaint.Text = "imageButton8";
            this.btnPaint.Toggle = false;
            this.btnPaint.ToolTipOpacity = 1D;
            this.btnPaint.ToolTipShown = false;
            this.btnPaint.TooltipX = 0;
            this.btnPaint.TooltipY = 0;
            this.btnPaint.Unablepic = null;
            this.btnPaint.UseVisualStyleBackColor = true;
            this.btnPaint.Click += new System.EventHandler(this.btnPaint_Click);
            // 
            // ibtnScreenshots
            // 
            this.ibtnScreenshots.Activepic = global::IMClient.Properties.Resources.screenshots_im_over_03;
            this.ibtnScreenshots.BackImage = null;
            this.ibtnScreenshots.ColorBack = System.Drawing.Color.Empty;
            this.ibtnScreenshots.Location = new System.Drawing.Point(20, 39);
            this.ibtnScreenshots.Margin = new System.Windows.Forms.Padding(0);
            this.ibtnScreenshots.Name = "ibtnScreenshots";
            this.ibtnScreenshots.Presspic = global::IMClient.Properties.Resources.screenshots_im_over_03;
            this.ibtnScreenshots.ShowText = null;
            this.ibtnScreenshots.Size = new System.Drawing.Size(20, 20);
            this.ibtnScreenshots.Staticpic = global::IMClient.Properties.Resources.screenshots_im_normal_07;
            this.ibtnScreenshots.Stretch = false;
            this.ibtnScreenshots.SupportToggle = false;
            this.ibtnScreenshots.TabIndex = 11;
            this.ibtnScreenshots.Text = "imageButton6";
            this.ibtnScreenshots.Toggle = false;
            this.ibtnScreenshots.ToolTipOpacity = 1D;
            this.ibtnScreenshots.ToolTipShown = false;
            this.ibtnScreenshots.TooltipX = 0;
            this.ibtnScreenshots.TooltipY = 0;
            this.ibtnScreenshots.Unablepic = null;
            this.ibtnScreenshots.UseVisualStyleBackColor = true;
            this.ibtnScreenshots.Click += new System.EventHandler(this.ibtnScreenshots_Click);
            // 
            // ibtnPicture
            // 
            this.ibtnPicture.Activepic = global::IMClient.Properties.Resources.picture_im_over_03;
            this.ibtnPicture.BackImage = null;
            this.ibtnPicture.ColorBack = System.Drawing.Color.Empty;
            this.ibtnPicture.Location = new System.Drawing.Point(50, 15);
            this.ibtnPicture.Margin = new System.Windows.Forms.Padding(0);
            this.ibtnPicture.Name = "ibtnPicture";
            this.ibtnPicture.Presspic = global::IMClient.Properties.Resources.picture_im_over_03;
            this.ibtnPicture.ShowText = null;
            this.ibtnPicture.Size = new System.Drawing.Size(20, 20);
            this.ibtnPicture.Staticpic = global::IMClient.Properties.Resources.picture_im_normal_07;
            this.ibtnPicture.Stretch = false;
            this.ibtnPicture.SupportToggle = false;
            this.ibtnPicture.TabIndex = 10;
            this.ibtnPicture.Text = "imageButton5";
            this.ibtnPicture.Toggle = false;
            this.ibtnPicture.ToolTipOpacity = 1D;
            this.ibtnPicture.ToolTipShown = false;
            this.ibtnPicture.TooltipX = 0;
            this.ibtnPicture.TooltipY = 0;
            this.ibtnPicture.Unablepic = null;
            this.ibtnPicture.UseVisualStyleBackColor = true;
            this.ibtnPicture.Click += new System.EventHandler(this.ibtnPicture_Click);
            // 
            // ibtnFont
            // 
            this.ibtnFont.Activepic = global::IMClient.Properties.Resources.word_im_over_03;
            this.ibtnFont.BackImage = null;
            this.ibtnFont.ColorBack = System.Drawing.Color.Empty;
            this.ibtnFont.Location = new System.Drawing.Point(20, 15);
            this.ibtnFont.Margin = new System.Windows.Forms.Padding(0);
            this.ibtnFont.Name = "ibtnFont";
            this.ibtnFont.Presspic = global::IMClient.Properties.Resources.word_im_over_03;
            this.ibtnFont.ShowText = null;
            this.ibtnFont.Size = new System.Drawing.Size(20, 20);
            this.ibtnFont.Staticpic = global::IMClient.Properties.Resources.word_im_normal_07;
            this.ibtnFont.Stretch = false;
            this.ibtnFont.SupportToggle = false;
            this.ibtnFont.TabIndex = 9;
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
            // btnSend
            // 
            this.btnSend.Activepic = global::IMClient.Properties.Resources.send_button_over_03;
            this.btnSend.BackImage = null;
            this.btnSend.ColorBack = System.Drawing.Color.Transparent;
            this.btnSend.Location = new System.Drawing.Point(402, 21);
            this.btnSend.Name = "btnSend";
            this.btnSend.Presspic = global::IMClient.Properties.Resources.send_button_over_03;
            this.btnSend.ShowText = null;
            this.btnSend.Size = new System.Drawing.Size(55, 25);
            this.btnSend.Staticpic = global::IMClient.Properties.Resources.send_button_normal_03;
            this.btnSend.Stretch = false;
            this.btnSend.SupportToggle = false;
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "imageButton1";
            this.btnSend.Toggle = false;
            this.btnSend.ToolTipOpacity = 1D;
            this.btnSend.ToolTipShown = false;
            this.btnSend.TooltipX = 0;
            this.btnSend.TooltipY = 0;
            this.btnSend.Unablepic = null;
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.chatBoxSend);
            this.panel1.Location = new System.Drawing.Point(98, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(286, 47);
            this.panel1.TabIndex = 7;
            // 
            // chatBoxSend
            // 
            this.chatBoxSend.AllowDrop = true;
            this.chatBoxSend.BackColor = System.Drawing.Color.White;
            this.chatBoxSend.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chatBoxSend.ContextMenuMode = JustLib.Controls.ChatBoxContextMenuMode.ForInput;
            this.chatBoxSend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatBoxSend.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.chatBoxSend.Location = new System.Drawing.Point(0, 0);
            this.chatBoxSend.Name = "chatBoxSend";
            this.chatBoxSend.PopoutImageWhenDoubleClick = false;
            this.chatBoxSend.Size = new System.Drawing.Size(286, 47);
            this.chatBoxSend.TabIndex = 141;
            this.chatBoxSend.Text = "";
            // 
            // pnlRight
            // 
            this.pnlRight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlRight.Controls.Add(this.btnAdd);
            this.pnlRight.Controls.Add(this.meetGrouplist);
            this.pnlRight.Location = new System.Drawing.Point(681, 41);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(203, 656);
            this.pnlRight.TabIndex = 10;
            this.pnlRight.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlRight_Paint);
            // 
            // btnAdd
            // 
            this.btnAdd.Activepic = global::IMClient.Properties.Resources.Invite_friends_over_03;
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.BackImage = null;
            this.btnAdd.ColorBack = System.Drawing.Color.Transparent;
            this.btnAdd.Location = new System.Drawing.Point(0, 618);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Presspic = global::IMClient.Properties.Resources.Invite_friends_over_03;
            this.btnAdd.ShowText = null;
            this.btnAdd.Size = new System.Drawing.Size(203, 38);
            this.btnAdd.Staticpic = global::IMClient.Properties.Resources.Invite_friends_normal_03;
            this.btnAdd.Stretch = true;
            this.btnAdd.SupportToggle = false;
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "imageButton1";
            this.btnAdd.Toggle = false;
            this.btnAdd.ToolTipOpacity = 1D;
            this.btnAdd.ToolTipShown = false;
            this.btnAdd.TooltipX = 0;
            this.btnAdd.TooltipY = 0;
            this.btnAdd.Unablepic = null;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // meetGrouplist
            // 
            this.meetGrouplist.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.meetGrouplist.BackColor = System.Drawing.Color.White;
            this.meetGrouplist.Location = new System.Drawing.Point(6, 0);
            this.meetGrouplist.Margin = new System.Windows.Forms.Padding(2);
            this.meetGrouplist.Name = "meetGrouplist";
            this.meetGrouplist.Size = new System.Drawing.Size(197, 613);
            this.meetGrouplist.TabIndex = 0;
            // 
            // pnlCenter
            // 
            this.pnlCenter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCenter.BackColor = System.Drawing.Color.White;
            this.pnlCenter.Controls.Add(this.pnlBottom);
            this.pnlCenter.Controls.Add(this.pnlUp);
            this.pnlCenter.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.pnlCenter.Location = new System.Drawing.Point(202, 41);
            this.pnlCenter.Name = "pnlCenter";
            this.pnlCenter.Size = new System.Drawing.Size(477, 588);
            this.pnlCenter.TabIndex = 10;
            this.pnlCenter.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlCenter_Paint);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Location = new System.Drawing.Point(20, 165);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(200, 100);
            this.pnlBottom.TabIndex = 6;
            // 
            // pnlUp
            // 
            this.pnlUp.Location = new System.Drawing.Point(20, 30);
            this.pnlUp.Name = "pnlUp";
            this.pnlUp.Size = new System.Drawing.Size(200, 100);
            this.pnlUp.TabIndex = 5;
            // 
            // pnlleft
            // 
            this.pnlleft.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlleft.Controls.Add(this.pnlVote);
            this.pnlleft.Controls.Add(this.pnlKeyPoint);
            this.pnlleft.Controls.Add(this.pnlContentTitle);
            this.pnlleft.Location = new System.Drawing.Point(0, 41);
            this.pnlleft.Name = "pnlleft";
            this.pnlleft.Size = new System.Drawing.Size(200, 656);
            this.pnlleft.TabIndex = 9;
            // 
            // pnlVote
            // 
            this.pnlVote.Controls.Add(this.pnlOptionResult);
            this.pnlVote.Controls.Add(this.pnlOption);
            this.pnlVote.Controls.Add(this.label1);
            this.pnlVote.Controls.Add(this.btnMeetingOver);
            this.pnlVote.Controls.Add(this.btnVote);
            this.pnlVote.Location = new System.Drawing.Point(0, 301);
            this.pnlVote.Name = "pnlVote";
            this.pnlVote.Size = new System.Drawing.Size(200, 352);
            this.pnlVote.TabIndex = 6;
            this.pnlVote.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlVote_Paint);
            // 
            // pnlOptionResult
            // 
            this.pnlOptionResult.Location = new System.Drawing.Point(4, 172);
            this.pnlOptionResult.Name = "pnlOptionResult";
            this.pnlOptionResult.Size = new System.Drawing.Size(193, 97);
            this.pnlOptionResult.TabIndex = 4;
            // 
            // pnlOption
            // 
            this.pnlOption.Location = new System.Drawing.Point(3, 32);
            this.pnlOption.Name = "pnlOption";
            this.pnlOption.Size = new System.Drawing.Size(193, 134);
            this.pnlOption.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(141)))), ((int)(((byte)(241)))));
            this.label1.Location = new System.Drawing.Point(68, 324);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 22);
            this.label1.TabIndex = 2;
            this.label1.Text = "结束会议";
            // 
            // btnMeetingOver
            // 
            this.btnMeetingOver.Activepic = global::IMClient.Properties.Resources.close_meeting_ico_03;
            this.btnMeetingOver.BackImage = null;
            this.btnMeetingOver.ColorBack = System.Drawing.Color.Transparent;
            this.btnMeetingOver.Location = new System.Drawing.Point(48, 327);
            this.btnMeetingOver.Name = "btnMeetingOver";
            this.btnMeetingOver.Presspic = global::IMClient.Properties.Resources.close_meeting_ico_03;
            this.btnMeetingOver.ShowText = null;
            this.btnMeetingOver.Size = new System.Drawing.Size(14, 15);
            this.btnMeetingOver.Staticpic = global::IMClient.Properties.Resources.close_meeting_ico_03;
            this.btnMeetingOver.Stretch = false;
            this.btnMeetingOver.SupportToggle = false;
            this.btnMeetingOver.TabIndex = 1;
            this.btnMeetingOver.Text = "imageButton1";
            this.btnMeetingOver.Toggle = false;
            this.btnMeetingOver.ToolTipOpacity = 1D;
            this.btnMeetingOver.ToolTipShown = false;
            this.btnMeetingOver.TooltipX = 0;
            this.btnMeetingOver.TooltipY = 0;
            this.btnMeetingOver.Unablepic = null;
            this.btnMeetingOver.UseVisualStyleBackColor = true;
            this.btnMeetingOver.Click += new System.EventHandler(this.btnMeetingOver_Click);
            // 
            // btnVote
            // 
            this.btnVote.Activepic = global::IMClient.Properties.Resources.vote_button_over_03;
            this.btnVote.BackImage = null;
            this.btnVote.ColorBack = System.Drawing.Color.Transparent;
            this.btnVote.Location = new System.Drawing.Point(45, 286);
            this.btnVote.Name = "btnVote";
            this.btnVote.Presspic = global::IMClient.Properties.Resources.vote_button_over_03;
            this.btnVote.ShowText = null;
            this.btnVote.Size = new System.Drawing.Size(97, 26);
            this.btnVote.Staticpic = global::IMClient.Properties.Resources.vote_button_normal_03;
            this.btnVote.Stretch = false;
            this.btnVote.SupportToggle = false;
            this.btnVote.TabIndex = 0;
            this.btnVote.Tag = "true";
            this.btnVote.Text = "imageButton1";
            this.btnVote.Toggle = false;
            this.btnVote.ToolTipOpacity = 1D;
            this.btnVote.ToolTipShown = false;
            this.btnVote.TooltipX = 0;
            this.btnVote.TooltipY = 0;
            this.btnVote.Unablepic = null;
            this.btnVote.UseVisualStyleBackColor = true;
            this.btnVote.Click += new System.EventHandler(this.btnVote_Click);
            // 
            // pnlKeyPoint
            // 
            this.pnlKeyPoint.Controls.Add(this.pnlContent);
            this.pnlKeyPoint.Location = new System.Drawing.Point(0, 100);
            this.pnlKeyPoint.Name = "pnlKeyPoint";
            this.pnlKeyPoint.Size = new System.Drawing.Size(200, 202);
            this.pnlKeyPoint.TabIndex = 5;
            this.pnlKeyPoint.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlKeyPoint_Paint);
            // 
            // pnlContent
            // 
            this.pnlContent.Location = new System.Drawing.Point(3, 31);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(193, 168);
            this.pnlContent.TabIndex = 0;
            // 
            // pnlContentTitle
            // 
            this.pnlContentTitle.Location = new System.Drawing.Point(0, 3);
            this.pnlContentTitle.Name = "pnlContentTitle";
            this.pnlContentTitle.Size = new System.Drawing.Size(200, 96);
            this.pnlContentTitle.TabIndex = 4;
            this.pnlContentTitle.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlContentTitle_Paint);
            // 
            // btnClose
            // 
            this.btnClose.Activepic = global::IMClient.Properties.Resources.close_over_02;
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackImage = null;
            this.btnClose.ColorBack = System.Drawing.Color.Empty;
            this.btnClose.Location = new System.Drawing.Point(852, 7);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Presspic = global::IMClient.Properties.Resources.close_down_02;
            this.btnClose.ShowText = null;
            this.btnClose.Size = new System.Drawing.Size(29, 24);
            this.btnClose.Staticpic = global::IMClient.Properties.Resources.close_normal_02;
            this.btnClose.Stretch = false;
            this.btnClose.SupportToggle = false;
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "imageButton3";
            this.btnClose.Toggle = false;
            this.btnClose.ToolTipOpacity = 1D;
            this.btnClose.ToolTipShown = false;
            this.btnClose.TooltipX = 0;
            this.btnClose.TooltipY = 0;
            this.btnClose.Unablepic = null;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ibtnMin
            // 
            this.ibtnMin.Activepic = global::IMClient.Properties.Resources.minimize_over_02;
            this.ibtnMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ibtnMin.BackImage = null;
            this.ibtnMin.ColorBack = System.Drawing.Color.Empty;
            this.ibtnMin.Location = new System.Drawing.Point(811, 7);
            this.ibtnMin.Name = "ibtnMin";
            this.ibtnMin.Presspic = global::IMClient.Properties.Resources.minimize_down_02;
            this.ibtnMin.ShowText = null;
            this.ibtnMin.Size = new System.Drawing.Size(29, 24);
            this.ibtnMin.Staticpic = global::IMClient.Properties.Resources.minimize_normal_02;
            this.ibtnMin.Stretch = false;
            this.ibtnMin.SupportToggle = false;
            this.ibtnMin.TabIndex = 6;
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
            // fontDialog1
            // 
            this.fontDialog1.ShowColor = true;
            // 
            // MeetingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(885, 698);
            this.Controls.Add(this.pnlSend);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlCenter);
            this.Controls.Add(this.pnlleft);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ibtnMin);
            this.Name = "MeetingForm";
            this.Shadow = true;
            this.Text = "";
            this.TitleColor = System.Drawing.Color.White;
            this.TitleOffset = new System.Drawing.Point(5, 5);
            this.TopMost = false;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_FormClosed);
            this.pnlSend.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            this.pnlCenter.ResumeLayout(false);
            this.pnlleft.ResumeLayout(false);
            this.pnlVote.ResumeLayout(false);
            this.pnlVote.PerformLayout();
            this.pnlKeyPoint.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ImageButton btnClose;
        private Controls.ImageButton ibtnMin;
        private System.Windows.Forms.Panel pnlleft;
        private System.Windows.Forms.Panel pnlCenter;
        private System.Windows.Forms.Panel pnlRight;

        private JustLib.UnitViews.GroupListBox meetGrouplist;
        private System.Windows.Forms.Panel pnlContentTitle;
        private System.Windows.Forms.Panel pnlVote;
        private System.Windows.Forms.Panel pnlKeyPoint;
        private System.Windows.Forms.Panel pnlSend;
        private JustLib.Controls.ChatBox chatBoxSend;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Panel pnlUp;
        private System.Windows.Forms.Panel panel1;
        private Controls.ImageButton btnVote;
        private Controls.ImageButton btnSend;
        private Controls.ImageButton btnAdd;
        private Controls.ImageButton btnPaint;
        private Controls.ImageButton ibtnScreenshots;
        private Controls.ImageButton ibtnPicture;
        private Controls.ImageButton ibtnFont;
        private Controls.ImageButton btnMeetingOver;
        private System.Windows.Forms.Label label1;
        private Controls.MyScroll pnlOption;
        private Controls.MyScroll pnlOptionResult;
        private Controls.MyScroll pnlContent;
        private System.Windows.Forms.FontDialog fontDialog1;
    }
}