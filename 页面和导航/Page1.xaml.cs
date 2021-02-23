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

namespace 页面和导航
{
    /// <summary>
    /// Page1.xaml 的交互逻辑
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
           var p = sender as Page1;
           var win= Window.GetWindow(this) as NavigationWindow;
            p.KeepAlive = true;
            p.WindowTitle = "hello page";
            //while (true)
            //{
            //    await Task.Delay(3000);
            //   // p.ShowsNavigationUI = !p.ShowsNavigationUI;
            //}
           
          
        }
    }
}
