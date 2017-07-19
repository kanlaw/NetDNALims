using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace IMClient.Logic
{
    public partial class SysParams
    {
        /// <summary>
        /// 默认头像图片
        /// </summary>
        public static Image defaultHead = null;

        public static byte[] defualtHead_byte = null;

        /// <summary>
        /// 初始化 系统参数
        /// </summary>
        public static void InitParams()
        {
            using (FileStream fs = new FileStream(
                AppDomain.CurrentDomain.BaseDirectory  + Sys_DefaultHeadImageFilePath, 
                FileMode.Open))
            {
                    int length = (int)fs.Length;
                    byte[] data = new byte[length];
                    fs.Position = 0;
                    fs.Read(data, 0, length);
                    MemoryStream ms = new MemoryStream(data);
                    defaultHead = Image.FromStream(ms);
                    defualtHead_byte = data;


            }
        }
    }
}
