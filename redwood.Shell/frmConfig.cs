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
            this.textBox1.Text = CustomConfig.Current.URL;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var cfg = CustomConfig.Current;
            if (!cfg.SetURL(textBox1.Text))
            {
                MessageBox.Show("输入网址不正确！");
                return;
            }
            cfg.SaveToFile();
            MessageBox.Show("保存成功，请重新打开程序");
            Close();
        }
    }
}
