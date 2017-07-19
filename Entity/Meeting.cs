using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class Meeting
    {
        private int meetingId;

        private string meetingGuid;

        private string roomName;

        private string subject;

        private string content;

        private int isPublic;

        private string createTime;

        private int createUser;

        /// <summary>
        /// 主持人Id
        /// </summary>
        private int emcee;

        /// <summary>
        /// 主持人 JID
        /// </summary>
        private string emceeJid;

        /// <summary>
        /// 主持人姓名
        /// </summary>
        private string emceeName;

        /// <summary>
        /// 主持人单位
        /// </summary>
        private string emceeCompany;

        private string pwd;

        private int memberLimit;

        private int personCount;

        private int meetingStatus = 0;

        private List<MeetingMember> members = new List<MeetingMember>();

        private List<MeetingOption> options = new List<MeetingOption>();


        public int MeetingId { get => meetingId; set => meetingId = value; }
        public string RoomName { get => roomName; set => roomName = value; }
        public string Subject { get => subject; set => subject = value; }
        public string Content { get => content; set => content = value; }
        public int IsPublic { get => isPublic; set => isPublic = value; }
        public string CreateTime { get => createTime; set => createTime = value; }
        public int CreateUser { get => createUser; set => createUser = value; }
        public int Emcee { get => emcee; set => emcee = value; }
        public string Pwd { get => pwd; set => pwd = value; }
        public List<MeetingMember> Members { get => members; set => members = value; }
        public List<MeetingOption> Options { get => options; set => options = value; }
        public string MeetingGuid { get => meetingGuid; set => meetingGuid = value; }
        public int MemberLimit { get => memberLimit; set => memberLimit = value; }
        public int PersonCount { get => personCount; set => personCount = value; }
        public string EmceeJid { get => emceeJid; set => emceeJid = value; }
        public int MeetingStatus { get => meetingStatus; set => meetingStatus = value; }
        public string EmceeName { get => emceeName; set => emceeName = value; }
        public string EmceeCompany { get => emceeCompany; set => emceeCompany = value; }
    }
}