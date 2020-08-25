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

namespace APP_窗口间交互
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            var w = new Window1();
            w.Owner = this;
            w.Show();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            App app = (App)Application.Current;
            foreach (var item in app.WindowsList)
            {
                item.Content = "refresh at"+DateTime.Now;

            }
            

        }
    }
}
