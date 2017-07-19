using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Reflection;
using CefSharp;
using DNANET.Data;
using Newtonsoft.Json;
using IMClient.Controls.Tools;

namespace IMClient.Logic
{
    /// <summary>
    /// 首页 JS调用处理方法
    /// </summary>
    public partial class Common :IJsDialogHandler
    {


        public ChromiumWebBrowser browser;

       

        public delegate void WebIsLoginUser();
        public event WebIsLoginUser webIsLoginUser;
        public void webIsLogin()
        {
            if (webIsLoginUser != null)
            {
                webIsLoginUser();
            }
        }

        public string[] getFriendUids()
        {
            return SysParams.userFriendUids.ToArray();
        }

        string mailAccount;
        string mailPassWord;
        public void loadUserMail(string account, string passWord)
        {

            browser.FrameLoadEnd += Browser_FrameLoadEnd;
            mailAccount = account;
            mailPassWord = passWord;

            //MessageBox.Show("1111");

            string mailUrl = string.Format(SysParams.Page_Mail, SysParams.MailServer);
            LoadUrl(mailUrl);
        }

        public bool userIsLogin;

        //
        public UserInformation judgeUserIsLogin()
        {
            if (SysParams.LoginUser == null)
            {
                //MessageBox.Show("请先登录账户!");
                FrmMsg.Show(MsgKind.ok, "提示", "请先登录账号");
                //CallJS(this.browser, "getUserUid('000', 'name')");
                return  null;
            }
            else
            {
                var json = JsonConvert.SerializeObject(SysParams.LoginUser);
                //CallJS(this.browser, "getUserUid('000', 'name')");
                UserInformation info = Clone(SysParams.LoginUser);

                return info;
            }
        }

        

        public string getUserServerDic()
        {
            //string[] results = new string[SysParams.LoginUser.Services.Length];
            //for(int i=0;i< SysParams.LoginUser.Services.Length;i++)
            //{
            //    UserServiceInformation si = SysParams.LoginUser.Services[i];
            //    results[i] = si.Service;
            //}

            //return results;

            if (SysParams.serversDic != null)
            {
                var json = JsonConvert.SerializeObject(SysParams.serversDic);
                return json;
            }
            else
            {
                return null;
            }
        }

        public static UserInformation Clone(UserInformation u)
        {
            UserInformation ui = new UserInformation();
            Type t = typeof(UserInformation);

            foreach (PropertyInfo p in t.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!p.CanWrite) continue;
                //if (@"LastAccess".Equals(p.Name)) continue;
                //if (@"ResetPassword".Equals(p.Name)) continue;
                if (@"Services".Equals(p.Name)) continue;

                p.SetValue(ui, p.GetValue(u, null), null);
            }

            return ui;
        }

        public void setLoginStatus(bool status)
        {
            this.userIsLogin = status;
        }

        public void loadPerson(string account, string passWord)
        {

            browser.FrameLoadEnd += Browser_FrameLoadEnd;
            mailAccount = account;
            mailPassWord = passWord;

            //MessageBox.Show("1111");

            string mailUrl = string.Format(SysParams.Page_Mail, SysParams.MailServer);
            LoadUrl(mailUrl);
        }

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

      

        public void LogoutEMail()
        {
            IBrowser b = this.browser.GetBrowser();
            IFrame frame = b.GetFrame("emailLogin");

            frame.EvaluateScriptAsync("App.logout();");
        }

