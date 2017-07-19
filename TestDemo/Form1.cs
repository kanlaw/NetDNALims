using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Matrix;
using Matrix.Xmpp.Client;
using Matrix.Xmpp;

namespace TestDemo
{
    public partial class Form1 : Form
    {

        private Jid roomid = string.Empty;
        Jid userjid = null;
        private string nickname = string.Empty;
        private string auth = string.Empty;

        public Form1()
        {
            InitializeComponent();


        }
        Jid j = null;
        private void btnCreat_Click(object sender, System.EventArgs e)
        {

            j = new Jid(txtRoomName.Text+"@conference.192.168.1.95");
            //StaticClass.muc.CreateEnterRoomStanza("
            StaticClass.muc.EnterRoom(j, "nickname");

            // Setup new Message Callback using the MessageFilter
            StaticClass.xmppClient.MessageFilter.Add(j, new BareJidComparer(), MessageCallback);

            // Setup new Presence Callback using the PresenceFilter
            StaticClass.xmppClient.PresenceFilter.Add(j, new BareJidComparer(), PresenceCallback);

            StaticClass.muc.RequestRoomConfiguration(j, GetRoomSetting_Click);

        }
        private void test_Click(object sender, IqEventArgs e)
        {
            //object obj= e.Iq.SetAttributeValue("");
            e.Iq.SetAttribute("maxusers", "10");
        }
        private void XmppClient_OnLogin(object sender, Matrix.EventArgs e)
        {
            btnCreat.Enabled = true;

        }

        private void btnlogin_Click(object sender, System.EventArgs e)
        {
            StaticClass.InitXmppClient("yujunming", "111111", "192.168.1.95", "192.168.2.192");
            auth = Program.CalcHash("username:yujunming@192.168.1.95 / password:111111");
            StaticClass.xmppClient.Open();
            StaticClass.xmppClient.OnLogin += XmppClient_OnLogin;

        }
        Matrix.Xmpp.XData.Data d = new Matrix.Xmpp.XData.Data();
        private void btnSetting_Click(object sender, System.EventArgs e)
        {
            Matrix.Xmpp.XData.Field f = d.GetField("muc#roomconfig_roomname");  //muc#roomconfig_maxusers.SetValues(new string[] { "10" });
            f.SetValues(new string[] { "rooms" });
            //f.AddOption(new Matrix.Xmpp.XData.Option("10", "10"));

            StaticClass.muc.SubmitRoomConfiguration(j, d, SetRoomSetting_Click);
        }

        public string test()
        {
            string xml = @" <x xmlns='jabber:x:data' type='form'>
    <title>Voice request</title>
    <instructions>
      To approve this request for voice, select 
      the &quot;Grant voice to this person?&quot;
      checkbox and click OK. To skip this request, 
      click the cancel button.
    </instructions>
    <field var='FORM_TYPE' type='hidden'>
        <value>http://jabber.org/protocol/muc#request</value>
    </field>
    <field var='muc#role'
           type='text-single'
           label='Requested role'>
      <value>participant</value>
    </field>
    <field var='muc#jid'
           type='jid-single'
           label='User ID'>
      <value>hag66@shakespeare.lit/pda</value>
    </field>
    <field var='muc#roomnick'
           type='text-single'
           label='Room Nickname'>
      <value>thirdwitch</value>
    </field>
    <field var='muc#request_allow'
           type='boolean'
           label='Grant voice to this person?'>
      <value>false</value>
    </field>
  </x>";


            string xml2 = @"<x xmlns='jabber:x:data' type='form'>
      <title>Dark Cave Registration</title>
      <instructions>
        Please provide the following information
        to register with this room.
      </instructions>
      <field
          type='hidden'
          var='FORM_TYPE'>
        <value>http://jabber.org/protocol/muc#register</value>
      </field>
      <field
          label='Given Name'
          type='text-single'
          var='muc#register_first'>
        <required/>
      </field>
      <field
          label='Family Name'
          type='text-single'
          var='muc#register_last'>
        <required/>
      </field>
      <field
          label='Desired Nickname'
          type='text-single'
          var='muc#register_roomnick'>
        <required/>
      </field>
      <field
          label='Your URL'
          type='text-single'
          var='muc#register_url'/>
      <field
          label='Email Address'
          type='text-single'
          var='muc#register_email'/>
      <field
          label='FAQ Entry'
          type='text-multi'
          var='muc#register_faqentry'/>
    </x>";
            return xml2;
        }

        private void GetRoomSetting_Click(object sender, IqEventArgs e)
        {
            d = (Matrix.Xmpp.XData.Data)e.Iq.Query.FirstElement;
            btnSetting.Enabled = true;

        }


        private void SetRoomSetting_Click(object sender, IqEventArgs e)
        {


        }



