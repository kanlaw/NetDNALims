using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMClient.Logic
{
    public  class FileClass
    {
        public FileClass()
        {
            if (string.IsNullOrEmpty(this.fileId))
            {
                this.fileId = Guid.NewGuid().ToString().Replace("-", "");
            }
        }

        /// <summary>
        /// 缩略图
        /// </summary>
        private string icoUrl = string.Empty;

        /// <summary>
        /// 文件发送对象 
        /// true  发送者
        /// false 接收者
        /// </summary>
        private bool isSender = true;


        /// <summary>
        /// 文件状态 
        /// 默认发送状态
        /// </summary>
        private FileStatus fileStatus = 0;


        /// <summary>
        ///  传递时的Id
        /// </summary>
        private string fileId;

        /// <summary>
        /// 发送任务建立时的独立标示Id
        /// </summary>
        private string sendId;

        /// <summary>
        /// 文件接收目录
        /// </summary>
        private string receivePath;


        /// <summary>
        /// 文件名
        /// </summary>
        private string saveFileName;


        /// <summary>
        /// 文件全部路径
        /// </summary>
        private string fileName;

        /// <summary>
        /// 文件目录
        /// </summary>
        private string folderName;


        /// <summary>
        /// 文件大小
        /// </summary>
        private long fileSize;

         /// <summary>
         /// 文件发送时间
         /// </summary>
        private string sendTime;

        /// <summary>
        /// 上传类型
        /// </summary>
        private SendFileType sendFileType= SendFileType.OnLine;

        /// <summary>
        /// 文件操作类型
        /// </summary>
        private OperationType operation = OperationType.Download;


        /// <summary>
        /// 接收目录
        /// </summary>
        private string receiveDir = string.Empty;

        /// <summary>
        /// 是否续传
        /// </summary>
        private bool isContinue = false;

        /// <summary>
        /// 文件下载的本地保存名
        /// </summary>
        private string receiveSaveFileName = string.Empty;


        public string FileName
        {
            get
            {
                return fileName;
            }

            set
            {
                fileName = value;
            }
        }

        public string FolderName
        {
            get
            {
                return folderName;
            }

            set
            {
                folderName = value;
            }
        }

        public long FileSize
        {
            get
            {
                return fileSize;
            }

            set
            {
                fileSize = value;
            }
        }

        public string SendTime
        {
            get
            {
                return sendTime;
            }

            set
            {
                sendTime = value;
            }
        }

        public string SaveFileName
        {
            get
            {
                return saveFileName;
            }

            set
            {
                saveFileName = value;
            }
        }

        public string FileId
        {
            get
            {
                return fileId;
            }

            set
            {
                fileId = value;
            }
        }

        public string SendId
        {
            get
            {
                return sendId;
            }

            set
            {
                sendId = value;
            }
        }

        public FileStatus FileStatus
        {
            get
            {
                return fileStatus;
            }

            set
            {
                fileStatus = value;
            }
        }

        public bool IsSender
        {
            get
            {
                return isSender;
            }

            set
            {
                isSender = value;
            }
        }

        public string ReceivePath
        {
            get
            {
                return receivePath;
            }

            set
            {
                receivePath = value;
            }
        }

        public string IcoUrl
        {
            get
            {
                return icoUrl;
            }

            set
            {
                icoUrl = value;
            }
        }

        public SendFileType SendFileType
        {
            get
            {
                return sendFileType;
            }

            set
            {
                sendFileType = value;
            }
        }

        public OperationType Operation
        {
            get
            {
                return operation;
            }

            set
            {
                operation = value;
            }
        }

        public string ReceiveDir
        {
            get
            {
                return receiveDir;
            }

            set
            {
                receiveDir = value;
            }
        }

        public bool IsContinue
        {
            get
            {
                return isContinue;
            }

            set
            {
                isContinue = value;
            }
        }

        public string ReceiveSaveFileName
        {
            get
            {
                return receiveSaveFileName;
            }

            set
            {
                receiveSaveFileName = value;
            }
        }
    }
}
