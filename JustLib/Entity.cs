using System;
using System.Collections.Generic;
using System.Text;
using ESBasic;
using System.Drawing;
using System.IO;

namespace JustLib
{
    #region IUnit
    public interface IUnit
    {
        string ID { get; }
        string Name { get; }
        int Version { get; set; }
        string LastWords { get; }

        bool IsGroup { get; }

        object Tag { get; set; }
        Parameter<string, string> GetIDName();
    } 
    #endregion

     #region IUser
    public interface IUser : IUnit
    {
       
        List<string> GroupList { get; }
        UserStatus UserStatus { get; set; }
        object Tag { get; set; }
        Dictionary<string, List<string>> FriendDicationary { get; }
        string Signature { get; }
        string DefaultFriendCatalog { get; }

        /// <summary>
        /// 如果为空字符串，则表示位于组织外。
        /// </summary>
        string Department { get; }
        string[] OrgPath { get; }
        bool IsInOrg { get; } 

        List<string> GetAllFriendList();
        List<string> GetFriendCatalogList();       
    }
    #endregion

    #region IGroup
    public interface IGroup : IUnit
    {
        string CreatorID { get; }

        List<string> MemberList { get; }

        void AddMember(string userID);
        void RemoveMember(string userID);

    }
    #endregion

    public interface IHeadImageGetter
    {
        Image GetHeadImage(IUser user);
    }

    public interface IUserInformationForm
    {
        void SetUser(IUser user);
    }

    //在线状态  
    public enum UserStatus
    {
        Online = 2,
        Away = 3,
        Busy = 4,
        DontDisturb = 5,
        OffLine = 6,
        Hide = 7
    }

    public enum GroupChangedType
    {
        /// <summary>
        /// 成员的资料发生变化
        /// </summary>
        MemberInfoChanged = 0,
        /// <summary>
        /// 组的资料（如组名称、公告等）发生变化
        /// </summary>
        GroupInfoChanged,
        SomeoneJoin,     
        SomeoneQuit,
        GroupDeleted
    }

    #region ContactRTDatas
    public class UserRTData
    {
        public UserRTData() { }
        public UserRTData(UserStatus status, int ver)
        {
            this.UserStatus = status;
            this.Version = ver;
        }

        public UserStatus UserStatus { get; set; }
        public int Version { get; set; }
    }

    public class ContactRTDatas
    {
        public Dictionary<string, UserRTData> UserStatusDictionary { get; set; }
        public Dictionary<string, int> GroupVersionDictionary { get; set; }
    }
    #endregion


    #region
    [Serializable]
    public class GGUser : IUser
    {
        #region Force Static Check
        public const string TableName = "GGUser";
        public const string _UserID = "UserID";
        public const string _PasswordMD5 = "PasswordMD5";
        public const string _Name = "Name";
        public const string _Department = "Department";
        public const string _Signature = "Signature";
        public const string _HeadImageIndex = "HeadImageIndex";
        public const string _HeadImageData = "HeadImageData";
        public const string _HeadImageUrl = "HeadImageUrl";
        public const string _Groups = "Groups";
        public const string _Friends = "Friends";
        public const string _DefaultFriendCatalog = "DefaultFriendCatalog";
        public const string _CreateTime = "CreateTime";
        public const Matrix.Jid _Jid = null;
        public const string _UserUid = "UserUid";
        public const string _UserHeadImageUser = "UserUid";
        public const string _UserAge = "UserAge";
        public const string _UserCompany = "UserCompany";
        public const string _UserName = "UserName";
        public const string _UserRole = "UserRole";
        public const string _UserSex = "UserSex";



        #endregion

        public GGUser() { }
        public GGUser(string id, string pwd, string _name, string _friends, string _signature, int headIndex, string _groups, Matrix.Jid jid, UserStatus userstatus)
        {
            this.UserID = id;
            this.passwordMD5 = pwd;
            this.Name = _name;
            this.friends = _friends;
            this.Signature = _signature;
            this.HeadImageIndex = headIndex;
            this.groups = _groups;
            JID = jid;
            userStatus = userstatus;

        }

        #region HeadImageUrl
        /// <summary>
        /// 读取头像
        /// </summary>
        private bool isRead = false;

        private string headImageUrl = "";
        /// <summary>
        /// 好友头像对应的url
        /// </summary>
        public string HeadImageUrl
        {
            get { return headImageUrl; }
            set { headImageUrl = value; }
        }

        public Image DefaultImage = null;

        #endregion

        #region UserSex
        private string userSex = "";
        /// <summary>
        /// 好友姓别
        /// </summary>
        public string UserSex
        {
            get { return userSex; }
            set { userSex = value; }
        }
        #endregion

