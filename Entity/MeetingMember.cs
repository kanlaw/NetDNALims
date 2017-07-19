using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class MeetingMember
    {
        private string mmid;

        private string meetingId;

        private int memberId;

        private int isJoin;

        private string userJid;


        public string Mmid { get => mmid; set => mmid = value; }
        public string MeetingId { get => meetingId; set => meetingId = value; }
        public int IsJoin { get => isJoin; set => isJoin = value; }
        public int MemberId { get => memberId; set => memberId = value; }
        public string UserJid { get => userJid; set => userJid = value; }
    }
}