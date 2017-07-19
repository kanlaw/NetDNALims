using CefSharp.WinForms;
using CefSharp.WinForms.Example.Minimal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CefSharp;
using CefSharp.Handler;

using NetDLims.Controls;
using NetDLims.Properties;
using System.Diagnostics;
using System.Runtime.InteropServices;

using System.IO;
using System.IO.Pipes;
using System.Threading;
using System.Security.Principal;
using IMClient.Controls.Tools;

using NetDLims.Services;
using DNANET.Data;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using IMClient.Logic;
using IMClient.Controls.Base;
using IMClient.XMPP.Forms;
using IMClient.Logic.Sql;

namespace NetDLims.Forms
{
    public partial class BaseTemplateForm : FrameBase
    {

        Common comm = new Common();

        //邮箱帐号和密码
        string mailAccount;
        string mailPassWord;

        //用户真实姓名
        string userTrueName;

        //声明一个登录窗口
        LoginForm loginForm;

        //声明一个表示用户已登录的变量
        bool isLogin;

        //声明IMClient的好友列表窗口
        IMClient.IMForm imForm;

        //声明一个加载网页的WebBrowser
        private ChromiumWebBrowser browser;

        //private UserInformation userInformation;

        TextureBrush BackGround = null;
        TextureBrush BackGroundFill = null;

        public BaseTemplateForm()
        {
            BackGround = new TextureBrush(Resources.title_block_01);
            BackGroundFill = new TextureBrush(Resources.line_blue_02);
            InitializeComponent();
            SysParams.InitParams();
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint |
            //ControlStyles.OptimizedDoubleBuffer |
            //ControlStyles.UserPaint, true);


            //this.SetStyle(
            //ControlStyles.SupportsTransparentBackColor |
            //ControlStyles.ResizeRedraw, true);
            SetBtnLocation();
            chkLocalDBDelegate cdb = chkLocalDB;
            cdb.BeginInvoke(null, null);


        }

        #region 检查本地库
        /// <summary>
        /// 检查本地库是否存在 并生成 相关本地库
        /// </summary>
        public delegate void chkLocalDBDelegate();


