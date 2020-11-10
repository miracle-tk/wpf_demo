using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace 自制控件
{
    /// <summary>
    /// 仪表盘.xaml 的交互逻辑
    /// </summary>
    public partial class Instrument : UserControl
    {
        private double radius;
        public Instrument()
        {
           
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SetSize();
            Refresh();
        }
        private void SetSize()
        {
            
            ellipse.Center = new Point(outCanvas.ActualWidth / 2, outCanvas.ActualHeight / 2);
            radius = Math.Min(this.ActualHeight / 2, this.ActualWidth / 2);
            ellipse.RadiusX = ellipse.RadiusY = radius;
            centerCircle.Height = centerCircle.Width = radius / 10;
        }



        private double step;

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(Instrument), new PropertyMetadata(0.0,new PropertyChangedCallback(OnValueChange)));

        private static void OnValueChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as Instrument).ValueChange();
        }

        private void Refresh()
        {
            if (radius <= 0)
            {
                return;
            }
            #region 画刻度
            int min = 0;
            int max = 100;
            mainCanvas.Children.Clear();
            Point center = ellipse.Center;
           
             step = 270.0 / (max - min);
            for (int i = 0; i <= max - min; i++)
            {
           
                var line = new Line();
                line.Stroke = Brushes.Blue;
                line.StrokeThickness = 2;
                line.X1 = Math.Cos((225 - i * step) * Math.PI / 180) * radius+center.X;
                line.Y1 = -Math.Sin((225 - i * step) * Math.PI / 180) * radius + center.Y;
                if (i % 10 == 0)
                {
                    line.X2 = Math.Cos((225 - i * step) * Math.PI / 180) * (radius - 40) + center.X;
                    line.Y2 = -Math.Sin((225 - i * step) * Math.PI / 180) * (radius - 40) + center.Y;
                    var tb = new TextBlock();
                    //tb.Width = 30;
                    //tb.Height = 20;
                    //tb.Background = Brushes.Red;
                    tb.FontSize = 15;
                    mainCanvas.Children.Add(tb);
                    tb.Text = i.ToString();
                    tb.VerticalAlignment = VerticalAlignment.Center;
                    tb.HorizontalAlignment = HorizontalAlignment.Center;
                    Canvas.SetLeft(tb, Math.Cos((225 - i * step) * Math.PI / 180) * (radius - 55) + center.X-tb.FontSize/2);
                    Canvas.SetTop(tb, -Math.Sin((225 - i * step) * Math.PI / 180) * (radius - 55) + center.Y - tb.FontSize / 2);
                   
                }
                else
                {
                    line.X2 = Math.Cos((225 - i * step) * Math.PI / 180) * (radius - 20) + center.X;
                    line.Y2 = -Math.Sin((225 - i * step) * Math.PI / 180) * (radius - 20) + center.Y;

                }
                
                mainCanvas.Children.Add(line);

            }
            #endregion
            #region 画内部圆弧

          //  string s = "M 250,40 L200,20 L200,60 Z";
            string sData = string.Format("M {0},{1} A{2},{2} 0 1 1 {3} ,{4}",
                Math.Cos((225 ) * Math.PI / 180) * radius / 3 + center.X,
                -Math.Sin((225) * Math.PI / 180) * radius / 3 + center.Y,
                radius/3,
                Math.Cos((-45) * Math.PI / 180) * radius / 3 + center.X,
                -Math.Sin((-45) * Math.PI / 180) * radius / 3 + center.Y
                );
            var converter = TypeDescriptor.GetConverter(typeof(Geometry));
            arcPath.Data = (Geometry)converter.ConvertFrom(sData);
            // mainCanvas.Children.Add(arcPath);
            #endregion

            #region 画中心圆
            Canvas.SetLeft(centerCircle, center.X - centerCircle.Width/2);
            Canvas.SetTop(centerCircle, center.Y - centerCircle.Height/2);

            #endregion
            #region 画指针
            string pointerData = string.Format("M {0},{1} L {2},{3} L{4},{5}",
                  -radius , 0,
               0,-5,
                 0,  + 5
                );
            var pointerconverter = TypeDescriptor.GetConverter(typeof(Geometry));
            pointer.Data = (Geometry)pointerconverter.ConvertFrom(pointerData);
           Canvas.SetTop(pointer, center.Y );
          Canvas.SetLeft(pointer, center.X);
            //pointer.RenderTransformOrigin = new Point(0.5,0.5);
              pointerAngle.Angle = step*(Value-min)-45;
           // pointerAngle.Angle = step * (Value - min) - 225;

            #endregion
        }

        private void ValueChange()
        {
            var doubleani = new DoubleAnimation()
            {
                To = step * (Value - 0) - 45,
                Duration = new Duration(TimeSpan.FromMilliseconds(500))

            };
            doubleani.EasingFunction = new ElasticEase() { Oscillations = 5, EasingMode = EasingMode.EaseOut };

            pointerAngle.BeginAnimation(RotateTransform.AngleProperty, doubleani);
        }
    }
    
}
