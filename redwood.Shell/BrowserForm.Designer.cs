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
            this.label1 = new System.Windows.Forms.Label();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.lblSearch_Result = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnSearch_Close = new System.Windows.Forms.Button();
            this.btnSearch_down = new System.Windows.Forms.Button();
            this.btnSearch_Up = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.pnlSearch.SuspendLayout();
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
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("宋体", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1183, 551);
            this.label1.TabIndex = 2;
            this.label1.Text = "正在关闭，请稍等...";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlSearch
            // 
            this.pnlSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSearch.Controls.Add(this.lblSearch_Result);
            this.pnlSearch.Controls.Add(this.btnSearch);
            this.pnlSearch.Controls.Add(this.btnSearch_Close);
            this.pnlSearch.Controls.Add(this.btnSearch_down);
            this.pnlSearch.Controls.Add(this.btnSearch_Up);
            this.pnlSearch.Controls.Add(this.txtSearch);
            this.pnlSearch.Location = new System.Drawing.Point(632, 0);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(551, 55);
            this.pnlSearch.TabIndex = 3;
            this.pnlSearch.Visible = false;
            // 
            // lblSearch_Result
            // 
            this.lblSearch_Result.AutoSize = true;
            this.lblSearch_Result.Location = new System.Drawing.Point(257, 17);
            this.lblSearch_Result.Name = "lblSearch_Result";
            this.lblSearch_Result.Size = new System.Drawing.Size(35, 18);
            this.lblSearch_Result.TabIndex = 5;
            this.lblSearch_Result.Text = "-/-";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(173, 9);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(78, 29);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "搜索";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnSearch_Close
            // 
            this.btnSearch_Close.Location = new System.Drawing.Point(476, 15);
            this.btnSearch_Close.Name = "btnSearch_Close";
            this.btnSearch_Close.Size = new System.Drawing.Size(60, 23);
            this.btnSearch_Close.TabIndex = 3;
            this.btnSearch_Close.Text = "关闭";
            this.btnSearch_Close.UseVisualStyleBackColor = true;
            this.btnSearch_Close.Click += new System.EventHandler(this.btnSearch_Close_Click);
            // 
            // btnSearch_down
            // 
            this.btnSearch_down.Location = new System.Drawing.Point(395, 15);
            this.btnSearch_down.Name = "btnSearch_down";
            this.btnSearch_down.Size = new System.Drawing.Size(75, 23);
            this.btnSearch_down.TabIndex = 2;
            this.btnSearch_down.Text = "下一个";
            this.btnSearch_down.UseVisualStyleBackColor = true;
            this.btnSearch_down.Click += new System.EventHandler(this.btnSearch_down_Click);
            // 
            // btnSearch_Up
            // 
            this.btnSearch_Up.Location = new System.Drawing.Point(314, 15);
            this.btnSearch_Up.Name = "btnSearch_Up";
            this.btnSearch_Up.Size = new System.Drawing.Size(75, 23);
            this.btnSearch_Up.TabIndex = 1;
            this.btnSearch_Up.Text = "前一个";
            this.btnSearch_Up.UseVisualStyleBackColor = true;
            this.btnSearch_Up.Click += new System.EventHandler(this.btnSearch_Up_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(9, 10);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(158, 28);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // BrowserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1183, 551);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BrowserForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BrowserForm_FormClosing);
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnSearch_Close;
        private System.Windows.Forms.Button btnSearch_down;
        private System.Windows.Forms.Button btnSearch_Up;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch_Result;
    }
}