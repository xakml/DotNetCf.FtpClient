using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

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

        private void button1_Click(object sender, EventArgs e)
        {
            var connection = this.Get134Connection();
            connection.ChDir("PDA");
            long size = connection.getFileSize("StartPDA.exe");
            Console.WriteLine(size);
            connection.Close();
        }
    }
}