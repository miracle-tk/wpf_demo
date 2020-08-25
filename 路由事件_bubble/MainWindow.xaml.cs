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

namespace 路由事件_bubble
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            slider1.AddHandler(Slider.MouseUpEvent, new MouseButtonEventHandler(SliderDO), true);
        }

        private void DoSomeThing(object sender, MouseButtonEventArgs e)
        {
            string msg = sender.ToString() + "\n"
                       + e.Source + "\n"
                       + e.OriginalSource+'\n'
                       + e.Device+'\n';
            listMsg.Items.Add(msg);
            e.Handled = (bool)isHandled.IsChecked;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            listMsg.Items.Clear();
        }

        private void SliderDO(object sender, MouseButtonEventArgs e)
        {
            string s = (sender as Slider).Value.ToString();
            listMsg.Items.Add(s);
        }

        private void keydown(object sender, KeyEventArgs e)
        {
            if (e.IsRepeat)
                return;
            e.Handled = true;
            Console.WriteLine(e.Key); 
            
        }
    }
}
