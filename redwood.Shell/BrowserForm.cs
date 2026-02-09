using CefSharp;
using CefSharp.WinForms;
using redwood.shell.Handle;
using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace redwood.Shell
{
    public partial class BrowserForm : Form
    {
        private ChromiumWebBrowser browser;

        static BrowserForm _current = null;

        public static BrowserForm Current
        {
            get
            {
                if (_current == null)
                {
                    _current = new BrowserForm();
                }
                return _current;
            }
        }

        string System_Title;
        string VerString;

        public BrowserForm()
        {
            InitializeComponent();
            this.label1.Visible = false;

            WindowState = FormWindowState.Maximized;           

            string systemTitle = ConfigurationManager.AppSettings["Title"];
            string assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.System_Title = systemTitle;
            this.VerString = " Ver:" + assemblyVersion;
            mnuText.Text = this.System_Title;
            this.SetTitle("");
            string url = CustomConfig.Current.URL;                      


            var move1 = new ControlMove(pnlSearch);

            CreateBrowse(url);
            this.Controls.Add(browser);
        }

        private void CreateBrowse(string url)
        {
            browser = new ChromiumWebBrowser("")
            {
                KeyboardHandler = new KeyBoardHander()
                {
                    Form = this,
                },
                Dock = DockStyle.Fill,
                RequestHandler = new MyRequestHandler(),

            };

            //CefSharpSettings.LegacyJavascriptBindingEnabled = true;// 不加这句会提示异常：CefSharpSettings.LegacyJavascriptBindingEnabled is currently false,
            //browser.IsBrowserInitializedChanged += OnIsBrowserInitializedChanged;

            browser.JavascriptObjectRepository.Settings.LegacyBindingEnabled = true;
            
            {
                browser.JavascriptObjectRepository.NameConverter = new MyNameConverter();
                var obj = new JsEvent(this, Path.Combine(Application.StartupPath, "fastreports"));
                //obj.ReportPath = Path.Combine(Application.StartupPath, "fastreports");
                browser.JavascriptObjectRepository.Register("desktop", obj, false);

                browser.DownloadHandler = new MyDownloadHandler();
                browser.FindHandler = new CustomFindHandler()
                {
                    Form = this,
                };
            }

            browser.MenuHandler = new MenuHandler(this);
            
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
                browser.Find(text, false, false, true);
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

        public void SetSearchResult(int count, int index)
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

        public void SetTitle(string text)
        {
            this.Text = this.System_Title + " " + text + " " + VerString;
        }

        //public void InvokeSetTitle(string txt)
        //{
        //    this.Invoke(new Action(() => {
        //        SetTitle(txt);
        //    }));
        //}           
    }
}
