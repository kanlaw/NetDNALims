using DNANET.Data;
using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace DNANET.Service
{
    #region ServiceResult

    /// <summary>
    /// 不带数据的服务响应数据协定
    /// </summary>
    [DataContract]
    [Serializable]
    public class ServiceResult
    {
        public const int S_OK = 0;

        public ServiceResult()
        {
            this.Code = 0;
            this.Message = "OK";
        }

        public ServiceResult(int code, string message)
        {
            this.Code = code;
            this.Message = message;
        }

        /// <summary>
        /// 响应代码
        /// </summary>
        [DataMember(Name = "code")]
        public int Code { get; set; }

        /// <summary>
        /// 响应消息
        /// </summary>
        [DataMember(Name = "msg")]
        public string Message { get; set; }
    }

    /// <summary>
    /// 带数据的服务响应数据协定
    /// </summary>
    /// <typeparam name="TData">服务成功调用时返回的数据类型</typeparam>
    [DataContract]
    [Serializable]
    public class ServiceResult<TData> : ServiceResult
    {
        public ServiceResult(TData data)
            : base()
        {
            this.Data = data;
        }

        public ServiceResult(int code, string message)
            : base(code, message)
        {
        }

        public ServiceResult(int code, string message, TData data)
            : base(code, message)
        {
            this.Data = data;
        }

        /// <summary>
        /// 响应数据正文
        /// </summary>
        [DataMember(Name = "data")]
        public TData Data { get; set; }
    }

    #endregion

    [ServiceContract]
    public interface IUserService
    {
        [WebInvoke(Method = "POST", UriTemplate = "/login")]
        ServiceResult<UserInformation> Login(string login, string password);

        [WebInvoke(Method = "POST", UriTemplate = "/register")]
        ServiceResult<UserInformation> Register(string user, string password, params SetItem[] request);

        [WebInvoke(Method = "POST", UriTemplate = "/{uid}/{service}/register")]
        ServiceResult RegisterUserService(string uid, string service, params SetItem[] request);

        [WebInvoke(Method = "POST", UriTemplate = "/update")]
        ServiceResult Update(params string[] uids);

        [WebInvoke(Method = "POST", UriTemplate = "/find?q={fullQuery}")]
        ServiceResult<UserInformation[]> FindUserInformation(bool fullQuery, string[] querys);

        [WebInvoke(Method = "GET", UriTemplate = "/{uid}")]
        ServiceResult<UserInformation> GetUserInformation(string uid);

        [WebInvoke(Method = "POST", UriTemplate = "/{uid}")]
        ServiceResult<UserInformation> SetUserInformation(string uid, params SetItem[] request);

        [WebInvoke(Method = "POST", UriTemplate = "/{uid}/resetPassword")]
        ServiceResult ResetPassword(string uid, string newPassword);
    }
}