using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace APP_窗口间交互
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private List<Window1> windows=new List<Window1>();

        public List<Window1> WindowsList
        {
            get { return windows; }
            set { windows = value; }
        }

    }
}
