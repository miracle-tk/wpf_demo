using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using 高性能图标.Model;

namespace 高性能图标.impl
{
    public abstract class AixsComponentBase:UserControl
    {
        #region 构造器
        public AixsComponentBase()
        {
            Root = new Canvas();
            Content = Root;
            SetCurrentValue(RangeProperty, new Range(0d, 100d));
            SetCurrentValue(StepProperty, 10d);
            SizeChanged += (s, e) => { (s as AixsComponentBase).Refresh(); };
        }
        #endregion
        #region 依赖属性

        public Range Range
        {
            get { return (Range)GetValue(RangeProperty); }
            set { SetValue(RangeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Range.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RangeProperty =
            DependencyProperty.Register("Range", typeof(Range), typeof(AixsComponentBase), new PropertyMetadata(default(Range), OnParamChanged));


        // Using a DependencyProperty as the backing store for Max.  This enables animation, styling, binding, etc...
      


        public double Step
        {
            get { return (double)GetValue(StepProperty); }
            set { SetValue(StepProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Step.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StepProperty =
            DependencyProperty.Register("Step", typeof(double), typeof(AixsComponentBase), new PropertyMetadata(default(double), OnParamChanged));


        protected  static  void OnParamChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => (d as AixsComponentBase).Refresh();

        protected abstract void Refresh();
        protected virtual bool CanRender()
        {
            if( Step<=0||Range==null||Range.Distance<=0) return false;
            return true;
        }

        protected abstract double Normalize(double v);



        #endregion
        #region 字段
        protected Canvas Root;
        #endregion
    }
}
