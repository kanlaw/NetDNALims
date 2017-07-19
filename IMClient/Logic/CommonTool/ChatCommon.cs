using CefSharp.WinForms;
using CSharpWin_JD.CaptureImage;
using IMClient.XMPP.Forms;
using JustLib.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Data;
using Newtonsoft.Json;

namespace IMClient.Logic
{
    public partial  class Common
    {

        /// <summary>
        /// 缩略图宽
        /// </summary>
        public static int minWidth= 400;

        /// <summary>
        /// 缩略图高
        /// </summary>
        public static int minHeight= 400;




        #region 生成缩略图
        /// <summary>
        /// 本地临时存放地址
        /// </summary>
        /// <param name="img"></param>
        /// <param name="imgType"></param>
        /// <returns></returns>
        public static string SaveThumbnailImageToTempFile(Image img,ImageType imgType)
        {
            string SavePath = string.Empty;
            string SaveDir = AppDomain.CurrentDomain.BaseDirectory + SysParams.Sys_ThumbnailImagePath;
            if (!Directory.Exists(SaveDir))
            {
                Directory.CreateDirectory(SaveDir);
            }
            try
            {
                string fileName = Guid.NewGuid().ToString().Replace("-", "");
                switch (imgType)
                {
                    case ImageType.GIF:
                        SavePath = SaveDir+fileName + ".gif";
                        img.Save(SavePath, ImageFormat.Gif);
                        break;
                    case ImageType.None:
                        break;
                    default:
                        SavePath = SaveDir+fileName + ".jpg";
                        img.Save(SavePath, ImageFormat.Jpeg);
                        break;
                }
            }
            catch (Exception ex)
            {
                SavePath = string.Empty;
            }
            return SavePath;
        }


      
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="image"></param>
        /// <param name="minWidth"></param>
        /// <param name="minHeight"></param>
        /// <returns></returns>
        public static Image GetThumbnailImageKeepRatio(Image image)
        {
            if (image == null || minWidth < 1 || minHeight < 1)
                return null;
            SizeF thumImage = GetThumbnailImageSize(image.Size.Width, image.Size.Height, minWidth, minHeight);
            Image bitmap = new Bitmap((int)thumImage.Width, (int)thumImage.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.DrawImage(image, new Rectangle(new Point(0,0), image.Size ),
                    new Rectangle(new Point(0,0), bitmap.Size), GraphicsUnit.Pixel);
                return bitmap;
            }
            
        }

        /// <summary>
        /// 返回缩略图尺寸
        /// </summary>
        /// <param name="imageWidth"></param>
        /// <param name="imageHeight"></param>
        /// <param name="minWidth"></param>
        /// <param name="minHeight"></param>
        /// <returns></returns>
        public static SizeF GetThumbnailImageSize(int imageWidth,int imageHeight, int minWidth, int minHeight)
        {
            float ratio = 1.0f;
            float w_ration = minWidth / imageWidth;
            float h_ration = minHeight / imageHeight;
            if (w_ration > 0 && h_ration < 0)
            {
                ratio = h_ration;
            }
            else if (w_ration > 0 && h_ration < 0)
            {
                ratio = w_ration;
            }
            else if (w_ration < 0 && h_ration < 0)
            {
                ratio = (w_ration < h_ration) ? w_ration : h_ration;
            }
            float width = imageWidth * ratio;
            float height = imageHeight * ratio;
            SizeF imgSize = new SizeF(width, height);
            return imgSize;
        }
        #endregion

        #region 判断图片类型

        public static SortedDictionary<int, ImageType> _imageTag;

        public static readonly string ErrType = ImageType.None.ToString();

