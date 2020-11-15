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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Win32;
namespace Win32Demo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private IntPtr handle = IntPtr.Zero;
        public MainWindow()
        {
            InitializeComponent();
            var i = User.FindWindow(null, "计算器");
            handle = new IntPtr(i);
            Loaded += MainWindow_Loaded;
          
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
          var bdHandle=  ((HwndSource)PresentationSource.FromVisual(bd)).Handle;
            User.SetParent(handle, bdHandle);
            User.MoveWindow(handle,0,0,111,111, 1);
        }
    }
}