        public void chkLocalDB()
        {
            try
            {
                SqlLiteHelper s = new SqlLiteHelper();
                s.createNewDatabase();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion;

        #region 测试函数
        //        public void showText(string textcontent)
        //        {
        //#if DEBUG
        //            MessageBox.Show(textcontent);
        //            MessageBox.Show("ceshi");
        //#endif
        //        }
        #endregion




        private void BaseTemplateForm_Load(object sender, EventArgs e)
        {
            //创建web
            BrowerInit bi = CreateBrowser;
            bi.BeginInvoke(null, null);

            //显示聊天列表的btn
            ibtnLK.Visible = false;


        }

        //TODO:webUrl的IP
        string IPStr = SysParams.AppServer;
        //string s=IMClient.Controls.StaticClass.IPStr;


        /// <summary>
        /// 浏览器异步加载
        /// </summary>
        public delegate void BrowerInit();

        //TODO:加载webBrowser
        private void CreateBrowser()
        {

            //browser = new ChromiumWebBrowser(string.Format(@"file://C:\Projects\test2.html"))
            browser = new ChromiumWebBrowser(string.Format(SysParams.Page_FirstPage, IPStr))
            {
                Dock = DockStyle.Fill,

            };

            browser.FrameLoadStart += Browser_FrameLoadStart;
            //browser.FrameLoadEnd += Browser_FrameLoadEnd;

            this.Invoke(new Action(() =>
            {
                panelWeb.Controls.Add(browser);

                this.comm.webIsLoginUser += Comm_webIsLoginUser;
                this.comm.browser = this.browser;
                this.comm.browser.JsDialogHandler = this.comm;
                this.comm.browser.DownloadHandler = new CefDownloadHandler() ;
                this.comm.RegObjectTOCEF(this.browser, this.comm, "jsOBJ");


                //RegObjectTOCEF();
            }));

        }

        private bool isWebLoginUser = false;
        private void Comm_webIsLoginUser()
        {
            //MessageBox.Show("用户登录");
            isWebLoginUser = true;
            creatLoginForm();
        }

        void Browser_FrameLoadStart(object sender, CefSharp.FrameLoadStartEventArgs e)
        {
            string tmpVal = "^" + SysParams.Server.Replace(".", "\\.");//.Replace(":", "\\:");
            // @"^http://192\.168\.1\.106"
            //检查是否是应用服务器
            if (Regex.IsMatch(e.Url, tmpVal))
            {
            }
        }


        //TODO:加载IMClient
        private void LoadIMClient(bool isLogin)
        {
            createIMForm();
            if (isLogin)
            {
                loadIMForm();
            }

            //appContainerIM.AppFilename = Application.StartupPath + @"\IMClient.exe";
            //appContainerIM.Start();

        }

        AppDomain adIM = AppDomain.CreateDomain("IM");

        //TODO:创建IMForm
        private void createIMForm()
        {
            if (imForm == null)
            {
                imForm = new IMClient.IMForm();
                imForm.ibtn_SJ += new IMClient.IMForm.ibtnSJ(btn_indent);
                imForm.isLoginSuccessBlock += ImForm_isLoginSuccessBlock;
                imForm.personPlatfromClick += ImForm_personPlatfromClick;
                imForm.personEmailClick += ImForm_personEmailClick;
                //Thread t = new Thread(this.IMProc);
                //t.SetApartmentState(ApartmentState.STA);
                //t.Start();

                //Thread.Sleep(500);

                //this.imForm = (IMClient.IMForm)this.adIM.GetData("IMForm");
                //this.imForm.ibtn_SJ += btn_indent;
                //this.imForm.isLoginSuccessBlock += ImForm_isLoginSuccessBlock;
            }
        }

        //点击个人邮箱
        private void ImForm_personEmailClick()
        {
            initImage();
            this.comm.loadUserMail(mailAccount, mailPassWord);
        }

        //点击个人平台回调
        private void ImForm_personPlatfromClick(string personUid)
        {
            initImage();
            //LoadUrl(string.Format("http://{0}/Front_Web/personal1.html?UID={1}", IPStr,personUid));
            LoadUrl(string.Format(SysParams.Page_PersonalInfo, IPStr, personUid));
        }

        void IMProc()
        {
            int t = this.adIM.ExecuteAssembly(SysParams.Chat_Name);
        }

        private void ImForm_isLoginSuccessBlock(bool isLoginSucc, string userName, byte[] userPhoto)
        {
            if (isLoginSucc)
            {

                //如果登录成功
                isLogin = true;
                comm.userIsLogin = true;
                pleaseLoginBtn.Visible = false;
                panel_Logined.Visible = true;
                label_UserName.Text = userTrueName;


                pictureBox_Grade.Image = Resources.level_03;

                panel_Logined.Invalidate();
                ////给当前用户头像赋值
                //string headImagePath = TempFile.retImgHeadByUserName_Localhost(userNameIM);
                ////userPhotoBtn.Image =

                //userPhotoBtn.Activepic = userPhotoBtn.Presspic=userPhotoBtn.Staticpic = Image.FromFile(headImagePath);
                //userPhotoBtn.Stretch = true;


                //登录成功后加载显示IMForm
                loadIMForm();

                showIMForm();
            }
#if DEBUG
            MessageBox.Show("测试IMForm回调");
#endif
        }

        #region 异步加载登录人本人头像
        public delegate void GetUserSelfHeadDelegate(string userNameIM, string imgUrl);

        public void GetUserSelfHead(string userNameIM, string imgUrl)
        {
            byte[] headData = null;
            bool result = TempFile.Download_Head(imgUrl, userNameIM, ref headData);
        }

        public void Complete(IAsyncResult iar)
        {
            try
            {
                string headImagePath = TempFile.retImgHeadByUserName_2(userNameIM);
                Image ImageHead_tmp = SysParams.defaultHead;
                if (!string.IsNullOrEmpty(headImagePath))
                {
                    ImageHead_tmp = Image.FromFile(headImagePath);
                }
                SysParams.loginUserImage = userPhotoBtn.Activepic = userPhotoBtn.Presspic = userPhotoBtn.Staticpic = ImageHead_tmp;
                userPhotoBtn.Stretch = true;
                userPhotoBtn.Invalidate();
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        //TODO:加载IMForm
        void loadIMForm()
        {
            if (imForm != null)
            {
                imForm.TopLevel = false;

                panelIM.Controls.Add(imForm);
                imForm.Dock = DockStyle.Fill;
                imForm.Show();
            }

        }

        /// <summary>
        /// IM缩进按钮
        /// </summary>
        private void btn_indent()
        {
            //panelIM隐藏
            hidePanelIM(true);

        }

        //TODO:panelIM隐藏
        private void hidePanelIM(bool isUserLogin)
        {
            if (isUserLogin)
            {
                panelIM.Width = 0;
                panelWeb.Width = this.Width;
                ibtnLK.Visible = true;
                imForm.ibtnSF.Visible = false;
            }
            else
            {
                panelIM.Width = 0;
                panelWeb.Width = this.Width;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            int h = BackGround.Image.Height;

            int wl = BackGround.Image.Width;
            int wr = this.ClientSize.Width - wl;

            e.Graphics.FillRectangle(BackGround, 0, 0, wl, h);
            e.Graphics.TranslateTransform(wl, 0);
            e.Graphics.FillRectangle(BackGroundFill, 0, 0, wr, h);
            e.Graphics.TranslateTransform(wr, 0);
            e.Graphics.ResetTransform();
            Pen p = new Pen(FrmStyle.BorderColor);
            e.Graphics.DrawLine(p, new Point(this.Width - 1, this.panelWeb.Location.Y - 2),
                new Point(this.Width - 1, this.Height));
            e.Graphics.DrawLine(p, new Point(0, this.Height - 1),
              new Point(this.Width - 1, this.Height - 1));
            e.Graphics.DrawLine(p, new Point(0, this.panelWeb.Location.Y - 2),
                 new Point(0, this.Height));




            //CssStyle style = StyleManager.Current["Frame"];
            //if (style.Border != null) e.Graphics.DrawRectangle(style.Border, 0, h, this.Width - 1, this.Height - h - 1);
        }

        private void personPlatformLoseFocus()
        {
            if (imForm != null)
            {
                if (imForm.PersonPlatformIsClick == true)
                {
                    imForm.PersonPlatformIsClick = false;
                }
            }

            isSZXTClick = false;
        }


        /// <summary>
        /// 技术指导  http://192.168.1.90:8020/DNAWeb/guidance.html 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageButton3_Click(object sender, EventArgs e)
        {
            initImage();
            this.btnJSZD.Staticpic = this.btnJSZD.Presspic;
            personPlatformLoseFocus();
            comm.CallJS(this.browser, "showIndex('jszn')");

            LoadUrl(string.Format(SysParams.Page_TechnicalGuidance, IPStr));
        }


        //TODO:初始化导航栏各模块的背景图片
        private void initImage()
        {
            this.btnMain.Staticpic = Resources.home_page_normal_02;
            this.btnJSZN.Staticpic = Resources.Construction_guide_normal_02;
            this.btnJSZD.Staticpic = Resources.Technical_guidance_normal_02;
            this.btnSZXT.Staticpic = Resources.Actual_combat_coordination_normal_02;
            this.btnXXJL.Staticpic = Resources.Study_communication_normal_02;
            this.btnKJCX.Staticpic = Resources.Science_technology_innovation_normal_02;
            this.btnXTGL.Staticpic = Resources.System_management_normal_02;
            this.Refresh();
        }

        //TODO:panelWeb加载URL
        private void LoadUrl(string url)
        {
            if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
            {
                if (browser != null)
                {
                    browser.Load(url);
                }

            }
        }

        /// <summary>
        /// 通过CEF从窗口去调用当前页面中的JS方法
        /// </summary>
        /// <param name="jsname"></param>
        //public void CallJS(string jsname)
        //{
        //    CefSharp.IFrame mainFrame = browser.GetBrowser().MainFrame;
        //    mainFrame.ExecuteJavaScriptAsync(jsname);
        //}

        /// <summary>
        /// 把需要通过页面反向控制窗口的对象传给CEF
        /// </summary>
        /// <param name="jsObj"></param>
        //public void RegObjectTOCEF()
        //{

        //    browser.RegisterJsObject("jsOBJ", this,false);

        //}

        /// <summary>
        /// 首页 http://192.168.1.90:8020/DNAWeb/index.html
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMain_Click(object sender, EventArgs e)
        {
            initImage();
            this.btnMain.Staticpic = this.btnMain.Presspic;
            personPlatformLoseFocus();
            comm.CallJS(this.browser, "showIndex('index')");
            LoadUrl(string.Format(SysParams.Page_FirstPage, IPStr));
          
            //LoadUrl("http://flashhq.gtja.com");
        }

        /// <summary>
        /// 建设指南 http://192.168.1.90:8020/DNAWeb/macro.html
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnJSZN_Click(object sender, EventArgs e)
        {
            initImage();
            this.btnJSZN.Staticpic = this.btnJSZN.Presspic;
            personPlatformLoseFocus();
            comm.CallJS(this.browser, "showIndex('jszn')");
            LoadUrl(string.Format(SysParams.Page_ConstructionGuide, IPStr));
            //browser.RegisterJsObject("obg", new JsEvent(), false);
        }


        bool isSZXTClick;
        /// <summary>
        /// 实战协同  http://192.168.1.90:8020/DNAWeb/combat.html 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSZXT_Click(object sender, EventArgs e)
        {
            
            initImage();
            this.btnSZXT.Staticpic = this.btnSZXT.Presspic;
            personPlatformLoseFocus();
            comm.CallJS(this.browser, "showIndex('jszn')");
            //LoadUrl(string.Format(SysParams.Page_ActualCooperation, IPStr) +
            // (!string.IsNullOrEmpty(this.userUid) ? "?a=1&b=1&uid=" + this.userUid : string.Empty));
            // LoadUrl(string.Format(SysParams.Page_ActualCooperation, IPStr));

            isSZXTClick = true;
            if (isLogin == true)
            {
                LoadUrl(string.Format(SysParams.Page_IsLoginActualCooperation,IPStr));
            }
            else {
                LoadUrl(string.Format(SysParams.Page_ActualCooperation, IPStr));
            }
        }

        /// <summary>
        /// 学习交流  http://192.168.1.90:8020/DNAWeb/learn.html 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnXXJL_Click(object sender, EventArgs e)
        {
            initImage();
            this.btnXXJL.Staticpic = this.btnXXJL.Presspic;
            personPlatformLoseFocus();
            comm.CallJS(this.browser, "showIndex('jszn')");
            LoadUrl(string.Format(SysParams.Page_Study, IPStr));
        }

        /// <summary>
        /// 科技创新 http://192.168.1.90:8020/DNAWeb/innovate1.html
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnKJCX_Click(object sender, EventArgs e)
        {
            initImage();
            this.btnKJCX.Staticpic = this.btnKJCX.Presspic;
            personPlatformLoseFocus();
            comm.CallJS(this.browser, "showIndex('jszn')");
            LoadUrl(string.Format(SysParams.Page_Science, IPStr));
        }



        /// <summary>
        /// 系统管理 http://192.168.1.90:8020/DNAWeb/setup.html
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnXTGL_Click(object sender, EventArgs e)
        {
            initImage();
            this.btnXTGL.Staticpic = this.btnXTGL.Presspic;
            personPlatformLoseFocus();
            comm.CallJS(this.browser, "showIndex('jszn')");
            LoadUrl(string.Format(SysParams.Page_SysManage, IPStr));
            //LoadUrl(string.Format("http://192.168.2.91"));
            //LoadUrl("http://192.168.2.130");
        }



        Point mouseOff;//鼠标移动位置变量
        bool leftFlag;//标签是否为左键
        /// <summary>
        /// 窗口标题栏拖动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseTemplateForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(mouseOff.X, mouseOff.Y);  //设置移动后的位置
                Location = mouseSet;
            }
        }

        /// <summary>
        /// 鼠标左键事件（为了记录鼠标是否左键按下）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseTemplateForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOff = new Point(-e.X, -e.Y); //得到变量的值
                leftFlag = true;                  //点击左键按下时标注为true;
            }
        }

        /// <summary>
        /// 鼠标左键释放事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseTemplateForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                leftFlag = false;//释放鼠标后标注为false;
            }
        }


