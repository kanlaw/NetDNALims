using IMClient.Controls.Tools;
using System;
using System.IO;
using System.Windows.Forms;

namespace CefSharp.Handler
{
    public class CefDownloadHandler : IDownloadHandler
    {
        public CefDownloadHandler()
        {
        }

        public void OnBeforeDownload(IBrowser browser, DownloadItem downloadItem, IBeforeDownloadCallback callback)
        {
            Console.WriteLine("[{0}] File:{1}", downloadItem.Id, downloadItem.SuggestedFileName);
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.SupportMultiDottedExtensions = false;
            saveFileDialog.Filter = "All|*.*";
            //saveFileDialog.FileName = System.IO.Path.GetFileName("");
            saveFileDialog.FileName = downloadItem.SuggestedFileName;
            DialogResult result = saveFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                callback.Continue(saveFileDialog.FileName, false);
            }

            //FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            //if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            //{
            //    string path = folderBrowserDialog.SelectedPath;
            //    callback.Continue(path+ "\\" + downloadItem.SuggestedFileName, false);
            //}
               
        }
            

        public void OnDownloadUpdated(IBrowser browser, DownloadItem downloadItem, IDownloadItemCallback callback)
        {
            Console.WriteLine("[{0}] {1} {2}/{3}", downloadItem.Id, downloadItem.SuggestedFileName, downloadItem.ReceivedBytes, downloadItem.TotalBytes);
            if (downloadItem.IsComplete)
            {
                FrmMsg.Show(MsgKind.ok, "提示", "文件下载成功");
                Console.WriteLine("[{0}] File:{1} OK {2}/{3}", downloadItem.Id, downloadItem.FullPath, downloadItem.ReceivedBytes, downloadItem.TotalBytes);
            }
        }
    }
}