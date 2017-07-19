namespace IMClient.XMPP.Forms
{
    partial class ImgForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImgForm));
            this.btnClose = new IMClient.Controls.ImageButton();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Activepic = ((System.Drawing.Image)(resources.GetObject("btnClose.Activepic")));
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackImage = null;
            this.btnClose.ColorBack = System.Drawing.Color.Empty;
            this.btnClose.Location = new System.Drawing.Point(505, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Presspic = ((System.Drawing.Image)(resources.GetObject("btnClose.Presspic")));
            this.btnClose.ShowText = null;
            this.btnClose.Size = new System.Drawing.Size(36, 36);
            this.btnClose.Staticpic = ((System.Drawing.Image)(resources.GetObject("btnClose.Staticpic")));
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
            // ImgForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(548, 345);
            this.CloseBoxSize = new System.Drawing.Size(36, 36);
            this.CloseDownBack = ((System.Drawing.Image)(resources.GetObject("$this.CloseDownBack")));
            this.CloseMouseBack = ((System.Drawing.Image)(resources.GetObject("$this.CloseMouseBack")));
            this.CloseNormlBack = ((System.Drawing.Image)(resources.GetObject("$this.CloseNormlBack")));
            this.Controls.Add(this.btnClose);
            this.Name = "ImgForm";
            this.Text = "";
            this.Shown += new System.EventHandler(this.ImgForm_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ImgForm_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ImageButton btnClose;
    }
}