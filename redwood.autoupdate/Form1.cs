using BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace redwood.autoupdate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Host = ConfigurationManager.AppSettings["host"];
            if(Host[Host.Length-1] !='/')
            {
                Host = Host + "/";
            }
        }

        public string Host;

        private async void Form1_Shown(object sender, EventArgs e)
        {
            //下载文件清单目录
            string filename = Path.Combine(Application.StartupPath, "filelist.xml");
            FileInfoManage fileManage;
            {                
                fileManage = FileInfoManage.LoadFromFile(filename);
                if (fileManage == null)
                    fileManage = new FileInfoManage();
            }

            FileInfoManage newFileMange = null;
           
            {
                string newfilename = Path.Combine(Application.StartupPath, "filelist.tmp");
                SetProgress("检测最新版本");
                if (WebUtils.DownloadFile(Host + "/filelist.xml", newfilename))
                {
                    newFileMange = FileInfoManage.LoadFromFile(newfilename);
                }
                if (newFileMange == null)
                {
                    MessageBox.Show("检测最新版本失败，请确定是否已经联网", "提示信息");
                }
            }
            #region 版本更新
            if (newFileMange != null && newFileMange.Version != fileManage.Version)
            {
                //版本更新
                SetProgress("检测需要更新的文件");
                var fileInfoList = newFileMange.BiJiao(fileManage);

               
                SetProgress("开始更新文件");

                progressBar2.Maximum = fileInfoList.Length;
                progressBar2.Visible = true;
                string extFileName = newFileMange.extFileName;
                const int CNT_Retry_Num = 5;
                foreach (var f in fileInfoList)
                {
                    
                        LblTitle2.Text = f.FileName;
                        if (progressBar2.Value + 1 < progressBar2.Maximum)
                            progressBar2.Value++;

                        Application.DoEvents();
                        string fileName = Path.Combine(Application.StartupPath, f.FileName);
                        //检测本地是否已经存在文件

                        string sha = Utils.ComputeFileHash(fileName);
                        if (f.SHA == sha)
                            continue;

                        //下载文件
                        string url = Host + f.FileName.Replace('/', '\\') + extFileName;


                    //WebUtils.DownloadFileAsync(url, fileName);


                    //if (!WebUtils.DownloadFile(url, fileName))
                    //bool bfinish = await WebUtils.DownloadFileAsync(url, fileName);
                    for (int i = 0; i < CNT_Retry_Num; i++)
                    {
                        bool bfinish = await WebUtils.startDownloadAsync(url, fileName, (totalBytesRead, totalLen) =>
                             {
                                 this.Invoke(
                                     new Action(() =>
                                     {
                                         LblTitle2.Text = string.Format("{0} {1:0.##}M / {2:0.##}M", f.FileName, totalBytesRead * 1.0 / 1000000, totalLen / 100000);
                                     }));
                             });
                        //var md = new MultiDownload(5, url, filename);
                        //md.Start(true);
                        //bool bfinish = true;
                       
                        if(bfinish)
                        {
                            //检测下载的sha是否正确
                            if (f.SHA != Utils.ComputeFileHash(fileName))
                            {
                                bfinish = false;
                                if(i <CNT_Retry_Num -1)
                                {
                                    continue;
                                }                                
                            }
                        }
                        if (!bfinish)
                        {
                            MessageBox.Show("下载文件失败", "提示信息");
                            this.Close();
                            return;
                        }
                    }
                }
                newFileMange.SaveToFile(filename);
                fileManage = newFileMange;
            }
            #endregion

            SetProgress("正在启动主程序", -1);

            if (string.IsNullOrEmpty(fileManage.StartExe_Name))
            {
                MessageBox.Show("未设置启动程序，请联系管理员", "提示信息");
            }
            else
            {
                string exeFileName_Full = Path.Combine(Application.StartupPath, fileManage.StartExe_Name);
                Process p = Process.Start(exeFileName_Full, fileManage.StartExe_Args);
            }
            Close();
        }

        void SetProgress(string title, int step = 1)
        {   
            lblTitle.Text = title;
            SetProgress(step);
        }

        void SetTitle2(string text)
        {
            lblTitle.Text = text;
        }

        void SetProgress(int step)
        {
            if (step == -1 || progressBar1.Value + step > progressBar1.Maximum)
                progressBar1.Value = progressBar1.Maximum;
            else
                progressBar1.Value += step;
            Application.DoEvents();
        }       
    }
}
