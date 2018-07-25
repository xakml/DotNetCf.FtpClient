namespace DotNetCf.FtpClient.Demo
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.btnDownload = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btnIISSendComamnd = new System.Windows.Forms.Button();
            this.txtIISftpCmd = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnServUSendCmd = new System.Windows.Forms.Button();
            this.txtServUCmd = new System.Windows.Forms.TextBox();
            this.btnServULogin = new System.Windows.Forms.Button();
            this.btnCloseFtp = new System.Windows.Forms.Button();
            this.txtWorkingDir = new System.Windows.Forms.TextBox();
            this.btnServU_CWD = new System.Windows.Forms.Button();
            this.btnServU_PWD = new System.Windows.Forms.Button();
            this.btnIISftp_login = new System.Windows.Forms.Button();
            this.btnIISftp_disconnect = new System.Windows.Forms.Button();
            this.btnIISftp_cwd = new System.Windows.Forms.Button();
            this.txtIISWorkingDir = new System.Windows.Forms.TextBox();
            this.btnIISftp_pwd = new System.Windows.Forms.Button();
            this.btnIISftp_files = new System.Windows.Forms.Button();
            this.btnchdir_dl = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 31);
            this.button1.TabIndex = 0;
            this.button1.Text = "FileSize";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(3, 38);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(115, 31);
            this.btnDownload.TabIndex = 1;
            this.btnDownload.Text = "download";
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(3, 73);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(115, 31);
            this.button2.TabIndex = 2;
            this.button2.Text = "dl from 171";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(3, 108);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(115, 31);
            this.button3.TabIndex = 3;
            this.button3.Text = "GetFolders134";
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnIISSendComamnd
            // 
            this.btnIISSendComamnd.Location = new System.Drawing.Point(163, 38);
            this.btnIISSendComamnd.Name = "btnIISSendComamnd";
            this.btnIISSendComamnd.Size = new System.Drawing.Size(72, 20);
            this.btnIISSendComamnd.TabIndex = 4;
            this.btnIISSendComamnd.Text = "Send cmd";
            this.btnIISSendComamnd.Click += new System.EventHandler(this.btnSendIISFTPCmd_Click);
            // 
            // txtIISftpCmd
            // 
            this.txtIISftpCmd.Location = new System.Drawing.Point(9, 35);
            this.txtIISftpCmd.Name = "txtIISftpCmd";
            this.txtIISftpCmd.Size = new System.Drawing.Size(148, 23);
            this.txtIISftpCmd.TabIndex = 5;
            this.txtIISftpCmd.Text = "NLST";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(3, 145);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(259, 192);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnchdir_dl);
            this.tabPage1.Controls.Add(this.btnIISftp_files);
            this.tabPage1.Controls.Add(this.btnIISftp_pwd);
            this.tabPage1.Controls.Add(this.btnIISftp_cwd);
            this.tabPage1.Controls.Add(this.txtIISWorkingDir);
            this.tabPage1.Controls.Add(this.btnIISftp_disconnect);
            this.tabPage1.Controls.Add(this.btnIISftp_login);
            this.tabPage1.Controls.Add(this.btnIISSendComamnd);
            this.tabPage1.Controls.Add(this.txtIISftpCmd);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(251, 163);
            this.tabPage1.Text = "IIS FTP";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnServU_PWD);
            this.tabPage2.Controls.Add(this.btnServU_CWD);
            this.tabPage2.Controls.Add(this.txtWorkingDir);
            this.tabPage2.Controls.Add(this.btnCloseFtp);
            this.tabPage2.Controls.Add(this.btnServULogin);
            this.tabPage2.Controls.Add(this.btnServUSendCmd);
            this.tabPage2.Controls.Add(this.txtServUCmd);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(269, 163);
            this.tabPage2.Text = "Serv-U";
            // 
            // btnServUSendCmd
            // 
            this.btnServUSendCmd.Enabled = false;
            this.btnServUSendCmd.Location = new System.Drawing.Point(157, 35);
            this.btnServUSendCmd.Name = "btnServUSendCmd";
            this.btnServUSendCmd.Size = new System.Drawing.Size(72, 20);
            this.btnServUSendCmd.TabIndex = 6;
            this.btnServUSendCmd.Text = "Send cmd";
            this.btnServUSendCmd.Click += new System.EventHandler(this.btnServUSendCmd_Click);
            // 
            // txtServUCmd
            // 
            this.txtServUCmd.Location = new System.Drawing.Point(3, 35);
            this.txtServUCmd.Name = "txtServUCmd";
            this.txtServUCmd.Size = new System.Drawing.Size(148, 23);
            this.txtServUCmd.TabIndex = 7;
            this.txtServUCmd.Text = "MLSD";
            // 
            // btnServULogin
            // 
            this.btnServULogin.Location = new System.Drawing.Point(3, 9);
            this.btnServULogin.Name = "btnServULogin";
            this.btnServULogin.Size = new System.Drawing.Size(72, 20);
            this.btnServULogin.TabIndex = 8;
            this.btnServULogin.Text = "login";
            this.btnServULogin.Click += new System.EventHandler(this.btnServULogin_Click);
            // 
            // btnCloseFtp
            // 
            this.btnCloseFtp.Enabled = false;
            this.btnCloseFtp.Location = new System.Drawing.Point(81, 9);
            this.btnCloseFtp.Name = "btnCloseFtp";
            this.btnCloseFtp.Size = new System.Drawing.Size(87, 20);
            this.btnCloseFtp.TabIndex = 9;
            this.btnCloseFtp.Text = "Disconnect";
            this.btnCloseFtp.Click += new System.EventHandler(this.btnCloseFtp_Click);
            // 
            // txtWorkingDir
            // 
            this.txtWorkingDir.Location = new System.Drawing.Point(3, 64);
            this.txtWorkingDir.Name = "txtWorkingDir";
            this.txtWorkingDir.Size = new System.Drawing.Size(148, 23);
            this.txtWorkingDir.TabIndex = 10;
            this.txtWorkingDir.Text = "MLSD";
            // 
            // btnServU_CWD
            // 
            this.btnServU_CWD.Location = new System.Drawing.Point(157, 67);
            this.btnServU_CWD.Name = "btnServU_CWD";
            this.btnServU_CWD.Size = new System.Drawing.Size(72, 20);
            this.btnServU_CWD.TabIndex = 11;
            this.btnServU_CWD.Text = "CWD";
            this.btnServU_CWD.Click += new System.EventHandler(this.btnServU_CWD_Click);
            // 
            // btnServU_PWD
            // 
            this.btnServU_PWD.Location = new System.Drawing.Point(157, 93);
            this.btnServU_PWD.Name = "btnServU_PWD";
            this.btnServU_PWD.Size = new System.Drawing.Size(72, 20);
            this.btnServU_PWD.TabIndex = 12;
            this.btnServU_PWD.Text = "PWD";
            this.btnServU_PWD.Click += new System.EventHandler(this.btnServU_PWD_Click);
            // 
            // btnIISftp_login
            // 
            this.btnIISftp_login.Location = new System.Drawing.Point(3, 12);
            this.btnIISftp_login.Name = "btnIISftp_login";
            this.btnIISftp_login.Size = new System.Drawing.Size(72, 20);
            this.btnIISftp_login.TabIndex = 6;
            this.btnIISftp_login.Text = "login";
            this.btnIISftp_login.Click += new System.EventHandler(this.btnIISftp_login_Click);
            // 
            // btnIISftp_disconnect
            // 
            this.btnIISftp_disconnect.Location = new System.Drawing.Point(89, 12);
            this.btnIISftp_disconnect.Name = "btnIISftp_disconnect";
            this.btnIISftp_disconnect.Size = new System.Drawing.Size(85, 20);
            this.btnIISftp_disconnect.TabIndex = 7;
            this.btnIISftp_disconnect.Text = "disconnect";
            this.btnIISftp_disconnect.Click += new System.EventHandler(this.btnIISftp_disconnect_Click);
            // 
            // btnIISftp_cwd
            // 
            this.btnIISftp_cwd.Location = new System.Drawing.Point(163, 67);
            this.btnIISftp_cwd.Name = "btnIISftp_cwd";
            this.btnIISftp_cwd.Size = new System.Drawing.Size(72, 20);
            this.btnIISftp_cwd.TabIndex = 8;
            this.btnIISftp_cwd.Text = "CWD";
            this.btnIISftp_cwd.Click += new System.EventHandler(this.btnIISftp_cwd_Click);
            // 
            // txtIISWorkingDir
            // 
            this.txtIISWorkingDir.Location = new System.Drawing.Point(9, 64);
            this.txtIISWorkingDir.Name = "txtIISWorkingDir";
            this.txtIISWorkingDir.Size = new System.Drawing.Size(148, 23);
            this.txtIISWorkingDir.TabIndex = 9;
            this.txtIISWorkingDir.Text = "PDA";
            // 
            // btnIISftp_pwd
            // 
            this.btnIISftp_pwd.Location = new System.Drawing.Point(163, 93);
            this.btnIISftp_pwd.Name = "btnIISftp_pwd";
            this.btnIISftp_pwd.Size = new System.Drawing.Size(72, 20);
            this.btnIISftp_pwd.TabIndex = 10;
            this.btnIISftp_pwd.Text = "PWD";
            // 
            // btnIISftp_files
            // 
            this.btnIISftp_files.Location = new System.Drawing.Point(163, 119);
            this.btnIISftp_files.Name = "btnIISftp_files";
            this.btnIISftp_files.Size = new System.Drawing.Size(72, 20);
            this.btnIISftp_files.TabIndex = 11;
            this.btnIISftp_files.Text = "files";
            this.btnIISftp_files.Click += new System.EventHandler(this.btnIISftp_files_Click);
            // 
            // btnchdir_dl
            // 
            this.btnchdir_dl.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.btnchdir_dl.Location = new System.Drawing.Point(9, 105);
            this.btnchdir_dl.Name = "btnchdir_dl";
            this.btnchdir_dl.Size = new System.Drawing.Size(113, 20);
            this.btnchdir_dl.TabIndex = 12;
            this.btnchdir_dl.Text = "切换目录并下载";
            this.btnchdir_dl.Click += new System.EventHandler(this.btnchdir_dl_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(638, 455);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "DotNetCf.FtpClient.demo";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnIISSendComamnd;
        private System.Windows.Forms.TextBox txtIISftpCmd;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnServUSendCmd;
        private System.Windows.Forms.TextBox txtServUCmd;
        private System.Windows.Forms.Button btnServULogin;
        private System.Windows.Forms.Button btnCloseFtp;
        private System.Windows.Forms.TextBox txtWorkingDir;
        private System.Windows.Forms.Button btnServU_CWD;
        private System.Windows.Forms.Button btnServU_PWD;
        private System.Windows.Forms.Button btnIISftp_login;
        private System.Windows.Forms.Button btnIISftp_disconnect;
        private System.Windows.Forms.Button btnIISftp_cwd;
        private System.Windows.Forms.TextBox txtIISWorkingDir;
        private System.Windows.Forms.Button btnIISftp_pwd;
        private System.Windows.Forms.Button btnIISftp_files;
        private System.Windows.Forms.Button btnchdir_dl;
    }
}

