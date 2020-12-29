using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace 高性能图标.impl
{
    public abstract class AxisTick : AixsComponentBase
    {
        #region 构造器
        public AxisTick() 
        {
            SetCurrentValue(StrokeThicknessProperty, 1d);
            SetCurrentValue(LineBrushProperty, Brushes.Chocolate);
            SetCurrentValue(LineLengthProperty, 10d);
        }
        #endregion
        #region 依赖属性
        public Brush LineBrush
        {
            get { return (Brush)GetValue(LineBrushProperty); }
            set { SetValue(LineBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LineBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineBrushProperty =
            DependencyProperty.Register("LineBrush", typeof(Brush), typeof(AxisTick), new PropertyMetadata(default(Brush), OnParamChanged));



        public double LineLength
        {
            get { return (double)GetValue(LineLengthProperty); }
            set { SetValue(LineLengthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LineLength.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineLengthProperty =
            DependencyProperty.Register("LineLength", typeof(double), typeof(AxisTick), new PropertyMetadata(default(double), OnParamChanged));



        public double StrokeThickness
        {
            get { return (double)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StrokeThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeThicknessProperty =
            DependencyProperty.Register("StrokeThickness", typeof(double), typeof(AxisTick), new PropertyMetadata(default(double), OnParamChanged));

        #endregion


    }
}
