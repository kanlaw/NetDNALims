namespace IMClient.XMPP.Forms
{
    partial class AddUserForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddUserForm));
            this.btnClose = new IMClient.Controls.ImageButton();
            this.txtMessage = new IMClient.Controls.ImageTextBox();
            this.btnAdd = new IMClient.Controls.ImageButton();
            this.btnAgree = new IMClient.Controls.ImageButton();
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
            this.btnClose.Location = new System.Drawing.Point(360, 6);
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
            // txtMessage
            // 
            this.txtMessage.Bordercolor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMessage.Controllist = ((System.Collections.ArrayList)(resources.GetObject("txtMessage.Controllist")));
            this.txtMessage.EmptyTextTip = null;
            this.txtMessage.Location = new System.Drawing.Point(152, 59);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.SelectedBordercolor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtMessage.Size = new System.Drawing.Size(237, 154);
            this.txtMessage.TabIndex = 12;
            this.txtMessage.Text = "我是";
            this.txtMessage.TStatus = IMClient.Controls.TextStatus.ALL;
            // 
            // btnAdd
            // 
            this.btnAdd.Activepic = null;
            this.btnAdd.BackImage = null;
            this.btnAdd.ColorBack = System.Drawing.Color.Transparent;
            this.btnAdd.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnAdd.Location = new System.Drawing.Point(152, 260);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Presspic = null;
            this.btnAdd.ShowText = null;
            this.btnAdd.Size = new System.Drawing.Size(48, 23);
            this.btnAdd.Staticpic = null;
            this.btnAdd.Stretch = false;
            this.btnAdd.SupportToggle = false;
            this.btnAdd.TabIndex = 13;
            this.btnAdd.Text = "imageButton1";
            this.btnAdd.Toggle = false;
            this.btnAdd.ToolTipOpacity = 1D;
            this.btnAdd.ToolTipShown = false;
            this.btnAdd.TooltipX = 0;
            this.btnAdd.TooltipY = 0;
            this.btnAdd.Unablepic = null;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Visible = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnAgree
            // 
            this.btnAgree.Activepic = global::IMClient.Properties.Resources.agree_over_03;
            this.btnAgree.BackImage = null;
            this.btnAgree.ColorBack = System.Drawing.Color.Transparent;
            this.btnAgree.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnAgree.Location = new System.Drawing.Point(265, 260);
            this.btnAgree.Name = "btnAgree";
            this.btnAgree.Presspic = global::IMClient.Properties.Resources.agree_over_03;
            this.btnAgree.ShowText = null;
            this.btnAgree.Size = new System.Drawing.Size(52, 25);
            this.btnAgree.Staticpic = global::IMClient.Properties.Resources.agree_normal_03;
            this.btnAgree.Stretch = false;
            this.btnAgree.SupportToggle = false;
            this.btnAgree.TabIndex = 16;
            this.btnAgree.Text = "imageButton1";
            this.btnAgree.Toggle = false;
            this.btnAgree.ToolTipOpacity = 1D;
            this.btnAgree.ToolTipShown = false;
            this.btnAgree.TooltipX = 0;
            this.btnAgree.TooltipY = 0;
            this.btnAgree.Unablepic = null;
            this.btnAgree.UseVisualStyleBackColor = true;
            this.btnAgree.Click += new System.EventHandler(this.btnAgree_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Activepic = global::IMClient.Properties.Resources.Refuse_over_03;
            this.btnCancel.BackImage = null;
            this.btnCancel.ColorBack = System.Drawing.Color.Transparent;
            this.btnCancel.Location = new System.Drawing.Point(337, 260);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Presspic = global::IMClient.Properties.Resources.Refuse_over_03;
            this.btnCancel.ShowText = null;
            this.btnCancel.Size = new System.Drawing.Size(52, 25);
            this.btnCancel.Staticpic = global::IMClient.Properties.Resources.Refuse_normal_03;
            this.btnCancel.Stretch = false;
            this.btnCancel.SupportToggle = false;
            this.btnCancel.TabIndex = 17;
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
            // AddUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAgree);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.btnClose);
            this.Name = "AddUserForm";
            this.RoundStyle = CCWin.SkinClass.RoundStyle.All;
            this.Text = "";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AddUserForm_FormClosed);
            this.Load += new System.EventHandler(this.AddUserForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.ImageButton btnClose;
        private Controls.ImageTextBox txtMessage;
        private Controls.ImageButton btnAdd;
        private Controls.ImageButton btnAgree;
        private Controls.ImageButton btnCancel;
    }
}