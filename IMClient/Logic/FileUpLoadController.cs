using IMClient.Controls;
using IMClient.Logic.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMClient.Logic
{
    public class FileUpLoadController:IDisposable
    {
        SqlLiteHelper sqlHelper = new SqlLiteHelper();
        //Thread[] thread = new Thread[SysParams.Limit_FileUPLoad];
        List<Thread> threadList = new List<Thread>();
        ~FileUpLoadController()
        {
           
        }

        Thread mainThread = null;

        public DataTable dtMission = null;

        public string fromUser = string.Empty;
        public string toUser = string.Empty;

        public FileUpLoadController(string fromUser,string toUser)
        {
            this.fromUser = fromUser;
            this.toUser = toUser;
            dtMissionInit();
            mainThread = new Thread(StartUpload);
            mainThread.IsBackground = true;
            mainThread.Start();
        }


        /// <summary>
        /// fileId=任务id
        /// FileSize=文件大小
        /// UploadSize=文件上传量
        /// Status=任务状态 0 未开始 1 进行中 2 完成
        /// fileType=任务类型 0 在线 1 离线
        /// item=上传任务控件 
        /// </summary>
        public void dtMissionInit()
        {
            dtMission = new DataTable();
            dtMission.Columns.Add("fileId",typeof(string));
            dtMission.Columns.Add("FileSize", typeof(long));
            dtMission.Columns.Add("UploadSize", typeof(long));
            dtMission.Columns.Add("Status", typeof(int));
            dtMission.Columns.Add("item", typeof(ItemUploadFile));
            dtMission.Columns.Add("fileType", typeof(int));//文件收发 类型 在线/离线
            dtMission.Columns.Add("Operation", typeof(int));//操作类型 上传/下载
            dtMission.Columns.Add("currentSize", typeof(long));//下载/上传 数据量
            dtMission.Columns["Status"].DefaultValue = 0;
            dtMission.Columns["UploadSize"].DefaultValue = 0;
            dtMission.Columns["fileType"].DefaultValue = 1;
            dtMission.Columns["Operation"].DefaultValue = 0;
            dtMission.Columns["currentSize"].DefaultValue = 0;
            //dtMission.Columns.Add("fileclass", typeof(FileClass));
        }

        /// <summary>
        /// 添加上传任务方法
        /// </summary>
        /// <param name="file"></param>
        public void dtMissionAdd(FileClass file, ItemUploadFile item,string fromUser,string toUser)
        {
            lock(this.dtMission)
            {
                DataRow dr = dtMission.NewRow();
                dr["fileId"] = item.File.FileId;
                dr["FileSize"] =item.File.FileSize;
                dr["item"] = item;
                dr["Operation"] = (int)file.Operation;             
                dtMission.Rows.Add(dr);
                if (file.Operation == OperationType.Download && !file.IsContinue)
                {
                    sqlHelper.AddMission(item.File.FileId, item.File.SaveFileName, item.File.ReceivePath,
        item.File.FileSize.ToString(), 0, (int)file.Operation,fromUser,toUser);
                }
            }
        }

        /// <summary>
        /// 每秒 
        /// </summary>
        public void StartUpload()
        {
            retOffLineMission(this.fromUser, this.toUser);
            while (true)
            {
                if (dtMission != null && dtMission.Rows.Count > 0)
                {
                    DataRow[] drRuningList =  dtMission.Select("Status='1'");
                    if (drRuningList.Length <= SysParams.Limit_FileUPLoad)
                    {
                        DataRow[] drlist = dtMission.Select("Status='0'");
                        if (drlist.Length > 0)
                        {
                            for (int i = 0; i < (SysParams.Limit_FileUPLoad- drRuningList.Length); i++)
                            {
                                if (drlist.Length > i && drlist[i]!=null)
                                {
                                    string fileId = drlist[i]["fileId"].ToString();

                                   
                                    if (threadList.Count < SysParams.Limit_FileUPLoad)
                                    {
                                        ParameterizedThreadStart ps = new ParameterizedThreadStart(ChangeItem); 
                                        Thread t = new Thread(ps);
                                        t.IsBackground = true;
                                        t.Name = fileId;
                                        this.threadList.Add(t);
                                        // this.threadList.Add(t);
                                        t.Start(drlist[i]);
                                    }
                                    else {
                                        List<Thread> tmplist = threadList.FindAll(item => item.ThreadState == ThreadState.Aborted || item.ThreadState == ThreadState.Stopped);
                                        if (tmplist.Count > 0)
                                        {
                                            ParameterizedThreadStart ps = new ParameterizedThreadStart(ChangeItem);
                                            tmplist[0] = new Thread(ps);
                                            tmplist[0].IsBackground = true;
                                            tmplist[0].Name = fileId;
                                            tmplist[0].Start(drlist[i]);
                                        }
                                    }
                                }
                                else {
                                    break;
                                }
                               
                            }
                        }
                    }
                    //if (drRuningList.Length > 0)
                    //{

                    //}
                    //修改界面进度
                }

                Thread.Sleep(500);
            }
        }

   
        public void Abort(string fileId)
        {
            if (this.threadList.Count > 0)
            {
                if (this.threadList.Exists(item => item.Name == fileId))
                {
                    Thread t = this.threadList.Find(item => item.Name == fileId);
                    t.Abort();
                }
            }
            sqlHelper.DeletedMission(fileId);
        }

        #region 上传
        public delegate void UploadFileDelegate(DataRow dr);

        public void Complete(IAsyncResult iar)
        {
            DataRow dr = (DataRow)iar.AsyncState;
            if (dtMission != null && dr.RowState != DataRowState.Detached)
            {
                lock (dtMission)
                {
                    dr["UploadSize"] = Convert.ToInt64(dr["FileSize"]);
                }
            }
        }

        public void UploadFile(DataRow dr)
        {
            //DataRow dr = (DataRow)objDr;
            string fileId = dr["fileId"].ToString();
            long fileSize = Convert.ToInt64(dr["FileSize"]);
            ItemUploadFile item = (ItemUploadFile)dr["item"];
            //dr["FileSize"] = item.File.FileSize;
            Dictionary<string, string> dictParams = new Dictionary<string, string>();
            dictParams["FILENAME"] = item.File.SaveFileName;
            dictParams["FILEID"] = item.File.FileId;
            string url = SysParams.FileServer + "tempFile" + ".file";
            lock(dr.Table)
            {
                dr["Status"] = 1;
            }
            int operation = Convert.ToInt16(dr["operation"]);
            switch (operation)
            {
                case (int)OperationType.Download://下载操作
                    Common.DownLoad_OffLine(fileId, item.File.SaveFileName,item.File.ReceiveSaveFileName, item.File.ReceiveDir, SysParams.Limit_UpData,dr);
                    sqlHelper.DeletedMission(item.File.FileId);
                    if (!string.IsNullOrEmpty(SysParams.tmpDownLoadName))
                    {
                        try
                        {
                            File.Move(item.File.ReceivePath + SysParams.tmpDownLoadName, item.File.ReceivePath);
                        }
                        catch (Exception ex)
                        {

                            throw;
                        }
                    }
                    break;
                case (int)OperationType.UpLoad://上传操作
                    Common.UpLoadFile_OffLine(item.File.FileName, url, SysParams.Limit_UpData, 0, dictParams, dr);
                    break;
            }

            //完成后修改回正确文件名
           

            lock (dr.Table)
            {
                dr["Status"] = 2;
            }
            //this.threadList.Remove(fileId);
        }
        #endregion

        #region 信息传递

        public delegate void SendMessageDelegate(FileClass file);

        public SendMessageDelegate sendMessage;

        #endregion

        #region 界面控制
        public void ChangeItem(object objDr)
        {
            DataRow dr = (DataRow)objDr;
            string fileId = dr["fileId"].ToString();
            ItemUploadFile item = (ItemUploadFile)dr["item"];
            UploadFileDelegate ufd = UploadFile;
            ufd.BeginInvoke(dr,this.Complete,dr);
            while (true)
            {

                if (dr != null && dr.RowState != DataRowState.Detached && dr.RowState != DataRowState.Deleted)
                {
                    long fileSize =0;
                    long currentSize =0;
                    try
                    {
                         fileSize = Convert.ToInt64(dr["FileSize"]);
                         currentSize = Convert.ToInt64(dr["UploadSize"]);
                    }
                    catch (Exception ex)
                    {
                        Thread.Sleep(50);
                        fileSize = Convert.ToInt64(dr["FileSize"]);
                        currentSize = Convert.ToInt64(dr["UploadSize"]);
                    }

                    int value = (int)((currentSize * 100) / fileSize);
                    item.Invoke(new Action(() =>
                    {
                        item.value = value;
                        item.Invalidate();
                        //item.Invalidate();

                    }));
                    if (value >= 100)
                    {
                        this.sendMessage.Invoke(item.File);
                        RemoveItem.Invoke(item.File);
                        this.addFileMessage(item.File);
                        lock (dtMission)
                        {
                            dtMission.Rows.Remove(dr);
                        }
                        break;
                    }
                }
                else {
                    break;
                }
                Thread.Sleep(500);
            }
        }


        public delegate void addMessage_ResultDelegate(FileClass file);
        /// <summary>
        /// 添加 消息记录
        /// </summary>
        public addMessage_ResultDelegate addFileMessage;

        public delegate void RemoveProgressBarDelegate(FileClass file, bool isLoad = false);
        /// <summary>
        /// 删除 上传任务
        /// </summary>
        public RemoveProgressBarDelegate RemoveItem;
        #endregion

        #region 资源释放
        public void Dispose()
        {
            if (mainThread != null && mainThread.ThreadState != ThreadState.Aborted)
            {
                mainThread.Abort();
                mainThread.DisableComObjectEagerCleanup();
            }

            if (this.dtMission != null)
            {
                this.dtMission.Clear();
                this.dtMission.Dispose();
            }

            foreach (Thread item in threadList)
            {
                if (item != null && item.ThreadState != ThreadState.Aborted)
                {
                    item.Abort();
                    item.DisableComObjectEagerCleanup();
                }
            }
            threadList.Clear();

            GC.Collect();
        }
        #endregion

        #region 加载 续传

        public void retOffLineMission(string fromUser, string toUser)
        {
            DataTable dt= sqlHelper.retMission(fromUser,toUser);
            if (dt != null && dt.Rows.Count >= 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    FileClass file = new FileClass();
                    file.IsSender = false;
                    file.Operation = OperationType.Download;
                    file.SendFileType =SendFileType.OffLine;
                    file.FileId = dr["fileid"].ToString();
                    file.IsContinue = true;
                    file.FileName= file.SaveFileName = dr["filename"].ToString();
                    file.ReceivePath = dr["filePath"].ToString();
                    file.ReceiveSaveFileName = file.ReceivePath.Substring(file.ReceivePath.LastIndexOf('\\')+1);
                    file.ReceiveDir = file.ReceivePath.Substring(0, file.ReceivePath.LastIndexOf('\\'));
                    file.FileSize = Convert.ToInt64(dr["fileSize"]);
                    this.AddProcessCotrol.Invoke(file, false);
                }
            }
        }

        public delegate void AddProcessCotrolDelegate(FileClass file, bool isLoad);
        
        public AddProcessCotrolDelegate AddProcessCotrol;

        #endregion
    }
}
