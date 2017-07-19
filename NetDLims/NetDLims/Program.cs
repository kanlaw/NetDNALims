using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms.Example.Minimal;
using NetDLims.Forms;
using NetDLims.Services;

namespace NetDLims
{
    static class Program
    {
        internal static Form MainWindow;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        public static int Main()
        {

            const bool simpleSubProcess = true;

            //AppDomain.CurrentDomain.AppendPrivatePath("CEF");   //增加DLL搜索路径

            Cef.EnableHighDPISupport();

            //NOTE: Using a simple sub processes uses your existing application executable to spawn instances of the sub process.
            //Features like JSB, EvaluateScriptAsync, custom schemes require the CefSharp.BrowserSubprocess to function
            if (simpleSubProcess)
            {

                var exitCode = Cef.ExecuteProcess();

                if (exitCode >= 0)
                {
                    return exitCode;
                }

                //#if DEBUG
                //                if (!System.Diagnostics.Debugger.IsAttached)
                //                {
                //                    MessageBox.Show("When running this Example outside of Visual Studio" +
                //                                    "please make sure you compile in `Release` mode.", "Warning");
                //                }
                //#endif


                IMClient.Controls.StaticClass.XMPPCrack();

                var settings = new CefSettings();
                //settings.LocalesDirPath = Application.StartupPath + @"\CEF\locales";
                //settings.BrowserSubprocessPath = "NetDLims.exe";
                //settings.BrowserSubprocessPath = "CefSharp.BrowserSubprocess.exe";
                settings.MultiThreadedMessageLoop = true;
                settings.AcceptLanguageList = "zh-CN";
                settings.CachePath = "caches";

                //加上特定版本的FLASH支持
                settings.Locale = "zh-CN";
                //settings.CefCommandLineArgs.Add("ppapi-flash-path", Application.StartupPath + "\\CEF\\PepperFlash\\pepflashplayer.dll"); //指定flash的版本，不使用系统安装的flash版本
                //settings.CefCommandLineArgs.Add("ppapi-flash-version", "22.0.0.192");



                bool ret = Cef.Initialize(settings, true, false);

                //var browser = new SimpleBrowserForm();
                //Application.Run(browser);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                
                Program.MainWindow = new BaseTemplateForm();

                AppDomain.CurrentDomain.SetData("MainWindow", Program.MainWindow);

                Application.Run(Program.MainWindow);
            }
            else
            {
                //#if DEBUG
                //                if (!System.Diagnostics.Debugger.IsAttached)
                //                {
                //                    MessageBox.Show("When running this Example outside of Visual Studio" +
                //                                    "please make sure you compile in `Release` mode.", "Warning");
                //                }
                //#endif


                const bool multiThreadedMessageLoop = true;
                //CefExample.Init(false, multiThreadedMessageLoop: multiThreadedMessageLoop);

                if (multiThreadedMessageLoop == false)
                {
                    //http://magpcss.org/ceforum/apidocs3/projects/%28default%29/%28_globals%29.html#CefDoMessageLoopWork%28%29
                    //Perform a single iteration of CEF message loop processing.
                    //This function is used to integrate the CEF message loop into an existing application message loop.
                    //Care must be taken to balance performance against excessive CPU usage.
                    //This function should only be called on the main application thread and only if CefInitialize() is called with a CefSettings.multi_threaded_message_loop value of false.
                    //This function will not block. 

                    Application.Idle += (s, e) => Cef.DoMessageLoopWork();
                }

                //var browser = new BrowserForm();
                //var browser = new SimpleBrowserForm();
                //var browser = new TabulationDemoForm();
                //Application.Run(browser);
            }

            return 0;
        }
    }
}