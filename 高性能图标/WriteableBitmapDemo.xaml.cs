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
using 高性能图标.impl;
using 高性能图标.Model;

namespace 高性能图标
{
    /// <summary>
    /// WriteableBitmapDemo.xaml 的交互逻辑
    /// </summary>
    public partial class WriteableBitmapDemo : UserControl
    {
        private WriteableBitmap wb ;
        public WriteableBitmapDemo()
        {
            SetCurrentValue(HorizontalRangeProperty, new Range(0d, 100d));
            SetCurrentValue(VerticalRangeProperty, new Range(0d, 100d));
            DataSeries = new DataSeries() {  new Point(10, 10),
                new Point(20,20),
                new Point(30,40)};
            var rand = new Random();
            for (int i = 0; i < 100; i++)
            {
                DataSeries.Add(new Point(100 * rand.NextDouble(), 100 * rand.NextDouble()));
            }

            InitializeComponent();
            Loaded += WriteableBitmapDemo_Loaded;
            SizeChanged += WriteableBitmapDemo_SizeChanged;
        }

        private void WriteableBitmapDemo_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ResetScale();
            refresh();
        }



        private void WriteableBitmapDemo_Loaded(object sender, RoutedEventArgs e)
        {
            var panel = Parent;
            Binding binding = new Binding(nameof(ActualWidth))
            {
                Source = panel
            };
            this.SetBinding(WidthProperty, binding);
            binding = new Binding(nameof(ActualHeight))
            {
                Source = panel
            };
            SetBinding(HeightProperty, binding);
            refresh();
        }

        private void refresh()
        {
            throw new NotImplementedException();
        }

        public Range HorizontalRange
        {
            get { return (Range)GetValue(HorizontalRangeProperty); }
            set { SetValue(HorizontalRangeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HorizontalRange.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HorizontalRangeProperty =
            DependencyProperty.Register("HorizontalRange", typeof(Range), typeof(WriteableBitmapDemo),
                new PropertyMetadata(default(Range), (s, e) => {

                    (s as WriteableBitmapDemo).ResetScale();
                }));



        public Range VerticalRange
        {
            get { return (Range)GetValue(VerticalRangeProperty); }
            set { SetValue(VerticalRangeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VerticalRange.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VerticalRangeProperty =
            DependencyProperty.Register("VerticalRange", typeof(Range), typeof(WriteableBitmapDemo), new PropertyMetadata(default(Range),
                (s, e) => {

                    (s as WriteableBitmapDemo).ResetScale();
                }));



        public DataSeries DataSeries
        {
            get { return (DataSeries)GetValue(DataSeriesProperty); }
            set { SetValue(DataSeriesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataSeries.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataSeriesProperty =
            DependencyProperty.Register("DataSeries", typeof(DataSeries), typeof(WriteableBitmapDemo), new FrameworkPropertyMetadata(default(DataSeries)));



        #region 方法
        private void ResetScale()
        {
            if (HorizontalRange == null || HorizontalRange.Distance <= 0 || VerticalRange == null || VerticalRange.Distance <= 0) return;
            _kx = (double.IsNaN(Width) ? ActualWidth : Width) / HorizontalRange.Distance;
            _ky = (double.IsNaN(Height) ? ActualHeight : Height) / VerticalRange.Distance;
            _height = double.IsNaN(Height) ? ActualHeight : Height;
            //this.InvalidateVisual();
        }
        private Point Normalize(Point p)
        {
            return new Point((p.X - HorizontalRange.Min) * _kx, _height - (p.Y - VerticalRange.Min) * _ky);

        }
        #endregion
        #region 字段
        private double _kx;
        private double _ky;
        private double _height;
        #endregion
    }
}
