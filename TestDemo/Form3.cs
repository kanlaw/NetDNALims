using Matrix;
using Matrix.Xmpp;
using Matrix.Xmpp.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestDemo
{
    public partial class Form3 : Form
    {
        Jid j = null;
        public  Jid userjid = null;
        public Form3(Jid jid)
        {
            InitializeComponent();
            j = jid;
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            StaticClass3.InitXmppClient("yuxiuhua", "111111", "192.168.1.95", "192.168.2.192");
            StaticClass3.xmppClient.Open();
            StaticClass3.xmppClient.OnLogin += XmppClient_OnLogin;

        }

        private void XmppClient_OnLogin(object sender, Matrix.EventArgs e)
        {
            button2.Enabled = true;
            // Setup new Message Callback using the MessageFilter
            StaticClass3.xmppClient.MessageFilter.Add(j, new BareJidComparer(), MessageCallback);

            // Setup new Presence Callback using the PresenceFilter
            StaticClass3.xmppClient.PresenceFilter.Add(j, new BareJidComparer(), PresenceCallback);

        }



        
        private void button2_Click(object sender, System.EventArgs e)
        {
            if (txtSend.Text.Length > 0)
            {
                var msg = new Matrix.Xmpp.Client.Message
                {
                    Type = MessageType.GroupChat,
                    To = j,
                    Body = txtSend.Text
                };

                StaticClass3.xmppClient.Send(msg);

                txtSend.Clear();
            }
        }

        private void btnEnter_Click(object sender, System.EventArgs e)
        {
            //Matrix.Xmpp.Show s =  Matrix.Xmpp.Show.Away;
            //string status = "123";

            StaticClass3.muc.EnterRoom(j, "nc3");
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

                //rtfChat.SelectionColor = Color.Red;
                //// The Nickname of the sender is in GroupChat in the Resource of the Jid
                //rtfChat.AppendText(msg.From.Resource + " said: ");
                //rtfChat.SelectionColor = Color.Black;
                //rtfChat.AppendText(msg.Body);
                //rtfChat.AppendText("\r\n");
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
            if (mucX!=null && mucX.HasStatus(201)) // 201 =  room is awaiting configuration.
                StaticClass2.muc.RequestInstantRoom(j);



            if (treeView1.Nodes.Count == 0)
            {
                TreeNode node = (new TreeNode(e.Presence.From.ToString()));
                node.Tag = e.Presence.From.ToString();
                treeView1.Nodes.Add(node);
            }

            TreeNode node2 = new TreeNode(e.Presence.MucUser.ToString());
            node2.Tag = e.Presence.MucUser.ToString();
            treeView1.Nodes[0].Nodes.Add(node2);

            /*
            var lvi = FindListViewItem(e.Presence.From);
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

                var lv = new ListViewItem(e.Presence.From.Resource) { Tag = e.Presence.From.ToString() };

                lv.SubItems.Add(e.Presence.Status ?? "");

                var u = e.Presence.MucUser;
                if (u != null)
                {
                    lv.SubItems.Add(u.Item.Affiliation.ToString());
                    lv.SubItems.Add(u.Item.Role.ToString());
                }
                lv.ImageIndex = imageIdx;
                lvwRoster.Items.Add(lv);
            }*/
        }

    }
}
