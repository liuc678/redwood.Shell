using BaseClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace redwood.autoupdate
{
    public partial class frmAdmin : Form
    {
        public frmAdmin()
        {
            InitializeComponent();

            SetRead();
            LoadCfg();
        }        

        void LoadCfg()
        {
            
            cmbList.Items.Clear();

            //查询本程序目录下的配置文件目录
            var dir = new DirectoryInfo(Application.StartupPath);
            foreach (var path in dir.GetDirectories())
            {
                //var mg = FileInfoManage.LoadFromFile(path.FullName);
                //if (mg == null)
                //    continue;
                //FileMangeInfo info = new FileMangeInfo();
                //info.Title = path.Name;
                //info.Item = mg;
                if (string.Compare(path.Name, "publish", true) == 0)
                    continue;
                cmbList.Items.Add(path.Name);
            }
        }
        FileInfoManage Entity;
        List<string> excludeFileNameList = new List<string>();
        void ShowEntity(FileInfoManage mg)
        {
            txtSystemName.Text = mg.SystemName;
            txtVersion.Text = mg.Version;
            txtExeName.Text = mg.StartExe_Name;
            txtArgs.Text = mg.StartExe_Args;
            txtExtFileName.Text = mg.extFileName;
            this.ShowDataGrid(mg.List);
            this.Entity = mg;
            SetRead();

            //显示排除文件列表
            {
                string txtFileName = Path.Combine(Application.StartupPath, GetSelectName() + ".txt");
                this.excludeFileNameList.Clear();
                if (File.Exists(txtFileName))
                {
                    var txt = File.ReadLines(txtFileName);                    
                    this.excludeFileNameList.AddRange(txt);
                    txtExcludeFileName.Lines = excludeFileNameList.ToArray();
                }
            }
        }      

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.LoadCfg();
            MessageBox.Show("加载完成", "提示信息");
        }

        string GetSelectName()
        {
            return this.cmbList.SelectedItem.ToString();
        }

        private void cmbList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = GetSelectName();
            var mg = FileInfoManage.LoadFromFile(Path.Combine(Application.StartupPath, name + ".xml"));
            if(mg == null)
            {
                mg = new FileInfoManage();
                mg.SystemName = name;
                mg.Version = "1.0.0.0";
                mg.extFileName = ".deloy";
            }
            this.ShowEntity(mg);
        }

        private void btnReload_Click_1(object sender, EventArgs e)
        {
            SetEdit();
            this.SetEntity();
            this.Entity.Make(Path.Combine(Application.StartupPath, GetSelectName()),this.excludeFileNameList.ToArray());
            ShowDataGrid(this.Entity.List);
            MessageBox.Show("文件已重新加载完成，请设置版本号", "提示信息");
        }

        private void btnPublic_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtVersion.Text))
            {
                MessageBox.Show("请设置版本号", "提示信息");
                return;
            }
            SetEntity();
            string name = GetSelectName();
            string publishPath = Path.Combine(Application.StartupPath, "publish", name);
            this.Entity.Publish(
                Path.Combine(Application.StartupPath,name),
                publishPath,true);
            this.Entity.SaveToFile(Path.Combine(publishPath, "filelist.xml"));
            this.Entity.SaveToFile(Path.Combine(Application.StartupPath, name +".xml"));
            File.WriteAllLines(Path.Combine(Application.StartupPath, GetSelectName() + ".txt"), excludeFileNameList.ToArray());
            SetRead();
            MessageBox.Show("程序已发布到【" + publishPath + "】目录下", "提示信息");
        }

        void SetRead()
        {
            txtSystemName.Enabled = false;
            txtVersion.Enabled = false;
            txtExeName.Enabled = false;
            txtArgs.Enabled = false;
            txtExtFileName.Enabled = false;            
            btnPublic.Enabled = false;
            btnReload.Enabled = true;          
        }

        void SetEdit()
        {
            txtSystemName.Enabled = true;
            txtVersion.Enabled = true;
            txtExeName.Enabled = true;            
            txtArgs.Enabled = true;
            txtExtFileName.Enabled = true;
            btnPublic.Enabled = true;
        }

        void SetEntity()
        {
            this.Entity.SystemName = txtSystemName.Text;
            this.Entity.Version = txtVersion.Text;
            this.Entity.StartExe_Name = txtExeName.Text;
            this.Entity.StartExe_Args = txtArgs.Text;
            this.Entity.extFileName = txtExtFileName.Text;
            this.excludeFileNameList.Clear();
            this.excludeFileNameList.AddRange(txtExcludeFileName.Lines);            
        }

        void ShowDataGrid(BaseClass.FileInfo[] list)
        {
            lblGridTitle.Text = string.Format("一共有{0}个文件",list==null? 0:list.Length);
            this.dataGridView1.DataSource = list;
        }
    }
}
