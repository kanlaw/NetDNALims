using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using IMClient.Controls.Tools;
using CCWin.SkinControl;
using IMClient.Controls.Base;

namespace NetDLims.Forms
{
    public partial class LoginForm : Form//FrameBase
    {

        private const int CS_DropSHADOW = 0x20000;
        private const int GCL_STYLE = (-26);
 
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetClassLong(IntPtr hwnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassLong(IntPtr hwnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int IParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        public LoginForm()
        {
            InitializeComponent();
        }



        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = Graphics.FromHwnd(base.Handle);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.AntiAlias;
        }

        public delegate void LoginCloseClickHandler();
        public event LoginCloseClickHandler loginCloseClick;
        //登陆form的关闭
        private void loginCloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            if (loginCloseClick != null)
            {
                loginCloseClick();
            }
        }


        public delegate void RegisterBtnClickHandler();
        public event RegisterBtnClickHandler registerBtnClick;
        //注册账号
        private void button1_Click(object sender, EventArgs e)
        {
            if (registerBtnClick != null)
            {
                registerBtnClick();
            }
        }

        public delegate void FindPassWordBtnClickHandler();
        public event FindPassWordBtnClickHandler findPassWordBtnClick;
        //找回密码
        private void button2_Click(object sender, EventArgs e)
        {
            if(findPassWordBtnClick != null)
            {
                findPassWordBtnClick(); 
            }
        }

        public delegate void UserLoginClickHandler(string userName,string passWord);
        public event UserLoginClickHandler userLoginClick;
        //用户登录
        private void imageButton_Login_Click(object sender, EventArgs e)
        {
            if (comboBox_UserName.Text == "" || textBox_PassWord.Text == "")
            {
                FrmMsg.Show(MsgKind.ok, "提示", "账号和密码不能为空!");
                return;
            }
            if (userLoginClick != null)
            {

                userLoginClick(comboBox_UserName.Text,textBox_PassWord.Text);
            }
        }

        public delegate void UserPKIClickHandler();
        public event UserPKIClickHandler userPKIClick;
        //PKI
        private void imageButton_PKI_Click(object sender, EventArgs e)
        {
            if (userPKIClick != null)
            {
                userPKIClick();
            }
        }



        /// <summary>
        /// 窗体加载实现阴影
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginForm_Load(object sender, EventArgs e)
        {
            SetClassLong(this.Handle, GCL_STYLE, GetClassLong(this.Handle, GCL_STYLE) | CS_DropSHADOW);
        }

        /// <summary>
        /// 拖动窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelLoginHead_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        //TODO:密码输入框的Enter事件
        private void textBox_PassWord_KeyDown(object sender, KeyEventArgs e)
        {
            //if(e.KeyCode == Keys.Enter)
            //{
            //    if (userLoginClick != null)
            //    {
            //        userLoginClick(comboBox_UserName.Text, textBox_PassWord.Text);
            //    }
            //}
        }

       
    }
}
