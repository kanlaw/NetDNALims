using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Matrix.License;
using System.Reflection;
using Matrix.Xmpp.Client;

namespace IMClient
{
    static class Program
    {
        public static IMForm IMMain;
        

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
           
            int retParent = 0;
            //先去寻找父窗口,如果未找到就启动失败，自动退出
            retParent = FindNetParent();
            if (retParent == -1)
            {
                //return;
            }

            //调用破解接口
            XMPPCrack();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Program.IMMain = new IMForm();
            SetNetParant(retParent);

            AppDomain.CurrentDomain.SetData("IMForm", Program.IMMain);

            Application.Run(IMMain);
        }

        /// <summary>
        /// 让XMPP类库不再捣乱
        /// </summary>
        public static void XMPPCrack()
        {
            Type tLicenseManager = typeof(LicenseManager);
            FieldInfo f_IsValid = tLicenseManager.GetField("m_IsValid", BindingFlags.Static | BindingFlags.NonPublic);
            Assembly aMatrix = tLicenseManager.Assembly;

            f_IsValid.SetValue(null, true);
        }
        

        //启动后去寻找NETLIMS的父窗口
        public static int FindNetParent()
        {
            
            //int hWnd = FindWindow(null, "BaseTemplateForm");
            //如果没找到就返回-1 并退出
            return -1;
        }

        //把找到的父窗口设置成聊天主窗口的爹
        public static int SetNetParant(int handleParent)
        {

            return 0;
        }
    }
}
