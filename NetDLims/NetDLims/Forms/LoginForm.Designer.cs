namespace NetDLims.Forms
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.panelLoginHead = new System.Windows.Forms.Panel();
            this.loginCloseBtn = new NetDLims.Controls.ImageButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox_PassWord = new NetDLims.Controls.ImageTextBox();
            this.comboBox_UserName = new NetDLims.Controls.ImageComboBox();
            this.checkBox_RPW = new System.Windows.Forms.CheckBox();
            this.imageButton_PKI = new NetDLims.Controls.ImageButton();
            this.imageButton_Login = new NetDLims.Controls.ImageButton();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label_PassWord = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panelLoginHead.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelLoginHead
            // 
            this.panelLoginHead.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelLoginHead.BackgroundImage = global::NetDLims.Properties.Resources.enter_bg2_03;
            this.panelLoginHead.Controls.Add(this.loginCloseBtn);
            this.panelLoginHead.Location = new System.Drawing.Point(0, 0);
            this.panelLoginHead.Name = "panelLoginHead";
            this.panelLoginHead.Size = new System.Drawing.Size(468, 140);
            this.panelLoginHead.TabIndex = 0;
            this.panelLoginHead.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelLoginHead_MouseDown);
            // 
            // loginCloseBtn
            // 
            this.loginCloseBtn.Activepic = global::NetDLims.Properties.Resources.close_over_02;
            this.loginCloseBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.loginCloseBtn.BackImage = null;
            this.loginCloseBtn.ColorBack = System.Drawing.Color.Empty;
            this.loginCloseBtn.Location = new System.Drawing.Point(436, 3);
            this.loginCloseBtn.Name = "loginCloseBtn";
            this.loginCloseBtn.Presspic = global::NetDLims.Properties.Resources.close_down_02;
            this.loginCloseBtn.ShowText = null;
            this.loginCloseBtn.Size = new System.Drawing.Size(29, 24);
            this.loginCloseBtn.Staticpic = global::NetDLims.Properties.Resources.close_normal_02;
            this.loginCloseBtn.Stretch = false;
            this.loginCloseBtn.SupportToggle = false;
            this.loginCloseBtn.TabIndex = 0;
            this.loginCloseBtn.Text = "imageButton1";
            this.loginCloseBtn.Toggle = false;
            this.loginCloseBtn.ToolTipOpacity = 1D;
            this.loginCloseBtn.ToolTipShown = false;
            this.loginCloseBtn.TooltipX = 0;
            this.loginCloseBtn.TooltipY = 0;
            this.loginCloseBtn.Unablepic = null;
            this.loginCloseBtn.UseVisualStyleBackColor = true;
            this.loginCloseBtn.Click += new System.EventHandler(this.loginCloseBtn_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoSize = true;
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.textBox_PassWord);
            this.panel1.Controls.Add(this.comboBox_UserName);
            this.panel1.Controls.Add(this.checkBox_RPW);
            this.panel1.Controls.Add(this.imageButton_PKI);
            this.panel1.Controls.Add(this.imageButton_Login);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label_PassWord);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 139);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(468, 221);
            this.panel1.TabIndex = 1;
            // 
            // textBox_PassWord
            // 
            this.textBox_PassWord.Bordercolor = System.Drawing.Color.DarkGray;
            this.textBox_PassWord.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_PassWord.Controllist = ((System.Collections.ArrayList)(resources.GetObject("textBox_PassWord.Controllist")));
            this.textBox_PassWord.EmptyTextTip = null;
            this.textBox_PassWord.Font = new System.Drawing.Font("微软雅黑", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_PassWord.Location = new System.Drawing.Point(110, 70);
            this.textBox_PassWord.Name = "textBox_PassWord";
            this.textBox_PassWord.PasswordChar = '●';
            this.textBox_PassWord.Size = new System.Drawing.Size(238, 30);
            this.textBox_PassWord.TabIndex = 11;
            this.textBox_PassWord.TStatus = NetDLims.Controls.TextStatus.ALL;
            this.textBox_PassWord.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_PassWord_KeyDown);
            // 
            // comboBox_UserName
            // 
            this.comboBox_UserName.ArrowColor = System.Drawing.Color.DarkGray;
            this.comboBox_UserName.BaseColor = System.Drawing.Color.White;
            this.comboBox_UserName.BorderColor = System.Drawing.Color.DarkGray;
            this.comboBox_UserName.EnableColor = System.Drawing.Color.White;
            this.comboBox_UserName.Font = new System.Drawing.Font("微软雅黑", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox_UserName.FormattingEnabled = true;
            this.comboBox_UserName.Location = new System.Drawing.Point(110, 41);
            this.comboBox_UserName.Name = "comboBox_UserName";
            this.comboBox_UserName.Size = new System.Drawing.Size(238, 31);
            this.comboBox_UserName.TabIndex = 10;
            // 
            // checkBox_RPW
            // 
            this.checkBox_RPW.AutoSize = true;
            this.checkBox_RPW.BackColor = System.Drawing.Color.Transparent;
            this.checkBox_RPW.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBox_RPW.Location = new System.Drawing.Point(110, 165);
            this.checkBox_RPW.Name = "checkBox_RPW";
            this.checkBox_RPW.Size = new System.Drawing.Size(69, 16);
            this.checkBox_RPW.TabIndex = 9;
            this.checkBox_RPW.Text = "记住密码";
            this.checkBox_RPW.UseVisualStyleBackColor = false;
            // 
            // imageButton_PKI
            // 
            this.imageButton_PKI.Activepic = global::NetDLims.Properties.Resources.pki_button1_over_03;
            this.imageButton_PKI.BackImage = null;
            this.imageButton_PKI.ColorBack = System.Drawing.Color.Empty;
            this.imageButton_PKI.Location = new System.Drawing.Point(234, 117);
            this.imageButton_PKI.Name = "imageButton_PKI";
            this.imageButton_PKI.Presspic = global::NetDLims.Properties.Resources.pki_button1_over_03;
            this.imageButton_PKI.ShowText = null;
            this.imageButton_PKI.Size = new System.Drawing.Size(114, 32);
            this.imageButton_PKI.Staticpic = global::NetDLims.Properties.Resources.pki_button1_normal_03;
            this.imageButton_PKI.Stretch = false;
            this.imageButton_PKI.SupportToggle = false;
            this.imageButton_PKI.TabIndex = 7;
            this.imageButton_PKI.Text = "imageButton2";
            this.imageButton_PKI.Toggle = false;
            this.imageButton_PKI.ToolTipOpacity = 1D;
            this.imageButton_PKI.ToolTipShown = false;
            this.imageButton_PKI.TooltipX = 0;
            this.imageButton_PKI.TooltipY = 0;
            this.imageButton_PKI.Unablepic = null;
            this.imageButton_PKI.UseVisualStyleBackColor = true;
            this.imageButton_PKI.Click += new System.EventHandler(this.imageButton_PKI_Click);
            // 
            // imageButton_Login
            // 
            this.imageButton_Login.Activepic = global::NetDLims.Properties.Resources.enter_button1_over_03;
            this.imageButton_Login.BackImage = null;
            this.imageButton_Login.ColorBack = System.Drawing.Color.Empty;
            this.imageButton_Login.Location = new System.Drawing.Point(110, 117);
            this.imageButton_Login.Name = "imageButton_Login";
            this.imageButton_Login.Presspic = global::NetDLims.Properties.Resources.enter_button1_over_03;
            this.imageButton_Login.ShowText = null;
            this.imageButton_Login.Size = new System.Drawing.Size(114, 32);
            this.imageButton_Login.Staticpic = global::NetDLims.Properties.Resources.enter_button1_normal_03;
            this.imageButton_Login.Stretch = false;
            this.imageButton_Login.SupportToggle = false;
            this.imageButton_Login.TabIndex = 6;
            this.imageButton_Login.Text = "imageButton1";
            this.imageButton_Login.Toggle = false;
            this.imageButton_Login.ToolTipOpacity = 1D;
            this.imageButton_Login.ToolTipShown = false;
            this.imageButton_Login.TooltipX = 0;
            this.imageButton_Login.TooltipY = 0;
            this.imageButton_Login.Unablepic = null;
            this.imageButton_Login.UseVisualStyleBackColor = true;
            this.imageButton_Login.Click += new System.EventHandler(this.imageButton_Login_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("宋体", 9F);
            this.button2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.button2.Location = new System.Drawing.Point(351, 75);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(66, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "找回密码";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("宋体", 9F);
            this.button1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.button1.Location = new System.Drawing.Point(351, 45);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(66, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "注册账号";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label_PassWord
            // 
            this.label_PassWord.Font = new System.Drawing.Font("宋体", 9F);
            this.label_PassWord.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label_PassWord.Location = new System.Drawing.Point(74, 76);
            this.label_PassWord.Name = "label_PassWord";
            this.label_PassWord.Size = new System.Drawing.Size(45, 20);
            this.label_PassWord.TabIndex = 2;
            this.label_PassWord.Text = "密码:";
            this.label_PassWord.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 9F);
            this.label1.Location = new System.Drawing.Point(74, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "账号:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LoginForm
            // 
            this.AcceptButton = this.imageButton_Login;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(468, 360);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelLoginHead);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.panelLoginHead.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelLoginHead;
        private Controls.ImageButton loginCloseBtn;
        private System.Windows.Forms.Panel panel1;
        private Controls.ImageButton imageButton_PKI;
        private Controls.ImageButton imageButton_Login;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label_PassWord;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox_RPW;
        private Controls.ImageComboBox comboBox_UserName;
        private Controls.ImageTextBox textBox_PassWord;
    }
}