        private void MessageCallback(object sender, MessageEventArgs e)
        {
            if (e.Message.Type == MessageType.GroupChat)
                IncomingMessage(e.Message);
        }
        private void IncomingMessage(Matrix.Xmpp.Client.Message msg)
        {
            if (msg.Type == MessageType.Error)
            {
                //Handle errors here
                // we dont handle them in this example
                return;
            }

            if (msg.Subject != null)
            {
                //txtSubject.Text = msg.Subject;

                //rtfChat.SelectionColor = Color.DarkGreen;
                //// The Nickname of the sender is in GroupChat in the Resource of the Jid
                //rtfChat.AppendText(msg.From.Resource + " changed subject: ");
                //rtfChat.SelectionColor = Color.Black;
                //rtfChat.AppendText(msg.Subject);
                //rtfChat.AppendText("\r\n");
            }
            else
            {
                if (msg.Body == null)
                    return;

                //txtContent.ForeColor = Color.Red;
                // The Nickname of the sender is in GroupChat in the Resource of the Jid
                txtContent.AppendText(msg.From.Resource + " said: ");
                txtContent.ForeColor = Color.Black;
                txtContent.AppendText(msg.Body);
                txtContent.AppendText("\r\n");
            }
        }

        private void PresenceCallback(object sender, PresenceEventArgs e)
        {

            var mucX = e.Presence.MucUser;

            // check for status code 201, this means the room is not ready to use yet
            // we request an instant room and accept the and accept the default configuration by the server
            if (mucX != null && mucX.HasStatus(201)) // 201 =  room is awaiting configuration.
                StaticClass.muc.RequestInstantRoom(j);


            var lvi = FindListViewItem(e.Presence.From);
            if (lvi != null)
            {

            }
            else
            {
                if (treeView1.Nodes.Count == 0)
                {
                    TreeNode node = (new TreeNode(e.Presence.From.ToString()));
                    node.Tag = e.Presence.From.ToString();
                    treeView1.Nodes.Add(node);
                }

                TreeNode node2 = new TreeNode(e.Presence.MucUser.ToString());
                node2.Tag = e.Presence.MucUser.ToString();
                treeView1.Nodes[0].Nodes.Add(node2);

            }
        /*   
       if (lvi != null)
       {
           if (e.Presence.Type == PresenceType.Unavailable)
           {
               lvi.Remove();
           }
           else
           {
               int imageIdx = Util.GetRosterImageIndex(e.Presence);
               lvi.ImageIndex = imageIdx;
               lvi.SubItems[1].Text = (e.Presence.Status ?? "");

               var u = e.Presence.MucUser;
               if (u != null)
               {
                   lvi.SubItems[2].Text = u.Item.Affiliation.ToString();
                   lvi.SubItems[3].Text = u.Item.Role.ToString();
               }
           }
       }
       else
       {
           int imageIdx = Util.GetRosterImageIndex(e.Presence);

           var lv = new TreeNode(e.Presence.From.Resource) { Tag = e.Presence.From.ToString() };

           lv.SubItems.Add(e.Presence.Status ?? "");

           var u = e.Presence.MucUser;
           if (u != null)
           {
               lv.SubItems.Add(u.Item.Affiliation.ToString());
               lv.SubItems.Add(u.Item.Role.ToString());
           }
           lv.ImageIndex = imageIdx;
           treeView1.Nodes.Add(lv);
       }*/
    }

        private TreeNode FindListViewItem(Jid jid)
        {
            foreach (TreeNode lvi in treeView1.Nodes)
            {
                if (jid.ToString().ToLower() == lvi.Tag.ToString().ToLower())
                    return lvi;
            }
            return null;
        }
        private void cmdSend_Click(object sender, System.EventArgs e)
        {
            // Make sure that the users send no empty messages
            if (txtSendContent.Text.Length > 0)
            {
                //var msg = new Matrix.Xmpp.Client.Message
                //{
                //    Type = MessageType.GroupChat,
                //    To = j,
                //    Body = txtSendContent.Text
                //};
                var msg = new Matrix.Xmpp.Client.Message
                {
                    Type = Matrix.Xmpp.MessageType.GroupChat,
                    To = this.j,
                    Body = txtSendContent.Text,
                    From = this.userjid

                };
                msg.XMucUser = new Matrix.Xmpp.Muc.User.X();
                msg.XMucUser.Item = new Matrix.Xmpp.Muc.User.Item();
                msg.XMucUser.Item.Jid = new Jid("yujunming@192.168.1.95");
                msg.XMucUser.AddTag("REALNAME", "俞俊明");
                msg.XMucUser.AddTag("UID", "1412795548");
                msg.XMucUser.AddTag("HEAD", "head.png");
                msg.XMucUser.AddTag("COMPANY", "上海市DNA实验室");
                StaticClass.xmppClient.Send(msg);

                txtSendContent.Clear();
            }
        }

        private void btnAddUser_Click(object sender, System.EventArgs e)
        {
            Form2 f2 = new Form2(this.j);
            f2.Show();
        }
     
        private void btnInv_Click(object sender, System.EventArgs e)
        {
            if (userjid != null)
            {
                this.userjid = new Jid("yujunming@192.168.1.95");
                StaticClass.muc.Invite(userjid, j);
            }
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            Form3 f3 = new Form3(this.j);
            f3.Show();
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            j = new Jid(txtRoomName.Text + "@conference.192.168.1.95");

            StaticClass.muc.DestroyRoom(j);
        }
    }
}