        //点击用户头像
        private void userPhotoBtn_Click(object sender, EventArgs e)
        {
            //UserListForm ul = new UserListForm();
            //ul.ShowDialog();
            //MeetingListForm ml = new MeetingListForm();
            //ml.ShowDialog();
            //UserListForm ul = new UserListForm();
            //ul.ShowDialog();
            //CreateMeetingForm cmf = new CreateMeetingForm();
            //cmf.Show();
            //return;


            //MeetingForm mf = new MeetingForm();
            //mf.ShowDialog();
            //用户登录
            userLogin();

            //测试调用主窗口内当前页面的JS方法
            //comm.CallJS(this.browser, "change(0)");



        }

        public void userLogin()
        {
            if (isLogin == false)
            {
                creatLoginForm();
            }
            else
            {
                comm.CallJS(this.browser, "onsend()");

#if DEBUG
                //用户已登录，显示用户的基本信息
                MessageBox.Show("用户信息");
#endif
                initImage();
                personPlatformLoseFocus();
                LoadUrl(string.Format(SysParams.Page_PersonalInfo2, IPStr, userUid));
            }
        }

        private void pleaseLoginBtn_MouseEnter(object sender, EventArgs e)
        {
            pleaseLoginBtn.BackColor = System.Drawing.Color.WhiteSmoke;
        }

