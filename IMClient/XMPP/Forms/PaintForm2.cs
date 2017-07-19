using IMClient.Controls.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IMClient.XMPP
{
    public partial class PaintForm2 : FrameBase
    {
        private string titleStr = "手写板";
        private Color currentColor = Color.Red;
        private List<float> penWidthList = new List<float>();
        private Bitmap currentImage;
        private Color borderColor = Color.FromArgb(178, 178, 178);
        public Bitmap CurrentImage
        {
            get { return currentImage; }
        }

        public PaintForm2()
        {
            InitializeComponent();
            this.penWidthList.Add(2);
            this.penWidthList.Add(4);
            this.penWidthList.Add(6);
            this.penWidthList.Add(8);
            this.penWidthList.Add(10);
            this.comboBox_brushWidth.DataSource = this.penWidthList;
            this.comboBox_brushWidth.SelectedIndex = 1;
        }

        private void PaintForm2_Paint(object sender, PaintEventArgs e)
        {
            int width = this.Width;
            int height = this.Height;
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            g.DrawRectangle(new Pen(borderColor, 1.0f),
                new Rectangle(new Point(0, 0), new Size(width, height)));

            int hwbWidth = this.hwb.Width + 1;
            int hwbHeight = this.hwb.Height + 1;
            Point hlocal= new Point(this.hwb.Location.X,this.hwb.Location.Y);
            g.DrawRectangle(new Pen(borderColor, 1.0f),
                new Rectangle(hlocal, new Size(hwbWidth, hwbHeight)));

            g.DrawString(this.titleStr, 
                new Font("微软雅黑", 10, FontStyle.Bold),
                Brushes.Black, new PointF(8, 8));

        }

        private void button_color_Click(object sender, EventArgs e)
        {

            try
            {
                this.colorDialog1.Color = this.currentColor;
                DialogResult result = this.colorDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    this.currentColor = this.colorDialog1.Color;
                    this.hwb.PenColor = this.currentColor;
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message, "GGTalk");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.hwb.Clear();
            this.DialogResult = System.Windows.Forms.DialogResult.None;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.hwb.Clear();
            this.DialogResult = System.Windows.Forms.DialogResult.None;
            this.Close();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            try
            {
                this.currentImage = this.hwb.GetHandWriting();
                if (this.CurrentImage != null)
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    
                }
                else {
                    this.hwb.Clear();
                    this.DialogResult = System.Windows.Forms.DialogResult.None;
                }
                this.Close();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnCloseIcon_Click(object sender, EventArgs e)
        {
            this.hwb.Clear();
            this.DialogResult = System.Windows.Forms.DialogResult.None;
            this.Close();
        }

        private void comboBox_brushWidth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox_brushWidth.SelectedIndex > 0)
            {
                this.hwb.PenWidth = this.penWidthList[this.comboBox_brushWidth.SelectedIndex];
            }
            else
            {
                this.hwb.PenWidth = this.penWidthList[0];
            }
        }
    }
}
