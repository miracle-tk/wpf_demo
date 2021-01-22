using System;
using System.Collections.Generic;
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

namespace 异步
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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("before do " + Thread.CurrentThread.ManagedThreadId.ToString());
            await  DoAsync();
            Console.WriteLine("after do " + Thread.CurrentThread.ManagedThreadId.ToString());
        }

        private async Task DoAsync()
        {
            Console.WriteLine("Before run " + Thread.CurrentThread.ManagedThreadId.ToString());
            await Task.Run(async () => {

                Thread.Sleep(3000);
              Console.WriteLine("In run "+Thread.CurrentThread.ManagedThreadId.ToString());

            });
            Console.WriteLine("after run " + Thread.CurrentThread.ManagedThreadId.ToString());
        }
    }
}
