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

namespace 布局
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var mp = new MediaPlayer();
            mp.Open(new Uri(@"D:\Project\01-PCC\B7\PCC_CIM\Sound\alarm.mp3"));
            mp.MediaEnded += Mp_MediaEnded;
            mp.Play();
            
            
            
        }

        private void Mp_MediaEnded(object sender, EventArgs e)
        {
            var mp = sender as MediaPlayer;
            mp.Position = new TimeSpan(0);
            mp.Play();
        }
    }
}
