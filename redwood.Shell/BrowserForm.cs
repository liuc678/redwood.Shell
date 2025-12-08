using CefSharp;
using CefSharp.JavascriptBinding;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace redwood.Shell
{
    public partial class BrowserForm : Form
    {
        private readonly ChromiumWebBrowser browser;

        public BrowserForm()
        {
            InitializeComponent();

            WindowState = FormWindowState.Maximized;
            this.Text = ConfigurationManager.AppSettings["Title"];
            mnuText.Text = this.Text;
            string url = CustomConfig.Current.URL;
            if (string.IsNullOrEmpty(url))
            {
                //string filePath = Application.StartupPath;
                //filePath = "file:///" + filePath.Replace("\\", "/");
                //url = filePath + "/a.html";
            }

            browser = new ChromiumWebBrowser("")
            {
                Dock = DockStyle.Fill,
            };
            this.Controls.Add(browser);
            //CefSharpSettings.LegacyJavascriptBindingEnabled = true;// 不加这句会提示异常：CefSharpSettings.LegacyJavascriptBindingEnabled is currently false,
            //browser.IsBrowserInitializedChanged += OnIsBrowserInitializedChanged;

            browser.JavascriptObjectRepository.Settings.LegacyBindingEnabled = true;
            //CefSharpSettings.WcfEnabled = true;
            // 添加你的C#类为可由JavaScript调用

            //var bind = new BindingOptions
            //{
            //    Binder = new DefaultBinder(new MyNameConverter())                
            //};
            // browser.JavascriptObjectRepository.Settings.LegacyBindingEnabled
           
            {
                browser.JavascriptObjectRepository.NameConverter = new MyNameConverter();            
                var obj = new JsEvent();
                obj.ReportPath = Path.Combine(Application.StartupPath, "fastreports");
                browser.JavascriptObjectRepository.Register("desktop", obj, false);

            }//browser.JavascriptObjectRepository.Register("jsObj", new JsEvent(), false, new BindingOptions { CamelCaseJavascriptNames = false });

            {
                //var menu = new ContextMenu();
                //menu.MenuItems.Add("刷新", new EventHandler(this.刷新ToolStripMenuItem_Click));
                //browser.ContextMenu = menu;
            }
            browser.MenuHandler = new MenuHandler();
            LoadUrl(url);
        }


        private void LoadUrl(string url)
        {
            if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
            {
                // this.urlTextBox.Text = url;
                browser.Load(url);
                //browser.RegisterJsObject("jsObj", new JsEvent(), new CefSharp.BindingOptions() { CamelCaseJavascriptNames = false }); //交互数据                                           

            }
        }

        private void ShowDevToolsMenuItemClick(object sender, EventArgs e)
        {
            browser.ShowDevTools();
        }


        private void BrowserForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            browser.Dispose();
        }        

        #region 菜单事件

        private void 打开控制台ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            browser.ShowDevTools();
        }
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show(); // 显示窗体
            //this.WindowState = FormWindowState.Maximized; // 恢复正常状态
            notifyIcon1.Visible = false; // 隐藏托盘图标
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 系统配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmConfig().ShowDialog();
        }

        private void 系统目录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Explorer.exe", Application.StartupPath);
        }

        private void 打印模版缓存目录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Explorer.exe", CustomConfig.GetReport_Path(true));
        }

        private void 清空打印模版缓存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
"你确定要清空打印模版缓存吗？", // 消息内容
"确认操作", // 对话框标题
MessageBoxButtons.YesNo, // 按钮类型：是和否
MessageBoxIcon.Question // 图标类型：问号
);
            if (result == DialogResult.Yes)
            {
                var folderPath = CustomConfig.GetReport_Path();
                if (Directory.Exists(folderPath))
                {
                    DirectoryInfo dir = new DirectoryInfo(folderPath);

                    // 删除所有文件
                    foreach (FileInfo file in dir.EnumerateFiles())
                    {
                        file.Delete();
                    }

                    // 删除所有子目录
                    foreach (DirectoryInfo subDir in dir.EnumerateDirectories())
                    {
                        subDir.Delete(true); // 递归删除子目录及其内容
                    }

                    MessageBox.Show("清空成功！");
                }
                else
                {
                   
                }
            }        
        }

        private void 检测打印模版缓存目录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomConfig.GetReport_Path(true);
            MessageBox.Show("目录已经创建好，请放心使用");
        }
        #endregion

        #region 下载文件

        #endregion

        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            browser.Reload();
        }
    }


    public class MyNameConverter : IJavascriptNameConverter
    {
        public string ConvertReturnedObjectPropertyAndFieldToNameJavascript(MemberInfo memberInfo)
        {
            return ConvertToJavascript(memberInfo);
        }

        public string ConvertToJavascript(MemberInfo memberInfo)
        {
            if ("CefCardReader".Equals(memberInfo.DeclaringType.Name))
            {
                string name = memberInfo.Name;
                if (name.Length == 1)
                {
                    return name;
                }
                //if (name.All(char.IsUpper))
                //{
                //    return name;
                //}
                var firstHalf = name.Substring(0, 1);
                var remainingHalf = name.Substring(1);
                return firstHalf.ToLowerInvariant() + remainingHalf;
            }
            return memberInfo.Name;
        }
    }

    public class JsEvent
    {
        public JsEvent()
        {
            this.msg = "redwood";
            this.version = "1";
            this.type = "win7-c#";
        }

        public string ReportPath { get; set; }

        public string msg { get; set; }
        public string version { get; set; }
        public string type { get; set; }   
             

        public void FR(Dictionary<string,object> paramList)
        {

            try
            {
                string token = paramList["token"].ToString();

                string host = paramList["host"].ToString();
                string reportTempId = paramList["reportTempId"].ToString();
                string rowGuids = paramList["rowGuids"].ToString();
                int version = int.Parse(paramList["version"].ToString());
                bool isPrev =bool.Parse(paramList["isPrew"].ToString());

                if (host[host.Length - 1] == '/')
                {
                    host = host.Substring(0, host.Length - 1);
                }

                //确保打印模板
                this.ExistTemplate(token, host, reportTempId, version);              

                string exeFileName = isPrev ? "fastreportXmlUrl.exe" : "frwithoutpreviewXML.exe";
                string param ="\"" +GetReportFileName(reportTempId,version) + "\" \"" + host + "/base/print/fast-client?token=" + token + "&reportTempId=" + reportTempId + "&rowGuids=" + rowGuids +"\"";
                                
                string exeFileName_Full = Path.Combine(Application.StartupPath, exeFileName);
                WriteLog(exeFileName_Full + " " + param);
                Process p = Process.Start(exeFileName_Full, param);                

            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }

            //p.WaitForExit(); // 等待外部程序退出
        }

        /// <summary>
        /// 检测模板是否存在，如果不存在，则下载
        /// 返回：是否有打印模板
        /// </summary>
        /// <returns></returns>
        public  string ExistTemplate(string token, string host, string reportTempId, int version)
        {
            string filename = Path.Combine(CustomConfig.GetReport_Path(true), this.GetReportFileName(reportTempId, version));
            if(!File.Exists(filename))
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

        static void WriteLog(string msg)
        {
            File.AppendAllText(Application.StartupPath + "\\log.txt", msg +"\r\n");
        }
    }
}
