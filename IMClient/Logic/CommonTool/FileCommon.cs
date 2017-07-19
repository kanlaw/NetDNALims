using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace IMClient.Logic
{
    public partial class Common
    {

        #region 文件下载


        /// <summary>
        /// 检查修改文件名
        /// </summary>
        /// <param name="fileSaveName"> 文件名</param>
        /// <param name="tmpDir"> 文件保存路径 </param>
        public static string CheckAndChangeFileName(string fileSaveName, string tmpDir)
        {

            string tmp_fileSave = tmpDir + fileSaveName;
            if (File.Exists(tmp_fileSave))//覆盖
            {
                DialogResult dr = MessageBox.Show("是否覆盖当前文件?", "提示", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        File.Delete(tmpDir + fileSaveName);//删除原文件
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                }
                else//重命名
                {
                    int index = 1;
                    string tmpName = fileSaveName.Substring(0, fileSaveName.LastIndexOf('.'));
                    string tmpType = fileSaveName.Substring(fileSaveName.LastIndexOf('.'));
                    while (true)
                    {
                        string tName = tmpDir + tmpName + "(" + index + ")" + tmpType;
                        if (!File.Exists(tName))
                        {
                            fileSaveName = tmpName + "(" + index + ")" + tmpType;
                            break;
                        }
                        index++;
                    }
                }
            }

            return fileSaveName;
        }

        /// <summary>
        /// 下载-离线文件
        /// </summary>
        /// <param name="fileId">唯一Id</param>
        /// <param name="UpLoadFileName">上载文件名</param>
        ///  <param name="DownloadFileName">下载文件名</param>
        /// <param name="saveDir">文件保存目录</param>
        public static void DownLoad_OffLine_2(string fileId,string UpLoadFileName,string DownloadFileName,string saveDir,
            int upSize,
            DataRow drmission)
        {
            //比如uri=http://localhost/Rabom/1.rar;iis就需要自己配置了。
            string uri = SysParams.FileServer+SysParams.FileServer_tmpFile +"/"+ fileId+"/"+ UpLoadFileName;
            //截取文件名
            string fileName = UpLoadFileName;
            //构造文件完全限定名,准备将网络流下载为本地文件
            string fileFullName = saveDir+ DownloadFileName+SysParams.tmpDownLoadName;
            //构造文件的配置文件的完全完全限定名
            //string fileCfgName = saveDir + fileName + ".cfg";

            //本地构造文件流
            FileStream fs;
            //本地配置文件流
            FileStream fsCfg;
            if (File.Exists(fileFullName))
            {
                fs = new FileStream(fileFullName, FileMode.Append, FileAccess.Write, FileShare.Write); 
                //如果存在配置文件，则继续下载
                //if (File.Exists(fileCfgName))
                //{
                //    fs = new FileStream(fileFullName, FileMode.Append, FileAccess.Write, FileShare.Write);
                //    fsCfg = fs = new FileStream(fileFullName, FileMode.Append, FileAccess.Write, FileShare.Write);
                //}
                //else
                //{
                //    return;//暂且这样
                //}
            }
            else
            {
                fs = new FileStream(fileFullName, FileMode.Create);
                //fsCfg = new FileStream(fileCfgName, FileMode.Create);
            }
            long currentLength = fs.Length;
            //开辟内存空间
            byte[] buffer = new byte[upSize];
            //请求地址
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            //请求开始位置
            request.AddRange(fs.Length);
            //获取网络流
            Stream ns = request.GetResponse().GetResponseStream();
            //获取文件实际长度
            long contentLength = request.GetResponse().ContentLength;
            //获取文件读取到的长度
            int length = ns.Read(buffer, 0, buffer.Length);
            while (length > 0)
            {
                if (currentLength < contentLength)
                {
                    //将字节数组写入流
                    fs.Write(buffer, 0, buffer.Length);
                }
                //继续下载
                buffer = new byte[upSize];
                length = ns.Read(buffer, 0, buffer.Length);
                currentLength += upSize;
                lock (drmission.Table)//更新已上传量
                {
                    drmission["UploadSize"] = fs.Length;
                }
            }
            ns.Close();
            fs.Close();
            //fsCfg.Close();
        }


        public static bool DownLoad_OffLine(string fileId, string UpLoadFileName, string DownloadFileName, string saveDir,
            int upSize,
            DataRow drmission)
        {
            bool flag = false;

            string uri = SysParams.FileServer + SysParams.FileServer_tmpFile + "/" + fileId + "/" + UpLoadFileName;
            //截取文件名
            string fileName = UpLoadFileName;
            //构造文件完全限定名,准备将网络流下载为本地文件
            string fileFullName = saveDir + DownloadFileName + SysParams.tmpDownLoadName;

            //打开上次下载的文件
            long SPosition = 0;
            //实例化流对象
            FileStream FStream;
            //判断要下载的文件夹是否存在
            if (File.Exists(fileFullName))
            {
                //打开要下载的文件
                FStream = File.OpenWrite(fileFullName);
                //获取已经下载的长度
                SPosition = FStream.Length;
                FStream.Seek(SPosition, SeekOrigin.Current);
            }
            else
            {
                //文件不保存创建一个文件
                FStream = new FileStream(fileFullName, FileMode.Create);
                SPosition = 0;
            }
            try
            {
                //打开网络连接
                HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(uri);
                if (SPosition > 0)
                    myRequest.AddRange((int)SPosition);             //设置Range值
                //向服务器请求,获得服务器的回应数据流
                Stream myStream = myRequest.GetResponse().GetResponseStream();
                //定义一个字节数据
                byte[] btContent = new byte[upSize];
                int intSize = 0;
                intSize = myStream.Read(btContent, 0, upSize);
                while (intSize > 0)
                {
                    FStream.Write(btContent, 0, intSize);
                    intSize = myStream.Read(btContent, 0, upSize);
                    lock (drmission.Table)//更新已上传量
                    {
                        drmission["UploadSize"] = FStream.Length;
                    }
                }
                //关闭流
                FStream.Close();
                myStream.Close();
                flag = true;        //返回true下载成功
            }
            catch (Exception)
            {
                FStream.Close();
                flag = false;       //返回false下载失败
            }
            return flag;
        }

        #endregion

        #region 文件上传


        /// <summary>
        ///  上传文件（自动分割）-离线上传
        /// </summary>
        /// <param name="filePath">待上传的文件全路径名称</param>
        /// <param name="hostURL">服务器的地址</param>
        /// <param name="byteCount">分割的字节大小 字节大小</param>        
        /// <param name="current">当前字节指针</param>
        /// <returns>成功返回"";失败则返回错误信息</returns>
        public static string UpLoadFile_OffLine(string filePath, string hostURL, int byteCount, 
            long current, Dictionary<string, string> DictParams,DataRow drmission)
        {
            string tmpURL = hostURL;
            byteCount = byteCount * 1024;

            System.Net.WebClient WebClientObj = new System.Net.WebClient();
            FileStream fStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);


            BinaryReader bReader = new BinaryReader(fStream);
            long length = fStream.Length;
            string sMsg = "上传成功";
            string fileName = filePath.Substring(filePath.LastIndexOf('\\') + 1);
            try
            {

                #region 续传处理
                byte[] data;
                if (current > 0)
                {
                    fStream.Seek(current, SeekOrigin.Current);
                }
                #endregion

                #region 分割文件上传
                for (; current <= length; current = current + byteCount)
                {
                    if (current + byteCount > length)
                    {
                        data = new byte[Convert.ToInt64((length - current))];
                        bReader.Read(data, 0, Convert.ToInt32((length - current)));
                    }
                    else
                    {
                        data = new byte[byteCount];
                        bReader.Read(data, 0, byteCount);
                    }

                    try
                    {


                        //***                        bytes 21010-47021/47022
                        WebClientObj.Headers.Remove(HttpRequestHeader.ContentRange);
                        WebClientObj.Headers.Add(HttpRequestHeader.ContentRange, "bytes " + current + "-" + (current + byteCount) + "/" + fStream.Length);

                        //hostURL = tmpURL + "?filename=" + mdfstr + Path.GetExtension(fileName);
                        hostURL = tmpURL;// + "?filename=" +fileName;
                        if (DictParams != null && DictParams.Keys.Count > 0)
                        {
                            hostURL += "?";
                            foreach (string key in DictParams.Keys)
                            {
                                hostURL += (key + "=" + DictParams[key]) + "&";
                                // + "?filename=" + fileName;
                            }
                            hostURL = hostURL.Substring(0, hostURL.Length - 1);

                        }
                        byte[] byRemoteInfo = WebClientObj.UploadData(hostURL, "POST", data);
                        string sRemoteInfo = System.Text.Encoding.Default.GetString(byRemoteInfo);

                        //  获取返回信息
                        if (sRemoteInfo.Trim() != "")
                        {
                            sMsg = sRemoteInfo;
                          
                            break;

                        }
                        lock (drmission.Table)//更新已上传量
                        {
                            drmission["UploadSize"] = current + byteCount;
                            //Controls.ItemUploadFile item = (Controls.ItemUploadFile)drmission["item"];
                        }
                    }
                    catch (Exception ex)
                    {
                        sMsg = ex.ToString();
                        break;
                    }
                    #endregion

                }
            }
            catch (Exception ex)
            {
                sMsg = sMsg + ex.ToString();
            }
            try
            {
                bReader.Close();
                fStream.Close();
            }
            catch (Exception exMsg)
            {
                sMsg = exMsg.ToString();
            }

            GC.Collect();
            return sMsg;
        }


        /// <summary>
        /// 上传文件（自动分割）
        /// </summary>
        /// <param name="filePath">待上传的文件全路径名称</param>
        /// <param name="hostURL">服务器的地址</param>
        /// <param name="byteCount">分割的字节大小</param>        
        /// <param name="cruuent">当前字节指针</param>
        /// <returns>成功返回"";失败则返回错误信息</returns>
        public static string UpLoadFile(string filePath, string hostURL, int byteCount, long cruuent,Dictionary<string,string> DictParams)
        {
            string tmpURL = hostURL;
            byteCount = byteCount * 1024;


            System.Net.WebClient WebClientObj = new System.Net.WebClient();
            FileStream fStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);


            BinaryReader bReader = new BinaryReader(fStream);
            long length = fStream.Length;
            string sMsg = "上传成功";
            string fileName = filePath.Substring(filePath.LastIndexOf('\\') + 1);
            try
            {

                #region 续传处理
                byte[] data;
                if (cruuent > 0)
                {
                    fStream.Seek(cruuent, SeekOrigin.Current);
                }
                #endregion

                #region 分割文件上传
                for (; cruuent <= length; cruuent = cruuent + byteCount)
                {
                    if (cruuent + byteCount > length)
                    {
                        data = new byte[Convert.ToInt64((length - cruuent))];
                        bReader.Read(data, 0, Convert.ToInt32((length - cruuent)));
                    }
                    else
                    {
                        data = new byte[byteCount];
                        bReader.Read(data, 0, byteCount);
                    }

                    try
                    {


                        //***                        bytes 21010-47021/47022
                        WebClientObj.Headers.Remove(HttpRequestHeader.ContentRange);
                        WebClientObj.Headers.Add(HttpRequestHeader.ContentRange, "bytes " + cruuent + "-" + (cruuent + byteCount) + "/" + fStream.Length);

                        //hostURL = tmpURL + "?filename=" + mdfstr + Path.GetExtension(fileName);
                        hostURL = tmpURL;// + "?filename=" +fileName;
                        if (DictParams != null && DictParams.Keys.Count > 0)
                        {
                            hostURL += "?";
                            foreach (string key in DictParams.Keys)
                            {
                                hostURL += (key + "=" + DictParams[key]) + "&";
                                // + "?filename=" + fileName;
                            }
                            hostURL = hostURL.Substring(0, hostURL.Length - 1);

                        }
                        byte[] byRemoteInfo = WebClientObj.UploadData(hostURL, "POST", data);
                        string sRemoteInfo = System.Text.Encoding.Default.GetString(byRemoteInfo);

                        //  获取返回信息
                        if (sRemoteInfo.Trim() != "")
                        {
                            sMsg = sRemoteInfo;
                            break;

                        }
                    }
                    catch (Exception ex)
                    {
                        sMsg = ex.ToString();
                        break;
                    }
                    #endregion

                }
            }
            catch (Exception ex)
            {
                sMsg = sMsg + ex.ToString();
            }
            try
            {
                bReader.Close();
                fStream.Close();
            }
            catch (Exception exMsg)
            {
                sMsg = exMsg.ToString();
            }

            GC.Collect();
            return sMsg;
        }


        /// <summary>
        /// 上传文件（自动分割）
        /// </summary>
        /// <param name="filePath">待上传的文件全路径名称</param>
        /// <param name="hostURL">服务器的地址</param>
        /// <param name="byteCount">分割的字节大小</param>        
        /// <param name="cruuent">当前字节指针</param>
        /// <returns>成功返回"";失败则返回错误信息</returns>
        public static string UpLoadFileByMemoryStream(string filePath, string hostURL, int byteCount, 
            long cruuent, MemoryStream ms)
        {
            string tmpURL = hostURL;
            byteCount = byteCount * 1024;


            System.Net.WebClient WebClientObj = new System.Net.WebClient();
            //FileStream fStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            
            BinaryReader bReader = new BinaryReader(ms);
            long length = ms.Length;
            string sMsg = "上传成功";
            string fileName = filePath.Substring(filePath.LastIndexOf('\\') + 1);
            try
            {

                #region 续传处理
                byte[] data;
                //if (cruuent > 0)
                //{
                //    ms.Seek(cruuent, SeekOrigin.Current);
                //}
                #endregion

                #region 分割文件上传
                for (; cruuent <= length; cruuent = cruuent + byteCount)
                {
                    if (cruuent + byteCount > length)
                    {
                        data = new byte[Convert.ToInt64((length - cruuent))];
                        bReader.Read(data, 0, Convert.ToInt32((length - cruuent)));
                    }
                    else
                    {
                        data = new byte[byteCount];
                        bReader.Read(data, 0, byteCount);
                    }

                    try
                    {


                        //***                        bytes 21010-47021/47022
                        WebClientObj.Headers.Remove(HttpRequestHeader.ContentRange);
                        WebClientObj.Headers.Add(HttpRequestHeader.ContentRange, "bytes " + cruuent + "-" + (cruuent + byteCount) + "/" + ms.Length);

                        //hostURL = tmpURL + "?filename=" + mdfstr + Path.GetExtension(fileName);
                        hostURL = tmpURL + "?filename=" + fileName;
                        byte[] byRemoteInfo = WebClientObj.UploadData(hostURL, "POST", data);
                        string sRemoteInfo = System.Text.Encoding.Default.GetString(byRemoteInfo);

                        //  获取返回信息
                        if (sRemoteInfo.Trim() != "")
                        {
                            sMsg = sRemoteInfo;
                            break;

                        }
                    }
                    catch (Exception ex)
                    {
                        sMsg = ex.ToString();
                        break;
                    }
                    #endregion

                }
            }
            catch (Exception ex)
            {
                sMsg = sMsg + ex.ToString();
            }
            try
            {
                bReader.Close();
                ms.Close();
            }
            catch (Exception exMsg)
            {
                sMsg = exMsg.ToString();
            }

            GC.Collect();
            return sMsg;
        }

        /// <summary>
        /// 上传文件（自动分割）
        /// </summary>
        /// <param name="filePath">待上传的文件全路径名称</param>
        /// <param name="hostURL">服务器的地址</param>
        /// <param name="byteCount">分割的字节大小</param>        
        /// <param name="cruuent">当前字节指针</param>
        /// <returns>成功返回"";失败则返回错误信息</returns>
        public static string UpLoadFileByBytes(string filePath, string hostURL, int byteCount,
            long cruuent, string mdfstr, byte[] content,Dictionary<string,string> DictParams)
        {
            string tmpURL = hostURL;
            byteCount = byteCount * 1024;


            System.Net.WebClient WebClientObj = new System.Net.WebClient();
            //FileStream fStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);


            //BinaryReader bReader = new BinaryReader(ms);
            
        
            long length = content.Length; //ms.Length;
            string sMsg = "上传成功";
            string fileName = filePath.Substring(filePath.LastIndexOf('\\') + 1);
            try
            {

                #region 续传处理
                byte[] data;
                //if (cruuent > 0)
                //{
                //    fStream.Seek(cruuent, SeekOrigin.Current);
                //}
                #endregion

                #region 分割文件上传
                for (; cruuent <= length; cruuent = cruuent + byteCount)
                {
                    if (cruuent + byteCount > length)
                    { 
                        long a = 0;
                        data = new byte[byteCount];
                        for (long i = cruuent; i < length; a++, i++)
                        {
                            data[a] = content[i];
                        }
                    }
                    else
                    {
                        data = new byte[byteCount];
                        //bReader.Read(data, 0, byteCount);
                        long a = 0;
                        for ( long i = cruuent; i < cruuent + byteCount;a++, i++)
                        {
                            data[a] = content[i];
                        }
                    }

                    try
                    {


                        //***                        bytes 21010-47021/47022
                        WebClientObj.Headers.Remove(HttpRequestHeader.ContentRange);
                        WebClientObj.Headers.Add(HttpRequestHeader.ContentRange, "bytes " + cruuent + "-" + (cruuent + byteCount) + "/" + content.Length);

                        //hostURL = tmpURL + "?filename=" + mdfstr + Path.GetExtension(fileName);
                        hostURL = tmpURL;
                        if (DictParams != null && DictParams.Keys.Count > 0)
                        {
                            hostURL += "?";
                            foreach (string key in DictParams.Keys)
                            {
                                hostURL += (key + "=" + DictParams[key]) + "&";
                               // + "?filename=" + fileName;
                            }
                            hostURL = hostURL.Substring(0, hostURL.Length - 1);
                            
                        }
                        byte[] byRemoteInfo = WebClientObj.UploadData(hostURL, "POST", data);
                        string sRemoteInfo = System.Text.Encoding.Default.GetString(byRemoteInfo);

                        //  获取返回信息
                        if (sRemoteInfo.Trim() != "")
                        {
                            sMsg = sRemoteInfo;
                            break;

                        }
                    }
                    catch (Exception ex)
                    {
                        sMsg = ex.ToString();
                        break;
                    }
                    #endregion

                }
            }
            catch (Exception ex)
            {
                sMsg = sMsg + ex.ToString();
            }
            try
            {
                //bReader.Close();
                //ms.Close();
            }
            catch (Exception exMsg)
            {
                sMsg = exMsg.ToString();
            }

            GC.Collect();
            return sMsg;
        }


        #endregion


        #region
        /// <summary>
        /// 根据头文件判断文件真实格式
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static FileType IsAllowedExtension(string filePath)
        {

            using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    string fileclass = "";
                    // byte buffer;
                    try
                    {

                        //buffer = reader.ReadByte();
                        //fileclass = buffer.ToString();
                        //buffer = reader.ReadByte();
                        //fileclass += buffer.ToString();

                        for (int i = 0; i < 2; i++)
                        {
                            fileclass += reader.ReadByte().ToString();
                        }

                    }
                    catch (Exception)
                    {

                        fileclass = "-1";
                    }
                    switch (int.Parse(fileclass))
                    {
                        case (int)FileType.jpg:
                            break;
                        case (int)FileType.gif:
                            break;
                        case (int)FileType.bmp:
                            break;
                        case (int)FileType.office03:
                            break;
                        case (int)FileType.office07:
                            break;
                        default:
                            return FileType.unKnow;
                    }
                    return FileType.unKnow;
                    //if (fileclass == "255216")
                    //{
                    //    return true;
                    //}
                    //else
                    //{
                    //    return false;
                    //}
                }
           
            }
            /*文件扩展名说明
             * 255216 jpg
             * 208207 doc xls ppt wps
             * 8075 docx pptx xlsx zip
             * 5150 txt
             * 8297 rar
             * 7790 exe
             * 3780 pdf      
             * 
             * 4946/104116 txt
             * 7173        gif 
             * 255216      jpg
             * 13780       png
             * 6677        bmp
             * 239187      txt,aspx,asp,sql
             * 208207      xls.doc.ppt
             * 6063        xml
             * 6033        htm,html
             * 4742        js
             * 8075        xlsx,zip,pptx,mmap,zip
             * 8297        rar   
             * 01          accdb,mdb
             * 7790        exe,dll
             * 5666        psd 
             * 255254      rdp 
             * 10056       bt种子 
             * 64101       bat 
             * 4059        sgf    
             */

        }

        /// <summary>
        /// 根据文件扩展名获取文件类型
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static FileType FileTypeByFileName(string fileName)
        {
            string fileType = fileName;
            if (!string.IsNullOrEmpty(fileName) && fileName.Contains("."))
            {
                fileType = fileName.Substring(fileName.LastIndexOf(".")+1);
            }
            switch (fileType.ToLower())
            {
                case "jpg":
                    return FileType.jpg;
                case "jpeg":
                    return FileType.jpg;
                case "gif":
                    return FileType.gif;
                case "bmp":
                    return FileType.bmp;
                case "png":
                    return FileType.png;
                case "xls":
                    return FileType.xls;
                case "xlsx":
                    return FileType.xls;
                case "xlsb":
                    return FileType.xls;
                case "xlsm":
                    return FileType.xls;
                case "doc":
                    return FileType.doc;
                case "docx":
                    return FileType.doc;
                case "dot":
                    return FileType.doc;
                case "docm":
                    return FileType.doc;
                case "ppt":
                    return FileType.ppt;
                case "pptx":
                    return FileType.ppt;
                case "pot":
                    return FileType.ppt;
                case "txt":
                    return FileType.txt;
                case "text":
                    return FileType.txt;
                default:
                    return FileType.unKnow;
            }
        }


        /// <summary>
        /// 根据文件名 加载文件缩略图Path
        /// </summary>
        /// <param name="FileName"></param>
        public static string  retIcoPathByFileName(string FileName)
        {
            string icoPath = AppDomain.CurrentDomain.BaseDirectory + SysParams.Sys_ImagePath + "unknown-im-ico_03.png";

            string sysIcoPath = AppDomain.CurrentDomain.BaseDirectory + SysParams.Sys_ImagePath;
            Logic.FileType ft = Logic.Common.FileTypeByFileName(FileName);

            switch (ft)
            {
                case Logic.FileType.gif:
                    goto case Logic.FileType.jpg;
                case Logic.FileType.bmp:
                    icoPath = AppDomain.CurrentDomain.BaseDirectory + SysParams.Sys_ImagePath + "bmp.ico";
                    break;
                case Logic.FileType.png:
                    icoPath = AppDomain.CurrentDomain.BaseDirectory + SysParams.Sys_ImagePath + "png.ico";
                    break;
                case Logic.FileType.jpg:
                    //if (System.IO.File.Exists(FileName))
                    //{
                    //    icoPath = FileName;
                    //}
                    icoPath = AppDomain.CurrentDomain.BaseDirectory + SysParams.Sys_ImagePath + "jpg.ico";
                    break;
                case Logic.FileType.xls:
                    icoPath = AppDomain.CurrentDomain.BaseDirectory + SysParams.Sys_ImagePath + "excel-im-ico_03.png";
                    break;
                case Logic.FileType.doc:
                    icoPath = AppDomain.CurrentDomain.BaseDirectory + SysParams.Sys_ImagePath + "word-im-ico_03.png";
                    break;
                case Logic.FileType.ppt:
                    icoPath = AppDomain.CurrentDomain.BaseDirectory + SysParams.Sys_ImagePath + "ppt-im-ico_03.png";
                    break;
                case Logic.FileType.txt:
                    icoPath = AppDomain.CurrentDomain.BaseDirectory + SysParams.Sys_ImagePath + "txt.ico";
                    break;
                default:
                    icoPath = AppDomain.CurrentDomain.BaseDirectory + SysParams.Sys_ImagePath + "unknown-im-ico_03.png";
                    break;


            }
            return icoPath;
            //this.icoImage = Image.FromFile(icoPath);
        }
        /// <summary>
        /// html路径 根据文件名 加载Html支持的系统缩略图路劲
        /// </summary>
        /// <param name="FileName"></param>
        public static string retIcoHtmlPathByFileName(string FileName)
        {
            string defaultPath = SysParams.Html_SysImagePath + "unknown-im-ico_03_03.png";

            string icoPath = string.Empty;
            Logic.FileType ft = Logic.Common.FileTypeByFileName(FileName);

            switch (ft)
            {
                case Logic.FileType.gif:
                    goto case Logic.FileType.jpg;
                case Logic.FileType.bmp:
                    icoPath = "bmp.ico";
                    break;
                case Logic.FileType.png:
                    icoPath =  "png.ico";
                    break;
                case Logic.FileType.jpg:
                    //if (System.IO.File.Exists(FileName))
                    //{
                    //    icoPath = FileName;
                    //}
                    icoPath =  "jpg.ico";
                    break;
                case Logic.FileType.xls:
                    icoPath = "excel-im-ico_03_03.png";
                    break;
                case Logic.FileType.doc:
                    icoPath = "word-im-ico_03_03.png";
                    break;
                case Logic.FileType.ppt:
                    icoPath = "ppt-im-ico_03_03.png";
                    break;
                case Logic.FileType.txt:
                    icoPath =  "txt.ico";
                    break;
                default:
                    icoPath = "unknown-im-ico_03_03.png";
                    break;


            }
            if (!string.IsNullOrEmpty(icoPath))
            {
                defaultPath = SysParams.Html_SysImagePath + icoPath;
            }
            return defaultPath;
            //this.icoImage = Image.FromFile(icoPath);
        }


        /// <summary>
        /// 显示文件大小
        /// </summary>
        /// <param name="fileSize"></param>
        /// <returns></returns>
        public static String FormatFileSize(Int64 fileSize)
        {
            if (fileSize < 0)
            {
                throw new ArgumentOutOfRangeException("fileSize");
            }
            else if (fileSize >= 1024 * 1024 * 1024)
            {
                return string.Format("{0:########0.00} GB", ((Double)fileSize) / (1024 * 1024 * 1024));
            }
            else if (fileSize >= 1024 * 1024)
            {
                return string.Format("{0:####0.00} MB", ((Double)fileSize) / (1024 * 1024));
            }
            else if (fileSize >= 1024)
            {
                return string.Format("{0:####0.00} KB", ((Double)fileSize) / 1024);
            }
            else
            {
                return string.Format("{0} bytes", fileSize);
            }
        }

        #region 判断文件是否被占用
        [DllImport("kernel32.dll")]
        public static extern IntPtr _lopen(string lpPathName, int iReadWrite);

        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr hObject);

        public const int OF_READWRITE = 2;
        public const int OF_SHARE_DENY_NONE = 0x40;
        public  static IntPtr HFILE_ERROR = new IntPtr(-1);

        /// <summary>
        /// 判断你是否被占用
        /// </summary>
        /// <param name="vFileName"></param>
        /// <returns></returns>
        public static bool FileIsLoading(string vFileName)
        {
            try
            {
                IntPtr vHandle = _lopen(vFileName, OF_READWRITE | OF_SHARE_DENY_NONE);
                if (vHandle == HFILE_ERROR)
                {
                    //MessageBox.Show("文件被占用！");
                    return true;
                }
                CloseHandle(vHandle);
                return false;
               
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 判断文件是否被占用
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool IsFileInUse(string fileName)
        {
            try
            {
                bool inUse = true;
                FileStream fs = null;
                try
                {
                    fs = new FileStream(fileName, FileMode.Open, FileAccess.Read,
                    FileShare.None);
                    inUse = false;
                }
                catch
                {
                }
                finally
                {
                    if (fs != null)
                    {
                        fs.Close();
                        //fs.Dispose();
                    }
                }
                return inUse;//true表示正在使用,false没有使用 
            }
            catch { }
            return false;
        }
        #endregion

        #endregion
    }
}
