using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class MeetingOption
    {
        private string moId;

        private string meetingId;

        private string letter;

        private string content;

        private int type;

        /// <summary>
        /// 已被投票数量
        /// </summary>
        private int voteCount;

        public string MoId { get => moId; set => moId = value; }
        public string MeetingId { get => meetingId; set => meetingId = value; }
        public string Letter { get => letter; set => letter = value; }
        public string Content { get => content; set => content = value; }
        public int Type { get => type; set => type = value; }
        public int VoteCount { get => voteCount; set => voteCount = value; }
    }
}