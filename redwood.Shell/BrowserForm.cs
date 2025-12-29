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
using CefSharp.Structs;

namespace redwood.Shell
{
    public partial class BrowserForm : Form
    {
        private readonly ChromiumWebBrowser browser;

        static BrowserForm _current =null;

        public static BrowserForm Current
        {
            get
            {
                if(_current == null)
                {
                    _current = new BrowserForm();
                }
                return _current;
            }
        }

        public BrowserForm()
        {
            InitializeComponent();
            this.label1.Visible = false;

            WindowState = FormWindowState.Maximized;

            string systemTitle = ConfigurationManager.AppSettings["Title"];
            string assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.Text = systemTitle + " Ver:" + assemblyVersion;
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
                KeyboardHandler = new KeyBoardHander()
                {
                    Form = this,
                },
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

                browser.DownloadHandler = new MyDownloadHandler();
                browser.FindHandler = new CustomFindHandler()
                {
                    Form = this,
                };
            }
            //browser.JavascriptObjectRepository.Register("jsObj", new JsEvent(), false, new BindingOptions { CamelCaseJavascriptNames = false });
            
            browser.MenuHandler = new MenuHandler(this);
            //url = "www.163.com";
            LoadUrl(url);

            var move1 = new ControlMove(pnlSearch);

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

        public void ReloadHomeURL()
        {
            LoadUrl(CustomConfig.Current.URL);
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
        private bool bCloseWindows = false;
        private void BrowserForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bCloseWindows)
            {
                return;
            }

            if (MessageBox.Show("是否确定退出程序？", "提示信息", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }

           //var url = CustomConfig.Current.LogoutURL;
           // if (!string.IsNullOrEmpty(url))
           // {
           //     e.Cancel = true;
           //     this.label1.Visible = true;
                
           //     browser.Visible = false;
           //     browser.FrameLoadEnd += Browser_FrameLoadEnd;
           //     browser.Load(url);                                
           // }
        }
        

        private void CloseWindow()
        {
            this.bCloseWindows = true;
            this.Close();
        }

        private void Browser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {   
            this.Invoke(new Action(CloseWindow));
        }

        private void btnSearch_Close_Click(object sender, EventArgs e)
        {
            pnlSearch.Visible = false;
            browser.Focus();
        }

        public void ShowFind()
        {
            this.Invoke(new Action(() =>
            {
                this.pnlSearch.Visible = true;
                txtSearch.Focus();
            }));            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string text = txtSearch.Text.Trim();            
            if (!string.IsNullOrEmpty(text))
            {
                txtSearch.Text = text;
                lblSearch_Result.Text = "-/-";
                browser.StopFinding(true);
                browser.Find(text, false, false, false);
            }

        }

        private void btnSearch_Up_Click(object sender, EventArgs e)
        {
            string text = txtSearch.Text.Trim();
            if (!string.IsNullOrEmpty(text))
            {
                browser.Find(text,false, false, true);
            }
        }

        private void btnSearch_down_Click(object sender, EventArgs e)
        {
            string text = txtSearch.Text.Trim();
            if (!string.IsNullOrEmpty(text))
            {
                browser.Find(text, true, false, true);
            }
        }

        public void SetSearchResult(int count,int index)
        {
            this.Invoke(new Action(
                () =>
                {
                    this.lblSearch_Result.Text = string.Format("{0}/{1}", index, count);
                    this.btnSearch.Enabled = true;
                }
                ));
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {            
            if (e.KeyCode == Keys.Return)
            {
                this.btnSearch_Click(null, null);
            }
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
                //WriteLog(exeFileName_Full + " " + param);
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

        public static void WriteLog(string msg)
        {
            File.AppendAllText(Application.StartupPath + "\\log.txt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "------------\n" + msg +"\r\n");
        }
    }

    public class CustomFindHandler : IFindHandler
    {
        
        public BrowserForm Form;

        public void OnFindResult(IWebBrowser chromiumWebBrowser, IBrowser browser, int identifier, int count, Rect selectionRect, int activeMatchOrdinal, bool finalUpdate)
        {
            this.Form.SetSearchResult(count, activeMatchOrdinal);
        }
    }
}