        private void Browser_FrameLoadEnd(object sender, CefSharp.FrameLoadEndEventArgs e)
        {
            string mailUrl = string.Format(SysParams.Page_Mail,SysParams.MailServer)+"?login=1";

            string js = "if(document.getElementById('selenium_login_email') != undefined || document.getElementById('selenium_login_password') != undefined) {";
            //登录页
            //js += "if(document.getElementById('selenium_login_email').value==''){ document.getElementById('selenium_login_email').value='"+ mailAccount + "';}";
            //js += "if(document.getElementById('selenium_login_password').value==''){ document.getElementById('selenium_login_password').value='" + mailPassWord + "';}";
            //js += "document.getElementById('selenium_login_signin_button').click();";
            js += "alert('欢迎登录邮箱');";
            js += "} else {";
            //邮件页
            js += "document.getElementById('selenium_logout_button').style.display = 'none';";
            js += "document.getElementById('selenium_settings_button').style.display = 'none';";
            /*         var parent= document.getElementById("").parentElement;
            var button= document.createElement("span");
            var button2=document.createElement("span");
            button2.className="link";
            button2.textContent="退出";
            button.className="item logout";
            button.appendChild(button2);
            button2.onclick=function()
            {
                App.logout();
            }
            parent.appendChild(button);*/
            
            js += "var ppp = document.getElementById('selenium_logout_button').parentElement.parentElement;";
            js += "if( document.getElementById('tmpSpan') == undefined){";
            js += "ppp.innerHTML = ppp.innerHTML + \"<span class='item logout'><span id='tmpSpan' class='link' onclick='jsOBJ.LogoutEMail();'>退出</span></span>\";";
            js += "}";
            //js += "var b1 = document.createElement('span');";
            //js += "var b2 = document.createElement('span');";
            ////js += "button2.className ='link';";
            //js += "b2.innerHTML = '退出1';";
            ////js += "button.className = 'item logout';";

            //js += "b2.addEventListener('onclick', function(){alert(1378314672846284);App.logout();});";
            //js += "b1.appendChild(b2);";
            //js += "ppp.appendChild(b1);";


            js += "}";

            if (Regex.IsMatch(e.Url, @"^(http|https):\/\/("+SysParams.MailWeb+@")\/", RegexOptions.IgnoreCase))
            {
                e.Frame.EvaluateScriptAsync(js);
                //e.Frame.EvaluateScriptAsync(string.Format(@"document.getElementById('selenium_login_email').value='{0}';", mailAccount));
                //e.Frame.EvaluateScriptAsync(string.Format(@"document.getElementById('selenium_login_password').value='{0}';", mailPassWord));
                //e.Frame.EvaluateScriptAsync(@"document.getElementById('selenium_login_signin_button').click();");
            }
            //else if (e.Url.ToLower().Equals("http://yanwk:10080/"))
            //{
                //e.Frame.EvaluateScriptAsync(string.Format(@"if(document.getElementById('selenium_login_email') != undefined && document.getElementById('selenium_login_password') != undefined) alert('10086');"));
                //mailUrl = string.Format(SysParams.Page_Mail, SysParams.MailServer);
                //mailUrl = string.Format(SysParams.Page_PersonalInfo, SysParams.MailServer) + "?login=1";
                //e.Frame.EvaluateScriptAsync(string.Format(@"var selenium_login_email = document.getElementById('selenium_login_email');"));
                //e.Frame.EvaluateScriptAsync(string.Format(@"var selenium_login_password = document.getElementById('selenium_login_password');"));
                //e.Frame.EvaluateScriptAsync(string.Format(@"if( && selenium_login_password!=undefined)
                //("));
                //LoadUrl(string.Format(SysParams.Page_PersonalInfo, SysParams.AppServer, "123"));
            //}
            return;
            /*
            if (e.Browser.MainFrame.Url.Contains("http://192.168.1.90:8020/Front_Web/emailLogin.html"))
            //Regex.IsMatch(e.Url,@"http:192.168.1.90:8020/Front_Web/emailLogin.html" @"^http://192\.168\.1\.139\:10080"))http://192.168.1.90:8020/Front_Web/emailLogin.html http://192.168.1.90:8020/Front_Web/emailLogin.html
            {
                string query = Regex.Match(e.Url, @"\?([^$]+)").Groups[1].Value;

                bool bLogin = false;

                foreach (Match m in Regex.Matches(query, @"([a-zA-Z0-9_]+)\=([^&]+)", RegexOptions.IgnoreCase))
                {
                    string k = m.Groups[1].Value.ToLower();
                    string v = m.Groups[2].Value;

                    switch (k)
                    {
                        case "login": bLogin = "1".Equals(v); break;
                    }
                }

                if (bLogin)
                {
                    e.Frame.EvaluateScriptAsync(string.Format(@"document.getElementById('selenium_login_email').value='{0}';", mailAccount));
                    e.Frame.EvaluateScriptAsync(string.Format(@"document.getElementById('selenium_login_password').value='{0}';", mailPassWord));
                    e.Frame.EvaluateScriptAsync(@"document.getElementById('selenium_login_signin_button').click();");
                }
            }*/
        }

        bool IJsDialogHandler.OnJSDialog(IWebBrowser browserControl, IBrowser browser, string originUrl, string acceptLang, CefJsDialogType dialogType, string messageText, string defaultPromptText, IJsDialogCallback callback, ref bool suppressMessage)
        {
            if (messageText == "欢迎登录邮箱")
            {
                IFrame frame = browser.GetFrame("emailLogin");

                Match mUrl = Regex.Match(frame.Url, @"(http:\/\/" + SysParams.MailWeb + @"\/)(\?login\=1)?", RegexOptions.IgnoreCase);

                if (mUrl.Groups[1].Success)
                {
                    if (mUrl.Groups[2].Success)
                    {
                        string js = string.Format(@"document.getElementById('selenium_login_email').value='{0}';", mailAccount);
                        js += string.Format(@"document.getElementById('selenium_login_password').value='{0}';", mailPassWord);
                        js += @"document.getElementById('selenium_login_signin_button').click();";

                        frame.EvaluateScriptAsync(js);
                    }
                    else
                    {
                        //LoadUrl(string.Format(SysParams.Page_PersonalInfo, SysParams.AppServer, "123"));
                    }
                }

                //suppressMessage = false;

                //return true;
            }
            return false;
        }

        bool IJsDialogHandler.OnJSBeforeUnload(IWebBrowser browserControl, IBrowser browser, string message, bool isReload, IJsDialogCallback callback)
        {
            return true;
        }

        void IJsDialogHandler.OnResetDialogState(IWebBrowser browserControl, IBrowser browser)
        {
        }

        void IJsDialogHandler.OnDialogClosed(IWebBrowser browserControl, IBrowser browser)
        {
        }


        public static bool checkAuth(UserInformation userinfo)
        {
            bool result = false;
            if (userinfo.Company.Contains("杭"))
            {
                result = true;
            }
            return result;
        }
    }
}
