namespace IMClient.XMPP
{
    partial class PaintForm2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaintForm2));
            this.hwb = new OMCS.Tools.HandwritingPanel();
            this.comboBox_brushWidth = new CCWin.SkinControl.SkinComboBox();
            this.button_color = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnEnter = new IMClient.Controls.ImageButton();
            this.btnClose = new IMClient.Controls.ImageButton();
            this.btnClear = new IMClient.Controls.ImageButton();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnCloseIcon = new IMClient.Controls.ImageButton();
            this.SuspendLayout();
            // 
            // hwb
            // 
            this.hwb.BackColor = System.Drawing.Color.White;
            this.hwb.Location = new System.Drawing.Point(12, 84);
            this.hwb.Name = "hwb";
            this.hwb.Size = new System.Drawing.Size(419, 301);
            this.hwb.TabIndex = 1;
            // 
            // comboBox_brushWidth
            // 
            this.comboBox_brushWidth.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.comboBox_brushWidth.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(178)))), ((int)(((byte)(178)))));
            this.comboBox_brushWidth.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(178)))), ((int)(((byte)(178)))));
            this.comboBox_brushWidth.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBox_brushWidth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_brushWidth.FormattingEnabled = true;
            this.comboBox_brushWidth.ItemBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(178)))), ((int)(((byte)(178)))));
            this.comboBox_brushWidth.Location = new System.Drawing.Point(76, 42);
            this.comboBox_brushWidth.Name = "comboBox_brushWidth";
            this.comboBox_brushWidth.Size = new System.Drawing.Size(67, 22);
            this.comboBox_brushWidth.TabIndex = 130;
            this.comboBox_brushWidth.WaterText = "";
            this.comboBox_brushWidth.SelectedIndexChanged += new System.EventHandler(this.comboBox_brushWidth_SelectedIndexChanged);
            // 
            // button_color
            // 
            this.button_color.BackColor = System.Drawing.Color.Transparent;
            this.button_color.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_color.BackgroundImage")));
            this.button_color.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button_color.Location = new System.Drawing.Point(162, 39);
            this.button_color.Name = "button_color";
            this.button_color.Size = new System.Drawing.Size(25, 25);
            this.button_color.TabIndex = 131;
            this.button_color.UseVisualStyleBackColor = false;
            this.button_color.Click += new System.EventHandler(this.button_color_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(24, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 23);
            this.label1.TabIndex = 129;
            this.label1.Text = "画笔：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnEnter
            // 
            this.btnEnter.Activepic = global::IMClient.Properties.Resources.complete_tablets_over_07;
            this.btnEnter.BackImage = null;
            this.btnEnter.ColorBack = System.Drawing.Color.Transparent;
            this.btnEnter.Location = new System.Drawing.Point(379, 409);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Presspic = global::IMClient.Properties.Resources.complete_tablets_over_07;
            this.btnEnter.ShowText = null;
            this.btnEnter.Size = new System.Drawing.Size(52, 25);
            this.btnEnter.Staticpic = global::IMClient.Properties.Resources.complete_tablets_normal_07;
            this.btnEnter.Stretch = false;
            this.btnEnter.SupportToggle = false;
            this.btnEnter.TabIndex = 135;
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
            // btnClose
            // 
            this.btnClose.Activepic = global::IMClient.Properties.Resources.cancel_tablets_over_07;
            this.btnClose.BackImage = null;
            this.btnClose.ColorBack = System.Drawing.Color.Transparent;
            this.btnClose.Location = new System.Drawing.Point(291, 409);
            this.btnClose.Name = "btnClose";
            this.btnClose.Presspic = global::IMClient.Properties.Resources.cancel_tablets_over_07;
            this.btnClose.ShowText = null;
            this.btnClose.Size = new System.Drawing.Size(52, 25);
            this.btnClose.Staticpic = global::IMClient.Properties.Resources.cancel_tablets_normal_07;
            this.btnClose.Stretch = false;
            this.btnClose.SupportToggle = false;
            this.btnClose.TabIndex = 136;
            this.btnClose.Text = "imageButton2";
            this.btnClose.Toggle = false;
            this.btnClose.ToolTipOpacity = 1D;
            this.btnClose.ToolTipShown = false;
            this.btnClose.TooltipX = 0;
            this.btnClose.TooltipY = 0;
            this.btnClose.Unablepic = null;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnClear
            // 
            this.btnClear.Activepic = global::IMClient.Properties.Resources.clear_over_03;
            this.btnClear.BackImage = null;
            this.btnClear.ColorBack = System.Drawing.Color.Transparent;
            this.btnClear.Location = new System.Drawing.Point(205, 43);
            this.btnClear.Name = "btnClear";
            this.btnClear.Presspic = global::IMClient.Properties.Resources.cancel_tablets_over_07;
            this.btnClear.ShowText = null;
            this.btnClear.Size = new System.Drawing.Size(52, 22);
            this.btnClear.Staticpic = global::IMClient.Properties.Resources.clear_normal_03;
            this.btnClear.Stretch = false;
            this.btnClear.SupportToggle = false;
            this.btnClear.TabIndex = 137;
            this.btnClear.Text = "imageButton2";
            this.btnClear.Toggle = false;
            this.btnClear.ToolTipOpacity = 1D;
            this.btnClear.ToolTipShown = false;
            this.btnClear.TooltipX = 0;
            this.btnClear.TooltipY = 0;
            this.btnClear.Unablepic = null;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnCloseIcon
            // 
            this.btnCloseIcon.Activepic = global::IMClient.Properties.Resources.tablets_close_over_03;
            this.btnCloseIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCloseIcon.BackImage = null;
            this.btnCloseIcon.ColorBack = System.Drawing.Color.Transparent;
            this.btnCloseIcon.Location = new System.Drawing.Point(401, 8);
            this.btnCloseIcon.Name = "btnCloseIcon";
            this.btnCloseIcon.Presspic = global::IMClient.Properties.Resources.tablets_close_over_03;
            this.btnCloseIcon.ShowText = null;
            this.btnCloseIcon.Size = new System.Drawing.Size(30, 18);
            this.btnCloseIcon.Staticpic = global::IMClient.Properties.Resources.tablets_close_normal_03;
            this.btnCloseIcon.Stretch = false;
            this.btnCloseIcon.SupportToggle = false;
            this.btnCloseIcon.TabIndex = 138;
            this.btnCloseIcon.Text = "imageButton1";
            this.btnCloseIcon.Toggle = false;
            this.btnCloseIcon.ToolTipOpacity = 1D;
            this.btnCloseIcon.ToolTipShown = false;
            this.btnCloseIcon.TooltipX = 0;
            this.btnCloseIcon.TooltipY = 0;
            this.btnCloseIcon.Unablepic = null;
            this.btnCloseIcon.UseVisualStyleBackColor = true;
            this.btnCloseIcon.Click += new System.EventHandler(this.btnCloseIcon_Click);
            // 
            // PaintForm2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(443, 450);
            this.Controls.Add(this.btnCloseIcon);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnEnter);
            this.Controls.Add(this.comboBox_brushWidth);
            this.Controls.Add(this.button_color);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.hwb);
            this.Name = "PaintForm2";
            this.Text = "";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintForm2_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private OMCS.Tools.HandwritingPanel hwb;
        private CCWin.SkinControl.SkinComboBox comboBox_brushWidth;
        private System.Windows.Forms.Button button_color;
        private System.Windows.Forms.Label label1;
        private Controls.ImageButton btnEnter;
        private Controls.ImageButton btnClose;
        private Controls.ImageButton btnClear;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ToolTip toolTip1;
        private Controls.ImageButton btnCloseIcon;
    }
}