        private void pleaseLoginBtn_MouseLeave(object sender, EventArgs e)
        {
            pleaseLoginBtn.BackColor = System.Drawing.Color.Transparent;
        }

        //TODO:请登录账号btn的click
        private void pleaseLoginBtn_Click(object sender, EventArgs e)
        {
            creatLoginForm();
        }

        //TODO:创建一个登录窗口
        public void creatLoginForm()
        {
            if (loginForm == null)
            {
                try
                {
                    if (!isWebLoginUser)
                    {
                        pleaseLoginBtn.Enabled = false;
                        userPhotoBtn.Enabled = false;
                    }
                  
                    loginForm = new LoginForm();
                    loginForm.userLoginClick += LoginForm_userLoginClick;
                    loginForm.loginCloseClick += LoginForm_loginCloseClick;
                    loginForm.userPKIClick += LoginForm_userPKIClick;
                    loginForm.registerBtnClick += LoginForm_registerBtnClick;
                    loginForm.findPassWordBtnClick += LoginForm_findPassWordBtnClick;
                    //loginForm.GetTopLevel = true;
                    loginForm.BringToFront();
                    loginForm.ShowDialog();
                }
                catch (Exception ex)
                {


                }
                finally {

                    if (!isWebLoginUser)
                    {
                        pleaseLoginBtn.Enabled = true;
                        userPhotoBtn.Enabled = true;
                    }
                   
                }

            }
        }

