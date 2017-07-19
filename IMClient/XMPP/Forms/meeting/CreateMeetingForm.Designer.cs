namespace IMClient.XMPP.Forms
{
    partial class CreateMeetingForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateMeetingForm));
            this.btnClose = new IMClient.Controls.ImageButton();
            this.ibtnMin = new IMClient.Controls.ImageButton();
            this.rbtnOpen = new System.Windows.Forms.RadioButton();
            this.rbtnEncrypt = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSubject = new IMClient.Controls.ImageTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMeetingPoint = new IMClient.Controls.ImageTextBox();
            this.lblVote = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSelectVote = new IMClient.Controls.ImageTextBox();
            this.PnlVote = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCancel = new IMClient.Controls.ImageButton();
            this.btnEnter = new IMClient.Controls.ImageButton();
            this.btnVoteAdd = new IMClient.Controls.ImageButton();
            this.iline1 = new IMClient.Controls.ItemLine();
            this.cbxVote = new System.Windows.Forms.CheckBox();
            this.meetGrouplist = new JustLib.UnitViews.GroupListBox();
            this.btnAdd = new IMClient.Controls.ImageButton();
            this.itemLine2 = new IMClient.Controls.ItemLine();
            this.label7 = new System.Windows.Forms.Label();
            this.txtRoomName = new IMClient.Controls.ImageTextBox();
            this.btnAddEmcee = new IMClient.Controls.ImageButton();
            this.cmsiAdd = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiEmcee = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMember = new System.Windows.Forms.ToolStripMenuItem();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPwd = new IMClient.Controls.ImageTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbxPerson = new IMClient.Controls.ImageComboBox();
            this.PnlVote.SuspendLayout();
            this.cmsiAdd.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Activepic = global::IMClient.Properties.Resources.close_over_02;
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackImage = null;
            this.btnClose.ColorBack = System.Drawing.Color.Empty;
            this.btnClose.Location = new System.Drawing.Point(750, 7);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Presspic = global::IMClient.Properties.Resources.close_down_02;
            this.btnClose.ShowText = null;
            this.btnClose.Size = new System.Drawing.Size(29, 24);
            this.btnClose.Staticpic = global::IMClient.Properties.Resources.close_normal_02;
            this.btnClose.Stretch = false;
            this.btnClose.SupportToggle = false;
            this.btnClose.TabIndex = 11;
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
            this.ibtnMin.Location = new System.Drawing.Point(718, 7);
            this.ibtnMin.Name = "ibtnMin";
            this.ibtnMin.Presspic = global::IMClient.Properties.Resources.minimize_down_02;
            this.ibtnMin.ShowText = null;
            this.ibtnMin.Size = new System.Drawing.Size(29, 24);
            this.ibtnMin.Staticpic = global::IMClient.Properties.Resources.minimize_normal_02;
            this.ibtnMin.Stretch = false;
            this.ibtnMin.SupportToggle = false;
            this.ibtnMin.TabIndex = 9;
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
            // rbtnOpen
            // 
            this.rbtnOpen.AutoSize = true;
            this.rbtnOpen.Checked = true;
            this.rbtnOpen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.rbtnOpen.Location = new System.Drawing.Point(124, 60);
            this.rbtnOpen.Name = "rbtnOpen";
            this.rbtnOpen.Size = new System.Drawing.Size(61, 17);
            this.rbtnOpen.TabIndex = 12;
            this.rbtnOpen.TabStop = true;
            this.rbtnOpen.Text = "开放型";
            this.rbtnOpen.UseVisualStyleBackColor = true;
            // 
            // rbtnEncrypt
            // 
            this.rbtnEncrypt.AutoSize = true;
            this.rbtnEncrypt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.rbtnEncrypt.Location = new System.Drawing.Point(200, 60);
            this.rbtnEncrypt.Name = "rbtnEncrypt";
            this.rbtnEncrypt.Size = new System.Drawing.Size(61, 17);
            this.rbtnEncrypt.TabIndex = 13;
            this.rbtnEncrypt.Text = "加密型";
            this.rbtnEncrypt.UseVisualStyleBackColor = true;
            this.rbtnEncrypt.CheckedChanged += new System.EventHandler(this.rbtnEncrypt_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(27, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 22);
            this.label1.TabIndex = 14;
            this.label1.Text = "会 议 类 型";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(27, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 22);
            this.label2.TabIndex = 15;
            this.label2.Text = "会 议 主 题";
            // 
            // txtSubject
            // 
            this.txtSubject.Bordercolor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(230)))), ((int)(((byte)(228)))));
            this.txtSubject.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSubject.Controllist = ((System.Collections.ArrayList)(resources.GetObject("txtSubject.Controllist")));
            this.txtSubject.EmptyTextTip = null;
            this.txtSubject.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.txtSubject.Location = new System.Drawing.Point(124, 134);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.SelectedBordercolor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(113)))), ((int)(((byte)(192)))));
            this.txtSubject.Size = new System.Drawing.Size(436, 25);
            this.txtSubject.TabIndex = 16;
            this.txtSubject.TStatus = IMClient.Controls.TextStatus.ALL;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(27, 173);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 22);
            this.label3.TabIndex = 17;
            this.label3.Text = "讨 论 要 点";
            // 
            // txtMeetingPoint
            // 
            this.txtMeetingPoint.Bordercolor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(230)))), ((int)(((byte)(228)))));
            this.txtMeetingPoint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMeetingPoint.Controllist = ((System.Collections.ArrayList)(resources.GetObject("txtMeetingPoint.Controllist")));
            this.txtMeetingPoint.EmptyTextTip = null;
            this.txtMeetingPoint.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.txtMeetingPoint.Location = new System.Drawing.Point(124, 173);
            this.txtMeetingPoint.Multiline = true;
            this.txtMeetingPoint.Name = "txtMeetingPoint";
            this.txtMeetingPoint.SelectedBordercolor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(113)))), ((int)(((byte)(192)))));
            this.txtMeetingPoint.Size = new System.Drawing.Size(436, 107);
            this.txtMeetingPoint.TabIndex = 18;
            this.txtMeetingPoint.TStatus = IMClient.Controls.TextStatus.ALL;
            // 
            // lblVote
            // 
            this.lblVote.AutoSize = true;
            this.lblVote.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.lblVote.Location = new System.Drawing.Point(27, 294);
            this.lblVote.Name = "lblVote";
            this.lblVote.Size = new System.Drawing.Size(42, 22);
            this.lblVote.TabIndex = 21;
            this.lblVote.Text = "投票";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label6.Location = new System.Drawing.Point(30, 341);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 20);
            this.label6.TabIndex = 24;
            this.label6.Text = "投票选项";
            // 
            // txtSelectVote
            // 
            this.txtSelectVote.Bordercolor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(230)))), ((int)(((byte)(228)))));
            this.txtSelectVote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSelectVote.Controllist = ((System.Collections.ArrayList)(resources.GetObject("txtSelectVote.Controllist")));
            this.txtSelectVote.EmptyTextTip = null;
            this.txtSelectVote.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.txtSelectVote.Location = new System.Drawing.Point(124, 341);
            this.txtSelectVote.Name = "txtSelectVote";
            this.txtSelectVote.SelectedBordercolor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(113)))), ((int)(((byte)(192)))));
            this.txtSelectVote.Size = new System.Drawing.Size(409, 25);
            this.txtSelectVote.TabIndex = 25;
            this.txtSelectVote.TStatus = IMClient.Controls.TextStatus.ALL;
            // 
            // PnlVote
            // 
            this.PnlVote.BackColor = System.Drawing.Color.White;
            this.PnlVote.Controls.Add(this.flowLayoutPanel1);
            this.PnlVote.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.PnlVote.Location = new System.Drawing.Point(31, 372);
            this.PnlVote.Name = "PnlVote";
            this.PnlVote.Size = new System.Drawing.Size(532, 135);
            this.PnlVote.TabIndex = 27;
            this.PnlVote.Paint += new System.Windows.Forms.PaintEventHandler(this.PnlVote_Paint);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(526, 129);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Activepic = global::IMClient.Properties.Resources.cancel02_over_03;
            this.btnCancel.BackImage = null;
            this.btnCancel.ColorBack = System.Drawing.Color.Transparent;
            this.btnCancel.Location = new System.Drawing.Point(511, 294);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Presspic = global::IMClient.Properties.Resources.cancel02_over_03;
            this.btnCancel.ShowText = null;
            this.btnCancel.Size = new System.Drawing.Size(52, 25);
            this.btnCancel.Staticpic = global::IMClient.Properties.Resources.cancel02_normal_03;
            this.btnCancel.Stretch = false;
            this.btnCancel.SupportToggle = false;
            this.btnCancel.TabIndex = 38;
            this.btnCancel.Text = "imageButton3";
            this.btnCancel.Toggle = false;
            this.btnCancel.ToolTipOpacity = 1D;
            this.btnCancel.ToolTipShown = false;
            this.btnCancel.TooltipX = 0;
            this.btnCancel.TooltipY = 0;
            this.btnCancel.Unablepic = null;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnEnter
            // 
            this.btnEnter.Activepic = global::IMClient.Properties.Resources.tok_over_03;
            this.btnEnter.BackImage = null;
            this.btnEnter.ColorBack = System.Drawing.Color.Transparent;
            this.btnEnter.Location = new System.Drawing.Point(436, 294);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Presspic = global::IMClient.Properties.Resources.tok_over_03;
            this.btnEnter.ShowText = null;
            this.btnEnter.Size = new System.Drawing.Size(52, 25);
            this.btnEnter.Staticpic = global::IMClient.Properties.Resources.ok_normal_03;
            this.btnEnter.Stretch = false;
            this.btnEnter.SupportToggle = false;
            this.btnEnter.TabIndex = 37;
            this.btnEnter.Text = "imageButton1";
            this.btnEnter.Toggle = false;
            this.btnEnter.ToolTipOpacity = 1D;
            this.btnEnter.ToolTipShown = false;
            this.btnEnter.TooltipX = 0;
            this.btnEnter.TooltipY = 0;
            this.btnEnter.Unablepic = null;
            this.btnEnter.UseVisualStyleBackColor = true;
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // btnVoteAdd
            // 
            this.btnVoteAdd.Activepic = global::IMClient.Properties.Resources.Add_user_over_03;
            this.btnVoteAdd.BackImage = null;
            this.btnVoteAdd.ColorBack = System.Drawing.Color.Transparent;
            this.btnVoteAdd.Location = new System.Drawing.Point(540, 343);
            this.btnVoteAdd.Name = "btnVoteAdd";
            this.btnVoteAdd.Presspic = global::IMClient.Properties.Resources.Add_user_over_03;
            this.btnVoteAdd.ShowText = null;
            this.btnVoteAdd.Size = new System.Drawing.Size(23, 23);
            this.btnVoteAdd.Staticpic = global::IMClient.Properties.Resources.Add_user_normal_03;
            this.btnVoteAdd.Stretch = false;
            this.btnVoteAdd.SupportToggle = false;
            this.btnVoteAdd.TabIndex = 28;
            this.btnVoteAdd.Text = "btnMeetingPoint";
            this.btnVoteAdd.Toggle = false;
            this.btnVoteAdd.ToolTipOpacity = 1D;
            this.btnVoteAdd.ToolTipShown = false;
            this.btnVoteAdd.TooltipX = 0;
            this.btnVoteAdd.TooltipY = 0;
            this.btnVoteAdd.Unablepic = null;
            this.btnVoteAdd.UseVisualStyleBackColor = true;
            this.btnVoteAdd.Click += new System.EventHandler(this.btnVoteAdd_Click);
            // 
            // iline1
            // 
            this.iline1.AutoLine2 = false;
            this.iline1.BackColor = System.Drawing.Color.Transparent;
            this.iline1.FontImageMargin = 2;
            this.iline1.FontLineMargin = 5;
            this.iline1.ImageLineMargin = 5;
            this.iline1.ImageSize = new System.Drawing.Size(20, 20);
            this.iline1.IsShowFont = false;
            this.iline1.IsShowImage = false;
            this.iline1.IsShowTwoLine = false;
            this.iline1.IsVertocal = false;
            this.iline1.LineLength = 20;
            this.iline1.LineLocation = IMClient.Controls.lineLocationType.center;
            this.iline1.Linetype = IMClient.Controls.lineType.solid;
            this.iline1.Location = new System.Drawing.Point(7, 294);
            this.iline1.Name = "iline1";
            this.iline1.PenColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.iline1.PenColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.iline1.PenWidth = 1F;
            this.iline1.PenWidth2 = 1F;
            this.iline1.Size = new System.Drawing.Size(556, 12);
            this.iline1.TabIndex = 29;
            this.iline1.Text = "itemLine1";
            this.iline1.TitleImage = ((System.Drawing.Image)(resources.GetObject("iline1.TitleImage")));
            this.iline1.Visible = false;
            // 
            // cbxVote
            // 
            this.cbxVote.AutoSize = true;
            this.cbxVote.Location = new System.Drawing.Point(124, 299);
            this.cbxVote.Name = "cbxVote";
            this.cbxVote.Size = new System.Drawing.Size(15, 14);
            this.cbxVote.TabIndex = 32;
            this.cbxVote.UseVisualStyleBackColor = true;
            this.cbxVote.CheckedChanged += new System.EventHandler(this.cbxVote_CheckedChanged);
            // 
            // meetGrouplist
            // 
            this.meetGrouplist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.meetGrouplist.BackColor = System.Drawing.Color.White;
            this.meetGrouplist.Location = new System.Drawing.Point(572, 60);
            this.meetGrouplist.Margin = new System.Windows.Forms.Padding(2);
            this.meetGrouplist.Name = "meetGrouplist";
            this.meetGrouplist.Size = new System.Drawing.Size(219, 210);
            this.meetGrouplist.TabIndex = 33;
            // 
            // btnAdd
            // 
            this.btnAdd.Activepic = global::IMClient.Properties.Resources.Invite_friends_over_03;
            this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnAdd.BackImage = null;
            this.btnAdd.ColorBack = System.Drawing.Color.Transparent;
            this.btnAdd.Location = new System.Drawing.Point(572, 275);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Presspic = global::IMClient.Properties.Resources.Invite_friends_over_03;
            this.btnAdd.ShowText = null;
            this.btnAdd.Size = new System.Drawing.Size(219, 38);
            this.btnAdd.Staticpic = global::IMClient.Properties.Resources.Invite_friends_normal_03;
            this.btnAdd.Stretch = true;
            this.btnAdd.SupportToggle = false;
            this.btnAdd.TabIndex = 34;
            this.btnAdd.Tag = "2";
            this.btnAdd.Text = "imageButton1";
            this.btnAdd.Toggle = false;
            this.btnAdd.ToolTipOpacity = 1D;
            this.btnAdd.ToolTipShown = false;
            this.btnAdd.TooltipX = 0;
            this.btnAdd.TooltipY = 0;
            this.btnAdd.Unablepic = null;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.tsmiEmcee_Click);
            // 
            // itemLine2
            // 
            this.itemLine2.AutoLine2 = false;
            this.itemLine2.BackColor = System.Drawing.Color.Transparent;
            this.itemLine2.CausesValidation = false;
            this.itemLine2.FontImageMargin = 2;
            this.itemLine2.FontLineMargin = 5;
            this.itemLine2.ImageLineMargin = 5;
            this.itemLine2.ImageSize = new System.Drawing.Size(20, 20);
            this.itemLine2.IsShowFont = false;
            this.itemLine2.IsShowImage = false;
            this.itemLine2.IsShowTwoLine = false;
            this.itemLine2.IsVertocal = true;
            this.itemLine2.LineLength = 20;
            this.itemLine2.LineLocation = IMClient.Controls.lineLocationType.center;
            this.itemLine2.Linetype = IMClient.Controls.lineType.solid;
            this.itemLine2.Location = new System.Drawing.Point(569, 31);
            this.itemLine2.Name = "itemLine2";
            this.itemLine2.PenColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.itemLine2.PenColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.itemLine2.PenWidth = 1F;
            this.itemLine2.PenWidth2 = 1F;
            this.itemLine2.Size = new System.Drawing.Size(3, 520);
            this.itemLine2.TabIndex = 35;
            this.itemLine2.Text = "itemLine2";
            this.itemLine2.TitleImage = ((System.Drawing.Image)(resources.GetObject("itemLine2.TitleImage")));
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(27, 95);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 22);
            this.label7.TabIndex = 36;
            this.label7.Text = "会议室名称";
            // 
            // txtRoomName
            // 
            this.txtRoomName.Bordercolor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(230)))), ((int)(((byte)(228)))));
            this.txtRoomName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRoomName.Controllist = ((System.Collections.ArrayList)(resources.GetObject("txtRoomName.Controllist")));
            this.txtRoomName.EmptyTextTip = null;
            this.txtRoomName.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.txtRoomName.Location = new System.Drawing.Point(124, 95);
            this.txtRoomName.Name = "txtRoomName";
            this.txtRoomName.SelectedBordercolor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(113)))), ((int)(((byte)(192)))));
            this.txtRoomName.Size = new System.Drawing.Size(253, 25);
            this.txtRoomName.TabIndex = 37;
            this.txtRoomName.TStatus = IMClient.Controls.TextStatus.ALL;
            // 
            // btnAddEmcee
            // 
            this.btnAddEmcee.Activepic = global::IMClient.Properties.Resources.Add_user_over_03;
            this.btnAddEmcee.BackImage = null;
            this.btnAddEmcee.ColorBack = System.Drawing.Color.Transparent;
            this.btnAddEmcee.ContextMenuStrip = this.cmsiAdd;
            this.btnAddEmcee.Location = new System.Drawing.Point(511, 60);
            this.btnAddEmcee.Name = "btnAddEmcee";
            this.btnAddEmcee.Presspic = global::IMClient.Properties.Resources.Add_user_over_03;
            this.btnAddEmcee.ShowText = null;
            this.btnAddEmcee.Size = new System.Drawing.Size(23, 23);
            this.btnAddEmcee.Staticpic = global::IMClient.Properties.Resources.Add_user_normal_03;
            this.btnAddEmcee.Stretch = false;
            this.btnAddEmcee.SupportToggle = false;
            this.btnAddEmcee.TabIndex = 39;
            this.btnAddEmcee.Text = "btnMeetingPoint";
            this.btnAddEmcee.Toggle = false;
            this.btnAddEmcee.ToolTipOpacity = 1D;
            this.btnAddEmcee.ToolTipShown = false;
            this.btnAddEmcee.TooltipX = 0;
            this.btnAddEmcee.TooltipY = 0;
            this.btnAddEmcee.Unablepic = null;
            this.btnAddEmcee.UseVisualStyleBackColor = true;
            this.btnAddEmcee.Click += new System.EventHandler(this.btnAddEmcee_Click);
            // 
            // cmsiAdd
            // 
            this.cmsiAdd.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEmcee,
            this.tsmiMember});
            this.cmsiAdd.Name = "cmsiAdd";
            this.cmsiAdd.Size = new System.Drawing.Size(125, 48);
            // 
            // tsmiEmcee
            // 
            this.tsmiEmcee.Name = "tsmiEmcee";
            this.tsmiEmcee.Size = new System.Drawing.Size(124, 22);
            this.tsmiEmcee.Tag = "1";
            this.tsmiEmcee.Text = "主持人";
            this.tsmiEmcee.Click += new System.EventHandler(this.tsmiEmcee_Click);
            // 
            // tsmiMember
            // 
            this.tsmiMember.Name = "tsmiMember";
            this.tsmiMember.Size = new System.Drawing.Size(124, 22);
            this.tsmiMember.Tag = "2";
            this.tsmiMember.Text = "与会人员";
            this.tsmiMember.Click += new System.EventHandler(this.tsmiEmcee_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(399, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 22);
            this.label4.TabIndex = 40;
            this.label4.Text = "成 员 添 加";
            // 
            // txtPwd
            // 
            this.txtPwd.Bordercolor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(230)))), ((int)(((byte)(228)))));
            this.txtPwd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPwd.Controllist = ((System.Collections.ArrayList)(resources.GetObject("txtPwd.Controllist")));
            this.txtPwd.EmptyTextTip = null;
            this.txtPwd.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.txtPwd.Location = new System.Drawing.Point(267, 56);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.SelectedBordercolor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(113)))), ((int)(((byte)(192)))));
            this.txtPwd.Size = new System.Drawing.Size(110, 25);
            this.txtPwd.TabIndex = 41;
            this.txtPwd.TStatus = IMClient.Controls.TextStatus.ALL;
            this.txtPwd.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(399, 95);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 22);
            this.label8.TabIndex = 42;
            this.label8.Text = "成 员 添 加";
            // 
            // cbxPerson
            // 
            this.cbxPerson.ArrowColor = System.Drawing.Color.DarkGray;
            this.cbxPerson.BaseColor = System.Drawing.Color.White;
            this.cbxPerson.BorderColor = System.Drawing.Color.DarkGray;
            this.cbxPerson.EnableColor = System.Drawing.Color.White;
            this.cbxPerson.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbxPerson.FormattingEnabled = true;
            this.cbxPerson.Items.AddRange(new object[] {
            "无限制",
            "10",
            "20",
            "30",
            "40",
            "50"});
            this.cbxPerson.Location = new System.Drawing.Point(495, 99);
            this.cbxPerson.Name = "cbxPerson";
            this.cbxPerson.Size = new System.Drawing.Size(65, 20);
            this.cbxPerson.TabIndex = 43;
            this.cbxPerson.Text = "无限制";
            // 
            // CreateMeetingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(794, 545);
            this.Controls.Add(this.cbxPerson);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtPwd);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnAddEmcee);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtRoomName);
            this.Controls.Add(this.btnEnter);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.itemLine2);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.meetGrouplist);
            this.Controls.Add(this.cbxVote);
            this.Controls.Add(this.iline1);
            this.Controls.Add(this.btnVoteAdd);
            this.Controls.Add(this.PnlVote);
            this.Controls.Add(this.txtSelectVote);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblVote);
            this.Controls.Add(this.txtMeetingPoint);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtSubject);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rbtnEncrypt);
            this.Controls.Add(this.rbtnOpen);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ibtnMin);
            this.Name = "CreateMeetingForm";
            this.Shadow = true;
            this.Text = "";
            this.TopMost = false;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CreateMeetingForm_FormClosed);
            this.Load += new System.EventHandler(this.CreateMeetingForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.CreateMeetingForm_Paint);
            this.PnlVote.ResumeLayout(false);
            this.cmsiAdd.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.ImageButton btnClose;
        private Controls.ImageButton ibtnMin;
        private System.Windows.Forms.RadioButton rbtnOpen;
        private System.Windows.Forms.RadioButton rbtnEncrypt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Controls.ImageTextBox txtSubject;
        private System.Windows.Forms.Label label3;
        private Controls.ImageTextBox txtMeetingPoint;
        private System.Windows.Forms.Label lblVote;
        private System.Windows.Forms.Label label6;
        private Controls.ImageTextBox txtSelectVote;
        private System.Windows.Forms.Panel PnlVote;
        private Controls.ImageButton btnVoteAdd;
        private Controls.ItemLine iline1;
        private System.Windows.Forms.CheckBox cbxVote;
        private JustLib.UnitViews.GroupListBox meetGrouplist;
        private Controls.ImageButton btnAdd;
        private Controls.ItemLine itemLine2;
        private Controls.ImageButton btnEnter;
        private Controls.ImageButton btnCancel;
        private System.Windows.Forms.Label label7;
        private Controls.ImageTextBox txtRoomName;
        private Controls.ImageButton btnAddEmcee;
        private System.Windows.Forms.Label label4;
        private Controls.ImageTextBox txtPwd;
        private System.Windows.Forms.ContextMenuStrip cmsiAdd;
        private System.Windows.Forms.ToolStripMenuItem tsmiEmcee;
        private System.Windows.Forms.ToolStripMenuItem tsmiMember;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label8;
        private Controls.ImageComboBox cbxPerson;
    }
}