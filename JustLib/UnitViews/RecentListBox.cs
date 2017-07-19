using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using JustLib.UnitViews;
using CCWin.SkinControl;
using ESBasic;
using JustLib.Properties;

namespace JustLib.UnitViews
{
    public partial class RecentListBox : UserControl
    {
        public event CbGeneric<ChatListSubItem> UnitDoubleClicked;
        public event CbGeneric<string, bool> ChatRecordClicked;

        public RecentListBox()
        {
            InitializeComponent();
        }

        private IHeadImageGetter resourceGetter;
        public void Initialize(IHeadImageGetter getter)
        {
            this.resourceGetter = getter;
        }

        public void Clear()
        {
            this.chatListBox.Items[0].SubItems.Clear();
        }

        public void AddRecentUnit(IUnit unit, int insertIndex)
        {
            this.chatListBox.Items[0].SubItems.Clear();
           // string recentID = RecentListBox.ConstructRecentID(unit);
            UserStatus status = unit.IsGroup ? UserStatus.Online : ((IUser)unit).UserStatus;
            //Image img = unit.IsGroup ? this.imageList1.Images[0] : this.resourceGetter.GetHeadImage((IUser)unit);
            ChatListSubItem subItem = new ChatListSubItem(unit.ID, "", unit.Name, "", this.ConvertUserStatus(status), Resource.Head_portrait02_index_32);
            subItem.Tag = unit;
            subItem.LastWords = unit.LastWords;
            this.chatListBox.Items[0].SubItems.Insert(insertIndex, subItem);
            this.chatListBox.Invalidate();
        }


        /// <summary>
        /// 添加最近联系人
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="insertIndex"></param>
        public void AddRecentLinkman(GGUser user, int insertIndex)
        {
            this.chatListBox.Items[0].SubItems.Clear();
            // string recentID = RecentListBox.ConstructRecentID(unit);
            UserStatus status = user.IsGroup ? UserStatus.Online : ((IUser)user).UserStatus;
            //Image img = unit.IsGroup ? this.imageList1.Images[0] : this.resourceGetter.GetHeadImage((IUser)unit);
     
            System.IO.MemoryStream ms = new System.IO.MemoryStream(user.HeadImageData);
            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);

            ChatListSubItem subItem = new ChatListSubItem(user.ID, "", user.Name, user.UserCompany, this.ConvertUserStatus(status), img);
         
            subItem.Tag = user;
            subItem.LastWords = user.LastWords;
           
            
            this.chatListBox.Items[0].SubItems.Insert(insertIndex, subItem);
            this.chatListBox.Invalidate();
        }

        /// <summary>
        /// 添加最近联系人
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="insertIndex"></param>
        public void AddRecentLinkman(List<GGUser> userlist)
        {
            this.chatListBox.Items[0].SubItems.Clear();
            for (int i = 0; i < userlist.Count; i++)
            {
                GGUser user = userlist[i];
                // string recentID = RecentListBox.ConstructRecentID(unit);
                UserStatus status = user.IsGroup ? UserStatus.Online : ((IUser)user).UserStatus;
                //Image img = unit.IsGroup ? this.imageList1.Images[0] : this.resourceGetter.GetHeadImage((IUser)unit);

                System.IO.MemoryStream ms = new System.IO.MemoryStream(user.HeadImageData);
                System.Drawing.Image img = System.Drawing.Image.FromStream(ms);

                ChatListSubItem subItem = new ChatListSubItem(user.ID, "", user.Name, user.UserCompany, this.ConvertUserStatus(status), img);

                subItem.Tag = user;
                subItem.LastWords = user.LastWords;


                this.chatListBox.Items[0].SubItems.Add(subItem);
            }
           
            this.chatListBox.Invalidate();
        }
         
