﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using 高性能图标.Model;

namespace 高性能图标.impl
{
    public class PolyFigure : Shape
    {
        
        #region 构造器
        public PolyFigure()
        {
            SetCurrentValue(StrokeProperty, Brushes.Lime);
            SetCurrentValue(StrokeThicknessProperty, 1d);
            SetCurrentValue(HorizontalRangeProperty, new Range(0d, 100d));
            SetCurrentValue(VerticalRangeProperty, new Range(0d, 100d));
            DataSeries = new DataSeries() {  new Point(10, 10),
                new Point(20,20),
                new Point(30,40)};
            var rand = new Random();
            for (int i = 0; i < 1000; i++)
            {
                DataSeries.Add(new Point(100 * rand.NextDouble(), 100 * rand.NextDouble()));
            }
           
            Loaded += PolyFigure_Loaded;
            SizeChanged += PolyFigure_SizeChanged;
        }

        private void PolyFigure_Loaded1(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void PolyFigure_Loaded(object sender, RoutedEventArgs e)
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
        }

        private void PolyFigure_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Console.WriteLine($"{e.NewSize.Width},{e.NewSize.Height}");
            ResetScale();
        }
        #endregion
        protected override Geometry DefiningGeometry {

            get {
                if (_kx == 0 || _ky == 0) return StreamGeometry.Empty;
                var stream = new StreamGeometry();
                var points = new List<Point>();
                using(var geo = stream.Open())
                {
                    DataSeries.ToList().ForEach(
                         p =>
                         {
                            points.Add((Normalize(p)));
                         });

                    geo.BeginFigure(points[0], false, false);
                    geo.PolyLineTo(points, true, true);
                }

                stream.Freeze();
                return stream;

            }
        }

        private Point Normalize(Point p)
        {
           return new Point((p.X-HorizontalRange.Min) * _kx,_height- (p.Y-VerticalRange.Min) * _ky);
            
        } 

        #region 依赖属性


        public Range HorizontalRange
        {
            get { return (Range)GetValue(HorizontalRangeProperty); }
            set { SetValue(HorizontalRangeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HorizontalRange.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HorizontalRangeProperty =
            DependencyProperty.Register("HorizontalRange", typeof(Range), typeof(PolyFigure), 
                new PropertyMetadata(default(Range),(s,e)=> {

                    (s as PolyFigure).ResetScale();
                }));



        public Range VerticalRange
        {
            get { return (Range)GetValue(VerticalRangeProperty); }
            set { SetValue(VerticalRangeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VerticalRange.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VerticalRangeProperty =
            DependencyProperty.Register("VerticalRange", typeof(Range), typeof(PolyFigure), new PropertyMetadata(default(Range), 
                (s, e) => {

                (s as PolyFigure).ResetScale();
            }));



        public DataSeries DataSeries
        {
            get { return (DataSeries)GetValue(DataSeriesProperty); }
            set { SetValue(DataSeriesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataSeries.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataSeriesProperty =
            DependencyProperty.Register("DataSeries", typeof(DataSeries), typeof(PolyFigure), new FrameworkPropertyMetadata(default(DataSeries),FrameworkPropertyMetadataOptions.AffectsRender));


        #endregion
        #region 方法
        private void ResetScale()
        {
            if (HorizontalRange == null || HorizontalRange.Distance <= 0 || VerticalRange == null || VerticalRange.Distance <= 0) return;
            _kx = (double.IsNaN(Width) ? ActualWidth : Width) / HorizontalRange.Distance;
            _ky = (double.IsNaN(Height) ? ActualHeight : Height) / VerticalRange.Distance;
            _height = double.IsNaN(Height) ? ActualHeight : Height;
            this.InvalidateVisual();
        }
        #endregion
        #region 字段
        private double _kx;
        private double _ky;
        private double _height;
        #endregion
    }

    public class DataSeries : ObservableCollection<Point>, ICloneable
    {
        public object Clone()
        {
            var ds = new DataSeries();
            this.ToList().ForEach(p => ds.Add(p));

            return ds;
           
        }
    }
}
