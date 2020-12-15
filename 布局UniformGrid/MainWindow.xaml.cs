using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace 布局UniformGrid
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
       // public ObservableCollection<UserControl> Contents = new ObservableCollection<UserControl>();
        private ObservableCollection<UserControl> _content=new ObservableCollection<UserControl>();

        public ObservableCollection<UserControl> Contents
        {
            get { return _content; }
            set { _content = value; }
        }

        public MainWindow()
        {
            Loaded += MainWindow_Loaded;
            InitializeComponent();
            this.DataContext = this;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Contents.Add(
               new UserControl1());
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            while (true)
            {
                Console.WriteLine($"Main:{Thread.CurrentThread.ManagedThreadId}");
                await Task.Delay(2000);
                Console.WriteLine($"sub:{Thread.CurrentThread.ManagedThreadId}");
                Contents.Add(
               new UserControl1());
            }
        }
    }
}
