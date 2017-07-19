using NAudio.CoreAudioApi;
using NAudio.Wave;
using IMClient.Controls.Base;
using IMClient.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IMClient.XMPP.Forms
{
    public partial class RecordForm : FrameBase
    {
        /// <summary>
        /// 滑块
        /// </summary>
        private Rectangle runRect = new Rectangle(new Point(0, 0), new Size(5,10));

        float limitTime = 10;
        float currentTime = 0;

        bool isRun = false;

        public RecordForm()
        {
            
            InitializeComponent();
            #if DEBUG
            recordOver.Visible = true;
            #endif
            this.StartPosition = FormStartPosition.CenterParent;
            runRect= new Rectangle(new Point(0, 2), new Size(14, pnlSave.Height-4));
        }


     
        MemoryStream soundStream;
        string encodeData;

        /// <summary>
        /// 文件名
        /// </summary>
        public string UploadFileName = string.Empty;

        NAudio.Wave.WaveFileWriter encoder;
        IWaveIn source;

        /// <summary>
        /// 录音开始的Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void recordStart_Click(object sender, EventArgs e)
        {
            //this.source = new AudioCaptureDevice()
            //{
            //    DesiredFrameSize = 4096,
            //    SampleRate = 22050,

            //    Format = SampleFormat.Format16Bit
            //this.source.NewFrame += Source_NewFrame;
            //this.source.AudioSourceError += Source_AudioSourceError;
            //this.source.RecordingStopped += Source_RecordingStopped;

            this.source = new WaveIn();
            this.source.WaveFormat = new WaveFormat(8000, 1);
            this.source.DataAvailable += this.Source_NewFrame;
            this.source.RecordingStopped += Source_RecordingStopped;

            //MemoryStream s = new MemoryStream();
            //this.encoder = new WaveEncoder(s);

            this.encoder = new WaveFileWriter(Path.Combine(Application.StartupPath,"sounds",string.Format("{0}.wav",System.IO.Path.GetRandomFileName())),this.source.WaveFormat);

            this.source.StartRecording();

            //this.soundStream = s;
        }

        private void Source_RecordingStopped(object sender, StoppedEventArgs e)
        {

            if (this.InvokeRequired)
            {
                this.BeginInvoke(new EventHandler<StoppedEventArgs>(Source_RecordingStopped), sender, e);
            }
            else
            {
                if (source != null) // 关闭录音对象
                {
                    this.source.Dispose();
                    this.source = null;
                }
                if (encoder != null)//关闭文件流
                {
                    encoder.Close();
                    encoder.Dispose();
                    encoder = null;
                }
                if (e.Exception != null)
                {
                    MessageBox.Show(String.Format("出现问题 {0}", e.Exception.Message));
                }
            }
            
        }

        //TODO:录音发生错误
        //private void Source_AudioSourceError(object sender, AudioSourceErrorEventArgs e)
        //{
        //    string s = e.Description;   
        //}

        // TODO:更新录音时间
        private void Source_NewFrame(object sender, WaveInEventArgs e)
        {
            //this.encoder.Encode(e.Signal);
            //this.currentTime = e.Signal.Duration.Seconds;
            //if (e.Signal.Duration.Seconds >= limitTime)//录制时间超过就停止
            //{
            //    endSave();
            //    //tmVoice.Stop();
            //}
            //this.pnlSave.Invalidate();

            this.encoder.Write(e.Buffer, 0, e.BytesRecorded);
            if((this.encoder.Length/this.encoder.WaveFormat.AverageBytesPerSecond)>30)
            {
                this.source.StopRecording();
            }


        }

        public delegate void RecordOverHandle(string recoedStr);
        public event RecordOverHandle recordOverSend;
        /// <summary>
        /// 录音结束的Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void recordOver_Click(object sender, EventArgs e)
        {
        //    byte[] content = File.ReadAllBytes(@"C:\Users\user\Desktop\元素添加测试\Source\default\voice\ring.wav");
        //    var startpoint1 = 0;
        //    string mdfstr1 = string.Empty;

        //    string fileName1 = Guid.NewGuid().ToString().Replace("-", "")+".wav";
        //    string url1 =SysParams.FileServer+ fileName1 + ".voice";
        //    Dictionary<string, string> dict = new Dictionary<string, string>();
        //    dict["filename"] = fileName1;
        //    Common.UpLoadFileByBytes(fileName1, url1, SysParams.Limit_UpData, startpoint1, mdfstr1, content,dict);

        //    UploadFileName = fileName1;
        //    GC.Collect();
        //    this.Close();
        //    return;
        //    //TODO:停止录音
        //    this.source.SignalToStop();
        //    this.source.WaitForStop();

        //    //TODO:音频流转成base64
        //    byte[] raw = this.soundStream.ToArray();



        //    //var startpoint = isResume(mdfstr, Path.GetExtension(fName));
        //    var startpoint = 0;
        //    string mdfstr = string.Empty;
        //    string url = string.Empty;
        //    string fileName = Guid.NewGuid().ToString().Replace("-", "");

        //   // Common.UpLoadFileByMemoryStream(fileName, url, 64, startpoint, mdfstr);
        //    //Common.UpLoadFileByBytes(fileName, url, 64, startpoint, raw);

        //    //string b64 = Convert.ToBase64String(raw);

        //    //this.encodeData = b64;

            if (recordOverSend != null)
            {
                recordOverSend(this.encodeData);
             }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void recordFromClose_Click(object sender, EventArgs e)
        {
            if (this.soundStream != null)
            {
                this.soundStream.Close();
                this.soundStream.Dispose();
                this.UploadFileName = string.Empty;
            }
            this.Close();
        }

       
        private void tmVoice_Tick(object sender, EventArgs e)
        {
            if (currentTime < limitTime)
            {
                //currentTime++;
                if (currentTime == 0)
                {
                    lblTm.Text = "00:10";
                }
                else if (currentTime < this.limitTime)
                {
                    lblTm.Text = "00:" + (this.limitTime - currentTime).ToString("00");
                }
                else //10秒钟 自动结束录音
                {
                    //endSave();
                    //tmVoice.Stop();
                }
                this.pnlSave.Invalidate();
            }
            else {
                //tmVoice.Stop();
            }
        }

        private void pnlSave_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int lineWidth = this.pnlSave.Width - 2;
            g.DrawLine(new Pen(Color.FromArgb(200, 200, 200), 6.0f), 
                new Point(1, pnlSave.Height / 2),
                new Point(lineWidth, pnlSave.Height / 2));

            //if (isRun)
            //{
                int x =(int)((currentTime / limitTime) * (lineWidth- this.runRect.Width));
                //this.runRect.Location = new Point(x, this.runRect.Location.Y);
                //g.FillRectangle(new SolidBrush(Color.Orange), this.runRect);
                g.DrawLine(new Pen(Color.FromArgb(17,112,193), 6.0f),
               new Point(1, pnlSave.Height / 2),
               new Point((int)(lineWidth*(currentTime / limitTime)), pnlSave.Height / 2));
            //}
            //else {
            //    int x = (int)((currentTime / limitTime) * (lineWidth - this.runRect.Width));
            //    //this.runRect.Location = new Point(x, this.runRect.Location.Y);
            //    //g.FillRectangle(new SolidBrush(Color.Orange), this.runRect);
            //    g.DrawLine(new Pen(Color.Red, 6.0f),
            //   new Point(1, pnlSave.Height / 2),
            //   new Point((int)(lineWidth * (currentTime / limitTime)), pnlSave.Height / 2));
            //}
        }


        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            currentTime = 0;
            isRun = false;
            this.tmVoice.Stop();
            this.pnlSave.Invalidate();

            if (this.soundStream != null)
            {
                this.soundStream.Close();
                this.soundStream.Dispose();
            }
        }

        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFinish_Click(object sender, EventArgs e)
        {
            //this.tmVoice.Start();
            //return;
            switch (isRun)
            {
                case true://正在录音
                    endSave();
                    break;
                case false://还未录音
                    beginSave();
                    btnFinish.Staticpic = Properties.Resources.start_record_normal_03;
                    btnFinish.Activepic = btnFinish.Presspic = Properties.Resources.start_record_over_03;
                    break;
            }
       
        }

        /// <summary>
        /// 结束录音
        /// </summary>
        public void endSave()
        {
            //this.tmVoice.Stop();
            this.isRun = false;



            //TODO:停止录音


            //this.source.SignalToStop();
            //this.source.WaitForStop();

            this.source.StopRecording();
            this.source.Dispose();
            this.encoder.Dispose();

            //TODO:音频流转成base64
            //byte[] raw = this.soundStream.ToArray();

            var startpoint = 0;
            string mdfstr = string.Empty;
            string url = string.Empty;
            // string fileName = Guid.NewGuid().ToString().Replace("-", "");


            string mdfstr1 = string.Empty;

            //string fileName1 = Guid.NewGuid().ToString().Replace("-", "") + ".wav";
            fileName = fileName + ".wav";
            string url1 = SysParams.FileServer + fileName + ".voice";

            byte[] content = File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory + "voices//" + fileName);
            soundStream = new MemoryStream(content);

            using (this.soundStream)//释放录音内存
            {
                this.soundStream.Seek(0, SeekOrigin.Begin);
                Common.UpLoadFileByMemoryStream(fileName, url1, SysParams.Limit_UpData,
                    startpoint, soundStream);
                UploadFileName = fileName;
                this.source.Dispose();
                GC.Collect();
            }

            this.Close();
        }


        string fileName;
        /// <summary>
        /// 开始录音
        /// </summary>
        public void beginSave()
        {
            //Accord.DirectSound.AudioDeviceCollection a1 = new AudioDeviceCollection(AudioDeviceCategory.Capture);
            //Accord.DirectSound.AudioDeviceCollection a2 = new AudioDeviceCollection(AudioDeviceCategory.Output);
            //AudioCaptureDevice a3 = new AudioCaptureDevice();

            //this.source = new AudioCaptureDevice()
            //{
            //    DesiredFrameSize = 4096,
            //    SampleRate = 22050,
            //    Format = SampleFormat.Format16Bit
            //};


            //this.source.NewFrame += Source_NewFrame;
            //this.source.AudioSourceError += Source_AudioSourceError;

            //MemoryStream s = new MemoryStream();
            //this.encoder = new WaveEncoder(s);

            //this.source.Start();

            //this.soundStream = s;
            this.source = new WaveIn();
            this.source.WaveFormat = new WaveFormat(8000, 1);
            this.source.DataAvailable += this.Source_NewFrame;
            this.source.RecordingStopped += Source_RecordingStopped;

            //MemoryStream s = new MemoryStream();
            //this.encoder = new WaveEncoder(s);

            //@"a.wav"
            fileName = Guid.NewGuid().ToString().Replace("-", "");

            string path = Path.Combine(Application.StartupPath, "voices");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = Path.Combine(path, string.Format("{0}.wav", fileName));
            this.encoder = new WaveFileWriter(path, this.source.WaveFormat);
            //this.encoder = new WaveFileWriter(string.Format("{0}.wav", fileName), this.source.WaveFormat);
            this.source.StartRecording();


            isRun = true;
            currentTime = 0;
            this.tmVoice.Start();
        }

        /// <summary>
        /// 录音
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {



            //this.source.NewFrame += Source_NewFrame;
            //this.source.AudioSourceError += Source_AudioSourceError;

            //MemoryStream s = new MemoryStream();
            //this.encoder = new WaveEncoder(s);

            //this.source.Start();

            //this.soundStream = s;

            isRun = true;
            currentTime = 0;
            this.tmVoice.Start();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.DrawRectangle(new Pen(Color.FromArgb(178, 178, 178)),
                new Rectangle(new Point(0, 0), new Size(this.Width-1, this.Height-1)));
        }

        MMDeviceEnumerator devices = new MMDeviceEnumerator();
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                base.OnLoad(e);
                MMDevice d = this.devices.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Multimedia);
                d.AudioEndpointVolume.Mute = false;
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {

                MessageBox.Show("由于缺少录音设备，无法录音。", "提示");
                this.Close();
            }
          
        }
    }
}
