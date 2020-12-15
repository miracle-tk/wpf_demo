using NetSDKCS.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace test1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
          // processBar1
            // var w =new Window1();
           
            //Loaded += MainWindow_Loaded;

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
              DateTime startTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day-5, 0, 0, 0);
            VideoTime[] videoTimeArray = new VideoTime[1];
            for (int i = 0; i < 1; i++)
            {
                var date = startTime;
              //  date.AddDays(1);
                var end = date;
                end.AddHours(5);
                videoTimeArray[i] = new VideoTime();
                videoTimeArray[i].StartTime = date;
                videoTimeArray[i].EndTime = end;
            }
         //   processBar1.Init(startTime, videoTimeArray);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
          
        }

        private void ProcessBar1_ProgressChanged(object sender, ProgressEventargs args)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new Window2().Show();
        }
    }
}
