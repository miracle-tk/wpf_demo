using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace 页面和导航
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
       
        private NavigationService nav;
        public MainWindow()
        {
            Loaded += MainWindow_Loaded;
            InitializeComponent();

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
             nav = NavigationService.GetNavigationService(frame);
             
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new Uri("http://www.baidu.com"));
            if (nav != null)
            {
                nav.Navigate(new Uri("http://www.baidu.com"));
            }
        }
    }
}
