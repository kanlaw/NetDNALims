using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using System.Windows.Forms;
using System.IO;
using System.Net;
//using RX.DNA.LimsClient.Logic.Model.Compare;
using System.Configuration;
namespace IMClient.Logic
{
    #region
    public delegate void Logincallback(IRestResponse response);
    public static class RestHelper
    {
        /// <summary>
        /// 获取RestFul数据
        /// </summary>
        /// <param name="Param">键值参数集合</param>
        /// <param name="serviceName">rest所在方法的服务名</param>
        /// <param name="Method">调用方法名</param>
        /// <returns>Json串</returns>
        public static string GetDataWaitTime(Dictionary<string, string> Param, string Service,
            string Method,int waitTime, RestSharp.Method method)
        {
            //获取指定模块的Rest客户端对象
            //var client = new RestClient(SysParams.TalkServiceRoot);

            var client = new RestClient(Service);
            //设置Rest对象超时时间3秒
            client.Timeout = waitTime;//300秒
            client.MaxRedirects = int.MaxValue;
            //组合请求方法Routing参数
            //string strreq = LRC.GenerateRestString("USER/LOGIN_CS", "");
            string strreq = Method;//LRC.GenerateRestString(Method, "");
            var request = new RestRequest(strreq, method);
            if (Param != null)
            {
                foreach (var item in Param)
                {
                    request.AddParameter(item.Key.Trim(), item.Value.Trim());
                }
            }

            IRestResponse res = client.Execute(request);

            return res.Content;
        }



    #endregion

       
        //发送消息到服务器
        public static string HttpConnectToServer(string ServerPage, string strXml, string strData)
        {
            string postData = "commitid=" + strData;
            byte[] dataArray = Encoding.Default.GetBytes(postData);
            //创建请求
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(ServerPage);
            request.Method = "POST";
            request.ContentLength = dataArray.Length;
            request.ContentType = "application/x-www-form-urlencoded";
            //创建输入流
            Stream dataStream = null;
            try
            {
                dataStream = request.GetRequestStream();
            }
            catch (Exception)
            {
                return null;//连接服务器失败
            }
            //发送请求
            dataStream.Write(dataArray, 0, dataArray.Length);
            dataStream.Close();
            //读取返回消息
            string res = string.Empty;
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                res = reader.ReadToEnd();
                reader.Close();
            }
            catch (Exception ex)
            {
                return null;//连接服务器失败
            }
            return res;
        }
    }
}
