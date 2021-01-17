using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace FileWatcher.CHFSApi
{
    public class CHFSServer
    {
        public static string chfsuser = ConfigurationManager.AppSettings["User"];
        public static string chfspwd = ConfigurationManager.AppSettings["password"];
        public static string chfsip = ConfigurationManager.AppSettings["ip"];
        public static void Start()
        {
            #region

            int chfsport =999;

            CHFSApi.init(chfsip, chfsport);
            #endregion
        }

        private static void runBat(
    string batPath, string args = "")
        {
            #region
            Process pro = new Process();
            FileInfo file = new FileInfo(batPath);
            pro.StartInfo.WorkingDirectory = file.Directory.FullName;

            pro.StartInfo.FileName = batPath;
            pro.StartInfo.UseShellExecute = false;//是否重定向标准输入 
            pro.StartInfo.RedirectStandardInput = false;//是否重定向标准转出 
            pro.StartInfo.RedirectStandardOutput = false;//是否重定向错误 
            pro.StartInfo.RedirectStandardError = false;//执行时是不是显示窗口 
            pro.StartInfo.CreateNoWindow = true;//启动 

            if (!string.IsNullOrEmpty(args))
                pro.StartInfo.Arguments = args;
            pro.Start();
            #endregion
        }

    }
}
