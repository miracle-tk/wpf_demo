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
using System.Windows.Shapes;

namespace test1
{
    /// <summary>
    /// Window2.xaml 的交互逻辑
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
            Unloaded += Window2_Unloaded;
            this.DataContext = new Window2VIewModel();
        }

        private void Window2_Unloaded(object sender, RoutedEventArgs e)
        {
            //form.Child = null;
            //mainGrid.Children.Remove(form);
            //form.Child
            //mainGrid.Children.Remove(hidpic);
        }
    }
}
