using Matrix.License;
using Matrix.Xmpp.Client;
using Matrix.Xmpp.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace TestDemo
{
    public static class StaticClass
    {
        //public static string IPStr =System AppServer;
        public static XmppClient xmppClient;

        /// <summary>
        /// 好友在线状态通知/推送
        /// </summary>
        public static PresenceManager pm;

        /// <summary>
        /// 文件传输管理
        /// </summary>
        public static FileTransferManager fm;
        /// <summary>
        /// 好友关系管理
        /// </summary>
        public static RosterManager rm;

        public static MucManager muc;
        public static XmppClient InitXmppClient(string userName, string passWord, string domain, string server)
        {
            StaticClass.xmppClient = new XmppClient(userName, domain, passWord);
            StaticClass.xmppClient.Hostname = server;

            //StaticClass.xmppClient.Open();

            StaticClass.pm = new PresenceManager(xmppClient);
            StaticClass.rm = new RosterManager(xmppClient);
          
            StaticClass.muc = new MucManager(xmppClient);

            StaticClass.fm = new FileTransferManager();
            //StaticClass.fm.OnProgress += Fm_OnProgress;
            //StaticClass.fm.OnEnd += Fm_OnEnd;

            return StaticClass.xmppClient;
        }

        //TODO:调用破解接口
        public static void XMPPCrack()
        {
            Type tLicenseManager = typeof(LicenseManager);
            FieldInfo f_IsValid = tLicenseManager.GetField("m_IsValid", BindingFlags.Static | BindingFlags.NonPublic);
            Assembly aMatrix = tLicenseManager.Assembly;

            f_IsValid.SetValue(null, true);
        }

        //TODO:正则匹配标准的JID
        public static string getStandardJid(string from)
        {
            Match mFrom = Regex.Match(from, @"^([^\/]+)(\/([^$]+))?$", RegexOptions.IgnoreCase);
            return mFrom.Groups[1].Value;
        }

        //TODO:注册帐号
        public static void registXMPPAccount(string userName, string passWord, string server, string domain)
        {

            xmppClient.OnRegister += txmppClient_OnRegister;
            xmppClient.OnRegisterInformation += xmppClient_OnRegisterInformation;
            xmppClient.OnRegisterError += xmppClient_OnRegisterError;



            xmppClient.Hostname = server;
            xmppClient.SetUsername(userName);
            xmppClient.SetXmppDomain(domain);
            xmppClient.Password = passWord;
            xmppClient.RegisterNewAccount = true;

            xmppClient.Open();
        }

        //初测失败返回的信息
        public static void xmppClient_OnRegisterError(object sender, IqEventArgs e)
        {

        }


        //返回注册信息
        public static void xmppClient_OnRegisterInformation(object sender, RegisterEventArgs e)
        {

            e.Register.RemoveAll();
            //e.Register.RemoveAll<Data>();

            e.Register.Username = xmppClient.Username;
            e.Register.Password = xmppClient.Password;

        }

        //注册成功返回到信息
        public static void txmppClient_OnRegister(object sender, Matrix.EventArgs e)
        {

        }
    }
}
