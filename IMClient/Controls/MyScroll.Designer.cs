namespace IMClient.Controls
{
    partial class MyScroll
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlOut = new System.Windows.Forms.Panel();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.ScrollDt = new CustomControls.CustomScrollbar();
            this.pnlOut.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlOut
            // 
            this.pnlOut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlOut.Controls.Add(this.pnlContent);
            this.pnlOut.Location = new System.Drawing.Point(3, 3);
            this.pnlOut.Name = "pnlOut";
            this.pnlOut.Size = new System.Drawing.Size(294, 247);
            this.pnlOut.TabIndex = 0;
            // 
            // pnlContent
            // 
            this.pnlContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlContent.AutoScroll = true;
            this.pnlContent.Location = new System.Drawing.Point(0, 0);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(309, 247);
            this.pnlContent.TabIndex = 1;
            // 
            // ScrollDt
            // 
            this.ScrollDt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ScrollDt.ChannelColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.ScrollDt.DownArrowImage = global::IMClient.Properties.Resources.scroll_bar_down_03;
            this.ScrollDt.LargeChange = 10;
            this.ScrollDt.Location = new System.Drawing.Point(297, 3);
            this.ScrollDt.Maximum = 100;
            this.ScrollDt.Minimum = 0;
            this.ScrollDt.MinimumSize = new System.Drawing.Size(15, 92);
            this.ScrollDt.Name = "ScrollDt";
            this.ScrollDt.Size = new System.Drawing.Size(15, 247);
            this.ScrollDt.SmallChange = 1;
            this.ScrollDt.TabIndex = 4;
            this.ScrollDt.ThumbBottomImage = global::IMClient.Properties.Resources.scroll_bar02_down_03;
            this.ScrollDt.ThumbBottomSpanImage = global::IMClient.Properties.Resources.scroll_bar02_middle_03;
            this.ScrollDt.ThumbMiddleImage = global::IMClient.Properties.Resources.scroll_bar02_middle_03;
            this.ScrollDt.ThumbTopImage = global::IMClient.Properties.Resources.scroll_bar02_up_03;
            this.ScrollDt.ThumbTopSpanImage = global::IMClient.Properties.Resources.scroll_bar02_middle_03;
            this.ScrollDt.UpArrowImage = global::IMClient.Properties.Resources.scroll_bar_up_03;
            this.ScrollDt.Value = 0;
            this.ScrollDt.Scroll += new System.EventHandler(this.ScrollDt_Scroll);
            // 
            // MyScroll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ScrollDt);
            this.Controls.Add(this.pnlOut);
            this.Name = "MyScroll";
            this.Size = new System.Drawing.Size(315, 253);
            this.pnlOut.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlOut;
        private System.Windows.Forms.Panel pnlContent;
        private CustomControls.CustomScrollbar ScrollDt;
    }
}
