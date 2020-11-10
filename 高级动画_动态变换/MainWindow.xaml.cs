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

namespace 高级动画_动态变换
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

        private void Ellipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var pos = e.GetPosition(ellipse);
            //var r = new RadialGradientBrush();
            //r.GradientOrigin = pos;
            //r.GradientStops.Add(new GradientStop());
            //r.GradientStops.Add(new GradientStop());
            //r.GradientStops[0].Color = Colors.Red;
            //r.GradientStops[0].Offset = 0;
            //r.GradientStops[1].Color = Colors.Yellow;
            //r.GradientStops[1].Offset = 1;
            //ellipse.Fill = r;
            var brush = ellipse.Fill as RadialGradientBrush;
            Point p = new Point(pos.X / ellipse.Width, pos.Y / ellipse.Height);
            brush.GradientOrigin = p;

            brush.GradientStops[0].Color = Colors.Red;
            brush.GradientStops[0].Offset = 0;
            brush.GradientStops[1].Color = Colors.Transparent;
            brush.GradientStops[1].Offset = 0.8;
        }

        private void WaveButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
