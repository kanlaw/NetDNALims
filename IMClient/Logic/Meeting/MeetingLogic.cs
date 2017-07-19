using IMClient.Controls;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IMClient.Logic
{
    public  class MeetingLogic
    {
        /// <summary>
        /// 会议添加
        /// </summary>
        /// <param name="meeting"></param>
        /// <returns></returns>
        public string AddMeeting(Meeting meeting)
        {
            //int result = 0;


            Dictionary<string, string> dict = new Dictionary<string, string>();

            dict["RESULT"] =JsonConvert.SerializeObject(meeting);
           
            string result = RestHelper.GetDataWaitTime(dict, SysParams.TalkServiceRoot, 
                SysParams.MeetingService_AddMeeting, 
                SysParams.WebTimeOut,
                RestSharp.Method.POST);
            //if (!string.IsNullOrEmpty(result))
            //{
            //    DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);
            //    return dt;
            //}

            return result;
        }

        /// <summary>
        /// 是否能参加会议
        /// </summary>
        /// <param name="meetingGuid"></param>
        /// <param name="userId"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public bool AttendMeeting(string meetingGuid,string userId,string limit)
        {
            //int result = 0;


            Dictionary<string, string> dict = new Dictionary<string, string>();

            dict["MEETGUID"] = meetingGuid;
            dict["USERID"] = userId;
            dict["LIMIT"] = limit;

            string val = RestHelper.GetDataWaitTime(dict, SysParams.TalkServiceRoot,
                SysParams.MeetingService_AttendMeeting,
                SysParams.WebTimeOut,
                RestSharp.Method.POST);
            bool result = false;
            try
            {
                if (int.Parse(val) == 1)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {

                result = false;
            }

            //if (!string.IsNullOrEmpty(result))
            //{
            //    DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);
            //    return dt;
            //}

            return result;
        }
        /// <summary>
        /// 会议成员添加
        /// </summary>
        /// <param name="meeting"></param>
        /// <returns></returns>
        public string AddMember(List<MeetingMember> member)
        {
            //int result = 0;


            Dictionary<string, string> dict = new Dictionary<string, string>();

            dict["MEMBER"] = JsonConvert.SerializeObject(member);

            string result = RestHelper.GetDataWaitTime(dict, SysParams.TalkServiceRoot,
                SysParams.MeetingService_AddMembers,
                SysParams.WebTimeOut,
                RestSharp.Method.POST);
            //if (!string.IsNullOrEmpty(result))
            //{
            //    DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);
            //    return dt;
            //}

            return result;
        }

        /// <summary>
        /// 根据JID获取用户信息
        /// </summary>
        /// <param name="userJid"></param>
        /// <returns></returns>
        public DataTable retUserInfoByJID(string userJid)
        {
            //int result = 0;


            Dictionary<string, string> dict = new Dictionary<string, string>();

            dict["JID"] = userJid;

            string result = RestHelper.GetDataWaitTime(dict, SysParams.TalkServiceRoot,
                SysParams.MeetingService_UserInfo_JID,
                SysParams.WebTimeOut,
                RestSharp.Method.POST);
            //if (!string.IsNullOrEmpty(result))
            //{
            //    DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);
            //    return dt;
            //}
            DataTable dt = null;
            if (!string.IsNullOrEmpty(result))
            {
                dt = JsonConvert.DeserializeObject<DataTable>(result);
            }
            return dt;
        }

       
        /// <summary>
        /// 更新会议室状态
        /// </summary>
        /// <param name="ms"></param>
        /// <param name="meetingGuid"></param>
        /// <returns></returns>
        public string UpdateMeetingStatus(MeetingStatus ms, string meetingGuid)
        {
            // UpdateMeetingStatus

            Dictionary<string, string> dict = new Dictionary<string, string>();

            dict["MEETINGGUID"] = meetingGuid;
            dict["MEETINGSTATUS"] = ((int)ms).ToString();

            string result = RestHelper.GetDataWaitTime(dict, SysParams.TalkServiceRoot,
                SysParams.MeetingService_UPDATESTATUS,
                SysParams.WebTimeOut,
                RestSharp.Method.POST);
            return result;
        }

        /// <summary>
        /// 销毁OpenFire中聊天室
        /// </summary>
        /// <param name="meetingJid"></param>
        public void CloseMeeting(Matrix.Jid meetingJid)
        {
            StaticClass.muc.DestroyRoom(meetingJid);
        }

       

        /// <summary>
        /// 增加选项的投票数量
        /// </summary>
        /// <param name="ms"></param>
        /// <param name="meetingGuid"></param>
        /// <returns></returns>
        public string UpdateVoteCount(string moid,string meetingGUID,string voteUser)
        {
            // UpdateMeetingStatus

            Dictionary<string, string> dict = new Dictionary<string, string>();

            dict["MOID"] = moid;
            dict["MEETINGGUID"] = meetingGUID;
            dict["USERID"] = voteUser;
            string result = RestHelper.GetDataWaitTime(dict, SysParams.TalkServiceRoot,
                SysParams.MeetingService_VOTE_COUNTUPDATE,
                SysParams.WebTimeOut,
                RestSharp.Method.POST);
            return result;
        }


        /// <summary>
        /// 获取会议室列表
        /// </summary>
        /// <param name="query"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public DataTable retMeetingInfo(string query,int currentPage,int pageSize)
        {
            DataTable dt = null;
            Dictionary<string, string> dict = new Dictionary<string, string>();

            dict["QUERY"] = query;
            dict["CURRENTPAGE"] = currentPage.ToString();
            dict["PAGESIZE"] = pageSize.ToString();
            string result=  RestHelper.GetDataWaitTime(dict, SysParams.TalkServiceRoot,
                SysParams.MeetingService_Query,
                SysParams.WebTimeOut,
                RestSharp.Method.POST);
            if (!string.IsNullOrEmpty(result) && result != "-1")
            {
                Dictionary<string, DataTable> dtDict = JsonConvert.DeserializeObject <Dictionary<string, DataTable>>(result);
                if (dtDict != null)
                {
                    foreach (string key in dtDict.Keys)
                    {
                        dt = dtDict[key];
                        dt.TableName = key;
                        break;
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// 创建/进入 OpenFire会议室
        /// </summary>
        /// <param name="roomName"></param>
        /// <param name="emceeNameJid"></param>
        public Matrix.Jid meetingEnterCreateOpenFire(string roomName, string enterUserId)
        {
            string roomName_tmp = roomName + SysParams.Sys_Meeting_RoomName;
            Matrix.Jid j = new Matrix.Jid(roomName_tmp);
            //StaticClass.muc.
            StaticClass.muc.EnterRoom(j, enterUserId);
            return j;
        }

        /// <summary>
        /// 退出聊天室
        /// </summary>
        /// <param name="jid"></param>
        /// <param name="enterUserId"></param>
        public void ExitMeeting(Matrix.Jid jid, string enterUserId)
        {
            StaticClass.muc.ExitRoom(jid, enterUserId);
        }


        /// <summary>
        /// 创建会议室
        /// 1 OpenFire
        /// 2 数据库会议室状态修改
        /// </summary>
        /// <param name="meetingGuid"></param>
        public Matrix.Jid creatMeeting(string meetingGuid)
        {
            Matrix.Jid j= meetingEnterCreateOpenFire(meetingGuid, SysParams.LoginUser.UID.ToString());
            UpdateMeetingStatus(MeetingStatus.opening, meetingGuid);
            return j;
        }

        /// <summary>
        /// 获取会议室列表
        /// </summary>
        /// <param name="query"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public Dictionary<string, string> retMeetingInfoByGuid(string query)
        {
            Dictionary<string, string> dictInfo = null;
            Dictionary<string, string> dict = new Dictionary<string, string>();

            dict["QUERY"] = query;
        
            string result = RestHelper.GetDataWaitTime(dict, SysParams.TalkServiceRoot,
                SysParams.MeetingService_Guid,
                SysParams.WebTimeOut,
                RestSharp.Method.POST);
            if (!string.IsNullOrEmpty(result) && result != "-1")
            {
                dictInfo = JsonConvert.DeserializeObject<Dictionary<string,string>>(result);
            }
            return dictInfo;
        }

        /// <summary>
        /// 反馈会议成员信息
        /// </summary>
        /// <param name="UserIdList"></param>
        /// <returns></returns>
        public DataTable retMeetingUserInfoByGuid(string UserIdList)
        {
            DataTable data = null;
            Dictionary<string, string> dict = new Dictionary<string, string>();

            dict["QUERY"] = UserIdList;

            string result = RestHelper.GetDataWaitTime(dict, SysParams.TalkServiceRoot,
                SysParams.MeetingService_UserInfo,
                SysParams.WebTimeOut,
                RestSharp.Method.POST);
            if (!string.IsNullOrEmpty(result) && result != "-1")
            {
                data = JsonConvert.DeserializeObject<DataTable>(result);
            }
            return data;
        }

        ///Meeting/QUERY/USERINFO
    }
}
