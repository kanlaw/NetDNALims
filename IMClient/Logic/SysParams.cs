using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using DNANET.Data;
using JustLib;
using CefSharp.WinForms;

namespace IMClient.Logic
{
    public partial class SysParams
    {
        #region 系统登录 静态变量

        //public static string UserJID = string.Empty;


        /// <summary>
        /// 登录用户的好友UID集合
        /// </summary>
        //TODO:储存用户好友的UID
        public static List<String> userFriendUids = null;


        /// <summary>
        /// 登录用户
        /// </summary>
        public static UserInformation LoginUser = null;

        /// <summary>
        /// 登录用户的servers
        /// </summary>
        public static Dictionary<string,string> serversDic = null;

        /// <summary>
        /// 登录用户头像
        /// </summary>
        public static Image loginUserImage = defaultHead;

        /// <summary>
        /// 当前用户好友列表
        /// </summary>
        public static List<GGUser> AllFriendList = null;

        #endregion;

        //public static ChromiumWebBrowser browser_Emcee;
        //public static ChromiumWebBrowser browser_Actor;
        #region 绘制参数
        /// <summary>
        /// 所有窗体的标题高度
        /// </summary>
        public const int Paint_title_Height = 35;

        /// <summary>
        /// 详细信息-头像尺寸
        /// </summary>
        public static Size HeadImageDetailSize = new Size(80, 80);

        /// <summary>
        /// 标题填充背景颜色
        /// </summary>
        public static Color Paint_title_Color = Color.FromArgb( 0,75, 146);

        /// <summary>
        /// 标题字体
        /// </summary>
        public static Font Paint_title_Font = new Font("微软雅黑", 10.0f, FontStyle.Bold);

        /// <summary>
        /// 标题字体颜色
        /// </summary>
        public static Color Paint_title_Font_Color = Color.White;

        /// <summary>
        /// 标题初始位置
        /// </summary>
        public static Point Paint_title_Point = new Point(7, 7);

        /// <summary>
        /// 分割线颜色
        /// </summary>
        public static Color Paint_SplitLine_Color = Color.FromArgb(195, 195, 195);

        #endregion

        #region 系统默认功能页面
        /// <summary>
        /// 系统首页
        /// </summary>
        public static string Page_FirstPage = "http://{0}/"+ConfigurationManager.AppSettings["FirstPage"];
        /// <summary>
        /// 建设指南
        /// </summary>
        public static string Page_ConstructionGuide = "http://{0}/" + ConfigurationManager.AppSettings["ConstructionGuide"];

        /// <summary>
        /// 技术指导
        /// </summary>
        public static string Page_TechnicalGuidance = "http://{0}/" + ConfigurationManager.AppSettings["TechnicalGuidance"];

        /// <summary>
        /// 实战协同
        /// </summary>
        public static string Page_ActualCooperation = "http://{0}/" + ConfigurationManager.AppSettings["ActualCooperation"];

        /// <summary>
        /// 登录实战协同
        /// </summary>
        public static string Page_IsLoginActualCooperation = "http://{0}/" + ConfigurationManager.AppSettings["IsLoginActualCooperation"];

        /// <summary>
        /// 学习交流
        /// </summary>
        public static string Page_Study = "http://{0}/" + ConfigurationManager.AppSettings["Study"];

        /// <summary>
        /// 科技创新
        /// </summary>
        public static string Page_Science = "http://{0}/" + ConfigurationManager.AppSettings["Science"];

        /// <summary>
        /// 系统管理/个人中心
        /// </summary>
        public static string Page_SysManage = "http://{0}/" + ConfigurationManager.AppSettings["SysManage"];

        /// <summary>
        /// 个人工作平台
        /// </summary>
        public static string Page_PersonalInfo = "http://{0}/" + ConfigurationManager.AppSettings["PersonalInfo"]+ "?UID={1}";

        /// <summary>
        /// 个人用户信息
        /// </summary>
        public static string Page_PersonalInfo2 = "http://{0}/" + ConfigurationManager.AppSettings["PersonalInfo2"] + "?UID={1}";

