using CefSharp.WinForms;
using IMClient.XMPP.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace IMClient.Logic
{
    public partial  class Common
    {
        
        /// <summary>
        /// 根据HTML 返回 本地路径图片
        /// </summary>
        /// <param name="htmlPath"></param>
        /// <param name="JID"></param>
        /// <returns></returns>
        public string JS_localPathByHtmlPath(string htmlPath)
        {
            string imgPath = string.Empty;
            if (!string.IsNullOrEmpty(htmlPath))
            {
                string FileName = htmlPath.Substring(htmlPath.LastIndexOf('/') + 1);
                string tmp = htmlPath.Substring(0, htmlPath.LastIndexOf('/') );
                tmp = tmp.Substring(0,tmp.LastIndexOf('/') );
                string JID= tmp.Substring(tmp.LastIndexOf('/') + 1);
                imgPath = AppDomain.CurrentDomain.BaseDirectory + "htm\\Source\\" + JID + "\\img\\" + FileName;
            }
      
            return imgPath;
        }

        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="FilePath"></param>
        public void JS_OpenFile(string FilePath)
        {
            if (File.Exists(FilePath))
            {
                Process.Start(FilePath);
            }
        }

        /// <summary>
        /// 打开文件夹 并选中文件
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="DirPath"></param>
        public void JS_OpenDir(string FilePath,string DirPath)
        {
            if (Directory.Exists(DirPath)&&File.Exists(FilePath))
            {
                Process.Start("explorer.exe", @"/select,"+ FilePath);
            }
        }

        

        public List<string[]>  JS_TestCodis()
        {
            //OpenFileDialog of = new OpenFileDialog();
            //of.ShowDialog();
            List<string[]> items = new List<string[]>();
            string txtContent = File.ReadAllText(@"D:\DNALIMS\LimsClient\LimsClient\Codis\test1.codis");
            string[] itemlist = txtContent.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 1; i < itemlist.Length; i++)
            {
                string[] itemContent=itemlist[i].Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                items.Add(itemContent);
            }
            return items;
        }

        public string JS_TestCodis2()
        {
            //OpenFileDialog of = new OpenFileDialog();
            //of.ShowDialog();
            List<string[]> items = new List<string[]>();
            string txtContent = File.ReadAllText(@"D:\DNALIMS\LimsClient\LimsClient\Codis\test1.codis");
            return txtContent;
        }
        public void OpenImage(string imgPath)
        {
            if (File.Exists(imgPath))
            {
                ImgForm frm = new ImgForm(imgPath);
                
                frm.TopLevel = true;
                frm.ShowDialog();
                frm.BringToFront();

            }
        }



        /// <summary>
        /// 获取服务器图片 并展示
        /// </summary>
        /// <param name="imgPath"></param>
        public void OpenRemoteImage(string imgPath)
        {
            string fileName = imgPath.Substring(imgPath.LastIndexOf("/")+1);
            string FilePath = AppDomain.CurrentDomain.BaseDirectory + "htm\\Source\\default\\img";
            if (Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }
            //FilePath = FilePath + "\\" + fileName;
            TempFile.DownLoad_Image(imgPath,ref FilePath);
            if (File.Exists(FilePath))
            {
                ImgForm frm = new ImgForm(FilePath);

                frm.TopLevel = true;
                frm.ShowDialog();
                frm.BringToFront();

            }
        }

        /// <summary>
        /// 画原型图
        /// </summary>
        /// <param name="img"></param>
        /// <param name="rec"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Image CutEllipse(Image img, Rectangle rec, Size size)
        {
            Bitmap bitmap = new Bitmap(size.Width, size.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                using (TextureBrush br = new TextureBrush(img, System.Drawing.Drawing2D.WrapMode.Clamp))
                {
                    //br.ScaleTransform(bitmap.Width / (float)rec.Width, bitmap.Height / (float)rec.Height);
                    br.ScaleTransform(bitmap.Width / (float)img.Width, bitmap.Height / (float)img.Height);
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.FillEllipse(br, new Rectangle(Point.Empty, size));
                }
            }
            return bitmap;
        }


        /// <summary>
        /// 矩形 园型边框
        /// </summary>
        /// <param name="img"></param>
        /// <param name="rec"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Image FillRoundRectangle(Image img, Rectangle rec, Size size)
        {
            Bitmap bitmap = new Bitmap(size.Width, size.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                using (TextureBrush br = new TextureBrush(img, System.Drawing.Drawing2D.WrapMode.Clamp))
                {
                    //br.ScaleTransform(bitmap.Width / (float)rec.Width, bitmap.Height / (float)rec.Height);
                    br.ScaleTransform(bitmap.Width / (float)img.Width, bitmap.Height / (float)img.Height);
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.FillPath(br, GetRoundRectangle(rec, 5));
                }
            }
            return bitmap;
        }


        /// <summary>  
        /// 根据普通矩形得到圆角矩形的路径  
        /// </summary>  
        /// <param name="rectangle">原始矩形</param>  
        /// <param name="r">半径</param>  
        /// <returns>图形路径</returns>  
        private static GraphicsPath GetRoundRectangle(Rectangle rectangle, int r)
        {
            int l = 2 * r;
            // 把圆角矩形分成八段直线、弧的组合，依次加到路径中  
            GraphicsPath gp = new GraphicsPath();
            gp.AddLine(new Point(rectangle.X + r, rectangle.Y), new Point(rectangle.Right - r, rectangle.Y));
            gp.AddArc(new Rectangle(rectangle.Right - l, rectangle.Y, l, l), 270F, 90F);

            gp.AddLine(new Point(rectangle.Right, rectangle.Y + r), new Point(rectangle.Right, rectangle.Bottom - r));
            gp.AddArc(new Rectangle(rectangle.Right - l, rectangle.Bottom - l, l, l), 0F, 90F);

            gp.AddLine(new Point(rectangle.Right - r, rectangle.Bottom), new Point(rectangle.X + r, rectangle.Bottom));
            gp.AddArc(new Rectangle(rectangle.X, rectangle.Bottom - l, l, l), 90F, 90F);

            gp.AddLine(new Point(rectangle.X, rectangle.Bottom - r), new Point(rectangle.X, rectangle.Y + r));
            gp.AddArc(new Rectangle(rectangle.X, rectangle.Y, l, l), 180F, 90F);
            return gp;
        }


        #region html Form 交互
        /// <summary>
        /// 通过CEF从窗口去调用当前页面中的JS方法
        /// </summary>
        /// <param name="jsname"></param>
        public void CallJS(ChromiumWebBrowser browser, string jsname)
        {
            CefSharp.IFrame mainFrame = browser.GetBrowser().MainFrame;
            mainFrame.ExecuteJavaScriptAsync(jsname);
        }

        /// <summary>
        /// 在html 中注册Form 类 供调用
        /// </summary>
        /// <param name="browser">浏览器控件</param>
        /// <param name="JSNAME">html中类名</param>
        public  void RegObjectTOCEF(ChromiumWebBrowser browser,object obj,string JSNAME)
        {
            browser.RegisterJsObject(JSNAME, obj, false);
        }
        //browser.RegisterJsObject("jsOBJ", this,false);

        #region 发送聊天记录
            /// <summary>
            /// 历史记录 时间间隔线
            /// </summary>
        private string jsFunction_HistoryLine = "addHistoryLine('{0}')";

        /// <summary>
        /// 发送JS方法
        /// 0  true false 本人/他人
        /// 1 发送内容
        /// 2 JID
        /// 3 发送时间
        /// 4 字体
        /// 5 字体大小
        /// 6 发送人头像
        /// </summary>
        private string jsFunction_SendMessageStr = "add({0},'{1} ','{2}','{3}','{4}','{5}','{6}')";


        /// <summary>
        /// 发送JS方法 系统消息
        /// addSys(strConent, fontFamily, fontSize)
        /// 1 发送内容
        /// 2 字体
        /// 3 字体大小
        /// </summary>
        private string jsFunction_SendSysMessageStr = "addSys('{0} ','{1}','{2}',{3})";

        /// <summary>
        /// 发送语音
        /// 1 是否自己 true/false
        /// 2 语音文件临时名字
        /// 3 发送人JID
        /// 4 发送时间
        /// 5 字体
        /// 6 大小
        /// 7 发送人头像
        /// 8 语音网络路径
        /// </summary>
        private string jsFunction_SendVoiceMessageStr = "addVoice({0},'{1}', '{2}', '{3}', '{4}','{5}','{6}','{7}')";

        /// <summary>
        ///  文件发送
        /// 0 是否自己
        /// 1 文件内容
        /// 2 发送者名称
        /// 3 发送时间
        /// 4 字体
        /// 5 字体大小
        /// 6 头像地址
        /// 7 文件ico
        /// 8 文件名称
        /// 9 文件目录
        /// 10 文件大小
        /// 11 文件传输结果
        /// 12 文件名称长度限制
        /// 13 传输结果文字长度限制
        /// </summary>
        private string jsFunction_SendFileMessageStr = "addFile({0}, '{1}', '{2}', '{3}', '{4}',  '{5}', '{6}',"
            + " '{7}','{8}','{9}','{10}','{11}',{12},{13})";

        /// <summary>
        /// 文件发送
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="isSelf"></param>
        /// <param name="strConent"></param>
        /// <param name="sendName"></param>
        /// <param name="sendTime"></param>
        /// <param name="fontFamily"></param>
        /// <param name="fontSize"></param>
        /// <param name="imgHeadUrl"></param>
        /// <param name="fileIcoUrl">文件缩略图</param>
        /// <param name="fileName">文件名称</param>
        ///   <param name="Dir">文件目录</param>
        /// <param name="fileSize">文件大小</param>
        /// <param name="fileResult">传输结果</param>
        public void sendFileMessage(ChromiumWebBrowser browser,string isSelf,string strConent,string sendName,string sendTime, 
           string fontFamily,string fontSize,string imgHeadUrl,
           string fileIcoUrl,string fileName,string fileDir,string fileSize,string fileResult)
        {
            string addFunctionStr = string.Format(jsFunction_SendFileMessageStr, new string[] {
              isSelf, strConent, sendName, sendTime,
              fontFamily, fontSize, imgHeadUrl, fileIcoUrl, fileName,fileDir, fileSize.ToString(), fileResult,SysParams.Limit_Html_StrLength_FileName.ToString(),SysParams.Limit_Html_StrLength_FileResult.ToString()
            });
            this.CallJS(browser, addFunctionStr);
        }
        /// <summary>
        /// 时间分割线
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="DateStr"></param>
        public void History_DateLine(ChromiumWebBrowser browser, string DateStr)
        {
            string addFunctionStr = string.Format(jsFunction_HistoryLine, new string[] {
              DateStr
            });
            this.CallJS(browser, addFunctionStr);
        }
        
        /// <summary>
        /// html 追加消息记录
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="isSelf"></param>
        /// <param name="content"></param>
        /// <param name="sendName"></param>
        /// <param name="sendTime"></param>
        /// <param name="fontFamily"></param>
        /// <param name="fontSize"></param>
        /// <param name="imgHeadPath"></param>
        public void sendMessage(ChromiumWebBrowser browser, string isSelf, string content, string sendName, string sendTime,
            string fontFamily,string fontSize,string imgHeadPath)
        {
            string addFunctionStr=string.Format(jsFunction_SendMessageStr,new string[] {
                isSelf,content,sendName,sendTime,fontFamily,fontSize,imgHeadPath
            });
            this.CallJS(browser, addFunctionStr);
        }

        /// <summary>
        /// 发送系统消息
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="isSelf"></param>
        /// <param name="content"></param>
        /// <param name="sendName"></param>
        /// <param name="sendTime"></param>
        /// <param name="fontFamily"></param>
        /// <param name="fontSize"></param>
        /// <param name="imgHeadPath"></param>
        public void sendMessage_Sys(ChromiumWebBrowser browser,  string content,SysInfoType sType)
        {
            string addFunctionStr = string.Format(jsFunction_SendSysMessageStr, new string[] {content,SysParams.sysFont.Name,SysParams.sysFont.Size.ToString(),((int)sType).ToString()});
            this.CallJS(browser, addFunctionStr);
        }

        /// <summary>
        /// 发送系统消息
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="isSelf"></param>
        /// <param name="content"></param>
        /// <param name="sendName"></param>
        /// <param name="sendTime"></param>
        /// <param name="fontFamily"></param>
        /// <param name="fontSize"></param>
        /// <param name="imgHeadPath"></param>
        public string sendMessage_Voice(ChromiumWebBrowser browser, string isSelf, 
            string fileName, string sendName, string sendTime,
            string fontFamily, string fontSize, string imgHeadPath,string voicePath)
        {
            string addFunctionStr = string.Format(jsFunction_SendVoiceMessageStr, new string[] { isSelf, fileName,
                sendName, sendTime, fontFamily, fontSize,
                imgHeadPath,voicePath });
            this.CallJS(browser, addFunctionStr);
            return addFunctionStr;
        }


        #endregion



        #region meeting

        #region 会议室
        public string jsFunction_Meeting_SetHeightStr = "setDivHeight({0})";
        public string jsFunction_Meeting_SendMessageNormalStr = "addMeeting_2({0},'{1} ','{2}','{3}','{4}','{5}','{6}',{7})";

        #endregion

        /// <summary>
        /// 强行设置Div的高度
        /// </summary>
        /// <param name="height"></param>
        public void setHeight(int height)
        {
            string addFunctionStr = string.Format(jsFunction_Meeting_SetHeightStr, new string[] { height.ToString() });
            this.CallJS(browser, addFunctionStr);
        }

        /// <summary>
        /// html 追加消息记录
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="isSelf"></param>
        /// <param name="content"></param>
        /// <param name="sendName"></param>
        /// <param name="sendTime"></param>
        /// <param name="fontFamily"></param>
        /// <param name="fontSize"></param>
        /// <param name="imgHeadPath"></param>
        public void sendMessage_Meeting(ChromiumWebBrowser browser, string isSelf, string content, string sendName, string sendTime,
            string fontFamily, string fontSize, string imgHeadPath,bool isEmcee)
        {
            string addFunctionStr = string.Format(jsFunction_Meeting_SendMessageNormalStr, new string[] {
                isSelf,content,sendName,sendTime,fontFamily,fontSize,imgHeadPath,isEmcee.ToString().ToLower()
            });
            this.CallJS(browser, addFunctionStr);
        }

        #endregion

        #endregion

    }
}
