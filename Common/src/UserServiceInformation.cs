using System;
using System.Runtime.Serialization;

namespace DNANET.Data
{
    [DataContract]
    [Serializable]
    public enum UserServiceStatus
    {
        OK = 0,
        Error = 1,
        Register = 2,
        Change = 3
    }

    /// <summary>
    /// 用户服务描述信息
    /// </summary>
    [DataContract]
    [Serializable]
    public class UserServiceInformation
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        [DataMember(Name = "service")]
        public string Service { get; set; }

        /// <summary>
        /// 服务数据
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public object Data { get; set; }

        /// <summary>
        /// 服务状态
        /// </summary>
        [DataMember(Name = "status")]
        public UserServiceStatus Status { get; set; }

        /// <summary>
        /// 服务状态原因消息
        /// </summary>
        [DataMember(Name = "msg")]
        public string Message { get; set; }
    }
}