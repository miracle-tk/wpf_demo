using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace 高性能图标.impl
{
    public class GridLineShape : Shape
    {
        protected override Geometry DefiningGeometry
        {
            get {
                
                double width = double.IsNaN(Width) ? RenderSize.Width:Width;
                double height = double.IsNaN(Height) ? RenderSize.Height : Height;
                if (width <= 0 || height <= 0 || HorizontalCount <= 1 || VerticalCount <= 1) return StreamGeometry.Empty;
                var stream = new StreamGeometry();
                using(var geo = stream.Open())
                {
                  
                    double offset = width / HorizontalCount;
                    int i = 1;
                    while (i < HorizontalCount)
                    {
                        geo.BeginFigure(new Point(i * offset, 0), false, false);
                        geo.LineTo(new Point(i * offset, height), true, false);
                        i++;
                    }
                    offset = height / VerticalCount;
                    i = 1;
                    while (i < VerticalCount)
                    {
                        geo.BeginFigure(new Point(0, i*offset), false, false);
                        geo.LineTo(new Point(width, i*offset), true, false);
                        i++;
                    }
                    
                }

                stream.Freeze();
                return stream;
            }
        }
        public GridLineShape()
        {
            SetCurrentValue(HorizontalCountProperty, 10d);
            SetCurrentValue(VerticalCountProperty, 10d);
            Loaded += GridLineShape_Loaded;
        }

        private void GridLineShape_Loaded(object sender, RoutedEventArgs e)
        {
            // if (!(Parent is Panel panel)) return;
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
        
        public double HorizontalCount
        {
            get { return (double)GetValue(HorizontalCountProperty); }
            set { SetValue(HorizontalCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HorizontalCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HorizontalCountProperty =
            DependencyProperty.Register("HorizontalCount", typeof(double), typeof(GridLineShape), new FrameworkPropertyMetadata(default(double),FrameworkPropertyMetadataOptions.AffectsRender));



        public double VerticalCount
        {
            get { return (double)GetValue(VerticalCountProperty); }
            set { SetValue(VerticalCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VerticalCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VerticalCountProperty =
            DependencyProperty.Register("VerticalCount", typeof(double), typeof(GridLineShape), new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.AffectsRender));


    }
}