        public static SortedDictionary<int, ImageType> InitImageTag()
        {
            SortedDictionary<int, ImageType> list = new SortedDictionary<int, ImageType>();

            list.Add((int)ImageType.BMP, ImageType.BMP);
            list.Add((int)ImageType.JPG, ImageType.JPG);
            list.Add((int)ImageType.GIF, ImageType.GIF);
            list.Add((int)ImageType.PCX, ImageType.PCX);
            list.Add((int)ImageType.PNG, ImageType.PNG);
            list.Add((int)ImageType.PSD, ImageType.PSD);
            list.Add((int)ImageType.RAS, ImageType.RAS);
            list.Add((int)ImageType.SGI, ImageType.SGI);
            list.Add((int)ImageType.TIFF, ImageType.TIFF);
            return list;

        }

 
        /// <summary>  
        /// 通过文件头判断图像文件的类型  
        /// </summary>  
        /// <param name="path"></param>  
        /// <returns></returns>  
        public static ImageType CheckImageType(string path)
        {
            byte[] buf = new byte[2];
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    int i = sr.BaseStream.Read(buf, 0, buf.Length);
                    if (i != buf.Length)
                    {
                        return ImageType.None;
                    }
                }
            }
            catch (Exception exc)
            {
                //Debug.Print(exc.ToString());
                return ImageType.None;
            }
            return CheckImageType(buf);
        }

        /// <summary>  
        /// 通过文件的前两个自己判断图像类型  
        /// </summary>  
        /// <param name="buf">至少2个字节</param>  
        /// <returns></returns>  
        public static ImageType CheckImageType(byte[] buf)
        {
            if (buf == null || buf.Length < 2)
            {
                return ImageType.None;
            }

            int key = (buf[1] << 8) + buf[0];
            ImageType s;
            try
            {
                if (_imageTag == null)
                {
                    _imageTag = InitImageTag();
                }

                if (_imageTag.TryGetValue(key, out s))
                {
                    return s;
                }
            }
            catch (Exception ex)
            {

            }
            return ImageType.None;
        }



        #endregion


        #region 截屏
        /// <summary>
        /// 截屏
        /// </summary>
        /// <param name="cbx"></param>
        public static void ScreenShot(ChatBox cbx)
        {
            CaptureImageTool capture = new CaptureImageTool();
            if (capture.ShowDialog() == DialogResult.OK)
            {
                Image image = capture.Image;
                cbx.InsertImage(image);
                cbx.Focus();
                cbx.ScrollToCaret();
            }
        }
        #endregion
 
        public static  bool isVibrationing = false;

        #region Vibration 震动

        /// <summary>
        /// 通过线程 进行震动
        /// </summary>
        /// <param name="cfwObj"></param>
        public static void VibrationThread(object cfwObj)
        {
            if (threadVibration == null)
            {
                threadVibration = new Thread(new ParameterizedThreadStart(Common.Vibration));
            }
            if (threadVibration != null && threadVibration.ThreadState != ThreadState.Running)
            {
                switch (threadVibration.ThreadState)
                {
                    case ThreadState.Unstarted:
                        threadVibration.Start(cfwObj);
                        break;
                    case ThreadState.Stopped:
                        threadVibration.Abort();// (cfwObj);
                        threadVibration = new Thread(new ParameterizedThreadStart(Common.Vibration));
                        goto case ThreadState.Unstarted;
                }
                
            }
        }

        //震动方法
        public static void Vibration(object cfwObj)
        {
            ChatFormForWeb cfw = cfwObj as ChatFormForWeb;
            if (!Common.isVibrationing)
            {
              
                cfw.Invoke(new Action(() => {
                isVibrationing = true;
                Point pOld = cfw.Location;//原来的位置
                int radius = 3;//半径
                for (int n = 0; n < 3; n++) //旋转圈数
                {
                    //右半圆逆时针
                    for (int i = -radius; i <= radius; i++)
                    {
                        int x = Convert.ToInt32(Math.Sqrt(radius * radius - i * i));
                        int y = -i;

                        cfw.Location = new Point(pOld.X + x, pOld.Y + y);
                        System.Threading.Thread.Sleep(10);
                    }
                    //左半圆逆时针
                    for (int j = radius; j >= -radius; j--)
                    {
                        int x = -Convert.ToInt32(Math.Sqrt(radius * radius - j * j));
                        int y = -j;

                        cfw.Location = new Point(pOld.X + x, pOld.Y + y);
                        System.Threading.Thread.Sleep(10);
                    }
                }
                //抖动完成，恢复原来位置
                cfw.Location = pOld;
                isVibrationing = false;
                }));
            }
        }


        /// <summary>
        /// 生成系统震动消息
        /// </summary>
        /// <param name="selfID"></param>
        /// <param name="VibrationSenderID"></param>
        /// <returns></returns>
        public static string VibrationMessage(bool isSelf, string VibrationSenderID)
        {
            string message = string.Empty;
            if (isSelf)
            {
                message = SysParams.Vibration_Send;

            }
            else
            {
                message = string.Format(SysParams.Vibration_Receive, VibrationSenderID); //VibrationSenderID+"给您发送了一个窗口抖动。";
            }
            return message;
        }

        #endregion

        #region 文件发送

        

        public static string File_Message(bool isSelf, string fileName,long fileSize,bool isSender)
        {
            string message = string.Empty;
            string sendStr = "发送";
            if (!isSender)
            {
                sendStr = "接收";
            }
            if (isSelf)
            {
                
                message = string.Format(SysParams.File_Cancel_Send,fileName, Common.FormatFileSize(fileSize), sendStr);
            }
            else
            {
                message = string.Format(SysParams.File_Cancel_Receive, sendStr, fileName,Common.FormatFileSize(fileSize)); 
            }
            return message;
        }

        public static string File_OffLine_Message(bool isSelf, string fileName, long fileSize, bool isSender)
        {
            string message = string.Format(SysParams.File_OFFLine_Success, fileName, Common.FormatFileSize(fileSize));
            return message;
        }
        #endregion

        #region 聊天记录

        /// <summary>
        /// 返回临时服务器状态
        /// </summary>
        /// <returns></returns>
        public static string retStatus_FileService()
        {
            //string url = string.Empty;
            string result = RestHelper.GetDataWaitTime(null,SysParams.FileServer, SysParams.FileServerStatus, 
                SysParams.WebTimeOut, RestSharp.Method.GET);
            return result;
        }

        public static DataTable retTalkHistory(string user1, string user2)
        {
            //WBS_TalkHistory.WBS_TalkHistory wt = new WBS_TalkHistory.WBS_TalkHistory();
            //wt.Url = SysParams.TalkService;
            //string result = wt.retTalkHistory(user1, user2);
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict["USER1"] = user1;
            dict["USER2"] = user2;
            string result = RestHelper.GetDataWaitTime(dict,SysParams.TalkServiceRoot, SysParams.TalkServiceResult, SysParams.WebTimeOut, RestSharp.Method.POST);
            if (!string.IsNullOrEmpty(result))
            {
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);
                return dt;
            }
            return null;
        }
        #endregion

        /// <summary>
        /// html信息过滤
        /// </summary>
        /// <param name="chatConent"></param>
        /// <returns></returns>
        public static string ChatTextFilter(string chatConent)
        {
          string txt= chatConent.Replace("\r\n", "<br //>").Replace("\n", "<br //>").Replace("\\", "\\\\").Replace("'", "\\'");
            return txt;
        }
    }
}
