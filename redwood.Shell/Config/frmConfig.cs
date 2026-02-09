using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace redwood.Shell
{
    public partial class frmConfig : frmBase
    {
        public frmConfig()
        {
            InitializeComponent();
            var cfg = CustomConfig.Current;
            this.textBox1.Text = cfg.URL;
            this.txtChromeExeName.Text = cfg.ChromeExeName;
            //txtLogout.Text = cfg.LogoutURL;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var cfg = CustomConfig.Current;
            if (!cfg.SetURL(textBox1.Text))
            {
                MessageBox.Show("输入默认网址不正确！");
                return;
            }

            if (!cfg.SetChromeExeName(txtChromeExeName.Text))
            {
                MessageBox.Show("设置浏览器地址失败");
            }

            cfg.SaveToFile();           
            var mainForm = BrowserForm.Current;
            if(mainForm != null)
            {
                mainForm.ReloadHomeURL();
                MessageBox.Show("保存成功");
            }
            else
            {
                MessageBox.Show("保存成功，请重新打开程序");
            }
            
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = txtChromeExeName.Text;
            if(openFileDialog1.ShowDialog(this)== DialogResult.OK)
            {
                var cfg = CustomConfig.Current;
                if(!cfg.SetChromeExeName(openFileDialog1.FileName))
                {
                    MessageBox.Show("设置失败");
                }
                
                txtChromeExeName.Text = cfg.ChromeExeName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var cfg = CustomConfig.Current;
            if(string.IsNullOrEmpty(cfg.ChromeExeName))
            {
                MessageBox.Show("请先设置浏览器exe文件名称", "提示信息");
                return;
            }
            Process p = Process.Start(cfg.ChromeExeName, "www.baidu.com");
        }       
    }
}