        /// <summary>
        /// 用户头像
        /// </summary>
        public static string Head_ImageInfo = "http://"+ConfigurationManager.AppSettings["HeadImageServer"]+ConfigurationManager.AppSettings["HeadImage"];

        /// <summary>
        /// 用户注册
        /// </summary>
        public static string Page_Register = "http://{0}/" + ConfigurationManager.AppSettings["Register"];

        /// <summary>
        /// 邮箱登陆
        /// </summary>
        public static string Page_Mail = "http://{0}/" + ConfigurationManager.AppSettings["Mail"];



        #endregion

        #region 所有服务器地址

        public static string MailWeb= ConfigurationManager.AppSettings["MailWeb"];

        /// <summary>
        /// 应用服务器地址
        /// </summary>
        public static string AppServer = ConfigurationManager.AppSettings["AppServer"];


        /// <summary>
        /// 邮箱服务器 
        /// </summary>
        public static string MailServer = ConfigurationManager.AppSettings["MailServer"];

        /// <summary>
        /// OpenFile服务地址
        /// </summary>
        public static string Server = ConfigurationManager.AppSettings["Server"];

        /// <summary>
        /// 文件服务器
        /// </summary>
        public static string FileServer = ConfigurationManager.AppSettings["FileServer"];

    

        #endregion

        #region 聊天记录 基本参数
        /// <summary>
        /// 聊天记录获取地址
        /// </summary>
        public static string TalkService = ConfigurationManager.AppSettings["TalkService"];

        /// <summary>
        /// 文件服务器状态
        /// </summary>
        public static string FileServerStatus = "FileStatus.status";

        /// <summary>
        /// RestFul 聊天记录
        /// </summary>
        public static string TalkServiceResult = "/TalkService/TALKHISTORY";

        /// <summary>
        /// 测试连接方法
        /// </summary>
        public static string TalkServiceTest = "/TalkService/HelloWorld";

        /// <summary>
        /// 会议室 添加会议
        /// </summary>
        public static string MeetingService_AddMeeting = "/MeetingService/AddMeeting";

        /// <summary>
        /// 会议室 是否参加会议室
        /// </summary>
        public static string MeetingService_AttendMeeting = "/MeetingService/Meeting/ATTEND";
      
        /// <summary>
        /// 会议室 根据JID获取用户信息
        /// </summary>
        public static string MeetingService_UserInfo_JID = "/MeetingService/Meeting/QUERY/JID";

        
        /// <summary>
        /// 会议室 修改会议室状态
        /// </summary>
        public static string MeetingService_UPDATESTATUS = "/MeetingService/UpdateMeetingStatus";

        /// <summary>
        /// 更新投票选项的数量
        /// </summary>
        public static string MeetingService_VOTE_COUNTUPDATE = "/MeetingService/Meeting/VOTE/COUNTUPDATE";
         

        /// <summary>
        /// 会议室 添加成员
        /// </summary>
        public static string MeetingService_AddMembers = "/MeetingService/AddMeetingMember";

        /// <summary>
        /// 会议室 添加选项
        /// </summary>
        public static string MeetingService_AddOptions = "/MeetingService/AddMeetingOption";

        /// <summary>
        /// 会议室查询会议
        /// </summary>
        public static string MeetingService_Query= "/MeetingService/Meeting/QUERY";

        /// <summary>
        /// 会议室查询根据GUID
        /// </summary>
        public static string MeetingService_Guid = "/MeetingService/Meeting/GUID";

        /// <summary>
        /// 反馈会议成员信息
        /// </summary>
        public static string MeetingService_UserInfo = "/MeetingService/Meeting/QUERY/USERINFO";

        /// <summary>
        /// restful根节点
        /// </summary>
        public static string TalkServiceRoot = ConfigurationManager.AppSettings["TalkServiceRoot"];

        public static string FileServer_tmpFile = ConfigurationManager.AppSettings["FileServer_TempFile"];

        ///MeetingService/AddMeeting
        //public static string MeetgingServiceRoot = ConfigurationManager.AppSettings["MeetingServiceRoot"];

        #endregion

        #region 本地库 基本参数
        /// <summary>
        /// 本地库名称
        /// </summary>
        public static string Sys_LocalDB_Name=ConfigurationManager.AppSettings["LocalDB"];