        //TODO:loginForm's 找回密码的代理
        private void LoginForm_findPassWordBtnClick()
        {
            if (loginForm != null)
            {
                loginForm.Close();
                loginForm = null;
            }
        }

        //TODO:loginForm's 注册账号的代理
        private void LoginForm_registerBtnClick()
        {
            if (loginForm != null)
            {
                loginForm.Close();
                loginForm = null;
            }
            initImage();
            LoadUrl(string.Format(SysParams.Page_Register, IPStr));


        }

        Dictionary<string, string> serverDic;


        string userNameIM;
        string userUid;
        //TODO:登录窗口登录按钮的delegate
        private void LoginForm_userLoginClick(string userName, string passWord)
        {
            serverDic = new Dictionary<string, string>();

            string domain = SysParams.Domain;
            string server = SysParams.Server;

            //登录成功后加在IMClient


            LoadIMClient(false);

            //调用login服务
            string msg = null;
            int code = 1000;

            SysParams.LoginUser = userLoginServer(userName, passWord, ref msg, ref code);
            //UserServiceInformation tmp = SysParams.LoginUser.Services.ToList<UserServiceInformation>().Find(item => item.Service == "im");
            //SysParams.UserJID = tmp.Data.ToString(); 
           

            //code=0,表示登录成功；code=-1，表示账户不存在；code=-2，表示密码错误；code=-3，表示账户登录错误；
            if (code != 0)
            {
                FrmMsg.Show(MsgKind.ok, "提示", msg);
                return;
            }
            else
            {
                switch (SysParams.LoginUser.State)
                {
                    case 0:
                        break;

                    case 1:
                        FrmMsg.Show(MsgKind.ok, "提示", "登录失败，用户待审核中。");
                        return;

                    case 2:
                        FrmMsg.Show(MsgKind.ok, "提示", "登录失败，用户审核未通过。");
                        return;

                    default:
                        return;
                }

                if (loginForm != null)
                {
                    loginForm.Close();
                    loginForm = null;
                }
            }

            if (SysParams.LoginUser != null)
            {
                if (isSZXTClick)
                {
                    //用户登录成功后
                    LoadUrl(string.Format(SysParams.Page_IsLoginActualCooperation, IPStr));
                }
               

                UserServiceInformation[] serverList = SysParams.LoginUser.Services;
                if (serverList.Count() != 0)
                {
                    for (int i = 0; i < serverList.Count(); i++)
                    {
                        serverDic.Add(serverList[i].Service, serverList[i].Data.ToString());
                    }
                }
                SysParams.serversDic = serverDic;

                if (serverDic.ContainsKey("im"))
                {

                    userNameIM = serverDic["im"].Substring(0, serverDic["im"].IndexOf("@"));
                    //判断用户是否登录成功
                    //imForm.Invoke(new Action<string, string, string, string>(imForm.adjustUserIsLoginSuccess), "", "", "", "");
                    imForm.adjustUserIsLoginSuccess(userNameIM, passWord, domain, server);
                }
                if (serverDic.ContainsKey("mail"))
                {
                    mailAccount = serverDic["mail"];
                    mailPassWord = passWord;
                }

                userTrueName = SysParams.LoginUser.Name;
                imForm.UserUid = SysParams.LoginUser.UID.ToString();
                userUid = SysParams.LoginUser.UID.ToString();

                GetUserSelfHeadDelegate gsd = GetUserSelfHead;
                SysParams.LoginUser.Head = SysParams.Head_ImageInfo + SysParams.LoginUser.Head.ToString(); 
                gsd.BeginInvoke(userNameIM, SysParams.LoginUser.Head, this.Complete, null);

                if (Common.checkAuth(SysParams.LoginUser))
                {
                    this.btnXTGL.Visible = true;
                    SetBtnLocation();
                }

                //byte[] headData = null;
                //bool result = TempFile.Download_Head(@"http://192.168.1.93/DNALIMS/upload/148835977589691%E7%99%BB%E5%BD%9502.png", userNameIM, ref headData);

            }



            //imForm.adjustUserIsLoginSuccess(userName, passWord, domain, server);
        }

