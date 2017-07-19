namespace IMClient.Controls.Tools
{
    partial class FrmMsg
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
            this.timerHide = new System.Windows.Forms.Timer(this.components);
            this.btnClose = new IMClient.Controls.ImageButton();
            this.SuspendLayout();
            // 
            // timerHide
            // 
            this.timerHide.Interval = 750;
            this.timerHide.Tick += new System.EventHandler(this.timerHide_Tick);
            // 
            // btnClose
            // 
            this.btnClose.Activepic = global::IMClient.Properties.Resources.close_button_small_03;
            this.btnClose.BackImage = null;
            this.btnClose.ColorBack = System.Drawing.Color.Empty;
            this.btnClose.Location = new System.Drawing.Point(249, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Presspic = global::IMClient.Properties.Resources.close_button_small_03;
            this.btnClose.ShowText = null;
            this.btnClose.Size = new System.Drawing.Size(10, 10);
            this.btnClose.Staticpic = global::IMClient.Properties.Resources.close_button_small_03;
            this.btnClose.Stretch = false;
            this.btnClose.SupportToggle = false;
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "imageButton1";
            this.btnClose.Toggle = false;
            this.btnClose.ToolTipOpacity = 1D;
            this.btnClose.ToolTipShown = false;
            this.btnClose.TooltipX = 0;
            this.btnClose.TooltipY = 0;
            this.btnClose.Unablepic = null;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FrmMsg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(262, 159);
            this.Controls.Add(this.btnClose);
            this.EffectCaption = CCWin.TitleType.EffectTitle;
            this.Mobile = CCWin.MobileStyle.Mobile;
            this.Name = "FrmMsg";
            this.ShowInTaskbar = false;
            this.Text = "FrmMsg";
            this.Load += new System.EventHandler(this.FrmMsg_Load);
            this.Shown += new System.EventHandler(this.FrmMsg_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerHide;
        private ImageButton btnClose;
    }
}