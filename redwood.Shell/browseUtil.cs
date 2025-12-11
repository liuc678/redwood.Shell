using CefSharp;
using System;
using System.IO;

namespace redwood.Shell
{

    public class MyDownloadHandler : IDownloadHandler
    {
        public bool CanDownload(IWebBrowser chromiumWebBrowser, IBrowser browser, string url, string requestMethod)
        {
            return true;
        }

        public void OnBeforeDownload(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem downloadItem, IBeforeDownloadCallback callback)
        {
            if (!callback.IsDisposed)
            {
                using (callback)
                {
                    // 弹出保存对话框
                    callback.Continue(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), downloadItem.SuggestedFileName), showDialog: true);
                }
            }
        }

        public void OnDownloadUpdated(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem downloadItem, IDownloadItemCallback callback)
        {
            if (downloadItem.IsComplete)
            {
                // 下载完成通知
                System.Windows.Forms.MessageBox.Show($"文件已下载到: {downloadItem.FullPath}");
            }
        }
    }
}
