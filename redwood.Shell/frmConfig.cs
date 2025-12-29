using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            //if (!cfg.SetLogoutURL(txtLogout.Text))
            //{
            //    MessageBox.Show("输入退出网址不正确！");
            //    return;
            //}

            cfg.SaveToFile();           
            var mainForm = BrowserForm.Current;
            if(mainForm != null)
            {
                mainForm.ReloadHomeURL();
                MessageBox.Show("保存成功，正在打开网址");
            }
            else
            {
                MessageBox.Show("保存成功，请重新打开程序");
            }
            
            Close();
        }
    }
}
