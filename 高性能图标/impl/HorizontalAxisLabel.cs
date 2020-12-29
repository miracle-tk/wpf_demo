using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace 高性能图标.impl
{
    public class HorizontalAxisLabel : AixsComponentBase
    {
        #region 方法
        protected override double Normalize(double v)
        {
            return (v - Range.Min) * kx;
        }

        protected override void Refresh()
        {

            if (!CanRender()) return;
            Root.Children.Clear();
            kx = RenderSize.Width / Range.Distance;
            var v = Range.Min;
            double x;
            while (v <= Range.Max)
            {
                x = Normalize(v);
                var label = new Label
                {
                    Content = v,

                };
                Root.Children.Add(label);
                Canvas.SetLeft(label, x);
                v += Step;
            }

        }
        protected override bool CanRender()
        {
            return base.CanRender() && (RenderSize.Width > 0);
        }
        #endregion

        #region 字段
        private double kx = 0;
        #endregion
    }
}
