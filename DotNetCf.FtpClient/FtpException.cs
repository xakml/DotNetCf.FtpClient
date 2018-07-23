using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace DotNetCf.FtpClient
{
    /// <summary>
    /// 专用于FTP操作的异常处理
    /// </summary>
    public class FtpException : Exception
    {
        /// <summary>
        /// 是否包含内部异常
        /// </summary>
        public bool HasInnerException { get; set; }

        public FtpException()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg">异常消息</param>
        public FtpException(string msg)
            : base(msg)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg">异常消息</param>
        /// <param name="inner_ex">内部异常</param>
        public FtpException(string msg, Exception inner_ex)
            : base(msg, inner_ex)
        {
            if (inner_ex != null)
            {
                this.HasInnerException = true;
            }
        }

    }
}
