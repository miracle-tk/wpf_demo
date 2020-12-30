using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using 高性能图标.Model;

namespace 高性能图标.impl
{
    public class PolyFigure : Shape
    {
        protected override Geometry DefiningGeometry {

            get {


                var stream = new StreamGeometry();


                return stream;

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

        
        #endregion
        #region 方法
        private void ResetScale()
        {

        }
        #endregion
        #region 字段
        private double _kx;
        private double _ky;
        private double _height;
        #endregion
    }
}
