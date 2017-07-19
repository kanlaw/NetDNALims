namespace IMClient.XMPP.Forms
{
    partial class UserListForm
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
            System.Drawing.StringFormat stringFormat1 = new System.Drawing.StringFormat();
            System.Drawing.StringFormat stringFormat2 = new System.Drawing.StringFormat();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserListForm));
            this.btnSearch = new IMClient.Controls.ImageButton();
            this.idtUserinfo = new IMClient.Controls.ItemDataTable();
            this.txtUserInfo = new IMClient.Controls.ImageTextBox();
            this.btnClose = new IMClient.Controls.ImageButton();
            this.outerPanel = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.ScrollDt = new CustomControls.CustomScrollbar();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblCity3 = new System.Windows.Forms.Label();
            this.lblCity2 = new System.Windows.Forms.Label();
            this.lblCity = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.outerPanel.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Activepic = global::IMClient.Properties.Resources.search_blue_small1_over_03;
            this.btnSearch.BackImage = null;
            this.btnSearch.ColorBack = System.Drawing.Color.Transparent;
            this.btnSearch.Location = new System.Drawing.Point(734, 39);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Presspic = global::IMClient.Properties.Resources.search_blue_small1_over_03;
            this.btnSearch.ShowText = null;
            this.btnSearch.Size = new System.Drawing.Size(20, 20);
            this.btnSearch.Staticpic = global::IMClient.Properties.Resources.search_blue_small1_normal_03;
            this.btnSearch.Stretch = true;
            this.btnSearch.SupportToggle = false;
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "imageButton1";
            this.btnSearch.Toggle = false;
            this.btnSearch.ToolTipOpacity = 1D;
            this.btnSearch.ToolTipShown = false;
            this.btnSearch.TooltipX = 0;
            this.btnSearch.TooltipY = 0;
            this.btnSearch.Unablepic = null;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // idtUserinfo
            // 
            this.idtUserinfo.BackColor = System.Drawing.Color.Transparent;
            this.idtUserinfo.ColHeight = 30;
            this.idtUserinfo.Color_font = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(145)))), ((int)(((byte)(173)))));
            this.idtUserinfo.Color_Select = System.Drawing.Color.White;
            this.idtUserinfo.Color_title = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(145)))), ((int)(((byte)(173)))));
            this.idtUserinfo.Font_Content = new System.Drawing.Font("微软雅黑", 9F);
            this.idtUserinfo.Font_Select = new System.Drawing.Font("微软雅黑", 9F);
            this.idtUserinfo.Font_title = new System.Drawing.Font("微软雅黑", 9F);
            this.idtUserinfo.IsTitle = true;
            this.idtUserinfo.Location = new System.Drawing.Point(3, 3);
            this.idtUserinfo.Name = "idtUserinfo";
            stringFormat1.Alignment = System.Drawing.StringAlignment.Near;
            stringFormat1.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
            stringFormat1.LineAlignment = System.Drawing.StringAlignment.Near;
            stringFormat1.Trimming = System.Drawing.StringTrimming.Character;
            this.idtUserinfo.Sf_Content = stringFormat1;
            stringFormat2.Alignment = System.Drawing.StringAlignment.Near;
            stringFormat2.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
            stringFormat2.LineAlignment = System.Drawing.StringAlignment.Near;
            stringFormat2.Trimming = System.Drawing.StringTrimming.Character;
            this.idtUserinfo.Sf_Title = stringFormat2;
            this.idtUserinfo.Size = new System.Drawing.Size(857, 452);
            this.idtUserinfo.TabIndex = 2;
            this.idtUserinfo.Text = "itemDataTable1";
            this.idtUserinfo.TitleHeight = 30;
            this.idtUserinfo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.idtUserinfo_MouseClick);
            this.idtUserinfo.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.idtUserinfo_MouseDoubleClick);
            // 
            // txtUserInfo
            // 
            this.txtUserInfo.Bordercolor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(145)))), ((int)(((byte)(175)))));
            this.txtUserInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUserInfo.Controllist = ((System.Collections.ArrayList)(resources.GetObject("txtUserInfo.Controllist")));
            this.txtUserInfo.EmptyTextTip = "姓名/性别/职务/联系方式";
            this.txtUserInfo.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txtUserInfo.Location = new System.Drawing.Point(449, 34);
            this.txtUserInfo.Name = "txtUserInfo";
            this.txtUserInfo.SelectedBordercolor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtUserInfo.Size = new System.Drawing.Size(257, 29);
            this.txtUserInfo.TabIndex = 3;
            this.txtUserInfo.TStatus = IMClient.Controls.TextStatus.ALL;
            // 
            // btnClose
            // 
            this.btnClose.Activepic = global::IMClient.Properties.Resources.close_over_02;
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackImage = null;
            this.btnClose.ColorBack = System.Drawing.Color.Empty;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(862, 5);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Presspic = global::IMClient.Properties.Resources.close_down_02;
            this.btnClose.ShowText = null;
            this.btnClose.Size = new System.Drawing.Size(29, 24);
            this.btnClose.Staticpic = global::IMClient.Properties.Resources.close_normal_02;
            this.btnClose.Stretch = false;
            this.btnClose.SupportToggle = false;
            this.btnClose.TabIndex = 12;
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
            // outerPanel
            // 
            this.outerPanel.Controls.Add(this.flowLayoutPanel1);
            this.outerPanel.Location = new System.Drawing.Point(7, 135);
            this.outerPanel.Name = "outerPanel";
            this.outerPanel.Size = new System.Drawing.Size(863, 458);
            this.outerPanel.TabIndex = 13;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.idtUserinfo);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(884, 455);
            this.flowLayoutPanel1.TabIndex = 14;
            // 
            // ScrollDt
            // 
            this.ScrollDt.ChannelColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.ScrollDt.DownArrowImage = global::IMClient.Properties.Resources.scroll_bar_down_03;
            this.ScrollDt.LargeChange = 10;
            this.ScrollDt.Location = new System.Drawing.Point(876, 135);
            this.ScrollDt.Maximum = 100;
            this.ScrollDt.Minimum = 0;
            this.ScrollDt.MinimumSize = new System.Drawing.Size(15, 92);
            this.ScrollDt.Name = "ScrollDt";
            this.ScrollDt.Size = new System.Drawing.Size(15, 458);
            this.ScrollDt.SmallChange = 1;
            this.ScrollDt.TabIndex = 3;
            this.ScrollDt.ThumbBottomImage = global::IMClient.Properties.Resources.scroll_bar02_down_03;
            this.ScrollDt.ThumbBottomSpanImage = global::IMClient.Properties.Resources.scroll_bar02_middle_03;
            this.ScrollDt.ThumbMiddleImage = global::IMClient.Properties.Resources.scroll_bar02_middle_03;
            this.ScrollDt.ThumbTopImage = global::IMClient.Properties.Resources.scroll_bar02_up_03;
            this.ScrollDt.ThumbTopSpanImage = global::IMClient.Properties.Resources.scroll_bar02_middle_03;
            this.ScrollDt.UpArrowImage = global::IMClient.Properties.Resources.scroll_bar_up_03;
            this.ScrollDt.Value = 0;
            this.ScrollDt.Scroll += new System.EventHandler(this.ScrollDt_Scroll);
            // 
            // pnlSearch
            // 
            this.pnlSearch.Controls.Add(this.pictureBox3);
            this.pnlSearch.Controls.Add(this.pictureBox2);
            this.pnlSearch.Controls.Add(this.pictureBox1);
            this.pnlSearch.Controls.Add(this.lblCity3);
            this.pnlSearch.Controls.Add(this.lblCity2);
            this.pnlSearch.Controls.Add(this.lblCity);
            this.pnlSearch.Controls.Add(this.label1);
            this.pnlSearch.Controls.Add(this.txtUserInfo);
            this.pnlSearch.Controls.Add(this.btnSearch);
            this.pnlSearch.Location = new System.Drawing.Point(7, 44);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(884, 85);
            this.pnlSearch.TabIndex = 14;
            this.pnlSearch.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlSearch_Paint);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackgroundImage = global::IMClient.Properties.Resources.arrow_red_03;
            this.pictureBox3.Location = new System.Drawing.Point(435, 45);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(8, 5);
            this.pictureBox3.TabIndex = 10;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::IMClient.Properties.Resources.arrow_red_03;
            this.pictureBox2.Location = new System.Drawing.Point(370, 45);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(8, 5);
            this.pictureBox2.TabIndex = 9;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::IMClient.Properties.Resources.arrow_red_03;
            this.pictureBox1.Location = new System.Drawing.Point(310, 45);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(8, 5);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // lblCity3
            // 
            this.lblCity3.AutoSize = true;
            this.lblCity3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.lblCity3.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.lblCity3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(40)))), ((int)(((byte)(23)))));
            this.lblCity3.Location = new System.Drawing.Point(392, 38);
            this.lblCity3.Name = "lblCity3";
            this.lblCity3.Size = new System.Drawing.Size(37, 19);
            this.lblCity3.TabIndex = 7;
            this.lblCity3.Text = "全部";
            // 
            // lblCity2
            // 
            this.lblCity2.AutoSize = true;
            this.lblCity2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.lblCity2.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.lblCity2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(40)))), ((int)(((byte)(23)))));
            this.lblCity2.Location = new System.Drawing.Point(327, 38);
            this.lblCity2.Name = "lblCity2";
            this.lblCity2.Size = new System.Drawing.Size(37, 19);
            this.lblCity2.TabIndex = 6;
            this.lblCity2.Text = "全部";
            this.lblCity2.Click += new System.EventHandler(this.label3_Click);
            // 
            // lblCity
            // 
            this.lblCity.AutoSize = true;
            this.lblCity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.lblCity.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.lblCity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(40)))), ((int)(((byte)(23)))));
            this.lblCity.Location = new System.Drawing.Point(267, 38);
            this.lblCity.Name = "lblCity";
            this.lblCity.Size = new System.Drawing.Size(37, 19);
            this.lblCity.TabIndex = 5;
            this.lblCity.Text = "全部";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(187, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 19);
            this.label1.TabIndex = 4;
            this.label1.Text = "查询条件";
            // 
            // UserListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(898, 600);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.outerPanel);
            this.Controls.Add(this.ScrollDt);
            this.Controls.Add(this.btnClose);
            this.Name = "UserListForm";
            this.Text = "";
            this.TitleColor = System.Drawing.Color.White;
            this.TopMost = false;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.UserListForm_FormClosed);
            this.Shown += new System.EventHandler(this.UserListForm_Shown);
            this.outerPanel.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Controls.ImageButton btnSearch;
        private Controls.ItemDataTable idtUserinfo;
        private Controls.ImageTextBox txtUserInfo;
        private Controls.ImageButton btnClose;
        private System.Windows.Forms.Panel outerPanel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private CustomControls.CustomScrollbar ScrollDt;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Label lblCity2;
        private System.Windows.Forms.Label lblCity;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCity3;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}