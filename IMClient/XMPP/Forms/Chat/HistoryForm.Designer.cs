namespace IMClient.XMPP.Forms
{
    partial class HistoryForm
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
            this.pnlContent = new System.Windows.Forms.Panel();
            this.btnClose = new IMClient.Controls.ImageButton();
            this.ibtnSF = new IMClient.Controls.ImageButton();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlContent.Location = new System.Drawing.Point(7, 41);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(278, 225);
            this.pnlContent.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Activepic = global::IMClient.Properties.Resources.close_over_02;
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackImage = null;
            this.btnClose.ColorBack = System.Drawing.Color.Empty;
            this.btnClose.Location = new System.Drawing.Point(256, 4);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Presspic = global::IMClient.Properties.Resources.close_down_02;
            this.btnClose.ShowText = null;
            this.btnClose.Size = new System.Drawing.Size(29, 24);
            this.btnClose.Staticpic = global::IMClient.Properties.Resources.close_normal_02;
            this.btnClose.Stretch = false;
            this.btnClose.SupportToggle = false;
            this.btnClose.TabIndex = 6;
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
            // ibtnSF
            // 
            this.ibtnSF.Activepic = global::IMClient.Properties.Resources.shrinkage01_32;
            this.ibtnSF.BackColor = System.Drawing.Color.White;
            this.ibtnSF.BackImage = null;
            this.ibtnSF.ColorBack = System.Drawing.Color.Empty;
            this.ibtnSF.Location = new System.Drawing.Point(-9, 114);
            this.ibtnSF.Name = "ibtnSF";
            this.ibtnSF.Presspic = global::IMClient.Properties.Resources.shrinkage01_32;
            this.ibtnSF.ShowText = null;
            this.ibtnSF.Size = new System.Drawing.Size(10, 60);
            this.ibtnSF.Staticpic = global::IMClient.Properties.Resources.shrinkage01_32;
            this.ibtnSF.Stretch = false;
            this.ibtnSF.SupportToggle = false;
            this.ibtnSF.TabIndex = 2;
            this.ibtnSF.Text = "缩进";
            this.ibtnSF.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.ibtnSF.Toggle = false;
            this.ibtnSF.ToolTipOpacity = 1D;
            this.ibtnSF.ToolTipShown = false;
            this.ibtnSF.TooltipX = 10;
            this.ibtnSF.TooltipY = 0;
            this.ibtnSF.Unablepic = null;
            this.ibtnSF.UseVisualStyleBackColor = false;
            this.ibtnSF.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // HistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.ibtnSF);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pnlContent);
            this.Name = "HistoryForm";
            this.Text = "历史记录";
            this.Shown += new System.EventHandler(this.HistoryForm_Shown);
            this.ResizeEnd += new System.EventHandler(this.HistoryForm_ResizeEnd);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlContent;
        private Controls.ImageButton btnClose;
        public Controls.ImageButton ibtnSF;
    }
}