using Matrix.License;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
namespace TestDemo
{
    static class Program
    {
        static System.Security.Cryptography.HashAlgorithm hash= System.Security.Cryptography.MD5.Create();

        public static byte[] CalcHash(params byte[] data)
        {
            return Program.hash.ComputeHash(data);
        }

        public static string CalcHash(string data)
        {
            byte[] raw = Encoding.UTF8.GetBytes(data);
            return Convert.ToBase64String(raw);
        }
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            XMPPCrack();

            string tmp= CalcHash(@"chengle:111111");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
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
    }
}
