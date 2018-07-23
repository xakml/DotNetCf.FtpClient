using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace DotNetCf.FtpClient
{
    public class FtpConnection
    {
        #region Arrbuite
        //private string this.ServerAddress, this.WorkingDirectory, this.FtpUserName, this.FtpPassword,
        string mes;
        //private int remotePort;
        int bytes;
        private Socket clientSocket;

        private int retValue;
        private Boolean debug;
        private Boolean logined;
        private string reply;
        private static int BLOCK_SIZE = 512;
        Byte[] buffer = new Byte[BLOCK_SIZE];
        Encoding ASCII = Encoding.ASCII;
        #endregion

        public string FtpUserName { get; set; }

        public string FtpPassword { get; set; }

        public string ServerAddress { get; set; }

        public int FtpPort { get; set; }

        /// <summary>
        /// 最后一次操作的消息输出
        /// </summary>
        public string LastMessage { get; set; }

        private static IPAddress GetIP(string import)
        {
            return IPAddress.Parse(import);
        }
        #region 构造函数

        /// <summary>
        /// 
        /// </summary>
        public FtpConnection()
        {
            this.FtpPort = 21;
            this.debug = false;
            this.logined = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip"></param>
        public FtpConnection(string ip)
            : this()
        {
            this.ServerAddress = ip;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip"></param>
        public FtpConnection(int port)
            : this()
        {
            this.FtpPort = port;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="user"></param>
        /// <param name="pwd"></param>
        public FtpConnection(string ip, string user, string pwd)
            : this()
        {
            this.ServerAddress = ip;
            this.FtpUserName = user;
            this.FtpPassword = pwd;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="user"></param>
        /// <param name="pwd"></param>
        /// <param name="port"></param>
        public FtpConnection(string ip, string user, string pwd, int port)
            : this()
        {
            this.ServerAddress = ip;
            this.FtpUserName = user;
            this.FtpPassword = pwd;
            this.FtpPort = port;
        }


        #endregion

        #region getFileList
        /**/
        /// <summary>
        /// Return a string array containing the remote directory's file list.
        /// </summary>
        /// <param name="mask"></param>
        /// <returns></returns>
        public string[] getFileList(string mask)
        {
            if (!logined)
            {
                Login();
            }
            Socket cSocket = createDataSocket();
            if (string.IsNullOrEmpty(mask))
                sendCommand("NLST");
            else
                sendCommand("NLST " + mask);
            //if (retValue == 226)
            //{
            //    Console.WriteLine(reply);
            //}
            if (!(retValue == 150 || retValue == 125))
            {
                if (retValue == 226)
                {
                    cSocket.Close();
                    return null;
                }
                else
                    throw new IOException(reply.Substring(4));
            }

            mes = "";

            while (true)
            {

                int bytes = cSocket.Receive(buffer, buffer.Length, 0);
                mes += ASCII.GetString(buffer, 0, bytes);

                if (bytes < buffer.Length)
                {
                    break;
                }
            }

            char[] seperator = { '\n' };
            string[] mess = mes.Split(seperator);

            cSocket.Close();

            readReply();

            if (retValue != 226)
            {
                throw new IOException(reply.Substring(4));
            }
            return mess;

        }

        /// <summary>
        /// 获取当前工作目录下的所有文件(推荐)
        /// </summary>
        /// <returns></returns>
        public string[] GetFiles()
        {
            if (!logined)
                Login();
            Socket cSocket = createDataSocket();

            sendCommand("NLST");
            if (!(retValue == 150 || retValue == 125))
            {
                if (retValue == 226)
                {
                    cSocket.Close();
                    Console.WriteLine(reply);
                    return null;
                }
                else
                    throw new IOException(reply.Substring(4));
            }

            //var data = this.ReadData(cSocket);
            //System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("UTF-8");

            //string dir_files = encoding.GetString(data, 0, data.Length);

            //return null;

            List<string> files = new List<string>();
            string line = null;
            while ((line = this.ReadLineFromSocket(cSocket)) != null)
            {
                files.Add(line);
            }
            cSocket.Close();
            readReply();

            if (retValue != 226)
                throw new IOException(reply.Substring(4));
            return files.ToArray();
        }

        #endregion

        #region getFileSize
        /**/
        /// <summary>
        /// Return the size of a file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public long getFileSize(string fileName)
        {

            if (!logined)
            {
                Login();
            }

            sendCommand("SIZE " + fileName);
            long size = 0;

            if (retValue == 213)
            {
                size = Int64.Parse(reply.Substring(4));
            }
            else
            {
                throw new IOException(reply.Substring(4));
            }

            return size;

        }
        #endregion

        /// <summary>
        /// Login to the remote server.
        /// </summary>
        public void Login()
        {

            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ep = new IPEndPoint(GetIP(this.ServerAddress), this.FtpPort);
            try
            {
                clientSocket.Connect(ep);
            }
            catch (Exception ex)
            {
                throw new IOException("Couldn't login the remote ftp server :" + ex.Message);
            }

            readReply();
            if (retValue != 220)
            {
                Close();
                //throw new IOException(reply.Substring(4));
            }

            sendCommand("USER " + this.FtpUserName);
            if (!(retValue == 331 || retValue == 230))
            {
                cleanup();
                //throw new IOException(reply.Substring(4));
            }

            if (retValue != 230)
            {
                sendCommand("PASS " + this.FtpPassword);
                if (!(retValue == 230 || retValue == 202))
                {
                    cleanup();
                    //throw new IOException(reply.Substring(4));
                }
            }
            logined = true;
            ChDir(this.WorkingDirectory);
        }

        /// <summary>
        /// If the value of mode is true, set binary mode for downloads.
        /// Else, set Ascii mode.
        /// </summary>
        /// <param name="mode"></param>
        public void setBinaryMode(Boolean mode)
        {

            if (mode)
            {
                sendCommand("TYPE I");
            }
            else
            {
                sendCommand("TYPE A");
            }
            if (retValue != 200)
            {
                throw new IOException(reply.Substring(4));
            }
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="remote_file">远程文件路径（如过下载当前工作目录中的文件，可以不添加完整路径）</param>
        /// <param name="local_file">本地文件路径（最好完整路径）</param>
        /// <returns></returns>
        public bool DownloadFile(string remote_file, string local_file)
        {
            if (!logined)
                Login();

            setBinaryMode(true);

            #region 删除已存在的同名文件
            try
            {
                FileInfo fileInfo = new FileInfo(local_file);
                if (fileInfo.Exists)
                    fileInfo.Delete();
            }
            catch
            {
                return false;
            }
            #endregion

            #region 开通ftp数据传输通道
            Socket socket = null;
            try
            {
                socket = createDataSocket();
                sendCommand("RETR " + remote_file);
                if (!(retValue == 150 || retValue == 125))
                {
                    socket.Close();
                    throw new IOException(reply);
                }
            }
            catch (Exception ex)
            {
                if (socket != null)
                    socket.Close();
                throw new FtpException("create data socket error:" + ex.Message, ex);
            }

            #endregion

            #region 下载并保存文件（基于当前目录）
            try
            {
                int received_bytes = 0;
                using (System.IO.FileStream localFileStream = new FileStream(local_file, FileMode.Create, FileAccess.Write))
                {
                    while (true)
                    {
                        received_bytes = 0;
                        Array.Clear(buffer, 0, buffer.Length);
                        received_bytes = socket.Receive(buffer, buffer.Length, SocketFlags.None);
                        if (received_bytes != 0)
                            localFileStream.Write(buffer, 0, received_bytes);
                        else
                            break;
                    }
                    localFileStream.Flush();
                    localFileStream.Close();
                }
                socket.Close();

                readReply();
                if (!(retValue == 226 || retValue == 250))
                {
                    throw new FtpException(reply);
                }
            }
            catch (Exception ex)
            {
                if (socket != null)
                    socket.Close();
                this.LastMessage = ex.Message;
                return false;
            }
            #endregion

            return true;
        }

        #region Upload
        /**/
        /// <summary>
        /// Upload a file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>Upload a file is success!</returns>
        public bool Upload(string fileName)
        {
            return Upload(fileName, false);
        }


        /**/
        /// <summary>
        /// Upload a file and set the resume flag.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="resume"></param>
        /// <returns></returns>
        public bool Upload(string fileName, Boolean resume)
        {
            if (!logined)
            {
                Login();
            }
            Socket cSocket = createDataSocket();
            long offset = 0;
            if (resume)
            {
                try
                {
                    setBinaryMode(true);
                    offset = getFileSize(fileName);

                }
                catch (Exception)
                {
                    offset = 0;
                }
            }

            if (offset > 0)
            {
                sendCommand("REST " + offset);
                if (retValue != 350)
                {
                    //throw new IOException(reply.Substring(4));
                    //Remote server may not support resuming.
                    offset = 0;
                }
            }

            sendCommand("STOR " + Path.GetFileName(fileName));

            if (!(retValue == 125 || retValue == 150))
            {
                throw new IOException(reply.Substring(4));
            }

            // open input stream to read source file
            FileStream input = new FileStream(fileName, FileMode.Open);
            if (offset != 0)
            {
                input.Seek(offset, SeekOrigin.Begin);
            }

            Console.WriteLine("Uploading file " + fileName + " to " + this.WorkingDirectory);

            while ((bytes = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                cSocket.Send(buffer, bytes, 0);
            }
            input.Close();
            if (cSocket.Connected)
            {
                cSocket.Close();
            }
            readReply();
            if (!(retValue == 226 || retValue == 250))
            {
                throw new IOException(reply.Substring(4));
            }
            return true;
        }
        #endregion

        #region DeleteRemoteFile
        /**/
        /// <summary>
        /// Delete a file from the remote FTP server.
        /// </summary>
        /// <param name="fileName"></param>
        public void DeleteRemoteFile(string fileName)
        {

            if (!logined)
            {
                Login();
            }

            sendCommand("DELE " + fileName);
            if (retValue != 250)
            {
                throw new IOException(reply.Substring(4));
            }

        }
        #endregion

        #region RenameRemoteFile
        /**/
        /// <summary>
        /// Rename a file on the remote FTP server.
        /// </summary>
        /// <param name="oldFileName"></param>
        /// <param name="newFileName"></param>
        public void RenameRemoteFile(string oldFileName, string newFileName)
        {

            if (!logined)
            {
                Login();
            }

            sendCommand("RNFR " + oldFileName);

            if (retValue != 350)
            {
                throw new IOException(reply.Substring(4));
            }

            //  known problem
            //  rnto will not take care of existing file.
            //  i.e. It will overwrite if newFileName exist
            sendCommand("RNTO " + newFileName);
            if (retValue != 250)
            {
                throw new IOException(reply.Substring(4));
            }

        }
        #endregion

        #region MkDir
        /**/
        /// <summary>
        /// Create a directory on the remote FTP server.
        /// </summary>
        /// <param name="dirName"></param>
        public void MkDir(string dirName)
        {

            if (!logined)
            {
                Login();
            }

            sendCommand("MKD " + dirName);

            if (retValue != 250)
            {
                throw new IOException(reply.Substring(4));
            }

        }
        #endregion

        #region RmDir
        /**/
        /// <summary>
        /// Delete a directory on the remote FTP server
        /// </summary>
        /// <param name="dirName"></param>
        public void RmDir(string dirName)
        {

            if (!logined)
            {
                Login();
            }

            sendCommand("RMD " + dirName);

            if (retValue != 250)
            {
                throw new IOException(reply.Substring(4));
            }

        }
        #endregion

        #region ChDir
        /**/
        /// <summary>
        /// Change the current working directory on the remote FTP server.
        /// </summary>
        /// <param name="dirName"></param>
        public void ChDir(string dirName)
        {

            if (dirName.Equals("."))
            {
                return;
            }

            if (!logined)
            {
                Login();
            }

            sendCommand("CWD " + dirName);
            if (retValue != 250)
            {
                throw new IOException(reply.Substring(4));
            }
        }
        #endregion

        #region Close()
        /**/
        /// <summary>
        ///  Close the FTP connection.
        /// </summary>
        public void Close()
        {

            if (clientSocket != null)
            {
                sendCommand("QUIT");
            }

            cleanup();
        }
        #endregion

        #region OTHER
        /**/
        /// <summary>
        /// Set debug mode.
        /// </summary>
        /// <param name="debug"></param>
        public void setDebug(Boolean debug)
        {
            this.debug = debug;
        }

        private void readReply()
        {
            mes = "";
            reply = readLine();
            retValue = Int32.Parse(reply.Substring(0, 3));
        }

        private void cleanup()
        {
            if (clientSocket != null)
            {
                clientSocket.Close();
                clientSocket = null;
            }
            logined = false;
        }
        #endregion

        #region readLine
        private string readLine()
        {

            while (true)
            {
                bytes = clientSocket.Receive(buffer, buffer.Length, 0);
                mes += ASCII.GetString(buffer, 0, bytes);
                if (bytes < buffer.Length)
                {
                    break;
                }
            }

            char[] seperator = { '\n' };
            string[] mess = mes.Split(seperator);

            if (mes.Length > 2)
            {
                mes = mess[mess.Length - 2];
            }
            else
            {
                mes = mess[0];
            }

            if (!mes.Substring(3, 1).Equals(" "))
            {
                return readLine();
            }
            return mes;
        }
        #endregion

        #region sendCommand
        public void sendCommand(String command)
        {

            Byte[] cmdBytes = Encoding.ASCII.GetBytes((command + "\r\n").ToCharArray());
            clientSocket.Send(cmdBytes, cmdBytes.Length, 0);
            readReply();
        }
        #endregion


        #region createDataSocket

        /// <summary>
        /// 创建ftp数据通道
        /// </summary>
        /// <exception cref="FtpClient.FtpException">创建基础socket链接出错</exception>
        /// <returns></returns>
        private Socket createDataSocket()
        {
            sendCommand("PASV");
            if (retValue != 227)
            {
                throw new IOException(reply.Substring(4));
            }

            int index1 = reply.IndexOf('(');
            int index2 = reply.IndexOf(')');
            string ipData = reply.Substring(index1 + 1, index2 - index1 - 1);
            int[] parts = new int[6];
            int len = ipData.Length;
            int partCount = 0;
            string buf = "";
            for (int i = 0; i < len && partCount <= 6; i++)
            {

                char ch = Convert.ToChar(ipData.Substring(i, 1));
                if (Char.IsDigit(ch))
                    buf += ch;
                else if (ch != ',')
                {
                    throw new IOException("Malformed PASV reply: " + reply);
                }

                if (ch == ',' || i + 1 == len)
                {

                    try
                    {
                        parts[partCount++] = Int32.Parse(buf);
                        buf = "";
                    }
                    catch (Exception)
                    {
                        throw new IOException("Malformed PASV reply: " + reply);
                    }
                }
            }
            string ipAddress = parts[0] + "." + parts[1] + "." + parts[2] + "." + parts[3];
            int port = (parts[4] << 8) + parts[5];
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ep = new IPEndPoint(GetIP(ipAddress), port);
            try
            {
                s.Connect(ep);
            }
            catch (System.Net.Sockets.SocketException socket_ex)
            {
                throw new FtpException(socket_ex.Message);
            }
            catch (Exception ex)
            {
                throw new IOException("createDataSocket connect to remote server error: " + ex.Message, ex);
            }
            return s;
        }
        #endregion


        public string WorkingDirectory
        {
            get
            {
                sendCommand("PWD");
                if (retValue == 257)
                {
                    System.Text.RegularExpressions.Match m;
                    if ((m = System.Text.RegularExpressions.Regex.Match(reply, "\"(?<pwd>.*)\"")).Success)
                    {
                        return m.Groups["pwd"].Value; ;
                    }
                    if ((m = System.Text.RegularExpressions.Regex.Match(reply, "PWD = (?<pwd>.*)")).Success)
                    {
                        return m.Groups["pwd"].Value;
                    }
                }
                else
                {
                    throw new IOException("view working directory failed");
                }
                return "./";
            }
        }
        /// <summary>
        /// 读取一行记录
        /// </summary>
        /// <param name="s">已连接的socket对象实例</param>
        /// <returns>一行</returns>
        private string ReadLineFromSocket(Socket s)
        {
            string line = null;
            System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("UTF-8");
            List<byte> temp = new List<byte>();
            byte[] temp_buffer = new byte[1];
            int received_bytes = 0;
            while (true)
            {
                received_bytes = s.Receive(temp_buffer, 1, SocketFlags.None);
                if (received_bytes == 1)
                {
                    if ((char)temp_buffer[0] != '\n')
                        temp.Add(temp_buffer[0]);
                    else
                    {
                        line = encoding.GetString(temp.ToArray(), 0, temp.Count).TrimEnd(new char[] { '\r' });
                        break;
                    }
                }
                else
                {
                    break;
                }

            }
            return line;

        }

        /// <summary>
        /// 读取所有接收到的数据
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private byte[] ReadData(Socket s)
        {
            List<byte> temp_buffer = new List<byte>();
            while (true)
            {
                int received_count = s.Receive(buffer, buffer.Length, SocketFlags.None);
                if (received_count < buffer.Length)
                {
                    byte[] temp = new byte[received_count];
                    Buffer.BlockCopy(buffer, 0, temp, 0, received_count);
                    temp_buffer.AddRange(temp.ToArray());
                    break;
                }
                else
                {
                    temp_buffer.AddRange(buffer);
                }
            }
            return temp_buffer.ToArray();
        }

        /// <summary>
        /// 返回到根目录
        /// </summary>
        public void GoToRootDirectory()
        {
            this.ChDir("./");
        }

        /// <summary>
        /// Gets the size of a remote file
        /// </summary>
        /// <param name="path">The full or relative path of the file</param>
        /// <returns>-1 if the command fails, otherwise the file size</returns>
        public long GetFileSize(string file_path)
        {
            if (!this.logined)
                this.Login();
            this.sendCommand("SIZE " + file_path);
            Console.WriteLine(this.reply);
            return -1;
        }
    }
}
//FTP命令详解
//https://blog.csdn.net/weiyuefei/article/details/51758288