        //TODO:调用login服务
        private UserInformation userLoginServer(string userLogin,string passWord,ref string msg,ref int code)
        { 

            UserServiceClient c = new UserServiceClient();
            c.Open();
            UserInformation info = c.Login(userLogin, passWord, ref msg, ref code);
            //for (int i = 0; i < info.Services.Length; i++)
            //{
            //    UserServiceInformation ui = info.Services[i];
            //    if (ui.Service == "im")
            //    {
            //        info.SetJID(ui.Data.ToString());
            //        break;
            //    }
            //}
            //c.FindUserInformation("yujunming", "chengle");

            c.Close();

            return info;
        }

        //TODO:loginForm PKI点击的delegate
        private void LoginForm_userPKIClick()
        {
            
        }

        //TODO:loginForm 关闭窗口点击的delegate
        private void LoginForm_loginCloseClick()
        {
            if (loginForm != null)
            {
                loginForm = null;
            }
        }

        /// <summary>
        /// 最小化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ibtnMin_Click(object sender, EventArgs e)
        {
            this.ShowInTaskbar = true;
            this.WindowState = FormWindowState.Minimized;
            this.BringToFront();
        }

        /// <summary>
        /// 放大缩小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ibtnMax_Click(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            ImageButton pbx = sender as ImageButton;
            this.WindowState = this.WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
            judgeFormSize(pbx);
            this.browser.ShowDevTools();
            //this.Location = new Point((System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - 1366) / 2, (System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - 768) / 2);


        }

      

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ibtnClose_Click(object sender, EventArgs e)
        {
            DialogResult result = FrmMsg.Show(MsgKind.yes_no, "提示", "是否关闭DNA网络化实验室系统?");
           // DialogResult result = MessageBox.Show("是否关闭DNA网络化实验室系统?", "提示",MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
            
        }

        /// <summary>
        /// 显示聊天IM的btnClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ibtnLK_Click(object sender, EventArgs e)
        {
            showIMForm();
        }

        //TODO:显示IMForm
        private void showIMForm()
        {
            panelIM.Width = 280;
            panelWeb.Width = this.Width - 280;
            ibtnLK.Visible = false;
            imForm.ibtnSF.Visible = true;
        }

        private void BaseTemplateForm_DoubleClick(object sender, EventArgs e)
        {
            this.WindowState = this.WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;

            judgeFormSize(ibtnMax);

           
        }


        /// <summary>
        /// 根据的当前窗体的大小来改变放大缩小按钮的背景图片
        /// </summary>
        /// <param name="imagebutton"></param>
        private void judgeFormSize(ImageButton imagebutton)
        {
            this.CenterToScreen();
            if (this.WindowState == FormWindowState.Maximized)
            {
                imagebutton.Activepic = Resources.Reducing_down_over_02;
                imagebutton.Staticpic = Resources.Reducing_down_normal_02;
                imagebutton.Presspic = Resources.Reducing_down_down_02;
            }
            else
            {
                imagebutton.Activepic = Resources.max_over_02;
                imagebutton.Staticpic = Resources.max_normal_02;
                imagebutton.Presspic = Resources.max_down_02;
            }
            imagebutton.Invalidate();
        }

        private void BaseTemplateForm_Shown(object sender, EventArgs e)
        {

            if (isLogin)
            {
                //创建IM
                LoadIMClient(true);
            }
            else
            {
                //隐藏panelIM
                hidePanelIM(false);
            }
        }

        private void BaseTemplateForm_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void BaseTemplateForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F12)
            {
                this.browser.ShowDevTools();
            }
        }

        private void panel_Logined_Paint(object sender, PaintEventArgs e)
        {
            if (!string.IsNullOrEmpty(userTrueName))
            {
                Graphics g = e.Graphics;
                Font f = new Font("微软雅黑", 12.0f, FontStyle.Bold);
                SizeF s = g.MeasureString(userTrueName, f);
                StringFormat sf = new StringFormat();
                sf.LineAlignment = StringAlignment.Center;
                g.DrawString(userTrueName, f, new SolidBrush(Color.White), 
                    new Rectangle(new Point(0, 0), new Size((int)s.Width + 1, panel_Logined.Height)),sf);

                int x = (int)s.Width + 1;
                int y = (panel_Logined.Height - pictureBox_Grade.Image.Height) / 2;
                g.DrawImage(pictureBox_Grade.Image, new Point(x, y));

            }
        }

        public void SetBtnLocation()
        {
            if (!this.btnXTGL.Visible)
            {
                Point p1 = this.btnXTGL.Location;
                Point p2 = Point.Empty;

                p2 = this.btnSZXT.Location;
                this.btnSZXT.Location = p1;
                p1 = p2;

                p2 = this.btnXXJL.Location;
                this.btnXXJL.Location = p1;
                p1 = p2;

                p2 = this.btnKJCX.Location;
                this.btnKJCX.Location = p1;
                p1 = p2;

                p2 = this.btnJSZD.Location;
                this.btnJSZD.Location = p1;
                p1 = p2;


                p2 = this.btnJSZN.Location;
                this.btnJSZN.Location = p1;
                p1 = p2;


                //p2 = this.btnSZXT.Location;
                //this.btnSZXT.Location = p1;
                //p1 = p2;

                p2 = this.btnMain.Location;
                this.btnMain.Location = p1;
                p1 = p2;
            }
            else if (this.btnXXJL.Visible && this.btnXTGL.Location == this.btnXTGL.Location)
            {
                int OffSetX = 20;

                this.btnSZXT.Location = new Point(this.btnXTGL.Location.X - (this.btnSZXT.Width + OffSetX),this.btnXTGL.Location.Y);
                this.btnXXJL.Location = new Point(this.btnSZXT.Location.X - (this.btnXXJL.Width + OffSetX), this.btnXTGL.Location.Y);
                this.btnKJCX.Location = new Point(this.btnXXJL.Location.X - (this.btnXXJL.Width + OffSetX), this.btnXTGL.Location.Y);
                this.btnJSZD.Location = new Point(this.btnKJCX.Location.X - (this.btnXXJL.Width + OffSetX), this.btnXTGL.Location.Y);
                this.btnJSZN.Location = new Point(this.btnJSZD.Location.X - (this.btnXXJL.Width + OffSetX), this.btnXTGL.Location.Y);
               // this.btnSZXT.Location = new Point(this.btnJSZN.Location.X - (this.btnXXJL.Width + OffSetX), this.btnXTGL.Location.Y);
                this.btnMain.Location = new Point(this.btnJSZN.Location.X - (this.btnXXJL.Width + OffSetX), this.btnXTGL.Location.Y);
            }
        }
    }
}
