using CefSharp.WinForms;
using IMClient.XMPP.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace IMClient.Logic
{
    public class TempFile
    {
        /// <summary>
        /// 文件上传链接
        /// </summary>
        public static string FileUploadWebSite = SysParams.FileServer; //"http://192.168.1.88:1111/";
        //public static string FileUploadWebSite = "http://localhost:10413/";
        /// <summary>
        /// 远程图片保存地址
        /// </summary>
        public static string FileDownloadWebSite = FileUploadWebSite+"img/";
       // public static string FileDownloadWebSite = "http://localhost:10413/img/";

        /// <summary>
        /// 默认头像资源-本地路径
        /// </summary>
        public static string defaultImgPath = "htm\\Source\\default\\head\\default.jpg";
        /// <summary>
        /// 默认头像资源-html路劲
        /// </summary>
        public static string defaultImgPath_html = "Source/default/head/default.jpg";
        public static string headImgMainPath = "htm\\Source\\";
        public static string headImgDirName = "\\head\\";


        /// <summary>
        /// 聊天记录记录中图片html路径
        /// </summary>
        public static string talkImg_Html = "source/{0}/img/{1}";

        /// <summary>
        /// 头像HTML的路径
        /// </summary>
        public static string talkImgHead_Html = "source/{0}/Head/{1}";

        /// <summary>
        /// 根据UID 获取用户头像信息 html
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static string retImgHeadByUserName(string uid)
        {
            //string defaultPath = AppDomain.CurrentDomain.BaseDirectory + defaultImgPath;
            string defaultPath = AppDomain.CurrentDomain.BaseDirectory + defaultImgPath;

            if (!File.Exists(defaultPath))
            {
                return string.Empty;
            }
            defaultPath = defaultImgPath_html;

            string userHeadPath = headImgMainPath + uid + headImgDirName;
            if (Directory.Exists(userHeadPath))
            {
                DirectoryInfo dinfo = new DirectoryInfo(userHeadPath);
                if (dinfo.GetFiles().Length > 0)
                {
                    defaultPath = string.Format(talkImgHead_Html, new string[] { uid, dinfo.GetFiles()[0].Name });
                }
            }

            return defaultPath;
        }


        /// <summary>
        /// 根据UID 获取用户头像信息 本地路劲
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static string retImgHeadByUserName_Localhost(string uid)
        {
            //string defaultPath = AppDomain.CurrentDomain.BaseDirectory + defaultImgPath;
            string defaultPath = AppDomain.CurrentDomain.BaseDirectory + defaultImgPath;

            if (!File.Exists(defaultPath))
            {
                return string.Empty;
            }
            // defaultPath = defaultImgPath_html;

            string userHeadPath = headImgMainPath + uid + headImgDirName;
            if (Directory.Exists(userHeadPath))
            {
                DirectoryInfo dinfo = new DirectoryInfo(userHeadPath);
                if (dinfo.GetFiles().Length > 0)
                {
                    defaultPath = AppDomain.CurrentDomain.BaseDirectory + userHeadPath + dinfo.GetFiles()[0].Name;
                }
            }

            return defaultPath;
        }

        /// <summary>
        ///  根据用户ID 获取头像
        /// </summary>
        /// <param name="uid"></param>
        /// <returns>string.Emtpy 说明用户头像没有下载 使用默认头像</returns>
        public static string retImgHeadByUserName_2(string uid)
        {
            //string defaultPath = AppDomain.CurrentDomain.BaseDirectory + defaultImgPath;
            string defaultPath = string.Empty ;


            string userHeadPath = headImgMainPath + uid + headImgDirName;
            if (Directory.Exists(userHeadPath))
            {
                DirectoryInfo dinfo = new DirectoryInfo(userHeadPath);
                if (dinfo.GetFiles().Length > 0)
                {
                    defaultPath = AppDomain.CurrentDomain.BaseDirectory + userHeadPath + dinfo.GetFiles()[0].Name;
                }
            }

            return defaultPath;
        }


        /// <summary>
        /// 获取默认头像
        /// </summary>
        /// <param name="headImage"></param>
        public static byte[] GetDefaultHeadImage()
        {
            string defaultPath = AppDomain.CurrentDomain.BaseDirectory + defaultImgPath;
            byte[]  headImage = File.ReadAllBytes(defaultPath);
            return headImage;
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static bool Download_Head(string path, string uid, ref byte[] headImage)
        {
  

            bool result = true;

            string savePath = AppDomain.CurrentDomain.BaseDirectory + headImgMainPath + "{0}" + headImgDirName + "{1}";
           
            savePath = string.Format(savePath, new string[] { uid, SysParams.Sys_DefaultHeadImageFileName });
            string defaultPath = AppDomain.CurrentDomain.BaseDirectory + defaultImgPath;

            //byte[] b;
            using (NewWebClient wc = new NewWebClient(1*1000))
            {
                try
                {

                    if (!Directory.Exists(headImgMainPath + uid))
                    {
                        Directory.CreateDirectory(headImgMainPath + uid);

                    }
                    if (!Directory.Exists(headImgMainPath + uid + headImgDirName))
                    {
                        Directory.CreateDirectory(headImgMainPath + uid + headImgDirName);
                    }

                    string headImageUrl = path;

                    headImage = wc.DownloadData(headImageUrl);
                    File.WriteAllBytes(savePath, headImage);

                }
                catch (Exception ex)
                {
                    result = true;

                    //throw;
                }
                finally {
                    if (headImage == null)
                    {
                        headImage = File.ReadAllBytes(defaultPath);
                    }
                }
            }
            return result;
        }


        /// <summary>
        /// 下载图片2 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static byte[] DownLoad_Head_2(string path, string uid)
        {

            string savePath = AppDomain.CurrentDomain.BaseDirectory + headImgMainPath + "{0}" + headImgDirName + "{1}";

            savePath = string.Format(savePath, new string[] { uid, SysParams.Sys_DefaultHeadImageFileName });
            string defaultPath = AppDomain.CurrentDomain.BaseDirectory + defaultImgPath;

            byte[] headImage = null;
            using (NewWebClient wc = new NewWebClient(3 * 1000))
            {
                try
                {

                    if (!Directory.Exists(headImgMainPath + uid))
                    {
                        Directory.CreateDirectory(headImgMainPath + uid);

                    }
                    if (!Directory.Exists(headImgMainPath + uid + headImgDirName))
                    {
                        Directory.CreateDirectory(headImgMainPath + uid + headImgDirName);
                    }

                    string headImageUrl = path;
                    headImage = wc.DownloadData(headImageUrl);
                    File.WriteAllBytes(savePath, headImage);
                    return headImage;

                }
                catch (Exception ex)
                {
                    //result = true;
                    return null;
                    //throw;
                }
                finally
                {
                   
                }
            }
        }


      /// <summary>
      /// 下载头像3
      ///  检查头像名称
      /// </summary>
      /// <param name="path"></param>
      /// <param name="uid"></param>
      /// <param name="imgName"></param>
      /// <returns></returns>
        public static byte[] DownLoad_Head_3(string path, string uid,string imgName)
        {

            //判断本地是否存在头像图片

            string savePath = AppDomain.CurrentDomain.BaseDirectory + headImgMainPath + "{0}" + headImgDirName + "{1}";

            savePath = string.Format(savePath, new string[] { uid, imgName });
            byte[] headImage = null;
            if (File.Exists(savePath))//本地存在，直接获取本地
            {
                headImage= File.ReadAllBytes(savePath);//
                return headImage;
            }
            else{
                string defaultPath = AppDomain.CurrentDomain.BaseDirectory + defaultImgPath;


                using (NewWebClient wc = new NewWebClient(3 * 1000))
                {
                    try
                    {

                        if (!Directory.Exists(headImgMainPath + uid))
                        {
                            Directory.CreateDirectory(headImgMainPath + uid);

                        }
                        if (!Directory.Exists(headImgMainPath + uid + headImgDirName))
                        {
                            Directory.CreateDirectory(headImgMainPath + uid + headImgDirName);
                        }

                        string headImageUrl = path;
                        headImage = wc.DownloadData(headImageUrl);
                        File.WriteAllBytes(savePath, headImage);
                        return headImage;

                    }
                    catch (Exception ex)
                    {
                        //result = true;
                        return null;
                        //throw;
                    }
                    finally
                    {

                    }
                }
            }
        }


        /// <summary>
        /// 上传照片
        /// </summary>
        /// <param name="uri">上传地址</param>
        /// <param name="path">本地文件路径</param>
        /// <returns></returns>
        public static string UpLoad_Image(string uri, string path)
        {
            //@" http://localhost:1111/"
            string result = string.Empty ;
            if (File.Exists(path))
            {
                WebClient wc = new WebClient();
                string fileName =Guid.NewGuid().ToString().Replace("-","")+ path.Substring(path.LastIndexOf("."));
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                try
                {
                    byte[] postArray = br.ReadBytes((int)fs.Length);
                    Uri u = new Uri(uri + fileName + ".abc");
                    Stream postStream = wc.OpenWrite(u, "POST");
                    if (postStream.CanWrite)
                    {
                        postStream.Write(postArray, 0, postArray.Length);
                    }
                    else
                    {
                    }
                    postStream.Close();
                }
                catch (WebException errMsg)
                {
                }
                result = FileDownloadWebSite + fileName;
            }
           
            return result;
        }


        /// <summary>
        ///  本系统产生的图片 上传照片
        /// </summary>
        /// <param name="uri">上传地址</param>
        /// <param name="path">本地文件路径</param>
        /// <returns></returns>
        public static string UpLoad_ImageStream(string uri, Image img)
        {
            //@" http://localhost:1111/"
            string fileName =  Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
            //_imageTag = InitImageTag();

            if (img!=null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    WebClient wc = new WebClient();

                    

                    img.Save(ms,ImageFormat.Jpeg);
                    try
                    {
                       
                        Uri u = new Uri(uri + fileName + ".abc");
             
                        Stream postStream = wc.OpenWrite(u, "POST");
                        if (postStream.CanWrite)
                        {
                            ms.CopyTo(postStream);
                            byte[] postArray = ms.ToArray();
                            postStream.Write(postArray, 0, postArray.Length);
                        }
                        else
                        {
                        }
                        postStream.Close();
                    }
                    catch (WebException errMsg)
                    {
                    }
                }
            }
            fileName = uri + "/img/" + fileName;
            return fileName;
        }


        


        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="uri">下载地址 含文件名</param>
        /// <param name="path">本地保存地址 不含文件名</param>
        /// <returns></returns>
        public static bool DownLoad_Image(string uri,ref string path)
        {
            bool result = true;
            try
            {
                //http://localhost:1111/"
                string fileName = uri.Substring(uri.LastIndexOf('/')+1);
                WebClient wc = new WebClient();
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = path + "\\"+ fileName;
                if (!File.Exists(path))
                {
                    wc.DownloadFile(uri, path);
                }
               
            }
            catch (Exception)
            {

                throw;
            }
         
            return result;
        }

        /// <summary>
        /// 保存临时文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="UID"></param>
        /// <returns></returns>
        public static string saveTalkImg(string path, string UID)
        {
            string result = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
            string source = AppDomain.CurrentDomain.BaseDirectory + headImgMainPath + UID;
            if (!Directory.Exists(source))
            {
                Directory.CreateDirectory(source);
            }
            string source_img = source + "\\img";
            if (!Directory.Exists(source_img))
            {
                Directory.CreateDirectory(source_img);
            }
            try
            {
                File.Copy(path, source_img + "\\" + result, true);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;

        }

        public static string saveTalkImg(Image img, string UID)
        {
            string result = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
            string source = AppDomain.CurrentDomain.BaseDirectory + headImgMainPath + UID;
            if (!Directory.Exists(source))
            {
                Directory.CreateDirectory(source);
            }
            string source_img = source + "\\img";
            if (!Directory.Exists(source_img))
            {
                Directory.CreateDirectory(source_img);
            }
            try
            {
                img.Save(source_img + "\\" + result);
                //File.Copy(path, source_img + "\\" + result, true);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;

        }



       
    }
}
