using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace 高性能图标.impl
{
    public class VerticalAxisTick : AxisTick
    {
        #region 方法
        protected override double Normalize(double v)
        {
            if (ky <= 0) return 0;
            return RenderSize.Height- ky * (v-Range.Min);
        }

        protected override void Refresh()
        {
            if (!CanRender()) return;
            Root.Children.Clear();
            var v = Range.Min;
            ky = RenderSize.Height / Range.Distance;
            while (v <= Range.Max)
            {
                double y = Normalize(v);
                Root.Children.Add(new Line
                { 
                    X1=0,
                    Y1=y,
                    X2=LineLength,
                    Y2=y,
                    Stroke= LineBrush,
                    StrokeThickness=StrokeThickness
                });
                v += Step;
            }
        }
        protected override bool CanRender()
        {
            return base.CanRender() && (RenderSize.Height > 0);
        }
        #endregion
        #region 字段
        private double ky = 0;
        #endregion
    }
}
