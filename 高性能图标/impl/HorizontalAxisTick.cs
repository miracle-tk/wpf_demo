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
    public class HorizontalAxisTick : AxisTick
    {
        #region 构造器

        #endregion
        #region 依赖属性


        #endregion
        #region 事件    

        #endregion

        #region 方法
        protected override double Normalize(double v)
        {
            return (v - Range.Min) * kx;
        }

        protected override void Refresh()
        {
            if (!CanRender()) return;
            Root.Children.Clear();
            GC.Collect();
            kx = RenderSize.Width / (Range.Distance);
            var v = Range.Min;
            double x=0;
            while (v <= Range.Max)
            //while (x <= RenderSize.Width)
            {
               x = Normalize(v);
                Root.Children.Add(new Line
                {
                    X1 = x,
                    Y1 = 0,
                    X2 = x,
                    Y2 = LineLength,
                    Stroke = LineBrush,
                    StrokeThickness = StrokeThickness
                });
                v += Step;
                //x += kx;
            }

        }
        protected override bool CanRender()
        {
            return base.CanRender()&&(RenderSize.Width>0);
        }
        #endregion

        #region 属性
        #endregion
        #region 字段
        private double kx = 0;
        #endregion

    }
}

