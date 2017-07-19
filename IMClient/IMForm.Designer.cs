namespace IMClient
{
    partial class IMForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IMForm));
            this.panelList = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ibtnSF = new IMClient.Controls.ImageButton();
            this.search_ImageTextBox = new IMClient.Controls.ImageTextBox();
            this.search_ImageBtn = new IMClient.Controls.ImageButton();
            this.ibtnAdress = new IMClient.Controls.ImageButton();
            this.ibtnMeeting = new IMClient.Controls.ImageButton();
            this.ibtnEmail = new IMClient.Controls.ImageButton();
            this.ibtnMessage = new IMClient.Controls.ImageButton();
            this.personPlatform = new IMClient.Controls.ImageButton();
            this.imageButton1 = new IMClient.Controls.ImageButton();
            this.cmsiAdd = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiCreateMeeting = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAttendMeeting = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSearchCancel = new IMClient.Controls.ImageButton();
            this.panelList.SuspendLayout();
            this.panel1.SuspendLayout();
            this.cmsiAdd.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelList
            // 
            this.panelList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelList.AutoSize = true;
            this.panelList.BackColor = System.Drawing.Color.White;
            this.panelList.Controls.Add(this.btnSearchCancel);
            this.panelList.Controls.Add(this.panel1);
            this.panelList.Controls.Add(this.search_ImageTextBox);
            this.panelList.Controls.Add(this.search_ImageBtn);
            this.panelList.Location = new System.Drawing.Point(0, 0);
            this.panelList.Margin = new System.Windows.Forms.Padding(0, 0, 50, 0);
            this.panelList.Name = "panelList";
            this.panelList.Size = new System.Drawing.Size(231, 541);
            this.panelList.TabIndex = 0;
            this.panelList.Paint += new System.Windows.Forms.PaintEventHandler(this.panelList_Paint);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.ibtnSF);
            this.panel1.Location = new System.Drawing.Point(1, 57);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(229, 448);
            this.panel1.TabIndex = 7;
            // 
            // ibtnSF
            // 
            this.ibtnSF.Activepic = global::IMClient.Properties.Resources.shrinkage01_32;
            this.ibtnSF.BackColor = System.Drawing.Color.White;
            this.ibtnSF.BackImage = null;
            this.ibtnSF.ColorBack = System.Drawing.Color.Empty;
            this.ibtnSF.Location = new System.Drawing.Point(0, 263);
            this.ibtnSF.Name = "ibtnSF";
            this.ibtnSF.Presspic = global::IMClient.Properties.Resources.shrinkage01_32;
            this.ibtnSF.ShowText = null;
            this.ibtnSF.Size = new System.Drawing.Size(10, 60);
            this.ibtnSF.Staticpic = global::IMClient.Properties.Resources.shrinkage01_32;
            this.ibtnSF.Stretch = false;
            this.ibtnSF.SupportToggle = false;
            this.ibtnSF.TabIndex = 0;
            this.ibtnSF.Text = "缩进";
            this.ibtnSF.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.ibtnSF.Toggle = false;
            this.ibtnSF.ToolTipOpacity = 1D;
            this.ibtnSF.ToolTipShown = false;
            this.ibtnSF.TooltipX = 10;
            this.ibtnSF.TooltipY = 0;
            this.ibtnSF.Unablepic = null;
            this.ibtnSF.UseVisualStyleBackColor = false;
            this.ibtnSF.Click += new System.EventHandler(this.ibtnSF_Click);
            // 
            // search_ImageTextBox
            // 
            this.search_ImageTextBox.Bordercolor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
            this.search_ImageTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.search_ImageTextBox.Controllist = ((System.Collections.ArrayList)(resources.GetObject("search_ImageTextBox.Controllist")));
            this.search_ImageTextBox.EmptyTextTip = "搜索";
            this.search_ImageTextBox.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.search_ImageTextBox.Location = new System.Drawing.Point(27, 13);
            this.search_ImageTextBox.Name = "search_ImageTextBox";
            this.search_ImageTextBox.SelectedBordercolor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.search_ImageTextBox.Size = new System.Drawing.Size(167, 25);
            this.search_ImageTextBox.TabIndex = 6;
            this.search_ImageTextBox.TStatus = IMClient.Controls.TextStatus.ALL;
            this.search_ImageTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.search_ImageTextBox_KeyDown);
            // 
            // search_ImageBtn
            // 
            this.search_ImageBtn.Activepic = global::IMClient.Properties.Resources.search_blue_small1_normal_03;
            this.search_ImageBtn.BackImage = null;
            this.search_ImageBtn.ColorBack = System.Drawing.Color.Empty;
            this.search_ImageBtn.Location = new System.Drawing.Point(7, 19);
            this.search_ImageBtn.Name = "search_ImageBtn";
            this.search_ImageBtn.Presspic = global::IMClient.Properties.Resources.search_blue_small1_over_03;
            this.search_ImageBtn.ShowText = null;
            this.search_ImageBtn.Size = new System.Drawing.Size(14, 14);
            this.search_ImageBtn.Staticpic = global::IMClient.Properties.Resources.search_blue_small1_normal_03;
            this.search_ImageBtn.Stretch = false;
            this.search_ImageBtn.SupportToggle = false;
            this.search_ImageBtn.TabIndex = 5;
            this.search_ImageBtn.Text = "imageButton";
            this.search_ImageBtn.Toggle = false;
            this.search_ImageBtn.ToolTipOpacity = 1D;
            this.search_ImageBtn.ToolTipShown = false;
            this.search_ImageBtn.TooltipX = 0;
            this.search_ImageBtn.TooltipY = 0;
            this.search_ImageBtn.Unablepic = null;
            this.search_ImageBtn.UseVisualStyleBackColor = true;
            this.search_ImageBtn.Click += new System.EventHandler(this.search_ImageBtn_Click);
            // 
            // ibtnAdress
            // 
            this.ibtnAdress.Activepic = global::IMClient.Properties.Resources.address_book_over_15;
            this.ibtnAdress.BackImage = null;
            this.ibtnAdress.ColorBack = System.Drawing.Color.Empty;
            this.ibtnAdress.Location = new System.Drawing.Point(241, 57);
            this.ibtnAdress.Name = "ibtnAdress";
            this.ibtnAdress.Presspic = global::IMClient.Properties.Resources.address_book_down_15;
            this.ibtnAdress.ShowText = null;
            this.ibtnAdress.Size = new System.Drawing.Size(28, 28);
            this.ibtnAdress.Staticpic = global::IMClient.Properties.Resources.address_book_normal_15;
            this.ibtnAdress.Stretch = false;
            this.ibtnAdress.SupportToggle = false;
            this.ibtnAdress.TabIndex = 5;
            this.ibtnAdress.Text = "imageButton4";
            this.ibtnAdress.Toggle = false;
            this.ibtnAdress.ToolTipOpacity = 1D;
            this.ibtnAdress.ToolTipShown = false;
            this.ibtnAdress.TooltipX = 0;
            this.ibtnAdress.TooltipY = 0;
            this.ibtnAdress.Unablepic = null;
            this.ibtnAdress.UseVisualStyleBackColor = true;
            this.ibtnAdress.Click += new System.EventHandler(this.ibtnAdress_Click);
            // 
            // ibtnMeeting
            // 
            this.ibtnMeeting.Activepic = global::IMClient.Properties.Resources.meeting_over_14;
            this.ibtnMeeting.BackImage = null;
            this.ibtnMeeting.ColorBack = System.Drawing.Color.Empty;
            this.ibtnMeeting.Location = new System.Drawing.Point(241, 104);
            this.ibtnMeeting.Name = "ibtnMeeting";
            this.ibtnMeeting.Presspic = global::IMClient.Properties.Resources.meeting_down_14;
            this.ibtnMeeting.ShowText = null;
            this.ibtnMeeting.Size = new System.Drawing.Size(28, 28);
            this.ibtnMeeting.Staticpic = global::IMClient.Properties.Resources.meeting_normal_14;
            this.ibtnMeeting.Stretch = false;
            this.ibtnMeeting.SupportToggle = false;
            this.ibtnMeeting.TabIndex = 4;
            this.ibtnMeeting.Text = "imageButton3";
            this.ibtnMeeting.Toggle = false;
            this.ibtnMeeting.ToolTipOpacity = 1D;
            this.ibtnMeeting.ToolTipShown = false;
            this.ibtnMeeting.TooltipX = 0;
            this.ibtnMeeting.TooltipY = 0;
            this.ibtnMeeting.Unablepic = null;
            this.ibtnMeeting.UseVisualStyleBackColor = true;
            this.ibtnMeeting.Click += new System.EventHandler(this.ibtnMeeting_Click);
            // 
            // ibtnEmail
            // 
            this.ibtnEmail.Activepic = global::IMClient.Properties.Resources.email_over_13;
            this.ibtnEmail.BackImage = null;
            this.ibtnEmail.ColorBack = System.Drawing.Color.Empty;
            this.ibtnEmail.Location = new System.Drawing.Point(241, 198);
            this.ibtnEmail.Name = "ibtnEmail";
            this.ibtnEmail.Presspic = global::IMClient.Properties.Resources.email_down_13;
            this.ibtnEmail.ShowText = null;
            this.ibtnEmail.Size = new System.Drawing.Size(28, 28);
            this.ibtnEmail.Staticpic = global::IMClient.Properties.Resources.email_normal_13;
            this.ibtnEmail.Stretch = false;
            this.ibtnEmail.SupportToggle = false;
            this.ibtnEmail.TabIndex = 3;
            this.ibtnEmail.Text = "imageButton2";
            this.ibtnEmail.Toggle = false;
            this.ibtnEmail.ToolTipOpacity = 1D;
            this.ibtnEmail.ToolTipShown = false;
            this.ibtnEmail.TooltipX = 0;
            this.ibtnEmail.TooltipY = 0;
            this.ibtnEmail.Unablepic = null;
            this.ibtnEmail.UseVisualStyleBackColor = true;
            this.ibtnEmail.Click += new System.EventHandler(this.ibtnEmail_Click);
            // 
            // ibtnMessage
            // 
            this.ibtnMessage.Activepic = global::IMClient.Properties.Resources.message_over_09;
            this.ibtnMessage.BackImage = null;
            this.ibtnMessage.ColorBack = System.Drawing.Color.Empty;
            this.ibtnMessage.Location = new System.Drawing.Point(241, 10);
            this.ibtnMessage.Name = "ibtnMessage";
            this.ibtnMessage.Presspic = global::IMClient.Properties.Resources.message_down_09;
            this.ibtnMessage.ShowText = null;
            this.ibtnMessage.Size = new System.Drawing.Size(28, 28);
            this.ibtnMessage.Staticpic = global::IMClient.Properties.Resources.message_normal_09;
            this.ibtnMessage.Stretch = false;
            this.ibtnMessage.SupportToggle = false;
            this.ibtnMessage.TabIndex = 2;
            this.ibtnMessage.Text = "信息";
            this.ibtnMessage.Toggle = false;
            this.ibtnMessage.ToolTipOpacity = 1D;
            this.ibtnMessage.ToolTipShown = false;
            this.ibtnMessage.TooltipX = 0;
            this.ibtnMessage.TooltipY = 0;
            this.ibtnMessage.Unablepic = null;
            this.ibtnMessage.UseVisualStyleBackColor = true;
            this.ibtnMessage.Click += new System.EventHandler(this.ibtnMessage_Click);
            // 
            // personPlatform
            // 
            this.personPlatform.Activepic = global::IMClient.Properties.Resources.add_Users_over_03;
            this.personPlatform.BackImage = null;
            this.personPlatform.ColorBack = System.Drawing.Color.Empty;
            this.personPlatform.Location = new System.Drawing.Point(241, 151);
            this.personPlatform.Name = "personPlatform";
            this.personPlatform.Presspic = global::IMClient.Properties.Resources.add_Users_down_03;
            this.personPlatform.ShowText = null;
            this.personPlatform.Size = new System.Drawing.Size(28, 28);
            this.personPlatform.Staticpic = global::IMClient.Properties.Resources.add_Users_normal_03;
            this.personPlatform.Stretch = false;
            this.personPlatform.SupportToggle = false;
            this.personPlatform.TabIndex = 6;
            this.personPlatform.Text = "personImageButton";
            this.personPlatform.Toggle = false;
            this.personPlatform.ToolTipOpacity = 1D;
            this.personPlatform.ToolTipShown = false;
            this.personPlatform.TooltipX = 0;
            this.personPlatform.TooltipY = 0;
            this.personPlatform.Unablepic = null;
            this.personPlatform.UseVisualStyleBackColor = true;
            this.personPlatform.Click += new System.EventHandler(this.imageButton1_Click);
            // 
            // imageButton1
            // 
            this.imageButton1.Activepic = global::IMClient.Properties.Resources.Personal_workbench_over_03;
            this.imageButton1.BackImage = null;
            this.imageButton1.ColorBack = System.Drawing.Color.Empty;
            this.imageButton1.Location = new System.Drawing.Point(241, 247);
            this.imageButton1.Name = "imageButton1";
            this.imageButton1.Presspic = global::IMClient.Properties.Resources.Personal_workbench_down_03;
            this.imageButton1.ShowText = null;
            this.imageButton1.Size = new System.Drawing.Size(28, 28);
            this.imageButton1.Staticpic = global::IMClient.Properties.Resources.Personal_workbench_normal_03;
            this.imageButton1.Stretch = false;
            this.imageButton1.SupportToggle = false;
            this.imageButton1.TabIndex = 7;
            this.imageButton1.Text = "personImageButton";
            this.imageButton1.Toggle = false;
            this.imageButton1.ToolTipOpacity = 1D;
            this.imageButton1.ToolTipShown = false;
            this.imageButton1.TooltipX = 0;
            this.imageButton1.TooltipY = 0;
            this.imageButton1.Unablepic = null;
            this.imageButton1.UseVisualStyleBackColor = true;
            this.imageButton1.Visible = false;
            this.imageButton1.Click += new System.EventHandler(this.imageButton1_Click);
            // 
            // cmsiAdd
            // 
            this.cmsiAdd.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCreateMeeting,
            this.tsmiAttendMeeting});
            this.cmsiAdd.Name = "cmsiAdd";
            this.cmsiAdd.Size = new System.Drawing.Size(137, 48);
            // 
            // tsmiCreateMeeting
            // 
            this.tsmiCreateMeeting.Name = "tsmiCreateMeeting";
            this.tsmiCreateMeeting.Size = new System.Drawing.Size(136, 22);
            this.tsmiCreateMeeting.Tag = "1";
            this.tsmiCreateMeeting.Text = "创建会议室";
            this.tsmiCreateMeeting.Click += new System.EventHandler(this.tsmiEmcee_Click);
            // 
            // tsmiAttendMeeting
            // 
            this.tsmiAttendMeeting.Name = "tsmiAttendMeeting";
            this.tsmiAttendMeeting.Size = new System.Drawing.Size(136, 22);
            this.tsmiAttendMeeting.Tag = "2";
            this.tsmiAttendMeeting.Text = "参 加 会 议";
            this.tsmiAttendMeeting.Click += new System.EventHandler(this.tsmiEmcee_Click);
            // 
            // btnSearchCancel
            // 
            this.btnSearchCancel.Activepic = global::IMClient.Properties.Resources.close_button_small_03;
            this.btnSearchCancel.BackImage = null;
            this.btnSearchCancel.ColorBack = System.Drawing.Color.Empty;
            this.btnSearchCancel.Location = new System.Drawing.Point(203, 19);
            this.btnSearchCancel.Name = "btnSearchCancel";
            this.btnSearchCancel.Presspic = global::IMClient.Properties.Resources.close_button_small_03;
            this.btnSearchCancel.ShowText = null;
            this.btnSearchCancel.Size = new System.Drawing.Size(14, 14);
            this.btnSearchCancel.Staticpic = global::IMClient.Properties.Resources.close_button_small_03;
            this.btnSearchCancel.Stretch = false;
            this.btnSearchCancel.SupportToggle = false;
            this.btnSearchCancel.TabIndex = 8;
            this.btnSearchCancel.Text = "imageButton";
            this.btnSearchCancel.Toggle = false;
            this.btnSearchCancel.ToolTipOpacity = 1D;
            this.btnSearchCancel.ToolTipShown = false;
            this.btnSearchCancel.TooltipX = 0;
            this.btnSearchCancel.TooltipY = 0;
            this.btnSearchCancel.Unablepic = null;
            this.btnSearchCancel.UseVisualStyleBackColor = true;
            this.btnSearchCancel.Click += new System.EventHandler(this.btnSearchCancel_Click);
            // 
            // IMForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(38)))), ((int)(((byte)(76)))));
            this.ClientSize = new System.Drawing.Size(280, 505);
            this.Controls.Add(this.imageButton1);
            this.Controls.Add(this.personPlatform);
            this.Controls.Add(this.ibtnAdress);
            this.Controls.Add(this.ibtnMeeting);
            this.Controls.Add(this.ibtnEmail);
            this.Controls.Add(this.ibtnMessage);
            this.Controls.Add(this.panelList);
            this.Name = "IMForm";
            this.TopMost = false;
            this.Load += new System.EventHandler(this.IMForm_Load);
            this.panelList.ResumeLayout(false);
            this.panelList.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.cmsiAdd.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelList;
        private Controls.ImageButton ibtnMessage;
        private Controls.ImageButton ibtnEmail;
        private Controls.ImageButton ibtnMeeting;
        private Controls.ImageButton ibtnAdress;
        private Controls.ImageButton search_ImageBtn;
        private Controls.ImageTextBox search_ImageTextBox;
        public System.Windows.Forms.Panel panel1;
        public Controls.ImageButton ibtnSF;
        private Controls.ImageButton personPlatform;
        private Controls.ImageButton imageButton1;
        private System.Windows.Forms.ContextMenuStrip cmsiAdd;
        private System.Windows.Forms.ToolStripMenuItem tsmiCreateMeeting;
        private System.Windows.Forms.ToolStripMenuItem tsmiAttendMeeting;
        private Controls.ImageButton btnSearchCancel;
    }
}

