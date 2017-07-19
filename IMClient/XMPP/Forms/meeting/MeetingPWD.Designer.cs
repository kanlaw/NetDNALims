namespace IMClient.XMPP.Forms
{
    partial class MeetingPWD
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MeetingPWD));
            this.btnClose = new IMClient.Controls.ImageButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPwd = new IMClient.Controls.ImageTextBox();
            this.btnEnter = new IMClient.Controls.ImageButton();
            this.btnCancel = new IMClient.Controls.ImageButton();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Activepic = global::IMClient.Properties.Resources.close_over_02;
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackImage = null;
            this.btnClose.ColorBack = System.Drawing.Color.Empty;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(243, 5);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "会议室密码";
            // 
            // txtPwd
            // 
            this.txtPwd.Bordercolor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(230)))), ((int)(((byte)(228)))));
            this.txtPwd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPwd.Controllist = ((System.Collections.ArrayList)(resources.GetObject("txtPwd.Controllist")));
            this.txtPwd.EmptyTextTip = null;
            this.txtPwd.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.txtPwd.Location = new System.Drawing.Point(86, 54);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.SelectedBordercolor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(113)))), ((int)(((byte)(192)))));
            this.txtPwd.Size = new System.Drawing.Size(186, 25);
            this.txtPwd.TabIndex = 38;
            this.txtPwd.TStatus = IMClient.Controls.TextStatus.ALL;
            // 
            // btnEnter
            // 
            this.btnEnter.Activepic = global::IMClient.Properties.Resources.determine_button_grey_over;
            this.btnEnter.BackImage = null;
            this.btnEnter.ColorBack = System.Drawing.Color.Transparent;
            this.btnEnter.Location = new System.Drawing.Point(69, 106);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Presspic = global::IMClient.Properties.Resources.determine_button_grey_over;
            this.btnEnter.ShowText = null;
            this.btnEnter.Size = new System.Drawing.Size(66, 27);
            this.btnEnter.Staticpic = global::IMClient.Properties.Resources.determine_button_grey_normal;
            this.btnEnter.Stretch = false;
            this.btnEnter.SupportToggle = false;
            this.btnEnter.TabIndex = 39;
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
            // btnCancel
            // 
            this.btnCancel.Activepic = global::IMClient.Properties.Resources.cancel_button_grey_normal;
            this.btnCancel.BackImage = null;
            this.btnCancel.ColorBack = System.Drawing.Color.Transparent;
            this.btnCancel.Location = new System.Drawing.Point(155, 106);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Presspic = global::IMClient.Properties.Resources.cancel_button_grey_over;
            this.btnCancel.ShowText = null;
            this.btnCancel.Size = new System.Drawing.Size(66, 27);
            this.btnCancel.Staticpic = global::IMClient.Properties.Resources.cancel_button_grey_normal;
            this.btnCancel.Stretch = false;
            this.btnCancel.SupportToggle = false;
            this.btnCancel.TabIndex = 40;
            this.btnCancel.Text = "imageButton2";
            this.btnCancel.Toggle = false;
            this.btnCancel.ToolTipOpacity = 1D;
            this.btnCancel.ToolTipShown = false;
            this.btnCancel.TooltipX = 0;
            this.btnCancel.TooltipY = 0;
            this.btnCancel.Unablepic = null;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // MeetingPWD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(279, 142);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnEnter);
            this.Controls.Add(this.txtPwd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Name = "MeetingPWD";
            this.Shadow = true;
            this.Text = "";
            this.TitleColor = System.Drawing.Color.White;
            this.TopMost = false;
            this.Shown += new System.EventHandler(this.UserListForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Controls.ImageButton btnClose;
        private System.Windows.Forms.Label label1;
        private Controls.ImageTextBox txtPwd;
        private Controls.ImageButton btnEnter;
        private Controls.ImageButton btnCancel;
    }
}