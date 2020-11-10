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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace 动画基础
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

        private void CmdGrow_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            
            DoubleAnimation widthAnimation = new DoubleAnimation();
            widthAnimation.From = btn.ActualWidth;
            widthAnimation.To = this.ActualWidth - 30;
            widthAnimation.AccelerationRatio = 0.7;
            widthAnimation.DecelerationRatio = 0.3;
            // widthAnimation.FillBehavior = FillBehavior.Stop; 
            //widthAnimation.By = 10;
            widthAnimation.Completed += WidthAnimation_Completed;
            widthAnimation.Duration = TimeSpan.FromSeconds(1);
            cmdGrow.BeginAnimation(Button.WidthProperty, widthAnimation);
            
            
        }

        private void WidthAnimation_Completed(object sender, EventArgs e)
        {
            double currentWidth = cmdGrow.Width;
            cmdGrow.BeginAnimation(Button.WidthProperty, null);
            cmdGrow.Width = currentWidth;
        }

        private void Changewidth_Click(object sender, RoutedEventArgs e)
        {
            cmdGrow.Width = 200;
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }
    }
}
