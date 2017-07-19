using System;
using System.Runtime.Serialization;

namespace DNANET.Data
{
    /// <summary>
    /// 用户信息
    /// </summary>
    [DataContract]
    [Serializable]
    public class UserInformation
    {
        public UserInformation()
        {
            this.OP();
        }

        /// <summary>
        /// 用户唯一性ID
        /// </summary>
        [DataMember(Name = "uid")]
#if UID_I4
        public int UID { get; set; }
#else
        public Guid UID { get; set; }
#endif

        /// <summary>
        /// 用户名称
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        [DataMember(Name = "sex", EmitDefaultValue = false)]
        public string Sex { get; set; }

        [DataMember(Name = "head", EmitDefaultValue = false)]
        public string Head { get; set; }

        [DataMember(Name = "company", EmitDefaultValue = false)]
        public string Company { get; set; }

        [DataMember(Name = "job", EmitDefaultValue = false)]
        public string Job { get; set; }
        
        [DataMember(Name = "role", EmitDefaultValue = false)]
        public int Role { get; set; }

        [DataMember(Name = "state", EmitDefaultValue = false)]
        public int State { get; set; }

        /// <summary>
        /// 用户服务信息
        /// </summary>
        [DataMember(Name = "services", EmitDefaultValue = false)]
        public UserServiceInformation[] Services { get; set; }

        [IgnoreDataMember]
        public byte[] ResetPassword { get; set; }

        [IgnoreDataMember]
        internal DateTime LastAccess { get; private set; }

        public void OP()
        {
            this.LastAccess = DateTime.Now;
        }
    }
}