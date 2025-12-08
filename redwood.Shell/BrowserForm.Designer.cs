namespace redwood.Shell
{
    partial class BrowserForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BrowserForm));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuText = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.清空打印模版缓存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打印模版缓存目录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.检测打印模版缓存目录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.系统配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.系统目录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.打开控制台ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.刷新ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuText,
            this.toolStripSeparator1,
            this.清空打印模版缓存ToolStripMenuItem,
            this.打印模版缓存目录ToolStripMenuItem,
            this.检测打印模版缓存目录ToolStripMenuItem,
            this.系统配置ToolStripMenuItem,
            this.系统目录ToolStripMenuItem,
            this.toolStripSeparator3,
            this.打开控制台ToolStripMenuItem,
            this.toolStripSeparator2,
            this.mnuExit});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(269, 262);
            // 
            // mnuText
            // 
            this.mnuText.Image = global::redwood.Shell.Properties.Resources.logo;
            this.mnuText.Name = "mnuText";
            this.mnuText.Size = new System.Drawing.Size(268, 30);
            this.mnuText.Text = "toolStripMenuItem1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(265, 6);
            // 
            // 清空打印模版缓存ToolStripMenuItem
            // 
            this.清空打印模版缓存ToolStripMenuItem.Name = "清空打印模版缓存ToolStripMenuItem";
            this.清空打印模版缓存ToolStripMenuItem.Size = new System.Drawing.Size(268, 30);
            this.清空打印模版缓存ToolStripMenuItem.Text = "清空打印模版缓存";
            this.清空打印模版缓存ToolStripMenuItem.Click += new System.EventHandler(this.清空打印模版缓存ToolStripMenuItem_Click);
            // 
            // 打印模版缓存目录ToolStripMenuItem
            // 
            this.打印模版缓存目录ToolStripMenuItem.Name = "打印模版缓存目录ToolStripMenuItem";
            this.打印模版缓存目录ToolStripMenuItem.Size = new System.Drawing.Size(268, 30);
            this.打印模版缓存目录ToolStripMenuItem.Text = "打印模版缓存目录";
            this.打印模版缓存目录ToolStripMenuItem.Click += new System.EventHandler(this.打印模版缓存目录ToolStripMenuItem_Click);
            // 
            // 检测打印模版缓存目录ToolStripMenuItem
            // 
            this.检测打印模版缓存目录ToolStripMenuItem.Name = "检测打印模版缓存目录ToolStripMenuItem";
            this.检测打印模版缓存目录ToolStripMenuItem.Size = new System.Drawing.Size(268, 30);
            this.检测打印模版缓存目录ToolStripMenuItem.Text = "检测打印模版缓存目录";
            this.检测打印模版缓存目录ToolStripMenuItem.Click += new System.EventHandler(this.检测打印模版缓存目录ToolStripMenuItem_Click);
            // 
            // 系统配置ToolStripMenuItem
            // 
            this.系统配置ToolStripMenuItem.Name = "系统配置ToolStripMenuItem";
            this.系统配置ToolStripMenuItem.Size = new System.Drawing.Size(268, 30);
            this.系统配置ToolStripMenuItem.Text = "系统配置";
            this.系统配置ToolStripMenuItem.Click += new System.EventHandler(this.系统配置ToolStripMenuItem_Click);
            // 
            // 系统目录ToolStripMenuItem
            // 
            this.系统目录ToolStripMenuItem.Name = "系统目录ToolStripMenuItem";
            this.系统目录ToolStripMenuItem.Size = new System.Drawing.Size(268, 30);
            this.系统目录ToolStripMenuItem.Text = "系统目录";
            this.系统目录ToolStripMenuItem.Click += new System.EventHandler(this.系统目录ToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(265, 6);
            // 
            // 打开控制台ToolStripMenuItem
            // 
            this.打开控制台ToolStripMenuItem.Name = "打开控制台ToolStripMenuItem";
            this.打开控制台ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.打开控制台ToolStripMenuItem.Size = new System.Drawing.Size(268, 30);
            this.打开控制台ToolStripMenuItem.Text = "打开控制台";
            this.打开控制台ToolStripMenuItem.Click += new System.EventHandler(this.打开控制台ToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(265, 6);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(268, 30);
            this.mnuExit.Text = "退出";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.刷新ToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(148, 32);
            // 
            // 刷新ToolStripMenuItem
            // 
            this.刷新ToolStripMenuItem.Name = "刷新ToolStripMenuItem";
            this.刷新ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.刷新ToolStripMenuItem.Size = new System.Drawing.Size(147, 28);
            this.刷新ToolStripMenuItem.Text = "刷新";
            this.刷新ToolStripMenuItem.Click += new System.EventHandler(this.刷新ToolStripMenuItem_Click);
            // 
            // BrowserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 450);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BrowserForm";
            this.Text = "BrowserForm";
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuText;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.ToolStripMenuItem 清空打印模版缓存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打印模版缓存目录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 检测打印模版缓存目录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 系统配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 系统目录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开控制台ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem 刷新ToolStripMenuItem;
    }
}