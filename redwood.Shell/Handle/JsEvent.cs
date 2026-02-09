using redwood.Shell;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace redwood.shell.Handle
{
    public class JsEvent
    {
        public JsEvent(BrowserForm form, string path)
        {
            this.msg = "redwood";
            this.version = "1";
            this.type = "win7-c#";
            this.MainForm = form;
            this.ReportPath = path;
        }
        public BrowserForm MainForm { get; set; }
        public string ReportPath { get; set; }

        public string msg { get; set; }
        public string version { get; set; }
        public string type { get; set; }

        public void setTitle(string text)
        {
            this.MainForm.Invoke(
                new Action(() =>
                {
                    MainForm.SetTitle(text);
                }
                ));
        }

        public bool openURL(string url)
        {
            var cfg = CustomConfig.Current;
            if(string.IsNullOrEmpty(cfg.ChromeExeName))
            {
                return false;
            }
            else
            { 
                Process p = Process.Start(cfg.ChromeExeName, url);
                return true;
            }
        }

        public void FR(Dictionary<string, object> paramList)
        {

            try
            {
                string token = paramList["token"].ToString();

                string host = paramList["host"].ToString();
                string reportTempId = paramList["reportTempId"].ToString();
                string rowGuids = paramList["rowGuids"].ToString();
                int version = int.Parse(paramList["version"].ToString());
                bool isPrev = bool.Parse(paramList["isPrew"].ToString());

                if (host[host.Length - 1] == '/')
                {
                    host = host.Substring(0, host.Length - 1);
                }

                //确保打印模板
                this.ExistTemplate(token, host, reportTempId, version);

                string exeFileName = isPrev ? "fastreportXmlUrl.exe" : "frwithoutpreviewXML.exe";
                string param = "\"" + GetReportFileName(reportTempId, version) + "\" \"" + host + "/base/print/fast-client?token=" + token + "&reportTempId=" + reportTempId + "&rowGuids=" + rowGuids + "\"";

                string exeFileName_Full = Path.Combine(Application.StartupPath, exeFileName);
                //WriteLog(exeFileName_Full + " " + param);
                Process p = Process.Start(exeFileName_Full, param);

            }
            catch (Exception E)
            {
                WriteLog(E);
                MessageBox.Show(E.Message,"提示信息");
            }

            //p.WaitForExit(); // 等待外部程序退出
        }

        /// <summary>
        /// 检测模板是否存在，如果不存在，则下载
        /// 返回：是否有打印模板
        /// </summary>
        /// <returns></returns>
        public string ExistTemplate(string token, string host, string reportTempId, int version)
        {
            string filename = Path.Combine(CustomConfig.GetReport_Path(true), this.GetReportFileName(reportTempId, version));
            if (!File.Exists(filename))
            {
                string url = host + "/base/print/downloadTemp?reportTempId=" + reportTempId;
                FileDownloader.DownloadFile(url, filename, token);
            }
            return filename;
        }

        string GetReportFileName(string reportTempId, int version)
        {
            return string.Format("{0}-{1}.frx", reportTempId, version);
        }

        public static void WriteLog(string msg)
        {
            File.AppendAllText(Application.StartupPath + "\\log.txt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "------------\n" + msg + "\r\n");
        }

        public static void WriteLog(Exception e)
        {
           WriteLog(e.Message + "\n" + e.Source + "\n" + e.StackTrace);
        }
    }
}
