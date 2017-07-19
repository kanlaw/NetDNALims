namespace IMClient.XMPP.Forms
{
    partial class RecordForm
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
            this.tmVoice = new System.Windows.Forms.Timer(this.components);
            this.btnCloseIcon = new IMClient.Controls.ImageButton();
            this.btnClose = new IMClient.Controls.ImageButton();
            this.btnFinish = new IMClient.Controls.ImageButton();
            this.btnSave = new IMClient.Controls.ImageButton();
            this.lblTm = new System.Windows.Forms.Label();
            this.pnlSave = new System.Windows.Forms.Panel();
            this.recordOver = new IMClient.Controls.ImageButton();
            this.recordStart = new IMClient.Controls.ImageButton();
            this.SuspendLayout();
            // 
            // tmVoice
            // 
            this.tmVoice.Interval = 1000;
            this.tmVoice.Tick += new System.EventHandler(this.tmVoice_Tick);
            // 
            // btnCloseIcon
            // 
            this.btnCloseIcon.Activepic = global::IMClient.Properties.Resources.tablets_close_over_03;
            this.btnCloseIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCloseIcon.BackImage = null;
            this.btnCloseIcon.ColorBack = System.Drawing.Color.Transparent;
            this.btnCloseIcon.Location = new System.Drawing.Point(342, 4);
            this.btnCloseIcon.Name = "btnCloseIcon";
            this.btnCloseIcon.Presspic = global::IMClient.Properties.Resources.tablets_close_over_03;
            this.btnCloseIcon.ShowText = null;
            this.btnCloseIcon.Size = new System.Drawing.Size(30, 18);
            this.btnCloseIcon.Staticpic = global::IMClient.Properties.Resources.tablets_close_normal_03;
            this.btnCloseIcon.Stretch = false;
            this.btnCloseIcon.SupportToggle = false;
            this.btnCloseIcon.TabIndex = 139;
            this.btnCloseIcon.Text = "imageButton1";
            this.btnCloseIcon.Toggle = false;
            this.btnCloseIcon.ToolTipOpacity = 1D;
            this.btnCloseIcon.ToolTipShown = false;
            this.btnCloseIcon.TooltipX = 0;
            this.btnCloseIcon.TooltipY = 0;
            this.btnCloseIcon.Unablepic = null;
            this.btnCloseIcon.UseVisualStyleBackColor = true;
            this.btnCloseIcon.Click += new System.EventHandler(this.recordFromClose_Click);
            // 
            // btnClose
            // 
            this.btnClose.Activepic = global::IMClient.Properties.Resources.stop_record_over_03;
            this.btnClose.BackImage = null;
            this.btnClose.ColorBack = System.Drawing.Color.Transparent;
            this.btnClose.Location = new System.Drawing.Point(115, 82);
            this.btnClose.Name = "btnClose";
            this.btnClose.Presspic = global::IMClient.Properties.Resources.stop_record_over_03;
            this.btnClose.ShowText = null;
            this.btnClose.Size = new System.Drawing.Size(28, 28);
            this.btnClose.Staticpic = global::IMClient.Properties.Resources.stop_record_normal_03;
            this.btnClose.Stretch = false;
            this.btnClose.SupportToggle = false;
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "imageButton3";
            this.btnClose.Toggle = false;
            this.btnClose.ToolTipOpacity = 1D;
            this.btnClose.ToolTipShown = false;
            this.btnClose.TooltipX = 0;
            this.btnClose.TooltipY = 0;
            this.btnClose.Unablepic = global::IMClient.Properties.Resources.stop_record_grey_03;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Visible = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnFinish
            // 
            this.btnFinish.Activepic = global::IMClient.Properties.Resources.record_over_03;
            this.btnFinish.BackImage = null;
            this.btnFinish.ColorBack = System.Drawing.Color.Transparent;
            this.btnFinish.Location = new System.Drawing.Point(172, 82);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Presspic = global::IMClient.Properties.Resources.record_over_03;
            this.btnFinish.ShowText = null;
            this.btnFinish.Size = new System.Drawing.Size(28, 28);
            this.btnFinish.Staticpic = global::IMClient.Properties.Resources.record_normal_03;
            this.btnFinish.Stretch = false;
            this.btnFinish.SupportToggle = false;
            this.btnFinish.TabIndex = 6;
            this.btnFinish.Text = "imageButton2";
            this.btnFinish.Toggle = false;
            this.btnFinish.ToolTipOpacity = 1D;
            this.btnFinish.ToolTipShown = false;
            this.btnFinish.TooltipX = 0;
            this.btnFinish.TooltipY = 0;
            this.btnFinish.Unablepic = null;
            this.btnFinish.UseVisualStyleBackColor = true;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // btnSave
            // 
            this.btnSave.Activepic = global::IMClient.Properties.Resources.play_record_over_03;
            this.btnSave.BackImage = null;
            this.btnSave.ColorBack = System.Drawing.Color.Transparent;
            this.btnSave.Location = new System.Drawing.Point(229, 82);
            this.btnSave.Name = "btnSave";
            this.btnSave.Presspic = global::IMClient.Properties.Resources.play_record_over_03;
            this.btnSave.ShowText = null;
            this.btnSave.Size = new System.Drawing.Size(28, 28);
            this.btnSave.Staticpic = global::IMClient.Properties.Resources.play_record_normal_03;
            this.btnSave.Stretch = false;
            this.btnSave.SupportToggle = false;
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "imageButton1";
            this.btnSave.Toggle = false;
            this.btnSave.ToolTipOpacity = 1D;
            this.btnSave.ToolTipShown = false;
            this.btnSave.TooltipX = 0;
            this.btnSave.TooltipY = 0;
            this.btnSave.Unablepic = global::IMClient.Properties.Resources.play_record_grey_03;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Visible = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblTm
            // 
            this.lblTm.AutoSize = true;
            this.lblTm.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(112)))), ((int)(((byte)(193)))));
            this.lblTm.Location = new System.Drawing.Point(46, 6);
            this.lblTm.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTm.Name = "lblTm";
            this.lblTm.Size = new System.Drawing.Size(49, 19);
            this.lblTm.TabIndex = 4;
            this.lblTm.Text = "00:10";
            // 
            // pnlSave
            // 
            this.pnlSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSave.AutoSize = true;
            this.pnlSave.Location = new System.Drawing.Point(4, 36);
            this.pnlSave.Margin = new System.Windows.Forms.Padding(0);
            this.pnlSave.Name = "pnlSave";
            this.pnlSave.Size = new System.Drawing.Size(371, 41);
            this.pnlSave.TabIndex = 3;
            this.pnlSave.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlSave_Paint);
            // 
            // recordOver
            // 
            this.recordOver.Activepic = null;
            this.recordOver.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.recordOver.BackColor = System.Drawing.Color.Red;
            this.recordOver.BackImage = null;
            this.recordOver.ColorBack = System.Drawing.Color.Empty;
            this.recordOver.Location = new System.Drawing.Point(229, 0);
            this.recordOver.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.recordOver.Name = "recordOver";
            this.recordOver.Presspic = null;
            this.recordOver.ShowText = null;
            this.recordOver.Size = new System.Drawing.Size(80, 26);
            this.recordOver.Staticpic = null;
            this.recordOver.Stretch = false;
            this.recordOver.SupportToggle = false;
            this.recordOver.TabIndex = 1;
            this.recordOver.Text = "完成";
            this.recordOver.Toggle = false;
            this.recordOver.ToolTipOpacity = 1D;
            this.recordOver.ToolTipShown = false;
            this.recordOver.TooltipX = 0;
            this.recordOver.TooltipY = 0;
            this.recordOver.Unablepic = null;
            this.recordOver.UseVisualStyleBackColor = false;
            this.recordOver.Visible = false;
            this.recordOver.Click += new System.EventHandler(this.recordOver_Click);
            // 
            // recordStart
            // 
            this.recordStart.Activepic = null;
            this.recordStart.BackColor = System.Drawing.Color.Red;
            this.recordStart.BackImage = null;
            this.recordStart.ColorBack = System.Drawing.Color.Red;
            this.recordStart.Location = new System.Drawing.Point(142, -1);
            this.recordStart.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.recordStart.Name = "recordStart";
            this.recordStart.Presspic = null;
            this.recordStart.ShowText = null;
            this.recordStart.Size = new System.Drawing.Size(80, 26);
            this.recordStart.Staticpic = null;
            this.recordStart.Stretch = false;
            this.recordStart.SupportToggle = false;
            this.recordStart.TabIndex = 0;
            this.recordStart.Text = "播放";
            this.recordStart.Toggle = false;
            this.recordStart.ToolTipOpacity = 1D;
            this.recordStart.ToolTipShown = false;
            this.recordStart.TooltipX = 0;
            this.recordStart.TooltipY = 0;
            this.recordStart.Unablepic = null;
            this.recordStart.UseVisualStyleBackColor = false;
            this.recordStart.Visible = false;
            this.recordStart.Click += new System.EventHandler(this.recordStart_Click);
            // 
            // RecordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CaptionFont = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.ClientSize = new System.Drawing.Size(379, 117);
            this.Controls.Add(this.btnCloseIcon);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnFinish);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblTm);
            this.Controls.Add(this.pnlSave);
            this.Controls.Add(this.recordOver);
            this.Controls.Add(this.recordStart);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximumSize = new System.Drawing.Size(1920, 1003);
            this.Name = "RecordForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "录音";
            this.TitleOffset = new System.Drawing.Point(3, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.ImageButton recordStart;
        private Controls.ImageButton recordOver;
        private System.Windows.Forms.Panel pnlSave;
        private System.Windows.Forms.Timer tmVoice;
        private System.Windows.Forms.Label lblTm;
        private Controls.ImageButton btnSave;
        private Controls.ImageButton btnFinish;
        private Controls.ImageButton btnClose;
        private Controls.ImageButton btnCloseIcon;
    }
}