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
        public Form1()
        {
            InitializeComponent();
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
    }
}