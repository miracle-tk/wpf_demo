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

namespace 高级动画_基于帧
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private int maxNum = 1000;
        private int minNum = 200;
        private int minVY = 1;
        private int maxVY = 50;
        private double speedRatio = 0.1;
        private double ellipseRadiux = 10;
        private double accY = 0.1;
        private TimeSpan lastTime ;
        public List<EllipseInfo> ellipses { get; set; } = new List<EllipseInfo>();
        private bool isRendering = false;
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!isRendering)
            {
                ellipses.Clear();
                canvas.Children.Clear();
                CompositionTarget.Rendering += CompositionTarget_Rendering;
                isRendering = true;
            }
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            TimeSpan now = new TimeSpan(DateTime.Now.Ticks);
            if (lastTime ==TimeSpan.Zero)
            {
                lastTime = now;
            }
            double seconds = (now - lastTime).Milliseconds;
            lastTime = now;
            Console.WriteLine(seconds);
            if (ellipses.Count == 0)
            {
                Random rand = new Random();
                for (int i = 0; i < rand.Next(minNum,maxNum); i++)
                {
                    var ell = new Ellipse();
                    ell.Fill = Brushes.LimeGreen;
                    ell.Width = ellipseRadiux;
                    ell.Height = ellipseRadiux;
                    Canvas.SetLeft(ell, rand.Next(0,(int)canvas.ActualWidth));
                    Canvas.SetTop(ell, 0);
                    var ellInfo = new EllipseInfo(ell, rand.Next(minVY, maxVY) * speedRatio);
                    ellipses.Add(ellInfo);
                    canvas.Children.Add(ellInfo.Ellipse);

                }
            }
            else
            {
                for (int i = ellipses.Count-1; i >=0; i--)
                {
                    var info = ellipses[i];
                    var top = Canvas.GetTop(info.Ellipse);
                    if (top >= canvas.ActualHeight- ellipseRadiux)
                    {
                        ellipses.Remove(info);
                    }
                    else
                    {
                        var targetTop = top + 1 * info.VelocityY> canvas.ActualHeight - ellipseRadiux ? canvas.ActualHeight - ellipseRadiux : top + 1 * info.VelocityY;
                        Canvas.SetTop(info.Ellipse, targetTop);
                        //if (targetTop> canvas.ActualHeight - 10)
                        //{

                        //    Canvas.SetTop(info.Ellipse, canvas.ActualHeight - 10);
                        //}
                        //else
                        //{
                        //    Canvas.SetTop(info.Ellipse, top + 1 * info.VelocityY);
                        //}
                        info.VelocityY += accY;
                    }
                   
                }
            }
            if (ellipses.Count == 0)
            {
                isRendering = false;
                CompositionTarget.Rendering -= CompositionTarget_Rendering;
            }
        }
    }

    public  class EllipseInfo
    {
        public EllipseInfo(Ellipse ellipse, double velocityY)
        {
            Ellipse = ellipse;
            VelocityY = velocityY;
        }

        public Ellipse Ellipse { get; set; }
        public double VelocityY { get; set; }
        public bool Direction { get; set; } = false;//false Down


    }
}