        #region UserRole
        private string userRole = "";
        /// <summary>
        /// 好友姓名
        /// </summary>
        public string UserRole
        {
            get { return userRole; }
            set { userRole = value; }
        }
        #endregion  

        #region UserName
        private string userName = "";
        /// <summary>
        /// 好友姓名
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        #endregion

        #region UserCompany
        private string userCompany = "";
        /// <summary>
        /// 好友单位
        /// </summary>
        public string UserCompany
        {
            get { return userCompany; }
            set { userCompany = value; }
        }
        #endregion  

        #region UserAge
        private string userAge = "";
        /// <summary>
        /// 好友年龄
        /// </summary>
        public string UserAge
        {
            get { return userAge; }
            set { userAge = value; }
        }
        #endregion 

        #region UserUid
        private string userUid = "";
        /// <summary>
        /// 好友在用户表中uid
        /// </summary>
        public string UserUid
        {
            get { return userUid; }
            set { userUid = value; }
        }
        #endregion 


        #region UserID
        private Matrix.Jid jid = "";
        /// <summary>
        /// Matrix.Jid类（包含用户信息）
        /// </summary>
        public Matrix.Jid JID
        {
            get { return jid; }
            set { jid = value; }
        }
        #endregion 



        #region PasswordMD5
        private string passwordMD5 = "";
        /// <summary>
        /// 登录密码(MD5加密)。
        /// </summary>
        public string PasswordMD5
        {
            get { return passwordMD5; }
            set { passwordMD5 = value; }
        }
        #endregion       

        #region UserID
        private string userID = "";
        /// <summary>
        /// 用户登录帐号。
        /// </summary>
        public string UserID
        {
            get { return userID; }
            set { userID = value; }
        }
        #endregion

        #region Name
        private string name = "";
        /// <summary>
        /// 昵称
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        #endregion        

        #region Friends
        private string friends = "";
        /// <summary>
        /// 好友。如 我的好友：10000,10001,1234;家人:1200,1201 。
        /// </summary>
        public string Friends
        {
            get { return friends; }
            set
            {
                friends = value;
                this.friendDicationary = null;
                this.allFriendList = null;
            }
        }

        #region 非DB字段
        private Dictionary<string, List<string>> friendDicationary = null;
        /// <summary>
        /// 好友ID的分组。非DB字段。
        /// </summary>
        public Dictionary<string, List<string>> FriendDicationary
        {
            get
            {
                if (this.friendDicationary == null)
                {
                    if (string.IsNullOrEmpty(this.friends))
                    {
                        this.friends = "我的好友:";
                    }
                    this.friendDicationary = new Dictionary<string, List<string>>();
                    string[] catalogs = this.friends.Split(';');
                    foreach (string catalog in catalogs)
                    {
                        string[] ary = catalog.Split(':');
                        string catalogName = ary[0];
                        List<string> friends = new List<string>(ary[1].Split(','));
                        if (friends.Count == 1)
                        {
                            friends.Remove("");
                        }
                        this.friendDicationary.Add(catalogName, friends);
                    }
                }
                return friendDicationary;
            }
        }

        private List<string> allFriendList = null;
        public List<string> GetAllFriendList()
        {
            if (this.allFriendList == null)
            {
                List<string> list = new List<string>();
                foreach (List<string> tmp in this.FriendDicationary.Values)
                {
                    list.AddRange(tmp);
                }
                this.allFriendList = list;
            }

            return this.allFriendList;
        }

        private string GetFriendsVal(Dictionary<string, List<string>> friendDic)
        {
            StringBuilder sb = new StringBuilder("");
            int count = 0;
            foreach (KeyValuePair<string, List<string>> pair in friendDic)
            {
                if (count > 0)
                {
                    sb.Append(";");
                }
                string ff = ESBasic.Helpers.StringHelper.ContactString(pair.Value, ",");
                sb.Append(string.Format("{0}:{1}", pair.Key, ff));
                ++count;
            }
            return sb.ToString();
        }
        #endregion

        public void AddFriend(string friendID, string catalog)
        {
            if (!this.FriendDicationary.ContainsKey(catalog))
            {
                return;
            }
            if (this.FriendDicationary[catalog].Contains(friendID))
            {
                return;
            }

            this.FriendDicationary[catalog].Add(friendID);
            this.friends = this.GetFriendsVal(this.friendDicationary);
            this.allFriendList = null;
        }

        public void RemoveFriend(string friendID)
        {
            foreach (KeyValuePair<string, List<string>> pair in this.FriendDicationary)
            {
                pair.Value.Remove(friendID);
            }

            this.friends = this.GetFriendsVal(this.friendDicationary);
            this.allFriendList = null;
        }

