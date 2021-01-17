using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileWatcher.CHFSApi
{
    public class ResponseResult
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 响应业务数据
        /// </summary>
        public string data { get; set; }
        /// <summary>
        /// cookie
        /// </summary>
        public string[] cookie { get; set; }
    }
}
