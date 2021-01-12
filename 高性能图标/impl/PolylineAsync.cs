using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using 高性能图标.Model;

namespace 高性能图标.impl
{
    public class PolylineAsync:Canvas
    {
        private DrawingVisual _dv;
        private List<Visual> visuals = new List<Visual>();
        protected override int VisualChildrenCount
        {
            get { return visuals.Count; }
        }
        protected override Visual GetVisualChild(int index)
        {
            return visuals[index];
        }
        public void AddVisual(Visual visual)
        {
            visuals.Add(visual);
            base.AddVisualChild(visual);
            base.AddLogicalChild(visual);
        }
        public void DeleteVisual(Visual visual)
        {
            visuals.Remove(visual);
            base.RemoveVisualChild(visual);
            base.RemoveLogicalChild(visual);
        }
   
     

        private  DrawingVisual DrawLine()
        {
            if (DataSeries == null || DataSeries.Count() <= 0) return null;
            //  var ds = DataSeries.Clone() as DataSeries ;
            // Task.Delay(1000);
            var points = new List<Point>();
            DataSeries.ToList().ForEach(p =>
            {

                points.Add(Normalize(p));

            });
        
                
                //Console.WriteLine($"DrawLine:{Thread.CurrentThread.ManagedThreadId.ToString()}");
                var dv = new DrawingVisual();
               
                using (var dc = dv.RenderOpen())
                {
                var pen = new Pen(Brushes.Red, 1);
                    pen.Freeze();

                    for (int i = 1; i < points.Count(); i++)
                    {

                        dc.DrawLine(pen, points[i - 1], points[i]);
                    }
                }
                return dv;
        

         
          
        }

        public PolylineAsync()
        {
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
            Loaded += PolylineAsync_Loaded;
            SizeChanged += PolylineAsync_SizeChanged;
        }

        private  void PolylineAsync_SizeChanged(object sender, SizeChangedEventArgs e)
        {
           // Console.WriteLine($"{e.NewSize.Width},{e.NewSize.Height}");
            ResetScale();
            refresh();
        }

        private  void PolylineAsync_Loaded(object sender, RoutedEventArgs e)
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

        private  void refresh()
        {
            if (visuals.Count() > 0)
            {
                DeleteVisual(visuals[0]);
                
            }
            
          //  Console.WriteLine($"refresh:{Thread.CurrentThread.ManagedThreadId.ToString()}");
            _dv = DrawLine();
            if (_dv != null)
            {
               // Console.WriteLine($"refresh before add:{Thread.CurrentThread.ManagedThreadId.ToString()}");
                AddVisual(_dv);
            }
        }


        #region 依赖属性


        public Range HorizontalRange
        {
            get { return (Range)GetValue(HorizontalRangeProperty); }
            set { SetValue(HorizontalRangeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HorizontalRange.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HorizontalRangeProperty =
            DependencyProperty.Register("HorizontalRange", typeof(Range), typeof(PolylineAsync),
                new PropertyMetadata(default(Range), (s, e) => {

                    (s as PolylineAsync).ResetScale();
                }));



        public Range VerticalRange
        {
            get { return (Range)GetValue(VerticalRangeProperty); }
            set { SetValue(VerticalRangeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VerticalRange.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VerticalRangeProperty =
            DependencyProperty.Register("VerticalRange", typeof(Range), typeof(PolylineAsync), new PropertyMetadata(default(Range),
                (s, e) => {

                    (s as PolylineAsync).ResetScale();
                }));



        public DataSeries DataSeries
        {
            get { return (DataSeries)GetValue(DataSeriesProperty); }
            set { SetValue(DataSeriesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataSeries.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataSeriesProperty =
            DependencyProperty.Register("DataSeries", typeof(DataSeries), typeof(PolylineAsync), new FrameworkPropertyMetadata(default(DataSeries)));


        #endregion
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