        public void ChangeFriendCatalogName(string oldName, string newName)
        {
            if (!this.FriendDicationary.ContainsKey(oldName))
            {
                return;
            }

            List<string> merged = new List<string>();
            if (this.FriendDicationary.ContainsKey(newName))
            {
                merged = this.FriendDicationary[newName];
                this.FriendDicationary.Remove(newName);
            }
            List<string> friends = this.friendDicationary[oldName];
            friends.AddRange(merged);
            this.FriendDicationary.Remove(oldName);
            this.FriendDicationary.Add(newName, friends);
            this.friends = this.GetFriendsVal(this.friendDicationary);
            if (oldName == this.defaultFriendCatalog)
            {
                this.defaultFriendCatalog = newName;
            }
        }

        public void AddFriendCatalog(string name)
        {
            if (this.FriendDicationary.ContainsKey(name))
            {
                return;
            }

            this.FriendDicationary.Add(name, new List<string>());
            this.friends = this.GetFriendsVal(this.friendDicationary);
        }

        public void RemvoeFriendCatalog(string name)
        {
            if (!this.FriendDicationary.ContainsKey(name) || this.defaultFriendCatalog == name)
            {
                return;
            }

            this.FriendDicationary.Remove(name);
            this.friends = this.GetFriendsVal(this.friendDicationary);
        }

        public void MoveFriend(string friendID, string oldCatalog, string newCatalog)
        {
            if (!this.FriendDicationary.ContainsKey(oldCatalog) || !this.FriendDicationary.ContainsKey(newCatalog))
            {
                return;
            }
            this.friendDicationary[oldCatalog].Remove(friendID);
            if (!this.friendDicationary[newCatalog].Contains(friendID))
            {
                this.friendDicationary[newCatalog].Add(friendID);
            }
            this.friends = this.GetFriendsVal(this.friendDicationary);
        }

        public List<string> GetFriendCatalogList()
        {
            return new List<string>(this.FriendDicationary.Keys);
        }
        #endregion

        #region Groups
        private string groups = "";
        /// <summary>
        /// 该用户所属的组。组ID用英文逗号隔开
        /// </summary>
        public string Groups
        {
            get { return groups; }
            set
            {
                groups = value;
                this.groupList = null;
            }
        }

        #region 非DB字段
        private List<string> groupList = null;
        /// <summary>
        /// 所属组ID的数组。非DB字段。
        /// </summary>
        public List<string> GroupList
        {
            get
            {
                if (this.groupList == null)
                {
                    this.groupList = new List<string>(this.groups.Split(','));
                    if (this.groupList.Count == 1)
                    {
                        this.groupList.Remove("");
                    }
                }
                return groupList;
            }
        }
        #endregion
        public void JoinGroup(string groupID)
        {
            if (this.GroupList.Contains(groupID))
            {
                return;
            }
            this.GroupList.Add(groupID);
            this.groups = ESBasic.Helpers.StringHelper.ContactString(this.GroupList, ",");
        }

        public void QuitGroup(string groupID)
        {
            this.GroupList.Remove(groupID);
            this.groups = ESBasic.Helpers.StringHelper.ContactString(this.GroupList, ",");
        }
        #endregion        

        #region CreateTime
        private DateTime createTime = DateTime.Now;
        public DateTime CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }
        #endregion

        #region Signature
        private string signature = "";
        /// <summary>
        /// 签名
        /// </summary>
        public string Signature
        {
            get { return signature; }
            set { signature = value; }
        }
        #endregion

        #region HeadImageIndex
        private int headImageIndex = 0;
        /// <summary>
        /// 头像图片的索引。如果为-1，表示自定义头像。
        /// </summary>
        public int HeadImageIndex
        {
            get { return headImageIndex; }
            set
            {
                headImageIndex = value;
                //this.headIcon = null;
            }
        }
        #endregion

        #region HeadImageData
        private byte[] headImageData = null;
        public byte[] HeadImageData
        {
            get {
                if (this.headImage == null)
                {
                    string defaultPath = AppDomain.CurrentDomain.BaseDirectory + "htm\\Source\\default\\head\\default.jpg";
                    this.headImageData = File.ReadAllBytes(defaultPath);
                }
                return headImageData; }
            set
            {
                headImageData = value;
                this.headImage = null;
                this.headImageGrey = null;
                //this.headIcon = null;
            }
        }
        #endregion

        #region DefaultFriendCatalog
        private string defaultFriendCatalog = "我的好友";
        /// <summary>
        /// 默认好友分组。不能被删除。
        /// </summary>
        public string DefaultFriendCatalog
        {
            get
            {
                if (string.IsNullOrEmpty(this.defaultFriendCatalog))
                {
                    this.defaultFriendCatalog = "我的好友";
                }
                return defaultFriendCatalog;
            }
            set { defaultFriendCatalog = value; }
        }
        #endregion

        #region Version
        private int version = 0;
        public int Version
        {
            get { return version; }
            set { version = value; }
        }
        #endregion

