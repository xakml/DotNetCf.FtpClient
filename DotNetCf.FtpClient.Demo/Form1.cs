using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace DotNetCf.FtpClient.Demo
{
    public partial class Form1 : Form
    {
        FtpConnection serv_u_ftpconnection = null;
        FtpConnection iis_ftpconnection = null;

        public Form1()
        {
            InitializeComponent();
            this.Load += new EventHandler(Form1_Load);
        }

        void Form1_Load(object sender, EventArgs e)
        {
            
        }
        private FtpConnection Get134Connection()
        {
            FtpClient.FtpConnection connection = new FtpConnection();
            connection.FtpUserName = "jcftpuser001";
            connection.FtpPassword = "jcftpuserpwd001";
            connection.ServerAddress = "115.238.63.134";
            connection.FtpPort = 21;
            return connection;
        }

        private FtpConnection Get171Connection()
        {
            FtpClient.FtpConnection connection = new FtpConnection();
            connection.FtpUserName = "jms_update_user";
            connection.FtpPassword = "atIGrCpLs8GEMDyVNJw8";
            connection.ServerAddress = "192.168.0.171";
            connection.FtpPort = 21;
            return connection;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var connection = this.Get134Connection();
            connection.ChDir("PDA");
            long size = connection.getFileSize("StartPDA.exe");
            Console.WriteLine(size);
            connection.Close();
        }

        private void DownFiles(FtpConnection connection)
        {
            try
            {
                connection.ChDir("PDA");
                string[] files = connection.GetFiles();
                if (null != files && files.Length > 0)
                {
                    var app_dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
                    for (int i = 0; i < files.Length; i++)
                    {
                        connection.DownloadFile(files[i], app_dir + "\\" + files[i]);
                    }
                    Console.WriteLine("download complete");
                }
                else
                {
                    Console.WriteLine("the directory has any file");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
           
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            var conn = this.Get134Connection();
            DownFiles(conn);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var conn = this.Get171Connection();
            DownFiles(conn);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var conn = this.Get134Connection();
            string[] files = conn.GetFiles();
            if (null != files && files.Length > 0)
            {
                foreach (var file in files)
                {
                    Console.WriteLine(file);
                }
            }
            conn.Close();
        }

        private void btnSendIISFTPCmd_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnIISSendComamnd.Enabled = false;
                Application.DoEvents();
                //var connection = this.Get134Connection();
                var connection = this.Get171Connection();
                byte[] data = connection.SendCmd(this.txtIISftpCmd.Text);
                connection.Close();
                string content = System.Text.Encoding.UTF8.GetString(data, 0, data.Length);
                Console.WriteLine(content);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                this.btnIISSendComamnd.Enabled = true;
            }
        }
        #region serv-u服务器操作

        //发送命令到serv-u ftp
        private void btnServUSendCmd_Click(object sender, EventArgs e)
        {
            try
            {
                btnServUSendCmd.Enabled = false;
                Application.DoEvents();
                //var connection = this.Get134Connection();
                byte[] data = serv_u_ftpconnection.SendCmd(this.txtServUCmd.Text);
                //connection.Close();
                string content = System.Text.Encoding.UTF8.GetString(data, 0, data.Length);
                Console.WriteLine(content);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                this.btnServUSendCmd.Enabled = true;
            }

        }

        private void btnServULogin_Click(object sender, EventArgs e)
        {
            try
            {
                this.serv_u_ftpconnection = this.Get134Connection();
                this.serv_u_ftpconnection.Login();
                this.btnCloseFtp.Enabled = true;
                this.btnServUSendCmd.Enabled = true;
                this.btnServULogin.Enabled = false;

            }
            catch (Exception ex)
            {
                Console.WriteLine("serv-u 登录失败" + ex.Message);
                this.btnCloseFtp.Enabled = false;
                this.btnServUSendCmd.Enabled = false;
                this.btnServULogin.Enabled = true ;
            }
            
        }

        private void btnCloseFtp_Click(object sender, EventArgs e)
        {
            if (null != serv_u_ftpconnection)
            {
                this.serv_u_ftpconnection.Close();
                this.btnCloseFtp.Enabled = false;
                this.btnServUSendCmd.Enabled = false;
                this.btnServULogin.Enabled = true;
            } 
        }

        private void btnServU_CWD_Click(object sender, EventArgs e)
        {
            this.serv_u_ftpconnection.ChDir(this.txtWorkingDir.Text.Trim());
        }

        private void btnServU_PWD_Click(object sender, EventArgs e)
        {
            Console.WriteLine(this.serv_u_ftpconnection.WorkingDirectory);
        }
#endregion

        private void btnIISftp_login_Click(object sender, EventArgs e)
        {
            try
            {
                this.iis_ftpconnection = this.Get171Connection();
                this.iis_ftpconnection.Login();
                this.btnIISftp_cwd.Enabled = true;
                this.btnIISftp_files.Enabled = true;
                this.btnIISftp_pwd.Enabled = true;
                this.btnIISftp_login.Enabled = false;

            }
            catch (Exception ex)
            {
                Console.WriteLine("serv-u 登录失败" + ex.Message);

                this.btnIISftp_cwd.Enabled = false;
                this.btnIISftp_files.Enabled = false;
                this.btnIISftp_pwd.Enabled = true;
                this.btnIISftp_login.Enabled = true ;

            }
        }

        private void btnIISftp_cwd_Click(object sender, EventArgs e)
        {
            this.iis_ftpconnection.ChDir(this.txtIISWorkingDir.Text.Trim());
        }

        private void btnIISftp_files_Click(object sender, EventArgs e)
        {
            var files = this.iis_ftpconnection.GetFiles();
            if (files != null && files.Length > 0)
            {
                foreach (var file in files)
                {
                    Console.WriteLine(file);
                }
            }
            else
            {
                Console.WriteLine("目录中没有文件：" + this.iis_ftpconnection.WorkingDirectory);
            }

        }

        private void btnIISftp_disconnect_Click(object sender, EventArgs e)
        {
            if (this.iis_ftpconnection != null)
            {
                this.iis_ftpconnection.Close();
                this.btnIISftp_cwd.Enabled = false;
                this.btnIISftp_files.Enabled = false;
                this.btnIISftp_pwd.Enabled = true;
                this.btnIISftp_login.Enabled = true;
            }
        }

        private void btnchdir_dl_Click(object sender, EventArgs e)
        {
            this.iis_ftpconnection.ChDir("pda");
            //Console.WriteLine(this.iis_ftpconnection.WorkingDirectory);
            var files = this.iis_ftpconnection.GetFiles();
            if (null != files && files.Length > 0)
            {
                Console.WriteLine(files.Length);
            }
            else
            {
                Console.WriteLine("file is null or empty");
            }
        }
    }
}
//参考资料
//https://blog.csdn.net/chenzhiqin20/article/details/17450597
//https://www.cnblogs.com/steven0lisa/archive/2011/11/02/2233160.html
//https://blog.csdn.net/baokx/article/details/14161085