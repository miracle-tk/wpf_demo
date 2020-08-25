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

namespace 布局_sharesize
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

        private void ShowOrHide_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            if (b.Content == "hide")
            {
                TestLabel.Visibility = Visibility.Collapsed;
                b.Content = "show";
            }
            else
            {
                TestLabel.Visibility = Visibility.Visible;
                b.Content = "hide";
            }
            
        }
    }
}