        #region 非DB字段
        #region HeadImage
        [NonSerialized]
        private Image headImage = null;
        /// <summary>
        /// 自定义头像。非DB字段。
        /// </summary>
        public Image HeadImage
        {
            get
            {
                if (this.headImage == null && this.headImageData != null)
                {
                    this.headImage = ESBasic.Helpers.ImageHelper.Convert(this.headImageData);
                    //this.headImage = Image.FromFile("http://192.168.1.93/DNALIMS/upload/148835237592534%E7%99%BB%E5%BD%9502.png");
                }
                return headImage;
            }
        }
        #endregion

        #region HeadImageGrey
        [NonSerialized]
        private Image headImageGrey = null;
        /// <summary>
        /// 自定义头像。非DB字段。
        /// </summary>
        public Image HeadImageGrey
        {
            get
            {
                if (this.headImageGrey == null && this.headImageData != null)
                {
                    this.headImageGrey = ESBasic.Helpers.ImageHelper.ConvertToGrey(this.HeadImage);
                }
                return this.headImageGrey;
            }
        }
        #endregion

        //#region GetHeadIcon
        //[NonSerialized]
        //private Icon headIcon = null;
        ///// <summary>
        ///// 自定义头像。非DB字段。
        ///// </summary>
        //public Icon GetHeadIcon(Image[] defaultHeadImages)
        //{
        //    if (this.headIcon != null)
        //    {
        //        return this.headIcon;
        //    }

        //    if (this.HeadImage != null)
        //    {
        //        this.headIcon = ImageHelper.ConvertToIcon(this.headImage, 64);
        //        return this.headIcon;
        //    }

        //    this.headIcon = ImageHelper.ConvertToIcon(defaultHeadImages[this.headImageIndex], 64);
        //    return this.headIcon;
        //}
        #endregion

        #region UserStatus
        private UserStatus userStatus = UserStatus.OffLine;
        /// <summary>
        /// 在线状态。非DB字段。
        /// </summary>

        public UserStatus UserStatus
        {
            get { return userStatus; }
            set { userStatus = value; }
        }
        #endregion 

        #region Tag
        private object tag;
        /// <summary>
        /// 可用于存储 LastWordsRecord。
        /// </summary>

        public object Tag
        {
            get { return tag; }
            set { tag = value; }
        }
        #endregion

        #region LastWords
        //public string LastWords
        //{
        //    get
        //    {
        //        if (this.tag == null)
        //        {
        //            return "";
        //        }

        //        LastWordsRecord record = this.tag as LastWordsRecord;
        //        if (record == null)
        //        {
        //            return "";
        //        }

        //        string content = record.ChatBoxContent.GetTextWithPicPlaceholder("[图]");
        //        return string.Format("{0}： {1}", record.IsMe ? "我" : "TA", content);
        //    }
        //}
        #endregion
        #endregion

        #region OnlineOrHide
        public bool OnlineOrHide
        {
            get
            {
                return this.userStatus != UserStatus.OffLine;
            }
        }
        #endregion

        #region OfflineOrHide
        public bool OfflineOrHide
        {
            get
            {
                return this.userStatus == UserStatus.OffLine || this.userStatus == UserStatus.Hide;
            }
        }
        #endregion

        #region IEntity Members
        public System.String GetPKeyValue()
        {
            return this.UserID;
        }
        #endregion     

        public override string ToString()
        {
            return string.Format("{0}({1})-{2}，Ver：{3}", this.name, this.UserID, this.userStatus, this.version);
        }

        public Parameter<string, string> GetIDName()
        {
            return new Parameter<string, string>(this.UserID, this.Name);
        }

        #region PartialCopy
        [NonSerialized]
        private GGUser partialCopy = null;
        public GGUser PartialCopy
        {
            get
            {
                if (this.partialCopy == null)
                {
                    this.partialCopy = (GGUser)this.MemberwiseClone();
                    this.partialCopy.Groups = "";
                    this.partialCopy.Friends = "";
                }
                else
                {
                    this.partialCopy.userStatus = this.userStatus;
                }
                return this.partialCopy;
            }
        }
        #endregion

        #region IUser 接口
        public string ID
        {
            get { return this.userID; }
        }

        public bool IsGroup
        {
            get { return false; }
        }

        #endregion

        public string Department
        {
            get { return ""; }
        }

        public List<string> FriendList
        {
            get { return this.GetAllFriendList(); }
        }

        public string[] OrgPath
        {
            get { return null; }
        }


        public bool IsInOrg
        {
            get { return false; }
        }

        public string LastWords
        {
            get
            {
                //throw new NotImplementedException();
                return "";
            }

        }

        public bool IsRead { get => isRead; set => isRead = value; }
    }
    #region
    #endregion
}