        /// <summary>
        /// 本地Sql文件存放位置
        /// </summary>
        public static string Sys_LocalDB_SQL_Dir= ConfigurationManager.AppSettings["LocalDB_SQL_Dir"] ;





        /// <summary>
        /// 本地库 建表语句
        /// </summary>
        public static string Sys_LocalDB_SQL = ConfigurationManager.AppSettings["LocalSQL"];

        /// <summary>
        /// 本地离线任务记录表名称
        /// </summary>
        public static string Sys_LocalDB_FileOperation = ConfigurationManager.AppSettings["LocalDB_FileOperation"];

        /// <summary>
        /// 最近联系人表名称
        /// </summary>
        public static string Sys_LocalDB_RecentLink = ConfigurationManager.AppSettings["LocalDB_Recent"];


        #endregion




        /// <summary>
        /// 未下载完成时的文件名称
        /// </summary>
        public static string tmpDownLoadName = ".imTmpFile";


        /// <summary>
        /// 聊天工具名称
        /// </summary>
        public static string Chat_Name = "IMClient.exe";

        public static string Sys_Meeting_RoomName = ConfigurationManager.AppSettings["MeetingRoomName"];

        #region 
        /// <summary>
        /// html使用 调用系统图片路径
        /// </summary>
        public const string Html_SysImagePath = "Source/default/sysImage/";


        #endregion

        #region  Form系统调用的本地路径

        /// <summary>
        /// Form 本地缩略图保存路径
        /// </summary>
        public const string Sys_ThumbnailImagePath = @"htm\Source\default\img\";

        /// <summary>
        /// 临时文件保存目录
        /// </summary>
        public const string Sys_tmpFile = "htm\\Source\\tmpFile\\";

        /// <summary>
        /// 系统图片目录
        /// </summary>
        public const string Sys_ImagePath= "htm\\Source\\default\\sysImage\\";

        /// <summary>
        /// 聊天消息展示文件目录
        /// </summary>
        public const string Sys_MessageShownHtml = @"htm\talkHistory.html";

        /// <summary>
        /// 会议室消息展示文件目录
        /// </summary>
        public const string Sys_MeetingMessageShownHtml = @"htm\meetingHistory.html";

        /// <summary>
        /// 参与者
        /// </summary>
        public const string Sys_Meeting_ActorMessageShownHtml = @"htm\meetingHistory_Actor.html";

        /// <summary>
        /// 默认头像路径
        /// </summary>
        public const string Sys_DefaultHeadImageFilePath = "htm\\Source\\default\\head\\default.jpg";

        /// <summary>
        /// 系统默认头像文件名称
        /// </summary>
        public const string Sys_DefaultHeadImageFileName = "default.jpg";
        #endregion

        #region 各种限制条件

        /// <summary>
        /// 限制上传文件的大小
        /// 500MB
        /// </summary>
        public const long UPLoad_FileSzie = 500 * 1024 * 1024 ;

        /// <summary>
        /// 网络连接超时时间
        /// </summary>
        public const int WebTimeOut = 5 * 1000;

        /// <summary>
        /// 聊天窗口 普通模式 宽度
        /// </summary>
        public const int chatWidth_Normal = 624;

        /// <summary>
        /// 聊天窗口 最大宽度
        /// </summary>
        public const int chatWidth_Max = 883;


        /// <summary>
        /// 上传限制 56KB
        /// </summary>
        public const int  Limit_UpData = 512;

        /// <summary>
        /// 每次限制上传的数量
        /// </summary>
        public const int  Limit_FileUPLoad = 5;

        /// <summary>
        /// 文字长度限制-文件名
        /// </summary>
        public const int  Limit_StrLength_FileName = 16;

        /// <summary>
        /// 文字长度限制-上传进度
        /// </summary>
        public const int Limit_StrLength_FileSize = 14;

        /// <summary>
        /// html 中限制长度长度限制
        /// </summary>
        public const int Limit_Html_StrLength_FileName = 12;

        /// <summary>
        /// html 中结果字符长度限制
        /// </summary>
        public const int  Limit_Html_StrLength_FileResult = 30;

        #endregion





     

    }
}