        public void SetUserHead(string JID, byte[] ImageBytes)
        {
            foreach(ChatListSubItem subItem in this.chatListBox.Items[0].SubItems)
            {
                if (subItem.ID == JID)
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(ImageBytes);
                    System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                    subItem.HeadImage = img;
                    this.chatListBox.Invoke(new Action(() =>
                    {
                        this.chatListBox.Invalidate(subItem.HeadRect);
                    }));
              
                    break;
                } 
            }
           // return Rectangle.Empty;
        }


        public void LastWordChanged(IUnit unit)
        {
            string recentID = RecentListBox.ConstructRecentID(unit);
            ChatListSubItem[] items = this.chatListBox.GetSubItemsById(recentID);
            if (items != null && items.Length > 0)
            {
                ChatListSubItem item = items[0];
                item.LastWords = unit.LastWords;
                item.OwnerListItem.SubItems.Remove(item);
                item.OwnerListItem.SubItems.Insert(0, item);
            }
            else
            {
                this.AddRecentUnit(unit, 0);
            }
            this.chatListBox.Invalidate();
        }

        public void RemoveUser(string userID)
        {
            string recentID = RecentListBox.ConstructRecentID4User(userID);
            this.chatListBox.RemoveSubItemsById(recentID);
            this.chatListBox.Invalidate();
        }

        public void RemoveUnit(IUnit unit)
        {
            string recentID = RecentListBox.ConstructRecentID(unit);
            this.chatListBox.RemoveSubItemsById(recentID);
            this.chatListBox.Invalidate();
        }

        public void UserStatusChanged(GGUser user)
        {
            //string recentID = RecentListBox.ConstructRecentID(user);
            ChatListSubItem[] items = this.chatListBox.GetSubItemsById(user.ID);
            if (items == null || items.Length == 0)
            {
                return;
            }
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].ID == user.JID.Bare)
                {
                    items[i].Status = this.ConvertUserStatus(user.UserStatus);
                    this.chatListBox.Invalidate();
                    break;
                }
            }
                    

            //items[0].DisplayName = user.Name;

            //System.IO.MemoryStream ms = new System.IO.MemoryStream(user.HeadImageData);
            //System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
            //items[0].HeadImage = img;
           
        }

        public List<string> GetRecentUserList(int maxCount)
        {
            List<string> recentList = new List<string>();
            int count = 0;
            foreach (ChatListSubItem item in this.chatListBox.Items[0].SubItems)
            {
                recentList.Add(item.ID);
                ++count;
                if (count >= maxCount)
                {
                    break;
                }
            }
            return recentList;
        }

        public void SetTwinkleState(string id, bool isGroup, bool twinkle)
        {
            string recentID = isGroup ? RecentListBox.ConstructRecentID4Group(id) : RecentListBox.ConstructRecentID4User(id);
            ChatListSubItem[] items = this.chatListBox.GetSubItemsById(recentID);
            if (items == null || items.Length == 0)
            {
                return;
            }
            items[0].IsTwinkle = twinkle;
        }

        public void SetAllUserOffline()
        {
            foreach (ChatListItem item in this.chatListBox.Items)
            {
                foreach (ChatListSubItem sub in item.SubItems)
                {
                    IUnit unit = (IUnit)sub.Tag;
                    if (!unit.IsGroup)
                    {
                        sub.Status = ChatListSubItem.UserStatus.OffLine;
                    }
                }
            }

            this.chatListBox.Invalidate();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            //ChatListSubItem item = this.chatListBox.SelectSubItem;          
            //item.IsTwinkle = false;

            //Parameter<string, bool> para = RecentListBox.ParseIDFromRecentID(item.ID);
            //if (this.UnitDoubleClicked != null)
            //{
            //    this.UnitDoubleClicked(para.Arg1, para.Arg2);
            //}
            ChatListSubItem item = this.chatListBox.SelectSubItem;
            item.IsTwinkle = false;
            if (this.UnitDoubleClicked != null)
            {
                this.UnitDoubleClicked(item);
            }

        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            ChatListSubItem item = this.chatListBox.SelectSubItem;
            //Parameter<string, bool> para = RecentListBox.ParseIDFromRecentID(item.ID);

            //if (this.ChatRecordClicked != null)
            //{
            //    this.ChatRecordClicked(para.Arg1, para.Arg2);
            //}
           

        }

        private void 从列表中移除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.chatListBox.Items[0].SubItems.Remove(this.chatListBox.SelectSubItem);
        }

        private void chatListBox_DoubleClickSubItem(object sender, ChatListEventArgs e)
        {
            
            //Parameter<string, bool> para = RecentListBox.ParseIDFromRecentID(e.SelectSubItem.ID);

            //if (this.UnitDoubleClicked != null)
            //{
            //    this.UnitDoubleClicked(para.Arg1, para.Arg2);
            //}
          

            ChatListSubItem item = this.chatListBox.SelectSubItem;
            item.IsTwinkle = false;         
            if (this.UnitDoubleClicked != null)
            {
                this.UnitDoubleClicked(item);
            }
        }

        #region RecentID
        public static string ConstructRecentID(IUnit unit)
        {
            return (unit.IsGroup ? "G_" : "U_") + unit.ID;
        }

        public static string ConstructRecentID4User(string userID)
        {
            return "U_" + userID;
        }

        public static string ConstructRecentID4Group(string groupID)
        {
            return "G_" + groupID;
        }

        public static Parameter<string, bool> ParseIDFromRecentID(string recentID)
        {
            string id = recentID.Substring(2);
            bool isGroup = recentID.StartsWith("G_");
            return new Parameter<string, bool>(id, isGroup);
        }
        #endregion

        #region ConvertUserStatus
        private ChatListSubItem.UserStatus ConvertUserStatus(UserStatus status)
        {
            if (status == UserStatus.Hide)
            {
                return ChatListSubItem.UserStatus.OffLine;
            }

            return (ChatListSubItem.UserStatus)((int)status);
        }
        #endregion
    }    
}
